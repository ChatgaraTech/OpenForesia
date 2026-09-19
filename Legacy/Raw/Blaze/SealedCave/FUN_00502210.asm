Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00502210  section .text  offset fichier 0x101610  2752 octets

  00502210  83ec20                sub       esp,20h
  00502213  8d442414              lea       eax,[esp+14h]
  00502217  8d4c2418              lea       ecx,[esp+18h]
  0050221b  8d542410              lea       edx,[esp+10h]
  0050221f  53                    push      ebx
  00502220  55                    push      ebp
  00502221  56                    push      esi
  00502222  8b742430              mov       esi,[esp+30h]
  00502226  57                    push      edi
  00502227  50                    push      eax
  00502228  51                    push      ecx
  00502229  52                    push      edx
  0050222a  56                    push      esi
  0050222b  e850670300            call      00538980h
  00502230  83c410                add       esp,10h
  00502233  8d442418              lea       eax,[esp+18h]
  00502237  8d4c2414              lea       ecx,[esp+14h]
  0050223b  8d542434              lea       edx,[esp+34h]
  0050223f  50                    push      eax
  00502240  51                    push      ecx
  00502241  8d442418              lea       eax,[esp+18h]
  00502245  52                    push      edx
  00502246  8d4c2438              lea       ecx,[esp+38h]
  0050224a  50                    push      eax
  0050224b  51                    push      ecx
  0050224c  56                    push      esi
  0050224d  e8ce3c0300            call      00535F20h
  00502252  8b44242c              mov       eax,[esp+2Ch]
  00502256  83c418                add       esp,18h
  00502259  85c0                  test      eax,eax
  0050225b  bd01000000            mov       ebp,1
  00502260  bf06000000            mov       edi,6
  00502265  bb2c010000            mov       ebx,12Ch
  0050226a  0f843a070000          je        near 005029AAh
  00502270  56                    push      esi
  00502271  e82a93fdff            call      004DB5A0h
  00502276  8b06                  mov       eax,[esi]
  00502278  83c404                add       esp,4
  0050227b  a802                  test      al,2
  0050227d  0f84ed030000          je        near 00502670h
  00502283  8a4c2418              mov       cl,[esp+18h]
  00502287  bb10000000            mov       ebx,10h
  0050228c  84cb                  test      bl,cl
  0050228e  740d                  je        short 0050229Dh
  00502290  24fd                  and       al,0FDh
  00502292  c6464300              mov       byte [esi+43h],0
  00502296  8906                  mov       [esi],eax
  00502298  e90d070000            jmp       005029AAh

LAB_0050229d:
  0050229d  33c9                  xor       ecx,ecx
  0050229f  8a4e42                mov       cl,[esi+42h]
  005022a2  83f904                cmp       ecx,4
  005022a5  0f87ff060000          ja        near 005029AAh
  005022ab  ff248d7c2c5000        jmp       dword [ecx*4+502C7Ch]
  005022b2  0fbf4610              movsx     eax,word [esi+10h]
  005022b6  83e800                sub       eax,0
  005022b9  747a                  je        short 00502335h
  005022bb  48                    dec       eax
  005022bc  0f85e8060000          jne       near 005029AAh
  005022c2  66837e1a07            cmp       word [esi+1Ah],7
  005022c7  c7465c00000000        mov       dword [esi+5Ch],0
  005022ce  7544                  jne       short 00502314h
  005022d0  0fbf5632              movsx     edx,word [esi+32h]
  005022d4  52                    push      edx
  005022d5  c7465c00800200        mov       dword [esi+5Ch],28000h
  005022dc  e83b370400            call      00545A1Ch
  005022e1  0faf465c              imul      eax,[esi+5Ch]
  005022e5  8b5e50                mov       ebx,[esi+50h]
  005022e8  c1f80c                sar       eax,0Ch
  005022eb  03d8                  add       ebx,eax
  005022ed  0fbf4632              movsx     eax,word [esi+32h]
  005022f1  50                    push      eax
  005022f2  895e50                mov       [esi+50h],ebx
  005022f5  e81c370400            call      00545A16h
  005022fa  0faf465c              imul      eax,[esi+5Ch]
  005022fe  8b4e58                mov       ecx,[esi+58h]
  00502301  c7465c00500000        mov       dword [esi+5Ch],5000h
  00502308  c1f80c                sar       eax,0Ch
  0050230b  03c8                  add       ecx,eax
  0050230d  66ff4e10              dec       word [esi+10h]
  00502311  894e58                mov       [esi+58h],ecx

LAB_00502314:
  00502314  8b442434              mov       eax,[esp+34h]
  00502318  8d8880000000          lea       ecx,[eax+80h]
  0050231e  81e1ff0f0000          and       ecx,0FFFh
  00502324  81f900010000          cmp       ecx,100h
  0050232a  0f8e76060000          jle       near 005029A6h
  00502330  e956060000            jmp       0050298Bh

LAB_00502335:
  00502335  8b4650                mov       eax,[esi+50h]
  00502338  8b4e58                mov       ecx,[esi+58h]
  0050233b  c1f80c                sar       eax,0Ch
  0050233e  8bd0                  mov       edx,eax
  00502340  c1f90c                sar       ecx,0Ch
  00502343  0fafd0                imul      edx,eax
  00502346  8bc1                  mov       eax,ecx
  00502348  0fafc1                imul      eax,ecx
  0050234b  03d0                  add       edx,eax
  0050234d  52                    push      edx
  0050234e  e8bd360400            call      00545A10h
  00502353  8b0e                  mov       ecx,[esi]
  00502355  f7c100000002          test      ecx,2000000h
  0050235b  740a                  je        short 00502367h
  0050235d  83f808                cmp       eax,8
  00502360  7f05                  jg        short 00502367h
  00502362  83e1fd                and       ecx,0FFFFFFFDh
  00502365  890e                  mov       [esi],ecx

