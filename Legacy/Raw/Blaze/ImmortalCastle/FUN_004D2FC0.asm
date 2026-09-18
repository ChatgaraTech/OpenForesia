C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004d2fc0  section .text  offset fichier 0xd23c0  3803 octets

  004d2fc0  81ec84000000          sub       esp,84h
  004d2fc6  53                    push      ebx
  004d2fc7  55                    push      ebp
  004d2fc8  56                    push      esi
  004d2fc9  8bb42494000000        mov       esi,[esp+94h]
  004d2fd0  b864000000            mov       eax,64h
  004d2fd5  33c9                  xor       ecx,ecx
  004d2fd7  8b5624                mov       edx,[esi+24h]
  004d2fda  8d6e1c                lea       ebp,[esi+1Ch]
  004d2fdd  668944242e            mov       [esp+2Eh],ax
  004d2fe2  6689442432            mov       [esp+32h],ax
  004d2fe7  8b4500                mov       eax,[ebp]
  004d2fea  66894c2428            mov       [esp+28h],cx
  004d2fef  66894c2430            mov       [esp+30h],cx
  004d2ff4  8b4e20                mov       ecx,[esi+20h]
  004d2ff7  8944241c              mov       [esp+1Ch],eax
  004d2ffb  8b4664                mov       eax,[esi+64h]
  004d2ffe  894c2420              mov       [esp+20h],ecx
  004d3002  8b4e60                mov       ecx,[esi+60h]
  004d3005  89542424              mov       [esp+24h],edx
  004d3009  8b567c                mov       edx,[esi+7Ch]
  004d300c  8944244c              mov       [esp+4Ch],eax
  004d3010  668b8690000000        mov       ax,[esi+90h]
  004d3017  894c2440              mov       [esp+40h],ecx
  004d301b  8b8e80000000          mov       ecx,[esi+80h]
  004d3021  89542458              mov       [esp+58h],edx
  004d3025  8b9684000000          mov       edx,[esi+84h]
  004d302b  6689442444            mov       [esp+44h],ax
  004d3030  8b8688000000          mov       eax,[esi+88h]
  004d3036  894c243c              mov       [esp+3Ch],ecx
  004d303a  8b8e8c000000          mov       ecx,[esi+8Ch]
  004d3040  89542448              mov       [esp+48h],edx
  004d3044  668b5632              mov       dx,[esi+32h]
  004d3048  89442434              mov       [esp+34h],eax
  004d304c  894c2454              mov       [esp+54h],ecx
  004d3050  8d44245c              lea       eax,[esp+5Ch]
  004d3054  57                    push      edi
  004d3055  8b7e6c                mov       edi,[esi+6Ch]
  004d3058  6689942482000000      mov       [esp+82h],dx
  004d3060  8d4c2468              lea       ecx,[esp+68h]
  004d3064  50                    push      eax
  004d3065  8d542468              lea       edx,[esp+68h]
  004d3069  51                    push      ecx
  004d306a  52                    push      edx
  004d306b  56                    push      esi
  004d306c  66c744243e3200        mov       word [esp+3Eh],32h
  004d3073  66c74424405000        mov       word [esp+40h],50h
  004d307a  e801590600            call      00538980h
  004d307f  83c410                add       esp,10h
  004d3082  8d442454              lea       eax,[esp+54h]
  004d3086  8d4c241c              lea       ecx,[esp+1Ch]
  004d308a  8d542418              lea       edx,[esp+18h]
  004d308e  50                    push      eax
  004d308f  51                    push      ecx
  004d3090  8d44241c              lea       eax,[esp+1Ch]
  004d3094  52                    push      edx
  004d3095  8d4c241c              lea       ecx,[esp+1Ch]
  004d3099  50                    push      eax
  004d309a  51                    push      ecx
  004d309b  56                    push      esi
  004d309c  e87f2e0600            call      00535F20h
  004d30a1  8b542428              mov       edx,[esp+28h]
  004d30a5  b840000000            mov       eax,40h
  004d30aa  83c418                add       esp,18h
  004d30ad  2bc2                  sub       eax,edx
  004d30af  8b542454              mov       edx,[esp+54h]
  004d30b3  8944243c              mov       [esp+3Ch],eax
  004d30b7  52                    push      edx
  004d30b8  56                    push      esi
  004d30b9  e8222d0000            call      004D5DE0h
  004d30be  8b4500                mov       eax,[ebp]
  004d30c1  8b571c                mov       edx,[edi+1Ch]
  004d30c4  8b4e20                mov       ecx,[esi+20h]
  004d30c7  8b5f20                mov       ebx,[edi+20h]
  004d30ca  2bc2                  sub       eax,edx
  004d30cc  8b5624                mov       edx,[esi+24h]
  004d30cf  2bcb                  sub       ecx,ebx
  004d30d1  8b5f24                mov       ebx,[edi+24h]
  004d30d4  83c408                add       esp,8
  004d30d7  c1f80c                sar       eax,0Ch
  004d30da  c1f90c                sar       ecx,0Ch
  004d30dd  2bd3                  sub       edx,ebx
  004d30df  8bd8                  mov       ebx,eax
  004d30e1  0fafd8                imul      ebx,eax
  004d30e4  8bc1                  mov       eax,ecx
  004d30e6  c1fa0c                sar       edx,0Ch
  004d30e9  0fafc1                imul      eax,ecx
  004d30ec  8bca                  mov       ecx,edx
  004d30ee  03d8                  add       ebx,eax
  004d30f0  0fafca                imul      ecx,edx
  004d30f3  03d9                  add       ebx,ecx
  004d30f5  53                    push      ebx
  004d30f6  e815290700            call      00545A10h
  004d30fb  8b4c241c              mov       ecx,[esp+1Ch]
  004d30ff  89442414              mov       [esp+14h],eax
  004d3103  85c9                  test      ecx,ecx
  004d3105  0f848c070000          je        near 004D3897h
  004d310b  8b06                  mov       eax,[esi]
  004d310d  0fbf5e18              movsx     ebx,word [esi+18h]
  004d3111  25fffffffd            and       eax,0FDFFFFFFh
  004d3116  8906                  mov       [esi],eax
  004d3118  8bd0                  mov       edx,eax
  004d311a  33c0                  xor       eax,eax
  004d311c  8a4604                mov       al,[esi+4]
  004d311f  f7c200000001          test      edx,1000000h
  004d3125  8b048540965e00        mov       eax,[eax*4+5E9640h]
  004d312c  8b0498                mov       eax,[eax+ebx*4]
  004d312f  741d                  je        short 004D314Eh
  004d3131  66ff461a              inc       word [esi+1Ah]
  004d3135  668b561a              mov       dx,[esi+1Ah]
  004d3139  33db                  xor       ebx,ebx
  004d313b  668b5816              mov       bx,[eax+16h]
  004d313f  0fbfd2                movsx     edx,dx
  004d3142  3bd3                  cmp       edx,ebx
  004d3144  7c2b                  jl        short 004D3171h
  004d3146  66c7461a0000          mov       word [esi+1Ah],0
  004d314c  eb23                  jmp       short 004D3171h

LAB_004d314e:
  004d314e  66ff461a              inc       word [esi+1Ah]
  004d3152  33db                  xor       ebx,ebx
  004d3154  668b5816              mov       bx,[eax+16h]
  004d3158  0fbf461a              movsx     eax,word [esi+1Ah]
  004d315c  3bc3                  cmp       eax,ebx
  004d315e  7c11                  jl        short 004D3171h
  004d3160  668b461a              mov       ax,[esi+1Ah]
  004d3164  48                    dec       eax
  004d3165  81ca00000002          or        edx,2000000h
  004d316b  6689461a              mov       [esi+1Ah],ax
  004d316f  8916                  mov       [esi],edx

LAB_004d3171:
  004d3171  8a4642                mov       al,[esi+42h]
  004d3174  3c05                  cmp       al,5
  004d3176  8b06                  mov       eax,[esi]
  004d3178  7504                  jne       short 004D317Eh
  004d317a  0c10                  or        al,10h
  004d317c  eb02                  jmp       short 004D3180h

LAB_004d317e:
  004d317e  24ef                  and       al,0EFh

