Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004c7240  section .text  offset fichier 0xc6640  2752 octets

  004c7240  83ec20                sub       esp,20h
  004c7243  8d442414              lea       eax,[esp+14h]
  004c7247  8d4c2418              lea       ecx,[esp+18h]
  004c724b  8d542410              lea       edx,[esp+10h]
  004c724f  53                    push      ebx
  004c7250  55                    push      ebp
  004c7251  56                    push      esi
  004c7252  8b742430              mov       esi,[esp+30h]
  004c7256  57                    push      edi
  004c7257  50                    push      eax
  004c7258  51                    push      ecx
  004c7259  52                    push      edx
  004c725a  56                    push      esi
  004c725b  e820170700            call      00538980h
  004c7260  83c410                add       esp,10h
  004c7263  8d442418              lea       eax,[esp+18h]
  004c7267  8d4c2414              lea       ecx,[esp+14h]
  004c726b  8d542434              lea       edx,[esp+34h]
  004c726f  50                    push      eax
  004c7270  51                    push      ecx
  004c7271  8d442418              lea       eax,[esp+18h]
  004c7275  52                    push      edx
  004c7276  8d4c2438              lea       ecx,[esp+38h]
  004c727a  50                    push      eax
  004c727b  51                    push      ecx
  004c727c  56                    push      esi
  004c727d  e89eec0600            call      00535F20h
  004c7282  8b44242c              mov       eax,[esp+2Ch]
  004c7286  33ed                  xor       ebp,ebp
  004c7288  83c418                add       esp,18h
  004c728b  3bc5                  cmp       eax,ebp
  004c728d  bf01000000            mov       edi,1
  004c7292  bb2c010000            mov       ebx,12Ch
  004c7297  0f8416070000          je        near 004C79B3h
  004c729d  56                    push      esi
  004c729e  e8fd420100            call      004DB5A0h
  004c72a3  8b06                  mov       eax,[esi]
  004c72a5  83c404                add       esp,4
  004c72a8  a802                  test      al,2
  004c72aa  0f84f8030000          je        near 004C76A8h
  004c72b0  8a4c2418              mov       cl,[esp+18h]
  004c72b4  bb10000000            mov       ebx,10h
  004c72b9  84cb                  test      bl,cl
  004c72bb  740d                  je        short 004C72CAh
  004c72bd  24fd                  and       al,0FDh
  004c72bf  c6464300              mov       byte [esi+43h],0
  004c72c3  8906                  mov       [esi],eax
  004c72c5  e9e9060000            jmp       004C79B3h

LAB_004c72ca:
  004c72ca  33c9                  xor       ecx,ecx
  004c72cc  8a4e42                mov       cl,[esi+42h]
  004c72cf  83f905                cmp       ecx,5
  004c72d2  0f87db060000          ja        near 004C79B3h
  004c72d8  ff248db07c4c00        jmp       dword [ecx*4+4C7CB0h]
  004c72df  0fbf4610              movsx     eax,word [esi+10h]
  004c72e3  2bc5                  sub       eax,ebp
  004c72e5  0f84a9000000          je        near 004C7394h
  004c72eb  48                    dec       eax
  004c72ec  0f85c1060000          jne       near 004C79B3h
  004c72f2  66837e1a0b            cmp       word [esi+1Ah],0Bh
  004c72f7  896e5c                mov       [esi+5Ch],ebp
  004c72fa  755c                  jne       short 004C7358h
  004c72fc  0fbf5632              movsx     edx,word [esi+32h]
  004c7300  52                    push      edx
  004c7301  c7465c00200200        mov       dword [esi+5Ch],22000h
  004c7308  e80fe70700            call      00545A1Ch
  004c730d  0faf465c              imul      eax,[esi+5Ch]
  004c7311  8b5e50                mov       ebx,[esi+50h]
  004c7314  c1f80c                sar       eax,0Ch
  004c7317  03d8                  add       ebx,eax
  004c7319  0fbf4632              movsx     eax,word [esi+32h]
  004c731d  50                    push      eax
  004c731e  895e50                mov       [esi+50h],ebx
  004c7321  e8f0e60700            call      00545A16h
  004c7326  0faf465c              imul      eax,[esi+5Ch]
  004c732a  8b4e58                mov       ecx,[esi+58h]
  004c732d  c7465c00500000        mov       dword [esi+5Ch],5000h
  004c7334  c1f80c                sar       eax,0Ch
  004c7337  03c8                  add       ecx,eax
  004c7339  66ff4e10              dec       word [esi+10h]
  004c733d  894e58                mov       [esi+58h],ecx
  004c7340  8d4e1c                lea       ecx,[esi+1Ch]
  004c7343  51                    push      ecx
  004c7344  6800180000            push      1800h
  004c7349  6895010000            push      195h
  004c734e  6a24                  push      24h
  004c7350  e8cb670700            call      0053DB20h
  004c7355  83c410                add       esp,10h

LAB_004c7358:
  004c7358  8b442434              mov       eax,[esp+34h]
  004c735c  8d9080000000          lea       edx,[eax+80h]
  004c7362  81e2ff0f0000          and       edx,0FFFh
  004c7368  81fa00010000          cmp       edx,100h
  004c736e  0f8e3b060000          jle       near 004C79AFh
  004c7374  25ff0f0000            and       eax,0FFFh
  004c7379  33c9                  xor       ecx,ecx
  004c737b  3d00080000            cmp       eax,800h
  004c7380  0f9dc1                setge     cl
  004c7383  49                    dec       ecx
  004c7384  81e100010000          and       ecx,100h
  004c738a  83c180                add       ecx,0FFFFFF80h
  004c738d  8bc1                  mov       eax,ecx
  004c738f  e91b060000            jmp       004C79AFh

