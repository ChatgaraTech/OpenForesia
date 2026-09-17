Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00499140  section .text  offset fichier 0x98540  3232 octets

  00499140  83ec28                sub       esp,28h
  00499143  8d442400              lea       eax,[esp]
  00499147  8d4c2424              lea       ecx,[esp+24h]
  0049914b  8d542420              lea       edx,[esp+20h]
  0049914f  53                    push      ebx
  00499150  55                    push      ebp
  00499151  56                    push      esi
  00499152  8b742438              mov       esi,[esp+38h]
  00499156  57                    push      edi
  00499157  50                    push      eax
  00499158  51                    push      ecx
  00499159  52                    push      edx
  0049915a  56                    push      esi
  0049915b  e820f80900            call      00538980h
  00499160  83c410                add       esp,10h
  00499163  8d44242c              lea       eax,[esp+2Ch]
  00499167  8d4c2424              lea       ecx,[esp+24h]
  0049916b  8d54243c              lea       edx,[esp+3Ch]
  0049916f  50                    push      eax
  00499170  51                    push      ecx
  00499171  8d44241c              lea       eax,[esp+1Ch]
  00499175  52                    push      edx
  00499176  8d4c2434              lea       ecx,[esp+34h]
  0049917a  50                    push      eax
  0049917b  51                    push      ecx
  0049917c  56                    push      esi
  0049917d  e89ecd0900            call      00535F20h
  00499182  8b44243c              mov       eax,[esp+3Ch]
  00499186  83c418                add       esp,18h
  00499189  85c0                  test      eax,eax
  0049918b  0f848c070000          je        near 0049991Dh
  00499191  56                    push      esi
  00499192  e809240400            call      004DB5A0h
  00499197  8b06                  mov       eax,[esi]
  00499199  83c404                add       esp,4
  0049919c  a802                  test      al,2
  0049919e  0f84ac050000          je        near 00499750h
  004991a4  8a4c242c              mov       cl,[esp+2Ch]
  004991a8  bb10000000            mov       ebx,10h
  004991ad  84cb                  test      bl,cl
  004991af  740d                  je        short 004991BEh
  004991b1  24fd                  and       al,0FDh
  004991b3  c6464300              mov       byte [esi+43h],0
  004991b7  8906                  mov       [esi],eax
  004991b9  e95f070000            jmp       0049991Dh

LAB_004991be:
  004991be  33c9                  xor       ecx,ecx
  004991c0  8a4e42                mov       cl,[esi+42h]
  004991c3  83f904                cmp       ecx,4
  004991c6  0f8751070000          ja        near 0049991Dh
  004991cc  ff248dac9d4900        jmp       dword [ecx*4+499DACh]
  004991d3  668b4e1a              mov       cx,[esi+1Ah]
  004991d7  6683f905              cmp       cx,5
  004991db  7f34                  jg        short 00499211h
  004991dd  8b44243c              mov       eax,[esp+3Ch]
  004991e1  8d5040                lea       edx,[eax+40h]
  004991e4  81e2ff0f0000          and       edx,0FFFh
  004991ea  81fa80000000          cmp       edx,80h
  004991f0  7e1b                  jle       short 0049920Dh
  004991f2  25ff0f0000            and       eax,0FFFh
  004991f7  33d2                  xor       edx,edx
  004991f9  3d00080000            cmp       eax,800h
  004991fe  0f9dc2                setge     dl
  00499201  4a                    dec       edx
  00499202  81e280000000          and       edx,80h
  00499208  83c2c0                add       edx,0FFFFFFC0h
  0049920b  8bc2                  mov       eax,edx

LAB_0049920d:
  0049920d  66014632              add       [esi+32h],ax

LAB_00499211:
  00499211  6683f908              cmp       cx,8
  00499215  7540                  jne       short 00499257h
  00499217  0fbf4632              movsx     eax,word [esi+32h]
  0049921b  50                    push      eax
  0049921c  c7465c00c00100        mov       dword [esi+5Ch],1C000h
  00499223  e8f4c70a00            call      00545A1Ch
  00499228  0faf465c              imul      eax,[esi+5Ch]
  0049922c  0fbf4e32              movsx     ecx,word [esi+32h]
  00499230  8b6e50                mov       ebp,[esi+50h]
  00499233  51                    push      ecx
  00499234  c1f80c                sar       eax,0Ch
  00499237  03e8                  add       ebp,eax
  00499239  896e50                mov       [esi+50h],ebp
  0049923c  e8d5c70a00            call      00545A16h
  00499241  0faf465c              imul      eax,[esi+5Ch]
  00499245  8b4e58                mov       ecx,[esi+58h]
  00499248  c7465c00000000        mov       dword [esi+5Ch],0
  0049924f  c1f80c                sar       eax,0Ch
  00499252  03c8                  add       ecx,eax
  00499254  894e58                mov       [esi+58h],ecx

LAB_00499257:
  00499257  66837e1a0b            cmp       word [esi+1Ah],0Bh
  0049925c  0f8c12010000          jl        near 00499374h
  00499262  f60620                test      byte [esi],20h
  00499265  0f8509010000          jne       near 00499374h
  0049926b  8d6e1c                lea       ebp,[esi+1Ch]
  0049926e  6a00                  push      0
  00499270  6830295500            push      552930h
  00499275  55                    push      ebp
  00499276  56                    push      esi
  00499277  e8b4090800            call      00519C30h
  0049927c  8bf8                  mov       edi,eax
  0049927e  83c410                add       esp,10h
  00499281  85ff                  test      edi,edi
  00499283  7406                  je        short 0049928Bh
  00499285  8b06                  mov       eax,[esi]
  00499287  0c20                  or        al,20h
  00499289  8906                  mov       [esi],eax

