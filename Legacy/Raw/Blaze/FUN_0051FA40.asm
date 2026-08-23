C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0051fa40  section .text  offset fichier 0x11ee40  5560 octets

  0051fa40  83ec68                sub       esp,68h
  0051fa43  53                    push      ebx
  0051fa44  55                    push      ebp
  0051fa45  56                    push      esi
  0051fa46  57                    push      edi
  0051fa47  68e0086000            push      6008E0h
  0051fa4c  e879600200            call      00545ACAh
  0051fa51  68e0086000            push      6008E0h
  0051fa56  e835610200            call      00545B90h
  0051fa5b  8b842488000000        mov       eax,[esp+88h]
  0051fa62  8b35d0086000          mov       esi,[6008D0h]
  0051fa68  8b15dc866000          mov       edx,[6086DCh]
  0051fa6e  8b08                  mov       ecx,[eax]
  0051fa70  03ce                  add       ecx,esi
  0051fa72  c1f90c                sar       ecx,0Ch
  0051fa75  66890a                mov       [edx],cx
  0051fa78  8b0dd4086000          mov       ecx,[6008D4h]
  0051fa7e  8b6804                mov       ebp,[eax+4]
  0051fa81  8b15dc866000          mov       edx,[6086DCh]
  0051fa87  2bcd                  sub       ecx,ebp
  0051fa89  c1f90c                sar       ecx,0Ch
  0051fa8c  66894a02              mov       [edx+2],cx
  0051fa90  8b4808                mov       ecx,[eax+8]
  0051fa93  8b3dd8086000          mov       edi,[6008D8h]
  0051fa99  8b15dc866000          mov       edx,[6086DCh]
  0051fa9f  03cf                  add       ecx,edi
  0051faa1  c1f90c                sar       ecx,0Ch
  0051faa4  66894a04              mov       [edx+4],cx
  0051faa8  668b4814              mov       cx,[eax+14h]
  0051faac  668b5016              mov       dx,[eax+16h]
  0051fab0  668b4018              mov       ax,[eax+18h]
  0051fab4  66030d28496000        add       cx,[604928h]
  0051fabb  6603052c496000        add       ax,[60492Ch]
  0051fac2  6603152a496000        add       dx,[60492Ah]
  0051fac9  66894c2450            mov       [esp+50h],cx
  0051face  8b0ddc866000          mov       ecx,[6086DCh]
  0051fad4  6689442454            mov       [esp+54h],ax
  0051fad9  a110886000            mov       eax,[608810h]
  0051fade  6689542452            mov       [esp+52h],dx
  0051fae3  0fbf5102              movsx     edx,word [ecx+2]
  0051fae7  0fbf7002              movsx     esi,word [eax+2]
  0051faeb  0fbf7804              movsx     edi,word [eax+4]
  0051faef  0fafd6                imul      edx,esi
  0051faf2  0fbf7104              movsx     esi,word [ecx+4]
  0051faf6  0faff7                imul      esi,edi
  0051faf9  0fbf09                movsx     ecx,word [ecx]
  0051fafc  03d6                  add       edx,esi
  0051fafe  8b6814                mov       ebp,[eax+14h]
  0051fb01  0fbf30                movsx     esi,word [eax]
  0051fb04  0faff1                imul      esi,ecx
  0051fb07  a1d4866000            mov       eax,[6086D4h]
  0051fb0c  03d6                  add       edx,esi
  0051fb0e  c1fa0c                sar       edx,0Ch
  0051fb11  03d5                  add       edx,ebp
  0051fb13  8910                  mov       [eax],edx
  0051fb15  8b0dd4866000          mov       ecx,[6086D4h]
  0051fb1b  a1d8866000            mov       eax,[6086D8h]
  0051fb20  668b11                mov       dx,[ecx]
  0051fb23  668910                mov       [eax],dx
  0051fb26  8b0ddc866000          mov       ecx,[6086DCh]
  0051fb2c  a110886000            mov       eax,[608810h]
  0051fb31  0fbf5102              movsx     edx,word [ecx+2]
  0051fb35  0fbf7008              movsx     esi,word [eax+8]
  0051fb39  0fbf780a              movsx     edi,word [eax+0Ah]
  0051fb3d  0fafd6                imul      edx,esi
  0051fb40  0fbf7104              movsx     esi,word [ecx+4]
  0051fb44  0faff7                imul      esi,edi
  0051fb47  0fbf09                movsx     ecx,word [ecx]
  0051fb4a  03d6                  add       edx,esi
  0051fb4c  0fbf7006              movsx     esi,word [eax+6]
  0051fb50  0faff1                imul      esi,ecx
  0051fb53  8b4818                mov       ecx,[eax+18h]
  0051fb56  a1d4866000            mov       eax,[6086D4h]
  0051fb5b  03d6                  add       edx,esi
  0051fb5d  c1fa0c                sar       edx,0Ch
  0051fb60  03d1                  add       edx,ecx
  0051fb62  895004                mov       [eax+4],edx
  0051fb65  8b0dd4866000          mov       ecx,[6086D4h]
  0051fb6b  a1d8866000            mov       eax,[6086D8h]
  0051fb70  668b5104              mov       dx,[ecx+4]
  0051fb74  66895002              mov       [eax+2],dx
  0051fb78  8b0ddc866000          mov       ecx,[6086DCh]
  0051fb7e  a110886000            mov       eax,[608810h]
  0051fb83  0fbf5102              movsx     edx,word [ecx+2]
  0051fb87  0fbf700e              movsx     esi,word [eax+0Eh]
  0051fb8b  0fbf7810              movsx     edi,word [eax+10h]
  0051fb8f  0fafd6                imul      edx,esi
  0051fb92  0fbf7104              movsx     esi,word [ecx+4]
  0051fb96  0faff7                imul      esi,edi
  0051fb99  0fbf09                movsx     ecx,word [ecx]
  0051fb9c  03d6                  add       edx,esi
  0051fb9e  0fbf700c              movsx     esi,word [eax+0Ch]
  0051fba2  0faff1                imul      esi,ecx
  0051fba5  03d6                  add       edx,esi
  0051fba7  8b701c                mov       esi,[eax+1Ch]
  0051fbaa  a1d4866000            mov       eax,[6086D4h]
  0051fbaf  c1fa0c                sar       edx,0Ch
  0051fbb2  03d6                  add       edx,esi
  0051fbb4  895008                mov       [eax+8],edx
  0051fbb7  8b0dd4866000          mov       ecx,[6086D4h]
  0051fbbd  a1d8866000            mov       eax,[6086D8h]
  0051fbc2  668b5108              mov       dx,[ecx+8]
  0051fbc6  66895004              mov       [eax+4],dx
  0051fbca  8b0dd4866000          mov       ecx,[6086D4h]
  0051fbd0  a110886000            mov       eax,[608810h]
  0051fbd5  8b11                  mov       edx,[ecx]
  0051fbd7  895014                mov       [eax+14h],edx
  0051fbda  8b0dd4866000          mov       ecx,[6086D4h]
  0051fbe0  a110886000            mov       eax,[608810h]
  0051fbe5  8b5104                mov       edx,[ecx+4]
  0051fbe8  895018                mov       [eax+18h],edx
  0051fbeb  8b0dd4866000          mov       ecx,[6086D4h]
  0051fbf1  a110886000            mov       eax,[608810h]
  0051fbf6  8b5108                mov       edx,[ecx+8]
  0051fbf9  8d4c2458              lea       ecx,[esp+58h]
  0051fbfd  89501c                mov       [eax+1Ch],edx
  0051fc00  8d542450              lea       edx,[esp+50h]
  0051fc04  51                    push      ecx
  0051fc05  52                    push      edx
  0051fc06  e8f5c6ffff            call      0051C300h
  0051fc0b  a110886000            mov       eax,[608810h]
  0051fc10  8b4c2460              mov       ecx,[esp+60h]
  0051fc14  83c408                add       esp,8
  0051fc17  8908                  mov       [eax],ecx
  0051fc19  8b54245c              mov       edx,[esp+5Ch]
  0051fc1d  895004                mov       [eax+4],edx
  0051fc20  8b4c2460              mov       ecx,[esp+60h]
  0051fc24  894808                mov       [eax+8],ecx
  0051fc27  8b542464              mov       edx,[esp+64h]
  0051fc2b  89500c                mov       [eax+0Ch],edx
  0051fc2e  668b4c2468            mov       cx,[esp+68h]
  0051fc33  66894810              mov       [eax+10h],cx
  0051fc37  668b1508935e00        mov       dx,[5E9308h]
  0051fc3e  66a10a935e00          mov       ax,[5E930Ah]
  0051fc44  668b0d0c935e00        mov       cx,[5E930Ch]
  0051fc4b  6601442452            add       [esp+52h],ax
  0051fc50  66014c2454            add       [esp+54h],cx
  0051fc55  6689542450            mov       [esp+50h],dx
  0051fc5a  8d542458              lea       edx,[esp+58h]
  0051fc5e  8d442450              lea       eax,[esp+50h]
  0051fc62  52                    push      edx
  0051fc63  50                    push      eax
  0051fc64  e897c6ffff            call      0051C300h
  0051fc69  8b3df8866000          mov       edi,[6086F8h]
  0051fc6f  b908000000            mov       ecx,8
  0051fc74  8d742460              lea       esi,[esp+60h]
  0051fc78  83c408                add       esp,8
  0051fc7b  f3a5                  rep movsd
  0051fc7d  8bb42484000000        mov       esi,[esp+84h]
  0051fc84  8b0d484a5600          mov       ecx,[564A48h]
  0051fc8a  85c9                  test      ecx,ecx
  0051fc8c  8b463c                mov       eax,[esi+3Ch]
  0051fc8f  7421                  je        short 0051FCB2h
  0051fc91  f6c402                test      ah,2
  0051fc94  741c                  je        short 0051FCB2h
  0051fc96  8b0d4c4a5600          mov       ecx,[564A4Ch]
  0051fc9c  85c9                  test      ecx,ecx
  0051fc9e  750f                  jne       short 0051FCAFh
  0051fca0  8b842480000000        mov       eax,[esp+80h]
  0051fca7  5f                    pop       edi
  0051fca8  5e                    pop       esi
  0051fca9  5d                    pop       ebp
  0051fcaa  5b                    pop       ebx
  0051fcab  83c468                add       esp,68h
  0051fcae  c3                    ret

LAB_0051fcaf:
  0051fcaf  80e4fd                and       ah,0FDh

