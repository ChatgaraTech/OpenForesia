Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00430360  section .text  offset fichier 0x2f760  1520 octets

  00430360  51                    push      ecx
  00430361  53                    push      ebx
  00430362  55                    push      ebp
  00430363  56                    push      esi
  00430364  8b742414              mov       esi,[esp+14h]
  00430368  57                    push      edi
  00430369  66837e1000            cmp       word [esi+10h],0
  0043036e  0f85fd040000          jne       near 00430871h
  00430374  c7463cc8095500        mov       dword [esi+3Ch],5509C8h
  0043037b  e8c06d1100            call      00547140h
  00430380  99                    cdq
  00430381  b958020000            mov       ecx,258h
  00430386  f7f9                  idiv      ecx
  00430388  81c200080000          add       edx,800h
  0043038e  66895628              mov       [esi+28h],dx
  00430392  e8a96d1100            call      00547140h
  00430397  99                    cdq
  00430398  b958020000            mov       ecx,258h
  0043039d  f7f9                  idiv      ecx
  0043039f  81c200080000          add       edx,800h
  004303a5  66837e1202            cmp       word [esi+12h],2
  004303aa  6689562a              mov       [esi+2Ah],dx
  004303ae  7411                  je        short 004303C1h
  004303b0  e88b6d1100            call      00547140h
  004303b5  99                    cdq
  004303b6  b906000000            mov       ecx,6
  004303bb  f7f9                  idiv      ecx
  004303bd  8bca                  mov       ecx,edx
  004303bf  eb13                  jmp       short 004303D4h

LAB_004303c1:
  004303c1  e87a6d1100            call      00547140h
  004303c6  99                    cdq
  004303c7  33c2                  xor       eax,edx
  004303c9  2bc2                  sub       eax,edx
  004303cb  83e007                and       eax,7
  004303ce  33c2                  xor       eax,edx
  004303d0  8bc8                  mov       ecx,eax
  004303d2  2bca                  sub       ecx,edx

LAB_004303d4:
  004303d4  6685c9                test      cx,cx
  004303d7  894c2418              mov       [esp+18h],ecx
  004303db  740a                  je        short 004303E7h
  004303dd  6683f904              cmp       cx,4
  004303e1  0f85cf000000          jne       near 004304B6h

LAB_004303e7:
  004303e7  0fbf5632              movsx     edx,word [esi+32h]
  004303eb  81c2bc020000          add       edx,2BCh
  004303f1  52                    push      edx
  004303f2  e825561100            call      00545A1Ch
  004303f7  8d0480                lea       eax,[eax+eax*4]
  004303fa  8d3c80                lea       edi,[eax+eax*4]
  004303fd  e83e6d1100            call      00547140h
  00430402  99                    cdq
  00430403  b914000000            mov       ecx,14h
  00430408  f7f9                  idiv      ecx
  0043040a  83ea0a                sub       edx,0Ah
  0043040d  c1e20c                shl       edx,0Ch
  00430410  8d3cfa                lea       edi,[edx+edi*8]
  00430413  0fbf5632              movsx     edx,word [esi+32h]
  00430417  81c2bc020000          add       edx,2BCh
  0043041d  52                    push      edx
  0043041e  e8f3551100            call      00545A16h
  00430423  8d0480                lea       eax,[eax+eax*4]
  00430426  8d1c80                lea       ebx,[eax+eax*4]
  00430429  e8126d1100            call      00547140h
  0043042e  99                    cdq
  0043042f  b914000000            mov       ecx,14h
  00430434  f7f9                  idiv      ecx
  00430436  83ea0a                sub       edx,0Ah
  00430439  c1e20c                shl       edx,0Ch
  0043043c  8d2cda                lea       ebp,[edx+ebx*8]
  0043043f  0fbf5632              movsx     edx,word [esi+32h]
  00430443  81eabc020000          sub       edx,2BCh
  00430449  52                    push      edx
  0043044a  e8cd551100            call      00545A1Ch
  0043044f  8d0480                lea       eax,[eax+eax*4]
  00430452  8d1c80                lea       ebx,[eax+eax*4]
  00430455  e8e66c1100            call      00547140h
  0043045a  99                    cdq
  0043045b  b914000000            mov       ecx,14h
  00430460  f7f9                  idiv      ecx
  00430462  0fbf4632              movsx     eax,word [esi+32h]
  00430466  2dbc020000            sub       eax,2BCh
  0043046b  50                    push      eax
  0043046c  83ea0a                sub       edx,0Ah
  0043046f  c1e20c                shl       edx,0Ch
  00430472  8d14da                lea       edx,[edx+ebx*8]
  00430475  89542414              mov       [esp+14h],edx
  00430479  e898551100            call      00545A16h
  0043047e  8d0480                lea       eax,[eax+eax*4]
  00430481  8d1c80                lea       ebx,[eax+eax*4]
  00430484  e8b76c1100            call      00547140h
  00430489  99                    cdq
  0043048a  b914000000            mov       ecx,14h
  0043048f  f7f9                  idiv      ecx
  00430491  83ea0a                sub       edx,0Ah
  00430494  c1e20c                shl       edx,0Ch
  00430497  8d1cda                lea       ebx,[edx+ebx*8]
  0043049a  e8a16c1100            call      00547140h
  0043049f  99                    cdq
  004304a0  33c2                  xor       eax,edx
  004304a2  8b4c2418              mov       ecx,[esp+18h]
  004304a6  2bc2                  sub       eax,edx
  004304a8  83e007                and       eax,7
  004304ab  33c2                  xor       eax,edx
  004304ad  2bc2                  sub       eax,edx
  004304af  ba08000000            mov       edx,8
  004304b4  eb14                  jmp       short 004304CAh

