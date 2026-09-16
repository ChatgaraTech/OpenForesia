Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0048e3a0  section .text  offset fichier 0x8d7a0  3728 octets

  0048e3a0  81ec88000000          sub       esp,88h
  0048e3a6  53                    push      ebx
  0048e3a7  55                    push      ebp
  0048e3a8  56                    push      esi
  0048e3a9  8bb42498000000        mov       esi,[esp+98h]
  0048e3b0  b864000000            mov       eax,64h
  0048e3b5  33ed                  xor       ebp,ebp
  0048e3b7  668944242a            mov       [esp+2Ah],ax
  0048e3bc  668944242e            mov       [esp+2Eh],ax
  0048e3c1  8a4641                mov       al,[esi+41h]
  0048e3c4  57                    push      edi
  0048e3c5  84c0                  test      al,al
  0048e3c7  66896c2428            mov       [esp+28h],bp
  0048e3cc  66c744242a3200        mov       word [esp+2Ah],32h
  0048e3d3  66c744242c5000        mov       word [esp+2Ch],50h
  0048e3da  66896c2430            mov       [esp+30h],bp
  0048e3df  754a                  jne       short 0048E42Bh
  0048e3e1  e85a8d0b00            call      00547140h
  0048e3e6  99                    cdq
  0048e3e7  33c2                  xor       eax,edx
  0048e3e9  55                    push      ebp
  0048e3ea  2bc2                  sub       eax,edx
  0048e3ec  6a05                  push      5
  0048e3ee  25ff0f0000            and       eax,0FFFh
  0048e3f3  56                    push      esi
  0048e3f4  33c2                  xor       eax,edx
  0048e3f6  c6464101              mov       byte [esi+41h],1
  0048e3fa  2bc2                  sub       eax,edx
  0048e3fc  66894614              mov       [esi+14h],ax
  0048e400  e86bddffff            call      0048C170h
  0048e405  8b4620                mov       eax,[esi+20h]
  0048e408  83c40c                add       esp,0Ch
  0048e40b  894648                mov       [esi+48h],eax
  0048e40e  668b4616              mov       ax,[esi+16h]
  0048e412  663d0a00              cmp       ax,0Ah
  0048e416  7c0d                  jl        short 0048E425h
  0048e418  83c0f6                add       eax,0FFFFFFF6h
  0048e41b  66894616              mov       [esi+16h],ax
  0048e41f  8b06                  mov       eax,[esi]
  0048e421  24df                  and       al,0DFh
  0048e423  eb04                  jmp       short 0048E429h

LAB_0048e425:
  0048e425  8b06                  mov       eax,[esi]
  0048e427  0c20                  or        al,20h

LAB_0048e429:
  0048e429  8906                  mov       [esi],eax

LAB_0048e42b:
  0048e42b  8b16                  mov       edx,[esi]
  0048e42d  8b4e1c                mov       ecx,[esi+1Ch]
  0048e430  8b4664                mov       eax,[esi+64h]
  0048e433  8d7e1c                lea       edi,[esi+1Ch]
  0048e436  83e2fe                and       edx,0FFFFFFFEh
  0048e439  894c2458              mov       [esp+58h],ecx
  0048e43d  8b4e60                mov       ecx,[esi+60h]
  0048e440  8916                  mov       [esi],edx
  0048e442  8b5624                mov       edx,[esi+24h]
  0048e445  89442440              mov       [esp+40h],eax
  0048e449  8b467c                mov       eax,[esi+7Ch]
  0048e44c  89542460              mov       [esp+60h],edx
  0048e450  668b5632              mov       dx,[esi+32h]
  0048e454  894c2438              mov       [esp+38h],ecx
  0048e458  668b8e90000000        mov       cx,[esi+90h]
  0048e45f  6689942486000000      mov       [esp+86h],dx
  0048e467  8b9680000000          mov       edx,[esi+80h]
  0048e46d  8b5e6c                mov       ebx,[esi+6Ch]
  0048e470  89442448              mov       [esp+48h],eax
  0048e474  8b8684000000          mov       eax,[esi+84h]
  0048e47a  66894c243c            mov       [esp+3Ch],cx
  0048e47f  8b8e88000000          mov       ecx,[esi+88h]
  0048e485  8954244c              mov       [esp+4Ch],edx
  0048e489  8b968c000000          mov       edx,[esi+8Ch]
  0048e48f  89442434              mov       [esp+34h],eax
  0048e493  894c2444              mov       [esp+44h],ecx
  0048e497  8d442464              lea       eax,[esp+64h]
  0048e49b  89542450              mov       [esp+50h],edx
  0048e49f  8d4c246c              lea       ecx,[esp+6Ch]
  0048e4a3  50                    push      eax
  0048e4a4  8d54246c              lea       edx,[esp+6Ch]
  0048e4a8  51                    push      ecx
  0048e4a9  52                    push      edx
  0048e4aa  56                    push      esi
  0048e4ab  895c2434              mov       [esp+34h],ebx
  0048e4af  e8cca40a00            call      00538980h
  0048e4b4  83c410                add       esp,10h
  0048e4b7  8d442454              lea       eax,[esp+54h]
  0048e4bb  8d4c241c              lea       ecx,[esp+1Ch]
  0048e4bf  8d542418              lea       edx,[esp+18h]
  0048e4c3  50                    push      eax
  0048e4c4  51                    push      ecx
  0048e4c5  8d44241c              lea       eax,[esp+1Ch]
  0048e4c9  52                    push      edx
  0048e4ca  8d4c241c              lea       ecx,[esp+1Ch]
  0048e4ce  50                    push      eax
  0048e4cf  51                    push      ecx
  0048e4d0  56                    push      esi
  0048e4d1  e84a7a0a00            call      00535F20h
  0048e4d6  8b4c2428              mov       ecx,[esp+28h]
  0048e4da  8b54246c              mov       edx,[esp+6Ch]
  0048e4de  83c418                add       esp,18h
  0048e4e1  b840000000            mov       eax,40h
  0048e4e6  2bc1                  sub       eax,ecx
  0048e4e8  52                    push      edx
  0048e4e9  56                    push      esi
  0048e4ea  89442428              mov       [esp+28h],eax
  0048e4ee  e8ed780400            call      004D5DE0h
  0048e4f3  8b07                  mov       eax,[edi]
  0048e4f5  8b531c                mov       edx,[ebx+1Ch]
  0048e4f8  8b4e24                mov       ecx,[esi+24h]
  0048e4fb  2bc2                  sub       eax,edx
  0048e4fd  8b5324                mov       edx,[ebx+24h]
  0048e500  83c408                add       esp,8
  0048e503  c1f80c                sar       eax,0Ch
  0048e506  2bca                  sub       ecx,edx
  0048e508  8bd0                  mov       edx,eax
  0048e50a  c1f90c                sar       ecx,0Ch
  0048e50d  0fafd0                imul      edx,eax
  0048e510  8bc1                  mov       eax,ecx
  0048e512  0fafc1                imul      eax,ecx
  0048e515  03d0                  add       edx,eax
  0048e517  52                    push      edx
  0048e518  e8f3740b00            call      00545A10h
  0048e51d  8b4c241c              mov       ecx,[esp+1Ch]
  0048e521  89442414              mov       [esp+14h],eax
  0048e525  3bcd                  cmp       ecx,ebp
  0048e527  0f8488070000          je        near 0048ECB5h
  0048e52d  8b2e                  mov       ebp,[esi]
  0048e52f  33db                  xor       ebx,ebx
  0048e531  8a5e04                mov       bl,[esi+4]
  0048e534  81e5fffffffd          and       ebp,0FDFFFFFFh
  0048e53a  892e                  mov       [esi],ebp
  0048e53c  8bd5                  mov       edx,ebp
  0048e53e  0fbf6e18              movsx     ebp,word [esi+18h]
  0048e542  8b1c9d40965e00        mov       ebx,[ebx*4+5E9640h]
  0048e549  f7c200000001          test      edx,1000000h
  0048e54f  8b2cab                mov       ebp,[ebx+ebp*4]
  0048e552  741d                  je        short 0048E571h
  0048e554  66ff461a              inc       word [esi+1Ah]
  0048e558  668b561a              mov       dx,[esi+1Ah]
  0048e55c  33db                  xor       ebx,ebx
  0048e55e  668b5d16              mov       bx,[ebp+16h]
  0048e562  0fbfd2                movsx     edx,dx
  0048e565  3bd3                  cmp       edx,ebx
  0048e567  7c2e                  jl        short 0048E597h
  0048e569  66c7461a0000          mov       word [esi+1Ah],0
  0048e56f  eb26                  jmp       short 0048E597h