LAB_0051fcb2:
  0051fcb2  c1e810                shr       eax,10h
  0051fcb5  25ff000000            and       eax,0FFh
  0051fcba  8b0485008f5e00        mov       eax,[eax*4+5E8F00h]
  0051fcc1  8bc8                  mov       ecx,eax
  0051fcc3  250000ffff            and       eax,0FFFF0000h
  0051fcc8  80cd08                or        ch,8
  0051fccb  8944243c              mov       [esp+3Ch],eax
  0051fccf  8b4608                mov       eax,[esi+8]
  0051fcd2  c1e110                shl       ecx,10h
  0051fcd5  894c2440              mov       [esp+40h],ecx
  0051fcd9  8b0db4876000          mov       ecx,[6087B4h]
  0051fcdf  8bf8                  mov       edi,eax
  0051fce1  25ffff0000            and       eax,0FFFFh
  0051fce6  8b09                  mov       ecx,[ecx]
  0051fce8  50                    push      eax
  0051fce9  51                    push      ecx
  0051fcea  8d4640                lea       eax,[esi+40h]
  0051fced  8d9100401b00          lea       edx,[ecx+1B4000h]
  0051fcf3  81c100001b00          add       ecx,1B0000h
  0051fcf9  52                    push      edx
  0051fcfa  51                    push      ecx
  0051fcfb  50                    push      eax
  0051fcfc  c1ff10                sar       edi,10h
  0051fcff  e8925e0200            call      00545B96h
  0051fd04  8b0d00876000          mov       ecx,[608700h]
  0051fd0a  57                    push      edi
  0051fd0b  c701ffffff00          mov       dword [ecx],0FFFFFFh
  0051fd11  8b15b4876000          mov       edx,[6087B4h]
  0051fd17  8b4e24                mov       ecx,[esi+24h]
  0051fd1a  8b02                  mov       eax,[edx]
  0051fd1c  0500801b00            add       eax,1B8000h
  0051fd21  50                    push      eax
  0051fd22  51                    push      ecx
  0051fd23  e85c5e0200            call      00545B84h
  0051fd28  8b4634                mov       eax,[esi+34h]
  0051fd2b  8b5628                mov       edx,[esi+28h]
  0051fd2e  8944244c              mov       [esp+4Ch],eax
  0051fd32  66a12a496000          mov       ax,[60492Ah]
  0051fd38  0500010000            add       eax,100h
  0051fd3d  89542438              mov       [esp+38h],edx
  0051fd41  c1f809                sar       eax,9
  0051fd44  83e007                and       eax,7
  0051fd47  8bb42480000000        mov       esi,[esp+80h]
  0051fd4e  0fbfc8                movsx     ecx,ax
  0051fd51  6689442452            mov       [esp+52h],ax
  0051fd56  33c0                  xor       eax,eax
  0051fd58  668b044d48455600      mov       ax,[ecx*2+564548h]
  0051fd60  8b0c8dd8455600        mov       ecx,[ecx*4+5645D8h]
  0051fd67  668b11                mov       dx,[ecx]
  0051fd6a  83c102                add       ecx,2
  0051fd6d  894c2448              mov       [esp+48h],ecx
  0051fd71  8b0dd0086000          mov       ecx,[6008D0h]
  0051fd77  f7d9                  neg       ecx
  0051fd79  c1f90c                sar       ecx,0Ch
  0051fd7c  6689542456            mov       [esp+56h],dx
  0051fd81  81c100200000          add       ecx,2000h
  0051fd87  6633d2                xor       dx,dx
  0051fd8a  66c74424600700        mov       word [esp+60h],7
  0051fd91  c1f909                sar       ecx,9
  0051fd94  8ad0                  mov       dl,al
  0051fd96  03ca                  add       ecx,edx
  0051fd98  8b15d8086000          mov       edx,[6008D8h]
  0051fd9e  f7da                  neg       edx
  0051fda0  c1fa0c                sar       edx,0Ch
  0051fda3  81c200200000          add       edx,2000h
  0051fda9  66894c2458            mov       [esp+58h],cx
  0051fdae  c1fa09                sar       edx,9
  0051fdb1  c1f808                sar       eax,8
  0051fdb4  03d0                  add       edx,eax
  0051fdb6  668954245a            mov       [esp+5Ah],dx

LAB_0051fdbb:
  0051fdbb  66b80700              mov       ax,7
  0051fdbf  668944245e            mov       [esp+5Eh],ax
  0051fdc4  eb05                  jmp       short 0051FDCBh

LAB_0051fdc6:
  0051fdc6  668b54245a            mov       dx,[esp+5Ah]

LAB_0051fdcb:
  0051fdcb  0fbf7c2460            movsx     edi,word [esp+60h]
  0051fdd0  0fbfc0                movsx     eax,ax
  0051fdd3  c1e708                shl       edi,8
  0051fdd6  0bf8                  or        edi,eax
  0051fdd8  0fbf442456            movsx     eax,word [esp+56h]
  0051fddd  3bc7                  cmp       eax,edi
  0051fddf  7518                  jne       short 0051FDF9h
  0051fde1  8b442448              mov       eax,[esp+48h]
  0051fde5  668b08                mov       cx,[eax]
  0051fde8  83c002                add       eax,2
  0051fdeb  66894c2456            mov       [esp+56h],cx
  0051fdf0  89442448              mov       [esp+48h],eax
  0051fdf4  e9850c0000            jmp       00520A7Eh

LAB_0051fdf9:
  0051fdf9  6683f920              cmp       cx,20h
  0051fdfd  0f837b0c0000          jae       near 00520A7Eh
  0051fe03  6683fa20              cmp       dx,20h
  0051fe07  0f83710c0000          jae       near 00520A7Eh
  0051fe0d  0fbfd2                movsx     edx,dx
  0051fe10  0fbfc1                movsx     eax,cx
  0051fe13  8b4c244c              mov       ecx,[esp+4Ch]
  0051fe17  c1e205                shl       edx,5
  0051fe1a  03d0                  add       edx,eax
  0051fe1c  8b0c91                mov       ecx,[ecx+edx*4]
  0051fe1f  8b01                  mov       eax,[ecx]
  0051fe21  83c104                add       ecx,4
  0051fe24  8944246c              mov       [esp+6Ch],eax
  0051fe28  8bf8                  mov       edi,eax
  0051fe2a  8b11                  mov       edx,[ecx]
  0051fe2c  0bfa                  or        edi,edx
  0051fe2e  89542470              mov       [esp+70h],edx
  0051fe32  0f84460c0000          je        near 00520A7Eh
  0051fe38  8bf8                  mov       edi,eax
  0051fe3a  8bd8                  mov       ebx,eax
  0051fe3c  c1ff18                sar       edi,18h
  0051fe3f  c1fb10                sar       ebx,10h
  0051fe42  81e7ff000000          and       edi,0FFh
  0051fe48  81e3ff000000          and       ebx,0FFh
  0051fe4e  03fb                  add       edi,ebx
  0051fe50  8bd8                  mov       ebx,eax
  0051fe52  c1fb08                sar       ebx,8
  0051fe55  81e3ff000000          and       ebx,0FFh
  0051fe5b  25ff000000            and       eax,0FFh
  0051fe60  03fb                  add       edi,ebx
  0051fe62  03f8                  add       edi,eax
  0051fe64  8d447904              lea       eax,[ecx+edi*2+4]
  0051fe68  89442410              mov       [esp+10h],eax
  0051fe6c  8bc2                  mov       eax,edx
  0051fe6e  25ff000000            and       eax,0FFh
  0051fe73  89442420              mov       [esp+20h],eax
  0051fe77  0f8493020000          je        near 00520110h

LAB_0051fe7d:
  0051fe7d  a118985e00            mov       eax,[5E9818h]
  0051fe82  85c0                  test      eax,eax
  0051fe84  0f8486020000          je        near 00520110h
  0051fe8a  8b4c2410              mov       ecx,[esp+10h]
  0051fe8e  8b542438              mov       edx,[esp+38h]
  0051fe92  33ff                  xor       edi,edi
  0051fe94  668b39                mov       di,[ecx]
  0051fe97  c1e705                shl       edi,5
  0051fe9a  03fa                  add       edi,edx
  0051fe9c  8b15b4876000          mov       edx,[6087B4h]
  0051fea2  8b02                  mov       eax,[edx]
  0051fea4  8b17                  mov       edx,[edi]
  0051fea6  8b4f04                mov       ecx,[edi+4]
  0051fea9  8bda                  mov       ebx,edx
  0051feab  0500001b00            add       eax,1B0000h
  0051feb0  81e3ffff0000          and       ebx,0FFFFh
  0051feb6  c1ea0e                shr       edx,0Eh
  0051feb9  89542430              mov       [esp+30h],edx
  0051febd  81e1ffff0000          and       ecx,0FFFFh
  0051fec3  8b1410                mov       edx,[eax+edx]
  0051fec6  c1e302                shl       ebx,2
  0051fec9  89942480000000        mov       [esp+80h],edx
  0051fed0  8b150c876000          mov       edx,[60870Ch]
  0051fed6  8b2c18                mov       ebp,[eax+ebx]
  0051fed9  c1e102                shl       ecx,2
  0051fedc  892a                  mov       [edx],ebp
  0051fede  8b1508876000          mov       edx,[608708h]
  0051fee4  8b0408                mov       eax,[eax+ecx]
  0051fee7  894c2434              mov       [esp+34h],ecx
  0051feeb  8b8c2480000000        mov       ecx,[esp+80h]
  0051fef2  6681fd4001            cmp       bp,140h
  0051fef7  890a                  mov       [edx],ecx
  0051fef9  8b1510876000          mov       edx,[608710h]
  0051feff  89ac2488000000        mov       [esp+88h],ebp
  0051ff06  89842484000000        mov       [esp+84h],eax
  0051ff0d  8902                  mov       [edx],eax
  0051ff0f  7211                  jb        short 0051FF22h
  0051ff11  6681f94001            cmp       cx,140h
  0051ff16  720a                  jb        short 0051FF22h
  0051ff18  663d4001              cmp       ax,140h
  0051ff1c  0f83d2010000          jae       near 005200F4h

LAB_0051ff22:
  0051ff22  8b0d08876000          mov       ecx,[608708h]
  0051ff28  a110876000            mov       eax,[608710h]
  0051ff2d  0fbf11                movsx     edx,word [ecx]
  0051ff30  0fbf4902              movsx     ecx,word [ecx+2]
  0051ff34  0fbf6802              movsx     ebp,word [eax+2]
  0051ff38  0fbf00                movsx     eax,word [eax]
  0051ff3b  89542414              mov       [esp+14h],edx
  0051ff3f  8b150c876000          mov       edx,[60870Ch]
  0051ff45  894c241c              mov       [esp+1Ch],ecx
  0051ff49  89442418              mov       [esp+18h],eax
  0051ff4d  0fbf0a                movsx     ecx,word [edx]
  0051ff50  0fbf5202              movsx     edx,word [edx+2]
  0051ff54  8b442414              mov       eax,[esp+14h]
  0051ff58  89542428              mov       [esp+28h],edx
  0051ff5c  8bd1                  mov       edx,ecx
  0051ff5e  0fafc5                imul      eax,ebp
  0051ff61  0faf54241c            imul      edx,[esp+1Ch]
  0051ff66  896c242c              mov       [esp+2Ch],ebp
  0051ff6a  8b6c2418              mov       ebp,[esp+18h]
  0051ff6e  03d0                  add       edx,eax
  0051ff70  8b442428              mov       eax,[esp+28h]
  0051ff74  0fafe8                imul      ebp,eax
  0051ff77  0faf442414            imul      eax,[esp+14h]
  0051ff7c  0faf4c242c            imul      ecx,[esp+2Ch]
  0051ff81  03d5                  add       edx,ebp
  0051ff83  2bd0                  sub       edx,eax
  0051ff85  8b442418              mov       eax,[esp+18h]
  0051ff89  0faf44241c            imul      eax,[esp+1Ch]
  0051ff8e  2bd1                  sub       edx,ecx
  0051ff90  8b0d14876000          mov       ecx,[608714h]
  0051ff96  2bd0                  sub       edx,eax
  0051ff98  8911                  mov       [ecx],edx
  0051ff9a  8b942488000000        mov       edx,[esp+88h]
  0051ffa1  81e20000ffff          and       edx,0FFFF0000h
  0051ffa7  81fa0000f000          cmp       edx,0F00000h
  0051ffad  722c                  jb        short 0051FFDBh
  0051ffaf  8b842480000000        mov       eax,[esp+80h]
  0051ffb6  250000ffff            and       eax,0FFFF0000h
  0051ffbb  3d0000f000            cmp       eax,0F00000h
  0051ffc0  7219                  jb        short 0051FFDBh
  0051ffc2  8b8c2484000000        mov       ecx,[esp+84h]
  0051ffc9  81e10000ffff          and       ecx,0FFFF0000h
  0051ffcf  81f90000f000          cmp       ecx,0F00000h
  0051ffd5  0f8319010000          jae       near 005200F4h

