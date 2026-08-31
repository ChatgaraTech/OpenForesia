
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047ca50  section .text  offset fichier 0x7be50  1007 octets

  0047ca50  83ec30                sub       esp,30h
  0047ca53  53                    push      ebx
  0047ca54  55                    push      ebp
  0047ca55  56                    push      esi
  0047ca56  8b742440              mov       esi,[esp+40h]
  0047ca5a  57                    push      edi
  0047ca5b  33ff                  xor       edi,edi
  0047ca5d  66397e10              cmp       [esi+10h],di
  0047ca61  66c744242800fc        mov       word [esp+28h],0FC00h
  0047ca68  66c744242a80ff        mov       word [esp+2Ah],0FF80h
  0047ca6f  66c744242c3200        mov       word [esp+2Ch],32h
  0047ca76  66c744242ec800        mov       word [esp+2Eh],0C8h
  0047ca7d  66897c2430            mov       [esp+30h],di
  0047ca82  66c74424328000        mov       word [esp+32h],80h
  0047ca89  7524                  jne       short 0047CAAFh
  0047ca8b  8b06                  mov       eax,[esi]
  0047ca8d  66c746100100          mov       word [esi+10h],1
  0047ca93  0c80                  or        al,80h
  0047ca95  8906                  mov       [esi],eax
  0047ca97  8d461c                lea       eax,[esi+1Ch]
  0047ca9a  50                    push      eax
  0047ca9b  6800120000            push      1200h
  0047caa0  6800040000            push      400h
  0047caa5  6a12                  push      12h
  0047caa7  e874100c00            call      0053DB20h
  0047caac  83c410                add       esp,10h

LAB_0047caaf:
  0047caaf  8b4624                mov       eax,[esi+24h]
  0047cab2  804e3606              or        byte [esi+36h],6
  0047cab6  8b4e1c                mov       ecx,[esi+1Ch]
  0047cab9  8b5620                mov       edx,[esi+20h]
  0047cabc  8d5e1c                lea       ebx,[esi+1Ch]
  0047cabf  89442424              mov       [esp+24h],eax
  0047cac3  8b4678                mov       eax,[esi+78h]
  0047cac6  894c241c              mov       [esp+1Ch],ecx
  0047caca  3bc7                  cmp       eax,edi
  0047cacc  89542420              mov       [esp+20h],edx
  0047cad0  89442418              mov       [esp+18h],eax
  0047cad4  0f848d000000          je        near 0047CB67h
  0047cada  f70000000050          test      dword [eax],50000000h
  0047cae0  7417                  je        short 0047CAF9h
  0047cae2  8b6c2444              mov       ebp,[esp+44h]
  0047cae6  897874                mov       [eax+74h],edi
  0047cae9  8b442444              mov       eax,[esp+44h]
  0047caed  897e78                mov       [esi+78h],edi
  0047caf0  897c2418              mov       [esp+18h],edi
  0047caf4  e991000000            jmp       0047CB8Ah

