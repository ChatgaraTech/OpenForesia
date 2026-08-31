
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047d670  section .text  offset fichier 0x7ca70  730 octets

  0047d670  83ec20                sub       esp,20h
  0047d673  53                    push      ebx
  0047d674  55                    push      ebp
  0047d675  56                    push      esi
  0047d676  8b742430              mov       esi,[esp+30h]
  0047d67a  33db                  xor       ebx,ebx
  0047d67c  57                    push      edi
  0047d67d  66395e10              cmp       [esi+10h],bx
  0047d681  c744241c101b5500      mov       dword [esp+1Ch],551B10h
  0047d689  c7442420181b5500      mov       dword [esp+20h],551B18h
  0047d691  c7442424201b5500      mov       dword [esp+24h],551B20h
  0047d699  c7442428b81b5500      mov       dword [esp+28h],551BB8h
  0047d6a1  c744242ca81b5500      mov       dword [esp+2Ch],551BA8h
  0047d6a9  66895c2410            mov       [esp+10h],bx
  0047d6ae  66c744241200ff        mov       word [esp+12h],0FF00h
  0047d6b5  66c74424140001        mov       word [esp+14h],100h
  0047d6bc  66c74424169001        mov       word [esp+16h],190h
  0047d6c3  66895c2418            mov       [esp+18h],bx
  0047d6c8  66c744241a0002        mov       word [esp+1Ah],200h
  0047d6cf  7525                  jne       short 0047D6F6h
  0047d6d1  668b4632              mov       ax,[esi+32h]
  0047d6d5  8b16                  mov       edx,[esi]
  0047d6d7  66894612              mov       [esi+12h],ax
  0047d6db  8b4674                mov       eax,[esi+74h]
  0047d6de  80ca80                or        dl,80h
  0047d6e1  66c746100100          mov       word [esi+10h],1
  0047d6e7  8916                  mov       [esi],edx
  0047d6e9  895e5c                mov       [esi+5Ch],ebx
  0047d6ec  897670                mov       [esi+70h],esi
  0047d6ef  8b08                  mov       ecx,[eax]
  0047d6f1  80c980                or        cl,80h
  0047d6f4  8908                  mov       [eax],ecx

LAB_0047d6f6:
  0047d6f6  817e38f0f0f000        cmp       dword [esi+38h],0F0F0F0h
  0047d6fd  7371                  jae       short 0047D770h
  0047d6ff  33c9                  xor       ecx,ecx
  0047d701  baff000000            mov       edx,0FFh
  0047d706  8a4e38                mov       cl,[esi+38h]
  0047d709  2bd1                  sub       edx,ecx
  0047d70b  c1fa03                sar       edx,3
  0047d70e  03ca                  add       ecx,edx
  0047d710  8bc1                  mov       eax,ecx
  0047d712  c1e008                shl       eax,8
  0047d715  0bc1                  or        eax,ecx
  0047d717  c1e008                shl       eax,8
  0047d71a  0bc1                  or        eax,ecx
  0047d71c  3df0f0f000            cmp       eax,0F0F0F0h
  0047d721  894638                mov       [esi+38h],eax
  0047d724  751e                  jne       short 0047D744h
  0047d726  8b4674                mov       eax,[esi+74h]
  0047d729  8b38                  mov       edi,[eax]
  0047d72b  81e77fffffff          and       edi,0FFFFFF7Fh
  0047d731  8938                  mov       [eax],edi
  0047d733  8d461c                lea       eax,[esi+1Ch]
  0047d736  50                    push      eax
  0047d737  6830255500            push      552530h
  0047d73c  e89f040c00            call      0053DBE0h
  0047d741  83c408                add       esp,8

LAB_0047d744:
  0047d744  0fbf4e28              movsx     ecx,word [esi+28h]
  0047d748  8b6e20                mov       ebp,[esi+20h]
  0047d74b  b800200000            mov       eax,2000h
  0047d750  2bc1                  sub       eax,ecx
  0047d752  c1f803                sar       eax,3
  0047d755  03e8                  add       ebp,eax
  0047d757  8d0441                lea       eax,[ecx+eax*2]
  0047d75a  896e20                mov       [esi+20h],ebp
  0047d75d  8bc8                  mov       ecx,eax
  0047d75f  6689462c              mov       [esi+2Ch],ax
  0047d763  c1e110                shl       ecx,10h
  0047d766  0bc8                  or        ecx,eax
  0047d768  894e28                mov       [esi+28h],ecx
  0047d76b  e9d9000000            jmp       0047D849h