LAB_0048e571:
  0048e571  66ff461a              inc       word [esi+1Ah]
  0048e575  668b461a              mov       ax,[esi+1Ah]
  0048e579  33db                  xor       ebx,ebx
  0048e57b  668b5d16              mov       bx,[ebp+16h]
  0048e57f  0fbfe8                movsx     ebp,ax
  0048e582  3beb                  cmp       ebp,ebx
  0048e584  7c0d                  jl        short 0048E593h
  0048e586  48                    dec       eax
  0048e587  81ca00000002          or        edx,2000000h
  0048e58d  6689461a              mov       [esi+1Ah],ax
  0048e591  8916                  mov       [esi],edx

LAB_0048e593:
  0048e593  8b442414              mov       eax,[esp+14h]

LAB_0048e597:
  0048e597  f60602                test      byte [esi],2
  0048e59a  7472                  je        short 0048E60Eh
  0048e59c  bb0e000000            mov       ebx,0Eh
  0048e5a1  66395e1a              cmp       [esi+1Ah],bx
  0048e5a5  7567                  jne       short 0048E60Eh
  0048e5a7  8d442428              lea       eax,[esp+28h]
  0048e5ab  6a00                  push      0
  0048e5ad  50                    push      eax
  0048e5ae  57                    push      edi
  0048e5af  56                    push      esi
  0048e5b0  e87bb60800            call      00519C30h
  0048e5b5  8be8                  mov       ebp,eax
  0048e5b7  a1b8e05d00            mov       eax,[5DE0B8h]
  0048e5bc  83c410                add       esp,10h
  0048e5bf  85c0                  test      eax,eax
  0048e5c1  7443                  je        short 0048E606h
  0048e5c3  66395e1a              cmp       [esi+1Ah],bx
  0048e5c7  753d                  jne       short 0048E606h
  0048e5c9  6683bd9600000000      cmp       word [ebp+96h],0
  0048e5d1  7533                  jne       short 0048E606h
  0048e5d3  8b4500                mov       eax,[ebp]
  0048e5d6  55                    push      ebp
  0048e5d7  0c40                  or        al,40h
  0048e5d9  56                    push      esi
  0048e5da  66c785960000001000    mov       word [ebp+96h],10h
  0048e5e3  894500                mov       [ebp],eax
  0048e5e6  e895b50a00            call      00539B80h
  0048e5eb  83c408                add       esp,8
  0048e5ee  83c51c                add       ebp,1Ch
  0048e5f1  55                    push      ebp
  0048e5f2  6800180000            push      1800h
  0048e5f7  6800030000            push      300h
  0048e5fc  6a03                  push      3
  0048e5fe  e81df50a00            call      0053DB20h
  0048e603  83c410                add       esp,10h

LAB_0048e606:
  0048e606  8b4c241c              mov       ecx,[esp+1Ch]
  0048e60a  8b442414              mov       eax,[esp+14h]

LAB_0048e60e:
  0048e60e  33d2                  xor       edx,edx
  0048e610  8a5642                mov       dl,[esi+42h]
  0048e613  83fa0c                cmp       edx,0Ch
  0048e616  0f87d5050000          ja        near 0048EBF1h
  0048e61c  ff2495f8f14800        jmp       dword [edx*4+48F1F8h]
  0048e623  33c9                  xor       ecx,ecx
  0048e625  66394e12              cmp       [esi+12h],cx
  0048e629  751b                  jne       short 0048E646h
  0048e62b  8b16                  mov       edx,[esi]
  0048e62d  66894e18              mov       [esi+18h],cx
  0048e631  81ca00000001          or        edx,1000000h
  0048e637  66894e1a              mov       [esi+1Ah],cx
  0048e63b  8916                  mov       [esi],edx
  0048e63d  66c746120100          mov       word [esi+12h],1
  0048e643  894e5c                mov       [esi+5Ch],ecx

LAB_0048e646:
  0048e646  0fbf4e10              movsx     ecx,word [esi+10h]
  0048e64a  3bc1                  cmp       eax,ecx
  0048e64c  7d09                  jge       short 0048E657h
  0048e64e  c6464300              mov       byte [esi+43h],0
  0048e652  e99a050000            jmp       0048EBF1h

LAB_0048e657:
  0048e657  8a4643                mov       al,[esi+43h]
  0048e65a  84c0                  test      al,al
  0048e65c  0f848a000000          je        near 0048E6ECh
  0048e662  fec8                  dec       al
  0048e664  c7465c00000000        mov       dword [esi+5Ch],0
  0048e66b  884643                mov       [esi+43h],al
  0048e66e  e97e050000            jmp       0048EBF1h
  0048e673  33c0                  xor       eax,eax
  0048e675  66394612              cmp       [esi+12h],ax
  0048e679  7517                  jne       short 0048E692h
  0048e67b  66894618              mov       [esi+18h],ax
  0048e67f  6689461a              mov       [esi+1Ah],ax
  0048e683  8b06                  mov       eax,[esi]
  0048e685  66c746120100          mov       word [esi+12h],1
  0048e68b  0d00000001            or        eax,1000000h
  0048e690  8906                  mov       [esi],eax

LAB_0048e692:
  0048e692  8b442418              mov       eax,[esp+18h]
  0048e696  8d90c8000000          lea       edx,[eax+0C8h]
  0048e69c  81e2ff0f0000          and       edx,0FFFh
  0048e6a2  81fa90010000          cmp       edx,190h
  0048e6a8  7f10                  jg        short 0048E6BAh
  0048e6aa  c6464300              mov       byte [esi+43h],0
  0048e6ae  c7465c00000000        mov       dword [esi+5Ch],0
  0048e6b5  e937050000            jmp       0048EBF1h

LAB_0048e6ba:
  0048e6ba  25ff0f0000            and       eax,0FFFh
  0048e6bf  3d00080000            cmp       eax,800h
  0048e6c4  7d17                  jge       short 0048E6DDh
  0048e6c6  8b442410              mov       eax,[esp+10h]
  0048e6ca  c7465c00000000        mov       dword [esi+5Ch],0
  0048e6d1  83c028                add       eax,28h
  0048e6d4  66014632              add       [esi+32h],ax
  0048e6d8  e914050000            jmp       0048EBF1h

LAB_0048e6dd:
  0048e6dd  8b4c2410              mov       ecx,[esp+10h]
  0048e6e1  b8d8ffffff            mov       eax,0FFFFFFD8h
  0048e6e6  2bc1                  sub       eax,ecx
  0048e6e8  66014632              add       [esi+32h],ax

LAB_0048e6ec:
  0048e6ec  c7465c00000000        mov       dword [esi+5Ch],0
  0048e6f3  e9f9040000            jmp       0048EBF1h
  0048e6f8  8b4c2410              mov       ecx,[esp+10h]
  0048e6fc  33d2                  xor       edx,edx
  0048e6fe  66395612              cmp       [esi+12h],dx
  0048e702  7527                  jne       short 0048E72Bh
  0048e704  8b3e                  mov       edi,[esi]
  0048e706  66895618              mov       [esi+18h],dx
  0048e70a  81cf00000001          or        edi,1000000h
  0048e710  6689561a              mov       [esi+1Ah],dx
  0048e714  893e                  mov       [esi],edi
  0048e716  8d3cc9                lea       edi,[ecx+ecx*8]
  0048e719  c1e704                shl       edi,4
  0048e71c  81c700a00000          add       edi,0A000h
  0048e722  66c746120100          mov       word [esi+12h],1
  0048e728  897e5c                mov       [esi+5Ch],edi

LAB_0048e72b:
  0048e72b  66837e1201            cmp       word [esi+12h],1
  0048e730  0f85bb040000          jne       near 0048EBF1h
  0048e736  0fbf7e10              movsx     edi,word [esi+10h]
  0048e73a  3bc7                  cmp       eax,edi
  0048e73c  7d0c                  jge       short 0048E74Ah
  0048e73e  c6464300              mov       byte [esi+43h],0
  0048e742  89565c                mov       [esi+5Ch],edx
  0048e745  e9a7040000            jmp       0048EBF1h

LAB_0048e74a:
  0048e74a  8a4643                mov       al,[esi+43h]
  0048e74d  84c0                  test      al,al
  0048e74f  7405                  je        short 0048E756h
  0048e751  fec8                  dec       al
  0048e753  884643                mov       [esi+43h],al

