Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00541550  section .text  offset fichier 0x140950  1584 octets

  00541550  83ec44                sub       esp,44h
  00541553  a1c07b5f00            mov       eax,[5F7BC0h]
  00541558  53                    push      ebx
  00541559  55                    push      ebp
  0054155a  8b6c2450              mov       ebp,[esp+50h]
  0054155e  8b4828                mov       ecx,[eax+28h]
  00541561  56                    push      esi
  00541562  894c2428              mov       [esp+28h],ecx
  00541566  8b4c2458              mov       ecx,[esp+58h]
  0054156a  57                    push      edi
  0054156b  c744244404000000      mov       dword [esp+44h],4
  00541573  8b01                  mov       eax,[ecx]
  00541575  c1f80c                sar       eax,0Ch
  00541578  89442420              mov       [esp+20h],eax
  0054157c  8b4108                mov       eax,[ecx+8]
  0054157f  c1f80c                sar       eax,0Ch
  00541582  89442410              mov       [esp+10h],eax
  00541586  8b451c                mov       eax,[ebp+1Ch]
  00541589  c1f80c                sar       eax,0Ch
  0054158c  89442418              mov       [esp+18h],eax
  00541590  8b4524                mov       eax,[ebp+24h]
  00541593  c1f80c                sar       eax,0Ch
  00541596  81c580000000          add       ebp,80h
  0054159c  89442414              mov       [esp+14h],eax
  005415a0  896c245c              mov       [esp+5Ch],ebp

LAB_005415a4:
  005415a4  c744243810000000      mov       dword [esp+38h],10h

LAB_005415ac:
  005415ac  8b4500                mov       eax,[ebp]
  005415af  8bc8                  mov       ecx,eax
  005415b1  89442450              mov       [esp+50h],eax
  005415b5  c1e910                shr       ecx,10h
  005415b8  f6c580                test      ch,80h
  005415bb  894c244c              mov       [esp+4Ch],ecx
  005415bf  0f8554040000          jne       near 00541A19h
  005415c5  8b4c242c              mov       ecx,[esp+2Ch]
  005415c9  25ffff0000            and       eax,0FFFFh
  005415ce  c1e004                shl       eax,4
  005415d1  03c1                  add       eax,ecx
  005415d3  8b1dc07c5f00          mov       ebx,[5F7CC0h]
  005415d9  89442428              mov       [esp+28h],eax
  005415dd  8b38                  mov       edi,[eax]
  005415df  8b5004                mov       edx,[eax+4]
  005415e2  8bcf                  mov       ecx,edi
  005415e4  8bf2                  mov       esi,edx
  005415e6  c1e910                shr       ecx,10h
  005415e9  894c2440              mov       [esp+40h],ecx
  005415ed  81e1ffff0000          and       ecx,0FFFFh
  005415f3  c1ee10                shr       esi,10h
  005415f6  0fbf2ccb              movsx     ebp,word [ebx+ecx*8]
  005415fa  81e7ffff0000          and       edi,0FFFFh
  00541600  8d0ccb                lea       ecx,[ebx+ecx*8]
  00541603  89742434              mov       [esp+34h],esi
  00541607  81e2ffff0000          and       edx,0FFFFh
  0054160d  8d34fb                lea       esi,[ebx+edi*8]
  00541610  668b5904              mov       bx,[ecx+4]
  00541614  8b0dc07c5f00          mov       ecx,[5F7CC0h]
  0054161a  896c2430              mov       [esp+30h],ebp
  0054161e  0fbf3e                movsx     edi,word [esi]
  00541621  668b7604              mov       si,[esi+4]
  00541625  8d14d1                lea       edx,[ecx+edx*8]
  00541628  0fbff6                movsx     esi,si
  0054162b  0fbf0a                movsx     ecx,word [edx]
  0054162e  668b5204              mov       dx,[edx+4]
  00541632  2bef                  sub       ebp,edi
  00541634  6689542448            mov       [esp+48h],dx
  00541639  897c2424              mov       [esp+24h],edi
  0054163d  0fbfd3                movsx     edx,bx
  00541640  8b5c2414              mov       ebx,[esp+14h]
  00541644  2bde                  sub       ebx,esi
  00541646  0fafdd                imul      ebx,ebp
  00541649  8b6c2418              mov       ebp,[esp+18h]
  0054164d  2bef                  sub       ebp,edi
  0054164f  8bfa                  mov       edi,edx
  00541651  2bfe                  sub       edi,esi
  00541653  0fafef                imul      ebp,edi
  00541656  2bdd                  sub       ebx,ebp
  00541658  7961                  jns       short 005416BBh
  0054165a  8b7c2414              mov       edi,[esp+14h]
  0054165e  8b5c2410              mov       ebx,[esp+10h]
  00541662  2bfb                  sub       edi,ebx
  00541664  8b5c2420              mov       ebx,[esp+20h]
  00541668  897c243c              mov       [esp+3Ch],edi
  0054166c  8b7c2418              mov       edi,[esp+18h]
  00541670  8bee                  mov       ebp,esi
  00541672  2bfb                  sub       edi,ebx
  00541674  2b6c2410              sub       ebp,[esp+10h]
  00541678  897c241c              mov       [esp+1Ch],edi
  0054167c  0fafef                imul      ebp,edi
  0054167f  8b7c2424              mov       edi,[esp+24h]
  00541683  2bfb                  sub       edi,ebx
  00541685  0faf7c243c            imul      edi,[esp+3Ch]
  0054168a  2bef                  sub       ebp,edi
  0054168c  85ed                  test      ebp,ebp
  0054168e  7f2b                  jg        short 005416BBh
  00541690  8b5c2410              mov       ebx,[esp+10h]
  00541694  8b6c2420              mov       ebp,[esp+20h]
  00541698  8bfa                  mov       edi,edx
  0054169a  2bfb                  sub       edi,ebx
  0054169c  8b5c2430              mov       ebx,[esp+30h]
  005416a0  0faf7c241c            imul      edi,[esp+1Ch]
  005416a5  2bdd                  sub       ebx,ebp
  005416a7  0faf5c243c            imul      ebx,[esp+3Ch]
  005416ac  2bfb                  sub       edi,ebx
  005416ae  780b                  js        short 005416BBh
  005416b0  8b08                  mov       ecx,[eax]
  005416b2  8b542440              mov       edx,[esp+40h]
  005416b6  e9fb010000            jmp       005418B6h