LAB_004c7394:
  004c7394  8b4650                mov       eax,[esi+50h]
  004c7397  8b4e58                mov       ecx,[esi+58h]
  004c739a  c1f80c                sar       eax,0Ch
  004c739d  8bd0                  mov       edx,eax
  004c739f  c1f90c                sar       ecx,0Ch
  004c73a2  0fafd0                imul      edx,eax
  004c73a5  8bc1                  mov       eax,ecx
  004c73a7  0fafc1                imul      eax,ecx
  004c73aa  03d0                  add       edx,eax
  004c73ac  52                    push      edx
  004c73ad  e85ee60700            call      00545A10h
  004c73b2  8b0e                  mov       ecx,[esi]
  004c73b4  f7c100000002          test      ecx,2000000h
  004c73ba  740a                  je        short 004C73C6h
  004c73bc  83f808                cmp       eax,8
  004c73bf  7f05                  jg        short 004C73C6h
  004c73c1  83e1fd                and       ecx,0FFFFFFFDh
  004c73c4  890e                  mov       [esi],ecx

LAB_004c73c6:
  004c73c6  83f803                cmp       eax,3
  004c73c9  0f8ce4050000          jl        near 004C79B3h
  004c73cf  8b442434              mov       eax,[esp+34h]
  004c73d3  8d4828                lea       ecx,[eax+28h]
  004c73d6  81e1ff0f0000          and       ecx,0FFFh
  004c73dc  83f950                cmp       ecx,50h
  004c73df  7e18                  jle       short 004C73F9h
  004c73e1  25ff0f0000            and       eax,0FFFh
  004c73e6  33d2                  xor       edx,edx
  004c73e8  3d00080000            cmp       eax,800h
  004c73ed  0f9dc2                setge     dl
  004c73f0  4a                    dec       edx
  004c73f1  83e250                and       edx,50h
  004c73f4  83c2d8                add       edx,0FFFFFFD8h
  004c73f7  8bc2                  mov       eax,edx

LAB_004c73f9:
  004c73f9  66014632              add       [esi+32h],ax
  004c73fd  55                    push      ebp
  004c73fe  8d461c                lea       eax,[esi+1Ch]
  004c7401  68d82b5500            push      552BD8h
  004c7406  50                    push      eax
  004c7407  56                    push      esi
  004c7408  e823280500            call      00519C30h
  004c740d  8bf8                  mov       edi,eax
  004c740f  a1b8e05d00            mov       eax,[5DE0B8h]
  004c7414  83c410                add       esp,10h
  004c7417  3bc5                  cmp       eax,ebp
  004c7419  0f8494050000          je        near 004C79B3h
  004c741f  66899f96000000        mov       [edi+96h],bx
  004c7426  8b1f                  mov       ebx,[edi]
  004c7428  83cb40                or        ebx,40h
  004c742b  57                    push      edi
  004c742c  56                    push      esi
  004c742d  891f                  mov       [edi],ebx
  004c742f  e84c270700            call      00539B80h
  004c7434  83c408                add       esp,8
  004c7437  83c71c                add       edi,1Ch
  004c743a  57                    push      edi
  004c743b  6800180000            push      1800h
  004c7440  6800030000            push      300h
  004c7445  6a03                  push      3
  004c7447  e8d4660700            call      0053DB20h
  004c744c  83c410                add       esp,10h
  004c744f  e95f050000            jmp       004C79B3h
  004c7454  0fbf4e10              movsx     ecx,word [esi+10h]
  004c7458  2bcd                  sub       ecx,ebp
  004c745a  0f8432020000          je        near 004C7692h
  004c7460  49                    dec       ecx
  004c7461  0f8539020000          jne       near 004C76A0h
  004c7467  a900000002            test      eax,2000000h
  004c746c  0f842e020000          je        near 004C76A0h
  004c7472  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  004c7479  8b7e50                mov       edi,[esi+50h]
  004c747c  8b6e54                mov       ebp,[esi+54h]
  004c747f  8b5e58                mov       ebx,[esi+58h]
  004c7482  6a00                  push      0
  004c7484  56                    push      esi
  004c7485  894c2424              mov       [esp+24h],ecx
  004c7489  66c7869a0000000100    mov       word [esi+9Ah],1
  004c7492  e8d9e20600            call      00535770h
  004c7497  0fbf10                movsx     edx,word [eax]
  004c749a  c1e20c                shl       edx,0Ch
  004c749d  895650                mov       [esi+50h],edx
  004c74a0  83c408                add       esp,8
  004c74a3  0fbf4802              movsx     ecx,word [eax+2]
  004c74a7  c1e10c                shl       ecx,0Ch
  004c74aa  894e54                mov       [esi+54h],ecx
  004c74ad  6aff                  push      0FFFFFFFFh
  004c74af  0fbf5004              movsx     edx,word [eax+4]
  004c74b3  c1e20c                shl       edx,0Ch
  004c74b6  895658                mov       [esi+58h],edx
  004c74b9  a1cc975e00            mov       eax,[5E97CCh]
  004c74be  6aff                  push      0FFFFFFFFh
  004c74c0  8b88cc000000          mov       ecx,[eax+0CCh]
  004c74c6  33c0                  xor       eax,eax
  004c74c8  8b11                  mov       edx,[ecx]
  004c74ca  8a420b                mov       al,[edx+0Bh]
  004c74cd  05c0000000            add       eax,0C0h
  004c74d2  50                    push      eax
  004c74d3  56                    push      esi
  004c74d4  e8f734fbff            call      0047A9D0h
  004c74d9  668b4c242c            mov       cx,[esp+2Ch]
  004c74de  83c410                add       esp,10h
  004c74e1  66ff4e10              dec       word [esi+10h]
  004c74e5  896e54                mov       [esi+54h],ebp
  004c74e8  33ed                  xor       ebp,ebp
  004c74ea  897e50                mov       [esi+50h],edi
  004c74ed  895e58                mov       [esi+58h],ebx
  004c74f0  66898e9a000000        mov       [esi+9Ah],cx
  004c74f7  c646430a              mov       byte [esi+43h],0Ah
  004c74fb  896e5c                mov       [esi+5Ch],ebp
  004c74fe  e9b0040000            jmp       004C79B3h
  004c7503  0fbf4e10              movsx     ecx,word [esi+10h]
  004c7507  2bcd                  sub       ecx,ebp
  004c7509  0f8483010000          je        near 004C7692h
  004c750f  49                    dec       ecx
  004c7510  0f858a010000          jne       near 004C76A0h
  004c7516  a900000002            test      eax,2000000h
  004c751b  0f847f010000          je        near 004C76A0h
  004c7521  8b566c                mov       edx,[esi+6Ch]
  004c7524  6aff                  push      0FFFFFFFFh
  004c7526  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  004c752d  66c7869a0000000200    mov       word [esi+9Ah],2
  004c7536  895678                mov       [esi+78h],edx
  004c7539  a1cc975e00            mov       eax,[5E97CCh]
  004c753e  6aff                  push      0FFFFFFFFh
  004c7540  8b88cc000000          mov       ecx,[eax+0CCh]
  004c7546  33c0                  xor       eax,eax
  004c7548  8b11                  mov       edx,[ecx]
  004c754a  8a420f                mov       al,[edx+0Fh]
  004c754d  05c0000000            add       eax,0C0h
  004c7552  50                    push      eax
  004c7553  56                    push      esi
  004c7554  e87734fbff            call      0047A9D0h
  004c7559  83c410                add       esp,10h
  004c755c  66ff4e10              dec       word [esi+10h]
  004c7560  6689be9a000000        mov       [esi+9Ah],di
  004c7567  c646430a              mov       byte [esi+43h],0Ah
  004c756b  896e5c                mov       [esi+5Ch],ebp
  004c756e  e940040000            jmp       004C79B3h
  004c7573  0fbf4e10              movsx     ecx,word [esi+10h]
  004c7577  2bcd                  sub       ecx,ebp
  004c7579  0f8413010000          je        near 004C7692h
  004c757f  49                    dec       ecx
  004c7580  0f851a010000          jne       near 004C76A0h
  004c7586  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004c758b  7511                  jne       short 004C759Eh
  004c758d  8d4e1c                lea       ecx,[esi+1Ch]
  004c7590  51                    push      ecx
  004c7591  68f82b5500            push      552BF8h
  004c7596  e845660700            call      0053DBE0h
  004c759b  83c408                add       esp,8

