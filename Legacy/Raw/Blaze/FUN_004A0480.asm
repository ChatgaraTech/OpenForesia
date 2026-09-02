C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004a0480  section .text  offset fichier 0x9f880  3936 octets

  004a0480  81ec88000000          sub       esp,88h
  004a0486  53                    push      ebx
  004a0487  55                    push      ebp
  004a0488  56                    push      esi
  004a0489  8bb42498000000        mov       esi,[esp+98h]
  004a0490  b864000000            mov       eax,64h
  004a0495  33c9                  xor       ecx,ecx
  004a0497  6689442436            mov       [esp+36h],ax
  004a049c  668944243a            mov       [esp+3Ah],ax
  004a04a1  8a4641                mov       al,[esi+41h]
  004a04a4  57                    push      edi
  004a04a5  84c0                  test      al,al
  004a04a7  66894c2434            mov       [esp+34h],cx
  004a04ac  66c74424361e00        mov       word [esp+36h],1Eh
  004a04b3  66c74424383200        mov       word [esp+38h],32h
  004a04ba  66894c243c            mov       [esp+3Ch],cx
  004a04bf  750f                  jne       short 004A04D0h
  004a04c1  51                    push      ecx
  004a04c2  51                    push      ecx
  004a04c3  56                    push      esi
  004a04c4  c6464101              mov       byte [esi+41h],1
  004a04c8  e8530b0000            call      004A1020h
  004a04cd  83c40c                add       esp,0Ch

LAB_004a04d0:
  004a04d0  8b461c                mov       eax,[esi+1Ch]
  004a04d3  8b4e20                mov       ecx,[esi+20h]
  004a04d6  8b5624                mov       edx,[esi+24h]
  004a04d9  8d5e1c                lea       ebx,[esi+1Ch]
  004a04dc  89442428              mov       [esp+28h],eax
  004a04e0  8b4664                mov       eax,[esi+64h]
  004a04e3  894c242c              mov       [esp+2Ch],ecx
  004a04e7  8b4e60                mov       ecx,[esi+60h]
  004a04ea  89542430              mov       [esp+30h],edx
  004a04ee  668b5632              mov       dx,[esi+32h]
  004a04f2  89442448              mov       [esp+48h],eax
  004a04f6  8b467c                mov       eax,[esi+7Ch]
  004a04f9  894c2444              mov       [esp+44h],ecx
  004a04fd  668b8e90000000        mov       cx,[esi+90h]
  004a0504  6689942486000000      mov       [esp+86h],dx
  004a050c  8b9680000000          mov       edx,[esi+80h]
  004a0512  89442460              mov       [esp+60h],eax
  004a0516  8b8684000000          mov       eax,[esi+84h]
  004a051c  66894c244c            mov       [esp+4Ch],cx
  004a0521  8b8e88000000          mov       ecx,[esi+88h]
  004a0527  8b7e6c                mov       edi,[esi+6Ch]
  004a052a  89542450              mov       [esp+50h],edx
  004a052e  8b968c000000          mov       edx,[esi+8Ch]
  004a0534  89442440              mov       [esp+40h],eax
  004a0538  894c245c              mov       [esp+5Ch],ecx
  004a053c  8d442464              lea       eax,[esp+64h]
  004a0540  89542454              mov       [esp+54h],edx
  004a0544  8d4c246c              lea       ecx,[esp+6Ch]
  004a0548  50                    push      eax
  004a0549  8d54246c              lea       edx,[esp+6Ch]
  004a054d  51                    push      ecx
  004a054e  52                    push      edx
  004a054f  56                    push      esi
  004a0550  e82b840900            call      00538980h
  004a0555  83c410                add       esp,10h
  004a0558  8d442458              lea       eax,[esp+58h]
  004a055c  8d4c241c              lea       ecx,[esp+1Ch]
  004a0560  8d542418              lea       edx,[esp+18h]
  004a0564  50                    push      eax
  004a0565  51                    push      ecx
  004a0566  8d44241c              lea       eax,[esp+1Ch]
  004a056a  52                    push      edx
  004a056b  8d4c241c              lea       ecx,[esp+1Ch]
  004a056f  50                    push      eax
  004a0570  51                    push      ecx
  004a0571  56                    push      esi
  004a0572  e8a9590900            call      00535F20h
  004a0577  8b542428              mov       edx,[esp+28h]
  004a057b  b840000000            mov       eax,40h
  004a0580  83c418                add       esp,18h
  004a0583  2bc2                  sub       eax,edx
  004a0585  8b542458              mov       edx,[esp+58h]
  004a0589  89442420              mov       [esp+20h],eax
  004a058d  52                    push      edx
  004a058e  56                    push      esi
  004a058f  e84c580300            call      004D5DE0h
  004a0594  8b03                  mov       eax,[ebx]
  004a0596  8b571c                mov       edx,[edi+1Ch]
  004a0599  8b4e20                mov       ecx,[esi+20h]
  004a059c  8b6f20                mov       ebp,[edi+20h]
  004a059f  2bc2                  sub       eax,edx
  004a05a1  8b5624                mov       edx,[esi+24h]
  004a05a4  c1f80c                sar       eax,0Ch
  004a05a7  2bcd                  sub       ecx,ebp
  004a05a9  8b6f24                mov       ebp,[edi+24h]
  004a05ac  8bf8                  mov       edi,eax
  004a05ae  2bd5                  sub       edx,ebp
  004a05b0  c1f90c                sar       ecx,0Ch
  004a05b3  0faff8                imul      edi,eax
  004a05b6  8bc1                  mov       eax,ecx
  004a05b8  83c408                add       esp,8
  004a05bb  c1fa0c                sar       edx,0Ch
  004a05be  0fafc1                imul      eax,ecx
  004a05c1  8bca                  mov       ecx,edx
  004a05c3  03f8                  add       edi,eax
  004a05c5  0fafca                imul      ecx,edx
  004a05c8  03f9                  add       edi,ecx
  004a05ca  57                    push      edi
  004a05cb  e840540a00            call      00545A10h
  004a05d0  8be8                  mov       ebp,eax
  004a05d2  896c2414              mov       [esp+14h],ebp
  004a05d6  8b4c241c              mov       ecx,[esp+1Ch]
  004a05da  85c9                  test      ecx,ecx
  004a05dc  0f8463040000          je        near 004A0A45h
  004a05e2  8b3e                  mov       edi,[esi]
  004a05e4  33d2                  xor       edx,edx
  004a05e6  8a5604                mov       dl,[esi+4]
  004a05e9  81e7fffffffd          and       edi,0FDFFFFFFh
  004a05ef  0fbf4618              movsx     eax,word [esi+18h]
  004a05f3  893e                  mov       [esi],edi
  004a05f5  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004a05fc  f7c700000001          test      edi,1000000h
  004a0602  8b0482                mov       eax,[edx+eax*4]
  004a0605  741d                  je        short 004A0624h
  004a0607  66ff461a              inc       word [esi+1Ah]
  004a060b  668b561a              mov       dx,[esi+1Ah]
  004a060f  33ff                  xor       edi,edi
  004a0611  668b7816              mov       di,[eax+16h]
  004a0615  0fbfc2                movsx     eax,dx
  004a0618  3bc7                  cmp       eax,edi
  004a061a  7c2b                  jl        short 004A0647h
  004a061c  66c7461a0000          mov       word [esi+1Ah],0
  004a0622  eb23                  jmp       short 004A0647h

LAB_004a0624:
  004a0624  66ff461a              inc       word [esi+1Ah]
  004a0628  33d2                  xor       edx,edx
  004a062a  668b5016              mov       dx,[eax+16h]
  004a062e  0fbf461a              movsx     eax,word [esi+1Ah]
  004a0632  3bc2                  cmp       eax,edx
  004a0634  7c11                  jl        short 004A0647h
  004a0636  668b461a              mov       ax,[esi+1Ah]
  004a063a  48                    dec       eax
  004a063b  81cf00000002          or        edi,2000000h
  004a0641  6689461a              mov       [esi+1Ah],ax
  004a0645  893e                  mov       [esi],edi