LAB_005416bb:
  005416bb  8b5c2414              mov       ebx,[esp+14h]
  005416bf  8b7c2430              mov       edi,[esp+30h]
  005416c3  0fbf442448            movsx     eax,word [esp+48h]
  005416c8  8be9                  mov       ebp,ecx
  005416ca  2bda                  sub       ebx,edx
  005416cc  2bef                  sub       ebp,edi
  005416ce  0fafdd                imul      ebx,ebp
  005416d1  8b6c2418              mov       ebp,[esp+18h]
  005416d5  2bef                  sub       ebp,edi
  005416d7  8bf8                  mov       edi,eax
  005416d9  2bfa                  sub       edi,edx
  005416db  0fafef                imul      ebp,edi
  005416de  2bdd                  sub       ebx,ebp
  005416e0  795a                  jns       short 0054173Ch
  005416e2  8b6c2414              mov       ebp,[esp+14h]
  005416e6  8b7c2410              mov       edi,[esp+10h]
  005416ea  8b5c2418              mov       ebx,[esp+18h]
  005416ee  2bef                  sub       ebp,edi
  005416f0  8b7c2420              mov       edi,[esp+20h]
  005416f4  2bdf                  sub       ebx,edi
  005416f6  895c241c              mov       [esp+1Ch],ebx
  005416fa  8b5c2410              mov       ebx,[esp+10h]
  005416fe  2bd3                  sub       edx,ebx
  00541700  8b5c2430              mov       ebx,[esp+30h]
  00541704  0faf54241c            imul      edx,[esp+1Ch]
  00541709  2bdf                  sub       ebx,edi
  0054170b  0fafdd                imul      ebx,ebp
  0054170e  2bd3                  sub       edx,ebx
  00541710  85d2                  test      edx,edx
  00541712  7f2c                  jg        short 00541740h
  00541714  8b5c2410              mov       ebx,[esp+10h]
  00541718  8bd0                  mov       edx,eax
  0054171a  2bd3                  sub       edx,ebx
  0054171c  8bd9                  mov       ebx,ecx
  0054171e  0faf54241c            imul      edx,[esp+1Ch]
  00541723  2bdf                  sub       ebx,edi
  00541725  0fafdd                imul      ebx,ebp
  00541728  2bd3                  sub       edx,ebx
  0054172a  7814                  js        short 00541740h
  0054172c  8b442428              mov       eax,[esp+28h]
  00541730  8b4c2440              mov       ecx,[esp+40h]
  00541734  8b5004                mov       edx,[eax+4]
  00541737  e97a010000            jmp       005418B6h

