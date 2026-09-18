Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004cb4f0  section .text  offset fichier 0xca8f0  4448 octets

  004cb4f0  81ec88000000          sub       esp,88h
  004cb4f6  53                    push      ebx
  004cb4f7  55                    push      ebp
  004cb4f8  56                    push      esi
  004cb4f9  8bb42498000000        mov       esi,[esp+98h]
  004cb500  33db                  xor       ebx,ebx
  004cb502  57                    push      edi
  004cb503  8a4641                mov       al,[esi+41h]
  004cb506  66895c2428            mov       [esp+28h],bx
  004cb50b  3ac3                  cmp       al,bl
  004cb50d  66c744242a1400        mov       word [esp+2Ah],14h
  004cb514  66c744242c5000        mov       word [esp+2Ch],50h
  004cb51b  66c744242e6400        mov       word [esp+2Eh],64h
  004cb522  66895c2430            mov       [esp+30h],bx
  004cb527  66c74424327800        mov       word [esp+32h],78h
  004cb52e  7531                  jne       short 004CB561h
  004cb530  53                    push      ebx
  004cb531  6a03                  push      3
  004cb533  56                    push      esi
  004cb534  c6464101              mov       byte [esi+41h],1
  004cb538  e843d8ffff            call      004C8D80h
  004cb53d  8b4620                mov       eax,[esi+20h]
  004cb540  83c40c                add       esp,0Ch
  004cb543  894648                mov       [esi+48h],eax
  004cb546  66837e160a            cmp       word [esi+16h],0Ah
  004cb54b  7c0e                  jl        short 004CB55Bh
  004cb54d  8b0e                  mov       ecx,[esi]
  004cb54f  83e1df                and       ecx,0FFFFFFDFh
  004cb552  890e                  mov       [esi],ecx
  004cb554  66834616f6            add       word [esi+16h],0FFF6h
  004cb559  eb06                  jmp       short 004CB561h

LAB_004cb55b:
  004cb55b  8b06                  mov       eax,[esi]
  004cb55d  0c20                  or        al,20h
  004cb55f  8906                  mov       [esi],eax

LAB_004cb561:
  004cb561  8a0d28e55d00          mov       cl,[5DE528h]
  004cb567  8b6e6c                mov       ebp,[esi+6Ch]
  004cb56a  80f906                cmp       cl,6
  004cb56d  896c241c              mov       [esp+1Ch],ebp
  004cb571  b800a00000            mov       eax,0A000h
  004cb576  753a                  jne       short 004CB5B2h
  004cb578  803d2ce55d0005        cmp       byte [5DE52Ch],5
  004cb57f  7531                  jne       short 004CB5B2h
  004cb581  66898682000000        mov       [esi+82h],ax
  004cb588  66898686000000        mov       [esi+86h],ax
  004cb58f  6689868a000000        mov       [esi+8Ah],ax
  004cb596  6689868e000000        mov       [esi+8Eh],ax
  004cb59d  8b0dc07b5f00          mov       ecx,[5F7BC0h]
  004cb5a3  6800800000            push      8000h
  004cb5a8  56                    push      esi
  004cb5a9  51                    push      ecx
  004cb5aa  e8217b0600            call      005330D0h
  004cb5af  83c40c                add       esp,0Ch

LAB_004cb5b2:
  004cb5b2  8b0e                  mov       ecx,[esi]
  004cb5b4  8b561c                mov       edx,[esi+1Ch]
  004cb5b7  8d7e1c                lea       edi,[esi+1Ch]
  004cb5ba  83e1fe                and       ecx,0FFFFFFFEh
  004cb5bd  890e                  mov       [esi],ecx
  004cb5bf  8b4620                mov       eax,[esi+20h]
  004cb5c2  8b4e24                mov       ecx,[esi+24h]
  004cb5c5  89542444              mov       [esp+44h],edx
  004cb5c9  8b5664                mov       edx,[esi+64h]
  004cb5cc  89442448              mov       [esp+48h],eax
  004cb5d0  8b4660                mov       eax,[esi+60h]
  004cb5d3  894c244c              mov       [esp+4Ch],ecx
  004cb5d7  668b4e32              mov       cx,[esi+32h]
  004cb5db  89542440              mov       [esp+40h],edx
  004cb5df  8b567c                mov       edx,[esi+7Ch]
  004cb5e2  89442438              mov       [esp+38h],eax
  004cb5e6  668b8690000000        mov       ax,[esi+90h]
  004cb5ed  66898c2486000000      mov       [esp+86h],cx
  004cb5f5  8b8e80000000          mov       ecx,[esi+80h]
  004cb5fb  89542460              mov       [esp+60h],edx
  004cb5ff  8b9684000000          mov       edx,[esi+84h]
  004cb605  668944243c            mov       [esp+3Ch],ax
  004cb60a  8b8688000000          mov       eax,[esi+88h]
  004cb610  894c2450              mov       [esp+50h],ecx
  004cb614  8b8e8c000000          mov       ecx,[esi+8Ch]
  004cb61a  89542434              mov       [esp+34h],edx
  004cb61e  8944245c              mov       [esp+5Ch],eax
  004cb622  8d542464              lea       edx,[esp+64h]
  004cb626  894c2454              mov       [esp+54h],ecx
  004cb62a  8d44246c              lea       eax,[esp+6Ch]
  004cb62e  52                    push      edx
  004cb62f  8d4c246c              lea       ecx,[esp+6Ch]
  004cb633  50                    push      eax
  004cb634  51                    push      ecx
  004cb635  56                    push      esi
  004cb636  e845d30600            call      00538980h
  004cb63b  83c410                add       esp,10h
  004cb63e  8d542458              lea       edx,[esp+58h]
  004cb642  8d442420              lea       eax,[esp+20h]
  004cb646  8d4c2418              lea       ecx,[esp+18h]
  004cb64a  52                    push      edx
  004cb64b  50                    push      eax
  004cb64c  8d54241c              lea       edx,[esp+1Ch]
  004cb650  51                    push      ecx
  004cb651  8d44241c              lea       eax,[esp+1Ch]
  004cb655  52                    push      edx
  004cb656  50                    push      eax
  004cb657  56                    push      esi
  004cb658  e8c3a80600            call      00535F20h
  004cb65d  8b4c2428              mov       ecx,[esp+28h]
  004cb661  b840000000            mov       eax,40h
  004cb666  83c418                add       esp,18h
  004cb669  2bc1                  sub       eax,ecx
  004cb66b  8b4c2458              mov       ecx,[esp+58h]
  004cb66f  89442424              mov       [esp+24h],eax
  004cb673  51                    push      ecx
  004cb674  56                    push      esi
  004cb675  e866a70000            call      004D5DE0h
  004cb67a  8b07                  mov       eax,[edi]
  004cb67c  8b551c                mov       edx,[ebp+1Ch]
  004cb67f  8b4e24                mov       ecx,[esi+24h]
  004cb682  2bc2                  sub       eax,edx
  004cb684  8b5524                mov       edx,[ebp+24h]
  004cb687  83c408                add       esp,8
  004cb68a  c1f80c                sar       eax,0Ch
  004cb68d  2bca                  sub       ecx,edx
  004cb68f  8bd0                  mov       edx,eax
  004cb691  c1f90c                sar       ecx,0Ch
  004cb694  0fafd0                imul      edx,eax
  004cb697  8bc1                  mov       eax,ecx
  004cb699  0fafc1                imul      eax,ecx
  004cb69c  03d0                  add       edx,eax
  004cb69e  52                    push      edx
  004cb69f  e86ca30700            call      00545A10h
  004cb6a4  8b4c2420              mov       ecx,[esp+20h]
  004cb6a8  89442414              mov       [esp+14h],eax
  004cb6ac  3bcb                  cmp       ecx,ebx
  004cb6ae  0f84fc090000          je        near 004CC0B0h
  004cb6b4  8b2e                  mov       ebp,[esi]
  004cb6b6  33db                  xor       ebx,ebx
  004cb6b8  8a5e04                mov       bl,[esi+4]
  004cb6bb  81e5fffffffd          and       ebp,0FDFFFFFFh
  004cb6c1  892e                  mov       [esi],ebp
  004cb6c3  8bd5                  mov       edx,ebp
  004cb6c5  0fbf6e18              movsx     ebp,word [esi+18h]
  004cb6c9  8b1c9d40965e00        mov       ebx,[ebx*4+5E9640h]
  004cb6d0  f7c200000001          test      edx,1000000h
  004cb6d6  8b2cab                mov       ebp,[ebx+ebp*4]
  004cb6d9  741d                  je        short 004CB6F8h
  004cb6db  66ff461a              inc       word [esi+1Ah]
  004cb6df  668b561a              mov       dx,[esi+1Ah]
  004cb6e3  33db                  xor       ebx,ebx
  004cb6e5  668b5d16              mov       bx,[ebp+16h]
  004cb6e9  0fbfd2                movsx     edx,dx
  004cb6ec  3bd3                  cmp       edx,ebx
  004cb6ee  7c3a                  jl        short 004CB72Ah
  004cb6f0  33db                  xor       ebx,ebx
  004cb6f2  66895e1a              mov       [esi+1Ah],bx
  004cb6f6  eb34                  jmp       short 004CB72Ch

LAB_004cb6f8:
  004cb6f8  66ff461a              inc       word [esi+1Ah]
  004cb6fc  668b461a              mov       ax,[esi+1Ah]
  004cb700  33db                  xor       ebx,ebx
  004cb702  668b5d16              mov       bx,[ebp+16h]
  004cb706  0fbfe8                movsx     ebp,ax
  004cb709  3beb                  cmp       ebp,ebx
  004cb70b  7c15                  jl        short 004CB722h
  004cb70d  48                    dec       eax
  004cb70e  81ca00000002          or        edx,2000000h
  004cb714  6689461a              mov       [esi+1Ah],ax
  004cb718  8b442414              mov       eax,[esp+14h]
  004cb71c  8916                  mov       [esi],edx
  004cb71e  33db                  xor       ebx,ebx
  004cb720  eb0a                  jmp       short 004CB72Ch

LAB_004cb722:
  004cb722  8b442414              mov       eax,[esp+14h]
  004cb726  33db                  xor       ebx,ebx
  004cb728  eb02                  jmp       short 004CB72Ch

LAB_004cb72a:
  004cb72a  33db                  xor       ebx,ebx

