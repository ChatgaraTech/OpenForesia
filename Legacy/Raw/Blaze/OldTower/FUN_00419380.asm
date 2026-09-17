C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00419380  section .text  offset fichier 0x18780  448 octets

  00419380  53                    push      ebx
  00419381  56                    push      esi
  00419382  8b74240c              mov       esi,[esp+0Ch]
  00419386  33db                  xor       ebx,ebx
  00419388  57                    push      edi
  00419389  66395e10              cmp       [esi+10h],bx
  0041938d  0f85cd000000          jne       near 00419460h
  00419393  66c746100100          mov       word [esi+10h],1
  00419399  c7463c28fc5400        mov       dword [esi+3Ch],54FC28h
  004193a0  c6460a01              mov       byte [esi+0Ah],1
  004193a4  885e0b                mov       [esi+0Bh],bl
  004193a7  e894dd1200            call      00547140h
  004193ac  99                    cdq
  004193ad  33c2                  xor       eax,edx
  004193af  2bc2                  sub       eax,edx
  004193b1  25ff0f0000            and       eax,0FFFh
  004193b6  33c2                  xor       eax,edx
  004193b8  2bc2                  sub       eax,edx
  004193ba  66894612              mov       [esi+12h],ax
  004193be  e87ddd1200            call      00547140h
  004193c3  99                    cdq
  004193c4  33c2                  xor       eax,edx
  004193c6  885e40                mov       [esi+40h],bl
  004193c9  2bc2                  sub       eax,edx
  004193cb  25ff030000            and       eax,3FFh
  004193d0  33c2                  xor       eax,edx
  004193d2  2bc2                  sub       eax,edx
  004193d4  0508070000            add       eax,708h
  004193d9  66894628              mov       [esi+28h],ax
  004193dd  6689462a              mov       [esi+2Ah],ax
  004193e1  e85add1200            call      00547140h
  004193e6  99                    cdq
  004193e7  b90b000000            mov       ecx,0Bh
  004193ec  f7f9                  idiv      ecx
  004193ee  83c20a                add       edx,0Ah
  004193f1  66895614              mov       [esi+14h],dx
  004193f5  e846dd1200            call      00547140h
  004193fa  99                    cdq
  004193fb  33c2                  xor       eax,edx
  004193fd  2bc2                  sub       eax,edx
  004193ff  83e007                and       eax,7
  00419402  33c2                  xor       eax,edx
  00419404  2bc2                  sub       eax,edx
  00419406  83c00f                add       eax,0Fh
  00419409  66894616              mov       [esi+16h],ax
  0041940d  e82edd1200            call      00547140h
  00419412  99                    cdq
  00419413  b90a000000            mov       ecx,0Ah
  00419418  895e5c                mov       [esi+5Ch],ebx
  0041941b  f7f9                  idiv      ecx
  0041941d  668b4616              mov       ax,[esi+16h]
  00419421  0fbf4e14              movsx     ecx,word [esi+14h]
  00419425  83c114                add       ecx,14h
  00419428  6689969a000000        mov       [esi+9Ah],dx
  0041942f  0fbfd0                movsx     edx,ax
  00419432  3bca                  cmp       ecx,edx
  00419434  7d0c                  jge       short 00419442h
  00419436  c1f902                sar       ecx,2
  00419439  66898e98000000        mov       [esi+98h],cx
  00419440  eb0b                  jmp       short 0041944Dh

LAB_00419442:
  00419442  66c1f802              sar       ax,2
  00419446  66898698000000        mov       [esi+98h],ax

LAB_0041944d:
  0041944d  8b4e20                mov       ecx,[esi+20h]
  00419450  8b06                  mov       eax,[esi]
  00419452  81c100200300          add       ecx,32000h
  00419458  80cc40                or        ah,40h
  0041945b  894e20                mov       [esi+20h],ecx
  0041945e  8906                  mov       [esi],eax

LAB_00419460:
  00419460  66837e1001            cmp       word [esi+10h],1
  00419465  b902000000            mov       ecx,2
  0041946a  7520                  jne       short 0041948Ch
  0041946c  8b465c                mov       eax,[esi+5Ch]
  0041946f  0fbf969a000000        movsx     edx,word [esi+9Ah]
  00419476  40                    inc       eax
  00419477  3bd0                  cmp       edx,eax
  00419479  89465c                mov       [esi+5Ch],eax
  0041947c  7f0e                  jg        short 0041948Ch
  0041947e  8b06                  mov       eax,[esi]
  00419480  66894e10              mov       [esi+10h],cx
  00419484  80e4bf                and       ah,0BFh
  00419487  895e5c                mov       [esi+5Ch],ebx
  0041948a  8906                  mov       [esi],eax

