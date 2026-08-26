Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004571d0  section .text  offset fichier 0x565d0  103 octets

  004571d0  56                    push      esi
  004571d1  8b742408              mov       esi,[esp+8]
  004571d5  66837e1000            cmp       word [esi+10h],0
  004571da  7530                  jne       short 0045720Ch
  004571dc  66c746100100          mov       word [esi+10h],1
  004571e2  e859ff0e00            call      00547140h
  004571e7  99                    cdq
  004571e8  33c2                  xor       eax,edx
  004571ea  2bc2                  sub       eax,edx
  004571ec  25ff0f0000            and       eax,0FFFh
  004571f1  33c2                  xor       eax,edx
  004571f3  2bc2                  sub       eax,edx
  004571f5  66894612              mov       [esi+12h],ax
  004571f9  e842ff0e00            call      00547140h
  004571fe  99                    cdq
  004571ff  b932000000            mov       ecx,32h
  00457204  f7f9                  idiv      ecx
  00457206  03d1                  add       edx,ecx
  00457208  66895614              mov       [esi+14h],dx

LAB_0045720c:
  0045720c  66837e1001            cmp       word [esi+10h],1
  00457211  7522                  jne       short 00457235h
  00457213  0fbf5612              movsx     edx,word [esi+12h]
  00457217  52                    push      edx
  00457218  e8ffe70e00            call      00545A1Ch
  0045721d  c1f805                sar       eax,5
  00457220  66894634              mov       [esi+34h],ax
  00457224  668b4614              mov       ax,[esi+14h]
  00457228  66034612              add       ax,[esi+12h]
  0045722c  25ff0f0000            and       eax,0FFFh
  00457231  66894612              mov       [esi+12h],ax

LAB_00457235:
  00457235  5e                    pop       esi
  00457236  c3                    ret

2 cible(s) de saut interne : 0x0045720c, 0x00457235
