C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0051e700  section .text  offset fichier 0x11db00  3400 octets

  0051e700  83ec60                sub       esp,60h
  0051e703  53                    push      ebx
  0051e704  55                    push      ebp
  0051e705  56                    push      esi
  0051e706  57                    push      edi
  0051e707  68e0086000            push      6008E0h
  0051e70c  e8b9730200            call      00545ACAh
  0051e711  68e0086000            push      6008E0h
  0051e716  e875740200            call      00545B90h
  0051e71b  8b9c2480000000        mov       ebx,[esp+80h]
  0051e722  8b0dd0086000          mov       ecx,[6008D0h]
  0051e728  8b03                  mov       eax,[ebx]
  0051e72a  03c1                  add       eax,ecx
  0051e72c  8b0dd0866000          mov       ecx,[6086D0h]
  0051e732  c1f80c                sar       eax,0Ch
  0051e735  668901                mov       [ecx],ax
  0051e738  8b15d4086000          mov       edx,[6008D4h]
  0051e73e  8b7b04                mov       edi,[ebx+4]
  0051e741  a1d0866000            mov       eax,[6086D0h]
  0051e746  2bd7                  sub       edx,edi
  0051e748  c1fa0c                sar       edx,0Ch
  0051e74b  66895002              mov       [eax+2],dx
  0051e74f  8b4b08                mov       ecx,[ebx+8]
  0051e752  8b15d8086000          mov       edx,[6008D8h]
  0051e758  03ca                  add       ecx,edx
  0051e75a  8b15d0866000          mov       edx,[6086D0h]
  0051e760  c1f90c                sar       ecx,0Ch
  0051e763  66894a04              mov       [edx+4],cx
  0051e767  8b0dd0866000          mov       ecx,[6086D0h]
  0051e76d  a110886000            mov       eax,[608810h]
  0051e772  0fbf5102              movsx     edx,word [ecx+2]
  0051e776  0fbf7002              movsx     esi,word [eax+2]
  0051e77a  0fbf7804              movsx     edi,word [eax+4]
  0051e77e  0fafd6                imul      edx,esi
  0051e781  0fbf7104              movsx     esi,word [ecx+4]
  0051e785  0faff7                imul      esi,edi
  0051e788  0fbf09                movsx     ecx,word [ecx]
  0051e78b  03d6                  add       edx,esi
  0051e78d  0fbf30                movsx     esi,word [eax]
  0051e790  0faff1                imul      esi,ecx
  0051e793  8b4814                mov       ecx,[eax+14h]
  0051e796  a1d4866000            mov       eax,[6086D4h]
  0051e79b  03d6                  add       edx,esi
  0051e79d  c1fa0c                sar       edx,0Ch
  0051e7a0  03d1                  add       edx,ecx
  0051e7a2  8910                  mov       [eax],edx
  0051e7a4  8b0dd0866000          mov       ecx,[6086D0h]
  0051e7aa  a110886000            mov       eax,[608810h]
  0051e7af  0fbf5102              movsx     edx,word [ecx+2]
  0051e7b3  0fbf7008              movsx     esi,word [eax+8]
  0051e7b7  0fbf780a              movsx     edi,word [eax+0Ah]
  0051e7bb  0fafd6                imul      edx,esi
  0051e7be  0fbf7104              movsx     esi,word [ecx+4]
  0051e7c2  0faff7                imul      esi,edi
  0051e7c5  0fbf09                movsx     ecx,word [ecx]
  0051e7c8  03d6                  add       edx,esi
  0051e7ca  8b6818                mov       ebp,[eax+18h]
  0051e7cd  0fbf7006              movsx     esi,word [eax+6]
  0051e7d1  0faff1                imul      esi,ecx
  0051e7d4  a1d4866000            mov       eax,[6086D4h]
  0051e7d9  03d6                  add       edx,esi
  0051e7db  c1fa0c                sar       edx,0Ch
  0051e7de  03d5                  add       edx,ebp
  0051e7e0  895004                mov       [eax+4],edx
  0051e7e3  8b0dd0866000          mov       ecx,[6086D0h]
  0051e7e9  a110886000            mov       eax,[608810h]
  0051e7ee  0fbf5102              movsx     edx,word [ecx+2]
  0051e7f2  0fbf700e              movsx     esi,word [eax+0Eh]
  0051e7f6  0fbf7810              movsx     edi,word [eax+10h]
  0051e7fa  0fafd6                imul      edx,esi
  0051e7fd  0fbf7104              movsx     esi,word [ecx+4]
  0051e801  0faff7                imul      esi,edi
  0051e804  0fbf09                movsx     ecx,word [ecx]
  0051e807  03d6                  add       edx,esi
  0051e809  0fbf700c              movsx     esi,word [eax+0Ch]
  0051e80d  0faff1                imul      esi,ecx
  0051e810  03d6                  add       edx,esi
  0051e812  8b701c                mov       esi,[eax+1Ch]
  0051e815  a1d4866000            mov       eax,[6086D4h]
  0051e81a  c1fa0c                sar       edx,0Ch
  0051e81d  03d6                  add       edx,esi
  0051e81f  895008                mov       [eax+8],edx
  0051e822  8b0dd4866000          mov       ecx,[6086D4h]
  0051e828  a1d8866000            mov       eax,[6086D8h]
  0051e82d  668b11                mov       dx,[ecx]
  0051e830  668910                mov       [eax],dx
  0051e833  8b0dd4866000          mov       ecx,[6086D4h]
  0051e839  a1d8866000            mov       eax,[6086D8h]
  0051e83e  668b5104              mov       dx,[ecx+4]
  0051e842  66895002              mov       [eax+2],dx
  0051e846  8b0dd4866000          mov       ecx,[6086D4h]
  0051e84c  a1d8866000            mov       eax,[6086D8h]
  0051e851  668b5108              mov       dx,[ecx+8]
  0051e855  33c9                  xor       ecx,ecx
  0051e857  66895004              mov       [eax+4],dx
  0051e85b  8b15dc866000          mov       edx,[6086DCh]
  0051e861  0fbf431a              movsx     eax,word [ebx+1Ah]
  0051e865  66890a                mov       [edx],cx
  0051e868  8b15dc866000          mov       edx,[6086DCh]
  0051e86e  a801                  test      al,1
  0051e870  66894a02              mov       [edx+2],cx
  0051e874  8b15dc866000          mov       edx,[6086DCh]
  0051e87a  89442434              mov       [esp+34h],eax
  0051e87e  66894a04              mov       [edx+4],cx
  0051e882  8b0dd4866000          mov       ecx,[6086D4h]
  0051e888  8b11                  mov       edx,[ecx]
  0051e88a  8b0d10886000          mov       ecx,[608810h]
  0051e890  895114                mov       [ecx+14h],edx
  0051e893  8b15d4866000          mov       edx,[6086D4h]
  0051e899  8b4a04                mov       ecx,[edx+4]
  0051e89c  8b1510886000          mov       edx,[608810h]
  0051e8a2  894a18                mov       [edx+18h],ecx
  0051e8a5  8b0dd4866000          mov       ecx,[6086D4h]
  0051e8ab  8b5108                mov       edx,[ecx+8]
  0051e8ae  8b0d10886000          mov       ecx,[608810h]
  0051e8b4  89511c                mov       [ecx+1Ch],edx
  0051e8b7  0f844f010000          je        near 0051EA0Ch
  0051e8bd  a1dc866000            mov       eax,[6086DCh]
  0051e8c2  0fbf7802              movsx     edi,word [eax+2]
  0051e8c6  0fbf6804              movsx     ebp,word [eax+4]
  0051e8ca  0fbf30                movsx     esi,word [eax]
  0051e8cd  a110886000            mov       eax,[608810h]
  0051e8d2  8bce                  mov       ecx,esi
  0051e8d4  89ac2480000000        mov       [esp+80h],ebp
  0051e8db  89742440              mov       [esp+40h],esi
  0051e8df  0fbf10                movsx     edx,word [eax]
  0051e8e2  0fafca                imul      ecx,edx
  0051e8e5  0fbf5002              movsx     edx,word [eax+2]
  0051e8e9  0fafd7                imul      edx,edi
  0051e8ec  03ca                  add       ecx,edx
  0051e8ee  0fbf5004              movsx     edx,word [eax+4]
  0051e8f2  0fafd5                imul      edx,ebp
  0051e8f5  03ca                  add       ecx,edx
  0051e8f7  0fbf500a              movsx     edx,word [eax+0Ah]
  0051e8fb  0fafd5                imul      edx,ebp
  0051e8fe  0fbf6808              movsx     ebp,word [eax+8]
  0051e902  0fafef                imul      ebp,edi
  0051e905  03d5                  add       edx,ebp
  0051e907  0fbf6806              movsx     ebp,word [eax+6]
  0051e90b  0fafee                imul      ebp,esi
  0051e90e  0fbf7010              movsx     esi,word [eax+10h]
  0051e912  03d5                  add       edx,ebp
  0051e914  0fafb42480000000      imul      esi,[esp+80h]
  0051e91c  0fbf680e              movsx     ebp,word [eax+0Eh]
  0051e920  0fafef                imul      ebp,edi
  0051e923  0fbf780c              movsx     edi,word [eax+0Ch]
  0051e927  0faf7c2440            imul      edi,[esp+40h]
  0051e92c  03f5                  add       esi,ebp
  0051e92e  03f7                  add       esi,edi
  0051e930  c1fe0c                sar       esi,0Ch
  0051e933  6603701c              add       si,[eax+1Ch]
  0051e937  c1f90c                sar       ecx,0Ch
  0051e93a  66034814              add       cx,[eax+14h]
  0051e93e  6689742444            mov       [esp+44h],si
  0051e943  c1fa0c                sar       edx,0Ch
  0051e946  66035018              add       dx,[eax+18h]
  0051e94a  0fbfc6                movsx     eax,si
  0051e94d  8b359c876000          mov       esi,[60879Ch]
  0051e953  0fbfd2                movsx     edx,dx
  0051e956  8906                  mov       [esi],eax
  0051e958  8b442444              mov       eax,[esp+44h]
  0051e95c  8b35a4876000          mov       esi,[6087A4h]
  0051e962  25ffff0000            and       eax,0FFFFh
  0051e967  0fbfc9                movsx     ecx,cx
  0051e96a  0fbf0446              movsx     eax,word [esi+eax*2]
  0051e96e  8b358c876000          mov       esi,[60878Ch]
  0051e974  0fafd0                imul      edx,eax
  0051e977  8b36                  mov       esi,[esi]
  0051e979  0fafc8                imul      ecx,eax
  0051e97c  a1ec866000            mov       eax,[6086ECh]
  0051e981  81e200f0ffff          and       edx,0FFFFF000h
  0051e987  c1e60c                shl       esi,0Ch
  0051e98a  03d6                  add       edx,esi
  0051e98c  8b30                  mov       esi,[eax]
  0051e98e  c1e204                shl       edx,4
  0051e991  c1f90c                sar       ecx,0Ch
  0051e994  03d1                  add       edx,ecx
  0051e996  8b0df0866000          mov       ecx,[6086F0h]
  0051e99c  03d6                  add       edx,esi
  0051e99e  8911                  mov       [ecx],edx
  0051e9a0  8b159c876000          mov       edx,[60879Ch]
  0051e9a6  8b0df4866000          mov       ecx,[6086F4h]
  0051e9ac  8b02                  mov       eax,[edx]
  0051e9ae  c1f802                sar       eax,2
  0051e9b1  8901                  mov       [ecx],eax
  0051e9b3  8b15f0866000          mov       edx,[6086F0h]
  0051e9b9  8b0d9c876000          mov       ecx,[60879Ch]
  0051e9bf  8b02                  mov       eax,[edx]
  0051e9c1  89442448              mov       [esp+48h],eax
  0051e9c5  8b11                  mov       edx,[ecx]
  0051e9c7  83c018                add       eax,18h
  0051e9ca  8954244c              mov       [esp+4Ch],edx
  0051e9ce  663d7001              cmp       ax,170h
  0051e9d2  0f83f1000000          jae       near 0051EAC9h
  0051e9d8  8b44244a              mov       eax,[esp+4Ah]
  0051e9dc  83c018                add       eax,18h
  0051e9df  663d2001              cmp       ax,120h
  0051e9e3  0f83e0000000          jae       near 0051EAC9h
  0051e9e9  668b4c244c            mov       cx,[esp+4Ch]
  0051e9ee  66c1f902              sar       cx,2
  0051e9f2  0fbfd1                movsx     edx,cx
  0051e9f5  83ea58                sub       edx,58h
  0051e9f8  81faa0030000          cmp       edx,3A0h
  0051e9fe  720c                  jb        short 0051EA0Ch
  0051ea00  8b442478              mov       eax,[esp+78h]
  0051ea04  5f                    pop       edi
  0051ea05  5e                    pop       esi
  0051ea06  5d                    pop       ebp
  0051ea07  5b                    pop       ebx
  0051ea08  83c460                add       esp,60h
  0051ea0b  c3                    ret

