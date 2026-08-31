Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044b6f0  section .text  offset fichier 0x4aaf0  544 octets

  0044b6f0  56                    push      esi
  0044b6f1  8b742408              mov       esi,[esp+8]
  0044b6f5  33c9                  xor       ecx,ecx
  0044b6f7  8b4678                mov       eax,[esi+78h]
  0044b6fa  3bc1                  cmp       eax,ecx
  0044b6fc  7505                  jne       short 0044B703h
  0044b6fe  b858e55d00            mov       eax,5DE558h

LAB_0044b703:
  0044b703  8b501c                mov       edx,[eax+1Ch]
  0044b706  66394e10              cmp       [esi+10h],cx
  0044b70a  89561c                mov       [esi+1Ch],edx
  0044b70d  8b5020                mov       edx,[eax+20h]
  0044b710  895620                mov       [esi+20h],edx
  0044b713  8b4024                mov       eax,[eax+24h]
  0044b716  894624                mov       [esi+24h],eax
  0044b719  b801000000            mov       eax,1
  0044b71e  752b                  jne       short 0044B74Bh
  0044b720  c6460403              mov       byte [esi+4],3
  0044b724  c6460a0d              mov       byte [esi+0Ah],0Dh
  0044b728  884e0b                mov       [esi+0Bh],cl
  0044b72b  66894610              mov       [esi+10h],ax
  0044b72f  66c746289001          mov       word [esi+28h],190h
  0044b735  66c7462c9001          mov       word [esi+2Ch],190h
  0044b73b  66c7462a0010          mov       word [esi+2Ah],1000h
  0044b741  66c74612f8ff          mov       word [esi+12h],0FFF8h
  0044b747  66894e14              mov       [esi+14h],cx

LAB_0044b74b:
  0044b74b  66394610              cmp       [esi+10h],ax
  0044b74f  7557                  jne       short 0044B7A8h
  0044b751  0fbf4e14              movsx     ecx,word [esi+14h]
  0044b755  51                    push      ecx
  0044b756  e8c1a20f00            call      00545A1Ch
  0044b75b  0fbf5614              movsx     edx,word [esi+14h]
  0044b75f  d1f8                  sar       eax,1
  0044b761  0590010000            add       eax,190h
  0044b766  52                    push      edx
  0044b767  66894628              mov       [esi+28h],ax
  0044b76b  e8aca20f00            call      00545A1Ch
  0044b770  6683461208            add       word [esi+12h],8
  0044b775  6683461460            add       word [esi+14h],60h
  0044b77a  d1f8                  sar       eax,1
  0044b77c  0590010000            add       eax,190h
  0044b781  8b5620                mov       edx,[esi+20h]
  0044b784  6689462c              mov       [esi+2Ch],ax
  0044b788  668b4612              mov       ax,[esi+12h]
  0044b78c  668b4e14              mov       cx,[esi+14h]
  0044b790  0fbfc0                movsx     eax,ax
  0044b793  c1e00c                shl       eax,0Ch
  0044b796  03d0                  add       edx,eax
  0044b798  6681f96c07            cmp       cx,76Ch
  0044b79d  895620                mov       [esi+20h],edx
  0044b7a0  7c06                  jl        short 0044B7A8h
  0044b7a2  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0044b7a8:
  0044b7a8  5e                    pop       esi
  0044b7a9  c3                    ret
  0044b7aa  90                    nop
  0044b7ab  90                    nop
  0044b7ac  90                    nop
  0044b7ad  90                    nop
  0044b7ae  90                    nop
  0044b7af  90                    nop
  0044b7b0  53                    push      ebx
  0044b7b1  56                    push      esi
  0044b7b2  8b74240c              mov       esi,[esp+0Ch]
  0044b7b6  57                    push      edi
  0044b7b7  6a00                  push      0
  0044b7b9  6a00                  push      0
  0044b7bb  56                    push      esi
  0044b7bc  e87fc8ffff            call      00448040h
  0044b7c1  83c40c                add       esp,0Ch
  0044b7c4  66837e1000            cmp       word [esi+10h],0
  0044b7c9  750d                  jne       short 0044B7D8h
  0044b7cb  6a00                  push      0
  0044b7cd  6a00                  push      0
  0044b7cf  56                    push      esi
  0044b7d0  e8dbc4ffff            call      00447CB0h
  0044b7d5  83c40c                add       esp,0Ch

LAB_0044b7d8:
  0044b7d8  66837e1000            cmp       word [esi+10h],0
  0044b7dd  bf01000000            mov       edi,1
  0044b7e2  7522                  jne       short 0044B806h
  0044b7e4  6aff                  push      0FFFFFFFFh
  0044b7e6  6a00                  push      0
  0044b7e8  688f000000            push      8Fh
  0044b7ed  8d461c                lea       eax,[esi+1Ch]
  0044b7f0  6a00                  push      0
  0044b7f2  50                    push      eax
  0044b7f3  56                    push      esi
  0044b7f4  66897e10              mov       [esi+10h],di
  0044b7f8  66c746160000          mov       word [esi+16h],0
  0044b7fe  e83df40200            call      0047AC40h
  0044b803  83c418                add       esp,18h

