Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004e2d60  section .text  offset fichier 0xe2160  2576 octets

  004e2d60  83ec44                sub       esp,44h
  004e2d63  33c0                  xor       eax,eax
  004e2d65  53                    push      ebx
  004e2d66  55                    push      ebp
  004e2d67  6689442434            mov       [esp+34h],ax
  004e2d6c  668944243c            mov       [esp+3Ch],ax
  004e2d71  6689442440            mov       [esp+40h],ax
  004e2d76  6689442442            mov       [esp+42h],ax
  004e2d7b  6689442448            mov       [esp+48h],ax
  004e2d80  56                    push      esi
  004e2d81  8b742454              mov       esi,[esp+54h]
  004e2d85  8d442430              lea       eax,[esp+30h]
  004e2d89  57                    push      edi
  004e2d8a  8d4c2438              lea       ecx,[esp+38h]
  004e2d8e  50                    push      eax
  004e2d8f  8d542434              lea       edx,[esp+34h]
  004e2d93  51                    push      ecx
  004e2d94  bf64000000            mov       edi,64h
  004e2d99  52                    push      edx
  004e2d9a  56                    push      esi
  004e2d9b  66c744244e2800        mov       word [esp+4Eh],28h
  004e2da2  66c74424503200        mov       word [esp+50h],32h
  004e2da9  66897c2452            mov       [esp+52h],di
  004e2dae  66c74424567800        mov       word [esp+56h],78h
  004e2db5  66c744245c3c00        mov       word [esp+5Ch],3Ch
  004e2dbc  66897c245e            mov       [esp+5Eh],di
  004e2dc1  66c74424625a00        mov       word [esp+62h],5Ah
  004e2dc8  e8b35b0500            call      00538980h
  004e2dcd  83c410                add       esp,10h
  004e2dd0  8d442420              lea       eax,[esp+20h]
  004e2dd4  8d4c241c              lea       ecx,[esp+1Ch]
  004e2dd8  8d542410              lea       edx,[esp+10h]
  004e2ddc  50                    push      eax
  004e2ddd  51                    push      ecx
  004e2dde  8d442420              lea       eax,[esp+20h]
  004e2de2  52                    push      edx
  004e2de3  8d4c2464              lea       ecx,[esp+64h]
  004e2de7  50                    push      eax
  004e2de8  51                    push      ecx
  004e2de9  56                    push      esi
  004e2dea  e831310500            call      00535F20h
  004e2def  8b4c2470              mov       ecx,[esp+70h]
  004e2df3  b840000000            mov       eax,40h
  004e2df8  2bc1                  sub       eax,ecx
  004e2dfa  83c418                add       esp,18h
  004e2dfd  89442414              mov       [esp+14h],eax
  004e2e01  8b44241c              mov       eax,[esp+1Ch]
  004e2e05  85c0                  test      eax,eax
  004e2e07  bd03000000            mov       ebp,3
  004e2e0c  bb01000000            mov       ebx,1
  004e2e11  0f84b8050000          je        near 004E33CFh
  004e2e17  56                    push      esi
  004e2e18  e88387ffff            call      004DB5A0h
  004e2e1d  8b06                  mov       eax,[esi]
  004e2e1f  83c404                add       esp,4
  004e2e22  a802                  test      al,2
  004e2e24  0f84b6020000          je        near 004E30E0h
  004e2e2a  f644242010            test      byte [esp+20h],10h
  004e2e2f  740d                  je        short 004E2E3Eh
  004e2e31  24fd                  and       al,0FDh
  004e2e33  c6464300              mov       byte [esi+43h],0
  004e2e37  8906                  mov       [esi],eax
  004e2e39  e991050000            jmp       004E33CFh

LAB_004e2e3e:
  004e2e3e  33c9                  xor       ecx,ecx
  004e2e40  8a4e42                mov       cl,[esi+42h]
  004e2e43  83e900                sub       ecx,0
  004e2e46  0f8482010000          je        near 004E2FCEh
  004e2e4c  49                    dec       ecx
  004e2e4d  0f857c050000          jne       near 004E33CFh
  004e2e53  8b4650                mov       eax,[esi+50h]
  004e2e56  8b4e58                mov       ecx,[esi+58h]
  004e2e59  c1f80c                sar       eax,0Ch
  004e2e5c  8bd0                  mov       edx,eax
  004e2e5e  c1f90c                sar       ecx,0Ch
  004e2e61  0fafd0                imul      edx,eax
  004e2e64  8bc1                  mov       eax,ecx
  004e2e66  0fafc1                imul      eax,ecx
  004e2e69  03d0                  add       edx,eax
  004e2e6b  52                    push      edx
  004e2e6c  e89f2b0600            call      00545A10h
  004e2e71  b908000000            mov       ecx,8
  004e2e76  66394e1a              cmp       [esi+1Ah],cx
  004e2e7a  7e1b                  jle       short 004E2E97h
  004e2e7c  66894e1a              mov       [esi+1Ah],cx
  004e2e80  8b4c2458              mov       ecx,[esp+58h]
  004e2e84  c1f903                sar       ecx,3
  004e2e87  41                    inc       ecx
  004e2e88  3bc1                  cmp       eax,ecx
  004e2e8a  7f0b                  jg        short 004E2E97h
  004e2e8c  8b06                  mov       eax,[esi]
  004e2e8e  24fd                  and       al,0FDh
  004e2e90  8906                  mov       [esi],eax
  004e2e92  e938050000            jmp       004E33CFh

