Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00481720  section .text  offset fichier 0x80b20  680 octets

  00481720  83ec08                sub       esp,8
  00481723  53                    push      ebx
  00481724  55                    push      ebp
  00481725  56                    push      esi
  00481726  8b742418              mov       esi,[esp+18h]
  0048172a  33ed                  xor       ebp,ebp
  0048172c  57                    push      edi
  0048172d  66396e10              cmp       [esi+10h],bp
  00481731  8b7e78                mov       edi,[esi+78h]
  00481734  753c                  jne       short 00481772h
  00481736  8b4674                mov       eax,[esi+74h]
  00481739  8b08                  mov       ecx,[eax]
  0048173b  80c980                or        cl,80h
  0048173e  8908                  mov       [eax],ecx
  00481740  804e3606              or        byte [esi+36h],6
  00481744  8d461c                lea       eax,[esi+1Ch]
  00481747  c70560915e0040004000  mov       dword [5E9160h],400040h
  00481751  50                    push      eax
  00481752  c70564915e0040000200  mov       dword [5E9164h],20040h
  0048175c  6898255500            push      552598h
  00481761  66c7869a0000000050    mov       word [esi+9Ah],5000h
  0048176a  e871c40b00            call      0053DBE0h
  0048176f  83c408                add       esp,8

LAB_00481772:
  00481772  668b4612              mov       ax,[esi+12h]
  00481776  66014632              add       [esi+32h],ax
  0048177a  83c008                add       eax,8
  0048177d  66894612              mov       [esi+12h],ax
  00481781  668b4610              mov       ax,[esi+10h]
  00481785  663d1000              cmp       ax,10h
  00481789  7d44                  jge       short 004817CFh
  0048178b  8b5638                mov       edx,[esi+38h]
  0048178e  83e00f                and       eax,0Fh
  00481791  c1e006                shl       eax,6
  00481794  81c208080800          add       edx,80808h
  0048179a  50                    push      eax
  0048179b  895638                mov       [esi+38h],edx
  0048179e  e879420c00            call      00545A1Ch
  004817a3  57                    push      edi
  004817a4  8bd8                  mov       ebx,eax
  004817a6  e815740b00            call      00538BC0h
  004817ab  0fafd8                imul      ebx,eax
  004817ae  b8b5814e1b            mov       eax,1B4E81B5h
  004817b3  83c404                add       esp,4
  004817b6  f7eb                  imul      ebx
  004817b8  c1fa03                sar       edx,3
  004817bb  8bca                  mov       ecx,edx
  004817bd  c1e91f                shr       ecx,1Fh
  004817c0  03d1                  add       edx,ecx
  004817c2  66895628              mov       [esi+28h],dx
  004817c6  6689562c              mov       [esi+2Ch],dx
  004817ca  e9ab010000            jmp       0048197Ah

LAB_004817cf:
  004817cf  663d2000              cmp       ax,20h
  004817d3  0f8ca1010000          jl        near 0048197Ah
  004817d9  663d3000              cmp       ax,30h
  004817dd  7d53                  jge       short 00481832h
  004817df  8b6e38                mov       ebp,[esi+38h]
  004817e2  83e00f                and       eax,0Fh
  004817e5  c1e006                shl       eax,6
  004817e8  81c507070700          add       ebp,70707h
  004817ee  50                    push      eax
  004817ef  896e38                mov       [esi+38h],ebp
  004817f2  e825420c00            call      00545A1Ch
  004817f7  bb00100000            mov       ebx,1000h
  004817fc  57                    push      edi
  004817fd  2bd8                  sub       ebx,eax
  004817ff  e8bc730b00            call      00538BC0h
  00481804  8bc8                  mov       ecx,eax
  00481806  b8b5814e1b            mov       eax,1B4E81B5h
  0048180b  0fafcb                imul      ecx,ebx
  0048180e  f7e9                  imul      ecx
  00481810  c1fa03                sar       edx,3
  00481813  8bc2                  mov       eax,edx
  00481815  83c404                add       esp,4
  00481818  c1e81f                shr       eax,1Fh
  0048181b  03d0                  add       edx,eax
  0048181d  66895628              mov       [esi+28h],dx
  00481821  6689562c              mov       [esi+2Ch],dx
  00481825  66895f28              mov       [edi+28h],bx
  00481829  66895f2c              mov       [edi+2Ch],bx
  0048182d  e948010000            jmp       0048197Ah

