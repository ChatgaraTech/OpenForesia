Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004efc20  section .text  offset fichier 0xef020  2320 octets

  004efc20  83ec2c                sub       esp,2Ch
  004efc23  53                    push      ebx
  004efc24  55                    push      ebp
  004efc25  56                    push      esi
  004efc26  8b74243c              mov       esi,[esp+3Ch]
  004efc2a  8d44241c              lea       eax,[esp+1Ch]
  004efc2e  57                    push      edi
  004efc2f  8d4c2438              lea       ecx,[esp+38h]
  004efc33  50                    push      eax
  004efc34  8d542428              lea       edx,[esp+28h]
  004efc38  51                    push      ecx
  004efc39  52                    push      edx
  004efc3a  56                    push      esi
  004efc3b  e8408d0400            call      00538980h
  004efc40  83c410                add       esp,10h
  004efc43  8d442428              lea       eax,[esp+28h]
  004efc47  8d4c241c              lea       ecx,[esp+1Ch]
  004efc4b  8d542410              lea       edx,[esp+10h]
  004efc4f  50                    push      eax
  004efc50  51                    push      ecx
  004efc51  8d442420              lea       eax,[esp+20h]
  004efc55  52                    push      edx
  004efc56  8d4c244c              lea       ecx,[esp+4Ch]
  004efc5a  50                    push      eax
  004efc5b  51                    push      ecx
  004efc5c  56                    push      esi
  004efc5d  e8be620400            call      00535F20h
  004efc62  8b4c2458              mov       ecx,[esp+58h]
  004efc66  b840000000            mov       eax,40h
  004efc6b  2bc1                  sub       eax,ecx
  004efc6d  83c418                add       esp,18h
  004efc70  89442414              mov       [esp+14h],eax
  004efc74  8b44241c              mov       eax,[esp+1Ch]
  004efc78  85c0                  test      eax,eax
  004efc7a  bb03000000            mov       ebx,3
  004efc7f  bd10000000            mov       ebp,10h
  004efc84  0f845e050000          je        near 004F01E8h
  004efc8a  56                    push      esi
  004efc8b  e810b9feff            call      004DB5A0h
  004efc90  8b06                  mov       eax,[esi]
  004efc92  83c404                add       esp,4
  004efc95  a802                  test      al,2
  004efc97  0f844a030000          je        near 004EFFE7h
  004efc9d  f644242810            test      byte [esp+28h],10h
  004efca2  740d                  je        short 004EFCB1h
  004efca4  24fd                  and       al,0FDh
  004efca6  c6464300              mov       byte [esi+43h],0
  004efcaa  8906                  mov       [esi],eax
  004efcac  e937050000            jmp       004F01E8h

LAB_004efcb1:
  004efcb1  33c9                  xor       ecx,ecx
  004efcb3  8a4e42                mov       cl,[esi+42h]
  004efcb6  83e900                sub       ecx,0
  004efcb9  0f8458020000          je        near 004EFF17h
  004efcbf  49                    dec       ecx
  004efcc0  0f8522050000          jne       near 004F01E8h
  004efcc6  0fbf4610              movsx     eax,word [esi+10h]
  004efcca  83e800                sub       eax,0
  004efccd  0f84a6000000          je        near 004EFD79h
  004efcd3  48                    dec       eax
  004efcd4  0f850e050000          jne       near 004F01E8h
  004efcda  66396e1a              cmp       [esi+1Ah],bp
  004efcde  c7465c00000000        mov       dword [esi+5Ch],0
  004efce5  7555                  jne       short 004EFD3Ch
  004efce7  8b542440              mov       edx,[esp+40h]
  004efceb  0fbf4632              movsx     eax,word [esi+32h]
  004efcef  81c200050000          add       edx,500h
  004efcf5  50                    push      eax
  004efcf6  c1e207                shl       edx,7
  004efcf9  89565c                mov       [esi+5Ch],edx
  004efcfc  e81b5d0500            call      00545A1Ch
  004efd01  0faf465c              imul      eax,[esi+5Ch]
  004efd05  8b4e50                mov       ecx,[esi+50h]
  004efd08  c1f80c                sar       eax,0Ch
  004efd0b  03c8                  add       ecx,eax
  004efd0d  894e50                mov       [esi+50h],ecx
  004efd10  0fbf4e32              movsx     ecx,word [esi+32h]
  004efd14  51                    push      ecx
  004efd15  e8fc5c0500            call      00545A16h
  004efd1a  0faf465c              imul      eax,[esi+5Ch]
  004efd1e  8b5e58                mov       ebx,[esi+58h]
  004efd21  c1f80c                sar       eax,0Ch
  004efd24  03d8                  add       ebx,eax
  004efd26  8b442440              mov       eax,[esp+40h]
  004efd2a  895e58                mov       [esi+58h],ebx
  004efd2d  8d5060                lea       edx,[eax+60h]
  004efd30  c1e207                shl       edx,7
  004efd33  66ff4e10              dec       word [esi+10h]
  004efd37  89565c                mov       [esi+5Ch],edx
  004efd3a  eb04                  jmp       short 004EFD40h

LAB_004efd3c:
  004efd3c  8b442440              mov       eax,[esp+40h]

