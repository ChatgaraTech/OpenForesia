Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004965b0  section .text  offset fichier 0x959b0  3520 octets

  004965b0  a1f02c5f00            mov       eax,[5F2CF0h]
  004965b5  33d2                  xor       edx,edx
  004965b7  b914000000            mov       ecx,14h
  004965bc  83ec28                sub       esp,28h
  004965bf  f7f1                  div       ecx
  004965c1  53                    push      ebx
  004965c2  55                    push      ebp
  004965c3  56                    push      esi
  004965c4  8b742438              mov       esi,[esp+38h]
  004965c8  57                    push      edi
  004965c9  85d2                  test      edx,edx
  004965cb  7526                  jne       short 004965F3h
  004965cd  33c0                  xor       eax,eax
  004965cf  8a4608                mov       al,[esi+8]
  004965d2  8d04c0                lea       eax,[eax+eax*8]
  004965d5  c1e005                shl       eax,5
  004965d8  0560a55e00            add       eax,5EA560h
  004965dd  6683404c08            add       word [eax+4Ch],8
  004965e2  668b504c              mov       dx,[eax+4Ch]
  004965e6  668b4848              mov       cx,[eax+48h]
  004965ea  663bd1                cmp       dx,cx
  004965ed  7e04                  jle       short 004965F3h
  004965ef  6689484c              mov       [eax+4Ch],cx

LAB_004965f3:
  004965f3  8d542410              lea       edx,[esp+10h]
  004965f7  8d442430              lea       eax,[esp+30h]
  004965fb  52                    push      edx
  004965fc  8d4c2430              lea       ecx,[esp+30h]
  00496600  50                    push      eax
  00496601  51                    push      ecx
  00496602  56                    push      esi
  00496603  e878230a00            call      00538980h
  00496608  83c410                add       esp,10h
  0049660b  8d542428              lea       edx,[esp+28h]
  0049660f  8d442424              lea       eax,[esp+24h]
  00496613  8d4c243c              lea       ecx,[esp+3Ch]
  00496617  52                    push      edx
  00496618  50                    push      eax
  00496619  8d542428              lea       edx,[esp+28h]
  0049661d  51                    push      ecx
  0049661e  8d442440              lea       eax,[esp+40h]
  00496622  52                    push      edx
  00496623  50                    push      eax
  00496624  56                    push      esi
  00496625  e8f6f80900            call      00535F20h
  0049662a  8b44243c              mov       eax,[esp+3Ch]
  0049662e  83c418                add       esp,18h
  00496631  85c0                  test      eax,eax
  00496633  0f8483080000          je        near 00496EBCh
  00496639  56                    push      esi
  0049663a  e8614f0400            call      004DB5A0h
  0049663f  8b06                  mov       eax,[esi]
  00496641  83c404                add       esp,4
  00496644  a802                  test      al,2
  00496646  0f84fd070000          je        near 00496E49h
  0049664c  f644242810            test      byte [esp+28h],10h
  00496651  740d                  je        short 00496660h
  00496653  24fd                  and       al,0FDh
  00496655  c6464300              mov       byte [esi+43h],0
  00496659  8906                  mov       [esi],eax
  0049665b  e95c080000            jmp       00496EBCh

LAB_00496660:
  00496660  33c9                  xor       ecx,ecx
  00496662  8a4e42                mov       cl,[esi+42h]
  00496665  83f904                cmp       ecx,4
  00496668  0f874e080000          ja        near 00496EBCh
  0049666e  ff248d4c734900        jmp       dword [ecx*4+49734Ch]
  00496675  668b5610              mov       dx,[esi+10h]
  00496679  0fbfca                movsx     ecx,dx
  0049667c  83e900                sub       ecx,0
  0049667f  0f84e3000000          je        near 00496768h
  00496685  49                    dec       ecx
  00496686  747a                  je        short 00496702h
  00496688  49                    dec       ecx
  00496689  0f852d080000          jne       near 00496EBCh
  0049668f  8b465c                mov       eax,[esi+5Ch]
  00496692  8bc8                  mov       ecx,eax
  00496694  c1f904                sar       ecx,4
  00496697  2bc1                  sub       eax,ecx
  00496699  66837e1a05            cmp       word [esi+1Ah],5
  0049669e  89465c                mov       [esi+5Ch],eax
  004966a1  751d                  jne       short 004966C0h
  004966a3  4a                    dec       edx
  004966a4  66895610              mov       [esi+10h],dx
  004966a8  8d561c                lea       edx,[esi+1Ch]
  004966ab  52                    push      edx
  004966ac  6800180000            push      1800h
  004966b1  6884030000            push      384h
  004966b6  6a36                  push      36h
  004966b8  e863740a00            call      0053DB20h
  004966bd  83c410                add       esp,10h

LAB_004966c0:
  004966c0  8b44243c              mov       eax,[esp+3Ch]
  004966c4  8d4840                lea       ecx,[eax+40h]
  004966c7  81e1ff0f0000          and       ecx,0FFFh
  004966cd  81f980000000          cmp       ecx,80h
  004966d3  7f09                  jg        short 004966DEh
  004966d5  66014632              add       [esi+32h],ax
  004966d9  e9de070000            jmp       00496EBCh

LAB_004966de:
  004966de  25ff0f0000            and       eax,0FFFh
  004966e3  33d2                  xor       edx,edx
  004966e5  3d00080000            cmp       eax,800h
  004966ea  0f9dc2                setge     dl
  004966ed  4a                    dec       edx
  004966ee  81e280000000          and       edx,80h
  004966f4  83c2c0                add       edx,0FFFFFFC0h
  004966f7  8bc2                  mov       eax,edx
  004966f9  66014632              add       [esi+32h],ax
  004966fd  e9ba070000            jmp       00496EBCh

LAB_00496702:
  00496702  0fbf4632              movsx     eax,word [esi+32h]
  00496706  50                    push      eax
  00496707  c7465c00900100        mov       dword [esi+5Ch],19000h
  0049670e  e809f30a00            call      00545A1Ch
  00496713  0faf465c              imul      eax,[esi+5Ch]
  00496717  0fbf4e32              movsx     ecx,word [esi+32h]
  0049671b  8b5e50                mov       ebx,[esi+50h]
  0049671e  51                    push      ecx
  0049671f  c1f80c                sar       eax,0Ch
  00496722  03d8                  add       ebx,eax
  00496724  895e50                mov       [esi+50h],ebx
  00496727  e8eaf20a00            call      00545A16h
  0049672c  0faf465c              imul      eax,[esi+5Ch]
  00496730  8b4e58                mov       ecx,[esi+58h]
  00496733  8d561c                lea       edx,[esi+1Ch]
  00496736  c1f80c                sar       eax,0Ch
  00496739  03c8                  add       ecx,eax
  0049673b  8b06                  mov       eax,[esi]
  0049673d  52                    push      edx
  0049673e  0c01                  or        al,1
  00496740  66ff4e10              dec       word [esi+10h]
  00496744  6800180000            push      1800h
  00496749  6800040000            push      400h
  0049674e  894e58                mov       [esi+58h],ecx
  00496751  c7465400200300        mov       dword [esi+54h],32000h
  00496758  8906                  mov       [esi],eax
  0049675a  c7465c00000000        mov       dword [esi+5Ch],0
  00496761  6a36                  push      36h
  00496763  e94c070000            jmp       00496EB4h