LAB_00481832:
  00481832  6a40                  push      40h
  00481834  896e70                mov       [esi+70h],ebp
  00481837  8d5e50                lea       ebx,[esi+50h]
  0048183a  e801590c00            call      00547140h
  0048183f  25ff000000            and       eax,0FFh
  00481844  2d80030000            sub       eax,380h
  00481849  50                    push      eax
  0048184a  e8f1580c00            call      00547140h
  0048184f  50                    push      eax
  00481850  53                    push      ebx
  00481851  e87a120b00            call      00532AD0h
  00481856  8b5654                mov       edx,[esi+54h]
  00481859  8b4e28                mov       ecx,[esi+28h]
  0048185c  8b4638                mov       eax,[esi+38h]
  0048185f  894c242c              mov       [esp+2Ch],ecx
  00481863  c1fa03                sar       edx,3
  00481866  83c410                add       esp,10h
  00481869  33c9                  xor       ecx,ecx
  0048186b  8a4e0b                mov       cl,[esi+0Bh]
  0048186e  895654                mov       [esi+54h],edx
  00481871  8b562c                mov       edx,[esi+2Ch]
  00481874  6aff                  push      0FFFFFFFFh
  00481876  89542414              mov       [esp+14h],edx
  0048187a  51                    push      ecx
  0048187b  68bf000000            push      0BFh
  00481880  8d561c                lea       edx,[esi+1Ch]
  00481883  89442420              mov       [esp+20h],eax
  00481887  68b8195500            push      5519B8h
  0048188c  b803000000            mov       eax,3
  00481891  52                    push      edx
  00481892  56                    push      esi
  00481893  c7462800100010        mov       dword [esi+28h],10001000h
  0048189a  6689462c              mov       [esi+2Ch],ax
  0048189e  c7463880808000        mov       dword [esi+38h],808080h
  004818a5  6689461a              mov       [esi+1Ah],ax
  004818a9  e89293ffff            call      0047AC40h
  004818ae  83c418                add       esp,18h
  004818b1  66837e1030            cmp       word [esi+10h],30h
  004818b6  757e                  jne       short 00481936h
  004818b8  6a04                  push      4
  004818ba  55                    push      ebp
  004818bb  6a6c                  push      6Ch
  004818bd  56                    push      esi
  004818be  896e44                mov       [esi+44h],ebp
  004818c1  896e48                mov       [esi+48h],ebp
  004818c4  896e4c                mov       [esi+4Ch],ebp
  004818c7  c70300100000          mov       dword [ebx],1000h
  004818cd  c74654000c0000        mov       dword [esi+54h],0C00h
  004818d4  c7465800100000        mov       dword [esi+58h],1000h
  004818db  896e28                mov       [esi+28h],ebp
  004818de  66896e2c              mov       [esi+2Ch],bp
  004818e2  897e70                mov       [esi+70h],edi
  004818e5  c6460407              mov       byte [esi+4],7
  004818e9  e8f291ffff            call      0047AAE0h
  004818ee  8b0f                  mov       ecx,[edi]
  004818f0  668b4732              mov       ax,[edi+32h]
  004818f4  81e1fffdfff7          and       ecx,0F7FFFDFFh
  004818fa  83c410                add       esp,10h
  004818fd  80cd20                or        ch,20h
  00481900  66ff4712              inc       word [edi+12h]
  00481904  66898798000000        mov       [edi+98h],ax
  0048190b  890f                  mov       [edi],ecx
  0048190d  a044e55d00            mov       al,[5DE544h]
  00481912  8a1538e55d00          mov       dl,[5DE538h]
  00481918  fec0                  inc       al
  0048191a  88153ce55d00          mov       [5DE53Ch],dl
  00481920  a244e55d00            mov       [5DE544h],al
  00481925  c605f8455600fe        mov       byte [5645F8h],0FEh
  0048192c  c70510e55d0002000000  mov       dword [5DE510h],2

LAB_00481936:
  00481936  8b44241c              mov       eax,[esp+1Ch]
  0048193a  8b4c2410              mov       ecx,[esp+10h]
  0048193e  8b542414              mov       edx,[esp+14h]
  00481942  66837e1040            cmp       word [esi+10h],40h
  00481947  894628                mov       [esi+28h],eax
  0048194a  894e2c                mov       [esi+2Ch],ecx
  0048194d  895638                mov       [esi+38h],edx
  00481950  7d15                  jge       short 00481967h
  00481952  8b4e20                mov       ecx,[esi+20h]
  00481955  81c100c00300          add       ecx,3C000h
  0048195b  894e20                mov       [esi+20h],ecx
  0048195e  8b07                  mov       eax,[edi]
  00481960  80cc50                or        ah,50h
  00481963  8907                  mov       [edi],eax
  00481965  eb13                  jmp       short 0048197Ah

LAB_00481967:
  00481967  8b4674                mov       eax,[esi+74h]
  0048196a  8b08                  mov       ecx,[eax]
  0048196c  80e17f                and       cl,7Fh
  0048196f  8908                  mov       [eax],ecx
  00481971  66892d66915e00        mov       [5E9166h],bp
  00481978  892e                  mov       [esi],ebp

LAB_0048197a:
  0048197a  0fbf4628              movsx     eax,word [esi+28h]
  0048197e  0fbf6e32              movsx     ebp,word [esi+32h]
  00481982  8d0440                lea       eax,[eax+eax*2]
  00481985  55                    push      ebp
  00481986  8d1c80                lea       ebx,[eax+eax*4]
  00481989  c1fb0c                sar       ebx,0Ch
  0048198c  e88b400c00            call      00545A1Ch
  00481991  0fafc3                imul      eax,ebx
  00481994  8b4f1c                mov       ecx,[edi+1Ch]
  00481997  55                    push      ebp
  00481998  03c1                  add       eax,ecx
  0048199a  89461c                mov       [esi+1Ch],eax
  0048199d  e874400c00            call      00545A16h
  004819a2  0fafc3                imul      eax,ebx
  004819a5  034724                add       eax,[edi+24h]
  004819a8  894624                mov       [esi+24h],eax
  004819ab  668b4610              mov       ax,[esi+10h]
  004819af  663d3000              cmp       ax,30h
  004819b3  7d06                  jge       short 004819BBh
  004819b5  8b4f60                mov       ecx,[edi+60h]
  004819b8  894e20                mov       [esi+20h],ecx

LAB_004819bb:
  004819bb  40                    inc       eax
  004819bc  5f                    pop       edi
  004819bd  66894610              mov       [esi+10h],ax
  004819c1  5e                    pop       esi
  004819c2  5d                    pop       ebp
  004819c3  5b                    pop       ebx
  004819c4  83c408                add       esp,8
  004819c7  c3                    ret

7 cible(s) de saut interne : 0x00481772, 0x004817cf, 0x00481832, 0x00481936, 0x00481967, 0x0048197a, 0x004819bb