LAB_0054173c:
  0054173c  8b7c2420              mov       edi,[esp+20h]

LAB_00541740:
  00541740  f644244c01            test      byte [esp+4Ch],1
  00541745  0f8404010000          je        near 0054184Fh
  0054174b  8b542434              mov       edx,[esp+34h]
  0054174f  8b3dc07c5f00          mov       edi,[5F7CC0h]
  00541755  81e2ffff0000          and       edx,0FFFFh
  0054175b  8b5c2414              mov       ebx,[esp+14h]
  0054175f  2bd8                  sub       ebx,eax
  00541761  8d14d7                lea       edx,[edi+edx*8]
  00541764  0fbf3a                movsx     edi,word [edx]
  00541767  0fbf5204              movsx     edx,word [edx+4]
  0054176b  897c2430              mov       [esp+30h],edi
  0054176f  2bf9                  sub       edi,ecx
  00541771  0fafdf                imul      ebx,edi
  00541774  8b7c2418              mov       edi,[esp+18h]
  00541778  8bea                  mov       ebp,edx
  0054177a  2bf9                  sub       edi,ecx
  0054177c  2be8                  sub       ebp,eax
  0054177e  0faffd                imul      edi,ebp
  00541781  2bdf                  sub       ebx,edi
  00541783  795a                  jns       short 005417DFh
  00541785  8b5c2414              mov       ebx,[esp+14h]
  00541789  8b7c2410              mov       edi,[esp+10h]
  0054178d  8b6c2418              mov       ebp,[esp+18h]
  00541791  2bdf                  sub       ebx,edi
  00541793  8b7c2420              mov       edi,[esp+20h]
  00541797  2bef                  sub       ebp,edi
  00541799  2bcf                  sub       ecx,edi
  0054179b  896c241c              mov       [esp+1Ch],ebp
  0054179f  8b6c2410              mov       ebp,[esp+10h]
  005417a3  2bc5                  sub       eax,ebp
  005417a5  8b6c241c              mov       ebp,[esp+1Ch]
  005417a9  0fafc5                imul      eax,ebp
  005417ac  0fafcb                imul      ecx,ebx
  005417af  2bc1                  sub       eax,ecx
  005417b1  85c0                  test      eax,eax
  005417b3  7f2e                  jg        short 005417E3h
  005417b5  8b442410              mov       eax,[esp+10h]
  005417b9  8bca                  mov       ecx,edx
  005417bb  2bc8                  sub       ecx,eax
  005417bd  8b442430              mov       eax,[esp+30h]
  005417c1  0fafcd                imul      ecx,ebp
  005417c4  8be8                  mov       ebp,eax
  005417c6  2bef                  sub       ebp,edi
  005417c8  0fafeb                imul      ebp,ebx
  005417cb  2bcd                  sub       ecx,ebp
  005417cd  7818                  js        short 005417E7h
  005417cf  8b442428              mov       eax,[esp+28h]
  005417d3  8b542434              mov       edx,[esp+34h]
  005417d7  8b4804                mov       ecx,[eax+4]
  005417da  e9d7000000            jmp       005418B6h

LAB_005417df:
  005417df  8b7c2420              mov       edi,[esp+20h]

LAB_005417e3:
  005417e3  8b442430              mov       eax,[esp+30h]

