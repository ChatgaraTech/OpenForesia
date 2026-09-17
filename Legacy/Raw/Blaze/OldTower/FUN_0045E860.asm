C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045e860  section .text  offset fichier 0x5dc60  592 octets

  0045e860  51                    push      ecx
  0045e861  53                    push      ebx
  0045e862  55                    push      ebp
  0045e863  56                    push      esi
  0045e864  8b742414              mov       esi,[esp+14h]
  0045e868  57                    push      edi
  0045e869  83cfff                or        edi,0FFFFFFFFh
  0045e86c  33d2                  xor       edx,edx
  0045e86e  b9a0ca5e00            mov       ecx,5ECAA0h
  0045e873  c744241020000000      mov       dword [esp+10h],20h
  0045e87b  bd64000000            mov       ebp,64h
  0045e880  bb78000000            mov       ebx,78h

LAB_0045e885:
  0045e885  8b01                  mov       eax,[ecx]
  0045e887  a900000080            test      eax,80000000h
  0045e88c  7434                  je        short 0045E8C2h
  0045e88e  80790c04              cmp       byte [ecx+0Ch],4
  0045e892  752e                  jne       short 0045E8C2h
  0045e894  a900000040            test      eax,40000000h
  0045e899  7527                  jne       short 0045E8C2h
  0045e89b  80cc40                or        ah,40h
  0045e89e  66c70560915e00fa00    mov       word [5E9160h],0FAh
  0045e8a7  8901                  mov       [ecx],eax
  0045e8a9  66892d62915e00        mov       [5E9162h],bp
  0045e8b0  66892d64915e00        mov       [5E9164h],bp
  0045e8b7  66c70566915e000200    mov       word [5E9166h],2
  0045e8c0  8bfa                  mov       edi,edx

LAB_0045e8c2:
  0045e8c2  8b01                  mov       eax,[ecx]
  0045e8c4  a900000080            test      eax,80000000h
  0045e8c9  7438                  je        short 0045E903h
  0045e8cb  80790c04              cmp       byte [ecx+0Ch],4
  0045e8cf  7532                  jne       short 0045E903h
  0045e8d1  a900000040            test      eax,40000000h
  0045e8d6  742b                  je        short 0045E903h
  0045e8d8  66891d60915e00        mov       [5E9160h],bx
  0045e8df  66891d62915e00        mov       [5E9162h],bx
  0045e8e6  66891d64915e00        mov       [5E9164h],bx
  0045e8ed  66c70566915e000200    mov       word [5E9166h],2
  0045e8f6  66837e1600            cmp       word [esi+16h],0
  0045e8fb  7506                  jne       short 0045E903h
  0045e8fd  66c746160100          mov       word [esi+16h],1

LAB_0045e903:
  0045e903  8b442410              mov       eax,[esp+10h]
  0045e907  42                    inc       edx
  0045e908  81c19c000000          add       ecx,9Ch
  0045e90e  48                    dec       eax
  0045e90f  89442410              mov       [esp+10h],eax
  0045e913  0f856cffffff          jne       near 0045E885h
  0045e919  83ffff                cmp       edi,0FFFFFFFFh
  0045e91c  b9a0ca5e00            mov       ecx,5ECAA0h
  0045e921  ba20000000            mov       edx,20h
  0045e926  7426                  je        short 0045E94Eh

LAB_0045e928:
  0045e928  8b01                  mov       eax,[ecx]
  0045e92a  a900000080            test      eax,80000000h
  0045e92f  7412                  je        short 0045E943h
  0045e931  80790c04              cmp       byte [ecx+0Ch],4
  0045e935  740c                  je        short 0045E943h
  0045e937  a900000040            test      eax,40000000h
  0045e93c  7505                  jne       short 0045E943h
  0045e93e  80cc20                or        ah,20h
  0045e941  8901                  mov       [ecx],eax

