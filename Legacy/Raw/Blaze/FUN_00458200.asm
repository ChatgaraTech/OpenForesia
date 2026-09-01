Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00458200  section .text  offset fichier 0x57600  2088 octets

  00458200  83ec2c                sub       esp,2Ch
  00458203  53                    push      ebx
  00458204  55                    push      ebp
  00458205  56                    push      esi
  00458206  8b74243c              mov       esi,[esp+3Ch]
  0045820a  33ed                  xor       ebp,ebp
  0045820c  57                    push      edi
  0045820d  817e2400c02501        cmp       dword [esi+24h],125C000h
  00458214  7e33                  jle       short 00458249h
  00458216  bfc0935e00            mov       edi,5E93C0h
  0045821b  bb04000000            mov       ebx,4

LAB_00458220:
  00458220  8b07                  mov       eax,[edi]
  00458222  a900000080            test      eax,80000000h
  00458227  7417                  je        short 00458240h
  00458229  a900000050            test      eax,50000000h
  0045822e  7510                  jne       short 00458240h
  00458230  a1cc975e00            mov       eax,[5E97CCh]
  00458235  55                    push      ebp
  00458236  57                    push      edi
  00458237  50                    push      eax
  00458238  e893bb0b00            call      00513DD0h
  0045823d  83c40c                add       esp,0Ch

LAB_00458240:
  00458240  81c79c000000          add       edi,9Ch
  00458246  4b                    dec       ebx
  00458247  75d7                  jne       short 00458220h

LAB_00458249:
  00458249  66396e10              cmp       [esi+10h],bp
  0045824d  bf00a00000            mov       edi,0A000h
  00458252  0f855b010000          jne       near 004583B3h
  00458258  a168e05d00            mov       eax,[5DE068h]
  0045825d  8b4e1c                mov       ecx,[esi+1Ch]
  00458260  8b5620                mov       edx,[esi+20h]
  00458263  8b5e24                mov       ebx,[esi+24h]
  00458266  c1f80c                sar       eax,0Ch
  00458269  c1f90c                sar       ecx,0Ch
  0045826c  2bc1                  sub       eax,ecx
  0045826e  8b0d6ce05d00          mov       ecx,[5DE06Ch]
  00458274  c1f90c                sar       ecx,0Ch
  00458277  c1fa0c                sar       edx,0Ch
  0045827a  2bca                  sub       ecx,edx
  0045827c  8b1570e05d00          mov       edx,[5DE070h]
  00458282  c1fa0c                sar       edx,0Ch
  00458285  c1fb0c                sar       ebx,0Ch
  00458288  2bd3                  sub       edx,ebx
  0045828a  8bd8                  mov       ebx,eax
  0045828c  0fafd8                imul      ebx,eax
  0045828f  8bc1                  mov       eax,ecx
  00458291  0fafc1                imul      eax,ecx
  00458294  8bca                  mov       ecx,edx
  00458296  03d8                  add       ebx,eax
  00458298  0fafca                imul      ecx,edx
  0045829b  03d9                  add       ebx,ecx
  0045829d  53                    push      ebx
  0045829e  e86dd70e00            call      00545A10h
  004582a3  83f864                cmp       eax,64h
  004582a6  7d1a                  jge       short 004582C2h
  004582a8  8b1568e05d00          mov       edx,[5DE068h]
  004582ae  89561c                mov       [esi+1Ch],edx
  004582b1  a16ce05d00            mov       eax,[5DE06Ch]
  004582b6  894620                mov       [esi+20h],eax
  004582b9  8b0d70e05d00          mov       ecx,[5DE070h]
  004582bf  894e24                mov       [esi+24h],ecx

