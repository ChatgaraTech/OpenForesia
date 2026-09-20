C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046a630  section .text  offset fichier 0x69a30  768 octets

  0046a630  8b442408              mov       eax,[esp+8]
  0046a634  53                    push      ebx
  0046a635  48                    dec       eax
  0046a636  55                    push      ebp
  0046a637  56                    push      esi
  0046a638  83f803                cmp       eax,3
  0046a63b  57                    push      edi
  0046a63c  7723                  ja        short 0046A661h
  0046a63e  ff24855ca74600        jmp       dword [eax*4+46A75Ch]
  0046a645  bf00060000            mov       edi,600h
  0046a64a  eb19                  jmp       short 0046A665h
  0046a64c  bf000a0000            mov       edi,0A00h
  0046a651  eb12                  jmp       short 0046A665h
  0046a653  bf000e0000            mov       edi,0E00h
  0046a658  eb0b                  jmp       short 0046A665h
  0046a65a  bf00120000            mov       edi,1200h
  0046a65f  eb04                  jmp       short 0046A665h

LAB_0046a661:
  0046a661  8b7c2418              mov       edi,[esp+18h]

LAB_0046a665:
  0046a665  8b742414              mov       esi,[esp+14h]
  0046a669  668b5e32              mov       bx,[esi+32h]
  0046a66d  8b4e20                mov       ecx,[esi+20h]
  0046a670  8b5624                mov       edx,[esi+24h]
  0046a673  8b6e1c                mov       ebp,[esi+1Ch]
  0046a676  0fbfc3                movsx     eax,bx
  0046a679  894c2418              mov       [esp+18h],ecx
  0046a67d  8bcf                  mov       ecx,edi
  0046a67f  03c8                  add       ecx,eax
  0046a681  89542414              mov       [esp+14h],edx
  0046a685  51                    push      ecx
  0046a686  e891b30d00            call      00545A1Ch
  0046a68b  8d0480                lea       eax,[eax+eax*4]
  0046a68e  8d1480                lea       edx,[eax+eax*4]
  0046a691  8b461c                mov       eax,[esi+1Ch]
  0046a694  c1e202                shl       edx,2
  0046a697  03c2                  add       eax,edx
  0046a699  89461c                mov       [esi+1Ch],eax
  0046a69c  0fbf4632              movsx     eax,word [esi+32h]
  0046a6a0  03f8                  add       edi,eax
  0046a6a2  57                    push      edi
  0046a6a3  e86eb30d00            call      00545A16h
  0046a6a8  8d0480                lea       eax,[eax+eax*4]
  0046a6ab  8d0c80                lea       ecx,[eax+eax*4]
  0046a6ae  8b4624                mov       eax,[esi+24h]
  0046a6b1  c1e102                shl       ecx,2
  0046a6b4  03c1                  add       eax,ecx
  0046a6b6  894624                mov       [esi+24h],eax
  0046a6b9  e882ca0d00            call      00547140h
  0046a6be  99                    cdq
  0046a6bf  33c2                  xor       eax,edx
  0046a6c1  bf07000000            mov       edi,7
  0046a6c6  2bc2                  sub       eax,edx
  0046a6c8  25ff000000            and       eax,0FFh
  0046a6cd  33c2                  xor       eax,edx
  0046a6cf  2bc2                  sub       eax,edx
  0046a6d1  0580030000            add       eax,380h
  0046a6d6  66014632              add       [esi+32h],ax

LAB_0046a6da:
  0046a6da  6a00                  push      0
  0046a6dc  6a00                  push      0
  0046a6de  6aff                  push      0FFFFFFFFh
  0046a6e0  6a00                  push      0
  0046a6e2  6a34                  push      34h
  0046a6e4  56                    push      esi
  0046a6e5  e826daffff            call      00468110h
  0046a6ea  83c418                add       esp,18h
  0046a6ed  4f                    dec       edi
  0046a6ee  75ea                  jne       short 0046A6DAh
  0046a6f0  e84bca0d00            call      00547140h
  0046a6f5  99                    cdq
  0046a6f6  b90a000000            mov       ecx,0Ah
  0046a6fb  f7f9                  idiv      ecx
  0046a6fd  83fa01                cmp       edx,1
  0046a700  753d                  jne       short 0046A73Fh
  0046a702  e839ca0d00            call      00547140h
  0046a707  99                    cdq
  0046a708  b964000000            mov       ecx,64h
  0046a70d  6a00                  push      0
  0046a70f  f7f9                  idiv      ecx
  0046a711  6a00                  push      0
  0046a713  6800080000            push      800h
  0046a718  83ea32                sub       edx,32h
  0046a71b  81c200040000          add       edx,400h
  0046a721  52                    push      edx
  0046a722  8b15cc975e00          mov       edx,[5E97CCh]
  0046a728  8b82a8000000          mov       eax,[edx+0A8h]
  0046a72e  8b08                  mov       ecx,[eax]
  0046a730  660fb65136            movzx     dx,byte [ecx+36h]
  0046a735  52                    push      edx
  0046a736  56                    push      esi
  0046a737  e844d9ffff            call      00468080h
  0046a73c  83c418                add       esp,18h