LAB_00496768:
  00496768  a900000002            test      eax,2000000h
  0049676d  7408                  je        short 00496777h
  0049676f  a801                  test      al,1
  00496771  7504                  jne       short 00496777h
  00496773  24dd                  and       al,0DDh
  00496775  8906                  mov       [esi],eax

LAB_00496777:
  00496777  f60620                test      byte [esi],20h
  0049677a  0f853c070000          jne       near 00496EBCh
  00496780  8b4654                mov       eax,[esi+54h]
  00496783  85c0                  test      eax,eax
  00496785  0f8e31070000          jle       near 00496EBCh
  0049678b  8d6e1c                lea       ebp,[esi+1Ch]
  0049678e  6a00                  push      0
  00496790  6808295500            push      552908h
  00496795  55                    push      ebp
  00496796  56                    push      esi
  00496797  e894340800            call      00519C30h
  0049679c  8bf8                  mov       edi,eax
  0049679e  83c410                add       esp,10h
  004967a1  85ff                  test      edi,edi
  004967a3  7406                  je        short 004967ABh
  004967a5  8b06                  mov       eax,[esi]
  004967a7  0c20                  or        al,20h
  004967a9  8906                  mov       [esi],eax

LAB_004967ab:
  004967ab  a1b8e05d00            mov       eax,[5DE0B8h]
  004967b0  85c0                  test      eax,eax
  004967b2  0f8404070000          je        near 00496EBCh
  004967b8  8b1f                  mov       ebx,[edi]
  004967ba  66c787960000001000    mov       word [edi+96h],10h
  004967c3  83cb40                or        ebx,40h
  004967c6  57                    push      edi
  004967c7  891f                  mov       [edi],ebx
  004967c9  8b4c2414              mov       ecx,[esp+14h]
  004967cd  56                    push      esi
  004967ce  668b4130              mov       ax,[ecx+30h]
  004967d2  0fbfd8                movsx     ebx,ax
  004967d5  66d1f8                sar       ax,1
  004967d8  66894130              mov       [ecx+30h],ax
  004967dc  e89f330a00            call      00539B80h
  004967e1  8b442418              mov       eax,[esp+18h]
  004967e5  83c408                add       esp,8
  004967e8  66895830              mov       [eax+30h],bx
  004967ec  8d5f1c                lea       ebx,[edi+1Ch]
  004967ef  53                    push      ebx
  004967f0  6800180000            push      1800h
  004967f5  6800030000            push      300h
  004967fa  6a03                  push      3
  004967fc  e81f730a00            call      0053DB20h
  00496801  8b07                  mov       eax,[edi]
  00496803  83c410                add       esp,10h
  00496806  a900000040            test      eax,40000000h
  0049680b  0f85ab060000          jne       near 00496EBCh
  00496811  0fbf4e32              movsx     ecx,word [esi+32h]
  00496815  51                    push      ecx
  00496816  e801f20a00            call      00545A1Ch
  0049681b  8d0440                lea       eax,[eax+eax*2]
  0049681e  8d1480                lea       edx,[eax+eax*4]
  00496821  d1e2                  shl       edx,1
  00496823  895750                mov       [edi+50h],edx
  00496826  0fbf4632              movsx     eax,word [esi+32h]
  0049682a  50                    push      eax
  0049682b  e8e6f10a00            call      00545A16h
  00496830  8d0440                lea       eax,[eax+eax*2]
  00496833  8d0c80                lea       ecx,[eax+eax*4]
  00496836  8b07                  mov       eax,[edi]
  00496838  d1e1                  shl       ecx,1
  0049683a  a801                  test      al,1
  0049683c  894f58                mov       [edi+58h],ecx
  0049683f  750b                  jne       short 0049684Ch
  00496841  0c01                  or        al,1
  00496843  c7475400800100        mov       dword [edi+54h],18000h
  0049684a  8907                  mov       [edi],eax

LAB_0049684c:
  0049684c  8b5500                mov       edx,[ebp]
  0049684f  8b4620                mov       eax,[esi+20h]
  00496852  8b4e24                mov       ecx,[esi+24h]
  00496855  89542414              mov       [esp+14h],edx
  00496859  8b13                  mov       edx,[ebx]
  0049685b  89442418              mov       [esp+18h],eax
  0049685f  895500                mov       [ebp],edx
  00496862  8b4720                mov       eax,[edi+20h]
  00496865  6aff                  push      0FFFFFFFFh
  00496867  894c2420              mov       [esp+20h],ecx
  0049686b  894620                mov       [esi+20h],eax
  0049686e  8b4f24                mov       ecx,[edi+24h]
  00496871  6a00                  push      0
  00496873  6a39                  push      39h
  00496875  56                    push      esi
  00496876  894e24                mov       [esi+24h],ecx
  00496879  e85241feff            call      0047A9D0h
  0049687e  8b542424              mov       edx,[esp+24h]
  00496882  8b442428              mov       eax,[esp+28h]
  00496886  8b4c242c              mov       ecx,[esp+2Ch]
  0049688a  83c410                add       esp,10h
  0049688d  895500                mov       [ebp],edx
  00496890  894620                mov       [esi+20h],eax
  00496893  894e24                mov       [esi+24h],ecx
  00496896  e921060000            jmp       00496EBCh
  0049689b  668b4e1a              mov       cx,[esi+1Ah]
  0049689f  6683f911              cmp       cx,11h
  004968a3  7d71                  jge       short 00496916h
  004968a5  8b44243c              mov       eax,[esp+3Ch]
  004968a9  8d5040                lea       edx,[eax+40h]
  004968ac  81e2ff0f0000          and       edx,0FFFh
  004968b2  81fa80000000          cmp       edx,80h
  004968b8  7e1b                  jle       short 004968D5h
  004968ba  25ff0f0000            and       eax,0FFFh
  004968bf  33d2                  xor       edx,edx
  004968c1  3d00080000            cmp       eax,800h
  004968c6  0f9dc2                setge     dl
  004968c9  4a                    dec       edx
  004968ca  81e280000000          and       edx,80h
  004968d0  83c2c0                add       edx,0FFFFFFC0h
  004968d3  8bc2                  mov       eax,edx

LAB_004968d5:
  004968d5  66014632              add       [esi+32h],ax
  004968d9  6683f901              cmp       cx,1
  004968dd  7518                  jne       short 004968F7h
  004968df  8d461c                lea       eax,[esi+1Ch]
  004968e2  50                    push      eax
  004968e3  6800180000            push      1800h
  004968e8  6800040000            push      400h
  004968ed  6a20                  push      20h
  004968ef  e82c720a00            call      0053DB20h
  004968f4  83c410                add       esp,10h

LAB_004968f7:
  004968f7  66837e1a10            cmp       word [esi+1Ah],10h
  004968fc  7518                  jne       short 00496916h
  004968fe  8d4e1c                lea       ecx,[esi+1Ch]
  00496901  51                    push      ecx
  00496902  6800180000            push      1800h
  00496907  6800020000            push      200h
  0049690c  6a24                  push      24h
  0049690e  e80d720a00            call      0053DB20h
  00496913  83c410                add       esp,10h

