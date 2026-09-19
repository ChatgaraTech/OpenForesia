Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004e84e0  section .text  offset fichier 0xe78e0  2320 octets

  004e84e0  83ec2c                sub       esp,2Ch
  004e84e3  53                    push      ebx
  004e84e4  55                    push      ebp
  004e84e5  56                    push      esi
  004e84e6  8b74243c              mov       esi,[esp+3Ch]
  004e84ea  8d44241c              lea       eax,[esp+1Ch]
  004e84ee  57                    push      edi
  004e84ef  8d4c2438              lea       ecx,[esp+38h]
  004e84f3  50                    push      eax
  004e84f4  8d542428              lea       edx,[esp+28h]
  004e84f8  51                    push      ecx
  004e84f9  52                    push      edx
  004e84fa  56                    push      esi
  004e84fb  e880040500            call      00538980h
  004e8500  83c410                add       esp,10h
  004e8503  8d442428              lea       eax,[esp+28h]
  004e8507  8d4c241c              lea       ecx,[esp+1Ch]
  004e850b  8d542410              lea       edx,[esp+10h]
  004e850f  50                    push      eax
  004e8510  51                    push      ecx
  004e8511  8d442420              lea       eax,[esp+20h]
  004e8515  52                    push      edx
  004e8516  8d4c244c              lea       ecx,[esp+4Ch]
  004e851a  50                    push      eax
  004e851b  51                    push      ecx
  004e851c  56                    push      esi
  004e851d  e8fed90400            call      00535F20h
  004e8522  8b4c2458              mov       ecx,[esp+58h]
  004e8526  b840000000            mov       eax,40h
  004e852b  2bc1                  sub       eax,ecx
  004e852d  83c418                add       esp,18h
  004e8530  89442414              mov       [esp+14h],eax
  004e8534  8b44241c              mov       eax,[esp+1Ch]
  004e8538  85c0                  test      eax,eax
  004e853a  bb03000000            mov       ebx,3
  004e853f  bd10000000            mov       ebp,10h
  004e8544  0f845e050000          je        near 004E8AA8h
  004e854a  56                    push      esi
  004e854b  e85030ffff            call      004DB5A0h
  004e8550  8b06                  mov       eax,[esi]
  004e8552  83c404                add       esp,4
  004e8555  a802                  test      al,2
  004e8557  0f844a030000          je        near 004E88A7h
  004e855d  f644242810            test      byte [esp+28h],10h
  004e8562  740d                  je        short 004E8571h
  004e8564  24fd                  and       al,0FDh
  004e8566  c6464300              mov       byte [esi+43h],0
  004e856a  8906                  mov       [esi],eax
  004e856c  e937050000            jmp       004E8AA8h

LAB_004e8571:
  004e8571  33c9                  xor       ecx,ecx
  004e8573  8a4e42                mov       cl,[esi+42h]
  004e8576  83e900                sub       ecx,0
  004e8579  0f8458020000          je        near 004E87D7h
  004e857f  49                    dec       ecx
  004e8580  0f8522050000          jne       near 004E8AA8h
  004e8586  0fbf4610              movsx     eax,word [esi+10h]
  004e858a  83e800                sub       eax,0
  004e858d  0f84a6000000          je        near 004E8639h
  004e8593  48                    dec       eax
  004e8594  0f850e050000          jne       near 004E8AA8h
  004e859a  66396e1a              cmp       [esi+1Ah],bp
  004e859e  c7465c00000000        mov       dword [esi+5Ch],0
  004e85a5  7555                  jne       short 004E85FCh
  004e85a7  8b542440              mov       edx,[esp+40h]
  004e85ab  0fbf4632              movsx     eax,word [esi+32h]
  004e85af  81c200050000          add       edx,500h
  004e85b5  50                    push      eax
  004e85b6  c1e207                shl       edx,7
  004e85b9  89565c                mov       [esi+5Ch],edx
  004e85bc  e85bd40500            call      00545A1Ch
  004e85c1  0faf465c              imul      eax,[esi+5Ch]
  004e85c5  8b4e50                mov       ecx,[esi+50h]
  004e85c8  c1f80c                sar       eax,0Ch
  004e85cb  03c8                  add       ecx,eax
  004e85cd  894e50                mov       [esi+50h],ecx
  004e85d0  0fbf4e32              movsx     ecx,word [esi+32h]
  004e85d4  51                    push      ecx
  004e85d5  e83cd40500            call      00545A16h
  004e85da  0faf465c              imul      eax,[esi+5Ch]
  004e85de  8b5e58                mov       ebx,[esi+58h]
  004e85e1  c1f80c                sar       eax,0Ch
  004e85e4  03d8                  add       ebx,eax
  004e85e6  8b442440              mov       eax,[esp+40h]
  004e85ea  895e58                mov       [esi+58h],ebx
  004e85ed  8d5060                lea       edx,[eax+60h]
  004e85f0  c1e207                shl       edx,7
  004e85f3  66ff4e10              dec       word [esi+10h]
  004e85f7  89565c                mov       [esi+5Ch],edx
  004e85fa  eb04                  jmp       short 004E8600h

LAB_004e85fc:
  004e85fc  8b442440              mov       eax,[esp+40h]

LAB_004e8600:
  004e8600  8b4c2410              mov       ecx,[esp+10h]
  004e8604  d1f8                  sar       eax,1
  004e8606  83c040                add       eax,40h
  004e8609  8d1408                lea       edx,[eax+ecx]
  004e860c  8d3c00                lea       edi,[eax+eax]
  004e860f  81e2ff0f0000          and       edx,0FFFh
  004e8615  3bd7                  cmp       edx,edi
  004e8617  7f09                  jg        short 004E8622h
  004e8619  66014e32              add       [esi+32h],cx
  004e861d  e986040000            jmp       004E8AA8h