LAB_00502367:
  00502367  83f803                cmp       eax,3
  0050236a  0f8c3a060000          jl        near 005029AAh
  00502370  8b442434              mov       eax,[esp+34h]
  00502374  8d4828                lea       ecx,[eax+28h]
  00502377  81e1ff0f0000          and       ecx,0FFFh
  0050237d  83f950                cmp       ecx,50h
  00502380  7e18                  jle       short 0050239Ah
  00502382  25ff0f0000            and       eax,0FFFh
  00502387  33d2                  xor       edx,edx
  00502389  3d00080000            cmp       eax,800h
  0050238e  0f9dc2                setge     dl
  00502391  4a                    dec       edx
  00502392  83e250                and       edx,50h
  00502395  83c2d8                add       edx,0FFFFFFD8h
  00502398  8bc2                  mov       eax,edx

LAB_0050239a:
  0050239a  66014632              add       [esi+32h],ax
  0050239e  6a00                  push      0
  005023a0  8d461c                lea       eax,[esi+1Ch]
  005023a3  6880315500            push      553180h
  005023a8  50                    push      eax
  005023a9  56                    push      esi
  005023aa  e881780100            call      00519C30h
  005023af  8bf8                  mov       edi,eax
  005023b1  a1b8e05d00            mov       eax,[5DE0B8h]
  005023b6  83c410                add       esp,10h
  005023b9  85c0                  test      eax,eax
  005023bb  0f84e9050000          je        near 005029AAh
  005023c1  8b17                  mov       edx,[edi]
  005023c3  57                    push      edi
  005023c4  83ca40                or        edx,40h
  005023c7  56                    push      esi
  005023c8  66899f96000000        mov       [edi+96h],bx
  005023cf  8917                  mov       [edi],edx
  005023d1  e8aa770300            call      00539B80h
  005023d6  83c408                add       esp,8
  005023d9  83c71c                add       edi,1Ch
  005023dc  57                    push      edi
  005023dd  6800180000            push      1800h
  005023e2  6800030000            push      300h
  005023e7  6a03                  push      3
  005023e9  e832b70300            call      0053DB20h
  005023ee  83c410                add       esp,10h
  005023f1  e9b4050000            jmp       005029AAh
  005023f6  0fbf4e10              movsx     ecx,word [esi+10h]
  005023fa  83e900                sub       ecx,0
  005023fd  0f8453020000          je        near 00502656h
  00502403  49                    dec       ecx
  00502404  0f855a020000          jne       near 00502664h
  0050240a  a900000002            test      eax,2000000h
  0050240f  0f844f020000          je        near 00502664h
  00502415  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  0050241c  8b7e50                mov       edi,[esi+50h]
  0050241f  8b6e54                mov       ebp,[esi+54h]
  00502422  8b5e58                mov       ebx,[esi+58h]
  00502425  6a00                  push      0
  00502427  56                    push      esi
  00502428  894c2424              mov       [esp+24h],ecx
  0050242c  66c7869a0000000000    mov       word [esi+9Ah],0
  00502435  e836330300            call      00535770h
  0050243a  0fbf10                movsx     edx,word [eax]
  0050243d  c1e20c                shl       edx,0Ch
  00502440  895650                mov       [esi+50h],edx
  00502443  83c408                add       esp,8
  00502446  0fbf4802              movsx     ecx,word [eax+2]
  0050244a  c1e10c                shl       ecx,0Ch
  0050244d  894e54                mov       [esi+54h],ecx
  00502450  6aff                  push      0FFFFFFFFh
  00502452  0fbf5004              movsx     edx,word [eax+4]
  00502456  c1e20c                shl       edx,0Ch
  00502459  895658                mov       [esi+58h],edx
  0050245c  a1cc975e00            mov       eax,[5E97CCh]
  00502461  6aff                  push      0FFFFFFFFh
  00502463  8b88cc000000          mov       ecx,[eax+0CCh]
  00502469  33c0                  xor       eax,eax
  0050246b  8b11                  mov       edx,[ecx]
  0050246d  8a420b                mov       al,[edx+0Bh]
  00502470  05c0000000            add       eax,0C0h
  00502475  50                    push      eax
  00502476  56                    push      esi
  00502477  e85485f7ff            call      0047A9D0h
  0050247c  668b4c242c            mov       cx,[esp+2Ch]
  00502481  83c410                add       esp,10h
  00502484  66ff4e10              dec       word [esi+10h]
  00502488  897e50                mov       [esi+50h],edi
  0050248b  896e54                mov       [esi+54h],ebp
  0050248e  895e58                mov       [esi+58h],ebx
  00502491  66898e9a000000        mov       [esi+9Ah],cx
  00502498  c646430a              mov       byte [esi+43h],0Ah
  0050249c  c7465c00000000        mov       dword [esi+5Ch],0
  005024a3  e902050000            jmp       005029AAh
  005024a8  0fbf4e10              movsx     ecx,word [esi+10h]
  005024ac  83e900                sub       ecx,0
  005024af  0f84a1010000          je        near 00502656h
  005024b5  49                    dec       ecx
  005024b6  0f85a8010000          jne       near 00502664h
  005024bc  66837e1a0c            cmp       word [esi+1Ah],0Ch
  005024c1  7511                  jne       short 005024D4h
  005024c3  8d561c                lea       edx,[esi+1Ch]
  005024c6  52                    push      edx
  005024c7  68a0315500            push      5531A0h
  005024cc  e80fb70300            call      0053DBE0h
  005024d1  83c408                add       esp,8