LAB_004e2e97:
  004e2e97  66396e1a              cmp       [esi+1Ah],bp
  004e2e9b  0f8ee0000000          jle       near 004E2F81h
  004e2ea1  83f806                cmp       eax,6
  004e2ea4  0f8cd3000000          jl        near 004E2F7Dh
  004e2eaa  8d542448              lea       edx,[esp+48h]
  004e2eae  8d5e1c                lea       ebx,[esi+1Ch]
  004e2eb1  6a00                  push      0
  004e2eb3  52                    push      edx
  004e2eb4  53                    push      ebx
  004e2eb5  56                    push      esi
  004e2eb6  e8756d0300            call      00519C30h
  004e2ebb  8bf8                  mov       edi,eax
  004e2ebd  a1b8e05d00            mov       eax,[5DE0B8h]
  004e2ec2  83c410                add       esp,10h
  004e2ec5  85c0                  test      eax,eax
  004e2ec7  0f84b0000000          je        near 004E2F7Dh
  004e2ecd  8b0f                  mov       ecx,[edi]
  004e2ecf  8d6f1c                lea       ebp,[edi+1Ch]
  004e2ed2  55                    push      ebp
  004e2ed3  6800180000            push      1800h
  004e2ed8  83c940                or        ecx,40h
  004e2edb  6800030000            push      300h
  004e2ee0  6a03                  push      3
  004e2ee2  66c787960000001000    mov       word [edi+96h],10h
  004e2eeb  890f                  mov       [edi],ecx
  004e2eed  e82eac0500            call      0053DB20h
  004e2ef2  0fbf4632              movsx     eax,word [esi+32h]
  004e2ef6  83c410                add       esp,10h
  004e2ef9  50                    push      eax
  004e2efa  e81d2b0600            call      00545A1Ch
  004e2eff  8d0440                lea       eax,[eax+eax*2]
  004e2f02  8d0c80                lea       ecx,[eax+eax*4]
  004e2f05  d1e1                  shl       ecx,1
  004e2f07  894f50                mov       [edi+50h],ecx
  004e2f0a  0fbf5632              movsx     edx,word [esi+32h]
  004e2f0e  52                    push      edx
  004e2f0f  e8022b0600            call      00545A16h
  004e2f14  8d0440                lea       eax,[eax+eax*2]
  004e2f17  8d0480                lea       eax,[eax+eax*4]
  004e2f1a  d1e0                  shl       eax,1
  004e2f1c  894758                mov       [edi+58h],eax
  004e2f1f  8b07                  mov       eax,[edi]
  004e2f21  a801                  test      al,1
  004e2f23  750b                  jne       short 004E2F30h
  004e2f25  0c01                  or        al,1
  004e2f27  c7475400800100        mov       dword [edi+54h],18000h
  004e2f2e  8907                  mov       [edi],eax

LAB_004e2f30:
  004e2f30  8b0b                  mov       ecx,[ebx]
  004e2f32  8b5620                mov       edx,[esi+20h]
  004e2f35  8b4624                mov       eax,[esi+24h]
  004e2f38  894c2424              mov       [esp+24h],ecx
  004e2f3c  8b4d00                mov       ecx,[ebp]
  004e2f3f  89542428              mov       [esp+28h],edx
  004e2f43  890b                  mov       [ebx],ecx
  004e2f45  8b5720                mov       edx,[edi+20h]
  004e2f48  6aff                  push      0FFFFFFFFh
  004e2f4a  89442430              mov       [esp+30h],eax
  004e2f4e  895620                mov       [esi+20h],edx
  004e2f51  8b4724                mov       eax,[edi+24h]
  004e2f54  6a00                  push      0
  004e2f56  6a39                  push      39h
  004e2f58  56                    push      esi
  004e2f59  894624                mov       [esi+24h],eax
  004e2f5c  e86f7af9ff            call      0047A9D0h
  004e2f61  8b4c2434              mov       ecx,[esp+34h]
  004e2f65  8b542438              mov       edx,[esp+38h]
  004e2f69  8b44243c              mov       eax,[esp+3Ch]
  004e2f6d  83c410                add       esp,10h
  004e2f70  890b                  mov       [ebx],ecx
  004e2f72  895620                mov       [esi+20h],edx
  004e2f75  894624                mov       [esi+24h],eax
  004e2f78  bd03000000            mov       ebp,3

LAB_004e2f7d:
  004e2f7d  66396e1a              cmp       [esi+1Ah],bp

LAB_004e2f81:
  004e2f81  0f8541040000          jne       near 004E33C8h
  004e2f87  8b4c2458              mov       ecx,[esp+58h]
  004e2f8b  0fbf5632              movsx     edx,word [esi+32h]
  004e2f8f  81c160030000          add       ecx,360h
  004e2f95  52                    push      edx
  004e2f96  c1e107                shl       ecx,7
  004e2f99  894e5c                mov       [esi+5Ch],ecx
  004e2f9c  e87b2a0600            call      00545A1Ch
  004e2fa1  0faf465c              imul      eax,[esi+5Ch]
  004e2fa5  8b5e50                mov       ebx,[esi+50h]
  004e2fa8  c1f80c                sar       eax,0Ch
  004e2fab  03d8                  add       ebx,eax
  004e2fad  0fbf4632              movsx     eax,word [esi+32h]
  004e2fb1  50                    push      eax
  004e2fb2  895e50                mov       [esi+50h],ebx
  004e2fb5  e85c2a0600            call      00545A16h
  004e2fba  0faf465c              imul      eax,[esi+5Ch]
  004e2fbe  8b4e58                mov       ecx,[esi+58h]
  004e2fc1  c1f80c                sar       eax,0Ch
  004e2fc4  03c8                  add       ecx,eax
  004e2fc6  894e58                mov       [esi+58h],ecx
  004e2fc9  e9fa030000            jmp       004E33C8h