LAB_00496916:
  00496916  66837e1a11            cmp       word [esi+1Ah],11h
  0049691b  7558                  jne       short 00496975h
  0049691d  0fbf5632              movsx     edx,word [esi+32h]
  00496921  52                    push      edx
  00496922  c7465c00000300        mov       dword [esi+5Ch],30000h
  00496929  e8eef00a00            call      00545A1Ch
  0049692e  0faf465c              imul      eax,[esi+5Ch]
  00496932  8b7e50                mov       edi,[esi+50h]
  00496935  c1f80c                sar       eax,0Ch
  00496938  03f8                  add       edi,eax
  0049693a  0fbf4632              movsx     eax,word [esi+32h]
  0049693e  50                    push      eax
  0049693f  897e50                mov       [esi+50h],edi
  00496942  e8cff00a00            call      00545A16h
  00496947  0faf465c              imul      eax,[esi+5Ch]
  0049694b  8b6e58                mov       ebp,[esi+58h]
  0049694e  8d4e1c                lea       ecx,[esi+1Ch]
  00496951  c1f80c                sar       eax,0Ch
  00496954  51                    push      ecx
  00496955  6800180000            push      1800h
  0049695a  03e8                  add       ebp,eax
  0049695c  682a030000            push      32Ah
  00496961  6a25                  push      25h
  00496963  896e58                mov       [esi+58h],ebp
  00496966  c7465c00000000        mov       dword [esi+5Ch],0
  0049696d  e8ae710a00            call      0053DB20h
  00496972  83c410                add       esp,10h

LAB_00496975:
  00496975  66837e1a12            cmp       word [esi+1Ah],12h
  0049697a  0f8c12010000          jl        near 00496A92h
  00496980  f60620                test      byte [esi],20h
  00496983  0f8509010000          jne       near 00496A92h
  00496989  8d5e1c                lea       ebx,[esi+1Ch]
  0049698c  6a00                  push      0
  0049698e  6814295500            push      552914h
  00496993  53                    push      ebx
  00496994  56                    push      esi
  00496995  e896320800            call      00519C30h
  0049699a  8bf8                  mov       edi,eax
  0049699c  83c410                add       esp,10h
  0049699f  85ff                  test      edi,edi
  004969a1  7406                  je        short 004969A9h
  004969a3  8b06                  mov       eax,[esi]
  004969a5  0c20                  or        al,20h
  004969a7  8906                  mov       [esi],eax

LAB_004969a9:
  004969a9  a1b8e05d00            mov       eax,[5DE0B8h]
  004969ae  85c0                  test      eax,eax
  004969b0  0f84dc000000          je        near 00496A92h
  004969b6  8b2f                  mov       ebp,[edi]
  004969b8  66c787960000001000    mov       word [edi+96h],10h
  004969c1  83cd40                or        ebp,40h
  004969c4  57                    push      edi
  004969c5  892f                  mov       [edi],ebp
  004969c7  8b4c2414              mov       ecx,[esp+14h]
  004969cb  56                    push      esi
  004969cc  668b4130              mov       ax,[ecx+30h]
  004969d0  0fbfe8                movsx     ebp,ax
  004969d3  66d1f8                sar       ax,1
  004969d6  66894130              mov       [ecx+30h],ax
  004969da  e8a1310a00            call      00539B80h
  004969df  8b542418              mov       edx,[esp+18h]
  004969e3  83c408                add       esp,8
  004969e6  66896a30              mov       [edx+30h],bp
  004969ea  8d6f1c                lea       ebp,[edi+1Ch]
  004969ed  55                    push      ebp
  004969ee  6800180000            push      1800h
  004969f3  6800030000            push      300h
  004969f8  6a03                  push      3
  004969fa  e821710a00            call      0053DB20h
  004969ff  8b07                  mov       eax,[edi]
  00496a01  83c410                add       esp,10h
  00496a04  a900000040            test      eax,40000000h
  00496a09  0f8583000000          jne       near 00496A92h
  00496a0f  0fbf4632              movsx     eax,word [esi+32h]
  00496a13  50                    push      eax
  00496a14  e803f00a00            call      00545A1Ch
  00496a19  8d0440                lea       eax,[eax+eax*2]
  00496a1c  8d0c80                lea       ecx,[eax+eax*4]
  00496a1f  d1e1                  shl       ecx,1
  00496a21  894f50                mov       [edi+50h],ecx
  00496a24  0fbf5632              movsx     edx,word [esi+32h]
  00496a28  52                    push      edx
  00496a29  e8e8ef0a00            call      00545A16h
  00496a2e  8d0440                lea       eax,[eax+eax*2]
  00496a31  8d0480                lea       eax,[eax+eax*4]
  00496a34  d1e0                  shl       eax,1
  00496a36  894758                mov       [edi+58h],eax
  00496a39  8b07                  mov       eax,[edi]
  00496a3b  a801                  test      al,1
  00496a3d  750b                  jne       short 00496A4Ah
  00496a3f  0c01                  or        al,1
  00496a41  c7475400800100        mov       dword [edi+54h],18000h
  00496a48  8907                  mov       [edi],eax

LAB_00496a4a:
  00496a4a  8b0b                  mov       ecx,[ebx]
  00496a4c  8b5620                mov       edx,[esi+20h]
  00496a4f  8b4624                mov       eax,[esi+24h]
  00496a52  894c2414              mov       [esp+14h],ecx
  00496a56  8b4d00                mov       ecx,[ebp]
  00496a59  89542418              mov       [esp+18h],edx
  00496a5d  890b                  mov       [ebx],ecx
  00496a5f  8b5720                mov       edx,[edi+20h]
  00496a62  6aff                  push      0FFFFFFFFh
  00496a64  89442420              mov       [esp+20h],eax
  00496a68  895620                mov       [esi+20h],edx
  00496a6b  8b4724                mov       eax,[edi+24h]
  00496a6e  6a00                  push      0
  00496a70  6a39                  push      39h
  00496a72  56                    push      esi
  00496a73  894624                mov       [esi+24h],eax
  00496a76  e8553ffeff            call      0047A9D0h
  00496a7b  8b4c2424              mov       ecx,[esp+24h]
  00496a7f  8b542428              mov       edx,[esp+28h]
  00496a83  8b44242c              mov       eax,[esp+2Ch]
  00496a87  83c410                add       esp,10h
  00496a8a  890b                  mov       [ebx],ecx
  00496a8c  895620                mov       [esi+20h],edx
  00496a8f  894624                mov       [esi+24h],eax

LAB_00496a92:
  00496a92  8b06                  mov       eax,[esi]
  00496a94  a900000002            test      eax,2000000h
  00496a99  0f84a1030000          je        near 00496E40h
  00496a9f  24dd                  and       al,0DDh
  00496aa1  c7465c00000000        mov       dword [esi+5Ch],0
  00496aa8  8906                  mov       [esi],eax
  00496aaa  e90d040000            jmp       00496EBCh
  00496aaf  8b44243c              mov       eax,[esp+3Ch]
  00496ab3  8d4840                lea       ecx,[eax+40h]
  00496ab6  81e1ff0f0000          and       ecx,0FFFh
  00496abc  81f980000000          cmp       ecx,80h
  00496ac2  7e1b                  jle       short 00496ADFh
  00496ac4  25ff0f0000            and       eax,0FFFh
  00496ac9  33d2                  xor       edx,edx
  00496acb  3d00080000            cmp       eax,800h
  00496ad0  0f9dc2                setge     dl
  00496ad3  4a                    dec       edx
  00496ad4  81e280000000          and       edx,80h
  00496ada  83c2c0                add       edx,0FFFFFFC0h
  00496add  8bc2                  mov       eax,edx