LAB_0041948c:
  0041948c  66394e10              cmp       [esi+10h],cx
  00419490  0f859d000000          jne       near 00419533h
  00419496  8a5640                mov       dl,[esi+40h]
  00419499  66ff4e14              dec       word [esi+14h]
  0041949d  668b4614              mov       ax,[esi+14h]
  004194a1  3ad1                  cmp       dl,cl
  004194a3  7210                  jb        short 004194B5h
  004194a5  8b0e                  mov       ecx,[esi]
  004194a7  a801                  test      al,1
  004194a9  7505                  jne       short 004194B0h
  004194ab  80cd40                or        ch,40h
  004194ae  eb03                  jmp       short 004194B3h

LAB_004194b0:
  004194b0  80e5bf                and       ch,0BFh

LAB_004194b3:
  004194b3  890e                  mov       [esi],ecx

LAB_004194b5:
  004194b5  8b4e20                mov       ecx,[esi+20h]
  004194b8  0fbfc0                movsx     eax,ax
  004194bb  c1e00c                shl       eax,0Ch
  004194be  03c8                  add       ecx,eax
  004194c0  894e20                mov       [esi+20h],ecx
  004194c3  0fbf4e12              movsx     ecx,word [esi+12h]
  004194c7  51                    push      ecx
  004194c8  e84fc51200            call      00545A1Ch
  004194cd  0fbf5616              movsx     edx,word [esi+16h]
  004194d1  0fafc2                imul      eax,edx
  004194d4  8b7e1c                mov       edi,[esi+1Ch]
  004194d7  03f8                  add       edi,eax
  004194d9  0fbf4612              movsx     eax,word [esi+12h]
  004194dd  897e1c                mov       [esi+1Ch],edi
  004194e0  668b7e16              mov       di,[esi+16h]
  004194e4  50                    push      eax
  004194e5  e82cc51200            call      00545A16h
  004194ea  0fbfcf                movsx     ecx,di
  004194ed  0fafc1                imul      eax,ecx
  004194f0  8b4e24                mov       ecx,[esi+24h]
  004194f3  03c8                  add       ecx,eax
  004194f5  663bfb                cmp       di,bx
  004194f8  894e24                mov       [esi+24h],ecx
  004194fb  7c07                  jl        short 00419504h
  004194fd  66837e14ec            cmp       word [esi+14h],0FFECh
  00419502  7d06                  jge       short 0041950Ah

LAB_00419504:
  00419504  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041950a:
  0041950a  8b565c                mov       edx,[esi+5Ch]
  0041950d  42                    inc       edx
  0041950e  89565c                mov       [esi+5Ch],edx
  00419511  8bc2                  mov       eax,edx
  00419513  0fbf9698000000        movsx     edx,word [esi+98h]
  0041951a  3bc2                  cmp       eax,edx
  0041951c  7c15                  jl        short 00419533h
  0041951e  8a4e40                mov       cl,[esi+40h]
  00419521  895e5c                mov       [esi+5Ch],ebx
  00419524  fec1                  inc       cl
  00419526  8ac1                  mov       al,cl
  00419528  884e40                mov       [esi+40h],cl
  0041952b  3c04                  cmp       al,4
  0041952d  7204                  jb        short 00419533h
  0041952f  c6464003              mov       byte [esi+40h],3

LAB_00419533:
  00419533  5f                    pop       edi
  00419534  5e                    pop       esi
  00419535  5b                    pop       ebx
  00419536  c3                    ret
  00419537  90                    nop
  00419538  90                    nop
  00419539  90                    nop
  0041953a  90                    nop
  0041953b  90                    nop
  0041953c  90                    nop
  0041953d  90                    nop
  0041953e  90                    nop
  0041953f  90                    nop

10 cible(s) de saut interne : 0x00419442, 0x0041944d, 0x00419460, 0x0041948c, 0x004194b0, 0x004194b3, 0x004194b5, 0x00419504, 0x0041950a, 0x00419533
