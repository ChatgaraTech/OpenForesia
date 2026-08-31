Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00449a70  section .text  offset fichier 0x48e70  435 octets

  00449a70  53                    push      ebx
  00449a71  56                    push      esi
  00449a72  8b74240c              mov       esi,[esp+0Ch]
  00449a76  33db                  xor       ebx,ebx
  00449a78  57                    push      edi
  00449a79  8b466c                mov       eax,[esi+6Ch]
  00449a7c  3bc3                  cmp       eax,ebx
  00449a7e  7505                  jne       short 00449A85h
  00449a80  b858e55d00            mov       eax,5DE558h

LAB_00449a85:
  00449a85  8b481c                mov       ecx,[eax+1Ch]
  00449a88  894e50                mov       [esi+50h],ecx
  00449a8b  8b5020                mov       edx,[eax+20h]
  00449a8e  81c200000500          add       edx,50000h
  00449a94  66395e10              cmp       [esi+10h],bx
  00449a98  895654                mov       [esi+54h],edx
  00449a9b  8b4024                mov       eax,[eax+24h]
  00449a9e  894658                mov       [esi+58h],eax
  00449aa1  0f85a9000000          jne       near 00449B50h
  00449aa7  66c746100100          mov       word [esi+10h],1
  00449aad  885e04                mov       [esi+4],bl
  00449ab0  c6460a03              mov       byte [esi+0Ah],3
  00449ab4  c6460b01              mov       byte [esi+0Bh],1
  00449ab8  e883d60f00            call      00547140h
  00449abd  99                    cdq
  00449abe  33c2                  xor       eax,edx
  00449ac0  2bc2                  sub       eax,edx
  00449ac2  25ff0f0000            and       eax,0FFFh
  00449ac7  33c2                  xor       eax,edx
  00449ac9  2bc2                  sub       eax,edx
  00449acb  66894612              mov       [esi+12h],ax
  00449acf  e86cd60f00            call      00547140h
  00449ad4  99                    cdq
  00449ad5  33c2                  xor       eax,edx
  00449ad7  2bc2                  sub       eax,edx
  00449ad9  25ff030000            and       eax,3FFh
  00449ade  33c2                  xor       eax,edx
  00449ae0  2bc2                  sub       eax,edx
  00449ae2  66894614              mov       [esi+14h],ax
  00449ae6  e855d60f00            call      00547140h
  00449aeb  bfc8000000            mov       edi,0C8h
  00449af0  99                    cdq
  00449af1  8bcf                  mov       ecx,edi
  00449af3  f7f9                  idiv      ecx
  00449af5  83c264                add       edx,64h
  00449af8  6689969a000000        mov       [esi+9Ah],dx
  00449aff  e83cd60f00            call      00547140h
  00449b04  99                    cdq
  00449b05  b91e000000            mov       ecx,1Eh
  00449b0a  66897e28              mov       [esi+28h],di
  00449b0e  f7f9                  idiv      ecx
  00449b10  8b4620                mov       eax,[esi+20h]
  00449b13  8b4e24                mov       ecx,[esi+24h]
  00449b16  894654                mov       [esi+54h],eax
  00449b19  894e58                mov       [esi+58h],ecx
  00449b1c  66897e2c              mov       [esi+2Ch],di
  00449b20  83c214                add       edx,14h
  00449b23  89565c                mov       [esi+5Ch],edx
  00449b26  8b561c                mov       edx,[esi+1Ch]
  00449b29  895650                mov       [esi+50h],edx
  00449b2c  e80fd60f00            call      00547140h
  00449b31  99                    cdq
  00449b32  b9e8030000            mov       ecx,3E8h
  00449b37  885e38                mov       [esi+38h],bl
  00449b3a  f7f9                  idiv      ecx
  00449b3c  8a463b                mov       al,[esi+3Bh]
  00449b3f  885e39                mov       [esi+39h],bl
  00449b42  885e3a                mov       [esi+3Ah],bl
  00449b45  03d1                  add       edx,ecx
  00449b47  24fd                  and       al,0FDh
  00449b49  6689562a              mov       [esi+2Ah],dx
  00449b4d  88463b                mov       [esi+3Bh],al