LAB_004efd40:
  004efd40  8b4c2410              mov       ecx,[esp+10h]
  004efd44  d1f8                  sar       eax,1
  004efd46  83c040                add       eax,40h
  004efd49  8d1408                lea       edx,[eax+ecx]
  004efd4c  8d3c00                lea       edi,[eax+eax]
  004efd4f  81e2ff0f0000          and       edx,0FFFh
  004efd55  3bd7                  cmp       edx,edi
  004efd57  7f09                  jg        short 004EFD62h
  004efd59  66014e32              add       [esi+32h],cx
  004efd5d  e986040000            jmp       004F01E8h

LAB_004efd62:
  004efd62  81e1ff0f0000          and       ecx,0FFFh
  004efd68  81f900080000          cmp       ecx,800h
  004efd6e  0f8c70040000          jl        near 004F01E4h
  004efd74  e969040000            jmp       004F01E2h

LAB_004efd79:
  004efd79  8b4650                mov       eax,[esi+50h]
  004efd7c  8b4e58                mov       ecx,[esi+58h]
  004efd7f  c1f80c                sar       eax,0Ch
  004efd82  8bd0                  mov       edx,eax
  004efd84  c1f90c                sar       ecx,0Ch
  004efd87  0fafd0                imul      edx,eax
  004efd8a  8bc1                  mov       eax,ecx
  004efd8c  0fafc1                imul      eax,ecx
  004efd8f  03d0                  add       edx,eax
  004efd91  52                    push      edx
  004efd92  e8795c0500            call      00545A10h
  004efd97  8b16                  mov       edx,[esi]
  004efd99  8b4c2440              mov       ecx,[esp+40h]
  004efd9d  f7c200000002          test      edx,2000000h
  004efda3  740f                  je        short 004EFDB4h
  004efda5  8bf9                  mov       edi,ecx
  004efda7  c1ff03                sar       edi,3
  004efdaa  47                    inc       edi
  004efdab  3bc7                  cmp       eax,edi
  004efdad  7f05                  jg        short 004EFDB4h
  004efdaf  83e2dd                and       edx,0FFFFFFDDh
  004efdb2  8916                  mov       [esi],edx

LAB_004efdb4:
  004efdb4  3bc3                  cmp       eax,ebx
  004efdb6  0f8c2c040000          jl        near 004F01E8h
  004efdbc  f60620                test      byte [esi],20h
  004efdbf  0f8523040000          jne       near 004F01E8h
  004efdc5  c1f903                sar       ecx,3
  004efdc8  83c120                add       ecx,20h
  004efdcb  8bc1                  mov       eax,ecx
  004efdcd  8b4c2410              mov       ecx,[esp+10h]
  004efdd1  8d1408                lea       edx,[eax+ecx]
  004efdd4  8d3c00                lea       edi,[eax+eax]
  004efdd7  81e2ff0f0000          and       edx,0FFFh
  004efddd  3bd7                  cmp       edx,edi
  004efddf  7f06                  jg        short 004EFDE7h
  004efde1  66014e32              add       [esi+32h],cx
  004efde5  eb14                  jmp       short 004EFDFBh

LAB_004efde7:
  004efde7  81e1ff0f0000          and       ecx,0FFFh
  004efded  81f900080000          cmp       ecx,800h
  004efdf3  7c02                  jl        short 004EFDF7h
  004efdf5  f7d8                  neg       eax

LAB_004efdf7:
  004efdf7  66014632              add       [esi+32h],ax

LAB_004efdfb:
  004efdfb  8d6e1c                lea       ebp,[esi+1Ch]
  004efdfe  6a00                  push      0
  004efe00  68242f5500            push      552F24h
  004efe05  55                    push      ebp
  004efe06  56                    push      esi
  004efe07  e8249e0200            call      00519C30h
  004efe0c  8bf8                  mov       edi,eax
  004efe0e  83c410                add       esp,10h
  004efe11  85ff                  test      edi,edi
  004efe13  7406                  je        short 004EFE1Bh
  004efe15  8b06                  mov       eax,[esi]
  004efe17  0c20                  or        al,20h
  004efe19  8906                  mov       [esi],eax