LAB_004e2fce:
  004e2fce  668b4e1a              mov       cx,[esi+1Ah]
  004e2fd2  6683f90e              cmp       cx,0Eh
  004e2fd6  7c60                  jl        short 004E3038h
  004e2fd8  6683f910              cmp       cx,10h
  004e2fdc  7f5a                  jg        short 004E3038h
  004e2fde  8d4c243c              lea       ecx,[esp+3Ch]
  004e2fe2  6a00                  push      0
  004e2fe4  8d561c                lea       edx,[esi+1Ch]
  004e2fe7  51                    push      ecx
  004e2fe8  52                    push      edx
  004e2fe9  56                    push      esi
  004e2fea  e8416c0300            call      00519C30h
  004e2fef  8bf8                  mov       edi,eax
  004e2ff1  a1b8e05d00            mov       eax,[5DE0B8h]
  004e2ff6  83c410                add       esp,10h
  004e2ff9  85c0                  test      eax,eax
  004e2ffb  7431                  je        short 004E302Eh
  004e2ffd  8b1f                  mov       ebx,[edi]
  004e2fff  57                    push      edi
  004e3000  83cb40                or        ebx,40h
  004e3003  56                    push      esi
  004e3004  66c787960000001000    mov       word [edi+96h],10h
  004e300d  891f                  mov       [edi],ebx
  004e300f  e86c6b0500            call      00539B80h
  004e3014  83c408                add       esp,8
  004e3017  83c71c                add       edi,1Ch
  004e301a  57                    push      edi
  004e301b  6800180000            push      1800h
  004e3020  6800030000            push      300h
  004e3025  55                    push      ebp
  004e3026  e8f5aa0500            call      0053DB20h
  004e302b  83c410                add       esp,10h

LAB_004e302e:
  004e302e  8b06                  mov       eax,[esi]
  004e3030  a801                  test      al,1
  004e3032  740c                  je        short 004E3040h
  004e3034  66ff4e1a              dec       word [esi+1Ah]

LAB_004e3038:
  004e3038  a801                  test      al,1
  004e303a  0f858f030000          jne       near 004E33CFh

LAB_004e3040:
  004e3040  668b561a              mov       dx,[esi+1Ah]
  004e3044  8b7c2458              mov       edi,[esp+58h]
  004e3048  6683fa0d              cmp       dx,0Dh
  004e304c  7d31                  jge       short 004E307Fh
  004e304e  8b4c2410              mov       ecx,[esp+10h]
  004e3052  8d4720                lea       eax,[edi+20h]
  004e3055  8d1c08                lea       ebx,[eax+ecx]
  004e3058  8d2c00                lea       ebp,[eax+eax]
  004e305b  81e3ff0f0000          and       ebx,0FFFh
  004e3061  3bdd                  cmp       ebx,ebp
  004e3063  7f06                  jg        short 004E306Bh
  004e3065  66014e32              add       [esi+32h],cx
  004e3069  eb14                  jmp       short 004E307Fh

LAB_004e306b:
  004e306b  81e1ff0f0000          and       ecx,0FFFh
  004e3071  81f900080000          cmp       ecx,800h
  004e3077  7c02                  jl        short 004E307Bh
  004e3079  f7d8                  neg       eax

LAB_004e307b:
  004e307b  66014632              add       [esi+32h],ax

LAB_004e307f:
  004e307f  6683fa0d              cmp       dx,0Dh
  004e3083  7545                  jne       short 004E30CAh
  004e3085  0fbf4632              movsx     eax,word [esi+32h]
  004e3089  81c780020000          add       edi,280h
  004e308f  50                    push      eax
  004e3090  c1e707                shl       edi,7
  004e3093  897e5c                mov       [esi+5Ch],edi
  004e3096  e881290600            call      00545A1Ch
  004e309b  0faf465c              imul      eax,[esi+5Ch]
  004e309f  0fbf4e32              movsx     ecx,word [esi+32h]
  004e30a3  8b5e50                mov       ebx,[esi+50h]
  004e30a6  51                    push      ecx
  004e30a7  c1f80c                sar       eax,0Ch
  004e30aa  03d8                  add       ebx,eax
  004e30ac  895e50                mov       [esi+50h],ebx
  004e30af  e862290600            call      00545A16h
  004e30b4  0faf465c              imul      eax,[esi+5Ch]
  004e30b8  8b4e58                mov       ecx,[esi+58h]
  004e30bb  c7465c00000000        mov       dword [esi+5Ch],0
  004e30c2  c1f80c                sar       eax,0Ch
  004e30c5  03c8                  add       ecx,eax
  004e30c7  894e58                mov       [esi+58h],ecx

LAB_004e30ca:
  004e30ca  8b06                  mov       eax,[esi]
  004e30cc  a900000002            test      eax,2000000h
  004e30d1  0f84f8020000          je        near 004E33CFh
  004e30d7  24fd                  and       al,0FDh
  004e30d9  8906                  mov       [esi],eax
  004e30db  e9ef020000            jmp       004E33CFh

LAB_004e30e0:
  004e30e0  a810                  test      al,10h
  004e30e2  7425                  je        short 004E3109h
  004e30e4  a900000002            test      eax,2000000h
  004e30e9  7412                  je        short 004E30FDh
  004e30eb  8a4e43                mov       cl,[esi+43h]
  004e30ee  84c9                  test      cl,cl
  004e30f0  740b                  je        short 004E30FDh
  004e30f2  fec9                  dec       cl
  004e30f4  884e43                mov       [esi+43h],cl
  004e30f7  7504                  jne       short 004E30FDh
  004e30f9  24ef                  and       al,0EFh
  004e30fb  8906                  mov       [esi],eax

LAB_004e30fd:
  004e30fd  8b542458              mov       edx,[esp+58h]
  004e3101  8d4240                lea       eax,[edx+40h]
  004e3104  e939020000            jmp       004E3342h

LAB_004e3109:
  004e3109  a904080000            test      eax,804h
  004e310e  0f85bb020000          jne       near 004E33CFh
  004e3114  a801                  test      al,1
  004e3116  7430                  je        short 004E3148h
  004e3118  e823400600            call      00547140h
  004e311d  99                    cdq
  004e311e  f7ff                  idiv      edi
  004e3120  8b442458              mov       eax,[esp+58h]
  004e3124  c1f802                sar       eax,2
  004e3127  3bd0                  cmp       edx,eax
  004e3129  0f8da0020000          jge       near 004E33CFh
  004e312f  8b0e                  mov       ecx,[esi]
  004e3131  896e18                mov       [esi+18h],ebp
  004e3134  81e1fffffffe          and       ecx,0FEFFFFFFh
  004e313a  c6464200              mov       byte [esi+42h],0
  004e313e  83c902                or        ecx,2
  004e3141  890e                  mov       [esi],ecx
  004e3143  e987020000            jmp       004E33CFh