LAB_004582c2:
  004582c2  8b4620                mov       eax,[esi+20h]
  004582c5  6689be82000000        mov       [esi+82h],di
  004582cc  6689be86000000        mov       [esi+86h],di
  004582d3  6689be8a000000        mov       [esi+8Ah],di
  004582da  6689be8e000000        mov       [esi+8Eh],di
  004582e1  894664                mov       [esi+64h],eax
  004582e4  894660                mov       [esi+60h],eax
  004582e7  8b15c07b5f00          mov       edx,[5F7BC0h]
  004582ed  6800800000            push      8000h
  004582f2  56                    push      esi
  004582f3  52                    push      edx
  004582f4  e8d7ad0d00            call      005330D0h
  004582f9  83c40c                add       esp,0Ch
  004582fc  66396e10              cmp       [esi+10h],bp
  00458300  0f85ad000000          jne       near 004583B3h
  00458306  f60500975e0001        test      byte [5E9700h],1
  0045830d  747a                  je        short 00458389h
  0045830f  a168e05d00            mov       eax,[5DE068h]
  00458314  8b0d70e05d00          mov       ecx,[5DE070h]
  0045831a  c1f80c                sar       eax,0Ch
  0045831d  c1f90c                sar       ecx,0Ch
  00458320  83f89c                cmp       eax,0FFFFFF9Ch
  00458323  7e64                  jle       short 00458389h
  00458325  3d2c010000            cmp       eax,12Ch
  0045832a  7d5d                  jge       short 00458389h
  0045832c  81f9e8030000          cmp       ecx,3E8h
  00458332  7e55                  jle       short 00458389h
  00458334  81f914050000          cmp       ecx,514h
  0045833a  7d4d                  jge       short 00458389h
  0045833c  8b4620                mov       eax,[esi+20h]
  0045833f  c7461c00a0f6ff        mov       dword [esi+1Ch],0FFF6A000h
  00458346  c7462400009600        mov       dword [esi+24h],960000h
  0045834d  66c746320004          mov       word [esi+32h],400h
  00458353  6689be82000000        mov       [esi+82h],di
  0045835a  6689be86000000        mov       [esi+86h],di
  00458361  6689be8a000000        mov       [esi+8Ah],di
  00458368  6689be8e000000        mov       [esi+8Eh],di
  0045836f  894664                mov       [esi+64h],eax
  00458372  894660                mov       [esi+60h],eax
  00458375  a1c07b5f00            mov       eax,[5F7BC0h]
  0045837a  6800800000            push      8000h
  0045837f  56                    push      esi
  00458380  50                    push      eax
  00458381  e84aad0d00            call      005330D0h
  00458386  83c40c                add       esp,0Ch

LAB_00458389:
  00458389  66396e10              cmp       [esi+10h],bp
  0045838d  7524                  jne       short 004583B3h
  0045838f  0fbf4614              movsx     eax,word [esi+14h]
  00458393  0fbf4e16              movsx     ecx,word [esi+16h]
  00458397  c1e008                shl       eax,8
  0045839a  0bc1                  or        eax,ecx
  0045839c  6689ae9a000000        mov       [esi+9Ah],bp
  004583a3  66894610              mov       [esi+10h],ax
  004583a7  66896e12              mov       [esi+12h],bp
  004583ab  66896e14              mov       [esi+14h],bp
  004583af  66896e16              mov       [esi+16h],bp

LAB_004583b3:
  004583b3  33db                  xor       ebx,ebx
  004583b5  896c2410              mov       [esp+10h],ebp
  004583b9  895c2440              mov       [esp+40h],ebx
  004583bd  33d2                  xor       edx,edx
  004583bf  b9c0935e00            mov       ecx,5E93C0h
  004583c4  bf04000000            mov       edi,4
  004583c9  bd00200000            mov       ebp,2000h

LAB_004583ce:
  004583ce  8b01                  mov       eax,[ecx]
  004583d0  a900000080            test      eax,80000000h
  004583d5  7461                  je        short 00458438h
  004583d7  a900000050            test      eax,50000000h
  004583dc  755a                  jne       short 00458438h
  004583de  8b467c                mov       eax,[esi+7Ch]
  004583e1  33db                  xor       ebx,ebx
  004583e3  8a580e                mov       bl,[eax+0Eh]
  004583e6  03da                  add       ebx,edx
  004583e8  66852c9d42945e00      test      [ebx*4+5E9442h],bp
  004583f0  743e                  je        short 00458430h
  004583f2  8b4178                mov       eax,[ecx+78h]
  004583f5  85c0                  test      eax,eax
  004583f7  743b                  je        short 00458434h
  004583f9  80780514              cmp       byte [eax+5],14h
  004583fd  7535                  jne       short 00458434h
  004583ff  8b5c2440              mov       ebx,[esp+40h]
  00458403  66b800fc              mov       ax,0FC00h
  00458407  662b4632              sub       ax,[esi+32h]
  0045840b  43                    inc       ebx
  0045840c  895c2440              mov       [esp+40h],ebx
  00458410  66a3aa155700          mov       [5715AAh],ax
  00458416  668b4630              mov       ax,[esi+30h]
  0045841a  66050002              add       ax,200h
  0045841e  66a3a8155700          mov       [5715A8h],ax
  00458424  668b4634              mov       ax,[esi+34h]
  00458428  66a3ac155700          mov       [5715ACh],ax
  0045842e  eb08                  jmp       short 00458438h