LAB_004c759e:
  004c759e  f70600000002          test      dword [esi],2000000h
  004c75a4  0f84f6000000          je        near 004C76A0h
  004c75aa  0fbfbe9a000000        movsx     edi,word [esi+9Ah]
  004c75b1  6689ae9a000000        mov       [esi+9Ah],bp
  004c75b8  8b15cc975e00          mov       edx,[5E97CCh]
  004c75be  6aff                  push      0FFFFFFFFh
  004c75c0  6aff                  push      0FFFFFFFFh
  004c75c2  8b82cc000000          mov       eax,[edx+0CCh]
  004c75c8  33d2                  xor       edx,edx
  004c75ca  8b08                  mov       ecx,[eax]
  004c75cc  8a5109                mov       dl,[ecx+9]
  004c75cf  81c2c0000000          add       edx,0C0h
  004c75d5  52                    push      edx
  004c75d6  56                    push      esi
  004c75d7  e8f433fbff            call      0047A9D0h
  004c75dc  83c410                add       esp,10h
  004c75df  66ff4e10              dec       word [esi+10h]
  004c75e3  6689be9a000000        mov       [esi+9Ah],di
  004c75ea  c646430a              mov       byte [esi+43h],0Ah
  004c75ee  896e5c                mov       [esi+5Ch],ebp
  004c75f1  e9bd030000            jmp       004C79B3h
  004c75f6  0fbf4e10              movsx     ecx,word [esi+10h]
  004c75fa  2bcd                  sub       ecx,ebp
  004c75fc  0f8490000000          je        near 004C7692h
  004c7602  49                    dec       ecx
  004c7603  0f8597000000          jne       near 004C76A0h
  004c7609  a900000002            test      eax,2000000h
  004c760e  0f848c000000          je        near 004C76A0h
  004c7614  a1cc975e00            mov       eax,[5E97CCh]
  004c7619  6aff                  push      0FFFFFFFFh
  004c761b  6aff                  push      0FFFFFFFFh
  004c761d  8b88cc000000          mov       ecx,[eax+0CCh]
  004c7623  33c0                  xor       eax,eax
  004c7625  8b11                  mov       edx,[ecx]
  004c7627  8a4217                mov       al,[edx+17h]
  004c762a  05c0000000            add       eax,0C0h
  004c762f  50                    push      eax
  004c7630  56                    push      esi
  004c7631  e89a33fbff            call      0047A9D0h
  004c7636  83c410                add       esp,10h
  004c7639  66ff4e10              dec       word [esi+10h]
  004c763d  c646430a              mov       byte [esi+43h],0Ah
  004c7641  896e5c                mov       [esi+5Ch],ebp
  004c7644  e96a030000            jmp       004C79B3h
  004c7649  0fbf4e10              movsx     ecx,word [esi+10h]
  004c764d  2bcd                  sub       ecx,ebp
  004c764f  7441                  je        short 004C7692h
  004c7651  49                    dec       ecx
  004c7652  754c                  jne       short 004C76A0h
  004c7654  a900000002            test      eax,2000000h
  004c7659  7445                  je        short 004C76A0h
  004c765b  8b0dcc975e00          mov       ecx,[5E97CCh]
  004c7661  6aff                  push      0FFFFFFFFh
  004c7663  6aff                  push      0FFFFFFFFh
  004c7665  8b91cc000000          mov       edx,[ecx+0CCh]
  004c766b  33c9                  xor       ecx,ecx
  004c766d  8b02                  mov       eax,[edx]
  004c766f  8a4818                mov       cl,[eax+18h]
  004c7672  81c1c0000000          add       ecx,0C0h
  004c7678  51                    push      ecx
  004c7679  56                    push      esi
  004c767a  e85133fbff            call      0047A9D0h
  004c767f  83c410                add       esp,10h
  004c7682  66ff4e10              dec       word [esi+10h]
  004c7686  c646433c              mov       byte [esi+43h],3Ch
  004c768a  896e5c                mov       [esi+5Ch],ebp
  004c768d  e921030000            jmp       004C79B3h

