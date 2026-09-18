C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004c9e00  section .text  offset fichier 0xc9200  2946 octets

  004c9e00  81ec84000000          sub       esp,84h
  004c9e06  53                    push      ebx
  004c9e07  55                    push      ebp
  004c9e08  56                    push      esi
  004c9e09  8bb42494000000        mov       esi,[esp+94h]
  004c9e10  57                    push      edi
  004c9e11  33db                  xor       ebx,ebx
  004c9e13  8a4641                mov       al,[esi+41h]
  004c9e16  bf64000000            mov       edi,64h
  004c9e1b  84c0                  test      al,al
  004c9e1d  66895c2430            mov       [esp+30h],bx
  004c9e22  66c74424321e00        mov       word [esp+32h],1Eh
  004c9e29  66c74424343200        mov       word [esp+34h],32h
  004c9e30  66897c2436            mov       [esp+36h],di
  004c9e35  66895c2438            mov       [esp+38h],bx
  004c9e3a  66897c243a            mov       [esp+3Ah],di
  004c9e3f  7527                  jne       short 004C9E68h
  004c9e41  53                    push      ebx
  004c9e42  6a03                  push      3
  004c9e44  56                    push      esi
  004c9e45  c6464101              mov       byte [esi+41h],1
  004c9e49  e832efffff            call      004C8D80h
  004c9e4e  83c40c                add       esp,0Ch
  004c9e51  e8ead20700            call      00547140h
  004c9e56  99                    cdq
  004c9e57  33c2                  xor       eax,edx
  004c9e59  2bc2                  sub       eax,edx
  004c9e5b  25ff0f0000            and       eax,0FFFh
  004c9e60  33c2                  xor       eax,edx
  004c9e62  2bc2                  sub       eax,edx
  004c9e64  66894614              mov       [esi+14h],ax

LAB_004c9e68:
  004c9e68  8b2e                  mov       ebp,[esi]
  004c9e6a  81cd00100001          or        ebp,1001000h
  004c9e70  892e                  mov       [esi],ebp
  004c9e72  66017e14              add       [esi+14h],di
  004c9e76  668b4614              mov       ax,[esi+14h]
  004c9e7a  663d0010              cmp       ax,1000h
  004c9e7e  7e04                  jle       short 004C9E84h
  004c9e80  66895e14              mov       [esi+14h],bx

LAB_004c9e84:
  004c9e84  0fbf4614              movsx     eax,word [esi+14h]
  004c9e88  50                    push      eax
  004c9e89  e88ebb0700            call      00545A1Ch
  004c9e8e  c1f806                sar       eax,6
  004c9e91  046e                  add       al,6Eh
  004c9e93  884638                mov       [esi+38h],al
  004c9e96  0fbf4e14              movsx     ecx,word [esi+14h]
  004c9e9a  51                    push      ecx
  004c9e9b  e87cbb0700            call      00545A1Ch
  004c9ea0  c1f806                sar       eax,6
  004c9ea3  046e                  add       al,6Eh
  004c9ea5  884639                mov       [esi+39h],al
  004c9ea8  0fbf5614              movsx     edx,word [esi+14h]
  004c9eac  52                    push      edx
  004c9ead  e86abb0700            call      00545A1Ch
  004c9eb2  c1f806                sar       eax,6
  004c9eb5  046e                  add       al,6Eh
  004c9eb7  88463a                mov       [esi+3Ah],al
  004c9eba  0fbf4614              movsx     eax,word [esi+14h]
  004c9ebe  50                    push      eax
  004c9ebf  e858bb0700            call      00545A1Ch
  004c9ec4  0fbf4e14              movsx     ecx,word [esi+14h]
  004c9ec8  c1f804                sar       eax,4
  004c9ecb  0500100000            add       eax,1000h
  004c9ed0  51                    push      ecx
  004c9ed1  66894628              mov       [esi+28h],ax
  004c9ed5  e842bb0700            call      00545A1Ch
  004c9eda  0fbf5614              movsx     edx,word [esi+14h]
  004c9ede  c1f804                sar       eax,4
  004c9ee1  0500100000            add       eax,1000h
  004c9ee6  52                    push      edx
  004c9ee7  6689462a              mov       [esi+2Ah],ax
  004c9eeb  e82cbb0700            call      00545A1Ch
  004c9ef0  8b4e20                mov       ecx,[esi+20h]
  004c9ef3  8b5624                mov       edx,[esi+24h]
  004c9ef6  c1f804                sar       eax,4
  004c9ef9  0500100000            add       eax,1000h
  004c9efe  8d6e1c                lea       ebp,[esi+1Ch]
  004c9f01  6689462c              mov       [esi+2Ch],ax
  004c9f05  894c2428              mov       [esp+28h],ecx
  004c9f09  8b4500                mov       eax,[ebp]
  004c9f0c  8b4e60                mov       ecx,[esi+60h]
  004c9f0f  89442424              mov       [esp+24h],eax
  004c9f13  8b4664                mov       eax,[esi+64h]
  004c9f16  8954242c              mov       [esp+2Ch],edx
  004c9f1a  668b5632              mov       dx,[esi+32h]
  004c9f1e  89442444              mov       [esp+44h],eax
  004c9f22  8b467c                mov       eax,[esi+7Ch]
  004c9f25  894c2440              mov       [esp+40h],ecx
  004c9f29  668b8e90000000        mov       cx,[esi+90h]
  004c9f30  6689942482000000      mov       [esp+82h],dx
  004c9f38  8b9680000000          mov       edx,[esi+80h]
  004c9f3e  8944245c              mov       [esp+5Ch],eax
  004c9f42  8b8684000000          mov       eax,[esi+84h]
  004c9f48  66894c2448            mov       [esp+48h],cx
  004c9f4d  8b8e88000000          mov       ecx,[esi+88h]
  004c9f53  8b7e6c                mov       edi,[esi+6Ch]
  004c9f56  8954244c              mov       [esp+4Ch],edx
  004c9f5a  8b968c000000          mov       edx,[esi+8Ch]
  004c9f60  8944243c              mov       [esp+3Ch],eax
  004c9f64  894c2458              mov       [esp+58h],ecx
  004c9f68  8d442460              lea       eax,[esp+60h]
  004c9f6c  89542450              mov       [esp+50h],edx
  004c9f70  8d4c2468              lea       ecx,[esp+68h]
  004c9f74  50                    push      eax
  004c9f75  8d542468              lea       edx,[esp+68h]
  004c9f79  51                    push      ecx
  004c9f7a  52                    push      edx
  004c9f7b  56                    push      esi
  004c9f7c  e8ffe90600            call      00538980h
  004c9f81  83c410                add       esp,10h
  004c9f84  8d442454              lea       eax,[esp+54h]
  004c9f88  8d4c2420              lea       ecx,[esp+20h]
  004c9f8c  8d542418              lea       edx,[esp+18h]
  004c9f90  50                    push      eax
  004c9f91  51                    push      ecx
  004c9f92  8d44241c              lea       eax,[esp+1Ch]
  004c9f96  52                    push      edx
  004c9f97  8d4c241c              lea       ecx,[esp+1Ch]
  004c9f9b  50                    push      eax
  004c9f9c  51                    push      ecx
  004c9f9d  56                    push      esi
  004c9f9e  e87dbf0600            call      00535F20h
  004c9fa3  8b4c2428              mov       ecx,[esp+28h]
  004c9fa7  8b54246c              mov       edx,[esp+6Ch]
  004c9fab  83c418                add       esp,18h
  004c9fae  b840000000            mov       eax,40h
  004c9fb3  2bc1                  sub       eax,ecx
  004c9fb5  52                    push      edx
  004c9fb6  56                    push      esi
  004c9fb7  89442424              mov       [esp+24h],eax
  004c9fbb  e820be0000            call      004D5DE0h
  004c9fc0  8b4500                mov       eax,[ebp]
  004c9fc3  8b571c                mov       edx,[edi+1Ch]
  004c9fc6  8b4e20                mov       ecx,[esi+20h]
  004c9fc9  2bc2                  sub       eax,edx
  004c9fcb  8b5720                mov       edx,[edi+20h]
  004c9fce  83c408                add       esp,8
  004c9fd1  2bca                  sub       ecx,edx
  004c9fd3  8b5624                mov       edx,[esi+24h]
  004c9fd6  2b5724                sub       edx,[edi+24h]
  004c9fd9  c1f80c                sar       eax,0Ch
  004c9fdc  8bf8                  mov       edi,eax
  004c9fde  c1f90c                sar       ecx,0Ch
  004c9fe1  0faff8                imul      edi,eax
  004c9fe4  8bc1                  mov       eax,ecx
  004c9fe6  c1fa0c                sar       edx,0Ch
  004c9fe9  0fafc1                imul      eax,ecx
  004c9fec  8bca                  mov       ecx,edx
  004c9fee  03f8                  add       edi,eax
  004c9ff0  0fafca                imul      ecx,edx
  004c9ff3  03f9                  add       edi,ecx
  004c9ff5  57                    push      edi
  004c9ff6  e815ba0700            call      00545A10h
  004c9ffb  8b4c2420              mov       ecx,[esp+20h]
  004c9fff  89442414              mov       [esp+14h],eax
  004ca003  3bcb                  cmp       ecx,ebx
  004ca005  0f8437040000          je        near 004CA442h
  004ca00b  8b16                  mov       edx,[esi]
  004ca00d  33c0                  xor       eax,eax
  004ca00f  8a4604                mov       al,[esi+4]
  004ca012  81e2fffffffd          and       edx,0FDFFFFFFh
  004ca018  0fbf7e18              movsx     edi,word [esi+18h]
  004ca01c  8916                  mov       [esi],edx
  004ca01e  8b048540965e00        mov       eax,[eax*4+5E9640h]
  004ca025  f7c200000001          test      edx,1000000h
  004ca02b  8b3cb8                mov       edi,[eax+edi*4]
  004ca02e  741d                  je        short 004CA04Dh
  004ca030  66ff461a              inc       word [esi+1Ah]
  004ca034  668b461a              mov       ax,[esi+1Ah]
  004ca038  33d2                  xor       edx,edx
  004ca03a  668b5716              mov       dx,[edi+16h]
  004ca03e  0fbfc0                movsx     eax,ax
  004ca041  3bc2                  cmp       eax,edx
  004ca043  7c2a                  jl        short 004CA06Fh
  004ca045  66c7461a0000          mov       word [esi+1Ah],0
  004ca04b  eb22                  jmp       short 004CA06Fh