LAB_0051ffdb:
  0051ffdb  e810c3ffff            call      0051C2F0h
  0051ffe0  85c0                  test      eax,eax
  0051ffe2  0f8d0c010000          jge       near 005200F4h
  0051ffe8  8b15b4876000          mov       edx,[6087B4h]
  0051ffee  8b442430              mov       eax,[esp+30h]
  0051fff2  8b2a                  mov       ebp,[edx]
  0051fff4  8b542434              mov       edx,[esp+34h]
  0051fff8  8b8c1d00401b00        mov       ecx,[ebp+ebx+1B4000h]
  0051ffff  8b840500401b00        mov       eax,[ebp+eax+1B4000h]
  00520006  8b941500401b00        mov       edx,[ebp+edx+1B4000h]
  0052000d  3bc1                  cmp       eax,ecx
  0052000f  7e02                  jle       short 00520013h
  00520011  8bc8                  mov       ecx,eax

LAB_00520013:
  00520013  3bd1                  cmp       edx,ecx
  00520015  7e02                  jle       short 00520019h
  00520017  8bca                  mov       ecx,edx

LAB_00520019:
  00520019  8d81c0feffff          lea       eax,[ecx-140h]
  0052001f  3dc00e0000            cmp       eax,0EC0h
  00520024  0f83ca000000          jae       near 005200F4h
  0052002a  8b4708                mov       eax,[edi+8]
  0052002d  8b5710                mov       edx,[edi+10h]
  00520030  8b5f14                mov       ebx,[edi+14h]
  00520033  25ffff0000            and       eax,0FFFFh
  00520038  89442444              mov       [esp+44h],eax
  0052003c  8b471c                mov       eax,[edi+1Ch]
  0052003f  8b3d484a5600          mov       edi,[564A48h]
  00520045  85ff                  test      edi,edi
  00520047  741a                  je        short 00520063h
  00520049  a900000002            test      eax,2000000h
  0052004e  7413                  je        short 00520063h
  00520050  8b3d4c4a5600          mov       edi,[564A4Ch]
  00520056  85ff                  test      edi,edi
  00520058  0f8496000000          je        near 005200F4h
  0052005e  25fffffffd            and       eax,0FDFFFFFFh

LAB_00520063:
  00520063  8b7c2444              mov       edi,[esp+44h]
  00520067  81e1fcff0000          and       ecx,0FFFCh
  0052006d  81e3ffff0000          and       ebx,0FFFFh
  00520073  8bacbd00801b00        mov       ebp,[ebp+edi*4+1B8000h]
  0052007a  8b7c247c              mov       edi,[esp+7Ch]
  0052007e  03cf                  add       ecx,edi
  00520080  8b39                  mov       edi,[ecx]
  00520082  8931                  mov       [ecx],esi
  00520084  8b8c2488000000        mov       ecx,[esp+88h]
  0052008b  81cf00000007          or        edi,7000000h
  00520091  894e08                mov       [esi+8],ecx
  00520094  8b8c2484000000        mov       ecx,[esp+84h]
  0052009b  894e10                mov       [esi+10h],ecx
  0052009e  8b8c2480000000        mov       ecx,[esp+80h]
  005200a5  894e18                mov       [esi+18h],ecx
  005200a8  8bc8                  mov       ecx,eax
  005200aa  81e1000000ff          and       ecx,0FF000000h
  005200b0  893e                  mov       [esi],edi
  005200b2  8b7c243c              mov       edi,[esp+3Ch]
  005200b6  0bcd                  or        ecx,ebp
  005200b8  894e04                mov       [esi+4],ecx
  005200bb  8bc8                  mov       ecx,eax
  005200bd  81e1f0fc0000          and       ecx,0FCF0h
  005200c3  250000ef00            and       eax,0EF0000h
  005200c8  c1e10c                shl       ecx,0Ch
  005200cb  03cf                  add       ecx,edi
  005200cd  8bfa                  mov       edi,edx
  005200cf  81e7ffff0000          and       edi,0FFFFh
  005200d5  83c620                add       esi,20h
  005200d8  0bcf                  or        ecx,edi
  005200da  8b7c2440              mov       edi,[esp+40h]
  005200de  03c7                  add       eax,edi
  005200e0  894eec                mov       [esi-14h],ecx
  005200e3  0bc3                  or        eax,ebx
  005200e5  c1ea10                shr       edx,10h
  005200e8  8946f4                mov       [esi-0Ch],eax
  005200eb  8956fc                mov       [esi-4],edx
  005200ee  ff0d18985e00          dec       dword [5E9818h]

LAB_005200f4:
  005200f4  8b442420              mov       eax,[esp+20h]
  005200f8  8b542410              mov       edx,[esp+10h]
  005200fc  48                    dec       eax
  005200fd  83c202                add       edx,2
  00520100  85c0                  test      eax,eax
  00520102  89442420              mov       [esp+20h],eax
  00520106  89542410              mov       [esp+10h],edx
  0052010a  0f856dfdffff          jne       near 0051FE7Dh

LAB_00520110:
  00520110  8b442470              mov       eax,[esp+70h]
  00520114  c1f808                sar       eax,8
  00520117  25ff000000            and       eax,0FFh
  0052011c  89442420              mov       [esp+20h],eax
  00520120  0f8413030000          je        near 00520439h

LAB_00520126:
  00520126  a118985e00            mov       eax,[5E9818h]
  0052012b  85c0                  test      eax,eax
  0052012d  0f8406030000          je        near 00520439h
  00520133  8b542410              mov       edx,[esp+10h]
  00520137  33ff                  xor       edi,edi
  00520139  a1b4876000            mov       eax,[6087B4h]
  0052013e  8b2d0c876000          mov       ebp,[60870Ch]
  00520144  668b3a                mov       di,[edx]
  00520147  8b542438              mov       edx,[esp+38h]
  0052014b  c1e705                shl       edi,5
  0052014e  8b00                  mov       eax,[eax]
  00520150  03fa                  add       edi,edx
  00520152  0500001b00            add       eax,1B0000h
  00520157  8b0f                  mov       ecx,[edi]
  00520159  8b5704                mov       edx,[edi+4]
  0052015c  8bd9                  mov       ebx,ecx
  0052015e  8954242c              mov       [esp+2Ch],edx
  00520162  81e3ffff0000          and       ebx,0FFFFh
  00520168  c1e302                shl       ebx,2
  0052016b  c1e90e                shr       ecx,0Eh
  0052016e  8b1418                mov       edx,[eax+ebx]
  00520171  894c2430              mov       [esp+30h],ecx
  00520175  8b0c08                mov       ecx,[eax+ecx]
  00520178  895500                mov       [ebp],edx
  0052017b  8b6c242c              mov       ebp,[esp+2Ch]
  0052017f  89942488000000        mov       [esp+88h],edx
  00520186  81e5ffff0000          and       ebp,0FFFFh
  0052018c  898c2480000000        mov       [esp+80h],ecx
  00520193  c1e502                shl       ebp,2
  00520196  8b1428                mov       edx,[eax+ebp]
  00520199  89942484000000        mov       [esp+84h],edx
  005201a0  8b1508876000          mov       edx,[608708h]
  005201a6  890a                  mov       [edx],ecx
  005201a8  8b54242c              mov       edx,[esp+2Ch]
  005201ac  c1ea0e                shr       edx,0Eh
  005201af  89542444              mov       [esp+44h],edx
  005201b3  8b0410                mov       eax,[eax+edx]
  005201b6  8b1510876000          mov       edx,[608710h]
  005201bc  89442424              mov       [esp+24h],eax
  005201c0  8b842484000000        mov       eax,[esp+84h]
  005201c7  8902                  mov       [edx],eax
  005201c9  ba40010000            mov       edx,140h
  005201ce  6639942488000000      cmp       [esp+88h],dx
  005201d6  7215                  jb        short 005201EDh
  005201d8  663bca                cmp       cx,dx
  005201db  7210                  jb        short 005201EDh
  005201dd  663bc2                cmp       ax,dx
  005201e0  720b                  jb        short 005201EDh
  005201e2  6639542424            cmp       [esp+24h],dx
  005201e7  0f8330020000          jae       near 0052041Dh

LAB_005201ed:
  005201ed  8b1508876000          mov       edx,[608708h]
  005201f3  a110876000            mov       eax,[608710h]
  005201f8  0fbf12                movsx     edx,word [edx]
  005201fb  89542414              mov       [esp+14h],edx
  005201ff  8b1508876000          mov       edx,[608708h]
  00520205  0fbf4802              movsx     ecx,word [eax+2]
  00520209  0fbf5202              movsx     edx,word [edx+2]
  0052020d  8954241c              mov       [esp+1Ch],edx
  00520211  8b150c876000          mov       edx,[60870Ch]
  00520217  0fbf00                movsx     eax,word [eax]
  0052021a  0fbf12                movsx     edx,word [edx]
  0052021d  89542434              mov       [esp+34h],edx
  00520221  8b150c876000          mov       edx,[60870Ch]
  00520227  89442418              mov       [esp+18h],eax
  0052022b  8b442414              mov       eax,[esp+14h]
  0052022f  0fbf5202              movsx     edx,word [edx+2]
  00520233  0fafc1                imul      eax,ecx
  00520236  89542428              mov       [esp+28h],edx
  0052023a  8b542434              mov       edx,[esp+34h]
  0052023e  0faf54241c            imul      edx,[esp+1Ch]
  00520243  894c242c              mov       [esp+2Ch],ecx
  00520247  8b4c2418              mov       ecx,[esp+18h]
  0052024b  03d0                  add       edx,eax
  0052024d  8b442428              mov       eax,[esp+28h]
  00520251  0fafc8                imul      ecx,eax
  00520254  0faf442414            imul      eax,[esp+14h]
  00520259  03d1                  add       edx,ecx
  0052025b  8b8c2488000000        mov       ecx,[esp+88h]
  00520262  2bd0                  sub       edx,eax
  00520264  8b442434              mov       eax,[esp+34h]
  00520268  0faf44242c            imul      eax,[esp+2Ch]
  0052026d  2bd0                  sub       edx,eax
  0052026f  8b442418              mov       eax,[esp+18h]
  00520273  0faf44241c            imul      eax,[esp+1Ch]
  00520278  2bd0                  sub       edx,eax
  0052027a  a114876000            mov       eax,[608714h]
  0052027f  81e10000ffff          and       ecx,0FFFF0000h
  00520285  81f90000f000          cmp       ecx,0F00000h
  0052028b  8910                  mov       [eax],edx
  0052028d  723e                  jb        short 005202CDh
  0052028f  8b942480000000        mov       edx,[esp+80h]
  00520296  81e20000ffff          and       edx,0FFFF0000h
  0052029c  81fa0000f000          cmp       edx,0F00000h
  005202a2  7229                  jb        short 005202CDh
  005202a4  8b842484000000        mov       eax,[esp+84h]
  005202ab  250000ffff            and       eax,0FFFF0000h
  005202b0  3d0000f000            cmp       eax,0F00000h
  005202b5  7216                  jb        short 005202CDh
  005202b7  8b4c2424              mov       ecx,[esp+24h]
  005202bb  81e10000ffff          and       ecx,0FFFF0000h
  005202c1  81f90000f000          cmp       ecx,0F00000h
  005202c7  0f8350010000          jae       near 0052041Dh

