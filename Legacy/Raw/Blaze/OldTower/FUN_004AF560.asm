Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004af560  section .text  offset fichier 0xae960  4256 octets

  004af560  81ec94000000          sub       esp,94h
  004af566  53                    push      ebx
  004af567  55                    push      ebp
  004af568  56                    push      esi
  004af569  8bb424a4000000        mov       esi,[esp+0A4h]
  004af570  b864000000            mov       eax,64h
  004af575  33c9                  xor       ecx,ecx
  004af577  6689442436            mov       [esp+36h],ax
  004af57c  668944243a            mov       [esp+3Ah],ax
  004af581  6689442442            mov       [esp+42h],ax
  004af586  6689442446            mov       [esp+46h],ax
  004af58b  8a4641                mov       al,[esi+41h]
  004af58e  57                    push      edi
  004af58f  84c0                  test      al,al
  004af591  66894c2434            mov       [esp+34h],cx
  004af596  66c74424361e00        mov       word [esp+36h],1Eh
  004af59d  66c74424383200        mov       word [esp+38h],32h
  004af5a4  66894c243c            mov       [esp+3Ch],cx
  004af5a9  66894c2440            mov       [esp+40h],cx
  004af5ae  66894c2442            mov       [esp+42h],cx
  004af5b3  66c74424445000        mov       word [esp+44h],50h
  004af5ba  66894c2448            mov       [esp+48h],cx
  004af5bf  7510                  jne       short 004AF5D1h
  004af5c1  51                    push      ecx
  004af5c2  6a03                  push      3
  004af5c4  56                    push      esi
  004af5c5  c6464101              mov       byte [esi+41h],1
  004af5c9  e892d2ffff            call      004AC860h
  004af5ce  83c40c                add       esp,0Ch

LAB_004af5d1:
  004af5d1  8b461c                mov       eax,[esi+1Ch]
  004af5d4  8b4e20                mov       ecx,[esi+20h]
  004af5d7  8b5624                mov       edx,[esi+24h]
  004af5da  8d5e1c                lea       ebx,[esi+1Ch]
  004af5dd  89442428              mov       [esp+28h],eax
  004af5e1  8b4664                mov       eax,[esi+64h]
  004af5e4  894c242c              mov       [esp+2Ch],ecx
  004af5e8  8b4e60                mov       ecx,[esi+60h]
  004af5eb  89542430              mov       [esp+30h],edx
  004af5ef  668b5632              mov       dx,[esi+32h]
  004af5f3  8944246c              mov       [esp+6Ch],eax
  004af5f7  8b467c                mov       eax,[esi+7Ch]
  004af5fa  894c2454              mov       [esp+54h],ecx
  004af5fe  668b8e90000000        mov       cx,[esi+90h]
  004af605  6689942492000000      mov       [esp+92h],dx
  004af60d  8b9680000000          mov       edx,[esi+80h]
  004af613  8b7e6c                mov       edi,[esi+6Ch]
  004af616  89442468              mov       [esp+68h],eax
  004af61a  8b8684000000          mov       eax,[esi+84h]
  004af620  66894c2470            mov       [esp+70h],cx
  004af625  8b8e88000000          mov       ecx,[esi+88h]
  004af62b  89542464              mov       [esp+64h],edx
  004af62f  8b968c000000          mov       edx,[esi+8Ch]
  004af635  8944244c              mov       [esp+4Ch],eax
  004af639  894c2460              mov       [esp+60h],ecx
  004af63d  8d442424              lea       eax,[esp+24h]
  004af641  8954245c              mov       [esp+5Ch],edx
  004af645  8d4c2474              lea       ecx,[esp+74h]
  004af649  50                    push      eax
  004af64a  8d54247c              lea       edx,[esp+7Ch]
  004af64e  51                    push      ecx
  004af64f  52                    push      edx
  004af650  56                    push      esi
  004af651  897c2460              mov       [esp+60h],edi
  004af655  e826930800            call      00538980h
  004af65a  83c410                add       esp,10h
  004af65d  8d442458              lea       eax,[esp+58h]
  004af661  8d4c2420              lea       ecx,[esp+20h]
  004af665  8d54241c              lea       edx,[esp+1Ch]
  004af669  50                    push      eax
  004af66a  51                    push      ecx
  004af66b  8d442420              lea       eax,[esp+20h]
  004af66f  52                    push      edx
  004af670  8d4c2420              lea       ecx,[esp+20h]
  004af674  50                    push      eax
  004af675  51                    push      ecx
  004af676  56                    push      esi
  004af677  e8a4680800            call      00535F20h
  004af67c  8b4c242c              mov       ecx,[esp+2Ch]
  004af680  8b542470              mov       edx,[esp+70h]
  004af684  83c418                add       esp,18h
  004af687  bd40000000            mov       ebp,40h
  004af68c  2be9                  sub       ebp,ecx
  004af68e  52                    push      edx
  004af68f  56                    push      esi
  004af690  896c2418              mov       [esp+18h],ebp
  004af694  e847670200            call      004D5DE0h
  004af699  8b03                  mov       eax,[ebx]
  004af69b  8b571c                mov       edx,[edi+1Ch]
  004af69e  8b4e20                mov       ecx,[esi+20h]
  004af6a1  2bc2                  sub       eax,edx
  004af6a3  8b5720                mov       edx,[edi+20h]
  004af6a6  83c408                add       esp,8
  004af6a9  2bca                  sub       ecx,edx
  004af6ab  8b5624                mov       edx,[esi+24h]
  004af6ae  2b5724                sub       edx,[edi+24h]
  004af6b1  c1f80c                sar       eax,0Ch
  004af6b4  8bf8                  mov       edi,eax
  004af6b6  c1f90c                sar       ecx,0Ch
  004af6b9  0faff8                imul      edi,eax
  004af6bc  8bc1                  mov       eax,ecx
  004af6be  c1fa0c                sar       edx,0Ch
  004af6c1  0fafc1                imul      eax,ecx
  004af6c4  8bca                  mov       ecx,edx
  004af6c6  03f8                  add       edi,eax
  004af6c8  0fafca                imul      ecx,edx
  004af6cb  03f9                  add       edi,ecx
  004af6cd  57                    push      edi
  004af6ce  e83d630900            call      00545A10h
  004af6d3  89442418              mov       [esp+18h],eax
  004af6d7  8b442420              mov       eax,[esp+20h]
  004af6db  85c0                  test      eax,eax
  004af6dd  0f84a0080000          je        near 004AFF83h
  004af6e3  8b3e                  mov       edi,[esi]
  004af6e5  33d2                  xor       edx,edx
  004af6e7  8a5604                mov       dl,[esi+4]
  004af6ea  81e7fffffffd          and       edi,0FDFFFFFFh
  004af6f0  0fbf4e18              movsx     ecx,word [esi+18h]
  004af6f4  893e                  mov       [esi],edi
  004af6f6  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004af6fd  8bc7                  mov       eax,edi
  004af6ff  8b0c8a                mov       ecx,[edx+ecx*4]
  004af702  a900000001            test      eax,1000000h
  004af707  741d                  je        short 004AF726h
  004af709  66ff461a              inc       word [esi+1Ah]
  004af70d  668b461a              mov       ax,[esi+1Ah]
  004af711  33d2                  xor       edx,edx
  004af713  668b5116              mov       dx,[ecx+16h]
  004af717  0fbfc0                movsx     eax,ax
  004af71a  3bc2                  cmp       eax,edx
  004af71c  7c29                  jl        short 004AF747h
  004af71e  66c7461a0000          mov       word [esi+1Ah],0
  004af724  eb21                  jmp       short 004AF747h

LAB_004af726:
  004af726  66ff461a              inc       word [esi+1Ah]
  004af72a  668b561a              mov       dx,[esi+1Ah]
  004af72e  33ff                  xor       edi,edi
  004af730  668b7916              mov       di,[ecx+16h]
  004af734  0fbfca                movsx     ecx,dx
  004af737  3bcf                  cmp       ecx,edi
  004af739  7c0c                  jl        short 004AF747h
  004af73b  4a                    dec       edx
  004af73c  0d00000002            or        eax,2000000h
  004af741  6689561a              mov       [esi+1Ah],dx
  004af745  8906                  mov       [esi],eax

LAB_004af747:
  004af747  8a4642                mov       al,[esi+42h]
  004af74a  8b0e                  mov       ecx,[esi]
  004af74c  3c05                  cmp       al,5
  004af74e  7505                  jne       short 004AF755h
  004af750  83c910                or        ecx,10h
  004af753  eb03                  jmp       short 004AF758h

LAB_004af755:
  004af755  83e1ef                and       ecx,0FFFFFFEFh

