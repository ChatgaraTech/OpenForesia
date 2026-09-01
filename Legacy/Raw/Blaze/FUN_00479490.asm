Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00479490  section .text  offset fichier 0x78890  1766 octets

  00479490  83ec20                sub       esp,20h
  00479493  53                    push      ebx
  00479494  55                    push      ebp
  00479495  56                    push      esi
  00479496  8b742430              mov       esi,[esp+30h]
  0047949a  33ed                  xor       ebp,ebp
  0047949c  57                    push      edi
  0047949d  8b06                  mov       eax,[esi]
  0047949f  66896c2424            mov       [esp+24h],bp
  004794a4  a900000002            test      eax,2000000h
  004794a9  66c7442426c4ff        mov       word [esp+26h],0FFC4h
  004794b0  66c74424283c00        mov       word [esp+28h],3Ch
  004794b7  66c744242a6400        mov       word [esp+2Ah],64h
  004794be  66896c242c            mov       [esp+2Ch],bp
  004794c3  66c744242e7800        mov       word [esp+2Eh],78h
  004794ca  742d                  je        short 004794F9h
  004794cc  668b4628              mov       ax,[esi+28h]
  004794d0  668bc8                mov       cx,ax
  004794d3  66c1f902              sar       cx,2
  004794d7  2bc1                  sub       eax,ecx
  004794d9  663d0800              cmp       ax,8
  004794dd  66894628              mov       [esi+28h],ax
  004794e1  6689462a              mov       [esi+2Ah],ax
  004794e5  6689462c              mov       [esi+2Ch],ax
  004794e9  0f8d80060000          jge       near 00479B6Fh
  004794ef  892e                  mov       [esi],ebp
  004794f1  5f                    pop       edi
  004794f2  5e                    pop       esi
  004794f3  5d                    pop       ebp
  004794f4  5b                    pop       ebx
  004794f5  83c420                add       esp,20h
  004794f8  c3                    ret

LAB_004794f9:
  004794f9  a802                  test      al,2
  004794fb  0f84d0020000          je        near 004797D1h
  00479501  8b566c                mov       edx,[esi+6Ch]
  00479504  8b5e78                mov       ebx,[esi+78h]
  00479507  8b0a                  mov       ecx,[edx]
  00479509  81e100000002          and       ecx,2000000h
  0047950f  0bc8                  or        ecx,eax
  00479511  3bdd                  cmp       ebx,ebp
  00479513  890e                  mov       [esi],ecx
  00479515  741d                  je        short 00479534h
  00479517  f70300000050          test      dword [ebx],50000000h
  0047951d  7507                  jne       short 00479526h
  0047951f  66837e1028            cmp       word [esi+10h],28h
  00479524  7c20                  jl        short 00479546h

LAB_00479526:
  00479526  896b74                mov       [ebx+74h],ebp
  00479529  896e78                mov       [esi+78h],ebp
  0047952c  66896e10              mov       [esi+10h],bp
  00479530  33db                  xor       ebx,ebx
  00479532  eb12                  jmp       short 00479546h

LAB_00479534:
  00479534  8b5674                mov       edx,[esi+74h]
  00479537  6a01                  push      1
  00479539  6aff                  push      0FFFFFFFFh
  0047953b  52                    push      edx
  0047953c  e86fb9ffff            call      00474EB0h
  00479541  83c40c                add       esp,0Ch
  00479544  8bd8                  mov       ebx,eax

LAB_00479546:
  00479546  668146128000          add       word [esi+12h],80h
  0047954c  668b4612              mov       ax,[esi+12h]
  00479550  3bdd                  cmp       ebx,ebp
  00479552  7434                  je        short 00479588h
  00479554  895e78                mov       [esi+78h],ebx
  00479557  8b7b1c                mov       edi,[ebx+1Ch]
  0047955a  897374                mov       [ebx+74h],esi
  0047955d  8b461c                mov       eax,[esi+1Ch]
  00479560  2bf8                  sub       edi,eax
  00479562  53                    push      ebx
  00479563  c1ff0c                sar       edi,0Ch
  00479566  e855f60b00            call      00538BC0h
  0047956b  8b4b20                mov       ecx,[ebx+20h]
  0047956e  8be8                  mov       ebp,eax
  00479570  8b4620                mov       eax,[esi+20h]
  00479573  83e5fe                and       ebp,0FFFFFFFEh
  00479576  8b5b24                mov       ebx,[ebx+24h]
  00479579  83c404                add       esp,4
  0047957c  c1e50b                shl       ebp,0Bh
  0047957f  03e9                  add       ebp,ecx
  00479581  2be8                  sub       ebp,eax
  00479583  c1fd0c                sar       ebp,0Ch
  00479586  eb5d                  jmp       short 004795E5h