LAB_00449b50:
  00449b50  66837e1001            cmp       word [esi+10h],1
  00449b55  0f85c4000000          jne       near 00449C1Fh
  00449b5b  6a1e                  push      1Eh
  00449b5d  6a1e                  push      1Eh
  00449b5f  6a1e                  push      1Eh
  00449b61  56                    push      esi
  00449b62  e879b2fbff            call      00404DE0h
  00449b67  668b565c              mov       dx,[esi+5Ch]
  00449b6b  83c410                add       esp,10h
  00449b6e  6629969a000000        sub       [esi+9Ah],dx
  00449b75  66399e9a000000        cmp       [esi+9Ah],bx
  00449b7c  7d10                  jge       short 00449B8Eh
  00449b7e  8b06                  mov       eax,[esi]
  00449b80  66899e9a000000        mov       [esi+9Ah],bx
  00449b87  25ffffff7f            and       eax,7FFFFFFFh
  00449b8c  8906                  mov       [esi],eax

LAB_00449b8e:
  00449b8e  0fbf4614              movsx     eax,word [esi+14h]
  00449b92  50                    push      eax
  00449b93  e87ebe0f00            call      00545A16h
  00449b98  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  00449b9f  8bf8                  mov       edi,eax
  00449ba1  0fbf5612              movsx     edx,word [esi+12h]
  00449ba5  0faff9                imul      edi,ecx
  00449ba8  52                    push      edx
  00449ba9  c1ff0c                sar       edi,0Ch
  00449bac  e86bbe0f00            call      00545A1Ch
  00449bb1  0faff8                imul      edi,eax
  00449bb4  0fbf4614              movsx     eax,word [esi+14h]
  00449bb8  8b5650                mov       edx,[esi+50h]
  00449bbb  50                    push      eax
  00449bbc  03fa                  add       edi,edx
  00449bbe  897e1c                mov       [esi+1Ch],edi
  00449bc1  e850be0f00            call      00545A16h
  00449bc6  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  00449bcd  8bf8                  mov       edi,eax
  00449bcf  0fbf5612              movsx     edx,word [esi+12h]
  00449bd3  0faff9                imul      edi,ecx
  00449bd6  52                    push      edx
  00449bd7  c1ff0c                sar       edi,0Ch
  00449bda  e837be0f00            call      00545A16h
  00449bdf  0faff8                imul      edi,eax
  00449be2  0fbf4614              movsx     eax,word [esi+14h]
  00449be6  8b5658                mov       edx,[esi+58h]
  00449be9  50                    push      eax
  00449bea  03fa                  add       edi,edx
  00449bec  897e24                mov       [esi+24h],edi
  00449bef  e828be0f00            call      00545A1Ch
  00449bf4  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  00449bfb  0fafc1                imul      eax,ecx
  00449bfe  8b5e54                mov       ebx,[esi+54h]
  00449c01  668b5612              mov       dx,[esi+12h]
  00449c05  03c3                  add       eax,ebx
  00449c07  6681c2000c            add       dx,0C00h
  00449c0c  894620                mov       [esi+20h],eax
  00449c0f  66b80004              mov       ax,400h
  00449c13  662b4614              sub       ax,[esi+14h]
  00449c17  66895632              mov       [esi+32h],dx
  00449c1b  66894634              mov       [esi+34h],ax

LAB_00449c1f:
  00449c1f  5f                    pop       edi
  00449c20  5e                    pop       esi
  00449c21  5b                    pop       ebx
  00449c22  c3                    ret

4 cible(s) de saut interne : 0x00449a85, 0x00449b50, 0x00449b8e, 0x00449c1f
