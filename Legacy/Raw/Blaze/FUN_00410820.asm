; 0x00410820 — la scene cinema muette de la foret (286 instructions).
; DECOMPILEE dans Blaze.c (FUN_00410820) ; le releve sert a confirmer la base du bloc prive.
; Copie : 0x00413c90, identique aux onze adresses du bloc pres (0x00571420 -> 0x00571450).

Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00410820  section .text  offset fichier 0xfc20  1200 octets

  00410820  53                    push      ebx
  00410821  55                    push      ebp
  00410822  56                    push      esi
  00410823  57                    push      edi
  00410824  8b7c2414              mov       edi,[esp+14h]
  00410828  bb04000000            mov       ebx,4
  0041082d  b8f8935e00            mov       eax,5E93F8h
  00410832  8bcb                  mov       ecx,ebx
  00410834  66c787980000003800    mov       word [edi+98h],38h
  0041083d  b2ff                  mov       dl,0FFh
  0041083f  befbffffff            mov       esi,0FFFFFFFBh

LAB_00410844:
  00410844  c640d200              mov       byte [eax-2Eh],0
  00410848  8810                  mov       [eax],dl
  0041084a  885001                mov       [eax+1],dl
  0041084d  885002                mov       [eax+2],dl
  00410850  662170fe              and       [eax-2],si
  00410854  66c7405e0000          mov       word [eax+5Eh],0
  0041085a  059c000000            add       eax,9Ch
  0041085f  49                    dec       ecx
  00410860  75e2                  jne       short 00410844h
  00410862  66837f1000            cmp       word [edi+10h],0
  00410867  7517                  jne       short 00410880h
  00410869  e8a2340100            call      00423D10h
  0041086e  66c747100100          mov       word [edi+10h],1
  00410874  660fb60548145700      movzx     ax,byte [571448h]
  0041087c  66894712              mov       [edi+12h],ax

LAB_00410880:
  00410880  66837f1001            cmp       word [edi+10h],1
  00410885  7515                  jne       short 0041089Ch
  00410887  e854a8ffff            call      0040B0E0h
  0041088c  8bf0                  mov       esi,eax
  0041088e  6683fe01              cmp       si,1
  00410892  750c                  jne       short 004108A0h
  00410894  66c747100200          mov       word [edi+10h],2
  0041089a  eb04                  jmp       short 004108A0h

LAB_0041089c:
  0041089c  8b742414              mov       esi,[esp+14h]

LAB_004108a0:
  004108a0  bd02000000            mov       ebp,2
  004108a5  66396f10              cmp       [edi+10h],bp
  004108a9  0f850d010000          jne       near 004109BCh
  004108af  0fbf4f12              movsx     ecx,word [edi+12h]
  004108b3  8d0489                lea       eax,[ecx+ecx*4]
  004108b6  c1e003                shl       eax,3
  004108b9  2bc1                  sub       eax,ecx
  004108bb  c1e002                shl       eax,2
  004108be  8b88dc935e00          mov       ecx,[eax+5E93DCh]
  004108c4  894f1c                mov       [edi+1Ch],ecx
  004108c7  8b90e0935e00          mov       edx,[eax+5E93E0h]
  004108cd  895720                mov       [edi+20h],edx
  004108d0  8b88e4935e00          mov       ecx,[eax+5E93E4h]
  004108d6  894f24                mov       [edi+24h],ecx
  004108d9  668b90f0935e00        mov       dx,[eax+5E93F0h]
  004108e0  66895730              mov       [edi+30h],dx
  004108e4  668b88f2935e00        mov       cx,[eax+5E93F2h]
  004108eb  66894f32              mov       [edi+32h],cx
  004108ef  668b90f4935e00        mov       dx,[eax+5E93F4h]
  004108f6  8b471c                mov       eax,[edi+1Ch]
  004108f9  66895734              mov       [edi+34h],dx
  004108fd  66c747100300          mov       word [edi+10h],3
  00410903  66c747160000          mov       word [edi+16h],0
  00410909  a338145700            mov       [571438h],eax
  0041090e  8b4f20                mov       ecx,[edi+20h]
  00410911  890d3c145700          mov       [57143Ch],ecx
  00410917  8b5724                mov       edx,[edi+24h]
  0041091a  891540145700          mov       [571440h],edx
  00410920  e81b681300            call      00547140h
  00410925  99                    cdq
  00410926  b990010000            mov       ecx,190h
  0041092b  f7f9                  idiv      ecx
  0041092d  66035730              add       dx,[edi+30h]
  00410931  81c2c8000000          add       edx,0C8h
  00410937  66891520145700        mov       [571420h],dx
  0041093e  e8fd671300            call      00547140h
  00410943  99                    cdq
  00410944  b9c8000000            mov       ecx,0C8h
  00410949  f7f9                  idiv      ecx
  0041094b  a194ca5e00            mov       eax,[5ECA94h]
  00410950  66035732              add       dx,[edi+32h]
  00410954  81c200020000          add       edx,200h
  0041095a  66891522145700        mov       [571422h],dx
  00410961  668b5734              mov       dx,[edi+34h]
  00410965  66891524145700        mov       [571424h],dx
  0041096c  8b08                  mov       ecx,[eax]
  0041096e  890d28145700          mov       [571428h],ecx
  00410974  8b5004                mov       edx,[eax+4]
  00410977  89152c145700          mov       [57142Ch],edx
  0041097d  8b4008                mov       eax,[eax+8]
  00410980  a330145700            mov       [571430h],eax
  00410985  e8b6671300            call      00547140h
  0041098a  99                    cdq
  0041098b  b9c8000000            mov       ecx,0C8h
  00410990  c70594ca5e0028145700  mov       dword [5ECA94h],571428h
  0041099a  f7f9                  idiv      ecx
  0041099c  c705e09a5e0020145700  mov       dword [5E9AE0h],571420h
  004109a6  c705bcde5e0038145700  mov       dword [5EDEBCh],571438h
  004109b0  81c2bc020000          add       edx,2BCh
  004109b6  891530145700          mov       [571430h],edx