LAB_004ca04d:
  004ca04d  66ff461a              inc       word [esi+1Ah]
  004ca051  668b461a              mov       ax,[esi+1Ah]
  004ca055  33db                  xor       ebx,ebx
  004ca057  668b5f16              mov       bx,[edi+16h]
  004ca05b  0fbff8                movsx     edi,ax
  004ca05e  3bfb                  cmp       edi,ebx
  004ca060  7c0d                  jl        short 004CA06Fh
  004ca062  48                    dec       eax
  004ca063  81ca00000002          or        edx,2000000h
  004ca069  6689461a              mov       [esi+1Ah],ax
  004ca06d  8916                  mov       [esi],edx

LAB_004ca06f:
  004ca06f  8a4642                mov       al,[esi+42h]
  004ca072  3c05                  cmp       al,5
  004ca074  8b06                  mov       eax,[esi]
  004ca076  7504                  jne       short 004CA07Ch
  004ca078  0c10                  or        al,10h
  004ca07a  eb02                  jmp       short 004CA07Eh

LAB_004ca07c:
  004ca07c  24ef                  and       al,0EFh

LAB_004ca07e:
  004ca07e  8906                  mov       [esi],eax
  004ca080  8a06                  mov       al,[esi]
  004ca082  a802                  test      al,2
  004ca084  7474                  je        short 004CA0FAh
  004ca086  bb06000000            mov       ebx,6
  004ca08b  66395e1a              cmp       [esi+1Ah],bx
  004ca08f  7558                  jne       short 004CA0E9h
  004ca091  8d4c2430              lea       ecx,[esp+30h]
  004ca095  6a00                  push      0
  004ca097  51                    push      ecx
  004ca098  55                    push      ebp
  004ca099  56                    push      esi
  004ca09a  e891fb0400            call      00519C30h
  004ca09f  8bf8                  mov       edi,eax
  004ca0a1  a1b8e05d00            mov       eax,[5DE0B8h]
  004ca0a6  83c410                add       esp,10h
  004ca0a9  85c0                  test      eax,eax
  004ca0ab  7438                  je        short 004CA0E5h
  004ca0ad  66395e1a              cmp       [esi+1Ah],bx
  004ca0b1  7532                  jne       short 004CA0E5h
  004ca0b3  8b1f                  mov       ebx,[edi]
  004ca0b5  57                    push      edi
  004ca0b6  83cb40                or        ebx,40h
  004ca0b9  56                    push      esi
  004ca0ba  66c787960000001000    mov       word [edi+96h],10h
  004ca0c3  891f                  mov       [edi],ebx
  004ca0c5  e8b6fa0600            call      00539B80h
  004ca0ca  83c408                add       esp,8
  004ca0cd  83c71c                add       edi,1Ch
  004ca0d0  57                    push      edi
  004ca0d1  6800180000            push      1800h
  004ca0d6  6800030000            push      300h
  004ca0db  6a03                  push      3
  004ca0dd  e83e3a0700            call      0053DB20h
  004ca0e2  83c410                add       esp,10h

