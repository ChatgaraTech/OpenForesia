Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004944e0  section .text  offset fichier 0x938e0  2096 octets

  004944e0  83ec1c                sub       esp,1Ch
  004944e3  8d442410              lea       eax,[esp+10h]
  004944e7  8d4c2414              lea       ecx,[esp+14h]
  004944eb  8d54240c              lea       edx,[esp+0Ch]
  004944ef  53                    push      ebx
  004944f0  55                    push      ebp
  004944f1  56                    push      esi
  004944f2  8b74242c              mov       esi,[esp+2Ch]
  004944f6  57                    push      edi
  004944f7  50                    push      eax
  004944f8  51                    push      ecx
  004944f9  52                    push      edx
  004944fa  56                    push      esi
  004944fb  e880440a00            call      00538980h
  00494500  83c410                add       esp,10h
  00494503  8d442418              lea       eax,[esp+18h]
  00494507  8d4c2414              lea       ecx,[esp+14h]
  0049450b  8d542430              lea       edx,[esp+30h]
  0049450f  50                    push      eax
  00494510  51                    push      ecx
  00494511  8d442418              lea       eax,[esp+18h]
  00494515  52                    push      edx
  00494516  8d4c2434              lea       ecx,[esp+34h]
  0049451a  50                    push      eax
  0049451b  51                    push      ecx
  0049451c  56                    push      esi
  0049451d  e8fe190a00            call      00535F20h
  00494522  8b44242c              mov       eax,[esp+2Ch]
  00494526  33db                  xor       ebx,ebx
  00494528  83c418                add       esp,18h
  0049452b  3bc3                  cmp       eax,ebx
  0049452d  bf01000000            mov       edi,1
  00494532  0f84f8040000          je        near 00494A30h
  00494538  56                    push      esi
  00494539  e862700400            call      004DB5A0h
  0049453e  8b06                  mov       eax,[esi]
  00494540  83c404                add       esp,4
  00494543  a802                  test      al,2
  00494545  0f84f8020000          je        near 00494843h
  0049454b  8a4c2418              mov       cl,[esp+18h]
  0049454f  ba10000000            mov       edx,10h
  00494554  84ca                  test      dl,cl
  00494556  740c                  je        short 00494564h
  00494558  24fd                  and       al,0FDh
  0049455a  885e43                mov       [esi+43h],bl
  0049455d  8906                  mov       [esi],eax
  0049455f  e9cc040000            jmp       00494A30h

LAB_00494564:
  00494564  33c9                  xor       ecx,ecx
  00494566  8a4e42                mov       cl,[esi+42h]
  00494569  2bcb                  sub       ecx,ebx
  0049456b  0f84ac010000          je        near 0049471Dh
  00494571  49                    dec       ecx
  00494572  0f84fa000000          je        near 00494672h
  00494578  49                    dec       ecx
  00494579  0f85b1040000          jne       near 00494A30h
  0049457f  668b5610              mov       dx,[esi+10h]
  00494583  0fbfca                movsx     ecx,dx
  00494586  2bcb                  sub       ecx,ebx
  00494588  0f8475010000          je        near 00494703h
  0049458e  49                    dec       ecx
  0049458f  0f85a6020000          jne       near 0049483Bh
  00494595  a900000002            test      eax,2000000h
  0049459a  7409                  je        short 004945A5h
  0049459c  4a                    dec       edx
  0049459d  c646430a              mov       byte [esi+43h],0Ah
  004945a1  66895610              mov       [esi+10h],dx

LAB_004945a5:
  004945a5  66837e1a0c            cmp       word [esi+1Ah],0Ch
  004945aa  7518                  jne       short 004945C4h
  004945ac  8d561c                lea       edx,[esi+1Ch]
  004945af  52                    push      edx
  004945b0  6800180000            push      1800h
  004945b5  682a030000            push      32Ah
  004945ba  6a02                  push      2
  004945bc  e85f950a00            call      0053DB20h
  004945c1  83c410                add       esp,10h

