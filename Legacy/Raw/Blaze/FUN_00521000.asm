C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00521000  section .text  offset fichier 0x120400  4400 octets

  00521000  83ec5c                sub       esp,5Ch
  00521003  53                    push      ebx
  00521004  55                    push      ebp
  00521005  56                    push      esi
  00521006  57                    push      edi
  00521007  68e0086000            push      6008E0h
  0052100c  e8b94a0200            call      00545ACAh
  00521011  68e0086000            push      6008E0h
  00521016  e8754b0200            call      00545B90h
  0052101b  8b44247c              mov       eax,[esp+7Ch]
  0052101f  8b35d0086000          mov       esi,[6008D0h]
  00521025  8b15dc866000          mov       edx,[6086DCh]
  0052102b  8b08                  mov       ecx,[eax]
  0052102d  03ce                  add       ecx,esi
  0052102f  c1f90c                sar       ecx,0Ch
  00521032  66890a                mov       [edx],cx
  00521035  8b0dd4086000          mov       ecx,[6008D4h]
  0052103b  8b6804                mov       ebp,[eax+4]
  0052103e  8b15dc866000          mov       edx,[6086DCh]
  00521044  2bcd                  sub       ecx,ebp
  00521046  c1f90c                sar       ecx,0Ch
  00521049  66894a02              mov       [edx+2],cx
  0052104d  8b4808                mov       ecx,[eax+8]
  00521050  8b3dd8086000          mov       edi,[6008D8h]
  00521056  8b15dc866000          mov       edx,[6086DCh]
  0052105c  03cf                  add       ecx,edi
  0052105e  c1f90c                sar       ecx,0Ch
  00521061  66894a04              mov       [edx+4],cx
  00521065  668b4814              mov       cx,[eax+14h]
  00521069  668b5016              mov       dx,[eax+16h]
  0052106d  668b4018              mov       ax,[eax+18h]
  00521071  66030d28496000        add       cx,[604928h]
  00521078  6603052c496000        add       ax,[60492Ch]
  0052107f  6603152a496000        add       dx,[60492Ah]
  00521086  66894c2444            mov       [esp+44h],cx
  0052108b  8b0ddc866000          mov       ecx,[6086DCh]
  00521091  6689442448            mov       [esp+48h],ax
  00521096  a110886000            mov       eax,[608810h]
  0052109b  6689542446            mov       [esp+46h],dx
  005210a0  0fbf5102              movsx     edx,word [ecx+2]
  005210a4  0fbf7002              movsx     esi,word [eax+2]
  005210a8  0fbf7804              movsx     edi,word [eax+4]
  005210ac  0fafd6                imul      edx,esi
  005210af  0fbf7104              movsx     esi,word [ecx+4]
  005210b3  0faff7                imul      esi,edi
  005210b6  0fbf09                movsx     ecx,word [ecx]
  005210b9  03d6                  add       edx,esi
  005210bb  8b5814                mov       ebx,[eax+14h]
  005210be  0fbf30                movsx     esi,word [eax]
  005210c1  0faff1                imul      esi,ecx
  005210c4  a1d4866000            mov       eax,[6086D4h]
  005210c9  03d6                  add       edx,esi
  005210cb  c1fa0c                sar       edx,0Ch
  005210ce  03d3                  add       edx,ebx
  005210d0  8910                  mov       [eax],edx
  005210d2  8b0dd4866000          mov       ecx,[6086D4h]
  005210d8  a1d8866000            mov       eax,[6086D8h]
  005210dd  668b11                mov       dx,[ecx]
  005210e0  668910                mov       [eax],dx
  005210e3  8b0ddc866000          mov       ecx,[6086DCh]
  005210e9  a110886000            mov       eax,[608810h]
  005210ee  0fbf5102              movsx     edx,word [ecx+2]
  005210f2  0fbf7008              movsx     esi,word [eax+8]
  005210f6  0fbf780a              movsx     edi,word [eax+0Ah]
  005210fa  0fafd6                imul      edx,esi
  005210fd  0fbf7104              movsx     esi,word [ecx+4]
  00521101  0faff7                imul      esi,edi
  00521104  0fbf09                movsx     ecx,word [ecx]
  00521107  03d6                  add       edx,esi
  00521109  8b5818                mov       ebx,[eax+18h]
  0052110c  0fbf7006              movsx     esi,word [eax+6]
  00521110  0faff1                imul      esi,ecx
  00521113  a1d4866000            mov       eax,[6086D4h]
  00521118  03d6                  add       edx,esi
  0052111a  c1fa0c                sar       edx,0Ch
  0052111d  03d3                  add       edx,ebx
  0052111f  895004                mov       [eax+4],edx
  00521122  8b0dd4866000          mov       ecx,[6086D4h]
  00521128  a1d8866000            mov       eax,[6086D8h]
  0052112d  668b5104              mov       dx,[ecx+4]
  00521131  66895002              mov       [eax+2],dx
  00521135  8b0ddc866000          mov       ecx,[6086DCh]
  0052113b  a110886000            mov       eax,[608810h]
  00521140  0fbf5102              movsx     edx,word [ecx+2]
  00521144  0fbf700e              movsx     esi,word [eax+0Eh]
  00521148  0fbf7810              movsx     edi,word [eax+10h]
  0052114c  0fafd6                imul      edx,esi
  0052114f  0fbf7104              movsx     esi,word [ecx+4]
  00521153  0faff7                imul      esi,edi
  00521156  0fbf09                movsx     ecx,word [ecx]
  00521159  03d6                  add       edx,esi
  0052115b  8b581c                mov       ebx,[eax+1Ch]
  0052115e  0fbf700c              movsx     esi,word [eax+0Ch]
  00521162  0faff1                imul      esi,ecx
  00521165  a1d4866000            mov       eax,[6086D4h]
  0052116a  03d6                  add       edx,esi
  0052116c  c1fa0c                sar       edx,0Ch
  0052116f  03d3                  add       edx,ebx
  00521171  895008                mov       [eax+8],edx
  00521174  8b0dd4866000          mov       ecx,[6086D4h]
  0052117a  a1d8866000            mov       eax,[6086D8h]
  0052117f  668b5108              mov       dx,[ecx+8]
  00521183  66895004              mov       [eax+4],dx
  00521187  8b0dd4866000          mov       ecx,[6086D4h]
  0052118d  a110886000            mov       eax,[608810h]
  00521192  8b11                  mov       edx,[ecx]
  00521194  895014                mov       [eax+14h],edx
  00521197  8b0dd4866000          mov       ecx,[6086D4h]
  0052119d  a110886000            mov       eax,[608810h]
  005211a2  8b5104                mov       edx,[ecx+4]
  005211a5  895018                mov       [eax+18h],edx
  005211a8  8b0dd4866000          mov       ecx,[6086D4h]
  005211ae  a110886000            mov       eax,[608810h]
  005211b3  8b5108                mov       edx,[ecx+8]
  005211b6  8d4c244c              lea       ecx,[esp+4Ch]
  005211ba  89501c                mov       [eax+1Ch],edx
  005211bd  8d542444              lea       edx,[esp+44h]
  005211c1  51                    push      ecx
  005211c2  52                    push      edx
  005211c3  e838b1ffff            call      0051C300h
  005211c8  83c408                add       esp,8
  005211cb  8d44244c              lea       eax,[esp+4Ch]
  005211cf  50                    push      eax
  005211d0  e8f5480200            call      00545ACAh
  005211d5  668b0d08935e00        mov       cx,[5E9308h]
  005211dc  668b150a935e00        mov       dx,[5E930Ah]
  005211e3  6601542446            add       [esp+46h],dx
  005211e8  66a10c935e00          mov       ax,[5E930Ch]
  005211ee  6601442448            add       [esp+48h],ax
  005211f3  66894c2444            mov       [esp+44h],cx
  005211f8  8d4c244c              lea       ecx,[esp+4Ch]
  005211fc  8d542444              lea       edx,[esp+44h]
  00521200  51                    push      ecx
  00521201  52                    push      edx
  00521202  e8f9b0ffff            call      0051C300h
  00521207  83c408                add       esp,8
  0052120a  8d44244c              lea       eax,[esp+4Ch]
  0052120e  50                    push      eax
  0052120f  e894490200            call      00545BA8h
  00521214  8b742478              mov       esi,[esp+78h]
  00521218  8b0d484a5600          mov       ecx,[564A48h]
  0052121e  85c9                  test      ecx,ecx
  00521220  8b463c                mov       eax,[esi+3Ch]
  00521223  741e                  je        short 00521243h
  00521225  f6c402                test      ah,2
  00521228  7419                  je        short 00521243h
  0052122a  8b0d4c4a5600          mov       ecx,[564A4Ch]
  00521230  85c9                  test      ecx,ecx
  00521232  750c                  jne       short 00521240h
  00521234  8b442474              mov       eax,[esp+74h]
  00521238  5f                    pop       edi
  00521239  5e                    pop       esi
  0052123a  5d                    pop       ebp
  0052123b  5b                    pop       ebx
  0052123c  83c45c                add       esp,5Ch
  0052123f  c3                    ret

LAB_00521240:
  00521240  80e4fd                and       ah,0FDh