LAB_005024d4:
  005024d4  f70600000002          test      dword [esi],2000000h
  005024da  0f8484010000          je        near 00502664h
  005024e0  8b7e44                mov       edi,[esi+44h]
  005024e3  8b6e48                mov       ebp,[esi+48h]
  005024e6  e8554c0400            call      00547140h
  005024eb  83e001                and       eax,1
  005024ee  c74648983a0000        mov       dword [esi+48h],3A98h
  005024f5  894644                mov       [esi+44h],eax
  005024f8  a1cc975e00            mov       eax,[5E97CCh]
  005024fd  6a02                  push      2
  005024ff  6a00                  push      0
  00502501  8b88cc000000          mov       ecx,[eax+0CCh]
  00502507  33c0                  xor       eax,eax
  00502509  8b11                  mov       edx,[ecx]
  0050250b  8a420e                mov       al,[edx+0Eh]
  0050250e  05c0000000            add       eax,0C0h
  00502513  50                    push      eax
  00502514  56                    push      esi
  00502515  e8b684f7ff            call      0047A9D0h
  0050251a  83c410                add       esp,10h
  0050251d  66ff4e10              dec       word [esi+10h]
  00502521  897e44                mov       [esi+44h],edi
  00502524  896e48                mov       [esi+48h],ebp
  00502527  c646430a              mov       byte [esi+43h],0Ah
  0050252b  c7465c00000000        mov       dword [esi+5Ch],0
  00502532  e973040000            jmp       005029AAh
  00502537  0fbf4e10              movsx     ecx,word [esi+10h]
  0050253b  83e900                sub       ecx,0
  0050253e  0f8412010000          je        near 00502656h
  00502544  49                    dec       ecx
  00502545  0f8519010000          jne       near 00502664h
  0050254b  66396e1a              cmp       [esi+1Ah],bp
  0050254f  7511                  jne       short 00502562h
  00502551  8d4e1c                lea       ecx,[esi+1Ch]
  00502554  51                    push      ecx
  00502555  68b8315500            push      5531B8h
  0050255a  e881b60300            call      0053DBE0h
  0050255f  83c408                add       esp,8

LAB_00502562:
  00502562  f70600000002          test      dword [esi],2000000h
  00502568  744f                  je        short 005025B9h
  0050256a  0fbf7e14              movsx     edi,word [esi+14h]
  0050256e  66c746140200          mov       word [esi+14h],2
  00502574  8b15cc975e00          mov       edx,[5E97CCh]
  0050257a  6a03                  push      3
  0050257c  6a00                  push      0
  0050257e  8b82d4000000          mov       eax,[edx+0D4h]
  00502584  33d2                  xor       edx,edx
  00502586  8b08                  mov       ecx,[eax]
  00502588  8d461c                lea       eax,[esi+1Ch]
  0050258b  8a5107                mov       dl,[ecx+7]
  0050258e  81c2c0000000          add       edx,0C0h
  00502594  52                    push      edx
  00502595  6a00                  push      0
  00502597  50                    push      eax
  00502598  56                    push      esi
  00502599  e8a286f7ff            call      0047AC40h
  0050259e  83c418                add       esp,18h
  005025a1  66ff4e10              dec       word [esi+10h]
  005025a5  66897e14              mov       [esi+14h],di
  005025a9  c646430f              mov       byte [esi+43h],0Fh
  005025ad  c7465c00000000        mov       dword [esi+5Ch],0
  005025b4  e9f1030000            jmp       005029AAh

LAB_005025b9:
  005025b9  f6461a01              test      byte [esi+1Ah],1
  005025bd  0f84a1000000          je        near 00502664h
  005025c3  0fbf7e14              movsx     edi,word [esi+14h]
  005025c7  66c746140000          mov       word [esi+14h],0
  005025cd  8b0dcc975e00          mov       ecx,[5E97CCh]
  005025d3  6a03                  push      3
  005025d5  6a00                  push      0
  005025d7  8b91d4000000          mov       edx,[ecx+0D4h]
  005025dd  33c9                  xor       ecx,ecx
  005025df  8b02                  mov       eax,[edx]
  005025e1  8d561c                lea       edx,[esi+1Ch]
  005025e4  8a4807                mov       cl,[eax+7]
  005025e7  81c1c0000000          add       ecx,0C0h
  005025ed  51                    push      ecx
  005025ee  6a00                  push      0
  005025f0  52                    push      edx
  005025f1  56                    push      esi
  005025f2  e84986f7ff            call      0047AC40h
  005025f7  83c418                add       esp,18h
  005025fa  66897e14              mov       [esi+14h],di
  005025fe  c7465c00000000        mov       dword [esi+5Ch],0
  00502605  e9a0030000            jmp       005029AAh
  0050260a  0fbf4e10              movsx     ecx,word [esi+10h]
  0050260e  83e900                sub       ecx,0
  00502611  7443                  je        short 00502656h
  00502613  49                    dec       ecx
  00502614  754e                  jne       short 00502664h
  00502616  a900000002            test      eax,2000000h
  0050261b  7447                  je        short 00502664h
  0050261d  a1cc975e00            mov       eax,[5E97CCh]
  00502622  6a02                  push      2
  00502624  6a00                  push      0
  00502626  8b88cc000000          mov       ecx,[eax+0CCh]
  0050262c  33c0                  xor       eax,eax
  0050262e  8b11                  mov       edx,[ecx]
  00502630  8a4216                mov       al,[edx+16h]
  00502633  05c0000000            add       eax,0C0h
  00502638  50                    push      eax
  00502639  56                    push      esi
  0050263a  e89183f7ff            call      0047A9D0h
  0050263f  83c410                add       esp,10h
  00502642  66ff4e10              dec       word [esi+10h]
  00502646  c646430a              mov       byte [esi+43h],0Ah
  0050264a  c7465c00000000        mov       dword [esi+5Ch],0
  00502651  e954030000            jmp       005029AAh