LAB_004945c4:
  004945c4  66837e1a1c            cmp       word [esi+1Ah],1Ch
  004945c9  0f856c020000          jne       near 0049483Bh
  004945cf  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  004945d6  0fbf6e34              movsx     ebp,word [esi+34h]
  004945da  8b5e5c                mov       ebx,[esi+5Ch]
  004945dd  6a00                  push      0
  004945df  56                    push      esi
  004945e0  66c7869a0000000000    mov       word [esi+9Ah],0
  004945e9  66c74634800f          mov       word [esi+34h],0F80h
  004945ef  c7465c32000000        mov       dword [esi+5Ch],32h
  004945f6  e875110a00            call      00535770h
  004945fb  0fbf08                movsx     ecx,word [eax]
  004945fe  c1e10c                shl       ecx,0Ch
  00494601  894e44                mov       [esi+44h],ecx
  00494604  83c408                add       esp,8
  00494607  0fbf5002              movsx     edx,word [eax+2]
  0049460b  c1e20c                shl       edx,0Ch
  0049460e  895648                mov       [esi+48h],edx
  00494611  6a02                  push      2
  00494613  0fbf4004              movsx     eax,word [eax+4]
  00494617  c1e00c                shl       eax,0Ch
  0049461a  89464c                mov       [esi+4Ch],eax
  0049461d  8b0dcc975e00          mov       ecx,[5E97CCh]
  00494623  6a00                  push      0
  00494625  8b91cc000000          mov       edx,[ecx+0CCh]
  0049462b  33c9                  xor       ecx,ecx
  0049462d  8b02                  mov       eax,[edx]
  0049462f  8a480d                mov       cl,[eax+0Dh]
  00494632  81c1c0000000          add       ecx,0C0h
  00494638  51                    push      ecx
  00494639  56                    push      esi
  0049463a  e89163feff            call      0047A9D0h
  0049463f  83c410                add       esp,10h
  00494642  8d561c                lea       edx,[esi+1Ch]
  00494645  6689be9a000000        mov       [esi+9Ah],di
  0049464c  66896e34              mov       [esi+34h],bp
  00494650  52                    push      edx
  00494651  6800180000            push      1800h
  00494656  6853060000            push      653h
  0049465b  6a20                  push      20h
  0049465d  895e5c                mov       [esi+5Ch],ebx
  00494660  e8bb940a00            call      0053DB20h
  00494665  83c410                add       esp,10h
  00494668  33db                  xor       ebx,ebx
  0049466a  895e5c                mov       [esi+5Ch],ebx
  0049466d  e9be030000            jmp       00494A30h

LAB_00494672:
  00494672  0fbf4e10              movsx     ecx,word [esi+10h]
  00494676  2bcb                  sub       ecx,ebx
  00494678  0f8485000000          je        near 00494703h
  0049467e  49                    dec       ecx
  0049467f  0f85b6010000          jne       near 0049483Bh
  00494685  6639561a              cmp       [esi+1Ah],dx
  00494689  7518                  jne       short 004946A3h
  0049468b  8d461c                lea       eax,[esi+1Ch]
  0049468e  50                    push      eax
  0049468f  6800180000            push      1800h
  00494694  6800020000            push      200h
  00494699  6a41                  push      41h
  0049469b  e880940a00            call      0053DB20h
  004946a0  83c410                add       esp,10h

LAB_004946a3:
  004946a3  f70600000002          test      dword [esi],2000000h
  004946a9  0f848c010000          je        near 0049483Bh
  004946af  8b7e44                mov       edi,[esi+44h]
  004946b2  8b6e48                mov       ebp,[esi+48h]
  004946b5  e8862a0b00            call      00547140h
  004946ba  83e001                and       eax,1
  004946bd  c74648983a0000        mov       dword [esi+48h],3A98h
  004946c4  894644                mov       [esi+44h],eax
  004946c7  8b0dcc975e00          mov       ecx,[5E97CCh]
  004946cd  6a02                  push      2
  004946cf  53                    push      ebx
  004946d0  8b91cc000000          mov       edx,[ecx+0CCh]
  004946d6  33c9                  xor       ecx,ecx
  004946d8  8b02                  mov       eax,[edx]
  004946da  8a480e                mov       cl,[eax+0Eh]
  004946dd  81c1c0000000          add       ecx,0C0h
  004946e3  51                    push      ecx
  004946e4  56                    push      esi
  004946e5  e8e662feff            call      0047A9D0h
  004946ea  83c410                add       esp,10h
  004946ed  66ff4e10              dec       word [esi+10h]
  004946f1  897e44                mov       [esi+44h],edi
  004946f4  896e48                mov       [esi+48h],ebp
  004946f7  c646430a              mov       byte [esi+43h],0Ah
  004946fb  895e5c                mov       [esi+5Ch],ebx
  004946fe  e92d030000            jmp       00494A30h

LAB_00494703:
  00494703  8a4e43                mov       cl,[esi+43h]
  00494706  fec9                  dec       cl
  00494708  884e43                mov       [esi+43h],cl
  0049470b  0f852a010000          jne       near 0049483Bh
  00494711  24fd                  and       al,0FDh
  00494713  895e5c                mov       [esi+5Ch],ebx
  00494716  8906                  mov       [esi],eax
  00494718  e913030000            jmp       00494A30h

