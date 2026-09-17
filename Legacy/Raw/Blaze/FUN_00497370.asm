Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00497370  section .text  offset fichier 0x96770  2944 octets

  00497370  81ec88000000          sub       esp,88h
  00497376  53                    push      ebx
  00497377  55                    push      ebp
  00497378  56                    push      esi
  00497379  8bb42498000000        mov       esi,[esp+98h]
  00497380  b864000000            mov       eax,64h
  00497385  33c9                  xor       ecx,ecx
  00497387  6689442432            mov       [esp+32h],ax
  0049738c  6689442436            mov       [esp+36h],ax
  00497391  8a4641                mov       al,[esi+41h]
  00497394  57                    push      edi
  00497395  84c0                  test      al,al
  00497397  66894c2430            mov       [esp+30h],cx
  0049739c  66c74424321e00        mov       word [esp+32h],1Eh
  004973a3  66c74424343200        mov       word [esp+34h],32h
  004973aa  66894c2438            mov       [esp+38h],cx
  004973af  750f                  jne       short 004973C0h
  004973b1  51                    push      ecx
  004973b2  51                    push      ecx
  004973b3  56                    push      esi
  004973b4  c6464101              mov       byte [esi+41h],1
  004973b8  e8d39effff            call      00491290h
  004973bd  83c40c                add       esp,0Ch

LAB_004973c0:
  004973c0  8b461c                mov       eax,[esi+1Ch]
  004973c3  8b4e20                mov       ecx,[esi+20h]
  004973c6  8b5624                mov       edx,[esi+24h]
  004973c9  8d5e1c                lea       ebx,[esi+1Ch]
  004973cc  89442424              mov       [esp+24h],eax
  004973d0  8b4664                mov       eax,[esi+64h]
  004973d3  894c2428              mov       [esp+28h],ecx
  004973d7  8b4e60                mov       ecx,[esi+60h]
  004973da  8954242c              mov       [esp+2Ch],edx
  004973de  668b5632              mov       dx,[esi+32h]
  004973e2  89442448              mov       [esp+48h],eax
  004973e6  8b467c                mov       eax,[esi+7Ch]
  004973e9  894c2440              mov       [esp+40h],ecx
  004973ed  668b8e90000000        mov       cx,[esi+90h]
  004973f4  6689942486000000      mov       [esp+86h],dx
  004973fc  8b9680000000          mov       edx,[esi+80h]
  00497402  8944244c              mov       [esp+4Ch],eax
  00497406  8b8684000000          mov       eax,[esi+84h]
  0049740c  66894c2444            mov       [esp+44h],cx
  00497411  8b8e88000000          mov       ecx,[esi+88h]
  00497417  8b7e6c                mov       edi,[esi+6Ch]
  0049741a  89542450              mov       [esp+50h],edx
  0049741e  8b968c000000          mov       edx,[esi+8Ch]
  00497424  8944243c              mov       [esp+3Ch],eax
  00497428  894c245c              mov       [esp+5Ch],ecx
  0049742c  8d442464              lea       eax,[esp+64h]
  00497430  89542454              mov       [esp+54h],edx
  00497434  8d4c246c              lea       ecx,[esp+6Ch]
  00497438  50                    push      eax
  00497439  8d54246c              lea       edx,[esp+6Ch]
  0049743d  51                    push      ecx
  0049743e  52                    push      edx
  0049743f  56                    push      esi
  00497440  e83b150a00            call      00538980h
  00497445  83c410                add       esp,10h
  00497448  8d442458              lea       eax,[esp+58h]
  0049744c  8d4c241c              lea       ecx,[esp+1Ch]
  00497450  8d542418              lea       edx,[esp+18h]
  00497454  50                    push      eax
  00497455  51                    push      ecx
  00497456  8d44241c              lea       eax,[esp+1Ch]
  0049745a  52                    push      edx
  0049745b  8d4c241c              lea       ecx,[esp+1Ch]
  0049745f  50                    push      eax
  00497460  51                    push      ecx
  00497461  56                    push      esi
  00497462  e8b9ea0900            call      00535F20h
  00497467  8b542428              mov       edx,[esp+28h]
  0049746b  b840000000            mov       eax,40h
  00497470  83c418                add       esp,18h
  00497473  2bc2                  sub       eax,edx
  00497475  8b542458              mov       edx,[esp+58h]
  00497479  89442420              mov       [esp+20h],eax
  0049747d  52                    push      edx
  0049747e  56                    push      esi
  0049747f  e85ce90300            call      004D5DE0h
  00497484  8b03                  mov       eax,[ebx]
  00497486  8b571c                mov       edx,[edi+1Ch]
  00497489  8b4e20                mov       ecx,[esi+20h]
  0049748c  8b6f20                mov       ebp,[edi+20h]
  0049748f  2bc2                  sub       eax,edx
  00497491  8b5624                mov       edx,[esi+24h]
  00497494  c1f80c                sar       eax,0Ch
  00497497  2bcd                  sub       ecx,ebp
  00497499  8b6f24                mov       ebp,[edi+24h]
  0049749c  8bf8                  mov       edi,eax
  0049749e  2bd5                  sub       edx,ebp
  004974a0  c1f90c                sar       ecx,0Ch
  004974a3  0faff8                imul      edi,eax
  004974a6  8bc1                  mov       eax,ecx
  004974a8  83c408                add       esp,8
  004974ab  c1fa0c                sar       edx,0Ch
  004974ae  0fafc1                imul      eax,ecx
  004974b1  8bca                  mov       ecx,edx
  004974b3  03f8                  add       edi,eax
  004974b5  0fafca                imul      ecx,edx
  004974b8  03f9                  add       edi,ecx
  004974ba  57                    push      edi
  004974bb  e850e50a00            call      00545A10h
  004974c0  8be8                  mov       ebp,eax
  004974c2  896c2414              mov       [esp+14h],ebp
  004974c6  8b4c241c              mov       ecx,[esp+1Ch]
  004974ca  85c9                  test      ecx,ecx
  004974cc  0f8454040000          je        near 00497926h
  004974d2  8b3e                  mov       edi,[esi]
  004974d4  33d2                  xor       edx,edx
  004974d6  8a5604                mov       dl,[esi+4]
  004974d9  81e7fffffffd          and       edi,0FDFFFFFFh
  004974df  0fbf4618              movsx     eax,word [esi+18h]
  004974e3  893e                  mov       [esi],edi
  004974e5  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004974ec  f7c700000001          test      edi,1000000h
  004974f2  8b0482                mov       eax,[edx+eax*4]
  004974f5  741d                  je        short 00497514h
  004974f7  66ff461a              inc       word [esi+1Ah]
  004974fb  668b561a              mov       dx,[esi+1Ah]
  004974ff  33ff                  xor       edi,edi
  00497501  668b7816              mov       di,[eax+16h]
  00497505  0fbfc2                movsx     eax,dx
  00497508  3bc7                  cmp       eax,edi
  0049750a  7c2b                  jl        short 00497537h
  0049750c  66c7461a0000          mov       word [esi+1Ah],0
  00497512  eb23                  jmp       short 00497537h