LAB_00521243:
  00521243  c1e810                shr       eax,10h
  00521246  25ff000000            and       eax,0FFh
  0052124b  8b0485008f5e00        mov       eax,[eax*4+5E8F00h]
  00521252  8bc8                  mov       ecx,eax
  00521254  250000ffff            and       eax,0FFFF0000h
  00521259  80cd08                or        ch,8
  0052125c  89442430              mov       [esp+30h],eax
  00521260  8b4608                mov       eax,[esi+8]
  00521263  c1e110                shl       ecx,10h
  00521266  894c2434              mov       [esp+34h],ecx
  0052126a  8b0db4876000          mov       ecx,[6087B4h]
  00521270  8bf8                  mov       edi,eax
  00521272  25ffff0000            and       eax,0FFFFh
  00521277  8b11                  mov       edx,[ecx]
  00521279  50                    push      eax
  0052127a  81c200001b00          add       edx,1B0000h
  00521280  8d4640                lea       eax,[esi+40h]
  00521283  52                    push      edx
  00521284  50                    push      eax
  00521285  c1ff10                sar       edi,10h
  00521288  e8fd480200            call      00545B8Ah
  0052128d  8b0d00876000          mov       ecx,[608700h]
  00521293  57                    push      edi
  00521294  c701ffffff00          mov       dword [ecx],0FFFFFFh
  0052129a  8b15b4876000          mov       edx,[6087B4h]
  005212a0  8b4e24                mov       ecx,[esi+24h]
  005212a3  8b02                  mov       eax,[edx]
  005212a5  0500801b00            add       eax,1B8000h
  005212aa  50                    push      eax
  005212ab  51                    push      ecx
  005212ac  e8d3480200            call      00545B84h
  005212b1  8b4634                mov       eax,[esi+34h]
  005212b4  8b5628                mov       edx,[esi+28h]
  005212b7  8944243c              mov       [esp+3Ch],eax
  005212bb  66a12a496000          mov       ax,[60492Ah]
  005212c1  0500010000            add       eax,100h
  005212c6  8954242c              mov       [esp+2Ch],edx
  005212ca  c1f809                sar       eax,9
  005212cd  83e007                and       eax,7
  005212d0  8b742474              mov       esi,[esp+74h]
  005212d4  0fbfc8                movsx     ecx,ax
  005212d7  6689442446            mov       [esp+46h],ax
  005212dc  33c0                  xor       eax,eax
  005212de  668b044d48455600      mov       ax,[ecx*2+564548h]
  005212e6  8b0c8dd8455600        mov       ecx,[ecx*4+5645D8h]
  005212ed  668b11                mov       dx,[ecx]
  005212f0  83c102                add       ecx,2
  005212f3  894c2438              mov       [esp+38h],ecx
  005212f7  8b0dd0086000          mov       ecx,[6008D0h]
  005212fd  f7d9                  neg       ecx
  005212ff  c1f90c                sar       ecx,0Ch
  00521302  668954244a            mov       [esp+4Ah],dx
  00521307  81c100200000          add       ecx,2000h
  0052130d  6633d2                xor       dx,dx
  00521310  66c74424540700        mov       word [esp+54h],7
  00521317  c1f909                sar       ecx,9
  0052131a  8ad0                  mov       dl,al
  0052131c  03ca                  add       ecx,edx
  0052131e  8b15d8086000          mov       edx,[6008D8h]
  00521324  f7da                  neg       edx
  00521326  c1fa0c                sar       edx,0Ch
  00521329  81c200200000          add       edx,2000h
  0052132f  66894c244c            mov       [esp+4Ch],cx
  00521334  c1fa09                sar       edx,9
  00521337  c1f808                sar       eax,8
  0052133a  03d0                  add       edx,eax
  0052133c  668954244e            mov       [esp+4Eh],dx

LAB_00521341:
  00521341  66b80700              mov       ax,7
  00521345  6689442452            mov       [esp+52h],ax
  0052134a  eb05                  jmp       short 00521351h

LAB_0052134c:
  0052134c  668b54244e            mov       dx,[esp+4Eh]

LAB_00521351:
  00521351  0fbf7c2454            movsx     edi,word [esp+54h]
  00521356  0fbfc0                movsx     eax,ax
  00521359  c1e708                shl       edi,8
  0052135c  0bf8                  or        edi,eax
  0052135e  0fbf44244a            movsx     eax,word [esp+4Ah]
  00521363  3bc7                  cmp       eax,edi
  00521365  7518                  jne       short 0052137Fh
  00521367  8b442438              mov       eax,[esp+38h]
  0052136b  668b08                mov       cx,[eax]
  0052136e  83c002                add       eax,2
  00521371  66894c244a            mov       [esp+4Ah],cx
  00521376  89442438              mov       [esp+38h],eax
  0052137a  e9110a0000            jmp       00521D90h

LAB_0052137f:
  0052137f  6683f920              cmp       cx,20h
  00521383  0f83070a0000          jae       near 00521D90h
  00521389  6683fa20              cmp       dx,20h
  0052138d  0f83fd090000          jae       near 00521D90h
  00521393  0fbfd2                movsx     edx,dx
  00521396  0fbfc1                movsx     eax,cx
  00521399  8b4c243c              mov       ecx,[esp+3Ch]
  0052139d  c1e205                shl       edx,5
  005213a0  03d0                  add       edx,eax
  005213a2  8b0c91                mov       ecx,[ecx+edx*4]
  005213a5  8b01                  mov       eax,[ecx]
  005213a7  83c104                add       ecx,4
  005213aa  89442460              mov       [esp+60h],eax
  005213ae  8bf8                  mov       edi,eax
  005213b0  8b11                  mov       edx,[ecx]
  005213b2  0bfa                  or        edi,edx
  005213b4  89542464              mov       [esp+64h],edx
  005213b8  0f84d2090000          je        near 00521D90h
  005213be  8bf8                  mov       edi,eax
  005213c0  8bd8                  mov       ebx,eax
  005213c2  c1ff08                sar       edi,8
  005213c5  c1fb10                sar       ebx,10h
  005213c8  81e7ff000000          and       edi,0FFh
  005213ce  81e3ff000000          and       ebx,0FFh
  005213d4  03fb                  add       edi,ebx
  005213d6  8bd8                  mov       ebx,eax
  005213d8  c1fb18                sar       ebx,18h
  005213db  81e3ff000000          and       ebx,0FFh
  005213e1  25ff000000            and       eax,0FFh
  005213e6  03fb                  add       edi,ebx
  005213e8  03f8                  add       edi,eax
  005213ea  8d447904              lea       eax,[ecx+edi*2+4]
  005213ee  8944247c              mov       [esp+7Ch],eax
  005213f2  8bc2                  mov       eax,edx
  005213f4  25ff000000            and       eax,0FFh
  005213f9  89442414              mov       [esp+14h],eax
  005213fd  0f840f020000          je        near 00521612h

LAB_00521403:
  00521403  a118985e00            mov       eax,[5E9818h]
  00521408  85c0                  test      eax,eax
  0052140a  0f8402020000          je        near 00521612h
  00521410  8b4c247c              mov       ecx,[esp+7Ch]
  00521414  8b54242c              mov       edx,[esp+2Ch]
  00521418  33ff                  xor       edi,edi
  0052141a  8b2d0c876000          mov       ebp,[60870Ch]
  00521420  668b39                mov       di,[ecx]
  00521423  c1e705                shl       edi,5
  00521426  03fa                  add       edi,edx
  00521428  8b15b4876000          mov       edx,[6087B4h]
  0052142e  8b0f                  mov       ecx,[edi]
  00521430  8b02                  mov       eax,[edx]
  00521432  8b5704                mov       edx,[edi+4]
  00521435  8bd9                  mov       ebx,ecx
  00521437  0500001b00            add       eax,1B0000h
  0052143c  81e3ffff0000          and       ebx,0FFFFh
  00521442  c1e90e                shr       ecx,0Eh
  00521445  8b1c98                mov       ebx,[eax+ebx*4]
  00521448  81e2ffff0000          and       edx,0FFFFh
  0052144e  8b0c01                mov       ecx,[ecx+eax]
  00521451  895d00                mov       [ebp],ebx
  00521454  8b0490                mov       eax,[eax+edx*4]
  00521457  8b1508876000          mov       edx,[608708h]
  0052145d  6681fb4001            cmp       bx,140h
  00521462  895c2410              mov       [esp+10h],ebx
  00521466  890a                  mov       [edx],ecx
  00521468  8b1510876000          mov       edx,[608710h]
  0052146e  894c2478              mov       [esp+78h],ecx
  00521472  89442474              mov       [esp+74h],eax
  00521476  8902                  mov       [edx],eax
  00521478  7211                  jb        short 0052148Bh
  0052147a  6681f94001            cmp       cx,140h
  0052147f  720a                  jb        short 0052148Bh
  00521481  663d4001              cmp       ax,140h
  00521485  0f836b010000          jae       near 005215F6h