LAB_004c7692:
  004c7692  8a4e43                mov       cl,[esi+43h]
  004c7695  fec9                  dec       cl
  004c7697  884e43                mov       [esi+43h],cl
  004c769a  7504                  jne       short 004C76A0h
  004c769c  24fd                  and       al,0FDh
  004c769e  8906                  mov       [esi],eax

LAB_004c76a0:
  004c76a0  896e5c                mov       [esi+5Ch],ebp
  004c76a3  e90b030000            jmp       004C79B3h

LAB_004c76a8:
  004c76a8  a810                  test      al,10h
  004c76aa  0f8503030000          jne       near 004C79B3h
  004c76b0  a904080000            test      eax,804h
  004c76b5  0f842b010000          je        near 004C77E6h
  004c76bb  f7442418e8030040      test      dword [esp+18h],400003E8h
  004c76c3  740b                  je        short 004C76D0h
  004c76c5  25fbf7ffff            and       eax,0FFFFF7FBh
  004c76ca  c6464300              mov       byte [esi+43h],0
  004c76ce  8906                  mov       [esi],eax

LAB_004c76d0:
  004c76d0  668b4610              mov       ax,[esi+10h]
  004c76d4  0fbfc8                movsx     ecx,ax
  004c76d7  83f903                cmp       ecx,3
  004c76da  0f87d3020000          ja        near 004C79B3h
  004c76e0  ff248dc87c4c00        jmp       dword [ecx*4+4C7CC8h]
  004c76e7  8b06                  mov       eax,[esi]
  004c76e9  a900000002            test      eax,2000000h
  004c76ee  0f84bf020000          je        near 004C79B3h
  004c76f4  80e4f7                and       ah,0F7h
  004c76f7  8906                  mov       [esi],eax
  004c76f9  e9b5020000            jmp       004C79B3h
  004c76fe  b807000000            mov       eax,7
  004c7703  66394618              cmp       [esi+18h],ax
  004c7707  7411                  je        short 004C771Ah
  004c7709  894618                mov       [esi+18h],eax
  004c770c  8b06                  mov       eax,[esi]
  004c770e  25fbfffffe            and       eax,0FEFFFFFBh
  004c7713  8906                  mov       [esi],eax
  004c7715  e999020000            jmp       004C79B3h

LAB_004c771a:
  004c771a  66837e1a0e            cmp       word [esi+1Ah],0Eh
  004c771f  0f858e020000          jne       near 004C79B3h
  004c7725  33c0                  xor       eax,eax
  004c7727  6aff                  push      0FFFFFFFFh
  004c7729  668b4606              mov       ax,[esi+6]
  004c772d  6aff                  push      0FFFFFFFFh
  004c772f  8bd0                  mov       edx,eax
  004c7731  c1f808                sar       eax,8
  004c7734  81e2ff000000          and       edx,0FFh
  004c773a  25ff000000            and       eax,0FFh
  004c773f  52                    push      edx
  004c7740  50                    push      eax
  004c7741  56                    push      esi
  004c7742  e839320700            call      0053A980h
  004c7747  83c414                add       esp,14h
  004c774a  66ff4e10              dec       word [esi+10h]
  004c774e  e960020000            jmp       004C79B3h
  004c7753  8a4e43                mov       cl,[esi+43h]
  004c7756  fec9                  dec       cl
  004c7758  884e43                mov       [esi+43h],cl
  004c775b  7505                  jne       short 004C7762h
  004c775d  48                    dec       eax
  004c775e  66894610              mov       [esi+10h],ax

LAB_004c7762:
  004c7762  8b442434              mov       eax,[esp+34h]
  004c7766  8d8880000000          lea       ecx,[eax+80h]
  004c776c  81e1ff0f0000          and       ecx,0FFFh
  004c7772  81f900010000          cmp       ecx,100h
  004c7778  0f8e31020000          jle       near 004C79AFh
  004c777e  e911020000            jmp       004C7994h
  004c7783  55                    push      ebp
  004c7784  55                    push      ebp
  004c7785  68bd000000            push      0BDh
  004c778a  56                    push      esi
  004c778b  e85033fbff            call      0047AAE0h
  004c7790  83c410                add       esp,10h
  004c7793  e8a8f90700            call      00547140h
  004c7798  99                    cdq
  004c7799  b907000000            mov       ecx,7
  004c779e  f7f9                  idiv      ecx
  004c77a0  33c0                  xor       eax,eax
  004c77a2  668b0455e82b5500      mov       ax,[edx*2+552BE8h]
  004c77aa  66894606              mov       [esi+6],ax
  004c77ae  8b0dc4975e00          mov       ecx,[5E97C4h]
  004c77b4  8bd0                  mov       edx,eax
  004c77b6  25ff000000            and       eax,0FFh
  004c77bb  8b899c000000          mov       ecx,[ecx+9Ch]
  004c77c1  c1fa08                sar       edx,8
  004c77c4  81e2ff000000          and       edx,0FFh
  004c77ca  8d0440                lea       eax,[eax+eax*2]
  004c77cd  c1e004                shl       eax,4
  004c77d0  8b0c91                mov       ecx,[ecx+edx*4]
  004c77d3  8a540113              mov       dl,[ecx+eax+13h]
  004c77d7  66ff4e10              dec       word [esi+10h]
  004c77db  885643                mov       [esi+43h],dl
  004c77de  896e5c                mov       [esi+5Ch],ebp
  004c77e1  e9cd010000            jmp       004C79B3h