LAB_00458430:
  00458430  ff442410              inc       dword [esp+10h]

LAB_00458434:
  00458434  8b5c2440              mov       ebx,[esp+40h]

LAB_00458438:
  00458438  83c227                add       edx,27h
  0045843b  81c19c000000          add       ecx,9Ch
  00458441  4f                    dec       edi
  00458442  758a                  jne       short 004583CEh
  00458444  e827e90000            call      00466D70h
  00458449  663bd8                cmp       bx,ax
  0045844c  755f                  jne       short 004584ADh
  0045844e  33ff                  xor       edi,edi
  00458450  6639be9a000000        cmp       [esi+9Ah],di
  00458457  7549                  jne       short 004584A2h
  00458459  8b0d4ce55d00          mov       ecx,[5DE54Ch]
  0045845f  a194ca5e00            mov       eax,[5ECA94h]
  00458464  890d7c4b6000          mov       [604B7Ch],ecx
  0045846a  c7054ce55d0003000000  mov       dword [5DE54Ch],3
  00458474  8b5008                mov       edx,[eax+8]
  00458477  891560165700          mov       [571660h],edx
  0045847d  8b08                  mov       ecx,[eax]
  0045847f  890d58165700          mov       [571658h],ecx
  00458485  8b5004                mov       edx,[eax+4]
  00458488  89155c165700          mov       [57165Ch],edx
  0045848e  c70594ca5e0058165700  mov       dword [5ECA94h],571658h
  00458498  c705e09a5e00a8155700  mov       dword [5E9AE0h],5715A8h

LAB_004584a2:
  004584a2  66c7869a0000000100    mov       word [esi+9Ah],1
  004584ab  eb02                  jmp       short 004584AFh

LAB_004584ad:
  004584ad  33ff                  xor       edi,edi

LAB_004584af:
  004584af  e8bce80000            call      00466D70h
  004584b4  8b6c2410              mov       ebp,[esp+10h]
  004584b8  663be8                cmp       bp,ax
  004584bb  755d                  jne       short 0045851Ah
  004584bd  6683be9a00000001      cmp       word [esi+9Ah],1
  004584c5  7544                  jne       short 0045850Bh
  004584c7  a1a8155700            mov       eax,[5715A8h]
  004584cc  668b0daa155700        mov       cx,[5715AAh]
  004584d3  8b15ac155700          mov       edx,[5715ACh]
  004584d9  25ff0f0000            and       eax,0FFFh
  004584de  81e1ff0f0000          and       ecx,0FFFh
  004584e4  81e2ff0f0000          and       edx,0FFFh
  004584ea  66a3e0676000          mov       [6067E0h],ax
  004584f0  66890de2676000        mov       [6067E2h],cx
  004584f7  668915e4676000        mov       [6067E4h],dx
  004584fe  66897e14              mov       [esi+14h],di
  00458502  66897e16              mov       [esi+16h],di
  00458506  e8a5b20a00            call      005037B0h

LAB_0045850b:
  0045850b  6689be9a000000        mov       [esi+9Ah],di
  00458512  5f                    pop       edi
  00458513  5e                    pop       esi
  00458514  5d                    pop       ebp
  00458515  5b                    pop       ebx
  00458516  83c42c                add       esp,2Ch
  00458519  c3                    ret

LAB_0045851a:
  0045851a  6639be9a000000        cmp       [esi+9Ah],di
  00458521  0f84e7040000          je        near 00458A0Eh
  00458527  663bef                cmp       bp,di
  0045852a  7515                  jne       short 00458541h
  0045852c  663bdf                cmp       bx,di
  0045852f  7510                  jne       short 00458541h
  00458531  66897e14              mov       [esi+14h],di
  00458535  66897e16              mov       [esi+16h],di
  00458539  5f                    pop       edi
  0045853a  5e                    pop       esi
  0045853b  5d                    pop       ebp
  0045853c  5b                    pop       ebx
  0045853d  83c42c                add       esp,2Ch
  00458540  c3                    ret