LAB_00497514:
  00497514  66ff461a              inc       word [esi+1Ah]
  00497518  33d2                  xor       edx,edx
  0049751a  668b5016              mov       dx,[eax+16h]
  0049751e  0fbf461a              movsx     eax,word [esi+1Ah]
  00497522  3bc2                  cmp       eax,edx
  00497524  7c11                  jl        short 00497537h
  00497526  668b461a              mov       ax,[esi+1Ah]
  0049752a  48                    dec       eax
  0049752b  81cf00000002          or        edi,2000000h
  00497531  6689461a              mov       [esi+1Ah],ax
  00497535  893e                  mov       [esi],edi

LAB_00497537:
  00497537  f60602                test      byte [esi],2
  0049753a  0f8482000000          je        near 004975C2h
  00497540  66837e1a0a            cmp       word [esi+1Ah],0Ah
  00497545  757b                  jne       short 004975C2h
  00497547  8d4c2430              lea       ecx,[esp+30h]
  0049754b  6a00                  push      0
  0049754d  51                    push      ecx
  0049754e  53                    push      ebx
  0049754f  56                    push      esi
  00497550  e8db260800            call      00519C30h
  00497555  8bf8                  mov       edi,eax
  00497557  a1b8e05d00            mov       eax,[5DE0B8h]
  0049755c  83c410                add       esp,10h
  0049755f  85c0                  test      eax,eax
  00497561  7457                  je        short 004975BAh
  00497563  66837e1a0a            cmp       word [esi+1Ah],0Ah
  00497568  7550                  jne       short 004975BAh
  0049756a  8b0f                  mov       ecx,[edi]
  0049756c  57                    push      edi
  0049756d  83c940                or        ecx,40h
  00497570  56                    push      esi
  00497571  66c787960000001000    mov       word [edi+96h],10h
  0049757a  890f                  mov       [edi],ecx
  0049757c  e8ff250a00            call      00539B80h
  00497581  83c408                add       esp,8
  00497584  8d571c                lea       edx,[edi+1Ch]
  00497587  52                    push      edx
  00497588  6800180000            push      1800h
  0049758d  6800030000            push      300h
  00497592  6a03                  push      3
  00497594  e887650a00            call      0053DB20h
  00497599  83c410                add       esp,10h
  0049759c  e89ffb0a00            call      00547140h
  004975a1  99                    cdq
  004975a2  b90f000000            mov       ecx,0Fh
  004975a7  f7f9                  idiv      ecx
  004975a9  83fa01                cmp       edx,1
  004975ac  750c                  jne       short 004975BAh
  004975ae  6a00                  push      0
  004975b0  57                    push      edi
  004975b1  56                    push      esi
  004975b2  e849b8ffff            call      00492E00h
  004975b7  83c40c                add       esp,0Ch

LAB_004975ba:
  004975ba  8b4c241c              mov       ecx,[esp+1Ch]
  004975be  8b6c2414              mov       ebp,[esp+14h]

LAB_004975c2:
  004975c2  33c0                  xor       eax,eax
  004975c4  8a4642                mov       al,[esi+42h]
  004975c7  83f80b                cmp       eax,0Bh
  004975ca  0f8756030000          ja        near 00497926h
  004975d0  ff2485bc7e4900        jmp       dword [eax*4+497EBCh]
  004975d7  33c9                  xor       ecx,ecx
  004975d9  66394e12              cmp       [esi+12h],cx
  004975dd  751a                  jne       short 004975F9h
  004975df  8b06                  mov       eax,[esi]
  004975e1  66894e18              mov       [esi+18h],cx
  004975e5  0d00000001            or        eax,1000000h
  004975ea  66894e1a              mov       [esi+1Ah],cx
  004975ee  8906                  mov       [esi],eax
  004975f0  66c746120100          mov       word [esi+12h],1
  004975f6  894e5c                mov       [esi+5Ch],ecx

LAB_004975f9:
  004975f9  0fbf5610              movsx     edx,word [esi+10h]
  004975fd  3bea                  cmp       ebp,edx
  004975ff  7d09                  jge       short 0049760Ah
  00497601  c6464300              mov       byte [esi+43h],0
  00497605  e91c030000            jmp       00497926h

LAB_0049760a:
  0049760a  8a4643                mov       al,[esi+43h]
  0049760d  84c0                  test      al,al
  0049760f  0f84ed020000          je        near 00497902h
  00497615  fec8                  dec       al
  00497617  894e5c                mov       [esi+5Ch],ecx
  0049761a  884643                mov       [esi+43h],al
  0049761d  e904030000            jmp       00497926h
  00497622  33c9                  xor       ecx,ecx
  00497624  66394e12              cmp       [esi+12h],cx
  00497628  751b                  jne       short 00497645h
  0049762a  8b16                  mov       edx,[esi]
  0049762c  b801000000            mov       eax,1
  00497631  81ca00000001          or        edx,1000000h
  00497637  66894618              mov       [esi+18h],ax
  0049763b  66894e1a              mov       [esi+1Ah],cx
  0049763f  8916                  mov       [esi],edx
  00497641  66894612              mov       [esi+12h],ax

LAB_00497645:
  00497645  8b442418              mov       eax,[esp+18h]
  00497649  8d90c8000000          lea       edx,[eax+0C8h]
  0049764f  81e2ff0f0000          and       edx,0FFFh
  00497655  81fa90010000          cmp       edx,190h
  0049765b  7f0c                  jg        short 00497669h
  0049765d  c6464300              mov       byte [esi+43h],0
  00497661  894e5c                mov       [esi+5Ch],ecx
  00497664  e9bd020000            jmp       00497926h