LAB_0052148b:
  0052148b  a108876000            mov       eax,[608708h]
  00521490  8b2d10876000          mov       ebp,[608710h]
  00521496  8b0d0c876000          mov       ecx,[60870Ch]
  0052149c  0fbf18                movsx     ebx,word [eax]
  0052149f  0fbf4002              movsx     eax,word [eax+2]
  005214a3  0fbf5502              movsx     edx,word [ebp+2]
  005214a7  0fbf6d00              movsx     ebp,word [ebp]
  005214ab  89442418              mov       [esp+18h],eax
  005214af  895c2420              mov       [esp+20h],ebx
  005214b3  0fbf01                movsx     eax,word [ecx]
  005214b6  0fafda                imul      ebx,edx
  005214b9  0fbf4902              movsx     ecx,word [ecx+2]
  005214bd  896c2424              mov       [esp+24h],ebp
  005214c1  0fafe9                imul      ebp,ecx
  005214c4  8954241c              mov       [esp+1Ch],edx
  005214c8  8b542418              mov       edx,[esp+18h]
  005214cc  03eb                  add       ebp,ebx
  005214ce  8bd8                  mov       ebx,eax
  005214d0  0faf44241c            imul      eax,[esp+1Ch]
  005214d5  0fafda                imul      ebx,edx
  005214d8  0faf4c2420            imul      ecx,[esp+20h]
  005214dd  03eb                  add       ebp,ebx
  005214df  2be8                  sub       ebp,eax
  005214e1  8b442424              mov       eax,[esp+24h]
  005214e5  0fafc2                imul      eax,edx
  005214e8  8b542410              mov       edx,[esp+10h]
  005214ec  2be9                  sub       ebp,ecx
  005214ee  8b0d14876000          mov       ecx,[608714h]
  005214f4  81e20000ffff          and       edx,0FFFF0000h
  005214fa  2be8                  sub       ebp,eax
  005214fc  81fa0000f000          cmp       edx,0F00000h
  00521502  8929                  mov       [ecx],ebp
  00521504  7226                  jb        short 0052152Ch
  00521506  8b442478              mov       eax,[esp+78h]
  0052150a  250000ffff            and       eax,0FFFF0000h
  0052150f  3d0000f000            cmp       eax,0F00000h
  00521514  7216                  jb        short 0052152Ch
  00521516  8b4c2474              mov       ecx,[esp+74h]
  0052151a  81e10000ffff          and       ecx,0FFFF0000h
  00521520  81f90000f000          cmp       ecx,0F00000h
  00521526  0f83ca000000          jae       near 005215F6h

LAB_0052152c:
  0052152c  e8bfadffff            call      0051C2F0h
  00521531  85c0                  test      eax,eax
  00521533  0f8dbd000000          jge       near 005215F6h
  00521539  8b5f08                mov       ebx,[edi+8]
  0052153c  8b4f10                mov       ecx,[edi+10h]
  0052153f  8b471c                mov       eax,[edi+1Ch]
  00521542  33d2                  xor       edx,edx
  00521544  85db                  test      ebx,ebx
  00521546  8b5f14                mov       ebx,[edi+14h]
  00521549  8b3d484a5600          mov       edi,[564A48h]
  0052154f  0f95c2                setne     dl
  00521552  85ff                  test      edi,edi
  00521554  741a                  je        short 00521570h
  00521556  a900000002            test      eax,2000000h
  0052155b  7413                  je        short 00521570h
  0052155d  8b3d4c4a5600          mov       edi,[564A4Ch]
  00521563  85ff                  test      edi,edi
  00521565  0f848b000000          je        near 005215F6h
  0052156b  25fffffffd            and       eax,0FDFFFFFFh

LAB_00521570:
  00521570  8b3db4876000          mov       edi,[6087B4h]
  00521576  8b6c2470              mov       ebp,[esp+70h]
  0052157a  81e3ffff0000          and       ebx,0FFFFh
  00521580  8b3f                  mov       edi,[edi]
  00521582  8bbc9700801b00        mov       edi,[edi+edx*4+1B8000h]
  00521589  8b5500                mov       edx,[ebp]
  0052158c  81ca00000007          or        edx,7000000h
  00521592  897500                mov       [ebp],esi
  00521595  8916                  mov       [esi],edx
  00521597  8b542410              mov       edx,[esp+10h]
  0052159b  895608                mov       [esi+8],edx
  0052159e  8b542474              mov       edx,[esp+74h]
  005215a2  895610                mov       [esi+10h],edx
  005215a5  8b542478              mov       edx,[esp+78h]
  005215a9  895618                mov       [esi+18h],edx
  005215ac  8bd0                  mov       edx,eax
  005215ae  81e2000000ff          and       edx,0FF000000h
  005215b4  83c620                add       esi,20h
  005215b7  0bd7                  or        edx,edi
  005215b9  8b7c2430              mov       edi,[esp+30h]
  005215bd  8956e4                mov       [esi-1Ch],edx
  005215c0  8bd0                  mov       edx,eax
  005215c2  81e2f0fc0000          and       edx,0FCF0h
  005215c8  250000ef00            and       eax,0EF0000h
  005215cd  c1e20c                shl       edx,0Ch
  005215d0  03d7                  add       edx,edi
  005215d2  8bf9                  mov       edi,ecx
  005215d4  81e7ffff0000          and       edi,0FFFFh
  005215da  0bd7                  or        edx,edi
  005215dc  8956ec                mov       [esi-14h],edx
  005215df  8b542434              mov       edx,[esp+34h]
  005215e3  03c2                  add       eax,edx
  005215e5  0bc3                  or        eax,ebx
  005215e7  c1e910                shr       ecx,10h
  005215ea  8946f4                mov       [esi-0Ch],eax
  005215ed  894efc                mov       [esi-4],ecx
  005215f0  ff0d18985e00          dec       dword [5E9818h]

LAB_005215f6:
  005215f6  8b442414              mov       eax,[esp+14h]
  005215fa  8b54247c              mov       edx,[esp+7Ch]
  005215fe  48                    dec       eax
  005215ff  83c202                add       edx,2
  00521602  85c0                  test      eax,eax
  00521604  89442414              mov       [esp+14h],eax
  00521608  8954247c              mov       [esp+7Ch],edx
  0052160c  0f85f1fdffff          jne       near 00521403h

LAB_00521612:
  00521612  8b442464              mov       eax,[esp+64h]
  00521616  c1f808                sar       eax,8
  00521619  25ff000000            and       eax,0FFh
  0052161e  89442414              mov       [esp+14h],eax
  00521622  0f845a020000          je        near 00521882h

LAB_00521628:
  00521628  a118985e00            mov       eax,[5E9818h]
  0052162d  85c0                  test      eax,eax
  0052162f  0f844d020000          je        near 00521882h
  00521635  8b44247c              mov       eax,[esp+7Ch]
  00521639  8b54242c              mov       edx,[esp+2Ch]
  0052163d  8b0db4876000          mov       ecx,[6087B4h]
  00521643  33ff                  xor       edi,edi
  00521645  668b38                mov       di,[eax]
  00521648  8b2d0c876000          mov       ebp,[60870Ch]
  0052164e  8b01                  mov       eax,[ecx]
  00521650  c1e705                shl       edi,5
  00521653  03fa                  add       edi,edx
  00521655  0500001b00            add       eax,1B0000h
  0052165a  8b0f                  mov       ecx,[edi]
  0052165c  8b5704                mov       edx,[edi+4]
  0052165f  8bd9                  mov       ebx,ecx
  00521661  81e3ffff0000          and       ebx,0FFFFh
  00521667  c1e90e                shr       ecx,0Eh
  0052166a  8b1c98                mov       ebx,[eax+ebx*4]
  0052166d  8b0c01                mov       ecx,[ecx+eax]
  00521670  895d00                mov       [ebp],ebx
  00521673  8bea                  mov       ebp,edx
  00521675  895c2410              mov       [esp+10h],ebx
  00521679  81e5ffff0000          and       ebp,0FFFFh
  0052167f  894c2478              mov       [esp+78h],ecx
  00521683  c1ea0e                shr       edx,0Eh
  00521686  8b2ca8                mov       ebp,[eax+ebp*4]
  00521689  6681fb4001            cmp       bx,140h
  0052168e  896c2474              mov       [esp+74h],ebp
  00521692  8b2d08876000          mov       ebp,[608708h]
  00521698  894d00                mov       [ebp],ecx
  0052169b  8b0402                mov       eax,[edx+eax]
  0052169e  8b1510876000          mov       edx,[608710h]
  005216a4  8b6c2474              mov       ebp,[esp+74h]
  005216a8  89442428              mov       [esp+28h],eax
  005216ac  892a                  mov       [edx],ebp
  005216ae  7218                  jb        short 005216C8h
  005216b0  6681f94001            cmp       cx,140h
  005216b5  7211                  jb        short 005216C8h
  005216b7  6681fd4001            cmp       bp,140h
  005216bc  720a                  jb        short 005216C8h
  005216be  663d4001              cmp       ax,140h
  005216c2  0f839e010000          jae       near 00521866h