LAB_004cb72c:
  004cb72c  f60602                test      byte [esi],2
  004cb72f  746d                  je        short 004CB79Eh
  004cb731  8d442428              lea       eax,[esp+28h]
  004cb735  53                    push      ebx
  004cb736  50                    push      eax
  004cb737  57                    push      edi
  004cb738  56                    push      esi
  004cb739  e8f2e40400            call      00519C30h
  004cb73e  8be8                  mov       ebp,eax
  004cb740  a1b8e05d00            mov       eax,[5DE0B8h]
  004cb745  83c410                add       esp,10h
  004cb748  3bc3                  cmp       eax,ebx
  004cb74a  7434                  je        short 004CB780h
  004cb74c  8b4d00                mov       ecx,[ebp]
  004cb74f  55                    push      ebp
  004cb750  83c940                or        ecx,40h
  004cb753  56                    push      esi
  004cb754  66c785960000001400    mov       word [ebp+96h],14h
  004cb75d  894d00                mov       [ebp],ecx
  004cb760  e81be40600            call      00539B80h
  004cb765  83c408                add       esp,8
  004cb768  83c51c                add       ebp,1Ch
  004cb76b  55                    push      ebp
  004cb76c  6800180000            push      1800h
  004cb771  6800030000            push      300h
  004cb776  6a03                  push      3
  004cb778  e8a3230700            call      0053DB20h
  004cb77d  83c410                add       esp,10h

LAB_004cb780:
  004cb780  8b06                  mov       eax,[esi]
  004cb782  a900000002            test      eax,2000000h
  004cb787  740d                  je        short 004CB796h
  004cb789  807e420c              cmp       byte [esi+42h],0Ch
  004cb78d  7407                  je        short 004CB796h
  004cb78f  24fd                  and       al,0FDh
  004cb791  885e43                mov       [esi+43h],bl
  004cb794  8906                  mov       [esi],eax

LAB_004cb796:
  004cb796  8b4c2420              mov       ecx,[esp+20h]
  004cb79a  8b442414              mov       eax,[esp+14h]

LAB_004cb79e:
  004cb79e  33d2                  xor       edx,edx
  004cb7a0  8a5642                mov       dl,[esi+42h]
  004cb7a3  83fa0e                cmp       edx,0Eh
  004cb7a6  0f873e080000          ja        near 004CBFEAh
  004cb7ac  ff249508c64c00        jmp       dword [edx*4+4CC608h]
  004cb7b3  66395e12              cmp       [esi+12h],bx
  004cb7b7  751e                  jne       short 004CB7D7h
  004cb7b9  8b16                  mov       edx,[esi]
  004cb7bb  b901000000            mov       ecx,1
  004cb7c0  81ca00000001          or        edx,1000000h
  004cb7c6  66894e18              mov       [esi+18h],cx
  004cb7ca  66895e1a              mov       [esi+1Ah],bx
  004cb7ce  8916                  mov       [esi],edx
  004cb7d0  66894e12              mov       [esi+12h],cx
  004cb7d4  895e5c                mov       [esi+5Ch],ebx

LAB_004cb7d7:
  004cb7d7  0fbf4e10              movsx     ecx,word [esi+10h]
  004cb7db  3bc1                  cmp       eax,ecx
  004cb7dd  7d08                  jge       short 004CB7E7h
  004cb7df  885e43                mov       [esi+43h],bl
  004cb7e2  e903080000            jmp       004CBFEAh

LAB_004cb7e7:
  004cb7e7  8a4643                mov       al,[esi+43h]
  004cb7ea  3ac3                  cmp       al,bl
  004cb7ec  0f84b5040000          je        near 004CBCA7h
  004cb7f2  fec8                  dec       al
  004cb7f4  895e5c                mov       [esi+5Ch],ebx
  004cb7f7  884643                mov       [esi+43h],al
  004cb7fa  e9eb070000            jmp       004CBFEAh
  004cb7ff  66395e12              cmp       [esi+12h],bx
  004cb803  7517                  jne       short 004CB81Ch
  004cb805  8b06                  mov       eax,[esi]
  004cb807  66895e18              mov       [esi+18h],bx
  004cb80b  0d00000001            or        eax,1000000h
  004cb810  66895e1a              mov       [esi+1Ah],bx
  004cb814  8906                  mov       [esi],eax
  004cb816  66c746120100          mov       word [esi+12h],1

LAB_004cb81c:
  004cb81c  8b442418              mov       eax,[esp+18h]
  004cb820  8d90c8000000          lea       edx,[eax+0C8h]
  004cb826  81e2ff0f0000          and       edx,0FFFh
  004cb82c  81fa90010000          cmp       edx,190h
  004cb832  7f0b                  jg        short 004CB83Fh
  004cb834  885e43                mov       [esi+43h],bl
  004cb837  895e5c                mov       [esi+5Ch],ebx
  004cb83a  e9ab070000            jmp       004CBFEAh

LAB_004cb83f:
  004cb83f  25ff0f0000            and       eax,0FFFh
  004cb844  3d00080000            cmp       eax,800h
  004cb849  7d15                  jge       short 004CB860h
  004cb84b  8b442410              mov       eax,[esp+10h]
  004cb84f  895e5c                mov       [esi+5Ch],ebx
  004cb852  0580000000            add       eax,80h
  004cb857  66014632              add       [esi+32h],ax
  004cb85b  e98a070000            jmp       004CBFEAh

LAB_004cb860:
  004cb860  8b4c2410              mov       ecx,[esp+10h]
  004cb864  b880ffffff            mov       eax,0FFFFFF80h
  004cb869  2bc1                  sub       eax,ecx
  004cb86b  895e5c                mov       [esi+5Ch],ebx
  004cb86e  66014632              add       [esi+32h],ax
  004cb872  e973070000            jmp       004CBFEAh
  004cb877  66395e12              cmp       [esi+12h],bx
  004cb87b  8b4c2410              mov       ecx,[esp+10h]
  004cb87f  752f                  jne       short 004CB8B0h
  004cb881  8b16                  mov       edx,[esi]
  004cb883  8d2cc9                lea       ebp,[ecx+ecx*8]
  004cb886  81ca00000001          or        edx,1000000h
  004cb88c  66895e18              mov       [esi+18h],bx
  004cb890  8916                  mov       [esi],edx
  004cb892  66895e1a              mov       [esi+1Ah],bx
  004cb896  0fbf5616              movsx     edx,word [esi+16h]
  004cb89a  c1e20d                shl       edx,0Dh
  004cb89d  c1e504                shl       ebp,4
  004cb8a0  66c746120100          mov       word [esi+12h],1
  004cb8a6  8d942a00700000        lea       edx,[edx+ebp+7000h]
  004cb8ad  89565c                mov       [esi+5Ch],edx

LAB_004cb8b0:
  004cb8b0  66837e1201            cmp       word [esi+12h],1
  004cb8b5  0f852f070000          jne       near 004CBFEAh
  004cb8bb  0fbf5610              movsx     edx,word [esi+10h]
  004cb8bf  3bc2                  cmp       eax,edx
  004cb8c1  7d08                  jge       short 004CB8CBh
  004cb8c3  885e43                mov       [esi+43h],bl
  004cb8c6  895e5c                mov       [esi+5Ch],ebx
  004cb8c9  eb43                  jmp       short 004CB90Eh

LAB_004cb8cb:
  004cb8cb  8a4643                mov       al,[esi+43h]
  004cb8ce  3ac3                  cmp       al,bl
  004cb8d0  7405                  je        short 004CB8D7h
  004cb8d2  fec8                  dec       al
  004cb8d4  884643                mov       [esi+43h],al

LAB_004cb8d7:
  004cb8d7  8b442418              mov       eax,[esp+18h]
  004cb8db  8d9000010000          lea       edx,[eax+100h]
  004cb8e1  81e2ff0f0000          and       edx,0FFFh
  004cb8e7  81fa00020000          cmp       edx,200h
  004cb8ed  7e1f                  jle       short 004CB90Eh
  004cb8ef  25ff0f0000            and       eax,0FFFh
  004cb8f4  3d00080000            cmp       eax,800h
  004cb8f9  7d08                  jge       short 004CB903h
  004cb8fb  8d8180000000          lea       eax,[ecx+80h]
  004cb901  eb07                  jmp       short 004CB90Ah

LAB_004cb903:
  004cb903  b880ffffff            mov       eax,0FFFFFF80h
  004cb908  2bc1                  sub       eax,ecx

LAB_004cb90a:
  004cb90a  66014632              add       [esi+32h],ax

LAB_004cb90e:
  004cb90e  e82db80700            call      00547140h
  004cb913  99                    cdq
  004cb914  b928000000            mov       ecx,28h
  004cb919  f7f9                  idiv      ecx
  004cb91b  83fa01                cmp       edx,1
  004cb91e  0f85c6060000          jne       near 004CBFEAh
  004cb924  66395e16              cmp       [esi+16h],bx
  004cb928  0f85bc060000          jne       near 004CBFEAh
  004cb92e  0fbf6e14              movsx     ebp,word [esi+14h]
  004cb932  6aff                  push      0FFFFFFFFh
  004cb934  53                    push      ebx
  004cb935  68e0000000            push      0E0h
  004cb93a  53                    push      ebx
  004cb93b  57                    push      edi
  004cb93c  56                    push      esi
  004cb93d  66895614              mov       [esi+14h],dx
  004cb941  e8faf2faff            call      0047AC40h
  004cb946  83c418                add       esp,18h
  004cb949  66896e14              mov       [esi+14h],bp
  004cb94d  e998060000            jmp       004CBFEAh
  004cb952  66837e1201            cmp       word [esi+12h],1
  004cb957  0f85d7000000          jne       near 004CBA34h
  004cb95d  8a4643                mov       al,[esi+43h]
  004cb960  3ac3                  cmp       al,bl
  004cb962  7405                  je        short 004CB969h
  004cb964  fec8                  dec       al
  004cb966  884643                mov       [esi+43h],al