LAB_004304b6:
  004304b6  8b7c2418              mov       edi,[esp+18h]
  004304ba  8b6c2418              mov       ebp,[esp+18h]
  004304be  8b5c2418              mov       ebx,[esp+18h]
  004304c2  8b542418              mov       edx,[esp+18h]
  004304c6  8b442418              mov       eax,[esp+18h]

LAB_004304ca:
  004304ca  6683f901              cmp       cx,1
  004304ce  7415                  je        short 004304E5h
  004304d0  66837e1202            cmp       word [esi+12h],2
  004304d5  0f85d7000000          jne       near 004305B2h
  004304db  6683f906              cmp       cx,6
  004304df  0f8ccd000000          jl        near 004305B2h

LAB_004304e5:
  004304e5  0fbf5632              movsx     edx,word [esi+32h]
  004304e9  81eabc020000          sub       edx,2BCh
  004304ef  52                    push      edx
  004304f0  e827551100            call      00545A1Ch
  004304f5  8d0480                lea       eax,[eax+eax*4]
  004304f8  8d3c80                lea       edi,[eax+eax*4]
  004304fb  e8406c1100            call      00547140h
  00430500  99                    cdq
  00430501  b914000000            mov       ecx,14h
  00430506  f7f9                  idiv      ecx
  00430508  83ea0a                sub       edx,0Ah
  0043050b  c1e20c                shl       edx,0Ch
  0043050e  8d3cfa                lea       edi,[edx+edi*8]
  00430511  0fbf5632              movsx     edx,word [esi+32h]
  00430515  81eabc020000          sub       edx,2BCh
  0043051b  52                    push      edx
  0043051c  e8f5541100            call      00545A16h
  00430521  8d0480                lea       eax,[eax+eax*4]
  00430524  8d1c80                lea       ebx,[eax+eax*4]
  00430527  e8146c1100            call      00547140h
  0043052c  99                    cdq
  0043052d  b914000000            mov       ecx,14h
  00430532  f7f9                  idiv      ecx
  00430534  83ea0a                sub       edx,0Ah
  00430537  c1e20c                shl       edx,0Ch
  0043053a  8d2cda                lea       ebp,[edx+ebx*8]
  0043053d  0fbf5632              movsx     edx,word [esi+32h]
  00430541  81ea14050000          sub       edx,514h
  00430547  52                    push      edx
  00430548  e8cf541100            call      00545A1Ch
  0043054d  8d0480                lea       eax,[eax+eax*4]
  00430550  8d1c80                lea       ebx,[eax+eax*4]
  00430553  e8e86b1100            call      00547140h
  00430558  99                    cdq
  00430559  b914000000            mov       ecx,14h
  0043055e  f7f9                  idiv      ecx
  00430560  0fbf4632              movsx     eax,word [esi+32h]
  00430564  2d14050000            sub       eax,514h
  00430569  50                    push      eax
  0043056a  83ea0a                sub       edx,0Ah
  0043056d  c1e20c                shl       edx,0Ch
  00430570  8d14da                lea       edx,[edx+ebx*8]
  00430573  89542414              mov       [esp+14h],edx
  00430577  e89a541100            call      00545A16h
  0043057c  8d0480                lea       eax,[eax+eax*4]
  0043057f  8d1c80                lea       ebx,[eax+eax*4]
  00430582  e8b96b1100            call      00547140h
  00430587  99                    cdq
  00430588  b914000000            mov       ecx,14h
  0043058d  f7f9                  idiv      ecx
  0043058f  83ea0a                sub       edx,0Ah
  00430592  c1e20c                shl       edx,0Ch
  00430595  8d1cda                lea       ebx,[edx+ebx*8]
  00430598  e8a36b1100            call      00547140h
  0043059d  99                    cdq
  0043059e  33c2                  xor       eax,edx
  004305a0  8b4c2418              mov       ecx,[esp+18h]
  004305a4  2bc2                  sub       eax,edx
  004305a6  83e003                and       eax,3
  004305a9  33c2                  xor       eax,edx
  004305ab  2bc2                  sub       eax,edx
  004305ad  ba04000000            mov       edx,4

