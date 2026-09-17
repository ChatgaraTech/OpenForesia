C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045d750  section .text  offset fichier 0x5cb50  752 octets

  0045d750  51                    push      ecx
  0045d751  53                    push      ebx
  0045d752  55                    push      ebp
  0045d753  56                    push      esi
  0045d754  8b742414              mov       esi,[esp+14h]
  0045d758  33db                  xor       ebx,ebx
  0045d75a  57                    push      edi
  0045d75b  66395e10              cmp       [esi+10h],bx
  0045d75f  7550                  jne       short 0045D7B1h
  0045d761  e8da990e00            call      00547140h
  0045d766  99                    cdq
  0045d767  33c2                  xor       eax,edx
  0045d769  668b4e34              mov       cx,[esi+34h]
  0045d76d  2bc2                  sub       eax,edx
  0045d76f  66895e12              mov       [esi+12h],bx
  0045d773  83e001                and       eax,1
  0045d776  33c2                  xor       eax,edx
  0045d778  2bc2                  sub       eax,edx
  0045d77a  40                    inc       eax
  0045d77b  66894610              mov       [esi+10h],ax
  0045d77f  668b4614              mov       ax,[esi+14h]
  0045d783  66894616              mov       [esi+16h],ax
  0045d787  66894e14              mov       [esi+14h],cx
  0045d78b  e8b0990e00            call      00547140h
  0045d790  99                    cdq
  0045d791  b9a4060000            mov       ecx,6A4h
  0045d796  66899e9a000000        mov       [esi+9Ah],bx
  0045d79d  f7f9                  idiv      ecx
  0045d79f  668b4614              mov       ax,[esi+14h]
  0045d7a3  0fbf4e32              movsx     ecx,word [esi+32h]
  0045d7a7  894e44                mov       [esi+44h],ecx
  0045d7aa  662bc2                sub       ax,dx
  0045d7ad  66894634              mov       [esi+34h],ax

LAB_0045d7b1:
  0045d7b1  0fbf5616              movsx     edx,word [esi+16h]
  0045d7b5  52                    push      edx
  0045d7b6  e861820e00            call      00545A1Ch
  0045d7bb  6683461640            add       word [esi+16h],40h
  0045d7c0  c1f803                sar       eax,3
  0045d7c3  66034644              add       ax,[esi+44h]
  0045d7c7  66894632              mov       [esi+32h],ax
  0045d7cb  668b4616              mov       ax,[esi+16h]
  0045d7cf  663d0010              cmp       ax,1000h
  0045d7d3  7e09                  jle       short 0045D7DEh
  0045d7d5  0500f0ffff            add       eax,0FFFFF000h
  0045d7da  66894616              mov       [esi+16h],ax

LAB_0045d7de:
  0045d7de  bf01000000            mov       edi,1
  0045d7e3  66397e10              cmp       [esi+10h],di
  0045d7e7  0f851f020000          jne       near 0045DA0Ch
  0045d7ed  53                    push      ebx
  0045d7ee  53                    push      ebx
  0045d7ef  53                    push      ebx
  0045d7f0  6a0a                  push      0Ah
  0045d7f2  56                    push      esi
  0045d7f3  e848020000            call      0045DA40h
  0045d7f8  83c414                add       esp,14h
  0045d7fb  66399e9a000000        cmp       [esi+9Ah],bx
  0045d802  751e                  jne       short 0045D822h
  0045d804  53                    push      ebx
  0045d805  53                    push      ebx
  0045d806  6800100000            push      1000h
  0045d80b  6820030000            push      320h
  0045d810  6a02                  push      2
  0045d812  56                    push      esi
  0045d813  e868a80000            call      00468080h
  0045d818  83c418                add       esp,18h
  0045d81b  6689be9a000000        mov       [esi+9Ah],di

LAB_0045d822:
  0045d822  668b4612              mov       ax,[esi+12h]
  0045d826  66294634              sub       [esi+34h],ax
  0045d82a  83c00f                add       eax,0Fh
  0045d82d  0fbf4e34              movsx     ecx,word [esi+34h]
  0045d831  66894612              mov       [esi+12h],ax
  0045d835  0fbf4614              movsx     eax,word [esi+14h]
  0045d839  2de8030000            sub       eax,3E8h
  0045d83e  3bc8                  cmp       ecx,eax
  0045d840  0f8fd6000000          jg        near 0045D91Ch
  0045d846  bbc0935e00            mov       ebx,5E93C0h
  0045d84b  c744241804000000      mov       dword [esp+18h],4

