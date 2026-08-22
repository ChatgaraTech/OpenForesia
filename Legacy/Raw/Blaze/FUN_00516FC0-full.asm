Legacy/Binaries/Blaze.exe  base 0x00400000
0x00516fc0  section .text  offset fichier 0x1163c0  10256 octets

  00516fc0  a1a8335600            mov       eax,[5633A8h]
  00516fc5  83ec60                sub       esp,60h
  00516fc8  53                    push      ebx
  00516fc9  33db                  xor       ebx,ebx
  00516fcb  55                    push      ebp
  00516fcc  56                    push      esi
  00516fcd  3bc3                  cmp       eax,ebx
  00516fcf  57                    push      edi
  00516fd0  7522                  jne       short 00516FF4h
  00516fd2  a028e55d00            mov       al,[5DE528h]
  00516fd7  8a0d2ce55d00          mov       cl,[5DE52Ch]
  00516fdd  8a1520905e00          mov       dl,[5E9020h]
  00516fe3  a214bc5d00            mov       [5DBC14h],al
  00516fe8  880dd0df5d00          mov       [5DDFD0h],cl
  00516fee  881518bc5d00          mov       [5DBC18h],dl

LAB_00516ff4:
  00516ff4  8d442410              lea       eax,[esp+10h]
  00516ff8  8d4c2420              lea       ecx,[esp+20h]
  00516ffc  50                    push      eax
  00516ffd  8d542418              lea       edx,[esp+18h]
  00517001  51                    push      ecx
  00517002  52                    push      edx
  00517003  e8d8310100            call      0052A1E0h
  00517008  8b15b4876000          mov       edx,[6087B4h]
  0051700e  c605906c560000        mov       byte [566C90h],0
  00517015  891df02c5f00          mov       [5F2CF0h],ebx
  0051701b  891d2ce05d00          mov       [5DE02Ch],ebx
  00517021  c605e49a5e0000        mov       byte [5E9AE4h],0
  00517028  891da8e05d00          mov       [5DE0A8h],ebx
  0051702e  891dace05d00          mov       [5DE0ACh],ebx
  00517034  891db0e05d00          mov       [5DE0B0h],ebx
  0051703a  891d24e05d00          mov       [5DE024h],ebx
  00517040  891d28e05d00          mov       [5DE028h],ebx
  00517046  8b3a                  mov       edi,[edx]
  00517048  b900040000            mov       ecx,400h
  0051704d  33c0                  xor       eax,eax
  0051704f  81c700501c00          add       edi,1C5000h
  00517055  f3ab                  rep stosd
  00517057  8b15b4876000          mov       edx,[6087B4h]
  0051705d  b900040000            mov       ecx,400h
  00517062  83c40c                add       esp,0Ch
  00517065  8b3a                  mov       edi,[edx]
  00517067  81c700401c00          add       edi,1C4000h
  0051706d  f3ab                  rep stosd
  0051706f  a034e55d00            mov       al,[5DE534h]
  00517074  8a0d8c6c5600          mov       cl,[566C8Ch]
  0051707a  3ac8                  cmp       cl,al
  0051707c  7412                  je        short 00517090h
  0051707e  33c9                  xor       ecx,ecx
  00517080  a28c6c5600            mov       [566C8Ch],al
  00517085  8ac8                  mov       cl,al
  00517087  51                    push      ecx
  00517088  e8432a0100            call      00529AD0h
  0051708d  83c404                add       esp,4

LAB_00517090:
  00517090  33c0                  xor       eax,eax
  00517092  a038e55d00            mov       al,[5DE538h]
  00517097  8d1480                lea       edx,[eax+eax*4]
  0051709a  c1e203                shl       edx,3
  0051709d  2bd0                  sub       edx,eax
  0051709f  a1f8455600            mov       eax,[5645F8h]
  005170a4  25ff000000            and       eax,0FFh
  005170a9  48                    dec       eax
  005170aa  8d3495c0935e00        lea       esi,[edx*4+5E93C0h]
  005170b1  7438                  je        short 005170EBh
  005170b3  48                    dec       eax
  005170b4  7551                  jne       short 00517107h
  005170b6  e8c59feeff            call      00401080h
  005170bb  a0946c5600            mov       al,[566C94h]
  005170c0  84c0                  test      al,al
  005170c2  740c                  je        short 005170D0h
  005170c4  c605946c560000        mov       byte [566C94h],0
  005170cb  e8309feeff            call      00401000h

LAB_005170d0:
  005170d0  a148e05d00            mov       eax,[5DE048h]
  005170d5  8b0dcc975e00          mov       ecx,[5E97CCh]
  005170db  25ffff0000            and       eax,0FFFFh
  005170e0  50                    push      eax
  005170e1  56                    push      esi
  005170e2  51                    push      ecx
  005170e3  e8e8ccffff            call      00513DD0h
  005170e8  83c40c                add       esp,0Ch

LAB_005170eb:
  005170eb  8b154ce05d00          mov       edx,[5DE04Ch]
  005170f1  a1cc975e00            mov       eax,[5E97CCh]
  005170f6  81e2ffff0000          and       edx,0FFFFh
  005170fc  52                    push      edx
  005170fd  56                    push      esi
  005170fe  50                    push      eax
  005170ff  e8ccccffff            call      00513DD0h
  00517104  83c40c                add       esp,0Ch

LAB_00517107:
  00517107  a060a55f00            mov       al,[5FA560h]
  0051710c  b901000000            mov       ecx,1
  00517111  3ac1                  cmp       al,cl
  00517113  723e                  jb        short 00517153h

LAB_00517115:
  00517115  a1cc975e00            mov       eax,[5E97CCh]
  0051711a  33d2                  xor       edx,edx
  0051711c  8a9160a55f00          mov       dl,[ecx+5FA560h]
  00517122  8b8088000000          mov       eax,[eax+88h]
  00517128  8b0490                mov       eax,[eax+edx*4]
  0051712b  3bc3                  cmp       eax,ebx
  0051712d  7413                  je        short 00517142h
  0051712f  8338ff                cmp       dword [eax],0FFFFFFFFh
  00517132  740e                  je        short 00517142h

LAB_00517134:
  00517134  c60000                mov       byte [eax],0
  00517137  8b5010                mov       edx,[eax+10h]
  0051713a  83c010                add       eax,10h
  0051713d  83faff                cmp       edx,0FFFFFFFFh
  00517140  75f2                  jne       short 00517134h

LAB_00517142:
  00517142  8b1560a55f00          mov       edx,[5FA560h]
  00517148  41                    inc       ecx
  00517149  81e2ff000000          and       edx,0FFh
  0051714f  3bca                  cmp       ecx,edx
  00517151  7ec2                  jle       short 00517115h

LAB_00517153:
  00517153  6890010000            push      190h
  00517158  e84fe90200            call      00545AACh
  0051715d  53                    push      ebx
  0051715e  e8add9feff            call      00504B10h
  00517163  83c404                add       esp,4
  00517166  bf286c5600            mov       edi,566C28h
  0051716b  bb38000000            mov       ebx,38h
  00517170  bd306c5600            mov       ebp,566C30h

LAB_00517175:
  00517175  33c0                  xor       eax,eax
  00517177  83ceff                or        esi,0FFFFFFFFh
  0051717a  a034e55d00            mov       al,[5DE534h]
  0051717f  50                    push      eax
  00517180  e87b34efff            call      0040A600h
  00517185  8b0dc4975e00          mov       ecx,[5E97C4h]
  0051718b  83c404                add       esp,4
  0051718e  51                    push      ecx
  0051718f  e86cbe0100            call      00533000h
  00517194  83c404                add       esp,4
  00517197  e8946a0200            call      0053DC30h
  0051719c  e80f71ffff            call      0050E2B0h
  005171a1  33c0                  xor       eax,eax
  005171a3  a038e55d00            mov       al,[5DE538h]
  005171a8  8d1480                lea       edx,[eax+eax*4]
  005171ab  c1e203                shl       edx,3
  005171ae  2bd0                  sub       edx,eax
  005171b0  8d0c95c0935e00        lea       ecx,[edx*4+5E93C0h]
  005171b7  8b1495c0935e00        mov       edx,[edx*4+5E93C0h]
  005171be  f7c200000100          test      edx,10000h
  005171c4  750e                  jne       short 005171D4h
  005171c6  8bd0                  mov       edx,eax
  005171c8  6bd24c                imul      edx,4Ch
  005171cb  80ba88915e0008        cmp       byte [edx+5E9188h],8
  005171d2  7517                  jne       short 005171EBh

LAB_005171d4:
  005171d4  6bc04c                imul      eax,4Ch
  005171d7  0580915e00            add       eax,5E9180h
  005171dc  6800000100            push      10000h
  005171e1  50                    push      eax
  005171e2  51                    push      ecx
  005171e3  e83873ffff            call      0050E520h
  005171e8  83c40c                add       esp,0Ch

LAB_005171eb:
  005171eb  a1a8335600            mov       eax,[5633A8h]
  005171f0  33d2                  xor       edx,edx
  005171f2  3bc2                  cmp       eax,edx
  005171f4  0f8531040000          jne       near 0051762Bh
  005171fa  8a0d20e05d00          mov       cl,[5DE020h]
  00517200  84c9                  test      cl,cl
  00517202  0f8423040000          je        near 0051762Bh
  00517208  8b4c2410              mov       ecx,[esp+10h]
  0051720c  b810000000            mov       eax,10h
  00517211  6aff                  push      0FFFFFFFFh
  00517213  6689442440            mov       [esp+40h],ax
  00517218  6689442442            mov       [esp+42h],ax
  0051721d  52                    push      edx
  0051721e  52                    push      edx
  0051721f  8b542420              mov       edx,[esp+20h]
  00517223  8d442440              lea       eax,[esp+40h]
  00517227  68284a5600            push      564A28h
  0051722c  50                    push      eax
  0051722d  51                    push      ecx
  0051722e  52                    push      edx
  0051722f  897c2450              mov       [esp+50h],edi
  00517233  e8e84e0000            call      0051C120h
  00517238  8b542430              mov       edx,[esp+30h]
  0051723c  83c41c                add       esp,1Ch
  0051723f  8d4c2434              lea       ecx,[esp+34h]
  00517243  be08000000            mov       esi,8
  00517248  660174243e            add       [esp+3Eh],si
  0051724d  6aff                  push      0FFFFFFFFh
  0051724f  6a00                  push      0
  00517251  6a00                  push      0
  00517253  68244a5600            push      564A24h
  00517258  51                    push      ecx
  00517259  50                    push      eax
  0051725a  52                    push      edx
  0051725b  8944242c              mov       [esp+2Ch],eax
  0051725f  e8bc4e0000            call      0051C120h
  00517264  8b542430              mov       edx,[esp+30h]
  00517268  83c41c                add       esp,1Ch
  0051726b  660174243e            add       [esp+3Eh],si
  00517270  8d4c2434              lea       ecx,[esp+34h]
  00517274  6aff                  push      0FFFFFFFFh
  00517276  6a00                  push      0
  00517278  6a00                  push      0
  0051727a  68204a5600            push      564A20h
  0051727f  51                    push      ecx
  00517280  50                    push      eax
  00517281  52                    push      edx
  00517282  8944242c              mov       [esp+2Ch],eax
  00517286  e8954e0000            call      0051C120h
  0051728b  8b542430              mov       edx,[esp+30h]
  0051728f  83c41c                add       esp,1Ch
  00517292  660174243e            add       [esp+3Eh],si
  00517297  8d4c2434              lea       ecx,[esp+34h]
  0051729b  6aff                  push      0FFFFFFFFh
  0051729d  6a00                  push      0
  0051729f  6a00                  push      0
  005172a1  681c4a5600            push      564A1Ch
  005172a6  51                    push      ecx
  005172a7  50                    push      eax
  005172a8  52                    push      edx
  005172a9  8944242c              mov       [esp+2Ch],eax
  005172ad  e86e4e0000            call      0051C120h
  005172b2  8b0d28496000          mov       ecx,[604928h]
  005172b8  83c41c                add       esp,1Ch
  005172bb  668344243ee8          add       word [esp+3Eh],0FFE8h
  005172c1  81e1ff0f0000          and       ecx,0FFFh
  005172c7  6a00                  push      0
  005172c9  6a04                  push      4
  005172cb  8d54243c              lea       edx,[esp+3Ch]
  005172cf  51                    push      ecx
  005172d0  52                    push      edx
  005172d1  89442420              mov       [esp+20h],eax
  005172d5  50                    push      eax
  005172d6  8b442428              mov       eax,[esp+28h]
  005172da  50                    push      eax
  005172db  66895c2454            mov       [esp+54h],bx
  005172e0  896c244c              mov       [esp+4Ch],ebp
  005172e4  c744245000000001      mov       dword [esp+50h],1000000h
  005172ec  e89f4b0000            call      0051BE90h
  005172f1  668b0d2a496000        mov       cx,[60492Ah]
  005172f8  668344245428          add       word [esp+54h],28h
  005172fe  83c418                add       esp,18h
  00517301  81e1ff0f0000          and       ecx,0FFFh
  00517307  89442410              mov       [esp+10h],eax
  0051730b  8d542434              lea       edx,[esp+34h]
  0051730f  6a00                  push      0
  00517311  6a04                  push      4
  00517313  51                    push      ecx
  00517314  52                    push      edx
  00517315  50                    push      eax
  00517316  8b442428              mov       eax,[esp+28h]
  0051731a  50                    push      eax
  0051731b  e8704b0000            call      0051BE90h
  00517320  6601742456            add       [esp+56h],si
  00517325  8b0ddc935e00          mov       ecx,[5E93DCh]
  0051732b  8b15e0935e00          mov       edx,[5E93E0h]
  00517331  8b35e4935e00          mov       esi,[5E93E4h]
  00517337  c1f90c                sar       ecx,0Ch
  0051733a  83c418                add       esp,18h
  0051733d  89442410              mov       [esp+10h],eax
  00517341  c1fa0c                sar       edx,0Ch
  00517344  c1fe0c                sar       esi,0Ch
  00517347  85c9                  test      ecx,ecx
  00517349  894c2448              mov       [esp+48h],ecx
  0051734d  8954244c              mov       [esp+4Ch],edx
  00517351  89742450              mov       [esp+50h],esi
  00517355  897c2434              mov       [esp+34h],edi
  00517359  7d3b                  jge       short 00517396h
  0051735b  8b542414              mov       edx,[esp+14h]
  0051735f  6aff                  push      0FFFFFFFFh
  00517361  f7d9                  neg       ecx
  00517363  894c244c              mov       [esp+4Ch],ecx
  00517367  6a00                  push      0
  00517369  6a00                  push      0
  0051736b  8d4c2440              lea       ecx,[esp+40h]
  0051736f  68184a5600            push      564A18h
  00517374  51                    push      ecx
  00517375  50                    push      eax
  00517376  52                    push      edx
  00517377  66c74424583000        mov       word [esp+58h],30h
  0051737e  e89d4d0000            call      0051C120h
  00517383  8b74246c              mov       esi,[esp+6Ch]
  00517387  8b542468              mov       edx,[esp+68h]
  0051738b  8b4c2464              mov       ecx,[esp+64h]
  0051738f  83c41c                add       esp,1Ch
  00517392  89442410              mov       [esp+10h],eax

LAB_00517396:
  00517396  85d2                  test      edx,edx
  00517398  7d37                  jge       short 005173D1h
  0051739a  6aff                  push      0FFFFFFFFh
  0051739c  6a00                  push      0
  0051739e  f7da                  neg       edx
  005173a0  6a00                  push      0
  005173a2  8d4c2440              lea       ecx,[esp+40h]
  005173a6  89542458              mov       [esp+58h],edx
  005173aa  8b542420              mov       edx,[esp+20h]
  005173ae  68184a5600            push      564A18h
  005173b3  51                    push      ecx
  005173b4  50                    push      eax
  005173b5  52                    push      edx
  005173b6  66c74424586800        mov       word [esp+58h],68h
  005173bd  e85e4d0000            call      0051C120h
  005173c2  8b74246c              mov       esi,[esp+6Ch]
  005173c6  8b4c2464              mov       ecx,[esp+64h]
  005173ca  83c41c                add       esp,1Ch
  005173cd  89442410              mov       [esp+10h],eax

LAB_005173d1:
  005173d1  85f6                  test      esi,esi
  005173d3  7d33                  jge       short 00517408h
  005173d5  8b542414              mov       edx,[esp+14h]
  005173d9  6aff                  push      0FFFFFFFFh
  005173db  6a00                  push      0
  005173dd  6a00                  push      0
  005173df  8d4c2440              lea       ecx,[esp+40h]
  005173e3  68184a5600            push      564A18h
  005173e8  51                    push      ecx
  005173e9  50                    push      eax
  005173ea  f7de                  neg       esi
  005173ec  52                    push      edx
  005173ed  8974246c              mov       [esp+6Ch],esi
  005173f1  66c7442458a000        mov       word [esp+58h],0A0h
  005173f8  e8234d0000            call      0051C120h
  005173fd  8b4c2464              mov       ecx,[esp+64h]
  00517401  83c41c                add       esp,1Ch
  00517404  89442410              mov       [esp+10h],eax

LAB_00517408:
  00517408  8b542414              mov       edx,[esp+14h]
  0051740c  6a00                  push      0
  0051740e  6a05                  push      5
  00517410  51                    push      ecx
  00517411  8d4c2440              lea       ecx,[esp+40h]
  00517415  66895c2448            mov       [esp+48h],bx
  0051741a  51                    push      ecx
  0051741b  50                    push      eax
  0051741c  52                    push      edx
  0051741d  896c244c              mov       [esp+4Ch],ebp
  00517421  e86a4a0000            call      0051BE90h
  00517426  8b4c2464              mov       ecx,[esp+64h]
  0051742a  83c418                add       esp,18h
  0051742d  66015c243c            add       [esp+3Ch],bx
  00517432  8d542434              lea       edx,[esp+34h]
  00517436  6a00                  push      0
  00517438  6a05                  push      5
  0051743a  51                    push      ecx
  0051743b  52                    push      edx
  0051743c  89442420              mov       [esp+20h],eax
  00517440  50                    push      eax
  00517441  8b442428              mov       eax,[esp+28h]
  00517445  50                    push      eax
  00517446  e8454a0000            call      0051BE90h
  0051744b  8b4c2468              mov       ecx,[esp+68h]
  0051744f  83c418                add       esp,18h
  00517452  66015c243c            add       [esp+3Ch],bx
  00517457  8d542434              lea       edx,[esp+34h]
  0051745b  6a00                  push      0
  0051745d  6a05                  push      5
  0051745f  51                    push      ecx
  00517460  52                    push      edx
  00517461  89442420              mov       [esp+20h],eax
  00517465  50                    push      eax
  00517466  8b442428              mov       eax,[esp+28h]
  0051746a  50                    push      eax
  0051746b  e8204a0000            call      0051BE90h
  00517470  8b35f4086000          mov       esi,[6008F4h]
  00517476  668344245608          add       word [esp+56h],8
  0051747c  8b0df8086000          mov       ecx,[6008F8h]
  00517482  8b15fc086000          mov       edx,[6008FCh]
  00517488  83c418                add       esp,18h
  0051748b  89442410              mov       [esp+10h],eax
  0051748f  85f6                  test      esi,esi
  00517491  89742448              mov       [esp+48h],esi
  00517495  894c244c              mov       [esp+4Ch],ecx
  00517499  89542450              mov       [esp+50h],edx
  0051749d  897c2434              mov       [esp+34h],edi
  005174a1  7d37                  jge       short 005174DAh
  005174a3  8b542414              mov       edx,[esp+14h]
  005174a7  6aff                  push      0FFFFFFFFh
  005174a9  6a00                  push      0
  005174ab  6a00                  push      0
  005174ad  8d4c2440              lea       ecx,[esp+40h]
  005174b1  68184a5600            push      564A18h
  005174b6  51                    push      ecx
  005174b7  50                    push      eax
  005174b8  f7de                  neg       esi
  005174ba  52                    push      edx
  005174bb  89742464              mov       [esp+64h],esi
  005174bf  66c74424583000        mov       word [esp+58h],30h
  005174c6  e8554c0000            call      0051C120h
  005174cb  8b54246c              mov       edx,[esp+6Ch]
  005174cf  8b4c2468              mov       ecx,[esp+68h]
  005174d3  83c41c                add       esp,1Ch
  005174d6  89442410              mov       [esp+10h],eax

