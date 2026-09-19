Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004ffc50  section .text  offset fichier 0xff050  1936 octets

  004ffc50  83ec2c                sub       esp,2Ch
  004ffc53  53                    push      ebx
  004ffc54  55                    push      ebp
  004ffc55  56                    push      esi
  004ffc56  8b74243c              mov       esi,[esp+3Ch]
  004ffc5a  8d442424              lea       eax,[esp+24h]
  004ffc5e  57                    push      edi
  004ffc5f  8d4c242c              lea       ecx,[esp+2Ch]
  004ffc63  50                    push      eax
  004ffc64  8d542428              lea       edx,[esp+28h]
  004ffc68  51                    push      ecx
  004ffc69  33ed                  xor       ebp,ebp
  004ffc6b  52                    push      edx
  004ffc6c  56                    push      esi
  004ffc6d  66896c2440            mov       [esp+40h],bp
  004ffc72  66896c2442            mov       [esp+42h],bp
  004ffc77  66c74424443200        mov       word [esp+44h],32h
  004ffc7e  66c74424466400        mov       word [esp+46h],64h
  004ffc85  66896c2448            mov       [esp+48h],bp
  004ffc8a  66c744244a5a00        mov       word [esp+4Ah],5Ah
  004ffc91  e8ea8c0300            call      00538980h
  004ffc96  83c410                add       esp,10h
  004ffc99  8d442420              lea       eax,[esp+20h]
  004ffc9d  8d4c241c              lea       ecx,[esp+1Ch]
  004ffca1  8d542440              lea       edx,[esp+40h]
  004ffca5  50                    push      eax
  004ffca6  51                    push      ecx
  004ffca7  8d442420              lea       eax,[esp+20h]
  004ffcab  52                    push      edx
  004ffcac  8d4c2420              lea       ecx,[esp+20h]
  004ffcb0  50                    push      eax
  004ffcb1  51                    push      ecx
  004ffcb2  56                    push      esi
  004ffcb3  e868620300            call      00535F20h
  004ffcb8  8b4c242c              mov       ecx,[esp+2Ch]
  004ffcbc  b840000000            mov       eax,40h
  004ffcc1  2bc1                  sub       eax,ecx
  004ffcc3  83c418                add       esp,18h
  004ffcc6  89442410              mov       [esp+10h],eax
  004ffcca  8b44241c              mov       eax,[esp+1Ch]
  004ffcce  3bc5                  cmp       eax,ebp
  004ffcd0  bf01000000            mov       edi,1
  004ffcd5  0f84e5030000          je        near 005000C0h
  004ffcdb  56                    push      esi
  004ffcdc  e8bfb8fdff            call      004DB5A0h
  004ffce1  8b06                  mov       eax,[esi]
  004ffce3  83c404                add       esp,4
  004ffce6  a802                  test      al,2
  004ffce8  0f849c000000          je        near 004FFD8Ah
  004ffcee  8a4c2420              mov       cl,[esp+20h]
  004ffcf2  bb10000000            mov       ebx,10h
  004ffcf7  84cb                  test      bl,cl
  004ffcf9  740d                  je        short 004FFD08h
  004ffcfb  24fd                  and       al,0FDh
  004ffcfd  c6464300              mov       byte [esi+43h],0
  004ffd01  8906                  mov       [esi],eax
  004ffd03  e9b8030000            jmp       005000C0h

LAB_004ffd08:
  004ffd08  66837e1a08            cmp       word [esi+1Ah],8
  004ffd0d  7c58                  jl        short 004FFD67h
  004ffd0f  8d542430              lea       edx,[esp+30h]
  004ffd13  55                    push      ebp
  004ffd14  8d461c                lea       eax,[esi+1Ch]
  004ffd17  52                    push      edx
  004ffd18  50                    push      eax
  004ffd19  56                    push      esi
  004ffd1a  e8119f0100            call      00519C30h
  004ffd1f  8bf8                  mov       edi,eax
  004ffd21  a1b8e05d00            mov       eax,[5DE0B8h]
  004ffd26  83c410                add       esp,10h
  004ffd29  3bc5                  cmp       eax,ebp
  004ffd2b  7430                  je        short 004FFD5Dh
  004ffd2d  66899f96000000        mov       [edi+96h],bx
  004ffd34  8b1f                  mov       ebx,[edi]
  004ffd36  83cb40                or        ebx,40h
  004ffd39  57                    push      edi
  004ffd3a  56                    push      esi
  004ffd3b  891f                  mov       [edi],ebx
  004ffd3d  e83e9e0300            call      00539B80h
  004ffd42  83c408                add       esp,8
  004ffd45  83c71c                add       edi,1Ch
  004ffd48  57                    push      edi
  004ffd49  6800180000            push      1800h
  004ffd4e  6800030000            push      300h
  004ffd53  6a03                  push      3
  004ffd55  e8c6dd0300            call      0053DB20h
  004ffd5a  83c410                add       esp,10h

