Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004b7540  section .text  offset fichier 0xb6940  2272 octets

  004b7540  83ec28                sub       esp,28h
  004b7543  8d442408              lea       eax,[esp+8]
  004b7547  8d4c2420              lea       ecx,[esp+20h]
  004b754b  8d54241c              lea       edx,[esp+1Ch]
  004b754f  53                    push      ebx
  004b7550  55                    push      ebp
  004b7551  56                    push      esi
  004b7552  8b742438              mov       esi,[esp+38h]
  004b7556  57                    push      edi
  004b7557  50                    push      eax
  004b7558  51                    push      ecx
  004b7559  52                    push      edx
  004b755a  56                    push      esi
  004b755b  e820140800            call      00538980h
  004b7560  83c410                add       esp,10h
  004b7563  8d44241c              lea       eax,[esp+1Ch]
  004b7567  8d4c2414              lea       ecx,[esp+14h]
  004b756b  8d54243c              lea       edx,[esp+3Ch]
  004b756f  50                    push      eax
  004b7570  51                    push      ecx
  004b7571  8d442418              lea       eax,[esp+18h]
  004b7575  52                    push      edx
  004b7576  8d4c2440              lea       ecx,[esp+40h]
  004b757a  50                    push      eax
  004b757b  51                    push      ecx
  004b757c  56                    push      esi
  004b757d  e89ee90700            call      00535F20h
  004b7582  8b44242c              mov       eax,[esp+2Ch]
  004b7586  33db                  xor       ebx,ebx
  004b7588  83c418                add       esp,18h
  004b758b  3bc3                  cmp       eax,ebx
  004b758d  0f84f6020000          je        near 004B7889h
  004b7593  56                    push      esi
  004b7594  e807400200            call      004DB5A0h
  004b7599  8b06                  mov       eax,[esi]
  004b759b  83c404                add       esp,4
  004b759e  a802                  test      al,2
  004b75a0  0f8449060000          je        near 004B7BEFh
  004b75a6  f644241c10            test      byte [esp+1Ch],10h
  004b75ab  740c                  je        short 004B75B9h
  004b75ad  24fd                  and       al,0FDh
  004b75af  885e43                mov       [esi+43h],bl
  004b75b2  8906                  mov       [esi],eax
  004b75b4  e9d0020000            jmp       004B7889h

LAB_004b75b9:
  004b75b9  33c9                  xor       ecx,ecx
  004b75bb  8a4e42                mov       cl,[esi+42h]
  004b75be  2bcb                  sub       ecx,ebx
  004b75c0  0f843d050000          je        near 004B7B03h
  004b75c6  49                    dec       ecx
  004b75c7  0f848a000000          je        near 004B7657h
  004b75cd  49                    dec       ecx
  004b75ce  0f85b5020000          jne       near 004B7889h
  004b75d4  0fbf4e10              movsx     ecx,word [esi+10h]
  004b75d8  2bcb                  sub       ecx,ebx
  004b75da  7465                  je        short 004B7641h
  004b75dc  49                    dec       ecx
  004b75dd  7570                  jne       short 004B764Fh
  004b75df  a900000002            test      eax,2000000h
  004b75e4  7469                  je        short 004B764Fh
  004b75e6  53                    push      ebx
  004b75e7  56                    push      esi
  004b75e8  e883e10700            call      00535770h
  004b75ed  0fbf10                movsx     edx,word [eax]
  004b75f0  c1e20c                shl       edx,0Ch
  004b75f3  895644                mov       [esi+44h],edx
  004b75f6  83c408                add       esp,8
  004b75f9  0fbf4802              movsx     ecx,word [eax+2]
  004b75fd  c1e10c                shl       ecx,0Ch
  004b7600  894e48                mov       [esi+48h],ecx
  004b7603  6a02                  push      2
  004b7605  0fbf5004              movsx     edx,word [eax+4]
  004b7609  c1e20c                shl       edx,0Ch
  004b760c  89564c                mov       [esi+4Ch],edx
  004b760f  a1cc975e00            mov       eax,[5E97CCh]
  004b7614  53                    push      ebx
  004b7615  8b88cc000000          mov       ecx,[eax+0CCh]
  004b761b  33c0                  xor       eax,eax
  004b761d  8b11                  mov       edx,[ecx]
  004b761f  8a420c                mov       al,[edx+0Ch]
  004b7622  05c0000000            add       eax,0C0h
  004b7627  50                    push      eax
  004b7628  56                    push      esi
  004b7629  e8a233fcff            call      0047A9D0h
  004b762e  83c410                add       esp,10h
  004b7631  66ff4e10              dec       word [esi+10h]
  004b7635  c646430a              mov       byte [esi+43h],0Ah
  004b7639  895e5c                mov       [esi+5Ch],ebx
  004b763c  e948020000            jmp       004B7889h

LAB_004b7641:
  004b7641  8a4e43                mov       cl,[esi+43h]
  004b7644  fec9                  dec       cl
  004b7646  884e43                mov       [esi+43h],cl
  004b7649  7504                  jne       short 004B764Fh
  004b764b  24fd                  and       al,0FDh
  004b764d  8906                  mov       [esi],eax

