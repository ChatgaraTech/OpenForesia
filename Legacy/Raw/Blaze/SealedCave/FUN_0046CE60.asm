Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0046ce60  section .text  offset fichier 0x6c260  176 octets

  0046ce60  56                    push      esi
  0046ce61  8b742408              mov       esi,[esp+8]
  0046ce65  b801000000            mov       eax,1
  0046ce6a  66837e1000            cmp       word [esi+10h],0
  0046ce6f  752b                  jne       short 0046CE9Ch
  0046ce71  8b4e1c                mov       ecx,[esi+1Ch]
  0046ce74  8b5620                mov       edx,[esi+20h]
  0046ce77  894e44                mov       [esi+44h],ecx
  0046ce7a  8b4e24                mov       ecx,[esi+24h]
  0046ce7d  894e4c                mov       [esi+4Ch],ecx
  0046ce80  895648                mov       [esi+48h],edx
  0046ce83  0fbf4e30              movsx     ecx,word [esi+30h]
  0046ce87  668b5632              mov       dx,[esi+32h]
  0046ce8b  66894610              mov       [esi+10h],ax
  0046ce8f  66895612              mov       [esi+12h],dx
  0046ce93  894e5c                mov       [esi+5Ch],ecx
  0046ce96  66c746300000          mov       word [esi+30h],0

LAB_0046ce9c:
  0046ce9c  66394610              cmp       [esi+10h],ax
  0046cea0  755f                  jne       short 0046CF01h
  0046cea2  0fbf5612              movsx     edx,word [esi+12h]
  0046cea6  52                    push      edx
  0046cea7  e8708b0d00            call      00545A1Ch
  0046ceac  0fbf4e14              movsx     ecx,word [esi+14h]
  0046ceb0  0fafc1                imul      eax,ecx
  0046ceb3  0fbf5612              movsx     edx,word [esi+12h]
  0046ceb7  8b4e44                mov       ecx,[esi+44h]
  0046ceba  52                    push      edx
  0046cebb  03c1                  add       eax,ecx
  0046cebd  89461c                mov       [esi+1Ch],eax
  0046cec0  e8518b0d00            call      00545A16h
  0046cec5  0fbf4e16              movsx     ecx,word [esi+16h]
  0046cec9  0fafc1                imul      eax,ecx
  0046cecc  8b4e4c                mov       ecx,[esi+4Ch]
  0046cecf  668b565c              mov       dx,[esi+5Ch]
  0046ced3  66015612              add       [esi+12h],dx
  0046ced7  03c1                  add       eax,ecx
  0046ced9  894624                mov       [esi+24h],eax
  0046cedc  668b4612              mov       ax,[esi+12h]
  0046cee0  663d0010              cmp       ax,1000h
  0046cee4  7e09                  jle       short 0046CEEFh
  0046cee6  0500f0ffff            add       eax,0FFFFF000h
  0046ceeb  66894612              mov       [esi+12h],ax

LAB_0046ceef:
  0046ceef  668b4612              mov       ax,[esi+12h]
  0046cef3  6685c0                test      ax,ax
  0046cef6  7d09                  jge       short 0046CF01h
  0046cef8  0500100000            add       eax,1000h
  0046cefd  66894612              mov       [esi+12h],ax

LAB_0046cf01:
  0046cf01  5e                    pop       esi
  0046cf02  c3                    ret
  0046cf03  90                    nop
  0046cf04  90                    nop
  0046cf05  90                    nop
  0046cf06  90                    nop
  0046cf07  90                    nop
  0046cf08  90                    nop
  0046cf09  90                    nop
  0046cf0a  90                    nop
  0046cf0b  90                    nop
  0046cf0c  90                    nop
  0046cf0d  90                    nop
  0046cf0e  90                    nop
  0046cf0f  90                    nop

3 cible(s) de saut interne : 0x0046ce9c, 0x0046ceef, 0x0046cf01