LAB_004af758:
  004af758  f6c102                test      cl,2
  004af75b  890e                  mov       [esi],ecx
  004af75d  0f8427020000          je        near 004AF98Ah
  004af763  3c0c                  cmp       al,0Ch
  004af765  747b                  je        short 004AF7E2h
  004af767  3c0d                  cmp       al,0Dh
  004af769  7477                  je        short 004AF7E2h
  004af76b  66837e1a08            cmp       word [esi+1Ah],8
  004af770  7555                  jne       short 004AF7C7h
  004af772  8d542434              lea       edx,[esp+34h]
  004af776  6a00                  push      0
  004af778  52                    push      edx
  004af779  53                    push      ebx
  004af77a  56                    push      esi
  004af77b  e8b0a40600            call      00519C30h
  004af780  8bf8                  mov       edi,eax
  004af782  a1b8e05d00            mov       eax,[5DE0B8h]
  004af787  83c410                add       esp,10h
  004af78a  85c0                  test      eax,eax
  004af78c  7439                  je        short 004AF7C7h
  004af78e  66837e1a08            cmp       word [esi+1Ah],8
  004af793  7532                  jne       short 004AF7C7h
  004af795  8b0f                  mov       ecx,[edi]
  004af797  57                    push      edi
  004af798  83c940                or        ecx,40h
  004af79b  56                    push      esi
  004af79c  66c787960000001000    mov       word [edi+96h],10h
  004af7a5  890f                  mov       [edi],ecx
  004af7a7  e8d4a30800            call      00539B80h
  004af7ac  83c408                add       esp,8
  004af7af  83c71c                add       edi,1Ch
  004af7b2  57                    push      edi
  004af7b3  6800180000            push      1800h
  004af7b8  6800030000            push      300h
  004af7bd  6a03                  push      3
  004af7bf  e85ce30800            call      0053DB20h
  004af7c4  83c410                add       esp,10h

LAB_004af7c7:
  004af7c7  8b06                  mov       eax,[esi]
  004af7c9  a900000002            test      eax,2000000h
  004af7ce  7412                  je        short 004AF7E2h
  004af7d0  a801                  test      al,1
  004af7d2  750a                  jne       short 004AF7DEh
  004af7d4  24fd                  and       al,0FDh
  004af7d6  c6464300              mov       byte [esi+43h],0
  004af7da  8906                  mov       [esi],eax
  004af7dc  eb04                  jmp       short 004AF7E2h

LAB_004af7de:
  004af7de  24fd                  and       al,0FDh
  004af7e0  8906                  mov       [esi],eax

LAB_004af7e2:
  004af7e2  807e420c              cmp       byte [esi+42h],0Ch
  004af7e6  0f85c6000000          jne       near 004AF8B2h
  004af7ec  668b461a              mov       ax,[esi+1Ah]
  004af7f0  663d0800              cmp       ax,8
  004af7f4  7507                  jne       short 004AF7FDh
  004af7f6  66837e1803            cmp       word [esi+18h],3
  004af7fb  7415                  je        short 004AF812h

LAB_004af7fd:
  004af7fd  663d0a00              cmp       ax,0Ah
  004af801  0f85ab000000          jne       near 004AF8B2h
  004af807  66837e1805            cmp       word [esi+18h],5
  004af80c  0f85a0000000          jne       near 004AF8B2h

LAB_004af812:
  004af812  8d442434              lea       eax,[esp+34h]
  004af816  6a00                  push      0
  004af818  50                    push      eax
  004af819  53                    push      ebx
  004af81a  56                    push      esi
  004af81b  e810a40600            call      00519C30h
  004af820  8bf8                  mov       edi,eax
  004af822  a1b8e05d00            mov       eax,[5DE0B8h]
  004af827  83c410                add       esp,10h
  004af82a  85c0                  test      eax,eax
  004af82c  0f8480000000          je        near 004AF8B2h
  004af832  668b461a              mov       ax,[esi+1Ah]
  004af836  663d0800              cmp       ax,8
  004af83a  7507                  jne       short 004AF843h
  004af83c  66837e1803            cmp       word [esi+18h],3
  004af841  740d                  je        short 004AF850h

LAB_004af843:
  004af843  663d0a00              cmp       ax,0Ah
  004af847  7569                  jne       short 004AF8B2h
  004af849  66837e1805            cmp       word [esi+18h],5
  004af84e  7562                  jne       short 004AF8B2h

LAB_004af850:
  004af850  66837e1201            cmp       word [esi+12h],1
  004af855  750b                  jne       short 004AF862h
  004af857  66c787960000000000    mov       word [edi+96h],0
  004af860  eb0f                  jmp       short 004AF871h

LAB_004af862:
  004af862  8b07                  mov       eax,[edi]
  004af864  66c787960000001000    mov       word [edi+96h],10h
  004af86d  0c40                  or        al,40h
  004af86f  8907                  mov       [edi],eax

LAB_004af871:
  004af871  8b4c2424              mov       ecx,[esp+24h]
  004af875  57                    push      edi
  004af876  56                    push      esi
  004af877  668b4130              mov       ax,[ecx+30h]
  004af87b  0fbfe8                movsx     ebp,ax
  004af87e  66c1f802              sar       ax,2
  004af882  66894130              mov       [ecx+30h],ax
  004af886  e8f5a20800            call      00539B80h
  004af88b  8b4c242c              mov       ecx,[esp+2Ch]
  004af88f  83c408                add       esp,8
  004af892  83c71c                add       edi,1Ch
  004af895  57                    push      edi
  004af896  6800180000            push      1800h
  004af89b  6800030000            push      300h
  004af8a0  6a03                  push      3
  004af8a2  66896930              mov       [ecx+30h],bp
  004af8a6  e875e20800            call      0053DB20h
  004af8ab  8b6c2420              mov       ebp,[esp+20h]
  004af8af  83c410                add       esp,10h

LAB_004af8b2:
  004af8b2  807e420d              cmp       byte [esi+42h],0Dh
  004af8b6  0f850a010000          jne       near 004AF9C6h
  004af8bc  8d542440              lea       edx,[esp+40h]
  004af8c0  6a00                  push      0
  004af8c2  52                    push      edx
  004af8c3  53                    push      ebx
  004af8c4  56                    push      esi
  004af8c5  e866a30600            call      00519C30h
  004af8ca  8bf8                  mov       edi,eax
  004af8cc  a1b8e05d00            mov       eax,[5DE0B8h]
  004af8d1  83c410                add       esp,10h
  004af8d4  85c0                  test      eax,eax
  004af8d6  0f84ea000000          je        near 004AF9C6h
  004af8dc  8b2f                  mov       ebp,[edi]
  004af8de  66c787960000001000    mov       word [edi+96h],10h
  004af8e7  83cd40                or        ebp,40h
  004af8ea  57                    push      edi
  004af8eb  892f                  mov       [edi],ebp
  004af8ed  8b4c2428              mov       ecx,[esp+28h]
  004af8f1  56                    push      esi
  004af8f2  668b4130              mov       ax,[ecx+30h]
  004af8f6  0fbfe8                movsx     ebp,ax
  004af8f9  66c1f802              sar       ax,2
  004af8fd  66894130              mov       [ecx+30h],ax
  004af901  e87aa20800            call      00539B80h
  004af906  8b44242c              mov       eax,[esp+2Ch]
  004af90a  83c408                add       esp,8
  004af90d  8d4f1c                lea       ecx,[edi+1Ch]
  004af910  51                    push      ecx
  004af911  6800180000            push      1800h
  004af916  6800030000            push      300h
  004af91b  6a03                  push      3
  004af91d  66896830              mov       [eax+30h],bp
  004af921  e8fae10800            call      0053DB20h
  004af926  8b07                  mov       eax,[edi]
  004af928  83c410                add       esp,10h
  004af92b  a900000040            test      eax,40000000h
  004af930  7548                  jne       short 004AF97Ah
  004af932  6aff                  push      0FFFFFFFFh
  004af934  6a00                  push      0
  004af936  6a39                  push      39h
  004af938  56                    push      esi
  004af939  e892b0fcff            call      0047A9D0h
  004af93e  8b07                  mov       eax,[edi]
  004af940  83c410                add       esp,10h
  004af943  0c01                  or        al,1
  004af945  8907                  mov       [edi],eax
  004af947  0fbf5632              movsx     edx,word [esi+32h]
  004af94b  52                    push      edx
  004af94c  e8cb600900            call      00545A1Ch
  004af951  8b4e5c                mov       ecx,[esi+5Ch]
  004af954  c1f90d                sar       ecx,0Dh
  004af957  0fafc1                imul      eax,ecx
  004af95a  894750                mov       [edi+50h],eax
  004af95d  0fbf5632              movsx     edx,word [esi+32h]
  004af961  52                    push      edx
  004af962  e8af600900            call      00545A16h
  004af967  8b4e5c                mov       ecx,[esi+5Ch]
  004af96a  c7475400800100        mov       dword [edi+54h],18000h
  004af971  c1f90d                sar       ecx,0Dh
  004af974  0fafc1                imul      eax,ecx
  004af977  894758                mov       [edi+58h],eax

LAB_004af97a:
  004af97a  8b06                  mov       eax,[esi]
  004af97c  8b6c2410              mov       ebp,[esp+10h]
  004af980  24fd                  and       al,0FDh
  004af982  8906                  mov       [esi],eax
  004af984  8b442414              mov       eax,[esp+14h]
  004af988  eb40                  jmp       short 004AF9CAh