LAB_005417e7:
  005417e7  8b4c2414              mov       ecx,[esp+14h]
  005417eb  8b5c2424              mov       ebx,[esp+24h]
  005417ef  2bca                  sub       ecx,edx
  005417f1  2bd8                  sub       ebx,eax
  005417f3  0fafcb                imul      ecx,ebx
  005417f6  8b5c2418              mov       ebx,[esp+18h]
  005417fa  8bee                  mov       ebp,esi
  005417fc  2bd8                  sub       ebx,eax
  005417fe  2bea                  sub       ebp,edx
  00541800  0fafdd                imul      ebx,ebp
  00541803  2bcb                  sub       ecx,ebx
  00541805  0f890a020000          jns       near 00541A15h
  0054180b  8b4c2414              mov       ecx,[esp+14h]
  0054180f  8b6c2410              mov       ebp,[esp+10h]
  00541813  8b5c2418              mov       ebx,[esp+18h]
  00541817  2bcd                  sub       ecx,ebp
  00541819  2bdf                  sub       ebx,edi
  0054181b  2bd5                  sub       edx,ebp
  0054181d  2bc7                  sub       eax,edi
  0054181f  0fafd3                imul      edx,ebx
  00541822  0fafc1                imul      eax,ecx
  00541825  2bd0                  sub       edx,eax
  00541827  85d2                  test      edx,edx
  00541829  0f8fe6010000          jg        near 00541A15h
  0054182f  8b442424              mov       eax,[esp+24h]
  00541833  2bf5                  sub       esi,ebp
  00541835  2bc7                  sub       eax,edi
  00541837  0faff3                imul      esi,ebx
  0054183a  0fafc1                imul      eax,ecx
  0054183d  2bf0                  sub       esi,eax
  0054183f  0f88d0010000          js        near 00541A15h
  00541845  8b4c2434              mov       ecx,[esp+34h]
  00541849  8b442428              mov       eax,[esp+28h]
  0054184d  eb65                  jmp       short 005418B4h

LAB_0054184f:
  0054184f  8b542414              mov       edx,[esp+14h]
  00541853  8b5c2424              mov       ebx,[esp+24h]
  00541857  2bd0                  sub       edx,eax
  00541859  2bd9                  sub       ebx,ecx
  0054185b  0fafd3                imul      edx,ebx
  0054185e  8b5c2418              mov       ebx,[esp+18h]
  00541862  8bee                  mov       ebp,esi
  00541864  2bd9                  sub       ebx,ecx
  00541866  2be8                  sub       ebp,eax
  00541868  0fafdd                imul      ebx,ebp
  0054186b  2bd3                  sub       edx,ebx
  0054186d  0f89a2010000          jns       near 00541A15h
  00541873  8b542414              mov       edx,[esp+14h]
  00541877  8b6c2410              mov       ebp,[esp+10h]
  0054187b  8b5c2418              mov       ebx,[esp+18h]
  0054187f  2bd5                  sub       edx,ebp
  00541881  2bdf                  sub       ebx,edi
  00541883  2bc5                  sub       eax,ebp
  00541885  2bcf                  sub       ecx,edi
  00541887  0fafc3                imul      eax,ebx
  0054188a  0fafca                imul      ecx,edx
  0054188d  2bc1                  sub       eax,ecx
  0054188f  85c0                  test      eax,eax
  00541891  0f8f7e010000          jg        near 00541A15h
  00541897  8b442424              mov       eax,[esp+24h]
  0054189b  2bf5                  sub       esi,ebp
  0054189d  2bc7                  sub       eax,edi
  0054189f  0faff3                imul      esi,ebx
  005418a2  0fafc2                imul      eax,edx
  005418a5  2bf0                  sub       esi,eax
  005418a7  0f8868010000          js        near 00541A15h
  005418ad  8b442428              mov       eax,[esp+28h]
  005418b1  8b4804                mov       ecx,[eax+4]

LAB_005418b4:
  005418b4  8b10                  mov       edx,[eax]

LAB_005418b6:
  005418b6  8b35c07b5f00          mov       esi,[5F7BC0h]
  005418bc  81e1ffff0000          and       ecx,0FFFFh
  005418c2  8b6c245c              mov       ebp,[esp+5Ch]
  005418c6  81e2ffff0000          and       edx,0FFFFh
  005418cc  8b7630                mov       esi,[esi+30h]
  005418cf  8b348e                mov       esi,[esi+ecx*4]
  005418d2  c1e110                shl       ecx,10h
  005418d5  0bca                  or        ecx,edx
  005418d7  89742440              mov       [esp+40h],esi
  005418db  894c2428              mov       [esp+28h],ecx
  005418df  668b0e                mov       cx,[esi]
  005418e2  6685c9                test      cx,cx
  005418e5  0f8c00010000          jl        near 005419EBh