LAB_004a0647:
  004a0647  f60602                test      byte [esi],2
  004a064a  7464                  je        short 004A06B0h
  004a064c  66837e1a10            cmp       word [esi+1Ah],10h
  004a0651  755d                  jne       short 004A06B0h
  004a0653  8d4c2434              lea       ecx,[esp+34h]
  004a0657  6a00                  push      0
  004a0659  51                    push      ecx
  004a065a  53                    push      ebx
  004a065b  56                    push      esi
  004a065c  e8cf950700            call      00519C30h
  004a0661  8bf8                  mov       edi,eax
  004a0663  a1b8e05d00            mov       eax,[5DE0B8h]
  004a0668  83c410                add       esp,10h
  004a066b  85c0                  test      eax,eax
  004a066d  7439                  je        short 004A06A8h
  004a066f  66837e1a10            cmp       word [esi+1Ah],10h
  004a0674  7532                  jne       short 004A06A8h
  004a0676  8b2f                  mov       ebp,[edi]
  004a0678  57                    push      edi
  004a0679  83cd40                or        ebp,40h
  004a067c  56                    push      esi
  004a067d  66c787960000001000    mov       word [edi+96h],10h
  004a0686  892f                  mov       [edi],ebp
  004a0688  e8f3940900            call      00539B80h
  004a068d  83c408                add       esp,8
  004a0690  83c71c                add       edi,1Ch
  004a0693  57                    push      edi
  004a0694  6800180000            push      1800h
  004a0699  6800030000            push      300h
  004a069e  6a03                  push      3
  004a06a0  e87bd40900            call      0053DB20h
  004a06a5  83c410                add       esp,10h

LAB_004a06a8:
  004a06a8  8b4c241c              mov       ecx,[esp+1Ch]
  004a06ac  8b6c2414              mov       ebp,[esp+14h]

LAB_004a06b0:
  004a06b0  33c0                  xor       eax,eax
  004a06b2  8a4642                mov       al,[esi+42h]
  004a06b5  83f80b                cmp       eax,0Bh
  004a06b8  0f8787030000          ja        near 004A0A45h
  004a06be  ff2485e80f4a00        jmp       dword [eax*4+4A0FE8h]
  004a06c5  33c9                  xor       ecx,ecx
  004a06c7  66394e12              cmp       [esi+12h],cx
  004a06cb  751a                  jne       short 004A06E7h
  004a06cd  8b06                  mov       eax,[esi]
  004a06cf  66894e18              mov       [esi+18h],cx
  004a06d3  0d00000001            or        eax,1000000h
  004a06d8  66894e1a              mov       [esi+1Ah],cx
  004a06dc  8906                  mov       [esi],eax
  004a06de  66c746120100          mov       word [esi+12h],1
  004a06e4  894e5c                mov       [esi+5Ch],ecx

LAB_004a06e7:
  004a06e7  0fbf5610              movsx     edx,word [esi+10h]
  004a06eb  3bea                  cmp       ebp,edx
  004a06ed  7d09                  jge       short 004A06F8h
  004a06ef  c6464300              mov       byte [esi+43h],0
  004a06f3  e94d030000            jmp       004A0A45h

LAB_004a06f8:
  004a06f8  8a4643                mov       al,[esi+43h]
  004a06fb  84c0                  test      al,al
  004a06fd  0f84f7020000          je        near 004A09FAh
  004a0703  fec8                  dec       al
  004a0705  894e5c                mov       [esi+5Ch],ecx
  004a0708  884643                mov       [esi+43h],al
  004a070b  e935030000            jmp       004A0A45h
  004a0710  33c9                  xor       ecx,ecx
  004a0712  66394e12              cmp       [esi+12h],cx
  004a0716  751b                  jne       short 004A0733h
  004a0718  8b16                  mov       edx,[esi]
  004a071a  b801000000            mov       eax,1
  004a071f  81ca00000001          or        edx,1000000h
  004a0725  66894618              mov       [esi+18h],ax
  004a0729  66894e1a              mov       [esi+1Ah],cx
  004a072d  8916                  mov       [esi],edx
  004a072f  66894612              mov       [esi+12h],ax

LAB_004a0733:
  004a0733  8b442418              mov       eax,[esp+18h]
  004a0737  8d90c8000000          lea       edx,[eax+0C8h]
  004a073d  81e2ff0f0000          and       edx,0FFFh
  004a0743  81fa90010000          cmp       edx,190h
  004a0749  7f0c                  jg        short 004A0757h
  004a074b  c6464300              mov       byte [esi+43h],0
  004a074f  894e5c                mov       [esi+5Ch],ecx
  004a0752  e9ee020000            jmp       004A0A45h

LAB_004a0757:
  004a0757  25ff0f0000            and       eax,0FFFh
  004a075c  3d00080000            cmp       eax,800h
  004a0761  7d13                  jge       short 004A0776h
  004a0763  8b442410              mov       eax,[esp+10h]
  004a0767  894e5c                mov       [esi+5Ch],ecx
  004a076a  83c028                add       eax,28h
  004a076d  66014632              add       [esi+32h],ax
  004a0771  e9cf020000            jmp       004A0A45h

LAB_004a0776:
  004a0776  8b542410              mov       edx,[esp+10h]
  004a077a  b8d8ffffff            mov       eax,0FFFFFFD8h
  004a077f  2bc2                  sub       eax,edx
  004a0781  894e5c                mov       [esi+5Ch],ecx
  004a0784  66014632              add       [esi+32h],ax
  004a0788  e9b8020000            jmp       004A0A45h
  004a078d  8b4c2410              mov       ecx,[esp+10h]
  004a0791  33d2                  xor       edx,edx
  004a0793  66395612              cmp       [esi+12h],dx
  004a0797  b801000000            mov       eax,1
  004a079c  7525                  jne       short 004A07C3h
  004a079e  8b3e                  mov       edi,[esi]
  004a07a0  66894618              mov       [esi+18h],ax
  004a07a4  81cf00000001          or        edi,1000000h
  004a07aa  6689561a              mov       [esi+1Ah],dx
  004a07ae  893e                  mov       [esi],edi
  004a07b0  8d3cc9                lea       edi,[ecx+ecx*8]
  004a07b3  c1e704                shl       edi,4
  004a07b6  81c700500000          add       edi,5000h
  004a07bc  66894612              mov       [esi+12h],ax
  004a07c0  897e5c                mov       [esi+5Ch],edi

LAB_004a07c3:
  004a07c3  66394612              cmp       [esi+12h],ax
  004a07c7  0f8578020000          jne       near 004A0A45h
  004a07cd  0fbf4610              movsx     eax,word [esi+10h]
  004a07d1  3be8                  cmp       ebp,eax
  004a07d3  7d0c                  jge       short 004A07E1h
  004a07d5  c6464300              mov       byte [esi+43h],0
  004a07d9  89565c                mov       [esi+5Ch],edx
  004a07dc  e964020000            jmp       004A0A45h

LAB_004a07e1:
  004a07e1  8a4643                mov       al,[esi+43h]
  004a07e4  84c0                  test      al,al
  004a07e6  7405                  je        short 004A07EDh
  004a07e8  fec8                  dec       al
  004a07ea  884643                mov       [esi+43h],al

LAB_004a07ed:
  004a07ed  8b442418              mov       eax,[esp+18h]
  004a07f1  8d9000010000          lea       edx,[eax+100h]
  004a07f7  81e2ff0f0000          and       edx,0FFFh
  004a07fd  81fa00020000          cmp       edx,200h
  004a0803  0f8e3c020000          jle       near 004A0A45h
  004a0809  25ff0f0000            and       eax,0FFFh
  004a080e  3d00080000            cmp       eax,800h
  004a0813  7d0d                  jge       short 004A0822h
  004a0815  8d440928              lea       eax,[ecx+ecx+28h]
  004a0819  66014632              add       [esi+32h],ax
  004a081d  e923020000            jmp       004A0A45h

LAB_004a0822:
  004a0822  03c9                  add       ecx,ecx
  004a0824  b8d8ffffff            mov       eax,0FFFFFFD8h
  004a0829  2bc1                  sub       eax,ecx
  004a082b  66014632              add       [esi+32h],ax
  004a082f  e911020000            jmp       004A0A45h
  004a0834  66837e1201            cmp       word [esi+12h],1
  004a0839  0f85a3000000          jne       near 004A08E2h
  004a083f  8a4643                mov       al,[esi+43h]
  004a0842  84c0                  test      al,al
  004a0844  7405                  je        short 004A084Bh
  004a0846  fec8                  dec       al
  004a0848  884643                mov       [esi+43h],al