LAB_004b764f:
  004b764f  895e5c                mov       [esi+5Ch],ebx
  004b7652  e932020000            jmp       004B7889h

LAB_004b7657:
  004b7657  0fbf4610              movsx     eax,word [esi+10h]
  004b765b  2bc3                  sub       eax,ebx
  004b765d  0f8497000000          je        near 004B76FAh
  004b7663  48                    dec       eax
  004b7664  0f851f020000          jne       near 004B7889h
  004b766a  66837e1a08            cmp       word [esi+1Ah],8
  004b766f  895e5c                mov       [esi+5Ch],ebx
  004b7672  7544                  jne       short 004B76B8h
  004b7674  0fbf4e32              movsx     ecx,word [esi+32h]
  004b7678  51                    push      ecx
  004b7679  c7465c00800300        mov       dword [esi+5Ch],38000h
  004b7680  e897e30800            call      00545A1Ch
  004b7685  0faf465c              imul      eax,[esi+5Ch]
  004b7689  0fbf5632              movsx     edx,word [esi+32h]
  004b768d  8b6e50                mov       ebp,[esi+50h]
  004b7690  52                    push      edx
  004b7691  c1f80c                sar       eax,0Ch
  004b7694  03e8                  add       ebp,eax
  004b7696  896e50                mov       [esi+50h],ebp
  004b7699  e878e30800            call      00545A16h
  004b769e  0faf465c              imul      eax,[esi+5Ch]
  004b76a2  8b4e58                mov       ecx,[esi+58h]
  004b76a5  c7465c00300000        mov       dword [esi+5Ch],3000h
  004b76ac  c1f80c                sar       eax,0Ch
  004b76af  03c8                  add       ecx,eax
  004b76b1  66ff4e10              dec       word [esi+10h]
  004b76b5  894e58                mov       [esi+58h],ecx

LAB_004b76b8:
  004b76b8  8b44243c              mov       eax,[esp+3Ch]
  004b76bc  8d4840                lea       ecx,[eax+40h]
  004b76bf  81e1ff0f0000          and       ecx,0FFFh
  004b76c5  81f980000000          cmp       ecx,80h
  004b76cb  7f09                  jg        short 004B76D6h
  004b76cd  66014632              add       [esi+32h],ax
  004b76d1  e9b3010000            jmp       004B7889h

LAB_004b76d6:
  004b76d6  25ff0f0000            and       eax,0FFFh
  004b76db  33d2                  xor       edx,edx
  004b76dd  3d00080000            cmp       eax,800h
  004b76e2  0f9dc2                setge     dl
  004b76e5  4a                    dec       edx
  004b76e6  81e280000000          and       edx,80h
  004b76ec  83c2c0                add       edx,0FFFFFFC0h
  004b76ef  8bc2                  mov       eax,edx
  004b76f1  66014632              add       [esi+32h],ax
  004b76f5  e98f010000            jmp       004B7889h

LAB_004b76fa:
  004b76fa  8b4650                mov       eax,[esi+50h]
  004b76fd  8b4e58                mov       ecx,[esi+58h]
  004b7700  c1f80c                sar       eax,0Ch
  004b7703  8bd0                  mov       edx,eax
  004b7705  c1f90c                sar       ecx,0Ch
  004b7708  0fafd0                imul      edx,eax
  004b770b  8bc1                  mov       eax,ecx
  004b770d  0fafc1                imul      eax,ecx
  004b7710  03d0                  add       edx,eax
  004b7712  52                    push      edx
  004b7713  e8f8e20800            call      00545A10h
  004b7718  8b0e                  mov       ecx,[esi]
  004b771a  f7c100000002          test      ecx,2000000h
  004b7720  740a                  je        short 004B772Ch
  004b7722  83f805                cmp       eax,5
  004b7725  7f05                  jg        short 004B772Ch
  004b7727  83e1dd                and       ecx,0FFFFFFDDh
  004b772a  890e                  mov       [esi],ecx

LAB_004b772c:
  004b772c  83f803                cmp       eax,3
  004b772f  0f8c54010000          jl        near 004B7889h
  004b7735  f60620                test      byte [esi],20h
  004b7738  0f854b010000          jne       near 004B7889h
  004b773e  8b44243c              mov       eax,[esp+3Ch]
  004b7742  8d4840                lea       ecx,[eax+40h]
  004b7745  81e1ff0f0000          and       ecx,0FFFh
  004b774b  81f980000000          cmp       ecx,80h
  004b7751  7e1b                  jle       short 004B776Eh
  004b7753  25ff0f0000            and       eax,0FFFh
  004b7758  33d2                  xor       edx,edx
  004b775a  3d00080000            cmp       eax,800h
  004b775f  0f9dc2                setge     dl
  004b7762  4a                    dec       edx
  004b7763  81e280000000          and       edx,80h
  004b7769  83c2c0                add       edx,0FFFFFFC0h
  004b776c  8bc2                  mov       eax,edx