LAB_0049928b:
  0049928b  a1b8e05d00            mov       eax,[5DE0B8h]
  00499290  85c0                  test      eax,eax
  00499292  0f84dc000000          je        near 00499374h
  00499298  66899f96000000        mov       [edi+96h],bx
  0049929f  8b1f                  mov       ebx,[edi]
  004992a1  83cb40                or        ebx,40h
  004992a4  57                    push      edi
  004992a5  891f                  mov       [edi],ebx
  004992a7  8b4c2414              mov       ecx,[esp+14h]
  004992ab  56                    push      esi
  004992ac  668b4130              mov       ax,[ecx+30h]
  004992b0  0fbfd8                movsx     ebx,ax
  004992b3  66d1f8                sar       ax,1
  004992b6  66894130              mov       [ecx+30h],ax
  004992ba  e8c1080a00            call      00539B80h
  004992bf  8b542418              mov       edx,[esp+18h]
  004992c3  83c408                add       esp,8
  004992c6  66895a30              mov       [edx+30h],bx
  004992ca  8d5f1c                lea       ebx,[edi+1Ch]
  004992cd  53                    push      ebx
  004992ce  6800180000            push      1800h
  004992d3  6800030000            push      300h
  004992d8  6a03                  push      3
  004992da  e841480a00            call      0053DB20h
  004992df  8b07                  mov       eax,[edi]
  004992e1  83c410                add       esp,10h
  004992e4  a900000040            test      eax,40000000h
  004992e9  0f8585000000          jne       near 00499374h
  004992ef  0fbf4632              movsx     eax,word [esi+32h]
  004992f3  50                    push      eax
  004992f4  e823c70a00            call      00545A1Ch
  004992f9  8d0440                lea       eax,[eax+eax*2]
  004992fc  8d0c80                lea       ecx,[eax+eax*4]
  004992ff  d1e1                  shl       ecx,1
  00499301  894f50                mov       [edi+50h],ecx
  00499304  0fbf5632              movsx     edx,word [esi+32h]
  00499308  52                    push      edx
  00499309  e808c70a00            call      00545A16h
  0049930e  8d0440                lea       eax,[eax+eax*2]
  00499311  8d0480                lea       eax,[eax+eax*4]
  00499314  d1e0                  shl       eax,1
  00499316  894758                mov       [edi+58h],eax
  00499319  8b07                  mov       eax,[edi]
  0049931b  a801                  test      al,1
  0049931d  750b                  jne       short 0049932Ah
  0049931f  0c01                  or        al,1
  00499321  c7475400800100        mov       dword [edi+54h],18000h
  00499328  8907                  mov       [edi],eax

LAB_0049932a:
  0049932a  8b4d00                mov       ecx,[ebp]
  0049932d  8b5620                mov       edx,[esi+20h]
  00499330  8b4624                mov       eax,[esi+24h]
  00499333  894c2418              mov       [esp+18h],ecx
  00499337  8b0b                  mov       ecx,[ebx]
  00499339  8954241c              mov       [esp+1Ch],edx
  0049933d  894d00                mov       [ebp],ecx
  00499340  8b5720                mov       edx,[edi+20h]
  00499343  6aff                  push      0FFFFFFFFh
  00499345  89442424              mov       [esp+24h],eax
  00499349  895620                mov       [esi+20h],edx
  0049934c  8b4724                mov       eax,[edi+24h]
  0049934f  6a00                  push      0
  00499351  6a39                  push      39h
  00499353  56                    push      esi
  00499354  894624                mov       [esi+24h],eax
  00499357  e87416feff            call      0047A9D0h
  0049935c  8b4c2428              mov       ecx,[esp+28h]
  00499360  8b54242c              mov       edx,[esp+2Ch]
  00499364  8b442430              mov       eax,[esp+30h]
  00499368  83c410                add       esp,10h
  0049936b  894d00                mov       [ebp],ecx
  0049936e  895620                mov       [esi+20h],edx
  00499371  894624                mov       [esi+24h],eax

LAB_00499374:
  00499374  8b06                  mov       eax,[esi]
  00499376  a900000002            test      eax,2000000h
  0049937b  0f84c3030000          je        near 00499744h
  00499381  24dd                  and       al,0DDh
  00499383  c7465c00000000        mov       dword [esi+5Ch],0
  0049938a  8906                  mov       [esi],eax
  0049938c  e98c050000            jmp       0049991Dh
  00499391  668b4e1a              mov       cx,[esi+1Ah]
  00499395  6683f908              cmp       cx,8
  00499399  7f34                  jg        short 004993CFh
  0049939b  8b44243c              mov       eax,[esp+3Ch]
  0049939f  8d5040                lea       edx,[eax+40h]
  004993a2  81e2ff0f0000          and       edx,0FFFh
  004993a8  81fa80000000          cmp       edx,80h
  004993ae  7e1b                  jle       short 004993CBh
  004993b0  25ff0f0000            and       eax,0FFFh
  004993b5  33d2                  xor       edx,edx
  004993b7  3d00080000            cmp       eax,800h
  004993bc  0f9dc2                setge     dl
  004993bf  4a                    dec       edx
  004993c0  81e280000000          and       edx,80h
  004993c6  83c2c0                add       edx,0FFFFFFC0h
  004993c9  8bc2                  mov       eax,edx

LAB_004993cb:
  004993cb  66014632              add       [esi+32h],ax

LAB_004993cf:
  004993cf  6683f90b              cmp       cx,0Bh
  004993d3  7540                  jne       short 00499415h
  004993d5  0fbf4632              movsx     eax,word [esi+32h]
  004993d9  50                    push      eax
  004993da  c7465c00c00100        mov       dword [esi+5Ch],1C000h
  004993e1  e836c60a00            call      00545A1Ch
  004993e6  0faf465c              imul      eax,[esi+5Ch]
  004993ea  0fbf4e32              movsx     ecx,word [esi+32h]
  004993ee  8b6e50                mov       ebp,[esi+50h]
  004993f1  51                    push      ecx
  004993f2  c1f80c                sar       eax,0Ch
  004993f5  03e8                  add       ebp,eax
  004993f7  896e50                mov       [esi+50h],ebp
  004993fa  e817c60a00            call      00545A16h
  004993ff  0faf465c              imul      eax,[esi+5Ch]
  00499403  8b4e58                mov       ecx,[esi+58h]
  00499406  c7465c00000000        mov       dword [esi+5Ch],0
  0049940d  c1f80c                sar       eax,0Ch
  00499410  03c8                  add       ecx,eax
  00499412  894e58                mov       [esi+58h],ecx

LAB_00499415:
  00499415  66837e1a0c            cmp       word [esi+1Ah],0Ch
  0049941a  7c4f                  jl        short 0049946Bh
  0049941c  6a00                  push      0
  0049941e  8d561c                lea       edx,[esi+1Ch]
  00499421  683c295500            push      55293Ch
  00499426  52                    push      edx
  00499427  56                    push      esi
  00499428  e803080800            call      00519C30h
  0049942d  8bf8                  mov       edi,eax
  0049942f  a1b8e05d00            mov       eax,[5DE0B8h]
  00499434  83c410                add       esp,10h
  00499437  85c0                  test      eax,eax
  00499439  7430                  je        short 0049946Bh
  0049943b  8b17                  mov       edx,[edi]
  0049943d  57                    push      edi
  0049943e  83ca40                or        edx,40h
  00499441  56                    push      esi
  00499442  66899f96000000        mov       [edi+96h],bx
  00499449  8917                  mov       [edi],edx
  0049944b  e830070a00            call      00539B80h
  00499450  83c408                add       esp,8
  00499453  83c71c                add       edi,1Ch
  00499456  57                    push      edi
  00499457  6800180000            push      1800h
  0049945c  6800030000            push      300h
  00499461  6a03                  push      3
  00499463  e8b8460a00            call      0053DB20h
  00499468  83c410                add       esp,10h