LAB_005216c8:
  005216c8  a108876000            mov       eax,[608708h]
  005216cd  8b2d10876000          mov       ebp,[608710h]
  005216d3  8b0d0c876000          mov       ecx,[60870Ch]
  005216d9  0fbf18                movsx     ebx,word [eax]
  005216dc  0fbf4002              movsx     eax,word [eax+2]
  005216e0  0fbf5502              movsx     edx,word [ebp+2]
  005216e4  0fbf6d00              movsx     ebp,word [ebp]
  005216e8  89442418              mov       [esp+18h],eax
  005216ec  895c2420              mov       [esp+20h],ebx
  005216f0  0fbf01                movsx     eax,word [ecx]
  005216f3  0fafda                imul      ebx,edx
  005216f6  0fbf4902              movsx     ecx,word [ecx+2]
  005216fa  896c2424              mov       [esp+24h],ebp
  005216fe  0fafe9                imul      ebp,ecx
  00521701  8954241c              mov       [esp+1Ch],edx
  00521705  8b542418              mov       edx,[esp+18h]
  00521709  03eb                  add       ebp,ebx
  0052170b  8bd8                  mov       ebx,eax
  0052170d  0faf44241c            imul      eax,[esp+1Ch]
  00521712  0fafda                imul      ebx,edx
  00521715  0faf4c2420            imul      ecx,[esp+20h]
  0052171a  03eb                  add       ebp,ebx
  0052171c  2be8                  sub       ebp,eax
  0052171e  8b442424              mov       eax,[esp+24h]
  00521722  0fafc2                imul      eax,edx
  00521725  8b542410              mov       edx,[esp+10h]
  00521729  2be9                  sub       ebp,ecx
  0052172b  8b0d14876000          mov       ecx,[608714h]
  00521731  81e20000ffff          and       edx,0FFFF0000h
  00521737  2be8                  sub       ebp,eax
  00521739  81fa0000f000          cmp       edx,0F00000h
  0052173f  8929                  mov       [ecx],ebp
  00521741  7238                  jb        short 0052177Bh
  00521743  8b442478              mov       eax,[esp+78h]
  00521747  250000ffff            and       eax,0FFFF0000h
  0052174c  3d0000f000            cmp       eax,0F00000h
  00521751  7228                  jb        short 0052177Bh
  00521753  8b4c2474              mov       ecx,[esp+74h]
  00521757  81e10000ffff          and       ecx,0FFFF0000h
  0052175d  81f90000f000          cmp       ecx,0F00000h
  00521763  7216                  jb        short 0052177Bh
  00521765  8b542428              mov       edx,[esp+28h]
  00521769  81e20000ffff          and       edx,0FFFF0000h
  0052176f  81fa0000f000          cmp       edx,0F00000h
  00521775  0f83eb000000          jae       near 00521866h

LAB_0052177b:
  0052177b  e870abffff            call      0051C2F0h
  00521780  85c0                  test      eax,eax
  00521782  7c18                  jl        short 0052179Ch
  00521784  a110876000            mov       eax,[608710h]
  00521789  8b4c2428              mov       ecx,[esp+28h]
  0052178d  8908                  mov       [eax],ecx
  0052178f  e8ecaaffff            call      0051C280h
  00521794  85c0                  test      eax,eax
  00521796  0f8dca000000          jge       near 00521866h

LAB_0052179c:
  0052179c  8b5f08                mov       ebx,[edi+8]
  0052179f  8b4f10                mov       ecx,[edi+10h]
  005217a2  8b5714                mov       edx,[edi+14h]
  005217a5  8b471c                mov       eax,[edi+1Ch]
  005217a8  8b3d484a5600          mov       edi,[564A48h]
  005217ae  81e3ffff0000          and       ebx,0FFFFh
  005217b4  85ff                  test      edi,edi
  005217b6  741a                  je        short 005217D2h
  005217b8  a900000002            test      eax,2000000h
  005217bd  7413                  je        short 005217D2h
  005217bf  8b3d4c4a5600          mov       edi,[564A4Ch]
  005217c5  85ff                  test      edi,edi
  005217c7  0f8499000000          je        near 00521866h
  005217cd  25fffffffd            and       eax,0FDFFFFFFh

LAB_005217d2:
  005217d2  8b3db4876000          mov       edi,[6087B4h]
  005217d8  8b6c2470              mov       ebp,[esp+70h]
  005217dc  8b3f                  mov       edi,[edi]
  005217de  8b9c9f00801b00        mov       ebx,[edi+ebx*4+1B8000h]
  005217e5  8b7d00                mov       edi,[ebp]
  005217e8  81cf00000009          or        edi,9000000h
  005217ee  897500                mov       [ebp],esi
  005217f1  893e                  mov       [esi],edi
  005217f3  8b7c2478              mov       edi,[esp+78h]
  005217f7  897e08                mov       [esi+8],edi
  005217fa  8b7c2410              mov       edi,[esp+10h]
  005217fe  897e10                mov       [esi+10h],edi
  00521801  8b7c2474              mov       edi,[esp+74h]
  00521805  897e18                mov       [esi+18h],edi
  00521808  8b7c2428              mov       edi,[esp+28h]
  0052180c  897e20                mov       [esi+20h],edi
  0052180f  8bf8                  mov       edi,eax
  00521811  81e7000000ff          and       edi,0FF000000h
  00521817  83c628                add       esi,28h
  0052181a  0bfb                  or        edi,ebx
  0052181c  8b5c2430              mov       ebx,[esp+30h]
  00521820  897edc                mov       [esi-24h],edi
  00521823  8bf8                  mov       edi,eax
  00521825  81e7f0fc0000          and       edi,0FCF0h
  0052182b  250000ef00            and       eax,0EF0000h
  00521830  c1e70c                shl       edi,0Ch
  00521833  03fb                  add       edi,ebx
  00521835  8bd9                  mov       ebx,ecx
  00521837  c1eb10                shr       ebx,10h
  0052183a  0bfb                  or        edi,ebx
  0052183c  8b5c2434              mov       ebx,[esp+34h]
  00521840  03c3                  add       eax,ebx
  00521842  81e1ffff0000          and       ecx,0FFFFh
  00521848  0bc1                  or        eax,ecx
  0052184a  897ee4                mov       [esi-1Ch],edi
  0052184d  8946ec                mov       [esi-14h],eax
  00521850  8bc2                  mov       eax,edx
  00521852  25ffff0000            and       eax,0FFFFh
  00521857  c1ea10                shr       edx,10h
  0052185a  8946f4                mov       [esi-0Ch],eax
  0052185d  8956fc                mov       [esi-4],edx
  00521860  ff0d18985e00          dec       dword [5E9818h]

LAB_00521866:
  00521866  8b442414              mov       eax,[esp+14h]
  0052186a  8b54247c              mov       edx,[esp+7Ch]
  0052186e  48                    dec       eax
  0052186f  83c202                add       edx,2
  00521872  85c0                  test      eax,eax
  00521874  89442414              mov       [esp+14h],eax
  00521878  8954247c              mov       [esp+7Ch],edx
  0052187c  0f85a6fdffff          jne       near 00521628h

LAB_00521882:
  00521882  8b442464              mov       eax,[esp+64h]
  00521886  c1f810                sar       eax,10h
  00521889  25ff000000            and       eax,0FFh
  0052188e  89442414              mov       [esp+14h],eax
  00521892  0f8445020000          je        near 00521ADDh

LAB_00521898:
  00521898  a118985e00            mov       eax,[5E9818h]
  0052189d  85c0                  test      eax,eax
  0052189f  0f8438020000          je        near 00521ADDh
  005218a5  8b4c247c              mov       ecx,[esp+7Ch]
  005218a9  8b54242c              mov       edx,[esp+2Ch]
  005218ad  33db                  xor       ebx,ebx
  005218af  8b2d0c876000          mov       ebp,[60870Ch]
  005218b5  668b19                mov       bx,[ecx]
  005218b8  c1e305                shl       ebx,5
  005218bb  03da                  add       ebx,edx
  005218bd  8b15b4876000          mov       edx,[6087B4h]
  005218c3  8b0b                  mov       ecx,[ebx]
  005218c5  8b02                  mov       eax,[edx]
  005218c7  8b5304                mov       edx,[ebx+4]
  005218ca  8bf9                  mov       edi,ecx
  005218cc  0500001b00            add       eax,1B0000h
  005218d1  81e7ffff0000          and       edi,0FFFFh
  005218d7  c1e90e                shr       ecx,0Eh
  005218da  8b3cb8                mov       edi,[eax+edi*4]
  005218dd  81e2ffff0000          and       edx,0FFFFh
  005218e3  8b0c01                mov       ecx,[ecx+eax]
  005218e6  897d00                mov       [ebp],edi
  005218e9  8b0490                mov       eax,[eax+edx*4]
  005218ec  8b1508876000          mov       edx,[608708h]
  005218f2  6681ff4001            cmp       di,140h
  005218f7  897c2410              mov       [esp+10h],edi
  005218fb  890a                  mov       [edx],ecx
  005218fd  8b1510876000          mov       edx,[608710h]
  00521903  894c2478              mov       [esp+78h],ecx
  00521907  89442474              mov       [esp+74h],eax
  0052190b  8902                  mov       [edx],eax
  0052190d  7211                  jb        short 00521920h
  0052190f  6681f94001            cmp       cx,140h
  00521914  720a                  jb        short 00521920h
  00521916  663d4001              cmp       ax,140h
  0052191a  0f83a1010000          jae       near 00521AC1h

