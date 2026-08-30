C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004433b0  section .text  offset fichier 0x427b0  1400 octets

  004433b0  51                    push      ecx
  004433b1  a0ff965e00            mov       al,[5E96FFh]
  004433b6  668b0dcaa75f00        mov       cx,[5FA7CAh]
  004433bd  53                    push      ebx
  004433be  8ad0                  mov       dl,al
  004433c0  55                    push      ebp
  004433c1  56                    push      esi
  004433c2  80e201                and       dl,1
  004433c5  57                    push      edi
  004433c6  88542412              mov       [esp+12h],dl
  004433ca  beffffffff            mov       esi,0FFFFFFFFh
  004433cf  bf63000000            mov       edi,63h
  004433d4  750c                  jne       short 004433E2h
  004433d6  663bce                cmp       cx,si
  004433d9  7507                  jne       short 004433E2h
  004433db  66893d90a65f00        mov       [5FA690h],di

LAB_004433e2:
  004433e2  84d2                  test      dl,dl
  004433e4  7510                  jne       short 004433F6h
  004433e6  66393592a65f00        cmp       [5FA692h],si
  004433ed  7507                  jne       short 004433F6h
  004433ef  66893dc8a75f00        mov       [5FA7C8h],di

LAB_004433f6:
  004433f6  2402                  and       al,2
  004433f8  88442413              mov       [esp+13h],al
  004433fc  7510                  jne       short 0044340Eh
  004433fe  663935f6a55f00        cmp       [5FA5F6h],si
  00443405  7507                  jne       short 0044340Eh
  00443407  66893d2ca75f00        mov       [5FA72Ch],di

LAB_0044340e:
  0044340e  84c0                  test      al,al
  00443410  7510                  jne       short 00443422h
  00443412  6639352ea75f00        cmp       [5FA72Eh],si
  00443419  7507                  jne       short 00443422h
  0044341b  66893df4a55f00        mov       [5FA5F4h],di

LAB_00443422:
  00443422  84d2                  test      dl,dl
  00443424  0f85f8010000          jne       near 00443622h
  0044342a  6683f901              cmp       cx,1
  0044342e  0f85ee000000          jne       near 00443522h
  00443434  66393dc8a75f00        cmp       [5FA7C8h],di
  0044343b  0f85e1000000          jne       near 00443522h
  00443441  33f6                  xor       esi,esi
  00443443  33ed                  xor       ebp,ebp
  00443445  bac0935e00            mov       edx,5E93C0h
  0044344a  bf04000000            mov       edi,4

LAB_0044344f:
  0044344f  8b02                  mov       eax,[edx]
  00443451  a900000080            test      eax,80000000h
  00443456  0f848f000000          je        near 004434EBh
  0044345c  a900000050            test      eax,50000000h
  00443461  0f8584000000          jne       near 004434EBh
  00443467  8b5a20                mov       ebx,[edx+20h]
  0044346a  8b421c                mov       eax,[edx+1Ch]
  0044346d  8b4a24                mov       ecx,[edx+24h]
  00443470  81e300f0ffff          and       ebx,0FFFFF000h
  00443476  46                    inc       esi
  00443477  c1f80c                sar       eax,0Ch
  0044347a  c1f90c                sar       ecx,0Ch
  0044347d  81fb00000500          cmp       ebx,50000h
  00443483  7c66                  jl        short 004434EBh
  00443485  8b9a90000000          mov       ebx,[edx+90h]
  0044348b  f6c720                test      bh,20h
  0044348e  755b                  jne       short 004434EBh
  00443490  3d98efffff            cmp       eax,0FFFFEF98h
  00443495  7c54                  jl        short 004434EBh
  00443497  3d6cf3ffff            cmp       eax,0FFFFF36Ch
  0044349c  7f10                  jg        short 004434AEh
  0044349e  81f9c2010000          cmp       ecx,1C2h
  004434a4  7c08                  jl        short 004434AEh
  004434a6  81f90c030000          cmp       ecx,30Ch
  004434ac  7e3c                  jle       short 004434EAh

LAB_004434ae:
  004434ae  3d98efffff            cmp       eax,0FFFFEF98h
  004434b3  7c36                  jl        short 004434EBh
  004434b5  3db8f2ffff            cmp       eax,0FFFFF2B8h
  004434ba  7f10                  jg        short 004434CCh
  004434bc  81f996000000          cmp       ecx,96h
  004434c2  7c08                  jl        short 004434CCh
  004434c4  81f9c2010000          cmp       ecx,1C2h
  004434ca  7e1e                  jle       short 004434EAh