LAB_00502656:
  00502656  8a4e43                mov       cl,[esi+43h]
  00502659  fec9                  dec       cl
  0050265b  884e43                mov       [esi+43h],cl
  0050265e  7504                  jne       short 00502664h
  00502660  24fd                  and       al,0FDh
  00502662  8906                  mov       [esi],eax

LAB_00502664:
  00502664  c7465c00000000        mov       dword [esi+5Ch],0
  0050266b  e93a030000            jmp       005029AAh

LAB_00502670:
  00502670  a810                  test      al,10h
  00502672  0f8532030000          jne       near 005029AAh
  00502678  a904080000            test      eax,804h
  0050267d  0f8447010000          je        near 005027CAh
  00502683  f7442418e8030040      test      dword [esp+18h],400003E8h
  0050268b  740b                  je        short 00502698h
  0050268d  25fbf7ffff            and       eax,0FFFFF7FBh
  00502692  c6464300              mov       byte [esi+43h],0
  00502696  8906                  mov       [esi],eax

LAB_00502698:
  00502698  668b4610              mov       ax,[esi+10h]
  0050269c  0fbfc8                movsx     ecx,ax
  0050269f  83f903                cmp       ecx,3
  005026a2  0f8702030000          ja        near 005029AAh
  005026a8  ff248d902c5000        jmp       dword [ecx*4+502C90h]
  005026af  8b06                  mov       eax,[esi]
  005026b1  a900000002            test      eax,2000000h
  005026b6  0f84ee020000          je        near 005029AAh
  005026bc  80e4f7                and       ah,0F7h
  005026bf  8906                  mov       [esi],eax
  005026c1  e9e4020000            jmp       005029AAh
  005026c6  66397e18              cmp       [esi+18h],di
  005026ca  7411                  je        short 005026DDh
  005026cc  8b06                  mov       eax,[esi]
  005026ce  897e18                mov       [esi+18h],edi
  005026d1  25fbfffffe            and       eax,0FEFFFFFBh
  005026d6  8906                  mov       [esi],eax
  005026d8  e9cd020000            jmp       005029AAh

LAB_005026dd:
  005026dd  66837e1a17            cmp       word [esi+1Ah],17h
  005026e2  0f85c2020000          jne       near 005029AAh
  005026e8  33c0                  xor       eax,eax
  005026ea  6aff                  push      0FFFFFFFFh
  005026ec  668b4606              mov       ax,[esi+6]
  005026f0  6aff                  push      0FFFFFFFFh
  005026f2  8bc8                  mov       ecx,eax
  005026f4  c1f808                sar       eax,8
  005026f7  81e1ff000000          and       ecx,0FFh
  005026fd  25ff000000            and       eax,0FFh
  00502702  51                    push      ecx
  00502703  50                    push      eax
  00502704  56                    push      esi
  00502705  e876820300            call      0053A980h
  0050270a  83c414                add       esp,14h
  0050270d  66ff4e10              dec       word [esi+10h]
  00502711  e994020000            jmp       005029AAh
  00502716  8a4e43                mov       cl,[esi+43h]
  00502719  fec9                  dec       cl
  0050271b  884e43                mov       [esi+43h],cl
  0050271e  7505                  jne       short 00502725h
  00502720  48                    dec       eax
  00502721  66894610              mov       [esi+10h],ax

LAB_00502725:
  00502725  8b442434              mov       eax,[esp+34h]
  00502729  8d9080000000          lea       edx,[eax+80h]
  0050272f  81e2ff0f0000          and       edx,0FFFh
  00502735  81fa00010000          cmp       edx,100h
  0050273b  0f8e65020000          jle       near 005029A6h
  00502741  25ff0f0000            and       eax,0FFFh
  00502746  33c9                  xor       ecx,ecx
  00502748  3d00080000            cmp       eax,800h
  0050274d  0f9dc1                setge     cl
  00502750  49                    dec       ecx
  00502751  81e100010000          and       ecx,100h
  00502757  83c180                add       ecx,0FFFFFF80h
  0050275a  8bc1                  mov       eax,ecx
  0050275c  e945020000            jmp       005029A6h
  00502761  6a00                  push      0
  00502763  6a00                  push      0
  00502765  68bd000000            push      0BDh
  0050276a  56                    push      esi
  0050276b  e87083f7ff            call      0047AAE0h
  00502770  83c410                add       esp,10h
  00502773  e8c8490400            call      00547140h
  00502778  99                    cdq
  00502779  b905000000            mov       ecx,5
  0050277e  f7f9                  idiv      ecx
  00502780  33c0                  xor       eax,eax
  00502782  668b045590315500      mov       ax,[edx*2+553190h]
  0050278a  66894606              mov       [esi+6],ax
  0050278e  8b0dc4975e00          mov       ecx,[5E97C4h]
  00502794  8bd0                  mov       edx,eax
  00502796  25ff000000            and       eax,0FFh
  0050279b  8b899c000000          mov       ecx,[ecx+9Ch]
  005027a1  c1fa08                sar       edx,8
  005027a4  81e2ff000000          and       edx,0FFh
  005027aa  8d0440                lea       eax,[eax+eax*2]
  005027ad  c1e004                shl       eax,4
  005027b0  8b0c91                mov       ecx,[ecx+edx*4]
  005027b3  8a540113              mov       dl,[ecx+eax+13h]
  005027b7  66ff4e10              dec       word [esi+10h]
  005027bb  885643                mov       [esi+43h],dl
  005027be  c7465c00000000        mov       dword [esi+5Ch],0
  005027c5  e9e0010000            jmp       005029AAh