LAB_005418eb:
  005418eb  8b54242c              mov       edx,[esp+2Ch]
  005418ef  0fbff1                movsx     esi,cx
  005418f2  89742448              mov       [esp+48h],esi
  005418f6  c1e604                shl       esi,4
  005418f9  03f2                  add       esi,edx
  005418fb  3bf0                  cmp       esi,eax
  005418fd  0f84cb000000          je        near 005419CEh
  00541903  8b16                  mov       edx,[esi]
  00541905  8b5c2428              mov       ebx,[esp+28h]
  00541909  8b4e0c                mov       ecx,[esi+0Ch]
  0054190c  8b7e04                mov       edi,[esi+4]
  0054190f  3bda                  cmp       ebx,edx
  00541911  7456                  je        short 00541969h
  00541913  8bda                  mov       ebx,edx
  00541915  8bef                  mov       ebp,edi
  00541917  c1eb10                shr       ebx,10h
  0054191a  c1e510                shl       ebp,10h
  0054191d  0bdd                  or        ebx,ebp
  0054191f  8b6c2428              mov       ebp,[esp+28h]
  00541923  3beb                  cmp       ebp,ebx
  00541925  743e                  je        short 00541965h
  00541927  8bd9                  mov       ebx,ecx
  00541929  83e301                and       ebx,1
  0054192c  6685db                test      bx,bx
  0054192f  895c244c              mov       [esp+4Ch],ebx
  00541933  7520                  jne       short 00541955h
  00541935  8bda                  mov       ebx,edx
  00541937  8bef                  mov       ebp,edi
  00541939  c1e310                shl       ebx,10h
  0054193c  81e5ffff0000          and       ebp,0FFFFh
  00541942  0bdd                  or        ebx,ebp
  00541944  8b6c2428              mov       ebp,[esp+28h]
  00541948  3beb                  cmp       ebp,ebx
  0054194a  7419                  je        short 00541965h
  0054194c  8b5c244c              mov       ebx,[esp+4Ch]
  00541950  6685db                test      bx,bx
  00541953  7475                  je        short 005419CAh

LAB_00541955:
  00541955  3bef                  cmp       ebp,edi
  00541957  740c                  je        short 00541965h
  00541959  c1e210                shl       edx,10h
  0054195c  c1ef10                shr       edi,10h
  0054195f  0bd7                  or        edx,edi
  00541961  3bea                  cmp       ebp,edx
  00541963  7565                  jne       short 005419CAh

LAB_00541965:
  00541965  8b6c245c              mov       ebp,[esp+5Ch]

LAB_00541969:
  00541969  8b542460              mov       edx,[esp+60h]
  0054196d  85d2                  test      edx,edx
  0054196f  740c                  je        short 0054197Dh
  00541971  8bf9                  mov       edi,ecx
  00541973  81e7ffff0000          and       edi,0FFFFh
  00541979  85fa                  test      edx,edi
  0054197b  746a                  je        short 005419E7h

LAB_0054197d:
  0054197d  8b7c2458              mov       edi,[esp+58h]
  00541981  8bd1                  mov       edx,ecx
  00541983  c1ea10                shr       edx,10h
  00541986  81e2ff000000          and       edx,0FFh
  0054198c  81c780000000          add       edi,80h
  00541992  8d3c97                lea       edi,[edi+edx*4]
  00541995  3bef                  cmp       ebp,edi
  00541997  750c                  jne       short 005419A5h
  00541999  668b5702              mov       dx,[edi+2]
  0054199d  81e200280000          and       edx,2800h
  005419a3  0bca                  or        ecx,edx

LAB_005419a5:
  005419a5  8a5502                mov       dl,[ebp+2]
  005419a8  83e202                and       edx,2
  005419ab  0bca                  or        ecx,edx
  005419ad  8a560f                mov       dl,[esi+0Fh]
  005419b0  84d2                  test      dl,dl
  005419b2  7403                  je        short 005419B7h
  005419b4  83c902                or        ecx,2

LAB_005419b7:
  005419b7  8b542448              mov       edx,[esp+48h]
  005419bb  81e1ffff0000          and       ecx,0FFFFh
  005419c1  c1e110                shl       ecx,10h
  005419c4  0bca                  or        ecx,edx
  005419c6  890f                  mov       [edi],ecx
  005419c8  eb04                  jmp       short 005419CEh

LAB_005419ca:
  005419ca  8b6c245c              mov       ebp,[esp+5Ch]

LAB_005419ce:
  005419ce  8b4c2440              mov       ecx,[esp+40h]
  005419d2  83c102                add       ecx,2
  005419d5  894c2440              mov       [esp+40h],ecx
  005419d9  668b09                mov       cx,[ecx]
  005419dc  6685c9                test      cx,cx
  005419df  0f8d06ffffff          jge       near 005418EBh
  005419e5  eb04                  jmp       short 005419EBh