LAB_004af98a:
  004af98a  f6c101                test      cl,1
  004af98d  7437                  je        short 004AF9C6h
  004af98f  66837e1a03            cmp       word [esi+1Ah],3
  004af994  7d30                  jge       short 004AF9C6h
  004af996  e8a5770900            call      00547140h
  004af99b  99                    cdq
  004af99c  b93c000000            mov       ecx,3Ch
  004af9a1  f7f9                  idiv      ecx
  004af9a3  8b442414              mov       eax,[esp+14h]
  004af9a7  3bd0                  cmp       edx,eax
  004af9a9  7d1f                  jge       short 004AF9CAh
  004af9ab  8b16                  mov       edx,[esi]
  004af9ad  66c746180300          mov       word [esi+18h],3
  004af9b3  81e2fffffffe          and       edx,0FEFFFFFFh
  004af9b9  66c7461a0000          mov       word [esi+1Ah],0
  004af9bf  83ca02                or        edx,2
  004af9c2  8916                  mov       [esi],edx
  004af9c4  eb04                  jmp       short 004AF9CAh

LAB_004af9c6:
  004af9c6  8b442414              mov       eax,[esp+14h]

LAB_004af9ca:
  004af9ca  33c9                  xor       ecx,ecx
  004af9cc  8a4e42                mov       cl,[esi+42h]
  004af9cf  83f90d                cmp       ecx,0Dh
  004af9d2  0f87ab050000          ja        near 004AFF83h
  004af9d8  ff248dc4054b00        jmp       dword [ecx*4+4B05C4h]
  004af9df  33c9                  xor       ecx,ecx
  004af9e1  66394e12              cmp       [esi+12h],cx
  004af9e5  751a                  jne       short 004AFA01h
  004af9e7  8b06                  mov       eax,[esi]
  004af9e9  66894e18              mov       [esi+18h],cx
  004af9ed  0d00000001            or        eax,1000000h
  004af9f2  66894e1a              mov       [esi+1Ah],cx
  004af9f6  8906                  mov       [esi],eax
  004af9f8  66c746120100          mov       word [esi+12h],1
  004af9fe  894e5c                mov       [esi+5Ch],ecx

LAB_004afa01:
  004afa01  0fbf4610              movsx     eax,word [esi+10h]
  004afa05  39442418              cmp       [esp+18h],eax
  004afa09  7d09                  jge       short 004AFA14h
  004afa0b  c6464300              mov       byte [esi+43h],0
  004afa0f  e96f050000            jmp       004AFF83h

LAB_004afa14:
  004afa14  8a4643                mov       al,[esi+43h]
  004afa17  84c0                  test      al,al
  004afa19  7405                  je        short 004AFA20h
  004afa1b  fec8                  dec       al
  004afa1d  884643                mov       [esi+43h],al

LAB_004afa20:
  004afa20  894e5c                mov       [esi+5Ch],ecx
  004afa23  e95b050000            jmp       004AFF83h
  004afa28  33d2                  xor       edx,edx
  004afa2a  66395612              cmp       [esi+12h],dx
  004afa2e  751b                  jne       short 004AFA4Bh
  004afa30  8b3e                  mov       edi,[esi]
  004afa32  b901000000            mov       ecx,1
  004afa37  81cf00000001          or        edi,1000000h
  004afa3d  66894e18              mov       [esi+18h],cx
  004afa41  6689561a              mov       [esi+1Ah],dx
  004afa45  893e                  mov       [esi],edi
  004afa47  66894e12              mov       [esi+12h],cx

LAB_004afa4b:
  004afa4b  8b4c241c              mov       ecx,[esp+1Ch]
  004afa4f  8db980000000          lea       edi,[ecx+80h]
  004afa55  81e7ff0f0000          and       edi,0FFFh
  004afa5b  81ff90010000          cmp       edi,190h
  004afa61  7f0c                  jg        short 004AFA6Fh
  004afa63  c6464300              mov       byte [esi+43h],0
  004afa67  89565c                mov       [esi+5Ch],edx
  004afa6a  e914050000            jmp       004AFF83h

LAB_004afa6f:
  004afa6f  81e1ff0f0000          and       ecx,0FFFh
  004afa75  81f900080000          cmp       ecx,800h
  004afa7b  7d12                  jge       short 004AFA8Fh
  004afa7d  8d8880000000          lea       ecx,[eax+80h]
  004afa83  89565c                mov       [esi+5Ch],edx
  004afa86  66014e32              add       [esi+32h],cx
  004afa8a  e9f4040000            jmp       004AFF83h

LAB_004afa8f:
  004afa8f  b980ffffff            mov       ecx,0FFFFFF80h
  004afa94  89565c                mov       [esi+5Ch],edx
  004afa97  2bc8                  sub       ecx,eax
  004afa99  66014e32              add       [esi+32h],cx
  004afa9d  e9e1040000            jmp       004AFF83h
  004afaa2  33d2                  xor       edx,edx
  004afaa4  b901000000            mov       ecx,1
  004afaa9  66395612              cmp       [esi+12h],dx
  004afaad  7525                  jne       short 004AFAD4h
  004afaaf  8b3e                  mov       edi,[esi]
  004afab1  66894e18              mov       [esi+18h],cx
  004afab5  81cf00000001          or        edi,1000000h
  004afabb  6689561a              mov       [esi+1Ah],dx
  004afabf  893e                  mov       [esi],edi
  004afac1  8d3cc0                lea       edi,[eax+eax*8]
  004afac4  c1e704                shl       edi,4
  004afac7  81c700700000          add       edi,7000h
  004afacd  66894e12              mov       [esi+12h],cx
  004afad1  897e5c                mov       [esi+5Ch],edi

LAB_004afad4:
  004afad4  66394e12              cmp       [esi+12h],cx
  004afad8  0f85a5040000          jne       near 004AFF83h
  004afade  0fbf4e10              movsx     ecx,word [esi+10h]
  004afae2  394c2418              cmp       [esp+18h],ecx
  004afae6  7d0c                  jge       short 004AFAF4h
  004afae8  c6464300              mov       byte [esi+43h],0
  004afaec  89565c                mov       [esi+5Ch],edx
  004afaef  e98f040000            jmp       004AFF83h

LAB_004afaf4:
  004afaf4  8a4e43                mov       cl,[esi+43h]
  004afaf7  84c9                  test      cl,cl
  004afaf9  7405                  je        short 004AFB00h
  004afafb  fec9                  dec       cl
  004afafd  884e43                mov       [esi+43h],cl

LAB_004afb00:
  004afb00  8b4c241c              mov       ecx,[esp+1Ch]
  004afb04  8d9100010000          lea       edx,[ecx+100h]
  004afb0a  81e2ff0f0000          and       edx,0FFFh
  004afb10  81fa00020000          cmp       edx,200h
  004afb16  0f8e67040000          jle       near 004AFF83h
  004afb1c  81e1ff0f0000          and       ecx,0FFFh
  004afb22  81f900080000          cmp       ecx,800h
  004afb28  7d10                  jge       short 004AFB3Ah
  004afb2a  8d840080000000        lea       eax,[eax+eax+80h]
  004afb31  66014632              add       [esi+32h],ax
  004afb35  e949040000            jmp       004AFF83h

LAB_004afb3a:
  004afb3a  8d0c00                lea       ecx,[eax+eax]
  004afb3d  b880ffffff            mov       eax,0FFFFFF80h
  004afb42  2bc1                  sub       eax,ecx
  004afb44  66014632              add       [esi+32h],ax
  004afb48  e936040000            jmp       004AFF83h
  004afb4d  66837e1201            cmp       word [esi+12h],1
  004afb52  0f859a000000          jne       near 004AFBF2h
  004afb58  8a4e43                mov       cl,[esi+43h]
  004afb5b  84c9                  test      cl,cl
  004afb5d  7405                  je        short 004AFB64h
  004afb5f  fec9                  dec       cl
  004afb61  884e43                mov       [esi+43h],cl

LAB_004afb64:
  004afb64  8b7c2420              mov       edi,[esp+20h]
  004afb68  8d14c0                lea       edx,[eax+eax*8]
  004afb6b  c1e204                shl       edx,4
  004afb6e  8d8200700000          lea       eax,[edx+7000h]
  004afb74  0faff8                imul      edi,eax
  004afb77  89465c                mov       [esi+5Ch],eax
  004afb7a  0fbf4610              movsx     eax,word [esi+10h]
  004afb7e  50                    push      eax
  004afb7f  c1ff0c                sar       edi,0Ch
  004afb82  e8955e0900            call      00545A1Ch
  004afb87  0fafc7                imul      eax,edi
  004afb8a  8b0b                  mov       ecx,[ebx]
  004afb8c  c1f80c                sar       eax,0Ch
  004afb8f  03c8                  add       ecx,eax
  004afb91  890b                  mov       [ebx],ecx
  004afb93  0fbf4e10              movsx     ecx,word [esi+10h]
  004afb97  51                    push      ecx
  004afb98  e8795e0900            call      00545A16h
  004afb9d  0fafc7                imul      eax,edi
  004afba0  8b5e24                mov       ebx,[esi+24h]
  004afba3  c7465c00000000        mov       dword [esi+5Ch],0
  004afbaa  c1f80c                sar       eax,0Ch
  004afbad  03d8                  add       ebx,eax
  004afbaf  8b44241c              mov       eax,[esp+1Ch]
  004afbb3  895e24                mov       [esi+24h],ebx
  004afbb6  8d9000010000          lea       edx,[eax+100h]
  004afbbc  81e2ff0f0000          and       edx,0FFFh
  004afbc2  81fa00020000          cmp       edx,200h
  004afbc8  7e28                  jle       short 004AFBF2h
  004afbca  25ff0f0000            and       eax,0FFFh
  004afbcf  3d00080000            cmp       eax,800h
  004afbd4  7d0a                  jge       short 004AFBE0h
  004afbd6  8b442414              mov       eax,[esp+14h]
  004afbda  8d440032              lea       eax,[eax+eax+32h]
  004afbde  eb0e                  jmp       short 004AFBEEh