LAB_004305b2:
  004305b2  6683f902              cmp       cx,2
  004305b6  740a                  je        short 004305C2h
  004305b8  6683f905              cmp       cx,5
  004305bc  0f85cd000000          jne       near 0043068Fh

LAB_004305c2:
  004305c2  0fbf5632              movsx     edx,word [esi+32h]
  004305c6  81ea14050000          sub       edx,514h
  004305cc  52                    push      edx
  004305cd  e84a541100            call      00545A1Ch
  004305d2  8d0480                lea       eax,[eax+eax*4]
  004305d5  8d3c80                lea       edi,[eax+eax*4]
  004305d8  e8636b1100            call      00547140h
  004305dd  99                    cdq
  004305de  b914000000            mov       ecx,14h
  004305e3  f7f9                  idiv      ecx
  004305e5  83ea0a                sub       edx,0Ah
  004305e8  c1e20c                shl       edx,0Ch
  004305eb  8d3cfa                lea       edi,[edx+edi*8]
  004305ee  0fbf5632              movsx     edx,word [esi+32h]
  004305f2  81ea14050000          sub       edx,514h
  004305f8  52                    push      edx
  004305f9  e818541100            call      00545A16h
  004305fe  8d0480                lea       eax,[eax+eax*4]
  00430601  8d1c80                lea       ebx,[eax+eax*4]
  00430604  e8376b1100            call      00547140h
  00430609  99                    cdq
  0043060a  b914000000            mov       ecx,14h
  0043060f  f7f9                  idiv      ecx
  00430611  83ea0a                sub       edx,0Ah
  00430614  c1e20c                shl       edx,0Ch
  00430617  8d2cda                lea       ebp,[edx+ebx*8]
  0043061a  0fbf5632              movsx     edx,word [esi+32h]
  0043061e  81c214050000          add       edx,514h
  00430624  52                    push      edx
  00430625  e8f2531100            call      00545A1Ch
  0043062a  8d0480                lea       eax,[eax+eax*4]
  0043062d  8d1c80                lea       ebx,[eax+eax*4]
  00430630  e80b6b1100            call      00547140h
  00430635  99                    cdq
  00430636  b914000000            mov       ecx,14h
  0043063b  f7f9                  idiv      ecx
  0043063d  0fbf4632              movsx     eax,word [esi+32h]
  00430641  0514050000            add       eax,514h
  00430646  50                    push      eax
  00430647  83ea0a                sub       edx,0Ah
  0043064a  c1e20c                shl       edx,0Ch
  0043064d  8d14da                lea       edx,[edx+ebx*8]
  00430650  89542414              mov       [esp+14h],edx
  00430654  e8bd531100            call      00545A16h
  00430659  8d0480                lea       eax,[eax+eax*4]
  0043065c  8d1c80                lea       ebx,[eax+eax*4]
  0043065f  e8dc6a1100            call      00547140h
  00430664  99                    cdq
  00430665  b914000000            mov       ecx,14h
  0043066a  f7f9                  idiv      ecx
  0043066c  83ea0a                sub       edx,0Ah
  0043066f  c1e20c                shl       edx,0Ch
  00430672  8d1cda                lea       ebx,[edx+ebx*8]
  00430675  e8c66a1100            call      00547140h
  0043067a  99                    cdq
  0043067b  33c2                  xor       eax,edx
  0043067d  8b4c2418              mov       ecx,[esp+18h]
  00430681  2bc2                  sub       eax,edx
  00430683  83e007                and       eax,7
  00430686  33c2                  xor       eax,edx
  00430688  2bc2                  sub       eax,edx
  0043068a  ba08000000            mov       edx,8