LAB_005202cd:
  005202cd  e81ec0ffff            call      0051C2F0h
  005202d2  85c0                  test      eax,eax
  005202d4  7c19                  jl        short 005202EFh
  005202d6  8b1510876000          mov       edx,[608710h]
  005202dc  8b442424              mov       eax,[esp+24h]
  005202e0  8902                  mov       [edx],eax
  005202e2  e899bfffff            call      0051C280h
  005202e7  85c0                  test      eax,eax
  005202e9  0f8d2e010000          jge       near 0052041Dh

LAB_005202ef:
  005202ef  8b0db4876000          mov       ecx,[6087B4h]
  005202f5  8b542430              mov       edx,[esp+30h]
  005202f9  8b01                  mov       eax,[ecx]
  005202fb  89442434              mov       [esp+34h],eax
  005202ff  8b8c1800401b00        mov       ecx,[eax+ebx+1B4000h]
  00520306  8b941000401b00        mov       edx,[eax+edx+1B4000h]
  0052030d  8b5c2444              mov       ebx,[esp+44h]
  00520311  8bac2800401b00        mov       ebp,[eax+ebp+1B4000h]
  00520318  3bd1                  cmp       edx,ecx
  0052031a  8b841800401b00        mov       eax,[eax+ebx+1B4000h]
  00520321  7e02                  jle       short 00520325h
  00520323  8bca                  mov       ecx,edx

LAB_00520325:
  00520325  3be9                  cmp       ebp,ecx
  00520327  7e02                  jle       short 0052032Bh
  00520329  8bcd                  mov       ecx,ebp

LAB_0052032b:
  0052032b  3bc1                  cmp       eax,ecx
  0052032d  7e02                  jle       short 00520331h
  0052032f  8bc8                  mov       ecx,eax

LAB_00520331:
  00520331  8d81c0feffff          lea       eax,[ecx-140h]
  00520337  3dc00e0000            cmp       eax,0EC0h
  0052033c  0f83db000000          jae       near 0052041Dh
  00520342  8b6f08                mov       ebp,[edi+8]
  00520345  8b5710                mov       edx,[edi+10h]
  00520348  8b5f14                mov       ebx,[edi+14h]
  0052034b  8b471c                mov       eax,[edi+1Ch]
  0052034e  8b3d484a5600          mov       edi,[564A48h]
  00520354  81e5ffff0000          and       ebp,0FFFFh
  0052035a  85ff                  test      edi,edi
  0052035c  741a                  je        short 00520378h
  0052035e  a900000002            test      eax,2000000h
  00520363  7413                  je        short 00520378h
  00520365  8b3d4c4a5600          mov       edi,[564A4Ch]
  0052036b  85ff                  test      edi,edi
  0052036d  0f84aa000000          je        near 0052041Dh
  00520373  25fffffffd            and       eax,0FDFFFFFFh

LAB_00520378:
  00520378  8b7c2434              mov       edi,[esp+34h]
  0052037c  81e1fcff0000          and       ecx,0FFFCh
  00520382  8bacaf00801b00        mov       ebp,[edi+ebp*4+1B8000h]
  00520389  8b7c247c              mov       edi,[esp+7Ch]
  0052038d  03cf                  add       ecx,edi
  0052038f  8b39                  mov       edi,[ecx]
  00520391  8931                  mov       [ecx],esi
  00520393  8b8c2480000000        mov       ecx,[esp+80h]
  0052039a  81cf00000009          or        edi,9000000h
  005203a0  894e08                mov       [esi+8],ecx
  005203a3  8b8c2488000000        mov       ecx,[esp+88h]
  005203aa  894e10                mov       [esi+10h],ecx
  005203ad  8b8c2484000000        mov       ecx,[esp+84h]
  005203b4  894e18                mov       [esi+18h],ecx
  005203b7  8b4c2424              mov       ecx,[esp+24h]
  005203bb  894e20                mov       [esi+20h],ecx
  005203be  8bc8                  mov       ecx,eax
  005203c0  81e1000000ff          and       ecx,0FF000000h
  005203c6  893e                  mov       [esi],edi
  005203c8  8b7c243c              mov       edi,[esp+3Ch]
  005203cc  0bcd                  or        ecx,ebp
  005203ce  894e04                mov       [esi+4],ecx
  005203d1  8bc8                  mov       ecx,eax
  005203d3  81e1f0fc0000          and       ecx,0FCF0h
  005203d9  250000ef00            and       eax,0EF0000h
  005203de  c1e10c                shl       ecx,0Ch
  005203e1  03cf                  add       ecx,edi
  005203e3  8bfa                  mov       edi,edx
  005203e5  c1ef10                shr       edi,10h
  005203e8  0bcf                  or        ecx,edi
  005203ea  8b7c2440              mov       edi,[esp+40h]
  005203ee  03c7                  add       eax,edi
  005203f0  81e2ffff0000          and       edx,0FFFFh
  005203f6  0bc2                  or        eax,edx
  005203f8  8bd3                  mov       edx,ebx
  005203fa  81e2ffff0000          and       edx,0FFFFh
  00520400  894e0c                mov       [esi+0Ch],ecx
  00520403  c1eb10                shr       ebx,10h
  00520406  894614                mov       [esi+14h],eax
  00520409  89561c                mov       [esi+1Ch],edx
  0052040c  895e24                mov       [esi+24h],ebx
  0052040f  a118985e00            mov       eax,[5E9818h]
  00520414  83c628                add       esi,28h
  00520417  48                    dec       eax
  00520418  a318985e00            mov       [5E9818h],eax

LAB_0052041d:
  0052041d  8b442420              mov       eax,[esp+20h]
  00520421  8b542410              mov       edx,[esp+10h]
  00520425  48                    dec       eax
  00520426  83c202                add       edx,2
  00520429  85c0                  test      eax,eax
  0052042b  89442420              mov       [esp+20h],eax
  0052042f  89542410              mov       [esp+10h],edx
  00520433  0f85edfcffff          jne       near 00520126h

LAB_00520439:
  00520439  8b442470              mov       eax,[esp+70h]
  0052043d  c1f810                sar       eax,10h
  00520440  25ff000000            and       eax,0FFh
  00520445  89442420              mov       [esp+20h],eax
  00520449  0f84ca020000          je        near 00520719h

LAB_0052044f:
  0052044f  a118985e00            mov       eax,[5E9818h]
  00520454  85c0                  test      eax,eax
  00520456  0f84bd020000          je        near 00520719h
  0052045c  8b442410              mov       eax,[esp+10h]
  00520460  8b542438              mov       edx,[esp+38h]
  00520464  8b0db4876000          mov       ecx,[6087B4h]
  0052046a  33db                  xor       ebx,ebx
  0052046c  668b18                mov       bx,[eax]
  0052046f  8b01                  mov       eax,[ecx]
  00520471  c1e305                shl       ebx,5
  00520474  03da                  add       ebx,edx
  00520476  0500001b00            add       eax,1B0000h
  0052047b  8b13                  mov       edx,[ebx]
  0052047d  8b4b04                mov       ecx,[ebx+4]
  00520480  8bfa                  mov       edi,edx
  00520482  81e1ffff0000          and       ecx,0FFFFh
  00520488  c1ea0e                shr       edx,0Eh
  0052048b  81e7ffff0000          and       edi,0FFFFh
  00520491  89542430              mov       [esp+30h],edx
  00520495  8b1410                mov       edx,[eax+edx]
  00520498  c1e702                shl       edi,2
  0052049b  89942480000000        mov       [esp+80h],edx
  005204a2  8b150c876000          mov       edx,[60870Ch]
  005204a8  8b2c38                mov       ebp,[eax+edi]
  005204ab  c1e102                shl       ecx,2
  005204ae  892a                  mov       [edx],ebp
  005204b0  8b1508876000          mov       edx,[608708h]
  005204b6  8b0408                mov       eax,[eax+ecx]
  005204b9  894c2434              mov       [esp+34h],ecx
  005204bd  8b8c2480000000        mov       ecx,[esp+80h]
  005204c4  6681fd4001            cmp       bp,140h
  005204c9  890a                  mov       [edx],ecx
  005204cb  8b1510876000          mov       edx,[608710h]
  005204d1  89ac2488000000        mov       [esp+88h],ebp
  005204d8  89842484000000        mov       [esp+84h],eax
  005204df  8902                  mov       [edx],eax
  005204e1  7211                  jb        short 005204F4h
  005204e3  6681f94001            cmp       cx,140h
  005204e8  720a                  jb        short 005204F4h
  005204ea  663d4001              cmp       ax,140h
  005204ee  0f8309020000          jae       near 005206FDh

LAB_005204f4:
  005204f4  8b0d08876000          mov       ecx,[608708h]
  005204fa  a110876000            mov       eax,[608710h]
  005204ff  0fbf11                movsx     edx,word [ecx]
  00520502  0fbf4902              movsx     ecx,word [ecx+2]
  00520506  0fbf6802              movsx     ebp,word [eax+2]
  0052050a  0fbf00                movsx     eax,word [eax]
  0052050d  89542414              mov       [esp+14h],edx
  00520511  8b150c876000          mov       edx,[60870Ch]
  00520517  894c241c              mov       [esp+1Ch],ecx
  0052051b  89442418              mov       [esp+18h],eax
  0052051f  0fbf0a                movsx     ecx,word [edx]
  00520522  0fbf5202              movsx     edx,word [edx+2]
  00520526  8b442414              mov       eax,[esp+14h]
  0052052a  89542428              mov       [esp+28h],edx
  0052052e  8bd1                  mov       edx,ecx
  00520530  0fafc5                imul      eax,ebp
  00520533  0faf54241c            imul      edx,[esp+1Ch]
  00520538  896c242c              mov       [esp+2Ch],ebp
  0052053c  8b6c2418              mov       ebp,[esp+18h]
  00520540  03d0                  add       edx,eax
  00520542  8b442428              mov       eax,[esp+28h]
  00520546  0fafe8                imul      ebp,eax
  00520549  0faf442414            imul      eax,[esp+14h]
  0052054e  0faf4c242c            imul      ecx,[esp+2Ch]
  00520553  03d5                  add       edx,ebp
  00520555  2bd0                  sub       edx,eax
  00520557  8b442418              mov       eax,[esp+18h]
  0052055b  0faf44241c            imul      eax,[esp+1Ch]
  00520560  2bd1                  sub       edx,ecx
  00520562  8b0d14876000          mov       ecx,[608714h]
  00520568  2bd0                  sub       edx,eax
  0052056a  8911                  mov       [ecx],edx
  0052056c  8b942488000000        mov       edx,[esp+88h]
  00520573  81e20000ffff          and       edx,0FFFF0000h
  00520579  81fa0000f000          cmp       edx,0F00000h
  0052057f  722c                  jb        short 005205ADh
  00520581  8b842480000000        mov       eax,[esp+80h]
  00520588  250000ffff            and       eax,0FFFF0000h
  0052058d  3d0000f000            cmp       eax,0F00000h
  00520592  7219                  jb        short 005205ADh
  00520594  8b8c2484000000        mov       ecx,[esp+84h]
  0052059b  81e10000ffff          and       ecx,0FFFF0000h
  005205a1  81f90000f000          cmp       ecx,0F00000h
  005205a7  0f8350010000          jae       near 005206FDh