LAB_004ca0e5:
  004ca0e5  8b4c2420              mov       ecx,[esp+20h]

LAB_004ca0e9:
  004ca0e9  66837e1a08            cmp       word [esi+1Ah],8
  004ca0ee  750a                  jne       short 004CA0FAh
  004ca0f0  8b06                  mov       eax,[esi]
  004ca0f2  c6464300              mov       byte [esi+43h],0
  004ca0f6  24fd                  and       al,0FDh
  004ca0f8  8906                  mov       [esi],eax

LAB_004ca0fa:
  004ca0fa  33c0                  xor       eax,eax
  004ca0fc  8a4642                mov       al,[esi+42h]
  004ca0ff  83f80c                cmp       eax,0Ch
  004ca102  0f873a030000          ja        near 004CA442h
  004ca108  ff248584a94c00        jmp       dword [eax*4+4CA984h]
  004ca10f  33c9                  xor       ecx,ecx
  004ca111  66394e12              cmp       [esi+12h],cx
  004ca115  7509                  jne       short 004CA120h
  004ca117  66c746120100          mov       word [esi+12h],1
  004ca11d  894e5c                mov       [esi+5Ch],ecx

LAB_004ca120:
  004ca120  0fbf5610              movsx     edx,word [esi+10h]
  004ca124  39542414              cmp       [esp+14h],edx
  004ca128  0f8c10030000          jl        near 004CA43Eh
  004ca12e  8a4643                mov       al,[esi+43h]
  004ca131  84c0                  test      al,al
  004ca133  0f8421020000          je        near 004CA35Ah
  004ca139  fec8                  dec       al
  004ca13b  894e5c                mov       [esi+5Ch],ecx
  004ca13e  884643                mov       [esi+43h],al
  004ca141  e9fc020000            jmp       004CA442h
  004ca146  66837e1200            cmp       word [esi+12h],0
  004ca14b  7506                  jne       short 004CA153h
  004ca14d  66c746120100          mov       word [esi+12h],1

LAB_004ca153:
  004ca153  8b442418              mov       eax,[esp+18h]
  004ca157  8d88c8000000          lea       ecx,[eax+0C8h]
  004ca15d  81e1ff0f0000          and       ecx,0FFFh
  004ca163  81f990010000          cmp       ecx,190h
  004ca169  7e75                  jle       short 004CA1E0h
  004ca16b  25ff0f0000            and       eax,0FFFh
  004ca170  3d00080000            cmp       eax,800h
  004ca175  7d1a                  jge       short 004CA191h
  004ca177  8b542410              mov       edx,[esp+10h]
  004ca17b  c7465c00000000        mov       dword [esi+5Ch],0
  004ca182  8d8280000000          lea       eax,[edx+80h]
  004ca188  66014632              add       [esi+32h],ax
  004ca18c  e9b1020000            jmp       004CA442h

LAB_004ca191:
  004ca191  8b4c2410              mov       ecx,[esp+10h]
  004ca195  b880ffffff            mov       eax,0FFFFFF80h
  004ca19a  2bc1                  sub       eax,ecx
  004ca19c  c7465c00000000        mov       dword [esi+5Ch],0
  004ca1a3  66014632              add       [esi+32h],ax
  004ca1a7  e996020000            jmp       004CA442h
  004ca1ac  66837e1200            cmp       word [esi+12h],0
  004ca1b1  8b4c2410              mov       ecx,[esp+10h]
  004ca1b5  7514                  jne       short 004CA1CBh
  004ca1b7  8d04c9                lea       eax,[ecx+ecx*8]
  004ca1ba  66c746120100          mov       word [esi+12h],1
  004ca1c0  c1e004                shl       eax,4
  004ca1c3  0500500000            add       eax,5000h
  004ca1c8  89465c                mov       [esi+5Ch],eax

LAB_004ca1cb:
  004ca1cb  66837e1201            cmp       word [esi+12h],1
  004ca1d0  0f856c020000          jne       near 004CA442h
  004ca1d6  0fbf5610              movsx     edx,word [esi+10h]
  004ca1da  39542414              cmp       [esp+14h],edx
  004ca1de  7d10                  jge       short 004CA1F0h

LAB_004ca1e0:
  004ca1e0  c6464300              mov       byte [esi+43h],0
  004ca1e4  c7465c00000000        mov       dword [esi+5Ch],0
  004ca1eb  e952020000            jmp       004CA442h

LAB_004ca1f0:
  004ca1f0  8a4643                mov       al,[esi+43h]
  004ca1f3  84c0                  test      al,al
  004ca1f5  7405                  je        short 004CA1FCh
  004ca1f7  fec8                  dec       al
  004ca1f9  884643                mov       [esi+43h],al

LAB_004ca1fc:
  004ca1fc  8b442418              mov       eax,[esp+18h]
  004ca200  8d9000010000          lea       edx,[eax+100h]
  004ca206  81e2ff0f0000          and       edx,0FFFh
  004ca20c  81fa00020000          cmp       edx,200h
  004ca212  0f8e2a020000          jle       near 004CA442h
  004ca218  25ff0f0000            and       eax,0FFFh
  004ca21d  3d00080000            cmp       eax,800h
  004ca222  7d0f                  jge       short 004CA233h
  004ca224  8d8180000000          lea       eax,[ecx+80h]
  004ca22a  66014632              add       [esi+32h],ax
  004ca22e  e90f020000            jmp       004CA442h

LAB_004ca233:
  004ca233  b880ffffff            mov       eax,0FFFFFF80h
  004ca238  2bc1                  sub       eax,ecx
  004ca23a  66014632              add       [esi+32h],ax
  004ca23e  e9ff010000            jmp       004CA442h
  004ca243  66837e1201            cmp       word [esi+12h],1
  004ca248  0f859d000000          jne       near 004CA2EBh
  004ca24e  8a4643                mov       al,[esi+43h]
  004ca251  84c0                  test      al,al
  004ca253  7405                  je        short 004CA25Ah
  004ca255  fec8                  dec       al
  004ca257  884643                mov       [esi+43h],al