LAB_004e8622:
  004e8622  81e1ff0f0000          and       ecx,0FFFh
  004e8628  81f900080000          cmp       ecx,800h
  004e862e  0f8c70040000          jl        near 004E8AA4h
  004e8634  e969040000            jmp       004E8AA2h

LAB_004e8639:
  004e8639  8b4650                mov       eax,[esi+50h]
  004e863c  8b4e58                mov       ecx,[esi+58h]
  004e863f  c1f80c                sar       eax,0Ch
  004e8642  8bd0                  mov       edx,eax
  004e8644  c1f90c                sar       ecx,0Ch
  004e8647  0fafd0                imul      edx,eax
  004e864a  8bc1                  mov       eax,ecx
  004e864c  0fafc1                imul      eax,ecx
  004e864f  03d0                  add       edx,eax
  004e8651  52                    push      edx
  004e8652  e8b9d30500            call      00545A10h
  004e8657  8b16                  mov       edx,[esi]
  004e8659  8b4c2440              mov       ecx,[esp+40h]
  004e865d  f7c200000002          test      edx,2000000h
  004e8663  740f                  je        short 004E8674h
  004e8665  8bf9                  mov       edi,ecx
  004e8667  c1ff03                sar       edi,3
  004e866a  47                    inc       edi
  004e866b  3bc7                  cmp       eax,edi
  004e866d  7f05                  jg        short 004E8674h
  004e866f  83e2dd                and       edx,0FFFFFFDDh
  004e8672  8916                  mov       [esi],edx

LAB_004e8674:
  004e8674  3bc3                  cmp       eax,ebx
  004e8676  0f8c2c040000          jl        near 004E8AA8h
  004e867c  f60620                test      byte [esi],20h
  004e867f  0f8523040000          jne       near 004E8AA8h
  004e8685  c1f903                sar       ecx,3
  004e8688  83c120                add       ecx,20h
  004e868b  8bc1                  mov       eax,ecx
  004e868d  8b4c2410              mov       ecx,[esp+10h]
  004e8691  8d1408                lea       edx,[eax+ecx]
  004e8694  8d3c00                lea       edi,[eax+eax]
  004e8697  81e2ff0f0000          and       edx,0FFFh
  004e869d  3bd7                  cmp       edx,edi
  004e869f  7f06                  jg        short 004E86A7h
  004e86a1  66014e32              add       [esi+32h],cx
  004e86a5  eb14                  jmp       short 004E86BBh

LAB_004e86a7:
  004e86a7  81e1ff0f0000          and       ecx,0FFFh
  004e86ad  81f900080000          cmp       ecx,800h
  004e86b3  7c02                  jl        short 004E86B7h
  004e86b5  f7d8                  neg       eax

LAB_004e86b7:
  004e86b7  66014632              add       [esi+32h],ax

LAB_004e86bb:
  004e86bb  8d6e1c                lea       ebp,[esi+1Ch]
  004e86be  6a00                  push      0
  004e86c0  688c2e5500            push      552E8Ch
  004e86c5  55                    push      ebp
  004e86c6  56                    push      esi
  004e86c7  e864150300            call      00519C30h
  004e86cc  8bf8                  mov       edi,eax
  004e86ce  83c410                add       esp,10h
  004e86d1  85ff                  test      edi,edi
  004e86d3  7406                  je        short 004E86DBh
  004e86d5  8b06                  mov       eax,[esi]
  004e86d7  0c20                  or        al,20h
  004e86d9  8906                  mov       [esi],eax

LAB_004e86db:
  004e86db  a1b8e05d00            mov       eax,[5DE0B8h]
  004e86e0  85c0                  test      eax,eax
  004e86e2  0f84c0030000          je        near 004E8AA8h
  004e86e8  8b1f                  mov       ebx,[edi]
  004e86ea  66c787960000001000    mov       word [edi+96h],10h
  004e86f3  83cb40                or        ebx,40h
  004e86f6  57                    push      edi
  004e86f7  891f                  mov       [edi],ebx
  004e86f9  8b4c2424              mov       ecx,[esp+24h]
  004e86fd  56                    push      esi
  004e86fe  668b4130              mov       ax,[ecx+30h]
  004e8702  0fbfd8                movsx     ebx,ax
  004e8705  66d1f8                sar       ax,1
  004e8708  66894130              mov       [ecx+30h],ax
  004e870c  e86f140500            call      00539B80h
  004e8711  8b442428              mov       eax,[esp+28h]
  004e8715  83c408                add       esp,8
  004e8718  66895830              mov       [eax+30h],bx
  004e871c  8d5f1c                lea       ebx,[edi+1Ch]
  004e871f  53                    push      ebx
  004e8720  6800180000            push      1800h
  004e8725  6800030000            push      300h
  004e872a  6a03                  push      3
  004e872c  e8ef530500            call      0053DB20h
  004e8731  8b07                  mov       eax,[edi]
  004e8733  83c410                add       esp,10h
  004e8736  a900000040            test      eax,40000000h
  004e873b  0f8567030000          jne       near 004E8AA8h
  004e8741  0fbf4e32              movsx     ecx,word [esi+32h]
  004e8745  8b542410              mov       edx,[esp+10h]
  004e8749  03d1                  add       edx,ecx
  004e874b  52                    push      edx
  004e874c  e8cbd20500            call      00545A1Ch
  004e8751  8d0440                lea       eax,[eax+eax*2]
  004e8754  8d0480                lea       eax,[eax+eax*4]
  004e8757  d1e0                  shl       eax,1
  004e8759  894750                mov       [edi+50h],eax
  004e875c  8b542410              mov       edx,[esp+10h]
  004e8760  0fbf4e32              movsx     ecx,word [esi+32h]
  004e8764  03d1                  add       edx,ecx
  004e8766  52                    push      edx
  004e8767  e8aad20500            call      00545A16h
  004e876c  8d0440                lea       eax,[eax+eax*2]
  004e876f  8d0480                lea       eax,[eax+eax*4]
  004e8772  d1e0                  shl       eax,1
  004e8774  894758                mov       [edi+58h],eax
  004e8777  8b07                  mov       eax,[edi]
  004e8779  a801                  test      al,1
  004e877b  750b                  jne       short 004E8788h
  004e877d  0c01                  or        al,1
  004e877f  c7475400800100        mov       dword [edi+54h],18000h
  004e8786  8907                  mov       [edi],eax