LAB_004109bc:
  004109bc  66837f1003            cmp       word [edi+10h],3
  004109c1  7529                  jne       short 004109ECh
  004109c3  0fbf9798000000        movsx     edx,word [edi+98h]
  004109ca  6aff                  push      0FFFFFFFFh
  004109cc  81c2c0000000          add       edx,0C0h
  004109d2  6a00                  push      0
  004109d4  52                    push      edx
  004109d5  8d471c                lea       eax,[edi+1Ch]
  004109d8  6a00                  push      0
  004109da  50                    push      eax
  004109db  57                    push      edi
  004109dc  66895f10              mov       [edi+10h],bx
  004109e0  66896f14              mov       [edi+14h],bp
  004109e4  e857a20600            call      0047AC40h
  004109e9  83c418                add       esp,18h

LAB_004109ec:
  004109ec  66ff4716              inc       word [edi+16h]
  004109f0  668b4716              mov       ax,[edi+16h]
  004109f4  663d8200              cmp       ax,82h
  004109f8  7c6a                  jl        short 00410A64h
  004109fa  663d9a00              cmp       ax,9Ah
  004109fe  7f64                  jg        short 00410A64h
  00410a00  8a0d28e05d00          mov       cl,[5DE028h]
  00410a06  8a1529e05d00          mov       dl,[5DE029h]
  00410a0c  b00a                  mov       al,0Ah
  00410a0e  c6052be05d0001        mov       byte [5DE02Bh],1
  00410a15  02c8                  add       cl,al
  00410a17  02d0                  add       dl,al
  00410a19  880d28e05d00          mov       [5DE028h],cl
  00410a1f  8a0d2ae05d00          mov       cl,[5DE02Ah]
  00410a25  02c8                  add       cl,al
  00410a27  a026e05d00            mov       al,[5DE026h]
  00410a2c  881529e05d00          mov       [5DE029h],dl
  00410a32  8a1524e05d00          mov       dl,[5DE024h]
  00410a38  880d2ae05d00          mov       [5DE02Ah],cl
  00410a3e  8a0d25e05d00          mov       cl,[5DE025h]
  00410a44  80c203                add       dl,3
  00410a47  80c103                add       cl,3
  00410a4a  0403                  add       al,3
  00410a4c  c60527e05d0001        mov       byte [5DE027h],1
  00410a53  881524e05d00          mov       [5DE024h],dl
  00410a59  880d25e05d00          mov       [5DE025h],cl
  00410a5f  a226e05d00            mov       [5DE026h],al

LAB_00410a64:
  00410a64  66817f169b00          cmp       word [edi+16h],9Bh
  00410a6a  0f8547010000          jne       near 00410BB7h
  00410a70  8b2db4876000          mov       ebp,[6087B4h]
  00410a76  8b442414              mov       eax,[esp+14h]
  00410a7a  bac0935e00            mov       edx,5E93C0h