LAB_004ca25a:
  004ca25a  8b442410              mov       eax,[esp+10h]
  004ca25e  8d04c0                lea       eax,[eax+eax*8]
  004ca261  c1e004                shl       eax,4
  004ca264  0500500000            add       eax,5000h
  004ca269  0fafc8                imul      ecx,eax
  004ca26c  c1f90c                sar       ecx,0Ch
  004ca26f  8bf9                  mov       edi,ecx
  004ca271  89465c                mov       [esi+5Ch],eax
  004ca274  0fbf4e10              movsx     ecx,word [esi+10h]
  004ca278  51                    push      ecx
  004ca279  e89eb70700            call      00545A1Ch
  004ca27e  0fafc7                imul      eax,edi
  004ca281  0fbf5610              movsx     edx,word [esi+10h]
  004ca285  8b4d00                mov       ecx,[ebp]
  004ca288  52                    push      edx
  004ca289  c1f80c                sar       eax,0Ch
  004ca28c  03c8                  add       ecx,eax
  004ca28e  894d00                mov       [ebp],ecx
  004ca291  e880b70700            call      00545A16h
  004ca296  0fafc7                imul      eax,edi
  004ca299  8b5e24                mov       ebx,[esi+24h]
  004ca29c  c7465c00000000        mov       dword [esi+5Ch],0
  004ca2a3  c1f80c                sar       eax,0Ch
  004ca2a6  03d8                  add       ebx,eax
  004ca2a8  8b442418              mov       eax,[esp+18h]
  004ca2ac  895e24                mov       [esi+24h],ebx
  004ca2af  8d8800010000          lea       ecx,[eax+100h]
  004ca2b5  81e1ff0f0000          and       ecx,0FFFh
  004ca2bb  81f900020000          cmp       ecx,200h
  004ca2c1  7e28                  jle       short 004CA2EBh
  004ca2c3  25ff0f0000            and       eax,0FFFh
  004ca2c8  3d00080000            cmp       eax,800h
  004ca2cd  7d0a                  jge       short 004CA2D9h
  004ca2cf  8b542410              mov       edx,[esp+10h]
  004ca2d3  8d441232              lea       eax,[edx+edx+32h]
  004ca2d7  eb0e                  jmp       short 004CA2E7h

LAB_004ca2d9:
  004ca2d9  8b442410              mov       eax,[esp+10h]
  004ca2dd  8d0c00                lea       ecx,[eax+eax]
  004ca2e0  b8ceffffff            mov       eax,0FFFFFFCEh
  004ca2e5  2bc1                  sub       eax,ecx

LAB_004ca2e7:
  004ca2e7  66014632              add       [esi+32h],ax

LAB_004ca2eb:
  004ca2eb  66837e1200            cmp       word [esi+12h],0
  004ca2f0  0f854c010000          jne       near 004CA442h
  004ca2f6  66c746120100          mov       word [esi+12h],1
  004ca2fc  e941010000            jmp       004CA442h
  004ca301  668b4e12              mov       cx,[esi+12h]
  004ca305  ba01000000            mov       edx,1
  004ca30a  663bca                cmp       cx,dx
  004ca30d  751c                  jne       short 004CA32Bh
  004ca30f  0fbf4610              movsx     eax,word [esi+10h]
  004ca313  39442414              cmp       [esp+14h],eax
  004ca317  7d06                  jge       short 004CA31Fh
  004ca319  c6464300              mov       byte [esi+43h],0
  004ca31d  eb0c                  jmp       short 004CA32Bh

LAB_004ca31f:
  004ca31f  8a4643                mov       al,[esi+43h]
  004ca322  84c0                  test      al,al
  004ca324  7405                  je        short 004CA32Bh
  004ca326  fec8                  dec       al
  004ca328  884643                mov       [esi+43h],al

LAB_004ca32b:
  004ca32b  6685c9                test      cx,cx
  004ca32e  0f850e010000          jne       near 004CA442h
  004ca334  8b442410              mov       eax,[esp+10h]
  004ca338  8b3e                  mov       edi,[esi]
  004ca33a  66894e1a              mov       [esi+1Ah],cx
  004ca33e  81cf00000001          or        edi,1000000h
  004ca344  8d0cc0                lea       ecx,[eax+eax*8]
  004ca347  66895618              mov       [esi+18h],dx
  004ca34b  c1e104                shl       ecx,4
  004ca34e  893e                  mov       [esi],edi
  004ca350  66895612              mov       [esi+12h],dx
  004ca354  81c100500000          add       ecx,5000h

LAB_004ca35a:
  004ca35a  894e5c                mov       [esi+5Ch],ecx
  004ca35d  e9e0000000            jmp       004CA442h
  004ca362  668b4e12              mov       cx,[esi+12h]
  004ca366  6683f901              cmp       cx,1
  004ca36a  7511                  jne       short 004CA37Dh
  004ca36c  8b06                  mov       eax,[esi]
  004ca36e  a900000002            test      eax,2000000h
  004ca373  7408                  je        short 004CA37Dh
  004ca375  24fd                  and       al,0FDh
  004ca377  c6464300              mov       byte [esi+43h],0
  004ca37b  8906                  mov       [esi],eax

LAB_004ca37d:
  004ca37d  6685c9                test      cx,cx
  004ca380  0f85bc000000          jne       near 004CA442h
  004ca386  8b06                  mov       eax,[esi]
  004ca388  66c746120100          mov       word [esi+12h],1
  004ca38e  25fffffffe            and       eax,0FEFFFFFFh
  004ca393  8906                  mov       [esi],eax
  004ca395  e9a8000000            jmp       004CA442h
  004ca39a  66837e1200            cmp       word [esi+12h],0
  004ca39f  7566                  jne       short 004CA407h
  004ca3a1  b900f45f00            mov       ecx,5FF400h

LAB_004ca3a6:
  004ca3a6  39716c                cmp       [ecx+6Ch],esi
  004ca3a9  750c                  jne       short 004CA3B7h
  004ca3ab  f70100000080          test      dword [ecx],80000000h
  004ca3b1  0f8587000000          jne       near 004CA43Eh

LAB_004ca3b7:
  004ca3b7  81c19c000000          add       ecx,9Ch
  004ca3bd  81f980076000          cmp       ecx,600780h
  004ca3c3  7ce1                  jl        short 004CA3A6h
  004ca3c5  33ff                  xor       edi,edi
  004ca3c7  33d2                  xor       edx,edx
  004ca3c9  b9e2ca5e00            mov       ecx,5ECAE2h

LAB_004ca3ce:
  004ca3ce  80390c                cmp       byte [ecx],0Ch
  004ca3d1  7509                  jne       short 004CA3DCh
  004ca3d3  33c0                  xor       eax,eax
  004ca3d5  8a4608                mov       al,[esi+8]
  004ca3d8  3bd0                  cmp       edx,eax
  004ca3da  7562                  jne       short 004CA43Eh

LAB_004ca3dc:
  004ca3dc  81c19c000000          add       ecx,9Ch
  004ca3e2  42                    inc       edx
  004ca3e3  81f962de5e00          cmp       ecx,5EDE62h
  004ca3e9  7ce3                  jl        short 004CA3CEh
  004ca3eb  8b06                  mov       eax,[esi]
  004ca3ed  66c746180300          mov       word [esi+18h],3
  004ca3f3  25fffffffc            and       eax,0FCFFFFFFh
  004ca3f8  66897e1a              mov       [esi+1Ah],di
  004ca3fc  66c746120100          mov       word [esi+12h],1
  004ca402  897e5c                mov       [esi+5Ch],edi
  004ca405  8906                  mov       [esi],eax