LAB_005174da:
  005174da  85c9                  test      ecx,ecx
  005174dc  7d33                  jge       short 00517511h
  005174de  8b542414              mov       edx,[esp+14h]
  005174e2  6aff                  push      0FFFFFFFFh
  005174e4  f7d9                  neg       ecx
  005174e6  894c2450              mov       [esp+50h],ecx
  005174ea  6a00                  push      0
  005174ec  6a00                  push      0
  005174ee  8d4c2440              lea       ecx,[esp+40h]
  005174f2  68184a5600            push      564A18h
  005174f7  51                    push      ecx
  005174f8  50                    push      eax
  005174f9  52                    push      edx
  005174fa  66c74424586800        mov       word [esp+58h],68h
  00517501  e81a4c0000            call      0051C120h
  00517506  8b54246c              mov       edx,[esp+6Ch]
  0051750a  83c41c                add       esp,1Ch
  0051750d  89442410              mov       [esp+10h],eax

LAB_00517511:
  00517511  85d2                  test      edx,edx
  00517513  7d2f                  jge       short 00517544h
  00517515  6aff                  push      0FFFFFFFFh
  00517517  6a00                  push      0
  00517519  f7da                  neg       edx
  0051751b  6a00                  push      0
  0051751d  8d4c2440              lea       ecx,[esp+40h]
  00517521  8954245c              mov       [esp+5Ch],edx
  00517525  8b542420              mov       edx,[esp+20h]
  00517529  68184a5600            push      564A18h
  0051752e  51                    push      ecx
  0051752f  50                    push      eax
  00517530  52                    push      edx
  00517531  66c7442458a000        mov       word [esp+58h],0A0h
  00517538  e8e34b0000            call      0051C120h
  0051753d  83c41c                add       esp,1Ch
  00517540  89442410              mov       [esp+10h],eax

LAB_00517544:
  00517544  8b4c2448              mov       ecx,[esp+48h]
  00517548  6a00                  push      0
  0051754a  6a05                  push      5
  0051754c  8d54243c              lea       edx,[esp+3Ch]
  00517550  51                    push      ecx
  00517551  52                    push      edx
  00517552  50                    push      eax
  00517553  8b442428              mov       eax,[esp+28h]
  00517557  50                    push      eax
  00517558  66895c2454            mov       [esp+54h],bx
  0051755d  896c244c              mov       [esp+4Ch],ebp
  00517561  e82a490000            call      0051BE90h
  00517566  8b4c2464              mov       ecx,[esp+64h]
  0051756a  83c418                add       esp,18h
  0051756d  66015c243c            add       [esp+3Ch],bx
  00517572  8d542434              lea       edx,[esp+34h]
  00517576  6a00                  push      0
  00517578  6a05                  push      5
  0051757a  51                    push      ecx
  0051757b  52                    push      edx
  0051757c  89442420              mov       [esp+20h],eax
  00517580  50                    push      eax
  00517581  8b442428              mov       eax,[esp+28h]
  00517585  50                    push      eax
  00517586  e805490000            call      0051BE90h
  0051758b  8b4c2468              mov       ecx,[esp+68h]
  0051758f  83c418                add       esp,18h
  00517592  66015c243c            add       [esp+3Ch],bx
  00517597  8d542434              lea       edx,[esp+34h]
  0051759b  6a00                  push      0
  0051759d  6a05                  push      5
  0051759f  51                    push      ecx
  005175a0  52                    push      edx
  005175a1  89442420              mov       [esp+20h],eax
  005175a5  50                    push      eax
  005175a6  8b442428              mov       eax,[esp+28h]
  005175aa  50                    push      eax
  005175ab  e8e0480000            call      0051BE90h
  005175b0  8b0d58e05d00          mov       ecx,[5DE058h]
  005175b6  83c418                add       esp,18h
  005175b9  668344243e08          add       word [esp+3Eh],8
  005175bf  81e1ff000000          and       ecx,0FFh
  005175c5  6a00                  push      0
  005175c7  6a05                  push      5
  005175c9  8d54243c              lea       edx,[esp+3Ch]
  005175cd  51                    push      ecx
  005175ce  52                    push      edx
  005175cf  89442420              mov       [esp+20h],eax
  005175d3  50                    push      eax
  005175d4  8b442428              mov       eax,[esp+28h]
  005175d8  50                    push      eax
  005175d9  66895c2454            mov       [esp+54h],bx
  005175de  e8ad480000            call      0051BE90h
  005175e3  b910000000            mov       ecx,10h
  005175e8  bee0965e00            mov       esi,5E96E0h
  005175ed  bf08bb5d00            mov       edi,5DBB08h
  005175f2  8be8                  mov       ebp,eax
  005175f4  f3a5                  rep movsd
  005175f6  b910000000            mov       ecx,10h
  005175fb  be40985e00            mov       esi,5E9840h
  00517600  bfd8df5d00            mov       edi,5DDFD8h
  00517605  a1a8335600            mov       eax,[5633A8h]
  0051760a  f3a5                  rep movsd
  0051760c  b908000000            mov       ecx,8
  00517611  be00905e00            mov       esi,5E9000h
  00517616  bf90df5d00            mov       edi,5DDF90h
  0051761b  83c418                add       esp,18h
  0051761e  f3a5                  rep movsd
  00517620  83ceff                or        esi,0FFFFFFFFh
  00517623  896c2410              mov       [esp+10h],ebp
  00517627  33d2                  xor       edx,edx
  00517629  eb04                  jmp       short 0051762Fh

LAB_0051762b:
  0051762b  8b6c2410              mov       ebp,[esp+10h]

LAB_0051762f:
  0051762f  f6052ce05d0040        test      byte [5DE02Ch],40h
  00517636  744b                  je        short 00517683h
  00517638  33c9                  xor       ecx,ecx
  0051763a  33c0                  xor       eax,eax
  0051763c  8a0d40e55d00          mov       cl,[5DE540h]
  00517642  8bd1                  mov       edx,ecx
  00517644  6bd24c                imul      edx,4Ch
  00517647  8a8288915e00          mov       al,[edx+5E9188h]
  0051764d  8d1489                lea       edx,[ecx+ecx*4]
  00517650  c1e004                shl       eax,4
  00517653  c1e203                shl       edx,3
  00517656  05204a6000            add       eax,604A20h
  0051765b  2bd1                  sub       edx,ecx
  0051765d  8b4c2414              mov       ecx,[esp+14h]
  00517661  50                    push      eax
  00517662  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  00517669  50                    push      eax
  0051766a  55                    push      ebp
  0051766b  51                    push      ecx
  0051766c  ff15f0af5600          call      dword [56AFF0h]
  00517672  83c410                add       esp,10h
  00517675  89442410              mov       [esp+10h],eax
  00517679  bd01000000            mov       ebp,1
  0051767e  e980200000            jmp       00519703h

LAB_00517683:
  00517683  8b0de49a5e00          mov       ecx,[5E9AE4h]
  00517689  83e103                and       ecx,3
  0051768c  83f903                cmp       ecx,3
  0051768f  0f871b030000          ja        near 005179B0h
  00517695  ff248db4975100        jmp       dword [ecx*4+5197B4h]
  0051769c  8b15b4876000          mov       edx,[6087B4h]
  005176a2  b900040000            mov       ecx,400h
  005176a7  33c0                  xor       eax,eax
  005176a9  8b3a                  mov       edi,[edx]
  005176ab  81c700501c00          add       edi,1C5000h
  005176b1  f3ab                  rep stosd
  005176b3  8b15b4876000          mov       edx,[6087B4h]
  005176b9  b900040000            mov       ecx,400h
  005176be  8b3a                  mov       edi,[edx]
  005176c0  81c700401c00          add       edi,1C4000h
  005176c6  f3ab                  rep stosd
  005176c8  83252ce05d00f7        and       dword [5DE02Ch],0FFFFFFF7h
  005176cf  ff15e8af5600          call      dword [56AFE8h]
  005176d5  e816b50100            call      00532BF0h
  005176da  a1cc975e00            mov       eax,[5E97CCh]
  005176df  50                    push      eax
  005176e0  e80bc8ffff            call      00513EF0h
  005176e5  83c404                add       esp,4
  005176e8  e8f36effff            call      0050E5E0h
  005176ed  e88e6fffff            call      0050E680h
  005176f2  e82974ffff            call      0050EB20h
  005176f7  a120905e00            mov       eax,[5E9020h]
  005176fc  33f6                  xor       esi,esi
  005176fe  85c0                  test      eax,eax
  00517700  c605e42c5f0000        mov       byte [5F2CE4h],0
  00517707  7e13                  jle       short 0051771Ch

LAB_00517709:
  00517709  56                    push      esi
  0051770a  e81148ffff            call      0050BF20h
  0051770f  a120905e00            mov       eax,[5E9020h]
  00517714  83c404                add       esp,4
  00517717  46                    inc       esi
  00517718  3bf0                  cmp       esi,eax
  0051771a  7ced                  jl        short 00517709h

LAB_0051771c:
  0051771c  e84f33f6ff            call      0047AA70h
  00517721  e84a34f6ff            call      0047AB70h
  00517726  e8c535f6ff            call      0047ACF0h
  0051772b  f6052ce05d0010        test      byte [5DE02Ch],10h
  00517732  7505                  jne       short 00517739h
  00517734  e83776ffff            call      0050ED70h

LAB_00517739:
  00517739  e89279ffff            call      0050F0D0h
  0051773e  8b0dcc975e00          mov       ecx,[5E97CCh]
  00517744  51                    push      ecx
  00517745  e8d6c7ffff            call      00513F20h
  0051774a  83c404                add       esp,4
  0051774d  e83ed80100            call      00534F90h
  00517752  e889bffeff            call      005036E0h
  00517757  a1f02c5f00            mov       eax,[5F2CF0h]
  0051775c  8b6c2410              mov       ebp,[esp+10h]
  00517760  40                    inc       eax
  00517761  a3f02c5f00            mov       [5F2CF0h],eax
  00517766  a1a8335600            mov       eax,[5633A8h]
  0051776b  e940020000            jmp       005179B0h
  00517770  8a0d80a55f00          mov       cl,[5FA580h]
  00517776  84c9                  test      cl,cl
  00517778  740d                  je        short 00517787h
  0051777a  fec9                  dec       cl
  0051777c  880d80a55f00          mov       [5FA580h],cl
  00517782  e929020000            jmp       005179B0h

LAB_00517787:
  00517787  33c9                  xor       ecx,ecx
  00517789  8a1de49a5e00          mov       bl,[5E9AE4h]
  0051778f  8a0d38e55d00          mov       cl,[5DE538h]
  00517795  fec3                  inc       bl
  00517797  881de49a5e00          mov       [5E9AE4h],bl
  0051779d  8915f8e65d00          mov       [5DE6F8h],edx
  005177a3  8d0489                lea       eax,[ecx+ecx*4]
  005177a6  8915fce65d00          mov       [5DE6FCh],edx
  005177ac  c1e003                shl       eax,3
  005177af  2bc1                  sub       eax,ecx
  005177b1  891504e75d00          mov       [5DE704h],edx
  005177b7  c1e002                shl       eax,2
  005177ba  89350ce75d00          mov       [5DE70Ch],esi
  005177c0  893510e75d00          mov       [5DE710h],esi
  005177c6  8b88dc935e00          mov       ecx,[eax+5E93DCh]
  005177cc  8b80e4935e00          mov       eax,[eax+5E93E4h]
  005177d2  c1f811                sar       eax,11h
  005177d5  c1f911                sar       ecx,11h
  005177d8  24fc                  and       al,0FCh
  005177da  83e1fc                and       ecx,0FFFFFFFCh
  005177dd  663da000              cmp       ax,0A0h
  005177e1  893518e75d00          mov       [5DE718h],esi
  005177e7  66890dd88e5e00        mov       [5E8ED8h],cx
  005177ee  66a3da8e5e00          mov       [5E8EDAh],ax
  005177f4  7e0a                  jle       short 00517800h
  005177f6  66b8a000              mov       ax,0A0h
  005177fa  66a3da8e5e00          mov       [5E8EDAh],ax

LAB_00517800:
  00517800  663d60ff              cmp       ax,0FF60h
  00517804  7d09                  jge       short 0051780Fh
  00517806  66c705da8e5e0060ff    mov       word [5E8EDAh],0FF60h

LAB_0051780f:
  0051780f  6681f970ff            cmp       cx,0FF70h
  00517814  7d0b                  jge       short 00517821h
  00517816  66b970ff              mov       cx,0FF70h
  0051781a  66890dd88e5e00        mov       [5E8ED8h],cx

LAB_00517821:
  00517821  6681f99000            cmp       cx,90h
  00517826  7e09                  jle       short 00517831h
  00517828  66c705d88e5e009000    mov       word [5E8ED8h],90h

LAB_00517831:
  00517831  a12ce05d00            mov       eax,[5DE02Ch]
  00517836  0c04                  or        al,4
  00517838  a32ce05d00            mov       [5DE02Ch],eax
  0051783d  e8be2defff            call      0040A600h
  00517842  e8b92defff            call      0040A600h
  00517847  8b6c2410              mov       ebp,[esp+10h]
  0051784b  a1a8335600            mov       eax,[5633A8h]
  00517850  e95b010000            jmp       005179B0h
  00517855  33c9                  xor       ecx,ecx
  00517857  33db                  xor       ebx,ebx
  00517859  8a0d40e55d00          mov       cl,[5DE540h]
  0051785f  6bc94c                imul      ecx,4Ch
  00517862  8a9988915e00          mov       bl,[ecx+5E9188h]
  00517868  8bcb                  mov       ecx,ebx
  0051786a  c1e104                shl       ecx,4
  0051786d  81c1204a6000          add       ecx,604A20h
  00517873  f6410201              test      byte [ecx+2],1
  00517877  7407                  je        short 00517880h
  00517879  83352ce05d0001        xor       dword [5DE02Ch],1

LAB_00517880:
  00517880  3bc2                  cmp       eax,edx
  00517882  0f8528010000          jne       near 005179B0h
  00517888  391560e05d00          cmp       [5DE060h],edx
  0051788e  0f84c6000000          je        near 0051795Ah
  00517894  668b11                mov       dx,[ecx]
  00517897  f6c201                test      dl,1
  0051789a  7436                  je        short 005178D2h
  0051789c  f6c220                test      dl,20h
  0051789f  7408                  je        short 005178A9h
  005178a1  6683050a935e0020      add       word [5E930Ah],20h

LAB_005178a9:
  005178a9  f60180                test      byte [ecx],80h
  005178ac  7408                  je        short 005178B6h
  005178ae  6683050a935e00e0      add       word [5E930Ah],0FFE0h

LAB_005178b6:
  005178b6  f60110                test      byte [ecx],10h
  005178b9  7408                  je        short 005178C3h
  005178bb  66830508935e00e0      add       word [5E9308h],0FFE0h

LAB_005178c3:
  005178c3  f60140                test      byte [ecx],40h
  005178c6  743b                  je        short 00517903h
  005178c8  66830508935e0020      add       word [5E9308h],20h
  005178d0  eb31                  jmp       short 00517903h

LAB_005178d2:
  005178d2  f6c220                test      dl,20h
  005178d5  ba0a000000            mov       edx,0Ah
  005178da  7406                  je        short 005178E2h
  005178dc  011558465600          add       [564658h],edx

LAB_005178e2:
  005178e2  f60180                test      byte [ecx],80h
  005178e5  7406                  je        short 005178EDh
  005178e7  291558465600          sub       [564658h],edx

LAB_005178ed:
  005178ed  f60110                test      byte [ecx],10h
  005178f0  7406                  je        short 005178F8h
  005178f2  29155c465600          sub       [56465Ch],edx

LAB_005178f8:
  005178f8  f60140                test      byte [ecx],40h
  005178fb  7406                  je        short 00517903h
  005178fd  01155c465600          add       [56465Ch],edx

LAB_00517903:
  00517903  668b5102              mov       dx,[ecx+2]
  00517907  81e2000f0000          and       edx,0F00h
  0051790d  81fa000f0000          cmp       edx,0F00h
  00517913  750a                  jne       short 0051791Fh
  00517915  c70560e05d0000000000  mov       dword [5DE060h],0

LAB_0051791f:
  0051791f  f6410380              test      byte [ecx+3],80h
  00517923  0f8487000000          je        near 005179B0h
  00517929  33c9                  xor       ecx,ecx
  0051792b  66c70550465600f401    mov       word [564650h],1F4h
  00517934  66890d52465600        mov       [564652h],cx
  0051793b  66890d54465600        mov       [564654h],cx
  00517942  890d58465600          mov       [564658h],ecx
  00517948  890d5c465600          mov       [56465Ch],ecx
  0051794e  c7056046560084030000  mov       dword [564660h],384h
  00517958  eb56                  jmp       short 005179B0h

LAB_0051795a:
  0051795a  668b4902              mov       cx,[ecx+2]
  0051795e  81e1000f0000          and       ecx,0F00h
  00517964  81f9000f0000          cmp       ecx,0F00h
  0051796a  7544                  jne       short 005179B0h
  0051796c  c70560e05d0001000000  mov       dword [5DE060h],1
  00517976  eb38                  jmp       short 005179B0h
  00517978  8a0d80a55f00          mov       cl,[5FA580h]
  0051797e  84c9                  test      cl,cl
  00517980  740a                  je        short 0051798Ch
  00517982  fec9                  dec       cl
  00517984  880d80a55f00          mov       [5FA580h],cl
  0051798a  eb24                  jmp       short 005179B0h

LAB_0051798c:
  0051798c  8a1de49a5e00          mov       bl,[5E9AE4h]
  00517992  89354c7b5f00          mov       [5F7B4Ch],esi
  00517998  fec3                  inc       bl
  0051799a  3bc2                  cmp       eax,edx
  0051799c  881de49a5e00          mov       [5E9AE4h],bl
  005179a2  8915a47b5f00          mov       [5F7BA4h],edx
  005179a8  7506                  jne       short 005179B0h
  005179aa  891560e05d00          mov       [5DE060h],edx

LAB_005179b0:
  005179b0  8a1d2ce05d00          mov       bl,[5DE02Ch]
  005179b6  f6c302                test      bl,2
  005179b9  0f85941d0000          jne       near 00519753h
  005179bf  f6c304                test      bl,4
  005179c2  0f84df000000          je        near 00517AA7h
  005179c8  8b0d60e05d00          mov       ecx,[5DE060h]
  005179ce  85c9                  test      ecx,ecx
  005179d0  7408                  je        short 005179DAh
  005179d2  85c0                  test      eax,eax
  005179d4  0f84cd000000          je        near 00517AA7h

LAB_005179da:
  005179da  85c0                  test      eax,eax
  005179dc  7550                  jne       short 00517A2Eh
  005179de  33c9                  xor       ecx,ecx
  005179e0  8a0d40e55d00          mov       cl,[5DE540h]
  005179e6  8d1489                lea       edx,[ecx+ecx*4]
  005179e9  c1e203                shl       edx,3
  005179ec  2bd1                  sub       edx,ecx
  005179ee  33c9                  xor       ecx,ecx
  005179f0  8a0495c8935e00        mov       al,[edx*4+5E93C8h]
  005179f7  8d1495c0935e00        lea       edx,[edx*4+5E93C0h]
  005179fe  6bc04c                imul      eax,4Ch
  00517a01  8a8888915e00          mov       cl,[eax+5E9188h]
  00517a07  c1e104                shl       ecx,4
  00517a0a  81c1204a6000          add       ecx,604A20h
  00517a10  f6c301                test      bl,1
  00517a13  51                    push      ecx
  00517a14  7415                  je        short 00517A2Bh
  00517a16  8b542418              mov       edx,[esp+18h]
  00517a1a  8d4c2438              lea       ecx,[esp+38h]
  00517a1e  51                    push      ecx
  00517a1f  55                    push      ebp
  00517a20  52                    push      edx
  00517a21  e80a300000            call      0051AA30h
  00517a26  83c410                add       esp,10h
  00517a29  eb4c                  jmp       short 00517A77h

LAB_00517a2b:
  00517a2b  52                    push      edx
  00517a2c  eb36                  jmp       short 00517A64h