LAB_004e8788:
  004e8788  8b4d00                mov       ecx,[ebp]
  004e878b  8b5620                mov       edx,[esi+20h]
  004e878e  8b4624                mov       eax,[esi+24h]
  004e8791  894c242c              mov       [esp+2Ch],ecx
  004e8795  8b0b                  mov       ecx,[ebx]
  004e8797  89542430              mov       [esp+30h],edx
  004e879b  894d00                mov       [ebp],ecx
  004e879e  8b5720                mov       edx,[edi+20h]
  004e87a1  6aff                  push      0FFFFFFFFh
  004e87a3  89442438              mov       [esp+38h],eax
  004e87a7  895620                mov       [esi+20h],edx
  004e87aa  8b4724                mov       eax,[edi+24h]
  004e87ad  6a00                  push      0
  004e87af  6a39                  push      39h
  004e87b1  56                    push      esi
  004e87b2  894624                mov       [esi+24h],eax
  004e87b5  e81622f9ff            call      0047A9D0h
  004e87ba  8b4c243c              mov       ecx,[esp+3Ch]
  004e87be  8b542440              mov       edx,[esp+40h]
  004e87c2  8b442444              mov       eax,[esp+44h]
  004e87c6  83c410                add       esp,10h
  004e87c9  894d00                mov       [ebp],ecx
  004e87cc  895620                mov       [esi+20h],edx
  004e87cf  894624                mov       [esi+24h],eax
  004e87d2  e9d1020000            jmp       004E8AA8h

LAB_004e87d7:
  004e87d7  66395e18              cmp       [esi+18h],bx
  004e87db  740a                  je        short 004E87E7h
  004e87dd  25fffffffe            and       eax,0FEFFFFFFh
  004e87e2  895e18                mov       [esi+18h],ebx
  004e87e5  8906                  mov       [esi],eax

LAB_004e87e7:
  004e87e7  8b442440              mov       eax,[esp+40h]
  004e87eb  8b4c2410              mov       ecx,[esp+10h]
  004e87ef  d1f8                  sar       eax,1
  004e87f1  83c040                add       eax,40h
  004e87f4  8d1408                lea       edx,[eax+ecx]
  004e87f7  8d3c00                lea       edi,[eax+eax]
  004e87fa  81e2ff0f0000          and       edx,0FFFh
  004e8800  3bd7                  cmp       edx,edi
  004e8802  7f06                  jg        short 004E880Ah
  004e8804  66014e32              add       [esi+32h],cx
  004e8808  eb14                  jmp       short 004E881Eh

LAB_004e880a:
  004e880a  81e1ff0f0000          and       ecx,0FFFh
  004e8810  81f900080000          cmp       ecx,800h
  004e8816  7c02                  jl        short 004E881Ah
  004e8818  f7d8                  neg       eax

LAB_004e881a:
  004e881a  66014632              add       [esi+32h],ax

LAB_004e881e:
  004e881e  6a00                  push      0
  004e8820  8d461c                lea       eax,[esi+1Ch]
  004e8823  68802e5500            push      552E80h
  004e8828  50                    push      eax
  004e8829  56                    push      esi
  004e882a  e801140300            call      00519C30h
  004e882f  8bf8                  mov       edi,eax
  004e8831  a1b8e05d00            mov       eax,[5DE0B8h]
  004e8836  83c410                add       esp,10h
  004e8839  85c0                  test      eax,eax
  004e883b  742f                  je        short 004E886Ch
  004e883d  6689af96000000        mov       [edi+96h],bp
  004e8844  8b2f                  mov       ebp,[edi]
  004e8846  83cd40                or        ebp,40h
  004e8849  57                    push      edi
  004e884a  56                    push      esi
  004e884b  892f                  mov       [edi],ebp
  004e884d  e82e130500            call      00539B80h
  004e8852  83c408                add       esp,8
  004e8855  83c71c                add       edi,1Ch
  004e8858  57                    push      edi
  004e8859  6800180000            push      1800h
  004e885e  6800030000            push      300h
  004e8863  53                    push      ebx
  004e8864  e8b7520500            call      0053DB20h
  004e8869  83c410                add       esp,10h