LAB_004efe1b:
  004efe1b  a1b8e05d00            mov       eax,[5DE0B8h]
  004efe20  85c0                  test      eax,eax
  004efe22  0f84c0030000          je        near 004F01E8h
  004efe28  8b1f                  mov       ebx,[edi]
  004efe2a  66c787960000001000    mov       word [edi+96h],10h
  004efe33  83cb40                or        ebx,40h
  004efe36  57                    push      edi
  004efe37  891f                  mov       [edi],ebx
  004efe39  8b4c2424              mov       ecx,[esp+24h]
  004efe3d  56                    push      esi
  004efe3e  668b4130              mov       ax,[ecx+30h]
  004efe42  0fbfd8                movsx     ebx,ax
  004efe45  66d1f8                sar       ax,1
  004efe48  66894130              mov       [ecx+30h],ax
  004efe4c  e82f9d0400            call      00539B80h
  004efe51  8b442428              mov       eax,[esp+28h]
  004efe55  83c408                add       esp,8
  004efe58  66895830              mov       [eax+30h],bx
  004efe5c  8d5f1c                lea       ebx,[edi+1Ch]
  004efe5f  53                    push      ebx
  004efe60  6800180000            push      1800h
  004efe65  6800030000            push      300h
  004efe6a  6a03                  push      3
  004efe6c  e8afdc0400            call      0053DB20h
  004efe71  8b07                  mov       eax,[edi]
  004efe73  83c410                add       esp,10h
  004efe76  a900000040            test      eax,40000000h
  004efe7b  0f8567030000          jne       near 004F01E8h
  004efe81  0fbf4e32              movsx     ecx,word [esi+32h]
  004efe85  8b542410              mov       edx,[esp+10h]
  004efe89  03d1                  add       edx,ecx
  004efe8b  52                    push      edx
  004efe8c  e88b5b0500            call      00545A1Ch
  004efe91  8d0440                lea       eax,[eax+eax*2]
  004efe94  8d0480                lea       eax,[eax+eax*4]
  004efe97  d1e0                  shl       eax,1
  004efe99  894750                mov       [edi+50h],eax
  004efe9c  8b542410              mov       edx,[esp+10h]
  004efea0  0fbf4e32              movsx     ecx,word [esi+32h]
  004efea4  03d1                  add       edx,ecx
  004efea6  52                    push      edx
  004efea7  e86a5b0500            call      00545A16h
  004efeac  8d0440                lea       eax,[eax+eax*2]
  004efeaf  8d0480                lea       eax,[eax+eax*4]
  004efeb2  d1e0                  shl       eax,1
  004efeb4  894758                mov       [edi+58h],eax
  004efeb7  8b07                  mov       eax,[edi]
  004efeb9  a801                  test      al,1
  004efebb  750b                  jne       short 004EFEC8h
  004efebd  0c01                  or        al,1
  004efebf  c7475400800100        mov       dword [edi+54h],18000h
  004efec6  8907                  mov       [edi],eax

LAB_004efec8:
  004efec8  8b4d00                mov       ecx,[ebp]
  004efecb  8b5620                mov       edx,[esi+20h]
  004efece  8b4624                mov       eax,[esi+24h]
  004efed1  894c242c              mov       [esp+2Ch],ecx
  004efed5  8b0b                  mov       ecx,[ebx]
  004efed7  89542430              mov       [esp+30h],edx
  004efedb  894d00                mov       [ebp],ecx
  004efede  8b5720                mov       edx,[edi+20h]
  004efee1  6aff                  push      0FFFFFFFFh
  004efee3  89442438              mov       [esp+38h],eax
  004efee7  895620                mov       [esi+20h],edx
  004efeea  8b4724                mov       eax,[edi+24h]
  004efeed  6a00                  push      0
  004efeef  6a39                  push      39h
  004efef1  56                    push      esi
  004efef2  894624                mov       [esi+24h],eax
  004efef5  e8d6aaf8ff            call      0047A9D0h
  004efefa  8b4c243c              mov       ecx,[esp+3Ch]
  004efefe  8b542440              mov       edx,[esp+40h]
  004eff02  8b442444              mov       eax,[esp+44h]
  004eff06  83c410                add       esp,10h
  004eff09  894d00                mov       [ebp],ecx
  004eff0c  895620                mov       [esi+20h],edx
  004eff0f  894624                mov       [esi+24h],eax
  004eff12  e9d1020000            jmp       004F01E8h

LAB_004eff17:
  004eff17  66395e18              cmp       [esi+18h],bx
  004eff1b  740a                  je        short 004EFF27h
  004eff1d  25fffffffe            and       eax,0FEFFFFFFh
  004eff22  895e18                mov       [esi+18h],ebx
  004eff25  8906                  mov       [esi],eax

LAB_004eff27:
  004eff27  8b442440              mov       eax,[esp+40h]
  004eff2b  8b4c2410              mov       ecx,[esp+10h]
  004eff2f  d1f8                  sar       eax,1
  004eff31  83c040                add       eax,40h
  004eff34  8d1408                lea       edx,[eax+ecx]
  004eff37  8d3c00                lea       edi,[eax+eax]
  004eff3a  81e2ff0f0000          and       edx,0FFFh
  004eff40  3bd7                  cmp       edx,edi
  004eff42  7f06                  jg        short 004EFF4Ah
  004eff44  66014e32              add       [esi+32h],cx
  004eff48  eb14                  jmp       short 004EFF5Eh

LAB_004eff4a:
  004eff4a  81e1ff0f0000          and       ecx,0FFFh
  004eff50  81f900080000          cmp       ecx,800h
  004eff56  7c02                  jl        short 004EFF5Ah
  004eff58  f7d8                  neg       eax

LAB_004eff5a:
  004eff5a  66014632              add       [esi+32h],ax

LAB_004eff5e:
  004eff5e  6a00                  push      0
  004eff60  8d461c                lea       eax,[esi+1Ch]
  004eff63  68182f5500            push      552F18h
  004eff68  50                    push      eax
  004eff69  56                    push      esi
  004eff6a  e8c19c0200            call      00519C30h
  004eff6f  8bf8                  mov       edi,eax
  004eff71  a1b8e05d00            mov       eax,[5DE0B8h]
  004eff76  83c410                add       esp,10h
  004eff79  85c0                  test      eax,eax
  004eff7b  742f                  je        short 004EFFACh
  004eff7d  6689af96000000        mov       [edi+96h],bp
  004eff84  8b2f                  mov       ebp,[edi]
  004eff86  83cd40                or        ebp,40h
  004eff89  57                    push      edi
  004eff8a  56                    push      esi
  004eff8b  892f                  mov       [edi],ebp
  004eff8d  e8ee9b0400            call      00539B80h
  004eff92  83c408                add       esp,8
  004eff95  83c71c                add       edi,1Ch
  004eff98  57                    push      edi
  004eff99  6800180000            push      1800h
  004eff9e  6800030000            push      300h
  004effa3  53                    push      ebx
  004effa4  e877db0400            call      0053DB20h
  004effa9  83c410                add       esp,10h