LAB_004ffd5d:
  004ffd5d  8b06                  mov       eax,[esi]
  004ffd5f  a801                  test      al,1
  004ffd61  740c                  je        short 004FFD6Fh
  004ffd63  66ff4e1a              dec       word [esi+1Ah]

LAB_004ffd67:
  004ffd67  a801                  test      al,1
  004ffd69  0f8551030000          jne       near 005000C0h

LAB_004ffd6f:
  004ffd6f  a900000002            test      eax,2000000h
  004ffd74  0f841c020000          je        near 004FFF96h
  004ffd7a  24fd                  and       al,0FDh
  004ffd7c  c6464300              mov       byte [esi+43h],0
  004ffd80  8906                  mov       [esi],eax
  004ffd82  896e5c                mov       [esi+5Ch],ebp
  004ffd85  e936030000            jmp       005000C0h

LAB_004ffd8a:
  004ffd8a  a810                  test      al,10h
  004ffd8c  0f852e030000          jne       near 005000C0h
  004ffd92  a904080000            test      eax,804h
  004ffd97  0f8452010000          je        near 004FFEEFh
  004ffd9d  f7442420e8030040      test      dword [esp+20h],400003E8h
  004ffda5  740b                  je        short 004FFDB2h
  004ffda7  25fbf7ffff            and       eax,0FFFFF7FBh
  004ffdac  c6464300              mov       byte [esi+43h],0
  004ffdb0  8906                  mov       [esi],eax

LAB_004ffdb2:
  004ffdb2  668b4610              mov       ax,[esi+10h]
  004ffdb6  0fbfc8                movsx     ecx,ax
  004ffdb9  83f903                cmp       ecx,3
  004ffdbc  0f87fe020000          ja        near 005000C0h
  004ffdc2  ff248dc0035000        jmp       dword [ecx*4+5003C0h]
  004ffdc9  8b06                  mov       eax,[esi]
  004ffdcb  a900000002            test      eax,2000000h
  004ffdd0  0f84ea020000          je        near 005000C0h
  004ffdd6  80e4f7                and       ah,0F7h
  004ffdd9  8906                  mov       [esi],eax
  004ffddb  e9e0020000            jmp       005000C0h
  004ffde0  b805000000            mov       eax,5
  004ffde5  66394618              cmp       [esi+18h],ax
  004ffde9  7411                  je        short 004FFDFCh
  004ffdeb  894618                mov       [esi+18h],eax
  004ffdee  8b06                  mov       eax,[esi]
  004ffdf0  25fbfffffe            and       eax,0FEFFFFFBh
  004ffdf5  8906                  mov       [esi],eax
  004ffdf7  e9c4020000            jmp       005000C0h

LAB_004ffdfc:
  004ffdfc  66837e1a07            cmp       word [esi+1Ah],7
  004ffe01  0f85b9020000          jne       near 005000C0h
  004ffe07  33c0                  xor       eax,eax
  004ffe09  6aff                  push      0FFFFFFFFh
  004ffe0b  668b4606              mov       ax,[esi+6]
  004ffe0f  6aff                  push      0FFFFFFFFh
  004ffe11  8bc8                  mov       ecx,eax
  004ffe13  c1f808                sar       eax,8
  004ffe16  81e1ff000000          and       ecx,0FFh
  004ffe1c  25ff000000            and       eax,0FFh
  004ffe21  51                    push      ecx
  004ffe22  50                    push      eax
  004ffe23  56                    push      esi
  004ffe24  e857ab0300            call      0053A980h
  004ffe29  83c414                add       esp,14h
  004ffe2c  66ff4e10              dec       word [esi+10h]
  004ffe30  e98b020000            jmp       005000C0h
  004ffe35  8a4e43                mov       cl,[esi+43h]
  004ffe38  fec9                  dec       cl
  004ffe3a  884e43                mov       [esi+43h],cl
  004ffe3d  7505                  jne       short 004FFE44h
  004ffe3f  48                    dec       eax
  004ffe40  66894610              mov       [esi+10h],ax