LAB_00497669:
  00497669  25ff0f0000            and       eax,0FFFh
  0049766e  3d00080000            cmp       eax,800h
  00497673  7d13                  jge       short 00497688h
  00497675  8b442410              mov       eax,[esp+10h]
  00497679  894e5c                mov       [esi+5Ch],ecx
  0049767c  83c028                add       eax,28h
  0049767f  66014632              add       [esi+32h],ax
  00497683  e99e020000            jmp       00497926h

LAB_00497688:
  00497688  8b542410              mov       edx,[esp+10h]
  0049768c  b8d8ffffff            mov       eax,0FFFFFFD8h
  00497691  2bc2                  sub       eax,edx
  00497693  894e5c                mov       [esi+5Ch],ecx
  00497696  66014632              add       [esi+32h],ax
  0049769a  e987020000            jmp       00497926h
  0049769f  8b542410              mov       edx,[esp+10h]
  004976a3  33ff                  xor       edi,edi
  004976a5  66397e12              cmp       [esi+12h],di
  004976a9  b801000000            mov       eax,1
  004976ae  7525                  jne       short 004976D5h
  004976b0  8b0e                  mov       ecx,[esi]
  004976b2  66894618              mov       [esi+18h],ax
  004976b6  81c900000001          or        ecx,1000000h
  004976bc  66897e1a              mov       [esi+1Ah],di
  004976c0  890e                  mov       [esi],ecx
  004976c2  8d8a00010000          lea       ecx,[edx+100h]
  004976c8  66894612              mov       [esi+12h],ax
  004976cc  8d0cc9                lea       ecx,[ecx+ecx*8]
  004976cf  c1e104                shl       ecx,4
  004976d2  894e5c                mov       [esi+5Ch],ecx

LAB_004976d5:
  004976d5  66394612              cmp       [esi+12h],ax
  004976d9  0f8547020000          jne       near 00497926h
  004976df  0fbf4610              movsx     eax,word [esi+10h]
  004976e3  3be8                  cmp       ebp,eax
  004976e5  7d0c                  jge       short 004976F3h
  004976e7  c6464300              mov       byte [esi+43h],0
  004976eb  897e5c                mov       [esi+5Ch],edi
  004976ee  e933020000            jmp       00497926h

LAB_004976f3:
  004976f3  8a4643                mov       al,[esi+43h]
  004976f6  84c0                  test      al,al
  004976f8  7405                  je        short 004976FFh
  004976fa  fec8                  dec       al
  004976fc  884643                mov       [esi+43h],al

LAB_004976ff:
  004976ff  8b442418              mov       eax,[esp+18h]
  00497703  8d8800010000          lea       ecx,[eax+100h]
  00497709  81e1ff0f0000          and       ecx,0FFFh
  0049770f  81f900020000          cmp       ecx,200h
  00497715  0f8e0b020000          jle       near 00497926h
  0049771b  25ff0f0000            and       eax,0FFFh
  00497720  3d00080000            cmp       eax,800h
  00497725  7d0d                  jge       short 00497734h
  00497727  8d441228              lea       eax,[edx+edx+28h]
  0049772b  66014632              add       [esi+32h],ax
  0049772f  e9f2010000            jmp       00497926h

LAB_00497734:
  00497734  03d2                  add       edx,edx
  00497736  b8d8ffffff            mov       eax,0FFFFFFD8h
  0049773b  2bc2                  sub       eax,edx
  0049773d  66014632              add       [esi+32h],ax
  00497741  e9e0010000            jmp       00497926h
  00497746  66837e1201            cmp       word [esi+12h],1
  0049774b  0f859b000000          jne       near 004977ECh
  00497751  8a4643                mov       al,[esi+43h]
  00497754  84c0                  test      al,al
  00497756  7405                  je        short 0049775Dh
  00497758  fec8                  dec       al
  0049775a  884643                mov       [esi+43h],al

LAB_0049775d:
  0049775d  8b442410              mov       eax,[esp+10h]
  00497761  8d04c0                lea       eax,[eax+eax*8]
  00497764  c1e004                shl       eax,4
  00497767  0500700000            add       eax,7000h
  0049776c  0fafc8                imul      ecx,eax
  0049776f  c1f90c                sar       ecx,0Ch
  00497772  8bf9                  mov       edi,ecx
  00497774  89465c                mov       [esi+5Ch],eax
  00497777  0fbf4e10              movsx     ecx,word [esi+10h]
  0049777b  51                    push      ecx
  0049777c  e89be20a00            call      00545A1Ch
  00497781  0fafc7                imul      eax,edi
  00497784  0fbf5610              movsx     edx,word [esi+10h]
  00497788  8b0b                  mov       ecx,[ebx]
  0049778a  52                    push      edx
  0049778b  c1f80c                sar       eax,0Ch
  0049778e  03c8                  add       ecx,eax
  00497790  890b                  mov       [ebx],ecx
  00497792  e87fe20a00            call      00545A16h
  00497797  0fafc7                imul      eax,edi
  0049779a  8b6e24                mov       ebp,[esi+24h]
  0049779d  c7465c00000000        mov       dword [esi+5Ch],0
  004977a4  c1f80c                sar       eax,0Ch
  004977a7  03e8                  add       ebp,eax
  004977a9  8b442418              mov       eax,[esp+18h]
  004977ad  896e24                mov       [esi+24h],ebp
  004977b0  8d8800010000          lea       ecx,[eax+100h]
  004977b6  81e1ff0f0000          and       ecx,0FFFh
  004977bc  81f900020000          cmp       ecx,200h
  004977c2  7e28                  jle       short 004977ECh
  004977c4  25ff0f0000            and       eax,0FFFh
  004977c9  3d00080000            cmp       eax,800h
  004977ce  7d0a                  jge       short 004977DAh
  004977d0  8b542410              mov       edx,[esp+10h]
  004977d4  8d441228              lea       eax,[edx+edx+28h]
  004977d8  eb0e                  jmp       short 004977E8h

LAB_004977da:
  004977da  8b442410              mov       eax,[esp+10h]
  004977de  8d0c00                lea       ecx,[eax+eax]
  004977e1  b8d8ffffff            mov       eax,0FFFFFFD8h
  004977e6  2bc1                  sub       eax,ecx

LAB_004977e8:
  004977e8  66014632              add       [esi+32h],ax