LAB_00517a2e:
  00517a2e  33c9                  xor       ecx,ecx
  00517a30  33c0                  xor       eax,eax
  00517a32  8a0d40e55d00          mov       cl,[5DE540h]
  00517a38  8d1489                lea       edx,[ecx+ecx*4]
  00517a3b  c1e203                shl       edx,3
  00517a3e  2bd1                  sub       edx,ecx
  00517a40  8a0495c8935e00        mov       al,[edx*4+5E93C8h]
  00517a47  8d0c95c0935e00        lea       ecx,[edx*4+5E93C0h]
  00517a4e  6bc04c                imul      eax,4Ch
  00517a51  33d2                  xor       edx,edx
  00517a53  8a9088915e00          mov       dl,[eax+5E9188h]
  00517a59  c1e204                shl       edx,4
  00517a5c  81c2204a6000          add       edx,604A20h
  00517a62  52                    push      edx
  00517a63  51                    push      ecx

LAB_00517a64:
  00517a64  8b442428              mov       eax,[esp+28h]
  00517a68  8b4c241c              mov       ecx,[esp+1Ch]
  00517a6c  55                    push      ebp
  00517a6d  50                    push      eax
  00517a6e  51                    push      ecx
  00517a6f  e85c2d0100            call      0052A7D0h
  00517a74  83c414                add       esp,14h

LAB_00517a77:
  00517a77  89442410              mov       [esp+10h],eax
  00517a7b  b800010000            mov       eax,100h
  00517a80  66a350995e00          mov       [5E9950h],ax
  00517a86  66a3e0985e00          mov       [5E98E0h],ax
  00517a8c  b8e0000000            mov       eax,0E0h
  00517a91  bd01000000            mov       ebp,1
  00517a96  66a352995e00          mov       [5E9952h],ax
  00517a9c  66a3e2985e00          mov       [5E98E2h],ax
  00517aa2  e95c1c0000            jmp       00519703h

LAB_00517aa7:
  00517aa7  8b154ce55d00          mov       edx,[5DE54Ch]
  00517aad  52                    push      edx
  00517aae  e80d97eeff            call      004011C0h
  00517ab3  83c404                add       esp,4
  00517ab6  e885b00100            call      00532B40h
  00517abb  a1c8975e00            mov       eax,[5E97C8h]
  00517ac0  50                    push      eax
  00517ac1  e83ab50100            call      00533000h
  00517ac6  8b0dcc975e00          mov       ecx,[5E97CCh]
  00517acc  83c404                add       esp,4
  00517acf  51                    push      ecx
  00517ad0  e82bb50100            call      00533000h
  00517ad5  8b0d38086000          mov       ecx,[600838h]
  00517adb  33d2                  xor       edx,edx
  00517add  8a1532086000          mov       dl,[600832h]
  00517ae3  8bc1                  mov       eax,ecx
  00517ae5  25ff000000            and       eax,0FFh
  00517aea  83c404                add       esp,4
  00517aed  8d2c95c07b5f00        lea       ebp,[edx*4+5F7BC0h]
  00517af4  8b1530086000          mov       edx,[600830h]
  00517afa  81e2ffff0000          and       edx,0FFFFh
  00517b00  c1ea08                shr       edx,8
  00517b03  2bc2                  sub       eax,edx
  00517b05  741a                  je        short 00517B21h
  00517b07  8bf0                  mov       esi,eax

LAB_00517b09:
  00517b09  8b4500                mov       eax,[ebp]
  00517b0c  50                    push      eax
  00517b0d  e8dea50100            call      005320F0h
  00517b12  83c404                add       esp,4
  00517b15  83c504                add       ebp,4
  00517b18  4e                    dec       esi
  00517b19  75ee                  jne       short 00517B09h
  00517b1b  8b0d38086000          mov       ecx,[600838h]

LAB_00517b21:
  00517b21  33c0                  xor       eax,eax
  00517b23  81e1ff000000          and       ecx,0FFh
  00517b29  a0ec2c5f00            mov       al,[5F2CECh]
  00517b2e  2bc1                  sub       eax,ecx
  00517b30  7414                  je        short 00517B46h
  00517b32  8bf0                  mov       esi,eax

LAB_00517b34:
  00517b34  8b4d00                mov       ecx,[ebp]
  00517b37  51                    push      ecx
  00517b38  e8b3a60100            call      005321F0h
  00517b3d  83c404                add       esp,4
  00517b40  83c504                add       ebp,4
  00517b43  4e                    dec       esi
  00517b44  75ee                  jne       short 00517B34h

LAB_00517b46:
  00517b46  a1e02c5f00            mov       eax,[5F2CE0h]
  00517b4b  8b0dcc975e00          mov       ecx,[5E97CCh]
  00517b51  33d2                  xor       edx,edx
  00517b53  8a500b                mov       dl,[eax+0Bh]
  00517b56  8b81a0000000          mov       eax,[ecx+0A0h]
  00517b5c  8d0c90                lea       ecx,[eax+edx*4]
  00517b5f  8b15e09a5e00          mov       edx,[5E9AE0h]
  00517b65  890d349b5e00          mov       [5E9B34h],ecx
  00517b6b  668b02                mov       ax,[edx]
  00517b6e  662b0528496000        sub       ax,[604928h]
  00517b75  25ff0f0000            and       eax,0FFFh
  00517b7a  668944245c            mov       [esp+5Ch],ax
  00517b7f  668b4a02              mov       cx,[edx+2]
  00517b83  662b0d2a496000        sub       cx,[60492Ah]
  00517b8a  81e1ff0f0000          and       ecx,0FFFh
  00517b90  66894c245e            mov       [esp+5Eh],cx
  00517b95  668b5204              mov       dx,[edx+4]
  00517b99  662b152c496000        sub       dx,[60492Ch]
  00517ba0  81e2ff0f0000          and       edx,0FFFh
  00517ba6  663d0008              cmp       ax,800h
  00517baa  6689542460            mov       [esp+60h],dx
  00517baf  7d08                  jge       short 00517BB9h
  00517bb1  0fbfc0                movsx     eax,ax
  00517bb4  c1f802                sar       eax,2
  00517bb7  eb0f                  jmp       short 00517BC8h

LAB_00517bb9:
  00517bb9  0fbff0                movsx     esi,ax
  00517bbc  b800100000            mov       eax,1000h
  00517bc1  2bc6                  sub       eax,esi
  00517bc3  c1f802                sar       eax,2
  00517bc6  f7d8                  neg       eax

LAB_00517bc8:
  00517bc8  66010528496000        add       [604928h],ax
  00517bcf  6681f90008            cmp       cx,800h
  00517bd4  7d08                  jge       short 00517BDEh
  00517bd6  0fbfc1                movsx     eax,cx
  00517bd9  c1f802                sar       eax,2
  00517bdc  eb0f                  jmp       short 00517BEDh

LAB_00517bde:
  00517bde  0fbfc9                movsx     ecx,cx
  00517be1  b800100000            mov       eax,1000h
  00517be6  2bc1                  sub       eax,ecx
  00517be8  c1f802                sar       eax,2
  00517beb  f7d8                  neg       eax

LAB_00517bed:
  00517bed  6601052a496000        add       [60492Ah],ax
  00517bf4  6681fa0008            cmp       dx,800h
  00517bf9  7d08                  jge       short 00517C03h
  00517bfb  0fbfc2                movsx     eax,dx
  00517bfe  c1f802                sar       eax,2
  00517c01  eb0f                  jmp       short 00517C12h

LAB_00517c03:
  00517c03  0fbfd2                movsx     edx,dx
  00517c06  b800100000            mov       eax,1000h
  00517c0b  2bc2                  sub       eax,edx
  00517c0d  c1f802                sar       eax,2
  00517c10  f7d8                  neg       eax

LAB_00517c12:
  00517c12  6601052c496000        add       [60492Ch],ax
  00517c19  68e0086000            push      6008E0h
  00517c1e  6828496000            push      604928h
  00517c23  e8d8460000            call      0051C300h
  00517c28  8b1d20905e00          mov       ebx,[5E9020h]
  00517c2e  83c408                add       esp,8
  00517c31  83ceff                or        esi,0FFFFFFFFh
  00517c34  b8c0935e00            mov       eax,5E93C0h
  00517c39  85db                  test      ebx,ebx
  00517c3b  ba80915e00            mov       edx,5E9180h
  00517c40  7e2b                  jle       short 00517C6Dh
  00517c42  8bfb                  mov       edi,ebx

LAB_00517c44:
  00517c44  f70000000050          test      dword [eax],50000000h
  00517c4a  7516                  jne       short 00517C62h
  00517c4c  8b8890000000          mov       ecx,[eax+90h]
  00517c52  f6c580                test      ch,80h
  00517c55  750b                  jne       short 00517C62h
  00517c57  0fbf0a                movsx     ecx,word [edx]
  00517c5a  3bf1                  cmp       esi,ecx
  00517c5c  7d04                  jge       short 00517C62h
  00517c5e  8bf1                  mov       esi,ecx
  00517c60  8be8                  mov       ebp,eax

LAB_00517c62:
  00517c62  059c000000            add       eax,9Ch
  00517c67  83c24c                add       edx,4Ch
  00517c6a  4f                    dec       edi
  00517c6b  75d7                  jne       short 00517C44h

LAB_00517c6d:
  00517c6d  83cfff                or        edi,0FFFFFFFFh
  00517c70  bec0935e00            mov       esi,5E93C0h
  00517c75  85db                  test      ebx,ebx
  00517c77  c744241800000000      mov       dword [esp+18h],0
  00517c7f  7e6e                  jle       short 00517CEFh

LAB_00517c81:
  00517c81  3bf5                  cmp       esi,ebp
  00517c83  7457                  je        short 00517CDCh
  00517c85  f70600000050          test      dword [esi],50000000h
  00517c8b  754f                  jne       short 00517CDCh
  00517c8d  8b8690000000          mov       eax,[esi+90h]
  00517c93  f6c480                test      ah,80h
  00517c96  7544                  jne       short 00517CDCh
  00517c98  8b461c                mov       eax,[esi+1Ch]
  00517c9b  8b4d1c                mov       ecx,[ebp+1Ch]
  00517c9e  2bc1                  sub       eax,ecx
  00517ca0  8b4e24                mov       ecx,[esi+24h]
  00517ca3  c1f80c                sar       eax,0Ch
  00517ca6  89442448              mov       [esp+48h],eax
  00517caa  8b5524                mov       edx,[ebp+24h]
  00517cad  2bca                  sub       ecx,edx
  00517caf  0fafc0                imul      eax,eax
  00517cb2  c1f90c                sar       ecx,0Ch
  00517cb5  0fafc9                imul      ecx,ecx
  00517cb8  89442448              mov       [esp+48h],eax
  00517cbc  03c1                  add       eax,ecx
  00517cbe  50                    push      eax
  00517cbf  894c2454              mov       [esp+54h],ecx
  00517cc3  e848dd0200            call      00545A10h
  00517cc8  3bf8                  cmp       edi,eax
  00517cca  8944244c              mov       [esp+4Ch],eax
  00517cce  7d06                  jge       short 00517CD6h
  00517cd0  8bf8                  mov       edi,eax
  00517cd2  8974241c              mov       [esp+1Ch],esi

LAB_00517cd6:
  00517cd6  8b1d20905e00          mov       ebx,[5E9020h]

LAB_00517cdc:
  00517cdc  8b442418              mov       eax,[esp+18h]
  00517ce0  81c69c000000          add       esi,9Ch
  00517ce6  40                    inc       eax
  00517ce7  3bc3                  cmp       eax,ebx
  00517ce9  89442418              mov       [esp+18h],eax
  00517ced  7c92                  jl        short 00517C81h

LAB_00517cef:
  00517cef  33ff                  xor       edi,edi
  00517cf1  33c0                  xor       eax,eax
  00517cf3  3bdf                  cmp       ebx,edi
  00517cf5  a398e05d00            mov       [5DE098h],eax
  00517cfa  893da0e05d00          mov       [5DE0A0h],edi
  00517d00  c744244c00000080      mov       dword [esp+4Ch],80000000h
  00517d08  897c2418              mov       [esp+18h],edi
  00517d0c  7e51                  jle       short 00517D5Fh
  00517d0e  b9dc935e00            mov       ecx,5E93DCh

LAB_00517d13:
  00517d13  8b51e4                mov       edx,[ecx-1Ch]
  00517d16  f7c200000050          test      edx,50000000h
  00517d1c  752d                  jne       short 00517D4Bh
  00517d1e  f7417400800000        test      dword [ecx+74h],8000h
  00517d25  7524                  jne       short 00517D4Bh
  00517d27  8b31                  mov       esi,[ecx]
  00517d29  03c6                  add       eax,esi
  00517d2b  8b7108                mov       esi,[ecx+8]
  00517d2e  03fe                  add       edi,esi
  00517d30  f7c200000100          test      edx,10000h
  00517d36  750f                  jne       short 00517D47h
  00517d38  8b5104                mov       edx,[ecx+4]
  00517d3b  8b74244c              mov       esi,[esp+4Ch]
  00517d3f  3bf2                  cmp       esi,edx
  00517d41  7d04                  jge       short 00517D47h
  00517d43  8954244c              mov       [esp+4Ch],edx

LAB_00517d47:
  00517d47  ff442418              inc       dword [esp+18h]

LAB_00517d4b:
  00517d4b  81c19c000000          add       ecx,9Ch
  00517d51  4b                    dec       ebx
  00517d52  75bf                  jne       short 00517D13h
  00517d54  893da0e05d00          mov       [5DE0A0h],edi
  00517d5a  a398e05d00            mov       [5DE098h],eax

LAB_00517d5f:
  00517d5f  8b4c2418              mov       ecx,[esp+18h]
  00517d63  85c9                  test      ecx,ecx
  00517d65  0f8493000000          je        near 00517DFEh
  00517d6b  83f901                cmp       ecx,1
  00517d6e  750c                  jne       short 00517D7Ch
  00517d70  8b4d1c                mov       ecx,[ebp+1Ch]
  00517d73  894c2448              mov       [esp+48h],ecx
  00517d77  8b7524                mov       esi,[ebp+24h]
  00517d7a  eb0e                  jmp       short 00517D8Ah

LAB_00517d7c:
  00517d7c  8b54241c              mov       edx,[esp+1Ch]
  00517d80  8b4a1c                mov       ecx,[edx+1Ch]
  00517d83  894c2448              mov       [esp+48h],ecx
  00517d87  8b7224                mov       esi,[edx+24h]

LAB_00517d8a:
  00517d8a  89742450              mov       [esp+50h],esi
  00517d8e  8b5d1c                mov       ebx,[ebp+1Ch]
  00517d91  03cb                  add       ecx,ebx
  00517d93  8b5c2418              mov       ebx,[esp+18h]
  00517d97  894c2448              mov       [esp+48h],ecx
  00517d9b  8b5524                mov       edx,[ebp+24h]
  00517d9e  03f2                  add       esi,edx
  00517da0  99                    cdq
  00517da1  f7fb                  idiv      ebx
  00517da3  d1f9                  sar       ecx,1
  00517da5  894c2448              mov       [esp+48h],ecx
  00517da9  d1fe                  sar       esi,1
  00517dab  89742450              mov       [esp+50h],esi
  00517daf  a398e05d00            mov       [5DE098h],eax
  00517db4  8bc7                  mov       eax,edi
  00517db6  99                    cdq
  00517db7  f7fb                  idiv      ebx
  00517db9  a3a0e05d00            mov       [5DE0A0h],eax
  00517dbe  a168e05d00            mov       eax,[5DE068h]
  00517dc3  2bc8                  sub       ecx,eax
  00517dc5  c1f902                sar       ecx,2
  00517dc8  03c1                  add       eax,ecx
  00517dca  a368e05d00            mov       [5DE068h],eax
  00517dcf  a170e05d00            mov       eax,[5DE070h]
  00517dd4  2bf0                  sub       esi,eax
  00517dd6  c1fe02                sar       esi,2
  00517dd9  03c6                  add       eax,esi
  00517ddb  a370e05d00            mov       [5DE070h],eax
  00517de0  8b44244c              mov       eax,[esp+4Ch]
  00517de4  3d00000080            cmp       eax,80000000h
  00517de9  7413                  je        short 00517DFEh
  00517deb  8b0d6ce05d00          mov       ecx,[5DE06Ch]
  00517df1  2bc1                  sub       eax,ecx
  00517df3  c1f802                sar       eax,2
  00517df6  03c8                  add       ecx,eax
  00517df8  890d6ce05d00          mov       [5DE06Ch],ecx

LAB_00517dfe:
  00517dfe  a1bcde5e00            mov       eax,[5EDEBCh]
  00517e03  8b0dd0086000          mov       ecx,[6008D0h]
  00517e09  8b3dace05d00          mov       edi,[5DE0ACh]
  00517e0f  8b35b0e05d00          mov       esi,[5DE0B0h]
  00517e15  8b10                  mov       edx,[eax]
  00517e17  6a78                  push      78h
  00517e19  03d1                  add       edx,ecx
  00517e1b  68a0000000            push      0A0h
  00517e20  f7da                  neg       edx
  00517e22  c1fa02                sar       edx,2
  00517e25  03ca                  add       ecx,edx
  00517e27  890dd0086000          mov       [6008D0h],ecx
  00517e2d  8b5004                mov       edx,[eax+4]
  00517e30  8b0dd4086000          mov       ecx,[6008D4h]
  00517e36  2bd1                  sub       edx,ecx
  00517e38  c1fa02                sar       edx,2
  00517e3b  03ca                  add       ecx,edx
  00517e3d  8b15a8e05d00          mov       edx,[5DE0A8h]
  00517e43  890dd4086000          mov       [6008D4h],ecx
  00517e49  8b4808                mov       ecx,[eax+8]
  00517e4c  a1d8086000            mov       eax,[6008D8h]
  00517e51  03c8                  add       ecx,eax
  00517e53  f7d9                  neg       ecx
  00517e55  c1f902                sar       ecx,2
  00517e58  03c1                  add       eax,ecx
  00517e5a  a3d8086000            mov       [6008D8h],eax
  00517e5f  a194ca5e00            mov       eax,[5ECA94h]
  00517e64  0310                  add       edx,[eax]
  00517e66  8915f4086000          mov       [6008F4h],edx
  00517e6c  8b4804                mov       ecx,[eax+4]
  00517e6f  03cf                  add       ecx,edi
  00517e71  890df8086000          mov       [6008F8h],ecx
  00517e77  8b5008                mov       edx,[eax+8]
  00517e7a  03d6                  add       edx,esi
  00517e7c  8915fc086000          mov       [6008FCh],edx
  00517e82  e81fdc0200            call      00545AA6h
  00517e87  a027e05d00            mov       al,[5DE027h]
  00517e8c  84c0                  test      al,al
  00517e8e  742c                  je        short 00517EBCh
  00517e90  a124e05d00            mov       eax,[5DE024h]
  00517e95  8b4c2410              mov       ecx,[esp+10h]
  00517e99  8b542414              mov       edx,[esp+14h]
  00517e9d  6800010000            push      100h
  00517ea2  33ff                  xor       edi,edi
  00517ea4  6840010000            push      140h
  00517ea9  57                    push      edi
  00517eaa  57                    push      edi
  00517eab  50                    push      eax
  00517eac  51                    push      ecx
  00517ead  52                    push      edx
  00517eae  e88d7a0000            call      0051F940h
  00517eb3  83c41c                add       esp,1Ch
  00517eb6  89442410              mov       [esp+10h],eax
  00517eba  eb02                  jmp       short 00517EBEh

LAB_00517ebc:
  00517ebc  33ff                  xor       edi,edi