LAB_0049946b:
  0049946b  8b06                  mov       eax,[esi]
  0049946d  a900000002            test      eax,2000000h
  00499472  0f84cc020000          je        near 00499744h
  00499478  24fd                  and       al,0FDh
  0049947a  c7465c00000000        mov       dword [esi+5Ch],0
  00499481  8906                  mov       [esi],eax
  00499483  e995040000            jmp       0049991Dh
  00499488  0fbf4e10              movsx     ecx,word [esi+10h]
  0049948c  83e900                sub       ecx,0
  0049948f  745e                  je        short 004994EFh
  00499491  49                    dec       ecx
  00499492  0f8585040000          jne       near 0049991Dh
  00499498  0fbf4632              movsx     eax,word [esi+32h]
  0049949c  50                    push      eax
  0049949d  c7465c00000500        mov       dword [esi+5Ch],50000h
  004994a4  e873c50a00            call      00545A1Ch
  004994a9  0faf465c              imul      eax,[esi+5Ch]
  004994ad  0fbf4e32              movsx     ecx,word [esi+32h]
  004994b1  8b6e50                mov       ebp,[esi+50h]
  004994b4  51                    push      ecx
  004994b5  c1f80c                sar       eax,0Ch
  004994b8  03e8                  add       ebp,eax
  004994ba  896e50                mov       [esi+50h],ebp
  004994bd  e854c50a00            call      00545A16h
  004994c2  0faf465c              imul      eax,[esi+5Ch]
  004994c6  8b5658                mov       edx,[esi+58h]
  004994c9  8b0e                  mov       ecx,[esi]
  004994cb  c1f80c                sar       eax,0Ch
  004994ce  03d0                  add       edx,eax
  004994d0  83c901                or        ecx,1
  004994d3  66ff4e10              dec       word [esi+10h]
  004994d7  895658                mov       [esi+58h],edx
  004994da  c7465400400600        mov       dword [esi+54h],64000h
  004994e1  890e                  mov       [esi],ecx
  004994e3  c7465c00000000        mov       dword [esi+5Ch],0
  004994ea  e92e040000            jmp       0049991Dh

LAB_004994ef:
  004994ef  a900000002            test      eax,2000000h
  004994f4  7408                  je        short 004994FEh
  004994f6  a801                  test      al,1
  004994f8  7504                  jne       short 004994FEh
  004994fa  24dd                  and       al,0DDh
  004994fc  8906                  mov       [esi],eax

LAB_004994fe:
  004994fe  66837e1a12            cmp       word [esi+1Ah],12h
  00499503  0f8c14040000          jl        near 0049991Dh
  00499509  f60620                test      byte [esi],20h
  0049950c  0f850b040000          jne       near 0049991Dh
  00499512  8d6e1c                lea       ebp,[esi+1Ch]
  00499515  6a00                  push      0
  00499517  6848295500            push      552948h
  0049951c  55                    push      ebp
  0049951d  56                    push      esi
  0049951e  e80d070800            call      00519C30h
  00499523  8bf8                  mov       edi,eax
  00499525  83c410                add       esp,10h
  00499528  85ff                  test      edi,edi
  0049952a  7406                  je        short 00499532h
  0049952c  8b06                  mov       eax,[esi]
  0049952e  0c20                  or        al,20h
  00499530  8906                  mov       [esi],eax

LAB_00499532:
  00499532  a1b8e05d00            mov       eax,[5DE0B8h]
  00499537  85c0                  test      eax,eax
  00499539  0f84de030000          je        near 0049991Dh
  0049953f  66899f96000000        mov       [edi+96h],bx
  00499546  8b1f                  mov       ebx,[edi]
  00499548  83cb40                or        ebx,40h
  0049954b  57                    push      edi
  0049954c  891f                  mov       [edi],ebx
  0049954e  8b4c2414              mov       ecx,[esp+14h]
  00499552  56                    push      esi
  00499553  668b4130              mov       ax,[ecx+30h]
  00499557  0fbfd8                movsx     ebx,ax
  0049955a  66d1f8                sar       ax,1
  0049955d  66894130              mov       [ecx+30h],ax
  00499561  e81a060a00            call      00539B80h
  00499566  8b542418              mov       edx,[esp+18h]
  0049956a  83c408                add       esp,8
  0049956d  66895a30              mov       [edx+30h],bx
  00499571  8d5f1c                lea       ebx,[edi+1Ch]
  00499574  53                    push      ebx
  00499575  6800180000            push      1800h
  0049957a  6800030000            push      300h
  0049957f  6a03                  push      3
  00499581  e89a450a00            call      0053DB20h
  00499586  8b07                  mov       eax,[edi]
  00499588  83c410                add       esp,10h
  0049958b  a900000040            test      eax,40000000h
  00499590  0f8587030000          jne       near 0049991Dh
  00499596  0fbf4632              movsx     eax,word [esi+32h]
  0049959a  50                    push      eax
  0049959b  e87cc40a00            call      00545A1Ch
  004995a0  8d0440                lea       eax,[eax+eax*2]
  004995a3  8d0c80                lea       ecx,[eax+eax*4]
  004995a6  d1e1                  shl       ecx,1
  004995a8  894f50                mov       [edi+50h],ecx
  004995ab  0fbf5632              movsx     edx,word [esi+32h]
  004995af  52                    push      edx
  004995b0  e861c40a00            call      00545A16h
  004995b5  8d0440                lea       eax,[eax+eax*2]
  004995b8  8d0480                lea       eax,[eax+eax*4]
  004995bb  d1e0                  shl       eax,1
  004995bd  894758                mov       [edi+58h],eax
  004995c0  8b07                  mov       eax,[edi]
  004995c2  a801                  test      al,1
  004995c4  750b                  jne       short 004995D1h
  004995c6  0c01                  or        al,1
  004995c8  c7475400800100        mov       dword [edi+54h],18000h
  004995cf  8907                  mov       [edi],eax