LAB_004afbe0:
  004afbe0  8b4c2414              mov       ecx,[esp+14h]
  004afbe4  b8ceffffff            mov       eax,0FFFFFFCEh
  004afbe9  8d1409                lea       edx,[ecx+ecx]
  004afbec  2bc2                  sub       eax,edx

LAB_004afbee:
  004afbee  66014632              add       [esi+32h],ax

LAB_004afbf2:
  004afbf2  66837e1200            cmp       word [esi+12h],0
  004afbf7  0f8586030000          jne       near 004AFF83h
  004afbfd  8b0e                  mov       ecx,[esi]
  004afbff  b801000000            mov       eax,1
  004afc04  81c900000001          or        ecx,1000000h
  004afc0a  66894618              mov       [esi+18h],ax
  004afc0e  66c7461a0000          mov       word [esi+1Ah],0
  004afc14  890e                  mov       [esi],ecx
  004afc16  66894612              mov       [esi+12h],ax
  004afc1a  e964030000            jmp       004AFF83h
  004afc1f  668b4e12              mov       cx,[esi+12h]
  004afc23  6683f901              cmp       cx,1
  004afc27  7514                  jne       short 004AFC3Dh
  004afc29  8a4643                mov       al,[esi+43h]
  004afc2c  84c0                  test      al,al
  004afc2e  740d                  je        short 004AFC3Dh
  004afc30  f70600000002          test      dword [esi],2000000h
  004afc36  7405                  je        short 004AFC3Dh
  004afc38  fec8                  dec       al
  004afc3a  884643                mov       [esi+43h],al

LAB_004afc3d:
  004afc3d  33c0                  xor       eax,eax
  004afc3f  663bc8                cmp       cx,ax
  004afc42  0f853b030000          jne       near 004AFF83h
  004afc48  6689461a              mov       [esi+1Ah],ax
  004afc4c  89465c                mov       [esi+5Ch],eax
  004afc4f  8b06                  mov       eax,[esi]
  004afc51  66c746120100          mov       word [esi+12h],1
  004afc57  25fffffffe            and       eax,0FEFFFFFFh
  004afc5c  66c746180600          mov       word [esi+18h],6
  004afc62  8906                  mov       [esi],eax
  004afc64  e91a030000            jmp       004AFF83h
  004afc69  66837e1200            cmp       word [esi+12h],0
  004afc6e  757f                  jne       short 004AFCEFh
  004afc70  8b0e                  mov       ecx,[esi]
  004afc72  f6c101                test      cl,1
  004afc75  7406                  je        short 004AFC7Dh
  004afc77  c6464300              mov       byte [esi+43h],0
  004afc7b  eb72                  jmp       short 004AFCEFh

LAB_004afc7d:
  004afc7d  8b7c2420              mov       edi,[esp+20h]
  004afc81  0500010000            add       eax,100h
  004afc86  c1e006                shl       eax,6
  004afc89  0faff8                imul      edi,eax
  004afc8c  89465c                mov       [esi+5Ch],eax
  004afc8f  81e1fffffffe          and       ecx,0FEFFFFFFh
  004afc95  0fbf4610              movsx     eax,word [esi+10h]
  004afc99  bb01000000            mov       ebx,1
  004afc9e  50                    push      eax
  004afc9f  890e                  mov       [esi],ecx
  004afca1  66895e18              mov       [esi+18h],bx
  004afca5  66c7461a0000          mov       word [esi+1Ah],0
  004afcab  c1ff0c                sar       edi,0Ch
  004afcae  e8695d0900            call      00545A1Ch
  004afcb3  0fafc7                imul      eax,edi
  004afcb6  8b4e50                mov       ecx,[esi+50h]
  004afcb9  c1f80c                sar       eax,0Ch
  004afcbc  03c8                  add       ecx,eax
  004afcbe  894e50                mov       [esi+50h],ecx
  004afcc1  0fbf4e10              movsx     ecx,word [esi+10h]
  004afcc5  51                    push      ecx
  004afcc6  e84b5d0900            call      00545A16h
  004afccb  0fafc7                imul      eax,edi
  004afcce  8b5658                mov       edx,[esi+58h]
  004afcd1  8b4e54                mov       ecx,[esi+54h]
  004afcd4  c1f80c                sar       eax,0Ch
  004afcd7  03d0                  add       edx,eax
  004afcd9  8b06                  mov       eax,[esi]
  004afcdb  81c100e00100          add       ecx,1E000h
  004afce1  0bc3                  or        eax,ebx
  004afce3  895658                mov       [esi+58h],edx
  004afce6  894e54                mov       [esi+54h],ecx
  004afce9  8906                  mov       [esi],eax
  004afceb  66895e12              mov       [esi+12h],bx

LAB_004afcef:
  004afcef  f60601                test      byte [esi],1
  004afcf2  0f858b020000          jne       near 004AFF83h
  004afcf8  c6464300              mov       byte [esi+43h],0
  004afcfc  e982020000            jmp       004AFF83h
  004afd01  668b5612              mov       dx,[esi+12h]
  004afd05  bf01000000            mov       edi,1
  004afd0a  663bd7                cmp       dx,di
  004afd0d  751c                  jne       short 004AFD2Bh
  004afd0f  0fbf4e10              movsx     ecx,word [esi+10h]
  004afd13  394c2418              cmp       [esp+18h],ecx
  004afd17  7d06                  jge       short 004AFD1Fh
  004afd19  c6464300              mov       byte [esi+43h],0
  004afd1d  eb0c                  jmp       short 004AFD2Bh

LAB_004afd1f:
  004afd1f  8a4e43                mov       cl,[esi+43h]
  004afd22  84c9                  test      cl,cl
  004afd24  7405                  je        short 004AFD2Bh
  004afd26  fec9                  dec       cl
  004afd28  884e43                mov       [esi+43h],cl

LAB_004afd2b:
  004afd2b  6685d2                test      dx,dx
  004afd2e  0f854f020000          jne       near 004AFF83h
  004afd34  6689561a              mov       [esi+1Ah],dx
  004afd38  8b16                  mov       edx,[esi]
  004afd3a  81ca00000001          or        edx,1000000h
  004afd40  66897e18              mov       [esi+18h],di
  004afd44  8916                  mov       [esi],edx
  004afd46  8d14c0                lea       edx,[eax+eax*8]
  004afd49  c1e204                shl       edx,4
  004afd4c  81c200700000          add       edx,7000h
  004afd52  66897e12              mov       [esi+12h],di
  004afd56  89565c                mov       [esi+5Ch],edx
  004afd59  e925020000            jmp       004AFF83h
  004afd5e  bf01000000            mov       edi,1
  004afd63  66397e12              cmp       [esi+12h],di
  004afd67  7520                  jne       short 004AFD89h
  004afd69  66837e1a08            cmp       word [esi+1Ah],8
  004afd6e  7519                  jne       short 004AFD89h
  004afd70  66397e16              cmp       [esi+16h],di
  004afd74  7513                  jne       short 004AFD89h
  004afd76  6aff                  push      0FFFFFFFFh
  004afd78  6a00                  push      0
  004afd7a  6a36                  push      36h
  004afd7c  56                    push      esi
  004afd7d  e84eacfcff            call      0047A9D0h
  004afd82  8b442424              mov       eax,[esp+24h]
  004afd86  83c410                add       esp,10h

LAB_004afd89:
  004afd89  8b0e                  mov       ecx,[esi]
  004afd8b  f7c100000002          test      ecx,2000000h
  004afd91  7404                  je        short 004AFD97h
  004afd93  c6464300              mov       byte [esi+43h],0

LAB_004afd97:
  004afd97  33d2                  xor       edx,edx
  004afd99  66395612              cmp       [esi+12h],dx
  004afd9d  0f85e0010000          jne       near 004AFF83h
  004afda3  81e1fffffffe          and       ecx,0FEFFFFFFh
  004afda9  83f81e                cmp       eax,1Eh
  004afdac  66897e12              mov       [esi+12h],di
  004afdb0  66c746180500          mov       word [esi+18h],5
  004afdb6  6689561a              mov       [esi+1Ah],dx
  004afdba  890e                  mov       [esi],ecx
  004afdbc  7d03                  jge       short 004AFDC1h
  004afdbe  89565c                mov       [esi+5Ch],edx