LAB_004ffe44:
  004ffe44  8b542414              mov       edx,[esp+14h]
  004ffe48  8b4c2440              mov       ecx,[esp+40h]
  004ffe4c  8d049532000000        lea       eax,[edx*4+32h]
  004ffe53  8d1408                lea       edx,[eax+ecx]
  004ffe56  8d3c00                lea       edi,[eax+eax]
  004ffe59  81e2ff0f0000          and       edx,0FFFh
  004ffe5f  3bd7                  cmp       edx,edi
  004ffe61  7f09                  jg        short 004FFE6Ch
  004ffe63  66014e32              add       [esi+32h],cx
  004ffe67  e954020000            jmp       005000C0h

LAB_004ffe6c:
  004ffe6c  81e1ff0f0000          and       ecx,0FFFh
  004ffe72  81f900080000          cmp       ecx,800h
  004ffe78  0f8c3e020000          jl        near 005000BCh
  004ffe7e  e937020000            jmp       005000BAh
  004ffe83  55                    push      ebp
  004ffe84  55                    push      ebp
  004ffe85  68bd000000            push      0BDh
  004ffe8a  56                    push      esi
  004ffe8b  e850acf7ff            call      0047AAE0h
  004ffe90  83c410                add       esp,10h
  004ffe93  e8a8720400            call      00547140h
  004ffe98  99                    cdq
  004ffe99  b903000000            mov       ecx,3
  004ffe9e  f7f9                  idiv      ecx
  004ffea0  0fbf4e14              movsx     ecx,word [esi+14h]
  004ffea4  33c0                  xor       eax,eax
  004ffea6  8d144a                lea       edx,[edx+ecx*2]
  004ffea9  03ca                  add       ecx,edx
  004ffeab  668b044d20305500      mov       ax,[ecx*2+553020h]
  004ffeb3  66894606              mov       [esi+6],ax
  004ffeb7  8b15c4975e00          mov       edx,[5E97C4h]
  004ffebd  8bc8                  mov       ecx,eax
  004ffebf  25ff000000            and       eax,0FFh
  004ffec4  8b929c000000          mov       edx,[edx+9Ch]
  004ffeca  c1f908                sar       ecx,8
  004ffecd  81e1ff000000          and       ecx,0FFh
  004ffed3  8d0440                lea       eax,[eax+eax*2]
  004ffed6  c1e004                shl       eax,4
  004ffed9  8b0c8a                mov       ecx,[edx+ecx*4]
  004ffedc  8a540113              mov       dl,[ecx+eax+13h]
  004ffee0  66ff4e10              dec       word [esi+10h]
  004ffee4  885643                mov       [esi+43h],dl
  004ffee7  896e5c                mov       [esi+5Ch],ebp
  004ffeea  e9d1010000            jmp       005000C0h

LAB_004ffeef:
  004ffeef  a801                  test      al,1
  004ffef1  0f85c9010000          jne       near 005000C0h
  004ffef7  33c9                  xor       ecx,ecx
  004ffef9  8a4e42                mov       cl,[esi+42h]
  004ffefc  83f903                cmp       ecx,3
  004ffeff  0f87bb010000          ja        near 005000C0h
  004fff05  ff248dd0035000        jmp       dword [ecx*4+5003D0h]
  004fff0c  66396e18              cmp       [esi+18h],bp
  004fff10  740a                  je        short 004FFF1Ch
  004fff12  0d00000001            or        eax,1000000h
  004fff17  896e18                mov       [esi+18h],ebp
  004fff1a  8906                  mov       [esi],eax

LAB_004fff1c:
  004fff1c  817c24182c010000      cmp       dword [esp+18h],12Ch
  004fff24  7d09                  jge       short 004FFF2Fh
  004fff26  c6464300              mov       byte [esi+43h],0
  004fff2a  e991010000            jmp       005000C0h