LAB_004995d1:
  004995d1  8b4d00                mov       ecx,[ebp]
  004995d4  8b5620                mov       edx,[esi+20h]
  004995d7  8b4624                mov       eax,[esi+24h]
  004995da  894c2418              mov       [esp+18h],ecx
  004995de  8b0b                  mov       ecx,[ebx]
  004995e0  8954241c              mov       [esp+1Ch],edx
  004995e4  894d00                mov       [ebp],ecx
  004995e7  8b5720                mov       edx,[edi+20h]
  004995ea  6aff                  push      0FFFFFFFFh
  004995ec  89442424              mov       [esp+24h],eax
  004995f0  895620                mov       [esi+20h],edx
  004995f3  8b4724                mov       eax,[edi+24h]
  004995f6  6a00                  push      0
  004995f8  6a39                  push      39h
  004995fa  56                    push      esi
  004995fb  894624                mov       [esi+24h],eax
  004995fe  e8cd13feff            call      0047A9D0h
  00499603  8b4c2428              mov       ecx,[esp+28h]
  00499607  8b54242c              mov       edx,[esp+2Ch]
  0049960b  8b442430              mov       eax,[esp+30h]
  0049960f  83c410                add       esp,10h
  00499612  894d00                mov       [ebp],ecx
  00499615  895620                mov       [esi+20h],edx
  00499618  894624                mov       [esi+24h],eax
  0049961b  e9fd020000            jmp       0049991Dh
  00499620  0fbf4e10              movsx     ecx,word [esi+10h]
  00499624  83e900                sub       ecx,0
  00499627  0f8409010000          je        near 00499736h
  0049962d  49                    dec       ecx
  0049962e  0f8510010000          jne       near 00499744h
  00499634  a900000002            test      eax,2000000h
  00499639  0f8405010000          je        near 00499744h
  0049963f  8b7e44                mov       edi,[esi+44h]
  00499642  8b5e48                mov       ebx,[esi+48h]
  00499645  e8f6da0a00            call      00547140h
  0049964a  83e001                and       eax,1
  0049964d  c74648204e0000        mov       dword [esi+48h],4E20h
  00499654  894644                mov       [esi+44h],eax
  00499657  8b0dcc975e00          mov       ecx,[5E97CCh]
  0049965d  6a02                  push      2
  0049965f  6a00                  push      0
  00499661  8b91cc000000          mov       edx,[ecx+0CCh]
  00499667  33c9                  xor       ecx,ecx
  00499669  8b02                  mov       eax,[edx]
  0049966b  8a480e                mov       cl,[eax+0Eh]
  0049966e  81c1c0000000          add       ecx,0C0h
  00499674  51                    push      ecx
  00499675  56                    push      esi
  00499676  e85513feff            call      0047A9D0h
  0049967b  83c410                add       esp,10h
  0049967e  66ff4e10              dec       word [esi+10h]
  00499682  897e44                mov       [esi+44h],edi
  00499685  895e48                mov       [esi+48h],ebx
  00499688  c646430a              mov       byte [esi+43h],0Ah
  0049968c  c7465c00000000        mov       dword [esi+5Ch],0
  00499693  e985020000            jmp       0049991Dh
  00499698  0fbf4e10              movsx     ecx,word [esi+10h]
  0049969c  83e900                sub       ecx,0
  0049969f  0f8491000000          je        near 00499736h
  004996a5  49                    dec       ecx
  004996a6  0f8598000000          jne       near 00499744h
  004996ac  66837e1a07            cmp       word [esi+1Ah],7
  004996b1  7518                  jne       short 004996CBh
  004996b3  8d561c                lea       edx,[esi+1Ch]
  004996b6  52                    push      edx
  004996b7  6800180000            push      1800h
  004996bc  6800010000            push      100h
  004996c1  6a02                  push      2
  004996c3  e858440a00            call      0053DB20h
  004996c8  83c410                add       esp,10h

LAB_004996cb:
  004996cb  f70600000002          test      dword [esi],2000000h
  004996d1  7471                  je        short 00499744h
  004996d3  6a00                  push      0
  004996d5  56                    push      esi
  004996d6  e895c00900            call      00535770h
  004996db  0fbf08                movsx     ecx,word [eax]
  004996de  c1e10c                shl       ecx,0Ch
  004996e1  894e44                mov       [esi+44h],ecx
  004996e4  83c408                add       esp,8
  004996e7  0fbf5002              movsx     edx,word [eax+2]
  004996eb  c1e20c                shl       edx,0Ch
  004996ee  895648                mov       [esi+48h],edx
  004996f1  6a03                  push      3
  004996f3  0fbf4004              movsx     eax,word [eax+4]
  004996f7  c1e00c                shl       eax,0Ch
  004996fa  89464c                mov       [esi+4Ch],eax
  004996fd  8b0dcc975e00          mov       ecx,[5E97CCh]
  00499703  6a00                  push      0
  00499705  8b91cc000000          mov       edx,[ecx+0CCh]
  0049970b  33c9                  xor       ecx,ecx
  0049970d  8b02                  mov       eax,[edx]
  0049970f  8a4813                mov       cl,[eax+13h]
  00499712  81c1c0000000          add       ecx,0C0h
  00499718  51                    push      ecx
  00499719  56                    push      esi
  0049971a  e8b112feff            call      0047A9D0h
  0049971f  83c410                add       esp,10h
  00499722  66ff4e10              dec       word [esi+10h]
  00499726  c6464328              mov       byte [esi+43h],28h
  0049972a  c7465c00000000        mov       dword [esi+5Ch],0
  00499731  e9e7010000            jmp       0049991Dh

LAB_00499736:
  00499736  8a4e43                mov       cl,[esi+43h]
  00499739  fec9                  dec       cl
  0049973b  884e43                mov       [esi+43h],cl
  0049973e  7504                  jne       short 00499744h
  00499740  24fd                  and       al,0FDh
  00499742  8906                  mov       [esi],eax

LAB_00499744:
  00499744  c7465c00000000        mov       dword [esi+5Ch],0
  0049974b  e9cd010000            jmp       0049991Dh

LAB_00499750:
  00499750  a810                  test      al,10h
  00499752  0f85c5010000          jne       near 0049991Dh
  00499758  a904080000            test      eax,804h
  0049975d  0f844e010000          je        near 004998B1h
  00499763  f744242ce8030040      test      dword [esp+2Ch],400003E8h
  0049976b  740b                  je        short 00499778h
  0049976d  25fbf7ffff            and       eax,0FFFFF7FBh
  00499772  c6464300              mov       byte [esi+43h],0
  00499776  8906                  mov       [esi],eax

LAB_00499778:
  00499778  668b4610              mov       ax,[esi+10h]
  0049977c  0fbfc8                movsx     ecx,ax
  0049977f  83f903                cmp       ecx,3
  00499782  0f8795010000          ja        near 0049991Dh
  00499788  ff248dc09d4900        jmp       dword [ecx*4+499DC0h]
  0049978f  8b06                  mov       eax,[esi]
  00499791  a900000002            test      eax,2000000h
  00499796  0f8481010000          je        near 0049991Dh
  0049979c  80e4f7                and       ah,0F7h
  0049979f  8906                  mov       [esi],eax
  004997a1  e977010000            jmp       0049991Dh
  004997a6  b807000000            mov       eax,7
  004997ab  66394618              cmp       [esi+18h],ax
  004997af  7411                  je        short 004997C2h
  004997b1  894618                mov       [esi+18h],eax
  004997b4  8b06                  mov       eax,[esi]
  004997b6  25fbfffffe            and       eax,0FEFFFFFBh
  004997bb  8906                  mov       [esi],eax
  004997bd  e95b010000            jmp       0049991Dh