LAB_00517ebe:
  00517ebe  a0b4e05d00            mov       al,[5DE0B4h]
  00517ec3  84c0                  test      al,al
  00517ec5  0f8497000000          je        near 00517F62h
  00517ecb  8b4c2410              mov       ecx,[esp+10h]
  00517ecf  8b542414              mov       edx,[esp+14h]
  00517ed3  fec8                  dec       al
  00517ed5  6a10                  push      10h
  00517ed7  a2b4e05d00            mov       [5DE0B4h],al
  00517edc  57                    push      edi
  00517edd  57                    push      edi
  00517ede  8d442440              lea       eax,[esp+40h]
  00517ee2  68d0a55f00            push      5FA5D0h
  00517ee7  50                    push      eax
  00517ee8  51                    push      ecx
  00517ee9  52                    push      edx
  00517eea  c7442450286c5600      mov       dword [esp+50h],566C28h
  00517ef2  66c74424580800        mov       word [esp+58h],8
  00517ef9  66c744245a1000        mov       word [esp+5Ah],10h
  00517f00  e81b420000            call      0051C120h
  00517f05  8b542430              mov       edx,[esp+30h]
  00517f09  83c41c                add       esp,1Ch
  00517f0c  668344243e0a          add       word [esp+3Eh],0Ah
  00517f12  8d4c2434              lea       ecx,[esp+34h]
  00517f16  6aff                  push      0FFFFFFFFh
  00517f18  57                    push      edi
  00517f19  57                    push      edi
  00517f1a  68144a5600            push      564A14h
  00517f1f  51                    push      ecx
  00517f20  50                    push      eax
  00517f21  52                    push      edx
  00517f22  8944242c              mov       [esp+2Ch],eax
  00517f26  e8f5410000            call      0051C120h
  00517f2b  0fbf0d229a5e00        movsx     ecx,word [5E9A22h]
  00517f32  83c41c                add       esp,1Ch
  00517f35  668344243c12          add       word [esp+3Ch],12h
  00517f3b  8d542434              lea       edx,[esp+34h]
  00517f3f  89442410              mov       [esp+10h],eax
  00517f43  57                    push      edi
  00517f44  6a05                  push      5
  00517f46  51                    push      ecx
  00517f47  52                    push      edx
  00517f48  50                    push      eax
  00517f49  8b442428              mov       eax,[esp+28h]
  00517f4d  50                    push      eax
  00517f4e  c744244c306c5600      mov       dword [esp+4Ch],566C30h
  00517f56  e8353f0000            call      0051BE90h
  00517f5b  83c418                add       esp,18h
  00517f5e  89442410              mov       [esp+10h],eax

LAB_00517f62:
  00517f62  8b542414              mov       edx,[esp+14h]
  00517f66  8d4c2410              lea       ecx,[esp+10h]
  00517f6a  83c208                add       edx,8
  00517f6d  be01000000            mov       esi,1
  00517f72  51                    push      ecx
  00517f73  52                    push      edx
  00517f74  89355ce15d00          mov       [5DE15Ch],esi
  00517f7a  893d54e15d00          mov       [5DE154h],edi
  00517f80  893d58e15d00          mov       [5DE158h],edi
  00517f86  e8953e0000            call      0051BE20h
  00517f8b  8b4c241c              mov       ecx,[esp+1Ch]
  00517f8f  83c408                add       esp,8
  00517f92  8d442410              lea       eax,[esp+10h]
  00517f96  83c108                add       ecx,8
  00517f99  50                    push      eax
  00517f9a  51                    push      ecx
  00517f9b  e8103e0000            call      0051BDB0h
  00517fa0  8b0d4ce55d00          mov       ecx,[5DE54Ch]
  00517fa6  83c408                add       esp,8
  00517fa9  33c0                  xor       eax,eax
  00517fab  83f902                cmp       ecx,2
  00517fae  893d5ce15d00          mov       [5DE15Ch],edi
  00517fb4  89442434              mov       [esp+34h],eax
  00517fb8  7509                  jne       short 00517FC3h
  00517fba  b888475600            mov       eax,564788h
  00517fbf  89442434              mov       [esp+34h],eax

LAB_00517fc3:
  00517fc3  3bce                  cmp       ecx,esi
  00517fc5  7512                  jne       short 00517FD9h
  00517fc7  803d50e55d0002        cmp       byte [5DE550h],2
  00517fce  7509                  jne       short 00517FD9h
  00517fd0  b890475600            mov       eax,564790h
  00517fd5  89442434              mov       [esp+34h],eax

LAB_00517fd9:
  00517fd9  3bc7                  cmp       eax,edi
  00517fdb  7467                  je        short 00518044h
  00517fdd  8b1560995e00          mov       edx,[5E9960h]
  00517fe3  66c744243c3001        mov       word [esp+3Ch],130h
  00517fea  c1e209                shl       edx,9
  00517fed  52                    push      edx
  00517fee  66c74424421000        mov       word [esp+42h],10h
  00517ff5  e822da0200            call      00545A1Ch
  00517ffa  c1f803                sar       eax,3
  00517ffd  0500100000            add       eax,1000h
  00518002  6689442440            mov       [esp+40h],ax
  00518007  a160995e00            mov       eax,[5E9960h]
  0051800c  c1e009                shl       eax,9
  0051800f  50                    push      eax
  00518010  e807da0200            call      00545A1Ch
  00518015  8b542410              mov       edx,[esp+10h]
  00518019  57                    push      edi
  0051801a  c1f803                sar       eax,3
  0051801d  0500100000            add       eax,1000h
  00518022  8d4c2438              lea       ecx,[esp+38h]
  00518026  6689442446            mov       [esp+46h],ax
  0051802b  8b442418              mov       eax,[esp+18h]
  0051802f  57                    push      edi
  00518030  51                    push      ecx
  00518031  52                    push      edx
  00518032  50                    push      eax
  00518033  66897c2458            mov       [esp+58h],di
  00518038  e8e39e0000            call      00521F20h
  0051803d  83c414                add       esp,14h
  00518040  89442410              mov       [esp+10h],eax

LAB_00518044:
  00518044  a1384a5600            mov       eax,[564A38h]
  00518049  89355ce15d00          mov       [5DE15Ch],esi
  0051804f  3bc7                  cmp       eax,edi
  00518051  a160e15d00            mov       eax,[5DE160h]
  00518056  7520                  jne       short 00518078h
  00518058  83f808                cmp       eax,8
  0051805b  7c1b                  jl        short 00518078h
  0051805d  b90a000000            mov       ecx,0Ah
  00518062  893d54e15d00          mov       [5DE154h],edi
  00518068  2bc8                  sub       ecx,eax
  0051806a  8d0c49                lea       ecx,[ecx+ecx*2]
  0051806d  c1e102                shl       ecx,2
  00518070  890d58e15d00          mov       [5DE158h],ecx
  00518076  eb1d                  jmp       short 00518095h

LAB_00518078:
  00518078  b90a000000            mov       ecx,0Ah
  0051807d  c70554e15d00a0000000  mov       dword [5DE154h],0A0h
  00518087  2bc8                  sub       ecx,eax
  00518089  8d1449                lea       edx,[ecx+ecx*2]
  0051808c  c1e202                shl       edx,2
  0051808f  891558e15d00          mov       [5DE158h],edx

LAB_00518095:
  00518095  393d5c3c5600          cmp       [563C5Ch],edi
  0051809b  7416                  je        short 005180B3h
  0051809d  8b442410              mov       eax,[esp+10h]
  005180a1  68b0335600            push      5633B0h
  005180a6  50                    push      eax
  005180a7  e8f4c20200            call      005443A0h
  005180ac  83c408                add       esp,8
  005180af  89442410              mov       [esp+10h],eax

LAB_005180b3:
  005180b3  8a15773c5600          mov       dl,[563C77h]
  005180b9  66a1b6335600          mov       ax,[5633B6h]
  005180bf  8aca                  mov       cl,dl
  005180c1  80e101                and       cl,1
  005180c4  8ad9                  mov       bl,cl
  005180c6  f6db                  neg       bl
  005180c8  1bdb                  sbb       ebx,ebx
  005180ca  0fbff0                movsx     esi,ax
  005180cd  83e370                and       ebx,70h
  005180d0  83c3b0                add       ebx,0FFFFFFB0h
  005180d3  2bde                  sub       ebx,esi
  005180d5  c1fb02                sar       ebx,2
  005180d8  6603c3                add       ax,bx
  005180db  84c9                  test      cl,cl
  005180dd  66a3b6335600          mov       [5633B6h],ax
  005180e3  740c                  je        short 005180F1h
  005180e5  0fbfc8                movsx     ecx,ax
  005180e8  3bf1                  cmp       esi,ecx
  005180ea  7505                  jne       short 005180F1h
  005180ec  80e2df                and       dl,0DFh
  005180ef  eb03                  jmp       short 005180F4h

LAB_005180f1:
  005180f1  80ca20                or        dl,20h

LAB_005180f4:
  005180f4  f6c201                test      dl,1
  005180f7  8815773c5600          mov       [563C77h],dl
  005180fd  7507                  jne       short 00518106h
  005180ff  0fbfd0                movsx     edx,ax
  00518102  3bf2                  cmp       esi,edx
  00518104  741b                  je        short 00518121h

LAB_00518106:
  00518106  8b442410              mov       eax,[esp+10h]
  0051810a  8b4c2414              mov       ecx,[esp+14h]
  0051810e  68b0335600            push      5633B0h
  00518113  50                    push      eax
  00518114  51                    push      ecx
  00518115  e826d20200            call      00545340h
  0051811a  83c40c                add       esp,0Ch
  0051811d  89442410              mov       [esp+10h],eax

LAB_00518121:
  00518121  a1a8335600            mov       eax,[5633A8h]
  00518126  893d54e15d00          mov       [5DE154h],edi
  0051812c  3bc7                  cmp       eax,edi
  0051812e  893d58e15d00          mov       [5DE158h],edi
  00518134  893d5ce15d00          mov       [5DE15Ch],edi
  0051813a  0f8533030000          jne       near 00518473h
  00518140  a020e05d00            mov       al,[5DE020h]
  00518145  84c0                  test      al,al
  00518147  0f8426030000          je        near 00518473h
  0051814d  a124455600            mov       eax,[564524h]
  00518152  85c0                  test      eax,eax
  00518154  0f85da010000          jne       near 00518334h
  0051815a  c60510bc5d0000        mov       byte [5DBC10h],0
  00518161  33db                  xor       ebx,ebx

LAB_00518163:
  00518163  33d2                  xor       edx,edx
  00518165  33c0                  xor       eax,eax
  00518167  8a93e0965e00          mov       dl,[ebx+5E96E0h]
  0051816d  8a8308bb5d00          mov       al,[ebx+5DBB08h]
  00518173  33ed                  xor       ebp,ebp
  00518175  8bfa                  mov       edi,edx
  00518177  896c2448              mov       [esp+48h],ebp
  0051817b  33c7                  xor       eax,edi

LAB_0051817d:
  0051817d  be01000000            mov       esi,1
  00518182  8bcd                  mov       ecx,ebp
  00518184  d3e6                  shl       esi,cl
  00518186  85f0                  test      eax,esi
  00518188  7441                  je        short 005181CBh
  0051818a  8b1510bc5d00          mov       edx,[5DBC10h]
  00518190  8a4c2448              mov       cl,[esp+48h]
  00518194  81e2ff000000          and       edx,0FFh
  0051819a  888a50bb5d00          mov       [edx+5DBB50h],cl
  005181a0  8bce                  mov       ecx,esi
  005181a2  23cf                  and       ecx,edi
  005181a4  889ad0bb5d00          mov       [edx+5DBBD0h],bl
  005181aa  f7d9                  neg       ecx
  005181ac  1bc9                  sbb       ecx,ecx
  005181ae  c68228bc5d0000        mov       byte [edx+5DBC28h],0
  005181b5  f7d9                  neg       ecx
  005181b7  888a90bb5d00          mov       [edx+5DBB90h],cl
  005181bd  8a0d10bc5d00          mov       cl,[5DBC10h]
  005181c3  fec1                  inc       cl
  005181c5  880d10bc5d00          mov       [5DBC10h],cl

LAB_005181cb:
  005181cb  45                    inc       ebp
  005181cc  83fd08                cmp       ebp,8
  005181cf  896c2448              mov       [esp+48h],ebp
  005181d3  7ca8                  jl        short 0051817Dh
  005181d5  43                    inc       ebx
  005181d6  83fb40                cmp       ebx,40h
  005181d9  7c88                  jl        short 00518163h
  005181db  33db                  xor       ebx,ebx

LAB_005181dd:
  005181dd  33d2                  xor       edx,edx
  005181df  33c0                  xor       eax,eax
  005181e1  8a9340985e00          mov       dl,[ebx+5E9840h]
  005181e7  8a83d8df5d00          mov       al,[ebx+5DDFD8h]
  005181ed  33ed                  xor       ebp,ebp
  005181ef  8bfa                  mov       edi,edx
  005181f1  896c2448              mov       [esp+48h],ebp
  005181f5  33c7                  xor       eax,edi

LAB_005181f7:
  005181f7  be01000000            mov       esi,1
  005181fc  8bcd                  mov       ecx,ebp
  005181fe  d3e6                  shl       esi,cl
  00518200  85c6                  test      esi,eax
  00518202  7441                  je        short 00518245h
  00518204  8b1510bc5d00          mov       edx,[5DBC10h]
  0051820a  8a4c2448              mov       cl,[esp+48h]
  0051820e  81e2ff000000          and       edx,0FFh
  00518214  888a50bb5d00          mov       [edx+5DBB50h],cl
  0051821a  8bcf                  mov       ecx,edi
  0051821c  23ce                  and       ecx,esi
  0051821e  889ad0bb5d00          mov       [edx+5DBBD0h],bl
  00518224  f7d9                  neg       ecx
  00518226  1bc9                  sbb       ecx,ecx
  00518228  c68228bc5d0001        mov       byte [edx+5DBC28h],1
  0051822f  f7d9                  neg       ecx
  00518231  888a90bb5d00          mov       [edx+5DBB90h],cl
  00518237  8a0d10bc5d00          mov       cl,[5DBC10h]
  0051823d  fec1                  inc       cl
  0051823f  880d10bc5d00          mov       [5DBC10h],cl

LAB_00518245:
  00518245  45                    inc       ebp
  00518246  83fd08                cmp       ebp,8
  00518249  896c2448              mov       [esp+48h],ebp
  0051824d  7ca8                  jl        short 005181F7h
  0051824f  43                    inc       ebx
  00518250  83fb40                cmp       ebx,40h
  00518253  7c88                  jl        short 005181DDh
  00518255  33db                  xor       ebx,ebx

LAB_00518257:
  00518257  33d2                  xor       edx,edx
  00518259  33c0                  xor       eax,eax
  0051825b  8a9300905e00          mov       dl,[ebx+5E9000h]
  00518261  8a8390df5d00          mov       al,[ebx+5DDF90h]
  00518267  33ed                  xor       ebp,ebp
  00518269  8bfa                  mov       edi,edx
  0051826b  896c2448              mov       [esp+48h],ebp
  0051826f  33c7                  xor       eax,edi

LAB_00518271:
  00518271  be01000000            mov       esi,1
  00518276  8bcd                  mov       ecx,ebp
  00518278  d3e6                  shl       esi,cl
  0051827a  85c6                  test      esi,eax
  0051827c  7441                  je        short 005182BFh
  0051827e  8b1510bc5d00          mov       edx,[5DBC10h]
  00518284  8a4c2448              mov       cl,[esp+48h]
  00518288  81e2ff000000          and       edx,0FFh
  0051828e  888a50bb5d00          mov       [edx+5DBB50h],cl
  00518294  8bcf                  mov       ecx,edi
  00518296  23ce                  and       ecx,esi
  00518298  889ad0bb5d00          mov       [edx+5DBBD0h],bl
  0051829e  f7d9                  neg       ecx
  005182a0  1bc9                  sbb       ecx,ecx
  005182a2  c68228bc5d0002        mov       byte [edx+5DBC28h],2
  005182a9  f7d9                  neg       ecx
  005182ab  888a90bb5d00          mov       [edx+5DBB90h],cl
  005182b1  8a0d10bc5d00          mov       cl,[5DBC10h]
  005182b7  fec1                  inc       cl
  005182b9  880d10bc5d00          mov       [5DBC10h],cl

LAB_005182bf:
  005182bf  45                    inc       ebp
  005182c0  83fd08                cmp       ebp,8
  005182c3  896c2448              mov       [esp+48h],ebp
  005182c7  7ca8                  jl        short 00518271h
  005182c9  43                    inc       ebx
  005182ca  83fb20                cmp       ebx,20h
  005182cd  7c88                  jl        short 00518257h
  005182cf  a010bc5d00            mov       al,[5DBC10h]
  005182d4  84c0                  test      al,al
  005182d6  744f                  je        short 00518327h
  005182d8  33c0                  xor       eax,eax
  005182da  6a00                  push      0
  005182dc  a038e55d00            mov       al,[5DE538h]
  005182e1  68783c5600            push      563C78h
  005182e6  8d1480                lea       edx,[eax+eax*4]
  005182e9  c1e203                shl       edx,3
  005182ec  2bd0                  sub       edx,eax
  005182ee  33c0                  xor       eax,eax
  005182f0  8a0495c8935e00        mov       al,[edx*4+5E93C8h]
  005182f7  8d0c95c0935e00        lea       ecx,[edx*4+5E93C0h]
  005182fe  6bc04c                imul      eax,4Ch
  00518301  33d2                  xor       edx,edx
  00518303  51                    push      ecx
  00518304  8a9088915e00          mov       dl,[eax+5E9188h]
  0051830a  68104a5600            push      564A10h
  0051830f  8bc2                  mov       eax,edx
  00518311  c1e004                shl       eax,4
  00518314  05204a6000            add       eax,604A20h
  00518319  66816002ff7f          and       word [eax+2],7FFFh
  0051831f  e82cc00200            call      00544350h
  00518324  83c410                add       esp,10h

LAB_00518327:
  00518327  a124455600            mov       eax,[564524h]
  0051832c  85c0                  test      eax,eax
  0051832e  0f843f010000          je        near 00518473h

LAB_00518334:
  00518334  a010bc5d00            mov       al,[5DBC10h]
  00518339  33f6                  xor       esi,esi
  0051833b  84c0                  test      al,al
  0051833d  c744243800000001      mov       dword [esp+38h],1000000h
  00518345  66c744243e6800        mov       word [esp+3Eh],68h
  0051834c  0f86f4000000          jbe       near 00518446h
  00518352  bd10000000            mov       ebp,10h
  00518357  bb306c5600            mov       ebx,566C30h
  0051835c  bf286c5600            mov       edi,566C28h

LAB_00518361:
  00518361  33c0                  xor       eax,eax
  00518363  6aff                  push      0FFFFFFFFh
  00518365  8a8628bc5d00          mov       al,[esi+5DBC28h]
  0051836b  6a00                  push      0
  0051836d  6a00                  push      0
  0051836f  8d542440              lea       edx,[esp+40h]
  00518373  8b0c8550335600        mov       ecx,[eax*4+563350h]
  0051837a  8b44241c              mov       eax,[esp+1Ch]
  0051837e  51                    push      ecx
  0051837f  8b4c2424              mov       ecx,[esp+24h]
  00518383  52                    push      edx
  00518384  50                    push      eax
  00518385  51                    push      ecx
  00518386  66896c2458            mov       [esp+58h],bp
  0051838b  897c2450              mov       [esp+50h],edi
  0051838f  e88c3d0000            call      0051C120h
  00518394  83c41c                add       esp,1Ch
  00518397  668344243c48          add       word [esp+3Ch],48h
  0051839d  33d2                  xor       edx,edx
  0051839f  8d4c2434              lea       ecx,[esp+34h]
  005183a3  8a96d0bb5d00          mov       dl,[esi+5DBBD0h]
  005183a9  6a00                  push      0
  005183ab  6a05                  push      5
  005183ad  52                    push      edx
  005183ae  8b542420              mov       edx,[esp+20h]
  005183b2  51                    push      ecx
  005183b3  50                    push      eax
  005183b4  52                    push      edx
  005183b5  89442428              mov       [esp+28h],eax
  005183b9  895c244c              mov       [esp+4Ch],ebx
  005183bd  e8ce3a0000            call      0051BE90h
  005183c2  83c418                add       esp,18h
  005183c5  33c9                  xor       ecx,ecx
  005183c7  8a8e50bb5d00          mov       cl,[esi+5DBB50h]
  005183cd  668344243c18          add       word [esp+3Ch],18h
  005183d3  6a00                  push      0
  005183d5  6a05                  push      5
  005183d7  8d54243c              lea       edx,[esp+3Ch]
  005183db  51                    push      ecx
  005183dc  52                    push      edx
  005183dd  89442420              mov       [esp+20h],eax
  005183e1  50                    push      eax
  005183e2  8b442428              mov       eax,[esp+28h]
  005183e6  50                    push      eax
  005183e7  895c244c              mov       [esp+4Ch],ebx
  005183eb  e8a03a0000            call      0051BE90h
  005183f0  33c9                  xor       ecx,ecx
  005183f2  83c418                add       esp,18h
  005183f5  8a8e90bb5d00          mov       cl,[esi+5DBB90h]
  005183fb  66016c243c            add       [esp+3Ch],bp
  00518400  6aff                  push      0FFFFFFFFh
  00518402  6a00                  push      0
  00518404  8b148d60335600        mov       edx,[ecx*4+563360h]
  0051840b  6a00                  push      0
  0051840d  8d4c2440              lea       ecx,[esp+40h]
  00518411  52                    push      edx
  00518412  8b542424              mov       edx,[esp+24h]
  00518416  51                    push      ecx
  00518417  50                    push      eax
  00518418  52                    push      edx
  00518419  8944242c              mov       [esp+2Ch],eax
  0051841d  897c2450              mov       [esp+50h],edi
  00518421  e8fa3c0000            call      0051C120h
  00518426  668344245a08          add       word [esp+5Ah],8
  0051842c  8944242c              mov       [esp+2Ch],eax
  00518430  a110bc5d00            mov       eax,[5DBC10h]
  00518435  83c41c                add       esp,1Ch
  00518438  46                    inc       esi
  00518439  25ff000000            and       eax,0FFh
  0051843e  3bf0                  cmp       esi,eax
  00518440  0f8c1bffffff          jl        near 00518361h