LAB_004e3148:
  004e3148  33c9                  xor       ecx,ecx
  004e314a  8a4e42                mov       cl,[esi+42h]
  004e314d  83f904                cmp       ecx,4
  004e3150  0f8779020000          ja        near 004E33CFh
  004e3156  ff248d54374e00        jmp       dword [ecx*4+4E3754h]
  004e315d  33c9                  xor       ecx,ecx
  004e315f  66394e18              cmp       [esi+18h],cx
  004e3163  740a                  je        short 004E316Fh
  004e3165  0d00000001            or        eax,1000000h
  004e316a  894e18                mov       [esi+18h],ecx
  004e316d  8906                  mov       [esi],eax

LAB_004e316f:
  004e316f  817c24182c010000      cmp       dword [esp+18h],12Ch
  004e3177  7d09                  jge       short 004E3182h
  004e3179  c6464300              mov       byte [esi+43h],0
  004e317d  e94d020000            jmp       004E33CFh

LAB_004e3182:
  004e3182  8a4643                mov       al,[esi+43h]
  004e3185  84c0                  test      al,al
  004e3187  7405                  je        short 004E318Eh
  004e3189  fec8                  dec       al
  004e318b  884643                mov       [esi+43h],al

LAB_004e318e:
  004e318e  894e5c                mov       [esi+5Ch],ecx
  004e3191  e939020000            jmp       004E33CFh
  004e3196  66395e18              cmp       [esi+18h],bx
  004e319a  740a                  je        short 004E31A6h
  004e319c  0d00000001            or        eax,1000000h
  004e31a1  895e18                mov       [esi+18h],ebx
  004e31a4  8906                  mov       [esi],eax

LAB_004e31a6:
  004e31a6  8b542458              mov       edx,[esp+58h]
  004e31aa  8b4c2410              mov       ecx,[esp+10h]
  004e31ae  8d841280000000        lea       eax,[edx+edx+80h]
  004e31b5  8d1408                lea       edx,[eax+ecx]
  004e31b8  8d3c00                lea       edi,[eax+eax]
  004e31bb  81e2ff0f0000          and       edx,0FFFh
  004e31c1  3bd7                  cmp       edx,edi
  004e31c3  7f0d                  jg        short 004E31D2h
  004e31c5  66014e32              add       [esi+32h],cx
  004e31c9  c6464300              mov       byte [esi+43h],0
  004e31cd  e9f6010000            jmp       004E33C8h

LAB_004e31d2:
  004e31d2  81e1ff0f0000          and       ecx,0FFFh
  004e31d8  81f900080000          cmp       ecx,800h
  004e31de  7c02                  jl        short 004E31E2h
  004e31e0  f7d8                  neg       eax

LAB_004e31e2:
  004e31e2  66014632              add       [esi+32h],ax
  004e31e6  e9dd010000            jmp       004E33C8h
  004e31eb  66395e18              cmp       [esi+18h],bx
  004e31ef  740a                  je        short 004E31FBh
  004e31f1  0d00000001            or        eax,1000000h
  004e31f6  895e18                mov       [esi+18h],ebx
  004e31f9  8906                  mov       [esi],eax

LAB_004e31fb:
  004e31fb  0fbf4610              movsx     eax,word [esi+10h]
  004e31ff  39442418              cmp       [esp+18h],eax
  004e3203  7d09                  jge       short 004E320Eh
  004e3205  c6464300              mov       byte [esi+43h],0
  004e3209  e9c1010000            jmp       004E33CFh

LAB_004e320e:
  004e320e  8a4643                mov       al,[esi+43h]
  004e3211  84c0                  test      al,al
  004e3213  7405                  je        short 004E321Ah
  004e3215  fec8                  dec       al
  004e3217  884643                mov       [esi+43h],al

LAB_004e321a:
  004e321a  8b542458              mov       edx,[esp+58h]
  004e321e  8b4c2410              mov       ecx,[esp+10h]
  004e3222  8d841280000000        lea       eax,[edx+edx+80h]
  004e3229  8d3c08                lea       edi,[eax+ecx]
  004e322c  8d1c00                lea       ebx,[eax+eax]
  004e322f  81e7ff0f0000          and       edi,0FFFh
  004e3235  3bfb                  cmp       edi,ebx
  004e3237  7f15                  jg        short 004E324Eh
  004e3239  66014e32              add       [esi+32h],cx
  004e323d  81c200010000          add       edx,100h
  004e3243  c1e207                shl       edx,7
  004e3246  89565c                mov       [esi+5Ch],edx
  004e3249  e981010000            jmp       004E33CFh

LAB_004e324e:
  004e324e  81e1ff0f0000          and       ecx,0FFFh
  004e3254  81f900080000          cmp       ecx,800h
  004e325a  7c02                  jl        short 004E325Eh
  004e325c  f7d8                  neg       eax

LAB_004e325e:
  004e325e  66014632              add       [esi+32h],ax
  004e3262  81c200010000          add       edx,100h
  004e3268  c1e207                shl       edx,7
  004e326b  89565c                mov       [esi+5Ch],edx
  004e326e  e95c010000            jmp       004E33CFh
  004e3273  8a4e43                mov       cl,[esi+43h]
  004e3276  84c9                  test      cl,cl
  004e3278  7405                  je        short 004E327Fh
  004e327a  fec9                  dec       cl
  004e327c  884e43                mov       [esi+43h],cl