LAB_005027ca:
  005027ca  33c9                  xor       ecx,ecx
  005027cc  8a4e42                mov       cl,[esi+42h]
  005027cf  83f903                cmp       ecx,3
  005027d2  0f87d2010000          ja        near 005029AAh
  005027d8  ff248da02c5000        jmp       dword [ecx*4+502CA0h]
  005027df  66837e1800            cmp       word [esi+18h],0
  005027e4  740e                  je        short 005027F4h
  005027e6  0d00000001            or        eax,1000000h
  005027eb  c7461800000000        mov       dword [esi+18h],0
  005027f2  8906                  mov       [esi],eax

LAB_005027f4:
  005027f4  395c2410              cmp       [esp+10h],ebx
  005027f8  7d09                  jge       short 00502803h
  005027fa  c6464300              mov       byte [esi+43h],0
  005027fe  e9a7010000            jmp       005029AAh

LAB_00502803:
  00502803  8a4643                mov       al,[esi+43h]
  00502806  84c0                  test      al,al
  00502808  7405                  je        short 0050280Fh
  0050280a  fec8                  dec       al
  0050280c  884643                mov       [esi+43h],al

LAB_0050280f:
  0050280f  8b465c                mov       eax,[esi+5Ch]
  00502812  8bc8                  mov       ecx,eax
  00502814  c1f904                sar       ecx,4
  00502817  2bc1                  sub       eax,ecx
  00502819  89465c                mov       [esi+5Ch],eax
  0050281c  e989010000            jmp       005029AAh
  00502821  66837e1800            cmp       word [esi+18h],0
  00502826  740e                  je        short 00502836h
  00502828  0d00000001            or        eax,1000000h
  0050282d  c7461800000000        mov       dword [esi+18h],0
  00502834  8906                  mov       [esi],eax

LAB_00502836:
  00502836  8b442434              mov       eax,[esp+34h]
  0050283a  8d9080000000          lea       edx,[eax+80h]
  00502840  81e2ff0f0000          and       edx,0FFFh
  00502846  81fa00010000          cmp       edx,100h
  0050284c  7f1a                  jg        short 00502868h
  0050284e  66014632              add       [esi+32h],ax
  00502852  8b465c                mov       eax,[esi+5Ch]
  00502855  8bd0                  mov       edx,eax
  00502857  c6464300              mov       byte [esi+43h],0
  0050285b  c1fa04                sar       edx,4
  0050285e  2bc2                  sub       eax,edx
  00502860  89465c                mov       [esi+5Ch],eax
  00502863  e942010000            jmp       005029AAh

LAB_00502868:
  00502868  25ff0f0000            and       eax,0FFFh
  0050286d  33c9                  xor       ecx,ecx
  0050286f  3d00080000            cmp       eax,800h
  00502874  0f9dc1                setge     cl
  00502877  49                    dec       ecx
  00502878  81e100010000          and       ecx,100h
  0050287e  83c180                add       ecx,0FFFFFF80h
  00502881  8bc1                  mov       eax,ecx
  00502883  66014632              add       [esi+32h],ax
  00502887  8b465c                mov       eax,[esi+5Ch]
  0050288a  8bd0                  mov       edx,eax
  0050288c  c1fa04                sar       edx,4
  0050288f  2bc2                  sub       eax,edx
  00502891  89465c                mov       [esi+5Ch],eax
  00502894  e911010000            jmp       005029AAh
  00502899  66396e18              cmp       [esi+18h],bp
  0050289d  740a                  je        short 005028A9h
  0050289f  0d00000001            or        eax,1000000h
  005028a4  896e18                mov       [esi+18h],ebp
  005028a7  8906                  mov       [esi],eax

LAB_005028a9:
  005028a9  395c2410              cmp       [esp+10h],ebx
  005028ad  7d09                  jge       short 005028B8h
  005028af  c6464300              mov       byte [esi+43h],0
  005028b3  e9f2000000            jmp       005029AAh

LAB_005028b8:
  005028b8  8a4643                mov       al,[esi+43h]
  005028bb  84c0                  test      al,al
  005028bd  7405                  je        short 005028C4h
  005028bf  fec8                  dec       al
  005028c1  884643                mov       [esi+43h],al

LAB_005028c4:
  005028c4  8b442434              mov       eax,[esp+34h]
  005028c8  8d8880000000          lea       ecx,[eax+80h]
  005028ce  81e1ff0f0000          and       ecx,0FFFh
  005028d4  81f900010000          cmp       ecx,100h
  005028da  7e1b                  jle       short 005028F7h
  005028dc  25ff0f0000            and       eax,0FFFh
  005028e1  33d2                  xor       edx,edx
  005028e3  3d00080000            cmp       eax,800h
  005028e8  0f9dc2                setge     dl
  005028eb  4a                    dec       edx
  005028ec  81e200010000          and       edx,100h
  005028f2  83c280                add       edx,0FFFFFF80h
  005028f5  8bc2                  mov       eax,edx

LAB_005028f7:
  005028f7  66014632              add       [esi+32h],ax
  005028fb  8b465c                mov       eax,[esi+5Ch]
  005028fe  b900000100            mov       ecx,10000h
  00502903  2bc8                  sub       ecx,eax
  00502905  c1f904                sar       ecx,4
  00502908  03c8                  add       ecx,eax
  0050290a  894e5c                mov       [esi+5Ch],ecx
  0050290d  e998000000            jmp       005029AAh
  00502912  66837e1800            cmp       word [esi+18h],0
  00502917  740e                  je        short 00502927h
  00502919  0d00000001            or        eax,1000000h
  0050291e  c7461800000000        mov       dword [esi+18h],0
  00502925  8906                  mov       [esi],eax

LAB_00502927:
  00502927  8a4643                mov       al,[esi+43h]
  0050292a  84c0                  test      al,al
  0050292c  7405                  je        short 00502933h
  0050292e  fec8                  dec       al
  00502930  884643                mov       [esi+43h],al