LAB_0044b806:
  0044b806  66837e1600            cmp       word [esi+16h],0
  0044b80b  7519                  jne       short 0044B826h
  0044b80d  6800100000            push      1000h
  0044b812  6800100000            push      1000h
  0044b817  6829030000            push      329h
  0044b81c  6a21                  push      21h
  0044b81e  e87d160f00            call      0053CEA0h
  0044b823  83c410                add       esp,10h

LAB_0044b826:
  0044b826  66397e16              cmp       [esi+16h],di
  0044b82a  7519                  jne       short 0044B845h
  0044b82c  6800100000            push      1000h
  0044b831  6800100000            push      1000h
  0044b836  6800080000            push      800h
  0044b83b  6a0c                  push      0Ch
  0044b83d  e85e160f00            call      0053CEA0h
  0044b842  83c410                add       esp,10h

LAB_0044b845:
  0044b845  66837e1602            cmp       word [esi+16h],2
  0044b84a  7519                  jne       short 0044B865h
  0044b84c  6800100000            push      1000h
  0044b851  6800100000            push      1000h
  0044b856  6800040000            push      400h
  0044b85b  6a0f                  push      0Fh
  0044b85d  e83e160f00            call      0053CEA0h
  0044b862  83c410                add       esp,10h

LAB_0044b865:
  0044b865  66837e1603            cmp       word [esi+16h],3
  0044b86a  7519                  jne       short 0044B885h
  0044b86c  6800100000            push      1000h
  0044b871  6800100000            push      1000h
  0044b876  6829030000            push      329h
  0044b87b  6a10                  push      10h
  0044b87d  e81e160f00            call      0053CEA0h
  0044b882  83c410                add       esp,10h

LAB_0044b885:
  0044b885  668b4616              mov       ax,[esi+16h]
  0044b889  663d0500              cmp       ax,5
  0044b88d  7406                  je        short 0044B895h
  0044b88f  663d0a00              cmp       ax,0Ah
  0044b893  753b                  jne       short 0044B8D0h

LAB_0044b895:
  0044b895  668b4e32              mov       cx,[esi+32h]
  0044b899  66c746320000          mov       word [esi+32h],0
  0044b89f  66894c2410            mov       [esp+10h],cx
  0044b8a4  bf08000000            mov       edi,8
  0044b8a9  bb00020000            mov       ebx,200h

LAB_0044b8ae:
  0044b8ae  6aff                  push      0FFFFFFFFh
  0044b8b0  6a00                  push      0
  0044b8b2  6891000000            push      91h
  0044b8b7  56                    push      esi
  0044b8b8  e823f20200            call      0047AAE0h
  0044b8bd  66015e32              add       [esi+32h],bx
  0044b8c1  83c410                add       esp,10h
  0044b8c4  4f                    dec       edi
  0044b8c5  75e7                  jne       short 0044B8AEh
  0044b8c7  668b542410            mov       dx,[esp+10h]
  0044b8cc  66895632              mov       [esi+32h],dx

LAB_0044b8d0:
  0044b8d0  66ff4616              inc       word [esi+16h]
  0044b8d4  66837e161e            cmp       word [esi+16h],1Eh
  0044b8d9  7531                  jne       short 0044B90Ch
  0044b8db  8b4674                mov       eax,[esi+74h]
  0044b8de  8a4e06                mov       cl,[esi+6]
  0044b8e1  6a00                  push      0
  0044b8e3  884806                mov       [eax+6],cl
  0044b8e6  8b5674                mov       edx,[esi+74h]
  0044b8e9  8a4607                mov       al,[esi+7]
  0044b8ec  884207                mov       [edx+7],al
  0044b8ef  8b4e78                mov       ecx,[esi+78h]
  0044b8f2  8b5674                mov       edx,[esi+74h]
  0044b8f5  51                    push      ecx
  0044b8f6  52                    push      edx
  0044b8f7  e814000000            call      0044B910h
  0044b8fc  83c40c                add       esp,0Ch
  0044b8ff  6a00                  push      0
  0044b901  6a00                  push      0
  0044b903  56                    push      esi
  0044b904  e817c7ffff            call      00448020h
  0044b909  83c40c                add       esp,0Ch

LAB_0044b90c:
  0044b90c  5f                    pop       edi
  0044b90d  5e                    pop       esi
  0044b90e  5b                    pop       ebx
  0044b90f  c3                    ret

13 cible(s) de saut interne : 0x0044b703, 0x0044b74b, 0x0044b7a8, 0x0044b7d8, 0x0044b806, 0x0044b826, 0x0044b845, 0x0044b865, 0x0044b885, 0x0044b895, 0x0044b8ae, 0x0044b8d0, 0x0044b90c