LAB_00458541:
  00458541  b800100000            mov       eax,1000h
  00458546  66894628              mov       [esi+28h],ax
  0045854a  6689462c              mov       [esi+2Ch],ax
  0045854e  e81de80000            call      00466D70h
  00458553  663bd8                cmp       bx,ax
  00458556  741a                  je        short 00458572h
  00458558  6683be9a00000001      cmp       word [esi+9Ah],1
  00458560  7510                  jne       short 00458572h
  00458562  66897e14              mov       [esi+14h],di
  00458566  66897e16              mov       [esi+16h],di
  0045856a  5f                    pop       edi
  0045856b  5e                    pop       esi
  0045856c  5d                    pop       ebp
  0045856d  5b                    pop       ebx
  0045856e  83c42c                add       esp,2Ch
  00458571  c3                    ret

LAB_00458572:
  00458572  33c9                  xor       ecx,ecx
  00458574  ba00000010            mov       edx,10000000h

LAB_00458579:
  00458579  0fbfc1                movsx     eax,cx
  0045857c  8d3c80                lea       edi,[eax+eax*4]
  0045857f  c1e703                shl       edi,3
  00458582  2bf8                  sub       edi,eax
  00458584  8514bdc0935e00        test      [edi*4+5E93C0h],edx
  0045858b  0f8585040000          jne       near 00458A16h
  00458591  41                    inc       ecx
  00458592  6683f903              cmp       cx,3
  00458596  7ee1                  jle       short 00458579h
  00458598  a160165700            mov       eax,[571660h]
  0045859d  b908070000            mov       ecx,708h
  004585a2  2bc8                  sub       ecx,eax
  004585a4  c1f903                sar       ecx,3
  004585a7  03c1                  add       eax,ecx
  004585a9  a360165700            mov       [571660h],eax
  004585ae  6683be9a00000001      cmp       word [esi+9Ah],1
  004585b6  755f                  jne       short 00458617h
  004585b8  8b9680000000          mov       edx,[esi+80h]
  004585be  8915707d5f00          mov       [5F7D70h],edx
  004585c4  8b8684000000          mov       eax,[esi+84h]
  004585ca  a3747d5f00            mov       [5F7D74h],eax
  004585cf  8b8e88000000          mov       ecx,[esi+88h]
  004585d5  890d787d5f00          mov       [5F7D78h],ecx
  004585db  8b968c000000          mov       edx,[esi+8Ch]
  004585e1  89157c7d5f00          mov       [5F7D7Ch],edx
  004585e7  8b461c                mov       eax,[esi+1Ch]
  004585ea  a3309a5e00            mov       [5E9A30h],eax
  004585ef  8b4e20                mov       ecx,[esi+20h]
  004585f2  890d349a5e00          mov       [5E9A34h],ecx
  004585f8  8b5624                mov       edx,[esi+24h]
  004585fb  8915389a5e00          mov       [5E9A38h],edx
  00458601  668b8690000000        mov       ax,[esi+90h]
  00458608  66a3389b5e00          mov       [5E9B38h],ax
  0045860e  8b4e7c                mov       ecx,[esi+7Ch]
  00458611  890d487b5f00          mov       [5F7B48h],ecx

LAB_00458617:
  00458617  668b4614              mov       ax,[esi+14h]
  0045861b  6685c0                test      ax,ax
  0045861e  7e05                  jle       short 00458625h
  00458620  48                    dec       eax
  00458621  66894614              mov       [esi+14h],ax

LAB_00458625:
  00458625  668b4614              mov       ax,[esi+14h]
  00458629  6685c0                test      ax,ax
  0045862c  7d05                  jge       short 00458633h
  0045862e  40                    inc       eax
  0045862f  66894614              mov       [esi+14h],ax

LAB_00458633:
  00458633  668b4616              mov       ax,[esi+16h]
  00458637  6685c0                test      ax,ax
  0045863a  7e05                  jle       short 00458641h
  0045863c  48                    dec       eax
  0045863d  66894616              mov       [esi+16h],ax