LAB_004997c2:
  004997c2  66837e1a13            cmp       word [esi+1Ah],13h
  004997c7  0f8550010000          jne       near 0049991Dh
  004997cd  33c0                  xor       eax,eax
  004997cf  6aff                  push      0FFFFFFFFh
  004997d1  668b4606              mov       ax,[esi+6]
  004997d5  6aff                  push      0FFFFFFFFh
  004997d7  8bd0                  mov       edx,eax
  004997d9  c1f808                sar       eax,8
  004997dc  81e2ff000000          and       edx,0FFh
  004997e2  25ff000000            and       eax,0FFh
  004997e7  52                    push      edx
  004997e8  50                    push      eax
  004997e9  56                    push      esi
  004997ea  e891110a00            call      0053A980h
  004997ef  83c414                add       esp,14h
  004997f2  66ff4e10              dec       word [esi+10h]
  004997f6  e922010000            jmp       0049991Dh
  004997fb  8a4e43                mov       cl,[esi+43h]
  004997fe  fec9                  dec       cl
  00499800  884e43                mov       [esi+43h],cl
  00499803  7505                  jne       short 0049980Ah
  00499805  48                    dec       eax
  00499806  66894610              mov       [esi+10h],ax

LAB_0049980a:
  0049980a  8b442428              mov       eax,[esp+28h]
  0049980e  8b4c243c              mov       ecx,[esp+3Ch]
  00499812  8d048532000000        lea       eax,[eax*4+32h]
  00499819  8d1408                lea       edx,[eax+ecx]
  0049981c  8d3c00                lea       edi,[eax+eax]
  0049981f  81e2ff0f0000          and       edx,0FFFh
  00499825  3bd7                  cmp       edx,edi
  00499827  7f09                  jg        short 00499832h
  00499829  66014e32              add       [esi+32h],cx
  0049982d  e9eb000000            jmp       0049991Dh

LAB_00499832:
  00499832  81e1ff0f0000          and       ecx,0FFFh
  00499838  81f900080000          cmp       ecx,800h
  0049983e  7c02                  jl        short 00499842h
  00499840  f7d8                  neg       eax

LAB_00499842:
  00499842  66014632              add       [esi+32h],ax
  00499846  e9d2000000            jmp       0049991Dh
  0049984b  6a00                  push      0
  0049984d  6a00                  push      0
  0049984f  68bd000000            push      0BDh
  00499854  56                    push      esi
  00499855  e88612feff            call      0047AAE0h
  0049985a  83c410                add       esp,10h
  0049985d  e8ded80a00            call      00547140h
  00499862  99                    cdq
  00499863  b903000000            mov       ecx,3
  00499868  f7f9                  idiv      ecx
  0049986a  33c0                  xor       eax,eax
  0049986c  668b045558295500      mov       ax,[edx*2+552958h]
  00499874  66894606              mov       [esi+6],ax
  00499878  8b0dc4975e00          mov       ecx,[5E97C4h]
  0049987e  8bd0                  mov       edx,eax
  00499880  25ff000000            and       eax,0FFh
  00499885  8b899c000000          mov       ecx,[ecx+9Ch]
  0049988b  c1fa08                sar       edx,8
  0049988e  81e2ff000000          and       edx,0FFh
  00499894  8d0440                lea       eax,[eax+eax*2]
  00499897  c1e004                shl       eax,4
  0049989a  8b0c91                mov       ecx,[ecx+edx*4]
  0049989d  8a540113              mov       dl,[ecx+eax+13h]
  004998a1  66ff4e10              dec       word [esi+10h]
  004998a5  885643                mov       [esi+43h],dl
  004998a8  c7465c00000000        mov       dword [esi+5Ch],0
  004998af  eb6c                  jmp       short 0049991Dh

LAB_004998b1:
  004998b1  a801                  test      al,1
  004998b3  7568                  jne       short 0049991Dh
  004998b5  33c9                  xor       ecx,ecx
  004998b7  8a4e42                mov       cl,[esi+42h]
  004998ba  83f903                cmp       ecx,3
  004998bd  772b                  ja        short 004998EAh
  004998bf  ff248dd09d4900        jmp       dword [ecx*4+499DD0h]
  004998c6  33c9                  xor       ecx,ecx
  004998c8  66394e18              cmp       [esi+18h],cx
  004998cc  740a                  je        short 004998D8h
  004998ce  0d00000001            or        eax,1000000h
  004998d3  894e18                mov       [esi+18h],ecx
  004998d6  8906                  mov       [esi],eax

LAB_004998d8:
  004998d8  817c24142c010000      cmp       dword [esp+14h],12Ch
  004998e0  0f8d2c030000          jge       near 00499C12h
  004998e6  c6464300              mov       byte [esi+43h],0

LAB_004998ea:
  004998ea  bf01000000            mov       edi,1

LAB_004998ef:
  004998ef  66397e18              cmp       [esi+18h],di
  004998f3  7528                  jne       short 0049991Dh
  004998f5  668b461a              mov       ax,[esi+1Ah]
  004998f9  663d0f00              cmp       ax,0Fh
  004998fd  7406                  je        short 00499905h
  004998ff  663d2200              cmp       ax,22h
  00499903  7518                  jne       short 0049991Dh

LAB_00499905:
  00499905  8d561c                lea       edx,[esi+1Ch]
  00499908  52                    push      edx
  00499909  6800180000            push      1800h
  0049990e  6800020000            push      200h
  00499913  6a45                  push      45h
  00499915  e806420a00            call      0053DB20h
  0049991a  83c410                add       esp,10h