LAB_004b776e:
  004b776e  66014632              add       [esi+32h],ax
  004b7772  8d6e1c                lea       ebp,[esi+1Ch]
  004b7775  53                    push      ebx
  004b7776  682c2b5500            push      552B2Ch
  004b777b  55                    push      ebp
  004b777c  56                    push      esi
  004b777d  e8ae240600            call      00519C30h
  004b7782  8bf8                  mov       edi,eax
  004b7784  83c410                add       esp,10h
  004b7787  3bfb                  cmp       edi,ebx
  004b7789  7406                  je        short 004B7791h
  004b778b  8b06                  mov       eax,[esi]
  004b778d  0c20                  or        al,20h
  004b778f  8906                  mov       [esi],eax

LAB_004b7791:
  004b7791  391db8e05d00          cmp       [5DE0B8h],ebx
  004b7797  0f84ec000000          je        near 004B7889h
  004b779d  8b1f                  mov       ebx,[edi]
  004b779f  66c787960000001000    mov       word [edi+96h],10h
  004b77a8  83cb40                or        ebx,40h
  004b77ab  57                    push      edi
  004b77ac  891f                  mov       [edi],ebx
  004b77ae  8b4c241c              mov       ecx,[esp+1Ch]
  004b77b2  56                    push      esi
  004b77b3  668b4130              mov       ax,[ecx+30h]
  004b77b7  0fbfd8                movsx     ebx,ax
  004b77ba  66d1f8                sar       ax,1
  004b77bd  66894130              mov       [ecx+30h],ax
  004b77c1  e8ba230800            call      00539B80h
  004b77c6  8b442420              mov       eax,[esp+20h]
  004b77ca  83c408                add       esp,8
  004b77cd  66895830              mov       [eax+30h],bx
  004b77d1  8d5f1c                lea       ebx,[edi+1Ch]
  004b77d4  53                    push      ebx
  004b77d5  6800180000            push      1800h
  004b77da  6800030000            push      300h
  004b77df  6a03                  push      3
  004b77e1  e83a630800            call      0053DB20h
  004b77e6  8b07                  mov       eax,[edi]
  004b77e8  83c410                add       esp,10h
  004b77eb  a900000040            test      eax,40000000h
  004b77f0  0f8591000000          jne       near 004B7887h
  004b77f6  0fbf4e32              movsx     ecx,word [esi+32h]
  004b77fa  8b54243c              mov       edx,[esp+3Ch]
  004b77fe  03d1                  add       edx,ecx
  004b7800  52                    push      edx
  004b7801  e816e20800            call      00545A1Ch
  004b7806  8d0440                lea       eax,[eax+eax*2]
  004b7809  8d0480                lea       eax,[eax+eax*4]
  004b780c  d1e0                  shl       eax,1
  004b780e  894750                mov       [edi+50h],eax
  004b7811  8b54243c              mov       edx,[esp+3Ch]
  004b7815  0fbf4e32              movsx     ecx,word [esi+32h]
  004b7819  03d1                  add       edx,ecx
  004b781b  52                    push      edx
  004b781c  e8f5e10800            call      00545A16h
  004b7821  8d0440                lea       eax,[eax+eax*2]
  004b7824  8d0480                lea       eax,[eax+eax*4]
  004b7827  d1e0                  shl       eax,1
  004b7829  894758                mov       [edi+58h],eax
  004b782c  8b07                  mov       eax,[edi]
  004b782e  a801                  test      al,1
  004b7830  750b                  jne       short 004B783Dh
  004b7832  0c01                  or        al,1
  004b7834  c7475400800100        mov       dword [edi+54h],18000h
  004b783b  8907                  mov       [edi],eax

LAB_004b783d:
  004b783d  8b4d00                mov       ecx,[ebp]
  004b7840  8b5620                mov       edx,[esi+20h]
  004b7843  8b4624                mov       eax,[esi+24h]
  004b7846  894c2420              mov       [esp+20h],ecx
  004b784a  8b0b                  mov       ecx,[ebx]
  004b784c  89542424              mov       [esp+24h],edx
  004b7850  894d00                mov       [ebp],ecx
  004b7853  8b5720                mov       edx,[edi+20h]
  004b7856  6aff                  push      0FFFFFFFFh
  004b7858  8944242c              mov       [esp+2Ch],eax
  004b785c  895620                mov       [esi+20h],edx
  004b785f  8b4724                mov       eax,[edi+24h]
  004b7862  6a00                  push      0
  004b7864  6a39                  push      39h
  004b7866  56                    push      esi
  004b7867  894624                mov       [esi+24h],eax
  004b786a  e86131fcff            call      0047A9D0h
  004b786f  8b4c2430              mov       ecx,[esp+30h]
  004b7873  8b542434              mov       edx,[esp+34h]
  004b7877  8b442438              mov       eax,[esp+38h]
  004b787b  83c410                add       esp,10h
  004b787e  894d00                mov       [ebp],ecx
  004b7881  895620                mov       [esi+20h],edx
  004b7884  894624                mov       [esi+24h],eax

LAB_004b7887:
  004b7887  33db                  xor       ebx,ebx