LAB_00458641:
  00458641  668b4616              mov       ax,[esi+16h]
  00458645  6685c0                test      ax,ax
  00458648  7d05                  jge       short 0045864Fh
  0045864a  40                    inc       eax
  0045864b  66894616              mov       [esi+16h],ax

LAB_0045864f:
  0045864f  8b561c                mov       edx,[esi+1Ch]
  00458652  8b4620                mov       eax,[esi+20h]
  00458655  8b4e24                mov       ecx,[esi+24h]
  00458658  8954242c              mov       [esp+2Ch],edx
  0045865c  668b5632              mov       dx,[esi+32h]
  00458660  89442430              mov       [esp+30h],eax
  00458664  8b8680000000          mov       eax,[esi+80h]
  0045866a  6689542410            mov       [esp+10h],dx
  0045866f  8b9688000000          mov       edx,[esi+88h]
  00458675  894c2434              mov       [esp+34h],ecx
  00458679  8b8e84000000          mov       ecx,[esi+84h]
  0045867f  89442414              mov       [esp+14h],eax
  00458683  8b868c000000          mov       eax,[esi+8Ch]
  00458689  8954241c              mov       [esp+1Ch],edx
  0045868d  668b9690000000        mov       dx,[esi+90h]
  00458694  894c2418              mov       [esp+18h],ecx
  00458698  8b4e7c                mov       ecx,[esi+7Ch]
  0045869b  89442420              mov       [esp+20h],eax
  0045869f  6689542428            mov       [esp+28h],dx
  004586a4  894c2424              mov       [esp+24h],ecx
  004586a8  33ff                  xor       edi,edi
  004586aa  b8c0935e00            mov       eax,5E93C0h
  004586af  ba04000000            mov       edx,4

LAB_004586b4:
  004586b4  8b08                  mov       ecx,[eax]
  004586b6  f7c100000080          test      ecx,80000000h
  004586bc  7411                  je        short 004586CFh
  004586be  f7c100000050          test      ecx,50000000h
  004586c4  7509                  jne       short 004586CFh
  004586c6  f7c100000100          test      ecx,10000h
  004586cc  7501                  jne       short 004586CFh
  004586ce  47                    inc       edi

LAB_004586cf:
  004586cf  059c000000            add       eax,9Ch
  004586d4  4a                    dec       edx
  004586d5  75dd                  jne       short 004586B4h
  004586d7  6683ff01              cmp       di,1
  004586db  7512                  jne       short 004586EFh
  004586dd  bd08000000            mov       ebp,8
  004586e2  bb04000000            mov       ebx,4
  004586e7  c744244023000000      mov       dword [esp+40h],23h

LAB_004586ef:
  004586ef  6683ff02              cmp       di,2
  004586f3  7512                  jne       short 00458707h
  004586f5  bd04000000            mov       ebp,4
  004586fa  bb02000000            mov       ebx,2
  004586ff  c74424402b000000      mov       dword [esp+40h],2Bh

LAB_00458707:
  00458707  6683ff03              cmp       di,3
  0045870b  7512                  jne       short 0045871Fh
  0045870d  bd03000000            mov       ebp,3
  00458712  bb02000000            mov       ebx,2
  00458717  c744244032000000      mov       dword [esp+40h],32h

LAB_0045871f:
  0045871f  6683ff04              cmp       di,4
  00458723  7512                  jne       short 00458737h
  00458725  bd02000000            mov       ebp,2
  0045872a  bb01000000            mov       ebx,1
  0045872f  c74424403c000000      mov       dword [esp+40h],3Ch

LAB_00458737:
  00458737  b9c8935e00            mov       ecx,5E93C8h
  0045873c  bf04000000            mov       edi,4

LAB_00458741:
  00458741  8b41f8                mov       eax,[ecx-8]
  00458744  a900000080            test      eax,80000000h
  00458749  0f8497000000          je        near 004587E6h
  0045874f  a900000050            test      eax,50000000h
  00458754  0f858c000000          jne       near 004587E6h
  0045875a  33c0                  xor       eax,eax
  0045875c  33d2                  xor       edx,edx
  0045875e  8a01                  mov       al,[ecx]
  00458760  6bc04c                imul      eax,4Ch
  00458763  8a9088915e00          mov       dl,[eax+5E9188h]
  00458769  c1e204                shl       edx,4
  0045876c  f682234a600004        test      byte [edx+604A23h],4
  00458773  7408                  je        short 0045877Dh
  00458775  66296e14              sub       [esi+14h],bp
  00458779  66015e16              add       [esi+16h],bx