LAB_00502933:
  00502933  0fbf5610              movsx     edx,word [esi+10h]
  00502937  52                    push      edx
  00502938  c7465c00400000        mov       dword [esi+5Ch],4000h
  0050293f  e8d8300400            call      00545A1Ch
  00502944  0faf465c              imul      eax,[esi+5Ch]
  00502948  8b5650                mov       edx,[esi+50h]
  0050294b  c1f80c                sar       eax,0Ch
  0050294e  03d0                  add       edx,eax
  00502950  0fbf4610              movsx     eax,word [esi+10h]
  00502954  50                    push      eax
  00502955  895650                mov       [esi+50h],edx
  00502958  e8b9300400            call      00545A16h
  0050295d  0faf465c              imul      eax,[esi+5Ch]
  00502961  8b5e58                mov       ebx,[esi+58h]
  00502964  c7465c00000000        mov       dword [esi+5Ch],0
  0050296b  c1f80c                sar       eax,0Ch
  0050296e  03d8                  add       ebx,eax
  00502970  8b442434              mov       eax,[esp+34h]
  00502974  895e58                mov       [esi+58h],ebx
  00502977  8d8880000000          lea       ecx,[eax+80h]
  0050297d  81e1ff0f0000          and       ecx,0FFFh
  00502983  81f900010000          cmp       ecx,100h
  00502989  7e1b                  jle       short 005029A6h

LAB_0050298b:
  0050298b  25ff0f0000            and       eax,0FFFh
  00502990  33d2                  xor       edx,edx
  00502992  3d00080000            cmp       eax,800h
  00502997  0f9dc2                setge     dl
  0050299a  4a                    dec       edx
  0050299b  81e200010000          and       edx,100h
  005029a1  83c280                add       edx,0FFFFFF80h
  005029a4  8bc2                  mov       eax,edx

LAB_005029a6:
  005029a6  66014632              add       [esi+32h],ax

LAB_005029aa:
  005029aa  8b442414              mov       eax,[esp+14h]
  005029ae  50                    push      eax
  005029af  56                    push      esi
  005029b0  e82b7ff9ff            call      0049A8E0h
  005029b5  83c408                add       esp,8
  005029b8  56                    push      esi
  005029b9  e8820f0300            call      00533940h
  005029be  8b9690000000          mov       edx,[esi+90h]
  005029c4  8b466c                mov       eax,[esi+6Ch]
  005029c7  81e2ffd8ffdf          and       edx,0DFFFD8FFh
  005029cd  83c404                add       esp,4
  005029d0  899690000000          mov       [esi+90h],edx
  005029d6  8a08                  mov       cl,[eax]
  005029d8  f6c101                test      cl,1
  005029db  7506                  jne       short 005029E3h
  005029dd  8b4860                mov       ecx,[eax+60h]
  005029e0  894e48                mov       [esi+48h],ecx

LAB_005029e3:
  005029e3  e858470400            call      00547140h
  005029e8  8b7e60                mov       edi,[esi+60h]
  005029eb  25ff000000            and       eax,0FFh
  005029f0  0500010000            add       eax,100h
  005029f5  66014614              add       [esi+14h],ax
  005029f9  8b4648                mov       eax,[esi+48h]
  005029fc  668b4e14              mov       cx,[esi+14h]
  00502a00  3bc7                  cmp       eax,edi
  00502a02  7c02                  jl        short 00502A06h
  00502a04  8bf8                  mov       edi,eax

LAB_00502a06:
  00502a06  f60602                test      byte [esi],2
  00502a09  7415                  je        short 00502A20h
  00502a0b  8b4620                mov       eax,[esi+20h]
  00502a0e  2bf8                  sub       edi,eax
  00502a10  81c700800200          add       edi,28000h
  00502a16  c1ff03                sar       edi,3
  00502a19  03f8                  add       edi,eax
  00502a1b  897e20                mov       [esi+20h],edi
  00502a1e  eb20                  jmp       short 00502A40h

LAB_00502a20:
  00502a20  8b6e20                mov       ebp,[esi+20h]
  00502a23  0fbfd1                movsx     edx,cx
  00502a26  52                    push      edx
  00502a27  e8f02f0400            call      00545A1Ch
  00502a2c  0500320000            add       eax,3200h
  00502a31  c1e006                shl       eax,6
  00502a34  03c7                  add       eax,edi
  00502a36  2bc5                  sub       eax,ebp
  00502a38  c1f804                sar       eax,4
  00502a3b  03c5                  add       eax,ebp
  00502a3d  894620                mov       [esi+20h],eax

