
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047c450  section .text  offset fichier 0x7b850  520 octets

  0047c450  83ec20                sub       esp,20h
  0047c453  53                    push      ebx
  0047c454  55                    push      ebp
  0047c455  56                    push      esi
  0047c456  8b742430              mov       esi,[esp+30h]
  0047c45a  33c0                  xor       eax,eax
  0047c45c  57                    push      edi
  0047c45d  66394610              cmp       [esi+10h],ax
  0047c461  6689442424            mov       [esp+24h],ax
  0047c466  66c7442426d8ff        mov       word [esp+26h],0FFD8h
  0047c46d  6689442428            mov       [esp+28h],ax
  0047c472  66c744242a6400        mov       word [esp+2Ah],64h
  0047c479  668944242c            mov       [esp+2Ch],ax
  0047c47e  66c744242e5000        mov       word [esp+2Eh],50h
  0047c485  750c                  jne       short 0047C493h
  0047c487  8b06                  mov       eax,[esi]
  0047c489  66c746100100          mov       word [esi+10h],1
  0047c48f  0c80                  or        al,80h
  0047c491  8906                  mov       [esi],eax

LAB_0047c493:
  0047c493  8b8680000000          mov       eax,[esi+80h]
  0047c499  8b8e84000000          mov       ecx,[esi+84h]
  0047c49f  8b7e1c                mov       edi,[esi+1Ch]
  0047c4a2  8b9688000000          mov       edx,[esi+88h]
  0047c4a8  89442434              mov       [esp+34h],eax
  0047c4ac  8b868c000000          mov       eax,[esi+8Ch]
  0047c4b2  8d5e1c                lea       ebx,[esi+1Ch]
  0047c4b5  894c2410              mov       [esp+10h],ecx
  0047c4b9  8b8e90000000          mov       ecx,[esi+90h]
  0047c4bf  89442418              mov       [esp+18h],eax
  0047c4c3  8b4650                mov       eax,[esi+50h]
  0047c4c6  894c241c              mov       [esp+1Ch],ecx
  0047c4ca  89542414              mov       [esp+14h],edx
  0047c4ce  8b5624                mov       edx,[esi+24h]
  0047c4d1  8d0c38                lea       ecx,[eax+edi]
  0047c4d4  89542420              mov       [esp+20h],edx
  0047c4d8  890b                  mov       [ebx],ecx
  0047c4da  8b4e58                mov       ecx,[esi+58h]
  0047c4dd  03d1                  add       edx,ecx
  0047c4df  8be8                  mov       ebp,eax
  0047c4e1  895624                mov       [esi+24h],edx
  0047c4e4  8bd0                  mov       edx,eax
  0047c4e6  c1fa08                sar       edx,8
  0047c4e9  2bea                  sub       ebp,edx
  0047c4eb  8bd1                  mov       edx,ecx
  0047c4ed  c1f805                sar       eax,5
  0047c4f0  2be8                  sub       ebp,eax
  0047c4f2  8bc1                  mov       eax,ecx
  0047c4f4  c1f805                sar       eax,5
  0047c4f7  2bd0                  sub       edx,eax
  0047c4f9  896e50                mov       [esi+50h],ebp
  0047c4fc  8b6e20                mov       ebp,[esi+20h]
  0047c4ff  56                    push      esi
  0047c500  c1f908                sar       ecx,8
  0047c503  2bd1                  sub       edx,ecx
  0047c505  895658                mov       [esi+58h],edx
  0047c508  e833740b00            call      00533940h
  0047c50d  8b8690000000          mov       eax,[esi+90h]
  0047c513  83c404                add       esp,4
  0047c516  a902080000            test      eax,802h
  0047c51b  896e20                mov       [esi+20h],ebp
  0047c51e  7451                  je        short 0047C571h
  0047c520  8b442434              mov       eax,[esp+34h]
  0047c524  8b4c2410              mov       ecx,[esp+10h]
  0047c528  8b542414              mov       edx,[esp+14h]
  0047c52c  668146320008          add       word [esi+32h],800h
  0047c532  898680000000          mov       [esi+80h],eax
  0047c538  8b442418              mov       eax,[esp+18h]
  0047c53c  898e84000000          mov       [esi+84h],ecx
  0047c542  8b4c241c              mov       ecx,[esp+1Ch]
  0047c546  89868c000000          mov       [esi+8Ch],eax
  0047c54c  8b4650                mov       eax,[esi+50h]
  0047c54f  898e90000000          mov       [esi+90h],ecx
  0047c555  8b4e58                mov       ecx,[esi+58h]
  0047c558  899688000000          mov       [esi+88h],edx
  0047c55e  8b542420              mov       edx,[esp+20h]
  0047c562  f7d8                  neg       eax
  0047c564  f7d9                  neg       ecx
  0047c566  893b                  mov       [ebx],edi
  0047c568  895624                mov       [esi+24h],edx
  0047c56b  894650                mov       [esi+50h],eax
  0047c56e  894e58                mov       [esi+58h],ecx