LAB_004afdc1:
  004afdc1  66395616              cmp       [esi+16h],dx
  004afdc5  0f85b8010000          jne       near 004AFF83h
  004afdcb  c6464300              mov       byte [esi+43h],0
  004afdcf  e9af010000            jmp       004AFF83h
  004afdd4  668b4e12              mov       cx,[esi+12h]
  004afdd8  6683f901              cmp       cx,1
  004afddc  7511                  jne       short 004AFDEFh
  004afdde  8b06                  mov       eax,[esi]
  004afde0  a900000002            test      eax,2000000h
  004afde5  7408                  je        short 004AFDEFh
  004afde7  24fd                  and       al,0FDh
  004afde9  c6464300              mov       byte [esi+43h],0
  004afded  8906                  mov       [esi],eax

LAB_004afdef:
  004afdef  6685c9                test      cx,cx
  004afdf2  0f858b010000          jne       near 004AFF83h
  004afdf8  8b06                  mov       eax,[esi]
  004afdfa  66c746120100          mov       word [esi+12h],1
  004afe00  25fffffffe            and       eax,0FEFFFFFFh
  004afe05  8906                  mov       [esi],eax
  004afe07  e977010000            jmp       004AFF83h
  004afe0c  bf02000000            mov       edi,2
  004afe11  66397e12              cmp       [esi+12h],di
  004afe15  752a                  jne       short 004AFE41h
  004afe17  66837e1a06            cmp       word [esi+1Ah],6
  004afe1c  7512                  jne       short 004AFE30h
  004afe1e  6aff                  push      0FFFFFFFFh
  004afe20  6a00                  push      0
  004afe22  68ba000000            push      0BAh
  004afe27  56                    push      esi
  004afe28  e8a3abfcff            call      0047A9D0h
  004afe2d  83c410                add       esp,10h

LAB_004afe30:
  004afe30  8b06                  mov       eax,[esi]
  004afe32  a900000002            test      eax,2000000h
  004afe37  7408                  je        short 004AFE41h
  004afe39  24fd                  and       al,0FDh
  004afe3b  c6464300              mov       byte [esi+43h],0
  004afe3f  8906                  mov       [esi],eax

LAB_004afe41:
  004afe41  b901000000            mov       ecx,1
  004afe46  66394e12              cmp       [esi+12h],cx
  004afe4a  7521                  jne       short 004AFE6Dh
  004afe4c  8b06                  mov       eax,[esi]
  004afe4e  a900000002            test      eax,2000000h
  004afe53  7418                  je        short 004AFE6Dh
  004afe55  25fffffffe            and       eax,0FEFFFFFFh
  004afe5a  66897e12              mov       [esi+12h],di
  004afe5e  8906                  mov       [esi],eax
  004afe60  b805000000            mov       eax,5
  004afe65  66894618              mov       [esi+18h],ax
  004afe69  6689461a              mov       [esi+1Ah],ax

LAB_004afe6d:
  004afe6d  33c0                  xor       eax,eax
  004afe6f  66394612              cmp       [esi+12h],ax
  004afe73  0f850a010000          jne       near 004AFF83h
  004afe79  66894e12              mov       [esi+12h],cx
  004afe7d  8b0e                  mov       ecx,[esi]
  004afe7f  81e1fffffffe          and       ecx,0FEFFFFFFh
  004afe85  66c746180300          mov       word [esi+18h],3
  004afe8b  0bcf                  or        ecx,edi
  004afe8d  6689461a              mov       [esi+1Ah],ax
  004afe91  890e                  mov       [esi],ecx
  004afe93  89465c                mov       [esi+5Ch],eax
  004afe96  e9e8000000            jmp       004AFF83h
  004afe9b  668b5612              mov       dx,[esi+12h]
  004afe9f  6683fa02              cmp       dx,2
  004afea3  7527                  jne       short 004AFECCh
  004afea5  8b4e5c                mov       ecx,[esi+5Ch]
  004afea8  8bf9                  mov       edi,ecx
  004afeaa  c1ff04                sar       edi,4
  004afead  2bcf                  sub       ecx,edi
  004afeaf  81f900a00000          cmp       ecx,0A000h
  004afeb5  894e5c                mov       [esi+5Ch],ecx
  004afeb8  7d12                  jge       short 004AFECCh
  004afeba  8b0e                  mov       ecx,[esi]
  004afebc  c6464300              mov       byte [esi+43h],0
  004afec0  83e1fd                and       ecx,0FFFFFFFDh
  004afec3  c7465c00000000        mov       dword [esi+5Ch],0
  004afeca  890e                  mov       [esi],ecx

LAB_004afecc:
  004afecc  6683fa01              cmp       dx,1
  004afed0  bf04000000            mov       edi,4
  004afed5  755c                  jne       short 004AFF33h
  004afed7  8b4c241c              mov       ecx,[esp+1Ch]
  004afedb  8d9100010000          lea       edx,[ecx+100h]
  004afee1  81e2ff0f0000          and       edx,0FFFh
  004afee7  81fa00020000          cmp       edx,200h
  004afeed  7e22                  jle       short 004AFF11h
  004afeef  81e1ff0f0000          and       ecx,0FFFh
  004afef5  81f900080000          cmp       ecx,800h
  004afefb  7d06                  jge       short 004AFF03h
  004afefd  8d440032              lea       eax,[eax+eax+32h]
  004aff01  eb0a                  jmp       short 004AFF0Dh

LAB_004aff03:
  004aff03  8d0c00                lea       ecx,[eax+eax]
  004aff06  b8ceffffff            mov       eax,0FFFFFFCEh
  004aff0b  2bc1                  sub       eax,ecx

LAB_004aff0d:
  004aff0d  66014632              add       [esi+32h],ax

LAB_004aff11:
  004aff11  66397e1a              cmp       [esi+1Ah],di
  004aff15  7c1c                  jl        short 004AFF33h
  004aff17  0fbf4616              movsx     eax,word [esi+16h]
  004aff1b  83c006                add       eax,6
  004aff1e  66c746120200          mov       word [esi+12h],2
  004aff24  8d1480                lea       edx,[eax+eax*4]
  004aff27  8b06                  mov       eax,[esi]
  004aff29  c1e20c                shl       edx,0Ch
  004aff2c  0c02                  or        al,2
  004aff2e  89565c                mov       [esi+5Ch],edx
  004aff31  8906                  mov       [esi],eax

LAB_004aff33:
  004aff33  33db                  xor       ebx,ebx
  004aff35  66395e12              cmp       [esi+12h],bx
  004aff39  7548                  jne       short 004AFF83h
  004aff3b  8b0e                  mov       ecx,[esi]
  004aff3d  66c746120100          mov       word [esi+12h],1
  004aff43  81e1fffffffe          and       ecx,0FEFFFFFFh
  004aff49  66395e16              cmp       [esi+16h],bx
  004aff4d  66897e18              mov       [esi+18h],di
  004aff51  66895e1a              mov       [esi+1Ah],bx
  004aff55  890e                  mov       [esi],ecx
  004aff57  895e5c                mov       [esi+5Ch],ebx
  004aff5a  7527                  jne       short 004AFF83h
  004aff5c  c6464202              mov       byte [esi+42h],2
  004aff60  e8db710900            call      00547140h
  004aff65  99                    cdq
  004aff66  b90a000000            mov       ecx,0Ah
  004aff6b  66c746105000          mov       word [esi+10h],50h
  004aff71  f7f9                  idiv      ecx
  004aff73  8bc5                  mov       eax,ebp
  004aff75  66895e12              mov       [esi+12h],bx
  004aff79  d1f8                  sar       eax,1
  004aff7b  02d0                  add       dl,al
  004aff7d  80c214                add       dl,14h
  004aff80  885643                mov       [esi+43h],dl