LAB_0045877d:
  0045877d  33c0                  xor       eax,eax
  0045877f  33d2                  xor       edx,edx
  00458781  8a01                  mov       al,[ecx]
  00458783  6bc04c                imul      eax,4Ch
  00458786  8a9088915e00          mov       dl,[eax+5E9188h]
  0045878c  c1e204                shl       edx,4
  0045878f  f682234a600008        test      byte [edx+604A23h],8
  00458796  7408                  je        short 004587A0h
  00458798  66016e14              add       [esi+14h],bp
  0045879c  66015e16              add       [esi+16h],bx

LAB_004587a0:
  004587a0  33c0                  xor       eax,eax
  004587a2  33d2                  xor       edx,edx
  004587a4  8a01                  mov       al,[ecx]
  004587a6  6bc04c                imul      eax,4Ch
  004587a9  8a9088915e00          mov       dl,[eax+5E9188h]
  004587af  c1e204                shl       edx,4
  004587b2  f682234a600001        test      byte [edx+604A23h],1
  004587b9  7408                  je        short 004587C3h
  004587bb  66016e14              add       [esi+14h],bp
  004587bf  66295e16              sub       [esi+16h],bx

LAB_004587c3:
  004587c3  33c0                  xor       eax,eax
  004587c5  33d2                  xor       edx,edx
  004587c7  8a01                  mov       al,[ecx]
  004587c9  6bc04c                imul      eax,4Ch
  004587cc  8a9088915e00          mov       dl,[eax+5E9188h]
  004587d2  c1e204                shl       edx,4
  004587d5  f682234a600002        test      byte [edx+604A23h],2
  004587dc  7408                  je        short 004587E6h
  004587de  66296e14              sub       [esi+14h],bp
  004587e2  66295e16              sub       [esi+16h],bx

LAB_004587e6:
  004587e6  81c19c000000          add       ecx,9Ch
  004587ec  4f                    dec       edi
  004587ed  0f854effffff          jne       near 00458741h
  004587f3  b9c0935e00            mov       ecx,5E93C0h
  004587f8  ba04000000            mov       edx,4
  004587fd  33ff                  xor       edi,edi

LAB_004587ff:
  004587ff  8b01                  mov       eax,[ecx]
  00458801  a900000080            test      eax,80000000h
  00458806  7413                  je        short 0045881Bh
  00458808  a900000040            test      eax,40000000h
  0045880d  740c                  je        short 0045881Bh
  0045880f  a880                  test      al,80h
  00458811  7408                  je        short 0045881Bh
  00458813  66897e14              mov       [esi+14h],di
  00458817  66897e16              mov       [esi+16h],di

LAB_0045881b:
  0045881b  81c19c000000          add       ecx,9Ch
  00458821  4a                    dec       edx
  00458822  75db                  jne       short 004587FFh
  00458824  8b4c2440              mov       ecx,[esp+40h]
  00458828  66394e16              cmp       [esi+16h],cx
  0045882c  7e04                  jle       short 00458832h
  0045882e  66894e16              mov       [esi+16h],cx

LAB_00458832:
  00458832  0fbf5616              movsx     edx,word [esi+16h]
  00458836  0fbfc1                movsx     eax,cx
  00458839  f7d8                  neg       eax
  0045883b  3bd0                  cmp       edx,eax
  0045883d  7d08                  jge       short 00458847h
  0045883f  8bd1                  mov       edx,ecx
  00458841  f7da                  neg       edx
  00458843  66895616              mov       [esi+16h],dx

LAB_00458847:
  00458847  66394e14              cmp       [esi+14h],cx
  0045884b  7e04                  jle       short 00458851h
  0045884d  66894e14              mov       [esi+14h],cx

LAB_00458851:
  00458851  0fbf5614              movsx     edx,word [esi+14h]
  00458855  3bd0                  cmp       edx,eax
  00458857  7d06                  jge       short 0045885Fh
  00458859  f7d9                  neg       ecx
  0045885b  66894e14              mov       [esi+14h],cx