LAB_004e886c:
  004e886c  8b4c2440              mov       ecx,[esp+40h]
  004e8870  8b465c                mov       eax,[esi+5Ch]
  004e8873  81c180020000          add       ecx,280h
  004e8879  c1e107                shl       ecx,7
  004e887c  2bc8                  sub       ecx,eax
  004e887e  c1f904                sar       ecx,4
  004e8881  03c8                  add       ecx,eax
  004e8883  8a4643                mov       al,[esi+43h]
  004e8886  84c0                  test      al,al
  004e8888  894e5c                mov       [esi+5Ch],ecx
  004e888b  0f8417020000          je        near 004E8AA8h
  004e8891  fec8                  dec       al
  004e8893  884643                mov       [esi+43h],al
  004e8896  0f850c020000          jne       near 004E8AA8h
  004e889c  8b06                  mov       eax,[esi]
  004e889e  24fd                  and       al,0FDh
  004e88a0  8906                  mov       [esi],eax
  004e88a2  e901020000            jmp       004E8AA8h

LAB_004e88a7:
  004e88a7  a810                  test      al,10h
  004e88a9  0f85f9010000          jne       near 004E8AA8h
  004e88af  a904080000            test      eax,804h
  004e88b4  0f85ee010000          jne       near 004E8AA8h
  004e88ba  33c9                  xor       ecx,ecx
  004e88bc  8a4e42                mov       cl,[esi+42h]
  004e88bf  3bcb                  cmp       ecx,ebx
  004e88c1  0f87e1010000          ja        near 004E8AA8h
  004e88c7  ff248dd48d4e00        jmp       dword [ecx*4+4E8DD4h]
  004e88ce  66837e1800            cmp       word [esi+18h],0
  004e88d3  740e                  je        short 004E88E3h
  004e88d5  0d00000001            or        eax,1000000h
  004e88da  c7461800000000        mov       dword [esi+18h],0
  004e88e1  8906                  mov       [esi],eax

LAB_004e88e3:
  004e88e3  817c24182c010000      cmp       dword [esp+18h],12Ch
  004e88eb  7d09                  jge       short 004E88F6h
  004e88ed  c6464300              mov       byte [esi+43h],0
  004e88f1  e9b2010000            jmp       004E8AA8h

LAB_004e88f6:
  004e88f6  8a4643                mov       al,[esi+43h]
  004e88f9  84c0                  test      al,al
  004e88fb  7405                  je        short 004E8902h
  004e88fd  fec8                  dec       al
  004e88ff  884643                mov       [esi+43h],al

LAB_004e8902:
  004e8902  8b465c                mov       eax,[esi+5Ch]
  004e8905  8bd0                  mov       edx,eax
  004e8907  c1fa04                sar       edx,4
  004e890a  2bc2                  sub       eax,edx
  004e890c  89465c                mov       [esi+5Ch],eax
  004e890f  e994010000            jmp       004E8AA8h
  004e8914  66837e1800            cmp       word [esi+18h],0
  004e8919  740e                  je        short 004E8929h
  004e891b  0d00000001            or        eax,1000000h
  004e8920  c7461800000000        mov       dword [esi+18h],0
  004e8927  8906                  mov       [esi],eax

LAB_004e8929:
  004e8929  8b442440              mov       eax,[esp+40h]
  004e892d  8b4c2410              mov       ecx,[esp+10h]
  004e8931  c1f802                sar       eax,2
  004e8934  83c038                add       eax,38h
  004e8937  8d1408                lea       edx,[eax+ecx]
  004e893a  8d3c00                lea       edi,[eax+eax]
  004e893d  81e2ff0f0000          and       edx,0FFFh
  004e8943  3bd7                  cmp       edx,edi
  004e8945  7f1a                  jg        short 004E8961h
  004e8947  8b465c                mov       eax,[esi+5Ch]
  004e894a  66014e32              add       [esi+32h],cx
  004e894e  8bc8                  mov       ecx,eax
  004e8950  c6464300              mov       byte [esi+43h],0
  004e8954  c1f904                sar       ecx,4
  004e8957  2bc1                  sub       eax,ecx
  004e8959  89465c                mov       [esi+5Ch],eax
  004e895c  e947010000            jmp       004E8AA8h

LAB_004e8961:
  004e8961  81e1ff0f0000          and       ecx,0FFFh
  004e8967  81f900080000          cmp       ecx,800h
  004e896d  7c02                  jl        short 004E8971h
  004e896f  f7d8                  neg       eax

LAB_004e8971:
  004e8971  66014632              add       [esi+32h],ax
  004e8975  8b465c                mov       eax,[esi+5Ch]
  004e8978  8bc8                  mov       ecx,eax
  004e897a  c1f904                sar       ecx,4
  004e897d  2bc1                  sub       eax,ecx
  004e897f  89465c                mov       [esi+5Ch],eax
  004e8982  e921010000            jmp       004E8AA8h
  004e8987  b901000000            mov       ecx,1
  004e898c  66394e18              cmp       [esi+18h],cx
  004e8990  740a                  je        short 004E899Ch
  004e8992  0d00000001            or        eax,1000000h
  004e8997  894e18                mov       [esi+18h],ecx
  004e899a  8906                  mov       [esi],eax

LAB_004e899c:
  004e899c  817c24182c010000      cmp       dword [esp+18h],12Ch
  004e89a4  7d09                  jge       short 004E89AFh
  004e89a6  c6464300              mov       byte [esi+43h],0
  004e89aa  e9f9000000            jmp       004E8AA8h

LAB_004e89af:
  004e89af  8a4643                mov       al,[esi+43h]
  004e89b2  84c0                  test      al,al
  004e89b4  7405                  je        short 004E89BBh
  004e89b6  fec8                  dec       al
  004e89b8  884643                mov       [esi+43h],al