LAB_004c77e6:
  004c77e6  33c9                  xor       ecx,ecx
  004c77e8  8a4e42                mov       cl,[esi+42h]
  004c77eb  83f903                cmp       ecx,3
  004c77ee  0f87bf010000          ja        near 004C79B3h
  004c77f4  ff248dd87c4c00        jmp       dword [ecx*4+4C7CD8h]
  004c77fb  66396e18              cmp       [esi+18h],bp
  004c77ff  740a                  je        short 004C780Bh
  004c7801  0d00000001            or        eax,1000000h
  004c7806  896e18                mov       [esi+18h],ebp
  004c7809  8906                  mov       [esi],eax

LAB_004c780b:
  004c780b  395c2410              cmp       [esp+10h],ebx
  004c780f  7d09                  jge       short 004C781Ah
  004c7811  c6464300              mov       byte [esi+43h],0
  004c7815  e999010000            jmp       004C79B3h

LAB_004c781a:
  004c781a  8a4643                mov       al,[esi+43h]
  004c781d  84c0                  test      al,al
  004c781f  7405                  je        short 004C7826h
  004c7821  fec8                  dec       al
  004c7823  884643                mov       [esi+43h],al

LAB_004c7826:
  004c7826  8b465c                mov       eax,[esi+5Ch]
  004c7829  8bc8                  mov       ecx,eax
  004c782b  c1f904                sar       ecx,4
  004c782e  2bc1                  sub       eax,ecx
  004c7830  89465c                mov       [esi+5Ch],eax
  004c7833  e97b010000            jmp       004C79B3h
  004c7838  66396e18              cmp       [esi+18h],bp
  004c783c  740a                  je        short 004C7848h
  004c783e  0d00000001            or        eax,1000000h
  004c7843  896e18                mov       [esi+18h],ebp
  004c7846  8906                  mov       [esi],eax

LAB_004c7848:
  004c7848  8b442434              mov       eax,[esp+34h]
  004c784c  8d9080000000          lea       edx,[eax+80h]
  004c7852  81e2ff0f0000          and       edx,0FFFh
  004c7858  81fa00010000          cmp       edx,100h
  004c785e  7f1a                  jg        short 004C787Ah
  004c7860  66014632              add       [esi+32h],ax
  004c7864  8b465c                mov       eax,[esi+5Ch]
  004c7867  8bd0                  mov       edx,eax
  004c7869  c6464300              mov       byte [esi+43h],0
  004c786d  c1fa04                sar       edx,4
  004c7870  2bc2                  sub       eax,edx
  004c7872  89465c                mov       [esi+5Ch],eax
  004c7875  e939010000            jmp       004C79B3h

LAB_004c787a:
  004c787a  25ff0f0000            and       eax,0FFFh
  004c787f  33c9                  xor       ecx,ecx
  004c7881  3d00080000            cmp       eax,800h
  004c7886  0f9dc1                setge     cl
  004c7889  49                    dec       ecx
  004c788a  81e100010000          and       ecx,100h
  004c7890  83c180                add       ecx,0FFFFFF80h
  004c7893  8bc1                  mov       eax,ecx
  004c7895  66014632              add       [esi+32h],ax
  004c7899  8b465c                mov       eax,[esi+5Ch]
  004c789c  8bd0                  mov       edx,eax
  004c789e  c1fa04                sar       edx,4
  004c78a1  2bc2                  sub       eax,edx
  004c78a3  89465c                mov       [esi+5Ch],eax
  004c78a6  e908010000            jmp       004C79B3h
  004c78ab  66397e18              cmp       [esi+18h],di
  004c78af  740a                  je        short 004C78BBh
  004c78b1  0d00000001            or        eax,1000000h
  004c78b6  897e18                mov       [esi+18h],edi
  004c78b9  8906                  mov       [esi],eax

LAB_004c78bb:
  004c78bb  395c2410              cmp       [esp+10h],ebx
  004c78bf  7d09                  jge       short 004C78CAh
  004c78c1  c6464300              mov       byte [esi+43h],0
  004c78c5  e9e9000000            jmp       004C79B3h

LAB_004c78ca:
  004c78ca  8a4643                mov       al,[esi+43h]
  004c78cd  84c0                  test      al,al
  004c78cf  7405                  je        short 004C78D6h
  004c78d1  fec8                  dec       al
  004c78d3  884643                mov       [esi+43h],al

LAB_004c78d6:
  004c78d6  8b442434              mov       eax,[esp+34h]
  004c78da  8d8880000000          lea       ecx,[eax+80h]
  004c78e0  81e1ff0f0000          and       ecx,0FFFh
  004c78e6  81f900010000          cmp       ecx,100h
  004c78ec  7e1b                  jle       short 004C7909h
  004c78ee  25ff0f0000            and       eax,0FFFh
  004c78f3  33d2                  xor       edx,edx
  004c78f5  3d00080000            cmp       eax,800h
  004c78fa  0f9dc2                setge     dl
  004c78fd  4a                    dec       edx
  004c78fe  81e200010000          and       edx,100h
  004c7904  83c280                add       edx,0FFFFFF80h
  004c7907  8bc2                  mov       eax,edx

LAB_004c7909:
  004c7909  66014632              add       [esi+32h],ax
  004c790d  8b465c                mov       eax,[esi+5Ch]
  004c7910  b900000100            mov       ecx,10000h
  004c7915  2bc8                  sub       ecx,eax
  004c7917  c1f904                sar       ecx,4
  004c791a  03c8                  add       ecx,eax
  004c791c  894e5c                mov       [esi+5Ch],ecx
  004c791f  e98f000000            jmp       004C79B3h
  004c7924  66396e18              cmp       [esi+18h],bp
  004c7928  740a                  je        short 004C7934h
  004c792a  0d00000001            or        eax,1000000h
  004c792f  896e18                mov       [esi+18h],ebp
  004c7932  8906                  mov       [esi],eax

LAB_004c7934:
  004c7934  8a4643                mov       al,[esi+43h]
  004c7937  84c0                  test      al,al
  004c7939  7405                  je        short 004C7940h
  004c793b  fec8                  dec       al
  004c793d  884643                mov       [esi+43h],al