LAB_0049991d:
  0049991d  8b442424              mov       eax,[esp+24h]
  00499921  66c7869a000000ff7f    mov       word [esi+9Ah],7FFFh
  0049992a  50                    push      eax
  0049992b  56                    push      esi
  0049992c  e82fe8feff            call      00488160h
  00499931  83c408                add       esp,8
  00499934  56                    push      esi
  00499935  e806a00900            call      00533940h
  0049993a  83c404                add       esp,4
  0049993d  56                    push      esi
  0049993e  e8ad610400            call      004DFAF0h
  00499943  8b4c2434              mov       ecx,[esp+34h]
  00499947  83c404                add       esp,4
  0049994a  51                    push      ecx
  0049994b  56                    push      esi
  0049994c  e81f9e0400            call      004E3770h
  00499951  8b44242c              mov       eax,[esp+2Ch]
  00499955  33db                  xor       ebx,ebx
  00499957  83c408                add       esp,8
  0049995a  3bc3                  cmp       eax,ebx
  0049995c  0f84a8020000          je        near 00499C0Ah
  00499962  f70617080000          test      dword [esi],817h
  00499968  0f859c020000          jne       near 00499C0Ah
  0049996e  8a4643                mov       al,[esi+43h]
  00499971  84c0                  test      al,al
  00499973  0f8591020000          jne       near 00499C0Ah
  00499979  8b466c                mov       eax,[esi+6Ch]
  0049997c  8b561c                mov       edx,[esi+1Ch]
  0049997f  8b781c                mov       edi,[eax+1Ch]
  00499982  8b6824                mov       ebp,[eax+24h]
  00499985  8b4624                mov       eax,[esi+24h]
  00499988  2bfa                  sub       edi,edx
  0049998a  2be8                  sub       ebp,eax
  0049998c  c1ff0c                sar       edi,0Ch
  0049998f  c1fd0c                sar       ebp,0Ch
  00499992  8bd7                  mov       edx,edi
  00499994  8bc5                  mov       eax,ebp
  00499996  0fafd7                imul      edx,edi
  00499999  0fafc5                imul      eax,ebp
  0049999c  03d0                  add       edx,eax
  0049999e  52                    push      edx
  0049999f  e86cc00a00            call      00545A10h
  004999a4  55                    push      ebp
  004999a5  57                    push      edi
  004999a6  8944241c              mov       [esp+1Ch],eax
  004999aa  e873c00a00            call      00545A22h
  004999af  668b4e32              mov       cx,[esi+32h]
  004999b3  2bc1                  sub       eax,ecx
  004999b5  25ff0f0000            and       eax,0FFFh
  004999ba  8944243c              mov       [esp+3Ch],eax
  004999be  8b442414              mov       eax,[esp+14h]
  004999c2  3df4010000            cmp       eax,1F4h
  004999c7  0f8da3000000          jge       near 00499A70h
  004999cd  e86ed70a00            call      00547140h
  004999d2  99                    cdq
  004999d3  b964000000            mov       ecx,64h
  004999d8  f7f9                  idiv      ecx
  004999da  83fa32                cmp       edx,32h
  004999dd  7d63                  jge       short 00499A42h
  004999df  8b44243c              mov       eax,[esp+3Ch]
  004999e3  0500020000            add       eax,200h
  004999e8  25ff0f0000            and       eax,0FFFh
  004999ed  3d00040000            cmp       eax,400h
  004999f2  0f8f9a010000          jg        near 00499B92h
  004999f8  f6c201                test      dl,1
  004999fb  7422                  je        short 00499A1Fh
  004999fd  8b0e                  mov       ecx,[esi]
  004999ff  c7461803000000        mov       dword [esi+18h],3
  00499a06  81e1fffffffe          and       ecx,0FEFFFFFFh
  00499a0c  885e42                mov       [esi+42h],bl
  00499a0f  83c902                or        ecx,2
  00499a12  895e5c                mov       [esi+5Ch],ebx
  00499a15  890e                  mov       [esi],ecx
  00499a17  5f                    pop       edi
  00499a18  5e                    pop       esi
  00499a19  5d                    pop       ebp
  00499a1a  5b                    pop       ebx
  00499a1b  83c428                add       esp,28h
  00499a1e  c3                    ret

LAB_00499a1f:
  00499a1f  8b16                  mov       edx,[esi]
  00499a21  c7461809000000        mov       dword [esi+18h],9
  00499a28  81e2fffffffe          and       edx,0FEFFFFFFh
  00499a2e  c6464201              mov       byte [esi+42h],1
  00499a32  83ca02                or        edx,2
  00499a35  895e5c                mov       [esi+5Ch],ebx
  00499a38  8916                  mov       [esi],edx
  00499a3a  5f                    pop       edi
  00499a3b  5e                    pop       esi
  00499a3c  5d                    pop       ebp
  00499a3d  5b                    pop       ebx
  00499a3e  83c428                add       esp,28h
  00499a41  c3                    ret

LAB_00499a42:
  00499a42  83fa4b                cmp       edx,4Bh
  00499a45  0f8d5c010000          jge       near 00499BA7h
  00499a4b  c6464202              mov       byte [esi+42h],2
  00499a4f  e8ecd60a00            call      00547140h
  00499a54  99                    cdq
  00499a55  b90a000000            mov       ecx,0Ah
  00499a5a  66c74610c800          mov       word [esi+10h],0C8h
  00499a60  f7f9                  idiv      ecx
  00499a62  80c214                add       dl,14h
  00499a65  885643                mov       [esi+43h],dl
  00499a68  5f                    pop       edi
  00499a69  5e                    pop       esi
  00499a6a  5d                    pop       ebp
  00499a6b  5b                    pop       ebx
  00499a6c  83c428                add       esp,28h
  00499a6f  c3                    ret

LAB_00499a70:
  00499a70  e8cbd60a00            call      00547140h
  00499a75  99                    cdq
  00499a76  b964000000            mov       ecx,64h
  00499a7b  f7f9                  idiv      ecx
  00499a7d  83fa1e                cmp       edx,1Eh
  00499a80  0f8da7000000          jge       near 00499B2Dh
  00499a86  8b54243c              mov       edx,[esp+3Ch]
  00499a8a  81c200020000          add       edx,200h
  00499a90  81e2ff0f0000          and       edx,0FFFh
  00499a96  81fa00040000          cmp       edx,400h
  00499a9c  0f8df0000000          jge       near 00499B92h
  00499aa2  e899d60a00            call      00547140h
  00499aa7  99                    cdq
  00499aa8  b903000000            mov       ecx,3
  00499aad  f7f9                  idiv      ecx
  00499aaf  2bd3                  sub       edx,ebx
  00499ab1  7454                  je        short 00499B07h
  00499ab3  4a                    dec       edx
  00499ab4  742d                  je        short 00499AE3h
  00499ab6  4a                    dec       edx
  00499ab7  0f854d010000          jne       near 00499C0Ah
  00499abd  8b16                  mov       edx,[esi]
  00499abf  c7461808000000        mov       dword [esi+18h],8
  00499ac6  81e2fffffffe          and       edx,0FEFFFFFFh
  00499acc  c6464204              mov       byte [esi+42h],4
  00499ad0  83ca02                or        edx,2
  00499ad3  66c746100100          mov       word [esi+10h],1
  00499ad9  8916                  mov       [esi],edx
  00499adb  5f                    pop       edi
  00499adc  5e                    pop       esi
  00499add  5d                    pop       ebp
  00499ade  5b                    pop       ebx
  00499adf  83c428                add       esp,28h
  00499ae2  c3                    ret