LAB_004977ec:
  004977ec  66837e1200            cmp       word [esi+12h],0
  004977f1  0f852f010000          jne       near 00497926h
  004977f7  8b0e                  mov       ecx,[esi]
  004977f9  b801000000            mov       eax,1
  004977fe  81c900000001          or        ecx,1000000h
  00497804  66894618              mov       [esi+18h],ax
  00497808  66c7461a0000          mov       word [esi+1Ah],0
  0049780e  890e                  mov       [esi],ecx
  00497810  66894612              mov       [esi+12h],ax
  00497814  e90d010000            jmp       00497926h
  00497819  66837e1200            cmp       word [esi+12h],0
  0049781e  0f8580000000          jne       near 004978A4h
  00497824  8b06                  mov       eax,[esi]
  00497826  a801                  test      al,1
  00497828  7406                  je        short 00497830h
  0049782a  c6464300              mov       byte [esi+43h],0
  0049782e  eb74                  jmp       short 004978A4h

LAB_00497830:
  00497830  8b542410              mov       edx,[esp+10h]
  00497834  25fffffffe            and       eax,0FEFFFFFFh
  00497839  8906                  mov       [esi],eax
  0049783b  bd01000000            mov       ebp,1
  00497840  8d8200010000          lea       eax,[edx+100h]
  00497846  66896e18              mov       [esi+18h],bp
  0049784a  c1e006                shl       eax,6
  0049784d  0fafc8                imul      ecx,eax
  00497850  89465c                mov       [esi+5Ch],eax
  00497853  66c7461a0000          mov       word [esi+1Ah],0
  00497859  0fbf4610              movsx     eax,word [esi+10h]
  0049785d  c1f90c                sar       ecx,0Ch
  00497860  50                    push      eax
  00497861  8bf9                  mov       edi,ecx
  00497863  e8b4e10a00            call      00545A1Ch
  00497868  0fafc7                imul      eax,edi
  0049786b  8b4e50                mov       ecx,[esi+50h]
  0049786e  c1f80c                sar       eax,0Ch
  00497871  03c8                  add       ecx,eax
  00497873  894e50                mov       [esi+50h],ecx
  00497876  0fbf4e10              movsx     ecx,word [esi+10h]
  0049787a  51                    push      ecx
  0049787b  e896e10a00            call      00545A16h
  00497880  0fafc7                imul      eax,edi
  00497883  8b5658                mov       edx,[esi+58h]
  00497886  8b4e54                mov       ecx,[esi+54h]
  00497889  c1f80c                sar       eax,0Ch
  0049788c  03d0                  add       edx,eax
  0049788e  8b06                  mov       eax,[esi]
  00497890  81c100e00100          add       ecx,1E000h
  00497896  0bc5                  or        eax,ebp
  00497898  895658                mov       [esi+58h],edx
  0049789b  894e54                mov       [esi+54h],ecx
  0049789e  8906                  mov       [esi],eax
  004978a0  66896e12              mov       [esi+12h],bp

LAB_004978a4:
  004978a4  f60601                test      byte [esi],1
  004978a7  757d                  jne       short 00497926h
  004978a9  c6464300              mov       byte [esi+43h],0
  004978ad  eb77                  jmp       short 00497926h
  004978af  668b4e12              mov       cx,[esi+12h]
  004978b3  ba01000000            mov       edx,1
  004978b8  663bca                cmp       cx,dx
  004978bb  751a                  jne       short 004978D7h
  004978bd  0fbf4610              movsx     eax,word [esi+10h]
  004978c1  3be8                  cmp       ebp,eax
  004978c3  7d06                  jge       short 004978CBh
  004978c5  c6464300              mov       byte [esi+43h],0
  004978c9  eb0c                  jmp       short 004978D7h

LAB_004978cb:
  004978cb  8a4643                mov       al,[esi+43h]
  004978ce  84c0                  test      al,al
  004978d0  7405                  je        short 004978D7h
  004978d2  fec8                  dec       al
  004978d4  884643                mov       [esi+43h],al

LAB_004978d7:
  004978d7  6685c9                test      cx,cx
  004978da  754a                  jne       short 00497926h
  004978dc  8b442410              mov       eax,[esp+10h]
  004978e0  8b3e                  mov       edi,[esi]
  004978e2  66894e1a              mov       [esi+1Ah],cx
  004978e6  81cf00000001          or        edi,1000000h
  004978ec  8d0cc0                lea       ecx,[eax+eax*8]
  004978ef  66895618              mov       [esi+18h],dx
  004978f3  c1e104                shl       ecx,4
  004978f6  893e                  mov       [esi],edi
  004978f8  66895612              mov       [esi+12h],dx
  004978fc  81c100700000          add       ecx,7000h

LAB_00497902:
  00497902  894e5c                mov       [esi+5Ch],ecx
  00497905  eb1f                  jmp       short 00497926h
  00497907  66837e1200            cmp       word [esi+12h],0
  0049790c  7518                  jne       short 00497926h
  0049790e  8b06                  mov       eax,[esi]
  00497910  c7465c00000000        mov       dword [esi+5Ch],0
  00497917  a900000002            test      eax,2000000h
  0049791c  7408                  je        short 00497926h
  0049791e  24fd                  and       al,0FDh
  00497920  c6464300              mov       byte [esi+43h],0
  00497924  8906                  mov       [esi],eax