LAB_004ca407:
  004ca407  66837e1201            cmp       word [esi+12h],1
  004ca40c  7534                  jne       short 004CA442h
  004ca40e  817c2414e8030000      cmp       dword [esp+14h],3E8h
  004ca416  7f26                  jg        short 004CA43Eh
  004ca418  8b4e6c                mov       ecx,[esi+6Ch]
  004ca41b  6aff                  push      0FFFFFFFFh
  004ca41d  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  004ca424  6a00                  push      0
  004ca426  68e0000000            push      0E0h
  004ca42b  56                    push      esi
  004ca42c  894e78                mov       [esi+78h],ecx
  004ca42f  e89c05fbff            call      0047A9D0h
  004ca434  83c410                add       esp,10h
  004ca437  6689be9a000000        mov       [esi+9Ah],di

LAB_004ca43e:
  004ca43e  c6464300              mov       byte [esi+43h],0

LAB_004ca442:
  004ca442  0fbf5632              movsx     edx,word [esi+32h]
  004ca446  52                    push      edx
  004ca447  e8d0b50700            call      00545A1Ch
  004ca44c  8944246c              mov       [esp+6Ch],eax
  004ca450  0fbf4632              movsx     eax,word [esi+32h]
  004ca454  50                    push      eax
  004ca455  e8bcb50700            call      00545A16h
  004ca45a  8b5650                mov       edx,[esi+50h]
  004ca45d  8b4c2420              mov       ecx,[esp+20h]
  004ca461  89442474              mov       [esp+74h],eax
  004ca465  8b5e54                mov       ebx,[esi+54h]
  004ca468  0faf4e5c              imul      ecx,[esi+5Ch]
  004ca46c  8bc2                  mov       eax,edx
  004ca46e  8bfa                  mov       edi,edx
  004ca470  c1f808                sar       eax,8
  004ca473  2bf8                  sub       edi,eax
  004ca475  8beb                  mov       ebp,ebx
  004ca477  0fbf869a000000        movsx     eax,word [esi+9Ah]
  004ca47e  c1fa03                sar       edx,3
  004ca481  2bfa                  sub       edi,edx
  004ca483  8bd0                  mov       edx,eax
  004ca485  c1fd08                sar       ebp,8
  004ca488  c1fa08                sar       edx,8
  004ca48b  2bd5                  sub       edx,ebp
  004ca48d  8b6e58                mov       ebp,[esi+58h]
  004ca490  03d3                  add       edx,ebx
  004ca492  8bdd                  mov       ebx,ebp
  004ca494  2bd0                  sub       edx,eax
  004ca496  8bc5                  mov       eax,ebp
  004ca498  c1fb08                sar       ebx,8
  004ca49b  c1f90c                sar       ecx,0Ch
  004ca49e  2bc3                  sub       eax,ebx
  004ca4a0  8bd9                  mov       ebx,ecx
  004ca4a2  0faf5c246c            imul      ebx,[esp+6Ch]
  004ca4a7  0faf4c2474            imul      ecx,[esp+74h]
  004ca4ac  c1fd03                sar       ebp,3
  004ca4af  c1fb0c                sar       ebx,0Ch
  004ca4b2  2bc5                  sub       eax,ebp
  004ca4b4  8b6e1c                mov       ebp,[esi+1Ch]
  004ca4b7  03df                  add       ebx,edi
  004ca4b9  56                    push      esi
  004ca4ba  03eb                  add       ebp,ebx
  004ca4bc  8b5e20                mov       ebx,[esi+20h]
  004ca4bf  c1f90c                sar       ecx,0Ch
  004ca4c2  896e1c                mov       [esi+1Ch],ebp
  004ca4c5  8b6e24                mov       ebp,[esi+24h]
  004ca4c8  03c8                  add       ecx,eax
  004ca4ca  03da                  add       ebx,edx
  004ca4cc  03e9                  add       ebp,ecx
  004ca4ce  897e50                mov       [esi+50h],edi
  004ca4d1  895654                mov       [esi+54h],edx
  004ca4d4  894658                mov       [esi+58h],eax
  004ca4d7  895e20                mov       [esi+20h],ebx
  004ca4da  896e24                mov       [esi+24h],ebp
  004ca4dd  e85e940600            call      00533940h
  004ca4e2  8b9690000000          mov       edx,[esi+90h]
  004ca4e8  8b4660                mov       eax,[esi+60h]
  004ca4eb  8b4e20                mov       ecx,[esi+20h]
  004ca4ee  83c404                add       esp,4
  004ca4f1  80e6fc                and       dh,0FCh
  004ca4f4  3bc8                  cmp       ecx,eax
  004ca4f6  899690000000          mov       [esi+90h],edx
  004ca4fc  7f2a                  jg        short 004CA528h
  004ca4fe  f6c680                test      dh,80h
  004ca501  7525                  jne       short 004CA528h
  004ca503  8b0e                  mov       ecx,[esi]
  004ca505  894620                mov       [esi+20h],eax
  004ca508  f6c101                test      cl,1
  004ca50b  740f                  je        short 004CA51Ch
  004ca50d  83e1fe                and       ecx,0FFFFFFFEh
  004ca510  33db                  xor       ebx,ebx
  004ca512  890e                  mov       [esi],ecx
  004ca514  895e54                mov       [esi+54h],ebx
  004ca517  e925010000            jmp       004CA641h

LAB_004ca51c:
  004ca51c  83e1fe                and       ecx,0FFFFFFFEh
  004ca51f  33db                  xor       ebx,ebx
  004ca521  890e                  mov       [esi],ecx
  004ca523  895e54                mov       [esi+54h],ebx
  004ca526  eb02                  jmp       short 004CA52Ah

LAB_004ca528:
  004ca528  33db                  xor       ebx,ebx

LAB_004ca52a:
  004ca52a  8b0e                  mov       ecx,[esi]
  004ca52c  f6c101                test      cl,1
  004ca52f  750a                  jne       short 004CA53Bh
  004ca531  394620                cmp       [esi+20h],eax
  004ca534  7e05                  jle       short 004CA53Bh
  004ca536  83c901                or        ecx,1
  004ca539  890e                  mov       [esi],ecx

LAB_004ca53b:
  004ca53b  f7c282ad0020          test      edx,2000AD82h
  004ca541  0f84fa000000          je        near 004CA641h
  004ca547  f60601                test      byte [esi],1
  004ca54a  0f85c9000000          jne       near 004CA619h
  004ca550  8a4e42                mov       cl,[esi+42h]
  004ca553  b007                  mov       al,7
  004ca555  3ac8                  cmp       cl,al
  004ca557  7420                  je        short 004CA579h
  004ca559  884642                mov       [esi+42h],al
  004ca55c  e8dfcb0700            call      00547140h
  004ca561  99                    cdq
  004ca562  b928000000            mov       ecx,28h
  004ca567  66895e12              mov       [esi+12h],bx
  004ca56b  f7f9                  idiv      ecx
  004ca56d  66c746105000          mov       word [esi+10h],50h
  004ca573  80c214                add       dl,14h
  004ca576  885643                mov       [esi+43h],dl