LAB_00502a40:
  00502a40  8b442420              mov       eax,[esp+20h]
  00502a44  50                    push      eax
  00502a45  56                    push      esi
  00502a46  e8a5f2f9ff            call      004A1CF0h
  00502a4b  8b44241c              mov       eax,[esp+1Ch]
  00502a4f  83c408                add       esp,8
  00502a52  85c0                  test      eax,eax
  00502a54  0f8417020000          je        near 00502C71h
  00502a5a  f70616080000          test      dword [esi],816h
  00502a60  0f850b020000          jne       near 00502C71h
  00502a66  8a4643                mov       al,[esi+43h]
  00502a69  84c0                  test      al,al
  00502a6b  0f8500020000          jne       near 00502C71h
  00502a71  8b466c                mov       eax,[esi+6Ch]
  00502a74  8b5e1c                mov       ebx,[esi+1Ch]
  00502a77  8b4e24                mov       ecx,[esi+24h]
  00502a7a  8b781c                mov       edi,[eax+1Ch]
  00502a7d  8b6824                mov       ebp,[eax+24h]
  00502a80  2bfb                  sub       edi,ebx
  00502a82  2be9                  sub       ebp,ecx
  00502a84  c1ff0c                sar       edi,0Ch
  00502a87  c1fd0c                sar       ebp,0Ch
  00502a8a  8bcf                  mov       ecx,edi
  00502a8c  8bd5                  mov       edx,ebp
  00502a8e  0fafcf                imul      ecx,edi
  00502a91  0fafd5                imul      edx,ebp
  00502a94  03ca                  add       ecx,edx
  00502a96  51                    push      ecx
  00502a97  e8742f0400            call      00545A10h
  00502a9c  55                    push      ebp
  00502a9d  57                    push      edi
  00502a9e  89442418              mov       [esp+18h],eax
  00502aa2  e87b2f0400            call      00545A22h
  00502aa7  668b4e32              mov       cx,[esi+32h]
  00502aab  2bc1                  sub       eax,ecx
  00502aad  25ff0f0000            and       eax,0FFFh
  00502ab2  89442434              mov       [esp+34h],eax
  00502ab6  e885460400            call      00547140h
  00502abb  99                    cdq
  00502abc  b964000000            mov       ecx,64h
  00502ac1  f7f9                  idiv      ecx
  00502ac3  817c24102c010000      cmp       dword [esp+10h],12Ch
  00502acb  7d49                  jge       short 00502B16h
  00502acd  83fa3c                cmp       edx,3Ch
  00502ad0  0f8d64010000          jge       near 00502C3Ah
  00502ad6  8b542434              mov       edx,[esp+34h]
  00502ada  81c200010000          add       edx,100h
  00502ae0  81e2ff0f0000          and       edx,0FFFh
  00502ae6  81fa00020000          cmp       edx,200h
  00502aec  0f8f14010000          jg        near 00502C06h
  00502af2  8b06                  mov       eax,[esi]
  00502af4  c7461803000000        mov       dword [esi+18h],3
  00502afb  25fffffffe            and       eax,0FEFFFFFFh
  00502b00  c6464200              mov       byte [esi+42h],0
  00502b04  0c02                  or        al,2
  00502b06  66c746100100          mov       word [esi+10h],1
  00502b0c  8906                  mov       [esi],eax
  00502b0e  5f                    pop       edi
  00502b0f  5e                    pop       esi
  00502b10  5d                    pop       ebp
  00502b11  5b                    pop       ebx
  00502b12  83c420                add       esp,20h
  00502b15  c3                    ret

LAB_00502b16:
  00502b16  83fa1e                cmp       edx,1Eh
  00502b19  0f8df7000000          jge       near 00502C16h
  00502b1f  8b442434              mov       eax,[esp+34h]
  00502b23  0500010000            add       eax,100h
  00502b28  25ff0f0000            and       eax,0FFFh
  00502b2d  3d00020000            cmp       eax,200h
  00502b32  0f8fce000000          jg        near 00502C06h
  00502b38  8bc2                  mov       eax,edx
  00502b3a  b905000000            mov       ecx,5
  00502b3f  99                    cdq
  00502b40  f7f9                  idiv      ecx
  00502b42  83fa04                cmp       edx,4
  00502b45  0f8726010000          ja        near 00502C71h
  00502b4b  ff2495b02c5000        jmp       dword [edx*4+502CB0h]
  00502b52  8b06                  mov       eax,[esi]
  00502b54  c7461805000000        mov       dword [esi+18h],5
  00502b5b  0d04080001            or        eax,1000804h
  00502b60  66c746100300          mov       word [esi+10h],3
  00502b66  8906                  mov       [esi],eax
  00502b68  5f                    pop       edi
  00502b69  5e                    pop       esi
  00502b6a  5d                    pop       ebp
  00502b6b  5b                    pop       ebx
  00502b6c  83c420                add       esp,20h
  00502b6f  c3                    ret
  00502b70  8b16                  mov       edx,[esi]
  00502b72  c7461803000000        mov       dword [esi+18h],3
  00502b79  81e2fffffffe          and       edx,0FEFFFFFFh
  00502b7f  c6464201              mov       byte [esi+42h],1
  00502b83  83ca02                or        edx,2
  00502b86  66c746100100          mov       word [esi+10h],1
  00502b8c  8916                  mov       [esi],edx
  00502b8e  5f                    pop       edi
  00502b8f  5e                    pop       esi
  00502b90  5d                    pop       ebp
  00502b91  5b                    pop       ebx
  00502b92  83c420                add       esp,20h
  00502b95  c3                    ret
  00502b96  8b06                  mov       eax,[esi]
  00502b98  c7461806000000        mov       dword [esi+18h],6
  00502b9f  25fffffffe            and       eax,0FEFFFFFFh
  00502ba4  c6464202              mov       byte [esi+42h],2
  00502ba8  0c02                  or        al,2
  00502baa  66c746100100          mov       word [esi+10h],1
  00502bb0  8906                  mov       [esi],eax
  00502bb2  5f                    pop       edi
  00502bb3  5e                    pop       esi
  00502bb4  5d                    pop       ebp
  00502bb5  5b                    pop       ebx
  00502bb6  83c420                add       esp,20h
  00502bb9  c3                    ret
  00502bba  8b0e                  mov       ecx,[esi]
  00502bbc  c7461806000000        mov       dword [esi+18h],6
  00502bc3  81e1fffffffe          and       ecx,0FEFFFFFFh
  00502bc9  c6464203              mov       byte [esi+42h],3
  00502bcd  83c902                or        ecx,2
  00502bd0  66c746100100          mov       word [esi+10h],1
  00502bd6  890e                  mov       [esi],ecx
  00502bd8  5f                    pop       edi
  00502bd9  5e                    pop       esi
  00502bda  5d                    pop       ebp
  00502bdb  5b                    pop       ebx
  00502bdc  83c420                add       esp,20h
  00502bdf  c3                    ret
  00502be0  8b16                  mov       edx,[esi]
  00502be2  c7461806000000        mov       dword [esi+18h],6
  00502be9  81e2fffffffe          and       edx,0FEFFFFFFh
  00502bef  c6464204              mov       byte [esi+42h],4
  00502bf3  83ca02                or        edx,2
  00502bf6  66c746100100          mov       word [esi+10h],1
  00502bfc  8916                  mov       [esi],edx
  00502bfe  5f                    pop       edi
  00502bff  5e                    pop       esi
  00502c00  5d                    pop       ebp
  00502c01  5b                    pop       ebx
  00502c02  83c420                add       esp,20h
  00502c05  c3                    ret