LAB_0051ea0c:
  0051ea0c  668b4314              mov       ax,[ebx+14h]
  0051ea10  668b4b16              mov       cx,[ebx+16h]
  0051ea14  66030528496000        add       ax,[604928h]
  0051ea1b  66030d2a496000        add       cx,[60492Ah]
  0051ea22  668b5318              mov       dx,[ebx+18h]
  0051ea26  6603152c496000        add       dx,[60492Ch]
  0051ea2d  6689442448            mov       [esp+48h],ax
  0051ea32  66894c244a            mov       [esp+4Ah],cx
  0051ea37  8d442450              lea       eax,[esp+50h]
  0051ea3b  8d4c2448              lea       ecx,[esp+48h]
  0051ea3f  50                    push      eax
  0051ea40  51                    push      ecx
  0051ea41  6689542454            mov       [esp+54h],dx
  0051ea46  e8b5d8ffff            call      0051C300h
  0051ea4b  83c408                add       esp,8
  0051ea4e  8d530c                lea       edx,[ebx+0Ch]
  0051ea51  8d442450              lea       eax,[esp+50h]
  0051ea55  52                    push      edx
  0051ea56  50                    push      eax
  0051ea57  e8543d0100            call      005327B0h
  0051ea5c  83c408                add       esp,8
  0051ea5f  8d4c2450              lea       ecx,[esp+50h]
  0051ea63  51                    push      ecx
  0051ea64  e861700200            call      00545ACAh
  0051ea69  668b1508935e00        mov       dx,[5E9308h]
  0051ea70  66a10a935e00          mov       ax,[5E930Ah]
  0051ea76  660144244a            add       [esp+4Ah],ax
  0051ea7b  668b0d0c935e00        mov       cx,[5E930Ch]
  0051ea82  66014c244c            add       [esp+4Ch],cx
  0051ea87  6689542448            mov       [esp+48h],dx
  0051ea8c  8d542450              lea       edx,[esp+50h]
  0051ea90  8d442448              lea       eax,[esp+48h]
  0051ea94  52                    push      edx
  0051ea95  50                    push      eax
  0051ea96  e865d8ffff            call      0051C300h
  0051ea9b  83c408                add       esp,8
  0051ea9e  8d4c2450              lea       ecx,[esp+50h]
  0051eaa2  51                    push      ecx
  0051eaa3  e800710200            call      00545BA8h
  0051eaa8  8b6c247c              mov       ebp,[esp+7Ch]
  0051eaac  a1484a5600            mov       eax,[564A48h]
  0051eab1  85c0                  test      eax,eax
  0051eab3  8b7d3c                mov       edi,[ebp+3Ch]
  0051eab6  7423                  je        short 0051EADBh
  0051eab8  f7c700020000          test      edi,200h
  0051eabe  741b                  je        short 0051EADBh
  0051eac0  a14c4a5600            mov       eax,[564A4Ch]
  0051eac5  85c0                  test      eax,eax
  0051eac7  750c                  jne       short 0051EAD5h

