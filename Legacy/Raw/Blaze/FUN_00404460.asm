Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00404460  section .text  offset fichier 0x3860  416 octets

  00404460  83ec0c                sub       esp,0Ch
  00404463  53                    push      ebx
  00404464  56                    push      esi
  00404465  8b742418              mov       esi,[esp+18h]
  00404469  57                    push      edi
  0040446a  33db                  xor       ebx,ebx
  0040446c  8b7e6c                mov       edi,[esi+6Ch]
  0040446f  3bfb                  cmp       edi,ebx
  00404471  0f846a010000          je        near 004045E1h
  00404477  8b471c                mov       eax,[edi+1Ch]
  0040447a  55                    push      ebp
  0040447b  8d6e1c                lea       ebp,[esi+1Ch]
  0040447e  894500                mov       [ebp],eax
  00404481  8b4f20                mov       ecx,[edi+20h]
  00404484  894e20                mov       [esi+20h],ecx
  00404487  8b5724                mov       edx,[edi+24h]
  0040448a  33c0                  xor       eax,eax
  0040448c  895624                mov       [esi+24h],edx
  0040448f  8a4704                mov       al,[edi+4]
  00404492  66c747180200          mov       word [edi+18h],2
  00404498  8b0c8540965e00        mov       ecx,[eax*4+5E9640h]
  0040449f  8b5108                mov       edx,[ecx+8]
  004044a2  8b0f                  mov       ecx,[edi]
  004044a4  83c908                or        ecx,8
  004044a7  89542410              mov       [esp+10h],edx
  004044ab  890f                  mov       [edi],ecx
  004044ad  66395e10              cmp       [esi+10h],bx
  004044b1  757a                  jne       short 0040452Dh
  004044b3  66c746100100          mov       word [esi+10h],1
  004044b9  e8822c1400            call      00547140h
  004044be  99                    cdq
  004044bf  b905000000            mov       ecx,5
  004044c4  f7f9                  idiv      ecx
  004044c6  8d442414              lea       eax,[esp+14h]
  004044ca  8d4c2418              lea       ecx,[esp+18h]
  004044ce  83c206                add       edx,6
  004044d1  66895612              mov       [esi+12h],dx
  004044d5  8d542420              lea       edx,[esp+20h]
  004044d9  52                    push      edx
  004044da  50                    push      eax
  004044db  51                    push      ecx
  004044dc  57                    push      edi
  004044dd  885f0a                mov       [edi+0Ah],bl
  004044e0  66895f1a              mov       [edi+1Ah],bx
  004044e4  e897441300            call      00538980h
  004044e9  8b442430              mov       eax,[esp+30h]
  004044ed  83c410                add       esp,10h
  004044f0  0fbf4820              movsx     ecx,word [eax+20h]
  004044f4  0fbf501e              movsx     edx,word [eax+1Eh]
  004044f8  0fbf401c              movsx     eax,word [eax+1Ch]
  004044fc  03ca                  add       ecx,edx
  004044fe  53                    push      ebx
  004044ff  03c8                  add       ecx,eax
  00404501  b856555555            mov       eax,55555556h
  00404506  f7e9                  imul      ecx
  00404508  8bca                  mov       ecx,edx
  0040450a  53                    push      ebx
  0040450b  c1e91f                shr       ecx,1Fh
  0040450e  68000a0000            push      0A00h
  00404513  6800080000            push      800h
  00404518  03d1                  add       edx,ecx
  0040451a  6a20                  push      20h
  0040451c  56                    push      esi
  0040451d  66895614              mov       [esi+14h],dx
  00404521  66895e16              mov       [esi+16h],bx
  00404525  e896ecffff            call      004031C0h
  0040452a  83c418                add       esp,18h