LAB_004c7940:
  004c7940  0fbf5610              movsx     edx,word [esi+10h]
  004c7944  52                    push      edx
  004c7945  c7465c00400000        mov       dword [esi+5Ch],4000h
  004c794c  e8cbe00700            call      00545A1Ch
  004c7951  0faf465c              imul      eax,[esi+5Ch]
  004c7955  8b7e50                mov       edi,[esi+50h]
  004c7958  c1f80c                sar       eax,0Ch
  004c795b  03f8                  add       edi,eax
  004c795d  0fbf4610              movsx     eax,word [esi+10h]
  004c7961  50                    push      eax
  004c7962  897e50                mov       [esi+50h],edi
  004c7965  e8ace00700            call      00545A16h
  004c796a  0faf465c              imul      eax,[esi+5Ch]
  004c796e  8b5e58                mov       ebx,[esi+58h]
  004c7971  896e5c                mov       [esi+5Ch],ebp
  004c7974  c1f80c                sar       eax,0Ch
  004c7977  03d8                  add       ebx,eax
  004c7979  8b442434              mov       eax,[esp+34h]
  004c797d  895e58                mov       [esi+58h],ebx
  004c7980  8d8880000000          lea       ecx,[eax+80h]
  004c7986  81e1ff0f0000          and       ecx,0FFFh
  004c798c  81f900010000          cmp       ecx,100h
  004c7992  7e1b                  jle       short 004C79AFh

LAB_004c7994:
  004c7994  25ff0f0000            and       eax,0FFFh
  004c7999  33d2                  xor       edx,edx
  004c799b  3d00080000            cmp       eax,800h
  004c79a0  0f9dc2                setge     dl
  004c79a3  4a                    dec       edx
  004c79a4  81e200010000          and       edx,100h
  004c79aa  83c280                add       edx,0FFFFFF80h
  004c79ad  8bc2                  mov       eax,edx

LAB_004c79af:
  004c79af  66014632              add       [esi+32h],ax

LAB_004c79b3:
  004c79b3  8b442414              mov       eax,[esp+14h]
  004c79b7  50                    push      eax
  004c79b8  56                    push      esi
  004c79b9  e8222ffdff            call      0049A8E0h
  004c79be  83c408                add       esp,8
  004c79c1  56                    push      esi
  004c79c2  e879bf0600            call      00533940h
  004c79c7  8b9690000000          mov       edx,[esi+90h]
  004c79cd  8b466c                mov       eax,[esi+6Ch]
  004c79d0  81e2ffd8ffdf          and       edx,0DFFFD8FFh
  004c79d6  83c404                add       esp,4
  004c79d9  899690000000          mov       [esi+90h],edx
  004c79df  8a08                  mov       cl,[eax]
  004c79e1  f6c101                test      cl,1
  004c79e4  7506                  jne       short 004C79ECh
  004c79e6  8b4860                mov       ecx,[eax+60h]
  004c79e9  894e48                mov       [esi+48h],ecx

LAB_004c79ec:
  004c79ec  e84ff70700            call      00547140h
  004c79f1  8b7e60                mov       edi,[esi+60h]
  004c79f4  25ff000000            and       eax,0FFh
  004c79f9  0500010000            add       eax,100h
  004c79fe  66014614              add       [esi+14h],ax
  004c7a02  8b4648                mov       eax,[esi+48h]
  004c7a05  668b4e14              mov       cx,[esi+14h]
  004c7a09  3bc7                  cmp       eax,edi
  004c7a0b  7c02                  jl        short 004C7A0Fh
  004c7a0d  8bf8                  mov       edi,eax

LAB_004c7a0f:
  004c7a0f  f60602                test      byte [esi],2
  004c7a12  7415                  je        short 004C7A29h
  004c7a14  8b4620                mov       eax,[esi+20h]
  004c7a17  2bf8                  sub       edi,eax
  004c7a19  81c700800200          add       edi,28000h
  004c7a1f  c1ff03                sar       edi,3
  004c7a22  03f8                  add       edi,eax
  004c7a24  897e20                mov       [esi+20h],edi
  004c7a27  eb22                  jmp       short 004C7A4Bh

LAB_004c7a29:
  004c7a29  8b6e20                mov       ebp,[esi+20h]
  004c7a2c  0fbfd1                movsx     edx,cx
  004c7a2f  52                    push      edx
  004c7a30  e8e7df0700            call      00545A1Ch
  004c7a35  0500410000            add       eax,4100h
  004c7a3a  c1e005                shl       eax,5
  004c7a3d  03c7                  add       eax,edi
  004c7a3f  2bc5                  sub       eax,ebp
  004c7a41  c1f804                sar       eax,4
  004c7a44  03c5                  add       eax,ebp
  004c7a46  33ed                  xor       ebp,ebp
  004c7a48  894620                mov       [esi+20h],eax