LAB_0051eac9:
  0051eac9  8b442478              mov       eax,[esp+78h]
  0051eacd  5f                    pop       edi
  0051eace  5e                    pop       esi
  0051eacf  5d                    pop       ebp
  0051ead0  5b                    pop       ebx
  0051ead1  83c460                add       esp,60h
  0051ead4  c3                    ret

LAB_0051ead5:
  0051ead5  81e7fffdffff          and       edi,0FFFFFDFFh

LAB_0051eadb:
  0051eadb  8bd7                  mov       edx,edi
  0051eadd  8bf7                  mov       esi,edi
  0051eadf  c1ea10                shr       edx,10h
  0051eae2  81e2ff000000          and       edx,0FFh
  0051eae8  81e6ff000000          and       esi,0FFh
  0051eaee  c1e605                shl       esi,5
  0051eaf1  8b0c95008f5e00        mov       ecx,[edx*4+5E8F00h]
  0051eaf8  8b942484000000        mov       edx,[esp+84h]
  0051eaff  668bc2                mov       ax,dx
  0051eb02  81e2ff000000          and       edx,0FFh
  0051eb08  66c1f802              sar       ax,2
  0051eb0c  25c0ff0000            and       eax,0FFC0h
  0051eb11  0bf1                  or        esi,ecx
  0051eb13  03c2                  add       eax,edx
  0051eb15  81e10000ffff          and       ecx,0FFFF0000h
  0051eb1b  c1e010                shl       eax,10h
  0051eb1e  03c1                  add       eax,ecx
  0051eb20  89442438              mov       [esp+38h],eax
  0051eb24  8b4508                mov       eax,[ebp+8]
  0051eb27  8bc8                  mov       ecx,eax
  0051eb29  25ffff0000            and       eax,0FFFFh
  0051eb2e  c1e910                shr       ecx,10h
  0051eb31  898c2480000000        mov       [esp+80h],ecx
  0051eb38  8b0db4876000          mov       ecx,[6087B4h]
  0051eb3e  50                    push      eax
  0051eb3f  8d4540                lea       eax,[ebp+40h]
  0051eb42  8b09                  mov       ecx,[ecx]
  0051eb44  51                    push      ecx
  0051eb45  8d9100401b00          lea       edx,[ecx+1B4000h]
  0051eb4b  81c100001b00          add       ecx,1B0000h
  0051eb51  52                    push      edx
  0051eb52  51                    push      ecx
  0051eb53  50                    push      eax
  0051eb54  c1e610                shl       esi,10h
  0051eb57  e83a700200            call      00545B96h
  0051eb5c  8b0db4876000          mov       ecx,[6087B4h]
  0051eb62  8b431c                mov       eax,[ebx+1Ch]
  0051eb65  81e700030000          and       edi,300h
  0051eb6b  8b11                  mov       edx,[ecx]
  0051eb6d  8bc8                  mov       ecx,eax
  0051eb6f  c1e909                shr       ecx,9
  0051eb72  81e100006000          and       ecx,600000h
  0051eb78  25ffffff03            and       eax,3FFFFFFh
  0051eb7d  33f1                  xor       esi,ecx
  0051eb7f  8a4c2434              mov       cl,[esp+34h]
  0051eb83  c1e710                shl       edi,10h
  0051eb86  81c200801b00          add       edx,1B8000h
  0051eb8c  33c7                  xor       eax,edi
  0051eb8e  f6c104                test      cl,4
  0051eb91  8974243c              mov       [esp+3Ch],esi
  0051eb95  7414                  je        short 0051EBABh
  0051eb97  8b8c2480000000        mov       ecx,[esp+80h]
  0051eb9e  8d0c49                lea       ecx,[ecx+ecx*2]
  0051eba1  85c9                  test      ecx,ecx
  0051eba3  7420                  je        short 0051EBC5h
  0051eba5  8bfa                  mov       edi,edx
  0051eba7  f3ab                  rep stosd
  0051eba9  eb1a                  jmp       short 0051EBC5h

LAB_0051ebab:
  0051ebab  8b0d00876000          mov       ecx,[608700h]
  0051ebb1  8901                  mov       [ecx],eax
  0051ebb3  8b842480000000        mov       eax,[esp+80h]
  0051ebba  8b4d24                mov       ecx,[ebp+24h]
  0051ebbd  50                    push      eax
  0051ebbe  52                    push      edx
  0051ebbf  51                    push      ecx
  0051ebc0  e8bf6f0200            call      00545B84h