LAB_00479588:
  00479588  8b5e74                mov       ebx,[esi+74h]
  0047958b  0fbff8                movsx     edi,ax
  0047958e  53                    push      ebx
  0047958f  895c2418              mov       [esp+18h],ebx
  00479593  897c2414              mov       [esp+14h],edi
  00479597  e824f60b00            call      00538BC0h
  0047959c  83c404                add       esp,4
  0047959f  8be8                  mov       ebp,eax
  004795a1  896c2434              mov       [esp+34h],ebp
  004795a5  57                    push      edi
  004795a6  e871c40c00            call      00545A1Ch
  004795ab  8bf8                  mov       edi,eax
  004795ad  8b431c                mov       eax,[ebx+1Ch]
  004795b0  0faffd                imul      edi,ebp
  004795b3  8b561c                mov       edx,[esi+1Ch]
  004795b6  8b4e20                mov       ecx,[esi+20h]
  004795b9  03f8                  add       edi,eax
  004795bb  8b442410              mov       eax,[esp+10h]
  004795bf  2bfa                  sub       edi,edx
  004795c1  8b5320                mov       edx,[ebx+20h]
  004795c4  c1e50c                shl       ebp,0Ch
  004795c7  03ea                  add       ebp,edx
  004795c9  50                    push      eax
  004795ca  2be9                  sub       ebp,ecx
  004795cc  c1ff0c                sar       edi,0Ch
  004795cf  c1fd0c                sar       ebp,0Ch
  004795d2  e83fc40c00            call      00545A16h
  004795d7  8bd8                  mov       ebx,eax
  004795d9  8b4c2414              mov       ecx,[esp+14h]
  004795dd  0faf5c2434            imul      ebx,[esp+34h]
  004795e2  035924                add       ebx,[ecx+24h]

LAB_004795e5:
  004795e5  8b4e24                mov       ecx,[esi+24h]
  004795e8  8bd7                  mov       edx,edi
  004795ea  2bd9                  sub       ebx,ecx
  004795ec  0fafd7                imul      edx,edi
  004795ef  c1fb0c                sar       ebx,0Ch
  004795f2  8bc3                  mov       eax,ebx
  004795f4  0fafc3                imul      eax,ebx
  004795f7  03d0                  add       edx,eax
  004795f9  52                    push      edx
  004795fa  e811c40c00            call      00545A10h
  004795ff  53                    push      ebx
  00479600  57                    push      edi
  00479601  8944243c              mov       [esp+3Ch],eax
  00479605  e818c40c00            call      00545A22h
  0047960a  8b4c2434              mov       ecx,[esp+34h]
  0047960e  8bf8                  mov       edi,eax
  00479610  51                    push      ecx
  00479611  55                    push      ebp
  00479612  e80bc40c00            call      00545A22h
  00479617  6a08                  push      8
  00479619  50                    push      eax
  0047961a  8d542420              lea       edx,[esp+20h]
  0047961e  57                    push      edi
  0047961f  52                    push      edx
  00479620  e8ab940b00            call      00532AD0h
  00479625  8b542430              mov       edx,[esp+30h]
  00479629  8b5e58                mov       ebx,[esi+58h]
  0047962c  8b442428              mov       eax,[esp+28h]
  00479630  8b4e50                mov       ecx,[esi+50h]
  00479633  8b6e54                mov       ebp,[esi+54h]
  00479636  03da                  add       ebx,edx
  00479638  895e58                mov       [esi+58h],ebx
  0047963b  8bd3                  mov       edx,ebx
  0047963d  668b5e32              mov       bx,[esi+32h]
  00479641  03c8                  add       ecx,eax
  00479643  2bfb                  sub       edi,ebx
  00479645  894e50                mov       [esi+50h],ecx
  00479648  8bc1                  mov       eax,ecx
  0047964a  8b4c242c              mov       ecx,[esp+2Ch]
  0047964e  81c700040000          add       edi,400h
  00479654  83c410                add       esp,10h
  00479657  81e7ff0f0000          and       edi,0FFFh
  0047965d  03e9                  add       ebp,ecx
  0047965f  81ff00080000          cmp       edi,800h
  00479665  896e54                mov       [esi+54h],ebp
  00479668  8bcd                  mov       ecx,ebp
  0047966a  7d07                  jge       short 00479673h
  0047966c  c1ff02                sar       edi,2
  0047966f  8bef                  mov       ebp,edi
  00479671  eb0c                  jmp       short 0047967Fh

