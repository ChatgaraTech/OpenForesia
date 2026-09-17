C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00417770  section .text  offset fichier 0x16b70  448 octets

  00417770  53                    push      ebx
  00417771  56                    push      esi
  00417772  8b74240c              mov       esi,[esp+0Ch]
  00417776  33db                  xor       ebx,ebx
  00417778  57                    push      edi
  00417779  66395e10              cmp       [esi+10h],bx
  0041777d  0f85cd000000          jne       near 00417850h
  00417783  66c746100100          mov       word [esi+10h],1
  00417789  c7463c80fa5400        mov       dword [esi+3Ch],54FA80h
  00417790  c6460a01              mov       byte [esi+0Ah],1
  00417794  885e0b                mov       [esi+0Bh],bl
  00417797  e8a4f91200            call      00547140h
  0041779c  99                    cdq
  0041779d  33c2                  xor       eax,edx
  0041779f  2bc2                  sub       eax,edx
  004177a1  25ff0f0000            and       eax,0FFFh
  004177a6  33c2                  xor       eax,edx
  004177a8  2bc2                  sub       eax,edx
  004177aa  66894612              mov       [esi+12h],ax
  004177ae  e88df91200            call      00547140h
  004177b3  99                    cdq
  004177b4  33c2                  xor       eax,edx
  004177b6  885e40                mov       [esi+40h],bl
  004177b9  2bc2                  sub       eax,edx
  004177bb  25ff030000            and       eax,3FFh
  004177c0  33c2                  xor       eax,edx
  004177c2  2bc2                  sub       eax,edx
  004177c4  0508070000            add       eax,708h
  004177c9  66894628              mov       [esi+28h],ax
  004177cd  6689462a              mov       [esi+2Ah],ax
  004177d1  e86af91200            call      00547140h
  004177d6  99                    cdq
  004177d7  b90b000000            mov       ecx,0Bh
  004177dc  f7f9                  idiv      ecx
  004177de  83c20a                add       edx,0Ah
  004177e1  66895614              mov       [esi+14h],dx
  004177e5  e856f91200            call      00547140h
  004177ea  99                    cdq
  004177eb  33c2                  xor       eax,edx
  004177ed  2bc2                  sub       eax,edx
  004177ef  83e007                and       eax,7
  004177f2  33c2                  xor       eax,edx
  004177f4  2bc2                  sub       eax,edx
  004177f6  83c00f                add       eax,0Fh
  004177f9  66894616              mov       [esi+16h],ax
  004177fd  e83ef91200            call      00547140h
  00417802  99                    cdq
  00417803  b90a000000            mov       ecx,0Ah
  00417808  895e5c                mov       [esi+5Ch],ebx
  0041780b  f7f9                  idiv      ecx
  0041780d  0fbf4614              movsx     eax,word [esi+14h]
  00417811  668b4e16              mov       cx,[esi+16h]
  00417815  83c014                add       eax,14h
  00417818  6689969a000000        mov       [esi+9Ah],dx
  0041781f  0fbfd1                movsx     edx,cx
  00417822  3bc2                  cmp       eax,edx
  00417824  7d0c                  jge       short 00417832h
  00417826  c1f802                sar       eax,2
  00417829  66898698000000        mov       [esi+98h],ax
  00417830  eb0b                  jmp       short 0041783Dh

LAB_00417832:
  00417832  66c1f902              sar       cx,2
  00417836  66898e98000000        mov       [esi+98h],cx

LAB_0041783d:
  0041783d  8b4e20                mov       ecx,[esi+20h]
  00417840  8b06                  mov       eax,[esi]
  00417842  81c100200300          add       ecx,32000h
  00417848  80cc40                or        ah,40h
  0041784b  894e20                mov       [esi+20h],ecx
  0041784e  8906                  mov       [esi],eax

LAB_00417850:
  00417850  66837e1001            cmp       word [esi+10h],1
  00417855  b902000000            mov       ecx,2
  0041785a  7520                  jne       short 0041787Ch
  0041785c  8b465c                mov       eax,[esi+5Ch]
  0041785f  0fbf969a000000        movsx     edx,word [esi+9Ah]
  00417866  40                    inc       eax
  00417867  3bd0                  cmp       edx,eax
  00417869  89465c                mov       [esi+5Ch],eax
  0041786c  7f0e                  jg        short 0041787Ch
  0041786e  8b06                  mov       eax,[esi]
  00417870  66894e10              mov       [esi+10h],cx
  00417874  80e4bf                and       ah,0BFh
  00417877  895e5c                mov       [esi+5Ch],ebx
  0041787a  8906                  mov       [esi],eax