LAB_004e89bb:
  004e89bb  8b542440              mov       edx,[esp+40h]
  004e89bf  8b4c2410              mov       ecx,[esp+10h]
  004e89c3  8bc2                  mov       eax,edx
  004e89c5  c1f802                sar       eax,2
  004e89c8  83c038                add       eax,38h
  004e89cb  8d3c08                lea       edi,[eax+ecx]
  004e89ce  8d1c00                lea       ebx,[eax+eax]
  004e89d1  81e7ff0f0000          and       edi,0FFFh
  004e89d7  3bfb                  cmp       edi,ebx
  004e89d9  7f06                  jg        short 004E89E1h
  004e89db  66014e32              add       [esi+32h],cx
  004e89df  eb14                  jmp       short 004E89F5h

LAB_004e89e1:
  004e89e1  81e1ff0f0000          and       ecx,0FFFh
  004e89e7  81f900080000          cmp       ecx,800h
  004e89ed  7c02                  jl        short 004E89F1h
  004e89ef  f7d8                  neg       eax

LAB_004e89f1:
  004e89f1  66014632              add       [esi+32h],ax

LAB_004e89f5:
  004e89f5  8b465c                mov       eax,[esi+5Ch]
  004e89f8  81c2a0010000          add       edx,1A0h
  004e89fe  c1e207                shl       edx,7
  004e8a01  2bd0                  sub       edx,eax
  004e8a03  c1fa04                sar       edx,4
  004e8a06  03d0                  add       edx,eax
  004e8a08  89565c                mov       [esi+5Ch],edx
  004e8a0b  e998000000            jmp       004E8AA8h
  004e8a10  33ff                  xor       edi,edi
  004e8a12  66397e18              cmp       [esi+18h],di
  004e8a16  740a                  je        short 004E8A22h
  004e8a18  0d00000001            or        eax,1000000h
  004e8a1d  897e18                mov       [esi+18h],edi
  004e8a20  8906                  mov       [esi],eax

LAB_004e8a22:
  004e8a22  8a4643                mov       al,[esi+43h]
  004e8a25  84c0                  test      al,al
  004e8a27  7405                  je        short 004E8A2Eh
  004e8a29  fec8                  dec       al
  004e8a2b  884643                mov       [esi+43h],al

LAB_004e8a2e:
  004e8a2e  8b542440              mov       edx,[esp+40h]
  004e8a32  0fbf4610              movsx     eax,word [esi+10h]
  004e8a36  83c240                add       edx,40h
  004e8a39  50                    push      eax
  004e8a3a  c1e207                shl       edx,7
  004e8a3d  89565c                mov       [esi+5Ch],edx
  004e8a40  e8d7cf0500            call      00545A1Ch
  004e8a45  0faf465c              imul      eax,[esi+5Ch]
  004e8a49  8b4e50                mov       ecx,[esi+50h]
  004e8a4c  c1f80c                sar       eax,0Ch
  004e8a4f  03c8                  add       ecx,eax
  004e8a51  894e50                mov       [esi+50h],ecx
  004e8a54  0fbf4e10              movsx     ecx,word [esi+10h]
  004e8a58  51                    push      ecx
  004e8a59  e8b8cf0500            call      00545A16h
  004e8a5e  0faf465c              imul      eax,[esi+5Ch]
  004e8a62  8b5e58                mov       ebx,[esi+58h]
  004e8a65  8b4c2410              mov       ecx,[esp+10h]
  004e8a69  c1f80c                sar       eax,0Ch
  004e8a6c  03d8                  add       ebx,eax
  004e8a6e  8b442440              mov       eax,[esp+40h]
  004e8a72  c1f802                sar       eax,2
  004e8a75  83c040                add       eax,40h
  004e8a78  897e5c                mov       [esi+5Ch],edi
  004e8a7b  895e58                mov       [esi+58h],ebx
  004e8a7e  8d1408                lea       edx,[eax+ecx]
  004e8a81  8d3c00                lea       edi,[eax+eax]
  004e8a84  81e2ff0f0000          and       edx,0FFFh
  004e8a8a  3bd7                  cmp       edx,edi
  004e8a8c  7f06                  jg        short 004E8A94h
  004e8a8e  66014e32              add       [esi+32h],cx
  004e8a92  eb14                  jmp       short 004E8AA8h

LAB_004e8a94:
  004e8a94  81e1ff0f0000          and       ecx,0FFFh
  004e8a9a  81f900080000          cmp       ecx,800h
  004e8aa0  7c02                  jl        short 004E8AA4h

LAB_004e8aa2:
  004e8aa2  f7d8                  neg       eax

LAB_004e8aa4:
  004e8aa4  66014632              add       [esi+32h],ax

LAB_004e8aa8:
  004e8aa8  8b44241c              mov       eax,[esp+1Ch]
  004e8aac  50                    push      eax
  004e8aad  56                    push      esi
  004e8aae  e82d1efbff            call      0049A8E0h
  004e8ab3  83c408                add       esp,8
  004e8ab6  56                    push      esi
  004e8ab7  e884ae0400            call      00533940h
  004e8abc  8b466c                mov       eax,[esi+6Ch]
  004e8abf  83c404                add       esp,4
  004e8ac2  f60001                test      byte [eax],1
  004e8ac5  7506                  jne       short 004E8ACDh
  004e8ac7  8b4860                mov       ecx,[eax+60h]
  004e8aca  894e48                mov       [esi+48h],ecx