LAB_00479673:
  00479673  bd00100000            mov       ebp,1000h
  00479678  2bef                  sub       ebp,edi
  0047967a  c1fd02                sar       ebp,2
  0047967d  f7dd                  neg       ebp

LAB_0047967f:
  0047967f  8bf8                  mov       edi,eax
  00479681  03dd                  add       ebx,ebp
  00479683  c1ff03                sar       edi,3
  00479686  2bc7                  sub       eax,edi
  00479688  8bf9                  mov       edi,ecx
  0047968a  c1ff03                sar       edi,3
  0047968d  2bcf                  sub       ecx,edi
  0047968f  8bfa                  mov       edi,edx
  00479691  8b6e20                mov       ebp,[esi+20h]
  00479694  66895e32              mov       [esi+32h],bx
  00479698  8b5e24                mov       ebx,[esi+24h]
  0047969b  894650                mov       [esi+50h],eax
  0047969e  c1ff03                sar       edi,3
  004796a1  2bd7                  sub       edx,edi
  004796a3  8b7e1c                mov       edi,[esi+1Ch]
  004796a6  03f8                  add       edi,eax
  004796a8  8d442424              lea       eax,[esp+24h]
  004796ac  03e9                  add       ebp,ecx
  004796ae  03da                  add       ebx,edx
  004796b0  50                    push      eax
  004796b1  56                    push      esi
  004796b2  894e54                mov       [esi+54h],ecx
  004796b5  895658                mov       [esi+58h],edx
  004796b8  897e1c                mov       [esi+1Ch],edi
  004796bb  896e20                mov       [esi+20h],ebp
  004796be  895e24                mov       [esi+24h],ebx
  004796c1  e8dab5ffff            call      00474CA0h
  004796c6  8bf8                  mov       edi,eax
  004796c8  83c408                add       esp,8
  004796cb  85ff                  test      edi,edi
  004796cd  742a                  je        short 004796F9h
  004796cf  8b4674                mov       eax,[esi+74h]
  004796d2  33c9                  xor       ecx,ecx
  004796d4  8a4e06                mov       cl,[esi+6]
  004796d7  33d2                  xor       edx,edx
  004796d9  8a5607                mov       dl,[esi+7]
  004796dc  6a00                  push      0
  004796de  51                    push      ecx
  004796df  52                    push      edx
  004796e0  57                    push      edi
  004796e1  50                    push      eax
  004796e2  e8c90b0c00            call      0053A2B0h
  004796e7  8b07                  mov       eax,[edi]
  004796e9  83c414                add       esp,14h
  004796ec  0c40                  or        al,40h
  004796ee  66c787960000001000    mov       word [edi+96h],10h
  004796f7  8907                  mov       [edi],eax