LAB_00521920:
  00521920  8b0d08876000          mov       ecx,[608708h]
  00521926  a110876000            mov       eax,[608710h]
  0052192b  8b150c876000          mov       edx,[60870Ch]
  00521931  0fbf29                movsx     ebp,word [ecx]
  00521934  0fbf4902              movsx     ecx,word [ecx+2]
  00521938  0fbf7802              movsx     edi,word [eax+2]
  0052193c  0fbf00                movsx     eax,word [eax]
  0052193f  894c2418              mov       [esp+18h],ecx
  00521943  896c2420              mov       [esp+20h],ebp
  00521947  0fbf0a                movsx     ecx,word [edx]
  0052194a  0fafef                imul      ebp,edi
  0052194d  0fbf5202              movsx     edx,word [edx+2]
  00521951  89442424              mov       [esp+24h],eax
  00521955  0fafc2                imul      eax,edx
  00521958  0faf542420            imul      edx,[esp+20h]
  0052195d  897c241c              mov       [esp+1Ch],edi
  00521961  8b7c2418              mov       edi,[esp+18h]
  00521965  03c5                  add       eax,ebp
  00521967  8be9                  mov       ebp,ecx
  00521969  0faf4c241c            imul      ecx,[esp+1Ch]
  0052196e  0fafef                imul      ebp,edi
  00521971  03c5                  add       eax,ebp
  00521973  2bc1                  sub       eax,ecx
  00521975  8b4c2410              mov       ecx,[esp+10h]
  00521979  2bc2                  sub       eax,edx
  0052197b  81e10000ffff          and       ecx,0FFFF0000h
  00521981  89442428              mov       [esp+28h],eax
  00521985  8b442424              mov       eax,[esp+24h]
  00521989  0fafc7                imul      eax,edi
  0052198c  8b542428              mov       edx,[esp+28h]
  00521990  2bd0                  sub       edx,eax
  00521992  a114876000            mov       eax,[608714h]
  00521997  81f90000f000          cmp       ecx,0F00000h
  0052199d  8910                  mov       [eax],edx
  0052199f  7226                  jb        short 005219C7h
  005219a1  8b542478              mov       edx,[esp+78h]
  005219a5  81e20000ffff          and       edx,0FFFF0000h
  005219ab  81fa0000f000          cmp       edx,0F00000h
  005219b1  7214                  jb        short 005219C7h
  005219b3  8b442474              mov       eax,[esp+74h]
  005219b7  250000ffff            and       eax,0FFFF0000h
  005219bc  3d0000f000            cmp       eax,0F00000h
  005219c1  0f83fa000000          jae       near 00521AC1h

LAB_005219c7:
  005219c7  e824a9ffff            call      0051C2F0h
  005219cc  85c0                  test      eax,eax
  005219ce  0f8ded000000          jge       near 00521AC1h
  005219d4  8b430c                mov       eax,[ebx+0Ch]
  005219d7  8b0d484a5600          mov       ecx,[564A48h]
  005219dd  8b5308                mov       edx,[ebx+8]
  005219e0  8b7b10                mov       edi,[ebx+10h]
  005219e3  8b6b14                mov       ebp,[ebx+14h]
  005219e6  25ffff0000            and       eax,0FFFFh
  005219eb  89442428              mov       [esp+28h],eax
  005219ef  8b431c                mov       eax,[ebx+1Ch]
  005219f2  85c9                  test      ecx,ecx
  005219f4  741a                  je        short 00521A10h
  005219f6  a900000002            test      eax,2000000h
  005219fb  7413                  je        short 00521A10h
  005219fd  8b0d4c4a5600          mov       ecx,[564A4Ch]
  00521a03  85c9                  test      ecx,ecx
  00521a05  0f84b6000000          je        near 00521AC1h
  00521a0b  25fffffffd            and       eax,0FDFFFFFFh

LAB_00521a10:
  00521a10  8b0db4876000          mov       ecx,[6087B4h]
  00521a16  8bda                  mov       ebx,edx
  00521a18  81e3ffff0000          and       ebx,0FFFFh
  00521a1e  81e5ffff0000          and       ebp,0FFFFh
  00521a24  8b09                  mov       ecx,[ecx]
  00521a26  81c100801b00          add       ecx,1B8000h
  00521a2c  c1ea0e                shr       edx,0Eh
  00521a2f  8b1c99                mov       ebx,[ecx+ebx*4]
  00521a32  8b140a                mov       edx,[edx+ecx]
  00521a35  895c2424              mov       [esp+24h],ebx
  00521a39  8b5c2428              mov       ebx,[esp+28h]
  00521a3d  8b0c99                mov       ecx,[ecx+ebx*4]
  00521a40  894c2428              mov       [esp+28h],ecx
  00521a44  8b4c2470              mov       ecx,[esp+70h]
  00521a48  8b19                  mov       ebx,[ecx]
  00521a4a  8931                  mov       [ecx],esi
  00521a4c  8b4c2410              mov       ecx,[esp+10h]
  00521a50  81cb00000009          or        ebx,9000000h
  00521a56  894e08                mov       [esi+8],ecx
  00521a59  8b4c2474              mov       ecx,[esp+74h]
  00521a5d  894e14                mov       [esi+14h],ecx
  00521a60  8b4c2478              mov       ecx,[esp+78h]
  00521a64  894e20                mov       [esi+20h],ecx
  00521a67  8bc8                  mov       ecx,eax
  00521a69  891e                  mov       [esi],ebx
  00521a6b  8b5c2424              mov       ebx,[esp+24h]
  00521a6f  81e1000000ff          and       ecx,0FF000000h
  00521a75  89561c                mov       [esi+1Ch],edx
  00521a78  0bcb                  or        ecx,ebx
  00521a7a  8bd0                  mov       edx,eax
  00521a7c  894e04                mov       [esi+4],ecx
  00521a7f  8b4c2428              mov       ecx,[esp+28h]
  00521a83  81e2f0fc0000          and       edx,0FCF0h
  00521a89  894e10                mov       [esi+10h],ecx
  00521a8c  8b4c2430              mov       ecx,[esp+30h]
  00521a90  250000ef00            and       eax,0EF0000h
  00521a95  c1e20c                shl       edx,0Ch
  00521a98  03d1                  add       edx,ecx
  00521a9a  8bcf                  mov       ecx,edi
  00521a9c  81e1ffff0000          and       ecx,0FFFFh
  00521aa2  83c628                add       esi,28h
  00521aa5  0bd1                  or        edx,ecx
  00521aa7  8956e4                mov       [esi-1Ch],edx
  00521aaa  8b542434              mov       edx,[esp+34h]
  00521aae  03c2                  add       eax,edx
  00521ab0  0bc5                  or        eax,ebp
  00521ab2  c1ef10                shr       edi,10h
  00521ab5  8946f0                mov       [esi-10h],eax
  00521ab8  897efc                mov       [esi-4],edi
  00521abb  ff0d18985e00          dec       dword [5E9818h]

LAB_00521ac1:
  00521ac1  8b442414              mov       eax,[esp+14h]
  00521ac5  8b54247c              mov       edx,[esp+7Ch]
  00521ac9  48                    dec       eax
  00521aca  83c202                add       edx,2
  00521acd  85c0                  test      eax,eax
  00521acf  89442414              mov       [esp+14h],eax
  00521ad3  8954247c              mov       [esp+7Ch],edx
  00521ad7  0f85bbfdffff          jne       near 00521898h

LAB_00521add:
  00521add  8b442464              mov       eax,[esp+64h]
  00521ae1  c1f818                sar       eax,18h
  00521ae4  25ff000000            and       eax,0FFh
  00521ae9  89442414              mov       [esp+14h],eax
  00521aed  0f849d020000          je        near 00521D90h

LAB_00521af3:
  00521af3  a118985e00            mov       eax,[5E9818h]
  00521af8  85c0                  test      eax,eax
  00521afa  0f8490020000          je        near 00521D90h
  00521b00  8b54247c              mov       edx,[esp+7Ch]
  00521b04  33ed                  xor       ebp,ebp
  00521b06  a1b4876000            mov       eax,[6087B4h]
  00521b0b  8b1d0c876000          mov       ebx,[60870Ch]
  00521b11  668b2a                mov       bp,[edx]
  00521b14  8b54242c              mov       edx,[esp+2Ch]
  00521b18  8b00                  mov       eax,[eax]
  00521b1a  c1e505                shl       ebp,5
  00521b1d  03ea                  add       ebp,edx
  00521b1f  0500001b00            add       eax,1B0000h
  00521b24  8b4d00                mov       ecx,[ebp]
  00521b27  8b5504                mov       edx,[ebp+4]
  00521b2a  8bf9                  mov       edi,ecx
  00521b2c  81e7ffff0000          and       edi,0FFFFh
  00521b32  c1e90e                shr       ecx,0Eh
  00521b35  8b3cb8                mov       edi,[eax+edi*4]
  00521b38  8b0c01                mov       ecx,[ecx+eax]
  00521b3b  893b                  mov       [ebx],edi
  00521b3d  8bda                  mov       ebx,edx
  00521b3f  897c2410              mov       [esp+10h],edi
  00521b43  81e3ffff0000          and       ebx,0FFFFh
  00521b49  894c2478              mov       [esp+78h],ecx
  00521b4d  c1ea0e                shr       edx,0Eh
  00521b50  8b1c98                mov       ebx,[eax+ebx*4]
  00521b53  6681ff4001            cmp       di,140h
  00521b58  895c2474              mov       [esp+74h],ebx
  00521b5c  8b1d08876000          mov       ebx,[608708h]
  00521b62  890b                  mov       [ebx],ecx
  00521b64  8b0402                mov       eax,[edx+eax]
  00521b67  8b1510876000          mov       edx,[608710h]
  00521b6d  8b5c2474              mov       ebx,[esp+74h]
  00521b71  89442428              mov       [esp+28h],eax
  00521b75  891a                  mov       [edx],ebx
  00521b77  7218                  jb        short 00521B91h
  00521b79  6681f94001            cmp       cx,140h
  00521b7e  7211                  jb        short 00521B91h
  00521b80  6681fb4001            cmp       bx,140h
  00521b85  720a                  jb        short 00521B91h
  00521b87  663d4001              cmp       ax,140h
  00521b8b  0f83e3010000          jae       near 00521D74h