LAB_004a084b:
  004a084b  8b442410              mov       eax,[esp+10h]
  004a084f  8d14c0                lea       edx,[eax+eax*8]
  004a0852  c1e204                shl       edx,4
  004a0855  8d8200500000          lea       eax,[edx+5000h]
  004a085b  0fafc8                imul      ecx,eax
  004a085e  89465c                mov       [esi+5Ch],eax
  004a0861  0fbf4610              movsx     eax,word [esi+10h]
  004a0865  c1f90c                sar       ecx,0Ch
  004a0868  50                    push      eax
  004a0869  8bf9                  mov       edi,ecx
  004a086b  e8ac510a00            call      00545A1Ch
  004a0870  0fafc7                imul      eax,edi
  004a0873  0fbf4e10              movsx     ecx,word [esi+10h]
  004a0877  8b2b                  mov       ebp,[ebx]
  004a0879  51                    push      ecx
  004a087a  c1f80c                sar       eax,0Ch
  004a087d  03e8                  add       ebp,eax
  004a087f  892b                  mov       [ebx],ebp
  004a0881  e890510a00            call      00545A16h
  004a0886  0fafc7                imul      eax,edi
  004a0889  8b6e24                mov       ebp,[esi+24h]
  004a088c  c7465c00000000        mov       dword [esi+5Ch],0
  004a0893  c1f80c                sar       eax,0Ch
  004a0896  03e8                  add       ebp,eax
  004a0898  8b442418              mov       eax,[esp+18h]
  004a089c  896e24                mov       [esi+24h],ebp
  004a089f  8d9000010000          lea       edx,[eax+100h]
  004a08a5  81e2ff0f0000          and       edx,0FFFh
  004a08ab  81fa00020000          cmp       edx,200h
  004a08b1  7e2f                  jle       short 004A08E2h
  004a08b3  25ff0f0000            and       eax,0FFFh
  004a08b8  3d00080000            cmp       eax,800h
  004a08bd  7d0d                  jge       short 004A08CCh
  004a08bf  8b442410              mov       eax,[esp+10h]
  004a08c3  8d04c528000000        lea       eax,[eax*8+28h]
  004a08ca  eb12                  jmp       short 004A08DEh

LAB_004a08cc:
  004a08cc  8b4c2410              mov       ecx,[esp+10h]
  004a08d0  b8d8ffffff            mov       eax,0FFFFFFD8h
  004a08d5  8d14cd00000000        lea       edx,[ecx*8]
  004a08dc  2bc2                  sub       eax,edx

LAB_004a08de:
  004a08de  66014632              add       [esi+32h],ax

LAB_004a08e2:
  004a08e2  66837e1200            cmp       word [esi+12h],0
  004a08e7  0f8558010000          jne       near 004A0A45h
  004a08ed  8b0e                  mov       ecx,[esi]
  004a08ef  b801000000            mov       eax,1
  004a08f4  81c900000001          or        ecx,1000000h
  004a08fa  66894618              mov       [esi+18h],ax
  004a08fe  66c7461a0000          mov       word [esi+1Ah],0
  004a0904  890e                  mov       [esi],ecx
  004a0906  66894612              mov       [esi+12h],ax
  004a090a  e936010000            jmp       004A0A45h
  004a090f  66837e1200            cmp       word [esi+12h],0
  004a0914  757f                  jne       short 004A0995h
  004a0916  8b06                  mov       eax,[esi]
  004a0918  a801                  test      al,1
  004a091a  7406                  je        short 004A0922h
  004a091c  c6464300              mov       byte [esi+43h],0
  004a0920  eb73                  jmp       short 004A0995h

LAB_004a0922:
  004a0922  25fffffffe            and       eax,0FEFFFFFFh
  004a0927  bd01000000            mov       ebp,1
  004a092c  8906                  mov       [esi],eax
  004a092e  8b442410              mov       eax,[esp+10h]
  004a0932  0500010000            add       eax,100h
  004a0937  66896e18              mov       [esi+18h],bp
  004a093b  c1e006                shl       eax,6
  004a093e  0fafc8                imul      ecx,eax
  004a0941  c1f90c                sar       ecx,0Ch
  004a0944  8bf9                  mov       edi,ecx
  004a0946  66c7461a0000          mov       word [esi+1Ah],0
  004a094c  0fbf4e10              movsx     ecx,word [esi+10h]
  004a0950  51                    push      ecx
  004a0951  89465c                mov       [esi+5Ch],eax
  004a0954  e8c3500a00            call      00545A1Ch
  004a0959  0fafc7                imul      eax,edi
  004a095c  0fbf5610              movsx     edx,word [esi+10h]
  004a0960  8b4e50                mov       ecx,[esi+50h]
  004a0963  52                    push      edx
  004a0964  c1f80c                sar       eax,0Ch
  004a0967  03c8                  add       ecx,eax
  004a0969  894e50                mov       [esi+50h],ecx
  004a096c  e8a5500a00            call      00545A16h
  004a0971  0fafc7                imul      eax,edi
  004a0974  8b5658                mov       edx,[esi+58h]
  004a0977  8b4e54                mov       ecx,[esi+54h]
  004a097a  c1f80c                sar       eax,0Ch
  004a097d  03d0                  add       edx,eax
  004a097f  8b06                  mov       eax,[esi]
  004a0981  81c100e00100          add       ecx,1E000h
  004a0987  0bc5                  or        eax,ebp
  004a0989  895658                mov       [esi+58h],edx
  004a098c  894e54                mov       [esi+54h],ecx
  004a098f  8906                  mov       [esi],eax
  004a0991  66896e12              mov       [esi+12h],bp

LAB_004a0995:
  004a0995  f60601                test      byte [esi],1
  004a0998  0f85a7000000          jne       near 004A0A45h
  004a099e  c6464300              mov       byte [esi+43h],0
  004a09a2  e99e000000            jmp       004A0A45h
  004a09a7  668b4e12              mov       cx,[esi+12h]
  004a09ab  ba01000000            mov       edx,1
  004a09b0  663bca                cmp       cx,dx
  004a09b3  751a                  jne       short 004A09CFh
  004a09b5  0fbf4610              movsx     eax,word [esi+10h]
  004a09b9  3be8                  cmp       ebp,eax
  004a09bb  7d06                  jge       short 004A09C3h
  004a09bd  c6464300              mov       byte [esi+43h],0
  004a09c1  eb0c                  jmp       short 004A09CFh

LAB_004a09c3:
  004a09c3  8a4643                mov       al,[esi+43h]
  004a09c6  84c0                  test      al,al
  004a09c8  7405                  je        short 004A09CFh
  004a09ca  fec8                  dec       al
  004a09cc  884643                mov       [esi+43h],al

LAB_004a09cf:
  004a09cf  6685c9                test      cx,cx
  004a09d2  7571                  jne       short 004A0A45h
  004a09d4  8b442410              mov       eax,[esp+10h]
  004a09d8  8b3e                  mov       edi,[esi]
  004a09da  66894e1a              mov       [esi+1Ah],cx
  004a09de  81cf00000001          or        edi,1000000h
  004a09e4  8d0cc0                lea       ecx,[eax+eax*8]
  004a09e7  66895618              mov       [esi+18h],dx
  004a09eb  c1e104                shl       ecx,4
  004a09ee  893e                  mov       [esi],edi
  004a09f0  66895612              mov       [esi+12h],dx
  004a09f4  81c100700000          add       ecx,7000h

LAB_004a09fa:
  004a09fa  894e5c                mov       [esi+5Ch],ecx
  004a09fd  eb46                  jmp       short 004A0A45h
  004a09ff  66837e1200            cmp       word [esi+12h],0
  004a0a04  753f                  jne       short 004A0A45h
  004a0a06  c7465c00000000        mov       dword [esi+5Ch],0
  004a0a0d  e82e670a00            call      00547140h
  004a0a12  99                    cdq
  004a0a13  b903000000            mov       ecx,3
  004a0a18  f7f9                  idiv      ecx
  004a0a1a  83fa01                cmp       edx,1
  004a0a1d  7515                  jne       short 004A0A34h
  004a0a1f  6aff                  push      0FFFFFFFFh
  004a0a21  6a00                  push      0
  004a0a23  68ef000000            push      0EFh
  004a0a28  6a00                  push      0
  004a0a2a  53                    push      ebx
  004a0a2b  56                    push      esi
  004a0a2c  e80fa2fdff            call      0047AC40h
  004a0a31  83c418                add       esp,18h

LAB_004a0a34:
  004a0a34  8b06                  mov       eax,[esi]
  004a0a36  a900000002            test      eax,2000000h
  004a0a3b  7408                  je        short 004A0A45h
  004a0a3d  24fd                  and       al,0FDh
  004a0a3f  c6464300              mov       byte [esi+43h],0
  004a0a43  8906                  mov       [esi],eax