LAB_004effac:
  004effac  8b4c2440              mov       ecx,[esp+40h]
  004effb0  8b465c                mov       eax,[esi+5Ch]
  004effb3  81c180020000          add       ecx,280h
  004effb9  c1e107                shl       ecx,7
  004effbc  2bc8                  sub       ecx,eax
  004effbe  c1f904                sar       ecx,4
  004effc1  03c8                  add       ecx,eax
  004effc3  8a4643                mov       al,[esi+43h]
  004effc6  84c0                  test      al,al
  004effc8  894e5c                mov       [esi+5Ch],ecx
  004effcb  0f8417020000          je        near 004F01E8h
  004effd1  fec8                  dec       al
  004effd3  884643                mov       [esi+43h],al
  004effd6  0f850c020000          jne       near 004F01E8h
  004effdc  8b06                  mov       eax,[esi]
  004effde  24fd                  and       al,0FDh
  004effe0  8906                  mov       [esi],eax
  004effe2  e901020000            jmp       004F01E8h

LAB_004effe7:
  004effe7  a810                  test      al,10h
  004effe9  0f85f9010000          jne       near 004F01E8h
  004effef  a904080000            test      eax,804h
  004efff4  0f85ee010000          jne       near 004F01E8h
  004efffa  33c9                  xor       ecx,ecx
  004efffc  8a4e42                mov       cl,[esi+42h]
  004effff  3bcb                  cmp       ecx,ebx
  004f0001  0f87e1010000          ja        near 004F01E8h
  004f0007  ff248d14054f00        jmp       dword [ecx*4+4F0514h]
  004f000e  66837e1800            cmp       word [esi+18h],0
  004f0013  740e                  je        short 004F0023h
  004f0015  0d00000001            or        eax,1000000h
  004f001a  c7461800000000        mov       dword [esi+18h],0
  004f0021  8906                  mov       [esi],eax

LAB_004f0023:
  004f0023  817c24182c010000      cmp       dword [esp+18h],12Ch
  004f002b  7d09                  jge       short 004F0036h
  004f002d  c6464300              mov       byte [esi+43h],0
  004f0031  e9b2010000            jmp       004F01E8h

LAB_004f0036:
  004f0036  8a4643                mov       al,[esi+43h]
  004f0039  84c0                  test      al,al
  004f003b  7405                  je        short 004F0042h
  004f003d  fec8                  dec       al
  004f003f  884643                mov       [esi+43h],al

LAB_004f0042:
  004f0042  8b465c                mov       eax,[esi+5Ch]
  004f0045  8bd0                  mov       edx,eax
  004f0047  c1fa04                sar       edx,4
  004f004a  2bc2                  sub       eax,edx
  004f004c  89465c                mov       [esi+5Ch],eax
  004f004f  e994010000            jmp       004F01E8h
  004f0054  66837e1800            cmp       word [esi+18h],0
  004f0059  740e                  je        short 004F0069h
  004f005b  0d00000001            or        eax,1000000h
  004f0060  c7461800000000        mov       dword [esi+18h],0
  004f0067  8906                  mov       [esi],eax

LAB_004f0069:
  004f0069  8b442440              mov       eax,[esp+40h]
  004f006d  8b4c2410              mov       ecx,[esp+10h]
  004f0071  c1f802                sar       eax,2
  004f0074  83c038                add       eax,38h
  004f0077  8d1408                lea       edx,[eax+ecx]
  004f007a  8d3c00                lea       edi,[eax+eax]
  004f007d  81e2ff0f0000          and       edx,0FFFh
  004f0083  3bd7                  cmp       edx,edi
  004f0085  7f1a                  jg        short 004F00A1h
  004f0087  8b465c                mov       eax,[esi+5Ch]
  004f008a  66014e32              add       [esi+32h],cx
  004f008e  8bc8                  mov       ecx,eax
  004f0090  c6464300              mov       byte [esi+43h],0
  004f0094  c1f904                sar       ecx,4
  004f0097  2bc1                  sub       eax,ecx
  004f0099  89465c                mov       [esi+5Ch],eax
  004f009c  e947010000            jmp       004F01E8h

LAB_004f00a1:
  004f00a1  81e1ff0f0000          and       ecx,0FFFh
  004f00a7  81f900080000          cmp       ecx,800h
  004f00ad  7c02                  jl        short 004F00B1h
  004f00af  f7d8                  neg       eax

LAB_004f00b1:
  004f00b1  66014632              add       [esi+32h],ax
  004f00b5  8b465c                mov       eax,[esi+5Ch]
  004f00b8  8bc8                  mov       ecx,eax
  004f00ba  c1f904                sar       ecx,4
  004f00bd  2bc1                  sub       eax,ecx
  004f00bf  89465c                mov       [esi+5Ch],eax
  004f00c2  e921010000            jmp       004F01E8h
  004f00c7  b901000000            mov       ecx,1
  004f00cc  66394e18              cmp       [esi+18h],cx
  004f00d0  740a                  je        short 004F00DCh
  004f00d2  0d00000001            or        eax,1000000h
  004f00d7  894e18                mov       [esi+18h],ecx
  004f00da  8906                  mov       [esi],eax