LAB_0047caf9:
  0047caf9  8b681c                mov       ebp,[eax+1Ch]
  0047cafc  897074                mov       [eax+74h],esi
  0047caff  8b0b                  mov       ecx,[ebx]
  0047cb01  50                    push      eax
  0047cb02  2be9                  sub       ebp,ecx
  0047cb04  c1fd0c                sar       ebp,0Ch
  0047cb07  e8b4c00b00            call      00538BC0h
  0047cb0c  8b5620                mov       edx,[esi+20h]
  0047cb0f  8bf8                  mov       edi,eax
  0047cb11  8b44241c              mov       eax,[esp+1Ch]
  0047cb15  83e7fe                and       edi,0FFFFFFFEh
  0047cb18  c1e70b                shl       edi,0Bh
  0047cb1b  8b4820                mov       ecx,[eax+20h]
  0047cb1e  8b4024                mov       eax,[eax+24h]
  0047cb21  03f9                  add       edi,ecx
  0047cb23  8bcd                  mov       ecx,ebp
  0047cb25  2bfa                  sub       edi,edx
  0047cb27  8b5624                mov       edx,[esi+24h]
  0047cb2a  2bc2                  sub       eax,edx
  0047cb2c  0fafcd                imul      ecx,ebp
  0047cb2f  c1f80c                sar       eax,0Ch
  0047cb32  8bd0                  mov       edx,eax
  0047cb34  83c404                add       esp,4
  0047cb37  0fafd0                imul      edx,eax
  0047cb3a  03ca                  add       ecx,edx
  0047cb3c  89442444              mov       [esp+44h],eax
  0047cb40  51                    push      ecx
  0047cb41  c1ff0c                sar       edi,0Ch
  0047cb44  e8c78e0c00            call      00545A10h
  0047cb49  89442410              mov       [esp+10h],eax
  0047cb4d  8b442444              mov       eax,[esp+44h]
  0047cb51  50                    push      eax
  0047cb52  55                    push      ebp
  0047cb53  e8ca8e0c00            call      00545A22h
  0047cb58  8b4c2410              mov       ecx,[esp+10h]
  0047cb5c  8be8                  mov       ebp,eax
  0047cb5e  51                    push      ecx
  0047cb5f  57                    push      edi
  0047cb60  e8bd8e0c00            call      00545A22h
  0047cb65  eb23                  jmp       short 0047CB8Ah

LAB_0047cb67:
  0047cb67  66ff4610              inc       word [esi+10h]
  0047cb6b  66837e100c            cmp       word [esi+10h],0Ch
  0047cb70  7c0a                  jl        short 0047CB7Ch
  0047cb72  893e                  mov       [esi],edi
  0047cb74  5f                    pop       edi
  0047cb75  5e                    pop       esi
  0047cb76  5d                    pop       ebp
  0047cb77  5b                    pop       ebx
  0047cb78  83c430                add       esp,30h
  0047cb7b  c3                    ret

LAB_0047cb7c:
  0047cb7c  0fbf6e32              movsx     ebp,word [esi+32h]
  0047cb80  0fbf4634              movsx     eax,word [esi+34h]
  0047cb84  81ed00040000          sub       ebp,400h