LAB_0049471d:
  0049471d  668b4e1a              mov       cx,[esi+1Ah]
  00494721  6683f905              cmp       cx,5
  00494725  7d38                  jge       short 0049475Fh
  00494727  8b442430              mov       eax,[esp+30h]
  0049472b  8d5040                lea       edx,[eax+40h]
  0049472e  81e2ff0f0000          and       edx,0FFFh
  00494734  81fa80000000          cmp       edx,80h
  0049473a  7e1b                  jle       short 00494757h
  0049473c  25ff0f0000            and       eax,0FFFh
  00494741  33d2                  xor       edx,edx
  00494743  3d00080000            cmp       eax,800h
  00494748  0f9dc2                setge     dl
  0049474b  4a                    dec       edx
  0049474c  81e280000000          and       edx,80h
  00494752  83c2c0                add       edx,0FFFFFFC0h
  00494755  8bc2                  mov       eax,edx

LAB_00494757:
  00494757  66014632              add       [esi+32h],ax
  0049475b  6683f905              cmp       cx,5

LAB_0049475f:
  0049475f  7554                  jne       short 004947B5h
  00494761  0fbf4632              movsx     eax,word [esi+32h]
  00494765  50                    push      eax
  00494766  c7465c00800200        mov       dword [esi+5Ch],28000h
  0049476d  e8aa120b00            call      00545A1Ch
  00494772  0faf465c              imul      eax,[esi+5Ch]
  00494776  0fbf4e32              movsx     ecx,word [esi+32h]
  0049477a  8b6e50                mov       ebp,[esi+50h]
  0049477d  51                    push      ecx
  0049477e  c1f80c                sar       eax,0Ch
  00494781  03e8                  add       ebp,eax
  00494783  896e50                mov       [esi+50h],ebp
  00494786  e88b120b00            call      00545A16h
  0049478b  0faf465c              imul      eax,[esi+5Ch]
  0049478f  8b4e58                mov       ecx,[esi+58h]
  00494792  8d561c                lea       edx,[esi+1Ch]
  00494795  c1f80c                sar       eax,0Ch
  00494798  52                    push      edx
  00494799  6800180000            push      1800h
  0049479e  03c8                  add       ecx,eax
  004947a0  682a030000            push      32Ah
  004947a5  6a05                  push      5
  004947a7  894e58                mov       [esi+58h],ecx
  004947aa  895e5c                mov       [esi+5Ch],ebx
  004947ad  e86e930a00            call      0053DB20h
  004947b2  83c410                add       esp,10h

LAB_004947b5:
  004947b5  bd06000000            mov       ebp,6
  004947ba  66396e1a              cmp       [esi+1Ah],bp
  004947be  7c6e                  jl        short 0049482Eh
  004947c0  53                    push      ebx
  004947c1  8d461c                lea       eax,[esi+1Ch]
  004947c4  68d8285500            push      5528D8h
  004947c9  50                    push      eax
  004947ca  56                    push      esi
  004947cb  e860540800            call      00519C30h
  004947d0  8bf8                  mov       edi,eax
  004947d2  a1b8e05d00            mov       eax,[5DE0B8h]
  004947d7  83c410                add       esp,10h
  004947da  3bc3                  cmp       eax,ebx
  004947dc  7450                  je        short 0049482Eh
  004947de  8b07                  mov       eax,[edi]
  004947e0  57                    push      edi
  004947e1  0c40                  or        al,40h
  004947e3  56                    push      esi
  004947e4  6689af96000000        mov       [edi+96h],bp
  004947eb  8907                  mov       [edi],eax
  004947ed  e88e530a00            call      00539B80h
  004947f2  83c408                add       esp,8
  004947f5  e846290b00            call      00547140h
  004947fa  99                    cdq
  004947fb  b964000000            mov       ecx,64h
  00494800  f7f9                  idiv      ecx
  00494802  83fa0a                cmp       edx,0Ah
  00494805  7d0f                  jge       short 00494816h
  00494807  53                    push      ebx
  00494808  57                    push      edi
  00494809  56                    push      esi
  0049480a  c6460707              mov       byte [esi+7],7
  0049480e  e8fd6f0a00            call      0053B810h
  00494813  83c40c                add       esp,0Ch

LAB_00494816:
  00494816  83c71c                add       edi,1Ch
  00494819  57                    push      edi
  0049481a  6800180000            push      1800h
  0049481f  6800030000            push      300h
  00494824  6a03                  push      3
  00494826  e8f5920a00            call      0053DB20h
  0049482b  83c410                add       esp,10h