LAB_00497926:
  00497926  0fbf5632              movsx     edx,word [esi+32h]
  0049792a  52                    push      edx
  0049792b  e8ece00a00            call      00545A1Ch
  00497930  89442470              mov       [esp+70h],eax
  00497934  0fbf4632              movsx     eax,word [esi+32h]
  00497938  50                    push      eax
  00497939  e8d8e00a00            call      00545A16h
  0049793e  8b5650                mov       edx,[esi+50h]
  00497941  89442478              mov       [esp+78h],eax
  00497945  8bc2                  mov       eax,edx
  00497947  8bfa                  mov       edi,edx
  00497949  c1f803                sar       eax,3
  0049794c  2bf8                  sub       edi,eax
  0049794e  8b4c241c              mov       ecx,[esp+1Ch]
  00497952  0fbf869a000000        movsx     eax,word [esi+9Ah]
  00497959  0faf4e5c              imul      ecx,[esi+5Ch]
  0049795d  c1fa08                sar       edx,8
  00497960  2bfa                  sub       edi,edx
  00497962  8b5654                mov       edx,[esi+54h]
  00497965  8be8                  mov       ebp,eax
  00497967  897e50                mov       [esi+50h],edi
  0049796a  c1fa08                sar       edx,8
  0049796d  c1fd08                sar       ebp,8
  00497970  2bea                  sub       ebp,edx
  00497972  8b5658                mov       edx,[esi+58h]
  00497975  2be8                  sub       ebp,eax
  00497977  8b4654                mov       eax,[esi+54h]
  0049797a  03c5                  add       eax,ebp
  0049797c  8bea                  mov       ebp,edx
  0049797e  89442460              mov       [esp+60h],eax
  00497982  894654                mov       [esi+54h],eax
  00497985  8bc2                  mov       eax,edx
  00497987  56                    push      esi
  00497988  c1f808                sar       eax,8
  0049798b  2be8                  sub       ebp,eax
  0049798d  8b442464              mov       eax,[esp+64h]
  00497991  c1fa03                sar       edx,3
  00497994  c1f90c                sar       ecx,0Ch
  00497997  2bea                  sub       ebp,edx
  00497999  8bd1                  mov       edx,ecx
  0049799b  0faf542474            imul      edx,[esp+74h]
  004979a0  0faf4c247c            imul      ecx,[esp+7Ch]
  004979a5  c1fa0c                sar       edx,0Ch
  004979a8  03d7                  add       edx,edi
  004979aa  8b3b                  mov       edi,[ebx]
  004979ac  03fa                  add       edi,edx
  004979ae  8b5620                mov       edx,[esi+20h]
  004979b1  c1f90c                sar       ecx,0Ch
  004979b4  03d0                  add       edx,eax
  004979b6  8b4624                mov       eax,[esi+24h]
  004979b9  03cd                  add       ecx,ebp
  004979bb  896e58                mov       [esi+58h],ebp
  004979be  03c1                  add       eax,ecx
  004979c0  893b                  mov       [ebx],edi
  004979c2  895620                mov       [esi+20h],edx
  004979c5  894624                mov       [esi+24h],eax
  004979c8  e873bf0900            call      00533940h
  004979cd  8b9690000000          mov       edx,[esi+90h]
  004979d3  8b4660                mov       eax,[esi+60h]
  004979d6  8b4e20                mov       ecx,[esi+20h]
  004979d9  83c404                add       esp,4
  004979dc  80e6fe                and       dh,0FEh
  004979df  3bc8                  cmp       ecx,eax
  004979e1  899690000000          mov       [esi+90h],edx
  004979e7  7f2a                  jg        short 00497A13h
  004979e9  f6c680                test      dh,80h
  004979ec  7525                  jne       short 00497A13h
  004979ee  8b0e                  mov       ecx,[esi]
  004979f0  894620                mov       [esi+20h],eax
  004979f3  f6c101                test      cl,1
  004979f6  740f                  je        short 00497A07h
  004979f8  83e1fe                and       ecx,0FFFFFFFEh
  004979fb  33ff                  xor       edi,edi
  004979fd  890e                  mov       [esi],ecx
  004979ff  897e54                mov       [esi+54h],edi
  00497a02  e924010000            jmp       00497B2Bh

LAB_00497a07:
  00497a07  83e1fe                and       ecx,0FFFFFFFEh
  00497a0a  33ff                  xor       edi,edi
  00497a0c  890e                  mov       [esi],ecx
  00497a0e  897e54                mov       [esi+54h],edi
  00497a11  eb02                  jmp       short 00497A15h

LAB_00497a13:
  00497a13  33ff                  xor       edi,edi

LAB_00497a15:
  00497a15  8b0e                  mov       ecx,[esi]
  00497a17  f6c101                test      cl,1
  00497a1a  750a                  jne       short 00497A26h
  00497a1c  394620                cmp       [esi+20h],eax
  00497a1f  7e05                  jle       short 00497A26h
  00497a21  83c901                or        ecx,1
  00497a24  890e                  mov       [esi],ecx

LAB_00497a26:
  00497a26  f7c282ad0020          test      edx,2000AD82h
  00497a2c  0f84f9000000          je        near 00497B2Bh
  00497a32  f60601                test      byte [esi],1
  00497a35  0f85c8000000          jne       near 00497B03h
  00497a3b  8a4e42                mov       cl,[esi+42h]
  00497a3e  b007                  mov       al,7
  00497a40  3ac8                  cmp       cl,al
  00497a42  7420                  je        short 00497A64h
  00497a44  884642                mov       [esi+42h],al
  00497a47  e8f4f60a00            call      00547140h
  00497a4c  99                    cdq
  00497a4d  b928000000            mov       ecx,28h
  00497a52  66897e12              mov       [esi+12h],di
  00497a56  f7f9                  idiv      ecx
  00497a58  66c746105000          mov       word [esi+10h],50h
  00497a5e  80c214                add       dl,14h
  00497a61  885643                mov       [esi+43h],dl

LAB_00497a64:
  00497a64  8b16                  mov       edx,[esi]
  00497a66  8b442428              mov       eax,[esp+28h]
  00497a6a  8b4c242c              mov       ecx,[esp+2Ch]
  00497a6e  83e2fd                and       edx,0FFFFFFFDh
  00497a71  8916                  mov       [esi],edx
  00497a73  8b542424              mov       edx,[esp+24h]
  00497a77  894620                mov       [esi+20h],eax
  00497a7a  8b442444              mov       eax,[esp+44h]
  00497a7e  8913                  mov       [ebx],edx
  00497a80  8b54244c              mov       edx,[esp+4Ch]
  00497a84  25ffff0000            and       eax,0FFFFh
  00497a89  894e24                mov       [esi+24h],ecx
  00497a8c  8b4c243c              mov       ecx,[esp+3Ch]
  00497a90  89567c                mov       [esi+7Ch],edx
  00497a93  8b54245c              mov       edx,[esp+5Ch]
  00497a97  898690000000          mov       [esi+90h],eax
  00497a9d  8b442450              mov       eax,[esp+50h]
  00497aa1  898e84000000          mov       [esi+84h],ecx
  00497aa7  8b4c2448              mov       ecx,[esp+48h]
  00497aab  898680000000          mov       [esi+80h],eax
  00497ab1  8b442454              mov       eax,[esp+54h]
  00497ab5  899688000000          mov       [esi+88h],edx
  00497abb  8b542440              mov       edx,[esp+40h]
  00497abf  6882ad0020            push      2000AD82h
  00497ac4  56                    push      esi
  00497ac5  89868c000000          mov       [esi+8Ch],eax
  00497acb  894e64                mov       [esi+64h],ecx
  00497ace  895660                mov       [esi+60h],edx
  00497ad1  e88ae30300            call      004D5E60h
  00497ad6  83c408                add       esp,8
  00497ad9  663bc7                cmp       ax,di
  00497adc  7515                  jne       short 00497AF3h
  00497ade  e85df60a00            call      00547140h
  00497ae3  99                    cdq
  00497ae4  33c2                  xor       eax,edx
  00497ae6  2bc2                  sub       eax,edx
  00497ae8  83e001                and       eax,1
  00497aeb  33c2                  xor       eax,edx
  00497aed  2bc2                  sub       eax,edx
  00497aef  8d4400ff              lea       eax,[eax+eax-1]