LAB_004aff83:
  004aff83  0fbf4e32              movsx     ecx,word [esi+32h]
  004aff87  51                    push      ecx
  004aff88  e88f5a0900            call      00545A1Ch
  004aff8d  0fbf5632              movsx     edx,word [esi+32h]
  004aff91  52                    push      edx
  004aff92  89842480000000        mov       [esp+80h],eax
  004aff99  e8785a0900            call      00545A16h
  004aff9e  8b4e50                mov       ecx,[esi+50h]
  004affa1  8b7e54                mov       edi,[esi+54h]
  004affa4  0fbfae9a000000        movsx     ebp,word [esi+9Ah]
  004affab  8b5c2420              mov       ebx,[esp+20h]
  004affaf  89842484000000        mov       [esp+84h],eax
  004affb6  0faf5e5c              imul      ebx,[esi+5Ch]
  004affba  8bc1                  mov       eax,ecx
  004affbc  8bd1                  mov       edx,ecx
  004affbe  c1f808                sar       eax,8
  004affc1  2bd0                  sub       edx,eax
  004affc3  8bc7                  mov       eax,edi
  004affc5  c1f903                sar       ecx,3
  004affc8  2bd1                  sub       edx,ecx
  004affca  8bcd                  mov       ecx,ebp
  004affcc  c1f808                sar       eax,8
  004affcf  c1f908                sar       ecx,8
  004affd2  2bc8                  sub       ecx,eax
  004affd4  895650                mov       [esi+50h],edx
  004affd7  03cf                  add       ecx,edi
  004affd9  8b7e58                mov       edi,[esi+58h]
  004affdc  8bc7                  mov       eax,edi
  004affde  2bcd                  sub       ecx,ebp
  004affe0  c1f803                sar       eax,3
  004affe3  8bef                  mov       ebp,edi
  004affe5  894e54                mov       [esi+54h],ecx
  004affe8  c1fb0c                sar       ebx,0Ch
  004affeb  2be8                  sub       ebp,eax
  004affed  8bc3                  mov       eax,ebx
  004affef  0faf44247c            imul      eax,[esp+7Ch]
  004afff4  c1f80c                sar       eax,0Ch
  004afff7  03c2                  add       eax,edx
  004afff9  8b561c                mov       edx,[esi+1Ch]
  004afffc  03d0                  add       edx,eax
  004afffe  8b4620                mov       eax,[esi+20h]
  004b0001  03c1                  add       eax,ecx
  004b0003  8bcb                  mov       ecx,ebx
  004b0005  0faf8c2484000000      imul      ecx,[esp+84h]
  004b000d  c1ff08                sar       edi,8
  004b0010  2bef                  sub       ebp,edi
  004b0012  894620                mov       [esi+20h],eax
  004b0015  8b4624                mov       eax,[esi+24h]
  004b0018  56                    push      esi
  004b0019  c1f90c                sar       ecx,0Ch
  004b001c  03cd                  add       ecx,ebp
  004b001e  896e58                mov       [esi+58h],ebp
  004b0021  03c1                  add       eax,ecx
  004b0023  89561c                mov       [esi+1Ch],edx
  004b0026  894624                mov       [esi+24h],eax
  004b0029  e812390800            call      00533940h
  004b002e  8b9690000000          mov       edx,[esi+90h]
  004b0034  8b4660                mov       eax,[esi+60h]
  004b0037  8b4e20                mov       ecx,[esi+20h]
  004b003a  83c404                add       esp,4
  004b003d  80e6fc                and       dh,0FCh
  004b0040  3bc8                  cmp       ecx,eax
  004b0042  899690000000          mov       [esi+90h],edx
  004b0048  7f2a                  jg        short 004B0074h
  004b004a  f6c680                test      dh,80h
  004b004d  7525                  jne       short 004B0074h
  004b004f  8b0e                  mov       ecx,[esi]
  004b0051  894620                mov       [esi+20h],eax
  004b0054  f6c101                test      cl,1
  004b0057  740f                  je        short 004B0068h
  004b0059  83e1fe                and       ecx,0FFFFFFFEh
  004b005c  33ff                  xor       edi,edi
  004b005e  890e                  mov       [esi],ecx
  004b0060  897e54                mov       [esi+54h],edi
  004b0063  e924010000            jmp       004B018Ch

LAB_004b0068:
  004b0068  83e1fe                and       ecx,0FFFFFFFEh
  004b006b  33ff                  xor       edi,edi
  004b006d  890e                  mov       [esi],ecx
  004b006f  897e54                mov       [esi+54h],edi
  004b0072  eb02                  jmp       short 004B0076h

LAB_004b0074:
  004b0074  33ff                  xor       edi,edi

LAB_004b0076:
  004b0076  8b0e                  mov       ecx,[esi]
  004b0078  f6c101                test      cl,1
  004b007b  750a                  jne       short 004B0087h
  004b007d  394620                cmp       [esi+20h],eax
  004b0080  7e05                  jle       short 004B0087h
  004b0082  83c901                or        ecx,1
  004b0085  890e                  mov       [esi],ecx

LAB_004b0087:
  004b0087  f7c282ad0020          test      edx,2000AD82h
  004b008d  0f84f9000000          je        near 004B018Ch
  004b0093  f60601                test      byte [esi],1
  004b0096  0f85c7000000          jne       near 004B0163h
  004b009c  807e4207              cmp       byte [esi+42h],7
  004b00a0  7421                  je        short 004B00C3h
  004b00a2  c6464207              mov       byte [esi+42h],7
  004b00a6  e895700900            call      00547140h
  004b00ab  99                    cdq
  004b00ac  b928000000            mov       ecx,28h
  004b00b1  66897e12              mov       [esi+12h],di
  004b00b5  f7f9                  idiv      ecx
  004b00b7  66c746105000          mov       word [esi+10h],50h
  004b00bd  80c214                add       dl,14h
  004b00c0  885643                mov       [esi+43h],dl

LAB_004b00c3:
  004b00c3  8b16                  mov       edx,[esi]
  004b00c5  8b44242c              mov       eax,[esp+2Ch]
  004b00c9  8b4c2430              mov       ecx,[esp+30h]
  004b00cd  83e2fd                and       edx,0FFFFFFFDh
  004b00d0  8916                  mov       [esi],edx
  004b00d2  8b542428              mov       edx,[esp+28h]
  004b00d6  894620                mov       [esi+20h],eax
  004b00d9  8b442470              mov       eax,[esp+70h]
  004b00dd  89561c                mov       [esi+1Ch],edx
  004b00e0  8b542468              mov       edx,[esp+68h]
  004b00e4  25ffff0000            and       eax,0FFFFh
  004b00e9  894e24                mov       [esi+24h],ecx
  004b00ec  8b4c244c              mov       ecx,[esp+4Ch]
  004b00f0  89567c                mov       [esi+7Ch],edx
  004b00f3  8b542460              mov       edx,[esp+60h]
  004b00f7  898690000000          mov       [esi+90h],eax
  004b00fd  8b442464              mov       eax,[esp+64h]
  004b0101  898e84000000          mov       [esi+84h],ecx
  004b0107  8b4c246c              mov       ecx,[esp+6Ch]
  004b010b  898680000000          mov       [esi+80h],eax
  004b0111  8b44245c              mov       eax,[esp+5Ch]
  004b0115  899688000000          mov       [esi+88h],edx
  004b011b  8b542454              mov       edx,[esp+54h]
  004b011f  6882ad0020            push      2000AD82h
  004b0124  56                    push      esi
  004b0125  89868c000000          mov       [esi+8Ch],eax
  004b012b  894e64                mov       [esi+64h],ecx
  004b012e  895660                mov       [esi+60h],edx
  004b0131  e82a5d0200            call      004D5E60h
  004b0136  83c408                add       esp,8
  004b0139  663bc7                cmp       ax,di
  004b013c  7515                  jne       short 004B0153h
  004b013e  e8fd6f0900            call      00547140h
  004b0143  99                    cdq
  004b0144  33c2                  xor       eax,edx
  004b0146  2bc2                  sub       eax,edx
  004b0148  83e001                and       eax,1
  004b014b  33c2                  xor       eax,edx
  004b014d  2bc2                  sub       eax,edx
  004b014f  8d4400ff              lea       eax,[eax+eax-1]

LAB_004b0153:
  004b0153  8b4c2414              mov       ecx,[esp+14h]
  004b0157  83c140                add       ecx,40h
  004b015a  0fafc8                imul      ecx,eax
  004b015d  66014e32              add       [esi+32h],cx
  004b0161  eb29                  jmp       short 004B018Ch

LAB_004b0163:
  004b0163  8b542428              mov       edx,[esp+28h]
  004b0167  8b442430              mov       eax,[esp+30h]
  004b016b  897e5c                mov       [esi+5Ch],edi
  004b016e  897e50                mov       [esi+50h],edi
  004b0171  897e58                mov       [esi+58h],edi
  004b0174  89561c                mov       [esi+1Ch],edx
  004b0177  894624                mov       [esi+24h],eax
  004b017a  c6464200              mov       byte [esi+42h],0
  004b017e  c6464328              mov       byte [esi+43h],28h
  004b0182  66897e12              mov       [esi+12h],di
  004b0186  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004b018c:
  004b018c  6aff                  push      0FFFFFFFFh
  004b018e  6aff                  push      0FFFFFFFFh
  004b0190  6aff                  push      0FFFFFFFFh
  004b0192  56                    push      esi
  004b0193  e878360100            call      004C3810h
  004b0198  8b442430              mov       eax,[esp+30h]
  004b019c  83c410                add       esp,10h
  004b019f  3bc7                  cmp       eax,edi
  004b01a1  7517                  jne       short 004B01BAh
  004b01a3  668b8c2492000000      mov       cx,[esp+92h]
  004b01ab  66894e32              mov       [esi+32h],cx
  004b01af  5f                    pop       edi
  004b01b0  5e                    pop       esi
  004b01b1  5d                    pop       ebp
  004b01b2  5b                    pop       ebx
  004b01b3  81c494000000          add       esp,94h
  004b01b9  c3                    ret