LAB_004c7a4b:
  004c7a4b  8b442420              mov       eax,[esp+20h]
  004c7a4f  50                    push      eax
  004c7a50  56                    push      esi
  004c7a51  e89aa2fdff            call      004A1CF0h
  004c7a56  8b44241c              mov       eax,[esp+1Ch]
  004c7a5a  83c408                add       esp,8
  004c7a5d  3bc5                  cmp       eax,ebp
  004c7a5f  0f8441020000          je        near 004C7CA6h
  004c7a65  f70616080000          test      dword [esi],816h
  004c7a6b  0f8535020000          jne       near 004C7CA6h
  004c7a71  8a4643                mov       al,[esi+43h]
  004c7a74  84c0                  test      al,al
  004c7a76  0f852a020000          jne       near 004C7CA6h
  004c7a7c  8b466c                mov       eax,[esi+6Ch]
  004c7a7f  8b5e1c                mov       ebx,[esi+1Ch]
  004c7a82  8b4e24                mov       ecx,[esi+24h]
  004c7a85  8b781c                mov       edi,[eax+1Ch]
  004c7a88  8b6824                mov       ebp,[eax+24h]
  004c7a8b  2bfb                  sub       edi,ebx
  004c7a8d  2be9                  sub       ebp,ecx
  004c7a8f  c1ff0c                sar       edi,0Ch
  004c7a92  c1fd0c                sar       ebp,0Ch
  004c7a95  8bcf                  mov       ecx,edi
  004c7a97  8bd5                  mov       edx,ebp
  004c7a99  0fafcf                imul      ecx,edi
  004c7a9c  0fafd5                imul      edx,ebp
  004c7a9f  03ca                  add       ecx,edx
  004c7aa1  51                    push      ecx
  004c7aa2  e869df0700            call      00545A10h
  004c7aa7  55                    push      ebp
  004c7aa8  57                    push      edi
  004c7aa9  89442418              mov       [esp+18h],eax
  004c7aad  e870df0700            call      00545A22h
  004c7ab2  668b4e32              mov       cx,[esi+32h]
  004c7ab6  2bc1                  sub       eax,ecx
  004c7ab8  25ff0f0000            and       eax,0FFFh
  004c7abd  89442434              mov       [esp+34h],eax
  004c7ac1  e87af60700            call      00547140h
  004c7ac6  99                    cdq
  004c7ac7  b964000000            mov       ecx,64h
  004c7acc  f7f9                  idiv      ecx
  004c7ace  817c24102c010000      cmp       dword [esp+10h],12Ch
  004c7ad6  7d49                  jge       short 004C7B21h
  004c7ad8  83fa3c                cmp       edx,3Ch
  004c7adb  0f8d8e010000          jge       near 004C7C6Fh
  004c7ae1  8b542434              mov       edx,[esp+34h]
  004c7ae5  81c200010000          add       edx,100h
  004c7aeb  81e2ff0f0000          and       edx,0FFFh
  004c7af1  81fa00020000          cmp       edx,200h
  004c7af7  0f8f3e010000          jg        near 004C7C3Bh
  004c7afd  8b06                  mov       eax,[esi]
  004c7aff  c7461803000000        mov       dword [esi+18h],3
  004c7b06  25fffffffe            and       eax,0FEFFFFFFh
  004c7b0b  c6464200              mov       byte [esi+42h],0
  004c7b0f  0c02                  or        al,2
  004c7b11  66c746100100          mov       word [esi+10h],1
  004c7b17  8906                  mov       [esi],eax
  004c7b19  5f                    pop       edi
  004c7b1a  5e                    pop       esi
  004c7b1b  5d                    pop       ebp
  004c7b1c  5b                    pop       ebx
  004c7b1d  83c420                add       esp,20h
  004c7b20  c3                    ret

LAB_004c7b21:
  004c7b21  83fa1e                cmp       edx,1Eh
  004c7b24  0f8d21010000          jge       near 004C7C4Bh
  004c7b2a  8b542434              mov       edx,[esp+34h]
  004c7b2e  81c200010000          add       edx,100h
  004c7b34  81e2ff0f0000          and       edx,0FFFh
  004c7b3a  81fa00020000          cmp       edx,200h
  004c7b40  0f8ff5000000          jg        near 004C7C3Bh
  004c7b46  e8f5f50700            call      00547140h
  004c7b4b  99                    cdq
  004c7b4c  b906000000            mov       ecx,6
  004c7b51  f7f9                  idiv      ecx
  004c7b53  83fa05                cmp       edx,5
  004c7b56  0f874a010000          ja        near 004C7CA6h
  004c7b5c  ff2495e87c4c00        jmp       dword [edx*4+4C7CE8h]
  004c7b63  8b06                  mov       eax,[esi]
  004c7b65  c7461806000000        mov       dword [esi+18h],6
  004c7b6c  0d04080001            or        eax,1000804h
  004c7b71  66c746100300          mov       word [esi+10h],3
  004c7b77  8906                  mov       [esi],eax
  004c7b79  5f                    pop       edi
  004c7b7a  5e                    pop       esi
  004c7b7b  5d                    pop       ebp
  004c7b7c  5b                    pop       ebx
  004c7b7d  83c420                add       esp,20h
  004c7b80  c3                    ret
  004c7b81  8b16                  mov       edx,[esi]
  004c7b83  c7461803000000        mov       dword [esi+18h],3
  004c7b8a  81e2fffffffe          and       edx,0FEFFFFFFh
  004c7b90  c6464201              mov       byte [esi+42h],1
  004c7b94  83ca02                or        edx,2
  004c7b97  66c746100100          mov       word [esi+10h],1
  004c7b9d  8916                  mov       [esi],edx
  004c7b9f  5f                    pop       edi
  004c7ba0  5e                    pop       esi
  004c7ba1  5d                    pop       ebp
  004c7ba2  5b                    pop       ebx
  004c7ba3  83c420                add       esp,20h
  004c7ba6  c3                    ret
  004c7ba7  8b06                  mov       eax,[esi]
  004c7ba9  c7461803000000        mov       dword [esi+18h],3
  004c7bb0  25fffffffe            and       eax,0FEFFFFFFh
  004c7bb5  c6464202              mov       byte [esi+42h],2
  004c7bb9  0c02                  or        al,2
  004c7bbb  66c746100100          mov       word [esi+10h],1
  004c7bc1  8906                  mov       [esi],eax
  004c7bc3  5f                    pop       edi
  004c7bc4  5e                    pop       esi
  004c7bc5  5d                    pop       ebp
  004c7bc6  5b                    pop       ebx
  004c7bc7  83c420                add       esp,20h
  004c7bca  c3                    ret
  004c7bcb  8b0e                  mov       ecx,[esi]
  004c7bcd  c7461803000000        mov       dword [esi+18h],3
  004c7bd4  81e1fffffffe          and       ecx,0FEFFFFFFh
  004c7bda  c6464203              mov       byte [esi+42h],3
  004c7bde  83c902                or        ecx,2
  004c7be1  66c746100100          mov       word [esi+10h],1
  004c7be7  890e                  mov       [esi],ecx
  004c7be9  5f                    pop       edi
  004c7bea  5e                    pop       esi
  004c7beb  5d                    pop       ebp
  004c7bec  5b                    pop       ebx
  004c7bed  83c420                add       esp,20h
  004c7bf0  c3                    ret
  004c7bf1  8b16                  mov       edx,[esi]
  004c7bf3  c7461803000000        mov       dword [esi+18h],3
  004c7bfa  81e2fffffffe          and       edx,0FEFFFFFFh
  004c7c00  c6464204              mov       byte [esi+42h],4
  004c7c04  83ca02                or        edx,2
  004c7c07  66c746100100          mov       word [esi+10h],1
  004c7c0d  8916                  mov       [esi],edx
  004c7c0f  5f                    pop       edi
  004c7c10  5e                    pop       esi
  004c7c11  5d                    pop       ebp
  004c7c12  5b                    pop       ebx
  004c7c13  83c420                add       esp,20h
  004c7c16  c3                    ret
  004c7c17  8b06                  mov       eax,[esi]
  004c7c19  c7461803000000        mov       dword [esi+18h],3
  004c7c20  25fffffffe            and       eax,0FEFFFFFFh
  004c7c25  c6464205              mov       byte [esi+42h],5
  004c7c29  0c02                  or        al,2
  004c7c2b  66c746100100          mov       word [esi+10h],1
  004c7c31  8906                  mov       [esi],eax
  004c7c33  5f                    pop       edi
  004c7c34  5e                    pop       esi
  004c7c35  5d                    pop       ebp
  004c7c36  5b                    pop       ebx
  004c7c37  83c420                add       esp,20h
  004c7c3a  c3                    ret