LAB_004b7889:
  004b7889  8b4c2414              mov       ecx,[esp+14h]
  004b788d  51                    push      ecx
  004b788e  56                    push      esi
  004b788f  e84c30feff            call      0049A8E0h
  004b7894  83c408                add       esp,8
  004b7897  56                    push      esi
  004b7898  e8a3c00700            call      00533940h
  004b789d  8b9690000000          mov       edx,[esi+90h]
  004b78a3  8b466c                mov       eax,[esi+6Ch]
  004b78a6  81e2ffd8ffdf          and       edx,0DFFFD8FFh
  004b78ac  83c404                add       esp,4
  004b78af  899690000000          mov       [esi+90h],edx
  004b78b5  8a08                  mov       cl,[eax]
  004b78b7  f6c101                test      cl,1
  004b78ba  7506                  jne       short 004B78C2h
  004b78bc  8b5060                mov       edx,[eax+60h]
  004b78bf  895648                mov       [esi+48h],edx

LAB_004b78c2:
  004b78c2  e879f80800            call      00547140h
  004b78c7  8b7e60                mov       edi,[esi+60h]
  004b78ca  25ff000000            and       eax,0FFh
  004b78cf  0500010000            add       eax,100h
  004b78d4  66014614              add       [esi+14h],ax
  004b78d8  8b4648                mov       eax,[esi+48h]
  004b78db  668b4e14              mov       cx,[esi+14h]
  004b78df  3bc7                  cmp       eax,edi
  004b78e1  7c02                  jl        short 004B78E5h
  004b78e3  8bf8                  mov       edi,eax

LAB_004b78e5:
  004b78e5  f60602                test      byte [esi],2
  004b78e8  7415                  je        short 004B78FFh
  004b78ea  8b4620                mov       eax,[esi+20h]
  004b78ed  2bf8                  sub       edi,eax
  004b78ef  81c700400600          add       edi,64000h
  004b78f5  c1ff03                sar       edi,3
  004b78f8  03f8                  add       edi,eax
  004b78fa  897e20                mov       [esi+20h],edi
  004b78fd  eb20                  jmp       short 004B791Fh

LAB_004b78ff:
  004b78ff  8b6e20                mov       ebp,[esi+20h]
  004b7902  0fbfc1                movsx     eax,cx
  004b7905  50                    push      eax
  004b7906  e811e10800            call      00545A1Ch
  004b790b  05004b0000            add       eax,4B00h
  004b7910  c1e006                shl       eax,6
  004b7913  03c7                  add       eax,edi
  004b7915  2bc5                  sub       eax,ebp
  004b7917  c1f804                sar       eax,4
  004b791a  03c5                  add       eax,ebp
  004b791c  894620                mov       [esi+20h],eax

LAB_004b791f:
  004b791f  8b4c242c              mov       ecx,[esp+2Ch]
  004b7923  51                    push      ecx
  004b7924  56                    push      esi
  004b7925  e8c6a3feff            call      004A1CF0h
  004b792a  8b44241c              mov       eax,[esp+1Ch]
  004b792e  83c408                add       esp,8
  004b7931  3bc3                  cmp       eax,ebx
  004b7933  0f84c2010000          je        near 004B7AFBh
  004b7939  f70616080000          test      dword [esi],816h
  004b793f  0f85b6010000          jne       near 004B7AFBh
  004b7945  8a4643                mov       al,[esi+43h]
  004b7948  84c0                  test      al,al
  004b794a  0f85ab010000          jne       near 004B7AFBh
  004b7950  8b466c                mov       eax,[esi+6Ch]
  004b7953  8b5e1c                mov       ebx,[esi+1Ch]
  004b7956  8b4e24                mov       ecx,[esi+24h]
  004b7959  8b781c                mov       edi,[eax+1Ch]
  004b795c  8b6824                mov       ebp,[eax+24h]
  004b795f  2bfb                  sub       edi,ebx
  004b7961  2be9                  sub       ebp,ecx
  004b7963  c1ff0c                sar       edi,0Ch
  004b7966  c1fd0c                sar       ebp,0Ch
  004b7969  8bd7                  mov       edx,edi
  004b796b  8bc5                  mov       eax,ebp
  004b796d  0fafd7                imul      edx,edi
  004b7970  0fafc5                imul      eax,ebp
  004b7973  03d0                  add       edx,eax
  004b7975  52                    push      edx
  004b7976  e895e00800            call      00545A10h
  004b797b  55                    push      ebp
  004b797c  57                    push      edi
  004b797d  89442418              mov       [esp+18h],eax
  004b7981  e89ce00800            call      00545A22h
  004b7986  668b4e32              mov       cx,[esi+32h]
  004b798a  2bc1                  sub       eax,ecx
  004b798c  25ff0f0000            and       eax,0FFFh
  004b7991  8944243c              mov       [esp+3Ch],eax
  004b7995  e8a6f70800            call      00547140h
  004b799a  99                    cdq
  004b799b  b964000000            mov       ecx,64h
  004b79a0  f7f9                  idiv      ecx
  004b79a2  817c2410fa000000      cmp       dword [esp+10h],0FAh
  004b79aa  0f8d82000000          jge       near 004B7A32h
  004b79b0  83fa3c                cmp       edx,3Ch
  004b79b3  7d3e                  jge       short 004B79F3h
  004b79b5  8b54243c              mov       edx,[esp+3Ch]
  004b79b9  81c200010000          add       edx,100h
  004b79bf  81e2ff0f0000          and       edx,0FFFh
  004b79c5  81fa00020000          cmp       edx,200h
  004b79cb  7f16                  jg        short 004B79E3h
  004b79cd  8b06                  mov       eax,[esi]
  004b79cf  c6464200              mov       byte [esi+42h],0
  004b79d3  0c02                  or        al,2
  004b79d5  c6464310              mov       byte [esi+43h],10h
  004b79d9  8906                  mov       [esi],eax
  004b79db  5f                    pop       edi
  004b79dc  5e                    pop       esi
  004b79dd  5d                    pop       ebp
  004b79de  5b                    pop       ebx
  004b79df  83c428                add       esp,28h
  004b79e2  c3                    ret