LAB_005205ad:
  005205ad  e83ebdffff            call      0051C2F0h
  005205b2  85c0                  test      eax,eax
  005205b4  0f8d43010000          jge       near 005206FDh
  005205ba  8b15b4876000          mov       edx,[6087B4h]
  005205c0  8b442430              mov       eax,[esp+30h]
  005205c4  8b2a                  mov       ebp,[edx]
  005205c6  8b542434              mov       edx,[esp+34h]
  005205ca  8b8c3d00401b00        mov       ecx,[ebp+edi+1B4000h]
  005205d1  8b840500401b00        mov       eax,[ebp+eax+1B4000h]
  005205d8  8b941500401b00        mov       edx,[ebp+edx+1B4000h]
  005205df  3bc1                  cmp       eax,ecx
  005205e1  7e02                  jle       short 005205E5h
  005205e3  8bc8                  mov       ecx,eax

LAB_005205e5:
  005205e5  3bd1                  cmp       edx,ecx
  005205e7  7e02                  jle       short 005205EBh
  005205e9  8bca                  mov       ecx,edx

LAB_005205eb:
  005205eb  8d81c0feffff          lea       eax,[ecx-140h]
  005205f1  3dc00e0000            cmp       eax,0EC0h
  005205f6  0f8301010000          jae       near 005206FDh
  005205fc  8b430c                mov       eax,[ebx+0Ch]
  005205ff  8b5308                mov       edx,[ebx+8]
  00520602  8b7b10                mov       edi,[ebx+10h]
  00520605  25ffff0000            and       eax,0FFFFh
  0052060a  89442434              mov       [esp+34h],eax
  0052060e  8b4314                mov       eax,[ebx+14h]
  00520611  89442444              mov       [esp+44h],eax
  00520615  8b431c                mov       eax,[ebx+1Ch]
  00520618  8b1d484a5600          mov       ebx,[564A48h]
  0052061e  85db                  test      ebx,ebx
  00520620  741a                  je        short 0052063Ch
  00520622  a900000002            test      eax,2000000h
  00520627  7413                  je        short 0052063Ch
  00520629  8b1d4c4a5600          mov       ebx,[564A4Ch]
  0052062f  85db                  test      ebx,ebx
  00520631  0f84c6000000          je        near 005206FDh
  00520637  25fffffffd            and       eax,0FDFFFFFFh

LAB_0052063c:
  0052063c  8bda                  mov       ebx,edx
  0052063e  81e1fcff0000          and       ecx,0FFFCh
  00520644  81e3ffff0000          and       ebx,0FFFFh
  0052064a  c1ea0e                shr       edx,0Eh
  0052064d  8b9c9d00801b00        mov       ebx,[ebp+ebx*4+1B8000h]
  00520654  8b942a00801b00        mov       edx,[edx+ebp+1B8000h]
  0052065b  895c2430              mov       [esp+30h],ebx
  0052065f  8b5c2434              mov       ebx,[esp+34h]
  00520663  8bac9d00801b00        mov       ebp,[ebp+ebx*4+1B8000h]
  0052066a  8b5c247c              mov       ebx,[esp+7Ch]
  0052066e  03cb                  add       ecx,ebx
  00520670  8b19                  mov       ebx,[ecx]
  00520672  8931                  mov       [ecx],esi
  00520674  8b8c2488000000        mov       ecx,[esp+88h]
  0052067b  81cb00000009          or        ebx,9000000h
  00520681  894e08                mov       [esi+8],ecx
  00520684  8b8c2484000000        mov       ecx,[esp+84h]
  0052068b  894e14                mov       [esi+14h],ecx
  0052068e  8b8c2480000000        mov       ecx,[esp+80h]
  00520695  894e20                mov       [esi+20h],ecx
  00520698  8bc8                  mov       ecx,eax
  0052069a  891e                  mov       [esi],ebx
  0052069c  8b5c2430              mov       ebx,[esp+30h]
  005206a0  81e1000000ff          and       ecx,0FF000000h
  005206a6  89561c                mov       [esi+1Ch],edx
  005206a9  8bd0                  mov       edx,eax
  005206ab  0bcb                  or        ecx,ebx
  005206ad  81e2f0fc0000          and       edx,0FCF0h
  005206b3  894e04                mov       [esi+4],ecx
  005206b6  8b4c243c              mov       ecx,[esp+3Ch]
  005206ba  250000ef00            and       eax,0EF0000h
  005206bf  c1e20c                shl       edx,0Ch
  005206c2  03d1                  add       edx,ecx
  005206c4  8bcf                  mov       ecx,edi
  005206c6  81e1ffff0000          and       ecx,0FFFFh
  005206cc  896e10                mov       [esi+10h],ebp
  005206cf  0bd1                  or        edx,ecx
  005206d1  8b4c2440              mov       ecx,[esp+40h]
  005206d5  03c1                  add       eax,ecx
  005206d7  8b4c2444              mov       ecx,[esp+44h]
  005206db  81e1ffff0000          and       ecx,0FFFFh
  005206e1  89560c                mov       [esi+0Ch],edx
  005206e4  0bc1                  or        eax,ecx
  005206e6  c1ef10                shr       edi,10h
  005206e9  894618                mov       [esi+18h],eax
  005206ec  897e24                mov       [esi+24h],edi
  005206ef  a118985e00            mov       eax,[5E9818h]
  005206f4  83c628                add       esi,28h
  005206f7  48                    dec       eax
  005206f8  a318985e00            mov       [5E9818h],eax

LAB_005206fd:
  005206fd  8b442420              mov       eax,[esp+20h]
  00520701  8b542410              mov       edx,[esp+10h]
  00520705  48                    dec       eax
  00520706  83c202                add       edx,2
  00520709  85c0                  test      eax,eax
  0052070b  89442420              mov       [esp+20h],eax
  0052070f  89542410              mov       [esp+10h],edx
  00520713  0f8536fdffff          jne       near 0052044Fh

LAB_00520719:
  00520719  8b442470              mov       eax,[esp+70h]
  0052071d  c1f818                sar       eax,18h
  00520720  25ff000000            and       eax,0FFh
  00520725  89442420              mov       [esp+20h],eax
  00520729  0f844f030000          je        near 00520A7Eh

LAB_0052072f:
  0052072f  a118985e00            mov       eax,[5E9818h]
  00520734  85c0                  test      eax,eax
  00520736  0f8442030000          je        near 00520A7Eh
  0052073c  8b542410              mov       edx,[esp+10h]
  00520740  33ed                  xor       ebp,ebp
  00520742  a1b4876000            mov       eax,[6087B4h]
  00520747  8b1d0c876000          mov       ebx,[60870Ch]
  0052074d  668b2a                mov       bp,[edx]
  00520750  8b542438              mov       edx,[esp+38h]
  00520754  c1e505                shl       ebp,5
  00520757  8b00                  mov       eax,[eax]
  00520759  03ea                  add       ebp,edx
  0052075b  0500001b00            add       eax,1B0000h
  00520760  8b4d00                mov       ecx,[ebp]
  00520763  8b5504                mov       edx,[ebp+4]
  00520766  8bf9                  mov       edi,ecx
  00520768  8954242c              mov       [esp+2Ch],edx
  0052076c  81e7ffff0000          and       edi,0FFFFh
  00520772  c1e702                shl       edi,2
  00520775  c1e90e                shr       ecx,0Eh
  00520778  8b1438                mov       edx,[eax+edi]
  0052077b  894c2430              mov       [esp+30h],ecx
  0052077f  8b0c08                mov       ecx,[eax+ecx]
  00520782  8913                  mov       [ebx],edx
  00520784  8b5c242c              mov       ebx,[esp+2Ch]
  00520788  89942488000000        mov       [esp+88h],edx
  0052078f  81e3ffff0000          and       ebx,0FFFFh
  00520795  898c2480000000        mov       [esp+80h],ecx
  0052079c  c1e302                shl       ebx,2
  0052079f  8b1418                mov       edx,[eax+ebx]
  005207a2  89942484000000        mov       [esp+84h],edx
  005207a9  8b1508876000          mov       edx,[608708h]
  005207af  890a                  mov       [edx],ecx
  005207b1  8b54242c              mov       edx,[esp+2Ch]
  005207b5  c1ea0e                shr       edx,0Eh
  005207b8  89542444              mov       [esp+44h],edx
  005207bc  8b0410                mov       eax,[eax+edx]
  005207bf  8b1510876000          mov       edx,[608710h]
  005207c5  89442424              mov       [esp+24h],eax
  005207c9  8b842484000000        mov       eax,[esp+84h]
  005207d0  8902                  mov       [edx],eax
  005207d2  ba40010000            mov       edx,140h
  005207d7  6639942488000000      cmp       [esp+88h],dx
  005207df  7215                  jb        short 005207F6h
  005207e1  663bca                cmp       cx,dx
  005207e4  7210                  jb        short 005207F6h
  005207e6  663bc2                cmp       ax,dx
  005207e9  720b                  jb        short 005207F6h
  005207eb  6639542424            cmp       [esp+24h],dx
  005207f0  0f836c020000          jae       near 00520A62h

LAB_005207f6:
  005207f6  8b1508876000          mov       edx,[608708h]
  005207fc  a110876000            mov       eax,[608710h]
  00520801  0fbf12                movsx     edx,word [edx]
  00520804  89542414              mov       [esp+14h],edx
  00520808  8b1508876000          mov       edx,[608708h]
  0052080e  0fbf4802              movsx     ecx,word [eax+2]
  00520812  0fbf5202              movsx     edx,word [edx+2]
  00520816  8954241c              mov       [esp+1Ch],edx
  0052081a  8b150c876000          mov       edx,[60870Ch]
  00520820  0fbf00                movsx     eax,word [eax]
  00520823  0fbf12                movsx     edx,word [edx]
  00520826  89542434              mov       [esp+34h],edx
  0052082a  8b150c876000          mov       edx,[60870Ch]
  00520830  89442418              mov       [esp+18h],eax
  00520834  8b442414              mov       eax,[esp+14h]
  00520838  0fbf5202              movsx     edx,word [edx+2]
  0052083c  0fafc1                imul      eax,ecx
  0052083f  89542428              mov       [esp+28h],edx
  00520843  8b542434              mov       edx,[esp+34h]
  00520847  0faf54241c            imul      edx,[esp+1Ch]
  0052084c  894c242c              mov       [esp+2Ch],ecx
  00520850  8b4c2418              mov       ecx,[esp+18h]
  00520854  03d0                  add       edx,eax
  00520856  8b442428              mov       eax,[esp+28h]
  0052085a  0fafc8                imul      ecx,eax
  0052085d  0faf442414            imul      eax,[esp+14h]
  00520862  03d1                  add       edx,ecx
  00520864  8b8c2488000000        mov       ecx,[esp+88h]
  0052086b  2bd0                  sub       edx,eax
  0052086d  8b442434              mov       eax,[esp+34h]
  00520871  0faf44242c            imul      eax,[esp+2Ch]
  00520876  2bd0                  sub       edx,eax
  00520878  8b442418              mov       eax,[esp+18h]
  0052087c  0faf44241c            imul      eax,[esp+1Ch]
  00520881  2bd0                  sub       edx,eax
  00520883  a114876000            mov       eax,[608714h]
  00520888  81e10000ffff          and       ecx,0FFFF0000h
  0052088e  81f90000f000          cmp       ecx,0F00000h
  00520894  8910                  mov       [eax],edx
  00520896  723e                  jb        short 005208D6h
  00520898  8b942480000000        mov       edx,[esp+80h]
  0052089f  81e20000ffff          and       edx,0FFFF0000h
  005208a5  81fa0000f000          cmp       edx,0F00000h
  005208ab  7229                  jb        short 005208D6h
  005208ad  8b842484000000        mov       eax,[esp+84h]
  005208b4  250000ffff            and       eax,0FFFF0000h
  005208b9  3d0000f000            cmp       eax,0F00000h
  005208be  7216                  jb        short 005208D6h
  005208c0  8b4c2424              mov       ecx,[esp+24h]
  005208c4  81e10000ffff          and       ecx,0FFFF0000h
  005208ca  81f90000f000          cmp       ecx,0F00000h
  005208d0  0f838c010000          jae       near 00520A62h