LAB_005419e7:
  005419e7  804d0308              or        byte [ebp+3],8

LAB_005419eb:
  005419eb  8b442450              mov       eax,[esp+50h]
  005419ef  8b4d00                mov       ecx,[ebp]
  005419f2  3bc1                  cmp       eax,ecx
  005419f4  7417                  je        short 00541A0Dh
  005419f6  f6450308              test      byte [ebp+3],8
  005419fa  751d                  jne       short 00541A19h
  005419fc  8b442438              mov       eax,[esp+38h]
  00541a00  48                    dec       eax
  00541a01  89442438              mov       [esp+38h],eax
  00541a05  0f85a1fbffff          jne       near 005415ACh
  00541a0b  eb0c                  jmp       short 00541A19h

LAB_00541a0d:
  00541a0d  66c745020080          mov       word [ebp+2],8000h
  00541a13  eb04                  jmp       short 00541A19h

LAB_00541a15:
  00541a15  8b6c245c              mov       ebp,[esp+5Ch]

LAB_00541a19:
  00541a19  8b442438              mov       eax,[esp+38h]
  00541a1d  85c0                  test      eax,eax
  00541a1f  7504                  jne       short 00541A25h
  00541a21  804d0208              or        byte [ebp+2],8

LAB_00541a25:
  00541a25  8b442444              mov       eax,[esp+44h]
  00541a29  83c504                add       ebp,4
  00541a2c  48                    dec       eax
  00541a2d  896c245c              mov       [esp+5Ch],ebp
  00541a31  89442444              mov       [esp+44h],eax
  00541a35  0f8569fbffff          jne       near 005415A4h
  00541a3b  8b5c2458              mov       ebx,[esp+58h]
  00541a3f  bf04000000            mov       edi,4
  00541a44  33c9                  xor       ecx,ecx

LAB_00541a46:
  00541a46  8b45fc                mov       eax,[ebp-4]
  00541a49  83ed04                sub       ebp,4
  00541a4c  8bf0                  mov       esi,eax
  00541a4e  c1ee10                shr       esi,10h
  00541a51  f7c600800000          test      esi,8000h
  00541a57  7408                  je        short 00541A61h
  00541a59  66094d02              or        [ebp+2],cx
  00541a5d  0bf1                  or        esi,ecx
  00541a5f  eb44                  jmp       short 00541AA5h

LAB_00541a61:
  00541a61  8b4c242c              mov       ecx,[esp+2Ch]
  00541a65  25ffff0000            and       eax,0FFFFh
  00541a6a  c1e004                shl       eax,4
  00541a6d  03c1                  add       eax,ecx
  00541a6f  8d4b1c                lea       ecx,[ebx+1Ch]
  00541a72  51                    push      ecx
  00541a73  50                    push      eax
  00541a74  89437c                mov       [ebx+7Ch],eax
  00541a77  e8d419ffff            call      00533450h
  00541a7c  8b4b20                mov       ecx,[ebx+20h]
  00541a7f  894360                mov       [ebx+60h],eax
  00541a82  0500e0fcff            add       eax,0FFFCE000h
  00541a87  83c408                add       esp,8
  00541a8a  3bc1                  cmp       eax,ecx
  00541a8c  7e2a                  jle       short 00541AB8h
  00541a8e  f7c600200000          test      esi,2000h
  00541a94  7414                  je        short 00541AAAh
  00541a96  804d0380              or        byte [ebp+3],80h
  00541a9a  81ce00800000          or        esi,8000h
  00541aa0  b900200000            mov       ecx,2000h

LAB_00541aa5:
  00541aa5  4f                    dec       edi
  00541aa6  759e                  jne       short 00541A46h
  00541aa8  eb1d                  jmp       short 00541AC7h

LAB_00541aaa:
  00541aaa  8b5320                mov       edx,[ebx+20h]
  00541aad  81ce00080000          or        esi,800h
  00541ab3  895360                mov       [ebx+60h],edx
  00541ab6  eb0f                  jmp       short 00541AC7h

LAB_00541ab8:
  00541ab8  8b437c                mov       eax,[ebx+7Ch]
  00541abb  f6400d20              test      byte [eax+0Dh],20h
  00541abf  7506                  jne       short 00541AC7h
  00541ac1  81e6ffdf0000          and       esi,0DFFFh