LAB_0048e756:
  0048e756  8b442418              mov       eax,[esp+18h]
  0048e75a  8d9000010000          lea       edx,[eax+100h]
  0048e760  81e2ff0f0000          and       edx,0FFFh
  0048e766  81fa00020000          cmp       edx,200h
  0048e76c  0f8e7f040000          jle       near 0048EBF1h
  0048e772  25ff0f0000            and       eax,0FFFh
  0048e777  3d00080000            cmp       eax,800h
  0048e77c  7d0c                  jge       short 0048E78Ah
  0048e77e  8d4128                lea       eax,[ecx+28h]
  0048e781  66014632              add       [esi+32h],ax
  0048e785  e967040000            jmp       0048EBF1h

LAB_0048e78a:
  0048e78a  b8d8ffffff            mov       eax,0FFFFFFD8h
  0048e78f  2bc1                  sub       eax,ecx
  0048e791  66014632              add       [esi+32h],ax
  0048e795  e957040000            jmp       0048EBF1h
  0048e79a  66837e1201            cmp       word [esi+12h],1
  0048e79f  0f8597000000          jne       near 0048E83Ch
  0048e7a5  8a4643                mov       al,[esi+43h]
  0048e7a8  84c0                  test      al,al
  0048e7aa  7405                  je        short 0048E7B1h
  0048e7ac  fec8                  dec       al
  0048e7ae  884643                mov       [esi+43h],al

LAB_0048e7b1:
  0048e7b1  8b442410              mov       eax,[esp+10h]
  0048e7b5  8d04c0                lea       eax,[eax+eax*8]
  0048e7b8  c1e004                shl       eax,4
  0048e7bb  0500800000            add       eax,8000h
  0048e7c0  0fafc8                imul      ecx,eax
  0048e7c3  c1f90c                sar       ecx,0Ch
  0048e7c6  8be9                  mov       ebp,ecx
  0048e7c8  89465c                mov       [esi+5Ch],eax
  0048e7cb  0fbf4e10              movsx     ecx,word [esi+10h]
  0048e7cf  51                    push      ecx
  0048e7d0  e847720b00            call      00545A1Ch
  0048e7d5  0fafc5                imul      eax,ebp
  0048e7d8  0fbf5610              movsx     edx,word [esi+10h]
  0048e7dc  8b0f                  mov       ecx,[edi]
  0048e7de  52                    push      edx
  0048e7df  c1f80c                sar       eax,0Ch
  0048e7e2  03c8                  add       ecx,eax
  0048e7e4  890f                  mov       [edi],ecx
  0048e7e6  e82b720b00            call      00545A16h
  0048e7eb  0fafc5                imul      eax,ebp
  0048e7ee  8b5e24                mov       ebx,[esi+24h]
  0048e7f1  c7465c00000000        mov       dword [esi+5Ch],0
  0048e7f8  c1f80c                sar       eax,0Ch
  0048e7fb  03d8                  add       ebx,eax
  0048e7fd  8b442418              mov       eax,[esp+18h]
  0048e801  895e24                mov       [esi+24h],ebx
  0048e804  8d8800010000          lea       ecx,[eax+100h]
  0048e80a  81e1ff0f0000          and       ecx,0FFFh
  0048e810  81f900020000          cmp       ecx,200h
  0048e816  7e24                  jle       short 0048E83Ch
  0048e818  25ff0f0000            and       eax,0FFFh
  0048e81d  3d00080000            cmp       eax,800h
  0048e822  7d09                  jge       short 0048E82Dh
  0048e824  8b542410              mov       edx,[esp+10h]
  0048e828  8d4232                lea       eax,[edx+32h]
  0048e82b  eb0b                  jmp       short 0048E838h

LAB_0048e82d:
  0048e82d  8b4c2410              mov       ecx,[esp+10h]
  0048e831  b8ceffffff            mov       eax,0FFFFFFCEh
  0048e836  2bc1                  sub       eax,ecx

LAB_0048e838:
  0048e838  66014632              add       [esi+32h],ax

LAB_0048e83c:
  0048e83c  66837e1200            cmp       word [esi+12h],0
  0048e841  0f85aa030000          jne       near 0048EBF1h
  0048e847  33c0                  xor       eax,eax
  0048e849  66c746120100          mov       word [esi+12h],1
  0048e84f  66894618              mov       [esi+18h],ax
  0048e853  6689461a              mov       [esi+1Ah],ax
  0048e857  810e00000001          or        dword [esi],1000000h
  0048e85d  e98f030000            jmp       0048EBF1h
  0048e862  668b4e12              mov       cx,[esi+12h]
  0048e866  6683f901              cmp       cx,1
  0048e86a  751a                  jne       short 0048E886h
  0048e86c  0fbf5610              movsx     edx,word [esi+10h]
  0048e870  3bc2                  cmp       eax,edx
  0048e872  7d06                  jge       short 0048E87Ah
  0048e874  c6464300              mov       byte [esi+43h],0
  0048e878  eb0c                  jmp       short 0048E886h

LAB_0048e87a:
  0048e87a  8a4643                mov       al,[esi+43h]
  0048e87d  84c0                  test      al,al
  0048e87f  7405                  je        short 0048E886h
  0048e881  fec8                  dec       al
  0048e883  884643                mov       [esi+43h],al

LAB_0048e886:
  0048e886  6685c9                test      cx,cx
  0048e889  0f8562030000          jne       near 0048EBF1h
  0048e88f  8b0e                  mov       ecx,[esi]
  0048e891  33c0                  xor       eax,eax
  0048e893  66894618              mov       [esi+18h],ax
  0048e897  6689461a              mov       [esi+1Ah],ax
  0048e89b  8b442410              mov       eax,[esp+10h]
  0048e89f  81c900000001          or        ecx,1000000h
  0048e8a5  0500010000            add       eax,100h
  0048e8aa  890e                  mov       [esi],ecx
  0048e8ac  66c746120100          mov       word [esi+12h],1
  0048e8b2  8d0cc0                lea       ecx,[eax+eax*8]
  0048e8b5  c1e104                shl       ecx,4
  0048e8b8  894e5c                mov       [esi+5Ch],ecx
  0048e8bb  e931030000            jmp       0048EBF1h
  0048e8c0  66837e1200            cmp       word [esi+12h],0
  0048e8c5  0f8525010000          jne       near 0048E9F0h
  0048e8cb  33ed                  xor       ebp,ebp
  0048e8cd  33ff                  xor       edi,edi
  0048e8cf  b800f45f00            mov       eax,5FF400h

LAB_0048e8d4:
  0048e8d4  39706c                cmp       [eax+6Ch],esi
  0048e8d7  750c                  jne       short 0048E8E5h
  0048e8d9  f70000000080          test      dword [eax],80000000h
  0048e8df  0f8562020000          jne       near 0048EB47h

LAB_0048e8e5:
  0048e8e5  059c000000            add       eax,9Ch
  0048e8ea  47                    inc       edi
  0048e8eb  3d80076000            cmp       eax,600780h
  0048e8f0  7ce2                  jl        short 0048E8D4h
  0048e8f2  8b06                  mov       eax,[esi]
  0048e8f4  a904080000            test      eax,804h
  0048e8f9  7410                  je        short 0048E90Bh
  0048e8fb  25fbf7ffff            and       eax,0FFFFF7FBh
  0048e900  c6464300              mov       byte [esi+43h],0
  0048e904  8906                  mov       [esi],eax
  0048e906  e9e6020000            jmp       0048EBF1h

LAB_0048e90b:
  0048e90b  6aff                  push      0FFFFFFFFh
  0048e90d  55                    push      ebp
  0048e90e  68bd000000            push      0BDh
  0048e913  56                    push      esi
  0048e914  e8c7c1feff            call      0047AAE0h
  0048e919  83c410                add       esp,10h
  0048e91c  66396e16              cmp       [esi+16h],bp
  0048e920  bb01000000            mov       ebx,1
  0048e925  66895e12              mov       [esi+12h],bx
  0048e929  751a                  jne       short 0048E945h
  0048e92b  e810880b00            call      00547140h
  0048e930  99                    cdq
  0048e931  33c2                  xor       eax,edx
  0048e933  2bc2                  sub       eax,edx
  0048e935  23c3                  and       eax,ebx
  0048e937  33c2                  xor       eax,edx
  0048e939  2bc2                  sub       eax,edx
  0048e93b  33ff                  xor       edi,edi
  0048e93d  668b3c45b0285500      mov       di,[eax*2+5528B0h]