LAB_0049482e:
  0049482e  8b06                  mov       eax,[esi]
  00494830  a900000002            test      eax,2000000h
  00494835  7404                  je        short 0049483Bh
  00494837  24fd                  and       al,0FDh
  00494839  8906                  mov       [esi],eax

LAB_0049483b:
  0049483b  895e5c                mov       [esi+5Ch],ebx
  0049483e  e9ed010000            jmp       00494A30h

LAB_00494843:
  00494843  a811                  test      al,11h
  00494845  0f85e5010000          jne       near 00494A30h
  0049484b  a904080000            test      eax,804h
  00494850  0f85da010000          jne       near 00494A30h
  00494856  33c9                  xor       ecx,ecx
  00494858  8a4e42                mov       cl,[esi+42h]
  0049485b  83f903                cmp       ecx,3
  0049485e  0f879e010000          ja        near 00494A02h
  00494864  ff248df44c4900        jmp       dword [ecx*4+494CF4h]
  0049486b  66395e18              cmp       [esi+18h],bx
  0049486f  740a                  je        short 0049487Bh
  00494871  0d00000001            or        eax,1000000h
  00494876  895e18                mov       [esi+18h],ebx
  00494879  8906                  mov       [esi],eax

LAB_0049487b:
  0049487b  817c24102c010000      cmp       dword [esp+10h],12Ch
  00494883  7d09                  jge       short 0049488Eh
  00494885  c6464300              mov       byte [esi+43h],0
  00494889  e974010000            jmp       00494A02h

LAB_0049488e:
  0049488e  8a4643                mov       al,[esi+43h]
  00494891  84c0                  test      al,al
  00494893  7464                  je        short 004948F9h
  00494895  fec8                  dec       al
  00494897  895e5c                mov       [esi+5Ch],ebx
  0049489a  884643                mov       [esi+43h],al
  0049489d  e960010000            jmp       00494A02h
  004948a2  66397e18              cmp       [esi+18h],di
  004948a6  740a                  je        short 004948B2h
  004948a8  0d00000001            or        eax,1000000h
  004948ad  897e18                mov       [esi+18h],edi
  004948b0  8906                  mov       [esi],eax

LAB_004948b2:
  004948b2  8b442430              mov       eax,[esp+30h]
  004948b6  8d9080000000          lea       edx,[eax+80h]
  004948bc  81e2ff0f0000          and       edx,0FFFh
  004948c2  81fa00010000          cmp       edx,100h
  004948c8  7f10                  jg        short 004948DAh
  004948ca  66014632              add       [esi+32h],ax
  004948ce  c6464300              mov       byte [esi+43h],0
  004948d2  895e5c                mov       [esi+5Ch],ebx
  004948d5  e928010000            jmp       00494A02h

LAB_004948da:
  004948da  25ff0f0000            and       eax,0FFFh
  004948df  33c9                  xor       ecx,ecx
  004948e1  3d00080000            cmp       eax,800h
  004948e6  0f9dc1                setge     cl
  004948e9  49                    dec       ecx
  004948ea  81e100010000          and       ecx,100h
  004948f0  83c180                add       ecx,0FFFFFF80h
  004948f3  8bc1                  mov       eax,ecx
  004948f5  66014632              add       [esi+32h],ax

LAB_004948f9:
  004948f9  895e5c                mov       [esi+5Ch],ebx
  004948fc  e901010000            jmp       00494A02h
  00494901  66397e18              cmp       [esi+18h],di
  00494905  740a                  je        short 00494911h
  00494907  0d00000001            or        eax,1000000h
  0049490c  897e18                mov       [esi+18h],edi
  0049490f  8906                  mov       [esi],eax

LAB_00494911:
  00494911  0fbf5610              movsx     edx,word [esi+10h]
  00494915  39542410              cmp       [esp+10h],edx
  00494919  7d09                  jge       short 00494924h
  0049491b  c6464300              mov       byte [esi+43h],0
  0049491f  e9de000000            jmp       00494A02h

LAB_00494924:
  00494924  8a4643                mov       al,[esi+43h]
  00494927  84c0                  test      al,al
  00494929  7405                  je        short 00494930h
  0049492b  fec8                  dec       al
  0049492d  884643                mov       [esi+43h],al

