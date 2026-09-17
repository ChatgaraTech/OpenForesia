C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041b460  section .text  offset fichier 0x1a860  864 octets

  0041b460  81eca4000000          sub       esp,0A4h
  0041b466  53                    push      ebx
  0041b467  55                    push      ebp
  0041b468  56                    push      esi
  0041b469  57                    push      edi
  0041b46a  b926000000            mov       ecx,26h
  0041b46f  33c0                  xor       eax,eax
  0041b471  8d7c241c              lea       edi,[esp+1Ch]
  0041b475  c744241800000000      mov       dword [esp+18h],0
  0041b47d  f3ab                  rep stosd
  0041b47f  8d7c2418              lea       edi,[esp+18h]
  0041b483  e8c8faffff            call      0041AF50h
  0041b488  e8b331ffff            call      0040E640h
  0041b48d  8bb424b8000000        mov       esi,[esp+0B8h]
  0041b494  b8dbca5e00            mov       eax,5ECADBh
  0041b499  bd20000000            mov       ebp,20h

LAB_0041b49e:
  0041b49e  8078d101              cmp       byte [eax-2Fh],1
  0041b4a2  7549                  jne       short 0041B4EDh
  0041b4a4  8b50c5                mov       edx,[eax-3Bh]
  0041b4a7  8d48c5                lea       ecx,[eax-3Bh]
  0041b4aa  f7c200000080          test      edx,80000000h
  0041b4b0  743b                  je        short 0041B4EDh
  0041b4b2  8b50e1                mov       edx,[eax-1Fh]
  0041b4b5  8bf9                  mov       edi,ecx
  0041b4b7  89561c                mov       [esi+1Ch],edx
  0041b4ba  8b50e5                mov       edx,[eax-1Bh]
  0041b4bd  895620                mov       [esi+20h],edx
  0041b4c0  8b50e9                mov       edx,[eax-17h]
  0041b4c3  895624                mov       [esi+24h],edx
  0041b4c6  33d2                  xor       edx,edx
  0041b4c8  8a50c9                mov       dl,[eax-37h]
  0041b4cb  8a18                  mov       bl,[eax]
  0041b4cd  66c740dd0200          mov       word [eax-23h],2
  0041b4d3  80cb02                or        bl,2
  0041b4d6  8b149540965e00        mov       edx,[edx*4+5E9640h]
  0041b4dd  8b5208                mov       edx,[edx+8]
  0041b4e0  8818                  mov       [eax],bl
  0041b4e2  89542410              mov       [esp+10h],edx
  0041b4e6  8b11                  mov       edx,[ecx]
  0041b4e8  80ce10                or        dh,10h
  0041b4eb  8911                  mov       [ecx],edx

LAB_0041b4ed:
  0041b4ed  059c000000            add       eax,9Ch
  0041b4f2  4d                    dec       ebp
  0041b4f3  75a9                  jne       short 0041B49Eh
  0041b4f5  66837e1000            cmp       word [esi+10h],0
  0041b4fa  bb01000000            mov       ebx,1
  0041b4ff  7554                  jne       short 0041B555h
  0041b501  33ed                  xor       ebp,ebp
  0041b503  66895e10              mov       [esi+10h],bx
  0041b507  66896e12              mov       [esi+12h],bp
  0041b50b  66896e14              mov       [esi+14h],bp
  0041b50f  66896e16              mov       [esi+16h],bp
  0041b513  0fbf05b8925e00        movsx     eax,word [5E92B8h]
  0041b51a  894650                mov       [esi+50h],eax
  0041b51d  0fbf0dba925e00        movsx     ecx,word [5E92BAh]
  0041b524  894e54                mov       [esi+54h],ecx
  0041b527  0fbf15bc925e00        movsx     edx,word [5E92BCh]
  0041b52e  895658                mov       [esi+58h],edx
  0041b531  8a4709                mov       al,[edi+9]
  0041b534  66896f1a              mov       [edi+1Ah],bp
  0041b538  c6470a00              mov       byte [edi+0Ah],0
  0041b53c  c74738ffffff00        mov       dword [edi+38h],0FFFFFFh
  0041b543  66895f36              mov       [edi+36h],bx
  0041b547  a2a8145700            mov       [5714A8h],al
  0041b54c  6689ae98000000        mov       [esi+98h],bp
  0041b553  eb02                  jmp       short 0041B557h

LAB_0041b555:
  0041b555  33ed                  xor       ebp,ebp

LAB_0041b557:
  0041b557  6639ae98000000        cmp       [esi+98h],bp
  0041b55e  7519                  jne       short 0041B579h
  0041b560  68000a0000            push      0A00h
  0041b565  68000a0000            push      0A00h
  0041b56a  6855020000            push      255h
  0041b56f  6a20                  push      20h
  0041b571  e82a191200            call      0053CEA0h
  0041b576  83c410                add       esp,10h