LAB_004d3180:
  004d3180  8906                  mov       [esi],eax
  004d3182  8a06                  mov       al,[esi]
  004d3184  a802                  test      al,2
  004d3186  7466                  je        short 004D31EEh
  004d3188  66837e1a17            cmp       word [esi+1Ah],17h
  004d318d  755f                  jne       short 004D31EEh
  004d318f  8d4c242c              lea       ecx,[esp+2Ch]
  004d3193  6a00                  push      0
  004d3195  51                    push      ecx
  004d3196  55                    push      ebp
  004d3197  56                    push      esi
  004d3198  e8936a0400            call      00519C30h
  004d319d  8bd8                  mov       ebx,eax
  004d319f  a1b8e05d00            mov       eax,[5DE0B8h]
  004d31a4  83c410                add       esp,10h
  004d31a7  85c0                  test      eax,eax
  004d31a9  743f                  je        short 004D31EAh
  004d31ab  66837e1a17            cmp       word [esi+1Ah],17h
  004d31b0  7538                  jne       short 004D31EAh
  004d31b2  8b13                  mov       edx,[ebx]
  004d31b4  53                    push      ebx
  004d31b5  83ca40                or        edx,40h
  004d31b8  56                    push      esi
  004d31b9  66c783960000001000    mov       word [ebx+96h],10h
  004d31c2  8913                  mov       [ebx],edx
  004d31c4  e8b7690600            call      00539B80h
  004d31c9  83c408                add       esp,8
  004d31cc  83c31c                add       ebx,1Ch
  004d31cf  53                    push      ebx
  004d31d0  6800180000            push      1800h
  004d31d5  6800030000            push      300h
  004d31da  6a03                  push      3
  004d31dc  e83fa90600            call      0053DB20h
  004d31e1  8b06                  mov       eax,[esi]
  004d31e3  83c410                add       esp,10h
  004d31e6  24fd                  and       al,0FDh
  004d31e8  8906                  mov       [esi],eax

LAB_004d31ea:
  004d31ea  8b4c241c              mov       ecx,[esp+1Ch]

LAB_004d31ee:
  004d31ee  33c0                  xor       eax,eax
  004d31f0  8a4642                mov       al,[esi+42h]
  004d31f3  83f80e                cmp       eax,0Eh
  004d31f6  0f879b060000          ja        near 004D3897h
  004d31fc  ff24859c3e4d00        jmp       dword [eax*4+4D3E9Ch]
  004d3203  33c9                  xor       ecx,ecx
  004d3205  66394e12              cmp       [esi+12h],cx
  004d3209  751a                  jne       short 004D3225h
  004d320b  8b06                  mov       eax,[esi]
  004d320d  66894e18              mov       [esi+18h],cx
  004d3211  0d00000001            or        eax,1000000h
  004d3216  66894e1a              mov       [esi+1Ah],cx
  004d321a  8906                  mov       [esi],eax
  004d321c  66c746120100          mov       word [esi+12h],1
  004d3222  894e5c                mov       [esi+5Ch],ecx

LAB_004d3225:
  004d3225  0fbf5610              movsx     edx,word [esi+10h]
  004d3229  39542414              cmp       [esp+14h],edx
  004d322d  7d09                  jge       short 004D3238h
  004d322f  c6464300              mov       byte [esi+43h],0
  004d3233  e95f060000            jmp       004D3897h

LAB_004d3238:
  004d3238  8a4643                mov       al,[esi+43h]
  004d323b  84c0                  test      al,al
  004d323d  0f8451060000          je        near 004D3894h
  004d3243  fec8                  dec       al
  004d3245  884643                mov       [esi+43h],al
  004d3248  e947060000            jmp       004D3894h
  004d324d  33c9                  xor       ecx,ecx
  004d324f  66394e12              cmp       [esi+12h],cx
  004d3253  751b                  jne       short 004D3270h
  004d3255  8b16                  mov       edx,[esi]
  004d3257  b801000000            mov       eax,1
  004d325c  81ca00000001          or        edx,1000000h
  004d3262  66894618              mov       [esi+18h],ax
  004d3266  66894e1a              mov       [esi+1Ah],cx
  004d326a  8916                  mov       [esi],edx
  004d326c  66894612              mov       [esi+12h],ax

LAB_004d3270:
  004d3270  8b442418              mov       eax,[esp+18h]
  004d3274  8d90c8000000          lea       edx,[eax+0C8h]
  004d327a  81e2ff0f0000          and       edx,0FFFh
  004d3280  81fa90010000          cmp       edx,190h
  004d3286  7f09                  jg        short 004D3291h
  004d3288  c6464300              mov       byte [esi+43h],0
  004d328c  e903060000            jmp       004D3894h

LAB_004d3291:
  004d3291  25ff0f0000            and       eax,0FFFh
  004d3296  3d00080000            cmp       eax,800h
  004d329b  7d10                  jge       short 004D32ADh
  004d329d  8b442410              mov       eax,[esp+10h]
  004d32a1  83c050                add       eax,50h
  004d32a4  66014632              add       [esi+32h],ax
  004d32a8  e9e7050000            jmp       004D3894h

LAB_004d32ad:
  004d32ad  8b542410              mov       edx,[esp+10h]
  004d32b1  b8b0ffffff            mov       eax,0FFFFFFB0h
  004d32b6  2bc2                  sub       eax,edx
  004d32b8  66014632              add       [esi+32h],ax
  004d32bc  e9d3050000            jmp       004D3894h
  004d32c1  8b4c2410              mov       ecx,[esp+10h]
  004d32c5  33d2                  xor       edx,edx
  004d32c7  66395612              cmp       [esi+12h],dx
  004d32cb  b801000000            mov       eax,1
  004d32d0  7525                  jne       short 004D32F7h
  004d32d2  8b3e                  mov       edi,[esi]
  004d32d4  66894618              mov       [esi+18h],ax
  004d32d8  81cf00000001          or        edi,1000000h
  004d32de  6689561a              mov       [esi+1Ah],dx
  004d32e2  893e                  mov       [esi],edi
  004d32e4  8d3cc9                lea       edi,[ecx+ecx*8]
  004d32e7  c1e704                shl       edi,4
  004d32ea  81c700500000          add       edi,5000h
  004d32f0  66894612              mov       [esi+12h],ax
  004d32f4  897e5c                mov       [esi+5Ch],edi

LAB_004d32f7:
  004d32f7  66394612              cmp       [esi+12h],ax
  004d32fb  0f8596050000          jne       near 004D3897h
  004d3301  0fbf4610              movsx     eax,word [esi+10h]
  004d3305  39442414              cmp       [esp+14h],eax
  004d3309  7d0c                  jge       short 004D3317h
  004d330b  c6464300              mov       byte [esi+43h],0
  004d330f  89565c                mov       [esi+5Ch],edx
  004d3312  e980050000            jmp       004D3897h

LAB_004d3317:
  004d3317  8a4643                mov       al,[esi+43h]
  004d331a  84c0                  test      al,al
  004d331c  7405                  je        short 004D3323h
  004d331e  fec8                  dec       al
  004d3320  884643                mov       [esi+43h],al

LAB_004d3323:
  004d3323  8b442418              mov       eax,[esp+18h]
  004d3327  8d9000010000          lea       edx,[eax+100h]
  004d332d  81e2ff0f0000          and       edx,0FFFh
  004d3333  81fa00020000          cmp       edx,200h
  004d3339  0f8e58050000          jle       near 004D3897h
  004d333f  25ff0f0000            and       eax,0FFFh
  004d3344  3d00080000            cmp       eax,800h
  004d3349  7d0f                  jge       short 004D335Ah
  004d334b  8d8180000000          lea       eax,[ecx+80h]
  004d3351  66014632              add       [esi+32h],ax
  004d3355  e93d050000            jmp       004D3897h

LAB_004d335a:
  004d335a  b880ffffff            mov       eax,0FFFFFF80h
  004d335f  2bc1                  sub       eax,ecx
  004d3361  66014632              add       [esi+32h],ax
  004d3365  e92d050000            jmp       004D3897h
  004d336a  66837e1201            cmp       word [esi+12h],1
  004d336f  0f859d000000          jne       near 004D3412h
  004d3375  8a4643                mov       al,[esi+43h]
  004d3378  84c0                  test      al,al
  004d337a  7405                  je        short 004D3381h
  004d337c  fec8                  dec       al
  004d337e  884643                mov       [esi+43h],al