LAB_005208d6:
  005208d6  e815baffff            call      0051C2F0h
  005208db  85c0                  test      eax,eax
  005208dd  7c19                  jl        short 005208F8h
  005208df  8b1510876000          mov       edx,[608710h]
  005208e5  8b442424              mov       eax,[esp+24h]
  005208e9  8902                  mov       [edx],eax
  005208eb  e890b9ffff            call      0051C280h
  005208f0  85c0                  test      eax,eax
  005208f2  0f8d6a010000          jge       near 00520A62h

LAB_005208f8:
  005208f8  8b0db4876000          mov       ecx,[6087B4h]
  005208fe  8b442430              mov       eax,[esp+30h]
  00520902  8b09                  mov       ecx,[ecx]
  00520904  8b943900401b00        mov       edx,[ecx+edi+1B4000h]
  0052090b  8b840100401b00        mov       eax,[ecx+eax+1B4000h]
  00520912  8b7c2444              mov       edi,[esp+44h]
  00520916  8b9c1900401b00        mov       ebx,[ecx+ebx+1B4000h]
  0052091d  3bc2                  cmp       eax,edx
  0052091f  8bbc3900401b00        mov       edi,[ecx+edi+1B4000h]
  00520926  7e02                  jle       short 0052092Ah
  00520928  8bd0                  mov       edx,eax

LAB_0052092a:
  0052092a  3bda                  cmp       ebx,edx
  0052092c  7e02                  jle       short 00520930h
  0052092e  8bd3                  mov       edx,ebx

LAB_00520930:
  00520930  3bfa                  cmp       edi,edx
  00520932  7e02                  jle       short 00520936h
  00520934  8bd7                  mov       edx,edi

LAB_00520936:
  00520936  8d82c0feffff          lea       eax,[edx-140h]
  0052093c  3dc00e0000            cmp       eax,0EC0h
  00520941  0f831b010000          jae       near 00520A62h
  00520947  8b4510                mov       eax,[ebp+10h]
  0052094a  8b7d08                mov       edi,[ebp+8]
  0052094d  8b5d0c                mov       ebx,[ebp+0Ch]
  00520950  89442430              mov       [esp+30h],eax
  00520954  8b4514                mov       eax,[ebp+14h]
  00520957  89442444              mov       [esp+44h],eax
  0052095b  8b451c                mov       eax,[ebp+1Ch]
  0052095e  8b2d484a5600          mov       ebp,[564A48h]
  00520964  85ed                  test      ebp,ebp
  00520966  741a                  je        short 00520982h
  00520968  a900000002            test      eax,2000000h
  0052096d  7413                  je        short 00520982h
  0052096f  8b2d4c4a5600          mov       ebp,[564A4Ch]
  00520975  85ed                  test      ebp,ebp
  00520977  0f84e5000000          je        near 00520A62h
  0052097d  25fffffffd            and       eax,0FDFFFFFFh

LAB_00520982:
  00520982  8bef                  mov       ebp,edi
  00520984  81e2fcff0000          and       edx,0FFFCh
  0052098a  c1ef0e                shr       edi,0Eh
  0052098d  81e5ffff0000          and       ebp,0FFFFh
  00520993  8bbc0f00801b00        mov       edi,[edi+ecx+1B8000h]
  0052099a  8baca900801b00        mov       ebp,[ecx+ebp*4+1B8000h]
  005209a1  897c2434              mov       [esp+34h],edi
  005209a5  8bfb                  mov       edi,ebx
  005209a7  81e7ffff0000          and       edi,0FFFFh
  005209ad  c1eb0e                shr       ebx,0Eh
  005209b0  8bbcb900801b00        mov       edi,[ecx+edi*4+1B8000h]
  005209b7  8b9c0b00801b00        mov       ebx,[ebx+ecx+1B8000h]
  005209be  8b4c247c              mov       ecx,[esp+7Ch]
  005209c2  03d1                  add       edx,ecx
  005209c4  8b0a                  mov       ecx,[edx]
  005209c6  8932                  mov       [edx],esi
  005209c8  8b942488000000        mov       edx,[esp+88h]
  005209cf  81c90000000c          or        ecx,0C000000h
  005209d5  890e                  mov       [esi],ecx
  005209d7  8b8c2480000000        mov       ecx,[esp+80h]
  005209de  894e08                mov       [esi+8],ecx
  005209e1  8b8c2484000000        mov       ecx,[esp+84h]
  005209e8  895614                mov       [esi+14h],edx
  005209eb  8b542424              mov       edx,[esp+24h]
  005209ef  894e20                mov       [esi+20h],ecx
  005209f2  8bc8                  mov       ecx,eax
  005209f4  89562c                mov       [esi+2Ch],edx
  005209f7  8b542434              mov       edx,[esp+34h]
  005209fb  81e1000000ff          and       ecx,0FF000000h
  00520a01  897e1c                mov       [esi+1Ch],edi
  00520a04  0bca                  or        ecx,edx
  00520a06  8bd0                  mov       edx,eax
  00520a08  81e2f0fc0000          and       edx,0FCF0h
  00520a0e  894e04                mov       [esi+4],ecx
  00520a11  8b4c243c              mov       ecx,[esp+3Ch]
  00520a15  250000ef00            and       eax,0EF0000h
  00520a1a  c1e20c                shl       edx,0Ch
  00520a1d  03d1                  add       edx,ecx
  00520a1f  8b4c2430              mov       ecx,[esp+30h]
  00520a23  8bf9                  mov       edi,ecx
  00520a25  81e1ffff0000          and       ecx,0FFFFh
  00520a2b  c1ef10                shr       edi,10h
  00520a2e  0bd7                  or        edx,edi
  00520a30  896e10                mov       [esi+10h],ebp
  00520a33  89560c                mov       [esi+0Ch],edx
  00520a36  8b542440              mov       edx,[esp+40h]
  00520a3a  03c2                  add       eax,edx
  00520a3c  895e28                mov       [esi+28h],ebx
  00520a3f  0bc1                  or        eax,ecx
  00520a41  894618                mov       [esi+18h],eax
  00520a44  8b442444              mov       eax,[esp+44h]
  00520a48  8bc8                  mov       ecx,eax
  00520a4a  83c634                add       esi,34h
  00520a4d  81e1ffff0000          and       ecx,0FFFFh
  00520a53  c1e810                shr       eax,10h
  00520a56  894ef0                mov       [esi-10h],ecx
  00520a59  8946fc                mov       [esi-4],eax
  00520a5c  ff0d18985e00          dec       dword [5E9818h]

LAB_00520a62:
  00520a62  8b442420              mov       eax,[esp+20h]
  00520a66  8b542410              mov       edx,[esp+10h]
  00520a6a  48                    dec       eax
  00520a6b  83c202                add       edx,2
  00520a6e  85c0                  test      eax,eax
  00520a70  89442420              mov       [esp+20h],eax
  00520a74  89542410              mov       [esp+10h],edx
  00520a78  0f85b1fcffff          jne       near 0052072Fh

LAB_00520a7e:
  00520a7e  668b44245e            mov       ax,[esp+5Eh]
  00520a83  668b4c2458            mov       cx,[esp+58h]
  00520a88  6648                  dec       ax
  00520a8a  6649                  dec       cx
  00520a8c  6685c0                test      ax,ax
  00520a8f  668944245e            mov       [esp+5Eh],ax
  00520a94  66894c2458            mov       [esp+58h],cx
  00520a99  0f8527f3ffff          jne       near 0051FDC6h
  00520a9f  668b442460            mov       ax,[esp+60h]
  00520aa4  668b54245a            mov       dx,[esp+5Ah]
  00520aa9  6683c107              add       cx,7
  00520aad  6648                  dec       ax
  00520aaf  664a                  dec       dx
  00520ab1  66894c2458            mov       [esp+58h],cx
  00520ab6  6685c0                test      ax,ax
  00520ab9  6689442460            mov       [esp+60h],ax
  00520abe  668954245a            mov       [esp+5Ah],dx
  00520ac3  0f85f2f2ffff          jne       near 0051FDBBh
  00520ac9  8bc6                  mov       eax,esi
  00520acb  5f                    pop       edi
  00520acc  5e                    pop       esi
  00520acd  5d                    pop       ebp
  00520ace  5b                    pop       ebx
  00520acf  83c468                add       esp,68h
  00520ad2  c3                    ret
  00520ad3  90                    nop
  00520ad4  90                    nop
  00520ad5  90                    nop
  00520ad6  90                    nop
  00520ad7  90                    nop
  00520ad8  90                    nop
  00520ad9  90                    nop
  00520ada  90                    nop
  00520adb  90                    nop
  00520adc  90                    nop
  00520add  90                    nop
  00520ade  90                    nop
  00520adf  90                    nop
  00520ae0  83ec18                sub       esp,18h
  00520ae3  53                    push      ebx
  00520ae4  55                    push      ebp
  00520ae5  56                    push      esi
  00520ae6  57                    push      edi
  00520ae7  8b7c2434              mov       edi,[esp+34h]
  00520aeb  8b4720                mov       eax,[edi+20h]
  00520aee  85c0                  test      eax,eax
  00520af0  89442418              mov       [esp+18h],eax
  00520af4  750c                  jne       short 00520B02h
  00520af6  8b442430              mov       eax,[esp+30h]
  00520afa  5f                    pop       edi
  00520afb  5e                    pop       esi
  00520afc  5d                    pop       ebp
  00520afd  5b                    pop       ebx
  00520afe  83c418                add       esp,18h
  00520b01  c3                    ret

LAB_00520b02:
  00520b02  8b7004                mov       esi,[eax+4]
  00520b05  a1484a5600            mov       eax,[564A48h]
  00520b0a  85c0                  test      eax,eax
  00520b0c  7423                  je        short 00520B31h
  00520b0e  f7c600020000          test      esi,200h
  00520b14  741b                  je        short 00520B31h
  00520b16  a14c4a5600            mov       eax,[564A4Ch]
  00520b1b  85c0                  test      eax,eax
  00520b1d  750c                  jne       short 00520B2Bh
  00520b1f  8b442430              mov       eax,[esp+30h]
  00520b23  5f                    pop       edi
  00520b24  5e                    pop       esi
  00520b25  5d                    pop       ebp
  00520b26  5b                    pop       ebx
  00520b27  83c418                add       esp,18h
  00520b2a  c3                    ret