LAB_004cb969:
  004cb969  8b442410              mov       eax,[esp+10h]
  004cb96d  0fbf5616              movsx     edx,word [esi+16h]
  004cb971  8d04c0                lea       eax,[eax+eax*8]
  004cb974  c1e20d                shl       edx,0Dh
  004cb977  c1e004                shl       eax,4
  004cb97a  8d840200500000        lea       eax,[edx+eax+5000h]
  004cb981  0fafc8                imul      ecx,eax
  004cb984  c1f90c                sar       ecx,0Ch
  004cb987  8be9                  mov       ebp,ecx
  004cb989  89465c                mov       [esi+5Ch],eax
  004cb98c  0fbf4e10              movsx     ecx,word [esi+10h]
  004cb990  51                    push      ecx
  004cb991  e886a00700            call      00545A1Ch
  004cb996  0fafc5                imul      eax,ebp
  004cb999  0fbf5610              movsx     edx,word [esi+10h]
  004cb99d  8b0f                  mov       ecx,[edi]
  004cb99f  52                    push      edx
  004cb9a0  c1f80c                sar       eax,0Ch
  004cb9a3  03c8                  add       ecx,eax
  004cb9a5  890f                  mov       [edi],ecx
  004cb9a7  e86aa00700            call      00545A16h
  004cb9ac  0fafc5                imul      eax,ebp
  004cb9af  8b4e24                mov       ecx,[esi+24h]
  004cb9b2  895e5c                mov       [esi+5Ch],ebx
  004cb9b5  c1f80c                sar       eax,0Ch
  004cb9b8  03c8                  add       ecx,eax
  004cb9ba  8b442418              mov       eax,[esp+18h]
  004cb9be  894e24                mov       [esi+24h],ecx
  004cb9c1  8d8800010000          lea       ecx,[eax+100h]
  004cb9c7  81e1ff0f0000          and       ecx,0FFFh
  004cb9cd  81f900020000          cmp       ecx,200h
  004cb9d3  7e28                  jle       short 004CB9FDh
  004cb9d5  25ff0f0000            and       eax,0FFFh
  004cb9da  3d00080000            cmp       eax,800h
  004cb9df  7d0a                  jge       short 004CB9EBh
  004cb9e1  8b542410              mov       edx,[esp+10h]
  004cb9e5  8d441232              lea       eax,[edx+edx+32h]
  004cb9e9  eb0e                  jmp       short 004CB9F9h

LAB_004cb9eb:
  004cb9eb  8b442410              mov       eax,[esp+10h]
  004cb9ef  8d0c00                lea       ecx,[eax+eax]
  004cb9f2  b8ceffffff            mov       eax,0FFFFFFCEh
  004cb9f7  2bc1                  sub       eax,ecx

LAB_004cb9f9:
  004cb9f9  66014632              add       [esi+32h],ax

LAB_004cb9fd:
  004cb9fd  e83eb70700            call      00547140h
  004cba02  99                    cdq
  004cba03  b928000000            mov       ecx,28h
  004cba08  f7f9                  idiv      ecx
  004cba0a  83fa01                cmp       edx,1
  004cba0d  7525                  jne       short 004CBA34h
  004cba0f  66395e16              cmp       [esi+16h],bx
  004cba13  751f                  jne       short 004CBA34h
  004cba15  0fbf6e14              movsx     ebp,word [esi+14h]
  004cba19  6aff                  push      0FFFFFFFFh
  004cba1b  53                    push      ebx
  004cba1c  68e0000000            push      0E0h
  004cba21  53                    push      ebx
  004cba22  57                    push      edi
  004cba23  56                    push      esi
  004cba24  66895614              mov       [esi+14h],dx
  004cba28  e813f2faff            call      0047AC40h
  004cba2d  83c418                add       esp,18h
  004cba30  66896e14              mov       [esi+14h],bp

LAB_004cba34:
  004cba34  66395e12              cmp       [esi+12h],bx
  004cba38  0f85ac050000          jne       near 004CBFEAh
  004cba3e  8b06                  mov       eax,[esi]
  004cba40  66895e18              mov       [esi+18h],bx
  004cba44  0d00000001            or        eax,1000000h
  004cba49  66895e1a              mov       [esi+1Ah],bx
  004cba4d  8906                  mov       [esi],eax
  004cba4f  66c746120100          mov       word [esi+12h],1
  004cba55  e990050000            jmp       004CBFEAh
  004cba5a  668b4e12              mov       cx,[esi+12h]
  004cba5e  6683f901              cmp       cx,1
  004cba62  7519                  jne       short 004CBA7Dh
  004cba64  0fbf5610              movsx     edx,word [esi+10h]
  004cba68  3bc2                  cmp       eax,edx
  004cba6a  7d05                  jge       short 004CBA71h
  004cba6c  885e43                mov       [esi+43h],bl
  004cba6f  eb0c                  jmp       short 004CBA7Dh

LAB_004cba71:
  004cba71  8a4643                mov       al,[esi+43h]
  004cba74  3ac3                  cmp       al,bl
  004cba76  7405                  je        short 004CBA7Dh
  004cba78  fec8                  dec       al
  004cba7a  884643                mov       [esi+43h],al

LAB_004cba7d:
  004cba7d  663bcb                cmp       cx,bx
  004cba80  0f8564050000          jne       near 004CBFEAh
  004cba86  8b442410              mov       eax,[esp+10h]
  004cba8a  8b3e                  mov       edi,[esi]
  004cba8c  81cf00000001          or        edi,1000000h
  004cba92  66895e18              mov       [esi+18h],bx
  004cba96  8d04c0                lea       eax,[eax+eax*8]
  004cba99  66895e1a              mov       [esi+1Ah],bx
  004cba9d  c1e004                shl       eax,4
  004cbaa0  0500500000            add       eax,5000h
  004cbaa5  893e                  mov       [esi],edi
  004cbaa7  66c746120100          mov       word [esi+12h],1
  004cbaad  89465c                mov       [esi+5Ch],eax
  004cbab0  e935050000            jmp       004CBFEAh
  004cbab5  66395e12              cmp       [esi+12h],bx
  004cbab9  750e                  jne       short 004CBAC9h
  004cbabb  66895e18              mov       [esi+18h],bx
  004cbabf  66895e1a              mov       [esi+1Ah],bx
  004cbac3  66c746120100          mov       word [esi+12h],1

LAB_004cbac9:
  004cbac9  66837e1201            cmp       word [esi+12h],1
  004cbace  7557                  jne       short 004CBB27h
  004cbad0  8b2e                  mov       ebp,[esi]
  004cbad2  8b5660                mov       edx,[esi+60h]
  004cbad5  81cd00000001          or        ebp,1000000h
  004cbadb  8bca                  mov       ecx,edx
  004cbadd  892e                  mov       [esi],ebp
  004cbadf  8b6e20                mov       ebp,[esi+20h]
  004cbae2  2bcd                  sub       ecx,ebp
  004cbae4  895e5c                mov       [esi+5Ch],ebx
  004cbae7  c1f905                sar       ecx,5
  004cbaea  66895e18              mov       [esi+18h],bx
  004cbaee  8bf9                  mov       edi,ecx
  004cbaf0  7902                  jns       short 004CBAF4h
  004cbaf2  f7df                  neg       edi

LAB_004cbaf4:
  004cbaf4  81e700f0ffff          and       edi,0FFFFF000h
  004cbafa  81ff00300000          cmp       edi,3000h
  004cbb00  7d02                  jge       short 004CBB04h
  004cbb02  d1e1                  shl       ecx,1

LAB_004cbb04:
  004cbb04  03cd                  add       ecx,ebp
  004cbb06  2bd1                  sub       edx,ecx
  004cbb08  894e20                mov       [esi+20h],ecx
  004cbb0b  c1fa0c                sar       edx,0Ch
  004cbb0e  7902                  jns       short 004CBB12h
  004cbb10  f7da                  neg       edx

LAB_004cbb12:
  004cbb12  83fa0a                cmp       edx,0Ah
  004cbb15  7d10                  jge       short 004CBB27h
  004cbb17  66c746120200          mov       word [esi+12h],2
  004cbb1d  66895e1a              mov       [esi+1Ah],bx
  004cbb21  66c746180500          mov       word [esi+18h],5

LAB_004cbb27:
  004cbb27  66837e1202            cmp       word [esi+12h],2
  004cbb2c  0f85b8040000          jne       near 004CBFEAh
  004cbb32  8b16                  mov       edx,[esi]
  004cbb34  8a4e43                mov       cl,[esi+43h]
  004cbb37  81ca00000002          or        edx,2000000h
  004cbb3d  fec9                  dec       cl
  004cbb3f  66c746180500          mov       word [esi+18h],5
  004cbb45  8916                  mov       [esi],edx
  004cbb47  884e43                mov       [esi+43h],cl
  004cbb4a  7503                  jne       short 004CBB4Fh
  004cbb4c  885e43                mov       [esi+43h],bl

LAB_004cbb4f:
  004cbb4f  3db4000000            cmp       eax,0B4h
  004cbb54  0f8e90040000          jle       near 004CBFEAh
  004cbb5a  e8e1b50700            call      00547140h
  004cbb5f  99                    cdq
  004cbb60  b914000000            mov       ecx,14h
  004cbb65  f7f9                  idiv      ecx
  004cbb67  3bd3                  cmp       edx,ebx
  004cbb69  0f857b040000          jne       near 004CBFEAh
  004cbb6f  c646420d              mov       byte [esi+42h],0Dh
  004cbb73  66895e12              mov       [esi+12h],bx
  004cbb77  c6464301              mov       byte [esi+43h],1
  004cbb7b  e96a040000            jmp       004CBFEAh
  004cbb80  8b54241c              mov       edx,[esp+1Ch]
  004cbb84  8b4e20                mov       ecx,[esi+20h]
  004cbb87  8b4220                mov       eax,[edx+20h]
  004cbb8a  8d9100400b00          lea       edx,[ecx+0B4000h]
  004cbb90  3bd0                  cmp       edx,eax
  004cbb92  7d0c                  jge       short 004CBBA0h
  004cbb94  8b06                  mov       eax,[esi]
  004cbb96  885e43                mov       [esi+43h],bl
  004cbb99  24fd                  and       al,0FDh
  004cbb9b  e948040000            jmp       004CBFE8h

LAB_004cbba0:
  004cbba0  2bc1                  sub       eax,ecx
  004cbba2  0500800200            add       eax,28000h
  004cbba7  c1f802                sar       eax,2
  004cbbaa  03c1                  add       eax,ecx
  004cbbac  668b4e12              mov       cx,[esi+12h]
  004cbbb0  6683f901              cmp       cx,1
  004cbbb4  894620                mov       [esi+20h],eax
  004cbbb7  7510                  jne       short 004CBBC9h
  004cbbb9  8b06                  mov       eax,[esi]
  004cbbbb  a900000002            test      eax,2000000h
  004cbbc0  7407                  je        short 004CBBC9h
  004cbbc2  24fd                  and       al,0FDh
  004cbbc4  885e43                mov       [esi+43h],bl
  004cbbc7  8906                  mov       [esi],eax