LAB_004ca579:
  004ca579  8b16                  mov       edx,[esi]
  004ca57b  8b442428              mov       eax,[esp+28h]
  004ca57f  8b4c242c              mov       ecx,[esp+2Ch]
  004ca583  83e2fd                and       edx,0FFFFFFFDh
  004ca586  8916                  mov       [esi],edx
  004ca588  8b542424              mov       edx,[esp+24h]
  004ca58c  894620                mov       [esi+20h],eax
  004ca58f  8b442448              mov       eax,[esp+48h]
  004ca593  89561c                mov       [esi+1Ch],edx
  004ca596  8b54245c              mov       edx,[esp+5Ch]
  004ca59a  25ffff0000            and       eax,0FFFFh
  004ca59f  894e24                mov       [esi+24h],ecx
  004ca5a2  8b4c243c              mov       ecx,[esp+3Ch]
  004ca5a6  89567c                mov       [esi+7Ch],edx
  004ca5a9  8b542458              mov       edx,[esp+58h]
  004ca5ad  898690000000          mov       [esi+90h],eax
  004ca5b3  8b44244c              mov       eax,[esp+4Ch]
  004ca5b7  898e84000000          mov       [esi+84h],ecx
  004ca5bd  8b4c2444              mov       ecx,[esp+44h]
  004ca5c1  898680000000          mov       [esi+80h],eax
  004ca5c7  8b442450              mov       eax,[esp+50h]
  004ca5cb  899688000000          mov       [esi+88h],edx
  004ca5d1  8b542440              mov       edx,[esp+40h]
  004ca5d5  6882ad0020            push      2000AD82h
  004ca5da  56                    push      esi
  004ca5db  89868c000000          mov       [esi+8Ch],eax
  004ca5e1  894e64                mov       [esi+64h],ecx
  004ca5e4  895660                mov       [esi+60h],edx
  004ca5e7  e874b80000            call      004D5E60h
  004ca5ec  83c408                add       esp,8
  004ca5ef  663bc3                cmp       ax,bx
  004ca5f2  7515                  jne       short 004CA609h
  004ca5f4  e847cb0700            call      00547140h
  004ca5f9  99                    cdq
  004ca5fa  33c2                  xor       eax,edx
  004ca5fc  2bc2                  sub       eax,edx
  004ca5fe  83e001                and       eax,1
  004ca601  33c2                  xor       eax,edx
  004ca603  2bc2                  sub       eax,edx
  004ca605  8d4400ff              lea       eax,[eax+eax-1]

LAB_004ca609:
  004ca609  8b4c2410              mov       ecx,[esp+10h]
  004ca60d  83c140                add       ecx,40h
  004ca610  0fafc8                imul      ecx,eax
  004ca613  66014e32              add       [esi+32h],cx
  004ca617  eb28                  jmp       short 004CA641h

LAB_004ca619:
  004ca619  8b542424              mov       edx,[esp+24h]
  004ca61d  8b44242c              mov       eax,[esp+2Ch]
  004ca621  895e5c                mov       [esi+5Ch],ebx
  004ca624  895e50                mov       [esi+50h],ebx
  004ca627  895e58                mov       [esi+58h],ebx
  004ca62a  89561c                mov       [esi+1Ch],edx
  004ca62d  894624                mov       [esi+24h],eax
  004ca630  885e42                mov       [esi+42h],bl
  004ca633  c6464328              mov       byte [esi+43h],28h
  004ca637  66895e12              mov       [esi+12h],bx
  004ca63b  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004ca641:
  004ca641  6aff                  push      0FFFFFFFFh
  004ca643  6aff                  push      0FFFFFFFFh
  004ca645  6aff                  push      0FFFFFFFFh
  004ca647  56                    push      esi
  004ca648  e8c391ffff            call      004C3810h
  004ca64d  8b442430              mov       eax,[esp+30h]
  004ca651  83c410                add       esp,10h
  004ca654  3bc3                  cmp       eax,ebx
  004ca656  7517                  jne       short 004CA66Fh
  004ca658  668b8c2482000000      mov       cx,[esp+82h]
  004ca660  66894e32              mov       [esi+32h],cx
  004ca664  5f                    pop       edi
  004ca665  5e                    pop       esi
  004ca666  5d                    pop       ebp
  004ca667  5b                    pop       ebx
  004ca668  81c484000000          add       esp,84h
  004ca66e  c3                    ret

LAB_004ca66f:
  004ca66f  385e43                cmp       [esi+43h],bl
  004ca672  0f85be020000          jne       near 004CA936h
  004ca678  8b466c                mov       eax,[esi+6Ch]
  004ca67b  8b561c                mov       edx,[esi+1Ch]
  004ca67e  8b781c                mov       edi,[eax+1Ch]
  004ca681  8b6824                mov       ebp,[eax+24h]
  004ca684  8b4624                mov       eax,[esi+24h]
  004ca687  2bfa                  sub       edi,edx
  004ca689  2be8                  sub       ebp,eax
  004ca68b  c1ff0c                sar       edi,0Ch
  004ca68e  c1fd0c                sar       ebp,0Ch
  004ca691  8bd7                  mov       edx,edi
  004ca693  8bc5                  mov       eax,ebp
  004ca695  0fafd7                imul      edx,edi
  004ca698  0fafc5                imul      eax,ebp
  004ca69b  03d0                  add       edx,eax
  004ca69d  52                    push      edx
  004ca69e  e86db30700            call      00545A10h
  004ca6a3  55                    push      ebp
  004ca6a4  57                    push      edi
  004ca6a5  8944241c              mov       [esp+1Ch],eax
  004ca6a9  e874b30700            call      00545A22h
  004ca6ae  668b4e32              mov       cx,[esi+32h]
  004ca6b2  66895e12              mov       [esi+12h],bx
  004ca6b6  2bc1                  sub       eax,ecx
  004ca6b8  25ff0f0000            and       eax,0FFFh
  004ca6bd  89442418              mov       [esp+18h],eax
  004ca6c1  8b442414              mov       eax,[esp+14h]
  004ca6c5  3dfa000000            cmp       eax,0FAh
  004ca6ca  0f8db2000000          jge       near 004CA782h
  004ca6d0  e86bca0700            call      00547140h
  004ca6d5  99                    cdq
  004ca6d6  b964000000            mov       ecx,64h
  004ca6db  f7f9                  idiv      ecx
  004ca6dd  8bea                  mov       ebp,edx
  004ca6df  83fd32                cmp       ebp,32h
  004ca6e2  7d6d                  jge       short 004CA751h
  004ca6e4  8b542418              mov       edx,[esp+18h]
  004ca6e8  8b7c241c              mov       edi,[esp+1Ch]
  004ca6ec  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004ca6f3  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004ca6fa  25ff0f0000            and       eax,0FFFh
  004ca6ff  3bc1                  cmp       eax,ecx
  004ca701  7f44                  jg        short 004CA747h
  004ca703  e838ca0700            call      00547140h
  004ca708  99                    cdq
  004ca709  8d4c3f01              lea       ecx,[edi+edi+1]
  004ca70d  f7f9                  idiv      ecx
  004ca70f  8b442414              mov       eax,[esp+14h]
  004ca713  83c264                add       edx,64h
  004ca716  3bc2                  cmp       eax,edx
  004ca718  7d2d                  jge       short 004CA747h
  004ca71a  83fd28                cmp       ebp,28h
  004ca71d  7d1a                  jge       short 004CA739h
  004ca71f  8b16                  mov       edx,[esi]
  004ca721  c646420b              mov       byte [esi+42h],0Bh
  004ca725  81e2fffffffe          and       edx,0FEFFFFFFh
  004ca72b  c6464301              mov       byte [esi+43h],1
  004ca72f  83ca02                or        edx,2
  004ca732  895e5c                mov       [esi+5Ch],ebx
  004ca735  8916                  mov       [esi],edx
  004ca737  eb4d                  jmp       short 004CA786h