LAB_004e8acd:
  004e8acd  e86ee60500            call      00547140h
  004e8ad2  8b7e60                mov       edi,[esi+60h]
  004e8ad5  25ff000000            and       eax,0FFh
  004e8ada  0500010000            add       eax,100h
  004e8adf  66014614              add       [esi+14h],ax
  004e8ae3  8b4648                mov       eax,[esi+48h]
  004e8ae6  668b4e14              mov       cx,[esi+14h]
  004e8aea  3bc7                  cmp       eax,edi
  004e8aec  7c02                  jl        short 004E8AF0h
  004e8aee  8bf8                  mov       edi,eax

LAB_004e8af0:
  004e8af0  f60602                test      byte [esi],2
  004e8af3  7415                  je        short 004E8B0Ah
  004e8af5  8b4620                mov       eax,[esi+20h]
  004e8af8  2bf8                  sub       edi,eax
  004e8afa  81c700800200          add       edi,28000h
  004e8b00  c1ff03                sar       edi,3
  004e8b03  03f8                  add       edi,eax
  004e8b05  897e20                mov       [esi+20h],edi
  004e8b08  eb20                  jmp       short 004E8B2Ah

LAB_004e8b0a:
  004e8b0a  8b6e20                mov       ebp,[esi+20h]
  004e8b0d  0fbfd1                movsx     edx,cx
  004e8b10  52                    push      edx
  004e8b11  e806cf0500            call      00545A1Ch
  004e8b16  0500320000            add       eax,3200h
  004e8b1b  c1e006                shl       eax,6
  004e8b1e  03c7                  add       eax,edi
  004e8b20  2bc5                  sub       eax,ebp
  004e8b22  c1f804                sar       eax,4
  004e8b25  03c5                  add       eax,ebp
  004e8b27  894620                mov       [esi+20h],eax

LAB_004e8b2a:
  004e8b2a  66837e160a            cmp       word [esi+16h],0Ah
  004e8b2f  7c0d                  jl        short 004E8B3Eh
  004e8b31  8b442424              mov       eax,[esp+24h]
  004e8b35  50                    push      eax
  004e8b36  56                    push      esi
  004e8b37  e834acffff            call      004E3770h
  004e8b3c  eb1d                  jmp       short 004E8B5Bh

LAB_004e8b3e:
  004e8b3e  8bbe90000000          mov       edi,[esi+90h]
  004e8b44  8b4c2424              mov       ecx,[esp+24h]
  004e8b48  81e7ffd8ffdf          and       edi,0DFFFD8FFh
  004e8b4e  51                    push      ecx
  004e8b4f  56                    push      esi
  004e8b50  89be90000000          mov       [esi+90h],edi
  004e8b56  e89591fbff            call      004A1CF0h

LAB_004e8b5b:
  004e8b5b  8b442424              mov       eax,[esp+24h]
  004e8b5f  83c408                add       esp,8
  004e8b62  85c0                  test      eax,eax
  004e8b64  0f8461020000          je        near 004E8DCBh
  004e8b6a  f70616080000          test      dword [esi],816h
  004e8b70  0f8555020000          jne       near 004E8DCBh
  004e8b76  8a4643                mov       al,[esi+43h]
  004e8b79  84c0                  test      al,al
  004e8b7b  0f854a020000          jne       near 004E8DCBh
  004e8b81  8b466c                mov       eax,[esi+6Ch]
  004e8b84  8b5e1c                mov       ebx,[esi+1Ch]
  004e8b87  8b4e24                mov       ecx,[esi+24h]
  004e8b8a  8b781c                mov       edi,[eax+1Ch]
  004e8b8d  8b6824                mov       ebp,[eax+24h]
  004e8b90  2bfb                  sub       edi,ebx
  004e8b92  2be9                  sub       ebp,ecx
  004e8b94  c1ff0c                sar       edi,0Ch
  004e8b97  c1fd0c                sar       ebp,0Ch
  004e8b9a  8bd7                  mov       edx,edi
  004e8b9c  8bc5                  mov       eax,ebp
  004e8b9e  0fafd7                imul      edx,edi
  004e8ba1  0fafc5                imul      eax,ebp
  004e8ba4  03d0                  add       edx,eax
  004e8ba6  52                    push      edx
  004e8ba7  e864ce0500            call      00545A10h
  004e8bac  55                    push      ebp
  004e8bad  57                    push      edi
  004e8bae  89442420              mov       [esp+20h],eax
  004e8bb2  e86bce0500            call      00545A22h
  004e8bb7  668b4e32              mov       cx,[esi+32h]
  004e8bbb  2bc1                  sub       eax,ecx
  004e8bbd  25ff0f0000            and       eax,0FFFh
  004e8bc2  89442410              mov       [esp+10h],eax
  004e8bc6  8b442418              mov       eax,[esp+18h]
  004e8bca  3dfa000000            cmp       eax,0FAh
  004e8bcf  0f8da3000000          jge       near 004E8C78h
  004e8bd5  e866e50500            call      00547140h
  004e8bda  99                    cdq
  004e8bdb  b964000000            mov       ecx,64h
  004e8be0  f7f9                  idiv      ecx
  004e8be2  83fa3c                cmp       edx,3Ch
  004e8be5  7d60                  jge       short 004E8C47h
  004e8be7  8b542410              mov       edx,[esp+10h]
  004e8beb  8b7c2414              mov       edi,[esp+14h]
  004e8bef  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004e8bf6  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004e8bfd  25ff0f0000            and       eax,0FFFh
  004e8c02  3bc1                  cmp       eax,ecx
  004e8c04  0f8f89010000          jg        near 004E8D93h
  004e8c0a  e831e50500            call      00547140h
  004e8c0f  99                    cdq
  004e8c10  8d4c3f01              lea       ecx,[edi+edi+1]
  004e8c14  f7f9                  idiv      ecx
  004e8c16  8b4c2418              mov       ecx,[esp+18h]
  004e8c1a  b82c010000            mov       eax,12Ch
  004e8c1f  2bc2                  sub       eax,edx
  004e8c21  3bc8                  cmp       ecx,eax
  004e8c23  0f8d6a010000          jge       near 004E8D93h
  004e8c29  8b1e                  mov       ebx,[esi]
  004e8c2b  8bcf                  mov       ecx,edi
  004e8c2d  c1f903                sar       ecx,3
  004e8c30  83cb02                or        ebx,2
  004e8c33  80c110                add       cl,10h
  004e8c36  891e                  mov       [esi],ebx
  004e8c38  c6464200              mov       byte [esi+42h],0
  004e8c3c  884e43                mov       [esi+43h],cl
  004e8c3f  5f                    pop       edi
  004e8c40  5e                    pop       esi
  004e8c41  5d                    pop       ebp
  004e8c42  5b                    pop       ebx
  004e8c43  83c42c                add       esp,2Ch
  004e8c46  c3                    ret