LAB_004cbbc9:
  004cbbc9  663bcb                cmp       cx,bx
  004cbbcc  0f8518040000          jne       near 004CBFEAh
  004cbbd2  8b06                  mov       eax,[esi]
  004cbbd4  66c746120100          mov       word [esi+12h],1
  004cbbda  25fffffffe            and       eax,0FEFFFFFFh
  004cbbdf  e904040000            jmp       004CBFE8h
  004cbbe4  66837e1201            cmp       word [esi+12h],1
  004cbbe9  8b7c2410              mov       edi,[esp+10h]
  004cbbed  8b542418              mov       edx,[esp+18h]
  004cbbf1  7577                  jne       short 004CBC6Ah
  004cbbf3  8b6e20                mov       ebp,[esi+20h]
  004cbbf6  8b4e60                mov       ecx,[esi+60h]
  004cbbf9  81c500300000          add       ebp,3000h
  004cbbff  8bc5                  mov       eax,ebp
  004cbc01  896e20                mov       [esi+20h],ebp
  004cbc04  2bc1                  sub       eax,ecx
  004cbc06  c1f80c                sar       eax,0Ch
  004cbc09  3dc8000000            cmp       eax,0C8h
  004cbc0e  7e21                  jle       short 004CBC31h
  004cbc10  8b06                  mov       eax,[esi]
  004cbc12  66c746120200          mov       word [esi+12h],2
  004cbc18  25fffffffe            and       eax,0FEFFFFFFh
  004cbc1d  66895e10              mov       [esi+10h],bx
  004cbc21  0c02                  or        al,2
  004cbc23  66c746180300          mov       word [esi+18h],3
  004cbc29  66c7461a0600          mov       word [esi+1Ah],6
  004cbc2f  8906                  mov       [esi],eax

LAB_004cbc31:
  004cbc31  8d8a00010000          lea       ecx,[edx+100h]
  004cbc37  81e1ff0f0000          and       ecx,0FFFh
  004cbc3d  81f900020000          cmp       ecx,200h
  004cbc43  7e25                  jle       short 004CBC6Ah
  004cbc45  8bc2                  mov       eax,edx
  004cbc47  25ff0f0000            and       eax,0FFFh
  004cbc4c  3d00080000            cmp       eax,800h
  004cbc51  7d09                  jge       short 004CBC5Ch
  004cbc53  8d843f80000000        lea       eax,[edi+edi+80h]
  004cbc5a  eb0a                  jmp       short 004CBC66h

LAB_004cbc5c:
  004cbc5c  8d0c3f                lea       ecx,[edi+edi]
  004cbc5f  b880ffffff            mov       eax,0FFFFFF80h
  004cbc64  2bc1                  sub       eax,ecx

LAB_004cbc66:
  004cbc66  66014632              add       [esi+32h],ax

LAB_004cbc6a:
  004cbc6a  66837e1202            cmp       word [esi+12h],2
  004cbc6f  0f85a7000000          jne       near 004CBD1Ch
  004cbc75  8bcf                  mov       ecx,edi
  004cbc77  b814000000            mov       eax,14h
  004cbc7c  83e1fc                and       ecx,0FFFFFFFCh
  004cbc7f  c1e10a                shl       ecx,0Ah
  004cbc82  83c10c                add       ecx,0Ch
  004cbc85  d3e0                  shl       eax,cl
  004cbc87  8b4e20                mov       ecx,[esi+20h]
  004cbc8a  8da900400b00          lea       ebp,[ecx+0B4000h]
  004cbc90  89465c                mov       [esi+5Ch],eax
  004cbc93  8b44241c              mov       eax,[esp+1Ch]
  004cbc97  8b4020                mov       eax,[eax+20h]
  004cbc9a  3be8                  cmp       ebp,eax
  004cbc9c  7d11                  jge       short 004CBCAFh
  004cbc9e  8b06                  mov       eax,[esi]
  004cbca0  885e43                mov       [esi+43h],bl
  004cbca3  24fd                  and       al,0FDh
  004cbca5  8906                  mov       [esi],eax

LAB_004cbca7:
  004cbca7  895e5c                mov       [esi+5Ch],ebx
  004cbcaa  e93b030000            jmp       004CBFEAh

LAB_004cbcaf:
  004cbcaf  2bc1                  sub       eax,ecx
  004cbcb1  0500400100            add       eax,14000h
  004cbcb6  c1f802                sar       eax,2
  004cbcb9  03c1                  add       eax,ecx
  004cbcbb  66ff4610              inc       word [esi+10h]
  004cbcbf  668b4e10              mov       cx,[esi+10h]
  004cbcc3  894620                mov       [esi+20h],eax
  004cbcc6  8d8200010000          lea       eax,[edx+100h]
  004cbccc  25ff0f0000            and       eax,0FFFh
  004cbcd1  3d00020000            cmp       eax,200h
  004cbcd6  7e25                  jle       short 004CBCFDh
  004cbcd8  81e2ff0f0000          and       edx,0FFFh
  004cbcde  81fa00080000          cmp       edx,800h
  004cbce4  7d09                  jge       short 004CBCEFh
  004cbce6  8d843f80000000        lea       eax,[edi+edi+80h]
  004cbced  eb0a                  jmp       short 004CBCF9h

LAB_004cbcef:
  004cbcef  8d143f                lea       edx,[edi+edi]
  004cbcf2  b880ffffff            mov       eax,0FFFFFF80h
  004cbcf7  2bc2                  sub       eax,edx

LAB_004cbcf9:
  004cbcf9  66014632              add       [esi+32h],ax

LAB_004cbcfd:
  004cbcfd  0fbf4616              movsx     eax,word [esi+16h]
  004cbd01  83c002                add       eax,2
  004cbd04  0fbfc9                movsx     ecx,cx
  004cbd07  8d0480                lea       eax,[eax+eax*4]
  004cbd0a  d1e0                  shl       eax,1
  004cbd0c  3bc8                  cmp       ecx,eax
  004cbd0e  7c0c                  jl        short 004CBD1Ch
  004cbd10  8b06                  mov       eax,[esi]
  004cbd12  66c746120300          mov       word [esi+12h],3
  004cbd18  24fd                  and       al,0FDh
  004cbd1a  8906                  mov       [esi],eax

LAB_004cbd1c:
  004cbd1c  668b4e12              mov       cx,[esi+12h]
  004cbd20  6683f903              cmp       cx,3
  004cbd24  751c                  jne       short 004CBD42h
  004cbd26  8b465c                mov       eax,[esi+5Ch]
  004cbd29  8bd0                  mov       edx,eax
  004cbd2b  c1fa03                sar       edx,3
  004cbd2e  2bc2                  sub       eax,edx
  004cbd30  89465c                mov       [esi+5Ch],eax
  004cbd33  2500f0ffff            and       eax,0FFFFF000h
  004cbd38  3d00a00000            cmp       eax,0A000h
  004cbd3d  7d03                  jge       short 004CBD42h
  004cbd3f  885e43                mov       [esi+43h],bl

LAB_004cbd42:
  004cbd42  663bcb                cmp       cx,bx
  004cbd45  0f859f020000          jne       near 004CBFEAh
  004cbd4b  b801000000            mov       eax,1
  004cbd50  66895e1a              mov       [esi+1Ah],bx
  004cbd54  66894612              mov       [esi+12h],ax
  004cbd58  66894618              mov       [esi+18h],ax
  004cbd5c  8b06                  mov       eax,[esi]
  004cbd5e  895e5c                mov       [esi+5Ch],ebx
  004cbd61  0d00000001            or        eax,1000000h
  004cbd66  e97d020000            jmp       004CBFE8h
  004cbd6b  66395e12              cmp       [esi+12h],bx
  004cbd6f  757c                  jne       short 004CBDEDh
  004cbd71  8b0e                  mov       ecx,[esi]
  004cbd73  66c746120100          mov       word [esi+12h],1
  004cbd79  81e1fffffffe          and       ecx,0FEFFFFFFh
  004cbd7f  66c746180400          mov       word [esi+18h],4
  004cbd85  890e                  mov       [esi],ecx
  004cbd87  66895e1a              mov       [esi+1Ah],bx
  004cbd8b  33d2                  xor       edx,edx
  004cbd8d  b9e2ca5e00            mov       ecx,5ECAE2h

LAB_004cbd92:
  004cbd92  80390d                cmp       byte [ecx],0Dh
  004cbd95  750f                  jne       short 004CBDA6h
  004cbd97  33db                  xor       ebx,ebx
  004cbd99  8a5e08                mov       bl,[esi+8]
  004cbd9c  3bd3                  cmp       edx,ebx
  004cbd9e  0f8558080000          jne       near 004CC5FCh
  004cbda4  33db                  xor       ebx,ebx

LAB_004cbda6:
  004cbda6  81c19c000000          add       ecx,9Ch
  004cbdac  42                    inc       edx
  004cbdad  81f962de5e00          cmp       ecx,5EDE62h
  004cbdb3  7cdd                  jl        short 004CBD92h
  004cbdb5  3d20030000            cmp       eax,320h
  004cbdba  7e08                  jle       short 004CBDC4h
  004cbdbc  885e43                mov       [esi+43h],bl
  004cbdbf  e926020000            jmp       004CBFEAh

LAB_004cbdc4:
  004cbdc4  a1cc975e00            mov       eax,[5E97CCh]
  004cbdc9  8d7e1c                lea       edi,[esi+1Ch]
  004cbdcc  57                    push      edi
  004cbdcd  6800300000            push      3000h
  004cbdd2  8b88a8000000          mov       ecx,[eax+0A8h]
  004cbdd8  33c0                  xor       eax,eax
  004cbdda  6800030000            push      300h
  004cbddf  8b11                  mov       edx,[ecx]
  004cbde1  8a423d                mov       al,[edx+3Dh]
  004cbde4  50                    push      eax
  004cbde5  e8361d0700            call      0053DB20h
  004cbdea  83c410                add       esp,10h

LAB_004cbded:
  004cbded  66837e1201            cmp       word [esi+12h],1
  004cbdf2  755d                  jne       short 004CBE51h
  004cbdf4  f6461a01              test      byte [esi+1Ah],1
  004cbdf8  741f                  je        short 004CBE19h
  004cbdfa  0fbf6e14              movsx     ebp,word [esi+14h]
  004cbdfe  6aff                  push      0FFFFFFFFh
  004cbe00  53                    push      ebx
  004cbe01  68e0000000            push      0E0h
  004cbe06  53                    push      ebx
  004cbe07  57                    push      edi
  004cbe08  56                    push      esi
  004cbe09  66895e14              mov       [esi+14h],bx
  004cbe0d  e82eeefaff            call      0047AC40h
  004cbe12  83c418                add       esp,18h
  004cbe15  66896e14              mov       [esi+14h],bp