LAB_0046a73f:
  0046a73f  8b442418              mov       eax,[esp+18h]
  0046a743  8b4c2414              mov       ecx,[esp+14h]
  0046a747  896e1c                mov       [esi+1Ch],ebp
  0046a74a  894620                mov       [esi+20h],eax
  0046a74d  894e24                mov       [esi+24h],ecx
  0046a750  66895e32              mov       [esi+32h],bx
  0046a754  5f                    pop       edi
  0046a755  5e                    pop       esi
  0046a756  5d                    pop       ebp
  0046a757  5b                    pop       ebx
  0046a758  c3                    ret
  0046a759  90                    nop
  0046a75a  8bff                  mov       edi,edi
  0046a75c  45                    inc       ebp
  0046a75d  a6                    cmpsb
  0046a75e  46                    inc       esi
  0046a75f  004ca646              add       [esi+46h],cl
  0046a763  0053a6                add       [ebx-5Ah],dl
  0046a766  46                    inc       esi
  0046a767  005aa6                add       [edx-5Ah],bl
  0046a76a  46                    inc       esi
  0046a76b  009090909033          add       [eax+33909090h],dl
  0046a771  c0a388185700a3        shl       byte [ebx+571888h],0A3h
  0046a778  8c18                  mov       [eax],ds
  0046a77a  57                    push      edi
  0046a77b  00c3                  add       bl,al
  0046a77d  90                    nop
  0046a77e  90                    nop
  0046a77f  90                    nop
  0046a780  56                    push      esi
  0046a781  8b742408              mov       esi,[esp+8]
  0046a785  66837e1000            cmp       word [esi+10h],0
  0046a78a  7537                  jne       short 0046A7C3h
  0046a78c  66c746100100          mov       word [esi+10h],1
  0046a792  66c746120000          mov       word [esi+12h],0
  0046a798  66c746140000          mov       word [esi+14h],0
  0046a79e  e89dc90d00            call      00547140h
  0046a7a3  99                    cdq
  0046a7a4  b903000000            mov       ecx,3
  0046a7a9  f7f9                  idiv      ecx
  0046a7ab  83fa01                cmp       edx,1
  0046a7ae  7513                  jne       short 0046A7C3h
  0046a7b0  6a00                  push      0
  0046a7b2  6a00                  push      0
  0046a7b4  6aff                  push      0FFFFFFFFh
  0046a7b6  6a00                  push      0
  0046a7b8  6a36                  push      36h
  0046a7ba  56                    push      esi
  0046a7bb  e84018ffff            call      0045C000h
  0046a7c0  83c418                add       esp,18h

LAB_0046a7c3:
  0046a7c3  66837e1001            cmp       word [esi+10h],1
  0046a7c8  753d                  jne       short 0046A807h
  0046a7ca  66ff4612              inc       word [esi+12h]
  0046a7ce  66837e1246            cmp       word [esi+12h],46h
  0046a7d3  7e32                  jle       short 0046A807h
  0046a7d5  e866c90d00            call      00547140h
  0046a7da  99                    cdq
  0046a7db  b914000000            mov       ecx,14h
  0046a7e0  f7f9                  idiv      ecx
  0046a7e2  83fa01                cmp       edx,1
  0046a7e5  7407                  je        short 0046A7EEh
  0046a7e7  66837e1264            cmp       word [esi+12h],64h
  0046a7ec  7e19                  jle       short 0046A807h

LAB_0046a7ee:
  0046a7ee  6a00                  push      0
  0046a7f0  6a00                  push      0
  0046a7f2  6aff                  push      0FFFFFFFFh
  0046a7f4  6a00                  push      0
  0046a7f6  6a36                  push      36h
  0046a7f8  56                    push      esi
  0046a7f9  66c746120000          mov       word [esi+12h],0
  0046a7ff  e8fc17ffff            call      0045C000h
  0046a804  83c418                add       esp,18h

LAB_0046a807:
  0046a807  5e                    pop       esi
  0046a808  c3                    ret
  0046a809  90                    nop
  0046a80a  90                    nop
  0046a80b  90                    nop
  0046a80c  90                    nop
  0046a80d  90                    nop
  0046a80e  90                    nop
  0046a80f  90                    nop
  0046a810  83ec08                sub       esp,8
  0046a813  56                    push      esi
  0046a814  8b742410              mov       esi,[esp+10h]
  0046a818  668b4610              mov       ax,[esi+10h]
  0046a81c  663d0200              cmp       ax,2
  0046a820  750b                  jne       short 0046A82Dh
  0046a822  66c746300000          mov       word [esi+30h],0
  0046a828  5e                    pop       esi
  0046a829  83c408                add       esp,8
  0046a82c  c3                    ret