LAB_00520b2b:
  00520b2b  81e6fffdffff          and       esi,0FFFFFDFFh

LAB_00520b31:
  00520b31  68e0086000            push      6008E0h
  00520b36  e88f4f0200            call      00545ACAh
  00520b3b  68e0086000            push      6008E0h
  00520b40  e84b500200            call      00545B90h
  00520b45  8b07                  mov       eax,[edi]
  00520b47  8b15d0086000          mov       edx,[6008D0h]
  00520b4d  8b0dd0866000          mov       ecx,[6086D0h]
  00520b53  03c2                  add       eax,edx
  00520b55  c1f80c                sar       eax,0Ch
  00520b58  668901                mov       [ecx],ax
  00520b5b  8b15d4086000          mov       edx,[6008D4h]
  00520b61  8b6f04                mov       ebp,[edi+4]
  00520b64  a1d0866000            mov       eax,[6086D0h]
  00520b69  2bd5                  sub       edx,ebp
  00520b6b  c1fa0c                sar       edx,0Ch
  00520b6e  66895002              mov       [eax+2],dx
  00520b72  8b4f08                mov       ecx,[edi+8]
  00520b75  8b15d8086000          mov       edx,[6008D8h]
  00520b7b  03ca                  add       ecx,edx
  00520b7d  8b15d0866000          mov       edx,[6086D0h]
  00520b83  c1f90c                sar       ecx,0Ch
  00520b86  66894a04              mov       [edx+4],cx
  00520b8a  8b0dd0866000          mov       ecx,[6086D0h]
  00520b90  a110886000            mov       eax,[608810h]
  00520b95  0fbf5102              movsx     edx,word [ecx+2]
  00520b99  0fbf5802              movsx     ebx,word [eax+2]
  00520b9d  0fbf6804              movsx     ebp,word [eax+4]
  00520ba1  0fafd3                imul      edx,ebx
  00520ba4  0fbf5904              movsx     ebx,word [ecx+4]
  00520ba8  0fafdd                imul      ebx,ebp
  00520bab  0fbf09                movsx     ecx,word [ecx]
  00520bae  03d3                  add       edx,ebx
  00520bb0  0fbf18                movsx     ebx,word [eax]
  00520bb3  0fafd9                imul      ebx,ecx
  00520bb6  8b4814                mov       ecx,[eax+14h]
  00520bb9  a1d4866000            mov       eax,[6086D4h]
  00520bbe  03d3                  add       edx,ebx
  00520bc0  c1fa0c                sar       edx,0Ch
  00520bc3  03d1                  add       edx,ecx
  00520bc5  8910                  mov       [eax],edx
  00520bc7  8b0dd0866000          mov       ecx,[6086D0h]
  00520bcd  a110886000            mov       eax,[608810h]
  00520bd2  0fbf5102              movsx     edx,word [ecx+2]
  00520bd6  0fbf5808              movsx     ebx,word [eax+8]
  00520bda  0fbf680a              movsx     ebp,word [eax+0Ah]
  00520bde  0fafd3                imul      edx,ebx
  00520be1  0fbf5904              movsx     ebx,word [ecx+4]
  00520be5  0fafdd                imul      ebx,ebp
  00520be8  0fbf09                movsx     ecx,word [ecx]
  00520beb  03d3                  add       edx,ebx
  00520bed  0fbf5806              movsx     ebx,word [eax+6]
  00520bf1  0fafd9                imul      ebx,ecx
  00520bf4  8b4818                mov       ecx,[eax+18h]
  00520bf7  a1d4866000            mov       eax,[6086D4h]
  00520bfc  03d3                  add       edx,ebx
  00520bfe  c1fa0c                sar       edx,0Ch
  00520c01  03d1                  add       edx,ecx
  00520c03  895004                mov       [eax+4],edx
  00520c06  8b0dd0866000          mov       ecx,[6086D0h]
  00520c0c  a110886000            mov       eax,[608810h]
  00520c11  0fbf5102              movsx     edx,word [ecx+2]
  00520c15  0fbf580e              movsx     ebx,word [eax+0Eh]
  00520c19  0fbf6810              movsx     ebp,word [eax+10h]
  00520c1d  0fafd3                imul      edx,ebx
  00520c20  0fbf5904              movsx     ebx,word [ecx+4]
  00520c24  0fafdd                imul      ebx,ebp
  00520c27  0fbf09                movsx     ecx,word [ecx]
  00520c2a  03d3                  add       edx,ebx
  00520c2c  0fbf580c              movsx     ebx,word [eax+0Ch]
  00520c30  0fafd9                imul      ebx,ecx
  00520c33  03d3                  add       edx,ebx
  00520c35  8b581c                mov       ebx,[eax+1Ch]
  00520c38  a1d4866000            mov       eax,[6086D4h]
  00520c3d  c1fa0c                sar       edx,0Ch
  00520c40  03d3                  add       edx,ebx
  00520c42  895008                mov       [eax+8],edx
  00520c45  8b0dd4866000          mov       ecx,[6086D4h]
  00520c4b  a1d8866000            mov       eax,[6086D8h]
  00520c50  668b11                mov       dx,[ecx]
  00520c53  668910                mov       [eax],dx
  00520c56  8b0dd4866000          mov       ecx,[6086D4h]
  00520c5c  a1d8866000            mov       eax,[6086D8h]
  00520c61  668b5104              mov       dx,[ecx+4]
  00520c65  66895002              mov       [eax+2],dx
  00520c69  8b0dd4866000          mov       ecx,[6086D4h]
  00520c6f  a1d8866000            mov       eax,[6086D8h]
  00520c74  668b5108              mov       dx,[ecx+8]
  00520c78  66895004              mov       [eax+4],dx
  00520c7c  8b15dc866000          mov       edx,[6086DCh]
  00520c82  0fbf4f1a              movsx     ecx,word [edi+1Ah]
  00520c86  33c0                  xor       eax,eax
  00520c88  894c2410              mov       [esp+10h],ecx
  00520c8c  668902                mov       [edx],ax
  00520c8f  8b0ddc866000          mov       ecx,[6086DCh]
  00520c95  66894102              mov       [ecx+2],ax
  00520c99  8b15dc866000          mov       edx,[6086DCh]
  00520c9f  66894204              mov       [edx+4],ax
  00520ca3  a1d4866000            mov       eax,[6086D4h]
  00520ca8  8b1510886000          mov       edx,[608810h]
  00520cae  8b08                  mov       ecx,[eax]
  00520cb0  894a14                mov       [edx+14h],ecx
  00520cb3  a1d4866000            mov       eax,[6086D4h]
  00520cb8  8b1510886000          mov       edx,[608810h]
  00520cbe  8b4804                mov       ecx,[eax+4]
  00520cc1  894a18                mov       [edx+18h],ecx
  00520cc4  a1d4866000            mov       eax,[6086D4h]
  00520cc9  8b1510886000          mov       edx,[608810h]
  00520ccf  8b4808                mov       ecx,[eax+8]
  00520cd2  894a1c                mov       [edx+1Ch],ecx
  00520cd5  a1dc866000            mov       eax,[6086DCh]
  00520cda  0fbf5802              movsx     ebx,word [eax+2]
  00520cde  0fbf7804              movsx     edi,word [eax+4]
  00520ce2  0fbf28                movsx     ebp,word [eax]
  00520ce5  a110886000            mov       eax,[608810h]
  00520cea  8bcd                  mov       ecx,ebp
  00520cec  897c2414              mov       [esp+14h],edi
  00520cf0  0fbf10                movsx     edx,word [eax]
  00520cf3  0fafca                imul      ecx,edx
  00520cf6  0fbf5004              movsx     edx,word [eax+4]
  00520cfa  0fafd7                imul      edx,edi
  00520cfd  03ca                  add       ecx,edx
  00520cff  0fbf5002              movsx     edx,word [eax+2]
  00520d03  0fafd3                imul      edx,ebx
  00520d06  03ca                  add       ecx,edx
  00520d08  0fbf500a              movsx     edx,word [eax+0Ah]
  00520d0c  0fafd7                imul      edx,edi
  00520d0f  0fbf7806              movsx     edi,word [eax+6]
  00520d13  0faffd                imul      edi,ebp
  00520d16  03d7                  add       edx,edi
  00520d18  0fbf7808              movsx     edi,word [eax+8]
  00520d1c  0faffb                imul      edi,ebx
  00520d1f  03d7                  add       edx,edi
  00520d21  0fbf780e              movsx     edi,word [eax+0Eh]
  00520d25  0faffb                imul      edi,ebx
  00520d28  0fbf580c              movsx     ebx,word [eax+0Ch]
  00520d2c  0fafdd                imul      ebx,ebp
  00520d2f  03fb                  add       edi,ebx
  00520d31  0fbf5810              movsx     ebx,word [eax+10h]
  00520d35  0faf5c2414            imul      ebx,[esp+14h]
  00520d3a  03fb                  add       edi,ebx
  00520d3c  c1ff0c                sar       edi,0Ch
  00520d3f  6603781c              add       di,[eax+1Ch]
  00520d43  c1f90c                sar       ecx,0Ch
  00520d46  66034814              add       cx,[eax+14h]
  00520d4a  66897c2424            mov       [esp+24h],di
  00520d4f  c1fa0c                sar       edx,0Ch
  00520d52  66035018              add       dx,[eax+18h]
  00520d56  0fbfc7                movsx     eax,di
  00520d59  8b3d9c876000          mov       edi,[60879Ch]
  00520d5f  0fbfd2                movsx     edx,dx
  00520d62  8907                  mov       [edi],eax
  00520d64  8b442424              mov       eax,[esp+24h]
  00520d68  8b3da4876000          mov       edi,[6087A4h]
  00520d6e  25ffff0000            and       eax,0FFFFh
  00520d73  0fbf0447              movsx     eax,word [edi+eax*2]
  00520d77  8b3d8c876000          mov       edi,[60878Ch]
  00520d7d  0fafd0                imul      edx,eax
  00520d80  8b3f                  mov       edi,[edi]
  00520d82  81e200f0ffff          and       edx,0FFFFF000h
  00520d88  c1e70c                shl       edi,0Ch
  00520d8b  03d7                  add       edx,edi
  00520d8d  c1e204                shl       edx,4
  00520d90  0fbfc9                movsx     ecx,cx
  00520d93  0fafc8                imul      ecx,eax
  00520d96  a1ec866000            mov       eax,[6086ECh]
  00520d9b  c1f90c                sar       ecx,0Ch
  00520d9e  03d1                  add       edx,ecx
  00520da0  8b08                  mov       ecx,[eax]
  00520da2  03d1                  add       edx,ecx
  00520da4  8b0df0866000          mov       ecx,[6086F0h]
  00520daa  8911                  mov       [ecx],edx
  00520dac  8b159c876000          mov       edx,[60879Ch]
  00520db2  8b0df4866000          mov       ecx,[6086F4h]
  00520db8  8b02                  mov       eax,[edx]
  00520dba  c1f802                sar       eax,2
  00520dbd  8901                  mov       [ecx],eax
  00520dbf  8b15f0866000          mov       edx,[6086F0h]
  00520dc5  8b0d9c876000          mov       ecx,[60879Ch]
  00520dcb  8b02                  mov       eax,[edx]
  00520dcd  8b11                  mov       edx,[ecx]
  00520dcf  89442420              mov       [esp+20h],eax
  00520dd3  89542424              mov       [esp+24h],edx
  00520dd7  668b542424            mov       dx,[esp+24h]
  00520ddc  66c1fa02              sar       dx,2
  00520de0  0fbfea                movsx     ebp,dx
  00520de3  6689542424            mov       [esp+24h],dx
  00520de8  8d4da8                lea       ecx,[ebp-58h]
  00520deb  81f9a0030000          cmp       ecx,3A0h
  00520df1  720c                  jb        short 00520DFFh
  00520df3  8b442430              mov       eax,[esp+30h]
  00520df7  5f                    pop       edi
  00520df8  5e                    pop       esi
  00520df9  5d                    pop       ebp
  00520dfa  5b                    pop       ebx
  00520dfb  83c418                add       esp,18h
  00520dfe  c3                    ret