LAB_004b01ba:
  004b01ba  8a4643                mov       al,[esi+43h]
  004b01bd  84c0                  test      al,al
  004b01bf  0f857a030000          jne       near 004B053Fh
  004b01c5  8b466c                mov       eax,[esi+6Ch]
  004b01c8  8b561c                mov       edx,[esi+1Ch]
  004b01cb  8b781c                mov       edi,[eax+1Ch]
  004b01ce  8b6824                mov       ebp,[eax+24h]
  004b01d1  8b4624                mov       eax,[esi+24h]
  004b01d4  2bfa                  sub       edi,edx
  004b01d6  2be8                  sub       ebp,eax
  004b01d8  c1ff0c                sar       edi,0Ch
  004b01db  c1fd0c                sar       ebp,0Ch
  004b01de  8bd7                  mov       edx,edi
  004b01e0  8bc5                  mov       eax,ebp
  004b01e2  0fafd7                imul      edx,edi
  004b01e5  0fafc5                imul      eax,ebp
  004b01e8  03d0                  add       edx,eax
  004b01ea  52                    push      edx
  004b01eb  e820580900            call      00545A10h
  004b01f0  55                    push      ebp
  004b01f1  57                    push      edi
  004b01f2  89442420              mov       [esp+20h],eax
  004b01f6  e827580900            call      00545A22h
  004b01fb  668b4e32              mov       cx,[esi+32h]
  004b01ff  66c746120000          mov       word [esi+12h],0
  004b0205  2bc1                  sub       eax,ecx
  004b0207  25ff0f0000            and       eax,0FFFh
  004b020c  8944241c              mov       [esp+1Ch],eax
  004b0210  8b442418              mov       eax,[esp+18h]
  004b0214  3dfa000000            cmp       eax,0FAh
  004b0219  0f8dee000000          jge       near 004B030Dh
  004b021f  e81c6f0900            call      00547140h
  004b0224  99                    cdq
  004b0225  b964000000            mov       ecx,64h
  004b022a  f7f9                  idiv      ecx
  004b022c  8bda                  mov       ebx,edx
  004b022e  83fb32                cmp       ebx,32h
  004b0231  7d7d                  jge       short 004B02B0h
  004b0233  8b7c2410              mov       edi,[esp+10h]
  004b0237  8b54241c              mov       edx,[esp+1Ch]
  004b023b  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004b0242  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004b0249  25ff0f0000            and       eax,0FFFh
  004b024e  3bc1                  cmp       eax,ecx
  004b0250  7f52                  jg        short 004B02A4h
  004b0252  e8e96e0900            call      00547140h
  004b0257  99                    cdq
  004b0258  8d4c3f01              lea       ecx,[edi+edi+1]
  004b025c  f7f9                  idiv      ecx
  004b025e  8b442418              mov       eax,[esp+18h]
  004b0262  83c264                add       edx,64h
  004b0265  3bc2                  cmp       eax,edx
  004b0267  7d3b                  jge       short 004B02A4h
  004b0269  83fb1e                cmp       ebx,1Eh
  004b026c  7d26                  jge       short 004B0294h
  004b026e  8b16                  mov       edx,[esi]
  004b0270  c646420b              mov       byte [esi+42h],0Bh
  004b0274  81e2fffffffe          and       edx,0FEFFFFFFh
  004b027a  c6464301              mov       byte [esi+43h],1
  004b027e  83ca02                or        edx,2
  004b0281  33ed                  xor       ebp,ebp
  004b0283  8916                  mov       [esi],edx
  004b0285  896e5c                mov       [esi+5Ch],ebp
  004b0288  66c746180300          mov       word [esi+18h],3
  004b028e  66896e1a              mov       [esi+1Ah],bp
  004b0292  eb7f                  jmp       short 004B0313h

LAB_004b0294:
  004b0294  33ed                  xor       ebp,ebp
  004b0296  c646420c              mov       byte [esi+42h],0Ch
  004b029a  c6464301              mov       byte [esi+43h],1
  004b029e  66896e12              mov       [esi+12h],bp
  004b02a2  eb6f                  jmp       short 004B0313h

LAB_004b02a4:
  004b02a4  c6464201              mov       byte [esi+42h],1
  004b02a8  c6464301              mov       byte [esi+43h],1
  004b02ac  33ed                  xor       ebp,ebp
  004b02ae  eb63                  jmp       short 004B0313h

LAB_004b02b0:
  004b02b0  83fb3c                cmp       ebx,3Ch
  004b02b3  7c2d                  jl        short 004B02E2h
  004b02b5  83fb46                cmp       ebx,46h
  004b02b8  7d28                  jge       short 004B02E2h
  004b02ba  8b4c2414              mov       ecx,[esp+14h]
  004b02be  8b442418              mov       eax,[esp+18h]
  004b02c2  83c164                add       ecx,64h
  004b02c5  3bc1                  cmp       eax,ecx
  004b02c7  7e19                  jle       short 004B02E2h
  004b02c9  c6464206              mov       byte [esi+42h],6
  004b02cd  c6464301              mov       byte [esi+43h],1
  004b02d1  e86a6e0900            call      00547140h
  004b02d6  33ed                  xor       ebp,ebp
  004b02d8  66894610              mov       [esi+10h],ax
  004b02dc  66896e12              mov       [esi+12h],bp
  004b02e0  eb2d                  jmp       short 004B030Fh

LAB_004b02e2:
  004b02e2  c6464202              mov       byte [esi+42h],2
  004b02e6  e8556e0900            call      00547140h
  004b02eb  99                    cdq
  004b02ec  b90a000000            mov       ecx,0Ah
  004b02f1  8b7c2410              mov       edi,[esp+10h]
  004b02f5  f7f9                  idiv      ecx
  004b02f7  8bc7                  mov       eax,edi
  004b02f9  66c746105000          mov       word [esi+10h],50h
  004b02ff  d1f8                  sar       eax,1
  004b0301  02d0                  add       dl,al
  004b0303  80c214                add       dl,14h
  004b0306  33ed                  xor       ebp,ebp
  004b0308  885643                mov       [esi+43h],dl
  004b030b  eb06                  jmp       short 004B0313h

LAB_004b030d:
  004b030d  33ed                  xor       ebp,ebp

LAB_004b030f:
  004b030f  8b7c2410              mov       edi,[esp+10h]

LAB_004b0313:
  004b0313  8b442418              mov       eax,[esp+18h]
  004b0317  3d58020000            cmp       eax,258h
  004b031c  0f8dda000000          jge       near 004B03FCh
  004b0322  3dfa000000            cmp       eax,0FAh
  004b0327  0f8ccf000000          jl        near 004B03FCh
  004b032d  e80e6e0900            call      00547140h
  004b0332  99                    cdq
  004b0333  b964000000            mov       ecx,64h
  004b0338  f7f9                  idiv      ecx
  004b033a  8bda                  mov       ebx,edx
  004b033c  83fb14                cmp       ebx,14h
  004b033f  7d2e                  jge       short 004B036Fh
  004b0341  8b54241c              mov       edx,[esp+1Ch]
  004b0345  81c200040000          add       edx,400h
  004b034b  81e2ff0f0000          and       edx,0FFFh
  004b0351  81fa00080000          cmp       edx,800h
  004b0357  7c0d                  jl        short 004B0366h
  004b0359  c6464201              mov       byte [esi+42h],1
  004b035d  c6464301              mov       byte [esi+43h],1
  004b0361  e996000000            jmp       004B03FCh

LAB_004b0366:
  004b0366  c646420d              mov       byte [esi+42h],0Dh
  004b036a  e985000000            jmp       004B03F4h

LAB_004b036f:
  004b036f  83fb28                cmp       ebx,28h
  004b0372  7d25                  jge       short 004B0399h
  004b0374  c6464202              mov       byte [esi+42h],2
  004b0378  e8c36d0900            call      00547140h
  004b037d  99                    cdq
  004b037e  b90a000000            mov       ecx,0Ah
  004b0383  66c746105000          mov       word [esi+10h],50h
  004b0389  f7f9                  idiv      ecx
  004b038b  8bc7                  mov       eax,edi
  004b038d  d1f8                  sar       eax,1
  004b038f  02d0                  add       dl,al
  004b0391  80c214                add       dl,14h
  004b0394  885643                mov       [esi+43h],dl
  004b0397  eb63                  jmp       short 004B03FCh

LAB_004b0399:
  004b0399  83fb3c                cmp       ebx,3Ch
  004b039c  7d13                  jge       short 004B03B1h
  004b039e  c6464206              mov       byte [esi+42h],6
  004b03a2  c6464301              mov       byte [esi+43h],1
  004b03a6  e8956d0900            call      00547140h
  004b03ab  66894610              mov       [esi+10h],ax
  004b03af  eb47                  jmp       short 004B03F8h