LAB_004cbe19:
  004cbe19  8b06                  mov       eax,[esi]
  004cbe1b  895e5c                mov       [esi+5Ch],ebx
  004cbe1e  a900000002            test      eax,2000000h
  004cbe23  742c                  je        short 004CBE51h
  004cbe25  0fbf6e14              movsx     ebp,word [esi+14h]
  004cbe29  6aff                  push      0FFFFFFFFh
  004cbe2b  53                    push      ebx
  004cbe2c  68e0000000            push      0E0h
  004cbe31  53                    push      ebx
  004cbe32  b802000000            mov       eax,2
  004cbe37  57                    push      edi
  004cbe38  56                    push      esi
  004cbe39  66894612              mov       [esi+12h],ax
  004cbe3d  c646430f              mov       byte [esi+43h],0Fh
  004cbe41  66894614              mov       [esi+14h],ax
  004cbe45  e8f6edfaff            call      0047AC40h
  004cbe4a  83c418                add       esp,18h
  004cbe4d  66896e14              mov       [esi+14h],bp

LAB_004cbe51:
  004cbe51  66837e1202            cmp       word [esi+12h],2
  004cbe56  0f858e010000          jne       near 004CBFEAh
  004cbe5c  fe4e43                dec       byte [esi+43h]
  004cbe5f  e986010000            jmp       004CBFEAh
  004cbe64  66395e12              cmp       [esi+12h],bx
  004cbe68  0f85ab000000          jne       near 004CBF19h
  004cbe6e  b800f45f00            mov       eax,5FF400h

LAB_004cbe73:
  004cbe73  39706c                cmp       [eax+6Ch],esi
  004cbe76  7508                  jne       short 004CBE80h
  004cbe78  f70000000080          test      dword [eax],80000000h
  004cbe7e  750e                  jne       short 004CBE8Eh

LAB_004cbe80:
  004cbe80  059c000000            add       eax,9Ch
  004cbe85  3d80076000            cmp       eax,600780h
  004cbe8a  7ce7                  jl        short 004CBE73h
  004cbe8c  eb08                  jmp       short 004CBE96h

LAB_004cbe8e:
  004cbe8e  885e43                mov       [esi+43h],bl
  004cbe91  e954010000            jmp       004CBFEAh

LAB_004cbe96:
  004cbe96  8b06                  mov       eax,[esi]
  004cbe98  a904080000            test      eax,804h
  004cbe9d  740d                  je        short 004CBEACh
  004cbe9f  885e43                mov       [esi+43h],bl
  004cbea2  25fbf7ffff            and       eax,0FFFFF7FBh
  004cbea7  e93c010000            jmp       004CBFE8h

LAB_004cbeac:
  004cbeac  6aff                  push      0FFFFFFFFh
  004cbeae  53                    push      ebx
  004cbeaf  68bd000000            push      0BDh
  004cbeb4  56                    push      esi
  004cbeb5  e826ecfaff            call      0047AAE0h
  004cbeba  b901000000            mov       ecx,1
  004cbebf  83c410                add       esp,10h
  004cbec2  66894e12              mov       [esi+12h],cx
  004cbec6  a1082c5500            mov       eax,[552C08h]
  004cbecb  25ffff0000            and       eax,0FFFFh
  004cbed0  66894606              mov       [esi+6],ax
  004cbed4  8b3dc4975e00          mov       edi,[5E97C4h]
  004cbeda  8bd0                  mov       edx,eax
  004cbedc  25ff000000            and       eax,0FFh
  004cbee1  8bbf9c000000          mov       edi,[edi+9Ch]
  004cbee7  c1fa08                sar       edx,8
  004cbeea  81e2ff000000          and       edx,0FFh
  004cbef0  8d0440                lea       eax,[eax+eax*2]
  004cbef3  c1e004                shl       eax,4
  004cbef6  8b1497                mov       edx,[edi+edx*4]
  004cbef9  660fb6440213          movzx     ax,byte [edx+eax+13h]
  004cbeff  66894610              mov       [esi+10h],ax
  004cbf03  8b06                  mov       eax,[esi]
  004cbf05  0d04080001            or        eax,1000804h
  004cbf0a  895e5c                mov       [esi+5Ch],ebx
  004cbf0d  66895e18              mov       [esi+18h],bx
  004cbf11  66895e1a              mov       [esi+1Ah],bx
  004cbf15  8906                  mov       [esi],eax
  004cbf17  eb05                  jmp       short 004CBF1Eh

LAB_004cbf19:
  004cbf19  b901000000            mov       ecx,1

LAB_004cbf1e:
  004cbf1e  66394e12              cmp       [esi+12h],cx
  004cbf22  753d                  jne       short 004CBF61h
  004cbf24  8b7c241c              mov       edi,[esp+1Ch]
  004cbf28  8b4620                mov       eax,[esi+20h]
  004cbf2b  8b5720                mov       edx,[edi+20h]
  004cbf2e  2bd0                  sub       edx,eax
  004cbf30  81c200400100          add       edx,14000h
  004cbf36  c1fa03                sar       edx,3
  004cbf39  03d0                  add       edx,eax
  004cbf3b  66ff4e10              dec       word [esi+10h]
  004cbf3f  66394e10              cmp       [esi+10h],cx
  004cbf43  895620                mov       [esi+20h],edx
  004cbf46  7d1d                  jge       short 004CBF65h
  004cbf48  8b06                  mov       eax,[esi]
  004cbf4a  66c746120200          mov       word [esi+12h],2
  004cbf50  25fbfffffe            and       eax,0FEFFFFFBh
  004cbf55  66895e18              mov       [esi+18h],bx
  004cbf59  66895e1a              mov       [esi+1Ah],bx
  004cbf5d  8906                  mov       [esi],eax
  004cbf5f  eb04                  jmp       short 004CBF65h

LAB_004cbf61:
  004cbf61  8b7c241c              mov       edi,[esp+1Ch]

LAB_004cbf65:
  004cbf65  66837e1202            cmp       word [esi+12h],2
  004cbf6a  754c                  jne       short 004CBFB8h
  004cbf6c  8b4620                mov       eax,[esi+20h]
  004cbf6f  8b4f20                mov       ecx,[edi+20h]
  004cbf72  2bc8                  sub       ecx,eax
  004cbf74  bd03000000            mov       ebp,3
  004cbf79  81c100800200          add       ecx,28000h
  004cbf7f  c1f903                sar       ecx,3
  004cbf82  03c8                  add       ecx,eax
  004cbf84  66396e1a              cmp       [esi+1Ah],bp
  004cbf88  894e20                mov       [esi+20h],ecx
  004cbf8b  7530                  jne       short 004CBFBDh
  004cbf8d  33c0                  xor       eax,eax
  004cbf8f  6aff                  push      0FFFFFFFFh
  004cbf91  668b4606              mov       ax,[esi+6]
  004cbf95  6aff                  push      0FFFFFFFFh
  004cbf97  8bd0                  mov       edx,eax
  004cbf99  c1f808                sar       eax,8
  004cbf9c  81e2ff000000          and       edx,0FFh
  004cbfa2  25ff000000            and       eax,0FFh
  004cbfa7  52                    push      edx
  004cbfa8  50                    push      eax
  004cbfa9  56                    push      esi
  004cbfaa  e8d1e90600            call      0053A980h
  004cbfaf  83c414                add       esp,14h
  004cbfb2  66896e12              mov       [esi+12h],bp
  004cbfb6  eb05                  jmp       short 004CBFBDh

LAB_004cbfb8:
  004cbfb8  bd03000000            mov       ebp,3

LAB_004cbfbd:
  004cbfbd  66396e12              cmp       [esi+12h],bp
  004cbfc1  7527                  jne       short 004CBFEAh
  004cbfc3  8b4620                mov       eax,[esi+20h]
  004cbfc6  8b4f20                mov       ecx,[edi+20h]
  004cbfc9  2bc8                  sub       ecx,eax
  004cbfcb  81c100800200          add       ecx,28000h
  004cbfd1  c1f903                sar       ecx,3
  004cbfd4  03c8                  add       ecx,eax
  004cbfd6  894e20                mov       [esi+20h],ecx
  004cbfd9  8b06                  mov       eax,[esi]
  004cbfdb  a900000002            test      eax,2000000h
  004cbfe0  7408                  je        short 004CBFEAh
  004cbfe2  885e43                mov       [esi+43h],bl
  004cbfe5  80e4f7                and       ah,0F7h

LAB_004cbfe8:
  004cbfe8  8906                  mov       [esi],eax

LAB_004cbfea:
  004cbfea  8b0e                  mov       ecx,[esi]
  004cbfec  8a4642                mov       al,[esi+42h]
  004cbfef  f6c120                test      cl,20h
  004cbff2  746a                  je        short 004CC05Eh
  004cbff4  3c0c                  cmp       al,0Ch
  004cbff6  7404                  je        short 004CBFFCh
  004cbff8  3c0a                  cmp       al,0Ah
  004cbffa  7505                  jne       short 004CC001h

LAB_004cbffc:
  004cbffc  f6c102                test      cl,2
  004cbfff  7444                  je        short 004CC045h

LAB_004cc001:
  004cc001  e83ab10700            call      00547140h
  004cc006  99                    cdq
  004cc007  b932000000            mov       ecx,32h
  004cc00c  8b7e20                mov       edi,[esi+20h]
  004cc00f  f7f9                  idiv      ecx
  004cc011  66035614              add       dx,[esi+14h]
  004cc015  03d1                  add       edx,ecx
  004cc017  81e2ff0f0000          and       edx,0FFFh
  004cc01d  66895614              mov       [esi+14h],dx
  004cc021  0fbfd2                movsx     edx,dx
  004cc024  52                    push      edx
  004cc025  e8f2990700            call      00545A1Ch
  004cc02a  8b4e48                mov       ecx,[esi+48h]
  004cc02d  0500300000            add       eax,3000h
  004cc032  8d0440                lea       eax,[eax+eax*2]
  004cc035  8d0480                lea       eax,[eax+eax*4]
  004cc038  8d1481                lea       edx,[ecx+eax*4]
  004cc03b  2bd7                  sub       edx,edi
  004cc03d  c1fa03                sar       edx,3
  004cc040  03d7                  add       edx,edi
  004cc042  895620                mov       [esi+20h],edx