LAB_00496adf:
  00496adf  66014632              add       [esi+32h],ax
  00496ae3  8d5e1c                lea       ebx,[esi+1Ch]
  00496ae6  6a00                  push      0
  00496ae8  6808295500            push      552908h
  00496aed  53                    push      ebx
  00496aee  56                    push      esi
  00496aef  e83c310800            call      00519C30h
  00496af4  8bf8                  mov       edi,eax
  00496af6  83c410                add       esp,10h
  00496af9  85ff                  test      edi,edi
  00496afb  7406                  je        short 00496B03h
  00496afd  8b06                  mov       eax,[esi]
  00496aff  0c20                  or        al,20h
  00496b01  8906                  mov       [esi],eax

LAB_00496b03:
  00496b03  a1b8e05d00            mov       eax,[5DE0B8h]
  00496b08  85c0                  test      eax,eax
  00496b0a  7432                  je        short 00496B3Eh
  00496b0c  8b2f                  mov       ebp,[edi]
  00496b0e  57                    push      edi
  00496b0f  83cd40                or        ebp,40h
  00496b12  56                    push      esi
  00496b13  66c787960000000600    mov       word [edi+96h],6
  00496b1c  892f                  mov       [edi],ebp
  00496b1e  e85d300a00            call      00539B80h
  00496b23  83c408                add       esp,8
  00496b26  83c71c                add       edi,1Ch
  00496b29  57                    push      edi
  00496b2a  6800180000            push      1800h
  00496b2f  6800030000            push      300h
  00496b34  6a03                  push      3
  00496b36  e8e56f0a00            call      0053DB20h
  00496b3b  83c410                add       esp,10h

LAB_00496b3e:
  00496b3e  668b461a              mov       ax,[esi+1Ah]
  00496b42  663d0100              cmp       ax,1
  00496b46  7406                  je        short 00496B4Eh
  00496b48  663d0b00              cmp       ax,0Bh
  00496b4c  7515                  jne       short 00496B63h

LAB_00496b4e:
  00496b4e  53                    push      ebx
  00496b4f  6800180000            push      1800h
  00496b54  682a030000            push      32Ah
  00496b59  6a36                  push      36h
  00496b5b  e8c06f0a00            call      0053DB20h
  00496b60  83c410                add       esp,10h

LAB_00496b63:
  00496b63  8b06                  mov       eax,[esi]
  00496b65  a900000002            test      eax,2000000h
  00496b6a  741a                  je        short 00496B86h
  00496b6c  25fffffffe            and       eax,0FEFFFFFFh
  00496b71  c7461803000000        mov       dword [esi+18h],3
  00496b78  0c02                  or        al,2
  00496b7a  c6464200              mov       byte [esi+42h],0
  00496b7e  8906                  mov       [esi],eax
  00496b80  66c746100200          mov       word [esi+10h],2

LAB_00496b86:
  00496b86  c7465c00c00000        mov       dword [esi+5Ch],0C000h
  00496b8d  e92a030000            jmp       00496EBCh
  00496b92  668b5610              mov       dx,[esi+10h]
  00496b96  0fbfca                movsx     ecx,dx
  00496b99  83e900                sub       ecx,0
  00496b9c  0f84b9000000          je        near 00496C5Bh
  00496ba2  49                    dec       ecx
  00496ba3  7462                  je        short 00496C07h
  00496ba5  49                    dec       ecx
  00496ba6  0f8510030000          jne       near 00496EBCh
  00496bac  8b465c                mov       eax,[esi+5Ch]
  00496baf  8bc8                  mov       ecx,eax
  00496bb1  c1f904                sar       ecx,4
  00496bb4  2bc1                  sub       eax,ecx
  00496bb6  66837e1a04            cmp       word [esi+1Ah],4
  00496bbb  89465c                mov       [esi+5Ch],eax
  00496bbe  7505                  jne       short 00496BC5h
  00496bc0  4a                    dec       edx
  00496bc1  66895610              mov       [esi+10h],dx

LAB_00496bc5:
  00496bc5  8b44243c              mov       eax,[esp+3Ch]
  00496bc9  8d5040                lea       edx,[eax+40h]
  00496bcc  81e2ff0f0000          and       edx,0FFFh
  00496bd2  81fa80000000          cmp       edx,80h
  00496bd8  7f09                  jg        short 00496BE3h
  00496bda  66014632              add       [esi+32h],ax
  00496bde  e9d9020000            jmp       00496EBCh

LAB_00496be3:
  00496be3  25ff0f0000            and       eax,0FFFh
  00496be8  33c9                  xor       ecx,ecx
  00496bea  3d00080000            cmp       eax,800h
  00496bef  0f9dc1                setge     cl
  00496bf2  49                    dec       ecx
  00496bf3  81e180000000          and       ecx,80h
  00496bf9  83c1c0                add       ecx,0FFFFFFC0h
  00496bfc  8bc1                  mov       eax,ecx
  00496bfe  66014632              add       [esi+32h],ax
  00496c02  e9b5020000            jmp       00496EBCh

LAB_00496c07:
  00496c07  0fbf5632              movsx     edx,word [esi+32h]
  00496c0b  bf00800200            mov       edi,28000h
  00496c10  52                    push      edx
  00496c11  897e5c                mov       [esi+5Ch],edi
  00496c14  e803ee0a00            call      00545A1Ch
  00496c19  0faf465c              imul      eax,[esi+5Ch]
  00496c1d  8b4e50                mov       ecx,[esi+50h]
  00496c20  c1f80c                sar       eax,0Ch
  00496c23  03c8                  add       ecx,eax
  00496c25  0fbf4632              movsx     eax,word [esi+32h]
  00496c29  50                    push      eax
  00496c2a  894e50                mov       [esi+50h],ecx
  00496c2d  e8e4ed0a00            call      00545A16h
  00496c32  0faf465c              imul      eax,[esi+5Ch]
  00496c36  8b5658                mov       edx,[esi+58h]
  00496c39  8b0e                  mov       ecx,[esi]
  00496c3b  c1f80c                sar       eax,0Ch
  00496c3e  03d0                  add       edx,eax
  00496c40  83c901                or        ecx,1
  00496c43  66ff4e10              dec       word [esi+10h]
  00496c47  895658                mov       [esi+58h],edx
  00496c4a  897e54                mov       [esi+54h],edi
  00496c4d  890e                  mov       [esi],ecx
  00496c4f  c7465c00000000        mov       dword [esi+5Ch],0
  00496c56  e961020000            jmp       00496EBCh

LAB_00496c5b:
  00496c5b  a900000002            test      eax,2000000h
  00496c60  7408                  je        short 00496C6Ah
  00496c62  a801                  test      al,1
  00496c64  7504                  jne       short 00496C6Ah
  00496c66  24dd                  and       al,0DDh
  00496c68  8906                  mov       [esi],eax

LAB_00496c6a:
  00496c6a  f60620                test      byte [esi],20h
  00496c6d  0f8549020000          jne       near 00496EBCh
  00496c73  8b4654                mov       eax,[esi+54h]
  00496c76  85c0                  test      eax,eax
  00496c78  0f8e3e020000          jle       near 00496EBCh
  00496c7e  8d6e1c                lea       ebp,[esi+1Ch]
  00496c81  6a00                  push      0
  00496c83  6808295500            push      552908h
  00496c88  55                    push      ebp
  00496c89  56                    push      esi
  00496c8a  e8a12f0800            call      00519C30h
  00496c8f  8bf8                  mov       edi,eax
  00496c91  83c410                add       esp,10h
  00496c94  85ff                  test      edi,edi
  00496c96  7406                  je        short 00496C9Eh
  00496c98  8b06                  mov       eax,[esi]
  00496c9a  0c20                  or        al,20h
  00496c9c  8906                  mov       [esi],eax