LAB_0051ebc5:
  0051ebc5  8b15b4876000          mov       edx,[6087B4h]
  0051ebcb  33db                  xor       ebx,ebx
  0051ebcd  668b5d16              mov       bx,[ebp+16h]
  0051ebd1  33c0                  xor       eax,eax
  0051ebd3  668b4514              mov       ax,[ebp+14h]
  0051ebd7  8b3a                  mov       edi,[edx]
  0051ebd9  33c9                  xor       ecx,ecx
  0051ebdb  03d8                  add       ebx,eax
  0051ebdd  668b4d12              mov       cx,[ebp+12h]
  0051ebe1  33d2                  xor       edx,edx
  0051ebe3  668b5510              mov       dx,[ebp+10h]
  0051ebe7  03d9                  add       ebx,ecx
  0051ebe9  8b742478              mov       esi,[esp+78h]
  0051ebed  03da                  add       ebx,edx
  0051ebef  8b5528                mov       edx,[ebp+28h]
  0051ebf2  33c0                  xor       eax,eax
  0051ebf4  668b4518              mov       ax,[ebp+18h]
  0051ebf8  81c700401b00          add       edi,1B4000h
  0051ebfe  c1e304                shl       ebx,4
  0051ec01  03da                  add       ebx,edx
  0051ec03  8944241c              mov       [esp+1Ch],eax
  0051ec07  85c0                  test      eax,eax
  0051ec09  0f8427020000          je        near 0051EE36h

LAB_0051ec0f:
  0051ec0f  a118985e00            mov       eax,[5E9818h]
  0051ec14  85c0                  test      eax,eax
  0051ec16  0f841a020000          je        near 0051EE36h
  0051ec1c  8b03                  mov       eax,[ebx]
  0051ec1e  8b2d0c876000          mov       ebp,[60870Ch]
  0051ec24  8bc8                  mov       ecx,eax
  0051ec26  81e1ff000000          and       ecx,0FFh
  0051ec2c  894c242c              mov       [esp+2Ch],ecx
  0051ec30  8b948f00c0ffff        mov       edx,[edi+ecx*4-4000h]
  0051ec37  8bc8                  mov       ecx,eax
  0051ec39  c1e908                shr       ecx,8
  0051ec3c  81e1ff000000          and       ecx,0FFh
  0051ec42  89542418              mov       [esp+18h],edx
  0051ec46  894c2430              mov       [esp+30h],ecx
  0051ec4a  8b8c8f00c0ffff        mov       ecx,[edi+ecx*4-4000h]
  0051ec51  895500                mov       [ebp],edx
  0051ec54  8b2d08876000          mov       ebp,[608708h]
  0051ec5a  898c2480000000        mov       [esp+80h],ecx
  0051ec61  c1e810                shr       eax,10h
  0051ec64  25ff000000            and       eax,0FFh
  0051ec69  6681fa4001            cmp       dx,140h
  0051ec6e  89442434              mov       [esp+34h],eax
  0051ec72  8b848700c0ffff        mov       eax,[edi+eax*4-4000h]
  0051ec79  894d00                mov       [ebp],ecx
  0051ec7c  8b2d10876000          mov       ebp,[608710h]
  0051ec82  89442478              mov       [esp+78h],eax
  0051ec86  894500                mov       [ebp],eax
  0051ec89  7211                  jb        short 0051EC9Ch
  0051ec8b  6681f94001            cmp       cx,140h
  0051ec90  720a                  jb        short 0051EC9Ch
  0051ec92  663d4001              cmp       ax,140h
  0051ec96  0f8386010000          jae       near 0051EE22h

LAB_0051ec9c:
  0051ec9c  a108876000            mov       eax,[608708h]
  0051eca1  8b0d10876000          mov       ecx,[608710h]
  0051eca7  0fbf10                movsx     edx,word [eax]
  0051ecaa  0fbf4002              movsx     eax,word [eax+2]
  0051ecae  0fbf6902              movsx     ebp,word [ecx+2]
  0051ecb2  0fbf09                movsx     ecx,word [ecx]
  0051ecb5  89942484000000        mov       [esp+84h],edx
  0051ecbc  8b150c876000          mov       edx,[60870Ch]
  0051ecc2  89442414              mov       [esp+14h],eax
  0051ecc6  894c2410              mov       [esp+10h],ecx
  0051ecca  0fbf02                movsx     eax,word [edx]
  0051eccd  0fbf5202              movsx     edx,word [edx+2]
  0051ecd1  8b8c2484000000        mov       ecx,[esp+84h]
  0051ecd8  89542424              mov       [esp+24h],edx
  0051ecdc  8bd0                  mov       edx,eax
  0051ecde  0fafcd                imul      ecx,ebp
  0051ece1  0faf542414            imul      edx,[esp+14h]
  0051ece6  896c2428              mov       [esp+28h],ebp
  0051ecea  8b6c2410              mov       ebp,[esp+10h]
  0051ecee  0faf442428            imul      eax,[esp+28h]
  0051ecf3  03d1                  add       edx,ecx
  0051ecf5  8b4c2424              mov       ecx,[esp+24h]
  0051ecf9  0fafe9                imul      ebp,ecx
  0051ecfc  0faf8c2484000000      imul      ecx,[esp+84h]
  0051ed04  03d5                  add       edx,ebp
  0051ed06  2bd1                  sub       edx,ecx
  0051ed08  8b4c2418              mov       ecx,[esp+18h]
  0051ed0c  2bd0                  sub       edx,eax
  0051ed0e  8b442410              mov       eax,[esp+10h]
  0051ed12  0faf442414            imul      eax,[esp+14h]
  0051ed17  2bd0                  sub       edx,eax
  0051ed19  a114876000            mov       eax,[608714h]
  0051ed1e  81e10000ffff          and       ecx,0FFFF0000h
  0051ed24  81f90000f000          cmp       ecx,0F00000h
  0051ed2a  8910                  mov       [eax],edx
  0051ed2c  7229                  jb        short 0051ED57h
  0051ed2e  8b942480000000        mov       edx,[esp+80h]
  0051ed35  81e20000ffff          and       edx,0FFFF0000h
  0051ed3b  81fa0000f000          cmp       edx,0F00000h
  0051ed41  7214                  jb        short 0051ED57h
  0051ed43  8b442478              mov       eax,[esp+78h]
  0051ed47  250000ffff            and       eax,0FFFF0000h
  0051ed4c  3d0000f000            cmp       eax,0F00000h
  0051ed51  0f83cb000000          jae       near 0051EE22h

LAB_0051ed57:
  0051ed57  e894d5ffff            call      0051C2F0h
  0051ed5c  85c0                  test      eax,eax
  0051ed5e  0f8dbe000000          jge       near 0051EE22h
  0051ed64  8b4c242c              mov       ecx,[esp+2Ch]
  0051ed68  8b442430              mov       eax,[esp+30h]
  0051ed6c  8b148f                mov       edx,[edi+ecx*4]
  0051ed6f  8b0487                mov       eax,[edi+eax*4]
  0051ed72  8b4c2434              mov       ecx,[esp+34h]
  0051ed76  3bc2                  cmp       eax,edx
  0051ed78  8b0c8f                mov       ecx,[edi+ecx*4]
  0051ed7b  7e02                  jle       short 0051ED7Fh
  0051ed7d  8bd0                  mov       edx,eax

LAB_0051ed7f:
  0051ed7f  3bca                  cmp       ecx,edx
  0051ed81  7e02                  jle       short 0051ED85h
  0051ed83  8bd1                  mov       edx,ecx