LAB_004d3381:
  004d3381  8b442410              mov       eax,[esp+10h]
  004d3385  8d04c0                lea       eax,[eax+eax*8]
  004d3388  c1e004                shl       eax,4
  004d338b  0500500000            add       eax,5000h
  004d3390  0fafc8                imul      ecx,eax
  004d3393  c1f90c                sar       ecx,0Ch
  004d3396  8bf9                  mov       edi,ecx
  004d3398  89465c                mov       [esi+5Ch],eax
  004d339b  0fbf4e10              movsx     ecx,word [esi+10h]
  004d339f  51                    push      ecx
  004d33a0  e877260700            call      00545A1Ch
  004d33a5  0fafc7                imul      eax,edi
  004d33a8  0fbf5610              movsx     edx,word [esi+10h]
  004d33ac  8b4d00                mov       ecx,[ebp]
  004d33af  52                    push      edx
  004d33b0  c1f80c                sar       eax,0Ch
  004d33b3  03c8                  add       ecx,eax
  004d33b5  894d00                mov       [ebp],ecx
  004d33b8  e859260700            call      00545A16h
  004d33bd  0fafc7                imul      eax,edi
  004d33c0  8b5e24                mov       ebx,[esi+24h]
  004d33c3  c7465c00000000        mov       dword [esi+5Ch],0
  004d33ca  c1f80c                sar       eax,0Ch
  004d33cd  03d8                  add       ebx,eax
  004d33cf  8b442418              mov       eax,[esp+18h]
  004d33d3  895e24                mov       [esi+24h],ebx
  004d33d6  8d8800010000          lea       ecx,[eax+100h]
  004d33dc  81e1ff0f0000          and       ecx,0FFFh
  004d33e2  81f900020000          cmp       ecx,200h
  004d33e8  7e28                  jle       short 004D3412h
  004d33ea  25ff0f0000            and       eax,0FFFh
  004d33ef  3d00080000            cmp       eax,800h
  004d33f4  7d0a                  jge       short 004D3400h
  004d33f6  8b542410              mov       edx,[esp+10h]
  004d33fa  8d441232              lea       eax,[edx+edx+32h]
  004d33fe  eb0e                  jmp       short 004D340Eh

LAB_004d3400:
  004d3400  8b442410              mov       eax,[esp+10h]
  004d3404  8d0c00                lea       ecx,[eax+eax]
  004d3407  b8ceffffff            mov       eax,0FFFFFFCEh
  004d340c  2bc1                  sub       eax,ecx

LAB_004d340e:
  004d340e  66014632              add       [esi+32h],ax

LAB_004d3412:
  004d3412  66837e1200            cmp       word [esi+12h],0
  004d3417  0f857a040000          jne       near 004D3897h
  004d341d  8b0e                  mov       ecx,[esi]
  004d341f  b801000000            mov       eax,1
  004d3424  81c900000001          or        ecx,1000000h
  004d342a  66894618              mov       [esi+18h],ax
  004d342e  66c7461a0000          mov       word [esi+1Ah],0
  004d3434  890e                  mov       [esi],ecx
  004d3436  66894612              mov       [esi+12h],ax
  004d343a  e958040000            jmp       004D3897h
  004d343f  668b4e12              mov       cx,[esi+12h]
  004d3443  6683f901              cmp       cx,1
  004d3447  7514                  jne       short 004D345Dh
  004d3449  8a4643                mov       al,[esi+43h]
  004d344c  84c0                  test      al,al
  004d344e  740d                  je        short 004D345Dh
  004d3450  f70600000002          test      dword [esi],2000000h
  004d3456  7405                  je        short 004D345Dh
  004d3458  fec8                  dec       al
  004d345a  884643                mov       [esi+43h],al

LAB_004d345d:
  004d345d  33c0                  xor       eax,eax
  004d345f  663bc8                cmp       cx,ax
  004d3462  0f852f040000          jne       near 004D3897h
  004d3468  6689461a              mov       [esi+1Ah],ax
  004d346c  89465c                mov       [esi+5Ch],eax
  004d346f  8b06                  mov       eax,[esi]
  004d3471  66c746120100          mov       word [esi+12h],1
  004d3477  25fffffffe            and       eax,0FEFFFFFFh
  004d347c  66c746180600          mov       word [esi+18h],6
  004d3482  8906                  mov       [esi],eax
  004d3484  e90e040000            jmp       004D3897h
  004d3489  66837e1200            cmp       word [esi+12h],0
  004d348e  757f                  jne       short 004D350Fh
  004d3490  8b06                  mov       eax,[esi]
  004d3492  a801                  test      al,1
  004d3494  7406                  je        short 004D349Ch
  004d3496  c6464300              mov       byte [esi+43h],0
  004d349a  eb73                  jmp       short 004D350Fh

LAB_004d349c:
  004d349c  8b542410              mov       edx,[esp+10h]
  004d34a0  25fffffffe            and       eax,0FEFFFFFFh
  004d34a5  8906                  mov       [esi],eax
  004d34a7  66c746180300          mov       word [esi+18h],3
  004d34ad  8d8280020000          lea       eax,[edx+280h]
  004d34b3  66c7461a0000          mov       word [esi+1Ah],0
  004d34b9  c1e006                shl       eax,6
  004d34bc  0fafc8                imul      ecx,eax
  004d34bf  89465c                mov       [esi+5Ch],eax
  004d34c2  0fbf4610              movsx     eax,word [esi+10h]
  004d34c6  c1f90c                sar       ecx,0Ch
  004d34c9  50                    push      eax
  004d34ca  8bf9                  mov       edi,ecx
  004d34cc  e84b250700            call      00545A1Ch
  004d34d1  0fafc7                imul      eax,edi
  004d34d4  0fbf4e10              movsx     ecx,word [esi+10h]
  004d34d8  8b6e50                mov       ebp,[esi+50h]
  004d34db  51                    push      ecx
  004d34dc  c1f80c                sar       eax,0Ch
  004d34df  03e8                  add       ebp,eax
  004d34e1  896e50                mov       [esi+50h],ebp
  004d34e4  e82d250700            call      00545A16h
  004d34e9  0fafc7                imul      eax,edi
  004d34ec  8b5658                mov       edx,[esi+58h]
  004d34ef  8b4e54                mov       ecx,[esi+54h]
  004d34f2  c1f80c                sar       eax,0Ch
  004d34f5  03d0                  add       edx,eax
  004d34f7  8b06                  mov       eax,[esi]
  004d34f9  81c100e00100          add       ecx,1E000h
  004d34ff  0c01                  or        al,1
  004d3501  895658                mov       [esi+58h],edx
  004d3504  894e54                mov       [esi+54h],ecx
  004d3507  8906                  mov       [esi],eax
  004d3509  66c746120100          mov       word [esi+12h],1

LAB_004d350f:
  004d350f  f60601                test      byte [esi],1
  004d3512  0f857f030000          jne       near 004D3897h
  004d3518  c6464300              mov       byte [esi+43h],0
  004d351c  e976030000            jmp       004D3897h
  004d3521  668b4e12              mov       cx,[esi+12h]
  004d3525  ba01000000            mov       edx,1
  004d352a  663bca                cmp       cx,dx
  004d352d  751c                  jne       short 004D354Bh
  004d352f  0fbf4610              movsx     eax,word [esi+10h]
  004d3533  39442414              cmp       [esp+14h],eax
  004d3537  7d06                  jge       short 004D353Fh
  004d3539  c6464300              mov       byte [esi+43h],0
  004d353d  eb0c                  jmp       short 004D354Bh

LAB_004d353f:
  004d353f  8a4643                mov       al,[esi+43h]
  004d3542  84c0                  test      al,al
  004d3544  7405                  je        short 004D354Bh
  004d3546  fec8                  dec       al
  004d3548  884643                mov       [esi+43h],al

LAB_004d354b:
  004d354b  6685c9                test      cx,cx
  004d354e  0f8543030000          jne       near 004D3897h
  004d3554  8b442410              mov       eax,[esp+10h]
  004d3558  8b3e                  mov       edi,[esi]
  004d355a  66894e1a              mov       [esi+1Ah],cx
  004d355e  81cf00000001          or        edi,1000000h
  004d3564  8d0cc0                lea       ecx,[eax+eax*8]
  004d3567  66895618              mov       [esi+18h],dx
  004d356b  c1e104                shl       ecx,4
  004d356e  893e                  mov       [esi],edi
  004d3570  66895612              mov       [esi+12h],dx
  004d3574  81c100500000          add       ecx,5000h
  004d357a  e915030000            jmp       004D3894h
  004d357f  66837e1200            cmp       word [esi+12h],0
  004d3584  0f85b8000000          jne       near 004D3642h
  004d358a  b800f45f00            mov       eax,5FF400h

LAB_004d358f:
  004d358f  39706c                cmp       [eax+6Ch],esi
  004d3592  750c                  jne       short 004D35A0h
  004d3594  f70000000080          test      dword [eax],80000000h
  004d359a  0f85f2080000          jne       near 004D3E92h