LAB_0048e945:
  0048e945  66395e16              cmp       [esi+16h],bx
  0048e949  751a                  jne       short 0048E965h
  0048e94b  e8f0870b00            call      00547140h
  0048e950  99                    cdq
  0048e951  33c2                  xor       eax,edx
  0048e953  2bc2                  sub       eax,edx
  0048e955  23c3                  and       eax,ebx
  0048e957  33c2                  xor       eax,edx
  0048e959  2bc2                  sub       eax,edx
  0048e95b  33ff                  xor       edi,edi
  0048e95d  668b3c45b4285500      mov       di,[eax*2+5528B4h]

LAB_0048e965:
  0048e965  66837e1602            cmp       word [esi+16h],2
  0048e96a  751a                  jne       short 0048E986h
  0048e96c  e8cf870b00            call      00547140h
  0048e971  99                    cdq
  0048e972  33c2                  xor       eax,edx
  0048e974  2bc2                  sub       eax,edx
  0048e976  23c3                  and       eax,ebx
  0048e978  33c2                  xor       eax,edx
  0048e97a  2bc2                  sub       eax,edx
  0048e97c  33ff                  xor       edi,edi
  0048e97e  668b3c45b8285500      mov       di,[eax*2+5528B8h]

LAB_0048e986:
  0048e986  66837e1603            cmp       word [esi+16h],3
  0048e98b  751a                  jne       short 0048E9A7h
  0048e98d  e8ae870b00            call      00547140h
  0048e992  99                    cdq
  0048e993  33c2                  xor       eax,edx
  0048e995  2bc2                  sub       eax,edx
  0048e997  23c3                  and       eax,ebx
  0048e999  33c2                  xor       eax,edx
  0048e99b  2bc2                  sub       eax,edx
  0048e99d  33ff                  xor       edi,edi
  0048e99f  668b3c45bc285500      mov       di,[eax*2+5528BCh]

LAB_0048e9a7:
  0048e9a7  66897e06              mov       [esi+6],di
  0048e9ab  a1c4975e00            mov       eax,[5E97C4h]
  0048e9b0  8bd7                  mov       edx,edi
  0048e9b2  81e7ff000000          and       edi,0FFh
  0048e9b8  8b889c000000          mov       ecx,[eax+9Ch]
  0048e9be  c1fa08                sar       edx,8
  0048e9c1  81e2ff000000          and       edx,0FFh
  0048e9c7  8d047f                lea       eax,[edi+edi*2]
  0048e9ca  c1e004                shl       eax,4
  0048e9cd  8b0c91                mov       ecx,[ecx+edx*4]
  0048e9d0  660fb6540113          movzx     dx,byte [ecx+eax+13h]
  0048e9d6  8b06                  mov       eax,[esi]
  0048e9d8  66895610              mov       [esi+10h],dx
  0048e9dc  0d04080001            or        eax,1000804h
  0048e9e1  896e5c                mov       [esi+5Ch],ebp
  0048e9e4  66c746180400          mov       word [esi+18h],4
  0048e9ea  66896e1a              mov       [esi+1Ah],bp
  0048e9ee  8906                  mov       [esi],eax

LAB_0048e9f0:
  0048e9f0  b801000000            mov       eax,1
  0048e9f5  66394612              cmp       [esi+12h],ax
  0048e9f9  7525                  jne       short 0048EA20h
  0048e9fb  66ff4e10              dec       word [esi+10h]
  0048e9ff  66394610              cmp       [esi+10h],ax
  0048ea03  7d1b                  jge       short 0048EA20h
  0048ea05  8b06                  mov       eax,[esi]
  0048ea07  66c746120200          mov       word [esi+12h],2
  0048ea0d  25fbfffffe            and       eax,0FEFFFFFBh
  0048ea12  66c746180500          mov       word [esi+18h],5
  0048ea18  66c7461a0000          mov       word [esi+1Ah],0
  0048ea1e  8906                  mov       [esi],eax

LAB_0048ea20:
  0048ea20  66837e1202            cmp       word [esi+12h],2
  0048ea25  7532                  jne       short 0048EA59h
  0048ea27  66837e1a0d            cmp       word [esi+1Ah],0Dh
  0048ea2c  752b                  jne       short 0048EA59h
  0048ea2e  33c0                  xor       eax,eax
  0048ea30  6aff                  push      0FFFFFFFFh
  0048ea32  668b4606              mov       ax,[esi+6]
  0048ea36  6aff                  push      0FFFFFFFFh
  0048ea38  8bc8                  mov       ecx,eax
  0048ea3a  c1f808                sar       eax,8
  0048ea3d  81e1ff000000          and       ecx,0FFh
  0048ea43  25ff000000            and       eax,0FFh
  0048ea48  51                    push      ecx
  0048ea49  50                    push      eax
  0048ea4a  56                    push      esi
  0048ea4b  e830bf0a00            call      0053A980h
  0048ea50  83c414                add       esp,14h
  0048ea53  66c746120300          mov       word [esi+12h],3

LAB_0048ea59:
  0048ea59  66837e1203            cmp       word [esi+12h],3
  0048ea5e  0f858d010000          jne       near 0048EBF1h
  0048ea64  8b06                  mov       eax,[esi]
  0048ea66  a900000002            test      eax,2000000h
  0048ea6b  0f8480010000          je        near 0048EBF1h
  0048ea71  80e4f7                and       ah,0F7h
  0048ea74  c6464300              mov       byte [esi+43h],0
  0048ea78  8906                  mov       [esi],eax
  0048ea7a  e972010000            jmp       0048EBF1h
  0048ea7f  8b4e20                mov       ecx,[esi+20h]
  0048ea82  8b542424              mov       edx,[esp+24h]
  0048ea86  8b5a20                mov       ebx,[edx+20h]
  0048ea89  8db900400b00          lea       edi,[ecx+0B4000h]
  0048ea8f  3bfb                  cmp       edi,ebx
  0048ea91  7d0b                  jge       short 0048EA9Eh
  0048ea93  8b3e                  mov       edi,[esi]
  0048ea95  c6464300              mov       byte [esi+43h],0
  0048ea99  83e7fd                and       edi,0FFFFFFFDh
  0048ea9c  893e                  mov       [esi],edi

LAB_0048ea9e:
  0048ea9e  8b5220                mov       edx,[edx+20h]
  0048eaa1  2bd1                  sub       edx,ecx
  0048eaa3  81c200e00100          add       edx,1E000h
  0048eaa9  c1fa02                sar       edx,2
  0048eaac  03d1                  add       edx,ecx
  0048eaae  668b4e1a              mov       cx,[esi+1Ah]
  0048eab2  895620                mov       [esi+20h],edx
  0048eab5  8b542410              mov       edx,[esp+10h]
  0048eab9  6683f90c              cmp       cx,0Ch
  0048eabd  7515                  jne       short 0048EAD4h
  0048eabf  8b2e                  mov       ebp,[esi]
  0048eac1  8bfa                  mov       edi,edx
  0048eac3  83e7fc                and       edi,0FFFFFFFCh
  0048eac6  83cd02                or        ebp,2
  0048eac9  83c750                add       edi,50h
  0048eacc  892e                  mov       [esi],ebp
  0048eace  c1e70a                shl       edi,0Ah
  0048ead1  897e5c                mov       [esi+5Ch],edi

LAB_0048ead4:
  0048ead4  6683f90d              cmp       cx,0Dh
  0048ead8  7f4f                  jg        short 0048EB29h
  0048eada  8b4c2418              mov       ecx,[esp+18h]
  0048eade  8db900010000          lea       edi,[ecx+100h]
  0048eae4  81e7ff0f0000          and       edi,0FFFh
  0048eaea  81ff00020000          cmp       edi,200h
  0048eaf0  0f8efb000000          jle       near 0048EBF1h
  0048eaf6  83f850                cmp       eax,50h
  0048eaf9  0f8ef2000000          jle       near 0048EBF1h
  0048eaff  81e1ff0f0000          and       ecx,0FFFh
  0048eb05  81f900080000          cmp       ecx,800h
  0048eb0b  7d0c                  jge       short 0048EB19h
  0048eb0d  8d421e                lea       eax,[edx+1Eh]
  0048eb10  66014632              add       [esi+32h],ax
  0048eb14  e9d8000000            jmp       0048EBF1h

LAB_0048eb19:
  0048eb19  b8e2ffffff            mov       eax,0FFFFFFE2h
  0048eb1e  2bc2                  sub       eax,edx
  0048eb20  66014632              add       [esi+32h],ax
  0048eb24  e9c8000000            jmp       0048EBF1h