LAB_004f00dc:
  004f00dc  817c24182c010000      cmp       dword [esp+18h],12Ch
  004f00e4  7d09                  jge       short 004F00EFh
  004f00e6  c6464300              mov       byte [esi+43h],0
  004f00ea  e9f9000000            jmp       004F01E8h

LAB_004f00ef:
  004f00ef  8a4643                mov       al,[esi+43h]
  004f00f2  84c0                  test      al,al
  004f00f4  7405                  je        short 004F00FBh
  004f00f6  fec8                  dec       al
  004f00f8  884643                mov       [esi+43h],al

LAB_004f00fb:
  004f00fb  8b542440              mov       edx,[esp+40h]
  004f00ff  8b4c2410              mov       ecx,[esp+10h]
  004f0103  8bc2                  mov       eax,edx
  004f0105  c1f802                sar       eax,2
  004f0108  83c038                add       eax,38h
  004f010b  8d3c08                lea       edi,[eax+ecx]
  004f010e  8d1c00                lea       ebx,[eax+eax]
  004f0111  81e7ff0f0000          and       edi,0FFFh
  004f0117  3bfb                  cmp       edi,ebx
  004f0119  7f06                  jg        short 004F0121h
  004f011b  66014e32              add       [esi+32h],cx
  004f011f  eb14                  jmp       short 004F0135h

LAB_004f0121:
  004f0121  81e1ff0f0000          and       ecx,0FFFh
  004f0127  81f900080000          cmp       ecx,800h
  004f012d  7c02                  jl        short 004F0131h
  004f012f  f7d8                  neg       eax

LAB_004f0131:
  004f0131  66014632              add       [esi+32h],ax

LAB_004f0135:
  004f0135  8b465c                mov       eax,[esi+5Ch]
  004f0138  81c2a0010000          add       edx,1A0h
  004f013e  c1e207                shl       edx,7
  004f0141  2bd0                  sub       edx,eax
  004f0143  c1fa04                sar       edx,4
  004f0146  03d0                  add       edx,eax
  004f0148  89565c                mov       [esi+5Ch],edx
  004f014b  e998000000            jmp       004F01E8h
  004f0150  33ff                  xor       edi,edi
  004f0152  66397e18              cmp       [esi+18h],di
  004f0156  740a                  je        short 004F0162h
  004f0158  0d00000001            or        eax,1000000h
  004f015d  897e18                mov       [esi+18h],edi
  004f0160  8906                  mov       [esi],eax

LAB_004f0162:
  004f0162  8a4643                mov       al,[esi+43h]
  004f0165  84c0                  test      al,al
  004f0167  7405                  je        short 004F016Eh
  004f0169  fec8                  dec       al
  004f016b  884643                mov       [esi+43h],al

LAB_004f016e:
  004f016e  8b542440              mov       edx,[esp+40h]
  004f0172  0fbf4610              movsx     eax,word [esi+10h]
  004f0176  83c240                add       edx,40h
  004f0179  50                    push      eax
  004f017a  c1e207                shl       edx,7
  004f017d  89565c                mov       [esi+5Ch],edx
  004f0180  e897580500            call      00545A1Ch
  004f0185  0faf465c              imul      eax,[esi+5Ch]
  004f0189  8b4e50                mov       ecx,[esi+50h]
  004f018c  c1f80c                sar       eax,0Ch
  004f018f  03c8                  add       ecx,eax
  004f0191  894e50                mov       [esi+50h],ecx
  004f0194  0fbf4e10              movsx     ecx,word [esi+10h]
  004f0198  51                    push      ecx
  004f0199  e878580500            call      00545A16h
  004f019e  0faf465c              imul      eax,[esi+5Ch]
  004f01a2  8b5e58                mov       ebx,[esi+58h]
  004f01a5  8b4c2410              mov       ecx,[esp+10h]
  004f01a9  c1f80c                sar       eax,0Ch
  004f01ac  03d8                  add       ebx,eax
  004f01ae  8b442440              mov       eax,[esp+40h]
  004f01b2  c1f802                sar       eax,2
  004f01b5  83c040                add       eax,40h
  004f01b8  897e5c                mov       [esi+5Ch],edi
  004f01bb  895e58                mov       [esi+58h],ebx
  004f01be  8d1408                lea       edx,[eax+ecx]
  004f01c1  8d3c00                lea       edi,[eax+eax]
  004f01c4  81e2ff0f0000          and       edx,0FFFh
  004f01ca  3bd7                  cmp       edx,edi
  004f01cc  7f06                  jg        short 004F01D4h
  004f01ce  66014e32              add       [esi+32h],cx
  004f01d2  eb14                  jmp       short 004F01E8h

LAB_004f01d4:
  004f01d4  81e1ff0f0000          and       ecx,0FFFh
  004f01da  81f900080000          cmp       ecx,800h
  004f01e0  7c02                  jl        short 004F01E4h

LAB_004f01e2:
  004f01e2  f7d8                  neg       eax

LAB_004f01e4:
  004f01e4  66014632              add       [esi+32h],ax