LAB_0051ed85:
  0051ed85  8d82c0feffff          lea       eax,[edx-140h]
  0051ed8b  3dc00e0000            cmp       eax,0EC0h
  0051ed90  0f838c000000          jae       near 0051EE22h
  0051ed96  8b6b04                mov       ebp,[ebx+4]
  0051ed99  8b4308                mov       eax,[ebx+8]
  0051ed9c  81e5ff000000          and       ebp,0FFh
  0051eda2  8b4b0c                mov       ecx,[ebx+0Ch]
  0051eda5  81e2fcff0000          and       edx,0FFFCh
  0051edab  81e1ffff0000          and       ecx,0FFFFh
  0051edb1  8bacaf00400000        mov       ebp,[edi+ebp*4+4000h]
  0051edb8  89ac2484000000        mov       [esp+84h],ebp
  0051edbf  8b6c2474              mov       ebp,[esp+74h]
  0051edc3  03d5                  add       edx,ebp
  0051edc5  8b2a                  mov       ebp,[edx]
  0051edc7  8932                  mov       [edx],esi
  0051edc9  8b542418              mov       edx,[esp+18h]
  0051edcd  81cd00000007          or        ebp,7000000h
  0051edd3  895608                mov       [esi+8],edx
  0051edd6  8b542478              mov       edx,[esp+78h]
  0051edda  895610                mov       [esi+10h],edx
  0051eddd  8b942480000000        mov       edx,[esp+80h]
  0051ede4  895618                mov       [esi+18h],edx
  0051ede7  8b942484000000        mov       edx,[esp+84h]
  0051edee  81ca00000024          or        edx,24000000h
  0051edf4  892e                  mov       [esi],ebp
  0051edf6  8b6c2438              mov       ebp,[esp+38h]
  0051edfa  895604                mov       [esi+4],edx
  0051edfd  8bd0                  mov       edx,eax
  0051edff  83c620                add       esi,20h
  0051ee02  81e2ffff0000          and       edx,0FFFFh
  0051ee08  0bd5                  or        edx,ebp
  0051ee0a  8b6c243c              mov       ebp,[esp+3Ch]
  0051ee0e  0bcd                  or        ecx,ebp
  0051ee10  8956ec                mov       [esi-14h],edx
  0051ee13  c1e810                shr       eax,10h
  0051ee16  894ef4                mov       [esi-0Ch],ecx
  0051ee19  8946fc                mov       [esi-4],eax
  0051ee1c  ff0d18985e00          dec       dword [5E9818h]

LAB_0051ee22:
  0051ee22  8b44241c              mov       eax,[esp+1Ch]
  0051ee26  83c310                add       ebx,10h
  0051ee29  48                    dec       eax
  0051ee2a  85c0                  test      eax,eax
  0051ee2c  8944241c              mov       [esp+1Ch],eax
  0051ee30  0f85d9fdffff          jne       near 0051EC0Fh

LAB_0051ee36:
  0051ee36  8b4c247c              mov       ecx,[esp+7Ch]
  0051ee3a  33c0                  xor       eax,eax
  0051ee3c  668b411a              mov       ax,[ecx+1Ah]
  0051ee40  85c0                  test      eax,eax
  0051ee42  8944241c              mov       [esp+1Ch],eax
  0051ee46  0f848c020000          je        near 0051F0D8h

LAB_0051ee4c:
  0051ee4c  a118985e00            mov       eax,[5E9818h]
  0051ee51  85c0                  test      eax,eax
  0051ee53  0f847f020000          je        near 0051F0D8h
  0051ee59  8b03                  mov       eax,[ebx]
  0051ee5b  8b150c876000          mov       edx,[60870Ch]
  0051ee61  8bc8                  mov       ecx,eax
  0051ee63  81e1ff000000          and       ecx,0FFh
  0051ee69  894c242c              mov       [esp+2Ch],ecx
  0051ee6d  8bac8f00c0ffff        mov       ebp,[edi+ecx*4-4000h]
  0051ee74  8bc8                  mov       ecx,eax
  0051ee76  c1e908                shr       ecx,8
  0051ee79  81e1ff000000          and       ecx,0FFh
  0051ee7f  896c2418              mov       [esp+18h],ebp
  0051ee83  894c2430              mov       [esp+30h],ecx
  0051ee87  8b8c8f00c0ffff        mov       ecx,[edi+ecx*4-4000h]
  0051ee8e  892a                  mov       [edx],ebp
  0051ee90  8bd0                  mov       edx,eax
  0051ee92  898c2480000000        mov       [esp+80h],ecx
  0051ee99  c1ea10                shr       edx,10h
  0051ee9c  81e2ff000000          and       edx,0FFh
  0051eea2  89542434              mov       [esp+34h],edx
  0051eea6  8b949700c0ffff        mov       edx,[edi+edx*4-4000h]
  0051eead  89542478              mov       [esp+78h],edx
  0051eeb1  8b1508876000          mov       edx,[608708h]
  0051eeb7  c1e818                shr       eax,18h
  0051eeba  890a                  mov       [edx],ecx
  0051eebc  8b1510876000          mov       edx,[608710h]
  0051eec2  89442440              mov       [esp+40h],eax
  0051eec6  8b848700c0ffff        mov       eax,[edi+eax*4-4000h]
  0051eecd  89442420              mov       [esp+20h],eax
  0051eed1  8b442478              mov       eax,[esp+78h]
  0051eed5  6681fd4001            cmp       bp,140h
  0051eeda  8902                  mov       [edx],eax
  0051eedc  721a                  jb        short 0051EEF8h
  0051eede  6681f94001            cmp       cx,140h
  0051eee3  7213                  jb        short 0051EEF8h
  0051eee5  663d4001              cmp       ax,140h
  0051eee9  720d                  jb        short 0051EEF8h
  0051eeeb  66817c24204001        cmp       word [esp+20h],140h
  0051eef2  0f83cc010000          jae       near 0051F0C4h