LAB_00497af3:
  00497af3  8b4c2410              mov       ecx,[esp+10h]
  00497af7  83c140                add       ecx,40h
  00497afa  0fafc8                imul      ecx,eax
  00497afd  66014e32              add       [esi+32h],cx
  00497b01  eb28                  jmp       short 00497B2Bh

LAB_00497b03:
  00497b03  8b542424              mov       edx,[esp+24h]
  00497b07  8b44242c              mov       eax,[esp+2Ch]
  00497b0b  897e5c                mov       [esi+5Ch],edi
  00497b0e  897e50                mov       [esi+50h],edi
  00497b11  897e58                mov       [esi+58h],edi
  00497b14  8913                  mov       [ebx],edx
  00497b16  894624                mov       [esi+24h],eax
  00497b19  c6464200              mov       byte [esi+42h],0
  00497b1d  c6464328              mov       byte [esi+43h],28h
  00497b21  66897e12              mov       [esi+12h],di
  00497b25  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_00497b2b:
  00497b2b  6aff                  push      0FFFFFFFFh
  00497b2d  6aff                  push      0FFFFFFFFh
  00497b2f  6aff                  push      0FFFFFFFFh
  00497b31  56                    push      esi
  00497b32  e8d9bc0200            call      004C3810h
  00497b37  8b44242c              mov       eax,[esp+2Ch]
  00497b3b  83c410                add       esp,10h
  00497b3e  3bc7                  cmp       eax,edi
  00497b40  7517                  jne       short 00497B59h
  00497b42  668b8c2486000000      mov       cx,[esp+86h]
  00497b4a  66894e32              mov       [esi+32h],cx
  00497b4e  5f                    pop       edi
  00497b4f  5e                    pop       esi
  00497b50  5d                    pop       ebp
  00497b51  5b                    pop       ebx
  00497b52  81c488000000          add       esp,88h
  00497b58  c3                    ret

LAB_00497b59:
  00497b59  8a4643                mov       al,[esi+43h]
  00497b5c  84c0                  test      al,al
  00497b5e  0f854c030000          jne       near 00497EB0h
  00497b64  8b466c                mov       eax,[esi+6Ch]
  00497b67  8b2b                  mov       ebp,[ebx]
  00497b69  8b4e24                mov       ecx,[esi+24h]
  00497b6c  8b781c                mov       edi,[eax+1Ch]
  00497b6f  2bfd                  sub       edi,ebp
  00497b71  8b6824                mov       ebp,[eax+24h]
  00497b74  2be9                  sub       ebp,ecx
  00497b76  c1ff0c                sar       edi,0Ch
  00497b79  c1fd0c                sar       ebp,0Ch
  00497b7c  8bd7                  mov       edx,edi
  00497b7e  8bc5                  mov       eax,ebp
  00497b80  0fafd7                imul      edx,edi
  00497b83  0fafc5                imul      eax,ebp
  00497b86  03d0                  add       edx,eax
  00497b88  52                    push      edx
  00497b89  e882de0a00            call      00545A10h
  00497b8e  55                    push      ebp
  00497b8f  57                    push      edi
  00497b90  8944241c              mov       [esp+1Ch],eax
  00497b94  e889de0a00            call      00545A22h
  00497b99  668b4e32              mov       cx,[esi+32h]
  00497b9d  66c746120000          mov       word [esi+12h],0
  00497ba3  2bc1                  sub       eax,ecx
  00497ba5  b306                  mov       bl,6
  00497ba7  25ff0f0000            and       eax,0FFFh
  00497bac  89442418              mov       [esp+18h],eax
  00497bb0  8b442414              mov       eax,[esp+14h]
  00497bb4  3dfa000000            cmp       eax,0FAh
  00497bb9  0f8d1c010000          jge       near 00497CDBh
  00497bbf  e87cf50a00            call      00547140h
  00497bc4  99                    cdq
  00497bc5  b964000000            mov       ecx,64h
  00497bca  f7f9                  idiv      ecx
  00497bcc  83fa28                cmp       edx,28h
  00497bcf  7d6e                  jge       short 00497C3Fh
  00497bd1  8b542418              mov       edx,[esp+18h]
  00497bd5  8b7c2420              mov       edi,[esp+20h]
  00497bd9  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  00497be0  8d0cfd00020000        lea       ecx,[edi*8+200h]
  00497be7  25ff0f0000            and       eax,0FFFh
  00497bec  3bc1                  cmp       eax,ecx
  00497bee  7f40                  jg        short 00497C30h
  00497bf0  e84bf50a00            call      00547140h
  00497bf5  99                    cdq
  00497bf6  8d4c3f01              lea       ecx,[edi+edi+1]
  00497bfa  f7f9                  idiv      ecx
  00497bfc  8b442414              mov       eax,[esp+14h]
  00497c00  83c264                add       edx,64h
  00497c03  3bc2                  cmp       eax,edx
  00497c05  7d29                  jge       short 00497C30h
  00497c07  8b16                  mov       edx,[esi]
  00497c09  c646420b              mov       byte [esi+42h],0Bh
  00497c0d  81e2fffffffe          and       edx,0FEFFFFFFh
  00497c13  c6464301              mov       byte [esi+43h],1
  00497c17  83ca02                or        edx,2
  00497c1a  33ed                  xor       ebp,ebp
  00497c1c  8916                  mov       [esi],edx
  00497c1e  896e5c                mov       [esi+5Ch],ebp
  00497c21  66c746180300          mov       word [esi+18h],3
  00497c27  66896e1a              mov       [esi+1Ah],bp
  00497c2b  e9b1000000            jmp       00497CE1h

LAB_00497c30:
  00497c30  c6464201              mov       byte [esi+42h],1
  00497c34  c6464301              mov       byte [esi+43h],1
  00497c38  33ed                  xor       ebp,ebp
  00497c3a  e9a2000000            jmp       00497CE1h