LAB_004f01e8:
  004f01e8  8b44241c              mov       eax,[esp+1Ch]
  004f01ec  50                    push      eax
  004f01ed  56                    push      esi
  004f01ee  e8eda6faff            call      0049A8E0h
  004f01f3  83c408                add       esp,8
  004f01f6  56                    push      esi
  004f01f7  e844370400            call      00533940h
  004f01fc  8b466c                mov       eax,[esi+6Ch]
  004f01ff  83c404                add       esp,4
  004f0202  f60001                test      byte [eax],1
  004f0205  7506                  jne       short 004F020Dh
  004f0207  8b4860                mov       ecx,[eax+60h]
  004f020a  894e48                mov       [esi+48h],ecx

LAB_004f020d:
  004f020d  e82e6f0500            call      00547140h
  004f0212  8b7e60                mov       edi,[esi+60h]
  004f0215  25ff000000            and       eax,0FFh
  004f021a  0500010000            add       eax,100h
  004f021f  66014614              add       [esi+14h],ax
  004f0223  8b4648                mov       eax,[esi+48h]
  004f0226  668b4e14              mov       cx,[esi+14h]
  004f022a  3bc7                  cmp       eax,edi
  004f022c  7c02                  jl        short 004F0230h
  004f022e  8bf8                  mov       edi,eax

LAB_004f0230:
  004f0230  f60602                test      byte [esi],2
  004f0233  7415                  je        short 004F024Ah
  004f0235  8b4620                mov       eax,[esi+20h]
  004f0238  2bf8                  sub       edi,eax
  004f023a  81c700800200          add       edi,28000h
  004f0240  c1ff03                sar       edi,3
  004f0243  03f8                  add       edi,eax
  004f0245  897e20                mov       [esi+20h],edi
  004f0248  eb20                  jmp       short 004F026Ah

LAB_004f024a:
  004f024a  8b6e20                mov       ebp,[esi+20h]
  004f024d  0fbfd1                movsx     edx,cx
  004f0250  52                    push      edx
  004f0251  e8c6570500            call      00545A1Ch
  004f0256  0500320000            add       eax,3200h
  004f025b  c1e006                shl       eax,6
  004f025e  03c7                  add       eax,edi
  004f0260  2bc5                  sub       eax,ebp
  004f0262  c1f804                sar       eax,4
  004f0265  03c5                  add       eax,ebp
  004f0267  894620                mov       [esi+20h],eax

LAB_004f026a:
  004f026a  66837e160a            cmp       word [esi+16h],0Ah
  004f026f  7c0d                  jl        short 004F027Eh
  004f0271  8b442424              mov       eax,[esp+24h]
  004f0275  50                    push      eax
  004f0276  56                    push      esi
  004f0277  e8f434ffff            call      004E3770h
  004f027c  eb1d                  jmp       short 004F029Bh

LAB_004f027e:
  004f027e  8bbe90000000          mov       edi,[esi+90h]
  004f0284  8b4c2424              mov       ecx,[esp+24h]
  004f0288  81e7ffd8ffdf          and       edi,0DFFFD8FFh
  004f028e  51                    push      ecx
  004f028f  56                    push      esi
  004f0290  89be90000000          mov       [esi+90h],edi
  004f0296  e8551afbff            call      004A1CF0h

LAB_004f029b:
  004f029b  8b442424              mov       eax,[esp+24h]
  004f029f  83c408                add       esp,8
  004f02a2  85c0                  test      eax,eax
  004f02a4  0f8461020000          je        near 004F050Bh
  004f02aa  f70616080000          test      dword [esi],816h
  004f02b0  0f8555020000          jne       near 004F050Bh
  004f02b6  8a4643                mov       al,[esi+43h]
  004f02b9  84c0                  test      al,al
  004f02bb  0f854a020000          jne       near 004F050Bh
  004f02c1  8b466c                mov       eax,[esi+6Ch]
  004f02c4  8b5e1c                mov       ebx,[esi+1Ch]
  004f02c7  8b4e24                mov       ecx,[esi+24h]
  004f02ca  8b781c                mov       edi,[eax+1Ch]
  004f02cd  8b6824                mov       ebp,[eax+24h]
  004f02d0  2bfb                  sub       edi,ebx
  004f02d2  2be9                  sub       ebp,ecx
  004f02d4  c1ff0c                sar       edi,0Ch
  004f02d7  c1fd0c                sar       ebp,0Ch
  004f02da  8bd7                  mov       edx,edi
  004f02dc  8bc5                  mov       eax,ebp
  004f02de  0fafd7                imul      edx,edi
  004f02e1  0fafc5                imul      eax,ebp
  004f02e4  03d0                  add       edx,eax
  004f02e6  52                    push      edx
  004f02e7  e824570500            call      00545A10h
  004f02ec  55                    push      ebp
  004f02ed  57                    push      edi
  004f02ee  89442420              mov       [esp+20h],eax
  004f02f2  e82b570500            call      00545A22h
  004f02f7  668b4e32              mov       cx,[esi+32h]
  004f02fb  2bc1                  sub       eax,ecx
  004f02fd  25ff0f0000            and       eax,0FFFh
  004f0302  89442410              mov       [esp+10h],eax
  004f0306  8b442418              mov       eax,[esp+18h]
  004f030a  3dfa000000            cmp       eax,0FAh
  004f030f  0f8da3000000          jge       near 004F03B8h
  004f0315  e8266e0500            call      00547140h
  004f031a  99                    cdq
  004f031b  b964000000            mov       ecx,64h
  004f0320  f7f9                  idiv      ecx
  004f0322  83fa3c                cmp       edx,3Ch
  004f0325  7d60                  jge       short 004F0387h
  004f0327  8b542410              mov       edx,[esp+10h]
  004f032b  8b7c2414              mov       edi,[esp+14h]
  004f032f  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004f0336  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004f033d  25ff0f0000            and       eax,0FFFh
  004f0342  3bc1                  cmp       eax,ecx
  004f0344  0f8f89010000          jg        near 004F04D3h
  004f034a  e8f16d0500            call      00547140h
  004f034f  99                    cdq
  004f0350  8d4c3f01              lea       ecx,[edi+edi+1]
  004f0354  f7f9                  idiv      ecx
  004f0356  8b4c2418              mov       ecx,[esp+18h]
  004f035a  b82c010000            mov       eax,12Ch
  004f035f  2bc2                  sub       eax,edx
  004f0361  3bc8                  cmp       ecx,eax
  004f0363  0f8d6a010000          jge       near 004F04D3h
  004f0369  8b1e                  mov       ebx,[esi]
  004f036b  8bcf                  mov       ecx,edi
  004f036d  c1f903                sar       ecx,3
  004f0370  83cb02                or        ebx,2
  004f0373  80c110                add       cl,10h
  004f0376  891e                  mov       [esi],ebx
  004f0378  c6464200              mov       byte [esi+42h],0
  004f037c  884e43                mov       [esi+43h],cl
  004f037f  5f                    pop       edi
  004f0380  5e                    pop       esi
  004f0381  5d                    pop       ebp
  004f0382  5b                    pop       ebx
  004f0383  83c42c                add       esp,2Ch
  004f0386  c3                    ret