LAB_0047c571:
  0047c571  0fbf4628              movsx     eax,word [esi+28h]
  0047c575  ba00300000            mov       edx,3000h
  0047c57a  8b6e38                mov       ebp,[esi+38h]
  0047c57d  2bd0                  sub       edx,eax
  0047c57f  81c5f8f7f7ff          add       ebp,0FFF7F7F8h
  0047c585  c1ea03                shr       edx,3
  0047c588  03c2                  add       eax,edx
  0047c58a  896e38                mov       [esi+38h],ebp
  0047c58d  8bc8                  mov       ecx,eax
  0047c58f  8bd0                  mov       edx,eax
  0047c591  c1e110                shl       ecx,10h
  0047c594  0bc8                  or        ecx,eax
  0047c596  894e28                mov       [esi+28h],ecx
  0047c599  8bc8                  mov       ecx,eax
  0047c59b  d1ea                  shr       edx,1
  0047c59d  6689562c              mov       [esi+2Ch],dx
  0047c5a1  8d542424              lea       edx,[esp+24h]
  0047c5a5  c1e907                shr       ecx,7
  0047c5a8  c1e806                shr       eax,6
  0047c5ab  83c11e                add       ecx,1Eh
  0047c5ae  83c01e                add       eax,1Eh
  0047c5b1  52                    push      edx
  0047c5b2  56                    push      esi
  0047c5b3  66894c2430            mov       [esp+30h],cx
  0047c5b8  6689442432            mov       [esp+32h],ax
  0047c5bd  e8de86ffff            call      00474CA0h
  0047c5c2  8bf8                  mov       edi,eax
  0047c5c4  83c408                add       esp,8
  0047c5c7  85ff                  test      edi,edi
  0047c5c9  7465                  je        short 0047C630h
  0047c5cb  8b5674                mov       edx,[esi+74h]
  0047c5ce  33c0                  xor       eax,eax
  0047c5d0  8a4606                mov       al,[esi+6]
  0047c5d3  33c9                  xor       ecx,ecx
  0047c5d5  8a4e07                mov       cl,[esi+7]
  0047c5d8  6a00                  push      0
  0047c5da  50                    push      eax
  0047c5db  51                    push      ecx
  0047c5dc  57                    push      edi
  0047c5dd  52                    push      edx
  0047c5de  e8cddc0b00            call      0053A2B0h
  0047c5e3  0fbf6e32              movsx     ebp,word [esi+32h]
  0047c5e7  83c414                add       esp,14h
  0047c5ea  55                    push      ebp
  0047c5eb  e82c940c00            call      00545A1Ch
  0047c5f0  8b4f50                mov       ecx,[edi+50h]
  0047c5f3  55                    push      ebp
  0047c5f4  c1e005                shl       eax,5
  0047c5f7  03c8                  add       ecx,eax
  0047c5f9  894f50                mov       [edi+50h],ecx
  0047c5fc  e815940c00            call      00545A16h
  0047c601  8b4f58                mov       ecx,[edi+58h]
  0047c604  53                    push      ebx
  0047c605  c1e005                shl       eax,5
  0047c608  03c8                  add       ecx,eax
  0047c60a  8b07                  mov       eax,[edi]
  0047c60c  6800120000            push      1200h
  0047c611  0c40                  or        al,40h
  0047c613  68a40e0000            push      0EA4h
  0047c618  6a1a                  push      1Ah
  0047c61a  894f58                mov       [edi+58h],ecx
  0047c61d  66c787960000001000    mov       word [edi+96h],10h
  0047c626  8907                  mov       [edi],eax
  0047c628  e8f3140c00            call      0053DB20h
  0047c62d  83c410                add       esp,10h

LAB_0047c630:
  0047c630  f74638ffffff00        test      dword [esi+38h],0FFFFFFh
  0047c637  7408                  je        short 0047C641h
  0047c639  f70600000040          test      dword [esi],40000000h
  0047c63f  740f                  je        short 0047C650h

LAB_0047c641:
  0047c641  c70600000000          mov       dword [esi],0
  0047c647  66c70566915e000000    mov       word [5E9166h],0

LAB_0047c650:
  0047c650  5f                    pop       edi
  0047c651  5e                    pop       esi
  0047c652  5d                    pop       ebp
  0047c653  5b                    pop       ebx
  0047c654  83c420                add       esp,20h
  0047c657  c3                    ret

5 cible(s) de saut interne : 0x0047c493, 0x0047c571, 0x0047c630, 0x0047c641, 0x0047c650
