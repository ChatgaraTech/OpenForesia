Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00456310  section .text  offset fichier 0x55710  640 octets

  00456310  55                    push      ebp
  00456311  56                    push      esi
  00456312  8b74240c              mov       esi,[esp+0Ch]
  00456316  57                    push      edi
  00456317  8b3e                  mov       edi,[esi]
  00456319  f7c700000040          test      edi,40000000h
  0045631f  743c                  je        short 0045635Dh
  00456321  0fbf4e14              movsx     ecx,word [esi+14h]
  00456325  8bc1                  mov       eax,ecx
  00456327  99                    cdq
  00456328  83e207                and       edx,7
  0045632b  03c2                  add       eax,edx
  0045632d  c1f803                sar       eax,3
  00456330  0fbfc0                movsx     eax,ax
  00456333  8d14c500000000        lea       edx,[eax*8]
  0045633a  8a80e0965e00          mov       al,[eax+5E96E0h]
  00456340  2bca                  sub       ecx,edx
  00456342  ba01000000            mov       edx,1
  00456347  d3e2                  shl       edx,cl
  00456349  84d0                  test      al,dl
  0045634b  0f842c020000          je        near 0045657Dh
  00456351  81e7ffffff7f          and       edi,7FFFFFFFh
  00456357  893e                  mov       [esi],edi
  00456359  5f                    pop       edi
  0045635a  5e                    pop       esi
  0045635b  5d                    pop       ebp
  0045635c  c3                    ret

LAB_0045635d:
  0045635d  33ff                  xor       edi,edi
  0045635f  66397e10              cmp       [esi+10h],di
  00456363  753b                  jne       short 004563A0h
  00456365  0fbf4e14              movsx     ecx,word [esi+14h]
  00456369  8bc1                  mov       eax,ecx
  0045636b  66c746100100          mov       word [esi+10h],1
  00456371  99                    cdq
  00456372  83e207                and       edx,7
  00456375  66897e12              mov       [esi+12h],di
  00456379  03c2                  add       eax,edx
  0045637b  c1f803                sar       eax,3
  0045637e  0fbfc0                movsx     eax,ax
  00456381  8d14c500000000        lea       edx,[eax*8]
  00456388  2bca                  sub       ecx,edx
  0045638a  b201                  mov       dl,1
  0045638c  d2e2                  shl       dl,cl
  0045638e  8a88e0965e00          mov       cl,[eax+5E96E0h]
  00456394  0aca                  or        cl,dl
  00456396  8888e0965e00          mov       [eax+5E96E0h],cl
  0045639c  66897e14              mov       [esi+14h],di

LAB_004563a0:
  004563a0  66837e1001            cmp       word [esi+10h],1
  004563a5  bd02000000            mov       ebp,2
  004563aa  0f8593000000          jne       near 00456443h
  004563b0  66ff4612              inc       word [esi+12h]
  004563b4  e8870d0f00            call      00547140h
  004563b9  99                    cdq
  004563ba  b91e000000            mov       ecx,1Eh
  004563bf  f7f9                  idiv      ecx
  004563c1  83fa01                cmp       edx,1
  004563c4  7406                  je        short 004563CCh
  004563c6  66394e12              cmp       [esi+12h],cx
  004563ca  7552                  jne       short 0045641Eh

LAB_004563cc:
  004563cc  66896e10              mov       [esi+10h],bp
  004563d0  e86b0d0f00            call      00547140h
  004563d5  99                    cdq
  004563d6  b905000000            mov       ecx,5
  004563db  57                    push      edi
  004563dc  f7f9                  idiv      ecx
  004563de  8b4624                mov       eax,[esi+24h]
  004563e1  57                    push      edi
  004563e2  c1f80c                sar       eax,0Ch
  004563e5  66894616              mov       [esi+16h],ax
  004563e9  6800100000            push      1000h
  004563ee  6800040000            push      400h
  004563f3  66895612              mov       [esi+12h],dx
  004563f7  8b561c                mov       edx,[esi+1Ch]
  004563fa  c1fa0c                sar       edx,0Ch
  004563fd  66895614              mov       [esi+14h],dx
  00456401  8b0dcc975e00          mov       ecx,[5E97CCh]
  00456407  8b91a8000000          mov       edx,[ecx+0A8h]
  0045640d  8b02                  mov       eax,[edx]
  0045640f  660fb64813            movzx     cx,byte [eax+13h]
  00456414  51                    push      ecx
  00456415  56                    push      esi
  00456416  e8651c0100            call      00468080h
  0045641b  83c418                add       esp,18h

LAB_0045641e:
  0045641e  57                    push      edi
  0045641f  57                    push      edi
  00456420  68e7030000            push      3E7h
  00456425  e8160d0f00            call      00547140h
  0045642a  99                    cdq
  0045642b  b905000000            mov       ecx,5
  00456430  f7f9                  idiv      ecx
  00456432  2bd5                  sub       edx,ebp
  00456434  52                    push      edx
  00456435  68e7030000            push      3E7h
  0045643a  56                    push      esi
  0045643b  e8205afcff            call      0041BE60h
  00456440  83c418                add       esp,18h