LAB_004796f9:
  004796f9  f6461003              test      byte [esi+10h],3
  004796fd  0f8539040000          jne       near 00479B3Ch
  00479703  0fbf7e32              movsx     edi,word [esi+32h]
  00479707  8b4e2c                mov       ecx,[esi+2Ch]
  0047970a  8b5638                mov       edx,[esi+38h]
  0047970d  8b5e28                mov       ebx,[esi+28h]
  00479710  81c700040000          add       edi,400h
  00479716  57                    push      edi
  00479717  894c2414              mov       [esp+14h],ecx
  0047971b  89542438              mov       [esp+38h],edx
  0047971f  e8f8c20c00            call      00545A1Ch
  00479724  8d0440                lea       eax,[eax+eax*2]
  00479727  8d2c80                lea       ebp,[eax+eax*4]
  0047972a  e811da0c00            call      00547140h
  0047972f  8b561c                mov       edx,[esi+1Ch]
  00479732  83e03f                and       eax,3Fh
  00479735  83e820                sub       eax,20h
  00479738  c1e00c                shl       eax,0Ch
  0047973b  8d04e8                lea       eax,[eax+ebp*8]
  0047973e  03c2                  add       eax,edx
  00479740  89442418              mov       [esp+18h],eax
  00479744  e8f7d90c00            call      00547140h
  00479749  8b4e20                mov       ecx,[esi+20h]
  0047974c  83e03f                and       eax,3Fh
  0047974f  83e820                sub       eax,20h
  00479752  57                    push      edi
  00479753  c1e00c                shl       eax,0Ch
  00479756  03c1                  add       eax,ecx
  00479758  89442420              mov       [esp+20h],eax
  0047975c  e8b5c20c00            call      00545A16h
  00479761  8d0440                lea       eax,[eax+eax*2]
  00479764  8d3c80                lea       edi,[eax+eax*4]
  00479767  e8d4d90c00            call      00547140h
  0047976c  8b5624                mov       edx,[esi+24h]
  0047976f  83e03f                and       eax,3Fh
  00479772  83e820                sub       eax,20h
  00479775  6aff                  push      0FFFFFFFFh
  00479777  c1e00c                shl       eax,0Ch
  0047977a  c7462800180018        mov       dword [esi+28h],18001800h
  00479781  66c7462c1f00          mov       word [esi+2Ch],1Fh
  00479787  8d0cf8                lea       ecx,[eax+edi*8]
  0047978a  8d44241c              lea       eax,[esp+1Ch]
  0047978e  03ca                  add       ecx,edx
  00479790  33d2                  xor       edx,edx
  00479792  8a560b                mov       dl,[esi+0Bh]
  00479795  894c2424              mov       [esp+24h],ecx
  00479799  52                    push      edx
  0047979a  68bf000000            push      0BFh
  0047979f  68d81a5500            push      551AD8h
  004797a4  50                    push      eax
  004797a5  56                    push      esi
  004797a6  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  004797ad  66c7461a0400          mov       word [esi+1Ah],4
  004797b3  e888140000            call      0047AC40h
  004797b8  8b4c2428              mov       ecx,[esp+28h]
  004797bc  8b54244c              mov       edx,[esp+4Ch]
  004797c0  83c418                add       esp,18h
  004797c3  895e28                mov       [esi+28h],ebx
  004797c6  894e2c                mov       [esi+2Ch],ecx
  004797c9  895638                mov       [esi+38h],edx
  004797cc  e96b030000            jmp       00479B3Ch

LAB_004797d1:
  004797d1  668b4e10              mov       cx,[esi+10h]
  004797d5  663bcd                cmp       cx,bp
  004797d8  751d                  jne       short 004797F7h
  004797da  8a4606                mov       al,[esi+6]
  004797dd  896e78                mov       [esi+78h],ebp
  004797e0  3c13                  cmp       al,13h
  004797e2  7509                  jne       short 004797EDh
  004797e4  c7463800000040        mov       dword [esi+38h],40000000h
  004797eb  eb03                  jmp       short 004797F0h

LAB_004797ed:
  004797ed  896e38                mov       [esi+38h],ebp

LAB_004797f0:
  004797f0  896e28                mov       [esi+28h],ebp
  004797f3  66896e2c              mov       [esi+2Ch],bp

LAB_004797f7:
  004797f7  66817e28f007          cmp       word [esi+28h],7F0h
  004797fd  7c31                  jl        short 00479830h
  004797ff  8b466c                mov       eax,[esi+6Ch]
  00479802  8b10                  mov       edx,[eax]
  00479804  83ca02                or        edx,2
  00479807  8910                  mov       [eax],edx
  00479809  8b4674                mov       eax,[esi+74h]
  0047980c  8b08                  mov       ecx,[eax]
  0047980e  80e17f                and       cl,7Fh
  00479811  8908                  mov       [eax],ecx
  00479813  8b06                  mov       eax,[esi]
  00479815  0c02                  or        al,2
  00479817  896e50                mov       [esi+50h],ebp
  0047981a  8906                  mov       [esi],eax
  0047981c  896e54                mov       [esi+54h],ebp
  0047981f  896e58                mov       [esi+58h],ebp
  00479822  66c7869a0000000010    mov       word [esi+9Ah],1000h
  0047982b  e9f1020000            jmp       00479B21h