LAB_00499ae3:
  00499ae3  8b06                  mov       eax,[esi]
  00499ae5  c7461805000000        mov       dword [esi+18h],5
  00499aec  25fffffffe            and       eax,0FEFFFFFFh
  00499af1  c6464203              mov       byte [esi+42h],3
  00499af5  0c02                  or        al,2
  00499af7  66c746100100          mov       word [esi+10h],1
  00499afd  8906                  mov       [esi],eax
  00499aff  5f                    pop       edi
  00499b00  5e                    pop       esi
  00499b01  5d                    pop       ebp
  00499b02  5b                    pop       ebx
  00499b03  83c428                add       esp,28h
  00499b06  c3                    ret

LAB_00499b07:
  00499b07  8b0e                  mov       ecx,[esi]
  00499b09  c7461804000000        mov       dword [esi+18h],4
  00499b10  81e1fffffffe          and       ecx,0FEFFFFFFh
  00499b16  c6464202              mov       byte [esi+42h],2
  00499b1a  83c902                or        ecx,2
  00499b1d  66c746100100          mov       word [esi+10h],1
  00499b23  890e                  mov       [esi],ecx
  00499b25  5f                    pop       edi
  00499b26  5e                    pop       esi
  00499b27  5d                    pop       ebp
  00499b28  5b                    pop       ebx
  00499b29  83c428                add       esp,28h
  00499b2c  c3                    ret

LAB_00499b2d:
  00499b2d  83fa32                cmp       edx,32h
  00499b30  7d25                  jge       short 00499B57h
  00499b32  c6464202              mov       byte [esi+42h],2
  00499b36  e805d60a00            call      00547140h
  00499b3b  99                    cdq
  00499b3c  b90a000000            mov       ecx,0Ah
  00499b41  66c74610c800          mov       word [esi+10h],0C8h
  00499b47  f7f9                  idiv      ecx
  00499b49  80c214                add       dl,14h
  00499b4c  885643                mov       [esi+43h],dl
  00499b4f  5f                    pop       edi
  00499b50  5e                    pop       esi
  00499b51  5d                    pop       ebp
  00499b52  5b                    pop       ebx
  00499b53  83c428                add       esp,28h
  00499b56  c3                    ret

LAB_00499b57:
  00499b57  83fa46                cmp       edx,46h
  00499b5a  7d46                  jge       short 00499BA2h
  00499b5c  8b54243c              mov       edx,[esp+3Ch]
  00499b60  81c200020000          add       edx,200h
  00499b66  81e2ff0f0000          and       edx,0FFFh
  00499b6c  81fa00040000          cmp       edx,400h
  00499b72  7d1e                  jge       short 00499B92h
  00499b74  8b06                  mov       eax,[esi]
  00499b76  c7461806000000        mov       dword [esi+18h],6
  00499b7d  0d04080001            or        eax,1000804h
  00499b82  66c746100300          mov       word [esi+10h],3
  00499b88  8906                  mov       [esi],eax
  00499b8a  5f                    pop       edi
  00499b8b  5e                    pop       esi
  00499b8c  5d                    pop       ebp
  00499b8d  5b                    pop       ebx
  00499b8e  83c428                add       esp,28h
  00499b91  c3                    ret

LAB_00499b92:
  00499b92  c6464201              mov       byte [esi+42h],1
  00499b96  c6464301              mov       byte [esi+43h],1
  00499b9a  5f                    pop       edi
  00499b9b  5e                    pop       esi
  00499b9c  5d                    pop       ebp
  00499b9d  5b                    pop       ebx
  00499b9e  83c428                add       esp,28h
  00499ba1  c3                    ret

LAB_00499ba2:
  00499ba2  83fa5a                cmp       edx,5Ah
  00499ba5  7d4c                  jge       short 00499BF3h

LAB_00499ba7:
  00499ba7  c6464203              mov       byte [esi+42h],3
  00499bab  e890d50a00            call      00547140h
  00499bb0  99                    cdq
  00499bb1  b90a000000            mov       ecx,0Ah
  00499bb6  f7f9                  idiv      ecx
  00499bb8  80c214                add       dl,14h
  00499bbb  885643                mov       [esi+43h],dl
  00499bbe  8b542428              mov       edx,[esp+28h]
  00499bc2  8dba80000000          lea       edi,[edx+80h]
  00499bc8  c1e704                shl       edi,4
  00499bcb  e870d50a00            call      00547140h
  00499bd0  99                    cdq
  00499bd1  f7ff                  idiv      edi
  00499bd3  d1ff                  sar       edi,1
  00499bd5  2bd7                  sub       edx,edi
  00499bd7  8b7c243c              mov       edi,[esp+3Ch]
  00499bdb  03d7                  add       edx,edi
  00499bdd  66035632              add       dx,[esi+32h]
  00499be1  81c200080000          add       edx,800h
  00499be7  66895610              mov       [esi+10h],dx
  00499beb  5f                    pop       edi
  00499bec  5e                    pop       esi
  00499bed  5d                    pop       ebp
  00499bee  5b                    pop       ebx
  00499bef  83c428                add       esp,28h
  00499bf2  c3                    ret

LAB_00499bf3:
  00499bf3  c6464200              mov       byte [esi+42h],0
  00499bf7  e844d50a00            call      00547140h
  00499bfc  99                    cdq
  00499bfd  b90a000000            mov       ecx,0Ah
  00499c02  f7f9                  idiv      ecx
  00499c04  80c214                add       dl,14h
  00499c07  885643                mov       [esi+43h],dl

LAB_00499c0a:
  00499c0a  5f                    pop       edi
  00499c0b  5e                    pop       esi
  00499c0c  5d                    pop       ebp
  00499c0d  5b                    pop       ebx
  00499c0e  83c428                add       esp,28h
  00499c11  c3                    ret

LAB_00499c12:
  00499c12  8a4643                mov       al,[esi+43h]
  00499c15  84c0                  test      al,al
  00499c17  7405                  je        short 00499C1Eh
  00499c19  fec8                  dec       al
  00499c1b  884643                mov       [esi+43h],al

LAB_00499c1e:
  00499c1e  894e5c                mov       [esi+5Ch],ecx
  00499c21  e9c4fcffff            jmp       004998EAh
  00499c26  bf01000000            mov       edi,1
  00499c2b  66397e18              cmp       [esi+18h],di
  00499c2f  740a                  je        short 00499C3Bh
  00499c31  0d00000001            or        eax,1000000h
  00499c36  897e18                mov       [esi+18h],edi
  00499c39  8906                  mov       [esi],eax

LAB_00499c3b:
  00499c3b  8b44243c              mov       eax,[esp+3Ch]
  00499c3f  8d8880000000          lea       ecx,[eax+80h]
  00499c45  81e1ff0f0000          and       ecx,0FFFh
  00499c4b  81f900010000          cmp       ecx,100h
  00499c51  7f14                  jg        short 00499C67h
  00499c53  66014632              add       [esi+32h],ax
  00499c57  c6464300              mov       byte [esi+43h],0
  00499c5b  c7465c00000000        mov       dword [esi+5Ch],0
  00499c62  e988fcffff            jmp       004998EFh