LAB_004b79e3:
  004b79e3  c6464201              mov       byte [esi+42h],1
  004b79e7  c6464301              mov       byte [esi+43h],1
  004b79eb  5f                    pop       edi
  004b79ec  5e                    pop       esi
  004b79ed  5d                    pop       ebp
  004b79ee  5b                    pop       ebx
  004b79ef  83c428                add       esp,28h
  004b79f2  c3                    ret

LAB_004b79f3:
  004b79f3  c6464203              mov       byte [esi+42h],3
  004b79f7  e844f70800            call      00547140h
  004b79fc  99                    cdq
  004b79fd  b90a000000            mov       ecx,0Ah
  004b7a02  f7f9                  idiv      ecx
  004b7a04  02d1                  add       dl,cl
  004b7a06  885643                mov       [esi+43h],dl
  004b7a09  e832f70800            call      00547140h
  004b7a0e  99                    cdq
  004b7a0f  b9000c0000            mov       ecx,0C00h
  004b7a14  8b7c243c              mov       edi,[esp+3Ch]
  004b7a18  f7f9                  idiv      ecx
  004b7a1a  03d7                  add       edx,edi
  004b7a1c  66035632              add       dx,[esi+32h]
  004b7a20  81ea00060000          sub       edx,600h
  004b7a26  66895610              mov       [esi+10h],dx
  004b7a2a  5f                    pop       edi
  004b7a2b  5e                    pop       esi
  004b7a2c  5d                    pop       ebp
  004b7a2d  5b                    pop       ebx
  004b7a2e  83c428                add       esp,28h
  004b7a31  c3                    ret

LAB_004b7a32:
  004b7a32  83fa3c                cmp       edx,3Ch
  004b7a35  0f8d84000000          jge       near 004B7ABFh
  004b7a3b  8b44243c              mov       eax,[esp+3Ch]
  004b7a3f  0500010000            add       eax,100h
  004b7a44  25ff0f0000            and       eax,0FFFh
  004b7a49  3d00020000            cmp       eax,200h
  004b7a4e  7f51                  jg        short 004B7AA1h
  004b7a50  f6c201                test      dl,1
  004b7a53  7426                  je        short 004B7A7Bh
  004b7a55  8b0e                  mov       ecx,[esi]
  004b7a57  c7461803000000        mov       dword [esi+18h],3
  004b7a5e  81e1fffffffe          and       ecx,0FEFFFFFFh
  004b7a64  c6464201              mov       byte [esi+42h],1
  004b7a68  83c902                or        ecx,2
  004b7a6b  66c746100100          mov       word [esi+10h],1
  004b7a71  890e                  mov       [esi],ecx
  004b7a73  5f                    pop       edi
  004b7a74  5e                    pop       esi
  004b7a75  5d                    pop       ebp
  004b7a76  5b                    pop       ebx
  004b7a77  83c428                add       esp,28h
  004b7a7a  c3                    ret

LAB_004b7a7b:
  004b7a7b  8b16                  mov       edx,[esi]
  004b7a7d  c7461805000000        mov       dword [esi+18h],5
  004b7a84  81e2fffffffe          and       edx,0FEFFFFFFh
  004b7a8a  c6464202              mov       byte [esi+42h],2
  004b7a8e  83ca02                or        edx,2
  004b7a91  66c746100100          mov       word [esi+10h],1
  004b7a97  8916                  mov       [esi],edx
  004b7a99  5f                    pop       edi
  004b7a9a  5e                    pop       esi
  004b7a9b  5d                    pop       ebp
  004b7a9c  5b                    pop       ebx
  004b7a9d  83c428                add       esp,28h
  004b7aa0  c3                    ret

LAB_004b7aa1:
  004b7aa1  c6464202              mov       byte [esi+42h],2
  004b7aa5  e896f60800            call      00547140h
  004b7aaa  99                    cdq
  004b7aab  b90a000000            mov       ecx,0Ah
  004b7ab0  f7f9                  idiv      ecx
  004b7ab2  02d1                  add       dl,cl
  004b7ab4  885643                mov       [esi+43h],dl
  004b7ab7  5f                    pop       edi
  004b7ab8  5e                    pop       esi
  004b7ab9  5d                    pop       ebp
  004b7aba  5b                    pop       ebx
  004b7abb  83c428                add       esp,28h
  004b7abe  c3                    ret