LAB_004cc045:
  004cc045  8b4c241c              mov       ecx,[esp+1Ch]
  004cc049  8b4164                mov       eax,[ecx+64h]
  004cc04c  8b4920                mov       ecx,[ecx+20h]
  004cc04f  33c8                  xor       ecx,eax
  004cc051  f7c100f0ffff          test      ecx,0FFFFF000h
  004cc057  7557                  jne       short 004CC0B0h
  004cc059  894648                mov       [esi+48h],eax
  004cc05c  eb52                  jmp       short 004CC0B0h

LAB_004cc05e:
  004cc05e  3c0c                  cmp       al,0Ch
  004cc060  7404                  je        short 004CC066h
  004cc062  3c0a                  cmp       al,0Ah
  004cc064  7505                  jne       short 004CC06Bh

LAB_004cc066:
  004cc066  f6c102                test      cl,2
  004cc069  7445                  je        short 004CC0B0h

LAB_004cc06b:
  004cc06b  e8d0b00700            call      00547140h
  004cc070  99                    cdq
  004cc071  b932000000            mov       ecx,32h
  004cc076  8b7e20                mov       edi,[esi+20h]
  004cc079  f7f9                  idiv      ecx
  004cc07b  66035614              add       dx,[esi+14h]
  004cc07f  03d1                  add       edx,ecx
  004cc081  81e2ff0f0000          and       edx,0FFFh
  004cc087  66895614              mov       [esi+14h],dx
  004cc08b  0fbfd2                movsx     edx,dx
  004cc08e  52                    push      edx
  004cc08f  e888990700            call      00545A1Ch
  004cc094  8b4e60                mov       ecx,[esi+60h]
  004cc097  8d0440                lea       eax,[eax+eax*2]
  004cc09a  8d0480                lea       eax,[eax+eax*4]
  004cc09d  8d1481                lea       edx,[ecx+eax*4]
  004cc0a0  2bd7                  sub       edx,edi
  004cc0a2  81c200c00800          add       edx,8C000h
  004cc0a8  c1fa03                sar       edx,3
  004cc0ab  03d7                  add       edx,edi
  004cc0ad  895620                mov       [esi+20h],edx

LAB_004cc0b0:
  004cc0b0  0fbf4632              movsx     eax,word [esi+32h]
  004cc0b4  50                    push      eax
  004cc0b5  e862990700            call      00545A1Ch
  004cc0ba  0fbf4e32              movsx     ecx,word [esi+32h]
  004cc0be  51                    push      ecx
  004cc0bf  89442474              mov       [esp+74h],eax
  004cc0c3  e84e990700            call      00545A16h
  004cc0c8  8b5650                mov       edx,[esi+50h]
  004cc0cb  89442478              mov       [esp+78h],eax
  004cc0cf  8bc2                  mov       eax,edx
  004cc0d1  8b4c2420              mov       ecx,[esp+20h]
  004cc0d5  c1f808                sar       eax,8
  004cc0d8  0faf4e5c              imul      ecx,[esi+5Ch]
  004cc0dc  8bfa                  mov       edi,edx
  004cc0de  56                    push      esi
  004cc0df  2bf8                  sub       edi,eax
  004cc0e1  c1fa03                sar       edx,3
  004cc0e4  2bfa                  sub       edi,edx
  004cc0e6  8b5654                mov       edx,[esi+54h]
  004cc0e9  8bc2                  mov       eax,edx
  004cc0eb  8bea                  mov       ebp,edx
  004cc0ed  c1f808                sar       eax,8
  004cc0f0  2be8                  sub       ebp,eax
  004cc0f2  897e50                mov       [esi+50h],edi
  004cc0f5  c1fa03                sar       edx,3
  004cc0f8  2bea                  sub       ebp,edx
  004cc0fa  8b5658                mov       edx,[esi+58h]
  004cc0fd  8bda                  mov       ebx,edx
  004cc0ff  8bc2                  mov       eax,edx
  004cc101  c1fb08                sar       ebx,8
  004cc104  2bc3                  sub       eax,ebx
  004cc106  8b5e1c                mov       ebx,[esi+1Ch]
  004cc109  c1fa03                sar       edx,3
  004cc10c  c1f90c                sar       ecx,0Ch
  004cc10f  2bc2                  sub       eax,edx
  004cc111  8bd1                  mov       edx,ecx
  004cc113  0faf542474            imul      edx,[esp+74h]
  004cc118  0faf4c247c            imul      ecx,[esp+7Ch]
  004cc11d  c1fa0c                sar       edx,0Ch
  004cc120  03d7                  add       edx,edi
  004cc122  8b7e20                mov       edi,[esi+20h]
  004cc125  03da                  add       ebx,edx
  004cc127  03fd                  add       edi,ebp
  004cc129  c1f90c                sar       ecx,0Ch
  004cc12c  895e1c                mov       [esi+1Ch],ebx
  004cc12f  8b5e24                mov       ebx,[esi+24h]
  004cc132  03c8                  add       ecx,eax
  004cc134  896e54                mov       [esi+54h],ebp
  004cc137  03d9                  add       ebx,ecx
  004cc139  894658                mov       [esi+58h],eax
  004cc13c  897e20                mov       [esi+20h],edi
  004cc13f  895e24                mov       [esi+24h],ebx
  004cc142  e8f9770600            call      00533940h
  004cc147  8b8690000000          mov       eax,[esi+90h]
  004cc14d  83c404                add       esp,4
  004cc150  a900200020            test      eax,20002000h
  004cc155  7414                  je        short 004CC16Bh
  004cc157  807e420a              cmp       byte [esi+42h],0Ah
  004cc15b  750e                  jne       short 004CC16Bh
  004cc15d  8b4c2448              mov       ecx,[esp+48h]
  004cc161  33db                  xor       ebx,ebx
  004cc163  885e43                mov       [esi+43h],bl
  004cc166  894e20                mov       [esi+20h],ecx
  004cc169  eb02                  jmp       short 004CC16Dh

LAB_004cc16b:
  004cc16b  33db                  xor       ebx,ebx

LAB_004cc16d:
  004cc16d  8a0e                  mov       cl,[esi]
  004cc16f  80e4f8                and       ah,0F8h
  004cc172  f6c120                test      cl,20h
  004cc175  898690000000          mov       [esi+90h],eax
  004cc17b  740b                  je        short 004CC188h
  004cc17d  25ffdfffdf            and       eax,0DFFFDFFFh
  004cc182  898690000000          mov       [esi+90h],eax

LAB_004cc188:
  004cc188  8b4660                mov       eax,[esi+60h]
  004cc18b  8b4e20                mov       ecx,[esi+20h]
  004cc18e  3bc8                  cmp       ecx,eax
  004cc190  7f17                  jg        short 004CC1A9h
  004cc192  8b8e90000000          mov       ecx,[esi+90h]
  004cc198  f6c580                test      ch,80h
  004cc19b  750c                  jne       short 004CC1A9h
  004cc19d  894620                mov       [esi+20h],eax
  004cc1a0  8b06                  mov       eax,[esi]
  004cc1a2  24fe                  and       al,0FEh
  004cc1a4  895e54                mov       [esi+54h],ebx
  004cc1a7  8906                  mov       [esi],eax

LAB_004cc1a9:
  004cc1a9  f7869000000082a80020  test      dword [esi+90h],2000A882h
  004cc1b3  0f84c0000000          je        near 004CC279h
  004cc1b9  8a4e42                mov       cl,[esi+42h]
  004cc1bc  b007                  mov       al,7
  004cc1be  3ac8                  cmp       cl,al
  004cc1c0  7420                  je        short 004CC1E2h
  004cc1c2  884642                mov       [esi+42h],al
  004cc1c5  e876af0700            call      00547140h
  004cc1ca  99                    cdq
  004cc1cb  b928000000            mov       ecx,28h
  004cc1d0  66895e12              mov       [esi+12h],bx
  004cc1d4  f7f9                  idiv      ecx
  004cc1d6  66c746105000          mov       word [esi+10h],50h
  004cc1dc  80c214                add       dl,14h
  004cc1df  885643                mov       [esi+43h],dl

LAB_004cc1e2:
  004cc1e2  8b16                  mov       edx,[esi]
  004cc1e4  8b44244c              mov       eax,[esp+4Ch]
  004cc1e8  8b4c2460              mov       ecx,[esp+60h]
  004cc1ec  83e2fd                and       edx,0FFFFFFFDh
  004cc1ef  894624                mov       [esi+24h],eax
  004cc1f2  8b44243c              mov       eax,[esp+3Ch]
  004cc1f6  8916                  mov       [esi],edx
  004cc1f8  8b542444              mov       edx,[esp+44h]
  004cc1fc  25ffff0000            and       eax,0FFFFh
  004cc201  89561c                mov       [esi+1Ch],edx
  004cc204  8b542450              mov       edx,[esp+50h]
  004cc208  894e7c                mov       [esi+7Ch],ecx
  004cc20b  8b4c245c              mov       ecx,[esp+5Ch]
  004cc20f  898690000000          mov       [esi+90h],eax
  004cc215  8b442434              mov       eax,[esp+34h]
  004cc219  899680000000          mov       [esi+80h],edx
  004cc21f  8b542454              mov       edx,[esp+54h]
  004cc223  898684000000          mov       [esi+84h],eax
  004cc229  8b442440              mov       eax,[esp+40h]
  004cc22d  898e88000000          mov       [esi+88h],ecx
  004cc233  8b4c2438              mov       ecx,[esp+38h]
  004cc237  6882a80020            push      2000A882h
  004cc23c  56                    push      esi
  004cc23d  89968c000000          mov       [esi+8Ch],edx
  004cc243  894664                mov       [esi+64h],eax
  004cc246  894e60                mov       [esi+60h],ecx
  004cc249  e8129c0000            call      004D5E60h
  004cc24e  83c408                add       esp,8
  004cc251  663bc3                cmp       ax,bx
  004cc254  7515                  jne       short 004CC26Bh
  004cc256  e8e5ae0700            call      00547140h
  004cc25b  99                    cdq
  004cc25c  33c2                  xor       eax,edx
  004cc25e  2bc2                  sub       eax,edx
  004cc260  83e001                and       eax,1
  004cc263  33c2                  xor       eax,edx
  004cc265  2bc2                  sub       eax,edx
  004cc267  8d4400ff              lea       eax,[eax+eax-1]