LAB_0048eb29:
  0048eb29  8b465c                mov       eax,[esi+5Ch]
  0048eb2c  8bc8                  mov       ecx,eax
  0048eb2e  c1f903                sar       ecx,3
  0048eb31  2bc1                  sub       eax,ecx
  0048eb33  3d00500000            cmp       eax,5000h
  0048eb38  89465c                mov       [esi+5Ch],eax
  0048eb3b  0f8db0000000          jge       near 0048EBF1h
  0048eb41  8b06                  mov       eax,[esi]
  0048eb43  24fd                  and       al,0FDh
  0048eb45  8906                  mov       [esi],eax

LAB_0048eb47:
  0048eb47  c6464300              mov       byte [esi+43h],0
  0048eb4b  e9a1000000            jmp       0048EBF1h
  0048eb50  66837e1200            cmp       word [esi+12h],0
  0048eb55  8b542410              mov       edx,[esp+10h]
  0048eb59  752c                  jne       short 0048EB87h
  0048eb5b  8b0e                  mov       ecx,[esi]
  0048eb5d  66c746180600          mov       word [esi+18h],6
  0048eb63  81e1fffffffe          and       ecx,0FEFFFFFFh
  0048eb69  66c7461a0000          mov       word [esi+1Ah],0
  0048eb6f  890e                  mov       [esi],ecx
  0048eb71  b940000000            mov       ecx,40h
  0048eb76  2bca                  sub       ecx,edx
  0048eb78  66c746120100          mov       word [esi+12h],1
  0048eb7e  c1f902                sar       ecx,2
  0048eb81  80c132                add       cl,32h
  0048eb84  884e43                mov       [esi+43h],cl

LAB_0048eb87:
  0048eb87  66837e1201            cmp       word [esi+12h],1
  0048eb8c  7563                  jne       short 0048EBF1h
  0048eb8e  8b4c2418              mov       ecx,[esp+18h]
  0048eb92  8db996000000          lea       edi,[ecx+96h]
  0048eb98  81e7ff0f0000          and       edi,0FFFh
  0048eb9e  81ff2c010000          cmp       edi,12Ch
  0048eba4  7e23                  jle       short 0048EBC9h
  0048eba6  83f850                cmp       eax,50h
  0048eba9  7e1e                  jle       short 0048EBC9h
  0048ebab  81e1ff0f0000          and       ecx,0FFFh
  0048ebb1  81f900080000          cmp       ecx,800h
  0048ebb7  7d05                  jge       short 0048EBBEh
  0048ebb9  8d4a1e                lea       ecx,[edx+1Eh]
  0048ebbc  eb07                  jmp       short 0048EBC5h

LAB_0048ebbe:
  0048ebbe  b9e2ffffff            mov       ecx,0FFFFFFE2h
  0048ebc3  2bca                  sub       ecx,edx

LAB_0048ebc5:
  0048ebc5  66014e32              add       [esi+32h],cx

LAB_0048ebc9:
  0048ebc9  8a5643                mov       dl,[esi+43h]
  0048ebcc  feca                  dec       dl
  0048ebce  66837e1a17            cmp       word [esi+1Ah],17h
  0048ebd3  885643                mov       [esi+43h],dl
  0048ebd6  7519                  jne       short 0048EBF1h
  0048ebd8  3dc4090000            cmp       eax,9C4h
  0048ebdd  7d12                  jge       short 0048EBF1h
  0048ebdf  6aff                  push      0FFFFFFFFh
  0048ebe1  6a00                  push      0
  0048ebe3  68df000000            push      0DFh
  0048ebe8  56                    push      esi
  0048ebe9  e8e2bdfeff            call      0047A9D0h
  0048ebee  83c410                add       esp,10h

LAB_0048ebf1:
  0048ebf1  8b0e                  mov       ecx,[esi]
  0048ebf3  8a4642                mov       al,[esi+42h]
  0048ebf6  f6c120                test      cl,20h
  0048ebf9  7467                  je        short 0048EC62h
  0048ebfb  3c0a                  cmp       al,0Ah
  0048ebfd  7505                  jne       short 0048EC04h
  0048ebff  f6c102                test      cl,2
  0048ec02  7445                  je        short 0048EC49h

LAB_0048ec04:
  0048ec04  e837850b00            call      00547140h
  0048ec09  99                    cdq
  0048ec0a  b932000000            mov       ecx,32h
  0048ec0f  8b7e20                mov       edi,[esi+20h]
  0048ec12  f7f9                  idiv      ecx
  0048ec14  66035614              add       dx,[esi+14h]
  0048ec18  03d1                  add       edx,ecx
  0048ec1a  81e2ff0f0000          and       edx,0FFFh
  0048ec20  66895614              mov       [esi+14h],dx
  0048ec24  0fbfd2                movsx     edx,dx
  0048ec27  52                    push      edx
  0048ec28  e8ef6d0b00            call      00545A1Ch
  0048ec2d  8b4e48                mov       ecx,[esi+48h]
  0048ec30  8d0440                lea       eax,[eax+eax*2]
  0048ec33  8d0480                lea       eax,[eax+eax*4]
  0048ec36  8d1481                lea       edx,[ecx+eax*4]
  0048ec39  2bd7                  sub       edx,edi
  0048ec3b  81c200c00800          add       edx,8C000h
  0048ec41  c1fa03                sar       edx,3
  0048ec44  03d7                  add       edx,edi
  0048ec46  895620                mov       [esi+20h],edx

LAB_0048ec49:
  0048ec49  8b4c2424              mov       ecx,[esp+24h]
  0048ec4d  8b4164                mov       eax,[ecx+64h]
  0048ec50  8b4920                mov       ecx,[ecx+20h]
  0048ec53  33c8                  xor       ecx,eax
  0048ec55  f7c100f0ffff          test      ecx,0FFFFF000h
  0048ec5b  7558                  jne       short 0048ECB5h
  0048ec5d  894648                mov       [esi+48h],eax
  0048ec60  eb53                  jmp       short 0048ECB5h

LAB_0048ec62:
  0048ec62  3c0c                  cmp       al,0Ch
  0048ec64  7404                  je        short 0048EC6Ah
  0048ec66  3c0a                  cmp       al,0Ah
  0048ec68  7505                  jne       short 0048EC6Fh

LAB_0048ec6a:
  0048ec6a  f6c102                test      cl,2
  0048ec6d  7446                  je        short 0048ECB5h

LAB_0048ec6f:
  0048ec6f  e8cc840b00            call      00547140h
  0048ec74  99                    cdq
  0048ec75  b932000000            mov       ecx,32h
  0048ec7a  8b7e20                mov       edi,[esi+20h]
  0048ec7d  f7f9                  idiv      ecx
  0048ec7f  66035614              add       dx,[esi+14h]
  0048ec83  03d1                  add       edx,ecx
  0048ec85  81e2ff0f0000          and       edx,0FFFh
  0048ec8b  66895614              mov       [esi+14h],dx
  0048ec8f  0fbfd2                movsx     edx,dx
  0048ec92  52                    push      edx
  0048ec93  e8846d0b00            call      00545A1Ch
  0048ec98  8b4e60                mov       ecx,[esi+60h]
  0048ec9b  8d0440                lea       eax,[eax+eax*2]
  0048ec9e  8d0480                lea       eax,[eax+eax*4]
  0048eca1  c1e002                shl       eax,2
  0048eca4  2bc7                  sub       eax,edi
  0048eca6  8d940800e00600        lea       edx,[eax+ecx+6E000h]
  0048ecad  c1fa03                sar       edx,3
  0048ecb0  03d7                  add       edx,edi
  0048ecb2  895620                mov       [esi+20h],edx