LAB_004fff2f:
  004fff2f  8a4643                mov       al,[esi+43h]
  004fff32  84c0                  test      al,al
  004fff34  7460                  je        short 004FFF96h
  004fff36  fec8                  dec       al
  004fff38  896e5c                mov       [esi+5Ch],ebp
  004fff3b  884643                mov       [esi+43h],al
  004fff3e  e97d010000            jmp       005000C0h
  004fff43  66397e18              cmp       [esi+18h],di
  004fff47  740a                  je        short 004FFF53h
  004fff49  0d00000001            or        eax,1000000h
  004fff4e  897e18                mov       [esi+18h],edi
  004fff51  8906                  mov       [esi],eax

LAB_004fff53:
  004fff53  8b442414              mov       eax,[esp+14h]
  004fff57  8b4c2440              mov       ecx,[esp+40h]
  004fff5b  8d840080000000        lea       eax,[eax+eax+80h]
  004fff62  8d1408                lea       edx,[eax+ecx]
  004fff65  8d3c00                lea       edi,[eax+eax]
  004fff68  81e2ff0f0000          and       edx,0FFFh
  004fff6e  3bd7                  cmp       edx,edi
  004fff70  7f10                  jg        short 004FFF82h
  004fff72  66014e32              add       [esi+32h],cx
  004fff76  c6464300              mov       byte [esi+43h],0
  004fff7a  896e5c                mov       [esi+5Ch],ebp
  004fff7d  e93e010000            jmp       005000C0h

LAB_004fff82:
  004fff82  81e1ff0f0000          and       ecx,0FFFh
  004fff88  81f900080000          cmp       ecx,800h
  004fff8e  7c02                  jl        short 004FFF92h
  004fff90  f7d8                  neg       eax

LAB_004fff92:
  004fff92  66014632              add       [esi+32h],ax

LAB_004fff96:
  004fff96  896e5c                mov       [esi+5Ch],ebp
  004fff99  e922010000            jmp       005000C0h
  004fff9e  66397e18              cmp       [esi+18h],di
  004fffa2  740a                  je        short 004FFFAEh
  004fffa4  0d00000001            or        eax,1000000h
  004fffa9  897e18                mov       [esi+18h],edi
  004fffac  8906                  mov       [esi],eax

LAB_004fffae:
  004fffae  0fbf4610              movsx     eax,word [esi+10h]
  004fffb2  39442418              cmp       [esp+18h],eax
  004fffb6  7d09                  jge       short 004FFFC1h
  004fffb8  c6464300              mov       byte [esi+43h],0
  004fffbc  e9ff000000            jmp       005000C0h

LAB_004fffc1:
  004fffc1  8a4643                mov       al,[esi+43h]
  004fffc4  84c0                  test      al,al
  004fffc6  7405                  je        short 004FFFCDh
  004fffc8  fec8                  dec       al
  004fffca  884643                mov       [esi+43h],al

LAB_004fffcd:
  004fffcd  8b542414              mov       edx,[esp+14h]
  004fffd1  8b4c2440              mov       ecx,[esp+40h]
  004fffd5  8d841280000000        lea       eax,[edx+edx+80h]
  004fffdc  8d3c08                lea       edi,[eax+ecx]
  004fffdf  8d1c00                lea       ebx,[eax+eax]
  004fffe2  81e7ff0f0000          and       edi,0FFFh
  004fffe8  3bfb                  cmp       edi,ebx
  004fffea  7f15                  jg        short 00500001h
  004fffec  66014e32              add       [esi+32h],cx
  004ffff0  81c2c0000000          add       edx,0C0h
  004ffff6  c1e207                shl       edx,7
  004ffff9  89565c                mov       [esi+5Ch],edx
  004ffffc  e9bf000000            jmp       005000C0h

LAB_00500001:
  00500001  81e1ff0f0000          and       ecx,0FFFh
  00500007  81f900080000          cmp       ecx,800h
  0050000d  7c02                  jl        short 00500011h
  0050000f  f7d8                  neg       eax

LAB_00500011:
  00500011  66014632              add       [esi+32h],ax
  00500015  81c2c0000000          add       edx,0C0h
  0050001b  c1e207                shl       edx,7
  0050001e  89565c                mov       [esi+5Ch],edx
  00500021  e99a000000            jmp       005000C0h
  00500026  8a4e43                mov       cl,[esi+43h]
  00500029  84c9                  test      cl,cl
  0050002b  7405                  je        short 00500032h
  0050002d  fec9                  dec       cl
  0050002f  884e43                mov       [esi+43h],cl