LAB_00410a7f:
  00410a7f  8b0a                  mov       ecx,[edx]
  00410a81  f7c100000080          test      ecx,80000000h
  00410a87  0f841d010000          je        near 00410BAAh
  00410a8d  f7c100000050          test      ecx,50000000h
  00410a93  0f8511010000          jne       near 00410BAAh
  00410a99  33c9                  xor       ecx,ecx
  00410a9b  8a4a08                mov       cl,[edx+8]
  00410a9e  83c178                add       ecx,78h
  00410aa1  c1e10d                shl       ecx,0Dh
  00410aa4  034d00                add       ecx,[ebp]
  00410aa7  8a8950010000          mov       cl,[ecx+150h]
  00410aad  f6c108                test      cl,8
  00410ab0  747b                  je        short 00410B2Dh
  00410ab2  80e107                and       cl,7
  00410ab5  750a                  jne       short 00410AC1h
  00410ab7  be06000000            mov       esi,6
  00410abc  b804000000            mov       eax,4

LAB_00410ac1:
  00410ac1  80f901                cmp       cl,1
  00410ac4  750a                  jne       short 00410AD0h
  00410ac6  be06000000            mov       esi,6
  00410acb  b809000000            mov       eax,9

LAB_00410ad0:
  00410ad0  80f902                cmp       cl,2
  00410ad3  7507                  jne       short 00410ADCh
  00410ad5  be03000000            mov       esi,3
  00410ada  33c0                  xor       eax,eax

LAB_00410adc:
  00410adc  80f903                cmp       cl,3
  00410adf  750a                  jne       short 00410AEBh
  00410ae1  be06000000            mov       esi,6
  00410ae6  b812000000            mov       eax,12h

LAB_00410aeb:
  00410aeb  80f904                cmp       cl,4
  00410aee  750a                  jne       short 00410AFAh
  00410af0  be05000000            mov       esi,5
  00410af5  b808000000            mov       eax,8

LAB_00410afa:
  00410afa  80f905                cmp       cl,5
  00410afd  750a                  jne       short 00410B09h
  00410aff  be07000000            mov       esi,7
  00410b04  b805000000            mov       eax,5

LAB_00410b09:
  00410b09  80f906                cmp       cl,6
  00410b0c  750a                  jne       short 00410B18h
  00410b0e  be06000000            mov       esi,6
  00410b13  b804000000            mov       eax,4

LAB_00410b18:
  00410b18  80f907                cmp       cl,7
  00410b1b  0f8581000000          jne       near 00410BA2h
  00410b21  be06000000            mov       esi,6
  00410b26  b809000000            mov       eax,9
  00410b2b  eb75                  jmp       short 00410BA2h

LAB_00410b2d:
  00410b2d  80e107                and       cl,7
  00410b30  750a                  jne       short 00410B3Ch
  00410b32  be05000000            mov       esi,5
  00410b37  b801000000            mov       eax,1

LAB_00410b3c:
  00410b3c  80f901                cmp       cl,1
  00410b3f  750a                  jne       short 00410B4Bh
  00410b41  be06000000            mov       esi,6
  00410b46  b812000000            mov       eax,12h

LAB_00410b4b:
  00410b4b  80f902                cmp       cl,2
  00410b4e  750a                  jne       short 00410B5Ah
  00410b50  be04000000            mov       esi,4
  00410b55  b805000000            mov       eax,5

LAB_00410b5a:
  00410b5a  80f903                cmp       cl,3
  00410b5d  7507                  jne       short 00410B66h
  00410b5f  be03000000            mov       esi,3
  00410b64  8bc6                  mov       eax,esi

LAB_00410b66:
  00410b66  80f904                cmp       cl,4
  00410b69  750a                  jne       short 00410B75h
  00410b6b  be05000000            mov       esi,5
  00410b70  b808000000            mov       eax,8

LAB_00410b75:
  00410b75  80f905                cmp       cl,5
  00410b78  750a                  jne       short 00410B84h
  00410b7a  be06000000            mov       esi,6
  00410b7f  b809000000            mov       eax,9

LAB_00410b84:
  00410b84  80f906                cmp       cl,6
  00410b87  750a                  jne       short 00410B93h
  00410b89  be06000000            mov       esi,6
  00410b8e  b80b000000            mov       eax,0Bh

LAB_00410b93:
  00410b93  80f907                cmp       cl,7
  00410b96  750a                  jne       short 00410BA2h
  00410b98  be06000000            mov       esi,6
  00410b9d  b812000000            mov       eax,12h

LAB_00410ba2:
  00410ba2  66897218              mov       [edx+18h],si
  00410ba6  6689421a              mov       [edx+1Ah],ax

LAB_00410baa:
  00410baa  81c29c000000          add       edx,9Ch
  00410bb0  4b                    dec       ebx
  00410bb1  0f85c8feffff          jne       near 00410A7Fh