LAB_0048ecb5:
  0048ecb5  0fbf4632              movsx     eax,word [esi+32h]
  0048ecb9  50                    push      eax
  0048ecba  e85d6d0b00            call      00545A1Ch
  0048ecbf  0fbf4e32              movsx     ecx,word [esi+32h]
  0048ecc3  51                    push      ecx
  0048ecc4  89442474              mov       [esp+74h],eax
  0048ecc8  e8496d0b00            call      00545A16h
  0048eccd  8b5650                mov       edx,[esi+50h]
  0048ecd0  89442478              mov       [esp+78h],eax
  0048ecd4  8bc2                  mov       eax,edx
  0048ecd6  8b4c241c              mov       ecx,[esp+1Ch]
  0048ecda  c1f808                sar       eax,8
  0048ecdd  0faf4e5c              imul      ecx,[esi+5Ch]
  0048ece1  8bfa                  mov       edi,edx
  0048ece3  56                    push      esi
  0048ece4  2bf8                  sub       edi,eax
  0048ece6  c1fa03                sar       edx,3
  0048ece9  2bfa                  sub       edi,edx
  0048eceb  8b5654                mov       edx,[esi+54h]
  0048ecee  8bc2                  mov       eax,edx
  0048ecf0  8bea                  mov       ebp,edx
  0048ecf2  c1f808                sar       eax,8
  0048ecf5  2be8                  sub       ebp,eax
  0048ecf7  897e50                mov       [esi+50h],edi
  0048ecfa  c1fa03                sar       edx,3
  0048ecfd  2bea                  sub       ebp,edx
  0048ecff  8b5658                mov       edx,[esi+58h]
  0048ed02  8bda                  mov       ebx,edx
  0048ed04  8bc2                  mov       eax,edx
  0048ed06  c1fb08                sar       ebx,8
  0048ed09  2bc3                  sub       eax,ebx
  0048ed0b  8b5e20                mov       ebx,[esi+20h]
  0048ed0e  c1fa03                sar       edx,3
  0048ed11  c1f90c                sar       ecx,0Ch
  0048ed14  2bc2                  sub       eax,edx
  0048ed16  8bd1                  mov       edx,ecx
  0048ed18  0faf542474            imul      edx,[esp+74h]
  0048ed1d  0faf4c247c            imul      ecx,[esp+7Ch]
  0048ed22  c1fa0c                sar       edx,0Ch
  0048ed25  c1f90c                sar       ecx,0Ch
  0048ed28  894658                mov       [esi+58h],eax
  0048ed2b  03d7                  add       edx,edi
  0048ed2d  8b7e1c                mov       edi,[esi+1Ch]
  0048ed30  03c8                  add       ecx,eax
  0048ed32  8b4624                mov       eax,[esi+24h]
  0048ed35  03fa                  add       edi,edx
  0048ed37  03dd                  add       ebx,ebp
  0048ed39  03c1                  add       eax,ecx
  0048ed3b  896e54                mov       [esi+54h],ebp
  0048ed3e  897e1c                mov       [esi+1Ch],edi
  0048ed41  895e20                mov       [esi+20h],ebx
  0048ed44  894624                mov       [esi+24h],eax
  0048ed47  e8f44b0a00            call      00533940h
  0048ed4c  8bbe90000000          mov       edi,[esi+90h]
  0048ed52  8b06                  mov       eax,[esi]
  0048ed54  83c404                add       esp,4
  0048ed57  81e7fff8ffff          and       edi,0FFFFF8FFh
  0048ed5d  a820                  test      al,20h
  0048ed5f  89be90000000          mov       [esi+90h],edi
  0048ed65  8bcf                  mov       ecx,edi
  0048ed67  740c                  je        short 0048ED75h
  0048ed69  81e1ffdfffdf          and       ecx,0DFFFDFFFh
  0048ed6f  898e90000000          mov       [esi+90h],ecx

LAB_0048ed75:
  0048ed75  8b4e60                mov       ecx,[esi+60h]
  0048ed78  8b5620                mov       edx,[esi+20h]
  0048ed7b  3bd1                  cmp       edx,ecx
  0048ed7d  7f19                  jg        short 0048ED98h
  0048ed7f  8b9690000000          mov       edx,[esi+90h]
  0048ed85  f6c680                test      dh,80h
  0048ed88  750e                  jne       short 0048ED98h
  0048ed8a  24fe                  and       al,0FEh
  0048ed8c  33db                  xor       ebx,ebx
  0048ed8e  894e20                mov       [esi+20h],ecx
  0048ed91  8906                  mov       [esi],eax
  0048ed93  895e54                mov       [esi+54h],ebx
  0048ed96  eb02                  jmp       short 0048ED9Ah

LAB_0048ed98:
  0048ed98  33db                  xor       ebx,ebx

LAB_0048ed9a:
  0048ed9a  f7869000000082a80020  test      dword [esi+90h],2000A882h
  0048eda4  0f84c3000000          je        near 0048EE6Dh
  0048edaa  8a4642                mov       al,[esi+42h]
  0048edad  3c07                  cmp       al,7
  0048edaf  7425                  je        short 0048EDD6h
  0048edb1  3c0a                  cmp       al,0Ah
  0048edb3  7421                  je        short 0048EDD6h
  0048edb5  c6464207              mov       byte [esi+42h],7
  0048edb9  e882830b00            call      00547140h
  0048edbe  99                    cdq
  0048edbf  b928000000            mov       ecx,28h
  0048edc4  66895e12              mov       [esi+12h],bx
  0048edc8  f7f9                  idiv      ecx
  0048edca  66c746105000          mov       word [esi+10h],50h
  0048edd0  80c214                add       dl,14h
  0048edd3  885643                mov       [esi+43h],dl

LAB_0048edd6:
  0048edd6  8b16                  mov       edx,[esi]
  0048edd8  8b442460              mov       eax,[esp+60h]
  0048eddc  8b4c2448              mov       ecx,[esp+48h]
  0048ede0  83e2fd                and       edx,0FFFFFFFDh
  0048ede3  894624                mov       [esi+24h],eax
  0048ede6  8b44243c              mov       eax,[esp+3Ch]
  0048edea  8916                  mov       [esi],edx
  0048edec  8b542458              mov       edx,[esp+58h]
  0048edf0  25ffff0000            and       eax,0FFFFh
  0048edf5  89561c                mov       [esi+1Ch],edx
  0048edf8  8b54244c              mov       edx,[esp+4Ch]
  0048edfc  894e7c                mov       [esi+7Ch],ecx
  0048edff  8b4c2444              mov       ecx,[esp+44h]
  0048ee03  898690000000          mov       [esi+90h],eax
  0048ee09  8b442434              mov       eax,[esp+34h]
  0048ee0d  899680000000          mov       [esi+80h],edx
  0048ee13  8b542450              mov       edx,[esp+50h]
  0048ee17  898684000000          mov       [esi+84h],eax
  0048ee1d  8b442440              mov       eax,[esp+40h]
  0048ee21  898e88000000          mov       [esi+88h],ecx
  0048ee27  8b4c2438              mov       ecx,[esp+38h]
  0048ee2b  6882a80020            push      2000A882h
  0048ee30  56                    push      esi
  0048ee31  89968c000000          mov       [esi+8Ch],edx
  0048ee37  894664                mov       [esi+64h],eax
  0048ee3a  894e60                mov       [esi+60h],ecx
  0048ee3d  e81e700400            call      004D5E60h
  0048ee42  83c408                add       esp,8
  0048ee45  663bc3                cmp       ax,bx
  0048ee48  7515                  jne       short 0048EE5Fh
  0048ee4a  e8f1820b00            call      00547140h
  0048ee4f  99                    cdq
  0048ee50  33c2                  xor       eax,edx
  0048ee52  2bc2                  sub       eax,edx
  0048ee54  83e001                and       eax,1
  0048ee57  33c2                  xor       eax,edx
  0048ee59  2bc2                  sub       eax,edx
  0048ee5b  8d4400ff              lea       eax,[eax+eax-1]

LAB_0048ee5f:
  0048ee5f  8b542410              mov       edx,[esp+10h]
  0048ee63  83c240                add       edx,40h
  0048ee66  0fafd0                imul      edx,eax
  0048ee69  66015632              add       [esi+32h],dx

LAB_0048ee6d:
  0048ee6d  6aff                  push      0FFFFFFFFh
  0048ee6f  6aff                  push      0FFFFFFFFh
  0048ee71  6aff                  push      0FFFFFFFFh
  0048ee73  56                    push      esi
  0048ee74  e897490300            call      004C3810h
  0048ee79  8b44242c              mov       eax,[esp+2Ch]
  0048ee7d  83c410                add       esp,10h
  0048ee80  3bc3                  cmp       eax,ebx
  0048ee82  7529                  jne       short 0048EEADh
  0048ee84  668b842486000000      mov       ax,[esp+86h]
  0048ee8c  66894632              mov       [esi+32h],ax
  0048ee90  8a4642                mov       al,[esi+42h]
  0048ee93  3c0c                  cmp       al,0Ch
  0048ee95  0f8551030000          jne       near 0048F1ECh
  0048ee9b  c6464301              mov       byte [esi+43h],1
  0048ee9f  885e42                mov       [esi+42h],bl
  0048eea2  5f                    pop       edi
  0048eea3  5e                    pop       esi
  0048eea4  5d                    pop       ebp
  0048eea5  5b                    pop       ebx
  0048eea6  81c488000000          add       esp,88h
  0048eeac  c3                    ret