LAB_0051eef8:
  0051eef8  a108876000            mov       eax,[608708h]
  0051eefd  8b0d10876000          mov       ecx,[608710h]
  0051ef03  0fbf10                movsx     edx,word [eax]
  0051ef06  0fbf4002              movsx     eax,word [eax+2]
  0051ef0a  0fbf6902              movsx     ebp,word [ecx+2]
  0051ef0e  0fbf09                movsx     ecx,word [ecx]
  0051ef11  89942484000000        mov       [esp+84h],edx
  0051ef18  8b150c876000          mov       edx,[60870Ch]
  0051ef1e  89442414              mov       [esp+14h],eax
  0051ef22  894c2410              mov       [esp+10h],ecx
  0051ef26  0fbf02                movsx     eax,word [edx]
  0051ef29  0fbf5202              movsx     edx,word [edx+2]
  0051ef2d  8b8c2484000000        mov       ecx,[esp+84h]
  0051ef34  89542424              mov       [esp+24h],edx
  0051ef38  8bd0                  mov       edx,eax
  0051ef3a  0fafcd                imul      ecx,ebp
  0051ef3d  0faf542414            imul      edx,[esp+14h]
  0051ef42  896c2428              mov       [esp+28h],ebp
  0051ef46  8b6c2410              mov       ebp,[esp+10h]
  0051ef4a  0faf442428            imul      eax,[esp+28h]
  0051ef4f  03d1                  add       edx,ecx
  0051ef51  8b4c2424              mov       ecx,[esp+24h]
  0051ef55  0fafe9                imul      ebp,ecx
  0051ef58  0faf8c2484000000      imul      ecx,[esp+84h]
  0051ef60  03d5                  add       edx,ebp
  0051ef62  2bd1                  sub       edx,ecx
  0051ef64  8b4c2418              mov       ecx,[esp+18h]
  0051ef68  2bd0                  sub       edx,eax
  0051ef6a  8b442410              mov       eax,[esp+10h]
  0051ef6e  0faf442414            imul      eax,[esp+14h]
  0051ef73  2bd0                  sub       edx,eax
  0051ef75  a114876000            mov       eax,[608714h]
  0051ef7a  81e10000ffff          and       ecx,0FFFF0000h
  0051ef80  81f90000f000          cmp       ecx,0F00000h
  0051ef86  8910                  mov       [eax],edx
  0051ef88  723b                  jb        short 0051EFC5h
  0051ef8a  8b942480000000        mov       edx,[esp+80h]
  0051ef91  81e20000ffff          and       edx,0FFFF0000h
  0051ef97  81fa0000f000          cmp       edx,0F00000h
  0051ef9d  7226                  jb        short 0051EFC5h
  0051ef9f  8b442478              mov       eax,[esp+78h]
  0051efa3  250000ffff            and       eax,0FFFF0000h
  0051efa8  3d0000f000            cmp       eax,0F00000h
  0051efad  7216                  jb        short 0051EFC5h
  0051efaf  8b4c2420              mov       ecx,[esp+20h]
  0051efb3  81e10000ffff          and       ecx,0FFFF0000h
  0051efb9  81f90000f000          cmp       ecx,0F00000h
  0051efbf  0f83ff000000          jae       near 0051F0C4h

LAB_0051efc5:
  0051efc5  e826d3ffff            call      0051C2F0h
  0051efca  85c0                  test      eax,eax
  0051efcc  7c19                  jl        short 0051EFE7h
  0051efce  8b1510876000          mov       edx,[608710h]
  0051efd4  8b442420              mov       eax,[esp+20h]
  0051efd8  8902                  mov       [edx],eax
  0051efda  e8a1d2ffff            call      0051C280h
  0051efdf  85c0                  test      eax,eax
  0051efe1  0f8ddd000000          jge       near 0051F0C4h

LAB_0051efe7:
  0051efe7  8b542430              mov       edx,[esp+30h]
  0051efeb  8b4c242c              mov       ecx,[esp+2Ch]
  0051efef  8b6c2440              mov       ebp,[esp+40h]
  0051eff3  8b0497                mov       eax,[edi+edx*4]
  0051eff6  8b0c8f                mov       ecx,[edi+ecx*4]
  0051eff9  8b542434              mov       edx,[esp+34h]
  0051effd  8b2caf                mov       ebp,[edi+ebp*4]
  0051f000  3bc1                  cmp       eax,ecx
  0051f002  8b1497                mov       edx,[edi+edx*4]
  0051f005  7e02                  jle       short 0051F009h
  0051f007  8bc8                  mov       ecx,eax

LAB_0051f009:
  0051f009  3bd1                  cmp       edx,ecx
  0051f00b  7e02                  jle       short 0051F00Fh
  0051f00d  8bca                  mov       ecx,edx

LAB_0051f00f:
  0051f00f  3be9                  cmp       ebp,ecx
  0051f011  7e02                  jle       short 0051F015h
  0051f013  8bcd                  mov       ecx,ebp

LAB_0051f015:
  0051f015  8d81c0feffff          lea       eax,[ecx-140h]
  0051f01b  3dc00e0000            cmp       eax,0EC0h
  0051f020  0f839e000000          jae       near 0051F0C4h
  0051f026  8b6b04                mov       ebp,[ebx+4]
  0051f029  8b4308                mov       eax,[ebx+8]
  0051f02c  81e5ff000000          and       ebp,0FFh
  0051f032  8b530c                mov       edx,[ebx+0Ch]
  0051f035  81e1fcff0000          and       ecx,0FFFCh
  0051f03b  8bacaf00400000        mov       ebp,[edi+ebp*4+4000h]
  0051f042  89ac2484000000        mov       [esp+84h],ebp
  0051f049  8b6c2474              mov       ebp,[esp+74h]
  0051f04d  03cd                  add       ecx,ebp
  0051f04f  8b29                  mov       ebp,[ecx]
  0051f051  8931                  mov       [ecx],esi
  0051f053  8b8c2480000000        mov       ecx,[esp+80h]
  0051f05a  81cd00000009          or        ebp,9000000h
  0051f060  894e08                mov       [esi+8],ecx
  0051f063  8b4c2418              mov       ecx,[esp+18h]
  0051f067  894e10                mov       [esi+10h],ecx
  0051f06a  8b4c2478              mov       ecx,[esp+78h]
  0051f06e  894e18                mov       [esi+18h],ecx
  0051f071  8b4c2420              mov       ecx,[esp+20h]
  0051f075  894e20                mov       [esi+20h],ecx
  0051f078  8b8c2484000000        mov       ecx,[esp+84h]
  0051f07f  81c90000002c          or        ecx,2C000000h
  0051f085  892e                  mov       [esi],ebp
  0051f087  8b6c2438              mov       ebp,[esp+38h]
  0051f08b  894e04                mov       [esi+4],ecx
  0051f08e  8bc8                  mov       ecx,eax
  0051f090  25ffff0000            and       eax,0FFFFh
  0051f095  c1e910                shr       ecx,10h
  0051f098  0bcd                  or        ecx,ebp
  0051f09a  8b6c243c              mov       ebp,[esp+3Ch]
  0051f09e  0bc5                  or        eax,ebp
  0051f0a0  894e0c                mov       [esi+0Ch],ecx
  0051f0a3  894614                mov       [esi+14h],eax
  0051f0a6  8bc2                  mov       eax,edx
  0051f0a8  25ffff0000            and       eax,0FFFFh
  0051f0ad  c1ea10                shr       edx,10h
  0051f0b0  89461c                mov       [esi+1Ch],eax
  0051f0b3  895624                mov       [esi+24h],edx
  0051f0b6  a118985e00            mov       eax,[5E9818h]
  0051f0bb  83c628                add       esi,28h
  0051f0be  48                    dec       eax
  0051f0bf  a318985e00            mov       [5E9818h],eax

LAB_0051f0c4:
  0051f0c4  8b44241c              mov       eax,[esp+1Ch]
  0051f0c8  83c310                add       ebx,10h
  0051f0cb  48                    dec       eax
  0051f0cc  85c0                  test      eax,eax
  0051f0ce  8944241c              mov       [esp+1Ch],eax
  0051f0d2  0f8574fdffff          jne       near 0051EE4Ch