LAB_004f0387:
  004f0387  c6464203              mov       byte [esi+42h],3
  004f038b  e8b06d0500            call      00547140h
  004f0390  99                    cdq
  004f0391  b90a000000            mov       ecx,0Ah
  004f0396  f7f9                  idiv      ecx
  004f0398  8b442414              mov       eax,[esp+14h]
  004f039c  c1f802                sar       eax,2
  004f039f  02d0                  add       dl,al
  004f03a1  02d1                  add       dl,cl
  004f03a3  8b4c2440              mov       ecx,[esp+40h]
  004f03a7  885643                mov       [esi+43h],dl
  004f03aa  8db980000000          lea       edi,[ecx+80h]
  004f03b0  c1e704                shl       edi,4
  004f03b3  e98b000000            jmp       004F0443h

LAB_004f03b8:
  004f03b8  3df4010000            cmp       eax,1F4h
  004f03bd  0f8dd2000000          jge       near 004F0495h
  004f03c3  e8786d0500            call      00547140h
  004f03c8  99                    cdq
  004f03c9  b964000000            mov       ecx,64h
  004f03ce  f7f9                  idiv      ecx
  004f03d0  83fa3c                cmp       edx,3Ch
  004f03d3  0f8d8c000000          jge       near 004F0465h
  004f03d9  8b542410              mov       edx,[esp+10h]
  004f03dd  8b7c2414              mov       edi,[esp+14h]
  004f03e1  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004f03e8  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004f03ef  25ff0f0000            and       eax,0FFFh
  004f03f4  3bc1                  cmp       eax,ecx
  004f03f6  7f26                  jg        short 004F041Eh
  004f03f8  8b16                  mov       edx,[esi]
  004f03fa  c7461804000000        mov       dword [esi+18h],4
  004f0401  81e2fffffffe          and       edx,0FEFFFFFFh
  004f0407  c6464201              mov       byte [esi+42h],1
  004f040b  83ca02                or        edx,2
  004f040e  66c746100100          mov       word [esi+10h],1
  004f0414  8916                  mov       [esi],edx
  004f0416  5f                    pop       edi
  004f0417  5e                    pop       esi
  004f0418  5d                    pop       ebp
  004f0419  5b                    pop       ebx
  004f041a  83c42c                add       esp,2Ch
  004f041d  c3                    ret

LAB_004f041e:
  004f041e  c6464203              mov       byte [esi+42h],3
  004f0422  e8196d0500            call      00547140h
  004f0427  99                    cdq
  004f0428  b90a000000            mov       ecx,0Ah
  004f042d  f7f9                  idiv      ecx
  004f042f  8bc7                  mov       eax,edi
  004f0431  8b7c2440              mov       edi,[esp+40h]
  004f0435  c1f802                sar       eax,2
  004f0438  c1e704                shl       edi,4
  004f043b  02d0                  add       dl,al
  004f043d  02d1                  add       dl,cl
  004f043f  47                    inc       edi
  004f0440  885643                mov       [esi+43h],dl

LAB_004f0443:
  004f0443  e8f86c0500            call      00547140h
  004f0448  99                    cdq
  004f0449  f7ff                  idiv      edi
  004f044b  8b4c2410              mov       ecx,[esp+10h]
  004f044f  d1ff                  sar       edi,1
  004f0451  2bd7                  sub       edx,edi
  004f0453  03d1                  add       edx,ecx
  004f0455  66035632              add       dx,[esi+32h]
  004f0459  66895610              mov       [esi+10h],dx
  004f045d  5f                    pop       edi
  004f045e  5e                    pop       esi
  004f045f  5d                    pop       ebp
  004f0460  5b                    pop       ebx
  004f0461  83c42c                add       esp,2Ch
  004f0464  c3                    ret