LAB_0045885f:
  0045885f  83cfff                or        edi,0FFFFFFFFh
  00458862  66837e1600            cmp       word [esi+16h],0
  00458867  7507                  jne       short 00458870h
  00458869  66837e1400            cmp       word [esi+14h],0
  0045886e  743f                  je        short 004588AFh

LAB_00458870:
  00458870  bb02000000            mov       ebx,2

LAB_00458875:
  00458875  6a00                  push      0
  00458877  6a00                  push      0
  00458879  57                    push      edi
  0045887a  6a00                  push      0
  0045887c  6a30                  push      30h
  0045887e  56                    push      esi
  0045887f  e88cf80000            call      00468110h
  00458884  83c418                add       esp,18h
  00458887  e8b4e80e00            call      00547140h
  0045888c  99                    cdq
  0045888d  b906000000            mov       ecx,6
  00458892  f7f9                  idiv      ecx
  00458894  6683fa01              cmp       dx,1
  00458898  7512                  jne       short 004588ACh
  0045889a  6a00                  push      0
  0045889c  6a00                  push      0
  0045889e  57                    push      edi
  0045889f  6a00                  push      0
  004588a1  6a30                  push      30h
  004588a3  56                    push      esi
  004588a4  e867f80000            call      00468110h
  004588a9  83c418                add       esp,18h

LAB_004588ac:
  004588ac  4b                    dec       ebx
  004588ad  75c6                  jne       short 00458875h

LAB_004588af:
  004588af  668b5614              mov       dx,[esi+14h]
  004588b3  66015632              add       [esi+32h],dx
  004588b7  0fbf4632              movsx     eax,word [esi+32h]
  004588bb  0500040000            add       eax,400h
  004588c0  50                    push      eax
  004588c1  e856d10e00            call      00545A1Ch
  004588c6  0fbf4e16              movsx     ecx,word [esi+16h]
  004588ca  0fbf5632              movsx     edx,word [esi+32h]
  004588ce  0fafc1                imul      eax,ecx
  004588d1  8b4e1c                mov       ecx,[esi+1Ch]
  004588d4  81c200040000          add       edx,400h
  004588da  03c8                  add       ecx,eax
  004588dc  52                    push      edx
  004588dd  894e1c                mov       [esi+1Ch],ecx
  004588e0  e831d10e00            call      00545A16h
  004588e5  0fbf4e16              movsx     ecx,word [esi+16h]
  004588e9  0fafc1                imul      eax,ecx
  004588ec  8b5624                mov       edx,[esi+24h]
  004588ef  56                    push      esi
  004588f0  03d0                  add       edx,eax
  004588f2  895624                mov       [esi+24h],edx
  004588f5  e846b00d00            call      00533940h
  004588fa  83c404                add       esp,4
  004588fd  56                    push      esi
  004588fe  e82d010000            call      00458A30h
  00458903  33c9                  xor       ecx,ecx
  00458905  83c404                add       esp,4
  00458908  663bc1                cmp       ax,cx
  0045890b  7467                  je        short 00458974h
  0045890d  8b54242c              mov       edx,[esp+2Ch]
  00458911  8b442430              mov       eax,[esp+30h]
  00458915  89561c                mov       [esi+1Ch],edx
  00458918  8b542434              mov       edx,[esp+34h]
  0045891c  895624                mov       [esi+24h],edx
  0045891f  668b542410            mov       dx,[esp+10h]
  00458924  66895632              mov       [esi+32h],dx
  00458928  8b542414              mov       edx,[esp+14h]
  0045892c  899680000000          mov       [esi+80h],edx
  00458932  8b542418              mov       edx,[esp+18h]
  00458936  899684000000          mov       [esi+84h],edx
  0045893c  8b54241c              mov       edx,[esp+1Ch]
  00458940  899688000000          mov       [esi+88h],edx
  00458946  8b542420              mov       edx,[esp+20h]
  0045894a  89968c000000          mov       [esi+8Ch],edx
  00458950  8b542424              mov       edx,[esp+24h]
  00458954  89567c                mov       [esi+7Ch],edx
  00458957  8b542428              mov       edx,[esp+28h]
  0045895b  81e2ffff0000          and       edx,0FFFFh
  00458961  894620                mov       [esi+20h],eax
  00458964  899690000000          mov       [esi+90h],edx
  0045896a  66894e14              mov       [esi+14h],cx
  0045896e  66894e16              mov       [esi+16h],cx
  00458972  eb03                  jmp       short 00458977h