LAB_00521b91:
  00521b91  a108876000            mov       eax,[608708h]
  00521b96  8b0d10876000          mov       ecx,[608710h]
  00521b9c  8b150c876000          mov       edx,[60870Ch]
  00521ba2  0fbf18                movsx     ebx,word [eax]
  00521ba5  0fbf4002              movsx     eax,word [eax+2]
  00521ba9  0fbf7902              movsx     edi,word [ecx+2]
  00521bad  0fbf09                movsx     ecx,word [ecx]
  00521bb0  89442418              mov       [esp+18h],eax
  00521bb4  895c2420              mov       [esp+20h],ebx
  00521bb8  0fbf02                movsx     eax,word [edx]
  00521bbb  0fafdf                imul      ebx,edi
  00521bbe  0fbf5202              movsx     edx,word [edx+2]
  00521bc2  894c2424              mov       [esp+24h],ecx
  00521bc6  0fafca                imul      ecx,edx
  00521bc9  03cb                  add       ecx,ebx
  00521bcb  897c241c              mov       [esp+1Ch],edi
  00521bcf  894c2440              mov       [esp+40h],ecx
  00521bd3  8b4c2418              mov       ecx,[esp+18h]
  00521bd7  8bf8                  mov       edi,eax
  00521bd9  0faf44241c            imul      eax,[esp+1Ch]
  00521bde  0faff9                imul      edi,ecx
  00521be1  0faf542420            imul      edx,[esp+20h]
  00521be6  8b5c2440              mov       ebx,[esp+40h]
  00521bea  03df                  add       ebx,edi
  00521bec  2bd8                  sub       ebx,eax
  00521bee  8b442424              mov       eax,[esp+24h]
  00521bf2  0fafc1                imul      eax,ecx
  00521bf5  8b0d14876000          mov       ecx,[608714h]
  00521bfb  2bda                  sub       ebx,edx
  00521bfd  8b542410              mov       edx,[esp+10h]
  00521c01  2bd8                  sub       ebx,eax
  00521c03  81e20000ffff          and       edx,0FFFF0000h
  00521c09  8919                  mov       [ecx],ebx
  00521c0b  81fa0000f000          cmp       edx,0F00000h
  00521c11  7238                  jb        short 00521C4Bh
  00521c13  8b442478              mov       eax,[esp+78h]
  00521c17  250000ffff            and       eax,0FFFF0000h
  00521c1c  3d0000f000            cmp       eax,0F00000h
  00521c21  7228                  jb        short 00521C4Bh
  00521c23  8b4c2474              mov       ecx,[esp+74h]
  00521c27  81e10000ffff          and       ecx,0FFFF0000h
  00521c2d  81f90000f000          cmp       ecx,0F00000h
  00521c33  7216                  jb        short 00521C4Bh
  00521c35  8b542428              mov       edx,[esp+28h]
  00521c39  81e20000ffff          and       edx,0FFFF0000h
  00521c3f  81fa0000f000          cmp       edx,0F00000h
  00521c45  0f8329010000          jae       near 00521D74h

LAB_00521c4b:
  00521c4b  e8a0a6ffff            call      0051C2F0h
  00521c50  85c0                  test      eax,eax
  00521c52  7c18                  jl        short 00521C6Ch
  00521c54  a110876000            mov       eax,[608710h]
  00521c59  8b4c2428              mov       ecx,[esp+28h]
  00521c5d  8908                  mov       [eax],ecx
  00521c5f  e81ca6ffff            call      0051C280h
  00521c64  85c0                  test      eax,eax
  00521c66  0f8d08010000          jge       near 00521D74h

LAB_00521c6c:
  00521c6c  8b4514                mov       eax,[ebp+14h]
  00521c6f  8b0d484a5600          mov       ecx,[564A48h]
  00521c75  8b5508                mov       edx,[ebp+8]
  00521c78  8b7d0c                mov       edi,[ebp+0Ch]
  00521c7b  8b5d10                mov       ebx,[ebp+10h]
  00521c7e  89442420              mov       [esp+20h],eax
  00521c82  8b451c                mov       eax,[ebp+1Ch]
  00521c85  85c9                  test      ecx,ecx
  00521c87  741a                  je        short 00521CA3h
  00521c89  a900000002            test      eax,2000000h
  00521c8e  7413                  je        short 00521CA3h
  00521c90  8b0d4c4a5600          mov       ecx,[564A4Ch]
  00521c96  85c9                  test      ecx,ecx
  00521c98  0f84d6000000          je        near 00521D74h
  00521c9e  25fffffffd            and       eax,0FDFFFFFFh

LAB_00521ca3:
  00521ca3  8b0db4876000          mov       ecx,[6087B4h]
  00521ca9  8bea                  mov       ebp,edx
  00521cab  c1ea0e                shr       edx,0Eh
  00521cae  8b09                  mov       ecx,[ecx]
  00521cb0  81e5ffff0000          and       ebp,0FFFFh
  00521cb6  81c100801b00          add       ecx,1B8000h
  00521cbc  8b140a                mov       edx,[edx+ecx]
  00521cbf  8b2ca9                mov       ebp,[ecx+ebp*4]
  00521cc2  89542440              mov       [esp+40h],edx
  00521cc6  8bd7                  mov       edx,edi
  00521cc8  81e2ffff0000          and       edx,0FFFFh
  00521cce  c1ef0e                shr       edi,0Eh
  00521cd1  8b1491                mov       edx,[ecx+edx*4]
  00521cd4  8b0c0f                mov       ecx,[edi+ecx]
  00521cd7  8b7c2470              mov       edi,[esp+70h]
  00521cdb  894c2424              mov       [esp+24h],ecx
  00521cdf  8b0f                  mov       ecx,[edi]
  00521ce1  8937                  mov       [edi],esi
  00521ce3  81c90000000c          or        ecx,0C000000h
  00521ce9  8b7c2440              mov       edi,[esp+40h]
  00521ced  890e                  mov       [esi],ecx
  00521cef  8b4c2478              mov       ecx,[esp+78h]
  00521cf3  894e08                mov       [esi+8],ecx
  00521cf6  8b4c2410              mov       ecx,[esp+10h]
  00521cfa  894e14                mov       [esi+14h],ecx
  00521cfd  8b4c2474              mov       ecx,[esp+74h]
  00521d01  894e20                mov       [esi+20h],ecx
  00521d04  8b4c2428              mov       ecx,[esp+28h]
  00521d08  894e2c                mov       [esi+2Ch],ecx
  00521d0b  8bc8                  mov       ecx,eax
  00521d0d  81e1000000ff          and       ecx,0FF000000h
  00521d13  89561c                mov       [esi+1Ch],edx
  00521d16  8b542424              mov       edx,[esp+24h]
  00521d1a  0bcf                  or        ecx,edi
  00521d1c  894e04                mov       [esi+4],ecx
  00521d1f  8bc8                  mov       ecx,eax
  00521d21  81e1f0fc0000          and       ecx,0FCF0h
  00521d27  896e10                mov       [esi+10h],ebp
  00521d2a  8b6c2430              mov       ebp,[esp+30h]
  00521d2e  895628                mov       [esi+28h],edx
  00521d31  c1e10c                shl       ecx,0Ch
  00521d34  8bd3                  mov       edx,ebx
  00521d36  03cd                  add       ecx,ebp
  00521d38  c1ea10                shr       edx,10h
  00521d3b  0bca                  or        ecx,edx
  00521d3d  8b542434              mov       edx,[esp+34h]
  00521d41  250000ef00            and       eax,0EF0000h
  00521d46  81e3ffff0000          and       ebx,0FFFFh
  00521d4c  03c2                  add       eax,edx
  00521d4e  894e0c                mov       [esi+0Ch],ecx
  00521d51  0bc3                  or        eax,ebx
  00521d53  894618                mov       [esi+18h],eax
  00521d56  8b442420              mov       eax,[esp+20h]
  00521d5a  8bc8                  mov       ecx,eax
  00521d5c  83c634                add       esi,34h
  00521d5f  81e1ffff0000          and       ecx,0FFFFh
  00521d65  c1e810                shr       eax,10h
  00521d68  894ef0                mov       [esi-10h],ecx
  00521d6b  8946fc                mov       [esi-4],eax
  00521d6e  ff0d18985e00          dec       dword [5E9818h]

LAB_00521d74:
  00521d74  8b442414              mov       eax,[esp+14h]
  00521d78  8b54247c              mov       edx,[esp+7Ch]
  00521d7c  48                    dec       eax
  00521d7d  83c202                add       edx,2
  00521d80  85c0                  test      eax,eax
  00521d82  89442414              mov       [esp+14h],eax
  00521d86  8954247c              mov       [esp+7Ch],edx
  00521d8a  0f8563fdffff          jne       near 00521AF3h