LAB_0041b579:
  0041b579  6683be980000000a      cmp       word [esi+98h],0Ah
  0041b581  7519                  jne       short 0041B59Ch
  0041b583  68000a0000            push      0A00h
  0041b588  68000a0000            push      0A00h
  0041b58d  68aa020000            push      2AAh
  0041b592  6a20                  push      20h
  0041b594  e807191200            call      0053CEA0h
  0041b599  83c410                add       esp,10h

LAB_0041b59c:
  0041b59c  6683be9800000014      cmp       word [esi+98h],14h
  0041b5a4  7519                  jne       short 0041B5BFh
  0041b5a6  68000a0000            push      0A00h
  0041b5ab  68000a0000            push      0A00h
  0041b5b0  68d4020000            push      2D4h
  0041b5b5  6a20                  push      20h
  0041b5b7  e8e4181200            call      0053CEA0h
  0041b5bc  83c410                add       esp,10h

LAB_0041b5bf:
  0041b5bf  6683be980000001e      cmp       word [esi+98h],1Eh
  0041b5c7  7519                  jne       short 0041B5E2h
  0041b5c9  68000a0000            push      0A00h
  0041b5ce  68000a0000            push      0A00h
  0041b5d3  6829030000            push      329h
  0041b5d8  6a20                  push      20h
  0041b5da  e8c1181200            call      0053CEA0h
  0041b5df  83c410                add       esp,10h

LAB_0041b5e2:
  0041b5e2  6683be9800000028      cmp       word [esi+98h],28h
  0041b5ea  7519                  jne       short 0041B605h
  0041b5ec  68000a0000            push      0A00h
  0041b5f1  68000a0000            push      0A00h
  0041b5f6  687e030000            push      37Eh
  0041b5fb  6a20                  push      20h
  0041b5fd  e89e181200            call      0053CEA0h
  0041b602  83c410                add       esp,10h

LAB_0041b605:
  0041b605  66ff8698000000        inc       word [esi+98h]
  0041b60c  6683be9800000032      cmp       word [esi+98h],32h
  0041b614  7507                  jne       short 0041B61Dh
  0041b616  6689ae98000000        mov       [esi+98h],bp

LAB_0041b61d:
  0041b61d  66395e10              cmp       [esi+10h],bx
  0041b621  0f8589010000          jne       near 0041B7B0h
  0041b627  e814bb1200            call      00547140h
  0041b62c  99                    cdq
  0041b62d  b93c000000            mov       ecx,3Ch
  0041b632  66891d66915e00        mov       [5E9166h],bx
  0041b639  f7f9                  idiv      ecx
  0041b63b  b896000000            mov       eax,96h
  0041b640  66a362915e00          mov       [5E9162h],ax
  0041b646  66a364915e00          mov       [5E9164h],ax
  0041b64c  81c2b4000000          add       edx,0B4h
  0041b652  66891560915e00        mov       [5E9160h],dx
  0041b659  66ff4612              inc       word [esi+12h]
  0041b65d  668b4612              mov       ax,[esi+12h]
  0041b661  66ff4616              inc       word [esi+16h]
  0041b665  668b4e16              mov       cx,[esi+16h]
  0041b669  663bc3                cmp       ax,bx
  0041b66c  7c6c                  jl        short 0041B6DAh
  0041b66e  6683f950              cmp       cx,50h
  0041b672  7d66                  jge       short 0041B6DAh
  0041b674  66ff4614              inc       word [esi+14h]
  0041b678  668b5e14              mov       bx,[esi+14h]
  0041b67c  66896e12              mov       [esi+12h],bp
  0041b680  e8bbba1200            call      00547140h
  0041b685  99                    cdq
  0041b686  33c2                  xor       eax,edx
  0041b688  6aff                  push      0FFFFFFFFh
  0041b68a  2bc2                  sub       eax,edx
  0041b68c  66896e14              mov       [esi+14h],bp
  0041b690  83e003                and       eax,3
  0041b693  6a00                  push      0
  0041b695  33c2                  xor       eax,edx
  0041b697  8d6e1c                lea       ebp,[esi+1Ch]
  0041b69a  68fa000000            push      0FAh
  0041b69f  6a00                  push      0
  0041b6a1  2bc2                  sub       eax,edx
  0041b6a3  55                    push      ebp
  0041b6a4  56                    push      esi
  0041b6a5  8944242c              mov       [esp+2Ch],eax
  0041b6a9  e892f50500            call      0047AC40h
  0041b6ae  668b4616              mov       ax,[esi+16h]
  0041b6b2  83c418                add       esp,18h
  0041b6b5  a801                  test      al,1
  0041b6b7  66895e14              mov       [esi+14h],bx
  0041b6bb  751b                  jne       short 0041B6D8h
  0041b6bd  663d4600              cmp       ax,46h
  0041b6c1  7d15                  jge       short 0041B6D8h
  0041b6c3  6aff                  push      0FFFFFFFFh
  0041b6c5  6a00                  push      0
  0041b6c7  68c2000000            push      0C2h
  0041b6cc  6a00                  push      0
  0041b6ce  55                    push      ebp
  0041b6cf  56                    push      esi
  0041b6d0  e86bf50500            call      0047AC40h
  0041b6d5  83c418                add       esp,18h