LAB_0045e943:
  0045e943  81c19c000000          add       ecx,9Ch
  0045e949  4a                    dec       edx
  0045e94a  75dc                  jne       short 0045E928h
  0045e94c  eb24                  jmp       short 0045E972h

LAB_0045e94e:
  0045e94e  8b01                  mov       eax,[ecx]
  0045e950  a900000080            test      eax,80000000h
  0045e955  7412                  je        short 0045E969h
  0045e957  80790c04              cmp       byte [ecx+0Ch],4
  0045e95b  740c                  je        short 0045E969h
  0045e95d  a900000040            test      eax,40000000h
  0045e962  7505                  jne       short 0045E969h
  0045e964  80e4df                and       ah,0DFh
  0045e967  8901                  mov       [ecx],eax

LAB_0045e969:
  0045e969  81c19c000000          add       ecx,9Ch
  0045e96f  4a                    dec       edx
  0045e970  75dc                  jne       short 0045E94Eh

LAB_0045e972:
  0045e972  bb01000000            mov       ebx,1
  0045e977  66395e16              cmp       [esi+16h],bx
  0045e97b  757e                  jne       short 0045E9FBh
  0045e97d  845e28                test      [esi+28h],bl
  0045e980  7533                  jne       short 0045E9B5h
  0045e982  a1cc975e00            mov       eax,[5E97CCh]
  0045e987  33ff                  xor       edi,edi
  0045e989  57                    push      edi
  0045e98a  57                    push      edi
  0045e98b  8b88a8000000          mov       ecx,[eax+0A8h]
  0045e991  6800180000            push      1800h
  0045e996  6800040000            push      400h
  0045e99b  8b11                  mov       edx,[ecx]
  0045e99d  660fb6422f            movzx     ax,byte [edx+2Fh]
  0045e9a2  50                    push      eax
  0045e9a3  56                    push      esi
  0045e9a4  e8d7960000            call      00468080h
  0045e9a9  8b06                  mov       eax,[esi]
  0045e9ab  83c418                add       esp,18h
  0045e9ae  80cc40                or        ah,40h
  0045e9b1  8906                  mov       [esi],eax
  0045e9b3  eb09                  jmp       short 0045E9BEh

LAB_0045e9b5:
  0045e9b5  8b06                  mov       eax,[esi]
  0045e9b7  80e4bf                and       ah,0BFh
  0045e9ba  33ff                  xor       edi,edi
  0045e9bc  8906                  mov       [esi],eax

LAB_0045e9be:
  0045e9be  57                    push      edi
  0045e9bf  57                    push      edi
  0045e9c0  6aff                  push      0FFFFFFFFh
  0045e9c2  57                    push      edi
  0045e9c3  6a36                  push      36h
  0045e9c5  56                    push      esi
  0045e9c6  e845970000            call      00468110h
  0045e9cb  b937ffffff            mov       ecx,0FFFFFF37h
  0045e9d0  83c418                add       esp,18h
  0045e9d3  66014e28              add       [esi+28h],cx
  0045e9d7  668b4628              mov       ax,[esi+28h]
  0045e9db  66014e2c              add       [esi+2Ch],cx
  0045e9df  663bc7                cmp       ax,di
  0045e9e2  7d19                  jge       short 0045E9FDh
  0045e9e4  8b06                  mov       eax,[esi]
  0045e9e6  66897e28              mov       [esi+28h],di
  0045e9ea  80cc40                or        ah,40h
  0045e9ed  66897e2c              mov       [esi+2Ch],di
  0045e9f1  66c746160200          mov       word [esi+16h],2
  0045e9f7  8906                  mov       [esi],eax
  0045e9f9  eb02                  jmp       short 0045E9FDh

LAB_0045e9fb:
  0045e9fb  33ff                  xor       edi,edi

LAB_0045e9fd:
  0045e9fd  66397e10              cmp       [esi+10h],di
  0045ea01  7510                  jne       short 0045EA13h
  0045ea03  66895e10              mov       [esi+10h],bx
  0045ea07  66897e12              mov       [esi+12h],di
  0045ea0b  66897e14              mov       [esi+14h],di
  0045ea0f  66897e16              mov       [esi+16h],di