LAB_004b7abf:
  004b7abf  c6464203              mov       byte [esi+42h],3
  004b7ac3  e878f60800            call      00547140h
  004b7ac8  99                    cdq
  004b7ac9  b90a000000            mov       ecx,0Ah
  004b7ace  f7f9                  idiv      ecx
  004b7ad0  02d1                  add       dl,cl
  004b7ad2  885643                mov       [esi+43h],dl
  004b7ad5  e866f60800            call      00547140h
  004b7ada  99                    cdq
  004b7adb  33c2                  xor       eax,edx
  004b7add  8b7c243c              mov       edi,[esp+3Ch]
  004b7ae1  2bc2                  sub       eax,edx
  004b7ae3  25ff030000            and       eax,3FFh
  004b7ae8  33c2                  xor       eax,edx
  004b7aea  2bc2                  sub       eax,edx
  004b7aec  03c7                  add       eax,edi
  004b7aee  66034632              add       ax,[esi+32h]
  004b7af2  2d00020000            sub       eax,200h
  004b7af7  66894610              mov       [esi+10h],ax

LAB_004b7afb:
  004b7afb  5f                    pop       edi
  004b7afc  5e                    pop       esi
  004b7afd  5d                    pop       ebp
  004b7afe  5b                    pop       ebx
  004b7aff  83c428                add       esp,28h
  004b7b02  c3                    ret

LAB_004b7b03:
  004b7b03  b904000000            mov       ecx,4
  004b7b08  66394e18              cmp       [esi+18h],cx
  004b7b0c  740a                  je        short 004B7B18h
  004b7b0e  25fffffffe            and       eax,0FEFFFFFFh
  004b7b13  894e18                mov       [esi+18h],ecx
  004b7b16  8906                  mov       [esi],eax

LAB_004b7b18:
  004b7b18  8b44243c              mov       eax,[esp+3Ch]
  004b7b1c  8d4840                lea       ecx,[eax+40h]
  004b7b1f  81e1ff0f0000          and       ecx,0FFFh
  004b7b25  81f980000000          cmp       ecx,80h
  004b7b2b  7e1b                  jle       short 004B7B48h
  004b7b2d  25ff0f0000            and       eax,0FFFh
  004b7b32  33d2                  xor       edx,edx
  004b7b34  3d00080000            cmp       eax,800h
  004b7b39  0f9dc2                setge     dl
  004b7b3c  4a                    dec       edx
  004b7b3d  81e280000000          and       edx,80h
  004b7b43  83c2c0                add       edx,0FFFFFFC0h
  004b7b46  8bc2                  mov       eax,edx

LAB_004b7b48:
  004b7b48  66014632              add       [esi+32h],ax
  004b7b4c  53                    push      ebx
  004b7b4d  8d461c                lea       eax,[esi+1Ch]
  004b7b50  68202b5500            push      552B20h
  004b7b55  50                    push      eax
  004b7b56  56                    push      esi
  004b7b57  e8d4200600            call      00519C30h
  004b7b5c  8bf8                  mov       edi,eax
  004b7b5e  a1b8e05d00            mov       eax,[5DE0B8h]
  004b7b63  83c410                add       esp,10h
  004b7b66  3bc3                  cmp       eax,ebx
  004b7b68  7452                  je        short 004B7BBCh
  004b7b6a  8b07                  mov       eax,[edi]
  004b7b6c  57                    push      edi
  004b7b6d  0c40                  or        al,40h
  004b7b6f  56                    push      esi
  004b7b70  66c787960000001000    mov       word [edi+96h],10h
  004b7b79  8907                  mov       [edi],eax
  004b7b7b  e800200800            call      00539B80h
  004b7b80  83c408                add       esp,8
  004b7b83  e8b8f50800            call      00547140h
  004b7b88  99                    cdq
  004b7b89  b964000000            mov       ecx,64h
  004b7b8e  f7f9                  idiv      ecx
  004b7b90  83fa1e                cmp       edx,1Eh
  004b7b93  7d0f                  jge       short 004B7BA4h
  004b7b95  53                    push      ebx
  004b7b96  57                    push      edi
  004b7b97  56                    push      esi
  004b7b98  c6460707              mov       byte [esi+7],7
  004b7b9c  e87f480800            call      0053C420h
  004b7ba1  83c40c                add       esp,0Ch

LAB_004b7ba4:
  004b7ba4  83c71c                add       edi,1Ch
  004b7ba7  57                    push      edi
  004b7ba8  6800180000            push      1800h
  004b7bad  6800030000            push      300h
  004b7bb2  6a03                  push      3
  004b7bb4  e8675f0800            call      0053DB20h
  004b7bb9  83c410                add       esp,10h

LAB_004b7bbc:
  004b7bbc  8b465c                mov       eax,[esi+5Ch]
  004b7bbf  ba00e00100            mov       edx,1E000h
  004b7bc4  2bd0                  sub       edx,eax
  004b7bc6  c1fa02                sar       edx,2
  004b7bc9  03d0                  add       edx,eax
  004b7bcb  8a4643                mov       al,[esi+43h]
  004b7bce  84c0                  test      al,al
  004b7bd0  89565c                mov       [esi+5Ch],edx
  004b7bd3  0f84b0fcffff          je        near 004B7889h
  004b7bd9  fec8                  dec       al
  004b7bdb  884643                mov       [esi+43h],al
  004b7bde  0f85a5fcffff          jne       near 004B7889h
  004b7be4  8b06                  mov       eax,[esi]
  004b7be6  24fd                  and       al,0FDh
  004b7be8  8906                  mov       [esi],eax
  004b7bea  e99afcffff            jmp       004B7889h