LAB_004a0a45:
  004a0a45  0fbf5632              movsx     edx,word [esi+32h]
  004a0a49  52                    push      edx
  004a0a4a  e8cd4f0a00            call      00545A1Ch
  004a0a4f  89442470              mov       [esp+70h],eax
  004a0a53  0fbf4632              movsx     eax,word [esi+32h]
  004a0a57  50                    push      eax
  004a0a58  e8b94f0a00            call      00545A16h
  004a0a5d  8b5650                mov       edx,[esi+50h]
  004a0a60  89442478              mov       [esp+78h],eax
  004a0a64  8bc2                  mov       eax,edx
  004a0a66  8bfa                  mov       edi,edx
  004a0a68  c1f808                sar       eax,8
  004a0a6b  2bf8                  sub       edi,eax
  004a0a6d  8b4654                mov       eax,[esi+54h]
  004a0a70  c1fa03                sar       edx,3
  004a0a73  2bfa                  sub       edi,edx
  004a0a75  8be8                  mov       ebp,eax
  004a0a77  0fbf969a000000        movsx     edx,word [esi+9Ah]
  004a0a7e  c1fd08                sar       ebp,8
  004a0a81  8b4c241c              mov       ecx,[esp+1Ch]
  004a0a85  896c2424              mov       [esp+24h],ebp
  004a0a89  0faf4e5c              imul      ecx,[esi+5Ch]
  004a0a8d  8bea                  mov       ebp,edx
  004a0a8f  8b542424              mov       edx,[esp+24h]
  004a0a93  c1fd08                sar       ebp,8
  004a0a96  2bea                  sub       ebp,edx
  004a0a98  897e50                mov       [esi+50h],edi
  004a0a9b  0fbf969a000000        movsx     edx,word [esi+9Ah]
  004a0aa2  2bea                  sub       ebp,edx
  004a0aa4  8b5658                mov       edx,[esi+58h]
  004a0aa7  03c5                  add       eax,ebp
  004a0aa9  8bea                  mov       ebp,edx
  004a0aab  89442424              mov       [esp+24h],eax
  004a0aaf  894654                mov       [esi+54h],eax
  004a0ab2  8bc2                  mov       eax,edx
  004a0ab4  56                    push      esi
  004a0ab5  c1f803                sar       eax,3
  004a0ab8  2be8                  sub       ebp,eax
  004a0aba  8b442428              mov       eax,[esp+28h]
  004a0abe  c1fa08                sar       edx,8
  004a0ac1  c1f90c                sar       ecx,0Ch
  004a0ac4  2bea                  sub       ebp,edx
  004a0ac6  8bd1                  mov       edx,ecx
  004a0ac8  0faf542474            imul      edx,[esp+74h]
  004a0acd  0faf4c247c            imul      ecx,[esp+7Ch]
  004a0ad2  c1fa0c                sar       edx,0Ch
  004a0ad5  03d7                  add       edx,edi
  004a0ad7  8b3b                  mov       edi,[ebx]
  004a0ad9  03fa                  add       edi,edx
  004a0adb  8b5620                mov       edx,[esi+20h]
  004a0ade  c1f90c                sar       ecx,0Ch
  004a0ae1  03d0                  add       edx,eax
  004a0ae3  8b4624                mov       eax,[esi+24h]
  004a0ae6  03cd                  add       ecx,ebp
  004a0ae8  896e58                mov       [esi+58h],ebp
  004a0aeb  03c1                  add       eax,ecx
  004a0aed  893b                  mov       [ebx],edi
  004a0aef  895620                mov       [esi+20h],edx
  004a0af2  894624                mov       [esi+24h],eax
  004a0af5  e8462e0900            call      00533940h
  004a0afa  8b9690000000          mov       edx,[esi+90h]
  004a0b00  8b4660                mov       eax,[esi+60h]
  004a0b03  8b4e20                mov       ecx,[esi+20h]
  004a0b06  83c404                add       esp,4
  004a0b09  80e6fc                and       dh,0FCh
  004a0b0c  3bc8                  cmp       ecx,eax
  004a0b0e  899690000000          mov       [esi+90h],edx
  004a0b14  7f2a                  jg        short 004A0B40h
  004a0b16  f6c680                test      dh,80h
  004a0b19  7525                  jne       short 004A0B40h
  004a0b1b  8b0e                  mov       ecx,[esi]
  004a0b1d  894620                mov       [esi+20h],eax
  004a0b20  f6c101                test      cl,1
  004a0b23  740f                  je        short 004A0B34h
  004a0b25  83e1fe                and       ecx,0FFFFFFFEh
  004a0b28  33ff                  xor       edi,edi
  004a0b2a  890e                  mov       [esi],ecx
  004a0b2c  897e54                mov       [esi+54h],edi
  004a0b2f  e924010000            jmp       004A0C58h

LAB_004a0b34:
  004a0b34  83e1fe                and       ecx,0FFFFFFFEh
  004a0b37  33ff                  xor       edi,edi
  004a0b39  890e                  mov       [esi],ecx
  004a0b3b  897e54                mov       [esi+54h],edi
  004a0b3e  eb02                  jmp       short 004A0B42h

LAB_004a0b40:
  004a0b40  33ff                  xor       edi,edi

LAB_004a0b42:
  004a0b42  8b0e                  mov       ecx,[esi]
  004a0b44  f6c101                test      cl,1
  004a0b47  750a                  jne       short 004A0B53h
  004a0b49  394620                cmp       [esi+20h],eax
  004a0b4c  7e05                  jle       short 004A0B53h
  004a0b4e  83c901                or        ecx,1
  004a0b51  890e                  mov       [esi],ecx

LAB_004a0b53:
  004a0b53  f7c282af0020          test      edx,2000AF82h
  004a0b59  0f84f9000000          je        near 004A0C58h
  004a0b5f  f60601                test      byte [esi],1
  004a0b62  0f85c8000000          jne       near 004A0C30h
  004a0b68  8a4e42                mov       cl,[esi+42h]
  004a0b6b  b007                  mov       al,7
  004a0b6d  3ac8                  cmp       cl,al
  004a0b6f  7420                  je        short 004A0B91h
  004a0b71  884642                mov       [esi+42h],al
  004a0b74  e8c7650a00            call      00547140h
  004a0b79  99                    cdq
  004a0b7a  b928000000            mov       ecx,28h
  004a0b7f  66897e12              mov       [esi+12h],di
  004a0b83  f7f9                  idiv      ecx
  004a0b85  66c746105000          mov       word [esi+10h],50h
  004a0b8b  80c214                add       dl,14h
  004a0b8e  885643                mov       [esi+43h],dl

LAB_004a0b91:
  004a0b91  8b16                  mov       edx,[esi]
  004a0b93  8b44242c              mov       eax,[esp+2Ch]
  004a0b97  8b4c2430              mov       ecx,[esp+30h]
  004a0b9b  83e2fd                and       edx,0FFFFFFFDh
  004a0b9e  8916                  mov       [esi],edx
  004a0ba0  8b542428              mov       edx,[esp+28h]
  004a0ba4  894620                mov       [esi+20h],eax
  004a0ba7  8b44244c              mov       eax,[esp+4Ch]
  004a0bab  8913                  mov       [ebx],edx
  004a0bad  8b542460              mov       edx,[esp+60h]
  004a0bb1  25ffff0000            and       eax,0FFFFh
  004a0bb6  894e24                mov       [esi+24h],ecx
  004a0bb9  8b4c2440              mov       ecx,[esp+40h]
  004a0bbd  89567c                mov       [esi+7Ch],edx
  004a0bc0  8b54245c              mov       edx,[esp+5Ch]
  004a0bc4  898690000000          mov       [esi+90h],eax
  004a0bca  8b442450              mov       eax,[esp+50h]
  004a0bce  898e84000000          mov       [esi+84h],ecx
  004a0bd4  8b4c2448              mov       ecx,[esp+48h]
  004a0bd8  898680000000          mov       [esi+80h],eax
  004a0bde  8b442454              mov       eax,[esp+54h]
  004a0be2  899688000000          mov       [esi+88h],edx
  004a0be8  8b542444              mov       edx,[esp+44h]
  004a0bec  6882af0020            push      2000AF82h
  004a0bf1  56                    push      esi
  004a0bf2  89868c000000          mov       [esi+8Ch],eax
  004a0bf8  894e64                mov       [esi+64h],ecx
  004a0bfb  895660                mov       [esi+60h],edx
  004a0bfe  e85d520300            call      004D5E60h
  004a0c03  83c408                add       esp,8
  004a0c06  663bc7                cmp       ax,di
  004a0c09  7515                  jne       short 004A0C20h
  004a0c0b  e830650a00            call      00547140h
  004a0c10  99                    cdq
  004a0c11  33c2                  xor       eax,edx
  004a0c13  2bc2                  sub       eax,edx
  004a0c15  83e001                and       eax,1
  004a0c18  33c2                  xor       eax,edx
  004a0c1a  2bc2                  sub       eax,edx
  004a0c1c  8d4400ff              lea       eax,[eax+eax-1]

LAB_004a0c20:
  004a0c20  8b4c2410              mov       ecx,[esp+10h]
  004a0c24  83c140                add       ecx,40h
  004a0c27  0fafc8                imul      ecx,eax
  004a0c2a  66014e32              add       [esi+32h],cx
  004a0c2e  eb28                  jmp       short 004A0C58h