LAB_0047cb8a:
  0047cb8a  6a10                  push      10h
  0047cb8c  50                    push      eax
  0047cb8d  8d54243c              lea       edx,[esp+3Ch]
  0047cb91  55                    push      ebp
  0047cb92  52                    push      edx
  0047cb93  e8385f0b00            call      00532AD0h
  0047cb98  8b4650                mov       eax,[esi+50h]
  0047cb9b  8b4c2444              mov       ecx,[esp+44h]
  0047cb9f  8b5654                mov       edx,[esi+54h]
  0047cba2  8d7e50                lea       edi,[esi+50h]
  0047cba5  8b6c244c              mov       ebp,[esp+4Ch]
  0047cba9  03c1                  add       eax,ecx
  0047cbab  8b4c2448              mov       ecx,[esp+48h]
  0047cbaf  83c410                add       esp,10h
  0047cbb2  03ca                  add       ecx,edx
  0047cbb4  8b5658                mov       edx,[esi+58h]
  0047cbb7  03d5                  add       edx,ebp
  0047cbb9  8be8                  mov       ebp,eax
  0047cbbb  c1fd03                sar       ebp,3
  0047cbbe  2bc5                  sub       eax,ebp
  0047cbc0  8be9                  mov       ebp,ecx
  0047cbc2  c1fd03                sar       ebp,3
  0047cbc5  2bcd                  sub       ecx,ebp
  0047cbc7  8bea                  mov       ebp,edx
  0047cbc9  c1fd03                sar       ebp,3
  0047cbcc  2bd5                  sub       edx,ebp
  0047cbce  8b2b                  mov       ebp,[ebx]
  0047cbd0  03e8                  add       ebp,eax
  0047cbd2  8907                  mov       [edi],eax
  0047cbd4  892b                  mov       [ebx],ebp
  0047cbd6  8bc5                  mov       eax,ebp
  0047cbd8  8b6e20                mov       ebp,[esi+20h]
  0047cbdb  894e54                mov       [esi+54h],ecx
  0047cbde  03e9                  add       ebp,ecx
  0047cbe0  895658                mov       [esi+58h],edx
  0047cbe3  896e20                mov       [esi+20h],ebp
  0047cbe6  8bcd                  mov       ecx,ebp
  0047cbe8  8b6e24                mov       ebp,[esi+24h]
  0047cbeb  03ea                  add       ebp,edx
  0047cbed  896e24                mov       [esi+24h],ebp
  0047cbf0  8bd5                  mov       edx,ebp
  0047cbf2  2b44241c              sub       eax,[esp+1Ch]
  0047cbf6  c1f80c                sar       eax,0Ch
  0047cbf9  8be8                  mov       ebp,eax
  0047cbfb  8b442420              mov       eax,[esp+20h]
  0047cbff  2bc8                  sub       ecx,eax
  0047cc01  8bc5                  mov       eax,ebp
  0047cc03  c1f90c                sar       ecx,0Ch
  0047cc06  0fafc5                imul      eax,ebp
  0047cc09  894c2414              mov       [esp+14h],ecx
  0047cc0d  8b4c2424              mov       ecx,[esp+24h]
  0047cc11  2bd1                  sub       edx,ecx
  0047cc13  c1fa0c                sar       edx,0Ch
  0047cc16  8bca                  mov       ecx,edx
  0047cc18  89542444              mov       [esp+44h],edx
  0047cc1c  0fafca                imul      ecx,edx
  0047cc1f  03c1                  add       eax,ecx
  0047cc21  50                    push      eax
  0047cc22  e8e98d0c00            call      00545A10h
  0047cc27  8b542444              mov       edx,[esp+44h]
  0047cc2b  89442410              mov       [esp+10h],eax
  0047cc2f  52                    push      edx
  0047cc30  55                    push      ebp
  0047cc31  e8ec8d0c00            call      00545A22h
  0047cc36  8b4c2414              mov       ecx,[esp+14h]
  0047cc3a  0500040000            add       eax,400h
  0047cc3f  66894632              mov       [esi+32h],ax
  0047cc43  8b442410              mov       eax,[esp+10h]
  0047cc47  50                    push      eax
  0047cc48  51                    push      ecx
  0047cc49  e8d48d0c00            call      00545A22h
  0047cc4e  66837e0608            cmp       word [esi+6],8
  0047cc53  66894634              mov       [esi+34h],ax
  0047cc57  0f85d3000000          jne       near 0047CD30h
  0047cc5d  8b17                  mov       edx,[edi]
  0047cc5f  8b4654                mov       eax,[esi+54h]
  0047cc62  8b4e58                mov       ecx,[esi+58h]
  0047cc65  8954241c              mov       [esp+1Ch],edx
  0047cc69  8b5628                mov       edx,[esi+28h]
  0047cc6c  89442420              mov       [esp+20h],eax
  0047cc70  8b462c                mov       eax,[esi+2Ch]
  0047cc73  894c2424              mov       [esp+24h],ecx
  0047cc77  8b4e38                mov       ecx,[esi+38h]
  0047cc7a  89542410              mov       [esp+10h],edx
  0047cc7e  89442414              mov       [esp+14h],eax
  0047cc82  894c2444              mov       [esp+44h],ecx
  0047cc86  c7467000000000        mov       dword [esi+70h],0
  0047cc8d  e8aea40c00            call      00547140h
  0047cc92  0fbf6e32              movsx     ebp,word [esi+32h]
  0047cc96  25ff000000            and       eax,0FFh
  0047cc9b  2be8                  sub       ebp,eax
  0047cc9d  81ed80030000          sub       ebp,380h
  0047cca3  e898a40c00            call      00547140h
  0047cca8  8bd0                  mov       edx,eax
  0047ccaa  6a20                  push      20h
  0047ccac  0fbf4634              movsx     eax,word [esi+34h]
  0047ccb0  81e2ff000000          and       edx,0FFh
  0047ccb6  2bc2                  sub       eax,edx
  0047ccb8  0580080000            add       eax,880h
  0047ccbd  50                    push      eax
  0047ccbe  55                    push      ebp
  0047ccbf  57                    push      edi
  0047ccc0  e80b5e0b00            call      00532AD0h
  0047ccc5  8b6e54                mov       ebp,[esi+54h]
  0047ccc8  83c410                add       esp,10h
  0047cccb  33c0                  xor       eax,eax
  0047cccd  c74628000c000c        mov       dword [esi+28h],0C000C00h
  0047ccd4  8a460b                mov       al,[esi+0Bh]
  0047ccd7  6aff                  push      0FFFFFFFFh
  0047ccd9  50                    push      eax
  0047ccda  68bf000000            push      0BFh
  0047ccdf  68a81b5500            push      551BA8h
  0047cce4  53                    push      ebx
  0047cce5  c1fd03                sar       ebp,3
  0047cce8  56                    push      esi
  0047cce9  896e54                mov       [esi+54h],ebp
  0047ccec  66c7462c0300          mov       word [esi+2Ch],3
  0047ccf2  c7463880808000        mov       dword [esi+38h],808080h
  0047ccf9  66c7461a0100          mov       word [esi+1Ah],1
  0047ccff  e83cdfffff            call      0047AC40h
  0047cd04  8b4c2434              mov       ecx,[esp+34h]
  0047cd08  8b542438              mov       edx,[esp+38h]
  0047cd0c  8b44243c              mov       eax,[esp+3Ch]
  0047cd10  890f                  mov       [edi],ecx
  0047cd12  8b4c2428              mov       ecx,[esp+28h]
  0047cd16  895654                mov       [esi+54h],edx
  0047cd19  8b54242c              mov       edx,[esp+2Ch]
  0047cd1d  894658                mov       [esi+58h],eax
  0047cd20  8b44245c              mov       eax,[esp+5Ch]
  0047cd24  83c418                add       esp,18h
  0047cd27  894e28                mov       [esi+28h],ecx
  0047cd2a  89562c                mov       [esi+2Ch],edx
  0047cd2d  894638                mov       [esi+38h],eax

