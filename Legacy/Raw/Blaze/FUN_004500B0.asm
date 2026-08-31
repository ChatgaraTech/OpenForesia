Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004500b0  section .text  offset fichier 0x4f4b0  288 octets

  004500b0  56                    push      esi
  004500b1  8b742408              mov       esi,[esp+8]
  004500b5  66837e1000            cmp       word [esi+10h],0
  004500ba  0f85a0000000          jne       near 00450160h
  004500c0  8a869a000000          mov       al,[esi+9Ah]
  004500c6  8b4e20                mov       ecx,[esi+20h]
  004500c9  88460a                mov       [esi+0Ah],al
  004500cc  b802000000            mov       eax,2
  004500d1  66894614              mov       [esi+14h],ax
  004500d5  66894612              mov       [esi+12h],ax
  004500d9  b888130000            mov       eax,1388h
  004500de  81c100000500          add       ecx,50000h
  004500e4  66894628              mov       [esi+28h],ax
  004500e8  6689462a              mov       [esi+2Ah],ax
  004500ec  b080                  mov       al,80h
  004500ee  c7463c58155500        mov       dword [esi+3Ch],551558h
  004500f5  c6464000              mov       byte [esi+40h],0
  004500f9  c6460b00              mov       byte [esi+0Bh],0
  004500fd  66c746100100          mov       word [esi+10h],1
  00450103  884638                mov       [esi+38h],al
  00450106  884639                mov       [esi+39h],al
  00450109  88463a                mov       [esi+3Ah],al
  0045010c  894e20                mov       [esi+20h],ecx
  0045010f  e82c700f00            call      00547140h
  00450114  99                    cdq
  00450115  b928000000            mov       ecx,28h
  0045011a  f7f9                  idiv      ecx
  0045011c  8b461c                mov       eax,[esi+1Ch]
  0045011f  83ea14                sub       edx,14h
  00450122  c1e20c                shl       edx,0Ch
  00450125  03c2                  add       eax,edx
  00450127  89461c                mov       [esi+1Ch],eax
  0045012a  e811700f00            call      00547140h
  0045012f  99                    cdq
  00450130  b928000000            mov       ecx,28h
  00450135  f7f9                  idiv      ecx
  00450137  8b4e24                mov       ecx,[esi+24h]
  0045013a  83ea14                sub       edx,14h
  0045013d  c1e20c                shl       edx,0Ch
  00450140  03ca                  add       ecx,edx
  00450142  894e24                mov       [esi+24h],ecx
  00450145  e8f66f0f00            call      00547140h
  0045014a  99                    cdq
  0045014b  b928000000            mov       ecx,28h
  00450150  f7f9                  idiv      ecx
  00450152  8b4620                mov       eax,[esi+20h]
  00450155  83ea14                sub       edx,14h
  00450158  c1e20c                shl       edx,0Ch
  0045015b  03c2                  add       eax,edx
  0045015d  894620                mov       [esi+20h],eax

LAB_00450160:
  00450160  66837e1001            cmp       word [esi+10h],1
  00450165  7543                  jne       short 004501AAh
  00450167  66ff4e12              dec       word [esi+12h]
  0045016b  66837e1200            cmp       word [esi+12h],0
  00450170  7f38                  jg        short 004501AAh
  00450172  6af6                  push      0FFFFFFF6h
  00450174  6af6                  push      0FFFFFFF6h
  00450176  b8f4010000            mov       eax,1F4h
  0045017b  6af6                  push      0FFFFFFF6h
  0045017d  66014628              add       [esi+28h],ax
  00450181  6601462a              add       [esi+2Ah],ax
  00450185  56                    push      esi
  00450186  e8554cfbff            call      00404DE0h
  0045018b  8a4e40                mov       cl,[esi+40h]
  0045018e  668b5614              mov       dx,[esi+14h]
  00450192  83c410                add       esp,10h
  00450195  fec1                  inc       cl
  00450197  8ac1                  mov       al,cl
  00450199  66895612              mov       [esi+12h],dx
  0045019d  3c04                  cmp       al,4
  0045019f  884e40                mov       [esi+40h],cl
  004501a2  7506                  jne       short 004501AAh
  004501a4  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004501aa:
  004501aa  5e                    pop       esi
  004501ab  c3                    ret
  004501ac  90                    nop
  004501ad  90                    nop
  004501ae  90                    nop
  004501af  90                    nop
  004501b0  53                    push      ebx
  004501b1  33db                  xor       ebx,ebx
  004501b3  56                    push      esi
  004501b4  8b74240c              mov       esi,[esp+0Ch]
  004501b8  53                    push      ebx
  004501b9  53                    push      ebx
  004501ba  56                    push      esi
  004501bb  e8807effff            call      00448040h
  004501c0  8b4678                mov       eax,[esi+78h]
  004501c3  83c40c                add       esp,0Ch
  004501c6  3bc3                  cmp       eax,ebx
  004501c8  7505                  jne       short 004501CFh
  004501ca  b858e55d00            mov       eax,5DE558h

LAB_004501cf:
  004501cf  8b                    (bad)
  (octet invalide — la plage demandee deborde probablement du code)