LAB_004434cc:
  004434cc  3d98efffff            cmp       eax,0FFFFEF98h
  004434d1  7c18                  jl        short 004434EBh
  004434d3  3d6cf3ffff            cmp       eax,0FFFFF36Ch
  004434d8  7f11                  jg        short 004434EBh
  004434da  81f938ffffff          cmp       ecx,0FFFFFF38h
  004434e0  7c09                  jl        short 004434EBh
  004434e2  81f996000000          cmp       ecx,96h
  004434e8  7f01                  jg        short 004434EBh

LAB_004434ea:
  004434ea  45                    inc       ebp

LAB_004434eb:
  004434eb  81c29c000000          add       edx,9Ch
  004434f1  4f                    dec       edi
  004434f2  0f8557ffffff          jne       near 0044344Fh
  004434f8  663bf5                cmp       si,bp
  004434fb  751c                  jne       short 00443519h
  004434fd  6685f6                test      si,si
  00443500  7417                  je        short 00443519h
  00443502  6685ed                test      bp,bp
  00443505  7412                  je        short 00443519h
  00443507  66c705c8a75f00ce00    mov       word [5FA7C8h],0CEh
  00443510  66c7054ca85f000100    mov       word [5FA84Ch],1

LAB_00443519:
  00443519  8a542412              mov       dl,[esp+12h]
  0044351d  bf63000000            mov       edi,63h

LAB_00443522:
  00443522  84d2                  test      dl,dl
  00443524  0f85f8000000          jne       near 00443622h
  0044352a  66833d92a65f0001      cmp       word [5FA692h],1
  00443532  0f85ea000000          jne       near 00443622h
  00443538  66393d90a65f00        cmp       [5FA690h],di
  0044353f  0f85dd000000          jne       near 00443622h
  00443545  33f6                  xor       esi,esi
  00443547  33ed                  xor       ebp,ebp
  00443549  bac0935e00            mov       edx,5E93C0h
  0044354e  bf04000000            mov       edi,4

LAB_00443553:
  00443553  8b02                  mov       eax,[edx]
  00443555  a900000080            test      eax,80000000h
  0044355a  0f848f000000          je        near 004435EFh
  00443560  a900000050            test      eax,50000000h
  00443565  0f8584000000          jne       near 004435EFh
  0044356b  8b5a20                mov       ebx,[edx+20h]
  0044356e  8b421c                mov       eax,[edx+1Ch]
  00443571  8b4a24                mov       ecx,[edx+24h]
  00443574  81e300f0ffff          and       ebx,0FFFFF000h
  0044357a  46                    inc       esi
  0044357b  c1f80c                sar       eax,0Ch
  0044357e  c1f90c                sar       ecx,0Ch
  00443581  81fb00000500          cmp       ebx,50000h
  00443587  7c66                  jl        short 004435EFh
  00443589  8b9a90000000          mov       ebx,[edx+90h]
  0044358f  f6c720                test      bh,20h
  00443592  755b                  jne       short 004435EFh
  00443594  3d98efffff            cmp       eax,0FFFFEF98h
  00443599  7c17                  jl        short 004435B2h
  0044359b  3df6f0ffff            cmp       eax,0FFFFF0F6h
  004435a0  7f17                  jg        short 004435B9h
  004435a2  81f938ffffff          cmp       ecx,0FFFFFF38h
  004435a8  7c08                  jl        short 004435B2h
  004435aa  81f90c030000          cmp       ecx,30Ch
  004435b0  7e3c                  jle       short 004435EEh

LAB_004435b2:
  004435b2  3df6f0ffff            cmp       eax,0FFFFF0F6h
  004435b7  7c17                  jl        short 004435D0h

LAB_004435b9:
  004435b9  3d22f2ffff            cmp       eax,0FFFFF222h
  004435be  7f17                  jg        short 004435D7h
  004435c0  81f938ffffff          cmp       ecx,0FFFFFF38h
  004435c6  7c08                  jl        short 004435D0h
  004435c8  81f958020000          cmp       ecx,258h
  004435ce  7e1e                  jle       short 004435EEh

LAB_004435d0:
  004435d0  3d22f2ffff            cmp       eax,0FFFFF222h
  004435d5  7c18                  jl        short 004435EFh

LAB_004435d7:
  004435d7  3d6cf3ffff            cmp       eax,0FFFFF36Ch
  004435dc  7f11                  jg        short 004435EFh
  004435de  81f938ffffff          cmp       ecx,0FFFFFF38h
  004435e4  7c09                  jl        short 004435EFh
  004435e6  81f90c030000          cmp       ecx,30Ch
  004435ec  7f01                  jg        short 004435EFh