LAB_004e8c47:
  004e8c47  c6464203              mov       byte [esi+42h],3
  004e8c4b  e8f0e40500            call      00547140h
  004e8c50  99                    cdq
  004e8c51  b90a000000            mov       ecx,0Ah
  004e8c56  f7f9                  idiv      ecx
  004e8c58  8b442414              mov       eax,[esp+14h]
  004e8c5c  c1f802                sar       eax,2
  004e8c5f  02d0                  add       dl,al
  004e8c61  02d1                  add       dl,cl
  004e8c63  8b4c2440              mov       ecx,[esp+40h]
  004e8c67  885643                mov       [esi+43h],dl
  004e8c6a  8db980000000          lea       edi,[ecx+80h]
  004e8c70  c1e704                shl       edi,4
  004e8c73  e98b000000            jmp       004E8D03h

LAB_004e8c78:
  004e8c78  3df4010000            cmp       eax,1F4h
  004e8c7d  0f8dd2000000          jge       near 004E8D55h
  004e8c83  e8b8e40500            call      00547140h
  004e8c88  99                    cdq
  004e8c89  b964000000            mov       ecx,64h
  004e8c8e  f7f9                  idiv      ecx
  004e8c90  83fa3c                cmp       edx,3Ch
  004e8c93  0f8d8c000000          jge       near 004E8D25h
  004e8c99  8b542410              mov       edx,[esp+10h]
  004e8c9d  8b7c2414              mov       edi,[esp+14h]
  004e8ca1  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004e8ca8  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004e8caf  25ff0f0000            and       eax,0FFFh
  004e8cb4  3bc1                  cmp       eax,ecx
  004e8cb6  7f26                  jg        short 004E8CDEh
  004e8cb8  8b16                  mov       edx,[esi]
  004e8cba  c7461804000000        mov       dword [esi+18h],4
  004e8cc1  81e2fffffffe          and       edx,0FEFFFFFFh
  004e8cc7  c6464201              mov       byte [esi+42h],1
  004e8ccb  83ca02                or        edx,2
  004e8cce  66c746100100          mov       word [esi+10h],1
  004e8cd4  8916                  mov       [esi],edx
  004e8cd6  5f                    pop       edi
  004e8cd7  5e                    pop       esi
  004e8cd8  5d                    pop       ebp
  004e8cd9  5b                    pop       ebx
  004e8cda  83c42c                add       esp,2Ch
  004e8cdd  c3                    ret

LAB_004e8cde:
  004e8cde  c6464203              mov       byte [esi+42h],3
  004e8ce2  e859e40500            call      00547140h
  004e8ce7  99                    cdq
  004e8ce8  b90a000000            mov       ecx,0Ah
  004e8ced  f7f9                  idiv      ecx
  004e8cef  8bc7                  mov       eax,edi
  004e8cf1  8b7c2440              mov       edi,[esp+40h]
  004e8cf5  c1f802                sar       eax,2
  004e8cf8  c1e704                shl       edi,4
  004e8cfb  02d0                  add       dl,al
  004e8cfd  02d1                  add       dl,cl
  004e8cff  47                    inc       edi
  004e8d00  885643                mov       [esi+43h],dl

LAB_004e8d03:
  004e8d03  e838e40500            call      00547140h
  004e8d08  99                    cdq
  004e8d09  f7ff                  idiv      edi
  004e8d0b  8b4c2410              mov       ecx,[esp+10h]
  004e8d0f  d1ff                  sar       edi,1
  004e8d11  2bd7                  sub       edx,edi
  004e8d13  03d1                  add       edx,ecx
  004e8d15  66035632              add       dx,[esi+32h]
  004e8d19  66895610              mov       [esi+10h],dx
  004e8d1d  5f                    pop       edi
  004e8d1e  5e                    pop       esi
  004e8d1f  5d                    pop       ebp
  004e8d20  5b                    pop       ebx
  004e8d21  83c42c                add       esp,2Ch
  004e8d24  c3                    ret