LAB_00518446:
  00518446  8b4c2410              mov       ecx,[esp+10h]
  0051844a  68783c5600            push      563C78h
  0051844f  51                    push      ecx
  00518450  e84bbf0200            call      005443A0h
  00518455  8b54241c              mov       edx,[esp+1Ch]
  00518459  83c408                add       esp,8
  0051845c  89442410              mov       [esp+10h],eax
  00518460  68783c5600            push      563C78h
  00518465  50                    push      eax
  00518466  52                    push      edx
  00518467  e8d4ce0200            call      00545340h
  0051846c  83c40c                add       esp,0Ch
  0051846f  89442410              mov       [esp+10h],eax

LAB_00518473:
  00518473  68b8000000            push      0B8h
  00518478  6880000000            push      80h
  0051847d  e824d60200            call      00545AA6h
  00518482  a1d0086000            mov       eax,[6008D0h]
  00518487  8b0dd4086000          mov       ecx,[6008D4h]
  0051848d  8b15d8086000          mov       edx,[6008D8h]
  00518493  a320a55f00            mov       [5FA520h],eax
  00518498  66a128496000          mov       ax,[604928h]
  0051849e  890d24a55f00          mov       [5FA524h],ecx
  005184a4  668b0d2a496000        mov       cx,[60492Ah]
  005184ab  891528a55f00          mov       [5FA528h],edx
  005184b1  668b152c496000        mov       dx,[60492Ch]
  005184b8  66a334a55f00          mov       [5FA534h],ax
  005184be  a1e0086000            mov       eax,[6008E0h]
  005184c3  66890d36a55f00        mov       [5FA536h],cx
  005184ca  8b0de4086000          mov       ecx,[6008E4h]
  005184d0  66891538a55f00        mov       [5FA538h],dx
  005184d7  8b15e8086000          mov       edx,[6008E8h]
  005184dd  a3009b5e00            mov       [5E9B00h],eax
  005184e2  a1ec086000            mov       eax,[6008ECh]
  005184e7  890d049b5e00          mov       [5E9B04h],ecx
  005184ed  8b0df0086000          mov       ecx,[6008F0h]
  005184f3  8915089b5e00          mov       [5E9B08h],edx
  005184f9  8b15f4086000          mov       edx,[6008F4h]
  005184ff  a30c9b5e00            mov       [5E9B0Ch],eax
  00518504  a1f8086000            mov       eax,[6008F8h]
  00518509  890d109b5e00          mov       [5E9B10h],ecx
  0051850f  8b0dfc086000          mov       ecx,[6008FCh]
  00518515  33db                  xor       ebx,ebx
  00518517  68e0086000            push      6008E0h
  0051851c  6828496000            push      604928h
  00518521  8915149b5e00          mov       [5E9B14h],edx
  00518527  a3189b5e00            mov       [5E9B18h],eax
  0051852c  890d1c9b5e00          mov       [5E9B1Ch],ecx
  00518532  891dd0086000          mov       [6008D0h],ebx
  00518538  891dd4086000          mov       [6008D4h],ebx
  0051853e  891dd8086000          mov       [6008D8h],ebx
  00518544  66c705284960000004    mov       word [604928h],400h
  0051854d  e8ae3d0000            call      0051C300h
  00518552  a1384a5600            mov       eax,[564A38h]
  00518557  83c408                add       esp,8
  0051855a  bd01000000            mov       ebp,1
  0051855f  3bc3                  cmp       eax,ebx
  00518561  a160e15d00            mov       eax,[5DE160h]
  00518566  c705f408600041000000  mov       dword [6008F4h],41h
  00518570  c705f808600021000000  mov       dword [6008F8h],21h
  0051857a  c705fc08600020030000  mov       dword [6008FCh],320h
  00518584  892d5ce15d00          mov       [5DE15Ch],ebp
  0051858a  751c                  jne       short 005185A8h
  0051858c  83f808                cmp       eax,8
  0051858f  7c17                  jl        short 005185A8h
  00518591  83c0f5                add       eax,0FFFFFFF5h
  00518594  891d54e15d00          mov       [5DE154h],ebx
  0051859a  8d1440                lea       edx,[eax+eax*2]
  0051859d  c1e202                shl       edx,2
  005185a0  891558e15d00          mov       [5DE158h],edx
  005185a6  eb18                  jmp       short 005185C0h

LAB_005185a8:
  005185a8  83c009                add       eax,9
  005185ab  c70554e15d00a0000000  mov       dword [5DE154h],0A0h
  005185b5  8d0440                lea       eax,[eax+eax*2]
  005185b8  c1e002                shl       eax,2
  005185bb  a358e15d00            mov       [5DE158h],eax

LAB_005185c0:
  005185c0  8b542414              mov       edx,[esp+14h]
  005185c4  8d4c2410              lea       ecx,[esp+10h]
  005185c8  83c208                add       edx,8
  005185cb  51                    push      ecx
  005185cc  52                    push      edx
  005185cd  e84e380000            call      0051BE20h
  005185d2  8b0d407b5f00          mov       ecx,[5F7B40h]
  005185d8  b800080000            mov       eax,800h
  005185dd  83c408                add       esp,8
  005185e0  beffffff01            mov       esi,1FFFFFFh
  005185e5  6689442454            mov       [esp+54h],ax
  005185ea  6689442458            mov       [esp+58h],ax
  005185ef  66c74424620400        mov       word [esp+62h],4
  005185f6  89742464              mov       [esp+64h],esi
  005185fa  895c2448              mov       [esp+48h],ebx
  005185fe  895c244c              mov       [esp+4Ch],ebx
  00518602  895c2450              mov       [esp+50h],ebx
  00518606  66895c2456            mov       [esp+56h],bx
  0051860b  66895c245c            mov       [esp+5Ch],bx
  00518610  66895c245e            mov       [esp+5Eh],bx
  00518615  66895c2460            mov       [esp+60h],bx
  0051861a  8b11                  mov       edx,[ecx]
  0051861c  8b4c2414              mov       ecx,[esp+14h]
  00518620  8d442448              lea       eax,[esp+48h]
  00518624  6a0e                  push      0Eh
  00518626  50                    push      eax
  00518627  8b442418              mov       eax,[esp+18h]
  0051862b  52                    push      edx
  0051862c  83c108                add       ecx,8
  0051862f  50                    push      eax
  00518630  51                    push      ecx
  00518631  e8ea3d0000            call      0051C420h
  00518636  83c414                add       esp,14h
  00518639  8d542434              lea       edx,[esp+34h]
  0051863d  89442410              mov       [esp+10h],eax
  00518641  66895c2444            mov       [esp+44h],bx
  00518646  53                    push      ebx
  00518647  53                    push      ebx
  00518648  52                    push      edx
  00518649  50                    push      eax
  0051864a  8b442424              mov       eax,[esp+24h]
  0051864e  c7442444b8465600      mov       dword [esp+44h],5646B8h
  00518656  83c008                add       eax,8
  00518659  89742448              mov       [esp+48h],esi
  0051865d  50                    push      eax
  0051865e  c74424508800b000      mov       dword [esp+50h],0B00088h
  00518666  e885970000            call      00521DF0h
  0051866b  8b542428              mov       edx,[esp+28h]
  0051866f  83c414                add       esp,14h
  00518672  8d4c2434              lea       ecx,[esp+34h]
  00518676  83c208                add       edx,8
  00518679  51                    push      ecx
  0051867a  50                    push      eax
  0051867b  52                    push      edx
  0051867c  8944241c              mov       [esp+1Ch],eax
  00518680  e83b730000            call      0051F9C0h
  00518685  83c40c                add       esp,0Ch
  00518688  89742438              mov       [esp+38h],esi
  0051868c  33ff                  xor       edi,edi
  0051868e  89442410              mov       [esp+10h],eax
  00518692  c7442434c0465600      mov       dword [esp+34h],5646C0h
  0051869a  33f6                  xor       esi,esi

LAB_0051869c:
  0051869c  0fbf052a496000        movsx     eax,word [60492Ah]
  005186a3  8bce                  mov       ecx,esi
  005186a5  66897c2444            mov       [esp+44h],di
  005186aa  2bc8                  sub       ecx,eax
  005186ac  51                    push      ecx
  005186ad  e86ad30200            call      00545A1Ch
  005186b2  8d1480                lea       edx,[eax+eax*4]
  005186b5  b99c000000            mov       ecx,9Ch
  005186ba  8d0450                lea       eax,[eax+edx*2]
  005186bd  0fbf152a496000        movsx     edx,word [60492Ah]
  005186c4  d1e0                  shl       eax,1
  005186c6  c1f80c                sar       eax,0Ch
  005186c9  2bc8                  sub       ecx,eax
  005186cb  8bc6                  mov       eax,esi
  005186cd  2bc2                  sub       eax,edx
  005186cf  66894c243c            mov       [esp+3Ch],cx
  005186d4  50                    push      eax
  005186d5  e83cd30200            call      00545A16h
  005186da  8d0c80                lea       ecx,[eax+eax*4]
  005186dd  53                    push      ebx
  005186de  53                    push      ebx
  005186df  8d1448                lea       edx,[eax+ecx*2]
  005186e2  b8c5000000            mov       eax,0C5h
  005186e7  d1e2                  shl       edx,1
  005186e9  c1fa0c                sar       edx,0Ch
  005186ec  2bc2                  sub       eax,edx
  005186ee  8b542418              mov       edx,[esp+18h]
  005186f2  6689442446            mov       [esp+46h],ax
  005186f7  8b44241c              mov       eax,[esp+1Ch]
  005186fb  8d4c243c              lea       ecx,[esp+3Ch]
  005186ff  83c008                add       eax,8
  00518702  51                    push      ecx
  00518703  52                    push      edx
  00518704  50                    push      eax
  00518705  e8e6960000            call      00521DF0h
  0051870a  83c414                add       esp,14h
  0051870d  81c600040000          add       esi,400h
  00518713  47                    inc       edi
  00518714  81fe00100000          cmp       esi,1000h
  0051871a  89442410              mov       [esp+10h],eax
  0051871e  0f8c78ffffff          jl        near 0051869Ch
  00518724  8b542414              mov       edx,[esp+14h]
  00518728  8d4c2434              lea       ecx,[esp+34h]
  0051872c  51                    push      ecx
  0051872d  83c208                add       edx,8
  00518730  50                    push      eax
  00518731  52                    push      edx
  00518732  e889720000            call      0051F9C0h
  00518737  8b4c2420              mov       ecx,[esp+20h]
  0051873b  83c40c                add       esp,0Ch
  0051873e  89442410              mov       [esp+10h],eax
  00518742  8d442410              lea       eax,[esp+10h]
  00518746  83c108                add       ecx,8
  00518749  50                    push      eax
  0051874a  51                    push      ecx
  0051874b  e860360000            call      0051BDB0h
  00518750  8b15009b5e00          mov       edx,[5E9B00h]
  00518756  a1049b5e00            mov       eax,[5E9B04h]
  0051875b  8b0d089b5e00          mov       ecx,[5E9B08h]
  00518761  8915e0086000          mov       [6008E0h],edx
  00518767  8b150c9b5e00          mov       edx,[5E9B0Ch]
  0051876d  a3e4086000            mov       [6008E4h],eax
  00518772  a1109b5e00            mov       eax,[5E9B10h]
  00518777  890de8086000          mov       [6008E8h],ecx
  0051877d  8b0d149b5e00          mov       ecx,[5E9B14h]
  00518783  8915ec086000          mov       [6008ECh],edx
  00518789  8b15189b5e00          mov       edx,[5E9B18h]
  0051878f  a3f0086000            mov       [6008F0h],eax
  00518794  a11c9b5e00            mov       eax,[5E9B1Ch]
  00518799  890df4086000          mov       [6008F4h],ecx
  0051879f  8b0d20a55f00          mov       ecx,[5FA520h]
  005187a5  8915f8086000          mov       [6008F8h],edx
  005187ab  8b1524a55f00          mov       edx,[5FA524h]
  005187b1  a3fc086000            mov       [6008FCh],eax
  005187b6  a128a55f00            mov       eax,[5FA528h]
  005187bb  83c408                add       esp,8
  005187be  890dd0086000          mov       [6008D0h],ecx
  005187c4  668b0d34a55f00        mov       cx,[5FA534h]
  005187cb  8915d4086000          mov       [6008D4h],edx
  005187d1  668b1536a55f00        mov       dx,[5FA536h]
  005187d8  a3d8086000            mov       [6008D8h],eax
  005187dd  66a138a55f00          mov       ax,[5FA538h]
  005187e3  6a78                  push      78h
  005187e5  68a0000000            push      0A0h
  005187ea  891d5ce15d00          mov       [5DE15Ch],ebx
  005187f0  891d54e15d00          mov       [5DE154h],ebx
  005187f6  891d58e15d00          mov       [5DE158h],ebx
  005187fc  66890d28496000        mov       [604928h],cx
  00518803  6689152a496000        mov       [60492Ah],dx
  0051880a  66a32c496000          mov       [60492Ch],ax
  00518810  e891d20200            call      00545AA6h
  00518815  c7442434386c5600      mov       dword [esp+34h],566C38h
  0051881d  66895c2444            mov       [esp+44h],bx
  00518822  be869b5e00            mov       esi,5E9B86h
  00518827  bf40000000            mov       edi,40h

LAB_0051882c:
  0051882c  8a4601                mov       al,[esi+1]
  0051882f  84c0                  test      al,al
  00518831  7450                  je        short 00518883h
  00518833  8d4c2454              lea       ecx,[esp+54h]
  00518837  8d56da                lea       edx,[esi-26h]
  0051883a  51                    push      ecx
  0051883b  52                    push      edx
  0051883c  e80f6e0000            call      0051F650h
  00518841  83c408                add       esp,8
  00518844  85c0                  test      eax,eax
  00518846  743b                  je        short 00518883h
  00518848  668b442454            mov       ax,[esp+54h]
  0051884d  668b4c2456            mov       cx,[esp+56h]
  00518852  660fb616              movzx     dx,byte [esi]
  00518856  668944243c            mov       [esp+3Ch],ax
  0051885b  66894c243e            mov       [esp+3Eh],cx
  00518860  0fbf46e6              movsx     eax,word [esi-1Ah]
  00518864  52                    push      edx
  00518865  8b542414              mov       edx,[esp+14h]
  00518869  6a05                  push      5
  0051886b  8d4c243c              lea       ecx,[esp+3Ch]
  0051886f  50                    push      eax
  00518870  8b442420              mov       eax,[esp+20h]
  00518874  51                    push      ecx
  00518875  52                    push      edx
  00518876  50                    push      eax
  00518877  e814360000            call      0051BE90h
  0051887c  83c418                add       esp,18h
  0051887f  89442410              mov       [esp+10h],eax

LAB_00518883:
  00518883  83c628                add       esi,28h
  00518886  4f                    dec       edi
  00518887  75a3                  jne       short 0051882Ch
  00518889  8b542410              mov       edx,[esp+10h]
  0051888d  8b442414              mov       eax,[esp+14h]
  00518891  8d4c2434              lea       ecx,[esp+34h]
  00518895  51                    push      ecx
  00518896  52                    push      edx
  00518897  50                    push      eax
  00518898  e823710000            call      0051F9C0h
  0051889d  8b15b4876000          mov       edx,[6087B4h]
  005188a3  8bc8                  mov       ecx,eax
  005188a5  a120905e00            mov       eax,[5E9020h]
  005188aa  894c241c              mov       [esp+1Ch],ecx
  005188ae  c60544a55f0000        mov       byte [5FA544h],0
  005188b5  66892d3aa55f00        mov       [5FA53Ah],bp
  005188bc  c7052ca55f00000e000e  mov       dword [5FA52Ch],0E000E00h
  005188c6  c70540a55f0090465600  mov       dword [5FA540h],564690h
  005188d0  891d5ce05d00          mov       [5DE05Ch],ebx
  005188d6  8b3a                  mov       edi,[edx]
  005188d8  83c40c                add       esp,0Ch
  005188db  81c700000f00          add       edi,0F0000h
  005188e1  3bc3                  cmp       eax,ebx
  005188e3  c744242480915e00      mov       dword [esp+24h],5E9180h
  005188eb  895c2418              mov       [esp+18h],ebx
  005188ef  0f8e66070000          jle       near 0051905Bh
  005188f5  c744241c56945e00      mov       dword [esp+1Ch],5E9456h
  005188fd  c744243048335600      mov       dword [esp+30h],563348h
  00518905  be6cbc5d00            mov       esi,5DBC6Ch
  0051890a  c744242c58495600      mov       dword [esp+2Ch],564958h
  00518912  c744242848495600      mov       dword [esp+28h],564948h

LAB_0051891a:
  0051891a  8b44241c              mov       eax,[esp+1Ch]
  0051891e  8b50fa                mov       edx,[eax-6]
  00518921  f6c680                test      dh,80h
  00518924  752c                  jne       short 00518952h
  00518926  0fbf0d28496000        movsx     ecx,word [604928h]
  0051892d  51                    push      ecx
  0051892e  e8e9d00200            call      00545A1Ch
  00518933  8b542424              mov       edx,[esp+24h]
  00518937  c1f803                sar       eax,3
  0051893a  0fbf0a                movsx     ecx,word [edx]
  0051893d  03c1                  add       eax,ecx
  0051893f  8b0d5ce05d00          mov       ecx,[5DE05Ch]
  00518945  3bc8                  cmp       ecx,eax
  00518947  7d05                  jge       short 0051894Eh
  00518949  a35ce05d00            mov       [5DE05Ch],eax

LAB_0051894e:
  0051894e  8b4c2410              mov       ecx,[esp+10h]

LAB_00518952:
  00518952  a1384a5600            mov       eax,[564A38h]
  00518957  c7055ce15d0001000000  mov       dword [5DE15Ch],1
  00518961  85c0                  test      eax,eax
  00518963  a160e15d00            mov       eax,[5DE160h]
  00518968  7541                  jne       short 005189ABh
  0051896a  83f808                cmp       eax,8
  0051896d  7c3c                  jl        short 005189ABh
  0051896f  83f80a                cmp       eax,0Ah
  00518972  7d19                  jge       short 0051898Dh
  00518974  8b542418              mov       edx,[esp+18h]
  00518978  80e201                and       dl,1
  0051897b  f6da                  neg       dl
  0051897d  1bd2                  sbb       edx,edx
  0051897f  83e2ce                and       edx,0FFFFFFCEh
  00518982  83c219                add       edx,19h
  00518985  891554e15d00          mov       [5DE154h],edx
  0051898b  eb0a                  jmp       short 00518997h

LAB_0051898d:
  0051898d  c70554e15d0000000000  mov       dword [5DE154h],0

LAB_00518997:
  00518997  8b542418              mov       edx,[esp+18h]
  0051899b  83c0f5                add       eax,0FFFFFFF5h
  0051899e  8d0440                lea       eax,[eax+eax*2]
  005189a1  c1e002                shl       eax,2
  005189a4  a358e15d00            mov       [5DE158h],eax
  005189a9  eb6d                  jmp       short 00518A18h