LAB_00500032:
  00500032  66397e18              cmp       [esi+18h],di
  00500036  740a                  je        short 00500042h
  00500038  0d00000001            or        eax,1000000h
  0050003d  897e18                mov       [esi+18h],edi
  00500040  8906                  mov       [esi],eax

LAB_00500042:
  00500042  8b4c2414              mov       ecx,[esp+14h]
  00500046  0fbf5610              movsx     edx,word [esi+10h]
  0050004a  81c180000000          add       ecx,80h
  00500050  52                    push      edx
  00500051  c1e107                shl       ecx,7
  00500054  894e5c                mov       [esi+5Ch],ecx
  00500057  e8c0590400            call      00545A1Ch
  0050005c  0faf465c              imul      eax,[esi+5Ch]
  00500060  8b7e1c                mov       edi,[esi+1Ch]
  00500063  c1f80c                sar       eax,0Ch
  00500066  03f8                  add       edi,eax
  00500068  0fbf4610              movsx     eax,word [esi+10h]
  0050006c  50                    push      eax
  0050006d  897e1c                mov       [esi+1Ch],edi
  00500070  e8a1590400            call      00545A16h
  00500075  0faf465c              imul      eax,[esi+5Ch]
  00500079  8b5e24                mov       ebx,[esi+24h]
  0050007c  8b4c2414              mov       ecx,[esp+14h]
  00500080  c1f80c                sar       eax,0Ch
  00500083  03d8                  add       ebx,eax
  00500085  8d048d32000000        lea       eax,[ecx*4+32h]
  0050008c  8b4c2440              mov       ecx,[esp+40h]
  00500090  895e24                mov       [esi+24h],ebx
  00500093  8d3c00                lea       edi,[eax+eax]
  00500096  896e5c                mov       [esi+5Ch],ebp
  00500099  8d1408                lea       edx,[eax+ecx]
  0050009c  81e2ff0f0000          and       edx,0FFFh
  005000a2  3bd7                  cmp       edx,edi
  005000a4  7f06                  jg        short 005000ACh
  005000a6  66014e32              add       [esi+32h],cx
  005000aa  eb14                  jmp       short 005000C0h

LAB_005000ac:
  005000ac  81e1ff0f0000          and       ecx,0FFFh
  005000b2  81f900080000          cmp       ecx,800h
  005000b8  7c02                  jl        short 005000BCh

LAB_005000ba:
  005000ba  f7d8                  neg       eax

LAB_005000bc:
  005000bc  66014632              add       [esi+32h],ax