LAB_004435ee:
  004435ee  45                    inc       ebp

LAB_004435ef:
  004435ef  81c29c000000          add       edx,9Ch
  004435f5  4f                    dec       edi
  004435f6  0f8557ffffff          jne       near 00443553h
  004435fc  663bf5                cmp       si,bp
  004435ff  751c                  jne       short 0044361Dh
  00443601  6685f6                test      si,si
  00443604  7417                  je        short 0044361Dh
  00443606  6685ed                test      bp,bp
  00443609  7412                  je        short 0044361Dh
  0044360b  66c70590a65f00ce00    mov       word [5FA690h],0CEh
  00443614  66c70514a75f000100    mov       word [5FA714h],1

LAB_0044361d:
  0044361d  bf63000000            mov       edi,63h

LAB_00443622:
  00443622  8a442413              mov       al,[esp+13h]
  00443626  84c0                  test      al,al
  00443628  0f85f8010000          jne       near 00443826h
  0044362e  66833df6a55f0001      cmp       word [5FA5F6h],1
  00443636  0f85ea000000          jne       near 00443726h
  0044363c  66393df4a55f00        cmp       [5FA5F4h],di
  00443643  0f85dd000000          jne       near 00443726h
  00443649  33f6                  xor       esi,esi
  0044364b  33ed                  xor       ebp,ebp
  0044364d  bac0935e00            mov       edx,5E93C0h
  00443652  bf04000000            mov       edi,4

LAB_00443657:
  00443657  8b02                  mov       eax,[edx]
  00443659  a900000080            test      eax,80000000h
  0044365e  0f848f000000          je        near 004436F3h
  00443664  a900000050            test      eax,50000000h
  00443669  0f8584000000          jne       near 004436F3h
  0044366f  8b5a20                mov       ebx,[edx+20h]
  00443672  8b421c                mov       eax,[edx+1Ch]
  00443675  8b4a24                mov       ecx,[edx+24h]
  00443678  81e300f0ffff          and       ebx,0FFFFF000h
  0044367e  46                    inc       esi
  0044367f  c1f80c                sar       eax,0Ch
  00443682  c1f90c                sar       ecx,0Ch
  00443685  81fb00000500          cmp       ebx,50000h
  0044368b  7c66                  jl        short 004436F3h
  0044368d  8b9a90000000          mov       ebx,[edx+90h]
  00443693  f6c720                test      bh,20h
  00443696  755b                  jne       short 004436F3h
  00443698  3d98efffff            cmp       eax,0FFFFEF98h
  0044369d  7c17                  jl        short 004436B6h
  0044369f  3df6f0ffff            cmp       eax,0FFFFF0F6h
  004436a4  7f17                  jg        short 004436BDh
  004436a6  81f9e4070000          cmp       ecx,7E4h
  004436ac  7c08                  jl        short 004436B6h
  004436ae  81f9b80b0000          cmp       ecx,0BB8h
  004436b4  7e3c                  jle       short 004436F2h

LAB_004436b6:
  004436b6  3df6f0ffff            cmp       eax,0FFFFF0F6h
  004436bb  7c17                  jl        short 004436D4h

LAB_004436bd:
  004436bd  3d22f2ffff            cmp       eax,0FFFFF222h
  004436c2  7f17                  jg        short 004436DBh
  004436c4  81f998080000          cmp       ecx,898h
  004436ca  7c08                  jl        short 004436D4h
  004436cc  81f9b80b0000          cmp       ecx,0BB8h
  004436d2  7e1e                  jle       short 004436F2h

LAB_004436d4:
  004436d4  3d22f2ffff            cmp       eax,0FFFFF222h
  004436d9  7c18                  jl        short 004436F3h

LAB_004436db:
  004436db  3d6cf3ffff            cmp       eax,0FFFFF36Ch
  004436e0  7f11                  jg        short 004436F3h
  004436e2  81f9e4070000          cmp       ecx,7E4h
  004436e8  7c09                  jl        short 004436F3h
  004436ea  81f9b80b0000          cmp       ecx,0BB8h
  004436f0  7f01                  jg        short 004436F3h

LAB_004436f2:
  004436f2  45                    inc       ebp

LAB_004436f3:
  004436f3  81c29c000000          add       edx,9Ch
  004436f9  4f                    dec       edi
  004436fa  0f8557ffffff          jne       near 00443657h
  00443700  663bf5                cmp       si,bp
  00443703  751c                  jne       short 00443721h
  00443705  6685f6                test      si,si
  00443708  7417                  je        short 00443721h
  0044370a  6685ed                test      bp,bp
  0044370d  7412                  je        short 00443721h
  0044370f  66c705f4a55f00ce00    mov       word [5FA5F4h],0CEh
  00443718  66c70578a65f000100    mov       word [5FA678h],1