LAB_0043068f:
  0043068f  6683f903              cmp       cx,3
  00430693  0f85c9000000          jne       near 00430762h
  00430699  0fbf5632              movsx     edx,word [esi+32h]
  0043069d  81c214050000          add       edx,514h
  004306a3  52                    push      edx
  004306a4  e873531100            call      00545A1Ch
  004306a9  8d0480                lea       eax,[eax+eax*4]
  004306ac  8d3c80                lea       edi,[eax+eax*4]
  004306af  e88c6a1100            call      00547140h
  004306b4  99                    cdq
  004306b5  b914000000            mov       ecx,14h
  004306ba  f7f9                  idiv      ecx
  004306bc  83ea0a                sub       edx,0Ah
  004306bf  c1e20c                shl       edx,0Ch
  004306c2  8d3cfa                lea       edi,[edx+edi*8]
  004306c5  0fbf5632              movsx     edx,word [esi+32h]
  004306c9  81c214050000          add       edx,514h
  004306cf  52                    push      edx
  004306d0  e841531100            call      00545A16h
  004306d5  8d0480                lea       eax,[eax+eax*4]
  004306d8  8d1c80                lea       ebx,[eax+eax*4]
  004306db  e8606a1100            call      00547140h
  004306e0  99                    cdq
  004306e1  b914000000            mov       ecx,14h
  004306e6  f7f9                  idiv      ecx
  004306e8  83ea0a                sub       edx,0Ah
  004306eb  c1e20c                shl       edx,0Ch
  004306ee  8d2cda                lea       ebp,[edx+ebx*8]
  004306f1  0fbf5632              movsx     edx,word [esi+32h]
  004306f5  81c2bc020000          add       edx,2BCh
  004306fb  52                    push      edx
  004306fc  e81b531100            call      00545A1Ch
  00430701  8d0480                lea       eax,[eax+eax*4]
  00430704  8d1c80                lea       ebx,[eax+eax*4]
  00430707  e8346a1100            call      00547140h
  0043070c  99                    cdq
  0043070d  b914000000            mov       ecx,14h
  00430712  f7f9                  idiv      ecx
  00430714  0fbf4632              movsx     eax,word [esi+32h]
  00430718  05bc020000            add       eax,2BCh
  0043071d  50                    push      eax
  0043071e  83ea0a                sub       edx,0Ah
  00430721  c1e20c                shl       edx,0Ch
  00430724  8d14da                lea       edx,[edx+ebx*8]
  00430727  89542414              mov       [esp+14h],edx
  0043072b  e8e6521100            call      00545A16h
  00430730  8d0480                lea       eax,[eax+eax*4]
  00430733  8d1c80                lea       ebx,[eax+eax*4]
  00430736  e8056a1100            call      00547140h
  0043073b  99                    cdq
  0043073c  b914000000            mov       ecx,14h
  00430741  f7f9                  idiv      ecx
  00430743  83ea0a                sub       edx,0Ah
  00430746  c1e20c                shl       edx,0Ch
  00430749  8d1cda                lea       ebx,[edx+ebx*8]
  0043074c  e8ef691100            call      00547140h
  00430751  99                    cdq
  00430752  33c2                  xor       eax,edx
  00430754  2bc2                  sub       eax,edx
  00430756  83e003                and       eax,3
  00430759  33c2                  xor       eax,edx
  0043075b  2bc2                  sub       eax,edx
  0043075d  ba04000000            mov       edx,4

