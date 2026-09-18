C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004633c0  section .text  offset fichier 0x627c0  495 octets

  004633c0  a0fc965e00            mov       al,[5E96FCh]
  004633c5  53                    push      ebx
  004633c6  56                    push      esi
  004633c7  8b74240c              mov       esi,[esp+0Ch]
  004633cb  2408                  and       al,8
  004633cd  57                    push      edi
  004633ce  bb00000000            mov       ebx,0
  004633d3  7422                  je        short 004633F7h
  004633d5  66395e10              cmp       [esi+10h],bx
  004633d9  751c                  jne       short 004633F7h
  004633db  8b4e1c                mov       ecx,[esi+1Ch]
  004633de  66c746100100          mov       word [esi+10h],1
  004633e4  81c100c00100          add       ecx,1C000h
  004633ea  668146340002          add       word [esi+34h],200h
  004633f0  894e1c                mov       [esi+1Ch],ecx
  004633f3  5f                    pop       edi
  004633f4  5e                    pop       esi
  004633f5  5b                    pop       ebx
  004633f6  c3                    ret

LAB_004633f7:
  004633f7  3ac3                  cmp       al,bl
  004633f9  bf01000000            mov       edi,1
  004633fe  740a                  je        short 0046340Ah
  00463400  66397e10              cmp       [esi+10h],di
  00463404  0f84a1010000          je        near 004635ABh

LAB_0046340a:
  0046340a  381d65905e00          cmp       [5E9065h],bl
  00463410  0f8495010000          je        near 004635ABh
  00463416  66395e10              cmp       [esi+10h],bx
  0046341a  753d                  jne       short 00463459h
  0046341c  8b466c                mov       eax,[esi+6Ch]
  0046341f  3bc3                  cmp       eax,ebx
  00463421  7412                  je        short 00463435h
  00463423  f70000000100          test      dword [eax],10000h
  00463429  740a                  je        short 00463435h
  0046342b  810e00000040          or        dword [esi],40000000h
  00463431  5f                    pop       edi
  00463432  5e                    pop       esi
  00463433  5b                    pop       ebx
  00463434  c3                    ret

LAB_00463435:
  00463435  53                    push      ebx
  00463436  66897e10              mov       [esi+10h],di
  0046343a  66895e12              mov       [esi+12h],bx
  0046343e  66899e98000000        mov       [esi+98h],bx
  00463445  894674                mov       [esi+74h],eax
  00463448  e8b31e0d00            call      00535300h
  0046344d  8b06                  mov       eax,[esi]
  0046344f  83c404                add       esp,4
  00463452  25fffffffb            and       eax,0FBFFFFFFh
  00463457  8906                  mov       [esi],eax

LAB_00463459:
  00463459  66397e10              cmp       [esi+10h],di
  0046345d  756a                  jne       short 004634C9h
  0046345f  66ff8698000000        inc       word [esi+98h]
  00463466  bf05000000            mov       edi,5
  0046346b  6639be98000000        cmp       [esi+98h],di
  00463472  752f                  jne       short 004634A3h
  00463474  a1cc975e00            mov       eax,[5E97CCh]
  00463479  53                    push      ebx
  0046347a  53                    push      ebx
  0046347b  6800100000            push      1000h
  00463480  8b88a8000000          mov       ecx,[eax+0A8h]
  00463486  6800020000            push      200h
  0046348b  8b11                  mov       edx,[ecx]
  0046348d  660fb64226            movzx     ax,byte [edx+26h]
  00463492  50                    push      eax
  00463493  56                    push      esi
  00463494  e8e74b0000            call      00468080h
  00463499  83c418                add       esp,18h
  0046349c  66899e98000000        mov       [esi+98h],bx