LAB_00494930:
  00494930  8b442430              mov       eax,[esp+30h]
  00494934  8d8880000000          lea       ecx,[eax+80h]
  0049493a  81e1ff0f0000          and       ecx,0FFFh
  00494940  81f900010000          cmp       ecx,100h
  00494946  7e1b                  jle       short 00494963h
  00494948  25ff0f0000            and       eax,0FFFh
  0049494d  33d2                  xor       edx,edx
  0049494f  3d00080000            cmp       eax,800h
  00494954  0f9dc2                setge     dl
  00494957  4a                    dec       edx
  00494958  81e200010000          and       edx,100h
  0049495e  83c280                add       edx,0FFFFFF80h
  00494961  8bc2                  mov       eax,edx

LAB_00494963:
  00494963  66014632              add       [esi+32h],ax
  00494967  c7465c00200100        mov       dword [esi+5Ch],12000h
  0049496e  e98f000000            jmp       00494A02h
  00494973  8a4e43                mov       cl,[esi+43h]
  00494976  84c9                  test      cl,cl
  00494978  7405                  je        short 0049497Fh
  0049497a  fec9                  dec       cl
  0049497c  884e43                mov       [esi+43h],cl

LAB_0049497f:
  0049497f  66397e18              cmp       [esi+18h],di
  00494983  740a                  je        short 0049498Fh
  00494985  0d00000001            or        eax,1000000h
  0049498a  897e18                mov       [esi+18h],edi
  0049498d  8906                  mov       [esi],eax

LAB_0049498f:
  0049498f  0fbf4610              movsx     eax,word [esi+10h]
  00494993  50                    push      eax
  00494994  c7465c00e00000        mov       dword [esi+5Ch],0E000h
  0049499b  e87c100b00            call      00545A1Ch
  004949a0  0faf465c              imul      eax,[esi+5Ch]
  004949a4  8b4e1c                mov       ecx,[esi+1Ch]
  004949a7  c1f80c                sar       eax,0Ch
  004949aa  03c8                  add       ecx,eax
  004949ac  894e1c                mov       [esi+1Ch],ecx
  004949af  0fbf4e10              movsx     ecx,word [esi+10h]
  004949b3  51                    push      ecx
  004949b4  e85d100b00            call      00545A16h
  004949b9  0faf465c              imul      eax,[esi+5Ch]
  004949bd  8b4e24                mov       ecx,[esi+24h]
  004949c0  895e5c                mov       [esi+5Ch],ebx
  004949c3  c1f80c                sar       eax,0Ch
  004949c6  03c8                  add       ecx,eax
  004949c8  8b442430              mov       eax,[esp+30h]
  004949cc  894e24                mov       [esi+24h],ecx
  004949cf  8d9080000000          lea       edx,[eax+80h]
  004949d5  81e2ff0f0000          and       edx,0FFFh
  004949db  81fa00010000          cmp       edx,100h
  004949e1  7e1b                  jle       short 004949FEh
  004949e3  25ff0f0000            and       eax,0FFFh
  004949e8  33c9                  xor       ecx,ecx
  004949ea  3d00080000            cmp       eax,800h
  004949ef  0f9dc1                setge     cl
  004949f2  49                    dec       ecx
  004949f3  81e100010000          and       ecx,100h
  004949f9  83c180                add       ecx,0FFFFFF80h
  004949fc  8bc1                  mov       eax,ecx

LAB_004949fe:
  004949fe  66014632              add       [esi+32h],ax

LAB_00494a02:
  00494a02  66397e18              cmp       [esi+18h],di
  00494a06  7528                  jne       short 00494A30h
  00494a08  668b461a              mov       ax,[esi+1Ah]
  00494a0c  663d0a00              cmp       ax,0Ah
  00494a10  7406                  je        short 00494A18h
  00494a12  663d1700              cmp       ax,17h
  00494a16  7518                  jne       short 00494A30h

LAB_00494a18:
  00494a18  8d561c                lea       edx,[esi+1Ch]
  00494a1b  52                    push      edx
  00494a1c  6800180000            push      1800h
  00494a21  6800020000            push      200h
  00494a26  6a35                  push      35h
  00494a28  e8f3900a00            call      0053DB20h
  00494a2d  83c410                add       esp,10h