LAB_004e8d25:
  004e8d25  83fa50                cmp       edx,50h
  004e8d28  0f8d9d000000          jge       near 004E8DCBh
  004e8d2e  c6464202              mov       byte [esi+42h],2
  004e8d32  e809e40500            call      00547140h
  004e8d37  99                    cdq
  004e8d38  b90a000000            mov       ecx,0Ah
  004e8d3d  f7f9                  idiv      ecx
  004e8d3f  8b442414              mov       eax,[esp+14h]
  004e8d43  c1f802                sar       eax,2
  004e8d46  02d0                  add       dl,al
  004e8d48  02d1                  add       dl,cl
  004e8d4a  885643                mov       [esi+43h],dl
  004e8d4d  5f                    pop       edi
  004e8d4e  5e                    pop       esi
  004e8d4f  5d                    pop       ebp
  004e8d50  5b                    pop       ebx
  004e8d51  83c42c                add       esp,2Ch
  004e8d54  c3                    ret

LAB_004e8d55:
  004e8d55  e8e6e30500            call      00547140h
  004e8d5a  99                    cdq
  004e8d5b  b964000000            mov       ecx,64h
  004e8d60  f7f9                  idiv      ecx
  004e8d62  83fa1e                cmp       edx,1Eh
  004e8d65  7d27                  jge       short 004E8D8Eh
  004e8d67  c6464200              mov       byte [esi+42h],0
  004e8d6b  e8d0e30500            call      00547140h
  004e8d70  99                    cdq
  004e8d71  b90a000000            mov       ecx,0Ah
  004e8d76  f7f9                  idiv      ecx
  004e8d78  8b442414              mov       eax,[esp+14h]
  004e8d7c  c1f802                sar       eax,2
  004e8d7f  02d0                  add       dl,al
  004e8d81  02d1                  add       dl,cl
  004e8d83  885643                mov       [esi+43h],dl
  004e8d86  5f                    pop       edi
  004e8d87  5e                    pop       esi
  004e8d88  5d                    pop       ebp
  004e8d89  5b                    pop       ebx
  004e8d8a  83c42c                add       esp,2Ch
  004e8d8d  c3                    ret

LAB_004e8d8e:
  004e8d8e  83fa3c                cmp       edx,3Ch
  004e8d91  7d10                  jge       short 004E8DA3h

LAB_004e8d93:
  004e8d93  c6464201              mov       byte [esi+42h],1
  004e8d97  c6464301              mov       byte [esi+43h],1
  004e8d9b  5f                    pop       edi
  004e8d9c  5e                    pop       esi
  004e8d9d  5d                    pop       ebp
  004e8d9e  5b                    pop       ebx
  004e8d9f  83c42c                add       esp,2Ch
  004e8da2  c3                    ret

LAB_004e8da3:
  004e8da3  c6464203              mov       byte [esi+42h],3
  004e8da7  e894e30500            call      00547140h
  004e8dac  99                    cdq
  004e8dad  b90a000000            mov       ecx,0Ah
  004e8db2  f7f9                  idiv      ecx
  004e8db4  8b442414              mov       eax,[esp+14h]
  004e8db8  c1f802                sar       eax,2
  004e8dbb  02d0                  add       dl,al
  004e8dbd  02d1                  add       dl,cl
  004e8dbf  885643                mov       [esi+43h],dl
  004e8dc2  e879e30500            call      00547140h
  004e8dc7  66894610              mov       [esi+10h],ax

LAB_004e8dcb:
  004e8dcb  5f                    pop       edi
  004e8dcc  5e                    pop       esi
  004e8dcd  5d                    pop       ebp
  004e8dce  5b                    pop       ebx
  004e8dcf  83c42c                add       esp,2Ch
  004e8dd2  c3                    ret
  004e8dd3  90                    nop
  004e8dd4  ce                    into
  004e8dd5  884e00                mov       [esi],cl
  004e8dd8  1489                  adc       al,89h
  004e8dda  4e                    dec       esi
  004e8ddb  0087894e0010          add       [edi+10004E89h],al
  004e8de1  8a4e00                mov       cl,[esi]
  004e8de4  90                    nop
  004e8de5  90                    nop
  004e8de6  90                    nop
  004e8de7  90                    nop
  004e8de8  90                    nop
  004e8de9  90                    nop
  004e8dea  90                    nop
  004e8deb  90                    nop
  004e8dec  90                    nop
  004e8ded  90                    nop
  004e8dee  90                    nop
  004e8def  90                    nop

52 cible(s) de saut interne : 0x004e8571, 0x004e85fc, 0x004e8600, 0x004e8622, 0x004e8639, 0x004e8674, 0x004e86a7, 0x004e86b7, 0x004e86bb, 0x004e86db, 0x004e8788, 0x004e87d7, 0x004e87e7, 0x004e880a, 0x004e881a, 0x004e881e, 0x004e886c, 0x004e88a7, 0x004e88e3, 0x004e88f6, 0x004e8902, 0x004e8929, 0x004e8961, 0x004e8971, 0x004e899c, 0x004e89af, 0x004e89bb, 0x004e89e1, 0x004e89f1, 0x004e89f5, 0x004e8a22, 0x004e8a2e, 0x004e8a94, 0x004e8aa2, 0x004e8aa4, 0x004e8aa8, 0x004e8acd, 0x004e8af0, 0x004e8b0a, 0x004e8b2a, 0x004e8b3e, 0x004e8b5b, 0x004e8c47, 0x004e8c78, 0x004e8cde, 0x004e8d03, 0x004e8d25, 0x004e8d55, 0x004e8d8e, 0x004e8d93, 0x004e8da3, 0x004e8dcb