LAB_004a0c30:
  004a0c30  8b542428              mov       edx,[esp+28h]
  004a0c34  8b442430              mov       eax,[esp+30h]
  004a0c38  897e5c                mov       [esi+5Ch],edi
  004a0c3b  897e50                mov       [esi+50h],edi
  004a0c3e  897e58                mov       [esi+58h],edi
  004a0c41  8913                  mov       [ebx],edx
  004a0c43  894624                mov       [esi+24h],eax
  004a0c46  c6464200              mov       byte [esi+42h],0
  004a0c4a  c6464328              mov       byte [esi+43h],28h
  004a0c4e  66897e12              mov       [esi+12h],di
  004a0c52  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004a0c58:
  004a0c58  6aff                  push      0FFFFFFFFh
  004a0c5a  6aff                  push      0FFFFFFFFh
  004a0c5c  6aff                  push      0FFFFFFFFh
  004a0c5e  56                    push      esi
  004a0c5f  e8ac2b0200            call      004C3810h
  004a0c64  8b44242c              mov       eax,[esp+2Ch]
  004a0c68  83c410                add       esp,10h
  004a0c6b  3bc7                  cmp       eax,edi
  004a0c6d  7517                  jne       short 004A0C86h
  004a0c6f  668b8c2486000000      mov       cx,[esp+86h]
  004a0c77  66894e32              mov       [esi+32h],cx
  004a0c7b  5f                    pop       edi
  004a0c7c  5e                    pop       esi
  004a0c7d  5d                    pop       ebp
  004a0c7e  5b                    pop       ebx
  004a0c7f  81c488000000          add       esp,88h
  004a0c85  c3                    ret

LAB_004a0c86:
  004a0c86  8a4643                mov       al,[esi+43h]
  004a0c89  84c0                  test      al,al
  004a0c8b  0f854c030000          jne       near 004A0FDDh
  004a0c91  8b466c                mov       eax,[esi+6Ch]
  004a0c94  8b2b                  mov       ebp,[ebx]
  004a0c96  8b4e24                mov       ecx,[esi+24h]
  004a0c99  8b781c                mov       edi,[eax+1Ch]
  004a0c9c  2bfd                  sub       edi,ebp
  004a0c9e  8b6824                mov       ebp,[eax+24h]
  004a0ca1  2be9                  sub       ebp,ecx
  004a0ca3  c1ff0c                sar       edi,0Ch
  004a0ca6  c1fd0c                sar       ebp,0Ch
  004a0ca9  8bd7                  mov       edx,edi
  004a0cab  8bc5                  mov       eax,ebp
  004a0cad  0fafd7                imul      edx,edi
  004a0cb0  0fafc5                imul      eax,ebp
  004a0cb3  03d0                  add       edx,eax
  004a0cb5  52                    push      edx
  004a0cb6  e8554d0a00            call      00545A10h
  004a0cbb  55                    push      ebp
  004a0cbc  57                    push      edi
  004a0cbd  8944241c              mov       [esp+1Ch],eax
  004a0cc1  e85c4d0a00            call      00545A22h
  004a0cc6  668b4e32              mov       cx,[esi+32h]
  004a0cca  66c746120000          mov       word [esi+12h],0
  004a0cd0  2bc1                  sub       eax,ecx
  004a0cd2  b306                  mov       bl,6
  004a0cd4  25ff0f0000            and       eax,0FFFh
  004a0cd9  89442418              mov       [esp+18h],eax
  004a0cdd  8b442414              mov       eax,[esp+14h]
  004a0ce1  3dfa000000            cmp       eax,0FAh
  004a0ce6  0f8d1c010000          jge       near 004A0E08h
  004a0cec  e84f640a00            call      00547140h
  004a0cf1  99                    cdq
  004a0cf2  b964000000            mov       ecx,64h
  004a0cf7  f7f9                  idiv      ecx
  004a0cf9  83fa28                cmp       edx,28h
  004a0cfc  7d6e                  jge       short 004A0D6Ch
  004a0cfe  8b542418              mov       edx,[esp+18h]
  004a0d02  8b7c2420              mov       edi,[esp+20h]
  004a0d06  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004a0d0d  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004a0d14  25ff0f0000            and       eax,0FFFh
  004a0d19  3bc1                  cmp       eax,ecx
  004a0d1b  7f40                  jg        short 004A0D5Dh
  004a0d1d  e81e640a00            call      00547140h
  004a0d22  99                    cdq
  004a0d23  8d4c3f01              lea       ecx,[edi+edi+1]
  004a0d27  f7f9                  idiv      ecx
  004a0d29  8b442414              mov       eax,[esp+14h]
  004a0d2d  83c264                add       edx,64h
  004a0d30  3bc2                  cmp       eax,edx
  004a0d32  7d29                  jge       short 004A0D5Dh
  004a0d34  8b16                  mov       edx,[esi]
  004a0d36  c646420b              mov       byte [esi+42h],0Bh
  004a0d3a  81e2fffffffe          and       edx,0FEFFFFFFh
  004a0d40  c6464301              mov       byte [esi+43h],1
  004a0d44  83ca02                or        edx,2
  004a0d47  33ed                  xor       ebp,ebp
  004a0d49  8916                  mov       [esi],edx
  004a0d4b  896e5c                mov       [esi+5Ch],ebp
  004a0d4e  66c746180300          mov       word [esi+18h],3
  004a0d54  66896e1a              mov       [esi+1Ah],bp
  004a0d58  e9b1000000            jmp       004A0E0Eh

LAB_004a0d5d:
  004a0d5d  c6464201              mov       byte [esi+42h],1
  004a0d61  c6464301              mov       byte [esi+43h],1
  004a0d65  33ed                  xor       ebp,ebp
  004a0d67  e9a2000000            jmp       004A0E0Eh

LAB_004a0d6c:
  004a0d6c  83fa3c                cmp       edx,3Ch
  004a0d6f  7d40                  jge       short 004A0DB1h
  004a0d71  33ed                  xor       ebp,ebp
  004a0d73  c6464203              mov       byte [esi+42h],3
  004a0d77  66896e12              mov       [esi+12h],bp
  004a0d7b  e8c0630a00            call      00547140h
  004a0d80  99                    cdq
  004a0d81  b90a000000            mov       ecx,0Ah
  004a0d86  8b7c2420              mov       edi,[esp+20h]
  004a0d8a  f7f9                  idiv      ecx
  004a0d8c  8bc7                  mov       eax,edi
  004a0d8e  d1f8                  sar       eax,1
  004a0d90  02d0                  add       dl,al
  004a0d92  80c214                add       dl,14h
  004a0d95  885643                mov       [esi+43h],dl
  004a0d98  e8a3630a00            call      00547140h
  004a0d9d  99                    cdq
  004a0d9e  33c2                  xor       eax,edx
  004a0da0  2bc2                  sub       eax,edx
  004a0da2  25ff0f0000            and       eax,0FFFh
  004a0da7  33c2                  xor       eax,edx
  004a0da9  2bc2                  sub       eax,edx
  004a0dab  66894610              mov       [esi+10h],ax
  004a0daf  eb5d                  jmp       short 004A0E0Eh

LAB_004a0db1:
  004a0db1  83fa46                cmp       edx,46h
  004a0db4  7d27                  jge       short 004A0DDDh
  004a0db6  8b4c2410              mov       ecx,[esp+10h]
  004a0dba  8b442414              mov       eax,[esp+14h]
  004a0dbe  83c164                add       ecx,64h
  004a0dc1  3bc1                  cmp       eax,ecx
  004a0dc3  7e18                  jle       short 004A0DDDh
  004a0dc5  885e42                mov       [esi+42h],bl
  004a0dc8  c6464301              mov       byte [esi+43h],1
  004a0dcc  e86f630a00            call      00547140h
  004a0dd1  33ed                  xor       ebp,ebp
  004a0dd3  66894610              mov       [esi+10h],ax
  004a0dd7  66896e12              mov       [esi+12h],bp
  004a0ddb  eb2d                  jmp       short 004A0E0Ah

LAB_004a0ddd:
  004a0ddd  c6464202              mov       byte [esi+42h],2
  004a0de1  e85a630a00            call      00547140h
  004a0de6  99                    cdq
  004a0de7  b90a000000            mov       ecx,0Ah
  004a0dec  8b7c2420              mov       edi,[esp+20h]
  004a0df0  f7f9                  idiv      ecx
  004a0df2  8bc7                  mov       eax,edi
  004a0df4  66c746105000          mov       word [esi+10h],50h
  004a0dfa  d1f8                  sar       eax,1
  004a0dfc  02d0                  add       dl,al
  004a0dfe  80c214                add       dl,14h
  004a0e01  33ed                  xor       ebp,ebp
  004a0e03  885643                mov       [esi+43h],dl
  004a0e06  eb06                  jmp       short 004A0E0Eh

LAB_004a0e08:
  004a0e08  33ed                  xor       ebp,ebp

LAB_004a0e0a:
  004a0e0a  8b7c2420              mov       edi,[esp+20h]

