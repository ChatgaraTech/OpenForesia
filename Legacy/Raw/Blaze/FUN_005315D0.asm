C:\Program Files (x86)\dotnet\sdk\10.0.401\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(325,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x005315d0  section .text  offset fichier 0x1309d0  1392 octets


LAB_005315d0:
  005315d0  e81b000000            call      005315F0h
  005315d5  e806010000            call      005316E0h
  005315da  85c0                  test      eax,eax
  005315dc  75f2                  jne       short 005315D0h
  005315de  a310e55d00            mov       [5DE510h],eax
  005315e3  c3                    ret
  005315e4  90                    nop
  005315e5  90                    nop
  005315e6  90                    nop
  005315e7  90                    nop
  005315e8  90                    nop
  005315e9  90                    nop
  005315ea  90                    nop
  005315eb  90                    nop
  005315ec  90                    nop
  005315ed  90                    nop
  005315ee  90                    nop
  005315ef  90                    nop
  005315f0  53                    push      ebx
  005315f1  56                    push      esi
  005315f2  57                    push      edi
  005315f3  e80890edff            call      0040A600h
  005315f8  a1c4975e00            mov       eax,[5E97C4h]
  005315fd  8b0db0925e00          mov       ecx,[5E92B0h]
  00531603  50                    push      eax
  00531604  51                    push      ecx
  00531605  e84685ffff            call      00529B50h
  0053160a  8b15c4975e00          mov       edx,[5E97C4h]
  00531610  83c408                add       esp,8
  00531613  a3c8975e00            mov       [5E97C8h],eax
  00531618  52                    push      edx
  00531619  e8520a0000            call      00532070h
  0053161e  a1c4975e00            mov       eax,[5E97C4h]
  00531623  8b1528e75d00          mov       edx,[5DE728h]
  00531629  83c404                add       esp,4
  0053162c  8b4810                mov       ecx,[eax+10h]
  0053162f  a1b4876000            mov       eax,[6087B4h]
  00531634  8d34d1                lea       esi,[ecx+edx*8]
  00531637  8b08                  mov       ecx,[eax]
  00531639  81c100001b00          add       ecx,1B0000h
  0053163f  8b16                  mov       edx,[esi]
  00531641  51                    push      ecx
  00531642  52                    push      edx
  00531643  e80885ffff            call      00529B50h
  00531648  a1b4876000            mov       eax,[6087B4h]
  0053164d  83c408                add       esp,8
  00531650  83c604                add       esi,4
  00531653  8b08                  mov       ecx,[eax]
  00531655  56                    push      esi
  00531656  81c100001b00          add       ecx,1B0000h
  0053165c  68008f5e00            push      5E8F00h
  00531661  51                    push      ecx
  00531662  e8890c0000            call      005322F0h
  00531667  8b15b4876000          mov       edx,[6087B4h]
  0053166d  8b0dc4975e00          mov       ecx,[5E97C4h]
  00531673  83c40c                add       esp,0Ch
  00531676  8b02                  mov       eax,[edx]
  00531678  8b512c                mov       edx,[ecx+2Ch]
  0053167b  8b0d28e75d00          mov       ecx,[5DE728h]
  00531681  0500801300            add       eax,138000h
  00531686  50                    push      eax
  00531687  8bd8                  mov       ebx,eax
  00531689  8d34ca                lea       esi,[edx+ecx*8]
  0053168c  8b14ca                mov       edx,[edx+ecx*8]
  0053168f  52                    push      edx
  00531690  e8bb84ffff            call      00529B50h
  00531695  83c408                add       esp,8
  00531698  8bf8                  mov       edi,eax
  0053169a  68a0965e00            push      5E96A0h
  0053169f  56                    push      esi
  005316a0  53                    push      ebx
  005316a1  e89a0f0000            call      00532640h
  005316a6  a1c4975e00            mov       eax,[5E97C4h]
  005316ab  8b1528e75d00          mov       edx,[5DE728h]
  005316b1  83c40c                add       esp,0Ch
  005316b4  8b487c                mov       ecx,[eax+7Ch]
  005316b7  57                    push      edi
  005316b8  8b0491                mov       eax,[ecx+edx*4]
  005316bb  50                    push      eax
  005316bc  e88f84ffff            call      00529B50h
  005316c1  83c408                add       esp,8
  005316c4  6840965e00            push      5E9640h
  005316c9  57                    push      edi
  005316ca  e801100000            call      005326D0h
  005316cf  83c408                add       esp,8
  005316d2  5f                    pop       edi
  005316d3  5e                    pop       esi
  005316d4  5b                    pop       ebx
  005316d5  c3                    ret
  005316d6  90                    nop
  005316d7  90                    nop
  005316d8  90                    nop
  005316d9  90                    nop
  005316da  90                    nop
  005316db  90                    nop
  005316dc  90                    nop
  005316dd  90                    nop
  005316de  90                    nop
  005316df  90                    nop
  005316e0  83ec1c                sub       esp,1Ch
  005316e3  53                    push      ebx
  005316e4  55                    push      ebp
  005316e5  56                    push      esi
  005316e6  8d442410              lea       eax,[esp+10h]
  005316ea  57                    push      edi
  005316eb  8d4c2424              lea       ecx,[esp+24h]
  005316ef  50                    push      eax
  005316f0  8d542420              lea       edx,[esp+20h]
  005316f4  33db                  xor       ebx,ebx
  005316f6  51                    push      ecx
  005316f7  52                    push      edx
  005316f8  895c2424              mov       [esp+24h],ebx
  005316fc  895c2434              mov       [esp+34h],ebx
  00531700  e8db8affff            call      0052A1E0h
  00531705  a1c4975e00            mov       eax,[5E97C4h]
  0053170a  83c40c                add       esp,0Ch
  0053170d  33ff                  xor       edi,edi
  0053170f  8b407c                mov       eax,[eax+7Ch]
  00531712  897c2420              mov       [esp+20h],edi
  00531716  3918                  cmp       [eax],ebx
  00531718  740f                  je        short 00531729h

LAB_0053171a:
  0053171a  8b4804                mov       ecx,[eax+4]
  0053171d  83c004                add       eax,4
  00531720  47                    inc       edi
  00531721  3bcb                  cmp       ecx,ebx
  00531723  75f5                  jne       short 0053171Ah
  00531725  897c2420              mov       [esp+20h],edi

LAB_00531729:
  00531729  8b2d28e75d00          mov       ebp,[5DE728h]
  0053172f  53                    push      ebx
  00531730  896c2414              mov       [esp+14h],ebp
  00531734  e8d733fdff            call      00504B10h
  00531739  b800180000            mov       eax,1800h
  0053173e  83c404                add       esp,4
  00531741  66a3e8935e00          mov       [5E93E8h],ax
  00531747  66a3ea935e00          mov       [5E93EAh],ax
  0053174d  66a3ec935e00          mov       [5E93ECh],ax
  00531753  b880000000            mov       eax,80h
  00531758  50                    push      eax
  00531759  b900020000            mov       ecx,200h
  0053175e  50                    push      eax
  0053175f  50                    push      eax
  00531760  66890d28496000        mov       [604928h],cx
  00531767  66c7052a4960000008    mov       word [60492Ah],800h
  00531770  66891d2c496000        mov       [60492Ch],bx
  00531777  891dd0086000          mov       [6008D0h],ebx
  0053177d  891dd4086000          mov       [6008D4h],ebx
  00531783  891dd8086000          mov       [6008D8h],ebx
  00531789  891df4086000          mov       [6008F4h],ebx
  0053178f  891df8086000          mov       [6008F8h],ebx
  00531795  890dfc086000          mov       [6008FCh],ecx
  0053179b  881dc4935e00          mov       [5E93C4h],bl
  005317a1  66891dd8935e00        mov       [5E93D8h],bx
  005317a8  66891dda935e00        mov       [5E93DAh],bx
  005317af  66c705f6935e000100    mov       word [5E93F6h],1
  005317b8  66891df0935e00        mov       [5E93F0h],bx
  005317bf  66891df2935e00        mov       [5E93F2h],bx
  005317c6  66891df4935e00        mov       [5E93F4h],bx
  005317cd  891ddc935e00          mov       [5E93DCh],ebx
  005317d3  891de0935e00          mov       [5E93E0h],ebx
  005317d9  891de4935e00          mov       [5E93E4h],ebx
  005317df  c705f8935e00ffffff00  mov       dword [5E93F8h],0FFFFFFh
  005317e9  66891dca935e00        mov       [5E93CAh],bx
  005317f0  66890d08935e00        mov       [5E9308h],cx
  005317f7  66c7050a935e008003    mov       word [5E930Ah],380h
  00531800  66891d0c935e00        mov       [5E930Ch],bx
  00531807  66a3a0935e00          mov       [5E93A0h],ax
  0053180d  66a3a2935e00          mov       [5E93A2h],ax
  00531813  66a3a4935e00          mov       [5E93A4h],ax
  00531819  e882420100            call      00545AA0h

LAB_0053181e:
  0053181e  8b0dc4975e00          mov       ecx,[5E97C4h]
  00531824  51                    push      ecx
  00531825  e8d6170000            call      00533000h
  0053182a  33c0                  xor       eax,eax
  0053182c  33c9                  xor       ecx,ecx
  0053182e  a038e55d00            mov       al,[5DE538h]
  00531833  83c404                add       esp,4
  00531836  8d1480                lea       edx,[eax+eax*4]
  00531839  c1e203                shl       edx,3
  0053183c  2bd0                  sub       edx,eax
  0053183e  33c0                  xor       eax,eax
  00531840  8a0495c8935e00        mov       al,[edx*4+5E93C8h]
  00531847  6bc04c                imul      eax,4Ch
  0053184a  8a8888915e00          mov       cl,[eax+5E9188h]
  00531850  8bf1                  mov       esi,ecx
  00531852  c1e604                shl       esi,4
  00531855  81c6204a6000          add       esi,604A20h
  0053185b  668b4602              mov       ax,[esi+2]
  0053185f  8ad0                  mov       dl,al
  00531861  80e209                and       dl,9
  00531864  80fa09                cmp       dl,9
  00531867  0f84aa020000          je        near 00531B17h
  0053186d  a801                  test      al,1
  0053186f  7405                  je        short 00531876h
  00531871  8374241801            xor       dword [esp+18h],1

LAB_00531876:
  00531876  a808                  test      al,8
  00531878  b900020000            mov       ecx,200h
  0053187d  7429                  je        short 005318A8h
  0053187f  66890d28496000        mov       [604928h],cx
  00531886  66c7052a4960000008    mov       word [60492Ah],800h
  0053188f  66891d2c496000        mov       [60492Ch],bx
  00531896  891df4086000          mov       [6008F4h],ebx
  0053189c  891df8086000          mov       [6008F8h],ebx
  005318a2  890dfc086000          mov       [6008FCh],ecx

LAB_005318a8:
  005318a8  668b06                mov       ax,[esi]
  005318ab  f6c480                test      ah,80h
  005318ae  7434                  je        short 005318E4h
  005318b0  668b4606              mov       ax,[esi+6]
  005318b4  a880                  test      al,80h
  005318b6  7405                  je        short 005318BDh
  005318b8  4d                    dec       ebp
  005318b9  896c2410              mov       [esp+10h],ebp

LAB_005318bd:
  005318bd  a820                  test      al,20h
  005318bf  7405                  je        short 005318C6h
  005318c1  45                    inc       ebp
  005318c2  896c2410              mov       [esp+10h],ebp

LAB_005318c6:
  005318c6  3beb                  cmp       ebp,ebx
  005318c8  7d07                  jge       short 005318D1h
  005318ca  8d6fff                lea       ebp,[edi-1]
  005318cd  896c2410              mov       [esp+10h],ebp

LAB_005318d1:
  005318d1  3bef                  cmp       ebp,edi
  005318d3  0f8ca7000000          jl        near 00531980h
  005318d9  33ed                  xor       ebp,ebp
  005318db  896c2410              mov       [esp+10h],ebp
  005318df  e99c000000            jmp       00531980h

LAB_005318e4:
  005318e4  392d28e75d00          cmp       [5DE728h],ebp
  005318ea  0f8519020000          jne       near 00531B09h
  005318f0  a810                  test      al,10h
  005318f2  7408                  je        short 005318FCh
  005318f4  66830528496000c0      add       word [604928h],0FFC0h

LAB_005318fc:
  005318fc  f60640                test      byte [esi],40h
  005318ff  7408                  je        short 00531909h
  00531901  6683052849600040      add       word [604928h],40h

LAB_00531909:
  00531909  f60680                test      byte [esi],80h
  0053190c  7408                  je        short 00531916h
  0053190e  6683052a49600040      add       word [60492Ah],40h

LAB_00531916:
  00531916  f60620                test      byte [esi],20h
  00531919  7408                  je        short 00531923h
  0053191b  6683052a496000c0      add       word [60492Ah],0FFC0h

LAB_00531923:
  00531923  f6460104              test      byte [esi+1],4
  00531927  b820000000            mov       eax,20h
  0053192c  7406                  je        short 00531934h
  0053192e  2905f4086000          sub       [6008F4h],eax

LAB_00531934:
  00531934  f6460108              test      byte [esi+1],8
  00531938  7406                  je        short 00531940h
  0053193a  0105f4086000          add       [6008F4h],eax

LAB_00531940:
  00531940  f6460110              test      byte [esi+1],10h
  00531944  7406                  je        short 0053194Ch
  00531946  2905fc086000          sub       [6008FCh],eax

LAB_0053194c:
  0053194c  f6460140              test      byte [esi+1],40h
  00531950  7406                  je        short 00531958h
  00531952  0105fc086000          add       [6008FCh],eax

LAB_00531958:
  00531958  f6460301              test      byte [esi+3],1
  0053195c  740e                  je        short 0053196Ch
  0053195e  66ff0dd8935e00        dec       word [5E93D8h]
  00531965  66891dda935e00        mov       [5E93DAh],bx

LAB_0053196c:
  0053196c  66854e02              test      [esi+2],cx
  00531970  740e                  je        short 00531980h
  00531972  66ff05d8935e00        inc       word [5E93D8h]
  00531979  66891dda935e00        mov       [5E93DAh],bx

LAB_00531980:
  00531980  668b0dd8935e00        mov       cx,[5E93D8h]
  00531987  a1c4935e00            mov       eax,[5E93C4h]
  0053198c  663bcb                cmp       cx,bx
  0053198f  7d1c                  jge       short 005319ADh
  00531991  8bc8                  mov       ecx,eax
  00531993  81e1ff000000          and       ecx,0FFh
  00531999  8b148d40965e00        mov       edx,[ecx*4+5E9640h]
  005319a0  668b4afc              mov       cx,[edx-4]
  005319a4  6649                  dec       cx
  005319a6  66890dd8935e00        mov       [5E93D8h],cx

LAB_005319ad:
  005319ad  25ff000000            and       eax,0FFh
  005319b2  0fbfc9                movsx     ecx,cx
  005319b5  8b048540965e00        mov       eax,[eax*4+5E9640h]
  005319bc  3b48fc                cmp       ecx,[eax-4]
  005319bf  7207                  jb        short 005319C8h
  005319c1  66891dd8935e00        mov       [5E93D8h],bx

LAB_005319c8:
  005319c8  6890010000            push      190h
  005319cd  e8da400100            call      00545AACh
  005319d2  68be000000            push      0BEh
  005319d7  68a0000000            push      0A0h
  005319dc  e8c5400100            call      00545AA6h
  005319e1  68e0086000            push      6008E0h
  005319e6  6828496000            push      604928h
  005319eb  e810a9feff            call      0051C300h
  005319f0  a1c4935e00            mov       eax,[5E93C4h]
  005319f5  8b7c2420              mov       edi,[esp+20h]
  005319f9  0fbf15d8935e00        movsx     edx,word [5E93D8h]
  00531a00  25ff000000            and       eax,0FFh
  00531a05  83c408                add       esp,8
  00531a08  c1e002                shl       eax,2
  00531a0b  3bfb                  cmp       edi,ebx
  00531a0d  8b8840965e00          mov       ecx,[eax+5E9640h]
  00531a13  8d0c91                lea       ecx,[ecx+edx*4]
  00531a16  8b11                  mov       edx,[ecx]
  00531a18  7425                  je        short 00531A3Fh
  00531a1a  f6460320              test      byte [esi+3],20h
  00531a1e  7443                  je        short 00531A63h
  00531a20  668b35da935e00        mov       si,[5E93DAh]
  00531a27  33ff                  xor       edi,edi
  00531a29  6646                  inc       si
  00531a2b  668935da935e00        mov       [5E93DAh],si
  00531a32  668b7a16              mov       di,[edx+16h]
  00531a36  0fbfd6                movsx     edx,si
  00531a39  3bd7                  cmp       edx,edi
  00531a3b  7c26                  jl        short 00531A63h
  00531a3d  eb1d                  jmp       short 00531A5Ch

LAB_00531a3f:
  00531a3f  668b35da935e00        mov       si,[5E93DAh]
  00531a46  33ff                  xor       edi,edi
  00531a48  6646                  inc       si
  00531a4a  668935da935e00        mov       [5E93DAh],si
  00531a51  668b7a16              mov       di,[edx+16h]
  00531a55  0fbfd6                movsx     edx,si
  00531a58  3bd7                  cmp       edx,edi
  00531a5a  7c07                  jl        short 00531A63h

LAB_00531a5c:
  00531a5c  66891dda935e00        mov       [5E93DAh],bx

LAB_00531a63:
  00531a63  668b15ca935e00        mov       dx,[5E93CAh]
  00531a6a  8b80a0965e00          mov       eax,[eax+5E96A0h]
  00531a70  8b09                  mov       ecx,[ecx]
  00531a72  52                    push      edx
  00531a73  0fbf15da935e00        movsx     edx,word [5E93DAh]
  00531a7a  52                    push      edx
  00531a7b  8b54241c              mov       edx,[esp+1Ch]
  00531a7f  68dc935e00            push      5E93DCh
  00531a84  50                    push      eax
  00531a85  8b44242c              mov       eax,[esp+2Ch]
  00531a89  51                    push      ecx
  00531a8a  52                    push      edx
  00531a8b  50                    push      eax
  00531a8c  e85fb7feff            call      0051D1F0h
  00531a91  0fbf0dd8935e00        movsx     ecx,word [5E93D8h]
  00531a98  89442430              mov       [esp+30h],eax
  00531a9c  a1c4935e00            mov       eax,[5E93C4h]
  00531aa1  25ff000000            and       eax,0FFh
  00531aa6  83c41c                add       esp,1Ch
  00531aa9  c1e002                shl       eax,2
  00531aac  33f6                  xor       esi,esi
  00531aae  33ff                  xor       edi,edi
  00531ab0  8b9040965e00          mov       edx,[eax+5E9640h]
  00531ab6  8b80a0965e00          mov       eax,[eax+5E96A0h]
  00531abc  8b148a                mov       edx,[edx+ecx*4]
  00531abf  33c9                  xor       ecx,ecx
  00531ac1  668b4a14              mov       cx,[edx+14h]
  00531ac5  3bcb                  cmp       ecx,ebx
  00531ac7  741c                  je        short 00531AE5h

LAB_00531ac9:
  00531ac9  8b10                  mov       edx,[eax]
  00531acb  33ed                  xor       ebp,ebp
  00531acd  83c004                add       eax,4
  00531ad0  668b6a1a              mov       bp,[edx+1Ah]
  00531ad4  03f5                  add       esi,ebp
  00531ad6  33ed                  xor       ebp,ebp
  00531ad8  668b6a1e              mov       bp,[edx+1Eh]
  00531adc  03fd                  add       edi,ebp
  00531ade  49                    dec       ecx
  00531adf  75e8                  jne       short 00531AC9h
  00531ae1  8b6c2410              mov       ebp,[esp+10h]

LAB_00531ae5:
  00531ae5  6a01                  push      1
  00531ae7  8d442418              lea       eax,[esp+18h]
  00531aeb  6a03                  push      3
  00531aed  8d4c242c              lea       ecx,[esp+2Ch]
  00531af1  50                    push      eax
  00531af2  8d542428              lea       edx,[esp+28h]
  00531af6  51                    push      ecx
  00531af7  52                    push      edx
  00531af8  e88387ffff            call      0052A280h
  00531afd  8b7c2434              mov       edi,[esp+34h]
  00531b01  83c414                add       esp,14h
  00531b04  e915fdffff            jmp       0053181Eh

LAB_00531b09:
  00531b09  892d28e75d00          mov       [5DE728h],ebp
  00531b0f  c744242801000000      mov       dword [esp+28h],1

LAB_00531b17:
  00531b17  6a01                  push      1
  00531b19  8d442418              lea       eax,[esp+18h]
  00531b1d  6a03                  push      3
  00531b1f  8d4c242c              lea       ecx,[esp+2Ch]
  00531b23  50                    push      eax
  00531b24  8d542428              lea       edx,[esp+28h]
  00531b28  51                    push      ecx
  00531b29  52                    push      edx
  00531b2a  e85187ffff            call      0052A280h
  00531b2f  8b44243c              mov       eax,[esp+3Ch]
  00531b33  83c414                add       esp,14h
  00531b36  5f                    pop       edi
  00531b37  5e                    pop       esi
  00531b38  5d                    pop       ebp
  00531b39  5b                    pop       ebx
  00531b3a  83c41c                add       esp,1Ch
  00531b3d  c3                    ret
  00531b3e  90                    nop
  00531b3f  90                    nop

29 cible(s) de saut interne : 0x005315d0, 0x0053171a, 0x00531729, 0x0053181e, 0x00531876, 0x005318a8, 0x005318bd, 0x005318c6, 0x005318d1, 0x005318e4, 0x005318fc, 0x00531909, 0x00531916, 0x00531923, 0x00531934, 0x00531940, 0x0053194c, 0x00531958, 0x0053196c, 0x00531980, 0x005319ad, 0x005319c8, 0x00531a3f, 0x00531a5c, 0x00531a63, 0x00531ac9, 0x00531ae5, 0x00531b09, 0x00531b17