LAB_005000c0:
  005000c0  8b44241c              mov       eax,[esp+1Ch]
  005000c4  50                    push      eax
  005000c5  56                    push      esi
  005000c6  e89580f8ff            call      00488160h
  005000cb  83c408                add       esp,8
  005000ce  56                    push      esi
  005000cf  e86c380300            call      00533940h
  005000d4  83c404                add       esp,4
  005000d7  56                    push      esi
  005000d8  e813fafdff            call      004DFAF0h
  005000dd  8b4c2428              mov       ecx,[esp+28h]
  005000e1  83c404                add       esp,4
  005000e4  51                    push      ecx
  005000e5  56                    push      esi
  005000e6  e88536feff            call      004E3770h
  005000eb  8b442424              mov       eax,[esp+24h]
  005000ef  83c408                add       esp,8
  005000f2  3bc5                  cmp       eax,ebp
  005000f4  0f84be020000          je        near 005003B8h
  005000fa  f70617080000          test      dword [esi],817h
  00500100  0f85b2020000          jne       near 005003B8h
  00500106  8a4643                mov       al,[esi+43h]
  00500109  84c0                  test      al,al
  0050010b  0f85a7020000          jne       near 005003B8h
  00500111  8b466c                mov       eax,[esi+6Ch]
  00500114  8b5e1c                mov       ebx,[esi+1Ch]
  00500117  8b4e24                mov       ecx,[esi+24h]
  0050011a  8b781c                mov       edi,[eax+1Ch]
  0050011d  8b6824                mov       ebp,[eax+24h]
  00500120  2bfb                  sub       edi,ebx
  00500122  2be9                  sub       ebp,ecx
  00500124  c1ff0c                sar       edi,0Ch
  00500127  c1fd0c                sar       ebp,0Ch
  0050012a  8bd7                  mov       edx,edi
  0050012c  8bc5                  mov       eax,ebp
  0050012e  0fafd7                imul      edx,edi
  00500131  0fafc5                imul      eax,ebp
  00500134  03d0                  add       edx,eax
  00500136  52                    push      edx
  00500137  e8d4580400            call      00545A10h
  0050013c  55                    push      ebp
  0050013d  57                    push      edi
  0050013e  89442420              mov       [esp+20h],eax
  00500142  e8db580400            call      00545A22h
  00500147  668b4e32              mov       cx,[esi+32h]
  0050014b  2bc1                  sub       eax,ecx
  0050014d  25ff0f0000            and       eax,0FFFh
  00500152  89442440              mov       [esp+40h],eax
  00500156  8b442418              mov       eax,[esp+18h]
  0050015a  3dfa000000            cmp       eax,0FAh
  0050015f  0f8dd0000000          jge       near 00500235h
  00500165  e8d66f0400            call      00547140h
  0050016a  99                    cdq
  0050016b  b964000000            mov       ecx,64h
  00500170  f7f9                  idiv      ecx
  00500172  83fa1e                cmp       edx,1Eh
  00500175  7d61                  jge       short 005001D8h
  00500177  8b542440              mov       edx,[esp+40h]
  0050017b  8b7c2410              mov       edi,[esp+10h]
  0050017f  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  00500186  8d0cfd00020000        lea       ecx,[edi*8+200h]
  0050018d  25ff0f0000            and       eax,0FFFh
  00500192  3bc1                  cmp       eax,ecx
  00500194  0f8fba010000          jg        near 00500354h
  0050019a  e8a16f0400            call      00547140h
  0050019f  99                    cdq
  005001a0  8d4c3f01              lea       ecx,[edi+edi+1]
  005001a4  f7f9                  idiv      ecx
  005001a6  8b442418              mov       eax,[esp+18h]
  005001aa  83c250                add       edx,50h
  005001ad  3bc2                  cmp       eax,edx
  005001af  0f8d9f010000          jge       near 00500354h
  005001b5  8b16                  mov       edx,[esi]
  005001b7  c7461803000000        mov       dword [esi+18h],3
  005001be  81e2fffffffe          and       edx,0FEFFFFFFh
  005001c4  c7465c00000000        mov       dword [esi+5Ch],0
  005001cb  83ca02                or        edx,2
  005001ce  8916                  mov       [esi],edx
  005001d0  5f                    pop       edi
  005001d1  5e                    pop       esi
  005001d2  5d                    pop       ebp
  005001d3  5b                    pop       ebx
  005001d4  83c42c                add       esp,2Ch
  005001d7  c3                    ret

LAB_005001d8:
  005001d8  83fa3c                cmp       edx,3Ch
  005001db  0f8cf6000000          jl        near 005002D7h
  005001e1  c6464203              mov       byte [esi+42h],3
  005001e5  e8566f0400            call      00547140h
  005001ea  99                    cdq
  005001eb  b90a000000            mov       ecx,0Ah
  005001f0  f7f9                  idiv      ecx
  005001f2  8b442410              mov       eax,[esp+10h]
  005001f6  8b4c2414              mov       ecx,[esp+14h]
  005001fa  d1f8                  sar       eax,1
  005001fc  8db980000000          lea       edi,[ecx+80h]
  00500202  c1e704                shl       edi,4
  00500205  02d0                  add       dl,al
  00500207  80c214                add       dl,14h
  0050020a  885643                mov       [esi+43h],dl
  0050020d  e82e6f0400            call      00547140h
  00500212  99                    cdq
  00500213  f7ff                  idiv      edi
  00500215  d1ff                  sar       edi,1
  00500217  2bd7                  sub       edx,edi
  00500219  8b7c2440              mov       edi,[esp+40h]
  0050021d  03d7                  add       edx,edi
  0050021f  66035632              add       dx,[esi+32h]
  00500223  81c200080000          add       edx,800h
  00500229  66895610              mov       [esi+10h],dx
  0050022d  5f                    pop       edi
  0050022e  5e                    pop       esi
  0050022f  5d                    pop       ebp
  00500230  5b                    pop       ebx
  00500231  83c42c                add       esp,2Ch
  00500234  c3                    ret

