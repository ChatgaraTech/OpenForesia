Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00430040  section .text  offset fichier 0x2f440  416 octets

  00430040  55                    push      ebp
  00430041  56                    push      esi
  00430042  8b74240c              mov       esi,[esp+0Ch]
  00430046  57                    push      edi
  00430047  bf01000000            mov       edi,1
  0043004c  66837e1000            cmp       word [esi+10h],0
  00430051  0f8512010000          jne       near 00430169h
  00430057  66897e10              mov       [esi+10h],di
  0043005b  c7463cc8095500        mov       dword [esi+3Ch],5509C8h
  00430062  e8d9701100            call      00547140h
  00430067  99                    cdq
  00430068  b958020000            mov       ecx,258h
  0043006d  f7f9                  idiv      ecx
  0043006f  81c200080000          add       edx,800h
  00430075  66895628              mov       [esi+28h],dx
  00430079  e8c2701100            call      00547140h
  0043007e  99                    cdq
  0043007f  b958020000            mov       ecx,258h
  00430084  f7f9                  idiv      ecx
  00430086  81c200080000          add       edx,800h
  0043008c  6689562a              mov       [esi+2Ah],dx
  00430090  e8ab701100            call      00547140h
  00430095  99                    cdq
  00430096  33c2                  xor       eax,edx
  00430098  2bc2                  sub       eax,edx
  0043009a  25ff070000            and       eax,7FFh
  0043009f  33c2                  xor       eax,edx
  004300a1  2bc2                  sub       eax,edx
  004300a3  2d00040000            sub       eax,400h
  004300a8  66014632              add       [esi+32h],ax
  004300ac  0fbf5632              movsx     edx,word [esi+32h]
  004300b0  81c200080000          add       edx,800h
  004300b6  52                    push      edx
  004300b7  e860591100            call      00545A1Ch
  004300bc  8d2c80                lea       ebp,[eax+eax*4]
  004300bf  e87c701100            call      00547140h
  004300c4  99                    cdq
  004300c5  b914000000            mov       ecx,14h
  004300ca  f7f9                  idiv      ecx
  004300cc  8b461c                mov       eax,[esi+1Ch]
  004300cf  83ea0a                sub       edx,0Ah
  004300d2  c1e20c                shl       edx,0Ch
  004300d5  8d14ea                lea       edx,[edx+ebp*8]
  004300d8  03c2                  add       eax,edx
  004300da  89461c                mov       [esi+1Ch],eax
  004300dd  0fbf4632              movsx     eax,word [esi+32h]
  004300e1  0500080000            add       eax,800h
  004300e6  50                    push      eax
  004300e7  e82a591100            call      00545A16h
  004300ec  8d2c80                lea       ebp,[eax+eax*4]
  004300ef  e84c701100            call      00547140h
  004300f4  99                    cdq
  004300f5  b914000000            mov       ecx,14h
  004300fa  f7f9                  idiv      ecx
  004300fc  8b4e24                mov       ecx,[esi+24h]
  004300ff  83ea0a                sub       edx,0Ah
  00430102  c1e20c                shl       edx,0Ch
  00430105  8d14ea                lea       edx,[edx+ebp*8]
  00430108  03ca                  add       ecx,edx
  0043010a  894e24                mov       [esi+24h],ecx
  0043010d  e82e701100            call      00547140h
  00430112  99                    cdq
  00430113  b914000000            mov       ecx,14h
  00430118  c6460a06              mov       byte [esi+0Ah],6
  0043011c  f7f9                  idiv      ecx
  0043011e  8b4e20                mov       ecx,[esi+20h]
  00430121  b80060ffff            mov       eax,0FFFF6000h
  00430126  c6460b00              mov       byte [esi+0Bh],0
  0043012a  c1e20c                shl       edx,0Ch
  0043012d  2bc2                  sub       eax,edx
  0043012f  8b5624                mov       edx,[esi+24h]
  00430132  03c8                  add       ecx,eax
  00430134  895658                mov       [esi+58h],edx
  00430137  894e20                mov       [esi+20h],ecx
  0043013a  8bc1                  mov       eax,ecx
  0043013c  8b4e1c                mov       ecx,[esi+1Ch]
  0043013f  894654                mov       [esi+54h],eax
  00430142  894e50                mov       [esi+50h],ecx
  00430145  e8f66f1100            call      00547140h
  0043014a  99                    cdq
  0043014b  b905000000            mov       ecx,5
  00430150  66c746142800          mov       word [esi+14h],28h
  00430156  f7f9                  idiv      ecx
  00430158  66c746160000          mov       word [esi+16h],0
  0043015e  c6464000              mov       byte [esi+40h],0
  00430162  83c208                add       edx,8
  00430165  66895612              mov       [esi+12h],dx

LAB_00430169:
  00430169  66397e10              cmp       [esi+10h],di
  0043016d  7568                  jne       short 004301D7h
  0043016f  0fbf5632              movsx     edx,word [esi+32h]
  00430173  81c200080000          add       edx,800h
  00430179  52                    push      edx
  0043017a  e89d581100            call      00545A1Ch
  0043017f  0fbf4e14              movsx     ecx,word [esi+14h]
  00430183  0fbf5632              movsx     edx,word [esi+32h]
  00430187  0fafc1                imul      eax,ecx
  0043018a  8b4e50                mov       ecx,[esi+50h]
  0043018d  668b7e14              mov       di,[esi+14h]
  00430191  81c200080000          add       edx,800h
  00430197  03c1                  add       eax,ecx
  00430199  52                    push      edx
  0043019a  89461c                mov       [esi+1Ch],eax
  0043019d  e874581100            call      00545A16h
  004301a2  0fbfcf                movsx     ecx,di
  004301a5  0fafc1                imul      eax,ecx
  004301a8  8b4e58                mov       ecx,[esi+58h]
  004301ab  662b7e12              sub       di,[esi+12h]
  004301af  03c1                  add       eax,ecx
  004301b1  894624                mov       [esi+24h],eax
  004301b4  8b4620                mov       eax,[esi+20h]
  004301b7  0500400100            add       eax,14000h
  004301bc  66ff4616              inc       word [esi+16h]
  004301c0  894620                mov       [esi+20h],eax
  004301c3  8a4616                mov       al,[esi+16h]
  004301c6  3c04                  cmp       al,4
  004301c8  66897e14              mov       [esi+14h],di
  004301cc  884640                mov       [esi+40h],al
  004301cf  7506                  jne       short 004301D7h
  004301d1  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004301d7:
  004301d7  5f                    pop       edi
  004301d8  5e                    pop       esi
  004301d9  5d                    pop       ebp
  004301da  c3                    ret
  004301db  90                    nop
  004301dc  90                    nop
  004301dd  90                    nop
  004301de  90                    nop
  004301df  90                    nop

2 cible(s) de saut interne : 0x00430169, 0x004301d7