LAB_00430762:
  00430762  0fbfc0                movsx     eax,ax
  00430765  c1ff0c                sar       edi,0Ch
  00430768  c1fd0c                sar       ebp,0Ch
  0043076b  40                    inc       eax
  0043076c  0fbfca                movsx     ecx,dx
  0043076f  8b542410              mov       edx,[esp+10h]
  00430773  89442418              mov       [esp+18h],eax
  00430777  c1fa0c                sar       edx,0Ch
  0043077a  8bc7                  mov       eax,edi
  0043077c  2bc2                  sub       eax,edx
  0043077e  99                    cdq
  0043077f  f7f9                  idiv      ecx
  00430781  8b561c                mov       edx,[esi+1Ch]
  00430784  c1fb0c                sar       ebx,0Ch
  00430787  0faf442418            imul      eax,[esp+18h]
  0043078c  2bf8                  sub       edi,eax
  0043078e  8bc5                  mov       eax,ebp
  00430790  c1e70c                shl       edi,0Ch
  00430793  03d7                  add       edx,edi
  00430795  2bc3                  sub       eax,ebx
  00430797  89561c                mov       [esi+1Ch],edx
  0043079a  99                    cdq
  0043079b  f7f9                  idiv      ecx
  0043079d  0faf442418            imul      eax,[esp+18h]
  004307a2  2be8                  sub       ebp,eax
  004307a4  8b4624                mov       eax,[esi+24h]
  004307a7  c1e50c                shl       ebp,0Ch
  004307aa  03c5                  add       eax,ebp
  004307ac  894624                mov       [esi+24h],eax
  004307af  e88c691100            call      00547140h
  004307b4  99                    cdq
  004307b5  b914000000            mov       ecx,14h
  004307ba  33ed                  xor       ebp,ebp
  004307bc  f7f9                  idiv      ecx
  004307be  b80060ffff            mov       eax,0FFFF6000h
  004307c3  8b4e1c                mov       ecx,[esi+1Ch]
  004307c6  bb01000000            mov       ebx,1
  004307cb  894e50                mov       [esi+50h],ecx
  004307ce  c6460a06              mov       byte [esi+0Ah],6
  004307d2  c6460b00              mov       byte [esi+0Bh],0
  004307d6  66895e10              mov       [esi+10h],bx
  004307da  c1e20c                shl       edx,0Ch
  004307dd  2bc2                  sub       eax,edx
  004307df  8b5620                mov       edx,[esi+20h]
  004307e2  03d0                  add       edx,eax
  004307e4  8bc2                  mov       eax,edx
  004307e6  895620                mov       [esi+20h],edx
  004307e9  8b5624                mov       edx,[esi+24h]
  004307ec  894654                mov       [esi+54h],eax
  004307ef  668b4612              mov       ax,[esi+12h]
  004307f3  895658                mov       [esi+58h],edx
  004307f6  663bc5                cmp       ax,bp
  004307f9  7507                  jne       short 00430802h
  004307fb  6689ae9a000000        mov       [esi+9Ah],bp

LAB_00430802:
  00430802  663bc3                cmp       ax,bx
  00430805  7514                  jne       short 0043081Bh
  00430807  b900040000            mov       ecx,400h
  0043080c  66899e9a000000        mov       [esi+9Ah],bx
  00430813  66014e28              add       [esi+28h],cx
  00430817  66014e2a              add       [esi+2Ah],cx

LAB_0043081b:
  0043081b  bf02000000            mov       edi,2
  00430820  663bc7                cmp       ax,di
  00430823  7514                  jne       short 00430839h
  00430825  b8e0fcffff            mov       eax,0FFFFFCE0h
  0043082a  6689be9a000000        mov       [esi+9Ah],di
  00430831  66014628              add       [esi+28h],ax
  00430835  6601462a              add       [esi+2Ah],ax

LAB_00430839:
  00430839  668b869a000000        mov       ax,[esi+9Ah]
  00430840  663bc3                cmp       ax,bx
  00430843  7405                  je        short 0043084Ah
  00430845  663bc7                cmp       ax,di
  00430848  7502                  jne       short 0043084Ch

LAB_0043084a:
  0043084a  8bfb                  mov       edi,ebx

LAB_0043084c:
  0043084c  e8ef681100            call      00547140h
  00430851  99                    cdq
  00430852  b905000000            mov       ecx,5
  00430857  66896e14              mov       [esi+14h],bp
  0043085b  f7f9                  idiv      ecx
  0043085d  66896e16              mov       [esi+16h],bp
  00430861  c6464000              mov       byte [esi+40h],0
  00430865  83c208                add       edx,8
  00430868  0fafd7                imul      edx,edi
  0043086b  66895612              mov       [esi+12h],dx
  0043086f  eb05                  jmp       short 00430876h

LAB_00430871:
  00430871  bb01000000            mov       ebx,1

