
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00475a80  section .text  offset fichier 0x74e80  272 octets

  00475a80  83ec08                sub       esp,8
  00475a83  56                    push      esi
  00475a84  8b742410              mov       esi,[esp+10h]
  00475a88  57                    push      edi
  00475a89  66837e1001            cmp       word [esi+10h],1
  00475a8e  7518                  jne       short 00475AA8h
  00475a90  8d461c                lea       eax,[esi+1Ch]
  00475a93  50                    push      eax
  00475a94  6800120000            push      1200h
  00475a99  6800020000            push      200h
  00475a9e  6a1a                  push      1Ah
  00475aa0  e87b800c00            call      0053DB20h
  00475aa5  83c410                add       esp,10h

LAB_00475aa8:
  00475aa8  8b7e78                mov       edi,[esi+78h]
  00475aab  e890160d00            call      00547140h
  00475ab0  668146328000          add       word [esi+32h],80h
  00475ab6  2403                  and       al,3
  00475ab8  0fbf4e32              movsx     ecx,word [esi+32h]
  00475abc  51                    push      ecx
  00475abd  884640                mov       [esi+40h],al
  00475ac0  e857ff0c00            call      00545A1Ch
  00475ac5  8d0440                lea       eax,[eax+eax*2]
  00475ac8  8d1480                lea       edx,[eax+eax*4]
  00475acb  8b471c                mov       eax,[edi+1Ch]
  00475ace  8d0c90                lea       ecx,[eax+edx*4]
  00475ad1  0fbf5610              movsx     edx,word [esi+10h]
  00475ad5  c1e208                shl       edx,8
  00475ad8  52                    push      edx
  00475ad9  894e1c                mov       [esi+1Ch],ecx
  00475adc  e83bff0c00            call      00545A1Ch
  00475ae1  8b5720                mov       edx,[edi+20h]
  00475ae4  05004b0000            add       eax,4B00h
  00475ae9  c1e005                shl       eax,5
  00475aec  03c2                  add       eax,edx
  00475aee  894620                mov       [esi+20h],eax
  00475af1  0fbf4632              movsx     eax,word [esi+32h]
  00475af5  50                    push      eax
  00475af6  e81bff0c00            call      00545A16h
  00475afb  8b5724                mov       edx,[edi+24h]
  00475afe  66837e1040            cmp       word [esi+10h],40h
  00475b03  8d0440                lea       eax,[eax+eax*2]
  00475b06  8d0c80                lea       ecx,[eax+eax*4]
  00475b09  8d048a                lea       eax,[edx+ecx*4]
  00475b0c  894624                mov       [esi+24h],eax
  00475b0f  7c74                  jl        short 00475B85h
  00475b11  6a27                  push      27h
  00475b13  6820de5e00            push      5EDE20h
  00475b18  56                    push      esi
  00475b19  c70600004080          mov       dword [esi],80400000h
  00475b1f  e8bcf70b00            call      005352E0h
  00475b24  83c40c                add       esp,0Ch
  00475b27  8d4c2414              lea       ecx,[esp+14h]
  00475b2b  8d542408              lea       edx,[esp+8]
  00475b2f  8d44240c              lea       eax,[esp+0Ch]
  00475b33  51                    push      ecx
  00475b34  52                    push      edx
  00475b35  50                    push      eax
  00475b36  6820de5e00            push      5EDE20h
  00475b3b  c60528de5e0020        mov       byte [5EDE28h],20h
  00475b42  e8392e0c00            call      00538980h
  00475b47  8b4c2424              mov       ecx,[esp+24h]
  00475b4b  8b542418              mov       edx,[esp+18h]
  00475b4f  8b44241c              mov       eax,[esp+1Ch]
  00475b53  83c410                add       esp,10h
  00475b56  51                    push      ecx
  00475b57  52                    push      edx
  00475b58  50                    push      eax
  00475b59  e8822e0c00            call      005389E0h
  00475b5e  668b4e06              mov       cx,[esi+6]
  00475b62  83c40c                add       esp,0Ch
  00475b65  66890d26de5e00        mov       [5EDE26h],cx
  00475b6c  8b5678                mov       edx,[esi+78h]
  00475b6f  6a00                  push      0
  00475b71  52                    push      edx
  00475b72  6820de5e00            push      5EDE20h
  00475b77  e8045a0c00            call      0053B580h
  00475b7c  83c40c                add       esp,0Ch
  00475b7f  c70600000000          mov       dword [esi],0

LAB_00475b85:
  00475b85  66ff4610              inc       word [esi+10h]
  00475b89  5f                    pop       edi
  00475b8a  5e                    pop       esi
  00475b8b  83c408                add       esp,8
  00475b8e  c3                    ret
  00475b8f  90                    nop

2 cible(s) de saut interne : 0x00475aa8, 0x00475b85