LAB_00496c9e:
  00496c9e  a1b8e05d00            mov       eax,[5DE0B8h]
  00496ca3  85c0                  test      eax,eax
  00496ca5  0f8411020000          je        near 00496EBCh
  00496cab  8b1f                  mov       ebx,[edi]
  00496cad  66c787960000001000    mov       word [edi+96h],10h
  00496cb6  83cb40                or        ebx,40h
  00496cb9  57                    push      edi
  00496cba  891f                  mov       [edi],ebx
  00496cbc  8b4c2414              mov       ecx,[esp+14h]
  00496cc0  56                    push      esi
  00496cc1  668b4130              mov       ax,[ecx+30h]
  00496cc5  0fbfd8                movsx     ebx,ax
  00496cc8  66d1f8                sar       ax,1
  00496ccb  66894130              mov       [ecx+30h],ax
  00496ccf  e8ac2e0a00            call      00539B80h
  00496cd4  8b4c2418              mov       ecx,[esp+18h]
  00496cd8  83c408                add       esp,8
  00496cdb  6a00                  push      0
  00496cdd  57                    push      edi
  00496cde  66895930              mov       [ecx+30h],bx
  00496ce2  56                    push      esi
  00496ce3  c6460707              mov       byte [esi+7],7
  00496ce7  e8244b0a00            call      0053B810h
  00496cec  83c40c                add       esp,0Ch
  00496cef  8d5f1c                lea       ebx,[edi+1Ch]
  00496cf2  53                    push      ebx
  00496cf3  6800180000            push      1800h
  00496cf8  6800030000            push      300h
  00496cfd  6a03                  push      3
  00496cff  e81c6e0a00            call      0053DB20h
  00496d04  8b07                  mov       eax,[edi]
  00496d06  83c410                add       esp,10h
  00496d09  a900000040            test      eax,40000000h
  00496d0e  0f85a8010000          jne       near 00496EBCh
  00496d14  0fbf5632              movsx     edx,word [esi+32h]
  00496d18  52                    push      edx
  00496d19  e8feec0a00            call      00545A1Ch
  00496d1e  8d0440                lea       eax,[eax+eax*2]
  00496d21  8d0480                lea       eax,[eax+eax*4]
  00496d24  d1e0                  shl       eax,1
  00496d26  894750                mov       [edi+50h],eax
  00496d29  0fbf4e32              movsx     ecx,word [esi+32h]
  00496d2d  51                    push      ecx
  00496d2e  e8e3ec0a00            call      00545A16h
  00496d33  8d0440                lea       eax,[eax+eax*2]
  00496d36  8d1480                lea       edx,[eax+eax*4]
  00496d39  8b07                  mov       eax,[edi]
  00496d3b  d1e2                  shl       edx,1
  00496d3d  a801                  test      al,1
  00496d3f  895758                mov       [edi+58h],edx
  00496d42  750b                  jne       short 00496D4Fh
  00496d44  0c01                  or        al,1
  00496d46  c7475400800100        mov       dword [edi+54h],18000h
  00496d4d  8907                  mov       [edi],eax

LAB_00496d4f:
  00496d4f  8b4500                mov       eax,[ebp]
  00496d52  8b4e20                mov       ecx,[esi+20h]
  00496d55  8b5624                mov       edx,[esi+24h]
  00496d58  89442414              mov       [esp+14h],eax
  00496d5c  8b03                  mov       eax,[ebx]
  00496d5e  894c2418              mov       [esp+18h],ecx
  00496d62  894500                mov       [ebp],eax
  00496d65  8b4f20                mov       ecx,[edi+20h]
  00496d68  6aff                  push      0FFFFFFFFh
  00496d6a  89542420              mov       [esp+20h],edx
  00496d6e  894e20                mov       [esi+20h],ecx
  00496d71  8b5724                mov       edx,[edi+24h]
  00496d74  6a00                  push      0
  00496d76  6a39                  push      39h
  00496d78  56                    push      esi
  00496d79  895624                mov       [esi+24h],edx
  00496d7c  e84f3cfeff            call      0047A9D0h
  00496d81  8b442424              mov       eax,[esp+24h]
  00496d85  8b4c2428              mov       ecx,[esp+28h]
  00496d89  8b54242c              mov       edx,[esp+2Ch]
  00496d8d  83c410                add       esp,10h
  00496d90  894500                mov       [ebp],eax
  00496d93  894e20                mov       [esi+20h],ecx
  00496d96  895624                mov       [esi+24h],edx
  00496d99  e91e010000            jmp       00496EBCh
  00496d9e  0fbf4e10              movsx     ecx,word [esi+10h]
  00496da2  83e900                sub       ecx,0
  00496da5  0f8487000000          je        near 00496E32h
  00496dab  49                    dec       ecx
  00496dac  0f858e000000          jne       near 00496E40h
  00496db2  66837e1a07            cmp       word [esi+1Ah],7
  00496db7  7518                  jne       short 00496DD1h
  00496db9  8d461c                lea       eax,[esi+1Ch]
  00496dbc  50                    push      eax
  00496dbd  6800180000            push      1800h
  00496dc2  6800020000            push      200h
  00496dc7  6a40                  push      40h
  00496dc9  e8526d0a00            call      0053DB20h
  00496dce  83c410                add       esp,10h

LAB_00496dd1:
  00496dd1  f70600000002          test      dword [esi],2000000h
  00496dd7  7467                  je        short 00496E40h
  00496dd9  8b7e44                mov       edi,[esi+44h]
  00496ddc  8b5e48                mov       ebx,[esi+48h]
  00496ddf  e85c030b00            call      00547140h
  00496de4  83e001                and       eax,1
  00496de7  c74648983a0000        mov       dword [esi+48h],3A98h
  00496dee  894644                mov       [esi+44h],eax
  00496df1  8b0dcc975e00          mov       ecx,[5E97CCh]
  00496df7  6a02                  push      2
  00496df9  6a00                  push      0
  00496dfb  8b91cc000000          mov       edx,[ecx+0CCh]
  00496e01  33c9                  xor       ecx,ecx
  00496e03  8b02                  mov       eax,[edx]
  00496e05  8a480e                mov       cl,[eax+0Eh]
  00496e08  81c1c0000000          add       ecx,0C0h
  00496e0e  51                    push      ecx
  00496e0f  56                    push      esi
  00496e10  e8bb3bfeff            call      0047A9D0h
  00496e15  83c410                add       esp,10h
  00496e18  66ff4e10              dec       word [esi+10h]
  00496e1c  897e44                mov       [esi+44h],edi
  00496e1f  895e48                mov       [esi+48h],ebx
  00496e22  c646430a              mov       byte [esi+43h],0Ah
  00496e26  c7465c00000000        mov       dword [esi+5Ch],0
  00496e2d  e98a000000            jmp       00496EBCh

LAB_00496e32:
  00496e32  8a4e43                mov       cl,[esi+43h]
  00496e35  fec9                  dec       cl
  00496e37  884e43                mov       [esi+43h],cl
  00496e3a  7504                  jne       short 00496E40h
  00496e3c  24fd                  and       al,0FDh
  00496e3e  8906                  mov       [esi],eax

LAB_00496e40:
  00496e40  c7465c00000000        mov       dword [esi+5Ch],0
  00496e47  eb73                  jmp       short 00496EBCh