LAB_0048eead:
  0048eead  385e43                cmp       [esi+43h],bl
  0048eeb0  0f8536030000          jne       near 0048F1ECh
  0048eeb6  8b466c                mov       eax,[esi+6Ch]
  0048eeb9  8b4e1c                mov       ecx,[esi+1Ch]
  0048eebc  8b5624                mov       edx,[esi+24h]
  0048eebf  8b781c                mov       edi,[eax+1Ch]
  0048eec2  8b6824                mov       ebp,[eax+24h]
  0048eec5  2bf9                  sub       edi,ecx
  0048eec7  2bea                  sub       ebp,edx
  0048eec9  c1ff0c                sar       edi,0Ch
  0048eecc  c1fd0c                sar       ebp,0Ch
  0048eecf  8bcf                  mov       ecx,edi
  0048eed1  8bd5                  mov       edx,ebp
  0048eed3  0fafcf                imul      ecx,edi
  0048eed6  0fafd5                imul      edx,ebp
  0048eed9  03ca                  add       ecx,edx
  0048eedb  51                    push      ecx
  0048eedc  e82f6b0b00            call      00545A10h
  0048eee1  55                    push      ebp
  0048eee2  57                    push      edi
  0048eee3  8944241c              mov       [esp+1Ch],eax
  0048eee7  e8366b0b00            call      00545A22h
  0048eeec  668b4e32              mov       cx,[esi+32h]
  0048eef0  66895e12              mov       [esi+12h],bx
  0048eef4  2bc1                  sub       eax,ecx
  0048eef6  25ff0f0000            and       eax,0FFFh
  0048eefb  89442418              mov       [esp+18h],eax
  0048eeff  8b442414              mov       eax,[esp+14h]
  0048ef03  3d96000000            cmp       eax,96h
  0048ef08  0f8d03010000          jge       near 0048F011h
  0048ef0e  e82d820b00            call      00547140h
  0048ef13  99                    cdq
  0048ef14  b964000000            mov       ecx,64h
  0048ef19  f7f9                  idiv      ecx
  0048ef1b  8bea                  mov       ebp,edx
  0048ef1d  83fd32                cmp       ebp,32h
  0048ef20  7d77                  jge       short 0048EF99h
  0048ef22  8b542418              mov       edx,[esp+18h]
  0048ef26  8b7c2420              mov       edi,[esp+20h]
  0048ef2a  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  0048ef31  8d0cfd00020000        lea       ecx,[edi*8+200h]
  0048ef38  25ff0f0000            and       eax,0FFFh
  0048ef3d  3bc1                  cmp       eax,ecx
  0048ef3f  7f4e                  jg        short 0048EF8Fh
  0048ef41  e8fa810b00            call      00547140h
  0048ef46  99                    cdq
  0048ef47  8d4c3f01              lea       ecx,[edi+edi+1]
  0048ef4b  f7f9                  idiv      ecx
  0048ef4d  8b442414              mov       eax,[esp+14h]
  0048ef51  83c264                add       edx,64h
  0048ef54  3bc2                  cmp       eax,edx
  0048ef56  7d37                  jge       short 0048EF8Fh
  0048ef58  83fd1e                cmp       ebp,1Eh
  0048ef5b  7d23                  jge       short 0048EF80h
  0048ef5d  8b06                  mov       eax,[esi]
  0048ef5f  c646420b              mov       byte [esi+42h],0Bh
  0048ef63  25fffffffe            and       eax,0FEFFFFFFh
  0048ef68  c6464301              mov       byte [esi+43h],1
  0048ef6c  8906                  mov       [esi],eax
  0048ef6e  895e5c                mov       [esi+5Ch],ebx
  0048ef71  66c746180300          mov       word [esi+18h],3
  0048ef77  66895e1a              mov       [esi+1Ah],bx
  0048ef7b  e995000000            jmp       0048F015h

LAB_0048ef80:
  0048ef80  885e42                mov       [esi+42h],bl
  0048ef83  885e43                mov       [esi+43h],bl
  0048ef86  66895e12              mov       [esi+12h],bx
  0048ef8a  e986000000            jmp       0048F015h

LAB_0048ef8f:
  0048ef8f  c6464201              mov       byte [esi+42h],1
  0048ef93  c6464301              mov       byte [esi+43h],1
  0048ef97  eb7c                  jmp       short 0048F015h

LAB_0048ef99:
  0048ef99  83fd46                cmp       ebp,46h
  0048ef9c  7d3e                  jge       short 0048EFDCh
  0048ef9e  c6464203              mov       byte [esi+42h],3
  0048efa2  66895e12              mov       [esi+12h],bx
  0048efa6  e895810b00            call      00547140h
  0048efab  99                    cdq
  0048efac  b90a000000            mov       ecx,0Ah
  0048efb1  8b7c2420              mov       edi,[esp+20h]
  0048efb5  f7f9                  idiv      ecx
  0048efb7  8bc7                  mov       eax,edi
  0048efb9  d1f8                  sar       eax,1
  0048efbb  02d0                  add       dl,al
  0048efbd  80c214                add       dl,14h
  0048efc0  885643                mov       [esi+43h],dl
  0048efc3  e878810b00            call      00547140h
  0048efc8  99                    cdq
  0048efc9  33c2                  xor       eax,edx
  0048efcb  2bc2                  sub       eax,edx
  0048efcd  25ff0f0000            and       eax,0FFFh
  0048efd2  33c2                  xor       eax,edx
  0048efd4  2bc2                  sub       eax,edx
  0048efd6  66894610              mov       [esi+10h],ax
  0048efda  eb39                  jmp       short 0048F015h

LAB_0048efdc:
  0048efdc  8b542410              mov       edx,[esp+10h]
  0048efe0  b964000000            mov       ecx,64h
  0048efe5  d1fa                  sar       edx,1
  0048efe7  2bcd                  sub       ecx,ebp
  0048efe9  83c20a                add       edx,0Ah
  0048efec  3bca                  cmp       ecx,edx
  0048efee  7d0e                  jge       short 0048EFFEh
  0048eff0  c646420a              mov       byte [esi+42h],0Ah
  0048eff4  c6464301              mov       byte [esi+43h],1
  0048eff8  66895e12              mov       [esi+12h],bx
  0048effc  eb13                  jmp       short 0048F011h

LAB_0048effe:
  0048effe  885e42                mov       [esi+42h],bl
  0048f001  e83a810b00            call      00547140h
  0048f006  99                    cdq
  0048f007  b928000000            mov       ecx,28h
  0048f00c  f7f9                  idiv      ecx
  0048f00e  885643                mov       [esi+43h],dl

LAB_0048f011:
  0048f011  8b7c2420              mov       edi,[esp+20h]

LAB_0048f015:
  0048f015  8b442414              mov       eax,[esp+14h]
  0048f019  3d58020000            cmp       eax,258h
  0048f01e  0f8de4000000          jge       near 0048F108h
  0048f024  3d96000000            cmp       eax,96h
  0048f029  0f8cd9000000          jl        near 0048F108h
  0048f02f  e80c810b00            call      00547140h
  0048f034  99                    cdq
  0048f035  b964000000            mov       ecx,64h
  0048f03a  f7f9                  idiv      ecx
  0048f03c  83fa14                cmp       edx,14h
  0048f03f  7d29                  jge       short 0048F06Ah
  0048f041  8b542418              mov       edx,[esp+18h]
  0048f045  81c2f4010000          add       edx,1F4h
  0048f04b  81e2ff0f0000          and       edx,0FFFh
  0048f051  81fa00080000          cmp       edx,800h
  0048f057  0f8c9f000000          jl        near 0048F0FCh
  0048f05d  c6464201              mov       byte [esi+42h],1
  0048f061  c6464301              mov       byte [esi+43h],1
  0048f065  e99e000000            jmp       0048F108h

LAB_0048f06a:
  0048f06a  83fa28                cmp       edx,28h
  0048f06d  7d25                  jge       short 0048F094h
  0048f06f  c6464202              mov       byte [esi+42h],2
  0048f073  e8c8800b00            call      00547140h
  0048f078  99                    cdq
  0048f079  b90a000000            mov       ecx,0Ah
  0048f07e  66c746105000          mov       word [esi+10h],50h
  0048f084  f7f9                  idiv      ecx
  0048f086  8bc7                  mov       eax,edi
  0048f088  d1f8                  sar       eax,1
  0048f08a  02d0                  add       dl,al
  0048f08c  80c214                add       dl,14h
  0048f08f  885643                mov       [esi+43h],dl
  0048f092  eb74                  jmp       short 0048F108h