LAB_0045d853:
  0045d853  8b03                  mov       eax,[ebx]
  0045d855  a900000080            test      eax,80000000h
  0045d85a  0f84a7000000          je        near 0045D907h
  0045d860  a900000050            test      eax,50000000h
  0045d865  0f859c000000          jne       near 0045D907h
  0045d86b  668b6e32              mov       bp,[esi+32h]
  0045d86f  8d8500fcffff          lea       eax,[ebp-400h]
  0045d875  0fbfd0                movsx     edx,ax
  0045d878  52                    push      edx
  0045d879  66894632              mov       [esi+32h],ax
  0045d87d  e89a810e00            call      00545A1Ch
  0045d882  8b4e1c                mov       ecx,[esi+1Ch]
  0045d885  8d0440                lea       eax,[eax+eax*2]
  0045d888  0fbf5632              movsx     edx,word [esi+32h]
  0045d88c  8d0480                lea       eax,[eax+eax*4]
  0045d88f  52                    push      edx
  0045d890  8d0480                lea       eax,[eax+eax*4]
  0045d893  8d3c41                lea       edi,[ecx+eax*2]
  0045d896  c1ff0c                sar       edi,0Ch
  0045d899  e878810e00            call      00545A16h
  0045d89e  8b4e24                mov       ecx,[esi+24h]
  0045d8a1  8d0440                lea       eax,[eax+eax*2]
  0045d8a4  66896e32              mov       [esi+32h],bp
  0045d8a8  8b531c                mov       edx,[ebx+1Ch]
  0045d8ab  8d0480                lea       eax,[eax+eax*4]
  0045d8ae  c1fa0c                sar       edx,0Ch
  0045d8b1  8d0480                lea       eax,[eax+eax*4]
  0045d8b4  2bfa                  sub       edi,edx
  0045d8b6  8bd7                  mov       edx,edi
  0045d8b8  8d0441                lea       eax,[ecx+eax*2]
  0045d8bb  8b4b24                mov       ecx,[ebx+24h]
  0045d8be  c1f80c                sar       eax,0Ch
  0045d8c1  0fafd7                imul      edx,edi
  0045d8c4  c1f90c                sar       ecx,0Ch
  0045d8c7  2bc1                  sub       eax,ecx
  0045d8c9  8bc8                  mov       ecx,eax
  0045d8cb  0fafc8                imul      ecx,eax
  0045d8ce  03d1                  add       edx,ecx
  0045d8d0  52                    push      edx
  0045d8d1  e83a810e00            call      00545A10h
  0045d8d6  83f832                cmp       eax,32h
  0045d8d9  7d2c                  jge       short 0045D907h
  0045d8db  6683bb9600000000      cmp       word [ebx+96h],0
  0045d8e3  7522                  jne       short 0045D907h
  0045d8e5  8b03                  mov       eax,[ebx]
  0045d8e7  6a09                  push      9
  0045d8e9  0c40                  or        al,40h
  0045d8eb  6a0a                  push      0Ah
  0045d8ed  53                    push      ebx
  0045d8ee  8903                  mov       [ebx],eax
  0045d8f0  e8bbbe0d00            call      005397B0h
  0045d8f5  8b03                  mov       eax,[ebx]
  0045d8f7  83c40c                add       esp,0Ch
  0045d8fa  0c40                  or        al,40h
  0045d8fc  8903                  mov       [ebx],eax
  0045d8fe  66c783960000003c00    mov       word [ebx+96h],3Ch

LAB_0045d907:
  0045d907  8b442418              mov       eax,[esp+18h]
  0045d90b  81c39c000000          add       ebx,9Ch
  0045d911  48                    dec       eax
  0045d912  89442418              mov       [esp+18h],eax
  0045d916  0f8537ffffff          jne       near 0045D853h