LAB_004b7bef:
  004b7bef  a810                  test      al,10h
  004b7bf1  0f8592fcffff          jne       near 004B7889h
  004b7bf7  a904080000            test      eax,804h
  004b7bfc  0f8587fcffff          jne       near 004B7889h
  004b7c02  33c9                  xor       ecx,ecx
  004b7c04  8a4e42                mov       cl,[esi+42h]
  004b7c07  83f903                cmp       ecx,3
  004b7c0a  0f87cc010000          ja        near 004B7DDCh
  004b7c10  ff248d107e4b00        jmp       dword [ecx*4+4B7E10h]
  004b7c17  66395e18              cmp       [esi+18h],bx
  004b7c1b  740a                  je        short 004B7C27h
  004b7c1d  0d00000001            or        eax,1000000h
  004b7c22  895e18                mov       [esi+18h],ebx
  004b7c25  8906                  mov       [esi],eax

LAB_004b7c27:
  004b7c27  817c24102c010000      cmp       dword [esp+10h],12Ch
  004b7c2f  7d09                  jge       short 004B7C3Ah
  004b7c31  c6464300              mov       byte [esi+43h],0
  004b7c35  e9a2010000            jmp       004B7DDCh

LAB_004b7c3a:
  004b7c3a  8a4643                mov       al,[esi+43h]
  004b7c3d  84c0                  test      al,al
  004b7c3f  7405                  je        short 004B7C46h
  004b7c41  fec8                  dec       al
  004b7c43  884643                mov       [esi+43h],al

LAB_004b7c46:
  004b7c46  8b465c                mov       eax,[esi+5Ch]
  004b7c49  8bc8                  mov       ecx,eax
  004b7c4b  c1f904                sar       ecx,4
  004b7c4e  2bc1                  sub       eax,ecx
  004b7c50  89465c                mov       [esi+5Ch],eax
  004b7c53  e984010000            jmp       004B7DDCh
  004b7c58  66395e18              cmp       [esi+18h],bx
  004b7c5c  740a                  je        short 004B7C68h
  004b7c5e  0d00000001            or        eax,1000000h
  004b7c63  895e18                mov       [esi+18h],ebx
  004b7c66  8906                  mov       [esi],eax

LAB_004b7c68:
  004b7c68  8b44243c              mov       eax,[esp+3Ch]
  004b7c6c  8d9080000000          lea       edx,[eax+80h]
  004b7c72  81e2ff0f0000          and       edx,0FFFh
  004b7c78  81fa00010000          cmp       edx,100h
  004b7c7e  7f1a                  jg        short 004B7C9Ah
  004b7c80  66014632              add       [esi+32h],ax
  004b7c84  8b465c                mov       eax,[esi+5Ch]
  004b7c87  8bd0                  mov       edx,eax
  004b7c89  c6464300              mov       byte [esi+43h],0
  004b7c8d  c1fa04                sar       edx,4
  004b7c90  2bc2                  sub       eax,edx
  004b7c92  89465c                mov       [esi+5Ch],eax
  004b7c95  e942010000            jmp       004B7DDCh

LAB_004b7c9a:
  004b7c9a  25ff0f0000            and       eax,0FFFh
  004b7c9f  33c9                  xor       ecx,ecx
  004b7ca1  3d00080000            cmp       eax,800h
  004b7ca6  0f9dc1                setge     cl
  004b7ca9  49                    dec       ecx
  004b7caa  81e100010000          and       ecx,100h
  004b7cb0  83c180                add       ecx,0FFFFFF80h
  004b7cb3  8bc1                  mov       eax,ecx
  004b7cb5  66014632              add       [esi+32h],ax
  004b7cb9  8b465c                mov       eax,[esi+5Ch]
  004b7cbc  8bd0                  mov       edx,eax
  004b7cbe  c1fa04                sar       edx,4
  004b7cc1  2bc2                  sub       eax,edx
  004b7cc3  89465c                mov       [esi+5Ch],eax
  004b7cc6  e911010000            jmp       004B7DDCh
  004b7ccb  b901000000            mov       ecx,1
  004b7cd0  66394e18              cmp       [esi+18h],cx
  004b7cd4  740a                  je        short 004B7CE0h
  004b7cd6  0d00000001            or        eax,1000000h
  004b7cdb  894e18                mov       [esi+18h],ecx
  004b7cde  8906                  mov       [esi],eax

LAB_004b7ce0:
  004b7ce0  817c24102c010000      cmp       dword [esp+10h],12Ch
  004b7ce8  7d09                  jge       short 004B7CF3h
  004b7cea  c6464300              mov       byte [esi+43h],0
  004b7cee  e9e9000000            jmp       004B7DDCh

LAB_004b7cf3:
  004b7cf3  8a4643                mov       al,[esi+43h]
  004b7cf6  84c0                  test      al,al
  004b7cf8  7405                  je        short 004B7CFFh
  004b7cfa  fec8                  dec       al
  004b7cfc  884643                mov       [esi+43h],al