LAB_00410bb7:
  00410bb7  668b4716              mov       ax,[edi+16h]
  00410bbb  663d9b00              cmp       ax,9Bh
  00410bbf  7c6c                  jl        short 00410C2Dh
  00410bc1  663db300              cmp       ax,0B3h
  00410bc5  7f66                  jg        short 00410C2Dh
  00410bc7  8a1d28e05d00          mov       bl,[5DE028h]
  00410bcd  8a1529e05d00          mov       dl,[5DE029h]
  00410bd3  8a0d2ae05d00          mov       cl,[5DE02Ah]
  00410bd9  b0f6                  mov       al,0F6h
  00410bdb  02d8                  add       bl,al
  00410bdd  02d0                  add       dl,al
  00410bdf  02c8                  add       cl,al
  00410be1  881d28e05d00          mov       [5DE028h],bl
  00410be7  8a1d24e05d00          mov       bl,[5DE024h]
  00410bed  881529e05d00          mov       [5DE029h],dl
  00410bf3  8a1525e05d00          mov       dl,[5DE025h]
  00410bf9  880d2ae05d00          mov       [5DE02Ah],cl
  00410bff  8a0d26e05d00          mov       cl,[5DE026h]
  00410c05  b0fd                  mov       al,0FDh
  00410c07  02d8                  add       bl,al
  00410c09  02d0                  add       dl,al
  00410c0b  02c8                  add       cl,al
  00410c0d  c6052be05d0001        mov       byte [5DE02Bh],1
  00410c14  c60527e05d0001        mov       byte [5DE027h],1
  00410c1b  881d24e05d00          mov       [5DE024h],bl
  00410c21  881525e05d00          mov       [5DE025h],dl
  00410c27  880d26e05d00          mov       [5DE026h],cl

LAB_00410c2d:
  00410c2d  66817f16c800          cmp       word [edi+16h],0C8h
  00410c33  7538                  jne       short 00410C6Dh
  00410c35  c6052be05d0001        mov       byte [5DE02Bh],1
  00410c3c  c60528e05d0000        mov       byte [5DE028h],0
  00410c43  c60529e05d0000        mov       byte [5DE029h],0
  00410c4a  c6052ae05d0000        mov       byte [5DE02Ah],0
  00410c51  c60527e05d0001        mov       byte [5DE027h],1
  00410c58  c60524e05d0000        mov       byte [5DE024h],0
  00410c5f  c60525e05d0000        mov       byte [5DE025h],0
  00410c66  c60526e05d0000        mov       byte [5DE026h],0

LAB_00410c6d:
  00410c6d  66817f16ca00          cmp       word [edi+16h],0CAh
  00410c73  7552                  jne       short 00410CC7h
  00410c75  e8362b0f00            call      005037B0h
  00410c7a  0fbf4712              movsx     eax,word [edi+12h]
  00410c7e  6a01                  push      1
  00410c80  8d0c80                lea       ecx,[eax+eax*4]
  00410c83  c1e103                shl       ecx,3
  00410c86  2bc8                  sub       ecx,eax
  00410c88  a1cc975e00            mov       eax,[5E97CCh]
  00410c8d  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  00410c94  52                    push      edx
  00410c95  50                    push      eax
  00410c96  e835311000            call      00513DD0h
  00410c9b  83c40c                add       esp,0Ch
  00410c9e  c70594ca5e0078e05d00  mov       dword [5ECA94h],5DE078h
  00410ca8  c705e09a5e0050465600  mov       dword [5E9AE0h],564650h
  00410cb2  c705bcde5e0068e05d00  mov       dword [5EDEBCh],5DE068h
  00410cbc  e8ef2a0f00            call      005037B0h
  00410cc1  8127ffffff7f          and       dword [edi],7FFFFFFFh

LAB_00410cc7:
  00410cc7  5f                    pop       edi
  00410cc8  5e                    pop       esi
  00410cc9  5d                    pop       ebp
  00410cca  5b                    pop       ebx
  00410ccb  c3                    ret
  00410ccc  90                    nop
  00410ccd  90                    nop
  00410cce  90                    nop
  00410ccf  90                    nop

29 cible(s) de saut interne : 0x00410844, 0x00410880, 0x0041089c, 0x004108a0, 0x004109bc, 0x004109ec, 0x00410a64, 0x00410a7f, 0x00410ac1, 0x00410ad0, 0x00410adc, 0x00410aeb, 0x00410afa, 0x00410b09, 0x00410b18, 0x00410b2d, 0x00410b3c, 0x00410b4b, 0x00410b5a, 0x00410b66, 0x00410b75, 0x00410b84, 0x00410b93, 0x00410ba2, 0x00410baa, 0x00410bb7, 0x00410c2d, 0x00410c6d, 0x00410cc7