LAB_00494a30:
  00494a30  8b442414              mov       eax,[esp+14h]
  00494a34  66c7869a000000ff7f    mov       word [esi+9Ah],7FFFh
  00494a3d  50                    push      eax
  00494a3e  56                    push      esi
  00494a3f  e81c37ffff            call      00488160h
  00494a44  83c408                add       esp,8
  00494a47  56                    push      esi
  00494a48  e8f3ee0900            call      00533940h
  00494a4d  83c404                add       esp,4
  00494a50  56                    push      esi
  00494a51  e89ab00400            call      004DFAF0h
  00494a56  8b4c2420              mov       ecx,[esp+20h]
  00494a5a  83c404                add       esp,4
  00494a5d  51                    push      ecx
  00494a5e  56                    push      esi
  00494a5f  e80ced0400            call      004E3770h
  00494a64  8b44241c              mov       eax,[esp+1Ch]
  00494a68  83c408                add       esp,8
  00494a6b  3bc3                  cmp       eax,ebx
  00494a6d  0f8476020000          je        near 00494CE9h
  00494a73  f70617080000          test      dword [esi],817h
  00494a79  0f856a020000          jne       near 00494CE9h
  00494a7f  8a4643                mov       al,[esi+43h]
  00494a82  84c0                  test      al,al
  00494a84  0f855f020000          jne       near 00494CE9h
  00494a8a  8b466c                mov       eax,[esi+6Ch]
  00494a8d  8b5e1c                mov       ebx,[esi+1Ch]
  00494a90  8b4e24                mov       ecx,[esi+24h]
  00494a93  8b781c                mov       edi,[eax+1Ch]
  00494a96  8b6824                mov       ebp,[eax+24h]
  00494a99  2bfb                  sub       edi,ebx
  00494a9b  2be9                  sub       ebp,ecx
  00494a9d  c1ff0c                sar       edi,0Ch
  00494aa0  c1fd0c                sar       ebp,0Ch
  00494aa3  8bd7                  mov       edx,edi
  00494aa5  8bc5                  mov       eax,ebp
  00494aa7  0fafd7                imul      edx,edi
  00494aaa  0fafc5                imul      eax,ebp
  00494aad  03d0                  add       edx,eax
  00494aaf  52                    push      edx
  00494ab0  e85b0f0b00            call      00545A10h
  00494ab5  55                    push      ebp
  00494ab6  57                    push      edi
  00494ab7  89442418              mov       [esp+18h],eax
  00494abb  e8620f0b00            call      00545A22h
  00494ac0  668b4e32              mov       cx,[esi+32h]
  00494ac4  2bc1                  sub       eax,ecx
  00494ac6  25ff0f0000            and       eax,0FFFh
  00494acb  89442430              mov       [esp+30h],eax
  00494acf  e86c260b00            call      00547140h
  00494ad4  bf64000000            mov       edi,64h
  00494ad9  99                    cdq
  00494ada  8bcf                  mov       ecx,edi
  00494adc  f7f9                  idiv      ecx
  00494ade  817c24102c010000      cmp       dword [esp+10h],12Ch
  00494ae6  0f8d01010000          jge       near 00494BEDh
  00494aec  83fa32                cmp       edx,32h
  00494aef  7d3a                  jge       short 00494B2Bh
  00494af1  8b542430              mov       edx,[esp+30h]
  00494af5  81c200010000          add       edx,100h
  00494afb  81e2ff0f0000          and       edx,0FFFh
  00494b01  81fa00020000          cmp       edx,200h
  00494b07  0f8f4b010000          jg        near 00494C58h
  00494b0d  8b06                  mov       eax,[esi]
  00494b0f  c7461803000000        mov       dword [esi+18h],3
  00494b16  25fffffffe            and       eax,0FEFFFFFFh
  00494b1b  c6464200              mov       byte [esi+42h],0
  00494b1f  0c02                  or        al,2
  00494b21  8906                  mov       [esi],eax
  00494b23  5f                    pop       edi
  00494b24  5e                    pop       esi
  00494b25  5d                    pop       ebp
  00494b26  5b                    pop       ebx
  00494b27  83c41c                add       esp,1Ch
  00494b2a  c3                    ret

LAB_00494b2b:
  00494b2b  83fa4b                cmp       edx,4Bh
  00494b2e  7d22                  jge       short 00494B52h
  00494b30  c6464202              mov       byte [esi+42h],2
  00494b34  e807260b00            call      00547140h
  00494b39  99                    cdq
  00494b3a  b90a000000            mov       ecx,0Ah
  00494b3f  66897e10              mov       [esi+10h],di
  00494b43  f7f9                  idiv      ecx
  00494b45  02d1                  add       dl,cl
  00494b47  885643                mov       [esi+43h],dl
  00494b4a  5f                    pop       edi
  00494b4b  5e                    pop       esi
  00494b4c  5d                    pop       ebp
  00494b4d  5b                    pop       ebx
  00494b4e  83c41c                add       esp,1Ch
  00494b51  c3                    ret