LAB_00521d90:
  00521d90  668b442452            mov       ax,[esp+52h]
  00521d95  668b4c244c            mov       cx,[esp+4Ch]
  00521d9a  6648                  dec       ax
  00521d9c  6649                  dec       cx
  00521d9e  6685c0                test      ax,ax
  00521da1  6689442452            mov       [esp+52h],ax
  00521da6  66894c244c            mov       [esp+4Ch],cx
  00521dab  0f859bf5ffff          jne       near 0052134Ch
  00521db1  668b442454            mov       ax,[esp+54h]
  00521db6  668b54244e            mov       dx,[esp+4Eh]
  00521dbb  6683c107              add       cx,7
  00521dbf  6648                  dec       ax
  00521dc1  664a                  dec       dx
  00521dc3  66894c244c            mov       [esp+4Ch],cx
  00521dc8  6685c0                test      ax,ax
  00521dcb  6689442454            mov       [esp+54h],ax
  00521dd0  668954244e            mov       [esp+4Eh],dx
  00521dd5  0f8566f5ffff          jne       near 00521341h
  00521ddb  8bc6                  mov       eax,esi
  00521ddd  5f                    pop       edi
  00521dde  5e                    pop       esi
  00521ddf  5d                    pop       ebp
  00521de0  5b                    pop       ebx
  00521de1  83c45c                add       esp,5Ch
  00521de4  c3                    ret
  00521de5  90                    nop
  00521de6  90                    nop
  00521de7  90                    nop
  00521de8  90                    nop
  00521de9  90                    nop
  00521dea  90                    nop
  00521deb  90                    nop
  00521dec  90                    nop
  00521ded  90                    nop
  00521dee  90                    nop
  00521def  90                    nop
  00521df0  51                    push      ecx
  00521df1  8b442410              mov       eax,[esp+10h]
  00521df5  53                    push      ebx
  00521df6  55                    push      ebp
  00521df7  56                    push      esi
  00521df8  0fbf4810              movsx     ecx,word [eax+10h]
  00521dfc  8b10                  mov       edx,[eax]
  00521dfe  57                    push      edi
  00521dff  8b3cca                mov       edi,[edx+ecx*8]
  00521e02  8d0cca                lea       ecx,[edx+ecx*8]
  00521e05  8b5008                mov       edx,[eax+8]
  00521e08  8b4004                mov       eax,[eax+4]
  00521e0b  8b4904                mov       ecx,[ecx+4]
  00521e0e  89442420              mov       [esp+20h],eax
  00521e12  a1484a5600            mov       eax,[564A48h]
  00521e17  89542410              mov       [esp+10h],edx
  00521e1b  85c0                  test      eax,eax
  00521e1d  7435                  je        short 00521E54h
  00521e1f  a14c4a5600            mov       eax,[564A4Ch]
  00521e24  f6c502                test      ch,2
  00521e27  7411                  je        short 00521E3Ah
  00521e29  85c0                  test      eax,eax
  00521e2b  750a                  jne       short 00521E37h
  00521e2d  8b44241c              mov       eax,[esp+1Ch]
  00521e31  5f                    pop       edi
  00521e32  5e                    pop       esi
  00521e33  5d                    pop       ebp
  00521e34  5b                    pop       ebx
  00521e35  59                    pop       ecx
  00521e36  c3                    ret

LAB_00521e37:
  00521e37  80e5fd                and       ch,0FDh

LAB_00521e3a:
  00521e3a  f644242402            test      byte [esp+24h],2
  00521e3f  7413                  je        short 00521E54h
  00521e41  85c0                  test      eax,eax
  00521e43  750a                  jne       short 00521E4Fh
  00521e45  8b44241c              mov       eax,[esp+1Ch]
  00521e49  5f                    pop       edi
  00521e4a  5e                    pop       esi
  00521e4b  5d                    pop       ebp
  00521e4c  5b                    pop       ebx
  00521e4d  59                    pop       ecx
  00521e4e  c3                    ret

LAB_00521e4f:
  00521e4f  80642424fd            and       byte [esp+24h],0FDh

LAB_00521e54:
  00521e54  8bd7                  mov       edx,edi
  00521e56  6633db                xor       bx,bx
  00521e59  c1ea10                shr       edx,10h
  00521e5c  8bc7                  mov       eax,edi
  00521e5e  8ada                  mov       bl,dl
  00521e60  c1e818                shr       eax,18h
  00521e63  6685db                test      bx,bx
  00521e66  660fb6f0              movzx     si,al
  00521e6a  7505                  jne       short 00521E71h
  00521e6c  bb00010000            mov       ebx,100h

LAB_00521e71:
  00521e71  8b542428              mov       edx,[esp+28h]
  00521e75  33ed                  xor       ebp,ebp
  00521e77  668bc2                mov       ax,dx
  00521e7a  81e2ff000000          and       edx,0FFh
  00521e80  66c1f802              sar       ax,2
  00521e84  25c0ff0000            and       eax,0FFC0h
  00521e89  81e7ffff0000          and       edi,0FFFFh
  00521e8f  0bc2                  or        eax,edx
  00521e91  8bd1                  mov       edx,ecx
  00521e93  c1ea10                shr       edx,10h
  00521e96  81e2ff000000          and       edx,0FFh
  00521e9c  81e6ffff0000          and       esi,0FFFFh
  00521ea2  c1e610                shl       esi,10h
  00521ea5  668b2c95028f5e00      mov       bp,[edx*4+5E8F02h]
  00521ead  8bd1                  mov       edx,ecx
  00521eaf  03c5                  add       eax,ebp
  00521eb1  80e100                and       cl,0
  00521eb4  c1ea18                shr       edx,18h
  00521eb7  03c2                  add       eax,edx
  00521eb9  8b542424              mov       edx,[esp+24h]
  00521ebd  81e2ff000000          and       edx,0FFh
  00521ec3  81e3ffff0000          and       ebx,0FFFFh
  00521ec9  83ca64                or        edx,64h
  00521ecc  0bf3                  or        esi,ebx
  00521ece  c1e208                shl       edx,8
  00521ed1  0bca                  or        ecx,edx
  00521ed3  8b542420              mov       edx,[esp+20h]
  00521ed7  c1e110                shl       ecx,10h
  00521eda  81e2ffffff03          and       edx,3FFFFFFh
  00521ee0  c1e010                shl       eax,10h
  00521ee3  0bca                  or        ecx,edx
  00521ee5  8b54241c              mov       edx,[esp+1Ch]
  00521ee9  0bc7                  or        eax,edi
  00521eeb  5f                    pop       edi
  00521eec  894a04                mov       [edx+4],ecx
  00521eef  8b4c240c              mov       ecx,[esp+0Ch]
  00521ef3  89420c                mov       [edx+0Ch],eax
  00521ef6  8b442414              mov       eax,[esp+14h]
  00521efa  894a08                mov       [edx+8],ecx
  00521efd  897210                mov       [edx+10h],esi
  00521f00  8b08                  mov       ecx,[eax]
  00521f02  5e                    pop       esi
  00521f03  81c900000004          or        ecx,4000000h
  00521f09  5d                    pop       ebp
  00521f0a  890a                  mov       [edx],ecx
  00521f0c  8910                  mov       [eax],edx
  00521f0e  8d4214                lea       eax,[edx+14h]
  00521f11  5b                    pop       ebx
  00521f12  59                    pop       ecx
  00521f13  c3                    ret
  00521f14  90                    nop
  00521f15  90                    nop
  00521f16  90                    nop
  00521f17  90                    nop
  00521f18  90                    nop
  00521f19  90                    nop
  00521f1a  90                    nop
  00521f1b  90                    nop
  00521f1c  90                    nop
  00521f1d  90                    nop
  00521f1e  90                    nop
  00521f1f  90                    nop
  00521f20  83ec14                sub       esp,14h
  00521f23  8b4c2420              mov       ecx,[esp+20h]
  00521f27  53                    push      ebx
  00521f28  55                    push      ebp
  00521f29  56                    push      esi
  00521f2a  0fbf4110              movsx     eax,word [ecx+10h]
  00521f2e  8b11                  mov       edx,[ecx]
  00521f30  57                    push      edi
  00521f31  8d34c2                lea       esi,[edx+eax*8]
  00521f34  8b4108                mov       eax,[ecx+8]
  00521f37  668b510c              mov       dx,[ecx+0Ch]
  00521f3b  8944241c              mov       [esp+1Ch],eax
  00521f3f  668b410e              mov       ax,[ecx+0Eh]
  00521f43  6685d2                test      dx,dx
  00521f46  7d02                  jge       short 00521F4Ah
  00521f48  f7da                  neg       edx

LAB_00521f4a:
  00521f4a  6685c0                test      ax,ax
  00521f4d  7d02                  jge       short 00521F51h
  00521f4f  f7d8                  neg       eax