LAB_004e327f:
  004e327f  66395e18              cmp       [esi+18h],bx
  004e3283  740a                  je        short 004E328Fh
  004e3285  0d00000001            or        eax,1000000h
  004e328a  895e18                mov       [esi+18h],ebx
  004e328d  8906                  mov       [esi],eax

LAB_004e328f:
  004e328f  e8ac3e0600            call      00547140h
  004e3294  99                    cdq
  004e3295  f7ff                  idiv      edi
  004e3297  8b442458              mov       eax,[esp+58h]
  004e329b  8bc8                  mov       ecx,eax
  004e329d  c1f904                sar       ecx,4
  004e32a0  3bd1                  cmp       edx,ecx
  004e32a2  7d4f                  jge       short 004E32F3h
  004e32a4  0fbf5610              movsx     edx,word [esi+10h]
  004e32a8  0540010000            add       eax,140h
  004e32ad  52                    push      edx
  004e32ae  c1e007                shl       eax,7
  004e32b1  89465c                mov       [esi+5Ch],eax
  004e32b4  e863270600            call      00545A1Ch
  004e32b9  0faf465c              imul      eax,[esi+5Ch]
  004e32bd  8b6e50                mov       ebp,[esi+50h]
  004e32c0  c1f80c                sar       eax,0Ch
  004e32c3  03e8                  add       ebp,eax
  004e32c5  0fbf4610              movsx     eax,word [esi+10h]
  004e32c9  50                    push      eax
  004e32ca  896e50                mov       [esi+50h],ebp
  004e32cd  e844270600            call      00545A16h
  004e32d2  0faf465c              imul      eax,[esi+5Ch]
  004e32d6  8b4e58                mov       ecx,[esi+58h]
  004e32d9  c7465400400100        mov       dword [esi+54h],14000h
  004e32e0  c1f80c                sar       eax,0Ch
  004e32e3  03c8                  add       ecx,eax
  004e32e5  8b06                  mov       eax,[esi]
  004e32e7  0bc3                  or        eax,ebx
  004e32e9  894e58                mov       [esi+58h],ecx
  004e32ec  8906                  mov       [esi],eax
  004e32ee  e9dc000000            jmp       004E33CFh

LAB_004e32f3:
  004e32f3  0fbf4e10              movsx     ecx,word [esi+10h]
  004e32f7  05c0000000            add       eax,0C0h
  004e32fc  51                    push      ecx
  004e32fd  c1e007                shl       eax,7
  004e3300  89465c                mov       [esi+5Ch],eax
  004e3303  e814270600            call      00545A1Ch
  004e3308  0faf465c              imul      eax,[esi+5Ch]
  004e330c  0fbf5610              movsx     edx,word [esi+10h]
  004e3310  8b5e1c                mov       ebx,[esi+1Ch]
  004e3313  52                    push      edx
  004e3314  c1f80c                sar       eax,0Ch
  004e3317  03d8                  add       ebx,eax
  004e3319  895e1c                mov       [esi+1Ch],ebx
  004e331c  e8f5260600            call      00545A16h
  004e3321  0faf465c              imul      eax,[esi+5Ch]
  004e3325  8b4e24                mov       ecx,[esi+24h]
  004e3328  c7465c00000000        mov       dword [esi+5Ch],0
  004e332f  c1f80c                sar       eax,0Ch
  004e3332  03c8                  add       ecx,eax
  004e3334  8b442458              mov       eax,[esp+58h]
  004e3338  894e24                mov       [esi+24h],ecx
  004e333b  8d048532000000        lea       eax,[eax*4+32h]

LAB_004e3342:
  004e3342  8b4c2410              mov       ecx,[esp+10h]
  004e3346  8d3c00                lea       edi,[eax+eax]
  004e3349  8d1408                lea       edx,[eax+ecx]
  004e334c  81e2ff0f0000          and       edx,0FFFh
  004e3352  3bd7                  cmp       edx,edi
  004e3354  7f06                  jg        short 004E335Ch
  004e3356  66014e32              add       [esi+32h],cx
  004e335a  eb73                  jmp       short 004E33CFh

LAB_004e335c:
  004e335c  81e1ff0f0000          and       ecx,0FFFh
  004e3362  81f900080000          cmp       ecx,800h
  004e3368  7c02                  jl        short 004E336Ch
  004e336a  f7d8                  neg       eax

LAB_004e336c:
  004e336c  66014632              add       [esi+32h],ax
  004e3370  eb5d                  jmp       short 004E33CFh
  004e3372  25fffffffe            and       eax,0FEFFFFFFh
  004e3377  8b7c241c              mov       edi,[esp+1Ch]
  004e337b  0bc3                  or        eax,ebx
  004e337d  c7461805000000        mov       dword [esi+18h],5
  004e3384  8906                  mov       [esi],eax
  004e3386  8b442458              mov       eax,[esp+58h]
  004e338a  0580020000            add       eax,280h
  004e338f  c1e007                shl       eax,7
  004e3392  0fbf4e10              movsx     ecx,word [esi+10h]
  004e3396  0faff8                imul      edi,eax
  004e3399  51                    push      ecx
  004e339a  89465c                mov       [esi+5Ch],eax
  004e339d  c1ff0c                sar       edi,0Ch
  004e33a0  e877260600            call      00545A1Ch
  004e33a5  0fafc7                imul      eax,edi
  004e33a8  0fbf5610              movsx     edx,word [esi+10h]
  004e33ac  c1f80c                sar       eax,0Ch
  004e33af  52                    push      edx
  004e33b0  894650                mov       [esi+50h],eax
  004e33b3  e85e260600            call      00545A16h
  004e33b8  0fafc7                imul      eax,edi
  004e33bb  c1f80c                sar       eax,0Ch
  004e33be  894658                mov       [esi+58h],eax
  004e33c1  c7465400c00100        mov       dword [esi+54h],1C000h

LAB_004e33c8:
  004e33c8  c7465c00000000        mov       dword [esi+5Ch],0