LAB_0046a82d:
  0046a82d  33c9                  xor       ecx,ecx
  0046a82f  57                    push      edi
  0046a830  55                    push      ebp
  0046a831  663bc1                cmp       ax,cx
  0046a834  53                    push      ebx
  0046a835  7533                  jne       short 0046A86Ah
  0046a837  0fbf4616              movsx     eax,word [esi+16h]
  0046a83b  89465c                mov       [esi+5Ch],eax
  0046a83e  668b4630              mov       ax,[esi+30h]
  0046a842  663bc1                cmp       ax,cx
  0046a845  66c746100100          mov       word [esi+10h],1
  0046a84b  66898e9a000000        mov       [esi+9Ah],cx
  0046a852  740d                  je        short 0046A861h
  0046a854  66898698000000        mov       [esi+98h],ax
  0046a85b  66894e30              mov       [esi+30h],cx
  0046a85f  eb09                  jmp       short 0046A86Ah

LAB_0046a861:
  0046a861  66c786980000002c01    mov       word [esi+98h],12Ch

LAB_0046a86a:
  0046a86a  8b4658                mov       eax,[esi+58h]
  0046a86d  8b4e50                mov       ecx,[esi+50h]
  0046a870  89442414              mov       [esp+14h],eax
  0046a874  894c241c              mov       [esp+1Ch],ecx
  0046a878  0fbf4614              movsx     eax,word [esi+14h]
  0046a87c  8b5654                mov       edx,[esi+54h]
  0046a87f  8b6e20                mov       ebp,[esi+20h]
  0046a882  8d0c80                lea       ecx,[eax+eax*4]
  0046a885  89542410              mov       [esp+10h],edx
  0046a889  c1e103                shl       ecx,3
  0046a88c  2bc8                  sub       ecx,eax
  0046a88e  8bfd                  mov       edi,ebp
  0046a890  c1ff0c                sar       edi,0Ch
  0046a893  8b548e1c              mov       edx,[esi+ecx*4+1Ch]
  0046a897  8d048e                lea       eax,[esi+ecx*4]
  0046a89a  895650                mov       [esi+50h],edx
  0046a89d  8b561c                mov       edx,[esi+1Ch]
  0046a8a0  8b4820                mov       ecx,[eax+20h]
  0046a8a3  894e54                mov       [esi+54h],ecx
  0046a8a6  8b4e50                mov       ecx,[esi+50h]
  0046a8a9  8b4024                mov       eax,[eax+24h]
  0046a8ac  c1fa0c                sar       edx,0Ch
  0046a8af  c1f90c                sar       ecx,0Ch
  0046a8b2  2bca                  sub       ecx,edx
  0046a8b4  8b5654                mov       edx,[esi+54h]
  0046a8b7  c1fa0c                sar       edx,0Ch
  0046a8ba  2bd7                  sub       edx,edi
  0046a8bc  8b7e24                mov       edi,[esi+24h]
  0046a8bf  894658                mov       [esi+58h],eax
  0046a8c2  c1ff0c                sar       edi,0Ch
  0046a8c5  c1f80c                sar       eax,0Ch
  0046a8c8  2bc7                  sub       eax,edi
  0046a8ca  8bf9                  mov       edi,ecx
  0046a8cc  0faff9                imul      edi,ecx
  0046a8cf  8bca                  mov       ecx,edx
  0046a8d1  0fafca                imul      ecx,edx
  0046a8d4  8bd0                  mov       edx,eax
  0046a8d6  03f9                  add       edi,ecx
  0046a8d8  0fafd0                imul      edx,eax
  0046a8db  03fa                  add       edi,edx
  0046a8dd  57                    push      edi
  0046a8de  e82db10d00            call      00545A10h
  0046a8e3  8b4e5c                mov       ecx,[esi+5Ch]
  0046a8e6  8bf8                  mov       edi,eax
  0046a8e8  8bc1                  mov       eax,ecx
  0046a8ea  99                    cdq
  0046a8eb  83e207                and       edx,7
  0046a8ee  03c2                  add       eax,edx
  0046a8f0  8bd8                  mov       ebx,eax
  0046a8f2  0fbf8698000000        movsx     eax,word [esi+98h]
  0046a8f9  c1fb03                sar       ebx,3
  0046a8fc  3bf8                  cmp       edi,eax
  0046a8fe  0f8d9e000000          jge       near 0046A9A2h
  0046a904  33ff                  xor       edi,edi
  0046a906  47                    inc       edi
  0046a907  66897e12              mov       [esi+12h],di
  0046a90b  e830c80d00            call      00547140h
  0046a910  99                    cdq
  0046a911  b903000000            mov       ecx,3
  0046a916  6a00                  push      0
  0046a918  f7f9                  idiv      ecx
  0046a91a  6a00                  push      0
  0046a91c  6aff                  push      0FFFFFFFFh
  0046a91e  6a10                  push      10h
  0046a920  6a30                  push      30h
  0046a922  56                    push      esi
  0046a923  66895616              mov       [esi+16h],dx
  0046a927  e8e4d7ffff            call      00468110h
  0046a92c  83c418                add       esp,18h
  0046a92f  66                    (bad)
  (octet invalide — la plage demandee deborde probablement du code)