LAB_0047cd30:
  0047cd30  56                    push      esi
  0047cd31  e80a6c0b00            call      00533940h
  0047cd36  8b4660                mov       eax,[esi+60h]
  0047cd39  8b4e20                mov       ecx,[esi+20h]
  0047cd3c  83c404                add       esp,4
  0047cd3f  3bc8                  cmp       ecx,eax
  0047cd41  7f0d                  jg        short 0047CD50h
  0047cd43  894620                mov       [esi+20h],eax
  0047cd46  c7869000000000080000  mov       dword [esi+90h],800h

LAB_0047cd50:
  0047cd50  f70600000040          test      dword [esi],40000000h
  0047cd56  0f85ae000000          jne       near 0047CE0Ah
  0047cd5c  8b8690000000          mov       eax,[esi+90h]
  0047cd62  f6c4a0                test      ah,0A0h
  0047cd65  750b                  jne       short 0047CD72h
  0047cd67  a902080000            test      eax,802h
  0047cd6c  0f8598000000          jne       near 0047CE0Ah

LAB_0047cd72:
  0047cd72  8d4c2428              lea       ecx,[esp+28h]
  0047cd76  51                    push      ecx
  0047cd77  56                    push      esi
  0047cd78  e8237fffff            call      00474CA0h
  0047cd7d  8bf8                  mov       edi,eax
  0047cd7f  83c408                add       esp,8
  0047cd82  85ff                  test      edi,edi
  0047cd84  0f84ad000000          je        near 0047CE37h
  0047cd8a  8b4e74                mov       ecx,[esi+74h]
  0047cd8d  33d2                  xor       edx,edx
  0047cd8f  8a5606                mov       dl,[esi+6]
  0047cd92  33c0                  xor       eax,eax
  0047cd94  8a4607                mov       al,[esi+7]
  0047cd97  6a00                  push      0
  0047cd99  52                    push      edx
  0047cd9a  50                    push      eax
  0047cd9b  57                    push      edi
  0047cd9c  51                    push      ecx
  0047cd9d  e80ed50b00            call      0053A2B0h
  0047cda2  0fbf6e32              movsx     ebp,word [esi+32h]
  0047cda6  83c414                add       esp,14h
  0047cda9  81ed00040000          sub       ebp,400h
  0047cdaf  55                    push      ebp
  0047cdb0  e8678c0c00            call      00545A1Ch
  0047cdb5  8b4f50                mov       ecx,[edi+50h]
  0047cdb8  55                    push      ebp
  0047cdb9  c1e004                shl       eax,4
  0047cdbc  03c8                  add       ecx,eax
  0047cdbe  894f50                mov       [edi+50h],ecx
  0047cdc1  e8508c0c00            call      00545A16h
  0047cdc6  8b4f58                mov       ecx,[edi+58h]
  0047cdc9  53                    push      ebx
  0047cdca  c1e004                shl       eax,4
  0047cdcd  03c8                  add       ecx,eax
  0047cdcf  8b07                  mov       eax,[edi]
  0047cdd1  6800120000            push      1200h
  0047cdd6  0c40                  or        al,40h
  0047cdd8  6800080000            push      800h
  0047cddd  6a0b                  push      0Bh
  0047cddf  894f58                mov       [edi+58h],ecx
  0047cde2  66c787960000001000    mov       word [edi+96h],10h
  0047cdeb  8907                  mov       [edi],eax
  0047cded  c7477400000000        mov       dword [edi+74h],0
  0047cdf4  e8270d0c00            call      0053DB20h
  0047cdf9  83c410                add       esp,10h
  0047cdfc  c70600000000          mov       dword [esi],0
  0047ce02  5f                    pop       edi
  0047ce03  5e                    pop       esi
  0047ce04  5d                    pop       ebp
  0047ce05  5b                    pop       ebx
  0047ce06  83c430                add       esp,30h
  0047ce09  c3                    ret

