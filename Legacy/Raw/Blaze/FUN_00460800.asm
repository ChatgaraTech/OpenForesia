C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00460800  section .text  offset fichier 0x5fc00  393 octets

  00460800  53                    push      ebx
  00460801  56                    push      esi
  00460802  8b74240c              mov       esi,[esp+0Ch]
  00460806  57                    push      edi
  00460807  66837e1000            cmp       word [esi+10h],0
  0046080c  752e                  jne       short 0046083Ch
  0046080e  66c746120000          mov       word [esi+12h],0
  00460814  e827690e00            call      00547140h
  00460819  99                    cdq
  0046081a  b91e000000            mov       ecx,1Eh
  0046081f  f7f9                  idiv      ecx
  00460821  83ea0f                sub       edx,0Fh
  00460824  66895614              mov       [esi+14h],dx
  00460828  e813690e00            call      00547140h
  0046082d  99                    cdq
  0046082e  b91e000000            mov       ecx,1Eh
  00460833  f7f9                  idiv      ecx
  00460835  83ea0f                sub       edx,0Fh
  00460838  66895616              mov       [esi+16h],dx

LAB_0046083c:
  0046083c  8a06                  mov       al,[esi]
  0046083e  bf01000000            mov       edi,1
  00460843  a810                  test      al,10h
  00460845  7436                  je        short 0046087Dh
  00460847  66837e1000            cmp       word [esi+10h],0
  0046084c  752f                  jne       short 0046087Dh
  0046084e  66897e10              mov       [esi+10h],di
  00460852  8b15cc975e00          mov       edx,[5E97CCh]
  00460858  6a00                  push      0
  0046085a  6a00                  push      0
  0046085c  8b82a8000000          mov       eax,[edx+0A8h]
  00460862  6800180000            push      1800h
  00460867  6800030000            push      300h
  0046086c  8b08                  mov       ecx,[eax]
  0046086e  660fb6511f            movzx     dx,byte [ecx+1Fh]
  00460873  52                    push      edx
  00460874  56                    push      esi
  00460875  e806780000            call      00468080h
  0046087a  83c418                add       esp,18h

LAB_0046087d:
  0046087d  66397e10              cmp       [esi+10h],di
  00460881  7548                  jne       short 004608CBh
  00460883  8b5e20                mov       ebx,[esi+20h]
  00460886  bf04000000            mov       edi,4
  0046088b  8bc3                  mov       eax,ebx
  0046088d  0500800c00            add       eax,0C8000h
  00460892  894620                mov       [esi+20h],eax

LAB_00460895:
  00460895  6a00                  push      0
  00460897  6a00                  push      0
  00460899  6aff                  push      0FFFFFFFFh
  0046089b  6a00                  push      0
  0046089d  6a33                  push      33h
  0046089f  56                    push      esi
  004608a0  e86b780000            call      00468110h
  004608a5  83c418                add       esp,18h
  004608a8  4f                    dec       edi
  004608a9  75ea                  jne       short 00460895h
  004608ab  81c300b0ffff          add       ebx,0FFFFB000h
  004608b1  66ff4612              inc       word [esi+12h]
  004608b5  66837e1214            cmp       word [esi+12h],14h
  004608ba  895e20                mov       [esi+20h],ebx
  004608bd  750c                  jne       short 004608CBh
  004608bf  66c746100200          mov       word [esi+10h],2
  004608c5  66c746120500          mov       word [esi+12h],5

LAB_004608cb:
  004608cb  66837e1002            cmp       word [esi+10h],2
  004608d0  0f85af000000          jne       near 00460985h
  004608d6  bf19000000            mov       edi,19h
  004608db  66397e12              cmp       [esi+12h],di
  004608df  752e                  jne       short 0046090Fh
  004608e1  a1cc975e00            mov       eax,[5E97CCh]
  004608e6  6a00                  push      0
  004608e8  6a00                  push      0
  004608ea  6800180000            push      1800h
  004608ef  8b88a8000000          mov       ecx,[eax+0A8h]
  004608f5  6800030000            push      300h
  004608fa  8b11                  mov       edx,[ecx]
  004608fc  660fb64220            movzx     ax,byte [edx+20h]
  00460901  50                    push      eax
  00460902  56                    push      esi
  00460903  e878770000            call      00468080h
  00460908  83c418                add       esp,18h
  0046090b  66397e12              cmp       [esi+12h],di

LAB_0046090f:
  0046090f  7d13                  jge       short 00460924h
  00460911  6a00                  push      0
  00460913  6a00                  push      0
  00460915  6aff                  push      0FFFFFFFFh
  00460917  6a00                  push      0
  00460919  6a33                  push      33h
  0046091b  56                    push      esi
  0046091c  e8ef770000            call      00468110h
  00460921  83c418                add       esp,18h

LAB_00460924:
  00460924  668b4e14              mov       cx,[esi+14h]
  00460928  668b4612              mov       ax,[esi+12h]
  0046092c  66014e32              add       [esi+32h],cx
  00460930  668b5616              mov       dx,[esi+16h]
  00460934  66015634              add       [esi+34h],dx
  00460938  8b5620                mov       edx,[esi+20h]
  0046093b  0fbfc8                movsx     ecx,ax
  0046093e  c1e10c                shl       ecx,0Ch
  00460941  2bd1                  sub       edx,ecx
  00460943  40                    inc       eax
  00460944  895620                mov       [esi+20h],edx
  00460947  81e200f0ffff          and       edx,0FFFFF000h
  0046094d  81fa0000ceff          cmp       edx,0FFCE0000h
  00460953  66894612              mov       [esi+12h],ax
  00460957  7d06                  jge       short 0046095Fh
  00460959  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0046095f:
  0046095f  663bc7                cmp       ax,di
  00460962  7c21                  jl        short 00460985h
  00460964  663d1e00              cmp       ax,1Eh
  00460968  7f1b                  jg        short 00460985h
  0046096a  bf03000000            mov       edi,3

LAB_0046096f:
  0046096f  6a00                  push      0
  00460971  6a00                  push      0
  00460973  6aff                  push      0FFFFFFFFh
  00460975  6a00                  push      0
  00460977  6a36                  push      36h
  00460979  56                    push      esi
  0046097a  e891770000            call      00468110h
  0046097f  83c418                add       esp,18h
  00460982  4f                    dec       edi
  00460983  75ea                  jne       short 0046096Fh

LAB_00460985:
  00460985  5f                    pop       edi
  00460986  5e                    pop       esi
  00460987  5b                    pop       ebx
  00460988  c3                    ret

9 cible(s) de saut interne : 0x0046083c, 0x0046087d, 0x00460895, 0x004608cb, 0x0046090f, 0x00460924, 0x0046095f, 0x0046096f, 0x00460985