LAB_004ca739:
  004ca739  c646420c              mov       byte [esi+42h],0Ch
  004ca73d  c6464301              mov       byte [esi+43h],1
  004ca741  66895e12              mov       [esi+12h],bx
  004ca745  eb3f                  jmp       short 004CA786h

LAB_004ca747:
  004ca747  c6464201              mov       byte [esi+42h],1
  004ca74b  c6464301              mov       byte [esi+43h],1
  004ca74f  eb35                  jmp       short 004CA786h

LAB_004ca751:
  004ca751  83fd3c                cmp       ebp,3Ch
  004ca754  0f8ce7010000          jl        near 004CA941h
  004ca75a  83fd46                cmp       ebp,46h
  004ca75d  0f8dde010000          jge       near 004CA941h
  004ca763  8b4c2410              mov       ecx,[esp+10h]
  004ca767  8b442414              mov       eax,[esp+14h]
  004ca76b  83c164                add       ecx,64h
  004ca76e  3bc1                  cmp       eax,ecx
  004ca770  0f8ecb010000          jle       near 004CA941h
  004ca776  c646420c              mov       byte [esi+42h],0Ch
  004ca77a  c6464301              mov       byte [esi+43h],1
  004ca77e  66895e12              mov       [esi+12h],bx

LAB_004ca782:
  004ca782  8b7c241c              mov       edi,[esp+1Ch]

LAB_004ca786:
  004ca786  8b442414              mov       eax,[esp+14h]
  004ca78a  3d58020000            cmp       eax,258h
  004ca78f  0f8dc9000000          jge       near 004CA85Eh
  004ca795  3dfa000000            cmp       eax,0FAh
  004ca79a  0f8cbe000000          jl        near 004CA85Eh
  004ca7a0  e89bc90700            call      00547140h
  004ca7a5  99                    cdq
  004ca7a6  b964000000            mov       ecx,64h
  004ca7ab  f7f9                  idiv      ecx
  004ca7ad  83fa14                cmp       edx,14h
  004ca7b0  7d0d                  jge       short 004CA7BFh
  004ca7b2  c6464201              mov       byte [esi+42h],1
  004ca7b6  c6464301              mov       byte [esi+43h],1
  004ca7ba  e99f000000            jmp       004CA85Eh

LAB_004ca7bf:
  004ca7bf  83fa1e                cmp       edx,1Eh
  004ca7c2  7d11                  jge       short 004CA7D5h
  004ca7c4  c646420c              mov       byte [esi+42h],0Ch
  004ca7c8  c6464301              mov       byte [esi+43h],1
  004ca7cc  66895e12              mov       [esi+12h],bx
  004ca7d0  e989000000            jmp       004CA85Eh

LAB_004ca7d5:
  004ca7d5  83fa28                cmp       edx,28h
  004ca7d8  7d25                  jge       short 004CA7FFh
  004ca7da  c6464202              mov       byte [esi+42h],2
  004ca7de  e85dc90700            call      00547140h
  004ca7e3  99                    cdq
  004ca7e4  b90a000000            mov       ecx,0Ah
  004ca7e9  66c746105000          mov       word [esi+10h],50h
  004ca7ef  f7f9                  idiv      ecx
  004ca7f1  8bc7                  mov       eax,edi
  004ca7f3  d1f8                  sar       eax,1
  004ca7f5  02d0                  add       dl,al
  004ca7f7  80c214                add       dl,14h
  004ca7fa  885643                mov       [esi+43h],dl
  004ca7fd  eb5f                  jmp       short 004CA85Eh

LAB_004ca7ff:
  004ca7ff  83fa46                cmp       edx,46h
  004ca802  7c22                  jl        short 004CA826h
  004ca804  8b4c2410              mov       ecx,[esp+10h]
  004ca808  b864000000            mov       eax,64h
  004ca80d  d1f9                  sar       ecx,1
  004ca80f  83c105                add       ecx,5
  004ca812  2bc2                  sub       eax,edx
  004ca814  3bc1                  cmp       eax,ecx
  004ca816  7d0e                  jge       short 004CA826h
  004ca818  c646420c              mov       byte [esi+42h],0Ch
  004ca81c  c6464301              mov       byte [esi+43h],1
  004ca820  66895e12              mov       [esi+12h],bx
  004ca824  eb38                  jmp       short 004CA85Eh

LAB_004ca826:
  004ca826  c6464203              mov       byte [esi+42h],3
  004ca82a  66895e12              mov       [esi+12h],bx
  004ca82e  e80dc90700            call      00547140h
  004ca833  99                    cdq
  004ca834  b90a000000            mov       ecx,0Ah
  004ca839  f7f9                  idiv      ecx
  004ca83b  8bc7                  mov       eax,edi
  004ca83d  d1f8                  sar       eax,1
  004ca83f  02d0                  add       dl,al
  004ca841  80c214                add       dl,14h
  004ca844  885643                mov       [esi+43h],dl
  004ca847  e8f4c80700            call      00547140h
  004ca84c  99                    cdq
  004ca84d  33c2                  xor       eax,edx
  004ca84f  2bc2                  sub       eax,edx
  004ca851  25ff0f0000            and       eax,0FFFh
  004ca856  33c2                  xor       eax,edx
  004ca858  2bc2                  sub       eax,edx
  004ca85a  66894610              mov       [esi+10h],ax

