Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004755f0  section .text  offset fichier 0x749f0  313 octets

  004755f0  83ec14                sub       esp,14h
  004755f3  53                    push      ebx
  004755f4  55                    push      ebp
  004755f5  56                    push      esi
  004755f6  8b742424              mov       esi,[esp+24h]
  004755fa  57                    push      edi
  004755fb  668b4610              mov       ax,[esi+10h]
  004755ff  8b6e6c                mov       ebp,[esi+6Ch]
  00475602  6685c0                test      ax,ax
  00475605  751e                  jne       short 00475625h
  00475607  6689460a              mov       [esi+0Ah],ax
  0047560b  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  00475612  c7462800180018        mov       dword [esi+28h],18001800h
  00475619  884640                mov       [esi+40h],al
  0047561c  66c7869a0000000040    mov       word [esi+9Ah],4000h

LAB_00475625:
  00475625  663d0100              cmp       ax,1
  00475629  7511                  jne       short 0047563Ch
  0047562b  8d461c                lea       eax,[esi+1Ch]
  0047562e  50                    push      eax
  0047562f  6858185500            push      551858h
  00475634  e8a7850c00            call      0053DBE0h
  00475639  83c408                add       esp,8

LAB_0047563c:
  0047563c  f6461001              test      byte [esi+10h],1
  00475640  7560                  jne       short 004756A2h
  00475642  e8f91a0d00            call      00547140h
  00475647  8bf8                  mov       edi,eax
  00475649  81e7ff0f0000          and       edi,0FFFh
  0047564f  e8ec1a0d00            call      00547140h
  00475654  99                    cdq
  00475655  b93c000000            mov       ecx,3Ch
  0047565a  57                    push      edi
  0047565b  f7f9                  idiv      ecx
  0047565d  8bda                  mov       ebx,edx
  0047565f  e8b8030d00            call      00545A1Ch
  00475664  0fafc3                imul      eax,ebx
  00475667  8b4d1c                mov       ecx,[ebp+1Ch]
  0047566a  57                    push      edi
  0047566b  03c1                  add       eax,ecx
  0047566d  8944241c              mov       [esp+1Ch],eax
  00475671  8b5520                mov       edx,[ebp+20h]
  00475674  89542420              mov       [esp+20h],edx
  00475678  e899030d00            call      00545A16h
  0047567d  0fafc3                imul      eax,ebx
  00475680  8b4d24                mov       ecx,[ebp+24h]
  00475683  6a03                  push      3
  00475685  03c1                  add       eax,ecx
  00475687  6a00                  push      0
  00475689  89442428              mov       [esp+28h],eax
  0047568d  6a3d                  push      3Dh
  0047568f  8d442424              lea       eax,[esp+24h]
  00475693  68d0195500            push      5519D0h
  00475698  50                    push      eax
  00475699  56                    push      esi
  0047569a  e8a1550000            call      0047AC40h
  0047569f  83c418                add       esp,18h

LAB_004756a2:
  004756a2  66ff4610              inc       word [esi+10h]
  004756a6  66837e1008            cmp       word [esi+10h],8
  004756ab  7c74                  jl        short 00475721h
  004756ad  6a27                  push      27h
  004756af  6820de5e00            push      5EDE20h
  004756b4  56                    push      esi
  004756b5  c70600004080          mov       dword [esi],80400000h
  004756bb  e820fc0b00            call      005352E0h
  004756c0  83c40c                add       esp,0Ch
  004756c3  8d4c2428              lea       ecx,[esp+28h]
  004756c7  8d542410              lea       edx,[esp+10h]
  004756cb  8d442414              lea       eax,[esp+14h]
  004756cf  51                    push      ecx
  004756d0  52                    push      edx
  004756d1  50                    push      eax
  004756d2  6820de5e00            push      5EDE20h
  004756d7  c60528de5e0020        mov       byte [5EDE28h],20h
  004756de  e89d320c00            call      00538980h
  004756e3  8b4c2438              mov       ecx,[esp+38h]
  004756e7  8b542420              mov       edx,[esp+20h]
  004756eb  8b442424              mov       eax,[esp+24h]
  004756ef  83c410                add       esp,10h
  004756f2  51                    push      ecx
  004756f3  52                    push      edx
  004756f4  50                    push      eax
  004756f5  e8e6320c00            call      005389E0h
  004756fa  668b4e06              mov       cx,[esi+6]
  004756fe  83c40c                add       esp,0Ch
  00475701  66890d26de5e00        mov       [5EDE26h],cx
  00475708  8b566c                mov       edx,[esi+6Ch]
  0047570b  6a00                  push      0
  0047570d  52                    push      edx
  0047570e  6820de5e00            push      5EDE20h
  00475713  e8086d0c00            call      0053C420h
  00475718  83c40c                add       esp,0Ch
  0047571b  c70600000000          mov       dword [esi],0

LAB_00475721:
  00475721  5f                    pop       edi
  00475722  5e                    pop       esi
  00475723  5d                    pop       ebp
  00475724  5b                    pop       ebx
  00475725  83c414                add       esp,14h
  00475728  c3                    ret

4 cible(s) de saut interne : 0x00475625, 0x0047563c, 0x004756a2, 0x00475721