LAB_004f0465:
  004f0465  83fa50                cmp       edx,50h
  004f0468  0f8d9d000000          jge       near 004F050Bh
  004f046e  c6464202              mov       byte [esi+42h],2
  004f0472  e8c96c0500            call      00547140h
  004f0477  99                    cdq
  004f0478  b90a000000            mov       ecx,0Ah
  004f047d  f7f9                  idiv      ecx
  004f047f  8b442414              mov       eax,[esp+14h]
  004f0483  c1f802                sar       eax,2
  004f0486  02d0                  add       dl,al
  004f0488  02d1                  add       dl,cl
  004f048a  885643                mov       [esi+43h],dl
  004f048d  5f                    pop       edi
  004f048e  5e                    pop       esi
  004f048f  5d                    pop       ebp
  004f0490  5b                    pop       ebx
  004f0491  83c42c                add       esp,2Ch
  004f0494  c3                    ret

LAB_004f0495:
  004f0495  e8a66c0500            call      00547140h
  004f049a  99                    cdq
  004f049b  b964000000            mov       ecx,64h
  004f04a0  f7f9                  idiv      ecx
  004f04a2  83fa1e                cmp       edx,1Eh
  004f04a5  7d27                  jge       short 004F04CEh
  004f04a7  c6464200              mov       byte [esi+42h],0
  004f04ab  e8906c0500            call      00547140h
  004f04b0  99                    cdq
  004f04b1  b90a000000            mov       ecx,0Ah
  004f04b6  f7f9                  idiv      ecx
  004f04b8  8b442414              mov       eax,[esp+14h]
  004f04bc  c1f802                sar       eax,2
  004f04bf  02d0                  add       dl,al
  004f04c1  02d1                  add       dl,cl
  004f04c3  885643                mov       [esi+43h],dl
  004f04c6  5f                    pop       edi
  004f04c7  5e                    pop       esi
  004f04c8  5d                    pop       ebp
  004f04c9  5b                    pop       ebx
  004f04ca  83c42c                add       esp,2Ch
  004f04cd  c3                    ret

LAB_004f04ce:
  004f04ce  83fa3c                cmp       edx,3Ch
  004f04d1  7d10                  jge       short 004F04E3h

LAB_004f04d3:
  004f04d3  c6464201              mov       byte [esi+42h],1
  004f04d7  c6464301              mov       byte [esi+43h],1
  004f04db  5f                    pop       edi
  004f04dc  5e                    pop       esi
  004f04dd  5d                    pop       ebp
  004f04de  5b                    pop       ebx
  004f04df  83c42c                add       esp,2Ch
  004f04e2  c3                    ret

LAB_004f04e3:
  004f04e3  c6464203              mov       byte [esi+42h],3
  004f04e7  e8546c0500            call      00547140h
  004f04ec  99                    cdq
  004f04ed  b90a000000            mov       ecx,0Ah
  004f04f2  f7f9                  idiv      ecx
  004f04f4  8b442414              mov       eax,[esp+14h]
  004f04f8  c1f802                sar       eax,2
  004f04fb  02d0                  add       dl,al
  004f04fd  02d1                  add       dl,cl
  004f04ff  885643                mov       [esi+43h],dl
  004f0502  e8396c0500            call      00547140h
  004f0507  66894610              mov       [esi+10h],ax

LAB_004f050b:
  004f050b  5f                    pop       edi
  004f050c  5e                    pop       esi
  004f050d  5d                    pop       ebp
  004f050e  5b                    pop       ebx
  004f050f  83c42c                add       esp,2Ch
  004f0512  c3                    ret
  004f0513  90                    nop
  004f0514  0e                    push      cs
  004f0515  004f00                add       [edi],cl
  004f0518  54                    push      esp
  004f0519  004f00                add       [edi],cl
  004f051c  c7004f005001          mov       dword [eax],150004Fh
  004f0522  4f                    dec       edi
  004f0523  009090909090          add       [eax-6F6F6F70h],dl
  004f0529  90                    nop
  004f052a  90                    nop
  004f052b  90                    nop
  004f052c  90                    nop
  004f052d  90                    nop
  004f052e  90                    nop
  004f052f  90                    nop

52 cible(s) de saut interne : 0x004efcb1, 0x004efd3c, 0x004efd40, 0x004efd62, 0x004efd79, 0x004efdb4, 0x004efde7, 0x004efdf7, 0x004efdfb, 0x004efe1b, 0x004efec8, 0x004eff17, 0x004eff27, 0x004eff4a, 0x004eff5a, 0x004eff5e, 0x004effac, 0x004effe7, 0x004f0023, 0x004f0036, 0x004f0042, 0x004f0069, 0x004f00a1, 0x004f00b1, 0x004f00dc, 0x004f00ef, 0x004f00fb, 0x004f0121, 0x004f0131, 0x004f0135, 0x004f0162, 0x004f016e, 0x004f01d4, 0x004f01e2, 0x004f01e4, 0x004f01e8, 0x004f020d, 0x004f0230, 0x004f024a, 0x004f026a, 0x004f027e, 0x004f029b, 0x004f0387, 0x004f03b8, 0x004f041e, 0x004f0443, 0x004f0465, 0x004f0495, 0x004f04ce, 0x004f04d3, 0x004f04e3, 0x004f050b