LAB_004b7cff:
  004b7cff  8b44243c              mov       eax,[esp+3Ch]
  004b7d03  8d8880000000          lea       ecx,[eax+80h]
  004b7d09  81e1ff0f0000          and       ecx,0FFFh
  004b7d0f  81f900010000          cmp       ecx,100h
  004b7d15  7e1b                  jle       short 004B7D32h
  004b7d17  25ff0f0000            and       eax,0FFFh
  004b7d1c  33d2                  xor       edx,edx
  004b7d1e  3d00080000            cmp       eax,800h
  004b7d23  0f9dc2                setge     dl
  004b7d26  4a                    dec       edx
  004b7d27  81e200010000          and       edx,100h
  004b7d2d  83c280                add       edx,0FFFFFF80h
  004b7d30  8bc2                  mov       eax,edx

LAB_004b7d32:
  004b7d32  66014632              add       [esi+32h],ax
  004b7d36  8b465c                mov       eax,[esi+5Ch]
  004b7d39  b900f00000            mov       ecx,0F000h
  004b7d3e  2bc8                  sub       ecx,eax
  004b7d40  c1f904                sar       ecx,4
  004b7d43  03c8                  add       ecx,eax
  004b7d45  894e5c                mov       [esi+5Ch],ecx
  004b7d48  e98f000000            jmp       004B7DDCh
  004b7d4d  66395e18              cmp       [esi+18h],bx
  004b7d51  740a                  je        short 004B7D5Dh
  004b7d53  0d00000001            or        eax,1000000h
  004b7d58  895e18                mov       [esi+18h],ebx
  004b7d5b  8906                  mov       [esi],eax

LAB_004b7d5d:
  004b7d5d  8a4643                mov       al,[esi+43h]
  004b7d60  84c0                  test      al,al
  004b7d62  7405                  je        short 004B7D69h
  004b7d64  fec8                  dec       al
  004b7d66  884643                mov       [esi+43h],al

LAB_004b7d69:
  004b7d69  0fbf5610              movsx     edx,word [esi+10h]
  004b7d6d  52                    push      edx
  004b7d6e  c7465c00500000        mov       dword [esi+5Ch],5000h
  004b7d75  e8a2dc0800            call      00545A1Ch
  004b7d7a  0faf465c              imul      eax,[esi+5Ch]
  004b7d7e  8b7e50                mov       edi,[esi+50h]
  004b7d81  c1f80c                sar       eax,0Ch
  004b7d84  03f8                  add       edi,eax
  004b7d86  0fbf4610              movsx     eax,word [esi+10h]
  004b7d8a  50                    push      eax
  004b7d8b  897e50                mov       [esi+50h],edi
  004b7d8e  e883dc0800            call      00545A16h
  004b7d93  0faf465c              imul      eax,[esi+5Ch]
  004b7d97  8b6e58                mov       ebp,[esi+58h]
  004b7d9a  895e5c                mov       [esi+5Ch],ebx
  004b7d9d  c1f80c                sar       eax,0Ch
  004b7da0  03e8                  add       ebp,eax
  004b7da2  8b44243c              mov       eax,[esp+3Ch]
  004b7da6  896e58                mov       [esi+58h],ebp
  004b7da9  8d8880000000          lea       ecx,[eax+80h]
  004b7daf  81e1ff0f0000          and       ecx,0FFFh
  004b7db5  81f900010000          cmp       ecx,100h
  004b7dbb  7e1b                  jle       short 004B7DD8h
  004b7dbd  25ff0f0000            and       eax,0FFFh
  004b7dc2  33d2                  xor       edx,edx
  004b7dc4  3d00080000            cmp       eax,800h
  004b7dc9  0f9dc2                setge     dl
  004b7dcc  4a                    dec       edx
  004b7dcd  81e200010000          and       edx,100h
  004b7dd3  83c280                add       edx,0FFFFFF80h
  004b7dd6  8bc2                  mov       eax,edx

LAB_004b7dd8:
  004b7dd8  66014632              add       [esi+32h],ax

LAB_004b7ddc:
  004b7ddc  66395e18              cmp       [esi+18h],bx
  004b7de0  0f85a3faffff          jne       near 004B7889h
  004b7de6  66395e1a              cmp       [esi+1Ah],bx
  004b7dea  0f8599faffff          jne       near 004B7889h
  004b7df0  8d461c                lea       eax,[esi+1Ch]
  004b7df3  50                    push      eax
  004b7df4  6800180000            push      1800h
  004b7df9  6800040000            push      400h
  004b7dfe  6a47                  push      47h
  004b7e00  e81b5d0800            call      0053DB20h
  004b7e05  83c410                add       esp,10h
  004b7e08  e97cfaffff            jmp       004B7889h
  004b7e0d  90                    nop
  004b7e0e  8bff                  mov       edi,edi
  004b7e10  17                    pop       ss
  004b7e11  7c4b                  jl        short 004B7E5Eh
  004b7e13  00587c                add       [eax+7Ch],bl
  004b7e16  4b                    dec       ebx
  004b7e17  00cb                  add       bl,cl
  004b7e19  7c4b                  jl        short 004B7E66h
  004b7e1b  004d7d                add       [ebp+7Dh],cl
  004b7e1e  4b                    dec       ebx
  004b7e1f  00                    (bad)
  (octet invalide - la plage demandee deborde probablement du code)