LAB_005189ab:
  005189ab  83f805                cmp       eax,5
  005189ae  7c25                  jl        short 005189D5h
  005189b0  8b542418              mov       edx,[esp+18h]
  005189b4  33db                  xor       ebx,ebx
  005189b6  83fa02                cmp       edx,2
  005189b9  8d6809                lea       ebp,[eax+9]
  005189bc  0f9dc3                setge     bl
  005189bf  4b                    dec       ebx
  005189c0  8d6c6d00              lea       ebp,[ebp+ebp*2]
  005189c4  83e31c                and       ebx,1Ch
  005189c7  83c3f2                add       ebx,0FFFFFFF2h
  005189ca  8d1cab                lea       ebx,[ebx+ebp*4]
  005189cd  891d58e15d00          mov       [5DE158h],ebx
  005189d3  eb13                  jmp       short 005189E8h

LAB_005189d5:
  005189d5  8d5009                lea       edx,[eax+9]
  005189d8  8d1452                lea       edx,[edx+edx*2]
  005189db  c1e202                shl       edx,2
  005189de  891558e15d00          mov       [5DE158h],edx
  005189e4  8b542418              mov       edx,[esp+18h]

LAB_005189e8:
  005189e8  83f809                cmp       eax,9
  005189eb  7c09                  jl        short 005189F6h
  005189ed  8b049528495600        mov       eax,[edx*4+564928h]
  005189f4  eb1d                  jmp       short 00518A13h

LAB_005189f6:
  005189f6  83f805                cmp       eax,5
  005189f9  7c09                  jl        short 00518A04h
  005189fb  8b049538495600        mov       eax,[edx*4+564938h]
  00518a02  eb0f                  jmp       short 00518A13h

LAB_00518a04:
  00518a04  83f801                cmp       eax,1
  00518a07  8b442428              mov       eax,[esp+28h]
  00518a0b  7d04                  jge       short 00518A11h
  00518a0d  8b44242c              mov       eax,[esp+2Ch]

LAB_00518a11:
  00518a11  8b00                  mov       eax,[eax]

LAB_00518a13:
  00518a13  a354e15d00            mov       [5DE154h],eax

LAB_00518a18:
  00518a18  8bc2                  mov       eax,edx
  00518a1a  8b1d20905e00          mov       ebx,[5E9020h]
  00518a20  2401                  and       al,1
  00518a22  f6d8                  neg       al
  00518a24  1bc0                  sbb       eax,eax
  00518a26  80e202                and       dl,2
  00518a29  25d1000000            and       eax,0D1h
  00518a2e  83c005                add       eax,5
  00518a31  f6da                  neg       dl
  00518a33  1bd2                  sbb       edx,edx
  00518a35  668906                mov       [esi],ax
  00518a38  83e21c                and       edx,1Ch
  00518a3b  33c0                  xor       eax,eax
  00518a3d  83fb03                cmp       ebx,3
  00518a40  8b5c241c              mov       ebx,[esp+1Ch]
  00518a44  0f9dc0                setge     al
  00518a47  48                    dec       eax
  00518a48  83e00e                and       eax,0Eh
  00518a4b  66833b00              cmp       word [ebx],0
  00518a4f  8d9402a6000000        lea       edx,[edx+eax+0A6h]
  00518a56  66895602              mov       [esi+2],dx
  00518a5a  7421                  je        short 00518A7Dh
  00518a5c  e8dfe60200            call      00547140h
  00518a61  83e003                and       eax,3
  00518a64  83e802                sub       eax,2
  00518a67  660106                add       [esi],ax
  00518a6a  e8d1e60200            call      00547140h
  00518a6f  8b4c2410              mov       ecx,[esp+10h]
  00518a73  83e003                and       eax,3
  00518a76  83e802                sub       eax,2
  00518a79  66014602              add       [esi+2],ax

LAB_00518a7d:
  00518a7d  8a03                  mov       al,[ebx]
  00518a7f  2401                  and       al,1
  00518a81  f6d8                  neg       al
  00518a83  1bc0                  sbb       eax,eax
  00518a85  25af0051ff            and       eax,0FF5100AFh
  00518a8a  052010cf02            add       eax,2CF1020h
  00518a8f  894618                mov       [esi+18h],eax
  00518a92  668b16                mov       dx,[esi]
  00518a95  668b4602              mov       ax,[esi+2]
  00518a99  6683c20a              add       dx,0Ah
  00518a9d  66051200              add       ax,12h
  00518aa1  668954243c            mov       [esp+3Ch],dx
  00518aa6  668944243e            mov       [esp+3Eh],ax
  00518aab  0fbf974c010000        movsx     edx,word [edi+14Ch]
  00518ab2  0fbf9f48010000        movsx     ebx,word [edi+148h]
  00518ab9  8bc2                  mov       eax,edx
  00518abb  c1e008                shl       eax,8
  00518abe  2bc2                  sub       eax,edx
  00518ac0  99                    cdq
  00518ac1  f7fb                  idiv      ebx
  00518ac3  83f87f                cmp       eax,7Fh
  00518ac6  7f07                  jg        short 00518ACFh
  00518ac8  c1e009                shl       eax,9
  00518acb  0cff                  or        al,0FFh
  00518acd  eb0d                  jmp       short 00518ADCh

LAB_00518acf:
  00518acf  05807f0000            add       eax,7F80h
  00518ad4  c1e011                shl       eax,11h
  00518ad7  0dffff0000            or        eax,0FFFFh

LAB_00518adc:
  00518adc  89442438              mov       [esp+38h],eax
  00518ae0  25ffffff00            and       eax,0FFFFFFh
  00518ae5  894640                mov       [esi+40h],eax
  00518ae8  c7442434206c5600      mov       dword [esp+34h],566C20h
  00518af0  0fbf974c010000        movsx     edx,word [edi+14Ch]
  00518af7  6a00                  push      0
  00518af9  6884000000            push      84h
  00518afe  8d44243c              lea       eax,[esp+3Ch]
  00518b02  52                    push      edx
  00518b03  50                    push      eax
  00518b04  51                    push      ecx
  00518b05  8b4c2428              mov       ecx,[esp+28h]
  00518b09  83c108                add       ecx,8
  00518b0c  51                    push      ecx
  00518b0d  e87e330000            call      0051BE90h
  00518b12  668344245414          add       word [esp+54h],14h
  00518b18  bd00000001            mov       ebp,1000000h
  00518b1d  83c418                add       esp,18h
  00518b20  89442410              mov       [esp+10h],eax
  00518b24  896c2438              mov       [esp+38h],ebp
  00518b28  8d4c2434              lea       ecx,[esp+34h]
  00518b2c  0fbf9748010000        movsx     edx,word [edi+148h]
  00518b33  6a00                  push      0
  00518b35  6a04                  push      4
  00518b37  52                    push      edx
  00518b38  8b542420              mov       edx,[esp+20h]
  00518b3c  51                    push      ecx
  00518b3d  83c208                add       edx,8
  00518b40  50                    push      eax
  00518b41  52                    push      edx
  00518b42  e849330000            call      0051BE90h
  00518b47  8bd8                  mov       ebx,eax
  00518b49  83c418                add       esp,18h
  00518b4c  895c2410              mov       [esp+10h],ebx
  00518b50  0fbf8f4e010000        movsx     ecx,word [edi+14Eh]
  00518b57  8bc1                  mov       eax,ecx
  00518b59  c1e008                shl       eax,8
  00518b5c  2bc1                  sub       eax,ecx
  00518b5e  0fbf8f4a010000        movsx     ecx,word [edi+14Ah]
  00518b65  99                    cdq
  00518b66  f7f9                  idiv      ecx
  00518b68  83f87f                cmp       eax,7Fh
  00518b6b  7f07                  jg        short 00518B74h
  00518b6d  c1e009                shl       eax,9
  00518b70  0cff                  or        al,0FFh
  00518b72  eb0d                  jmp       short 00518B81h

LAB_00518b74:
  00518b74  05807f0000            add       eax,7F80h
  00518b79  c1e011                shl       eax,11h
  00518b7c  0dffff0000            or        eax,0FFFFh

LAB_00518b81:
  00518b81  668344243c18          add       word [esp+3Ch],18h
  00518b87  8b4c2414              mov       ecx,[esp+14h]
  00518b8b  89442438              mov       [esp+38h],eax
  00518b8f  6a00                  push      0
  00518b91  0fbf974e010000        movsx     edx,word [edi+14Eh]
  00518b98  6884000000            push      84h
  00518b9d  8d44243c              lea       eax,[esp+3Ch]
  00518ba1  52                    push      edx
  00518ba2  50                    push      eax
  00518ba3  83c108                add       ecx,8
  00518ba6  53                    push      ebx
  00518ba7  51                    push      ecx
  00518ba8  e8e3320000            call      0051BE90h
  00518bad  668344245414          add       word [esp+54h],14h
  00518bb3  83c418                add       esp,18h
  00518bb6  89442410              mov       [esp+10h],eax
  00518bba  896c2438              mov       [esp+38h],ebp
  00518bbe  0fbf974a010000        movsx     edx,word [edi+14Ah]
  00518bc5  6a00                  push      0
  00518bc7  6a04                  push      4
  00518bc9  52                    push      edx
  00518bca  8b542420              mov       edx,[esp+20h]
  00518bce  8d4c2440              lea       ecx,[esp+40h]
  00518bd2  83c208                add       edx,8
  00518bd5  51                    push      ecx
  00518bd6  50                    push      eax
  00518bd7  52                    push      edx
  00518bd8  e8b3320000            call      0051BE90h
  00518bdd  668b0e                mov       cx,[esi]
  00518be0  668b5602              mov       dx,[esi+2]
  00518be4  83c418                add       esp,18h
  00518be7  6683c10a              add       cx,0Ah
  00518beb  6683c20a              add       dx,0Ah
  00518bef  66894c243c            mov       [esp+3Ch],cx
  00518bf4  6a08                  push      8
  00518bf6  6689542442            mov       [esp+42h],dx
  00518bfb  6a00                  push      0
  00518bfd  8d8f00010000          lea       ecx,[edi+100h]
  00518c03  6a00                  push      0
  00518c05  8d542440              lea       edx,[esp+40h]
  00518c09  51                    push      ecx
  00518c0a  52                    push      edx
  00518c0b  89442424              mov       [esp+24h],eax
  00518c0f  50                    push      eax
  00518c10  8b44242c              mov       eax,[esp+2Ch]
  00518c14  896c2450              mov       [esp+50h],ebp
  00518c18  83c008                add       eax,8
  00518c1b  c744244c286c5600      mov       dword [esp+4Ch],566C28h
  00518c23  50                    push      eax
  00518c24  e827360000            call      0051C250h
  00518c29  8b542430              mov       edx,[esp+30h]
  00518c2d  83c41c                add       esp,1Ch
  00518c30  668344243c10          add       word [esp+3Ch],10h
  00518c36  668344243e08          add       word [esp+3Eh],8
  00518c3c  6aff                  push      0FFFFFFFFh
  00518c3e  6a00                  push      0
  00518c40  6a00                  push      0
  00518c42  8d4c2440              lea       ecx,[esp+40h]
  00518c46  680c4a5600            push      564A0Ch
  00518c4b  51                    push      ecx
  00518c4c  83c208                add       edx,8
  00518c4f  50                    push      eax
  00518c50  52                    push      edx
  00518c51  8944242c              mov       [esp+2Ch],eax
  00518c55  c7442450186c5600      mov       dword [esp+50h],566C18h
  00518c5d  e8be340000            call      0051C120h
  00518c62  8b542430              mov       edx,[esp+30h]
  00518c66  83c41c                add       esp,1Ch
  00518c69  668344243c2c          add       word [esp+3Ch],2Ch
  00518c6f  8d4c2434              lea       ecx,[esp+34h]
  00518c73  6aff                  push      0FFFFFFFFh
  00518c75  6a00                  push      0
  00518c77  6a00                  push      0
  00518c79  680c4a5600            push      564A0Ch
  00518c7e  51                    push      ecx
  00518c7f  83c208                add       edx,8
  00518c82  50                    push      eax
  00518c83  52                    push      edx
  00518c84  8944242c              mov       [esp+2Ch],eax
  00518c88  e893340000            call      0051C120h
  00518c8d  83c41c                add       esp,1Ch
  00518c90  668b4e02              mov       cx,[esi+2]
  00518c94  89442410              mov       [esp+10h],eax
  00518c98  668b06                mov       ax,[esi]
  00518c9b  8d542410              lea       edx,[esp+10h]
  00518c9f  66050a00              add       ax,0Ah
  00518ca3  6683c11a              add       cx,1Ah
  00518ca7  668944243c            mov       [esp+3Ch],ax
  00518cac  8b442414              mov       eax,[esp+14h]
  00518cb0  83c008                add       eax,8
  00518cb3  52                    push      edx
  00518cb4  50                    push      eax
  00518cb5  66c744244c0000        mov       word [esp+4Ch],0
  00518cbc  c744243cb8485600      mov       dword [esp+3Ch],5648B8h
  00518cc4  66894c2446            mov       [esp+46h],cx
  00518cc9  e852310000            call      0051BE20h
  00518cce  8b442418              mov       eax,[esp+18h]
  00518cd2  83c408                add       esp,8
  00518cd5  bb00495600            mov       ebx,564900h
  00518cda  33ed                  xor       ebp,ebp

LAB_00518cdc:
  00518cdc  8b0b                  mov       ecx,[ebx]
  00518cde  8b9740010000          mov       edx,[edi+140h]
  00518ce4  85ca                  test      edx,ecx
  00518ce6  741c                  je        short 00518D04h
  00518ce8  55                    push      ebp
  00518ce9  8d542438              lea       edx,[esp+38h]
  00518ced  55                    push      ebp
  00518cee  52                    push      edx
  00518cef  50                    push      eax
  00518cf0  8b442424              mov       eax,[esp+24h]
  00518cf4  83c008                add       eax,8
  00518cf7  50                    push      eax
  00518cf8  e8f3900000            call      00521DF0h
  00518cfd  83c414                add       esp,14h
  00518d00  89442410              mov       [esp+10h],eax

LAB_00518d04:
  00518d04  668344243c08          add       word [esp+3Ch],8
  00518d0a  83c304                add       ebx,4
  00518d0d  66ff442444            inc       word [esp+44h]
  00518d12  81fb24495600          cmp       ebx,564924h
  00518d18  7cc2                  jl        short 00518CDCh
  00518d1a  8b542414              mov       edx,[esp+14h]
  00518d1e  8d4c2410              lea       ecx,[esp+10h]
  00518d22  83c208                add       edx,8
  00518d25  51                    push      ecx
  00518d26  52                    push      edx
  00518d27  e884300000            call      0051BDB0h
  00518d2c  8b54241c              mov       edx,[esp+1Ch]
  00518d30  8b4c2418              mov       ecx,[esp+18h]
  00518d34  83c408                add       esp,8
  00518d37  8d442434              lea       eax,[esp+34h]
  00518d3b  83c208                add       edx,8
  00518d3e  66896c2444            mov       [esp+44h],bp
  00518d43  50                    push      eax
  00518d44  51                    push      ecx
  00518d45  52                    push      edx
  00518d46  e8756c0000            call      0051F9C0h
  00518d4b  8b542420              mov       edx,[esp+20h]
  00518d4f  83c40c                add       esp,0Ch
  00518d52  8d4efc                lea       ecx,[esi-4]
  00518d55  83c208                add       edx,8
  00518d58  51                    push      ecx
  00518d59  50                    push      eax
  00518d5a  52                    push      edx
  00518d5b  8944241c              mov       [esp+1Ch],eax
  00518d5f  e8dcc50200            call      00545340h
  00518d64  8bc8                  mov       ecx,eax
  00518d66  a118876000            mov       eax,[608718h]
  00518d6b  894c241c              mov       [esp+1Ch],ecx
  00518d6f  892d54e15d00          mov       [5DE154h],ebp
  00518d75  892d58e15d00          mov       [5DE158h],ebp
  00518d7b  892d5ce15d00          mov       [5DE15Ch],ebp
  00518d81  8b10                  mov       edx,[eax]
  00518d83  83c40c                add       esp,0Ch
  00518d86  3bd5                  cmp       edx,ebp
  00518d88  8b6c241c              mov       ebp,[esp+1Ch]
  00518d8c  0f856d020000          jne       near 00518FFFh
  00518d92  8b856affffff          mov       eax,[ebp-96h]
  00518d98  f6c440                test      ah,40h
  00518d9b  0f85d0010000          jne       near 00518F71h
  00518da1  668b9574ffffff        mov       dx,[ebp-8Ch]
  00518da8  33c0                  xor       eax,eax
  00518daa  8a856effffff          mov       al,[ebp-92h]
  00518db0  52                    push      edx
  00518db1  0fbf5584              movsx     edx,word [ebp-7Ch]
  00518db5  c1e002                shl       eax,2
  00518db8  8d5d86                lea       ebx,[ebp-7Ah]
  00518dbb  52                    push      edx
  00518dbc  8b9000985e00          mov       edx,[eax+5E9800h]
  00518dc2  8b8080965e00          mov       eax,[eax+5E9680h]
  00518dc8  53                    push      ebx
  00518dc9  52                    push      edx
  00518dca  0fbf5582              movsx     edx,word [ebp-7Eh]
  00518dce  8b1490                mov       edx,[eax+edx*4]
  00518dd1  8b442424              mov       eax,[esp+24h]
  00518dd5  52                    push      edx
  00518dd6  51                    push      ecx
  00518dd7  50                    push      eax
  00518dd8  e813440000            call      0051D1F0h
  00518ddd  8bc8                  mov       ecx,eax
  00518ddf  83c41c                add       esp,1Ch
  00518de2  894c2410              mov       [esp+10h],ecx
  00518de6  8a87b9020000          mov       al,[edi+2B9h]
  00518dec  84c0                  test      al,al
  00518dee  0f84a0000000          je        near 00518E94h
  00518df4  8d4c2454              lea       ecx,[esp+54h]
  00518df8  51                    push      ecx
  00518df9  53                    push      ebx
  00518dfa  e851680000            call      0051F650h
  00518dff  83c408                add       esp,8
  00518e02  85c0                  test      eax,eax
  00518e04  0f8486000000          je        near 00518E90h
  00518e0a  f7856affffff00000100  test      dword [ebp-96h],10000h
  00518e14  751c                  jne       short 00518E32h
  00518e16  8b442424              mov       eax,[esp+24h]
  00518e1a  80780808              cmp       byte [eax+8],8
  00518e1e  7412                  je        short 00518E32h
  00518e20  33d2                  xor       edx,edx
  00518e22  8a5009                mov       dl,[eax+9]
  00518e25  8d04d598475600        lea       eax,[edx*8+564798h]
  00518e2c  89442434              mov       [esp+34h],eax
  00518e30  eb08                  jmp       short 00518E3Ah

LAB_00518e32:
  00518e32  c7442434b8475600      mov       dword [esp+34h],5647B8h

LAB_00518e3a:
  00518e3a  8b4c2454              mov       ecx,[esp+54h]
  00518e3e  668b542456            mov       dx,[esp+56h]
  00518e43  83c1f4                add       ecx,0FFFFFFF4h
  00518e46  668954243e            mov       [esp+3Eh],dx
  00518e4b  8b542414              mov       edx,[esp+14h]
  00518e4f  66894c243c            mov       [esp+3Ch],cx
  00518e54  8b4c2410              mov       ecx,[esp+10h]
  00518e58  6a00                  push      0
  00518e5a  8d442438              lea       eax,[esp+38h]
  00518e5e  6a00                  push      0
  00518e60  50                    push      eax
  00518e61  83c20c                add       edx,0Ch
  00518e64  51                    push      ecx
  00518e65  52                    push      edx
  00518e66  e8858f0000            call      00521DF0h
  00518e6b  8b542428              mov       edx,[esp+28h]
  00518e6f  83c414                add       esp,14h
  00518e72  8d4c2434              lea       ecx,[esp+34h]
  00518e76  83c20c                add       edx,0Ch
  00518e79  51                    push      ecx
  00518e7a  50                    push      eax
  00518e7b  52                    push      edx
  00518e7c  8944241c              mov       [esp+1Ch],eax
  00518e80  e83b6b0000            call      0051F9C0h
  00518e85  8bc8                  mov       ecx,eax
  00518e87  83c40c                add       esp,0Ch
  00518e8a  894c2410              mov       [esp+10h],ecx
  00518e8e  eb04                  jmp       short 00518E94h

LAB_00518e90:
  00518e90  8b4c2410              mov       ecx,[esp+10h]