LAB_004cc26b:
  004cc26b  8b542410              mov       edx,[esp+10h]
  004cc26f  83c240                add       edx,40h
  004cc272  0fafd0                imul      edx,eax
  004cc275  66015632              add       [esi+32h],dx

LAB_004cc279:
  004cc279  6aff                  push      0FFFFFFFFh
  004cc27b  6aff                  push      0FFFFFFFFh
  004cc27d  6aff                  push      0FFFFFFFFh
  004cc27f  56                    push      esi
  004cc280  e88b75ffff            call      004C3810h
  004cc285  8b442430              mov       eax,[esp+30h]
  004cc289  83c410                add       esp,10h
  004cc28c  3bc3                  cmp       eax,ebx
  004cc28e  7517                  jne       short 004CC2A7h
  004cc290  668b842486000000      mov       ax,[esp+86h]
  004cc298  66894632              mov       [esi+32h],ax
  004cc29c  5f                    pop       edi
  004cc29d  5e                    pop       esi
  004cc29e  5d                    pop       ebp
  004cc29f  5b                    pop       ebx
  004cc2a0  81c488000000          add       esp,88h
  004cc2a6  c3                    ret

LAB_004cc2a7:
  004cc2a7  385e43                cmp       [esi+43h],bl
  004cc2aa  0f8541030000          jne       near 004CC5F1h
  004cc2b0  8b466c                mov       eax,[esi+6Ch]
  004cc2b3  8b4e1c                mov       ecx,[esi+1Ch]
  004cc2b6  8b5624                mov       edx,[esi+24h]
  004cc2b9  8b781c                mov       edi,[eax+1Ch]
  004cc2bc  8b6824                mov       ebp,[eax+24h]
  004cc2bf  2bf9                  sub       edi,ecx
  004cc2c1  2bea                  sub       ebp,edx
  004cc2c3  c1ff0c                sar       edi,0Ch
  004cc2c6  c1fd0c                sar       ebp,0Ch
  004cc2c9  8bcf                  mov       ecx,edi
  004cc2cb  8bd5                  mov       edx,ebp
  004cc2cd  0fafcf                imul      ecx,edi
  004cc2d0  0fafd5                imul      edx,ebp
  004cc2d3  03ca                  add       ecx,edx
  004cc2d5  51                    push      ecx
  004cc2d6  e835970700            call      00545A10h
  004cc2db  55                    push      ebp
  004cc2dc  57                    push      edi
  004cc2dd  8944241c              mov       [esp+1Ch],eax
  004cc2e1  e83c970700            call      00545A22h
  004cc2e6  668b4e32              mov       cx,[esi+32h]
  004cc2ea  66895e12              mov       [esi+12h],bx
  004cc2ee  2bc1                  sub       eax,ecx
  004cc2f0  25ff0f0000            and       eax,0FFFh
  004cc2f5  89442418              mov       [esp+18h],eax
  004cc2f9  8b442414              mov       eax,[esp+14h]
  004cc2fd  3d96000000            cmp       eax,96h
  004cc302  0f8ddd000000          jge       near 004CC3E5h
  004cc308  e833ae0700            call      00547140h
  004cc30d  99                    cdq
  004cc30e  b964000000            mov       ecx,64h
  004cc313  f7f9                  idiv      ecx
  004cc315  8bea                  mov       ebp,edx
  004cc317  83fd32                cmp       ebp,32h
  004cc31a  7d7e                  jge       short 004CC39Ah
  004cc31c  8b542418              mov       edx,[esp+18h]
  004cc320  8b7c2424              mov       edi,[esp+24h]
  004cc324  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004cc32b  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004cc332  25ff0f0000            and       eax,0FFFh
  004cc337  3bc1                  cmp       eax,ecx
  004cc339  7f55                  jg        short 004CC390h
  004cc33b  e800ae0700            call      00547140h
  004cc340  99                    cdq
  004cc341  8d4c3f01              lea       ecx,[edi+edi+1]
  004cc345  f7f9                  idiv      ecx
  004cc347  8b442414              mov       eax,[esp+14h]
  004cc34b  83c264                add       edx,64h
  004cc34e  3bc2                  cmp       eax,edx
  004cc350  7d3e                  jge       short 004CC390h
  004cc352  83fd1e                cmp       ebp,1Eh
  004cc355  7d24                  jge       short 004CC37Bh
  004cc357  8b16                  mov       edx,[esi]
  004cc359  c646420b              mov       byte [esi+42h],0Bh
  004cc35d  81e2fffffffe          and       edx,0FEFFFFFFh
  004cc363  c6464301              mov       byte [esi+43h],1
  004cc367  83ca02                or        edx,2
  004cc36a  895e5c                mov       [esi+5Ch],ebx
  004cc36d  8916                  mov       [esi],edx
  004cc36f  66c746180300          mov       word [esi+18h],3
  004cc375  66895e1a              mov       [esi+1Ah],bx
  004cc379  eb6e                  jmp       short 004CC3E9h

LAB_004cc37b:
  004cc37b  885e42                mov       [esi+42h],bl
  004cc37e  e8bdad0700            call      00547140h
  004cc383  99                    cdq
  004cc384  b928000000            mov       ecx,28h
  004cc389  f7f9                  idiv      ecx
  004cc38b  885643                mov       [esi+43h],dl
  004cc38e  eb59                  jmp       short 004CC3E9h

LAB_004cc390:
  004cc390  c6464201              mov       byte [esi+42h],1
  004cc394  c6464301              mov       byte [esi+43h],1
  004cc398  eb4f                  jmp       short 004CC3E9h

LAB_004cc39a:
  004cc39a  83fd46                cmp       ebp,46h
  004cc39d  7d3e                  jge       short 004CC3DDh
  004cc39f  c6464203              mov       byte [esi+42h],3
  004cc3a3  66895e12              mov       [esi+12h],bx
  004cc3a7  e894ad0700            call      00547140h
  004cc3ac  99                    cdq
  004cc3ad  b90a000000            mov       ecx,0Ah
  004cc3b2  8b7c2424              mov       edi,[esp+24h]
  004cc3b6  f7f9                  idiv      ecx
  004cc3b8  8bc7                  mov       eax,edi
  004cc3ba  d1f8                  sar       eax,1
  004cc3bc  02d0                  add       dl,al
  004cc3be  80c214                add       dl,14h
  004cc3c1  885643                mov       [esi+43h],dl
  004cc3c4  e877ad0700            call      00547140h
  004cc3c9  99                    cdq
  004cc3ca  33c2                  xor       eax,edx
  004cc3cc  2bc2                  sub       eax,edx
  004cc3ce  25ff0f0000            and       eax,0FFFh
  004cc3d3  33c2                  xor       eax,edx
  004cc3d5  2bc2                  sub       eax,edx
  004cc3d7  66894610              mov       [esi+10h],ax
  004cc3db  eb0c                  jmp       short 004CC3E9h

LAB_004cc3dd:
  004cc3dd  c646420c              mov       byte [esi+42h],0Ch
  004cc3e1  c6464301              mov       byte [esi+43h],1

LAB_004cc3e5:
  004cc3e5  8b7c2424              mov       edi,[esp+24h]

LAB_004cc3e9:
  004cc3e9  8b442414              mov       eax,[esp+14h]
  004cc3ed  3d58020000            cmp       eax,258h
  004cc3f2  0f8d0f010000          jge       near 004CC507h
  004cc3f8  3d96000000            cmp       eax,96h
  004cc3fd  0f8c04010000          jl        near 004CC507h
  004cc403  e838ad0700            call      00547140h
  004cc408  99                    cdq
  004cc409  b964000000            mov       ecx,64h
  004cc40e  f7f9                  idiv      ecx
  004cc410  83fa14                cmp       edx,14h
  004cc413  7d34                  jge       short 004CC449h
  004cc415  8b542418              mov       edx,[esp+18h]
  004cc419  81c200040000          add       edx,400h
  004cc41f  81e2ff0f0000          and       edx,0FFFh
  004cc425  81fa00080000          cmp       edx,800h
  004cc42b  7c13                  jl        short 004CC440h
  004cc42d  66395e16              cmp       [esi+16h],bx
  004cc431  0f85c8000000          jne       near 004CC4FFh
  004cc437  c6464201              mov       byte [esi+42h],1
  004cc43b  e9c3000000            jmp       004CC503h

LAB_004cc440:
  004cc440  c646420c              mov       byte [esi+42h],0Ch
  004cc444  e9ba000000            jmp       004CC503h

LAB_004cc449:
  004cc449  83fa28                cmp       edx,28h
  004cc44c  7d28                  jge       short 004CC476h
  004cc44e  c6464202              mov       byte [esi+42h],2
  004cc452  e8e9ac0700            call      00547140h
  004cc457  99                    cdq
  004cc458  b90a000000            mov       ecx,0Ah
  004cc45d  66c746105000          mov       word [esi+10h],50h
  004cc463  f7f9                  idiv      ecx
  004cc465  8bc7                  mov       eax,edi
  004cc467  d1f8                  sar       eax,1
  004cc469  02d0                  add       dl,al
  004cc46b  80c214                add       dl,14h
  004cc46e  885643                mov       [esi+43h],dl
  004cc471  e991000000            jmp       004CC507h

LAB_004cc476:
  004cc476  83fa50                cmp       edx,50h
  004cc479  7d3a                  jge       short 004CC4B5h
  004cc47b  c6464203              mov       byte [esi+42h],3
  004cc47f  66895e12              mov       [esi+12h],bx
  004cc483  e8b8ac0700            call      00547140h
  004cc488  99                    cdq
  004cc489  b90a000000            mov       ecx,0Ah
  004cc48e  f7f9                  idiv      ecx
  004cc490  8bc7                  mov       eax,edi
  004cc492  d1f8                  sar       eax,1
  004cc494  02d0                  add       dl,al
  004cc496  80c214                add       dl,14h
  004cc499  885643                mov       [esi+43h],dl
  004cc49c  e89fac0700            call      00547140h
  004cc4a1  99                    cdq
  004cc4a2  33c2                  xor       eax,edx
  004cc4a4  2bc2                  sub       eax,edx
  004cc4a6  25ff0f0000            and       eax,0FFFh
  004cc4ab  33c2                  xor       eax,edx
  004cc4ad  2bc2                  sub       eax,edx
  004cc4af  66894610              mov       [esi+10h],ax
  004cc4b3  eb52                  jmp       short 004CC507h