LAB_0047ce0a:
  0047ce0a  6a04                  push      4
  0047ce0c  6a00                  push      0
  0047ce0e  68bd000000            push      0BDh
  0047ce13  68081a5500            push      551A08h
  0047ce18  53                    push      ebx
  0047ce19  56                    push      esi
  0047ce1a  e821deffff            call      0047AC40h
  0047ce1f  8b442430              mov       eax,[esp+30h]
  0047ce23  83c418                add       esp,18h
  0047ce26  85c0                  test      eax,eax
  0047ce28  7407                  je        short 0047CE31h
  0047ce2a  c7407400000000        mov       dword [eax+74h],0

LAB_0047ce31:
  0047ce31  c70600000000          mov       dword [esi],0

LAB_0047ce37:
  0047ce37  5f                    pop       edi
  0047ce38  5e                    pop       esi
  0047ce39  5d                    pop       ebp
  0047ce3a  5b                    pop       ebx
  0047ce3b  83c430                add       esp,30h
  0047ce3e  c3                    ret

11 cible(s) de saut interne : 0x0047caaf, 0x0047caf9, 0x0047cb67, 0x0047cb7c, 0x0047cb8a, 0x0047cd30, 0x0047cd50, 0x0047cd72, 0x0047ce0a, 0x0047ce31, 0x0047ce37