LAB_004d35a0:
  004d35a0  059c000000            add       eax,9Ch
  004d35a5  3d80076000            cmp       eax,600780h
  004d35aa  7ce3                  jl        short 004D358Fh
  004d35ac  8b06                  mov       eax,[esi]
  004d35ae  a904080000            test      eax,804h
  004d35b3  7410                  je        short 004D35C5h
  004d35b5  25fbf7ffff            and       eax,0FFFFF7FBh
  004d35ba  c6464300              mov       byte [esi+43h],0
  004d35be  8906                  mov       [esi],eax
  004d35c0  e9d2020000            jmp       004D3897h

LAB_004d35c5:
  004d35c5  33db                  xor       ebx,ebx
  004d35c7  6aff                  push      0FFFFFFFFh
  004d35c9  53                    push      ebx
  004d35ca  68bd000000            push      0BDh
  004d35cf  56                    push      esi
  004d35d0  e80b75faff            call      0047AAE0h
  004d35d5  83c410                add       esp,10h
  004d35d8  66c746120100          mov       word [esi+12h],1
  004d35de  e85d3b0700            call      00547140h
  004d35e3  99                    cdq
  004d35e4  33c2                  xor       eax,edx
  004d35e6  33c9                  xor       ecx,ecx
  004d35e8  2bc2                  sub       eax,edx
  004d35ea  83e003                and       eax,3
  004d35ed  33c2                  xor       eax,edx
  004d35ef  2bc2                  sub       eax,edx
  004d35f1  668b0c45502c5500      mov       cx,[eax*2+552C50h]
  004d35f9  66894e06              mov       [esi+6],cx
  004d35fd  a1c4975e00            mov       eax,[5E97C4h]
  004d3602  8bd1                  mov       edx,ecx
  004d3604  81e1ff000000          and       ecx,0FFh
  004d360a  8b809c000000          mov       eax,[eax+9Ch]
  004d3610  c1fa08                sar       edx,8
  004d3613  81e2ff000000          and       edx,0FFh
  004d3619  8d0c49                lea       ecx,[ecx+ecx*2]
  004d361c  c1e104                shl       ecx,4
  004d361f  8b1490                mov       edx,[eax+edx*4]
  004d3622  660fb6440a13          movzx     ax,byte [edx+ecx+13h]
  004d3628  66894610              mov       [esi+10h],ax
  004d362c  8b06                  mov       eax,[esi]
  004d362e  0d04080001            or        eax,1000804h
  004d3633  895e5c                mov       [esi+5Ch],ebx
  004d3636  66c746180400          mov       word [esi+18h],4
  004d363c  66895e1a              mov       [esi+1Ah],bx
  004d3640  8906                  mov       [esi],eax

LAB_004d3642:
  004d3642  b901000000            mov       ecx,1
  004d3647  66394e12              cmp       [esi+12h],cx
  004d364b  753b                  jne       short 004D3688h
  004d364d  8b4620                mov       eax,[esi+20h]
  004d3650  8b5720                mov       edx,[edi+20h]
  004d3653  2bd0                  sub       edx,eax
  004d3655  81c200400100          add       edx,14000h
  004d365b  c1fa03                sar       edx,3
  004d365e  03d0                  add       edx,eax
  004d3660  66ff4e10              dec       word [esi+10h]
  004d3664  66394e10              cmp       [esi+10h],cx
  004d3668  895620                mov       [esi+20h],edx
  004d366b  7d1b                  jge       short 004D3688h
  004d366d  8b06                  mov       eax,[esi]
  004d366f  66c746120200          mov       word [esi+12h],2
  004d3675  25fbfffffe            and       eax,0FEFFFFFBh
  004d367a  66c746180500          mov       word [esi+18h],5
  004d3680  66c7461a0000          mov       word [esi+1Ah],0
  004d3686  8906                  mov       [esi],eax

LAB_004d3688:
  004d3688  66837e1202            cmp       word [esi+12h],2
  004d368d  7532                  jne       short 004D36C1h
  004d368f  66837e1a19            cmp       word [esi+1Ah],19h
  004d3694  752b                  jne       short 004D36C1h
  004d3696  33c0                  xor       eax,eax
  004d3698  6aff                  push      0FFFFFFFFh
  004d369a  668b4606              mov       ax,[esi+6]
  004d369e  6aff                  push      0FFFFFFFFh
  004d36a0  8bc8                  mov       ecx,eax
  004d36a2  c1f808                sar       eax,8
  004d36a5  81e1ff000000          and       ecx,0FFh
  004d36ab  25ff000000            and       eax,0FFh
  004d36b0  51                    push      ecx
  004d36b1  50                    push      eax
  004d36b2  56                    push      esi
  004d36b3  e8c8720600            call      0053A980h
  004d36b8  83c414                add       esp,14h
  004d36bb  66c746120300          mov       word [esi+12h],3

LAB_004d36c1:
  004d36c1  66837e1203            cmp       word [esi+12h],3
  004d36c6  0f85cb010000          jne       near 004D3897h
  004d36cc  8b06                  mov       eax,[esi]
  004d36ce  a900000002            test      eax,2000000h
  004d36d3  0f84be010000          je        near 004D3897h
  004d36d9  80e4f7                and       ah,0F7h
  004d36dc  c6464300              mov       byte [esi+43h],0
  004d36e0  8906                  mov       [esi],eax
  004d36e2  e9b0010000            jmp       004D3897h
  004d36e7  668b4e12              mov       cx,[esi+12h]
  004d36eb  6683f901              cmp       cx,1
  004d36ef  7525                  jne       short 004D3716h
  004d36f1  8b465c                mov       eax,[esi+5Ch]
  004d36f4  0500f0ffff            add       eax,0FFFFF000h
  004d36f9  89465c                mov       [esi+5Ch],eax
  004d36fc  7907                  jns       short 004D3705h
  004d36fe  c7465c00000000        mov       dword [esi+5Ch],0

LAB_004d3705:
  004d3705  8b06                  mov       eax,[esi]
  004d3707  a900000002            test      eax,2000000h
  004d370c  7408                  je        short 004D3716h
  004d370e  24fd                  and       al,0FDh
  004d3710  c6464300              mov       byte [esi+43h],0
  004d3714  8906                  mov       [esi],eax

LAB_004d3716:
  004d3716  6685c9                test      cx,cx
  004d3719  0f8578010000          jne       near 004D3897h
  004d371f  8b16                  mov       edx,[esi]
  004d3721  c7465c00f00000        mov       dword [esi+5Ch],0F000h
  004d3728  81e2fffffffe          and       edx,0FEFFFFFFh
  004d372e  66c746180300          mov       word [esi+18h],3
  004d3734  83ca02                or        edx,2
  004d3737  66894e1a              mov       [esi+1Ah],cx
  004d373b  8916                  mov       [esi],edx
  004d373d  66c746120100          mov       word [esi+12h],1
  004d3743  e94f010000            jmp       004D3897h
  004d3748  66837e1202            cmp       word [esi+12h],2
  004d374d  7536                  jne       short 004D3785h
  004d374f  a1582c5500            mov       eax,[552C58h]
  004d3754  6aff                  push      0FFFFFFFFh
  004d3756  25ffff0000            and       eax,0FFFFh
  004d375b  6aff                  push      0FFFFFFFFh
  004d375d  66894606              mov       [esi+6],ax
  004d3761  25ffff0000            and       eax,0FFFFh
  004d3766  8bc8                  mov       ecx,eax
  004d3768  c1f808                sar       eax,8
  004d376b  81e1ff000000          and       ecx,0FFh
  004d3771  25ff000000            and       eax,0FFh
  004d3776  51                    push      ecx
  004d3777  50                    push      eax
  004d3778  56                    push      esi
  004d3779  e802720600            call      0053A980h
  004d377e  83c414                add       esp,14h
  004d3781  c6464300              mov       byte [esi+43h],0

LAB_004d3785:
  004d3785  b901000000            mov       ecx,1
  004d378a  66394e12              cmp       [esi+12h],cx
  004d378e  754e                  jne       short 004D37DEh
  004d3790  f70600000002          test      dword [esi],2000000h
  004d3796  7406                  je        short 004D379Eh
  004d3798  66c746120200          mov       word [esi+12h],2