LAB_004e33cf:
  004e33cf  8b44241c              mov       eax,[esp+1Ch]
  004e33d3  50                    push      eax
  004e33d4  56                    push      esi
  004e33d5  e8864dfaff            call      00488160h
  004e33da  83c408                add       esp,8
  004e33dd  56                    push      esi
  004e33de  e85d050500            call      00533940h
  004e33e3  83c404                add       esp,4
  004e33e6  56                    push      esi
  004e33e7  e804c7ffff            call      004DFAF0h
  004e33ec  8b4c2434              mov       ecx,[esp+34h]
  004e33f0  83c404                add       esp,4
  004e33f3  51                    push      ecx
  004e33f4  56                    push      esi
  004e33f5  e876030000            call      004E3770h
  004e33fa  8b442424              mov       eax,[esp+24h]
  004e33fe  83c408                add       esp,8
  004e3401  85c0                  test      eax,eax
  004e3403  0f8441030000          je        near 004E374Ah
  004e3409  f70617080000          test      dword [esi],817h
  004e340f  0f8535030000          jne       near 004E374Ah
  004e3415  8a4643                mov       al,[esi+43h]
  004e3418  84c0                  test      al,al
  004e341a  0f852a030000          jne       near 004E374Ah
  004e3420  8b466c                mov       eax,[esi+6Ch]
  004e3423  8b5e1c                mov       ebx,[esi+1Ch]
  004e3426  8b4e24                mov       ecx,[esi+24h]
  004e3429  8b781c                mov       edi,[eax+1Ch]
  004e342c  8b6824                mov       ebp,[eax+24h]
  004e342f  2bfb                  sub       edi,ebx
  004e3431  2be9                  sub       ebp,ecx
  004e3433  c1ff0c                sar       edi,0Ch
  004e3436  c1fd0c                sar       ebp,0Ch
  004e3439  8bd7                  mov       edx,edi
  004e343b  8bc5                  mov       eax,ebp
  004e343d  0fafd7                imul      edx,edi
  004e3440  0fafc5                imul      eax,ebp
  004e3443  03d0                  add       edx,eax
  004e3445  52                    push      edx
  004e3446  e8c5250600            call      00545A10h
  004e344b  55                    push      ebp
  004e344c  57                    push      edi
  004e344d  89442420              mov       [esp+20h],eax
  004e3451  e8cc250600            call      00545A22h
  004e3456  668b4e32              mov       cx,[esi+32h]
  004e345a  2bc1                  sub       eax,ecx
  004e345c  25ff0f0000            and       eax,0FFFh
  004e3461  89442410              mov       [esp+10h],eax
  004e3465  8b442418              mov       eax,[esp+18h]
  004e3469  3dfa000000            cmp       eax,0FAh
  004e346e  0f8dae010000          jge       near 004E3622h
  004e3474  e8c73c0600            call      00547140h
  004e3479  99                    cdq
  004e347a  b964000000            mov       ecx,64h
  004e347f  f7f9                  idiv      ecx
  004e3481  83fa32                cmp       edx,32h
  004e3484  0f8dd0000000          jge       near 004E355Ah
  004e348a  8b7c2414              mov       edi,[esp+14h]
  004e348e  8b542410              mov       edx,[esp+10h]
  004e3492  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004e3499  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004e34a0  25ff0f0000            and       eax,0FFFh
  004e34a5  3bc1                  cmp       eax,ecx
  004e34a7  7f41                  jg        short 004E34EAh
  004e34a9  e8923c0600            call      00547140h
  004e34ae  99                    cdq
  004e34af  8d4c3f01              lea       ecx,[edi+edi+1]
  004e34b3  f7f9                  idiv      ecx
  004e34b5  8b442418              mov       eax,[esp+18h]
  004e34b9  81c2c8000000          add       edx,0C8h
  004e34bf  3bc2                  cmp       eax,edx
  004e34c1  7d27                  jge       short 004E34EAh
  004e34c3  8b16                  mov       edx,[esi]
  004e34c5  c7461803000000        mov       dword [esi+18h],3
  004e34cc  81e2fffffffe          and       edx,0FEFFFFFFh
  004e34d2  c6464200              mov       byte [esi+42h],0
  004e34d6  83ca02                or        edx,2
  004e34d9  c7465c00000000        mov       dword [esi+5Ch],0
  004e34e0  8916                  mov       [esi],edx
  004e34e2  5f                    pop       edi
  004e34e3  5e                    pop       esi
  004e34e4  5d                    pop       ebp
  004e34e5  5b                    pop       ebx
  004e34e6  83c444                add       esp,44h
  004e34e9  c3                    ret

LAB_004e34ea:
  004e34ea  8b442410              mov       eax,[esp+10h]
  004e34ee  8d14fd00040000        lea       edx,[edi*8+400h]
  004e34f5  8d8cb800020000        lea       ecx,[eax+edi*4+200h]
  004e34fc  81e1ff0f0000          and       ecx,0FFFh
  004e3502  3bca                  cmp       ecx,edx
  004e3504  0f8f08020000          jg        near 004E3712h
  004e350a  e8313c0600            call      00547140h
  004e350f  99                    cdq
  004e3510  b964000000            mov       ecx,64h
  004e3515  f7f9                  idiv      ecx
  004e3517  d1ff                  sar       edi,1
  004e3519  83c720                add       edi,20h
  004e351c  3bd7                  cmp       edx,edi
  004e351e  0f8dee010000          jge       near 004E3712h
  004e3524  8b16                  mov       edx,[esi]
  004e3526  c7461804000000        mov       dword [esi+18h],4
  004e352d  81e2fffffffe          and       edx,0FEFFFFFFh
  004e3533  83ca10                or        edx,10h
  004e3536  8916                  mov       [esi],edx
  004e3538  e8033c0600            call      00547140h
  004e353d  99                    cdq
  004e353e  b905000000            mov       ecx,5
  004e3543  c7465c00000000        mov       dword [esi+5Ch],0
  004e354a  f7f9                  idiv      ecx
  004e354c  80c20a                add       dl,0Ah
  004e354f  885643                mov       [esi+43h],dl
  004e3552  5f                    pop       edi
  004e3553  5e                    pop       esi
  004e3554  5d                    pop       ebp
  004e3555  5b                    pop       ebx
  004e3556  83c444                add       esp,44h
  004e3559  c3                    ret