LAB_00443721:
  00443721  bf63000000            mov       edi,63h

LAB_00443726:
  00443726  8a442413              mov       al,[esp+13h]
  0044372a  84c0                  test      al,al
  0044372c  0f85f4000000          jne       near 00443826h
  00443732  66833d2ea75f0001      cmp       word [5FA72Eh],1
  0044373a  0f85e6000000          jne       near 00443826h
  00443740  66393d2ca75f00        cmp       [5FA72Ch],di
  00443747  0f85d9000000          jne       near 00443826h
  0044374d  33f6                  xor       esi,esi
  0044374f  33db                  xor       ebx,ebx
  00443751  bac0935e00            mov       edx,5E93C0h
  00443756  bf04000000            mov       edi,4

LAB_0044375b:
  0044375b  8b02                  mov       eax,[edx]
  0044375d  a900000080            test      eax,80000000h
  00443762  0f8490000000          je        near 004437F8h
  00443768  a900000050            test      eax,50000000h
  0044376d  0f8585000000          jne       near 004437F8h
  00443773  8b6a20                mov       ebp,[edx+20h]
  00443776  8b421c                mov       eax,[edx+1Ch]
  00443779  8b4a24                mov       ecx,[edx+24h]
  0044377c  81e500f0ffff          and       ebp,0FFFFF000h
  00443782  46                    inc       esi
  00443783  c1f80c                sar       eax,0Ch
  00443786  c1f90c                sar       ecx,0Ch
  00443789  81fd00000500          cmp       ebp,50000h
  0044378f  7c67                  jl        short 004437F8h
  00443791  f7829000000000200000  test      dword [edx+90h],2000h
  0044379b  755b                  jne       short 004437F8h
  0044379d  3d98efffff            cmp       eax,0FFFFEF98h
  004437a2  7c54                  jl        short 004437F8h
  004437a4  3d6cf3ffff            cmp       eax,0FFFFF36Ch
  004437a9  7f10                  jg        short 004437BBh
  004437ab  81f95a0a0000          cmp       ecx,0A5Ah
  004437b1  7c08                  jl        short 004437BBh
  004437b3  81f9b80b0000          cmp       ecx,0BB8h
  004437b9  7e3c                  jle       short 004437F7h

LAB_004437bb:
  004437bb  3d98efffff            cmp       eax,0FFFFEF98h
  004437c0  7c36                  jl        short 004437F8h
  004437c2  3db8f2ffff            cmp       eax,0FFFFF2B8h
  004437c7  7f10                  jg        short 004437D9h
  004437c9  81f92e090000          cmp       ecx,92Eh
  004437cf  7c08                  jl        short 004437D9h
  004437d1  81f95a0a0000          cmp       ecx,0A5Ah
  004437d7  7e1e                  jle       short 004437F7h

LAB_004437d9:
  004437d9  3d98efffff            cmp       eax,0FFFFEF98h
  004437de  7c18                  jl        short 004437F8h
  004437e0  3d6cf3ffff            cmp       eax,0FFFFF36Ch
  004437e5  7f11                  jg        short 004437F8h
  004437e7  81f9e4070000          cmp       ecx,7E4h
  004437ed  7c09                  jl        short 004437F8h
  004437ef  81f92e090000          cmp       ecx,92Eh
  004437f5  7f01                  jg        short 004437F8h

LAB_004437f7:
  004437f7  43                    inc       ebx

LAB_004437f8:
  004437f8  81c29c000000          add       edx,9Ch
  004437fe  4f                    dec       edi
  004437ff  0f8556ffffff          jne       near 0044375Bh
  00443805  663bf3                cmp       si,bx
  00443808  751c                  jne       short 00443826h
  0044380a  6685f6                test      si,si
  0044380d  7417                  je        short 00443826h
  0044380f  6685db                test      bx,bx
  00443812  7412                  je        short 00443826h
  00443814  66c7052ca75f00ce00    mov       word [5FA72Ch],0CEh
  0044381d  66c705b0a75f000100    mov       word [5FA7B0h],1