LAB_0051f0d8:
  0051f0d8  8b4c247c              mov       ecx,[esp+7Ch]
  0051f0dc  33c0                  xor       eax,eax
  0051f0de  668b411c              mov       ax,[ecx+1Ch]
  0051f0e2  85c0                  test      eax,eax
  0051f0e4  8944241c              mov       [esp+1Ch],eax
  0051f0e8  0f8460020000          je        near 0051F34Eh

LAB_0051f0ee:
  0051f0ee  a118985e00            mov       eax,[5E9818h]
  0051f0f3  85c0                  test      eax,eax
  0051f0f5  0f8453020000          je        near 0051F34Eh
  0051f0fb  8b03                  mov       eax,[ebx]
  0051f0fd  8b2d0c876000          mov       ebp,[60870Ch]
  0051f103  8bc8                  mov       ecx,eax
  0051f105  81e1ff000000          and       ecx,0FFh
  0051f10b  894c242c              mov       [esp+2Ch],ecx
  0051f10f  8b948f00c0ffff        mov       edx,[edi+ecx*4-4000h]
  0051f116  8bc8                  mov       ecx,eax
  0051f118  c1e908                shr       ecx,8
  0051f11b  81e1ff000000          and       ecx,0FFh
  0051f121  89542418              mov       [esp+18h],edx
  0051f125  894c2430              mov       [esp+30h],ecx
  0051f129  8b8c8f00c0ffff        mov       ecx,[edi+ecx*4-4000h]
  0051f130  895500                mov       [ebp],edx
  0051f133  8b2d08876000          mov       ebp,[608708h]
  0051f139  898c2480000000        mov       [esp+80h],ecx
  0051f140  c1e810                shr       eax,10h
  0051f143  25ff000000            and       eax,0FFh
  0051f148  6681fa4001            cmp       dx,140h
  0051f14d  89442434              mov       [esp+34h],eax
  0051f151  8b848700c0ffff        mov       eax,[edi+eax*4-4000h]
  0051f158  894d00                mov       [ebp],ecx
  0051f15b  8b2d10876000          mov       ebp,[608710h]
  0051f161  89442478              mov       [esp+78h],eax
  0051f165  894500                mov       [ebp],eax
  0051f168  7211                  jb        short 0051F17Bh
  0051f16a  6681f94001            cmp       cx,140h
  0051f16f  720a                  jb        short 0051F17Bh
  0051f171  663d4001              cmp       ax,140h
  0051f175  0f83bf010000          jae       near 0051F33Ah

LAB_0051f17b:
  0051f17b  a108876000            mov       eax,[608708h]
  0051f180  8b0d10876000          mov       ecx,[608710h]
  0051f186  0fbf10                movsx     edx,word [eax]
  0051f189  0fbf4002              movsx     eax,word [eax+2]
  0051f18d  0fbf6902              movsx     ebp,word [ecx+2]
  0051f191  0fbf09                movsx     ecx,word [ecx]
  0051f194  89942484000000        mov       [esp+84h],edx
  0051f19b  8b150c876000          mov       edx,[60870Ch]
  0051f1a1  89442414              mov       [esp+14h],eax
  0051f1a5  894c2410              mov       [esp+10h],ecx
  0051f1a9  0fbf02                movsx     eax,word [edx]
  0051f1ac  0fbf5202              movsx     edx,word [edx+2]
  0051f1b0  8b8c2484000000        mov       ecx,[esp+84h]
  0051f1b7  89542424              mov       [esp+24h],edx
  0051f1bb  8bd0                  mov       edx,eax
  0051f1bd  0fafcd                imul      ecx,ebp
  0051f1c0  0faf542414            imul      edx,[esp+14h]
  0051f1c5  896c2428              mov       [esp+28h],ebp
  0051f1c9  8b6c2410              mov       ebp,[esp+10h]
  0051f1cd  0faf442428            imul      eax,[esp+28h]
  0051f1d2  03d1                  add       edx,ecx
  0051f1d4  8b4c2424              mov       ecx,[esp+24h]
  0051f1d8  0fafe9                imul      ebp,ecx
  0051f1db  0faf8c2484000000      imul      ecx,[esp+84h]
  0051f1e3  03d5                  add       edx,ebp
  0051f1e5  2bd1                  sub       edx,ecx
  0051f1e7  8b4c2418              mov       ecx,[esp+18h]
  0051f1eb  2bd0                  sub       edx,eax
  0051f1ed  8b442410              mov       eax,[esp+10h]
  0051f1f1  0faf442414            imul      eax,[esp+14h]
  0051f1f6  2bd0                  sub       edx,eax
  0051f1f8  a114876000            mov       eax,[608714h]
  0051f1fd  81e10000ffff          and       ecx,0FFFF0000h
  0051f203  81f90000f000          cmp       ecx,0F00000h
  0051f209  8910                  mov       [eax],edx
  0051f20b  7229                  jb        short 0051F236h
  0051f20d  8b942480000000        mov       edx,[esp+80h]
  0051f214  81e20000ffff          and       edx,0FFFF0000h
  0051f21a  81fa0000f000          cmp       edx,0F00000h
  0051f220  7214                  jb        short 0051F236h
  0051f222  8b442478              mov       eax,[esp+78h]
  0051f226  250000ffff            and       eax,0FFFF0000h
  0051f22b  3d0000f000            cmp       eax,0F00000h
  0051f230  0f8304010000          jae       near 0051F33Ah

LAB_0051f236:
  0051f236  e8b5d0ffff            call      0051C2F0h
  0051f23b  85c0                  test      eax,eax
  0051f23d  0f8df7000000          jge       near 0051F33Ah
  0051f243  8b4c242c              mov       ecx,[esp+2Ch]
  0051f247  8b442430              mov       eax,[esp+30h]
  0051f24b  8b148f                mov       edx,[edi+ecx*4]
  0051f24e  8b0487                mov       eax,[edi+eax*4]
  0051f251  8b4c2434              mov       ecx,[esp+34h]
  0051f255  3bc2                  cmp       eax,edx
  0051f257  8b0c8f                mov       ecx,[edi+ecx*4]
  0051f25a  7e02                  jle       short 0051F25Eh
  0051f25c  8bd0                  mov       edx,eax

LAB_0051f25e:
  0051f25e  3bca                  cmp       ecx,edx
  0051f260  7e02                  jle       short 0051F264h
  0051f262  8bd1                  mov       edx,ecx