LAB_0047d770:
  0047d770  0fbf6e12              movsx     ebp,word [esi+12h]
  0047d774  6683463260            add       word [esi+32h],60h
  0047d779  8b7e5c                mov       edi,[esi+5Ch]
  0047d77c  55                    push      ebp
  0047d77d  8d5e1c                lea       ebx,[esi+1Ch]
  0047d780  e897820c00            call      00545A1Ch
  0047d785  0fafc7                imul      eax,edi
  0047d788  8b0b                  mov       ecx,[ebx]
  0047d78a  55                    push      ebp
  0047d78b  c1f80c                sar       eax,0Ch
  0047d78e  03c8                  add       ecx,eax
  0047d790  890b                  mov       [ebx],ecx
  0047d792  e87f820c00            call      00545A16h
  0047d797  0fafc7                imul      eax,edi
  0047d79a  8b5624                mov       edx,[esi+24h]
  0047d79d  6aff                  push      0FFFFFFFFh
  0047d79f  c1f80c                sar       eax,0Ch
  0047d7a2  03d0                  add       edx,eax
  0047d7a4  33c0                  xor       eax,eax
  0047d7a6  8a460b                mov       al,[esi+0Bh]
  0047d7a9  895624                mov       [esi+24h],edx
  0047d7ac  ba00a00000            mov       edx,0A000h
  0047d7b1  50                    push      eax
  0047d7b2  2bd7                  sub       edx,edi
  0047d7b4  6a4e                  push      4Eh
  0047d7b6  c1fa02                sar       edx,2
  0047d7b9  03d7                  add       edx,edi
  0047d7bb  89565c                mov       [esi+5Ch],edx
  0047d7be  e87d990c00            call      00547140h
  0047d7c3  99                    cdq
  0047d7c4  b905000000            mov       ecx,5
  0047d7c9  f7f9                  idiv      ecx
  0047d7cb  8b549428              mov       edx,[esp+edx*4+28h]
  0047d7cf  52                    push      edx
  0047d7d0  53                    push      ebx
  0047d7d1  56                    push      esi
  0047d7d2  e869d4ffff            call      0047AC40h
  0047d7d7  83c418                add       esp,18h
  0047d7da  8d442410              lea       eax,[esp+10h]
  0047d7de  50                    push      eax
  0047d7df  56                    push      esi
  0047d7e0  e8bb74ffff            call      00474CA0h
  0047d7e5  8bf8                  mov       edi,eax
  0047d7e7  83c408                add       esp,8
  0047d7ea  85ff                  test      edi,edi
  0047d7ec  7455                  je        short 0047D843h
  0047d7ee  8b4674                mov       eax,[esi+74h]
  0047d7f1  33c9                  xor       ecx,ecx
  0047d7f3  8a4e06                mov       cl,[esi+6]
  0047d7f6  33d2                  xor       edx,edx
  0047d7f8  8a5607                mov       dl,[esi+7]
  0047d7fb  6a00                  push      0
  0047d7fd  51                    push      ecx
  0047d7fe  52                    push      edx
  0047d7ff  57                    push      edi
  0047d800  50                    push      eax
  0047d801  e8aaca0b00            call      0053A2B0h
  0047d806  8b07                  mov       eax,[edi]
  0047d808  83c414                add       esp,14h
  0047d80b  0c40                  or        al,40h
  0047d80d  33c9                  xor       ecx,ecx
  0047d80f  66c787960000001e00    mov       word [edi+96h],1Eh
  0047d818  8907                  mov       [edi],eax
  0047d81a  8a4e0b                mov       cl,[esi+0Bh]
  0047d81d  6aff                  push      0FFFFFFFFh
  0047d81f  51                    push      ecx
  0047d820  6a72                  push      72h
  0047d822  56                    push      esi
  0047d823  897e78                mov       [esi+78h],edi
  0047d826  e8b5d2ffff            call      0047AAE0h
  0047d82b  83c410                add       esp,10h
  0047d82e  53                    push      ebx
  0047d82f  6800120000            push      1200h
  0047d834  68fd060000            push      6FDh
  0047d839  6a19                  push      19h
  0047d83b  e8e0020c00            call      0053DB20h
  0047d840  83c410                add       esp,10h

LAB_0047d843:
  0047d843  66ff4610              inc       word [esi+10h]
  0047d847  33db                  xor       ebx,ebx

