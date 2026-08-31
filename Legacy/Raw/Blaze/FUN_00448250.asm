Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00448250  section .text  offset fichier 0x47650  176 octets

  00448250  56                    push      esi
  00448251  8b742408              mov       esi,[esp+8]
  00448255  8b4678                mov       eax,[esi+78h]
  00448258  85c0                  test      eax,eax
  0044825a  7505                  jne       short 00448261h
  0044825c  b858e55d00            mov       eax,5DE558h

LAB_00448261:
  00448261  8b481c                mov       ecx,[eax+1Ch]
  00448264  894e1c                mov       [esi+1Ch],ecx
  00448267  8b5020                mov       edx,[eax+20h]
  0044826a  81c200000500          add       edx,50000h
  00448270  66837e1000            cmp       word [esi+10h],0
  00448275  895620                mov       [esi+20h],edx
  00448278  8b4024                mov       eax,[eax+24h]
  0044827b  894624                mov       [esi+24h],eax
  0044827e  b901000000            mov       ecx,1
  00448283  752c                  jne       short 004482B1h
  00448285  b8f4110000            mov       eax,11F4h
  0044828a  c6460402              mov       byte [esi+4],2
  0044828e  c6460a06              mov       byte [esi+0Ah],6
  00448292  884e0b                mov       [esi+0Bh],cl
  00448295  c6463800              mov       byte [esi+38h],0
  00448299  c6463900              mov       byte [esi+39h],0
  0044829d  c6463a00              mov       byte [esi+3Ah],0
  004482a1  66894628              mov       [esi+28h],ax
  004482a5  6689462a              mov       [esi+2Ah],ax
  004482a9  6689462c              mov       [esi+2Ch],ax
  004482ad  66894e10              mov       [esi+10h],cx

LAB_004482b1:
  004482b1  66394e10              cmp       [esi+10h],cx
  004482b5  751c                  jne       short 004482D3h
  004482b7  6a32                  push      32h
  004482b9  6a32                  push      32h
  004482bb  6a32                  push      32h
  004482bd  56                    push      esi
  004482be  e81dcbfbff            call      00404DE0h
  004482c3  8a4638                mov       al,[esi+38h]
  004482c6  83c410                add       esp,10h
  004482c9  3cb4                  cmp       al,0B4h
  004482cb  7206                  jb        short 004482D3h
  004482cd  66c746100200          mov       word [esi+10h],2

LAB_004482d3:
  004482d3  66837e1002            cmp       word [esi+10h],2
  004482d8  751c                  jne       short 004482F6h
  004482da  6af1                  push      0FFFFFFF1h
  004482dc  6af6                  push      0FFFFFFF6h
  004482de  6af1                  push      0FFFFFFF1h
  004482e0  56                    push      esi
  004482e1  e8facafbff            call      00404DE0h
  004482e6  8a4639                mov       al,[esi+39h]
  004482e9  83c410                add       esp,10h
  004482ec  84c0                  test      al,al
  004482ee  7506                  jne       short 004482F6h
  004482f0  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004482f6:
  004482f6  5e                    pop       esi
  004482f7  c3                    ret
  004482f8  90                    nop
  004482f9  90                    nop
  004482fa  90                    nop
  004482fb  90                    nop
  004482fc  90                    nop
  004482fd  90                    nop
  004482fe  90                    nop
  004482ff  90                    nop

4 cible(s) de saut interne : 0x00448261, 0x004482b1, 0x004482d3, 0x004482f6