LAB_00500235:
  00500235  3df4010000            cmp       eax,1F4h
  0050023a  0f8dc4000000          jge       near 00500304h
  00500240  e8fb6e0400            call      00547140h
  00500245  99                    cdq
  00500246  b964000000            mov       ecx,64h
  0050024b  f7f9                  idiv      ecx
  0050024d  83fa2d                cmp       edx,2Dh
  00500250  7d29                  jge       short 0050027Bh
  00500252  8b542440              mov       edx,[esp+40h]
  00500256  8b442410              mov       eax,[esp+10h]
  0050025a  8d8c8200010000        lea       ecx,[edx+eax*4+100h]
  00500261  8d14c500020000        lea       edx,[eax*8+200h]
  00500268  81e1ff0f0000          and       ecx,0FFFh
  0050026e  3bca                  cmp       ecx,edx
  00500270  0f8fde000000          jg        near 00500354h
  00500276  e9bb000000            jmp       00500336h

LAB_0050027b:
  0050027b  83fa46                cmp       edx,46h
  0050027e  7d4e                  jge       short 005002CEh
  00500280  c6464203              mov       byte [esi+42h],3
  00500284  e8b76e0400            call      00547140h
  00500289  99                    cdq
  0050028a  b90a000000            mov       ecx,0Ah
  0050028f  f7f9                  idiv      ecx
  00500291  8b442410              mov       eax,[esp+10h]
  00500295  8b4c2414              mov       ecx,[esp+14h]
  00500299  d1f8                  sar       eax,1
  0050029b  8db980000000          lea       edi,[ecx+80h]
  005002a1  c1e704                shl       edi,4
  005002a4  02d0                  add       dl,al
  005002a6  80c214                add       dl,14h
  005002a9  885643                mov       [esi+43h],dl
  005002ac  e88f6e0400            call      00547140h
  005002b1  99                    cdq
  005002b2  f7ff                  idiv      edi
  005002b4  8b4c2440              mov       ecx,[esp+40h]
  005002b8  d1ff                  sar       edi,1
  005002ba  2bd7                  sub       edx,edi
  005002bc  03d1                  add       edx,ecx
  005002be  66035632              add       dx,[esi+32h]
  005002c2  66895610              mov       [esi+10h],dx
  005002c6  5f                    pop       edi
  005002c7  5e                    pop       esi
  005002c8  5d                    pop       ebp
  005002c9  5b                    pop       ebx
  005002ca  83c42c                add       esp,2Ch
  005002cd  c3                    ret

LAB_005002ce:
  005002ce  83fa5a                cmp       edx,5Ah
  005002d1  0f8d92000000          jge       near 00500369h

LAB_005002d7:
  005002d7  c6464202              mov       byte [esi+42h],2
  005002db  e8606e0400            call      00547140h
  005002e0  99                    cdq
  005002e1  b90a000000            mov       ecx,0Ah
  005002e6  66c746106400          mov       word [esi+10h],64h
  005002ec  f7f9                  idiv      ecx
  005002ee  8b442410              mov       eax,[esp+10h]
  005002f2  d1f8                  sar       eax,1
  005002f4  02d0                  add       dl,al
  005002f6  80c214                add       dl,14h
  005002f9  885643                mov       [esi+43h],dl
  005002fc  5f                    pop       edi
  005002fd  5e                    pop       esi
  005002fe  5d                    pop       ebp
  005002ff  5b                    pop       ebx
  00500300  83c42c                add       esp,2Ch
  00500303  c3                    ret

LAB_00500304:
  00500304  e8376e0400            call      00547140h
  00500309  99                    cdq
  0050030a  b964000000            mov       ecx,64h
  0050030f  f7f9                  idiv      ecx
  00500311  83fa1e                cmp       edx,1Eh
  00500314  7d4e                  jge       short 00500364h
  00500316  8b542440              mov       edx,[esp+40h]
  0050031a  8b442410              mov       eax,[esp+10h]
  0050031e  8d8c8200010000        lea       ecx,[edx+eax*4+100h]
  00500325  8d14c500020000        lea       edx,[eax*8+200h]
  0050032c  81e1ff0f0000          and       ecx,0FFFh
  00500332  3bca                  cmp       ecx,edx
  00500334  7f1e                  jg        short 00500354h