LAB_00502c06:
  00502c06  c6464201              mov       byte [esi+42h],1
  00502c0a  c6464301              mov       byte [esi+43h],1
  00502c0e  5f                    pop       edi
  00502c0f  5e                    pop       esi
  00502c10  5d                    pop       ebp
  00502c11  5b                    pop       ebx
  00502c12  83c420                add       esp,20h
  00502c15  c3                    ret

LAB_00502c16:
  00502c16  83fa5a                cmp       edx,5Ah
  00502c19  7d1f                  jge       short 00502C3Ah
  00502c1b  c6464202              mov       byte [esi+42h],2
  00502c1f  e81c450400            call      00547140h
  00502c24  99                    cdq
  00502c25  b90a000000            mov       ecx,0Ah
  00502c2a  f7f9                  idiv      ecx
  00502c2c  80c214                add       dl,14h
  00502c2f  885643                mov       [esi+43h],dl
  00502c32  5f                    pop       edi
  00502c33  5e                    pop       esi
  00502c34  5d                    pop       ebp
  00502c35  5b                    pop       ebx
  00502c36  83c420                add       esp,20h
  00502c39  c3                    ret

LAB_00502c3a:
  00502c3a  c6464203              mov       byte [esi+42h],3
  00502c3e  e8fd440400            call      00547140h
  00502c43  99                    cdq
  00502c44  b90a000000            mov       ecx,0Ah
  00502c49  f7f9                  idiv      ecx
  00502c4b  02d1                  add       dl,cl
  00502c4d  885643                mov       [esi+43h],dl
  00502c50  e8eb440400            call      00547140h
  00502c55  99                    cdq
  00502c56  b9000c0000            mov       ecx,0C00h
  00502c5b  8b7c2434              mov       edi,[esp+34h]
  00502c5f  f7f9                  idiv      ecx
  00502c61  03d7                  add       edx,edi
  00502c63  66035632              add       dx,[esi+32h]
  00502c67  81ea00060000          sub       edx,600h
  00502c6d  66895610              mov       [esi+10h],dx

LAB_00502c71:
  00502c71  5f                    pop       edi
  00502c72  5e                    pop       esi
  00502c73  5d                    pop       ebp
  00502c74  5b                    pop       ebx
  00502c75  83c420                add       esp,20h
  00502c78  c3                    ret
  00502c79  90                    nop
  00502c7a  8bff                  mov       edi,edi
  00502c7c  b222                  mov       dl,22h
  00502c7e  50                    push      eax
  00502c7f  00f6                  add       dh,dh
  00502c81  235000                and       edx,[eax]
  00502c84  a824                  test      al,24h
  00502c86  50                    push      eax
  00502c87  0037                  add       [edi],dh
  00502c89  2550000a26            and       eax,260A0050h
  00502c8e  50                    push      eax
  00502c8f  00af265000c6          add       [edi-39FFAFDAh],ch
  00502c95  2650                  push      eax
  00502c97  0016                  add       [esi],dl
  00502c99  27                    daa
  00502c9a  50                    push      eax
  00502c9b  006127                add       [ecx+27h],ah
  00502c9e  50                    push      eax
  00502c9f  00df                  add       bh,bl
  00502ca1  27                    daa
  00502ca2  50                    push      eax
  00502ca3  0021                  add       [ecx],ah
  00502ca5  285000                sub       [eax],dl
  00502ca8  99                    cdq
  00502ca9  285000                sub       [eax],dl
  00502cac  1229                  adc       ch,[ecx]
  00502cae  50                    push      eax
  00502caf  00522b                add       [edx+2Bh],dl
  00502cb2  50                    push      eax
  00502cb3  00702b                add       [eax+2Bh],dh
  00502cb6  50                    push      eax
  00502cb7  00962b5000ba          add       [esi-45FFAFD5h],dl
  00502cbd  2b5000                sub       edx,[eax]
  00502cc0  e02b                  loopne    00502CEDh
  00502cc2  50                    push      eax
  00502cc3  009090909090          add       [eax-6F6F6F70h],dl
  00502cc9  90                    nop
  00502cca  90                    nop
  00502ccb  90                    nop
  00502ccc  90                    nop
  00502ccd  90                    nop
  00502cce  90                    nop
  00502ccf  90                    nop

38 cible(s) de saut interne : 0x0050229d, 0x00502314, 0x00502335, 0x00502367, 0x0050239a, 0x005024d4, 0x00502562, 0x005025b9, 0x00502656, 0x00502664, 0x00502670, 0x00502698, 0x005026dd, 0x00502725, 0x005027ca, 0x005027f4, 0x00502803, 0x0050280f, 0x00502836, 0x00502868, 0x005028a9, 0x005028b8, 0x005028c4, 0x005028f7, 0x00502927, 0x00502933, 0x0050298b, 0x005029a6, 0x005029aa, 0x005029e3, 0x00502a06, 0x00502a20, 0x00502a40, 0x00502b16, 0x00502c06, 0x00502c16, 0x00502c3a, 0x00502c71