LAB_0045ea13:
  0045ea13  6683463244            add       word [esi+32h],44h
  0045ea18  668b4632              mov       ax,[esi+32h]
  0045ea1c  663d0010              cmp       ax,1000h
  0045ea20  7e09                  jle       short 0045EA2Bh
  0045ea22  0500f0ffff            add       eax,0FFFFF000h
  0045ea27  66894632              mov       [esi+32h],ax

LAB_0045ea2b:
  0045ea2b  0fbf4e32              movsx     ecx,word [esi+32h]
  0045ea2f  51                    push      ecx
  0045ea30  e8e76f0e00            call      00545A1Ch
  0045ea35  8a4e12                mov       cl,[esi+12h]
  0045ea38  d3e0                  shl       eax,cl
  0045ea3a  8b4e20                mov       ecx,[esi+20h]
  0045ea3d  03c8                  add       ecx,eax
  0045ea3f  66397e14              cmp       [esi+14h],di
  0045ea43  894e20                mov       [esi+20h],ecx
  0045ea46  745d                  je        short 0045EAA5h
  0045ea48  57                    push      edi
  0045ea49  57                    push      edi
  0045ea4a  6aff                  push      0FFFFFFFFh
  0045ea4c  57                    push      edi
  0045ea4d  6a36                  push      36h
  0045ea4f  56                    push      esi
  0045ea50  e8bb960000            call      00468110h
  0045ea55  83c418                add       esp,18h
  0045ea58  57                    push      edi
  0045ea59  57                    push      edi
  0045ea5a  6aff                  push      0FFFFFFFFh
  0045ea5c  57                    push      edi
  0045ea5d  6a36                  push      36h
  0045ea5f  56                    push      esi
  0045ea60  e8ab960000            call      00468110h
  0045ea65  83c418                add       esp,18h
  0045ea68  57                    push      edi
  0045ea69  57                    push      edi
  0045ea6a  6aff                  push      0FFFFFFFFh
  0045ea6c  57                    push      edi
  0045ea6d  6a36                  push      36h
  0045ea6f  56                    push      esi
  0045ea70  e89b960000            call      00468110h
  0045ea75  8b15cc975e00          mov       edx,[5E97CCh]
  0045ea7b  83c418                add       esp,18h
  0045ea7e  8b82a8000000          mov       eax,[edx+0A8h]
  0045ea84  57                    push      edi
  0045ea85  57                    push      edi
  0045ea86  6800100000            push      1000h
  0045ea8b  8b08                  mov       ecx,[eax]
  0045ea8d  6800040000            push      400h
  0045ea92  660fb6512a            movzx     dx,byte [ecx+2Ah]
  0045ea97  52                    push      edx
  0045ea98  56                    push      esi
  0045ea99  e8e2950000            call      00468080h
  0045ea9e  83c418                add       esp,18h
  0045eaa1  66897e14              mov       [esi+14h],di

LAB_0045eaa5:
  0045eaa5  5f                    pop       edi
  0045eaa6  5e                    pop       esi
  0045eaa7  5d                    pop       ebp
  0045eaa8  5b                    pop       ebx
  0045eaa9  59                    pop       ecx
  0045eaaa  c3                    ret
  0045eaab  90                    nop
  0045eaac  90                    nop
  0045eaad  90                    nop
  0045eaae  90                    nop
  0045eaaf  90                    nop

15 cible(s) de saut interne : 0x0045e885, 0x0045e8c2, 0x0045e903, 0x0045e928, 0x0045e943, 0x0045e94e, 0x0045e969, 0x0045e972, 0x0045e9b5, 0x0045e9be, 0x0045e9fb, 0x0045e9fd, 0x0045ea13, 0x0045ea2b, 0x0045eaa5