LAB_004cc4b5:
  004cc4b5  b964000000            mov       ecx,64h
  004cc4ba  2bca                  sub       ecx,edx
  004cc4bc  8b542410              mov       edx,[esp+10h]
  004cc4c0  c1fa02                sar       edx,2
  004cc4c3  3bca                  cmp       ecx,edx
  004cc4c5  7e1d                  jle       short 004CC4E4h
  004cc4c7  c646420a              mov       byte [esi+42h],0Ah
  004cc4cb  e870ac0700            call      00547140h
  004cc4d0  99                    cdq
  004cc4d1  b91e000000            mov       ecx,1Eh
  004cc4d6  66895e12              mov       [esi+12h],bx
  004cc4da  f7f9                  idiv      ecx
  004cc4dc  80c214                add       dl,14h
  004cc4df  885643                mov       [esi+43h],dl
  004cc4e2  eb23                  jmp       short 004CC507h

LAB_004cc4e4:
  004cc4e4  66395e16              cmp       [esi+16h],bx
  004cc4e8  7515                  jne       short 004CC4FFh
  004cc4ea  885e42                mov       [esi+42h],bl
  004cc4ed  e84eac0700            call      00547140h
  004cc4f2  99                    cdq
  004cc4f3  b928000000            mov       ecx,28h
  004cc4f8  f7f9                  idiv      ecx
  004cc4fa  885643                mov       [esi+43h],dl
  004cc4fd  eb08                  jmp       short 004CC507h

LAB_004cc4ff:
  004cc4ff  c646420e              mov       byte [esi+42h],0Eh

LAB_004cc503:
  004cc503  c6464301              mov       byte [esi+43h],1

LAB_004cc507:
  004cc507  817c241458020000      cmp       dword [esp+14h],258h
  004cc50f  0f8cdc000000          jl        near 004CC5F1h
  004cc515  e826ac0700            call      00547140h
  004cc51a  bd64000000            mov       ebp,64h
  004cc51f  99                    cdq
  004cc520  8bcd                  mov       ecx,ebp
  004cc522  f7f9                  idiv      ecx
  004cc524  83fa32                cmp       edx,32h
  004cc527  7d45                  jge       short 004CC56Eh
  004cc529  8b442410              mov       eax,[esp+10h]
  004cc52d  8d4c000a              lea       ecx,[eax+eax+0Ah]
  004cc531  3bd1                  cmp       edx,ecx
  004cc533  0f8c97000000          jl        near 004CC5D0h
  004cc539  885e42                mov       [esi+42h],bl
  004cc53c  e8ffab0700            call      00547140h
  004cc541  99                    cdq
  004cc542  b90a000000            mov       ecx,0Ah
  004cc547  66895e12              mov       [esi+12h],bx
  004cc54b  f7f9                  idiv      ecx
  004cc54d  8b4c2410              mov       ecx,[esp+10h]
  004cc551  8bc7                  mov       eax,edi
  004cc553  c1f802                sar       eax,2
  004cc556  02d0                  add       dl,al
  004cc558  885643                mov       [esi+43h],dl
  004cc55b  8d540950              lea       edx,[ecx+ecx+50h]
  004cc55f  66895610              mov       [esi+10h],dx
  004cc563  5f                    pop       edi
  004cc564  5e                    pop       esi
  004cc565  5d                    pop       ebp
  004cc566  5b                    pop       ebx
  004cc567  81c488000000          add       esp,88h
  004cc56d  c3                    ret

LAB_004cc56e:
  004cc56e  83fa4b                cmp       edx,4Bh
  004cc571  7d5d                  jge       short 004CC5D0h
  004cc573  66837e1601            cmp       word [esi+16h],1
  004cc578  7513                  jne       short 004CC58Dh
  004cc57a  c646420c              mov       byte [esi+42h],0Ch
  004cc57e  c6464301              mov       byte [esi+43h],1
  004cc582  5f                    pop       edi
  004cc583  5e                    pop       esi
  004cc584  5d                    pop       ebp
  004cc585  5b                    pop       ebx
  004cc586  81c488000000          add       esp,88h
  004cc58c  c3                    ret

LAB_004cc58d:
  004cc58d  c6464203              mov       byte [esi+42h],3
  004cc591  66895e12              mov       [esi+12h],bx
  004cc595  e8a6ab0700            call      00547140h
  004cc59a  99                    cdq
  004cc59b  b90a000000            mov       ecx,0Ah
  004cc5a0  f7f9                  idiv      ecx
  004cc5a2  8bc7                  mov       eax,edi
  004cc5a4  d1f8                  sar       eax,1
  004cc5a6  02d0                  add       dl,al
  004cc5a8  80c214                add       dl,14h
  004cc5ab  885643                mov       [esi+43h],dl
  004cc5ae  e88dab0700            call      00547140h
  004cc5b3  99                    cdq
  004cc5b4  33c2                  xor       eax,edx
  004cc5b6  2bc2                  sub       eax,edx
  004cc5b8  25ff0f0000            and       eax,0FFFh
  004cc5bd  33c2                  xor       eax,edx
  004cc5bf  2bc2                  sub       eax,edx
  004cc5c1  66894610              mov       [esi+10h],ax
  004cc5c5  5f                    pop       edi
  004cc5c6  5e                    pop       esi
  004cc5c7  5d                    pop       ebp
  004cc5c8  5b                    pop       ebx
  004cc5c9  81c488000000          add       esp,88h
  004cc5cf  c3                    ret

LAB_004cc5d0:
  004cc5d0  c6464202              mov       byte [esi+42h],2
  004cc5d4  e867ab0700            call      00547140h
  004cc5d9  99                    cdq
  004cc5da  b90a000000            mov       ecx,0Ah
  004cc5df  66896e10              mov       [esi+10h],bp
  004cc5e3  f7f9                  idiv      ecx
  004cc5e5  8bc7                  mov       eax,edi
  004cc5e7  d1f8                  sar       eax,1
  004cc5e9  02d0                  add       dl,al
  004cc5eb  80c21e                add       dl,1Eh
  004cc5ee  885643                mov       [esi+43h],dl

LAB_004cc5f1:
  004cc5f1  5f                    pop       edi
  004cc5f2  5e                    pop       esi
  004cc5f3  5d                    pop       ebp
  004cc5f4  5b                    pop       ebx
  004cc5f5  81c488000000          add       esp,88h
  004cc5fb  c3                    ret

LAB_004cc5fc:
  004cc5fc  c6464300              mov       byte [esi+43h],0
  004cc600  e9e5f9ffff            jmp       004CBFEAh
  004cc605  90                    nop
  004cc606  8bff                  mov       edi,edi
  004cc608  b3b7                  mov       bl,0B7h
  004cc60a  4c                    dec       esp
  004cc60b  00ff                  add       bh,bh
  004cc60d  b74c                  mov       bh,4Ch
  004cc60f  0077b8                add       [edi-48h],dh
  004cc612  4c                    dec       esp
  004cc613  0052b9                add       [edx-47h],dl
  004cc616  4c                    dec       esp
  004cc617  00ea                  add       dl,ch
  004cc619  bf4c00eabf            mov       edi,0BFEA004Ch
  004cc61e  4c                    dec       esp
  004cc61f  00ea                  add       dl,ch
  004cc621  bf4c005aba            mov       edi,0BA5A004Ch
  004cc626  4c                    dec       esp
  004cc627  00ea                  add       dl,ch
  004cc629  bf4c00eabf            mov       edi,0BFEA004Ch
  004cc62e  4c                    dec       esp
  004cc62f  00b5ba4c0080          add       [ebp-7FFFB346h],dh
  004cc635  bb4c00e4bb            mov       ebx,0BBE4004Ch
  004cc63a  4c                    dec       esp
  004cc63b  006bbd                add       [ebx-43h],ch
  004cc63e  4c                    dec       esp
  004cc63f  0064be4c              add       [esi+edi*4+4Ch],ah
  004cc643  009090909090          add       [eax-6F6F6F70h],dl
  004cc649  90                    nop
  004cc64a  90                    nop
  004cc64b  90                    nop
  004cc64c  90                    nop
  004cc64d  90                    nop
  004cc64e  90                    nop
  004cc64f  90                    nop

100 cible(s) de saut interne : 0x004cb55b, 0x004cb561, 0x004cb5b2, 0x004cb6f8, 0x004cb722, 0x004cb72a, 0x004cb72c, 0x004cb780, 0x004cb796, 0x004cb79e, 0x004cb7d7, 0x004cb7e7, 0x004cb81c, 0x004cb83f, 0x004cb860, 0x004cb8b0, 0x004cb8cb, 0x004cb8d7, 0x004cb903, 0x004cb90a, 0x004cb90e, 0x004cb969, 0x004cb9eb, 0x004cb9f9, 0x004cb9fd, 0x004cba34, 0x004cba71, 0x004cba7d, 0x004cbac9, 0x004cbaf4, 0x004cbb04, 0x004cbb12, 0x004cbb27, 0x004cbb4f, 0x004cbba0, 0x004cbbc9, 0x004cbc31, 0x004cbc5c, 0x004cbc66, 0x004cbc6a, 0x004cbca7, 0x004cbcaf, 0x004cbcef, 0x004cbcf9, 0x004cbcfd, 0x004cbd1c, 0x004cbd42, 0x004cbd92, 0x004cbda6, 0x004cbdc4, 0x004cbded, 0x004cbe19, 0x004cbe51, 0x004cbe73, 0x004cbe80, 0x004cbe8e, 0x004cbe96, 0x004cbeac, 0x004cbf19, 0x004cbf1e, 0x004cbf61, 0x004cbf65, 0x004cbfb8, 0x004cbfbd, 0x004cbfe8, 0x004cbfea, 0x004cbffc, 0x004cc001, 0x004cc045, 0x004cc05e, 0x004cc066, 0x004cc06b, 0x004cc0b0, 0x004cc16b, 0x004cc16d, 0x004cc188, 0x004cc1a9, 0x004cc1e2, 0x004cc26b, 0x004cc279, 0x004cc2a7, 0x004cc37b, 0x004cc390, 0x004cc39a, 0x004cc3dd, 0x004cc3e5, 0x004cc3e9, 0x004cc440, 0x004cc449, 0x004cc476, 0x004cc4b5, 0x004cc4e4, 0x004cc4ff, 0x004cc503, 0x004cc507, 0x004cc56e, 0x004cc58d, 0x004cc5d0, 0x004cc5f1, 0x004cc5fc