LAB_00499c67:
  00499c67  25ff0f0000            and       eax,0FFFh
  00499c6c  33d2                  xor       edx,edx
  00499c6e  3d00080000            cmp       eax,800h
  00499c73  c7465c00000000        mov       dword [esi+5Ch],0
  00499c7a  0f9dc2                setge     dl
  00499c7d  4a                    dec       edx
  00499c7e  81e200010000          and       edx,100h
  00499c84  83c280                add       edx,0FFFFFF80h
  00499c87  8bc2                  mov       eax,edx
  00499c89  66014632              add       [esi+32h],ax
  00499c8d  e95dfcffff            jmp       004998EFh
  00499c92  bf01000000            mov       edi,1
  00499c97  66397e18              cmp       [esi+18h],di
  00499c9b  740a                  je        short 00499CA7h
  00499c9d  0d00000001            or        eax,1000000h
  00499ca2  897e18                mov       [esi+18h],edi
  00499ca5  8906                  mov       [esi],eax

LAB_00499ca7:
  00499ca7  0fbf4610              movsx     eax,word [esi+10h]
  00499cab  39442414              cmp       [esp+14h],eax
  00499caf  7d09                  jge       short 00499CBAh
  00499cb1  c6464300              mov       byte [esi+43h],0
  00499cb5  e935fcffff            jmp       004998EFh

LAB_00499cba:
  00499cba  8a4643                mov       al,[esi+43h]
  00499cbd  84c0                  test      al,al
  00499cbf  7405                  je        short 00499CC6h
  00499cc1  fec8                  dec       al
  00499cc3  884643                mov       [esi+43h],al

LAB_00499cc6:
  00499cc6  8b44243c              mov       eax,[esp+3Ch]
  00499cca  8d8880000000          lea       ecx,[eax+80h]
  00499cd0  81e1ff0f0000          and       ecx,0FFFh
  00499cd6  81f900010000          cmp       ecx,100h
  00499cdc  7e1b                  jle       short 00499CF9h
  00499cde  25ff0f0000            and       eax,0FFFh
  00499ce3  33d2                  xor       edx,edx
  00499ce5  3d00080000            cmp       eax,800h
  00499cea  0f9dc2                setge     dl
  00499ced  4a                    dec       edx
  00499cee  81e200010000          and       edx,100h
  00499cf4  83c280                add       edx,0FFFFFF80h
  00499cf7  8bc2                  mov       eax,edx

LAB_00499cf9:
  00499cf9  66014632              add       [esi+32h],ax
  00499cfd  c7465c00400100        mov       dword [esi+5Ch],14000h
  00499d04  e9e6fbffff            jmp       004998EFh
  00499d09  8a4e43                mov       cl,[esi+43h]
  00499d0c  84c9                  test      cl,cl
  00499d0e  7405                  je        short 00499D15h
  00499d10  fec9                  dec       cl
  00499d12  884e43                mov       [esi+43h],cl

LAB_00499d15:
  00499d15  bf01000000            mov       edi,1
  00499d1a  66397e18              cmp       [esi+18h],di
  00499d1e  740a                  je        short 00499D2Ah
  00499d20  0d00000001            or        eax,1000000h
  00499d25  897e18                mov       [esi+18h],edi
  00499d28  8906                  mov       [esi],eax

LAB_00499d2a:
  00499d2a  0fbf4610              movsx     eax,word [esi+10h]
  00499d2e  50                    push      eax
  00499d2f  c7465c00000100        mov       dword [esi+5Ch],10000h
  00499d36  e8e1bc0a00            call      00545A1Ch
  00499d3b  0faf465c              imul      eax,[esi+5Ch]
  00499d3f  0fbf4e10              movsx     ecx,word [esi+10h]
  00499d43  8b5e1c                mov       ebx,[esi+1Ch]
  00499d46  51                    push      ecx
  00499d47  c1f80c                sar       eax,0Ch
  00499d4a  03d8                  add       ebx,eax
  00499d4c  895e1c                mov       [esi+1Ch],ebx
  00499d4f  e8c2bc0a00            call      00545A16h
  00499d54  0faf465c              imul      eax,[esi+5Ch]
  00499d58  8b6e24                mov       ebp,[esi+24h]
  00499d5b  c7465c00000000        mov       dword [esi+5Ch],0
  00499d62  c1f80c                sar       eax,0Ch
  00499d65  03e8                  add       ebp,eax
  00499d67  8b44243c              mov       eax,[esp+3Ch]
  00499d6b  896e24                mov       [esi+24h],ebp
  00499d6e  8d5060                lea       edx,[eax+60h]
  00499d71  81e2ff0f0000          and       edx,0FFFh
  00499d77  81fac0000000          cmp       edx,0C0h
  00499d7d  7f09                  jg        short 00499D88h
  00499d7f  66014632              add       [esi+32h],ax
  00499d83  e967fbffff            jmp       004998EFh

LAB_00499d88:
  00499d88  25ff0f0000            and       eax,0FFFh
  00499d8d  33c9                  xor       ecx,ecx
  00499d8f  3d00080000            cmp       eax,800h
  00499d94  0f9dc1                setge     cl
  00499d97  49                    dec       ecx
  00499d98  81e1c0000000          and       ecx,0C0h
  00499d9e  83c1a0                add       ecx,0FFFFFFA0h
  00499da1  8bc1                  mov       eax,ecx
  00499da3  66014632              add       [esi+32h],ax
  00499da7  e943fbffff            jmp       004998EFh
  00499dac  d39149009193          rcl       dword [ecx-6C6EFFB7h],cl
  00499db2  49                    dec       ecx
  00499db3  008894490020          add       [eax+20004994h],cl
  00499db9  96                    xchg      esi,eax
  00499dba  49                    dec       ecx
  00499dbb  00989649008f          add       [eax-70FFB66Ah],bl
  00499dc1  97                    xchg      edi,eax
  00499dc2  49                    dec       ecx
  00499dc3  00a6974900fb          add       [esi-4FFB669h],ah
  00499dc9  97                    xchg      edi,eax
  00499dca  49                    dec       ecx
  00499dcb  004b98                add       [ebx-68h],cl
  00499dce  49                    dec       ecx
  00499dcf  00c6                  add       dh,al
  00499dd1  98                    cwde
  00499dd2  49                    dec       ecx
  00499dd3  0026                  add       [esi],ah
  00499dd5  9c                    pushf
  00499dd6  49                    dec       ecx
  00499dd7  00929c490009          add       [edx+900499Ch],dl
  00499ddd  9d                    popf
  00499dde  49                    dec       ecx
  00499ddf  00                    (bad)
  (octet invalide — la plage demandee deborde probablement du code)