LAB_00496e49:
  00496e49  a811                  test      al,11h
  00496e4b  756f                  jne       short 00496EBCh
  00496e4d  a904080000            test      eax,804h
  00496e52  7568                  jne       short 00496EBCh
  00496e54  33c9                  xor       ecx,ecx
  00496e56  8a4e42                mov       cl,[esi+42h]
  00496e59  83f903                cmp       ecx,3
  00496e5c  772b                  ja        short 00496E89h
  00496e5e  ff248d60734900        jmp       dword [ecx*4+497360h]
  00496e65  33c9                  xor       ecx,ecx
  00496e67  66394e18              cmp       [esi+18h],cx
  00496e6b  740a                  je        short 00496E77h
  00496e6d  0d00000001            or        eax,1000000h
  00496e72  894e18                mov       [esi+18h],ecx
  00496e75  8906                  mov       [esi],eax

LAB_00496e77:
  00496e77  817c24202c010000      cmp       dword [esp+20h],12Ch
  00496e7f  0f8d2a030000          jge       near 004971AFh
  00496e85  c6464300              mov       byte [esi+43h],0

LAB_00496e89:
  00496e89  bf01000000            mov       edi,1

LAB_00496e8e:
  00496e8e  66397e18              cmp       [esi+18h],di
  00496e92  7528                  jne       short 00496EBCh
  00496e94  668b461a              mov       ax,[esi+1Ah]
  00496e98  663d0400              cmp       ax,4
  00496e9c  7406                  je        short 00496EA4h
  00496e9e  663d0c00              cmp       ax,0Ch
  00496ea2  7518                  jne       short 00496EBCh

LAB_00496ea4:
  00496ea4  8d561c                lea       edx,[esi+1Ch]
  00496ea7  52                    push      edx
  00496ea8  6800180000            push      1800h
  00496ead  6852030000            push      352h
  00496eb2  6a35                  push      35h

LAB_00496eb4:
  00496eb4  e8676c0a00            call      0053DB20h
  00496eb9  83c410                add       esp,10h

LAB_00496ebc:
  00496ebc  8b442424              mov       eax,[esp+24h]
  00496ec0  66c7869a000000ff7f    mov       word [esi+9Ah],7FFFh
  00496ec9  50                    push      eax
  00496eca  56                    push      esi
  00496ecb  e89012ffff            call      00488160h
  00496ed0  83c408                add       esp,8
  00496ed3  56                    push      esi
  00496ed4  e867ca0900            call      00533940h
  00496ed9  83c404                add       esp,4
  00496edc  56                    push      esi
  00496edd  e80e8c0400            call      004DFAF0h
  00496ee2  8b4c2430              mov       ecx,[esp+30h]
  00496ee6  83c404                add       esp,4
  00496ee9  51                    push      ecx
  00496eea  56                    push      esi
  00496eeb  e880c80400            call      004E3770h
  00496ef0  8b44242c              mov       eax,[esp+2Ch]
  00496ef4  83c408                add       esp,8
  00496ef7  85c0                  test      eax,eax
  00496ef9  0f84a8020000          je        near 004971A7h
  00496eff  f70617080000          test      dword [esi],817h
  00496f05  0f859c020000          jne       near 004971A7h
  00496f0b  8a4643                mov       al,[esi+43h]
  00496f0e  84c0                  test      al,al
  00496f10  0f8591020000          jne       near 004971A7h
  00496f16  8b466c                mov       eax,[esi+6Ch]
  00496f19  8b5e1c                mov       ebx,[esi+1Ch]
  00496f1c  8b4e24                mov       ecx,[esi+24h]
  00496f1f  8b781c                mov       edi,[eax+1Ch]
  00496f22  8b6824                mov       ebp,[eax+24h]
  00496f25  2bfb                  sub       edi,ebx
  00496f27  2be9                  sub       ebp,ecx
  00496f29  c1ff0c                sar       edi,0Ch
  00496f2c  c1fd0c                sar       ebp,0Ch
  00496f2f  8bd7                  mov       edx,edi
  00496f31  8bc5                  mov       eax,ebp
  00496f33  0fafd7                imul      edx,edi
  00496f36  0fafc5                imul      eax,ebp
  00496f39  03d0                  add       edx,eax
  00496f3b  52                    push      edx
  00496f3c  e8cfea0a00            call      00545A10h
  00496f41  55                    push      ebp
  00496f42  57                    push      edi
  00496f43  89442428              mov       [esp+28h],eax
  00496f47  e8d6ea0a00            call      00545A22h
  00496f4c  668b4e32              mov       cx,[esi+32h]
  00496f50  2bc1                  sub       eax,ecx
  00496f52  25ff0f0000            and       eax,0FFFh
  00496f57  8944243c              mov       [esp+3Ch],eax
  00496f5b  e8e0010b00            call      00547140h
  00496f60  bf64000000            mov       edi,64h
  00496f65  99                    cdq
  00496f66  8bcf                  mov       ecx,edi
  00496f68  f7f9                  idiv      ecx
  00496f6a  817c24202c010000      cmp       dword [esp+20h],12Ch
  00496f72  0f8d36010000          jge       near 004970AEh
  00496f78  83fa32                cmp       edx,32h
  00496f7b  7d64                  jge       short 00496FE1h
  00496f7d  8b44243c              mov       eax,[esp+3Ch]
  00496f81  0500010000            add       eax,100h
  00496f86  25ff0f0000            and       eax,0FFFh
  00496f8b  3d00020000            cmp       eax,200h
  00496f90  0f8f7d010000          jg        near 00497113h
  00496f96  f6c201                test      dl,1
  00496f99  7426                  je        short 00496FC1h
  00496f9b  8b0e                  mov       ecx,[esi]
  00496f9d  c7461803000000        mov       dword [esi+18h],3
  00496fa4  81e1fffffffe          and       ecx,0FEFFFFFFh
  00496faa  c6464200              mov       byte [esi+42h],0
  00496fae  83c902                or        ecx,2
  00496fb1  66c746100200          mov       word [esi+10h],2
  00496fb7  890e                  mov       [esi],ecx
  00496fb9  5f                    pop       edi
  00496fba  5e                    pop       esi
  00496fbb  5d                    pop       ebp
  00496fbc  5b                    pop       ebx
  00496fbd  83c428                add       esp,28h
  00496fc0  c3                    ret

LAB_00496fc1:
  00496fc1  8b16                  mov       edx,[esi]
  00496fc3  c7461805000000        mov       dword [esi+18h],5
  00496fca  81e2fffffffe          and       edx,0FEFFFFFFh
  00496fd0  c6464202              mov       byte [esi+42h],2
  00496fd4  83ca02                or        edx,2
  00496fd7  8916                  mov       [esi],edx
  00496fd9  5f                    pop       edi
  00496fda  5e                    pop       esi
  00496fdb  5d                    pop       ebp
  00496fdc  5b                    pop       ebx
  00496fdd  83c428                add       esp,28h
  00496fe0  c3                    ret

LAB_00496fe1:
  00496fe1  83fa4b                cmp       edx,4Bh
  00496fe4  7d23                  jge       short 00497009h
  00496fe6  c6464202              mov       byte [esi+42h],2
  00496fea  e851010b00            call      00547140h
  00496fef  99                    cdq
  00496ff0  b90a000000            mov       ecx,0Ah
  00496ff5  66897e10              mov       [esi+10h],di
  00496ff9  f7f9                  idiv      ecx
  00496ffb  80c214                add       dl,14h
  00496ffe  885643                mov       [esi+43h],dl
  00497001  5f                    pop       edi
  00497002  5e                    pop       esi
  00497003  5d                    pop       ebp
  00497004  5b                    pop       ebx
  00497005  83c428                add       esp,28h
  00497008  c3                    ret