LAB_00500336:
  00500336  8b06                  mov       eax,[esi]
  00500338  c7461804000000        mov       dword [esi+18h],4
  0050033f  0d04080001            or        eax,1000804h
  00500344  66c746100300          mov       word [esi+10h],3
  0050034a  8906                  mov       [esi],eax
  0050034c  5f                    pop       edi
  0050034d  5e                    pop       esi
  0050034e  5d                    pop       ebp
  0050034f  5b                    pop       ebx
  00500350  83c42c                add       esp,2Ch
  00500353  c3                    ret

LAB_00500354:
  00500354  c6464201              mov       byte [esi+42h],1
  00500358  c6464301              mov       byte [esi+43h],1
  0050035c  5f                    pop       edi
  0050035d  5e                    pop       esi
  0050035e  5d                    pop       ebp
  0050035f  5b                    pop       ebx
  00500360  83c42c                add       esp,2Ch
  00500363  c3                    ret

LAB_00500364:
  00500364  83fa3c                cmp       edx,3Ch
  00500367  7d27                  jge       short 00500390h

LAB_00500369:
  00500369  c6464200              mov       byte [esi+42h],0
  0050036d  e8ce6d0400            call      00547140h
  00500372  99                    cdq
  00500373  b90a000000            mov       ecx,0Ah
  00500378  f7f9                  idiv      ecx
  0050037a  8b442410              mov       eax,[esp+10h]
  0050037e  d1f8                  sar       eax,1
  00500380  02d0                  add       dl,al
  00500382  80c214                add       dl,14h
  00500385  885643                mov       [esi+43h],dl
  00500388  5f                    pop       edi
  00500389  5e                    pop       esi
  0050038a  5d                    pop       ebp
  0050038b  5b                    pop       ebx
  0050038c  83c42c                add       esp,2Ch
  0050038f  c3                    ret

LAB_00500390:
  00500390  c6464203              mov       byte [esi+42h],3
  00500394  e8a76d0400            call      00547140h
  00500399  99                    cdq
  0050039a  b90a000000            mov       ecx,0Ah
  0050039f  f7f9                  idiv      ecx
  005003a1  8b442410              mov       eax,[esp+10h]
  005003a5  d1f8                  sar       eax,1
  005003a7  02d0                  add       dl,al
  005003a9  80c214                add       dl,14h
  005003ac  885643                mov       [esi+43h],dl
  005003af  e88c6d0400            call      00547140h
  005003b4  66894610              mov       [esi+10h],ax

LAB_005003b8:
  005003b8  5f                    pop       edi
  005003b9  5e                    pop       esi
  005003ba  5d                    pop       ebp
  005003bb  5b                    pop       ebx
  005003bc  83c42c                add       esp,2Ch
  005003bf  c3                    ret
  005003c0  c9                    leave
  005003c1  fd                    std
  005003c2  4f                    dec       edi
  005003c3  00e0                  add       al,ah
  005003c5  fd                    std
  005003c6  4f                    dec       edi
  005003c7  0035fe4f0083          add       [83004FFEh],dh
  005003cd  fe4f00                dec       byte [edi]
  005003d0  0cff                  or        al,0FFh
  005003d2  4f                    dec       edi
  005003d3  0043ff                add       [ebx-1],al
  005003d6  4f                    dec       edi
  005003d7  009eff4f0026          add       [esi+26004FFFh],bl
  005003dd  005000                add       [eax],dl

39 cible(s) de saut interne : 0x004ffd08, 0x004ffd5d, 0x004ffd67, 0x004ffd6f, 0x004ffd8a, 0x004ffdb2, 0x004ffdfc, 0x004ffe44, 0x004ffe6c, 0x004ffeef, 0x004fff1c, 0x004fff2f, 0x004fff53, 0x004fff82, 0x004fff92, 0x004fff96, 0x004fffae, 0x004fffc1, 0x004fffcd, 0x00500001, 0x00500011, 0x00500032, 0x00500042, 0x005000ac, 0x005000ba, 0x005000bc, 0x005000c0, 0x005001d8, 0x00500235, 0x0050027b, 0x005002ce, 0x005002d7, 0x00500304, 0x00500336, 0x00500354, 0x00500364, 0x00500369, 0x00500390, 0x005003b8
