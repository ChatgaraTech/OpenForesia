Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00457140  section .text  offset fichier 0x56540  142 octets

  00457140  56                    push      esi
  00457141  8b742408              mov       esi,[esp+8]
  00457145  66837e1000            cmp       word [esi+10h],0
  0045714a  753b                  jne       short 00457187h
  0045714c  66c746100100          mov       word [esi+10h],1
  00457152  e8e9ff0e00            call      00547140h
  00457157  99                    cdq
  00457158  33c2                  xor       eax,edx
  0045715a  2bc2                  sub       eax,edx
  0045715c  25ff0f0000            and       eax,0FFFh
  00457161  33c2                  xor       eax,edx
  00457163  2bc2                  sub       eax,edx
  00457165  66894612              mov       [esi+12h],ax
  00457169  e8d2ff0e00            call      00547140h
  0045716e  99                    cdq
  0045716f  b932000000            mov       ecx,32h
  00457174  f7f9                  idiv      ecx
  00457176  b01a                  mov       al,1Ah
  00457178  884638                mov       [esi+38h],al
  0045717b  884639                mov       [esi+39h],al
  0045717e  88463a                mov       [esi+3Ah],al
  00457181  03d1                  add       edx,ecx
  00457183  66895614              mov       [esi+14h],dx

LAB_00457187:
  00457187  66837e1001            cmp       word [esi+10h],1
  0045718c  753e                  jne       short 004571CCh
  0045718e  0fbf5612              movsx     edx,word [esi+12h]
  00457192  52                    push      edx
  00457193  e884e80e00            call      00545A1Ch
  00457198  c1f804                sar       eax,4
  0045719b  05d80e0000            add       eax,0ED8h
  004571a0  66894628              mov       [esi+28h],ax
  004571a4  0fbf4612              movsx     eax,word [esi+12h]
  004571a8  50                    push      eax
  004571a9  e86ee80e00            call      00545A1Ch
  004571ae  668b4e14              mov       cx,[esi+14h]
  004571b2  66034e12              add       cx,[esi+12h]
  004571b6  c1f804                sar       eax,4
  004571b9  05d80e0000            add       eax,0ED8h
  004571be  81e1ff0f0000          and       ecx,0FFFh
  004571c4  6689462c              mov       [esi+2Ch],ax
  004571c8  66894e12              mov       [esi+12h],cx

LAB_004571cc:
  004571cc  5e                    pop       esi
  004571cd  c3                    ret

2 cible(s) de saut interne : 0x00457187, 0x004571cc