LAB_00541ac7:
  00541ac7  f7c600800000          test      esi,8000h
  00541acd  7418                  je        short 00541AE7h
  00541acf  f7c600200000          test      esi,2000h
  00541ad5  7510                  jne       short 00541AE7h
  00541ad7  8b0d80ca5e00          mov       ecx,[5ECA80h]
  00541add  f60120                test      byte [ecx],20h
  00541ae0  7405                  je        short 00541AE7h
  00541ae2  be00080000            mov       esi,800h

LAB_00541ae7:
  00541ae7  81e6ffff0000          and       esi,0FFFFh
  00541aed  ba01000000            mov       edx,1
  00541af2  85ff                  test      edi,edi
  00541af4  89b390000000          mov       [ebx+90h],esi
  00541afa  7447                  je        short 00541B43h
  00541afc  8bf7                  mov       esi,edi

LAB_00541afe:
  00541afe  8b4500                mov       eax,[ebp]
  00541b01  8bc8                  mov       ecx,eax
  00541b03  c1e910                shr       ecx,10h
  00541b06  f6c580                test      ch,80h
  00541b09  7532                  jne       short 00541B3Dh
  00541b0b  8b4c242c              mov       ecx,[esp+2Ch]
  00541b0f  25ffff0000            and       eax,0FFFFh
  00541b14  c1e004                shl       eax,4
  00541b17  03c1                  add       eax,ecx
  00541b19  6685d2                test      dx,dx
  00541b1c  668b480c              mov       cx,[eax+0Ch]
  00541b20  66894d02              mov       [ebp+2],cx
  00541b24  7417                  je        short 00541B3Dh
  00541b26  f6c510                test      ch,10h
  00541b29  7512                  jne       short 00541B3Dh
  00541b2b  8d4b1c                lea       ecx,[ebx+1Ch]
  00541b2e  51                    push      ecx
  00541b2f  50                    push      eax
  00541b30  e81b19ffff            call      00533450h
  00541b35  83c408                add       esp,8
  00541b38  894364                mov       [ebx+64h],eax
  00541b3b  33d2                  xor       edx,edx

LAB_00541b3d:
  00541b3d  83ed04                sub       ebp,4
  00541b40  4e                    dec       esi
  00541b41  75bb                  jne       short 00541AFEh

LAB_00541b43:
  00541b43  8b8390000000          mov       eax,[ebx+90h]
  00541b49  6685d2                test      dx,dx
  00541b4c  741a                  je        short 00541B68h
  00541b4e  0d00100020            or        eax,20001000h
  00541b53  c7436400000080        mov       dword [ebx+64h],80000000h
  00541b5a  898390000000          mov       [ebx+90h],eax
  00541b60  5f                    pop       edi
  00541b61  5e                    pop       esi
  00541b62  5d                    pop       ebp
  00541b63  5b                    pop       ebx
  00541b64  83c444                add       esp,44h
  00541b67  c3                    ret

LAB_00541b68:
  00541b68  80e4ef                and       ah,0EFh
  00541b6b  5f                    pop       edi
  00541b6c  5e                    pop       esi
  00541b6d  898390000000          mov       [ebx+90h],eax
  00541b73  5d                    pop       ebp
  00541b74  5b                    pop       ebx
  00541b75  83c444                add       esp,44h
  00541b78  c3                    ret
  00541b79  90                    nop
  00541b7a  90                    nop
  00541b7b  90                    nop
  00541b7c  90                    nop
  00541b7d  90                    nop
  00541b7e  90                    nop
  00541b7f  90                    nop

37 cible(s) de saut interne : 0x005415a4, 0x005415ac, 0x005416bb, 0x0054173c, 0x00541740, 0x005417df, 0x005417e3, 0x005417e7, 0x0054184f, 0x005418b4, 0x005418b6, 0x005418eb, 0x00541955, 0x00541965, 0x00541969, 0x0054197d, 0x005419a5, 0x005419b7, 0x005419ca, 0x005419ce, 0x005419e7, 0x005419eb, 0x00541a0d, 0x00541a15, 0x00541a19, 0x00541a25, 0x00541a46, 0x00541a61, 0x00541aa5, 0x00541aaa, 0x00541ab8, 0x00541ac7, 0x00541ae7, 0x00541afe, 0x00541b3d, 0x00541b43, 0x00541b68