LAB_00479830:
  00479830  6683f914              cmp       cx,14h
  00479834  0f8de7020000          jge       near 00479B21h
  0047983a  8b461c                mov       eax,[esi+1Ch]
  0047983d  8d7e1c                lea       edi,[esi+1Ch]
  00479840  89442418              mov       [esp+18h],eax
  00479844  8b4624                mov       eax,[esi+24h]
  00479847  8b5620                mov       edx,[esi+20h]
  0047984a  89442420              mov       [esp+20h],eax
  0047984e  33c0                  xor       eax,eax
  00479850  8954241c              mov       [esp+1Ch],edx
  00479854  8a4606                mov       al,[esi+6]
  00479857  83c0f0                add       eax,0FFFFFFF0h
  0047985a  83f803                cmp       eax,3
  0047985d  0f87aa020000          ja        near 00479B0Dh
  00479863  ff2485789b4700        jmp       dword [eax*4+479B78h]
  0047986a  f6c101                test      cl,1
  0047986d  0f849a020000          je        near 00479B0Dh
  00479873  8b5e28                mov       ebx,[esi+28h]
  00479876  8b6e38                mov       ebp,[esi+38h]
  00479879  c7462800200020        mov       dword [esi+28h],20002000h
  00479880  c7463800000001        mov       dword [esi+38h],1000000h
  00479887  e8b4d80c00            call      00547140h
  0047988c  8b0f                  mov       ecx,[edi]
  0047988e  25ff010000            and       eax,1FFh
  00479893  2d00010000            sub       eax,100h
  00479898  c1e00c                shl       eax,0Ch
  0047989b  03c8                  add       ecx,eax
  0047989d  890f                  mov       [edi],ecx
  0047989f  e89cd80c00            call      00547140h
  004798a4  8b4e20                mov       ecx,[esi+20h]
  004798a7  25ff010000            and       eax,1FFh
  004798ac  2d00010000            sub       eax,100h
  004798b1  c1e00c                shl       eax,0Ch
  004798b4  03c8                  add       ecx,eax
  004798b6  894e20                mov       [esi+20h],ecx
  004798b9  e882d80c00            call      00547140h
  004798be  8b4e24                mov       ecx,[esi+24h]
  004798c1  25ff010000            and       eax,1FFh
  004798c6  2d00010000            sub       eax,100h
  004798cb  6a02                  push      2
  004798cd  c1e00c                shl       eax,0Ch
  004798d0  03c8                  add       ecx,eax
  004798d2  6a00                  push      0
  004798d4  6a57                  push      57h
  004798d6  894e24                mov       [esi+24h],ecx
  004798d9  66c7461a0400          mov       word [esi+1Ah],4
  004798df  68d81a5500            push      551AD8h
  004798e4  eb66                  jmp       short 0047994Ch
  004798e6  f6c103                test      cl,3
  004798e9  0f851e020000          jne       near 00479B0Dh
  004798ef  8b5e28                mov       ebx,[esi+28h]
  004798f2  8b6e38                mov       ebp,[esi+38h]
  004798f5  c7462800200020        mov       dword [esi+28h],20002000h
  004798fc  c7463800000001        mov       dword [esi+38h],1000000h
  00479903  e838d80c00            call      00547140h
  00479908  8b0f                  mov       ecx,[edi]
  0047990a  25ff000000            and       eax,0FFh
  0047990f  2d80000000            sub       eax,80h
  00479914  c1e00c                shl       eax,0Ch
  00479917  03c8                  add       ecx,eax
  00479919  890f                  mov       [edi],ecx
  0047991b  8b4e60                mov       ecx,[esi+60h]
  0047991e  894e20                mov       [esi+20h],ecx
  00479921  e81ad80c00            call      00547140h
  00479926  8b4e24                mov       ecx,[esi+24h]
  00479929  25ff000000            and       eax,0FFh
  0047992e  2d80000000            sub       eax,80h
  00479933  6a03                  push      3
  00479935  c1e00c                shl       eax,0Ch
  00479938  03c8                  add       ecx,eax
  0047993a  6a00                  push      0
  0047993c  6a57                  push      57h
  0047993e  894e24                mov       [esi+24h],ecx
  00479941  66c7461a0700          mov       word [esi+1Ah],7
  00479947  68d0195500            push      5519D0h