LAB_004634a3:
  004634a3  8b561c                mov       edx,[esi+1Ch]
  004634a6  81c200200000          add       edx,2000h
  004634ac  66ff4612              inc       word [esi+12h]
  004634b0  66837e120e            cmp       word [esi+12h],0Eh
  004634b5  89561c                mov       [esi+1Ch],edx
  004634b8  7c14                  jl        short 004634CEh
  004634ba  66c746100200          mov       word [esi+10h],2
  004634c0  66895e12              mov       [esi+12h],bx
  004634c4  897e5c                mov       [esi+5Ch],edi
  004634c7  eb05                  jmp       short 004634CEh

LAB_004634c9:
  004634c9  bf05000000            mov       edi,5

LAB_004634ce:
  004634ce  66837e1002            cmp       word [esi+10h],2
  004634d3  0f85d2000000          jne       near 004635ABh
  004634d9  66ff8698000000        inc       word [esi+98h]
  004634e0  6639be98000000        cmp       [esi+98h],di
  004634e7  7530                  jne       short 00463519h
  004634e9  8b0dcc975e00          mov       ecx,[5E97CCh]
  004634ef  53                    push      ebx
  004634f0  53                    push      ebx
  004634f1  6800100000            push      1000h
  004634f6  8b91a8000000          mov       edx,[ecx+0A8h]
  004634fc  6800020000            push      200h
  00463501  8b02                  mov       eax,[edx]
  00463503  660fb64826            movzx     cx,byte [eax+26h]
  00463508  51                    push      ecx
  00463509  56                    push      esi
  0046350a  e8714b0000            call      00468080h
  0046350f  83c418                add       esp,18h
  00463512  66899e98000000        mov       [esi+98h],bx

LAB_00463519:
  00463519  668b465c              mov       ax,[esi+5Ch]
  0046351d  8b4e5c                mov       ecx,[esi+5Ch]
  00463520  66014634              add       [esi+34h],ax
  00463524  66014612              add       [esi+12h],ax
  00463528  03cf                  add       ecx,edi
  0046352a  66817e120002          cmp       word [esi+12h],200h
  00463530  894e5c                mov       [esi+5Ch],ecx
  00463533  7c76                  jl        short 004635ABh
  00463535  8b15cc975e00          mov       edx,[5E97CCh]
  0046353b  53                    push      ebx
  0046353c  53                    push      ebx
  0046353d  6800100000            push      1000h
  00463542  8b82a8000000          mov       eax,[edx+0A8h]
  00463548  6800010000            push      100h
  0046354d  8b08                  mov       ecx,[eax]
  0046354f  660fb65128            movzx     dx,byte [ecx+28h]
  00463554  52                    push      edx
  00463555  56                    push      esi
  00463556  e8254b0000            call      00468080h
  0046355b  66c746100300          mov       word [esi+10h],3
  00463561  8a15fc965e00          mov       dl,[5E96FCh]
  00463567  80ca08                or        dl,8
  0046356a  83c418                add       esp,18h
  0046356d  8815fc965e00          mov       [5E96FCh],dl
  00463573  e8881e0d00            call      00535400h
  00463578  8a0d40985e00          mov       cl,[5E9840h]
  0046357e  b020                  mov       al,20h
  00463580  84c8                  test      al,cl
  00463582  7527                  jne       short 004635ABh
  00463584  0ac8                  or        cl,al
  00463586  53                    push      ebx
  00463587  880d40985e00          mov       [5E9840h],cl
  0046358d  8b0dcc975e00          mov       ecx,[5E97CCh]
  00463593  8b4674                mov       eax,[esi+74h]
  00463596  68b0335600            push      5633B0h
  0046359b  8b5170                mov       edx,[ecx+70h]
  0046359e  50                    push      eax
  0046359f  8b4204                mov       eax,[edx+4]
  004635a2  50                    push      eax
  004635a3  e8a80d0e00            call      00544350h
  004635a8  83c410                add       esp,10h

LAB_004635ab:
  004635ab  5f                    pop       edi
  004635ac  5e                    pop       esi
  004635ad  5b                    pop       ebx
  004635ae  c3                    ret

9 cible(s) de saut interne : 0x004633f7, 0x0046340a, 0x00463435, 0x00463459, 0x004634a3, 0x004634c9, 0x004634ce, 0x00463519, 0x004635ab