LAB_004ca85e:
  004ca85e  817c241458020000      cmp       dword [esp+14h],258h
  004ca866  0f8cca000000          jl        near 004CA936h
  004ca86c  e8cfc80700            call      00547140h
  004ca871  99                    cdq
  004ca872  b964000000            mov       ecx,64h
  004ca877  f7f9                  idiv      ecx
  004ca879  83fa1e                cmp       edx,1Eh
  004ca87c  7d35                  jge       short 004CA8B3h
  004ca87e  885e42                mov       [esi+42h],bl
  004ca881  e8bac80700            call      00547140h
  004ca886  99                    cdq
  004ca887  b90a000000            mov       ecx,0Ah
  004ca88c  66895e12              mov       [esi+12h],bx
  004ca890  f7f9                  idiv      ecx
  004ca892  8b4c2410              mov       ecx,[esp+10h]
  004ca896  8bc7                  mov       eax,edi
  004ca898  c1f802                sar       eax,2
  004ca89b  02d0                  add       dl,al
  004ca89d  885643                mov       [esi+43h],dl
  004ca8a0  8d540950              lea       edx,[ecx+ecx+50h]
  004ca8a4  66895610              mov       [esi+10h],dx
  004ca8a8  5f                    pop       edi
  004ca8a9  5e                    pop       esi
  004ca8aa  5d                    pop       ebp
  004ca8ab  5b                    pop       ebx
  004ca8ac  81c484000000          add       esp,84h
  004ca8b2  c3                    ret

LAB_004ca8b3:
  004ca8b3  83fa32                cmp       edx,32h
  004ca8b6  7d43                  jge       short 004CA8FBh
  004ca8b8  c6464203              mov       byte [esi+42h],3
  004ca8bc  66895e12              mov       [esi+12h],bx
  004ca8c0  e87bc80700            call      00547140h
  004ca8c5  99                    cdq
  004ca8c6  b90a000000            mov       ecx,0Ah
  004ca8cb  f7f9                  idiv      ecx
  004ca8cd  8bc7                  mov       eax,edi
  004ca8cf  d1f8                  sar       eax,1
  004ca8d1  02d0                  add       dl,al
  004ca8d3  80c214                add       dl,14h
  004ca8d6  885643                mov       [esi+43h],dl
  004ca8d9  e862c80700            call      00547140h
  004ca8de  99                    cdq
  004ca8df  33c2                  xor       eax,edx
  004ca8e1  2bc2                  sub       eax,edx
  004ca8e3  25ff0f0000            and       eax,0FFFh
  004ca8e8  33c2                  xor       eax,edx
  004ca8ea  2bc2                  sub       eax,edx
  004ca8ec  66894610              mov       [esi+10h],ax
  004ca8f0  5f                    pop       edi
  004ca8f1  5e                    pop       esi
  004ca8f2  5d                    pop       ebp
  004ca8f3  5b                    pop       ebx
  004ca8f4  81c484000000          add       esp,84h
  004ca8fa  c3                    ret

LAB_004ca8fb:
  004ca8fb  83fa46                cmp       edx,46h
  004ca8fe  7d13                  jge       short 004CA913h
  004ca900  c6464201              mov       byte [esi+42h],1
  004ca904  c6464301              mov       byte [esi+43h],1
  004ca908  5f                    pop       edi
  004ca909  5e                    pop       esi
  004ca90a  5d                    pop       ebp
  004ca90b  5b                    pop       ebx
  004ca90c  81c484000000          add       esp,84h
  004ca912  c3                    ret

LAB_004ca913:
  004ca913  c6464202              mov       byte [esi+42h],2
  004ca917  e824c80700            call      00547140h
  004ca91c  99                    cdq
  004ca91d  b90a000000            mov       ecx,0Ah
  004ca922  66c746106400          mov       word [esi+10h],64h
  004ca928  f7f9                  idiv      ecx
  004ca92a  8bc7                  mov       eax,edi
  004ca92c  d1f8                  sar       eax,1
  004ca92e  02d0                  add       dl,al
  004ca930  80c214                add       dl,14h
  004ca933  885643                mov       [esi+43h],dl

LAB_004ca936:
  004ca936  5f                    pop       edi
  004ca937  5e                    pop       esi
  004ca938  5d                    pop       ebp
  004ca939  5b                    pop       ebx
  004ca93a  81c484000000          add       esp,84h
  004ca940  c3                    ret

LAB_004ca941:
  004ca941  c6464203              mov       byte [esi+42h],3
  004ca945  66895e12              mov       [esi+12h],bx
  004ca949  e8f2c70700            call      00547140h
  004ca94e  99                    cdq
  004ca94f  b90a000000            mov       ecx,0Ah
  004ca954  8b7c241c              mov       edi,[esp+1Ch]
  004ca958  f7f9                  idiv      ecx
  004ca95a  8bc7                  mov       eax,edi
  004ca95c  d1f8                  sar       eax,1
  004ca95e  02d0                  add       dl,al
  004ca960  80c214                add       dl,14h
  004ca963  885643                mov       [esi+43h],dl
  004ca966  e8d5c70700            call      00547140h
  004ca96b  99                    cdq
  004ca96c  33c2                  xor       eax,edx
  004ca96e  2bc2                  sub       eax,edx
  004ca970  25ff0f0000            and       eax,0FFFh
  004ca975  33c2                  xor       eax,edx
  004ca977  2bc2                  sub       eax,edx
  004ca979  66894610              mov       [esi+10h],ax
  004ca97d  e904feffff            jmp       004CA786h

56 cible(s) de saut interne : 0x004c9e68, 0x004c9e84, 0x004ca04d, 0x004ca06f, 0x004ca07c, 0x004ca07e, 0x004ca0e5, 0x004ca0e9, 0x004ca0fa, 0x004ca120, 0x004ca153, 0x004ca191, 0x004ca1cb, 0x004ca1e0, 0x004ca1f0, 0x004ca1fc, 0x004ca233, 0x004ca25a, 0x004ca2d9, 0x004ca2e7, 0x004ca2eb, 0x004ca31f, 0x004ca32b, 0x004ca35a, 0x004ca37d, 0x004ca3a6, 0x004ca3b7, 0x004ca3ce, 0x004ca3dc, 0x004ca407, 0x004ca43e, 0x004ca442, 0x004ca51c, 0x004ca528, 0x004ca52a, 0x004ca53b, 0x004ca579, 0x004ca609, 0x004ca619, 0x004ca641, 0x004ca66f, 0x004ca739, 0x004ca747, 0x004ca751, 0x004ca782, 0x004ca786, 0x004ca7bf, 0x004ca7d5, 0x004ca7ff, 0x004ca826, 0x004ca85e, 0x004ca8b3, 0x004ca8fb, 0x004ca913, 0x004ca936, 0x004ca941