LAB_00497009:
  00497009  83fa5a                cmp       edx,5Ah
  0049700c  7d60                  jge       short 0049706Eh
  0049700e  8b44243c              mov       eax,[esp+3Ch]
  00497012  0500010000            add       eax,100h
  00497017  25ff0f0000            and       eax,0FFFh
  0049701c  3d00020000            cmp       eax,200h
  00497021  7f4b                  jg        short 0049706Eh
  00497023  f6c201                test      dl,1
  00497026  7420                  je        short 00497048h
  00497028  8b0e                  mov       ecx,[esi]
  0049702a  c7461804000000        mov       dword [esi+18h],4
  00497031  81e1fffffffe          and       ecx,0FEFFFFFFh
  00497037  c6464201              mov       byte [esi+42h],1
  0049703b  83c902                or        ecx,2
  0049703e  890e                  mov       [esi],ecx
  00497040  5f                    pop       edi
  00497041  5e                    pop       esi
  00497042  5d                    pop       ebp
  00497043  5b                    pop       ebx
  00497044  83c428                add       esp,28h
  00497047  c3                    ret

LAB_00497048:
  00497048  8b16                  mov       edx,[esi]
  0049704a  c7461806000000        mov       dword [esi+18h],6
  00497051  81e2fffffffe          and       edx,0FEFFFFFFh
  00497057  c6464203              mov       byte [esi+42h],3
  0049705b  83ca02                or        edx,2
  0049705e  66c746100200          mov       word [esi+10h],2
  00497064  8916                  mov       [esi],edx
  00497066  5f                    pop       edi
  00497067  5e                    pop       esi
  00497068  5d                    pop       ebp
  00497069  5b                    pop       ebx
  0049706a  83c428                add       esp,28h
  0049706d  c3                    ret

LAB_0049706e:
  0049706e  c6464203              mov       byte [esi+42h],3
  00497072  e8c9000b00            call      00547140h
  00497077  99                    cdq
  00497078  b90a000000            mov       ecx,0Ah
  0049707d  f7f9                  idiv      ecx
  0049707f  80c214                add       dl,14h
  00497082  885643                mov       [esi+43h],dl
  00497085  e8b6000b00            call      00547140h
  0049708a  99                    cdq
  0049708b  b9000c0000            mov       ecx,0C00h
  00497090  8b7c243c              mov       edi,[esp+3Ch]
  00497094  f7f9                  idiv      ecx
  00497096  03d7                  add       edx,edi
  00497098  66035632              add       dx,[esi+32h]
  0049709c  81c200020000          add       edx,200h
  004970a2  66895610              mov       [esi+10h],dx
  004970a6  5f                    pop       edi
  004970a7  5e                    pop       esi
  004970a8  5d                    pop       ebp
  004970a9  5b                    pop       ebx
  004970aa  83c428                add       esp,28h
  004970ad  c3                    ret

LAB_004970ae:
  004970ae  83fa2d                cmp       edx,2Dh
  004970b1  7d70                  jge       short 00497123h
  004970b3  8b44243c              mov       eax,[esp+3Ch]
  004970b7  0500020000            add       eax,200h
  004970bc  25ff0f0000            and       eax,0FFFh
  004970c1  3d00040000            cmp       eax,400h
  004970c6  7f4b                  jg        short 00497113h
  004970c8  f6c201                test      dl,1
  004970cb  7420                  je        short 004970EDh
  004970cd  8b0e                  mov       ecx,[esi]
  004970cf  c7461804000000        mov       dword [esi+18h],4
  004970d6  81e1fffffffe          and       ecx,0FEFFFFFFh
  004970dc  c6464201              mov       byte [esi+42h],1
  004970e0  83c902                or        ecx,2
  004970e3  890e                  mov       [esi],ecx
  004970e5  5f                    pop       edi
  004970e6  5e                    pop       esi
  004970e7  5d                    pop       ebp
  004970e8  5b                    pop       ebx
  004970e9  83c428                add       esp,28h
  004970ec  c3                    ret

LAB_004970ed:
  004970ed  8b16                  mov       edx,[esi]
  004970ef  c7461803000000        mov       dword [esi+18h],3
  004970f6  81e2fffffffe          and       edx,0FEFFFFFFh
  004970fc  c6464204              mov       byte [esi+42h],4
  00497100  83ca02                or        edx,2
  00497103  66c746100100          mov       word [esi+10h],1
  00497109  8916                  mov       [esi],edx
  0049710b  5f                    pop       edi
  0049710c  5e                    pop       esi
  0049710d  5d                    pop       ebp
  0049710e  5b                    pop       ebx
  0049710f  83c428                add       esp,28h
  00497112  c3                    ret

LAB_00497113:
  00497113  c6464201              mov       byte [esi+42h],1
  00497117  c6464301              mov       byte [esi+43h],1
  0049711b  5f                    pop       edi
  0049711c  5e                    pop       esi
  0049711d  5d                    pop       ebp
  0049711e  5b                    pop       ebx
  0049711f  83c428                add       esp,28h
  00497122  c3                    ret

LAB_00497123:
  00497123  83fa46                cmp       edx,46h
  00497126  7d40                  jge       short 00497168h
  00497128  c6464203              mov       byte [esi+42h],3
  0049712c  e80f000b00            call      00547140h
  00497131  99                    cdq
  00497132  b90a000000            mov       ecx,0Ah
  00497137  f7f9                  idiv      ecx
  00497139  80c214                add       dl,14h
  0049713c  885643                mov       [esi+43h],dl
  0049713f  e8fcff0a00            call      00547140h
  00497144  99                    cdq
  00497145  b9000c0000            mov       ecx,0C00h
  0049714a  8b7c243c              mov       edi,[esp+3Ch]
  0049714e  f7f9                  idiv      ecx
  00497150  03d7                  add       edx,edi
  00497152  66035632              add       dx,[esi+32h]
  00497156  81ea00060000          sub       edx,600h
  0049715c  66895610              mov       [esi+10h],dx
  00497160  5f                    pop       edi
  00497161  5e                    pop       esi
  00497162  5d                    pop       ebp
  00497163  5b                    pop       ebx
  00497164  83c428                add       esp,28h
  00497167  c3                    ret

LAB_00497168:
  00497168  83fa5a                cmp       edx,5Ah
  0049716b  7d23                  jge       short 00497190h
  0049716d  c6464202              mov       byte [esi+42h],2
  00497171  e8caff0a00            call      00547140h
  00497176  99                    cdq
  00497177  b90a000000            mov       ecx,0Ah
  0049717c  66897e10              mov       [esi+10h],di
  00497180  f7f9                  idiv      ecx
  00497182  80c214                add       dl,14h
  00497185  885643                mov       [esi+43h],dl
  00497188  5f                    pop       edi
  00497189  5e                    pop       esi
  0049718a  5d                    pop       ebp
  0049718b  5b                    pop       ebx
  0049718c  83c428                add       esp,28h
  0049718f  c3                    ret

LAB_00497190:
  00497190  c6464200              mov       byte [esi+42h],0
  00497194  e8a7ff0a00            call      00547140h
  00497199  99                    cdq
  0049719a  b90a000000            mov       ecx,0Ah
  0049719f  f7f9                  idiv      ecx
  004971a1  80c214                add       dl,14h
  004971a4  885643                mov       [esi+43h],dl