LAB_00494b52:
  00494b52  83fa5a                cmp       edx,5Ah
  00494b55  7d57                  jge       short 00494BAEh
  00494b57  8b542430              mov       edx,[esp+30h]
  00494b5b  81c200010000          add       edx,100h
  00494b61  81e2ff0f0000          and       edx,0FFFh
  00494b67  81fa00020000          cmp       edx,200h
  00494b6d  7f3f                  jg        short 00494BAEh
  00494b6f  c6464203              mov       byte [esi+42h],3
  00494b73  e8c8250b00            call      00547140h
  00494b78  99                    cdq
  00494b79  b90a000000            mov       ecx,0Ah
  00494b7e  f7f9                  idiv      ecx
  00494b80  02d1                  add       dl,cl
  00494b82  885643                mov       [esi+43h],dl
  00494b85  e8b6250b00            call      00547140h
  00494b8a  99                    cdq
  00494b8b  b9000c0000            mov       ecx,0C00h
  00494b90  8b7c2430              mov       edi,[esp+30h]
  00494b94  f7f9                  idiv      ecx
  00494b96  03d7                  add       edx,edi
  00494b98  66035632              add       dx,[esi+32h]
  00494b9c  81c200020000          add       edx,200h
  00494ba2  66895610              mov       [esi+10h],dx
  00494ba6  5f                    pop       edi
  00494ba7  5e                    pop       esi
  00494ba8  5d                    pop       ebp
  00494ba9  5b                    pop       ebx
  00494baa  83c41c                add       esp,1Ch
  00494bad  c3                    ret

LAB_00494bae:
  00494bae  c6464203              mov       byte [esi+42h],3
  00494bb2  e889250b00            call      00547140h
  00494bb7  99                    cdq
  00494bb8  b90a000000            mov       ecx,0Ah
  00494bbd  f7f9                  idiv      ecx
  00494bbf  02d1                  add       dl,cl
  00494bc1  885643                mov       [esi+43h],dl
  00494bc4  e877250b00            call      00547140h
  00494bc9  99                    cdq
  00494bca  b9000c0000            mov       ecx,0C00h
  00494bcf  8b7c2430              mov       edi,[esp+30h]
  00494bd3  f7f9                  idiv      ecx
  00494bd5  03d7                  add       edx,edi
  00494bd7  66035632              add       dx,[esi+32h]
  00494bdb  81c200020000          add       edx,200h
  00494be1  66895610              mov       [esi+10h],dx
  00494be5  5f                    pop       edi
  00494be6  5e                    pop       esi
  00494be7  5d                    pop       ebp
  00494be8  5b                    pop       ebx
  00494be9  83c41c                add       esp,1Ch
  00494bec  c3                    ret

LAB_00494bed:
  00494bed  83fa2d                cmp       edx,2Dh
  00494bf0  7d76                  jge       short 00494C68h
  00494bf2  8b442430              mov       eax,[esp+30h]
  00494bf6  0500020000            add       eax,200h
  00494bfb  25ff0f0000            and       eax,0FFFh
  00494c00  3d00040000            cmp       eax,400h
  00494c05  7f51                  jg        short 00494C58h
  00494c07  f6c201                test      dl,1
  00494c0a  7426                  je        short 00494C32h
  00494c0c  8b0e                  mov       ecx,[esi]
  00494c0e  c7461806000000        mov       dword [esi+18h],6
  00494c15  81e1fffffffe          and       ecx,0FEFFFFFFh
  00494c1b  c6464201              mov       byte [esi+42h],1
  00494c1f  83c902                or        ecx,2
  00494c22  66c746100100          mov       word [esi+10h],1
  00494c28  890e                  mov       [esi],ecx
  00494c2a  5f                    pop       edi
  00494c2b  5e                    pop       esi
  00494c2c  5d                    pop       ebp
  00494c2d  5b                    pop       ebx
  00494c2e  83c41c                add       esp,1Ch
  00494c31  c3                    ret

LAB_00494c32:
  00494c32  8b16                  mov       edx,[esi]
  00494c34  c7461804000000        mov       dword [esi+18h],4
  00494c3b  81e2fffffffe          and       edx,0FEFFFFFFh
  00494c41  c6464202              mov       byte [esi+42h],2
  00494c45  83ca02                or        edx,2
  00494c48  66c746100100          mov       word [esi+10h],1
  00494c4e  8916                  mov       [esi],edx
  00494c50  5f                    pop       edi
  00494c51  5e                    pop       esi
  00494c52  5d                    pop       ebp
  00494c53  5b                    pop       ebx
  00494c54  83c41c                add       esp,1Ch
  00494c57  c3                    ret