LAB_00518e94:
  00518e94  8a87ba020000          mov       al,[edi+2BAh]
  00518e9a  84c0                  test      al,al
  00518e9c  7464                  je        short 00518F02h
  00518e9e  8d442448              lea       eax,[esp+48h]
  00518ea2  6a0a                  push      0Ah
  00518ea4  50                    push      eax
  00518ea5  53                    push      ebx
  00518ea6  e835c40100            call      005352E0h
  00518eab  8b4c2458              mov       ecx,[esp+58h]
  00518eaf  8b44243c              mov       eax,[esp+3Ch]
  00518eb3  8b1514985e00          mov       edx,[5E9814h]
  00518eb9  81c100200300          add       ecx,32000h
  00518ebf  894c2458              mov       [esp+58h],ecx
  00518ec3  8b0d60995e00          mov       ecx,[5E9960h]
  00518ec9  c1e106                shl       ecx,6
  00518ecc  66894c246c            mov       [esp+6Ch],cx
  00518ed1  668b08                mov       cx,[eax]
  00518ed4  a1407b5f00            mov       eax,[5F7B40h]
  00518ed9  83c40c                add       esp,0Ch
  00518edc  89542464              mov       [esp+64h],edx
  00518ee0  8d542448              lea       edx,[esp+48h]
  00518ee4  51                    push      ecx
  00518ee5  8b480c                mov       ecx,[eax+0Ch]
  00518ee8  8b442418              mov       eax,[esp+18h]
  00518eec  52                    push      edx
  00518eed  8b542418              mov       edx,[esp+18h]
  00518ef1  51                    push      ecx
  00518ef2  52                    push      edx
  00518ef3  50                    push      eax
  00518ef4  e827350000            call      0051C420h
  00518ef9  8bc8                  mov       ecx,eax
  00518efb  83c414                add       esp,14h
  00518efe  894c2410              mov       [esp+10h],ecx

LAB_00518f02:
  00518f02  8b45a6                mov       eax,[ebp-5Ah]
  00518f05  85c0                  test      eax,eax
  00518f07  7468                  je        short 00518F71h
  00518f09  8d4c2448              lea       ecx,[esp+48h]
  00518f0d  6a0a                  push      0Ah
  00518f0f  51                    push      ecx
  00518f10  53                    push      ebx
  00518f11  e8cac30100            call      005352E0h
  00518f16  83c40c                add       esp,0Ch
  00518f19  8d856affffff          lea       eax,[ebp-96h]
  00518f1f  50                    push      eax
  00518f20  e89bfc0100            call      00538BC0h
  00518f25  8b5c2450              mov       ebx,[esp+50h]
  00518f29  8b4c2418              mov       ecx,[esp+18h]
  00518f2d  83c019                add       eax,19h
  00518f30  83c404                add       esp,4
  00518f33  c1e00c                shl       eax,0Ch
  00518f36  03d8                  add       ebx,eax
  00518f38  8b442410              mov       eax,[esp+10h]
  00518f3c  6a00                  push      0
  00518f3e  8d54244c              lea       edx,[esp+4Ch]
  00518f42  6a00                  push      0
  00518f44  52                    push      edx
  00518f45  50                    push      eax
  00518f46  51                    push      ecx
  00518f47  895c2460              mov       [esp+60h],ebx
  00518f4b  c744246800100010      mov       dword [esp+68h],10001000h
  00518f53  c744247800000001      mov       dword [esp+78h],1000000h
  00518f5b  c684248000000000      mov       byte [esp+80h],0
  00518f63  e8787b0000            call      00520AE0h
  00518f68  8bc8                  mov       ecx,eax
  00518f6a  83c414                add       esp,14h
  00518f6d  894c2410              mov       [esp+10h],ecx

LAB_00518f71:
  00518f71  8b45fa                mov       eax,[ebp-6]
  00518f74  f6c490                test      ah,90h
  00518f77  7559                  jne       short 00518FD2h
  00518f79  8b856affffff          mov       eax,[ebp-96h]
  00518f7f  f6c410                test      ah,10h
  00518f82  754e                  jne       short 00518FD2h
  00518f84  8b45ce                mov       eax,[ebp-32h]
  00518f87  8b5586                mov       edx,[ebp-7Ah]
  00518f8a  a324a55f00            mov       [5FA524h],eax
  00518f8f  8b442414              mov       eax,[esp+14h]
  00518f93  6a00                  push      0
  00518f95  6a00                  push      0
  00518f97  891520a55f00          mov       [5FA520h],edx
  00518f9d  8b558e                mov       edx,[ebp-72h]
  00518fa0  6820a55f00            push      5FA520h
  00518fa5  83c014                add       eax,14h
  00518fa8  51                    push      ecx
  00518fa9  50                    push      eax
  00518faa  891528a55f00          mov       [5FA528h],edx
  00518fb0  c7054c4a560000000000  mov       dword [564A4Ch],0
  00518fba  e8217b0000            call      00520AE0h
  00518fbf  8bc8                  mov       ecx,eax
  00518fc1  83c414                add       esp,14h
  00518fc4  894c2410              mov       [esp+10h],ecx
  00518fc8  c7054c4a560001000000  mov       dword [564A4Ch],1

LAB_00518fd2:
  00518fd2  f7856affffff00000008  test      dword [ebp-96h],8000000h
  00518fdc  7421                  je        short 00518FFFh
  00518fde  8b542424              mov       edx,[esp+24h]
  00518fe2  57                    push      edi
  00518fe3  8d856affffff          lea       eax,[ebp-96h]
  00518fe9  52                    push      edx
  00518fea  50                    push      eax
  00518feb  8b442420              mov       eax,[esp+20h]
  00518fef  51                    push      ecx
  00518ff0  50                    push      eax
  00518ff1  e8ba790100            call      005309B0h
  00518ff6  8bc8                  mov       ecx,eax
  00518ff8  83c414                add       esp,14h
  00518ffb  894c2410              mov       [esp+10h],ecx

LAB_00518fff:
  00518fff  8b5c2428              mov       ebx,[esp+28h]
  00519003  ba04000000            mov       edx,4
  00519008  8b442418              mov       eax,[esp+18h]
  0051900c  03da                  add       ebx,edx
  0051900e  895c2428              mov       [esp+28h],ebx
  00519012  8b5c242c              mov       ebx,[esp+2Ch]
  00519016  03da                  add       ebx,edx
  00519018  8b1520905e00          mov       edx,[5E9020h]
  0051901e  81c59c000000          add       ebp,9Ch
  00519024  895c242c              mov       [esp+2Ch],ebx
  00519028  8b5c2430              mov       ebx,[esp+30h]
  0051902c  896c241c              mov       [esp+1Ch],ebp
  00519030  8b6c2424              mov       ebp,[esp+24h]
  00519034  40                    inc       eax
  00519035  81c6c8080000          add       esi,8C8h
  0051903b  83c302                add       ebx,2
  0051903e  83c54c                add       ebp,4Ch
  00519041  81c700200000          add       edi,2000h
  00519047  3bc2                  cmp       eax,edx
  00519049  89442418              mov       [esp+18h],eax
  0051904d  895c2430              mov       [esp+30h],ebx
  00519051  896c2424              mov       [esp+24h],ebp
  00519055  0f8cbff8ffff          jl        near 0051891Ah

LAB_0051905b:
  0051905b  a02be05d00            mov       al,[5DE02Bh]
  00519060  84c0                  test      al,al
  00519062  742e                  je        short 00519092h
  00519064  8b1528e05d00          mov       edx,[5DE028h]
  0051906a  8b442414              mov       eax,[esp+14h]
  0051906e  6800010000            push      100h
  00519073  33db                  xor       ebx,ebx
  00519075  6840010000            push      140h
  0051907a  53                    push      ebx
  0051907b  53                    push      ebx
  0051907c  52                    push      edx
  0051907d  83c008                add       eax,8
  00519080  51                    push      ecx
  00519081  50                    push      eax
  00519082  e8b9680000            call      0051F940h
  00519087  8bc8                  mov       ecx,eax
  00519089  83c41c                add       esp,1Ch
  0051908c  894c2410              mov       [esp+10h],ecx
  00519090  eb02                  jmp       short 00519094h

LAB_00519092:
  00519092  33db                  xor       ebx,ebx

LAB_00519094:
  00519094  66a12a496000          mov       ax,[60492Ah]
  0051909a  bd01000000            mov       ebp,1
  0051909f  0500010000            add       eax,100h
  005190a4  c1f809                sar       eax,9
  005190a7  83e007                and       eax,7
  005190aa  0fbfd0                movsx     edx,ax
  005190ad  668944245e            mov       [esp+5Eh],ax
  005190b2  33c0                  xor       eax,eax
  005190b4  8b3c95d8455600        mov       edi,[edx*4+5645D8h]
  005190bb  668b045548455600      mov       ax,[edx*2+564548h]
  005190c3  8bf0                  mov       esi,eax
  005190c5  668b17                mov       dx,[edi]
  005190c8  81e6ff000000          and       esi,0FFh
  005190ce  6689542462            mov       [esp+62h],dx
  005190d3  8b15d0086000          mov       edx,[6008D0h]
  005190d9  f7da                  neg       edx
  005190db  c1fa0c                sar       edx,0Ch
  005190de  81c200200000          add       edx,2000h
  005190e4  83c702                add       edi,2
  005190e7  c1fa09                sar       edx,9
  005190ea  03d6                  add       edx,esi
  005190ec  897c241c              mov       [esp+1Ch],edi
  005190f0  89542448              mov       [esp+48h],edx
  005190f4  8b15d8086000          mov       edx,[6008D8h]
  005190fa  f7da                  neg       edx
  005190fc  c1fa0c                sar       edx,0Ch
  005190ff  81c200200000          add       edx,2000h
  00519105  66c74424600700        mov       word [esp+60h],7
  0051910c  c1fa09                sar       edx,9
  0051910f  c1f808                sar       eax,8
  00519112  03d0                  add       edx,eax
  00519114  89542450              mov       [esp+50h],edx

LAB_00519118:
  00519118  66ba0700              mov       dx,7
  0051911c  b820000000            mov       eax,20h
  00519121  668954245c            mov       [esp+5Ch],dx
  00519126  eb05                  jmp       short 0051912Dh

LAB_00519128:
  00519128  b820000000            mov       eax,20h

LAB_0051912d:
  0051912d  0fbf742460            movsx     esi,word [esp+60h]
  00519132  0fbfd2                movsx     edx,dx
  00519135  c1e608                shl       esi,8
  00519138  0bf2                  or        esi,edx
  0051913a  0fbf542462            movsx     edx,word [esp+62h]
  0051913f  3bd6                  cmp       edx,esi
  00519141  7515                  jne       short 00519158h
  00519143  660fb607              movzx     ax,byte [edi]
  00519147  83c702                add       edi,2
  0051914a  6689442462            mov       [esp+62h],ax
  0051914f  897c241c              mov       [esp+1Ch],edi
  00519153  e90c040000            jmp       00519564h

LAB_00519158:
  00519158  6639442448            cmp       [esp+48h],ax
  0051915d  0f8301040000          jae       near 00519564h
  00519163  6639442450            cmp       [esp+50h],ax
  00519168  0f83f6030000          jae       near 00519564h
  0051916e  8b542450              mov       edx,[esp+50h]
  00519172  a1b4876000            mov       eax,[6087B4h]
  00519177  8b742448              mov       esi,[esp+48h]
  0051917b  81c2a0380000          add       edx,38A0h
  00519181  8b00                  mov       eax,[eax]
  00519183  c1e205                shl       edx,5
  00519186  03d6                  add       edx,esi
  00519188  8b3490                mov       esi,[eax+edx*4]
  0051918b  3bf3                  cmp       esi,ebx
  0051918d  0f84d1020000          je        near 00519464h

LAB_00519193:
  00519193  8b06                  mov       eax,[esi]
  00519195  f6c440                test      ah,40h
  00519198  0f85b7020000          jne       near 00519455h
  0051919e  f6463680              test      byte [esi+36h],80h
  005191a2  0f85ad020000          jne       near 00519455h
  005191a8  f6c480                test      ah,80h
  005191ab  0f84de010000          je        near 0051938Fh
  005191b1  8b1518876000          mov       edx,[608718h]
  005191b7  391a                  cmp       [edx],ebx
  005191b9  0f8540020000          jne       near 005193FFh
  005191bf  a900000800            test      eax,80000h
  005191c4  7429                  je        short 005191EFh
  005191c6  8b15407b5f00          mov       edx,[5F7B40h]
  005191cc  8d461c                lea       eax,[esi+1Ch]
  005191cf  6a0c                  push      0Ch
  005191d1  50                    push      eax
  005191d2  8b4204                mov       eax,[edx+4]
  005191d5  50                    push      eax
  005191d6  51                    push      ecx
  005191d7  8b4c2424              mov       ecx,[esp+24h]
  005191db  51                    push      ecx
  005191dc  e83f320000            call      0051C420h
  005191e1  8bc8                  mov       ecx,eax
  005191e3  83c414                add       esp,14h
  005191e6  894c2410              mov       [esp+10h],ecx
  005191ea  e966020000            jmp       00519455h

LAB_005191ef:
  005191ef  a900001000            test      eax,100000h
  005191f4  7462                  je        short 00519258h
  005191f6  8a4604                mov       al,[esi+4]
  005191f9  8a15c4de5e00          mov       dl,[5EDEC4h]
  005191ff  3ac2                  cmp       al,dl
  00519201  7310                  jae       short 00519213h
  00519203  8b1588a55f00          mov       edx,[5FA588h]
  00519209  25ff000000            and       eax,0FFh
  0051920e  8b0482                mov       eax,[edx+eax*4]
  00519211  eb1b                  jmp       short 0051922Eh

LAB_00519213:
  00519213  33db                  xor       ebx,ebx
  00519215  25ff000000            and       eax,0FFh
  0051921a  8ada                  mov       bl,dl
  0051921c  8b1580076000          mov       edx,[600780h]
  00519222  2bc3                  sub       eax,ebx
  00519224  bd01000000            mov       ebp,1
  00519229  33db                  xor       ebx,ebx
  0051922b  8b0482                mov       eax,[edx+eax*4]

LAB_0051922e:
  0051922e  668b560a              mov       dx,[esi+0Ah]
  00519232  52                    push      edx
  00519233  8d561c                lea       edx,[esi+1Ch]
  00519236  52                    push      edx
  00519237  50                    push      eax
  00519238  0fbf462e              movsx     eax,word [esi+2Eh]
  0051923c  51                    push      ecx
  0051923d  8b4c2424              mov       ecx,[esp+24h]
  00519241  8d1481                lea       edx,[ecx+eax*4]
  00519244  52                    push      edx
  00519245  e8b6540000            call      0051E700h
  0051924a  8bc8                  mov       ecx,eax
  0051924c  83c414                add       esp,14h
  0051924f  894c2410              mov       [esp+10h],ecx
  00519253  e9fd010000            jmp       00519455h

LAB_00519258:
  00519258  a900004400            test      eax,440000h
  0051925d  0f849c010000          je        near 005193FFh
  00519263  c7052ca55f00000e000e  mov       dword [5FA52Ch],0E000E00h
  0051926d  668b560a              mov       dx,[esi+0Ah]
  00519271  33c0                  xor       eax,eax
  00519273  52                    push      edx
  00519274  8a4604                mov       al,[esi+4]
  00519277  8d7e1c                lea       edi,[esi+1Ch]
  0051927a  0fbf561a              movsx     edx,word [esi+1Ah]
  0051927e  c1e002                shl       eax,2
  00519281  52                    push      edx
  00519282  57                    push      edi
  00519283  8b90a0965e00          mov       edx,[eax+5E96A0h]
  00519289  8b8040965e00          mov       eax,[eax+5E9640h]
  0051928f  52                    push      edx
  00519290  0fbf5618              movsx     edx,word [esi+18h]
  00519294  8b1490                mov       edx,[eax+edx*4]
  00519297  8b442424              mov       eax,[esp+24h]
  0051929b  52                    push      edx
  0051929c  51                    push      ecx
  0051929d  50                    push      eax
  0051929e  e84d3f0000            call      0051D1F0h
  005192a3  8bc8                  mov       ecx,eax
  005192a5  a1a8335600            mov       eax,[5633A8h]
  005192aa  83c41c                add       esp,1Ch
  005192ad  3bc3                  cmp       eax,ebx
  005192af  894c2410              mov       [esp+10h],ecx
  005192b3  0f8546010000          jne       near 005193FFh
  005192b9  a020e05d00            mov       al,[5DE020h]
  005192be  84c0                  test      al,al
  005192c0  0f8439010000          je        near 005193FFh
  005192c6  8d4c2454              lea       ecx,[esp+54h]
  005192ca  51                    push      ecx
  005192cb  57                    push      edi
  005192cc  e87f630000            call      0051F650h
  005192d1  83c408                add       esp,8
  005192d4  85c0                  test      eax,eax
  005192d6  0f841f010000          je        near 005193FBh
  005192dc  8b542454              mov       edx,[esp+54h]
  005192e0  668b442456            mov       ax,[esp+56h]
  005192e5  83c2f4                add       edx,0FFFFFFF4h
  005192e8  668944243e            mov       [esp+3Eh],ax
  005192ed  33c0                  xor       eax,eax
  005192ef  c744243800000001      mov       dword [esp+38h],1000000h
  005192f7  c7442434306c5600      mov       dword [esp+34h],566C30h
  005192ff  668954243c            mov       [esp+3Ch],dx
  00519304  8a4608                mov       al,[esi+8]
  00519307  33c9                  xor       ecx,ecx
  00519309  53                    push      ebx
  0051930a  6a05                  push      5
  0051930c  8d3cc0                lea       edi,[eax+eax*8]
  0051930f  8b442418              mov       eax,[esp+18h]
  00519313  c1e705                shl       edi,5
  00519316  81c760a55e00          add       edi,5EA560h
  0051931c  8d54243c              lea       edx,[esp+3Ch]
  00519320  668b4f44              mov       cx,[edi+44h]
  00519324  51                    push      ecx
  00519325  8b4c2420              mov       ecx,[esp+20h]
  00519329  52                    push      edx
  0051932a  83c108                add       ecx,8
  0051932d  50                    push      eax
  0051932e  51                    push      ecx
  0051932f  e85c2b0000            call      0051BE90h
  00519334  668344245608          add       word [esp+56h],8
  0051933a  83c418                add       esp,18h
  0051933d  89442410              mov       [esp+10h],eax
  00519341  8d4c2434              lea       ecx,[esp+34h]
  00519345  0fbf574c              movsx     edx,word [edi+4Ch]
  00519349  53                    push      ebx
  0051934a  6a05                  push      5
  0051934c  52                    push      edx
  0051934d  8b542420              mov       edx,[esp+20h]
  00519351  51                    push      ecx
  00519352  83c208                add       edx,8
  00519355  50                    push      eax
  00519356  52                    push      edx
  00519357  e8342b0000            call      0051BE90h
  0051935c  668344245608          add       word [esp+56h],8
  00519362  83c418                add       esp,18h
  00519365  89442410              mov       [esp+10h],eax
  00519369  8d542434              lea       edx,[esp+34h]
  0051936d  0fbf4f4e              movsx     ecx,word [edi+4Eh]
  00519371  53                    push      ebx
  00519372  6a05                  push      5
  00519374  51                    push      ecx
  00519375  52                    push      edx
  00519376  50                    push      eax
  00519377  8b442428              mov       eax,[esp+28h]
  0051937b  83c008                add       eax,8
  0051937e  50                    push      eax
  0051937f  e80c2b0000            call      0051BE90h
  00519384  8bc8                  mov       ecx,eax
  00519386  83c418                add       esp,18h
  00519389  894c2410              mov       [esp+10h],ecx
  0051938d  eb70                  jmp       short 005193FFh

LAB_0051938f:
  0051938f  a900001000            test      eax,100000h
  00519394  7431                  je        short 005193C7h
  00519396  c7052ca55f0000070007  mov       dword [5FA52Ch],7000700h
  005193a0  668b560a              mov       dx,[esi+0Ah]
  005193a4  52                    push      edx
  005193a5  8b542418              mov       edx,[esp+18h]
  005193a9  8d461c                lea       eax,[esi+1Ch]
  005193ac  53                    push      ebx
  005193ad  50                    push      eax
  005193ae  51                    push      ecx
  005193af  0fbf4e2e              movsx     ecx,word [esi+2Eh]
  005193b3  8d048a                lea       eax,[edx+ecx*4]
  005193b6  50                    push      eax
  005193b7  e824770000            call      00520AE0h
  005193bc  8bc8                  mov       ecx,eax
  005193be  83c414                add       esp,14h
  005193c1  894c2410              mov       [esp+10h],ecx
  005193c5  eb38                  jmp       short 005193FFh