LAB_004a0e0e:
  004a0e0e  8b442414              mov       eax,[esp+14h]
  004a0e12  3d58020000            cmp       eax,258h
  004a0e17  0f8dc0000000          jge       near 004A0EDDh
  004a0e1d  3dfa000000            cmp       eax,0FAh
  004a0e22  0f8cb5000000          jl        near 004A0EDDh
  004a0e28  e813630a00            call      00547140h
  004a0e2d  99                    cdq
  004a0e2e  b964000000            mov       ecx,64h
  004a0e33  f7f9                  idiv      ecx
  004a0e35  83fa14                cmp       edx,14h
  004a0e38  7d26                  jge       short 004A0E60h
  004a0e3a  8b542418              mov       edx,[esp+18h]
  004a0e3e  81c200040000          add       edx,400h
  004a0e44  81e2ff0f0000          and       edx,0FFFh
  004a0e4a  81fa00080000          cmp       edx,800h
  004a0e50  0f8c87000000          jl        near 004A0EDDh
  004a0e56  c6464201              mov       byte [esi+42h],1
  004a0e5a  c6464301              mov       byte [esi+43h],1
  004a0e5e  eb7d                  jmp       short 004A0EDDh

LAB_004a0e60:
  004a0e60  83fa28                cmp       edx,28h
  004a0e63  7d25                  jge       short 004A0E8Ah
  004a0e65  c6464202              mov       byte [esi+42h],2
  004a0e69  e8d2620a00            call      00547140h
  004a0e6e  99                    cdq
  004a0e6f  b90a000000            mov       ecx,0Ah
  004a0e74  66c746105000          mov       word [esi+10h],50h
  004a0e7a  f7f9                  idiv      ecx
  004a0e7c  8bc7                  mov       eax,edi
  004a0e7e  d1f8                  sar       eax,1
  004a0e80  02d0                  add       dl,al
  004a0e82  80c214                add       dl,14h
  004a0e85  885643                mov       [esi+43h],dl
  004a0e88  eb53                  jmp       short 004A0EDDh

LAB_004a0e8a:
  004a0e8a  83fa3c                cmp       edx,3Ch
  004a0e8d  7d16                  jge       short 004A0EA5h
  004a0e8f  885e42                mov       [esi+42h],bl
  004a0e92  c6464301              mov       byte [esi+43h],1
  004a0e96  e8a5620a00            call      00547140h
  004a0e9b  66894610              mov       [esi+10h],ax
  004a0e9f  66896e12              mov       [esi+12h],bp
  004a0ea3  eb38                  jmp       short 004A0EDDh

LAB_004a0ea5:
  004a0ea5  c6464203              mov       byte [esi+42h],3
  004a0ea9  66896e12              mov       [esi+12h],bp
  004a0ead  e88e620a00            call      00547140h
  004a0eb2  99                    cdq
  004a0eb3  b90a000000            mov       ecx,0Ah
  004a0eb8  f7f9                  idiv      ecx
  004a0eba  8bc7                  mov       eax,edi
  004a0ebc  d1f8                  sar       eax,1
  004a0ebe  02d0                  add       dl,al
  004a0ec0  80c214                add       dl,14h
  004a0ec3  885643                mov       [esi+43h],dl
  004a0ec6  e875620a00            call      00547140h
  004a0ecb  99                    cdq
  004a0ecc  33c2                  xor       eax,edx
  004a0ece  2bc2                  sub       eax,edx
  004a0ed0  25ff0f0000            and       eax,0FFFh
  004a0ed5  33c2                  xor       eax,edx
  004a0ed7  2bc2                  sub       eax,edx
  004a0ed9  66894610              mov       [esi+10h],ax

LAB_004a0edd:
  004a0edd  817c241458020000      cmp       dword [esp+14h],258h
  004a0ee5  0f8cf2000000          jl        near 004A0FDDh
  004a0eeb  e850620a00            call      00547140h
  004a0ef0  99                    cdq
  004a0ef1  b964000000            mov       ecx,64h
  004a0ef6  f7f9                  idiv      ecx
  004a0ef8  83fa1e                cmp       edx,1Eh
  004a0efb  7d70                  jge       short 004A0F6Dh
  004a0efd  8b442410              mov       eax,[esp+10h]
  004a0f01  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004a0f05  3bd1                  cmp       edx,ecx
  004a0f07  7d2e                  jge       short 004A0F37h
  004a0f09  c6464202              mov       byte [esi+42h],2
  004a0f0d  e82e620a00            call      00547140h
  004a0f12  99                    cdq
  004a0f13  b90a000000            mov       ecx,0Ah
  004a0f18  66c746106400          mov       word [esi+10h],64h
  004a0f1e  f7f9                  idiv      ecx
  004a0f20  8bc7                  mov       eax,edi
  004a0f22  d1f8                  sar       eax,1
  004a0f24  02d0                  add       dl,al
  004a0f26  80c214                add       dl,14h
  004a0f29  885643                mov       [esi+43h],dl
  004a0f2c  5f                    pop       edi
  004a0f2d  5e                    pop       esi
  004a0f2e  5d                    pop       ebp
  004a0f2f  5b                    pop       ebx
  004a0f30  81c488000000          add       esp,88h
  004a0f36  c3                    ret

LAB_004a0f37:
  004a0f37  c6464200              mov       byte [esi+42h],0
  004a0f3b  e800620a00            call      00547140h
  004a0f40  99                    cdq
  004a0f41  b90a000000            mov       ecx,0Ah
  004a0f46  66896e12              mov       [esi+12h],bp
  004a0f4a  f7f9                  idiv      ecx
  004a0f4c  8b4c2410              mov       ecx,[esp+10h]
  004a0f50  8bc7                  mov       eax,edi
  004a0f52  c1f802                sar       eax,2
  004a0f55  02d0                  add       dl,al
  004a0f57  885643                mov       [esi+43h],dl
  004a0f5a  8d540950              lea       edx,[ecx+ecx+50h]
  004a0f5e  66895610              mov       [esi+10h],dx
  004a0f62  5f                    pop       edi
  004a0f63  5e                    pop       esi
  004a0f64  5d                    pop       ebp
  004a0f65  5b                    pop       ebx
  004a0f66  81c488000000          add       esp,88h
  004a0f6c  c3                    ret

LAB_004a0f6d:
  004a0f6d  83fa28                cmp       edx,28h
  004a0f70  7d43                  jge       short 004A0FB5h
  004a0f72  c6464203              mov       byte [esi+42h],3
  004a0f76  66896e12              mov       [esi+12h],bp
  004a0f7a  e8c1610a00            call      00547140h
  004a0f7f  99                    cdq
  004a0f80  b90a000000            mov       ecx,0Ah
  004a0f85  f7f9                  idiv      ecx
  004a0f87  8bc7                  mov       eax,edi
  004a0f89  d1f8                  sar       eax,1
  004a0f8b  02d0                  add       dl,al
  004a0f8d  80c214                add       dl,14h
  004a0f90  885643                mov       [esi+43h],dl
  004a0f93  e8a8610a00            call      00547140h
  004a0f98  99                    cdq
  004a0f99  33c2                  xor       eax,edx
  004a0f9b  2bc2                  sub       eax,edx
  004a0f9d  25ff0f0000            and       eax,0FFFh
  004a0fa2  33c2                  xor       eax,edx
  004a0fa4  2bc2                  sub       eax,edx
  004a0fa6  66894610              mov       [esi+10h],ax
  004a0faa  5f                    pop       edi
  004a0fab  5e                    pop       esi
  004a0fac  5d                    pop       ebp
  004a0fad  5b                    pop       ebx
  004a0fae  81c488000000          add       esp,88h
  004a0fb4  c3                    ret

LAB_004a0fb5:
  004a0fb5  83fa3c                cmp       edx,3Ch
  004a0fb8  7d13                  jge       short 004A0FCDh
  004a0fba  c6464201              mov       byte [esi+42h],1
  004a0fbe  c6464301              mov       byte [esi+43h],1
  004a0fc2  5f                    pop       edi
  004a0fc3  5e                    pop       esi
  004a0fc4  5d                    pop       ebp
  004a0fc5  5b                    pop       ebx
  004a0fc6  81c488000000          add       esp,88h
  004a0fcc  c3                    ret

LAB_004a0fcd:
  004a0fcd  885e42                mov       [esi+42h],bl
  004a0fd0  c6464301              mov       byte [esi+43h],1
  004a0fd4  e867610a00            call      00547140h
  004a0fd9  66894610              mov       [esi+10h],ax