LAB_00521f51:
  00521f51  8b4904                mov       ecx,[ecx+4]
  00521f54  8b3e                  mov       edi,[esi]
  00521f56  0fbfc0                movsx     eax,ax
  00521f59  0fbfd2                movsx     edx,dx
  00521f5c  c1e010                shl       eax,10h
  00521f5f  03c2                  add       eax,edx
  00521f61  8b15484a5600          mov       edx,[564A48h]
  00521f67  894c2418              mov       [esp+18h],ecx
  00521f6b  8b4e04                mov       ecx,[esi+4]
  00521f6e  85d2                  test      edx,edx
  00521f70  897c2430              mov       [esp+30h],edi
  00521f74  743a                  je        short 00521FB0h
  00521f76  8b354c4a5600          mov       esi,[564A4Ch]
  00521f7c  f6c502                test      ch,2
  00521f7f  7413                  je        short 00521F94h
  00521f81  85f6                  test      esi,esi
  00521f83  750c                  jne       short 00521F91h
  00521f85  8b44242c              mov       eax,[esp+2Ch]
  00521f89  5f                    pop       edi
  00521f8a  5e                    pop       esi
  00521f8b  5d                    pop       ebp
  00521f8c  5b                    pop       ebx
  00521f8d  83c414                add       esp,14h
  00521f90  c3                    ret

LAB_00521f91:
  00521f91  80e5fd                and       ch,0FDh

LAB_00521f94:
  00521f94  f644243402            test      byte [esp+34h],2
  00521f99  7415                  je        short 00521FB0h
  00521f9b  85f6                  test      esi,esi
  00521f9d  750c                  jne       short 00521FABh
  00521f9f  8b44242c              mov       eax,[esp+2Ch]
  00521fa3  5f                    pop       edi
  00521fa4  5e                    pop       esi
  00521fa5  5d                    pop       ebp
  00521fa6  5b                    pop       ebx
  00521fa7  83c414                add       esp,14h
  00521faa  c3                    ret

LAB_00521fab:
  00521fab  80642434fd            and       byte [esp+34h],0FDh

LAB_00521fb0:
  00521fb0  33d2                  xor       edx,edx
  00521fb2  8a542432              mov       dl,[esp+32h]
  00521fb6  8bf2                  mov       esi,edx
  00521fb8  8bd7                  mov       edx,edi
  00521fba  c1ea18                shr       edx,18h
  00521fbd  85f6                  test      esi,esi
  00521fbf  89742410              mov       [esp+10h],esi
  00521fc3  7509                  jne       short 00521FCEh
  00521fc5  beff000000            mov       esi,0FFh
  00521fca  89742410              mov       [esp+10h],esi

LAB_00521fce:
  00521fce  0fbff8                movsx     edi,ax
  00521fd1  c1e810                shr       eax,10h
  00521fd4  0faff7                imul      esi,edi
  00521fd7  0fafc2                imul      eax,edx
  00521fda  c1e80c                shr       eax,0Ch
  00521fdd  8b5c2428              mov       ebx,[esp+28h]
  00521fe1  8bf8                  mov       edi,eax
  00521fe3  8bc1                  mov       eax,ecx
  00521fe5  8b2b                  mov       ebp,[ebx]
  00521fe7  c1e810                shr       eax,10h
  00521fea  25ff000000            and       eax,0FFh
  00521fef  81cd00000009          or        ebp,9000000h
  00521ff5  c1e208                shl       edx,8
  00521ff8  8b0485008f5e00        mov       eax,[eax*4+5E8F00h]
  00521fff  89442414              mov       [esp+14h],eax
  00522003  8b44242c              mov       eax,[esp+2Ch]
  00522007  c1ee0c                shr       esi,0Ch
  0052200a  8928                  mov       [eax],ebp
  0052200c  8903                  mov       [ebx],eax
  0052200e  8b5c2434              mov       ebx,[esp+34h]
  00522012  8be9                  mov       ebp,ecx
  00522014  81e3ff000000          and       ebx,0FFh
  0052201a  81e500ffffff          and       ebp,0FFFFFF00h
  00522020  83cb2c                or        ebx,2Ch
  00522023  c1e308                shl       ebx,8
  00522026  0beb                  or        ebp,ebx
  00522028  8b5c2418              mov       ebx,[esp+18h]
  0052202c  c1e510                shl       ebp,10h
  0052202f  81e3ffffff03          and       ebx,3FFFFFFh
  00522035  0beb                  or        ebp,ebx
  00522037  8b5c2430              mov       ebx,[esp+30h]
  0052203b  896804                mov       [eax+4],ebp
  0052203e  8b6c2410              mov       ebp,[esp+10h]
  00522042  81e3ff000000          and       ebx,0FFh
  00522048  895c2420              mov       [esp+20h],ebx
  0052204c  03eb                  add       ebp,ebx
  0052204e  8b5c2430              mov       ebx,[esp+30h]
  00522052  896c2434              mov       [esp+34h],ebp
  00522056  81e300ff0000          and       ebx,0FF00h
  0052205c  8bee                  mov       ebp,esi
  0052205e  895c242c              mov       [esp+2Ch],ebx
  00522062  03d3                  add       edx,ebx
  00522064  8b5c241c              mov       ebx,[esp+1Ch]
  00522068  89542418              mov       [esp+18h],edx
  0052206c  d1ed                  shr       ebp,1
  0052206e  8bd3                  mov       edx,ebx
  00522070  81e30000ffff          and       ebx,0FFFF0000h
  00522076  2bd5                  sub       edx,ebp
  00522078  81e2ffff0000          and       edx,0FFFFh
  0052207e  8d2c32                lea       ebp,[edx+esi]
  00522081  8bf7                  mov       esi,edi
  00522083  d1ee                  shr       esi,1
  00522085  c1e610                shl       esi,10h
  00522088  2bde                  sub       ebx,esi
  0052208a  81e5ffff0000          and       ebp,0FFFFh
  00522090  8bf3                  mov       esi,ebx
  00522092  c1e710                shl       edi,10h
  00522095  03fe                  add       edi,esi
  00522097  897c2428              mov       [esp+28h],edi
  0052209b  8bfe                  mov       edi,esi
  0052209d  0bfa                  or        edi,edx
  0052209f  897808                mov       [eax+8],edi
  005220a2  8b7c2438              mov       edi,[esp+38h]
  005220a6  668bdf                mov       bx,di
  005220a9  81e7ff000000          and       edi,0FFh
  005220af  66c1fb02              sar       bx,2
  005220b3  81e3c0ff0000          and       ebx,0FFC0h
  005220b9  03df                  add       ebx,edi
  005220bb  8b7c2414              mov       edi,[esp+14h]
  005220bf  c1e310                shl       ebx,10h
  005220c2  81e70000ffff          and       edi,0FFFF0000h
  005220c8  03df                  add       ebx,edi
  005220ca  8bf9                  mov       edi,ecx
  005220cc  c1ef08                shr       edi,8
  005220cf  81e70000ff00          and       edi,0FF0000h
  005220d5  03df                  add       ebx,edi
  005220d7  8b7c2430              mov       edi,[esp+30h]
  005220db  81e7ffff0000          and       edi,0FFFFh
  005220e1  0bdf                  or        ebx,edi
  005220e3  0bf5                  or        esi,ebp
  005220e5  89580c                mov       [eax+0Ch],ebx
  005220e8  8b7c2414              mov       edi,[esp+14h]
  005220ec  81e1ff000000          and       ecx,0FFh
  005220f2  c1e105                shl       ecx,5
  005220f5  0bcf                  or        ecx,edi
  005220f7  8b7c2434              mov       edi,[esp+34h]
  005220fb  c1e110                shl       ecx,10h
  005220fe  897010                mov       [eax+10h],esi
  00522101  8b74242c              mov       esi,[esp+2Ch]
  00522105  0bcf                  or        ecx,edi
  00522107  8b5c2420              mov       ebx,[esp+20h]
  0052210b  0bce                  or        ecx,esi
  0052210d  8b742428              mov       esi,[esp+28h]
  00522111  894814                mov       [eax+14h],ecx
  00522114  8b4c2418              mov       ecx,[esp+18h]
  00522118  0bd6                  or        edx,esi
  0052211a  0bf5                  or        esi,ebp
  0052211c  895018                mov       [eax+18h],edx
  0052211f  8bd1                  mov       edx,ecx
  00522121  0bcf                  or        ecx,edi
  00522123  0bd3                  or        edx,ebx
  00522125  897020                mov       [eax+20h],esi
  00522128  5f                    pop       edi
  00522129  5e                    pop       esi
  0052212a  89501c                mov       [eax+1Ch],edx
  0052212d  894824                mov       [eax+24h],ecx

44 cible(s) de saut interne : 0x00521240, 0x00521243, 0x00521341, 0x0052134c, 0x00521351, 0x0052137f, 0x00521403, 0x0052148b, 0x0052152c, 0x00521570, 0x005215f6, 0x00521612, 0x00521628, 0x005216c8, 0x0052177b, 0x0052179c, 0x005217d2, 0x00521866, 0x00521882, 0x00521898, 0x00521920, 0x005219c7, 0x00521a10, 0x00521ac1, 0x00521add, 0x00521af3, 0x00521b91, 0x00521c4b, 0x00521c6c, 0x00521ca3, 0x00521d74, 0x00521d90, 0x00521e37, 0x00521e3a, 0x00521e4f, 0x00521e54, 0x00521e71, 0x00521f4a, 0x00521f51, 0x00521f91, 0x00521f94, 0x00521fab, 0x00521fb0, 0x00521fce