LAB_005193c7:
  005193c7  a900000800            test      eax,80000h
  005193cc  7431                  je        short 005193FFh
  005193ce  c7052ca55f0000070007  mov       dword [5FA52Ch],7000700h
  005193d8  668b560a              mov       dx,[esi+0Ah]
  005193dc  52                    push      edx
  005193dd  8d461c                lea       eax,[esi+1Ch]
  005193e0  53                    push      ebx
  005193e1  50                    push      eax
  005193e2  51                    push      ecx
  005193e3  8b4c2424              mov       ecx,[esp+24h]
  005193e7  83c1ec                add       ecx,0FFFFFFECh
  005193ea  51                    push      ecx
  005193eb  e8f0760000            call      00520AE0h
  005193f0  8bc8                  mov       ecx,eax
  005193f2  83c414                add       esp,14h
  005193f5  894c2410              mov       [esp+10h],ecx
  005193f9  eb04                  jmp       short 005193FFh

LAB_005193fb:
  005193fb  8b4c2410              mov       ecx,[esp+10h]

LAB_005193ff:
  005193ff  8b8690000000          mov       eax,[esi+90h]
  00519405  f6c490                test      ah,90h
  00519408  754b                  jne       short 00519455h
  0051940a  8b06                  mov       eax,[esi]
  0051940c  f6c410                test      ah,10h
  0051940f  7544                  jne       short 00519455h
  00519411  8b561c                mov       edx,[esi+1Ch]
  00519414  53                    push      ebx
  00519415  891520a55f00          mov       [5FA520h],edx
  0051941b  8b4664                mov       eax,[esi+64h]
  0051941e  a324a55f00            mov       [5FA524h],eax
  00519423  8b442418              mov       eax,[esp+18h]
  00519427  8b5624                mov       edx,[esi+24h]
  0051942a  53                    push      ebx
  0051942b  6820a55f00            push      5FA520h
  00519430  83c014                add       eax,14h
  00519433  51                    push      ecx
  00519434  50                    push      eax
  00519435  891528a55f00          mov       [5FA528h],edx
  0051943b  891d4c4a5600          mov       [564A4Ch],ebx
  00519441  e89a760000            call      00520AE0h
  00519446  8bc8                  mov       ecx,eax
  00519448  83c414                add       esp,14h
  0051944b  894c2410              mov       [esp+10h],ecx
  0051944f  892d4c4a5600          mov       [564A4Ch],ebp

LAB_00519455:
  00519455  8b7668                mov       esi,[esi+68h]
  00519458  3bf3                  cmp       esi,ebx
  0051945a  0f8533fdffff          jne       near 00519193h
  00519460  8b7c241c              mov       edi,[esp+1Ch]

LAB_00519464:
  00519464  8b542450              mov       edx,[esp+50h]
  00519468  a1b4876000            mov       eax,[6087B4h]
  0051946d  8b742448              mov       esi,[esp+48h]
  00519471  81c280380000          add       edx,3880h
  00519477  8b00                  mov       eax,[eax]
  00519479  c1e205                shl       edx,5
  0051947c  03d6                  add       edx,esi
  0051947e  8b3490                mov       esi,[eax+edx*4]
  00519481  8b1518876000          mov       edx,[608718h]
  00519487  391a                  cmp       [edx],ebx
  00519489  0f85d5000000          jne       near 00519564h
  0051948f  3bf3                  cmp       esi,ebx
  00519491  0f84cd000000          je        near 00519564h

LAB_00519497:
  00519497  8b06                  mov       eax,[esi]
  00519499  f6c440                test      ah,40h
  0051949c  0f85b7000000          jne       near 00519559h
  005194a2  8a4607                mov       al,[esi+7]
  005194a5  83e003                and       eax,3
  005194a8  2bc3                  sub       eax,ebx
  005194aa  747c                  je        short 00519528h
  005194ac  48                    dec       eax
  005194ad  7441                  je        short 005194F0h
  005194af  48                    dec       eax
  005194b0  0f85a3000000          jne       near 00519559h
  005194b6  668b460a              mov       ax,[esi+0Ah]
  005194ba  8d561c                lea       edx,[esi+1Ch]
  005194bd  50                    push      eax
  005194be  52                    push      edx
  005194bf  8b1538086000          mov       edx,[600838h]
  005194c5  33c0                  xor       eax,eax
  005194c7  8a4604                mov       al,[esi+4]
  005194ca  81e2ff000000          and       edx,0FFh
  005194d0  03c2                  add       eax,edx
  005194d2  8b0485c07b5f00        mov       eax,[eax*4+5F7BC0h]
  005194d9  50                    push      eax
  005194da  51                    push      ecx
  005194db  8b4c2424              mov       ecx,[esp+24h]
  005194df  51                    push      ecx
  005194e0  e81b520000            call      0051E700h
  005194e5  8bc8                  mov       ecx,eax
  005194e7  83c414                add       esp,14h
  005194ea  894c2410              mov       [esp+10h],ecx
  005194ee  eb69                  jmp       short 00519559h

LAB_005194f0:
  005194f0  668b560a              mov       dx,[esi+0Ah]
  005194f4  8d461c                lea       eax,[esi+1Ch]
  005194f7  52                    push      edx
  005194f8  50                    push      eax
  005194f9  a138086000            mov       eax,[600838h]
  005194fe  33d2                  xor       edx,edx
  00519500  8a5604                mov       dl,[esi+4]
  00519503  25ff000000            and       eax,0FFh
  00519508  03d0                  add       edx,eax
  0051950a  8b44241c              mov       eax,[esp+1Ch]
  0051950e  8b1495c07b5f00        mov       edx,[edx*4+5F7BC0h]
  00519515  52                    push      edx
  00519516  51                    push      ecx
  00519517  50                    push      eax
  00519518  e8032f0000            call      0051C420h
  0051951d  8bc8                  mov       ecx,eax
  0051951f  83c414                add       esp,14h
  00519522  894c2410              mov       [esp+10h],ecx
  00519526  eb31                  jmp       short 00519559h

LAB_00519528:
  00519528  891d4c4a5600          mov       [564A4Ch],ebx
  0051952e  668b560a              mov       dx,[esi+0Ah]
  00519532  52                    push      edx
  00519533  8b542418              mov       edx,[esp+18h]
  00519537  8d461c                lea       eax,[esi+1Ch]
  0051953a  53                    push      ebx
  0051953b  50                    push      eax
  0051953c  51                    push      ecx
  0051953d  0fbf4e2e              movsx     ecx,word [esi+2Eh]
  00519541  8d048a                lea       eax,[edx+ecx*4]
  00519544  50                    push      eax
  00519545  e896750000            call      00520AE0h
  0051954a  8bc8                  mov       ecx,eax
  0051954c  83c414                add       esp,14h
  0051954f  894c2410              mov       [esp+10h],ecx
  00519553  892d4c4a5600          mov       [564A4Ch],ebp

LAB_00519559:
  00519559  8b7668                mov       esi,[esi+68h]
  0051955c  3bf3                  cmp       esi,ebx
  0051955e  0f8533ffffff          jne       near 00519497h

LAB_00519564:
  00519564  668b54245c            mov       dx,[esp+5Ch]
  00519569  8b742448              mov       esi,[esp+48h]
  0051956d  664a                  dec       dx
  0051956f  4e                    dec       esi
  00519570  663bd3                cmp       dx,bx
  00519573  668954245c            mov       [esp+5Ch],dx
  00519578  89742448              mov       [esp+48h],esi
  0051957c  0f85a6fbffff          jne       near 00519128h
  00519582  668b442460            mov       ax,[esp+60h]
  00519587  83c607                add       esi,7
  0051958a  89742448              mov       [esp+48h],esi
  0051958e  8b742450              mov       esi,[esp+50h]
  00519592  6648                  dec       ax
  00519594  4e                    dec       esi
  00519595  663bc3                cmp       ax,bx
  00519598  6689442460            mov       [esp+60h],ax
  0051959d  89742450              mov       [esp+50h],esi
  005195a1  0f8571fbffff          jne       near 00519118h
  005195a7  a1e02c5f00            mov       eax,[5F2CE0h]
  005195ac  33d2                  xor       edx,edx
  005195ae  895c2448              mov       [esp+48h],ebx
  005195b2  895c244c              mov       [esp+4Ch],ebx
  005195b6  895c2450              mov       [esp+50h],ebx
  005195ba  895c245c              mov       [esp+5Ch],ebx
  005195be  895c2460              mov       [esp+60h],ebx
  005195c2  c744245400100010      mov       dword [esp+54h],10001000h
  005195ca  c744245800100000      mov       dword [esp+58h],1000h
  005195d2  c7442464ffffff00      mov       dword [esp+64h],0FFFFFFh
  005195da  8a5008                mov       dl,[eax+8]
  005195dd  a1cc975e00            mov       eax,[5E97CCh]
  005195e2  8b8080000000          mov       eax,[eax+80h]
  005195e8  8b3490                mov       esi,[eax+edx*4]
  005195eb  8b1518876000          mov       edx,[608718h]
  005195f1  8974241c              mov       [esp+1Ch],esi
  005195f5  391a                  cmp       [edx],ebx
  005195f7  0f8506010000          jne       near 00519703h
  005195fd  33c0                  xor       eax,eax
  005195ff  a032086000            mov       al,[600832h]
  00519604  8d3c85c07b5f00        lea       edi,[eax*4+5F7BC0h]
  0051960b  8a06                  mov       al,[esi]
  0051960d  3cff                  cmp       al,0FFh
  0051960f  7429                  je        short 0051963Ah

LAB_00519611:
  00519611  25ff000000            and       eax,0FFh
  00519616  8d542448              lea       edx,[esp+48h]
  0051961a  52                    push      edx
  0051961b  8b0487                mov       eax,[edi+eax*4]
  0051961e  50                    push      eax
  0051961f  51                    push      ecx
  00519620  8b4c2420              mov       ecx,[esp+20h]
  00519624  51                    push      ecx
  00519625  e816640000            call      0051FA40h
  0051962a  83c410                add       esp,10h
  0051962d  8bc8                  mov       ecx,eax
  0051962f  46                    inc       esi
  00519630  894c2410              mov       [esp+10h],ecx
  00519634  8a06                  mov       al,[esi]
  00519636  3cff                  cmp       al,0FFh
  00519638  75d7                  jne       short 00519611h

LAB_0051963a:
  0051963a  8b1534086000          mov       edx,[600834h]
  00519640  8a4601                mov       al,[esi+1]
  00519643  81e2ff000000          and       edx,0FFh
  00519649  46                    inc       esi
  0051964a  3cff                  cmp       al,0FFh
  0051964c  8d3c95c07b5f00        lea       edi,[edx*4+5F7BC0h]
  00519653  7429                  je        short 0051967Eh

LAB_00519655:
  00519655  25ff000000            and       eax,0FFh
  0051965a  8d542448              lea       edx,[esp+48h]
  0051965e  52                    push      edx
  0051965f  8b0487                mov       eax,[edi+eax*4]
  00519662  50                    push      eax
  00519663  51                    push      ecx
  00519664  8b4c242c              mov       ecx,[esp+2Ch]
  00519668  51                    push      ecx
  00519669  e892790000            call      00521000h
  0051966e  83c410                add       esp,10h
  00519671  8bc8                  mov       ecx,eax
  00519673  46                    inc       esi
  00519674  894c2410              mov       [esp+10h],ecx
  00519678  8a06                  mov       al,[esi]
  0051967a  3cff                  cmp       al,0FFh
  0051967c  75d7                  jne       short 00519655h

LAB_0051967e:
  0051967e  8a4601                mov       al,[esi+1]
  00519681  33d2                  xor       edx,edx
  00519683  8a1536086000          mov       dl,[600836h]
  00519689  46                    inc       esi
  0051968a  3cff                  cmp       al,0FFh
  0051968c  8974241c              mov       [esp+1Ch],esi
  00519690  8d3c95c07b5f00        lea       edi,[edx*4+5F7BC0h]
  00519697  742d                  je        short 005196C6h

LAB_00519699:
  00519699  25ff000000            and       eax,0FFh
  0051969e  8d542448              lea       edx,[esp+48h]
  005196a2  52                    push      edx
  005196a3  8b0487                mov       eax,[edi+eax*4]
  005196a6  50                    push      eax
  005196a7  51                    push      ecx
  005196a8  8b4c242c              mov       ecx,[esp+2Ch]
  005196ac  51                    push      ecx
  005196ad  e88e8a0000            call      00522140h
  005196b2  83c410                add       esp,10h
  005196b5  8bc8                  mov       ecx,eax
  005196b7  46                    inc       esi
  005196b8  894c2410              mov       [esp+10h],ecx
  005196bc  8974241c              mov       [esp+1Ch],esi
  005196c0  8a06                  mov       al,[esi]
  005196c2  3cff                  cmp       al,0FFh
  005196c4  75d3                  jne       short 00519699h

LAB_005196c6:
  005196c6  8b1580ca5e00          mov       edx,[5ECA80h]
  005196cc  f60240                test      byte [edx],40h
  005196cf  7432                  je        short 00519703h
  005196d1  a1cc975e00            mov       eax,[5E97CCh]
  005196d6  53                    push      ebx
  005196d7  53                    push      ebx
  005196d8  8b503c                mov       edx,[eax+3Ch]
  005196db  8b442428              mov       eax,[esp+28h]
  005196df  8b32                  mov       esi,[edx]
  005196e1  56                    push      esi
  005196e2  51                    push      ecx
  005196e3  50                    push      eax
  005196e4  e807870000            call      00521DF0h
  005196e9  8b4c2434              mov       ecx,[esp+34h]
  005196ed  83c414                add       esp,14h
  005196f0  89442410              mov       [esp+10h],eax
  005196f4  56                    push      esi
  005196f5  50                    push      eax
  005196f6  51                    push      ecx
  005196f7  e8c4620000            call      0051F9C0h
  005196fc  83c40c                add       esp,0Ch
  005196ff  89442410              mov       [esp+10h],eax

LAB_00519703:
  00519703  55                    push      ebp
  00519704  8d542414              lea       edx,[esp+14h]
  00519708  6a03                  push      3
  0051970a  8d442428              lea       eax,[esp+28h]
  0051970e  52                    push      edx
  0051970f  8d4c2420              lea       ecx,[esp+20h]
  00519713  50                    push      eax
  00519714  51                    push      ecx
  00519715  e8660b0100            call      0052A280h
  0051971a  b840010000            mov       eax,140h
  0051971f  83c414                add       esp,14h
  00519722  66a350995e00          mov       [5E9950h],ax
  00519728  66a3e0985e00          mov       [5E98E0h],ax
  0051972e  b8f0000000            mov       eax,0F0h
  00519733  bb38000000            mov       ebx,38h
  00519738  66a352995e00          mov       [5E9952h],ax
  0051973e  66a3e2985e00          mov       [5E98E2h],ax
  00519744  bd306c5600            mov       ebp,566C30h
  00519749  bf286c5600            mov       edi,566C28h
  0051974e  e922daffff            jmp       00517175h

LAB_00519753:
  00519753  33c0                  xor       eax,eax
  00519755  a03ce55d00            mov       al,[5DE53Ch]
  0051975a  8d1480                lea       edx,[eax+eax*4]
  0051975d  c1e203                shl       edx,3
  00519760  2bd0                  sub       edx,eax
  00519762  a1f8455600            mov       eax,[5645F8h]
  00519767  25ff000000            and       eax,0FFh
  0051976c  48                    dec       eax
  0051976d  8d0c95c0935e00        lea       ecx,[edx*4+5E93C0h]
  00519774  751b                  jne       short 00519791h
  00519776  a150e05d00            mov       eax,[5DE050h]
  0051977b  25ffff0000            and       eax,0FFFFh
  00519780  50                    push      eax
  00519781  51                    push      ecx
  00519782  8b0dcc975e00          mov       ecx,[5E97CCh]
  00519788  51                    push      ecx
  00519789  e842a6ffff            call      00513DD0h
  0051978e  83c40c                add       esp,0Ch

LAB_00519791:
  00519791  e86a0eefff            call      0040A600h
  00519796  e8650eefff            call      0040A600h
  0051979b  5f                    pop       edi
  0051979c  b801000000            mov       eax,1
  005197a1  5e                    pop       esi
  005197a2  5d                    pop       ebp
  005197a3  a398985e00            mov       [5E9898h],eax
  005197a8  a308995e00            mov       [5E9908h],eax
  005197ad  5b                    pop       ebx
  005197ae  83c460                add       esp,60h
  005197b1  c3                    ret
  005197b2  8bff                  mov       edi,edi
  005197b4  9c                    pushf
  005197b5  7651                  jbe       short 00519808h
  005197b7  007077                add       [eax+77h],dh
  005197ba  51                    push      ecx
  005197bb  005578                add       [ebp+78h],dl
  005197be  51                    push      ecx
  005197bf  007879                add       [eax+79h],bh
  005197c2  51                    push      ecx
  005197c3  009090909090          add       [eax-6F6F6F70h],dl
  005197c9  90                    nop
  005197ca  90                    nop
  005197cb  90                    nop
  005197cc  90                    nop
  005197cd  90                    nop
  005197ce  90                    nop
  005197cf  90                    nop

164 cible(s) de saut interne : 0x00516ff4, 0x00517090, 0x005170d0, 0x005170eb, 0x00517107, 0x00517115, 0x00517134, 0x00517142, 0x00517153, 0x00517175, 0x005171d4, 0x005171eb, 0x00517396, 0x005173d1, 0x00517408, 0x005174da, 0x00517511, 0x00517544, 0x0051762b, 0x0051762f, 0x00517683, 0x00517709, 0x0051771c, 0x00517739, 0x00517787, 0x00517800, 0x0051780f, 0x00517821, 0x00517831, 0x00517880, 0x005178a9, 0x005178b6, 0x005178c3, 0x005178d2, 0x005178e2, 0x005178ed, 0x005178f8, 0x00517903, 0x0051791f, 0x0051795a, 0x0051798c, 0x005179b0, 0x005179da, 0x00517a2b, 0x00517a2e, 0x00517a64, 0x00517a77, 0x00517aa7, 0x00517b09, 0x00517b21, 0x00517b34, 0x00517b46, 0x00517bb9, 0x00517bc8, 0x00517bde, 0x00517bed, 0x00517c03, 0x00517c12, 0x00517c44, 0x00517c62, 0x00517c6d, 0x00517c81, 0x00517cd6, 0x00517cdc, 0x00517cef, 0x00517d13, 0x00517d47, 0x00517d4b, 0x00517d5f, 0x00517d7c, 0x00517d8a, 0x00517dfe, 0x00517ebc, 0x00517ebe, 0x00517f62, 0x00517fc3, 0x00517fd9, 0x00518044, 0x00518078, 0x00518095, 0x005180b3, 0x005180f1, 0x005180f4, 0x00518106, 0x00518121, 0x00518163, 0x0051817d, 0x005181cb, 0x005181dd, 0x005181f7, 0x00518245, 0x00518257, 0x00518271, 0x005182bf, 0x00518327, 0x00518334, 0x00518361, 0x00518446, 0x00518473, 0x005185a8, 0x005185c0, 0x0051869c, 0x0051882c, 0x00518883, 0x0051891a, 0x0051894e, 0x00518952, 0x0051898d, 0x00518997, 0x005189ab, 0x005189d5, 0x005189e8, 0x005189f6, 0x00518a04, 0x00518a11, 0x00518a13, 0x00518a18, 0x00518a7d, 0x00518acf, 0x00518adc, 0x00518b74, 0x00518b81, 0x00518cdc, 0x00518d04, 0x00518e32, 0x00518e3a, 0x00518e90, 0x00518e94, 0x00518f02, 0x00518f71, 0x00518fd2, 0x00518fff, 0x0051905b, 0x00519092, 0x00519094, 0x00519118, 0x00519128, 0x0051912d, 0x00519158, 0x00519193, 0x005191ef, 0x00519213, 0x0051922e, 0x00519258, 0x0051938f, 0x005193c7, 0x005193fb, 0x005193ff, 0x00519455, 0x00519464, 0x00519497, 0x005194f0, 0x00519528, 0x00519559, 0x00519564, 0x00519611, 0x0051963a, 0x00519655, 0x0051967e, 0x00519699, 0x005196c6, 0x00519703, 0x00519753, 0x00519791
