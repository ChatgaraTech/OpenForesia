C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045b540  section .text  offset fichier 0x5a940  384 octets

  0045b540  53                    push      ebx
  0045b541  56                    push      esi
  0045b542  8b74240c              mov       esi,[esp+0Ch]
  0045b546  57                    push      edi
  0045b547  56                    push      esi
  0045b548  e8735a0000            call      00460FC0h
  0045b54d  8b06                  mov       eax,[esi]
  0045b54f  83c404                add       esp,4
  0045b552  a900000040            test      eax,40000000h
  0045b557  0f8552010000          jne       near 0045B6AFh
  0045b55d  66837e1000            cmp       word [esi+10h],0
  0045b562  bb01000000            mov       ebx,1
  0045b567  750a                  jne       short 0045B573h
  0045b569  66895e10              mov       [esi+10h],bx
  0045b56d  66c746120000          mov       word [esi+12h],0

LAB_0045b573:
  0045b573  66395e10              cmp       [esi+10h],bx
  0045b577  0f85e3000000          jne       near 0045B660h
  0045b57d  66ff4612              inc       word [esi+12h]
  0045b581  66395e14              cmp       [esi+14h],bx
  0045b585  668b4612              mov       ax,[esi+12h]
  0045b589  0f85be000000          jne       near 0045B64Dh
  0045b58f  663d1d00              cmp       ax,1Dh
  0045b593  0f8eb4000000          jle       near 0045B64Dh
  0045b599  6a00                  push      0
  0045b59b  6a00                  push      0
  0045b59d  6800100000            push      1000h
  0045b5a2  6800040000            push      400h
  0045b5a7  6a0b                  push      0Bh
  0045b5a9  56                    push      esi
  0045b5aa  e8d1ca0000            call      00468080h
  0045b5af  668b4612              mov       ax,[esi+12h]
  0045b5b3  83c418                add       esp,18h
  0045b5b6  6689442410            mov       [esp+10h],ax
  0045b5bb  a0f4965e00            mov       al,[5E96F4h]
  0045b5c0  a804                  test      al,4
  0045b5c2  7439                  je        short 0045B5FDh
  0045b5c4  841dfe965e00          test      [5E96FEh],bl
  0045b5ca  7406                  je        short 0045B5D2h
  0045b5cc  66c746120800          mov       word [esi+12h],8

LAB_0045b5d2:
  0045b5d2  f605fe965e0002        test      byte [5E96FEh],2
  0045b5d9  7406                  je        short 0045B5E1h
  0045b5db  66c746120200          mov       word [esi+12h],2

LAB_0045b5e1:
  0045b5e1  bf15000000            mov       edi,15h

LAB_0045b5e6:
  0045b5e6  6a00                  push      0
  0045b5e8  6a00                  push      0
  0045b5ea  6aff                  push      0FFFFFFFFh
  0045b5ec  53                    push      ebx
  0045b5ed  6a31                  push      31h
  0045b5ef  56                    push      esi
  0045b5f0  e81bcb0000            call      00468110h
  0045b5f5  83c418                add       esp,18h
  0045b5f8  4f                    dec       edi
  0045b5f9  75eb                  jne       short 0045B5E6h
  0045b5fb  eb47                  jmp       short 0045B644h

LAB_0045b5fd:
  0045b5fd  66c746120800          mov       word [esi+12h],8
  0045b603  bf15000000            mov       edi,15h

LAB_0045b608:
  0045b608  6a00                  push      0
  0045b60a  6a00                  push      0
  0045b60c  6aff                  push      0FFFFFFFFh
  0045b60e  53                    push      ebx
  0045b60f  6a31                  push      31h
  0045b611  56                    push      esi
  0045b612  e8f9ca0000            call      00468110h
  0045b617  83c418                add       esp,18h
  0045b61a  4f                    dec       edi
  0045b61b  75eb                  jne       short 0045B608h
  0045b61d  66c746120200          mov       word [esi+12h],2
  0045b623  bf15000000            mov       edi,15h

LAB_0045b628:
  0045b628  6a00                  push      0
  0045b62a  6a00                  push      0
  0045b62c  6aff                  push      0FFFFFFFFh
  0045b62e  53                    push      ebx
  0045b62f  6a31                  push      31h
  0045b631  56                    push      esi
  0045b632  e8d9ca0000            call      00468110h
  0045b637  83c418                add       esp,18h
  0045b63a  4f                    dec       edi
  0045b63b  75eb                  jne       short 0045B628h
  0045b63d  800df4965e0004        or        byte [5E96F4h],4

LAB_0045b644:
  0045b644  668b4c2410            mov       cx,[esp+10h]
  0045b649  66894e12              mov       [esi+12h],cx

LAB_0045b64d:
  0045b64d  66837e121e            cmp       word [esi+12h],1Eh
  0045b652  750c                  jne       short 0045B660h
  0045b654  66c746100200          mov       word [esi+10h],2
  0045b65a  66c746120000          mov       word [esi+12h],0

LAB_0045b660:
  0045b660  b802000000            mov       eax,2
  0045b665  66394610              cmp       [esi+10h],ax
  0045b669  7544                  jne       short 0045B6AFh
  0045b66b  66014612              add       [esi+12h],ax
  0045b66f  8b4620                mov       eax,[esi+20h]
  0045b672  0fbf5612              movsx     edx,word [esi+12h]
  0045b676  c1e20c                shl       edx,0Ch
  0045b679  2bc2                  sub       eax,edx
  0045b67b  6a00                  push      0
  0045b67d  894620                mov       [esi+20h],eax
  0045b680  0fbf4616              movsx     eax,word [esi+16h]
  0045b684  f7d8                  neg       eax
  0045b686  c1e00c                shl       eax,0Ch
  0045b689  50                    push      eax
  0045b68a  6a00                  push      0
  0045b68c  56                    push      esi
  0045b68d  e8ee8effff            call      00454580h
  0045b692  8b4620                mov       eax,[esi+20h]
  0045b695  6683463414            add       word [esi+34h],14h
  0045b69a  6683463204            add       word [esi+32h],4
  0045b69f  83c410                add       esp,10h
  0045b6a2  3d000006ff            cmp       eax,0FF060000h
  0045b6a7  7d06                  jge       short 0045B6AFh
  0045b6a9  810e00200040          or        dword [esi],40002000h

LAB_0045b6af:
  0045b6af  5f                    pop       edi
  0045b6b0  5e                    pop       esi
  0045b6b1  5b                    pop       ebx
  0045b6b2  c3                    ret
  0045b6b3  90                    nop
  0045b6b4  90                    nop
  0045b6b5  90                    nop
  0045b6b6  90                    nop
  0045b6b7  90                    nop
  0045b6b8  90                    nop
  0045b6b9  90                    nop
  0045b6ba  90                    nop
  0045b6bb  90                    nop
  0045b6bc  90                    nop
  0045b6bd  90                    nop
  0045b6be  90                    nop
  0045b6bf  90                    nop

11 cible(s) de saut interne : 0x0045b573, 0x0045b5d2, 0x0045b5e1, 0x0045b5e6, 0x0045b5fd, 0x0045b608, 0x0045b628, 0x0045b644, 0x0045b64d, 0x0045b660, 0x0045b6af