LAB_00497c3f:
  00497c3f  83fa3c                cmp       edx,3Ch
  00497c42  7d40                  jge       short 00497C84h
  00497c44  33ed                  xor       ebp,ebp
  00497c46  c6464203              mov       byte [esi+42h],3
  00497c4a  66896e12              mov       [esi+12h],bp
  00497c4e  e8edf40a00            call      00547140h
  00497c53  99                    cdq
  00497c54  b90a000000            mov       ecx,0Ah
  00497c59  8b7c2420              mov       edi,[esp+20h]
  00497c5d  f7f9                  idiv      ecx
  00497c5f  8bc7                  mov       eax,edi
  00497c61  d1f8                  sar       eax,1
  00497c63  02d0                  add       dl,al
  00497c65  80c214                add       dl,14h
  00497c68  885643                mov       [esi+43h],dl
  00497c6b  e8d0f40a00            call      00547140h
  00497c70  99                    cdq
  00497c71  33c2                  xor       eax,edx
  00497c73  2bc2                  sub       eax,edx
  00497c75  25ff0f0000            and       eax,0FFFh
  00497c7a  33c2                  xor       eax,edx
  00497c7c  2bc2                  sub       eax,edx
  00497c7e  66894610              mov       [esi+10h],ax
  00497c82  eb5d                  jmp       short 00497CE1h

LAB_00497c84:
  00497c84  83fa46                cmp       edx,46h
  00497c87  7d27                  jge       short 00497CB0h
  00497c89  8b4c2410              mov       ecx,[esp+10h]
  00497c8d  8b442414              mov       eax,[esp+14h]
  00497c91  83c164                add       ecx,64h
  00497c94  3bc1                  cmp       eax,ecx
  00497c96  7e18                  jle       short 00497CB0h
  00497c98  885e42                mov       [esi+42h],bl
  00497c9b  c6464301              mov       byte [esi+43h],1
  00497c9f  e89cf40a00            call      00547140h
  00497ca4  33ed                  xor       ebp,ebp
  00497ca6  66894610              mov       [esi+10h],ax
  00497caa  66896e12              mov       [esi+12h],bp
  00497cae  eb2d                  jmp       short 00497CDDh

LAB_00497cb0:
  00497cb0  c6464202              mov       byte [esi+42h],2
  00497cb4  e887f40a00            call      00547140h
  00497cb9  99                    cdq
  00497cba  b90a000000            mov       ecx,0Ah
  00497cbf  8b7c2420              mov       edi,[esp+20h]
  00497cc3  f7f9                  idiv      ecx
  00497cc5  8bc7                  mov       eax,edi
  00497cc7  66c746105000          mov       word [esi+10h],50h
  00497ccd  d1f8                  sar       eax,1
  00497ccf  02d0                  add       dl,al
  00497cd1  80c214                add       dl,14h
  00497cd4  33ed                  xor       ebp,ebp
  00497cd6  885643                mov       [esi+43h],dl
  00497cd9  eb06                  jmp       short 00497CE1h

LAB_00497cdb:
  00497cdb  33ed                  xor       ebp,ebp

LAB_00497cdd:
  00497cdd  8b7c2420              mov       edi,[esp+20h]

LAB_00497ce1:
  00497ce1  8b442414              mov       eax,[esp+14h]
  00497ce5  3d58020000            cmp       eax,258h
  00497cea  0f8dc0000000          jge       near 00497DB0h
  00497cf0  3dfa000000            cmp       eax,0FAh
  00497cf5  0f8cb5000000          jl        near 00497DB0h
  00497cfb  e840f40a00            call      00547140h
  00497d00  99                    cdq
  00497d01  b964000000            mov       ecx,64h
  00497d06  f7f9                  idiv      ecx
  00497d08  83fa14                cmp       edx,14h
  00497d0b  7d26                  jge       short 00497D33h
  00497d0d  8b542418              mov       edx,[esp+18h]
  00497d11  81c200040000          add       edx,400h
  00497d17  81e2ff0f0000          and       edx,0FFFh
  00497d1d  81fa00080000          cmp       edx,800h
  00497d23  0f8c87000000          jl        near 00497DB0h
  00497d29  c6464201              mov       byte [esi+42h],1
  00497d2d  c6464301              mov       byte [esi+43h],1
  00497d31  eb7d                  jmp       short 00497DB0h

LAB_00497d33:
  00497d33  83fa32                cmp       edx,32h
  00497d36  7d25                  jge       short 00497D5Dh
  00497d38  c6464202              mov       byte [esi+42h],2
  00497d3c  e8fff30a00            call      00547140h
  00497d41  99                    cdq
  00497d42  b90a000000            mov       ecx,0Ah
  00497d47  66c746105000          mov       word [esi+10h],50h
  00497d4d  f7f9                  idiv      ecx
  00497d4f  8bc7                  mov       eax,edi
  00497d51  d1f8                  sar       eax,1
  00497d53  02d0                  add       dl,al
  00497d55  80c214                add       dl,14h
  00497d58  885643                mov       [esi+43h],dl
  00497d5b  eb53                  jmp       short 00497DB0h

LAB_00497d5d:
  00497d5d  83fa46                cmp       edx,46h
  00497d60  7d16                  jge       short 00497D78h
  00497d62  885e42                mov       [esi+42h],bl
  00497d65  c6464301              mov       byte [esi+43h],1
  00497d69  e8d2f30a00            call      00547140h
  00497d6e  66894610              mov       [esi+10h],ax
  00497d72  66896e12              mov       [esi+12h],bp
  00497d76  eb38                  jmp       short 00497DB0h

LAB_00497d78:
  00497d78  c6464203              mov       byte [esi+42h],3
  00497d7c  66896e12              mov       [esi+12h],bp
  00497d80  e8bbf30a00            call      00547140h
  00497d85  99                    cdq
  00497d86  b90a000000            mov       ecx,0Ah
  00497d8b  f7f9                  idiv      ecx
  00497d8d  8bc7                  mov       eax,edi
  00497d8f  d1f8                  sar       eax,1
  00497d91  02d0                  add       dl,al
  00497d93  80c214                add       dl,14h
  00497d96  885643                mov       [esi+43h],dl
  00497d99  e8a2f30a00            call      00547140h
  00497d9e  99                    cdq
  00497d9f  33c2                  xor       eax,edx
  00497da1  2bc2                  sub       eax,edx
  00497da3  25ff0f0000            and       eax,0FFFh
  00497da8  33c2                  xor       eax,edx
  00497daa  2bc2                  sub       eax,edx
  00497dac  66894610              mov       [esi+10h],ax