LAB_00456443:
  00456443  66396e10              cmp       [esi+10h],bp
  00456447  0f85fd000000          jne       near 0045654Ah
  0045644d  66837e123a            cmp       word [esi+12h],3Ah
  00456452  7d38                  jge       short 0045648Ch
  00456454  0fbf5612              movsx     edx,word [esi+12h]
  00456458  6a00                  push      0
  0045645a  bf3c000000            mov       edi,3Ch
  0045645f  6a00                  push      0
  00456461  68e7030000            push      3E7h
  00456466  2bfa                  sub       edi,edx
  00456468  e8d30c0f00            call      00547140h
  0045646d  8bcf                  mov       ecx,edi
  0045646f  c1f902                sar       ecx,2
  00456472  99                    cdq
  00456473  41                    inc       ecx
  00456474  f7f9                  idiv      ecx
  00456476  c1ff03                sar       edi,3
  00456479  2bd7                  sub       edx,edi
  0045647b  52                    push      edx
  0045647c  68e7030000            push      3E7h
  00456481  56                    push      esi
  00456482  e8d959fcff            call      0041BE60h
  00456487  83c418                add       esp,18h
  0045648a  33ff                  xor       edi,edi

LAB_0045648c:
  0045648c  0fbf5612              movsx     edx,word [esi+12h]
  00456490  8b4e20                mov       ecx,[esi+20h]
  00456493  c1e209                shl       edx,9
  00456496  2bca                  sub       ecx,edx
  00456498  894e20                mov       [esi+20h],ecx
  0045649b  e8a00c0f00            call      00547140h
  004564a0  99                    cdq
  004564a1  b903000000            mov       ecx,3
  004564a6  f7f9                  idiv      ecx
  004564a8  03d5                  add       edx,ebp
  004564aa  66015612              add       [esi+12h],dx
  004564ae  0fbf5632              movsx     edx,word [esi+32h]
  004564b2  52                    push      edx
  004564b3  e864f50e00            call      00545A1Ch
  004564b8  8be8                  mov       ebp,eax
  004564ba  e8810c0f00            call      00547140h
  004564bf  99                    cdq
  004564c0  b905000000            mov       ecx,5
  004564c5  f7f9                  idiv      ecx
  004564c7  c1e20b                shl       edx,0Bh
  004564ca  03ea                  add       ebp,edx
  004564cc  8b561c                mov       edx,[esi+1Ch]
  004564cf  d1e5                  shl       ebp,1
  004564d1  03d5                  add       edx,ebp
  004564d3  89561c                mov       [esi+1Ch],edx
  004564d6  0fbf5632              movsx     edx,word [esi+32h]
  004564da  52                    push      edx
  004564db  e836f50e00            call      00545A16h
  004564e0  8be8                  mov       ebp,eax
  004564e2  e8590c0f00            call      00547140h
  004564e7  99                    cdq
  004564e8  b905000000            mov       ecx,5
  004564ed  f7f9                  idiv      ecx
  004564ef  c1e20b                shl       edx,0Bh
  004564f2  03ea                  add       ebp,edx
  004564f4  8b5624                mov       edx,[esi+24h]
  004564f7  d1e5                  shl       ebp,1
  004564f9  03d5                  add       edx,ebp
  004564fb  895624                mov       [esi+24h],edx
  004564fe  e83d0c0f00            call      00547140h
  00456503  99                    cdq
  00456504  b914000000            mov       ecx,14h
  00456509  f7f9                  idiv      ecx
  0045650b  83ea0a                sub       edx,0Ah
  0045650e  66015632              add       [esi+32h],dx
  00456512  e8290c0f00            call      00547140h
  00456517  99                    cdq
  00456518  b914000000            mov       ecx,14h
  0045651d  f7f9                  idiv      ecx
  0045651f  83ea0a                sub       edx,0Ah
  00456522  66015634              add       [esi+34h],dx
  00456526  66817e122c01          cmp       word [esi+12h],12Ch
  0045652c  7c1c                  jl        short 0045654Ah
  0045652e  57                    push      edi
  0045652f  57                    push      edi
  00456530  68e7030000            push      3E7h
  00456535  57                    push      edi
  00456536  68e7030000            push      3E7h
  0045653b  56                    push      esi
  0045653c  66c746100300          mov       word [esi+10h],3
  00456542  e81959fcff            call      0041BE60h
  00456547  83c418                add       esp,18h

LAB_0045654a:
  0045654a  66837e1003            cmp       word [esi+10h],3
  0045654f  752c                  jne       short 0045657Dh
  00456551  6683463240            add       word [esi+32h],40h
  00456556  b9c0ffffff            mov       ecx,0FFFFFFC0h
  0045655b  66014e28              add       [esi+28h],cx
  0045655f  668b4628              mov       ax,[esi+28h]
  00456563  66014e2c              add       [esi+2Ch],cx
  00456567  663bc7                cmp       ax,di
  0045656a  7f11                  jg        short 0045657Dh
  0045656c  8b06                  mov       eax,[esi]
  0045656e  66897e28              mov       [esi+28h],di
  00456572  0d00600050            or        eax,50006000h
  00456577  66897e2c              mov       [esi+2Ch],di
  0045657b  8906                  mov       [esi],eax

LAB_0045657d:
  0045657d  5f                    pop       edi
  0045657e  5e                    pop       esi
  0045657f  5d                    pop       ebp
  00456580  c3                    ret
  00456581  90                    nop
  00456582  90                    nop
  00456583  90                    nop
  00456584  90                    nop
  00456585  90                    nop
  00456586  90                    nop
  00456587  90                    nop
  00456588  90                    nop
  00456589  90                    nop
  0045658a  90                    nop
  0045658b  90                    nop
  0045658c  90                    nop
  0045658d  90                    nop
  0045658e  90                    nop
  0045658f  90                    nop

8 cible(s) de saut interne : 0x0045635d, 0x004563a0, 0x004563cc, 0x0045641e, 0x00456443, 0x0045648c, 0x0045654a, 0x0045657d