LAB_004d379e:
  004d379e  8b442418              mov       eax,[esp+18h]
  004d37a2  8d90c8000000          lea       edx,[eax+0C8h]
  004d37a8  81e2ff0f0000          and       edx,0FFFh
  004d37ae  81fa90010000          cmp       edx,190h
  004d37b4  7e28                  jle       short 004D37DEh
  004d37b6  25ff0f0000            and       eax,0FFFh
  004d37bb  3d00080000            cmp       eax,800h
  004d37c0  7d0b                  jge       short 004D37CDh
  004d37c2  8b442410              mov       eax,[esp+10h]
  004d37c6  d1f8                  sar       eax,1
  004d37c8  83c032                add       eax,32h
  004d37cb  eb0d                  jmp       short 004D37DAh

LAB_004d37cd:
  004d37cd  8b542410              mov       edx,[esp+10h]
  004d37d1  b8ceffffff            mov       eax,0FFFFFFCEh
  004d37d6  d1fa                  sar       edx,1
  004d37d8  2bc2                  sub       eax,edx

LAB_004d37da:
  004d37da  66014632              add       [esi+32h],ax

LAB_004d37de:
  004d37de  33c0                  xor       eax,eax
  004d37e0  66394612              cmp       [esi+12h],ax
  004d37e4  0f85ad000000          jne       near 004D3897h
  004d37ea  89465c                mov       [esi+5Ch],eax
  004d37ed  6689461a              mov       [esi+1Ah],ax
  004d37f1  8b06                  mov       eax,[esi]
  004d37f3  66c746180400          mov       word [esi+18h],4
  004d37f9  25fffffffe            and       eax,0FEFFFFFFh
  004d37fe  66894e12              mov       [esi+12h],cx
  004d3802  8906                  mov       [esi],eax
  004d3804  e98e000000            jmp       004D3897h
  004d3809  b801000000            mov       eax,1
  004d380e  66394612              cmp       [esi+12h],ax
  004d3812  7562                  jne       short 004D3876h
  004d3814  8a4643                mov       al,[esi+43h]
  004d3817  84c0                  test      al,al
  004d3819  7405                  je        short 004D3820h
  004d381b  fec8                  dec       al
  004d381d  884643                mov       [esi+43h],al

LAB_004d3820:
  004d3820  0fbf4632              movsx     eax,word [esi+32h]
  004d3824  0500080000            add       eax,800h
  004d3829  50                    push      eax
  004d382a  e8ed210700            call      00545A1Ch
  004d382f  0fbf4e32              movsx     ecx,word [esi+32h]
  004d3833  81c100080000          add       ecx,800h
  004d3839  8bf8                  mov       edi,eax
  004d383b  51                    push      ecx
  004d383c  e8d5210700            call      00545A16h
  004d3841  8b542410              mov       edx,[esp+10h]
  004d3845  8d4a60                lea       ecx,[edx+60h]
  004d3848  0faf4c241c            imul      ecx,[esp+1Ch]
  004d384d  c1e108                shl       ecx,8
  004d3850  c1f90c                sar       ecx,0Ch
  004d3853  8bd1                  mov       edx,ecx
  004d3855  0fafc8                imul      ecx,eax
  004d3858  0fafd7                imul      edx,edi
  004d385b  8b7d00                mov       edi,[ebp]
  004d385e  8b4624                mov       eax,[esi+24h]
  004d3861  c1fa0c                sar       edx,0Ch
  004d3864  c1f90c                sar       ecx,0Ch
  004d3867  03fa                  add       edi,edx
  004d3869  03c1                  add       eax,ecx
  004d386b  894624                mov       [esi+24h],eax
  004d386e  897d00                mov       [ebp],edi
  004d3871  b801000000            mov       eax,1

LAB_004d3876:
  004d3876  33c9                  xor       ecx,ecx
  004d3878  66394e12              cmp       [esi+12h],cx
  004d387c  7519                  jne       short 004D3897h
  004d387e  8b16                  mov       edx,[esi]
  004d3880  66894618              mov       [esi+18h],ax
  004d3884  81ca00000001          or        edx,1000000h
  004d388a  66894e1a              mov       [esi+1Ah],cx
  004d388e  8916                  mov       [esi],edx
  004d3890  66894612              mov       [esi+12h],ax

LAB_004d3894:
  004d3894  894e5c                mov       [esi+5Ch],ecx

LAB_004d3897:
  004d3897  0fbf4632              movsx     eax,word [esi+32h]
  004d389b  50                    push      eax
  004d389c  e87b210700            call      00545A1Ch
  004d38a1  0fbf4e32              movsx     ecx,word [esi+32h]
  004d38a5  51                    push      ecx
  004d38a6  89442470              mov       [esp+70h],eax
  004d38aa  e867210700            call      00545A16h
  004d38af  8b5650                mov       edx,[esi+50h]
  004d38b2  89442474              mov       [esp+74h],eax
  004d38b6  8bc2                  mov       eax,edx
  004d38b8  8b6e54                mov       ebp,[esi+54h]
  004d38bb  c1f803                sar       eax,3
  004d38be  8bfa                  mov       edi,edx
  004d38c0  8b4c241c              mov       ecx,[esp+1Ch]
  004d38c4  2bf8                  sub       edi,eax
  004d38c6  0faf4e5c              imul      ecx,[esi+5Ch]
  004d38ca  c1fa08                sar       edx,8
  004d38cd  2bfa                  sub       edi,edx
  004d38cf  8bdd                  mov       ebx,ebp
  004d38d1  0fbf969a000000        movsx     edx,word [esi+9Ah]
  004d38d8  8bc2                  mov       eax,edx
  004d38da  897e50                mov       [esi+50h],edi
  004d38dd  c1f808                sar       eax,8
  004d38e0  c1fb08                sar       ebx,8
  004d38e3  2bc3                  sub       eax,ebx
  004d38e5  56                    push      esi
  004d38e6  2bc2                  sub       eax,edx
  004d38e8  8b5658                mov       edx,[esi+58h]
  004d38eb  8bda                  mov       ebx,edx
  004d38ed  03c5                  add       eax,ebp
  004d38ef  c1fb08                sar       ebx,8
  004d38f2  8bea                  mov       ebp,edx
  004d38f4  894654                mov       [esi+54h],eax
  004d38f7  2beb                  sub       ebp,ebx
  004d38f9  8b5e1c                mov       ebx,[esi+1Ch]
  004d38fc  c1fa03                sar       edx,3
  004d38ff  c1f90c                sar       ecx,0Ch
  004d3902  2bea                  sub       ebp,edx
  004d3904  8bd1                  mov       edx,ecx
  004d3906  0faf542470            imul      edx,[esp+70h]
  004d390b  0faf4c2478            imul      ecx,[esp+78h]
  004d3910  c1fa0c                sar       edx,0Ch
  004d3913  c1f90c                sar       ecx,0Ch
  004d3916  896e58                mov       [esi+58h],ebp
  004d3919  03d7                  add       edx,edi
  004d391b  8b7e20                mov       edi,[esi+20h]
  004d391e  03cd                  add       ecx,ebp
  004d3920  8b6e24                mov       ebp,[esi+24h]
  004d3923  03da                  add       ebx,edx
  004d3925  03f8                  add       edi,eax
  004d3927  03e9                  add       ebp,ecx
  004d3929  895e1c                mov       [esi+1Ch],ebx
  004d392c  897e20                mov       [esi+20h],edi
  004d392f  896e24                mov       [esi+24h],ebp
  004d3932  e809000600            call      00533940h
  004d3937  8b9690000000          mov       edx,[esi+90h]
  004d393d  8b4660                mov       eax,[esi+60h]
  004d3940  8b4e20                mov       ecx,[esi+20h]
  004d3943  83c404                add       esp,4
  004d3946  80e6fc                and       dh,0FCh
  004d3949  3bc8                  cmp       ecx,eax
  004d394b  899690000000          mov       [esi+90h],edx
  004d3951  7f2a                  jg        short 004D397Dh
  004d3953  f6c680                test      dh,80h
  004d3956  7525                  jne       short 004D397Dh
  004d3958  8b0e                  mov       ecx,[esi]
  004d395a  894620                mov       [esi+20h],eax
  004d395d  f6c101                test      cl,1
  004d3960  740f                  je        short 004D3971h
  004d3962  83e1fe                and       ecx,0FFFFFFFEh
  004d3965  33ff                  xor       edi,edi
  004d3967  890e                  mov       [esi],ecx
  004d3969  897e54                mov       [esi+54h],edi
  004d396c  e926010000            jmp       004D3A97h

LAB_004d3971:
  004d3971  83e1fe                and       ecx,0FFFFFFFEh
  004d3974  33ff                  xor       edi,edi
  004d3976  890e                  mov       [esi],ecx
  004d3978  897e54                mov       [esi+54h],edi
  004d397b  eb02                  jmp       short 004D397Fh