LAB_004e355a:
  004e355a  83fa46                cmp       edx,46h
  004e355d  7d28                  jge       short 004E3587h
  004e355f  8b442458              mov       eax,[esp+58h]
  004e3563  8b4c2418              mov       ecx,[esp+18h]
  004e3567  83c064                add       eax,64h
  004e356a  3bc8                  cmp       ecx,eax
  004e356c  7e19                  jle       short 004E3587h
  004e356e  c6464204              mov       byte [esi+42h],4
  004e3572  c6464301              mov       byte [esi+43h],1
  004e3576  e8c53b0600            call      00547140h
  004e357b  66894610              mov       [esi+10h],ax
  004e357f  5f                    pop       edi
  004e3580  5e                    pop       esi
  004e3581  5d                    pop       ebp
  004e3582  5b                    pop       ebx
  004e3583  83c444                add       esp,44h
  004e3586  c3                    ret

LAB_004e3587:
  004e3587  83fa5a                cmp       edx,5Ah
  004e358a  7d40                  jge       short 004E35CCh
  004e358c  8b4c2410              mov       ecx,[esp+10h]
  004e3590  8b7c2414              mov       edi,[esp+14h]
  004e3594  8d94b900010000        lea       edx,[ecx+edi*4+100h]
  004e359b  8d04fd00020000        lea       eax,[edi*8+200h]
  004e35a2  81e2ff0f0000          and       edx,0FFFh
  004e35a8  3bd0                  cmp       edx,eax
  004e35aa  7f24                  jg        short 004E35D0h
  004e35ac  8b0e                  mov       ecx,[esi]
  004e35ae  c7461804000000        mov       dword [esi+18h],4
  004e35b5  81e1fffffffe          and       ecx,0FEFFFFFFh
  004e35bb  c6464201              mov       byte [esi+42h],1
  004e35bf  83c902                or        ecx,2
  004e35c2  890e                  mov       [esi],ecx
  004e35c4  5f                    pop       edi
  004e35c5  5e                    pop       esi
  004e35c6  5d                    pop       ebp
  004e35c7  5b                    pop       ebx
  004e35c8  83c444                add       esp,44h
  004e35cb  c3                    ret

LAB_004e35cc:
  004e35cc  8b7c2414              mov       edi,[esp+14h]

LAB_004e35d0:
  004e35d0  c6464203              mov       byte [esi+42h],3
  004e35d4  e8673b0600            call      00547140h
  004e35d9  99                    cdq
  004e35da  b90a000000            mov       ecx,0Ah
  004e35df  f7f9                  idiv      ecx
  004e35e1  8b4c2458              mov       ecx,[esp+58h]
  004e35e5  8bc7                  mov       eax,edi
  004e35e7  d1f8                  sar       eax,1
  004e35e9  8db980000000          lea       edi,[ecx+80h]
  004e35ef  c1e704                shl       edi,4
  004e35f2  02d0                  add       dl,al
  004e35f4  80c214                add       dl,14h
  004e35f7  885643                mov       [esi+43h],dl
  004e35fa  e8413b0600            call      00547140h
  004e35ff  99                    cdq
  004e3600  f7ff                  idiv      edi
  004e3602  d1ff                  sar       edi,1
  004e3604  2bd7                  sub       edx,edi
  004e3606  8b7c2410              mov       edi,[esp+10h]
  004e360a  03d7                  add       edx,edi
  004e360c  66035632              add       dx,[esi+32h]
  004e3610  81c200080000          add       edx,800h
  004e3616  66895610              mov       [esi+10h],dx
  004e361a  5f                    pop       edi
  004e361b  5e                    pop       esi
  004e361c  5d                    pop       ebp
  004e361d  5b                    pop       ebx
  004e361e  83c444                add       esp,44h
  004e3621  c3                    ret

LAB_004e3622:
  004e3622  3df4010000            cmp       eax,1F4h
  004e3627  0f8da7000000          jge       near 004E36D4h
  004e362d  e80e3b0600            call      00547140h
  004e3632  99                    cdq
  004e3633  b964000000            mov       ecx,64h
  004e3638  f7f9                  idiv      ecx
  004e363a  83fa2d                cmp       edx,2Dh
  004e363d  7d10                  jge       short 004E364Fh
  004e363f  c6464201              mov       byte [esi+42h],1
  004e3643  c6464301              mov       byte [esi+43h],1
  004e3647  5f                    pop       edi
  004e3648  5e                    pop       esi
  004e3649  5d                    pop       ebp
  004e364a  5b                    pop       ebx
  004e364b  83c444                add       esp,44h
  004e364e  c3                    ret

LAB_004e364f:
  004e364f  83fa46                cmp       edx,46h
  004e3652  7d4e                  jge       short 004E36A2h
  004e3654  c6464203              mov       byte [esi+42h],3
  004e3658  e8e33a0600            call      00547140h
  004e365d  99                    cdq
  004e365e  b90a000000            mov       ecx,0Ah
  004e3663  f7f9                  idiv      ecx
  004e3665  8b442414              mov       eax,[esp+14h]
  004e3669  8b4c2458              mov       ecx,[esp+58h]
  004e366d  d1f8                  sar       eax,1
  004e366f  8db980000000          lea       edi,[ecx+80h]
  004e3675  c1e704                shl       edi,4
  004e3678  02d0                  add       dl,al
  004e367a  80c214                add       dl,14h
  004e367d  885643                mov       [esi+43h],dl
  004e3680  e8bb3a0600            call      00547140h
  004e3685  99                    cdq
  004e3686  f7ff                  idiv      edi
  004e3688  8b4c2410              mov       ecx,[esp+10h]
  004e368c  d1ff                  sar       edi,1
  004e368e  2bd7                  sub       edx,edi
  004e3690  03d1                  add       edx,ecx
  004e3692  66035632              add       dx,[esi+32h]
  004e3696  66895610              mov       [esi+10h],dx
  004e369a  5f                    pop       edi
  004e369b  5e                    pop       esi
  004e369c  5d                    pop       ebp
  004e369d  5b                    pop       ebx
  004e369e  83c444                add       esp,44h
  004e36a1  c3                    ret