LAB_00494c58:
  00494c58  c6464201              mov       byte [esi+42h],1
  00494c5c  c6464301              mov       byte [esi+43h],1
  00494c60  5f                    pop       edi
  00494c61  5e                    pop       esi
  00494c62  5d                    pop       ebp
  00494c63  5b                    pop       ebx
  00494c64  83c41c                add       esp,1Ch
  00494c67  c3                    ret

LAB_00494c68:
  00494c68  83fa46                cmp       edx,46h
  00494c6b  7d3f                  jge       short 00494CACh
  00494c6d  c6464203              mov       byte [esi+42h],3
  00494c71  e8ca240b00            call      00547140h
  00494c76  99                    cdq
  00494c77  b90a000000            mov       ecx,0Ah
  00494c7c  f7f9                  idiv      ecx
  00494c7e  02d1                  add       dl,cl
  00494c80  885643                mov       [esi+43h],dl
  00494c83  e8b8240b00            call      00547140h
  00494c88  99                    cdq
  00494c89  b9000c0000            mov       ecx,0C00h
  00494c8e  8b7c2430              mov       edi,[esp+30h]
  00494c92  f7f9                  idiv      ecx
  00494c94  03d7                  add       edx,edi
  00494c96  66035632              add       dx,[esi+32h]
  00494c9a  81ea00060000          sub       edx,600h
  00494ca0  66895610              mov       [esi+10h],dx
  00494ca4  5f                    pop       edi
  00494ca5  5e                    pop       esi
  00494ca6  5d                    pop       ebp
  00494ca7  5b                    pop       ebx
  00494ca8  83c41c                add       esp,1Ch
  00494cab  c3                    ret

LAB_00494cac:
  00494cac  83fa5a                cmp       edx,5Ah
  00494caf  7d22                  jge       short 00494CD3h
  00494cb1  c6464202              mov       byte [esi+42h],2
  00494cb5  e886240b00            call      00547140h
  00494cba  99                    cdq
  00494cbb  b90a000000            mov       ecx,0Ah
  00494cc0  66897e10              mov       [esi+10h],di
  00494cc4  f7f9                  idiv      ecx
  00494cc6  02d1                  add       dl,cl
  00494cc8  885643                mov       [esi+43h],dl
  00494ccb  5f                    pop       edi
  00494ccc  5e                    pop       esi
  00494ccd  5d                    pop       ebp
  00494cce  5b                    pop       ebx
  00494ccf  83c41c                add       esp,1Ch
  00494cd2  c3                    ret

LAB_00494cd3:
  00494cd3  c6464200              mov       byte [esi+42h],0
  00494cd7  e864240b00            call      00547140h
  00494cdc  99                    cdq
  00494cdd  b90a000000            mov       ecx,0Ah
  00494ce2  f7f9                  idiv      ecx
  00494ce4  02d1                  add       dl,cl
  00494ce6  885643                mov       [esi+43h],dl

LAB_00494ce9:
  00494ce9  5f                    pop       edi
  00494cea  5e                    pop       esi
  00494ceb  5d                    pop       ebp
  00494cec  5b                    pop       ebx
  00494ced  83c41c                add       esp,1Ch
  00494cf0  c3                    ret
  00494cf1  90                    nop
  00494cf2  8bff                  mov       edi,edi
  00494cf4  6b484900              imul      ecx,[eax+49h],0
  00494cf8  a248490001            mov       [1004948h],al
  00494cfd  49                    dec       ecx
  00494cfe  49                    dec       ecx
  00494cff  007349                add       [ebx+49h],dh
  00494d02  49                    dec       ecx
  00494d03  009090909090          add       [eax-6F6F6F70h],dl
  00494d09  90                    nop
  00494d0a  90                    nop
  00494d0b  90                    nop
  00494d0c  90                    nop
  00494d0d  90                    nop
  00494d0e  90                    nop
  00494d0f  90                    nop

39 cible(s) de saut interne : 0x00494564, 0x004945a5, 0x004945c4, 0x00494672, 0x004946a3, 0x00494703, 0x0049471d, 0x00494757, 0x0049475f, 0x004947b5, 0x00494816, 0x0049482e, 0x0049483b, 0x00494843, 0x0049487b, 0x0049488e, 0x004948b2, 0x004948da, 0x004948f9, 0x00494911, 0x00494924, 0x00494930, 0x00494963, 0x0049497f, 0x0049498f, 0x004949fe, 0x00494a02, 0x00494a18, 0x00494a30, 0x00494b2b, 0x00494b52, 0x00494bae, 0x00494bed, 0x00494c32, 0x00494c58, 0x00494c68, 0x00494cac, 0x00494cd3, 0x00494ce9