LAB_00430876:
  00430876  66395e10              cmp       [esi+10h],bx
  0043087a  755c                  jne       short 004308D8h
  0043087c  8b2e                  mov       ebp,[esi]
  0043087e  81cd00400000          or        ebp,4000h
  00430884  66ff4614              inc       word [esi+14h]
  00430888  668b4e14              mov       cx,[esi+14h]
  0043088c  892e                  mov       [esi],ebp
  0043088e  6683f903              cmp       cx,3
  00430892  8bc5                  mov       eax,ebp
  00430894  7e16                  jle       short 004308ACh
  00430896  668b8e9a000000        mov       cx,[esi+9Ah]
  0043089d  6685c9                test      cx,cx
  004308a0  7405                  je        short 004308A7h
  004308a2  663bcb                cmp       cx,bx
  004308a5  7505                  jne       short 004308ACh

LAB_004308a7:
  004308a7  80e4bf                and       ah,0BFh
  004308aa  8906                  mov       [esi],eax

LAB_004308ac:
  004308ac  e88f681100            call      00547140h
  004308b1  99                    cdq
  004308b2  b90a000000            mov       ecx,0Ah
  004308b7  f7f9                  idiv      ecx
  004308b9  66837e1408            cmp       word [esi+14h],8
  004308be  7f05                  jg        short 004308C5h
  004308c0  6685d2                test      dx,dx
  004308c3  7513                  jne       short 004308D8h

LAB_004308c5:
  004308c5  8b06                  mov       eax,[esi]
  004308c7  66c746100200          mov       word [esi+10h],2
  004308cd  80e4bf                and       ah,0BFh
  004308d0  66c746140000          mov       word [esi+14h],0
  004308d6  8906                  mov       [esi],eax

LAB_004308d8:
  004308d8  66837e1002            cmp       word [esi+10h],2
  004308dd  755d                  jne       short 0043093Ch
  004308df  0fbf5612              movsx     edx,word [esi+12h]
  004308e3  8b6e20                mov       ebp,[esi+20h]
  004308e6  c1e20c                shl       edx,0Ch
  004308e9  03ea                  add       ebp,edx
  004308eb  896e20                mov       [esi+20h],ebp
  004308ee  e84d681100            call      00547140h
  004308f3  99                    cdq
  004308f4  b914000000            mov       ecx,14h
  004308f9  f7f9                  idiv      ecx
  004308fb  8b4e1c                mov       ecx,[esi+1Ch]
  004308fe  83ea0a                sub       edx,0Ah
  00430901  c1e20c                shl       edx,0Ch
  00430904  03ca                  add       ecx,edx
  00430906  894e1c                mov       [esi+1Ch],ecx
  00430909  e832681100            call      00547140h
  0043090e  99                    cdq
  0043090f  b914000000            mov       ecx,14h
  00430914  8b5e24                mov       ebx,[esi+24h]
  00430917  f7f9                  idiv      ecx
  00430919  83ea0a                sub       edx,0Ah
  0043091c  c1e20c                shl       edx,0Ch
  0043091f  03da                  add       ebx,edx
  00430921  66ff4616              inc       word [esi+16h]
  00430925  668b4616              mov       ax,[esi+16h]
  00430929  895e24                mov       [esi+24h],ebx
  0043092c  66d1f8                sar       ax,1
  0043092f  3c04                  cmp       al,4
  00430931  884640                mov       [esi+40h],al
  00430934  7506                  jne       short 0043093Ch
  00430936  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043093c:
  0043093c  5f                    pop       edi
  0043093d  5e                    pop       esi
  0043093e  5d                    pop       ebp
  0043093f  5b                    pop       ebx
  00430940  59                    pop       ecx
  00430941  c3                    ret
  00430942  90                    nop
  00430943  90                    nop
  00430944  90                    nop
  00430945  90                    nop
  00430946  90                    nop
  00430947  90                    nop
  00430948  90                    nop
  00430949  90                    nop
  0043094a  90                    nop
  0043094b  90                    nop
  0043094c  90                    nop
  0043094d  90                    nop
  0043094e  90                    nop
  0043094f  90                    nop

22 cible(s) de saut interne : 0x004303c1, 0x004303d4, 0x004303e7, 0x004304b6, 0x004304ca, 0x004304e5, 0x004305b2, 0x004305c2, 0x0043068f, 0x00430762, 0x00430802, 0x0043081b, 0x00430839, 0x0043084a, 0x0043084c, 0x00430871, 0x00430876, 0x004308a7, 0x004308ac, 0x004308c5, 0x004308d8, 0x0043093c