LAB_004e36a2:
  004e36a2  83fa5a                cmp       edx,5Ah
  004e36a5  7d3f                  jge       short 004E36E6h
  004e36a7  c6464202              mov       byte [esi+42h],2
  004e36ab  e8903a0600            call      00547140h
  004e36b0  99                    cdq
  004e36b1  b90a000000            mov       ecx,0Ah
  004e36b6  66c746106400          mov       word [esi+10h],64h
  004e36bc  f7f9                  idiv      ecx
  004e36be  8b442414              mov       eax,[esp+14h]
  004e36c2  d1f8                  sar       eax,1
  004e36c4  02d0                  add       dl,al
  004e36c6  80c214                add       dl,14h
  004e36c9  885643                mov       [esi+43h],dl
  004e36cc  5f                    pop       edi
  004e36cd  5e                    pop       esi
  004e36ce  5d                    pop       ebp
  004e36cf  5b                    pop       ebx
  004e36d0  83c444                add       esp,44h
  004e36d3  c3                    ret

LAB_004e36d4:
  004e36d4  e8673a0600            call      00547140h
  004e36d9  99                    cdq
  004e36da  b964000000            mov       ecx,64h
  004e36df  f7f9                  idiv      ecx
  004e36e1  83fa1e                cmp       edx,1Eh
  004e36e4  7d27                  jge       short 004E370Dh

LAB_004e36e6:
  004e36e6  c6464200              mov       byte [esi+42h],0
  004e36ea  e8513a0600            call      00547140h
  004e36ef  99                    cdq
  004e36f0  b90a000000            mov       ecx,0Ah
  004e36f5  f7f9                  idiv      ecx
  004e36f7  8b442414              mov       eax,[esp+14h]
  004e36fb  d1f8                  sar       eax,1
  004e36fd  02d0                  add       dl,al
  004e36ff  80c214                add       dl,14h
  004e3702  885643                mov       [esi+43h],dl
  004e3705  5f                    pop       edi
  004e3706  5e                    pop       esi
  004e3707  5d                    pop       ebp
  004e3708  5b                    pop       ebx
  004e3709  83c444                add       esp,44h
  004e370c  c3                    ret

LAB_004e370d:
  004e370d  83fa3c                cmp       edx,3Ch
  004e3710  7d10                  jge       short 004E3722h

LAB_004e3712:
  004e3712  c6464201              mov       byte [esi+42h],1
  004e3716  c6464301              mov       byte [esi+43h],1
  004e371a  5f                    pop       edi
  004e371b  5e                    pop       esi
  004e371c  5d                    pop       ebp
  004e371d  5b                    pop       ebx
  004e371e  83c444                add       esp,44h
  004e3721  c3                    ret

LAB_004e3722:
  004e3722  c6464203              mov       byte [esi+42h],3
  004e3726  e8153a0600            call      00547140h
  004e372b  99                    cdq
  004e372c  b90a000000            mov       ecx,0Ah
  004e3731  f7f9                  idiv      ecx
  004e3733  8b442414              mov       eax,[esp+14h]
  004e3737  d1f8                  sar       eax,1
  004e3739  02d0                  add       dl,al
  004e373b  80c214                add       dl,14h
  004e373e  885643                mov       [esi+43h],dl
  004e3741  e8fa390600            call      00547140h
  004e3746  66894610              mov       [esi+10h],ax

LAB_004e374a:
  004e374a  5f                    pop       edi
  004e374b  5e                    pop       esi
  004e374c  5d                    pop       ebp
  004e374d  5b                    pop       ebx
  004e374e  83c444                add       esp,44h
  004e3751  c3                    ret
  004e3752  8bff                  mov       edi,edi
  004e3754  5d                    pop       ebp
  004e3755  314e00                xor       [esi],ecx
  004e3758  96                    xchg      esi,eax
  004e3759  314e00                xor       [esi],ecx
  004e375c  eb31                  jmp       short 004E378Fh
  004e375e  4e                    dec       esi
  004e375f  007332                add       [ebx+32h],dh
  004e3762  4e                    dec       esi
  004e3763  007233                add       [edx+33h],dh
  004e3766  4e                    dec       esi
  004e3767  009090909090          add       [eax-6F6F6F70h],dl
  004e376d  90                    nop
  004e376e  90                    nop
  004e376f  90                    nop

50 cible(s) de saut interne : 0x004e2e3e, 0x004e2e97, 0x004e2f30, 0x004e2f7d, 0x004e2f81, 0x004e2fce, 0x004e302e, 0x004e3038, 0x004e3040, 0x004e306b, 0x004e307b, 0x004e307f, 0x004e30ca, 0x004e30e0, 0x004e30fd, 0x004e3109, 0x004e3148, 0x004e316f, 0x004e3182, 0x004e318e, 0x004e31a6, 0x004e31d2, 0x004e31e2, 0x004e31fb, 0x004e320e, 0x004e321a, 0x004e324e, 0x004e325e, 0x004e327f, 0x004e328f, 0x004e32f3, 0x004e3342, 0x004e335c, 0x004e336c, 0x004e33c8, 0x004e33cf, 0x004e34ea, 0x004e355a, 0x004e3587, 0x004e35cc, 0x004e35d0, 0x004e3622, 0x004e364f, 0x004e36a2, 0x004e36d4, 0x004e36e6, 0x004e370d, 0x004e3712, 0x004e3722, 0x004e374a