LAB_004971a7:
  004971a7  5f                    pop       edi
  004971a8  5e                    pop       esi
  004971a9  5d                    pop       ebp
  004971aa  5b                    pop       ebx
  004971ab  83c428                add       esp,28h
  004971ae  c3                    ret

LAB_004971af:
  004971af  8a4643                mov       al,[esi+43h]
  004971b2  84c0                  test      al,al
  004971b4  7405                  je        short 004971BBh
  004971b6  fec8                  dec       al
  004971b8  884643                mov       [esi+43h],al

LAB_004971bb:
  004971bb  894e5c                mov       [esi+5Ch],ecx
  004971be  e9c6fcffff            jmp       00496E89h
  004971c3  bf01000000            mov       edi,1
  004971c8  66397e18              cmp       [esi+18h],di
  004971cc  740a                  je        short 004971D8h
  004971ce  0d00000001            or        eax,1000000h
  004971d3  897e18                mov       [esi+18h],edi
  004971d6  8906                  mov       [esi],eax

LAB_004971d8:
  004971d8  8b44243c              mov       eax,[esp+3Ch]
  004971dc  8d9080000000          lea       edx,[eax+80h]
  004971e2  81e2ff0f0000          and       edx,0FFFh
  004971e8  81fa00010000          cmp       edx,100h
  004971ee  7f14                  jg        short 00497204h
  004971f0  66014632              add       [esi+32h],ax
  004971f4  c6464300              mov       byte [esi+43h],0
  004971f8  c7465c00000000        mov       dword [esi+5Ch],0
  004971ff  e98afcffff            jmp       00496E8Eh

LAB_00497204:
  00497204  25ff0f0000            and       eax,0FFFh
  00497209  33c9                  xor       ecx,ecx
  0049720b  3d00080000            cmp       eax,800h
  00497210  c7465c00000000        mov       dword [esi+5Ch],0
  00497217  0f9dc1                setge     cl
  0049721a  49                    dec       ecx
  0049721b  81e100010000          and       ecx,100h
  00497221  83c180                add       ecx,0FFFFFF80h
  00497224  8bc1                  mov       eax,ecx
  00497226  66014632              add       [esi+32h],ax
  0049722a  e95ffcffff            jmp       00496E8Eh
  0049722f  bf01000000            mov       edi,1
  00497234  66397e18              cmp       [esi+18h],di
  00497238  740a                  je        short 00497244h
  0049723a  0d00000001            or        eax,1000000h
  0049723f  897e18                mov       [esi+18h],edi
  00497242  8906                  mov       [esi],eax

LAB_00497244:
  00497244  0fbf5610              movsx     edx,word [esi+10h]
  00497248  39542420              cmp       [esp+20h],edx
  0049724c  7d09                  jge       short 00497257h
  0049724e  c6464300              mov       byte [esi+43h],0
  00497252  e937fcffff            jmp       00496E8Eh

LAB_00497257:
  00497257  8a4643                mov       al,[esi+43h]
  0049725a  84c0                  test      al,al
  0049725c  7405                  je        short 00497263h
  0049725e  fec8                  dec       al
  00497260  884643                mov       [esi+43h],al

LAB_00497263:
  00497263  8b44243c              mov       eax,[esp+3Ch]
  00497267  8d8880000000          lea       ecx,[eax+80h]
  0049726d  81e1ff0f0000          and       ecx,0FFFh
  00497273  81f900010000          cmp       ecx,100h
  00497279  7e1b                  jle       short 00497296h
  0049727b  25ff0f0000            and       eax,0FFFh
  00497280  33d2                  xor       edx,edx
  00497282  3d00080000            cmp       eax,800h
  00497287  0f9dc2                setge     dl
  0049728a  4a                    dec       edx
  0049728b  81e200010000          and       edx,100h
  00497291  83c280                add       edx,0FFFFFF80h
  00497294  8bc2                  mov       eax,edx

LAB_00497296:
  00497296  66014632              add       [esi+32h],ax
  0049729a  c7465c00200100        mov       dword [esi+5Ch],12000h
  004972a1  e9e8fbffff            jmp       00496E8Eh
  004972a6  8a4e43                mov       cl,[esi+43h]
  004972a9  84c9                  test      cl,cl
  004972ab  7405                  je        short 004972B2h
  004972ad  fec9                  dec       cl
  004972af  884e43                mov       [esi+43h],cl

LAB_004972b2:
  004972b2  bf01000000            mov       edi,1
  004972b7  66397e18              cmp       [esi+18h],di
  004972bb  740a                  je        short 004972C7h
  004972bd  0d00000001            or        eax,1000000h
  004972c2  897e18                mov       [esi+18h],edi
  004972c5  8906                  mov       [esi],eax

LAB_004972c7:
  004972c7  0fbf4610              movsx     eax,word [esi+10h]
  004972cb  50                    push      eax
  004972cc  c7465c00e00000        mov       dword [esi+5Ch],0E000h
  004972d3  e844e70a00            call      00545A1Ch
  004972d8  0faf465c              imul      eax,[esi+5Ch]
  004972dc  0fbf4e10              movsx     ecx,word [esi+10h]
  004972e0  8b5e1c                mov       ebx,[esi+1Ch]
  004972e3  51                    push      ecx
  004972e4  c1f80c                sar       eax,0Ch
  004972e7  03d8                  add       ebx,eax
  004972e9  895e1c                mov       [esi+1Ch],ebx
  004972ec  e825e70a00            call      00545A16h
  004972f1  0faf465c              imul      eax,[esi+5Ch]
  004972f5  8b6e24                mov       ebp,[esi+24h]
  004972f8  c7465c00000000        mov       dword [esi+5Ch],0
  004972ff  c1f80c                sar       eax,0Ch
  00497302  03e8                  add       ebp,eax
  00497304  8b44243c              mov       eax,[esp+3Ch]
  00497308  896e24                mov       [esi+24h],ebp
  0049730b  8d9080000000          lea       edx,[eax+80h]
  00497311  81e2ff0f0000          and       edx,0FFFh
  00497317  81fa00010000          cmp       edx,100h
  0049731d  7f09                  jg        short 00497328h
  0049731f  66014632              add       [esi+32h],ax
  00497323  e966fbffff            jmp       00496E8Eh

LAB_00497328:
  00497328  25ff0f0000            and       eax,0FFFh
  0049732d  33c9                  xor       ecx,ecx
  0049732f  3d00080000            cmp       eax,800h
  00497334  0f9dc1                setge     cl
  00497337  49                    dec       ecx
  00497338  81e100010000          and       ecx,100h
  0049733e  83c180                add       ecx,0FFFFFF80h
  00497341  8bc1                  mov       eax,ecx
  00497343  66014632              add       [esi+32h],ax
  00497347  e942fbffff            jmp       00496E8Eh
  0049734c  7566                  jne       short 004973B4h
  0049734e  49                    dec       ecx
  0049734f  009b684900af          add       [ebx-50FFB698h],bl
  00497355  6a49                  push      49h
  00497357  00926b49009e          add       [edx-61FFB695h],dl
  0049735d  6d                    insd
  0049735e  49                    dec       ecx
  0049735f  00656e                add       [ebp+6Eh],ah
  00497362  49                    dec       ecx
  00497363  00c3                  add       bl,al
  00497365  7149                  jno       short 004973B0h
  00497367  002f                  add       [edi],ch
  00497369  7249                  jb        short 004973B4h
  0049736b  00a6724900            (bad)
  (octet invalide — la plage demandee deborde probablement du code)