LAB_004b03b1:
  004b03b1  83fb50                cmp       ebx,50h
  004b03b4  7d3a                  jge       short 004B03F0h
  004b03b6  c6464203              mov       byte [esi+42h],3
  004b03ba  66896e12              mov       [esi+12h],bp
  004b03be  e87d6d0900            call      00547140h
  004b03c3  99                    cdq
  004b03c4  b90a000000            mov       ecx,0Ah
  004b03c9  f7f9                  idiv      ecx
  004b03cb  8bc7                  mov       eax,edi
  004b03cd  d1f8                  sar       eax,1
  004b03cf  02d0                  add       dl,al
  004b03d1  80c214                add       dl,14h
  004b03d4  885643                mov       [esi+43h],dl
  004b03d7  e8646d0900            call      00547140h
  004b03dc  99                    cdq
  004b03dd  33c2                  xor       eax,edx
  004b03df  2bc2                  sub       eax,edx
  004b03e1  25ff0f0000            and       eax,0FFFh
  004b03e6  33c2                  xor       eax,edx
  004b03e8  2bc2                  sub       eax,edx
  004b03ea  66894610              mov       [esi+10h],ax
  004b03ee  eb0c                  jmp       short 004B03FCh

LAB_004b03f0:
  004b03f0  c646420a              mov       byte [esi+42h],0Ah

LAB_004b03f4:
  004b03f4  c6464301              mov       byte [esi+43h],1

LAB_004b03f8:
  004b03f8  66896e12              mov       [esi+12h],bp

LAB_004b03fc:
  004b03fc  817c241858020000      cmp       dword [esp+18h],258h
  004b0404  0f8c35010000          jl        near 004B053Fh
  004b040a  e8316d0900            call      00547140h
  004b040f  bd64000000            mov       ebp,64h
  004b0414  99                    cdq
  004b0415  8bcd                  mov       ecx,ebp
  004b0417  f7f9                  idiv      ecx
  004b0419  8bda                  mov       ebx,edx
  004b041b  83fb1e                cmp       ebx,1Eh
  004b041e  7d68                  jge       short 004B0488h
  004b0420  8b542414              mov       edx,[esp+14h]
  004b0424  8d44120a              lea       eax,[edx+edx+0Ah]
  004b0428  3bd8                  cmp       ebx,eax
  004b042a  7d28                  jge       short 004B0454h
  004b042c  c6464202              mov       byte [esi+42h],2
  004b0430  e80b6d0900            call      00547140h
  004b0435  99                    cdq
  004b0436  b90a000000            mov       ecx,0Ah
  004b043b  66896e10              mov       [esi+10h],bp
  004b043f  f7f9                  idiv      ecx
  004b0441  8b442410              mov       eax,[esp+10h]
  004b0445  d1f8                  sar       eax,1
  004b0447  02d0                  add       dl,al
  004b0449  80c214                add       dl,14h
  004b044c  885643                mov       [esi+43h],dl
  004b044f  e9eb000000            jmp       004B053Fh

LAB_004b0454:
  004b0454  c6464200              mov       byte [esi+42h],0
  004b0458  e8e36c0900            call      00547140h
  004b045d  99                    cdq
  004b045e  b90a000000            mov       ecx,0Ah
  004b0463  66c746120000          mov       word [esi+12h],0
  004b0469  f7f9                  idiv      ecx
  004b046b  8b442410              mov       eax,[esp+10h]
  004b046f  8b4c2414              mov       ecx,[esp+14h]
  004b0473  c1f802                sar       eax,2
  004b0476  02d0                  add       dl,al
  004b0478  885643                mov       [esi+43h],dl
  004b047b  8d540950              lea       edx,[ecx+ecx+50h]
  004b047f  66895610              mov       [esi+10h],dx
  004b0483  e9b7000000            jmp       004B053Fh

LAB_004b0488:
  004b0488  83fb28                cmp       ebx,28h
  004b048b  7d3a                  jge       short 004B04C7h
  004b048d  c6464203              mov       byte [esi+42h],3
  004b0491  66c746120000          mov       word [esi+12h],0
  004b0497  e8a46c0900            call      00547140h
  004b049c  99                    cdq
  004b049d  b90a000000            mov       ecx,0Ah
  004b04a2  f7f9                  idiv      ecx
  004b04a4  8b442410              mov       eax,[esp+10h]
  004b04a8  d1f8                  sar       eax,1
  004b04aa  02d0                  add       dl,al
  004b04ac  80c214                add       dl,14h
  004b04af  885643                mov       [esi+43h],dl
  004b04b2  e8896c0900            call      00547140h
  004b04b7  99                    cdq
  004b04b8  33c2                  xor       eax,edx
  004b04ba  2bc2                  sub       eax,edx
  004b04bc  25ff0f0000            and       eax,0FFFh
  004b04c1  33c2                  xor       eax,edx
  004b04c3  2bc2                  sub       eax,edx
  004b04c5  eb74                  jmp       short 004B053Bh

LAB_004b04c7:
  004b04c7  83fb3c                cmp       ebx,3Ch
  004b04ca  7d0a                  jge       short 004B04D6h
  004b04cc  c6464201              mov       byte [esi+42h],1
  004b04d0  c6464301              mov       byte [esi+43h],1
  004b04d4  eb69                  jmp       short 004B053Fh

LAB_004b04d6:
  004b04d6  83fb50                cmp       ebx,50h
  004b04d9  7d53                  jge       short 004B052Eh
  004b04db  8b7c2410              mov       edi,[esp+10h]
  004b04df  8b4c241c              mov       ecx,[esp+1Ch]
  004b04e3  8d94b900010000        lea       edx,[ecx+edi*4+100h]
  004b04ea  8d04fd00020000        lea       eax,[edi*8+200h]
  004b04f1  81e2ff0f0000          and       edx,0FFFh
  004b04f7  3bd0                  cmp       edx,eax
  004b04f9  7f10                  jg        short 004B050Bh
  004b04fb  c646420a              mov       byte [esi+42h],0Ah
  004b04ff  c6464301              mov       byte [esi+43h],1
  004b0503  66c746120000          mov       word [esi+12h],0
  004b0509  eb34                  jmp       short 004B053Fh

LAB_004b050b:
  004b050b  c6464202              mov       byte [esi+42h],2
  004b050f  e82c6c0900            call      00547140h
  004b0514  99                    cdq
  004b0515  b90a000000            mov       ecx,0Ah
  004b051a  66896e10              mov       [esi+10h],bp
  004b051e  f7f9                  idiv      ecx
  004b0520  8bc7                  mov       eax,edi
  004b0522  d1f8                  sar       eax,1
  004b0524  02d0                  add       dl,al
  004b0526  80c214                add       dl,14h
  004b0529  885643                mov       [esi+43h],dl
  004b052c  eb11                  jmp       short 004B053Fh

LAB_004b052e:
  004b052e  c6464206              mov       byte [esi+42h],6
  004b0532  c6464301              mov       byte [esi+43h],1
  004b0536  e8056c0900            call      00547140h

LAB_004b053b:
  004b053b  66894610              mov       [esi+10h],ax

LAB_004b053f:
  004b053f  8b442410              mov       eax,[esp+10h]
  004b0543  8b4c241c              mov       ecx,[esp+1Ch]
  004b0547  8d948100020000        lea       edx,[ecx+eax*4+200h]
  004b054e  8d0cc500040000        lea       ecx,[eax*8+400h]
  004b0555  81e2ff0f0000          and       edx,0FFFh
  004b055b  3bd1                  cmp       edx,ecx
  004b055d  7f5a                  jg        short 004B05B9h
  004b055f  8b542450              mov       edx,[esp+50h]
  004b0563  f60202                test      byte [edx],2
  004b0566  7451                  je        short 004B05B9h
  004b0568  3bd8                  cmp       ebx,eax
  004b056a  7d4d                  jge       short 004B05B9h
  004b056c  f60601                test      byte [esi],1
  004b056f  7548                  jne       short 004B05B9h
  004b0571  807e4203              cmp       byte [esi+42h],3
  004b0575  7742                  ja        short 004B05B9h
  004b0577  817c2418c8000000      cmp       dword [esp+18h],0C8h
  004b057f  7d38                  jge       short 004B05B9h
  004b0581  c6464205              mov       byte [esi+42h],5
  004b0585  e8b66b0900            call      00547140h
  004b058a  99                    cdq
  004b058b  b905000000            mov       ecx,5
  004b0590  66c746180600          mov       word [esi+18h],6
  004b0596  f7f9                  idiv      ecx
  004b0598  8b06                  mov       eax,[esi]
  004b059a  66c7461a0700          mov       word [esi+1Ah],7
  004b05a0  c7465c00000000        mov       dword [esi+5Ch],0
  004b05a7  66c746120100          mov       word [esi+12h],1
  004b05ad  02d1                  add       dl,cl
  004b05af  25fffffffe            and       eax,0FEFFFFFFh
  004b05b4  885643                mov       [esi+43h],dl
  004b05b7  8906                  mov       [esi],eax

LAB_004b05b9:
  004b05b9  5f                    pop       edi
  004b05ba  5e                    pop       esi
  004b05bb  5d                    pop       ebp
  004b05bc  5b                    pop       ebx
  004b05bd  81c494000000          add       esp,94h
  004b05c3  c3                    ret
  004b05c4  dff9                  (bad)
  (octet invalide - la plage demandee deborde probablement du code)