LAB_00497db0:
  00497db0  817c241458020000      cmp       dword [esp+14h],258h
  00497db8  0f8cf2000000          jl        near 00497EB0h
  00497dbe  e87df30a00            call      00547140h
  00497dc3  99                    cdq
  00497dc4  b964000000            mov       ecx,64h
  00497dc9  f7f9                  idiv      ecx
  00497dcb  83fa1e                cmp       edx,1Eh
  00497dce  7d70                  jge       short 00497E40h
  00497dd0  8b442410              mov       eax,[esp+10h]
  00497dd4  8d4c000a              lea       ecx,[eax+eax+0Ah]
  00497dd8  3bd1                  cmp       edx,ecx
  00497dda  7d2e                  jge       short 00497E0Ah
  00497ddc  c6464202              mov       byte [esi+42h],2
  00497de0  e85bf30a00            call      00547140h
  00497de5  99                    cdq
  00497de6  b90a000000            mov       ecx,0Ah
  00497deb  66c746106400          mov       word [esi+10h],64h
  00497df1  f7f9                  idiv      ecx
  00497df3  8bc7                  mov       eax,edi
  00497df5  d1f8                  sar       eax,1
  00497df7  02d0                  add       dl,al
  00497df9  80c214                add       dl,14h
  00497dfc  885643                mov       [esi+43h],dl
  00497dff  5f                    pop       edi
  00497e00  5e                    pop       esi
  00497e01  5d                    pop       ebp
  00497e02  5b                    pop       ebx
  00497e03  81c488000000          add       esp,88h
  00497e09  c3                    ret

LAB_00497e0a:
  00497e0a  c6464200              mov       byte [esi+42h],0
  00497e0e  e82df30a00            call      00547140h
  00497e13  99                    cdq
  00497e14  b90a000000            mov       ecx,0Ah
  00497e19  66896e12              mov       [esi+12h],bp
  00497e1d  f7f9                  idiv      ecx
  00497e1f  8b4c2410              mov       ecx,[esp+10h]
  00497e23  8bc7                  mov       eax,edi
  00497e25  c1f802                sar       eax,2
  00497e28  02d0                  add       dl,al
  00497e2a  885643                mov       [esi+43h],dl
  00497e2d  8d540950              lea       edx,[ecx+ecx+50h]
  00497e31  66895610              mov       [esi+10h],dx
  00497e35  5f                    pop       edi
  00497e36  5e                    pop       esi
  00497e37  5d                    pop       ebp
  00497e38  5b                    pop       ebx
  00497e39  81c488000000          add       esp,88h
  00497e3f  c3                    ret

LAB_00497e40:
  00497e40  83fa28                cmp       edx,28h
  00497e43  7d43                  jge       short 00497E88h
  00497e45  c6464203              mov       byte [esi+42h],3
  00497e49  66896e12              mov       [esi+12h],bp
  00497e4d  e8eef20a00            call      00547140h
  00497e52  99                    cdq
  00497e53  b90a000000            mov       ecx,0Ah
  00497e58  f7f9                  idiv      ecx
  00497e5a  8bc7                  mov       eax,edi
  00497e5c  d1f8                  sar       eax,1
  00497e5e  02d0                  add       dl,al
  00497e60  80c214                add       dl,14h
  00497e63  885643                mov       [esi+43h],dl
  00497e66  e8d5f20a00            call      00547140h
  00497e6b  99                    cdq
  00497e6c  33c2                  xor       eax,edx
  00497e6e  2bc2                  sub       eax,edx
  00497e70  25ff0f0000            and       eax,0FFFh
  00497e75  33c2                  xor       eax,edx
  00497e77  2bc2                  sub       eax,edx
  00497e79  66894610              mov       [esi+10h],ax
  00497e7d  5f                    pop       edi
  00497e7e  5e                    pop       esi
  00497e7f  5d                    pop       ebp
  00497e80  5b                    pop       ebx
  00497e81  81c488000000          add       esp,88h
  00497e87  c3                    ret

LAB_00497e88:
  00497e88  83fa3c                cmp       edx,3Ch
  00497e8b  7d13                  jge       short 00497EA0h
  00497e8d  c6464201              mov       byte [esi+42h],1
  00497e91  c6464301              mov       byte [esi+43h],1
  00497e95  5f                    pop       edi
  00497e96  5e                    pop       esi
  00497e97  5d                    pop       ebp
  00497e98  5b                    pop       ebx
  00497e99  81c488000000          add       esp,88h
  00497e9f  c3                    ret

LAB_00497ea0:
  00497ea0  885e42                mov       [esi+42h],bl
  00497ea3  c6464301              mov       byte [esi+43h],1
  00497ea7  e894f20a00            call      00547140h
  00497eac  66894610              mov       [esi+10h],ax

LAB_00497eb0:
  00497eb0  5f                    pop       edi
  00497eb1  5e                    pop       esi
  00497eb2  5d                    pop       ebp
  00497eb3  5b                    pop       ebx
  00497eb4  81c488000000          add       esp,88h
  00497eba  c3                    ret
  00497ebb  90                    nop
  00497ebc  d7                    xlatb
  00497ebd  7549                  jne       short 00497F08h
  00497ebf  0022                  add       [edx],ah
  00497ec1  7649                  jbe       short 00497F0Ch
  00497ec3  009f76490046          add       [edi+46004976h],bl
  00497ec9  7749                  ja        short 00497F14h
  00497ecb  0026                  add       [esi],ah
  00497ecd  7949                  jns       short 00497F18h
  00497ecf  0026                  add       [esi],ah
  00497ed1  7949                  jns       short 00497F1Ch
  00497ed3  0019                  add       [ecx],bl
  00497ed5  7849                  js        short 00497F20h
  00497ed7  00af78490026          add       [edi+26004978h],ch
  00497edd  7949                  jns       short 00497F28h
  00497edf  0026                  add       [esi],ah
  00497ee1  7949                  jns       short 00497F2Ch
  00497ee3  0026                  add       [esi],ah
  00497ee5  7949                  jns       short 00497F30h
  00497ee7  0007                  add       [edi],al
  00497ee9  7949                  jns       short 00497F34h
  00497eeb  0090909090            (bad)
  (octet invalide — la plage demandee deborde probablement du code)