LAB_0041b6d8:
  0041b6d8  33ed                  xor       ebp,ebp

LAB_0041b6da:
  0041b6da  66837e1664            cmp       word [esi+16h],64h
  0041b6df  7e29                  jle       short 0041B70Ah
  0041b6e1  668b5650              mov       dx,[esi+50h]
  0041b6e5  66891560915e00        mov       [5E9160h],dx
  0041b6ec  668b4654              mov       ax,[esi+54h]
  0041b6f0  66a362915e00          mov       [5E9162h],ax
  0041b6f6  668b4e58              mov       cx,[esi+58h]
  0041b6fa  66890d64915e00        mov       [5E9164h],cx
  0041b701  66c70566915e000200    mov       word [5E9166h],2

LAB_0041b70a:
  0041b70a  66817e168c00          cmp       word [esi+16h],8Ch
  0041b710  7c3e                  jl        short 0041B750h
  0041b712  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0041b718  e893800e00            call      005037B0h
  0041b71d  a1a8145700            mov       eax,[5714A8h]
  0041b722  8b0dcc975e00          mov       ecx,[5E97CCh]
  0041b728  25ff000000            and       eax,0FFh
  0041b72d  55                    push      ebp
  0041b72e  8d1480                lea       edx,[eax+eax*4]
  0041b731  c1e203                shl       edx,3
  0041b734  2bd0                  sub       edx,eax
  0041b736  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  0041b73d  50                    push      eax
  0041b73e  51                    push      ecx
  0041b73f  e88c860f00            call      00513DD0h
  0041b744  8b07                  mov       eax,[edi]
  0041b746  83c40c                add       esp,0Ch
  0041b749  0d00400002            or        eax,2004000h
  0041b74e  8907                  mov       [edi],eax

LAB_0041b750:
  0041b750  f6461601              test      byte [esi+16h],1
  0041b754  7524                  jne       short 0041B77Ah
  0041b756  8b542410              mov       edx,[esp+10h]
  0041b75a  66ff471a              inc       word [edi+1Ah]
  0041b75e  668b4f1a              mov       cx,[edi+1Ah]
  0041b762  668b4216              mov       ax,[edx+16h]
  0041b766  0fbfc9                movsx     ecx,cx
  0041b769  8bd0                  mov       edx,eax
  0041b76b  81e2ffff0000          and       edx,0FFFFh
  0041b771  3bca                  cmp       ecx,edx
  0041b773  7c05                  jl        short 0041B77Ah
  0041b775  48                    dec       eax
  0041b776  6689471a              mov       [edi+1Ah],ax

LAB_0041b77a:
  0041b77a  660fb64738            movzx     ax,byte [edi+38h]
  0041b77f  660fb65739            movzx     dx,byte [edi+39h]
  0041b784  660fb64f3a            movzx     cx,byte [edi+3Ah]
  0041b789  83e802                sub       eax,2
  0041b78c  83ea04                sub       edx,4
  0041b78f  83e904                sub       ecx,4
  0041b792  663bc5                cmp       ax,bp
  0041b795  7d02                  jge       short 0041B799h
  0041b797  33c0                  xor       eax,eax

LAB_0041b799:
  0041b799  663bd5                cmp       dx,bp
  0041b79c  7d02                  jge       short 0041B7A0h
  0041b79e  33d2                  xor       edx,edx

LAB_0041b7a0:
  0041b7a0  663bcd                cmp       cx,bp
  0041b7a3  7d02                  jge       short 0041B7A7h
  0041b7a5  33c9                  xor       ecx,ecx

LAB_0041b7a7:
  0041b7a7  884738                mov       [edi+38h],al
  0041b7aa  885739                mov       [edi+39h],dl
  0041b7ad  884f3a                mov       [edi+3Ah],cl

LAB_0041b7b0:
  0041b7b0  5f                    pop       edi
  0041b7b1  5e                    pop       esi
  0041b7b2  5d                    pop       ebp
  0041b7b3  5b                    pop       ebx
  0041b7b4  81c4a4000000          add       esp,0A4h
  0041b7ba  c3                    ret
  0041b7bb  90                    nop
  0041b7bc  90                    nop
  0041b7bd  90                    nop
  0041b7be  90                    nop
  0041b7bf  90                    nop

19 cible(s) de saut interne : 0x0041b49e, 0x0041b4ed, 0x0041b555, 0x0041b557, 0x0041b579, 0x0041b59c, 0x0041b5bf, 0x0041b5e2, 0x0041b605, 0x0041b61d, 0x0041b6d8, 0x0041b6da, 0x0041b70a, 0x0041b750, 0x0041b77a, 0x0041b799, 0x0041b7a0, 0x0041b7a7, 0x0041b7b0