LAB_004d397d:
  004d397d  33ff                  xor       edi,edi

LAB_004d397f:
  004d397f  8b0e                  mov       ecx,[esi]
  004d3981  f6c101                test      cl,1
  004d3984  750a                  jne       short 004D3990h
  004d3986  394620                cmp       [esi+20h],eax
  004d3989  7e05                  jle       short 004D3990h
  004d398b  83c901                or        ecx,1
  004d398e  890e                  mov       [esi],ecx

LAB_004d3990:
  004d3990  f7c282ad0020          test      edx,2000AD82h
  004d3996  0f84fb000000          je        near 004D3A97h
  004d399c  f60601                test      byte [esi],1
  004d399f  0f85c9000000          jne       near 004D3A6Eh
  004d39a5  8a4e42                mov       cl,[esi+42h]
  004d39a8  b007                  mov       al,7
  004d39aa  3ac8                  cmp       cl,al
  004d39ac  7420                  je        short 004D39CEh
  004d39ae  884642                mov       [esi+42h],al
  004d39b1  e88a370700            call      00547140h
  004d39b6  99                    cdq
  004d39b7  b928000000            mov       ecx,28h
  004d39bc  66897e12              mov       [esi+12h],di
  004d39c0  f7f9                  idiv      ecx
  004d39c2  66c746105000          mov       word [esi+10h],50h
  004d39c8  80c214                add       dl,14h
  004d39cb  885643                mov       [esi+43h],dl

LAB_004d39ce:
  004d39ce  8b16                  mov       edx,[esi]
  004d39d0  8b442424              mov       eax,[esp+24h]
  004d39d4  8b4c2428              mov       ecx,[esp+28h]
  004d39d8  83e2fd                and       edx,0FFFFFFFDh
  004d39db  8916                  mov       [esi],edx
  004d39dd  8b542420              mov       edx,[esp+20h]
  004d39e1  894620                mov       [esi+20h],eax
  004d39e4  8b442448              mov       eax,[esp+48h]
  004d39e8  89561c                mov       [esi+1Ch],edx
  004d39eb  8b54245c              mov       edx,[esp+5Ch]
  004d39ef  25ffff0000            and       eax,0FFFFh
  004d39f4  894e24                mov       [esi+24h],ecx
  004d39f7  8b4c244c              mov       ecx,[esp+4Ch]
  004d39fb  89567c                mov       [esi+7Ch],edx
  004d39fe  8b542438              mov       edx,[esp+38h]
  004d3a02  898690000000          mov       [esi+90h],eax
  004d3a08  8b442440              mov       eax,[esp+40h]
  004d3a0c  898e84000000          mov       [esi+84h],ecx
  004d3a12  8b4c2450              mov       ecx,[esp+50h]
  004d3a16  898680000000          mov       [esi+80h],eax
  004d3a1c  8b442458              mov       eax,[esp+58h]
  004d3a20  899688000000          mov       [esi+88h],edx
  004d3a26  8b542444              mov       edx,[esp+44h]
  004d3a2a  6882ad0020            push      2000AD82h
  004d3a2f  56                    push      esi
  004d3a30  89868c000000          mov       [esi+8Ch],eax
  004d3a36  894e64                mov       [esi+64h],ecx
  004d3a39  895660                mov       [esi+60h],edx
  004d3a3c  e81f240000            call      004D5E60h
  004d3a41  83c408                add       esp,8
  004d3a44  663bc7                cmp       ax,di
  004d3a47  7515                  jne       short 004D3A5Eh
  004d3a49  e8f2360700            call      00547140h
  004d3a4e  99                    cdq
  004d3a4f  33c2                  xor       eax,edx
  004d3a51  2bc2                  sub       eax,edx
  004d3a53  83e001                and       eax,1
  004d3a56  33c2                  xor       eax,edx
  004d3a58  2bc2                  sub       eax,edx
  004d3a5a  8d4400ff              lea       eax,[eax+eax-1]

LAB_004d3a5e:
  004d3a5e  8b4c2410              mov       ecx,[esp+10h]
  004d3a62  83c140                add       ecx,40h
  004d3a65  0fafc8                imul      ecx,eax
  004d3a68  66014e32              add       [esi+32h],cx
  004d3a6c  eb29                  jmp       short 004D3A97h

LAB_004d3a6e:
  004d3a6e  8b542420              mov       edx,[esp+20h]
  004d3a72  8b442428              mov       eax,[esp+28h]
  004d3a76  897e5c                mov       [esi+5Ch],edi
  004d3a79  897e50                mov       [esi+50h],edi
  004d3a7c  897e58                mov       [esi+58h],edi
  004d3a7f  89561c                mov       [esi+1Ch],edx
  004d3a82  894624                mov       [esi+24h],eax
  004d3a85  c6464200              mov       byte [esi+42h],0
  004d3a89  c6464328              mov       byte [esi+43h],28h
  004d3a8d  66897e12              mov       [esi+12h],di
  004d3a91  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004d3a97:
  004d3a97  6aff                  push      0FFFFFFFFh
  004d3a99  6aff                  push      0FFFFFFFFh
  004d3a9b  6aff                  push      0FFFFFFFFh
  004d3a9d  56                    push      esi
  004d3a9e  e86dfdfeff            call      004C3810h
  004d3aa3  8b44242c              mov       eax,[esp+2Ch]
  004d3aa7  83c410                add       esp,10h
  004d3aaa  3bc7                  cmp       eax,edi
  004d3aac  7517                  jne       short 004D3AC5h
  004d3aae  668b8c2482000000      mov       cx,[esp+82h]
  004d3ab6  66894e32              mov       [esi+32h],cx
  004d3aba  5f                    pop       edi
  004d3abb  5e                    pop       esi
  004d3abc  5d                    pop       ebp
  004d3abd  5b                    pop       ebx
  004d3abe  81c484000000          add       esp,84h
  004d3ac4  c3                    ret

LAB_004d3ac5:
  004d3ac5  8a4643                mov       al,[esi+43h]
  004d3ac8  84c0                  test      al,al
  004d3aca  0f85b7030000          jne       near 004D3E87h
  004d3ad0  8b5c243c              mov       ebx,[esp+3Ch]

LAB_004d3ad4:
  004d3ad4  8b466c                mov       eax,[esi+6Ch]
  004d3ad7  8b561c                mov       edx,[esi+1Ch]
  004d3ada  8b781c                mov       edi,[eax+1Ch]
  004d3add  8b6824                mov       ebp,[eax+24h]
  004d3ae0  8b4624                mov       eax,[esi+24h]
  004d3ae3  2bfa                  sub       edi,edx
  004d3ae5  2be8                  sub       ebp,eax
  004d3ae7  c1ff0c                sar       edi,0Ch
  004d3aea  c1fd0c                sar       ebp,0Ch
  004d3aed  8bd7                  mov       edx,edi
  004d3aef  8bc5                  mov       eax,ebp
  004d3af1  0fafd7                imul      edx,edi
  004d3af4  0fafc5                imul      eax,ebp
  004d3af7  03d0                  add       edx,eax
  004d3af9  52                    push      edx
  004d3afa  e8111f0700            call      00545A10h
  004d3aff  55                    push      ebp
  004d3b00  57                    push      edi
  004d3b01  8944241c              mov       [esp+1Ch],eax
  004d3b05  e8181f0700            call      00545A22h
  004d3b0a  668b4e32              mov       cx,[esi+32h]
  004d3b0e  66c746120000          mov       word [esi+12h],0
  004d3b14  2bc1                  sub       eax,ecx
  004d3b16  25ff0f0000            and       eax,0FFFh
  004d3b1b  89442418              mov       [esp+18h],eax
  004d3b1f  8b442414              mov       eax,[esp+14h]
  004d3b23  3dfa000000            cmp       eax,0FAh
  004d3b28  0f8d64010000          jge       near 004D3C92h
  004d3b2e  e80d360700            call      00547140h
  004d3b33  99                    cdq
  004d3b34  b964000000            mov       ecx,64h
  004d3b39  f7f9                  idiv      ecx
  004d3b3b  8bfa                  mov       edi,edx
  004d3b3d  83ff32                cmp       edi,32h
  004d3b40  0f8d80000000          jge       near 004D3BC6h
  004d3b46  8b542418              mov       edx,[esp+18h]
  004d3b4a  8d0cdd00020000        lea       ecx,[ebx*8+200h]
  004d3b51  8d849a00010000        lea       eax,[edx+ebx*4+100h]
  004d3b58  25ff0f0000            and       eax,0FFFh
  004d3b5d  3bc1                  cmp       eax,ecx
  004d3b5f  7f58                  jg        short 004D3BB9h
  004d3b61  e8da350700            call      00547140h
  004d3b66  99                    cdq
  004d3b67  8d4c1b01              lea       ecx,[ebx+ebx+1]
  004d3b6b  f7f9                  idiv      ecx
  004d3b6d  8b442414              mov       eax,[esp+14h]
  004d3b71  83c264                add       edx,64h
  004d3b74  3bc2                  cmp       eax,edx
  004d3b76  7d41                  jge       short 004D3BB9h
  004d3b78  83ff28                cmp       edi,28h
  004d3b7b  7d29                  jge       short 004D3BA6h
  004d3b7d  8b16                  mov       edx,[esi]
  004d3b7f  c646420b              mov       byte [esi+42h],0Bh
  004d3b83  81e2fffffffe          and       edx,0FEFFFFFFh
  004d3b89  c6464301              mov       byte [esi+43h],1
  004d3b8d  83ca02                or        edx,2
  004d3b90  33c0                  xor       eax,eax
  004d3b92  8916                  mov       [esi],edx
  004d3b94  89465c                mov       [esi+5Ch],eax
  004d3b97  66c746180500          mov       word [esi+18h],5
  004d3b9d  6689461a              mov       [esi+1Ah],ax
  004d3ba1  e9ec000000            jmp       004D3C92h