LAB_00520dff:
  00520dff  f644241001            test      byte [esp+10h],1
  00520e04  7422                  je        short 00520E28h
  00520e06  83c018                add       eax,18h
  00520e09  663d7001              cmp       ax,170h
  00520e0d  730d                  jae       short 00520E1Ch
  00520e0f  8b442422              mov       eax,[esp+22h]
  00520e13  83c018                add       eax,18h
  00520e16  663d2001              cmp       ax,120h
  00520e1a  720c                  jb        short 00520E28h

LAB_00520e1c:
  00520e1c  8b442430              mov       eax,[esp+30h]
  00520e20  5f                    pop       edi
  00520e21  5e                    pop       esi
  00520e22  5d                    pop       ebp
  00520e23  5b                    pop       ebx
  00520e24  83c418                add       esp,18h
  00520e27  c3                    ret

LAB_00520e28:
  00520e28  8b4c2434              mov       ecx,[esp+34h]
  00520e2c  8b7c2418              mov       edi,[esp+18h]
  00520e30  8b411c                mov       eax,[ecx+1Ch]
  00520e33  8944241c              mov       [esp+1Ch],eax
  00520e37  33c0                  xor       eax,eax
  00520e39  8a4124                mov       al,[ecx+24h]
  00520e3c  8b1cc7                mov       ebx,[edi+eax*8]
  00520e3f  33c0                  xor       eax,eax
  00520e41  895c2418              mov       [esp+18h],ebx
  00520e45  8bfb                  mov       edi,ebx
  00520e47  8a44241a              mov       al,[esp+1Ah]
  00520e4b  c1ef18                shr       edi,18h
  00520e4e  48                    dec       eax
  00520e4f  4f                    dec       edi
  00520e50  89442410              mov       [esp+10h],eax
  00520e54  b800001900            mov       eax,190000h
  00520e59  6685d2                test      dx,dx
  00520e5c  7403                  je        short 00520E61h
  00520e5e  99                    cdq
  00520e5f  f7fd                  idiv      ebp

LAB_00520e61:
  00520e61  0fbf490c              movsx     ecx,word [ecx+0Ch]
  00520e65  0faf4c2410            imul      ecx,[esp+10h]
  00520e6a  8b542434              mov       edx,[esp+34h]
  00520e6e  c1e90c                shr       ecx,0Ch
  00520e71  0fbf520e              movsx     edx,word [edx+0Eh]
  00520e75  0fafc8                imul      ecx,eax
  00520e78  0fafd7                imul      edx,edi
  00520e7b  c1ea0c                shr       edx,0Ch
  00520e7e  c1e90c                shr       ecx,0Ch
  00520e81  0fafd0                imul      edx,eax
  00520e84  0fbfc1                movsx     eax,cx
  00520e87  8bce                  mov       ecx,esi
  00520e89  89442434              mov       [esp+34h],eax
  00520e8d  c1e910                shr       ecx,10h
  00520e90  81e1ff000000          and       ecx,0FFh
  00520e96  c1e708                shl       edi,8
  00520e99  8b048d008f5e00        mov       eax,[ecx*4+5E8F00h]
  00520ea0  8b4c242c              mov       ecx,[esp+2Ch]
  00520ea4  89442414              mov       [esp+14h],eax
  00520ea8  8b442430              mov       eax,[esp+30h]
  00520eac  8b4ca9d8              mov       ecx,[ecx+ebp*4-28h]
  00520eb0  81c900000009          or        ecx,9000000h
  00520eb6  8908                  mov       [eax],ecx
  00520eb8  8b4c242c              mov       ecx,[esp+2Ch]
  00520ebc  c1ea0c                shr       edx,0Ch
  00520ebf  8944a9d8              mov       [ecx+ebp*4-28h],eax
  00520ec3  8b6c2438              mov       ebp,[esp+38h]
  00520ec7  81e5ff000000          and       ebp,0FFh
  00520ecd  8bce                  mov       ecx,esi
  00520ecf  83cd2c                or        ebp,2Ch
  00520ed2  80e100                and       cl,0
  00520ed5  c1e508                shl       ebp,8
  00520ed8  0be9                  or        ebp,ecx
  00520eda  8b4c241c              mov       ecx,[esp+1Ch]
  00520ede  c1e510                shl       ebp,10h
  00520ee1  81e1ffffff03          and       ecx,3FFFFFFh
  00520ee7  0be9                  or        ebp,ecx
  00520ee9  8b4c2410              mov       ecx,[esp+10h]
  00520eed  896804                mov       [eax+4],ebp
  00520ef0  8beb                  mov       ebp,ebx
  00520ef2  81e5ff000000          and       ebp,0FFh
  00520ef8  03cd                  add       ecx,ebp
  00520efa  894c242c              mov       [esp+2Ch],ecx
  00520efe  8bcb                  mov       ecx,ebx
  00520f00  81e100ff0000          and       ecx,0FF00h
  00520f06  03f9                  add       edi,ecx
  00520f08  894c2438              mov       [esp+38h],ecx
  00520f0c  8b4c2420              mov       ecx,[esp+20h]
  00520f10  897c2418              mov       [esp+18h],edi
  00520f14  8b7c2434              mov       edi,[esp+34h]
  00520f18  d1ef                  shr       edi,1
  00520f1a  2bcf                  sub       ecx,edi
  00520f1c  8b7c2434              mov       edi,[esp+34h]
  00520f20  81e1ffff0000          and       ecx,0FFFFh
  00520f26  894c2430              mov       [esp+30h],ecx
  00520f2a  03cf                  add       ecx,edi
  00520f2c  0fbfd2                movsx     edx,dx
  00520f2f  81e1ffff0000          and       ecx,0FFFFh
  00520f35  8bfa                  mov       edi,edx
  00520f37  894c2434              mov       [esp+34h],ecx
  00520f3b  0fbf4c2422            movsx     ecx,word [esp+22h]
  00520f40  d1ef                  shr       edi,1
  00520f42  2bcf                  sub       ecx,edi
  00520f44  8b7c2430              mov       edi,[esp+30h]
  00520f48  c1e110                shl       ecx,10h
  00520f4b  c1e210                shl       edx,10h
  00520f4e  03d1                  add       edx,ecx
  00520f50  8954241c              mov       [esp+1Ch],edx
  00520f54  8bd1                  mov       edx,ecx
  00520f56  0bd7                  or        edx,edi
  00520f58  895008                mov       [eax+8],edx
  00520f5b  8b54243c              mov       edx,[esp+3Ch]
  00520f5f  668bfa                mov       di,dx
  00520f62  81e2ff000000          and       edx,0FFh
  00520f68  66c1ff02              sar       di,2
  00520f6c  81e7c0ff0000          and       edi,0FFC0h
  00520f72  03fa                  add       edi,edx
  00520f74  8b542414              mov       edx,[esp+14h]
  00520f78  c1e710                shl       edi,10h
  00520f7b  81e20000ffff          and       edx,0FFFF0000h
  00520f81  03fa                  add       edi,edx
  00520f83  8bd6                  mov       edx,esi
  00520f85  81e3ffff0000          and       ebx,0FFFFh
  00520f8b  c1ea08                shr       edx,8
  00520f8e  81e20000ff00          and       edx,0FF0000h
  00520f94  81e6ff000000          and       esi,0FFh
  00520f9a  03fa                  add       edi,edx
  00520f9c  0bfb                  or        edi,ebx
  00520f9e  8b5c2434              mov       ebx,[esp+34h]
  00520fa2  8bd3                  mov       edx,ebx
  00520fa4  89780c                mov       [eax+0Ch],edi
  00520fa7  8b7c242c              mov       edi,[esp+2Ch]
  00520fab  0bd1                  or        edx,ecx
  00520fad  8b4c2414              mov       ecx,[esp+14h]
  00520fb1  895010                mov       [eax+10h],edx
  00520fb4  c1e605                shl       esi,5
  00520fb7  8b54241c              mov       edx,[esp+1Ch]
  00520fbb  0bf1                  or        esi,ecx
  00520fbd  8b4c2438              mov       ecx,[esp+38h]
  00520fc1  c1e610                shl       esi,10h
  00520fc4  0bf7                  or        esi,edi
  00520fc6  0bf1                  or        esi,ecx
  00520fc8  8bca                  mov       ecx,edx
  00520fca  897014                mov       [eax+14h],esi
  00520fcd  8b742430              mov       esi,[esp+30h]
  00520fd1  0bce                  or        ecx,esi
  00520fd3  0bd3                  or        edx,ebx
  00520fd5  894818                mov       [eax+18h],ecx
  00520fd8  8b4c2418              mov       ecx,[esp+18h]
  00520fdc  8bf1                  mov       esi,ecx
  00520fde  0bcf                  or        ecx,edi
  00520fe0  0bf5                  or        esi,ebp
  00520fe2  5f                    pop       edi
  00520fe3  89701c                mov       [eax+1Ch],esi
  00520fe6  5e                    pop       esi
  00520fe7  895020                mov       [eax+20h],edx
  00520fea  894824                mov       [eax+24h],ecx
  00520fed  5d                    pop       ebp
  00520fee  83c028                add       eax,28h
  00520ff1  5b                    pop       ebx
  00520ff2  83c418                add       esp,18h
  00520ff5  c3                    ret
  00520ff6  90                    nop
  00520ff7  90                    nop

49 cible(s) de saut interne : 0x0051fcaf, 0x0051fcb2, 0x0051fdbb, 0x0051fdc6, 0x0051fdcb, 0x0051fdf9, 0x0051fe7d, 0x0051ff22, 0x0051ffdb, 0x00520013, 0x00520019, 0x00520063, 0x005200f4, 0x00520110, 0x00520126, 0x005201ed, 0x005202cd, 0x005202ef, 0x00520325, 0x0052032b, 0x00520331, 0x00520378, 0x0052041d, 0x00520439, 0x0052044f, 0x005204f4, 0x005205ad, 0x005205e5, 0x005205eb, 0x0052063c, 0x005206fd, 0x00520719, 0x0052072f, 0x005207f6, 0x005208d6, 0x005208f8, 0x0052092a, 0x00520930, 0x00520936, 0x00520982, 0x00520a62, 0x00520a7e, 0x00520b02, 0x00520b2b, 0x00520b31, 0x00520dff, 0x00520e1c, 0x00520e28, 0x00520e61