LAB_004c7c3b:
  004c7c3b  c6464201              mov       byte [esi+42h],1
  004c7c3f  c6464301              mov       byte [esi+43h],1
  004c7c43  5f                    pop       edi
  004c7c44  5e                    pop       esi
  004c7c45  5d                    pop       ebp
  004c7c46  5b                    pop       ebx
  004c7c47  83c420                add       esp,20h
  004c7c4a  c3                    ret

LAB_004c7c4b:
  004c7c4b  83fa50                cmp       edx,50h
  004c7c4e  7d1f                  jge       short 004C7C6Fh
  004c7c50  c6464202              mov       byte [esi+42h],2
  004c7c54  e8e7f40700            call      00547140h
  004c7c59  99                    cdq
  004c7c5a  b90a000000            mov       ecx,0Ah
  004c7c5f  f7f9                  idiv      ecx
  004c7c61  80c214                add       dl,14h
  004c7c64  885643                mov       [esi+43h],dl
  004c7c67  5f                    pop       edi
  004c7c68  5e                    pop       esi
  004c7c69  5d                    pop       ebp
  004c7c6a  5b                    pop       ebx
  004c7c6b  83c420                add       esp,20h
  004c7c6e  c3                    ret

LAB_004c7c6f:
  004c7c6f  c6464203              mov       byte [esi+42h],3
  004c7c73  e8c8f40700            call      00547140h
  004c7c78  99                    cdq
  004c7c79  b90a000000            mov       ecx,0Ah
  004c7c7e  f7f9                  idiv      ecx
  004c7c80  02d1                  add       dl,cl
  004c7c82  885643                mov       [esi+43h],dl
  004c7c85  e8b6f40700            call      00547140h
  004c7c8a  99                    cdq
  004c7c8b  b9000c0000            mov       ecx,0C00h
  004c7c90  8b7c2434              mov       edi,[esp+34h]
  004c7c94  f7f9                  idiv      ecx
  004c7c96  03d7                  add       edx,edi
  004c7c98  66035632              add       dx,[esi+32h]
  004c7c9c  81ea00060000          sub       edx,600h
  004c7ca2  66895610              mov       [esi+10h],dx

LAB_004c7ca6:
  004c7ca6  5f                    pop       edi
  004c7ca7  5e                    pop       esi
  004c7ca8  5d                    pop       ebp
  004c7ca9  5b                    pop       ebx
  004c7caa  83c420                add       esp,20h
  004c7cad  c3                    ret
  004c7cae  8bff                  mov       edi,edi
  004c7cb0  df724c                fbstp     tword [edx+4Ch]
  004c7cb3  0054744c              add       [esp+esi*2+4Ch],dl
  004c7cb7  0003                  add       [ebx],al
  004c7cb9  754c                  jne       short 004C7D07h
  004c7cbb  007375                add       [ebx+75h],dh
  004c7cbe  4c                    dec       esp
  004c7cbf  00f6                  add       dh,dh
  004c7cc1  754c                  jne       short 004C7D0Fh
  004c7cc3  004976                add       [ecx+76h],cl
  004c7cc6  4c                    dec       esp
  004c7cc7  00e7                  add       bh,ah
  004c7cc9  764c                  jbe       short 004C7D17h
  004c7ccb  00fe                  add       dh,bh
  004c7ccd  764c                  jbe       short 004C7D1Bh
  004c7ccf  005377                add       [ebx+77h],dl
  004c7cd2  4c                    dec       esp
  004c7cd3  0083774c00fb          add       [ebx-4FFB389h],al
  004c7cd9  774c                  ja        short 004C7D27h
  004c7cdb  0038                  add       [eax],bh
  004c7cdd  784c                  js        short 004C7D2Bh
  004c7cdf  00ab784c0024          add       [ebx+24004C78h],ch
  004c7ce5  794c                  jns       short 004C7D33h
  004c7ce7  00637b                add       [ebx+7Bh],ah
  004c7cea  4c                    dec       esp
  004c7ceb  00817b4c00a7          add       [ecx-58FFB385h],al
  004c7cf1  7b4c                  jnp       short 004C7D3Fh
  004c7cf3  00cb                  add       bl,cl
  004c7cf5  7b4c                  jnp       short 004C7D43h
  004c7cf7  00f1                  add       cl,dh
  004c7cf9  7b4c                  jnp       short 004C7D47h
  004c7cfb  0017                  add       [edi],dl
  004c7cfd  7c4c                  jl        short 004C7D4Bh
  004c7cff  00                    (bad)
  (octet invalide - la plage demandee deborde probablement du code)