LAB_0047994c:
  0047994c  57                    push      edi
  0047994d  56                    push      esi
  0047994e  e8ed120000            call      0047AC40h
  00479953  83c418                add       esp,18h
  00479956  895e28                mov       [esi+28h],ebx
  00479959  896e38                mov       [esi+38h],ebp
  0047995c  e9ac010000            jmp       00479B0Dh
  00479961  f6c103                test      cl,3
  00479964  0f85a3010000          jne       near 00479B0Dh
  0047996a  0fbf4e12              movsx     ecx,word [esi+12h]
  0047996e  8b5628                mov       edx,[esi+28h]
  00479971  8b4638                mov       eax,[esi+38h]
  00479974  0fbf5e32              movsx     ebx,word [esi+32h]
  00479978  0fbf6e34              movsx     ebp,word [esi+34h]
  0047997c  89542434              mov       [esp+34h],edx
  00479980  89442414              mov       [esp+14h],eax
  00479984  894c2410              mov       [esp+10h],ecx
  00479988  e8b3d70c00            call      00547140h
  0047998d  83e07f                and       eax,7Fh
  00479990  c7462800200020        mov       dword [esi+28h],20002000h
  00479997  05c8000000            add       eax,0C8h
  0047999c  c7463800000000        mov       dword [esi+38h],0
  004799a3  66894612              mov       [esi+12h],ax
  004799a7  66c7461a0700          mov       word [esi+1Ah],7
  004799ad  e88ed70c00            call      00547140h
  004799b2  66894632              mov       [esi+32h],ax
  004799b6  e885d70c00            call      00547140h
  004799bb  6a04                  push      4
  004799bd  6a00                  push      0
  004799bf  25ff010000            and       eax,1FFh
  004799c4  6a50                  push      50h
  004799c6  68081a5500            push      551A08h
  004799cb  2d00010000            sub       eax,100h
  004799d0  57                    push      edi
  004799d1  56                    push      esi
  004799d2  66894634              mov       [esi+34h],ax
  004799d6  e865120000            call      0047AC40h
  004799db  8b54244c              mov       edx,[esp+4Ch]
  004799df  8b44242c              mov       eax,[esp+2Ch]
  004799e3  668b4c2428            mov       cx,[esp+28h]
  004799e8  83c418                add       esp,18h
  004799eb  66895e32              mov       [esi+32h],bx
  004799ef  66896e34              mov       [esi+34h],bp
  004799f3  895628                mov       [esi+28h],edx
  004799f6  894638                mov       [esi+38h],eax
  004799f9  66894e12              mov       [esi+12h],cx
  004799fd  e90b010000            jmp       00479B0Dh
  00479a02  f6c103                test      cl,3
  00479a05  0f8502010000          jne       near 00479B0Dh
  00479a0b  8b5628                mov       edx,[esi+28h]
  00479a0e  8b462c                mov       eax,[esi+2Ch]
  00479a11  8b4e38                mov       ecx,[esi+38h]
  00479a14  6681463200fc          add       word [esi+32h],0FC00h
  00479a1a  33db                  xor       ebx,ebx
  00479a1c  89542410              mov       [esp+10h],edx
  00479a20  8a5e04                mov       bl,[esi+4]
  00479a23  89442434              mov       [esp+34h],eax
  00479a27  894c2414              mov       [esp+14h],ecx
  00479a2b  c7462800100040        mov       dword [esi+28h],40001000h
  00479a32  66c7462c0010          mov       word [esi+2Ch],1000h
  00479a38  c7463800000001        mov       dword [esi+38h],1000000h
  00479a3f  e8fcd60c00            call      00547140h
  00479a44  8b17                  mov       edx,[edi]
  00479a46  83e07f                and       eax,7Fh
  00479a49  83e840                sub       eax,40h
  00479a4c  c1e00c                shl       eax,0Ch
  00479a4f  03d0                  add       edx,eax
  00479a51  8917                  mov       [edi],edx
  00479a53  8b5660                mov       edx,[esi+60h]
  00479a56  895620                mov       [esi+20h],edx
  00479a59  e8e2d60c00            call      00547140h
  00479a5e  8b5624                mov       edx,[esi+24h]
  00479a61  83e07f                and       eax,7Fh
  00479a64  83e840                sub       eax,40h
  00479a67  6a01                  push      1
  00479a69  c1e00c                shl       eax,0Ch
  00479a6c  55                    push      ebp
  00479a6d  03d0                  add       edx,eax
  00479a6f  688b000000            push      8Bh
  00479a74  56                    push      esi
  00479a75  895624                mov       [esi+24h],edx
  00479a78  c646041e              mov       byte [esi+4],1Eh
  00479a7c  e85f100000            call      0047AAE0h
  00479a81  83c410                add       esp,10h
  00479a84  66c7869a0000000060    mov       word [esi+9Ah],6000h
  00479a8d  e8aed60c00            call      00547140h
  00479a92  8be8                  mov       ebp,eax
  00479a94  55                    push      ebp
  00479a95  e882bf0c00            call      00545A1Ch
  00479a9a  c1e004                shl       eax,4
  00479a9d  894650                mov       [esi+50h],eax
  00479aa0  e89bd60c00            call      00547140h
  00479aa5  83e01f                and       eax,1Fh
  00479aa8  55                    push      ebp
  00479aa9  83c01e                add       eax,1Eh
  00479aac  c1e00c                shl       eax,0Ch
  00479aaf  894654                mov       [esi+54h],eax
  00479ab2  e85fbf0c00            call      00545A16h
  00479ab7  6a01                  push      1
  00479ab9  6a00                  push      0
  00479abb  68bf000000            push      0BFh
  00479ac0  68b81b5500            push      551BB8h
  00479ac5  c1e004                shl       eax,4
  00479ac8  57                    push      edi
  00479ac9  56                    push      esi
  00479aca  894658                mov       [esi+58h],eax
  00479acd  c74638f0f0f001        mov       dword [esi+38h],1F0F0F0h
  00479ad4  c7462800188001        mov       dword [esi+28h],1801800h
  00479adb  66c7462c1f00          mov       word [esi+2Ch],1Fh
  00479ae1  66c7461a0100          mov       word [esi+1Ah],1
  00479ae7  e854110000            call      0047AC40h
  00479aec  8b442428              mov       eax,[esp+28h]
  00479af0  8b4c244c              mov       ecx,[esp+4Ch]
  00479af4  8b54242c              mov       edx,[esp+2Ch]
  00479af8  83c418                add       esp,18h
  00479afb  668146320004          add       word [esi+32h],400h
  00479b01  894628                mov       [esi+28h],eax
  00479b04  894e2c                mov       [esi+2Ch],ecx
  00479b07  895638                mov       [esi+38h],edx
  00479b0a  885e04                mov       [esi+4],bl