LAB_0040452d:
  0040452d  66837e1001            cmp       word [esi+10h],1
  00404532  7524                  jne       short 00404558h
  00404534  66ff4e12              dec       word [esi+12h]
  00404538  6aff                  push      0FFFFFFFFh
  0040453a  53                    push      ebx
  0040453b  6a35                  push      35h
  0040453d  53                    push      ebx
  0040453e  55                    push      ebp
  0040453f  56                    push      esi
  00404540  e8fb660700            call      0047AC40h
  00404545  83c418                add       esp,18h
  00404548  66395e12              cmp       [esi+12h],bx
  0040454c  7d0a                  jge       short 00404558h
  0040454e  66c746100200          mov       word [esi+10h],2
  00404554  66895e12              mov       [esi+12h],bx

LAB_00404558:
  00404558  66837e1002            cmp       word [esi+10h],2
  0040455d  7553                  jne       short 004045B2h
  0040455f  66ff4612              inc       word [esi+12h]
  00404563  66837e120a            cmp       word [esi+12h],0Ah
  00404568  7531                  jne       short 0040459Bh
  0040456a  53                    push      ebx
  0040456b  53                    push      ebx
  0040456c  68000a0000            push      0A00h
  00404571  6800040000            push      400h
  00404576  6a25                  push      25h
  00404578  56                    push      esi
  00404579  e842ecffff            call      004031C0h
  0040457e  83c418                add       esp,18h
  00404581  bb0b000000            mov       ebx,0Bh

LAB_00404586:
  00404586  6aff                  push      0FFFFFFFFh
  00404588  6a00                  push      0
  0040458a  6a36                  push      36h
  0040458c  6a00                  push      0
  0040458e  55                    push      ebp
  0040458f  56                    push      esi
  00404590  e8ab660700            call      0047AC40h
  00404595  83c418                add       esp,18h
  00404598  4b                    dec       ebx
  00404599  75eb                  jne       short 00404586h

LAB_0040459b:
  0040459b  66837e120f            cmp       word [esi+12h],0Fh
  004045a0  7c10                  jl        short 004045B2h
  004045a2  8b0e                  mov       ecx,[esi]
  004045a4  81e1ffffff7f          and       ecx,7FFFFFFFh
  004045aa  890e                  mov       [esi],ecx
  004045ac  8b07                  mov       eax,[edi]
  004045ae  24f7                  and       al,0F7h
  004045b0  8907                  mov       [edi],eax

LAB_004045b2:
  004045b2  f6461601              test      byte [esi+16h],1
  004045b6  7424                  je        short 004045DCh
  004045b8  8b542410              mov       edx,[esp+10h]
  004045bc  66ff471a              inc       word [edi+1Ah]
  004045c0  668b4f1a              mov       cx,[edi+1Ah]
  004045c4  668b4216              mov       ax,[edx+16h]
  004045c8  8bd0                  mov       edx,eax
  004045ca  0fbfc9                movsx     ecx,cx
  004045cd  81e2ffff0000          and       edx,0FFFFh
  004045d3  3bca                  cmp       ecx,edx
  004045d5  7c05                  jl        short 004045DCh
  004045d7  48                    dec       eax
  004045d8  6689471a              mov       [edi+1Ah],ax

LAB_004045dc:
  004045dc  66ff4616              inc       word [esi+16h]
  004045e0  5d                    pop       ebp

LAB_004045e1:
  004045e1  5f                    pop       edi
  004045e2  5e                    pop       esi
  004045e3  5b                    pop       ebx
  004045e4  83c40c                add       esp,0Ch
  004045e7  c3                    ret
  004045e8  90                    nop
  004045e9  90                    nop
  004045ea  90                    nop
  004045eb  90                    nop
  004045ec  90                    nop
  004045ed  90                    nop
  004045ee  90                    nop
  004045ef  90                    nop
  004045f0  53                    push      ebx
  004045f1  56                    push      esi
  004045f2  8b74240c              mov       esi,[esp+0Ch]
  004045f6  33db                  xor       ebx,ebx
  004045f8  57                    push      edi
  004045f9  66395e10              cmp       [esi+10h],bx
  004045fd  0f85e6                (bad)
  (octet invalide — la plage demandee deborde probablement du code)