LAB_004a0fdd:
  004a0fdd  5f                    pop       edi
  004a0fde  5e                    pop       esi
  004a0fdf  5d                    pop       ebp
  004a0fe0  5b                    pop       ebx
  004a0fe1  81c488000000          add       esp,88h
  004a0fe7  c3                    ret
  004a0fe8  c506                  lds       eax,[esi]
  004a0fea  4a                    dec       edx
  004a0feb  0010                  add       [eax],dl
  004a0fed  07                    pop       es
  004a0fee  4a                    dec       edx
  004a0fef  008d074a0034          add       [ebp+34004A07h],cl
  004a0ff5  084a00                or        [edx],cl
  004a0ff8  45                    inc       ebp
  004a0ff9  0a4a00                or        cl,[edx]
  004a0ffc  45                    inc       ebp
  004a0ffd  0a4a00                or        cl,[edx]
  004a1000  0f09                  wbinvd
  004a1002  4a                    dec       edx
  004a1003  00a7094a0045          add       [edi+45004A09h],ah
  004a1009  0a4a00                or        cl,[edx]
  004a100c  45                    inc       ebp
  004a100d  0a4a00                or        cl,[edx]
  004a1010  45                    inc       ebp
  004a1011  0a4a00                or        cl,[edx]
  004a1014  ff09                  dec       dword [ecx]
  004a1016  4a                    dec       edx
  004a1017  009090909090          add       [eax-6F6F6F70h],dl
  004a101d  90                    nop
  004a101e  90                    nop
  004a101f  90                    nop
  004a1020  83ec10                sub       esp,10h
  004a1023  b002                  mov       al,2
  004a1025  53                    push      ebx
  004a1026  88442406              mov       [esp+6],al
  004a102a  88442409              mov       [esp+9],al
  004a102e  8844240c              mov       [esp+0Ch],al
  004a1032  88442413              mov       [esp+13h],al
  004a1036  8b442418              mov       eax,[esp+18h]
  004a103a  55                    push      ebp
  004a103b  56                    push      esi
  004a103c  33db                  xor       ebx,ebx
  004a103e  57                    push      edi
  004a103f  8b786c                mov       edi,[eax+6Ch]
  004a1042  885c2410              mov       [esp+10h],bl
  004a1046  c644241101            mov       byte [esp+11h],1
  004a104b  c644241303            mov       byte [esp+13h],3
  004a1050  c644241401            mov       byte [esp+14h],1
  004a1055  c644241603            mov       byte [esp+16h],3
  004a105a  885c2417              mov       [esp+17h],bl
  004a105e  c644241903            mov       byte [esp+19h],3
  004a1063  885c241a              mov       [esp+1Ah],bl
  004a1067  c644241b01            mov       byte [esp+1Bh],1
  004a106c  c644241c03            mov       byte [esp+1Ch],3
  004a1071  885c241d              mov       [esp+1Dh],bl
  004a1075  c644241e01            mov       byte [esp+1Eh],1
  004a107a  e8c1600a00            call      00547140h
  004a107f  99                    cdq
  004a1080  33c2                  xor       eax,edx
  004a1082  2bc2                  sub       eax,edx
  004a1084  660fb66f08            movzx     bp,byte [edi+8]
  004a1089  83e003                and       eax,3
  004a108c  33c2                  xor       eax,edx
  004a108e  8bf0                  mov       esi,eax
  004a1090  2bf2                  sub       esi,edx
  004a1092  e8a9600a00            call      00547140h
  004a1097  99                    cdq
  004a1098  b90a000000            mov       ecx,0Ah
  004a109d  f7f9                  idiv      ecx
  004a109f  6683fa05              cmp       dx,5
  004a10a3  0f8c0b030000          jl        near 004A13B4h
  004a10a9  e892600a00            call      00547140h
  004a10ae  99                    cdq
  004a10af  b90a000000            mov       ecx,0Ah
  004a10b4  f7f9                  idiv      ecx
  004a10b6  6683fa05              cmp       dx,5
  004a10ba  7f05                  jg        short 004A10C1h
  004a10bc  83c8ff                or        eax,0FFFFFFFFh
  004a10bf  eb04                  jmp       short 004A10C5h

LAB_004a10c1:
  004a10c1  8b442428              mov       eax,[esp+28h]

LAB_004a10c5:
  004a10c5  0fbfc0                movsx     eax,ax
  004a10c8  40                    inc       eax
  004a10c9  83f808                cmp       eax,8
  004a10cc  0f87c9020000          ja        near 004A139Bh
  004a10d2  ff2485bc134a00        jmp       dword [eax*4+4A13BCh]
  004a10d9  33f6                  xor       esi,esi

LAB_004a10db:
  004a10db  e860600a00            call      00547140h
  004a10e0  99                    cdq
  004a10e1  33c2                  xor       eax,edx
  004a10e3  2bc2                  sub       eax,edx
  004a10e5  83e003                and       eax,3
  004a10e8  33c2                  xor       eax,edx
  004a10ea  2bc2                  sub       eax,edx
  004a10ec  8d1480                lea       edx,[eax+eax*4]
  004a10ef  c1e203                shl       edx,3
  004a10f2  2bd0                  sub       edx,eax
  004a10f4  8b0c95c0935e00        mov       ecx,[edx*4+5E93C0h]
  004a10fb  f7c100000080          test      ecx,80000000h
  004a1101  740f                  je        short 004A1112h
  004a1103  f7c100000050          test      ecx,50000000h
  004a1109  7507                  jne       short 004A1112h
  004a110b  be01000000            mov       esi,1
  004a1110  8be8                  mov       ebp,eax

LAB_004a1112:
  004a1112  663bf3                cmp       si,bx
  004a1115  74c4                  je        short 004A10DBh
  004a1117  e97f020000            jmp       004A139Bh
  004a111c  8b3db4876000          mov       edi,[6087B4h]
  004a1122  33ed                  xor       ebp,ebp
  004a1124  0fbfc6                movsx     eax,si
  004a1127  c744242804000000      mov       dword [esp+28h],4
  004a112f  8d748410              lea       esi,[esp+eax*4+10h]

LAB_004a1133:
  004a1133  8a1e                  mov       bl,[esi]
  004a1135  0fbec3                movsx     eax,bl
  004a1138  8d0c80                lea       ecx,[eax+eax*4]
  004a113b  c1e103                shl       ecx,3
  004a113e  2bc8                  sub       ecx,eax
  004a1140  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004a1147  f7c100000080          test      ecx,80000000h
  004a114d  742e                  je        short 004A117Dh
  004a114f  f7c100000050          test      ecx,50000000h
  004a1155  7526                  jne       short 004A117Dh
  004a1157  8d4878                lea       ecx,[eax+78h]
  004a115a  8b07                  mov       eax,[edi]
  004a115c  c1e10d                shl       ecx,0Dh
  004a115f  03c8                  add       ecx,eax
  004a1161  0fbf8148010000        movsx     eax,word [ecx+148h]
  004a1168  0fbf894c010000        movsx     ecx,word [ecx+14Ch]
  004a116f  c1e00c                shl       eax,0Ch
  004a1172  99                    cdq
  004a1173  f7f9                  idiv      ecx
  004a1175  85c0                  test      eax,eax
  004a1177  7e04                  jle       short 004A117Dh
  004a1179  660fbeeb              movsx     bp,bl

LAB_004a117d:
  004a117d  8b442428              mov       eax,[esp+28h]
  004a1181  46                    inc       esi
  004a1182  48                    dec       eax
  004a1183  89442428              mov       [esp+28h],eax
  004a1187  75aa                  jne       short 004A1133h
  004a1189  e90d020000            jmp       004A139Bh
  004a118e  8b3db4876000          mov       edi,[6087B4h]
  004a1194  33ed                  xor       ebp,ebp
  004a1196  0fbfd6                movsx     edx,si
  004a1199  c744242804000000      mov       dword [esp+28h],4
  004a11a1  8d749410              lea       esi,[esp+edx*4+10h]

LAB_004a11a5:
  004a11a5  8a1e                  mov       bl,[esi]
  004a11a7  0fbec3                movsx     eax,bl
  004a11aa  8d0c80                lea       ecx,[eax+eax*4]
  004a11ad  c1e103                shl       ecx,3
  004a11b0  2bc8                  sub       ecx,eax
  004a11b2  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004a11b9  f7c100000080          test      ecx,80000000h
  004a11bf  7431                  je        short 004A11F2h
  004a11c1  f7c100000050          test      ecx,50000000h
  004a11c7  7529                  jne       short 004A11F2h
  004a11c9  8d4878                lea       ecx,[eax+78h]
  004a11cc  8b07                  mov       eax,[edi]
  004a11ce  c1e10d                shl       ecx,0Dh
  004a11d1  03c8                  add       ecx,eax
  004a11d3  0fbf8148010000        movsx     eax,word [ecx+148h]
  004a11da  0fbf894c010000        movsx     ecx,word [ecx+14Ch]
  004a11e1  c1e00c                shl       eax,0Ch
  004a11e4  99                    cdq
  004a11e5  f7f9                  idiv      ecx
  004a11e7  3d10270000            cmp       eax,2710h
  004a11ec  7d04                  jge       short 004A11F2h
  004a11ee  660fbeeb              movsx     bp,bl