LAB_00458974:
  00458974  8b4620                mov       eax,[esi+20h]

LAB_00458977:
  00458977  56                    push      esi
  00458978  894664                mov       [esi+64h],eax
  0045897b  894660                mov       [esi+60h],eax
  0045897e  e8bdaf0d00            call      00533940h
  00458983  0fbf4e10              movsx     ecx,word [esi+10h]
  00458987  8b15cc975e00          mov       edx,[5E97CCh]
  0045898d  8bc1                  mov       eax,ecx
  0045898f  c1f808                sar       eax,8
  00458992  8b5230                mov       edx,[edx+30h]
  00458995  81e1ff000000          and       ecx,0FFh
  0045899b  c1e105                shl       ecx,5
  0045899e  8b0482                mov       eax,[edx+eax*4]
  004589a1  83c404                add       esp,4
  004589a4  03c1                  add       eax,ecx
  004589a6  8b4e1c                mov       ecx,[esi+1Ch]
  004589a9  c1f90c                sar       ecx,0Ch
  004589ac  66894804              mov       [eax+4],cx
  004589b0  8b5620                mov       edx,[esi+20h]
  004589b3  c1fa0c                sar       edx,0Ch
  004589b6  66895006              mov       [eax+6],dx
  004589ba  8b4e24                mov       ecx,[esi+24h]
  004589bd  c1f90c                sar       ecx,0Ch
  004589c0  66894808              mov       [eax+8],cx
  004589c4  668b5632              mov       dx,[esi+32h]
  004589c8  6689500c              mov       [eax+0Ch],dx
  004589cc  8b4e1c                mov       ecx,[esi+1Ch]
  004589cf  c1f90c                sar       ecx,0Ch
  004589d2  66890d50165700        mov       [571650h],cx
  004589d9  8b5620                mov       edx,[esi+20h]
  004589dc  c1fa0c                sar       edx,0Ch
  004589df  66891552165700        mov       [571652h],dx
  004589e6  8b4e24                mov       ecx,[esi+24h]
  004589e9  c1f90c                sar       ecx,0Ch
  004589ec  66890d54165700        mov       [571654h],cx
  004589f3  668b5632              mov       dx,[esi+32h]
  004589f7  66891580155700        mov       [571580h],dx
  004589fe  66897810              mov       [eax+10h],di
  00458a02  66897812              mov       [eax+12h],di
  00458a06  66897814              mov       [eax+14h],di
  00458a0a  66897816              mov       [eax+16h],di

LAB_00458a0e:
  00458a0e  5f                    pop       edi
  00458a0f  5e                    pop       esi
  00458a10  5d                    pop       ebp
  00458a11  5b                    pop       ebx
  00458a12  83c42c                add       esp,2Ch
  00458a15  c3                    ret

LAB_00458a16:
  00458a16  33c0                  xor       eax,eax
  00458a18  5f                    pop       edi
  00458a19  66894614              mov       [esi+14h],ax
  00458a1d  66894616              mov       [esi+16h],ax
  00458a21  5e                    pop       esi
  00458a22  5d                    pop       ebp
  00458a23  5b                    pop       ebx
  00458a24  83c42c                add       esp,2Ch
  00458a27  c3                    ret

48 cible(s) de saut interne : 0x00458220, 0x00458240, 0x00458249, 0x004582c2, 0x00458389, 0x004583b3, 0x004583ce, 0x00458430, 0x00458434, 0x00458438, 0x004584a2, 0x004584ad, 0x004584af, 0x0045850b, 0x0045851a, 0x00458541, 0x00458572, 0x00458579, 0x00458617, 0x00458625, 0x00458633, 0x00458641, 0x0045864f, 0x004586b4, 0x004586cf, 0x004586ef, 0x00458707, 0x0045871f, 0x00458737, 0x00458741, 0x0045877d, 0x004587a0, 0x004587c3, 0x004587e6, 0x004587ff, 0x0045881b, 0x00458832, 0x00458847, 0x00458851, 0x0045885f, 0x00458870, 0x00458875, 0x004588ac, 0x004588af, 0x00458974, 0x00458977, 0x00458a0e, 0x00458a16