LAB_004d3ba6:
  004d3ba6  c646420c              mov       byte [esi+42h],0Ch
  004d3baa  c6464301              mov       byte [esi+43h],1
  004d3bae  66c746120000          mov       word [esi+12h],0
  004d3bb4  e9d9000000            jmp       004D3C92h

LAB_004d3bb9:
  004d3bb9  c6464201              mov       byte [esi+42h],1
  004d3bbd  c6464301              mov       byte [esi+43h],1
  004d3bc1  e9cc000000            jmp       004D3C92h

LAB_004d3bc6:
  004d3bc6  83ff3c                cmp       edi,3Ch
  004d3bc9  7d55                  jge       short 004D3C20h
  004d3bcb  8b442414              mov       eax,[esp+14h]
  004d3bcf  bf50000000            mov       edi,50h
  004d3bd4  3bc7                  cmp       eax,edi
  004d3bd6  7d1b                  jge       short 004D3BF3h
  004d3bd8  c646420e              mov       byte [esi+42h],0Eh
  004d3bdc  e85f350700            call      00547140h
  004d3be1  99                    cdq
  004d3be2  b914000000            mov       ecx,14h
  004d3be7  f7f9                  idiv      ecx
  004d3be9  02d1                  add       dl,cl
  004d3beb  885643                mov       [esi+43h],dl
  004d3bee  e99f000000            jmp       004D3C92h

LAB_004d3bf3:
  004d3bf3  807e420e              cmp       byte [esi+42h],0Eh
  004d3bf7  0f84cc000000          je        near 004D3CC9h
  004d3bfd  c6464202              mov       byte [esi+42h],2
  004d3c01  e83a350700            call      00547140h
  004d3c06  99                    cdq
  004d3c07  b90a000000            mov       ecx,0Ah
  004d3c0c  66897e10              mov       [esi+10h],di
  004d3c10  f7f9                  idiv      ecx
  004d3c12  8bc3                  mov       eax,ebx
  004d3c14  d1f8                  sar       eax,1
  004d3c16  02d0                  add       dl,al
  004d3c18  80c214                add       dl,14h
  004d3c1b  885643                mov       [esi+43h],dl
  004d3c1e  eb72                  jmp       short 004D3C92h

LAB_004d3c20:
  004d3c20  8b442414              mov       eax,[esp+14h]
  004d3c24  83ff46                cmp       edi,46h
  004d3c27  7d23                  jge       short 004D3C4Ch
  004d3c29  8b4c2410              mov       ecx,[esp+10h]
  004d3c2d  83c164                add       ecx,64h
  004d3c30  3bc1                  cmp       eax,ecx
  004d3c32  7e18                  jle       short 004D3C4Ch
  004d3c34  c646420e              mov       byte [esi+42h],0Eh
  004d3c38  e803350700            call      00547140h
  004d3c3d  99                    cdq
  004d3c3e  b914000000            mov       ecx,14h
  004d3c43  f7f9                  idiv      ecx
  004d3c45  02d1                  add       dl,cl
  004d3c47  885643                mov       [esi+43h],dl
  004d3c4a  eb46                  jmp       short 004D3C92h

LAB_004d3c4c:
  004d3c4c  83f850                cmp       eax,50h
  004d3c4f  7d18                  jge       short 004D3C69h
  004d3c51  c646420e              mov       byte [esi+42h],0Eh
  004d3c55  e8e6340700            call      00547140h
  004d3c5a  99                    cdq
  004d3c5b  b914000000            mov       ecx,14h
  004d3c60  f7f9                  idiv      ecx
  004d3c62  02d1                  add       dl,cl
  004d3c64  885643                mov       [esi+43h],dl
  004d3c67  eb29                  jmp       short 004D3C92h

LAB_004d3c69:
  004d3c69  807e420e              cmp       byte [esi+42h],0Eh
  004d3c6d  745a                  je        short 004D3CC9h
  004d3c6f  c6464202              mov       byte [esi+42h],2
  004d3c73  e8c8340700            call      00547140h
  004d3c78  99                    cdq
  004d3c79  b90a000000            mov       ecx,0Ah
  004d3c7e  66c746105000          mov       word [esi+10h],50h
  004d3c84  f7f9                  idiv      ecx
  004d3c86  8bc3                  mov       eax,ebx
  004d3c88  d1f8                  sar       eax,1
  004d3c8a  02d0                  add       dl,al
  004d3c8c  80c214                add       dl,14h
  004d3c8f  885643                mov       [esi+43h],dl

LAB_004d3c92:
  004d3c92  8b442414              mov       eax,[esp+14h]
  004d3c96  3d20030000            cmp       eax,320h
  004d3c9b  0f8df8000000          jge       near 004D3D99h
  004d3ca1  3dfa000000            cmp       eax,0FAh
  004d3ca6  0f8ced000000          jl        near 004D3D99h
  004d3cac  e88f340700            call      00547140h
  004d3cb1  99                    cdq
  004d3cb2  b964000000            mov       ecx,64h
  004d3cb7  f7f9                  idiv      ecx
  004d3cb9  83fa14                cmp       edx,14h
  004d3cbc  7c21                  jl        short 004D3CDFh
  004d3cbe  83fa1e                cmp       edx,1Eh
  004d3cc1  7d6a                  jge       short 004D3D2Dh
  004d3cc3  807e420e              cmp       byte [esi+42h],0Eh
  004d3cc7  753f                  jne       short 004D3D08h

LAB_004d3cc9:
  004d3cc9  8a4643                mov       al,[esi+43h]
  004d3ccc  84c0                  test      al,al
  004d3cce  0f8400feffff          je        near 004D3AD4h
  004d3cd4  5f                    pop       edi
  004d3cd5  5e                    pop       esi
  004d3cd6  5d                    pop       ebp
  004d3cd7  5b                    pop       ebx
  004d3cd8  81c484000000          add       esp,84h
  004d3cde  c3                    ret

LAB_004d3cdf:
  004d3cdf  8b542418              mov       edx,[esp+18h]
  004d3ce3  81c200040000          add       edx,400h
  004d3ce9  81e2ff0f0000          and       edx,0FFFh
  004d3cef  81fa00080000          cmp       edx,800h
  004d3cf5  0f8c90000000          jl        near 004D3D8Bh
  004d3cfb  c6464201              mov       byte [esi+42h],1
  004d3cff  c6464301              mov       byte [esi+43h],1
  004d3d03  e991000000            jmp       004D3D99h

LAB_004d3d08:
  004d3d08  c6464202              mov       byte [esi+42h],2
  004d3d0c  e82f340700            call      00547140h
  004d3d11  99                    cdq
  004d3d12  b90a000000            mov       ecx,0Ah
  004d3d17  66c746105000          mov       word [esi+10h],50h
  004d3d1d  f7f9                  idiv      ecx
  004d3d1f  8bc3                  mov       eax,ebx
  004d3d21  d1f8                  sar       eax,1
  004d3d23  02d0                  add       dl,al
  004d3d25  80c214                add       dl,14h
  004d3d28  885643                mov       [esi+43h],dl
  004d3d2b  eb6c                  jmp       short 004D3D99h