LAB_00443826:
  00443826  5f                    pop       edi
  00443827  5e                    pop       esi
  00443828  5d                    pop       ebp
  00443829  33c0                  xor       eax,eax
  0044382b  5b                    pop       ebx
  0044382c  59                    pop       ecx
  0044382d  c3                    ret
  0044382e  90                    nop
  0044382f  90                    nop
  00443830  51                    push      ecx
  00443831  53                    push      ebx
  00443832  8b1db4876000          mov       ebx,[6087B4h]
  00443838  55                    push      ebp
  00443839  56                    push      esi
  0044383a  57                    push      edi
  0044383b  668b7c241c            mov       di,[esp+1Ch]
  00443840  c744241001000000      mov       dword [esp+10h],1
  00443848  b900000f00            mov       ecx,0F0000h
  0044384d  bac0935e00            mov       edx,5E93C0h
  00443852  be04000000            mov       esi,4

LAB_00443857:
  00443857  8b02                  mov       eax,[edx]
  00443859  a900000080            test      eax,80000000h
  0044385e  7422                  je        short 00443882h
  00443860  a900000050            test      eax,50000000h
  00443865  751b                  jne       short 00443882h
  00443867  8b03                  mov       eax,[ebx]
  00443869  33ed                  xor       ebp,ebp
  0044386b  668bac0844010000      mov       bp,[eax+ecx+144h]
  00443873  0fbfc7                movsx     eax,di
  00443876  3be8                  cmp       ebp,eax
  00443878  7c08                  jl        short 00443882h
  0044387a  c744241000000000      mov       dword [esp+10h],0

LAB_00443882:
  00443882  81c100200000          add       ecx,2000h
  00443888  81c29c000000          add       edx,9Ch
  0044388e  4e                    dec       esi
  0044388f  75c6                  jne       short 00443857h
  00443891  0fbf442410            movsx     eax,word [esp+10h]
  00443896  5f                    pop       edi
  00443897  5e                    pop       esi
  00443898  5d                    pop       ebp
  00443899  5b                    pop       ebx
  0044389a  59                    pop       ecx
  0044389b  c3                    ret
  0044389c  90                    nop
  0044389d  90                    nop
  0044389e  90                    nop
  0044389f  90                    nop
  004438a0  a164905e00            mov       eax,[5E9064h]
  004438a5  8b0db4876000          mov       ecx,[6087B4h]
  004438ab  25ff000000            and       eax,0FFh
  004438b0  8b11                  mov       edx,[ecx]
  004438b2  83c078                add       eax,78h
  004438b5  c1e00d                shl       eax,0Dh
  004438b8  03c2                  add       eax,edx
  004438ba  33c9                  xor       ecx,ecx
  004438bc  8b905c010000          mov       edx,[eax+15Ch]
  004438c2  880d68905e00          mov       [5E9068h],cl
  004438c8  891564905e00          mov       [5E9064h],edx
  004438ce  8b905c010000          mov       edx,[eax+15Ch]
  004438d4  83fa0a                cmp       edx,0Ah
  004438d7  7205                  jb        short 004438DEh
  004438d9  b901000000            mov       ecx,1

LAB_004438de:
  004438de  83fa14                cmp       edx,14h
  004438e1  7201                  jb        short 004438E4h
  004438e3  41                    inc       ecx

LAB_004438e4:
  004438e4  83fa28                cmp       edx,28h
  004438e7  7201                  jb        short 004438EAh
  004438e9  41                    inc       ecx

LAB_004438ea:
  004438ea  83fa50                cmp       edx,50h
  004438ed  7201                  jb        short 004438F0h
  004438ef  41                    inc       ecx

LAB_004438f0:
  004438f0  81faa0000000          cmp       edx,0A0h
  004438f6  7201                  jb        short 004438F9h
  004438f8  41                    inc       ecx

LAB_004438f9:
  004438f9  81fa40010000          cmp       edx,140h
  004438ff  7201                  jb        short 00443902h
  00443901  41                    inc       ecx

LAB_00443902:
  00443902  81fa80020000          cmp       edx,280h
  00443908  7201                  jb        short 0044390Bh
  0044390a  41                    inc       ecx

LAB_0044390b:
  0044390b  81fa00050000          cmp       edx,500h
  00443911  7201                  jb        short 00443914h
  00443913  41                    inc       ecx

LAB_00443914:
  00443914  81fa000a0000          cmp       edx,0A00h
  0044391a  7201                  jb        short 0044391Dh
  0044391c  41                    inc       ecx

LAB_0044391d:
  0044391d  81fa00140000          cmp       edx,1400h
  00443923  7201                  jb        short 00443926h
  00443925  41                    inc       ecx

LAB_00443926:
  00443926  81fa                  (bad)
  (octet invalide — la plage demandee deborde probablement du code)