LAB_00479b0d:
  00479b0d  8b442418              mov       eax,[esp+18h]
  00479b11  8b4c241c              mov       ecx,[esp+1Ch]
  00479b15  8b542420              mov       edx,[esp+20h]
  00479b19  8907                  mov       [edi],eax
  00479b1b  894e20                mov       [esi+20h],ecx
  00479b1e  895624                mov       [esi+24h],edx

LAB_00479b21:
  00479b21  8b4638                mov       eax,[esi+38h]
  00479b24  8bc8                  mov       ecx,eax
  00479b26  81e1ffffff00          and       ecx,0FFFFFFh
  00479b2c  81f9f0f0f000          cmp       ecx,0F0F0F0h
  00479b32  7308                  jae       short 00479B3Ch
  00479b34  0508080800            add       eax,80808h
  00479b39  894638                mov       [esi+38h],eax

LAB_00479b3c:
  00479b3c  0fbf5610              movsx     edx,word [esi+10h]
  00479b40  668b7e28              mov       di,[esi+28h]
  00479b44  c1e208                shl       edx,8
  00479b47  52                    push      edx
  00479b48  e8cfbe0c00            call      00545A1Ch
  00479b4d  0fbfcf                movsx     ecx,di
  00479b50  c1f802                sar       eax,2
  00479b53  2bc1                  sub       eax,ecx
  00479b55  0500080000            add       eax,800h
  00479b5a  c1f803                sar       eax,3
  00479b5d  03c7                  add       eax,edi
  00479b5f  66ff4610              inc       word [esi+10h]
  00479b63  66894628              mov       [esi+28h],ax
  00479b67  6689462a              mov       [esi+2Ah],ax
  00479b6b  6689462c              mov       [esi+2Ch],ax

LAB_00479b6f:
  00479b6f  5f                    pop       edi
  00479b70  5e                    pop       esi
  00479b71  5d                    pop       ebp
  00479b72  5b                    pop       ebx
  00479b73  83c420                add       esp,20h

19 cible(s) de saut interne : 0x004794f9, 0x00479526, 0x00479534, 0x00479546, 0x00479588, 0x004795e5, 0x00479673, 0x0047967f, 0x004796f9, 0x004797d1, 0x004797ed, 0x004797f0, 0x004797f7, 0x00479830, 0x0047994c, 0x00479b0d, 0x00479b21, 0x00479b3c, 0x00479b6f