LAB_0041787c:
  0041787c  66394e10              cmp       [esi+10h],cx
  00417880  0f859d000000          jne       near 00417923h
  00417886  8a5640                mov       dl,[esi+40h]
  00417889  66ff4e14              dec       word [esi+14h]
  0041788d  668b4614              mov       ax,[esi+14h]
  00417891  3ad1                  cmp       dl,cl
  00417893  7210                  jb        short 004178A5h
  00417895  8b0e                  mov       ecx,[esi]
  00417897  a801                  test      al,1
  00417899  7505                  jne       short 004178A0h
  0041789b  80cd40                or        ch,40h
  0041789e  eb03                  jmp       short 004178A3h

LAB_004178a0:
  004178a0  80e5bf                and       ch,0BFh

LAB_004178a3:
  004178a3  890e                  mov       [esi],ecx

LAB_004178a5:
  004178a5  8b4e20                mov       ecx,[esi+20h]
  004178a8  0fbfc0                movsx     eax,ax
  004178ab  c1e00c                shl       eax,0Ch
  004178ae  03c8                  add       ecx,eax
  004178b0  894e20                mov       [esi+20h],ecx
  004178b3  0fbf4e12              movsx     ecx,word [esi+12h]
  004178b7  51                    push      ecx
  004178b8  e85fe11200            call      00545A1Ch
  004178bd  0fbf5616              movsx     edx,word [esi+16h]
  004178c1  0fafc2                imul      eax,edx
  004178c4  8b7e1c                mov       edi,[esi+1Ch]
  004178c7  03f8                  add       edi,eax
  004178c9  0fbf4612              movsx     eax,word [esi+12h]
  004178cd  897e1c                mov       [esi+1Ch],edi
  004178d0  668b7e16              mov       di,[esi+16h]
  004178d4  50                    push      eax
  004178d5  e83ce11200            call      00545A16h
  004178da  0fbfcf                movsx     ecx,di
  004178dd  0fafc1                imul      eax,ecx
  004178e0  8b4e24                mov       ecx,[esi+24h]
  004178e3  03c8                  add       ecx,eax
  004178e5  663bfb                cmp       di,bx
  004178e8  894e24                mov       [esi+24h],ecx
  004178eb  7c07                  jl        short 004178F4h
  004178ed  66837e14ec            cmp       word [esi+14h],0FFECh
  004178f2  7d06                  jge       short 004178FAh

LAB_004178f4:
  004178f4  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004178fa:
  004178fa  8b565c                mov       edx,[esi+5Ch]
  004178fd  42                    inc       edx
  004178fe  89565c                mov       [esi+5Ch],edx
  00417901  8bc2                  mov       eax,edx
  00417903  0fbf9698000000        movsx     edx,word [esi+98h]
  0041790a  3bc2                  cmp       eax,edx
  0041790c  7c15                  jl        short 00417923h
  0041790e  8a4e40                mov       cl,[esi+40h]
  00417911  895e5c                mov       [esi+5Ch],ebx
  00417914  fec1                  inc       cl
  00417916  8ac1                  mov       al,cl
  00417918  884e40                mov       [esi+40h],cl
  0041791b  3c04                  cmp       al,4
  0041791d  7204                  jb        short 00417923h
  0041791f  c6464003              mov       byte [esi+40h],3

LAB_00417923:
  00417923  5f                    pop       edi
  00417924  5e                    pop       esi
  00417925  5b                    pop       ebx
  00417926  c3                    ret
  00417927  90                    nop
  00417928  90                    nop
  00417929  90                    nop
  0041792a  90                    nop
  0041792b  90                    nop
  0041792c  90                    nop
  0041792d  90                    nop
  0041792e  90                    nop
  0041792f  90                    nop

10 cible(s) de saut interne : 0x00417832, 0x0041783d, 0x00417850, 0x0041787c, 0x004178a0, 0x004178a3, 0x004178a5, 0x004178f4, 0x004178fa, 0x00417923