LAB_0047d849:
  0047d849  8b7e20                mov       edi,[esi+20h]
  0047d84c  56                    push      esi
  0047d84d  e8ee600b00            call      00533940h
  0047d852  8b4660                mov       eax,[esi+60h]
  0047d855  83c404                add       esp,4
  0047d858  3bf8                  cmp       edi,eax
  0047d85a  897e20                mov       [esi+20h],edi
  0047d85d  7f0d                  jg        short 0047D86Ch
  0047d85f  894620                mov       [esi+20h],eax
  0047d862  c7869000000000080000  mov       dword [esi+90h],800h

LAB_0047d86c:
  0047d86c  f70600000040          test      dword [esi],40000000h
  0047d872  7510                  jne       short 0047D884h
  0047d874  f7869000000002880000  test      dword [esi+90h],8802h
  0047d87e  0f8492000000          je        near 0047D916h

LAB_0047d884:
  0047d884  66c7869a0000000060    mov       word [esi+9Ah],6000h
  0047d88d  8d6e1c                lea       ebp,[esi+1Ch]
  0047d890  bb04000000            mov       ebx,4

LAB_0047d895:
  0047d895  e8a6980c00            call      00547140h
  0047d89a  8bf8                  mov       edi,eax
  0047d89c  57                    push      edi
  0047d89d  e87a810c00            call      00545A1Ch
  0047d8a2  c1e004                shl       eax,4
  0047d8a5  894650                mov       [esi+50h],eax
  0047d8a8  e893980c00            call      00547140h
  0047d8ad  83e01f                and       eax,1Fh
  0047d8b0  57                    push      edi
  0047d8b1  83c01e                add       eax,1Eh
  0047d8b4  c1e00c                shl       eax,0Ch
  0047d8b7  894654                mov       [esi+54h],eax
  0047d8ba  e857810c00            call      00545A16h
  0047d8bf  33d2                  xor       edx,edx
  0047d8c1  6aff                  push      0FFFFFFFFh
  0047d8c3  8a560b                mov       dl,[esi+0Bh]
  0047d8c6  c74638f0f0f001        mov       dword [esi+38h],1F0F0F0h
  0047d8cd  c1e004                shl       eax,4
  0047d8d0  52                    push      edx
  0047d8d1  68bf000000            push      0BFh
  0047d8d6  894658                mov       [esi+58h],eax
  0047d8d9  c7462800188001        mov       dword [esi+28h],1801800h
  0047d8e0  66c7462c1f00          mov       word [esi+2Ch],1Fh
  0047d8e6  66c7461a0100          mov       word [esi+1Ah],1
  0047d8ec  e84f980c00            call      00547140h
  0047d8f1  99                    cdq
  0047d8f2  b903000000            mov       ecx,3
  0047d8f7  f7f9                  idiv      ecx
  0047d8f9  8d14d5101b5500        lea       edx,[edx*8+551B10h]
  0047d900  52                    push      edx
  0047d901  55                    push      ebp
  0047d902  56                    push      esi
  0047d903  e838d3ffff            call      0047AC40h
  0047d908  83c418                add       esp,18h
  0047d90b  4b                    dec       ebx
  0047d90c  7587                  jne       short 0047D895h
  0047d90e  66c746105000          mov       word [esi+10h],50h
  0047d914  33db                  xor       ebx,ebx

LAB_0047d916:
  0047d916  66837e1050            cmp       word [esi+10h],50h
  0047d91b  7c11                  jl        short 0047D92Eh
  0047d91d  66891d66915e00        mov       [5E9166h],bx
  0047d924  891e                  mov       [esi],ebx
  0047d926  5f                    pop       edi
  0047d927  5e                    pop       esi
  0047d928  5d                    pop       ebp
  0047d929  5b                    pop       ebx
  0047d92a  83c420                add       esp,20h
  0047d92d  c3                    ret

LAB_0047d92e:
  0047d92e  5f                    pop       edi
  0047d92f  5e                    pop       esi
  0047d930  5d                    pop       ebp
  0047d931  c70560915e0040004000  mov       dword [5E9160h],400040h
  0047d93b  c70564915e0080000200  mov       dword [5E9164h],20080h
  0047d945  5b                    pop       ebx
  0047d946  83c420                add       esp,20h
  0047d949  c3                    ret

10 cible(s) de saut interne : 0x0047d6f6, 0x0047d744, 0x0047d770, 0x0047d843, 0x0047d849, 0x0047d86c, 0x0047d884, 0x0047d895, 0x0047d916, 0x0047d92e
