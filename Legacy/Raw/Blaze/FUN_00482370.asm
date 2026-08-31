Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00482370  section .text  offset fichier 0x81770  381 octets

  00482370  53                    push      ebx
  00482371  56                    push      esi
  00482372  8b74240c              mov       esi,[esp+0Ch]
  00482376  57                    push      edi
  00482377  66837e1000            cmp       word [esi+10h],0
  0048237c  8b5e74                mov       ebx,[esi+74h]
  0048237f  7573                  jne       short 004823F4h
  00482381  0fbf7e32              movsx     edi,word [esi+32h]
  00482385  57                    push      edi
  00482386  e891360c00            call      00545A1Ch
  0048238b  c1e005                shl       eax,5
  0048238e  57                    push      edi
  0048238f  894650                mov       [esi+50h],eax
  00482392  c7465400c0feff        mov       dword [esi+54h],0FFFEC000h
  00482399  e878360c00            call      00545A16h
  0048239e  668146320004          add       word [esi+32h],400h
  004823a4  66c7869a000000ff7f    mov       word [esi+9Ah],7FFFh
  004823ad  c1e005                shl       eax,5
  004823b0  894658                mov       [esi+58h],eax
  004823b3  668b4632              mov       ax,[esi+32h]
  004823b7  0fbff8                movsx     edi,ax
  004823ba  81ef00040000          sub       edi,400h
  004823c0  66c7463400fd          mov       word [esi+34h],0FD00h
  004823c6  57                    push      edi
  004823c7  e850360c00            call      00545A1Ch
  004823cc  8b4b1c                mov       ecx,[ebx+1Ch]
  004823cf  57                    push      edi
  004823d0  c1e008                shl       eax,8
  004823d3  2bc8                  sub       ecx,eax
  004823d5  894e44                mov       [esi+44h],ecx
  004823d8  8b5360                mov       edx,[ebx+60h]
  004823db  81c200003200          add       edx,320000h
  004823e1  895648                mov       [esi+48h],edx
  004823e4  e82d360c00            call      00545A16h
  004823e9  8b4b24                mov       ecx,[ebx+24h]
  004823ec  c1e008                shl       eax,8
  004823ef  2bc8                  sub       ecx,eax
  004823f1  894e4c                mov       [esi+4Ch],ecx

LAB_004823f4:
  004823f4  668b4610              mov       ax,[esi+10h]
  004823f8  a807                  test      al,7
  004823fa  0f85b0000000          jne       near 004824B0h
  00482400  663d3c00              cmp       ax,3Ch
  00482404  0f8da6000000          jge       near 004824B0h
  0048240a  e8314d0c00            call      00547140h
  0048240f  8b4e44                mov       ecx,[esi+44h]
  00482412  8b5648                mov       edx,[esi+48h]
  00482415  25ff010000            and       eax,1FFh
  0048241a  895620                mov       [esi+20h],edx
  0048241d  2d00010000            sub       eax,100h
  00482422  c1e00c                shl       eax,0Ch
  00482425  03c1                  add       eax,ecx
  00482427  89461c                mov       [esi+1Ch],eax
  0048242a  e8114d0c00            call      00547140h
  0048242f  8b7e4c                mov       edi,[esi+4Ch]
  00482432  25ff010000            and       eax,1FFh
  00482437  2d00010000            sub       eax,100h
  0048243c  8bcb                  mov       ecx,ebx
  0048243e  c1e00c                shl       eax,0Ch
  00482441  03c7                  add       eax,edi
  00482443  2bce                  sub       ecx,esi
  00482445  894624                mov       [esi+24h],eax
  00482448  8d467c                lea       eax,[esi+7Ch]
  0048244b  ba06000000            mov       edx,6

LAB_00482450:
  00482450  8b3c01                mov       edi,[ecx+eax]
  00482453  8938                  mov       [eax],edi
  00482455  83c004                add       eax,4
  00482458  4a                    dec       edx
  00482459  75f5                  jne       short 00482450h
  0048245b  c7869000000000000000  mov       dword [esi+90h],0
  00482465  33ff                  xor       edi,edi
  00482467  bb03000000            mov       ebx,3

LAB_0048246c:
  0048246c  56                    push      esi
  0048246d  e8ce140b00            call      00533940h
  00482472  8b8e90000000          mov       ecx,[esi+90h]
  00482478  83c404                add       esp,4
  0048247b  0bf9                  or        edi,ecx
  0048247d  4b                    dec       ebx
  0048247e  75ec                  jne       short 0048246Ch
  00482480  f7c700880000          test      edi,8800h
  00482486  750f                  jne       short 00482497h
  00482488  6aff                  push      0FFFFFFFFh
  0048248a  6a00                  push      0
  0048248c  6a65                  push      65h
  0048248e  56                    push      esi
  0048248f  e83c85ffff            call      0047A9D0h
  00482494  83c410                add       esp,10h

LAB_00482497:
  00482497  6800120000            push      1200h
  0048249c  6800120000            push      1200h
  004824a1  6800040000            push      400h
  004824a6  6a20                  push      20h
  004824a8  e8f3a90b00            call      0053CEA0h
  004824ad  83c410                add       esp,10h

LAB_004824b0:
  004824b0  c70560915e0080004000  mov       dword [5E9160h],400080h
  004824ba  c70564915e0040000200  mov       dword [5E9164h],20040h
  004824c4  66837e1050            cmp       word [esi+10h],50h
  004824c9  7d0b                  jge       short 004824D6h
  004824cb  8b4674                mov       eax,[esi+74h]
  004824ce  f70000000040          test      dword [eax],40000000h
  004824d4  740f                  je        short 004824E5h

LAB_004824d6:
  004824d6  c70600000000          mov       dword [esi],0
  004824dc  66c70566915e000000    mov       word [5E9166h],0

LAB_004824e5:
  004824e5  66ff4610              inc       word [esi+10h]
  004824e9  5f                    pop       edi
  004824ea  5e                    pop       esi
  004824eb  5b                    pop       ebx
  004824ec  c3                    ret

7 cible(s) de saut interne : 0x004823f4, 0x00482450, 0x0048246c, 0x00482497, 0x004824b0, 0x004824d6, 0x004824e5