LAB_004d3d2d:
  004d3d2d  83fa32                cmp       edx,32h
  004d3d30  7d18                  jge       short 004D3D4Ah
  004d3d32  c646420e              mov       byte [esi+42h],0Eh
  004d3d36  e805340700            call      00547140h
  004d3d3b  99                    cdq
  004d3d3c  b914000000            mov       ecx,14h
  004d3d41  f7f9                  idiv      ecx
  004d3d43  02d1                  add       dl,cl
  004d3d45  885643                mov       [esi+43h],dl
  004d3d48  eb4f                  jmp       short 004D3D99h

LAB_004d3d4a:
  004d3d4a  83fa5a                cmp       edx,5Ah
  004d3d4d  7d3c                  jge       short 004D3D8Bh
  004d3d4f  c6464203              mov       byte [esi+42h],3
  004d3d53  66c746120000          mov       word [esi+12h],0
  004d3d59  e8e2330700            call      00547140h
  004d3d5e  99                    cdq
  004d3d5f  b90a000000            mov       ecx,0Ah
  004d3d64  f7f9                  idiv      ecx
  004d3d66  8bc3                  mov       eax,ebx
  004d3d68  d1f8                  sar       eax,1
  004d3d6a  02d0                  add       dl,al
  004d3d6c  80c214                add       dl,14h
  004d3d6f  885643                mov       [esi+43h],dl
  004d3d72  e8c9330700            call      00547140h
  004d3d77  99                    cdq
  004d3d78  33c2                  xor       eax,edx
  004d3d7a  2bc2                  sub       eax,edx
  004d3d7c  25ff0f0000            and       eax,0FFFh
  004d3d81  33c2                  xor       eax,edx
  004d3d83  2bc2                  sub       eax,edx
  004d3d85  66894610              mov       [esi+10h],ax
  004d3d89  eb0e                  jmp       short 004D3D99h

LAB_004d3d8b:
  004d3d8b  c646420a              mov       byte [esi+42h],0Ah
  004d3d8f  c6464301              mov       byte [esi+43h],1
  004d3d93  66c746120000          mov       word [esi+12h],0

LAB_004d3d99:
  004d3d99  817c241420030000      cmp       dword [esp+14h],320h
  004d3da1  0f8ce0000000          jl        near 004D3E87h
  004d3da7  e894330700            call      00547140h
  004d3dac  bf64000000            mov       edi,64h
  004d3db1  99                    cdq
  004d3db2  8bcf                  mov       ecx,edi
  004d3db4  f7f9                  idiv      ecx
  004d3db6  83fa1e                cmp       edx,1Eh
  004d3db9  7d12                  jge       short 004D3DCDh
  004d3dbb  8b442410              mov       eax,[esp+10h]
  004d3dbf  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004d3dc3  3bd1                  cmp       edx,ecx
  004d3dc5  0f8d91000000          jge       near 004D3E5Ch
  004d3dcb  eb65                  jmp       short 004D3E32h

LAB_004d3dcd:
  004d3dcd  83fa28                cmp       edx,28h
  004d3dd0  7d43                  jge       short 004D3E15h
  004d3dd2  c6464203              mov       byte [esi+42h],3
  004d3dd6  66c746120000          mov       word [esi+12h],0
  004d3ddc  e85f330700            call      00547140h
  004d3de1  99                    cdq
  004d3de2  b90a000000            mov       ecx,0Ah
  004d3de7  f7f9                  idiv      ecx
  004d3de9  d1fb                  sar       ebx,1
  004d3deb  02d3                  add       dl,bl
  004d3ded  80c214                add       dl,14h
  004d3df0  885643                mov       [esi+43h],dl
  004d3df3  e848330700            call      00547140h
  004d3df8  99                    cdq
  004d3df9  33c2                  xor       eax,edx
  004d3dfb  2bc2                  sub       eax,edx
  004d3dfd  25ff0f0000            and       eax,0FFFh
  004d3e02  33c2                  xor       eax,edx
  004d3e04  2bc2                  sub       eax,edx
  004d3e06  66894610              mov       [esi+10h],ax
  004d3e0a  5f                    pop       edi
  004d3e0b  5e                    pop       esi
  004d3e0c  5d                    pop       ebp
  004d3e0d  5b                    pop       ebx
  004d3e0e  81c484000000          add       esp,84h
  004d3e14  c3                    ret

LAB_004d3e15:
  004d3e15  83fa3c                cmp       edx,3Ch
  004d3e18  7d13                  jge       short 004D3E2Dh
  004d3e1a  c6464201              mov       byte [esi+42h],1
  004d3e1e  c6464301              mov       byte [esi+43h],1
  004d3e22  5f                    pop       edi
  004d3e23  5e                    pop       esi
  004d3e24  5d                    pop       ebp
  004d3e25  5b                    pop       ebx
  004d3e26  81c484000000          add       esp,84h
  004d3e2c  c3                    ret

LAB_004d3e2d:
  004d3e2d  83fa50                cmp       edx,50h
  004d3e30  7d2a                  jge       short 004D3E5Ch

LAB_004d3e32:
  004d3e32  c6464202              mov       byte [esi+42h],2
  004d3e36  e805330700            call      00547140h
  004d3e3b  99                    cdq
  004d3e3c  b90a000000            mov       ecx,0Ah
  004d3e41  66897e10              mov       [esi+10h],di
  004d3e45  f7f9                  idiv      ecx
  004d3e47  d1fb                  sar       ebx,1
  004d3e49  02d3                  add       dl,bl
  004d3e4b  80c214                add       dl,14h
  004d3e4e  885643                mov       [esi+43h],dl
  004d3e51  5f                    pop       edi
  004d3e52  5e                    pop       esi
  004d3e53  5d                    pop       ebp
  004d3e54  5b                    pop       ebx
  004d3e55  81c484000000          add       esp,84h
  004d3e5b  c3                    ret

LAB_004d3e5c:
  004d3e5c  c6464200              mov       byte [esi+42h],0
  004d3e60  e8db320700            call      00547140h
  004d3e65  99                    cdq
  004d3e66  b90a000000            mov       ecx,0Ah
  004d3e6b  66c746120000          mov       word [esi+12h],0
  004d3e71  f7f9                  idiv      ecx
  004d3e73  c1fb02                sar       ebx,2
  004d3e76  02d3                  add       dl,bl
  004d3e78  885643                mov       [esi+43h],dl
  004d3e7b  8b542410              mov       edx,[esp+10h]
  004d3e7f  8d441250              lea       eax,[edx+edx+50h]
  004d3e83  66894610              mov       [esi+10h],ax

LAB_004d3e87:
  004d3e87  5f                    pop       edi
  004d3e88  5e                    pop       esi
  004d3e89  5d                    pop       ebp
  004d3e8a  5b                    pop       ebx
  004d3e8b  81c484000000          add       esp,84h
  004d3e91  c3                    ret

LAB_004d3e92:
  004d3e92  c6464300              mov       byte [esi+43h],0
  004d3e96  e9fcf9ffff            jmp       004D3897h

73 cible(s) de saut interne : 0x004d314e, 0x004d3171, 0x004d317e, 0x004d3180, 0x004d31ea, 0x004d31ee, 0x004d3225, 0x004d3238, 0x004d3270, 0x004d3291, 0x004d32ad, 0x004d32f7, 0x004d3317, 0x004d3323, 0x004d335a, 0x004d3381, 0x004d3400, 0x004d340e, 0x004d3412, 0x004d345d, 0x004d349c, 0x004d350f, 0x004d353f, 0x004d354b, 0x004d358f, 0x004d35a0, 0x004d35c5, 0x004d3642, 0x004d3688, 0x004d36c1, 0x004d3705, 0x004d3716, 0x004d3785, 0x004d379e, 0x004d37cd, 0x004d37da, 0x004d37de, 0x004d3820, 0x004d3876, 0x004d3894, 0x004d3897, 0x004d3971, 0x004d397d, 0x004d397f, 0x004d3990, 0x004d39ce, 0x004d3a5e, 0x004d3a6e, 0x004d3a97, 0x004d3ac5, 0x004d3ad4, 0x004d3ba6, 0x004d3bb9, 0x004d3bc6, 0x004d3bf3, 0x004d3c20, 0x004d3c4c, 0x004d3c69, 0x004d3c92, 0x004d3cc9, 0x004d3cdf, 0x004d3d08, 0x004d3d2d, 0x004d3d4a, 0x004d3d8b, 0x004d3d99, 0x004d3dcd, 0x004d3e15, 0x004d3e2d, 0x004d3e32, 0x004d3e5c, 0x004d3e87, 0x004d3e92