LAB_0051f264:
  0051f264  8d82c0feffff          lea       eax,[edx-140h]
  0051f26a  3dc00e0000            cmp       eax,0EC0h
  0051f26f  0f83c5000000          jae       near 0051F33Ah
  0051f275  8b6b0c                mov       ebp,[ebx+0Ch]
  0051f278  8b4304                mov       eax,[ebx+4]
  0051f27b  896c2440              mov       [esp+40h],ebp
  0051f27f  8be8                  mov       ebp,eax
  0051f281  81e5ff000000          and       ebp,0FFh
  0051f287  8b4b08                mov       ecx,[ebx+8]
  0051f28a  81e2fcff0000          and       edx,0FFFCh
  0051f290  8bacaf00400000        mov       ebp,[edi+ebp*4+4000h]
  0051f297  89ac2484000000        mov       [esp+84h],ebp
  0051f29e  8be8                  mov       ebp,eax
  0051f2a0  c1e810                shr       eax,10h
  0051f2a3  25ff000000            and       eax,0FFh
  0051f2a8  c1ed08                shr       ebp,8
  0051f2ab  8b848700400000        mov       eax,[edi+eax*4+4000h]
  0051f2b2  81e5ff000000          and       ebp,0FFh
  0051f2b8  89442434              mov       [esp+34h],eax
  0051f2bc  8b442474              mov       eax,[esp+74h]
  0051f2c0  8bacaf00400000        mov       ebp,[edi+ebp*4+4000h]
  0051f2c7  03d0                  add       edx,eax
  0051f2c9  8bc2                  mov       eax,edx
  0051f2cb  8b10                  mov       edx,[eax]
  0051f2cd  8930                  mov       [eax],esi
  0051f2cf  8b442478              mov       eax,[esp+78h]
  0051f2d3  81ca00000009          or        edx,9000000h
  0051f2d9  8916                  mov       [esi],edx
  0051f2db  8b542418              mov       edx,[esp+18h]
  0051f2df  894614                mov       [esi+14h],eax
  0051f2e2  8b842484000000        mov       eax,[esp+84h]
  0051f2e9  895608                mov       [esi+8],edx
  0051f2ec  8b942480000000        mov       edx,[esp+80h]
  0051f2f3  0d00000034            or        eax,34000000h
  0051f2f8  895620                mov       [esi+20h],edx
  0051f2fb  894604                mov       [esi+4],eax
  0051f2fe  8b442434              mov       eax,[esp+34h]
  0051f302  8bd1                  mov       edx,ecx
  0051f304  894610                mov       [esi+10h],eax
  0051f307  8b442438              mov       eax,[esp+38h]
  0051f30b  81e2ffff0000          and       edx,0FFFFh
  0051f311  0bd0                  or        edx,eax
  0051f313  8b442440              mov       eax,[esp+40h]
  0051f317  89560c                mov       [esi+0Ch],edx
  0051f31a  8b54243c              mov       edx,[esp+3Ch]
  0051f31e  c1e910                shr       ecx,10h
  0051f321  0bca                  or        ecx,edx
  0051f323  896e1c                mov       [esi+1Ch],ebp
  0051f326  894e18                mov       [esi+18h],ecx
  0051f329  894624                mov       [esi+24h],eax
  0051f32c  a118985e00            mov       eax,[5E9818h]
  0051f331  83c628                add       esi,28h
  0051f334  48                    dec       eax
  0051f335  a318985e00            mov       [5E9818h],eax

LAB_0051f33a:
  0051f33a  8b44241c              mov       eax,[esp+1Ch]
  0051f33e  83c310                add       ebx,10h
  0051f341  48                    dec       eax
  0051f342  85c0                  test      eax,eax
  0051f344  8944241c              mov       [esp+1Ch],eax
  0051f348  0f85a0fdffff          jne       near 0051F0EEh

LAB_0051f34e:
  0051f34e  8b4c247c              mov       ecx,[esp+7Ch]
  0051f352  33c0                  xor       eax,eax
  0051f354  668b411e              mov       ax,[ecx+1Eh]
  0051f358  85c0                  test      eax,eax
  0051f35a  8944241c              mov       [esp+1Ch],eax
  0051f35e  0f84dd020000          je        near 0051F641h
  0051f364  a118985e00            mov       eax,[5E9818h]
  0051f369  85c0                  test      eax,eax
  0051f36b  0f84d0020000          je        near 0051F641h
  0051f371  8b03                  mov       eax,[ebx]
  0051f373  8b150c876000          mov       edx,[60870Ch]
  0051f379  8bc8                  mov       ecx,eax
  0051f37b  81e1ff000000          and       ecx,0FFh
  0051f381  894c242c              mov       [esp+2Ch],ecx
  0051f385  8bac8f00c0ffff        mov       ebp,[edi+ecx*4-4000h]
  0051f38c  8bc8                  mov       ecx,eax
  0051f38e  c1e908                shr       ecx,8
  0051f391  81e1ff000000          and       ecx,0FFh
  0051f397  896c2418              mov       [esp+18h],ebp
  0051f39b  894c2430              mov       [esp+30h],ecx
  0051f39f  8b8c8f00c0ffff        mov       ecx,[edi+ecx*4-4000h]
  0051f3a6  892a                  mov       [edx],ebp
  0051f3a8  8bd0                  mov       edx,eax
  0051f3aa  898c2480000000        mov       [esp+80h],ecx
  0051f3b1  c1ea10                shr       edx,10h
  0051f3b4  81e2ff000000          and       edx,0FFh
  0051f3ba  89542434              mov       [esp+34h],edx
  0051f3be  8b949700c0ffff        mov       edx,[edi+edx*4-4000h]
  0051f3c5  89542478              mov       [esp+78h],edx
  0051f3c9  8b1508876000          mov       edx,[608708h]
  0051f3cf  c1e818                shr       eax,18h
  0051f3d2  890a                  mov       [edx],ecx
  0051f3d4  8b1510876000          mov       edx,[608710h]
  0051f3da  89442440              mov       [esp+40h],eax
  0051f3de  8b848700c0ffff        mov       eax,[edi+eax*4-4000h]
  0051f3e5  89442420              mov       [esp+20h],eax
  0051f3e9  8b442478              mov       eax,[esp+78h]
  0051f3ed  6681fd4001            cmp       bp,140h
  0051f3f2  8902                  mov       [edx],eax
  0051f3f4  721a                  jb        short 0051F410h
  0051f3f6  6681f94001            cmp       cx,140h
  0051f3fb  7213                  jb        short 0051F410h
  0051f3fd  663d4001              cmp       ax,140h
  0051f401  720d                  jb        short 0051F410h
  0051f403  66817c24204001        cmp       word [esp+20h],140h
  0051f40a  0f831d020000          jae       near 0051F62Dh

LAB_0051f410:
  0051f410  a108876000            mov       eax,[608708h]
  0051f415  8b0d10876000          mov       ecx,[608710h]
  0051f41b  0fbf10                movsx     edx,word [eax]
  0051f41e  0fbf4002              movsx     eax,word [eax+2]
  0051f422  0fbf6902              movsx     ebp,word [ecx+2]
  0051f426  0fbf09                movsx     ecx,word [ecx]
  0051f429  89942484000000        mov       [esp+84h],edx
  0051f430  8b150c876000          mov       edx,[60870Ch]
  0051f436  89442414              mov       [esp+14h],eax
  0051f43a  894c2410              mov       [esp+10h],ecx
  0051f43e  0fbf02                movsx     eax,word [edx]
  0051f441  0fbf5202              movsx     edx,word [edx+2]
  0051f445  8b8c24                (bad)
  (octet invalide — la plage demandee deborde probablement du code)