LAB_0045d91c:
  0045d91c  0fbf5614              movsx     edx,word [esi+14h]
  0045d920  0fbf4634              movsx     eax,word [esi+34h]
  0045d924  81ea00070000          sub       edx,700h
  0045d92a  3bc2                  cmp       eax,edx
  0045d92c  0f8fda000000          jg        near 0045DA0Ch
  0045d932  8b0dcc975e00          mov       ecx,[5E97CCh]
  0045d938  6a00                  push      0
  0045d93a  6a00                  push      0
  0045d93c  6800100000            push      1000h
  0045d941  8b91a8000000          mov       edx,[ecx+0A8h]
  0045d947  6800040000            push      400h
  0045d94c  8b02                  mov       eax,[edx]
  0045d94e  660fb64826            movzx     cx,byte [eax+26h]
  0045d953  51                    push      ecx
  0045d954  56                    push      esi
  0045d955  e826a70000            call      00468080h
  0045d95a  668b5614              mov       dx,[esi+14h]
  0045d95e  8b4620                mov       eax,[esi+20h]
  0045d961  668b7e32              mov       di,[esi+32h]
  0045d965  8b4e24                mov       ecx,[esi+24h]
  0045d968  6681ea0007            sub       dx,700h
  0045d96d  8b6e1c                mov       ebp,[esi+1Ch]
  0045d970  89442430              mov       [esp+30h],eax
  0045d974  8d8700fcffff          lea       eax,[edi-400h]
  0045d97a  66895634              mov       [esi+34h],dx
  0045d97e  83c418                add       esp,18h
  0045d981  0fbfd0                movsx     edx,ax
  0045d984  52                    push      edx
  0045d985  66c746100200          mov       word [esi+10h],2
  0045d98b  894c2414              mov       [esp+14h],ecx
  0045d98f  66894632              mov       [esi+32h],ax
  0045d993  e884800e00            call      00545A1Ch
  0045d998  0fbf4e32              movsx     ecx,word [esi+32h]
  0045d99c  8d0440                lea       eax,[eax+eax*2]
  0045d99f  8b5e1c                mov       ebx,[esi+1Ch]
  0045d9a2  51                    push      ecx
  0045d9a3  8d0480                lea       eax,[eax+eax*4]
  0045d9a6  8d0480                lea       eax,[eax+eax*4]
  0045d9a9  d1e0                  shl       eax,1
  0045d9ab  03d8                  add       ebx,eax
  0045d9ad  895e1c                mov       [esi+1Ch],ebx
  0045d9b0  e861800e00            call      00545A16h
  0045d9b5  8d0440                lea       eax,[eax+eax*2]
  0045d9b8  8b5e24                mov       ebx,[esi+24h]
  0045d9bb  8d0480                lea       eax,[eax+eax*4]
  0045d9be  8d1480                lea       edx,[eax+eax*4]
  0045d9c1  d1e2                  shl       edx,1
  0045d9c3  03da                  add       ebx,edx
  0045d9c5  895e24                mov       [esi+24h],ebx
  0045d9c8  e873970e00            call      00547140h
  0045d9cd  99                    cdq
  0045d9ce  b906000000            mov       ecx,6
  0045d9d3  f7f9                  idiv      ecx
  0045d9d5  83c203                add       edx,3
  0045d9d8  6685d2                test      dx,dx
  0045d9db  7c1a                  jl        short 0045D9F7h
  0045d9dd  0fbfda                movsx     ebx,dx
  0045d9e0  43                    inc       ebx

LAB_0045d9e1:
  0045d9e1  6a00                  push      0
  0045d9e3  6a00                  push      0
  0045d9e5  6aff                  push      0FFFFFFFFh
  0045d9e7  6a00                  push      0
  0045d9e9  6a3a                  push      3Ah
  0045d9eb  56                    push      esi
  0045d9ec  e81fa70000            call      00468110h
  0045d9f1  83c418                add       esp,18h
  0045d9f4  4b                    dec       ebx
  0045d9f5  75ea                  jne       short 0045D9E1h

LAB_0045d9f7:
  0045d9f7  8b542418              mov       edx,[esp+18h]
  0045d9fb  8b442410              mov       eax,[esp+10h]
  0045d9ff  896e1c                mov       [esi+1Ch],ebp
  0045da02  895620                mov       [esi+20h],edx
  0045da05  894624                mov       [esi+24h],eax
  0045da08  66897e32              mov       [esi+32h],di

LAB_0045da0c:
  0045da0c  66837e1002            cmp       word [esi+10h],2
  0045da11  7524                  jne       short 0045DA37h
  0045da13  668346343c            add       word [esi+34h],3Ch
  0045da18  668b4634              mov       ax,[esi+34h]
  0045da1c  663b4614              cmp       ax,[esi+14h]
  0045da20  7e15                  jle       short 0045DA37h
  0045da22  66c746100100          mov       word [esi+10h],1
  0045da28  66c746126400          mov       word [esi+12h],64h
  0045da2e  66c7869a0000000000    mov       word [esi+9Ah],0

LAB_0045da37:
  0045da37  5f                    pop       edi
  0045da38  5e                    pop       esi
  0045da39  5d                    pop       ebp
  0045da3a  5b                    pop       ebx
  0045da3b  59                    pop       ecx
  0045da3c  c3                    ret
  0045da3d  90                    nop
  0045da3e  90                    nop
  0045da3f  90                    nop

10 cible(s) de saut interne : 0x0045d7b1, 0x0045d7de, 0x0045d822, 0x0045d853, 0x0045d907, 0x0045d91c, 0x0045d9e1, 0x0045d9f7, 0x0045da0c, 0x0045da37
