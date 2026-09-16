Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004380c0  section .text  offset fichier 0x374c0  432 octets

  004380c0  53                    push      ebx
  004380c1  56                    push      esi
  004380c2  8b74240c              mov       esi,[esp+0Ch]
  004380c6  33db                  xor       ebx,ebx
  004380c8  57                    push      edi
  004380c9  66395e10              cmp       [esi+10h],bx
  004380cd  0f85c0000000          jne       near 00438193h
  004380d3  c7463c300f5500        mov       dword [esi+3Ch],550F30h
  004380da  885e40                mov       [esi+40h],bl
  004380dd  e85ef01000            call      00547140h
  004380e2  99                    cdq
  004380e3  b9e8030000            mov       ecx,3E8h
  004380e8  f7f9                  idiv      ecx
  004380ea  8d82ac0d0000          lea       eax,[edx+0DACh]
  004380f0  66894628              mov       [esi+28h],ax
  004380f4  6689462a              mov       [esi+2Ah],ax
  004380f8  e843f01000            call      00547140h
  004380fd  99                    cdq
  004380fe  b903000000            mov       ecx,3
  00438103  f7f9                  idiv      ecx
  00438105  6683fa01              cmp       dx,1
  00438109  7f06                  jg        short 00438111h
  0043810b  c6460a09              mov       byte [esi+0Ah],9
  0043810f  eb04                  jmp       short 00438115h

LAB_00438111:
  00438111  c6460a02              mov       byte [esi+0Ah],2

LAB_00438115:
  00438115  885e0b                mov       [esi+0Bh],bl
  00438118  e823f01000            call      00547140h
  0043811d  99                    cdq
  0043811e  b91e000000            mov       ecx,1Eh
  00438123  8b7e20                mov       edi,[esi+20h]
  00438126  f7f9                  idiv      ecx
  00438128  c1e20c                shl       edx,0Ch
  0043812b  03fa                  add       edi,edx
  0043812d  897e20                mov       [esi+20h],edi
  00438130  e80bf01000            call      00547140h
  00438135  99                    cdq
  00438136  33c2                  xor       eax,edx
  00438138  2bc2                  sub       eax,edx
  0043813a  25ff0f0000            and       eax,0FFFh
  0043813f  33c2                  xor       eax,edx
  00438141  2bc2                  sub       eax,edx
  00438143  66894612              mov       [esi+12h],ax
  00438147  e8f4ef1000            call      00547140h
  0043814c  99                    cdq
  0043814d  b978000000            mov       ecx,78h
  00438152  66c7869a0000002800    mov       word [esi+9Ah],28h
  0043815b  f7f9                  idiv      ecx
  0043815d  895e5c                mov       [esi+5Ch],ebx
  00438160  83c25a                add       edx,5Ah
  00438163  66895614              mov       [esi+14h],dx
  00438167  e8d4ef1000            call      00547140h
  0043816c  99                    cdq
  0043816d  b907000000            mov       ecx,7
  00438172  66c746100100          mov       word [esi+10h],1
  00438178  f7f9                  idiv      ecx
  0043817a  8b4620                mov       eax,[esi+20h]
  0043817d  8b4e24                mov       ecx,[esi+24h]
  00438180  894654                mov       [esi+54h],eax
  00438183  894e58                mov       [esi+58h],ecx
  00438186  66899698000000        mov       [esi+98h],dx
  0043818d  8b561c                mov       edx,[esi+1Ch]
  00438190  895650                mov       [esi+50h],edx

LAB_00438193:
  00438193  66837e1001            cmp       word [esi+10h],1
  00438198  0f85bd000000          jne       near 0043825Bh
  0043819e  0fbf5612              movsx     edx,word [esi+12h]
  004381a2  52                    push      edx
  004381a3  e874d81000            call      00545A1Ch
  004381a8  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  004381af  0fafc1                imul      eax,ecx
  004381b2  0fbf5612              movsx     edx,word [esi+12h]
  004381b6  8b4e50                mov       ecx,[esi+50h]
  004381b9  668bbe9a000000        mov       di,[esi+9Ah]
  004381c0  03c1                  add       eax,ecx
  004381c2  52                    push      edx
  004381c3  89461c                mov       [esi+1Ch],eax
  004381c6  e84bd81000            call      00545A16h
  004381cb  0fbfcf                movsx     ecx,di
  004381ce  0fafc1                imul      eax,ecx
  004381d1  0fbf9698000000        movsx     edx,word [esi+98h]
  004381d8  8b4e58                mov       ecx,[esi+58h]
  004381db  6683461264            add       word [esi+12h],64h
  004381e0  03c1                  add       eax,ecx
  004381e2  8b4e20                mov       ecx,[esi+20h]
  004381e5  c1e20c                shl       edx,0Ch
  004381e8  03ca                  add       ecx,edx
  004381ea  894624                mov       [esi+24h],eax
  004381ed  894e20                mov       [esi+20h],ecx
  004381f0  668b4e14              mov       cx,[esi+14h]
  004381f4  8d4705                lea       eax,[edi+5]
  004381f7  663bc1                cmp       ax,cx
  004381fa  6689869a000000        mov       [esi+9Ah],ax
  00438201  7e07                  jle       short 0043820Ah
  00438203  66898e9a000000        mov       [esi+9Ah],cx

LAB_0043820a:
  0043820a  8b565c                mov       edx,[esi+5Ch]
  0043820d  42                    inc       edx
  0043820e  8bc2                  mov       eax,edx
  00438210  89565c                mov       [esi+5Ch],edx
  00438213  a801                  test      al,1
  00438215  7409                  je        short 00438220h
  00438217  807e4003              cmp       byte [esi+40h],3
  0043821b  7203                  jb        short 00438220h
  0043821d  885e40                mov       [esi+40h],bl

LAB_00438220:
  00438220  83f81e                cmp       eax,1Eh
  00438223  7e36                  jle       short 0043825Bh
  00438225  660fb64638            movzx     ax,byte [esi+38h]
  0043822a  660fb64e39            movzx     cx,byte [esi+39h]
  0043822f  660fb6563a            movzx     dx,byte [esi+3Ah]
  00438234  83e80a                sub       eax,0Ah
  00438237  83e90a                sub       ecx,0Ah
  0043823a  83ea0a                sub       edx,0Ah
  0043823d  663bc3                cmp       ax,bx
  00438240  7d10                  jge       short 00438252h
  00438242  8b3e                  mov       edi,[esi]
  00438244  33c0                  xor       eax,eax
  00438246  33c9                  xor       ecx,ecx
  00438248  33d2                  xor       edx,edx
  0043824a  81e7ffffff7f          and       edi,7FFFFFFFh
  00438250  893e                  mov       [esi],edi

LAB_00438252:
  00438252  884638                mov       [esi+38h],al
  00438255  884e39                mov       [esi+39h],cl
  00438258  88563a                mov       [esi+3Ah],dl

LAB_0043825b:
  0043825b  5f                    pop       edi
  0043825c  5e                    pop       esi
  0043825d  5b                    pop       ebx
  0043825e  c3                    ret
  0043825f  90                    nop
  00438260  53                    push      ebx
  00438261  56                    push      esi
  00438262  8b74240c              mov       esi,[esp+0Ch]
  00438266  33db                  xor       ebx,ebx
  00438268  66395e10              cmp       [esi+10h],bx
  0043826c  0f85a900              (bad)
  (octet invalide - la plage demandee deborde probablement du code)