LAB_004a11f2:
  004a11f2  8b442428              mov       eax,[esp+28h]
  004a11f6  46                    inc       esi
  004a11f7  48                    dec       eax
  004a11f8  89442428              mov       [esp+28h],eax
  004a11fc  75a7                  jne       short 004A11A5h
  004a11fe  e998010000            jmp       004A139Bh
  004a1203  8b3db4876000          mov       edi,[6087B4h]
  004a1209  bb04000000            mov       ebx,4
  004a120e  0fbfd6                movsx     edx,si
  004a1211  8d749410              lea       esi,[esp+edx*4+10h]

LAB_004a1215:
  004a1215  8a16                  mov       dl,[esi]
  004a1217  0fbec2                movsx     eax,dl
  004a121a  8d0c80                lea       ecx,[eax+eax*4]
  004a121d  c1e103                shl       ecx,3
  004a1220  2bc8                  sub       ecx,eax
  004a1222  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004a1229  f7c100000080          test      ecx,80000000h
  004a122f  741b                  je        short 004A124Ch
  004a1231  f7c100000050          test      ecx,50000000h
  004a1237  7513                  jne       short 004A124Ch
  004a1239  8b0f                  mov       ecx,[edi]
  004a123b  c1e00d                shl       eax,0Dh
  004a123e  f6840850010f0007      test      byte [eax+ecx+0F0150h],7
  004a1246  7504                  jne       short 004A124Ch
  004a1248  660fbeea              movsx     bp,dl

LAB_004a124c:
  004a124c  46                    inc       esi
  004a124d  4b                    dec       ebx
  004a124e  75c5                  jne       short 004A1215h
  004a1250  e946010000            jmp       004A139Bh
  004a1255  8b3db4876000          mov       edi,[6087B4h]
  004a125b  bb04000000            mov       ebx,4
  004a1260  0fbfd6                movsx     edx,si
  004a1263  8d749410              lea       esi,[esp+edx*4+10h]

LAB_004a1267:
  004a1267  8a16                  mov       dl,[esi]
  004a1269  0fbec2                movsx     eax,dl
  004a126c  8d0c80                lea       ecx,[eax+eax*4]
  004a126f  c1e103                shl       ecx,3
  004a1272  2bc8                  sub       ecx,eax
  004a1274  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004a127b  f7c100000080          test      ecx,80000000h
  004a1281  741e                  je        short 004A12A1h
  004a1283  f7c100000050          test      ecx,50000000h
  004a1289  7516                  jne       short 004A12A1h
  004a128b  8b0f                  mov       ecx,[edi]
  004a128d  c1e00d                shl       eax,0Dh
  004a1290  8a840850010f00        mov       al,[eax+ecx+0F0150h]
  004a1297  2407                  and       al,7
  004a1299  3c01                  cmp       al,1
  004a129b  7504                  jne       short 004A12A1h
  004a129d  660fbeea              movsx     bp,dl

LAB_004a12a1:
  004a12a1  46                    inc       esi
  004a12a2  4b                    dec       ebx
  004a12a3  75c2                  jne       short 004A1267h
  004a12a5  e9f1000000            jmp       004A139Bh
  004a12aa  8b3db4876000          mov       edi,[6087B4h]
  004a12b0  bb04000000            mov       ebx,4
  004a12b5  0fbfce                movsx     ecx,si
  004a12b8  8d748c10              lea       esi,[esp+ecx*4+10h]

LAB_004a12bc:
  004a12bc  8a16                  mov       dl,[esi]
  004a12be  0fbec2                movsx     eax,dl
  004a12c1  8d0c80                lea       ecx,[eax+eax*4]
  004a12c4  c1e103                shl       ecx,3
  004a12c7  2bc8                  sub       ecx,eax
  004a12c9  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004a12d0  f7c100000080          test      ecx,80000000h
  004a12d6  741e                  je        short 004A12F6h
  004a12d8  f7c100000050          test      ecx,50000000h
  004a12de  7516                  jne       short 004A12F6h
  004a12e0  8b0f                  mov       ecx,[edi]
  004a12e2  c1e00d                shl       eax,0Dh
  004a12e5  8a840850010f00        mov       al,[eax+ecx+0F0150h]
  004a12ec  2407                  and       al,7
  004a12ee  3c03                  cmp       al,3
  004a12f0  7504                  jne       short 004A12F6h
  004a12f2  660fbeea              movsx     bp,dl

LAB_004a12f6:
  004a12f6  46                    inc       esi
  004a12f7  4b                    dec       ebx
  004a12f8  75c2                  jne       short 004A12BCh
  004a12fa  e99c000000            jmp       004A139Bh
  004a12ff  8b3db4876000          mov       edi,[6087B4h]
  004a1305  bb04000000            mov       ebx,4
  004a130a  0fbfce                movsx     ecx,si
  004a130d  8d748c10              lea       esi,[esp+ecx*4+10h]

LAB_004a1311:
  004a1311  8a16                  mov       dl,[esi]
  004a1313  0fbec2                movsx     eax,dl
  004a1316  8d0c80                lea       ecx,[eax+eax*4]
  004a1319  c1e103                shl       ecx,3
  004a131c  2bc8                  sub       ecx,eax
  004a131e  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004a1325  f7c100000080          test      ecx,80000000h
  004a132b  741b                  je        short 004A1348h
  004a132d  f7c100000050          test      ecx,50000000h
  004a1333  7513                  jne       short 004A1348h
  004a1335  8b0f                  mov       ecx,[edi]
  004a1337  c1e00d                shl       eax,0Dh
  004a133a  f6840850010f0008      test      byte [eax+ecx+0F0150h],8
  004a1342  7504                  jne       short 004A1348h
  004a1344  660fbeea              movsx     bp,dl

LAB_004a1348:
  004a1348  46                    inc       esi
  004a1349  4b                    dec       ebx
  004a134a  75c5                  jne       short 004A1311h
  004a134c  eb4d                  jmp       short 004A139Bh
  004a134e  8b3db4876000          mov       edi,[6087B4h]
  004a1354  bb04000000            mov       ebx,4
  004a1359  0fbfd6                movsx     edx,si
  004a135c  8d749410              lea       esi,[esp+edx*4+10h]

LAB_004a1360:
  004a1360  8a16                  mov       dl,[esi]
  004a1362  0fbec2                movsx     eax,dl
  004a1365  8d0c80                lea       ecx,[eax+eax*4]
  004a1368  c1e103                shl       ecx,3
  004a136b  2bc8                  sub       ecx,eax
  004a136d  8b0c8dc0935e00        mov       ecx,[ecx*4+5E93C0h]
  004a1374  f7c100000080          test      ecx,80000000h
  004a137a  741b                  je        short 004A1397h
  004a137c  f7c100000050          test      ecx,50000000h
  004a1382  7513                  jne       short 004A1397h
  004a1384  8b0f                  mov       ecx,[edi]
  004a1386  c1e00d                shl       eax,0Dh
  004a1389  f6840850010f0008      test      byte [eax+ecx+0F0150h],8
  004a1391  7404                  je        short 004A1397h
  004a1393  660fbeea              movsx     bp,dl

LAB_004a1397:
  004a1397  46                    inc       esi
  004a1398  4b                    dec       ebx
  004a1399  75c5                  jne       short 004A1360h

LAB_004a139b:
  004a139b  0fbfc5                movsx     eax,bp
  004a139e  8b4c2424              mov       ecx,[esp+24h]
  004a13a2  8d1480                lea       edx,[eax+eax*4]
  004a13a5  c1e203                shl       edx,3
  004a13a8  2bd0                  sub       edx,eax
  004a13aa  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  004a13b1  89416c                mov       [ecx+6Ch],eax

LAB_004a13b4:
  004a13b4  5f                    pop       edi
  004a13b5  5e                    pop       esi
  004a13b6  5d                    pop       ebp
  004a13b7  5b                    pop       ebx
  004a13b8  83c410                add       esp,10h
  004a13bb  c3                    ret
  004a13bc  d910                  fst       dword [eax]
  004a13be  4a                    dec       edx
  004a13bf  009b134a001c          add       [ebx+1C004A13h],bl
  004a13c5  114a00                adc       [edx],ecx
  004a13c8  8e11                  mov       ss,[ecx]
  004a13ca  4a                    dec       edx
  004a13cb  0003                  add       [ebx],al
  004a13cd  124a00                adc       cl,[edx]
  004a13d0  55                    push      ebp
  004a13d1  124a00                adc       cl,[edx]
  004a13d4  aa                    stosb
  004a13d5  124a00                adc       cl,[edx]
  004a13d8  ff12                  call      dword [edx]
  004a13da  4a                    dec       edx
  004a13db  004e13                add       [esi+13h],cl
  004a13de  4a                    dec       edx
  004a13df  00                    (bad)
  (octet invalide — la plage demandee deborde probablement du code)