LAB_0048f094:
  0048f094  83fa50                cmp       edx,50h
  0048f097  7d3a                  jge       short 0048F0D3h
  0048f099  c6464203              mov       byte [esi+42h],3
  0048f09d  66895e12              mov       [esi+12h],bx
  0048f0a1  e89a800b00            call      00547140h
  0048f0a6  99                    cdq
  0048f0a7  b90a000000            mov       ecx,0Ah
  0048f0ac  f7f9                  idiv      ecx
  0048f0ae  8bc7                  mov       eax,edi
  0048f0b0  d1f8                  sar       eax,1
  0048f0b2  02d0                  add       dl,al
  0048f0b4  80c214                add       dl,14h
  0048f0b7  885643                mov       [esi+43h],dl
  0048f0ba  e881800b00            call      00547140h
  0048f0bf  99                    cdq
  0048f0c0  33c2                  xor       eax,edx
  0048f0c2  2bc2                  sub       eax,edx
  0048f0c4  25ff0f0000            and       eax,0FFFh
  0048f0c9  33c2                  xor       eax,edx
  0048f0cb  2bc2                  sub       eax,edx
  0048f0cd  66894610              mov       [esi+10h],ax
  0048f0d1  eb35                  jmp       short 0048F108h

LAB_0048f0d3:
  0048f0d3  b964000000            mov       ecx,64h
  0048f0d8  2bca                  sub       ecx,edx
  0048f0da  8b542410              mov       edx,[esp+10h]
  0048f0de  d1fa                  sar       edx,1
  0048f0e0  83c20a                add       edx,0Ah
  0048f0e3  3bca                  cmp       ecx,edx
  0048f0e5  7d15                  jge       short 0048F0FCh
  0048f0e7  668b4616              mov       ax,[esi+16h]
  0048f0eb  663bc3                cmp       ax,bx
  0048f0ee  740c                  je        short 0048F0FCh
  0048f0f0  663d0100              cmp       ax,1
  0048f0f4  7406                  je        short 0048F0FCh
  0048f0f6  c646420a              mov       byte [esi+42h],0Ah
  0048f0fa  eb04                  jmp       short 0048F100h

LAB_0048f0fc:
  0048f0fc  c646420c              mov       byte [esi+42h],0Ch

LAB_0048f100:
  0048f100  c6464301              mov       byte [esi+43h],1
  0048f104  66895e12              mov       [esi+12h],bx

LAB_0048f108:
  0048f108  817c241458020000      cmp       dword [esp+14h],258h
  0048f110  0f8cd6000000          jl        near 0048F1ECh
  0048f116  e825800b00            call      00547140h
  0048f11b  99                    cdq
  0048f11c  b964000000            mov       ecx,64h
  0048f121  f7f9                  idiv      ecx
  0048f123  83fa32                cmp       edx,32h
  0048f126  7d6f                  jge       short 0048F197h
  0048f128  8b442410              mov       eax,[esp+10h]
  0048f12c  8d4c000a              lea       ecx,[eax+eax+0Ah]
  0048f130  3bd1                  cmp       edx,ecx
  0048f132  7d2e                  jge       short 0048F162h
  0048f134  c6464202              mov       byte [esi+42h],2
  0048f138  e803800b00            call      00547140h
  0048f13d  99                    cdq
  0048f13e  b90a000000            mov       ecx,0Ah
  0048f143  66c746106400          mov       word [esi+10h],64h
  0048f149  f7f9                  idiv      ecx
  0048f14b  8bc7                  mov       eax,edi
  0048f14d  d1f8                  sar       eax,1
  0048f14f  02d0                  add       dl,al
  0048f151  80c214                add       dl,14h
  0048f154  885643                mov       [esi+43h],dl
  0048f157  5f                    pop       edi
  0048f158  5e                    pop       esi
  0048f159  5d                    pop       ebp
  0048f15a  5b                    pop       ebx
  0048f15b  81c488000000          add       esp,88h
  0048f161  c3                    ret

LAB_0048f162:
  0048f162  885e42                mov       [esi+42h],bl
  0048f165  e8d67f0b00            call      00547140h
  0048f16a  99                    cdq
  0048f16b  b90a000000            mov       ecx,0Ah
  0048f170  66895e12              mov       [esi+12h],bx
  0048f174  f7f9                  idiv      ecx
  0048f176  8b4c2410              mov       ecx,[esp+10h]
  0048f17a  8bc7                  mov       eax,edi
  0048f17c  c1f802                sar       eax,2
  0048f17f  02d0                  add       dl,al
  0048f181  885643                mov       [esi+43h],dl
  0048f184  8d540950              lea       edx,[ecx+ecx+50h]
  0048f188  66895610              mov       [esi+10h],dx
  0048f18c  5f                    pop       edi
  0048f18d  5e                    pop       esi
  0048f18e  5d                    pop       ebp
  0048f18f  5b                    pop       ebx
  0048f190  81c488000000          add       esp,88h
  0048f196  c3                    ret

LAB_0048f197:
  0048f197  83fa4b                cmp       edx,4Bh
  0048f19a  7d43                  jge       short 0048F1DFh
  0048f19c  c6464203              mov       byte [esi+42h],3
  0048f1a0  66895e12              mov       [esi+12h],bx
  0048f1a4  e8977f0b00            call      00547140h
  0048f1a9  99                    cdq
  0048f1aa  b90a000000            mov       ecx,0Ah
  0048f1af  f7f9                  idiv      ecx
  0048f1b1  8bc7                  mov       eax,edi
  0048f1b3  d1f8                  sar       eax,1
  0048f1b5  02d0                  add       dl,al
  0048f1b7  80c214                add       dl,14h
  0048f1ba  885643                mov       [esi+43h],dl
  0048f1bd  e87e7f0b00            call      00547140h
  0048f1c2  99                    cdq
  0048f1c3  33c2                  xor       eax,edx
  0048f1c5  2bc2                  sub       eax,edx
  0048f1c7  25ff0f0000            and       eax,0FFFh
  0048f1cc  33c2                  xor       eax,edx
  0048f1ce  2bc2                  sub       eax,edx
  0048f1d0  66894610              mov       [esi+10h],ax
  0048f1d4  5f                    pop       edi
  0048f1d5  5e                    pop       esi
  0048f1d6  5d                    pop       ebp
  0048f1d7  5b                    pop       ebx
  0048f1d8  81c488000000          add       esp,88h
  0048f1de  c3                    ret

LAB_0048f1df:
  0048f1df  83fa3c                cmp       edx,3Ch
  0048f1e2  7d08                  jge       short 0048F1ECh
  0048f1e4  c6464201              mov       byte [esi+42h],1
  0048f1e8  c6464301              mov       byte [esi+43h],1

LAB_0048f1ec:
  0048f1ec  5f                    pop       edi
  0048f1ed  5e                    pop       esi
  0048f1ee  5d                    pop       ebp
  0048f1ef  5b                    pop       ebx
  0048f1f0  81c488000000          add       esp,88h
  0048f1f6  c3                    ret
  0048f1f7  90                    nop
  0048f1f8  23e6                  and       esp,esi
  0048f1fa  48                    dec       eax
  0048f1fb  0073e6                add       [ebx-1Ah],dh
  0048f1fe  48                    dec       eax
  0048f1ff  00f8                  add       al,bh
  0048f201  e648                  out       48h,al
  0048f203  009ae74800f1          add       [edx-0EFFB719h],bl
  0048f209  eb48                  jmp       short 0048F253h
  0048f20b  00f1                  add       cl,dh
  0048f20d  eb48                  jmp       short 0048F257h
  0048f20f  00f1                  add       cl,dh
  0048f211  eb48                  jmp       short 0048F25Bh
  0048f213  0062e8                add       [edx-18h],ah
  0048f216  48                    dec       eax
  0048f217  00f1                  add       cl,dh
  0048f219  eb48                  jmp       short 0048F263h
  0048f21b  00f1                  add       cl,dh
  0048f21d  eb48                  jmp       short 0048F267h
  0048f21f  00c0                  add       al,al
  0048f221  e848007fea            call      0EAC7F26Eh
  0048f226  48                    dec       eax
  0048f227  0050eb                add       [eax-15h],dl
  0048f22a  48                    dec       eax
  0048f22b  0090909090            (bad)
  (octet invalide - la plage demandee deborde probablement du code)
