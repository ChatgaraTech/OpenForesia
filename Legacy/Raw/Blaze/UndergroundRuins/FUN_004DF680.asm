Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004df680  section .text  offset fichier 0xdea80  1136 octets

  004df680  83ec1c                sub       esp,1Ch
  004df683  8d442410              lea       eax,[esp+10h]
  004df687  8d4c2414              lea       ecx,[esp+14h]
  004df68b  8d54240c              lea       edx,[esp+0Ch]
  004df68f  53                    push      ebx
  004df690  55                    push      ebp
  004df691  56                    push      esi
  004df692  8b74242c              mov       esi,[esp+2Ch]
  004df696  57                    push      edi
  004df697  50                    push      eax
  004df698  51                    push      ecx
  004df699  52                    push      edx
  004df69a  56                    push      esi
  004df69b  e8e0920500            call      00538980h
  004df6a0  83c410                add       esp,10h
  004df6a3  8d442418              lea       eax,[esp+18h]
  004df6a7  8d4c2414              lea       ecx,[esp+14h]
  004df6ab  8d542410              lea       edx,[esp+10h]
  004df6af  50                    push      eax
  004df6b0  51                    push      ecx
  004df6b1  8d442438              lea       eax,[esp+38h]
  004df6b5  52                    push      edx
  004df6b6  8d4c2434              lea       ecx,[esp+34h]
  004df6ba  50                    push      eax
  004df6bb  51                    push      ecx
  004df6bc  56                    push      esi
  004df6bd  e85e680500            call      00535F20h
  004df6c2  8b44242c              mov       eax,[esp+2Ch]
  004df6c6  33ed                  xor       ebp,ebp
  004df6c8  83c418                add       esp,18h
  004df6cb  3bc5                  cmp       eax,ebp
  004df6cd  0f8458020000          je        near 004DF92Bh
  004df6d3  56                    push      esi
  004df6d4  e8c7beffff            call      004DB5A0h
  004df6d9  33c9                  xor       ecx,ecx
  004df6db  83c404                add       esp,4
  004df6de  8a4e08                mov       cl,[esi+8]
  004df6e1  8d1489                lea       edx,[ecx+ecx*4]
  004df6e4  c1e203                shl       edx,3
  004df6e7  2bd1                  sub       edx,ecx
  004df6e9  c1e202                shl       edx,2
  004df6ec  8a82cb935e00          mov       al,[edx+5E93CBh]
  004df6f2  88460b                mov       [esi+0Bh],al
  004df6f5  8b06                  mov       eax,[esi]
  004df6f7  a802                  test      al,2
  004df6f9  0f8410010000          je        near 004DF80Fh
  004df6ff  f644241810            test      byte [esp+18h],10h
  004df704  7409                  je        short 004DF70Fh
  004df706  24fd                  and       al,0FDh
  004df708  8906                  mov       [esi],eax
  004df70a  e918020000            jmp       004DF927h

LAB_004df70f:
  004df70f  8b15b4876000          mov       edx,[6087B4h]
  004df715  33db                  xor       ebx,ebx
  004df717  c1e10d                shl       ecx,0Dh
  004df71a  8b12                  mov       edx,[edx]
  004df71c  8a9c1150010f00        mov       bl,[ecx+edx+0F0150h]
  004df723  8bfb                  mov       edi,ebx
  004df725  83e707                and       edi,7
  004df728  83ff04                cmp       edi,4
  004df72b  741c                  je        short 004DF749h
  004df72d  83ff07                cmp       edi,7
  004df730  7550                  jne       short 004DF782h
  004df732  817e1803000300        cmp       dword [esi+18h],30003h
  004df739  7547                  jne       short 004DF782h
  004df73b  6aff                  push      0FFFFFFFFh
  004df73d  80cc08                or        ah,8
  004df740  6aff                  push      0FFFFFFFFh
  004df742  55                    push      ebp
  004df743  8906                  mov       [esi],eax
  004df745  6a06                  push      6
  004df747  eb29                  jmp       short 004DF772h

LAB_004df749:
  004df749  8b4e18                mov       ecx,[esi+18h]
  004df74c  81f903001000          cmp       ecx,100003h
  004df752  7505                  jne       short 004DF759h
  004df754  f6c308                test      bl,8
  004df757  740d                  je        short 004DF766h

LAB_004df759:
  004df759  81f903000c00          cmp       ecx,0C0003h
  004df75f  7521                  jne       short 004DF782h
  004df761  f6c308                test      bl,8
  004df764  741c                  je        short 004DF782h

LAB_004df766:
  004df766  6aff                  push      0FFFFFFFFh
  004df768  80cc08                or        ah,8
  004df76b  6aff                  push      0FFFFFFFFh
  004df76d  55                    push      ebp
  004df76e  8906                  mov       [esi],eax
  004df770  6a05                  push      5

LAB_004df772:
  004df772  56                    push      esi
  004df773  e808b20500            call      0053A980h
  004df778  8b06                  mov       eax,[esi]
  004df77a  83c414                add       esp,14h
  004df77d  80e4f7                and       ah,0F7h
  004df780  8906                  mov       [esi],eax

LAB_004df782:
  004df782  8b06                  mov       eax,[esi]
  004df784  a900000002            test      eax,2000000h
  004df789  7469                  je        short 004DF7F4h
  004df78b  83ff04                cmp       edi,4
  004df78e  745a                  je        short 004DF7EAh
  004df790  83ff07                cmp       edi,7
  004df793  7455                  je        short 004DF7EAh
  004df795  8d045b                lea       eax,[ebx+ebx*2]
  004df798  55                    push      ebp
  004df799  8d561c                lea       edx,[esi+1Ch]
  004df79c  8d0c85e82c5500        lea       ecx,[eax*4+552CE8h]
  004df7a3  51                    push      ecx
  004df7a4  52                    push      edx
  004df7a5  56                    push      esi
  004df7a6  e885a40300            call      00519C30h
  004df7ab  8bf8                  mov       edi,eax
  004df7ad  a1b8e05d00            mov       eax,[5DE0B8h]
  004df7b2  83c410                add       esp,10h
  004df7b5  3bc5                  cmp       eax,ebp
  004df7b7  7431                  je        short 004DF7EAh
  004df7b9  57                    push      edi
  004df7ba  56                    push      esi
  004df7bb  e8c0a30500            call      00539B80h
  004df7c0  8b07                  mov       eax,[edi]
  004df7c2  83c408                add       esp,8
  004df7c5  0c40                  or        al,40h
  004df7c7  66c787960000001000    mov       word [edi+96h],10h
  004df7d0  8907                  mov       [edi],eax
  004df7d2  83c71c                add       edi,1Ch
  004df7d5  57                    push      edi
  004df7d6  6800180000            push      1800h
  004df7db  6800030000            push      300h
  004df7e0  6a03                  push      3
  004df7e2  e839e30500            call      0053DB20h
  004df7e7  83c410                add       esp,10h

LAB_004df7ea:
  004df7ea  8b06                  mov       eax,[esi]
  004df7ec  a801                  test      al,1
  004df7ee  740c                  je        short 004DF7FCh
  004df7f0  66ff4e1a              dec       word [esi+1Ah]

LAB_004df7f4:
  004df7f4  a801                  test      al,1
  004df7f6  0f852f010000          jne       near 004DF92Bh

LAB_004df7fc:
  004df7fc  a900000002            test      eax,2000000h
  004df801  7404                  je        short 004DF807h
  004df803  24fd                  and       al,0FDh
  004df805  8906                  mov       [esi],eax

LAB_004df807:
  004df807  896e5c                mov       [esi+5Ch],ebp
  004df80a  e91c010000            jmp       004DF92Bh

LAB_004df80f:
  004df80f  a811                  test      al,11h
  004df811  0f8514010000          jne       near 004DF92Bh
  004df817  a904080000            test      eax,804h
  004df81c  0f8509010000          jne       near 004DF92Bh
  004df822  33c9                  xor       ecx,ecx
  004df824  8a4e42                mov       cl,[esi+42h]
  004df827  83f903                cmp       ecx,3
  004df82a  0f87ee000000          ja        near 004DF91Eh
  004df830  ff248dd8fa4d00        jmp       dword [ecx*4+4DFAD8h]
  004df837  66396e18              cmp       [esi+18h],bp
  004df83b  740a                  je        short 004DF847h
  004df83d  0d00000001            or        eax,1000000h
  004df842  896e18                mov       [esi+18h],ebp
  004df845  8906                  mov       [esi],eax

LAB_004df847:
  004df847  817c24302c010000      cmp       dword [esp+30h],12Ch
  004df84f  7d09                  jge       short 004DF85Ah
  004df851  c6464300              mov       byte [esi+43h],0
  004df855  e9c4000000            jmp       004DF91Eh

LAB_004df85a:
  004df85a  8a4643                mov       al,[esi+43h]
  004df85d  84c0                  test      al,al
  004df85f  7405                  je        short 004DF866h
  004df861  fec8                  dec       al
  004df863  884643                mov       [esi+43h],al

LAB_004df866:
  004df866  896e5c                mov       [esi+5Ch],ebp
  004df869  e9b0000000            jmp       004DF91Eh
  004df86e  668b442410            mov       ax,[esp+10h]
  004df873  c6464300              mov       byte [esi+43h],0
  004df877  66034632              add       ax,[esi+32h]
  004df87b  25000f0000            and       eax,0F00h
  004df880  66894632              mov       [esi+32h],ax
  004df884  e995000000            jmp       004DF91Eh
  004df889  b901000000            mov       ecx,1
  004df88e  66394e18              cmp       [esi+18h],cx
  004df892  740a                  je        short 004DF89Eh
  004df894  0d00000001            or        eax,1000000h
  004df899  894e18                mov       [esi+18h],ecx
  004df89c  8906                  mov       [esi],eax

LAB_004df89e:
  004df89e  0fbf4e10              movsx     ecx,word [esi+10h]
  004df8a2  394c2430              cmp       [esp+30h],ecx
  004df8a6  7d06                  jge       short 004DF8AEh
  004df8a8  c6464300              mov       byte [esi+43h],0
  004df8ac  eb70                  jmp       short 004DF91Eh

LAB_004df8ae:
  004df8ae  8a4643                mov       al,[esi+43h]
  004df8b1  84c0                  test      al,al
  004df8b3  7405                  je        short 004DF8BAh
  004df8b5  fec8                  dec       al
  004df8b7  884643                mov       [esi+43h],al

LAB_004df8ba:
  004df8ba  8b442410              mov       eax,[esp+10h]
  004df8be  8d8800010000          lea       ecx,[eax+100h]
  004df8c4  81e1ff0f0000          and       ecx,0FFFh
  004df8ca  81f900020000          cmp       ecx,200h
  004df8d0  7e45                  jle       short 004DF917h
  004df8d2  25ff0f0000            and       eax,0FFFh
  004df8d7  33c9                  xor       ecx,ecx
  004df8d9  3d00080000            cmp       eax,800h
  004df8de  0f9dc1                setge     cl
  004df8e1  49                    dec       ecx
  004df8e2  81e100020000          and       ecx,200h
  004df8e8  81c100ffffff          add       ecx,0FFFFFF00h
  004df8ee  8bc1                  mov       eax,ecx
  004df8f0  66014632              add       [esi+32h],ax
  004df8f4  eb21                  jmp       short 004DF917h
  004df8f6  b901000000            mov       ecx,1
  004df8fb  66394e18              cmp       [esi+18h],cx
  004df8ff  740a                  je        short 004DF90Bh
  004df901  0d00000001            or        eax,1000000h
  004df906  894e18                mov       [esi+18h],ecx
  004df909  8906                  mov       [esi],eax

LAB_004df90b:
  004df90b  8a4643                mov       al,[esi+43h]
  004df90e  84c0                  test      al,al
  004df910  7405                  je        short 004DF917h
  004df912  fec8                  dec       al
  004df914  884643                mov       [esi+43h],al

LAB_004df917:
  004df917  c7465c00400100        mov       dword [esi+5Ch],14000h

LAB_004df91e:
  004df91e  f682c0935e0006        test      byte [edx+5E93C0h],6
  004df925  7404                  je        short 004DF92Bh

LAB_004df927:
  004df927  c6464300              mov       byte [esi+43h],0

LAB_004df92b:
  004df92b  8b542414              mov       edx,[esp+14h]
  004df92f  52                    push      edx
  004df930  56                    push      esi
  004df931  e82a88faff            call      00488160h
  004df936  83c408                add       esp,8
  004df939  56                    push      esi
  004df93a  e801400500            call      00533940h
  004df93f  83c404                add       esp,4
  004df942  56                    push      esi
  004df943  e8a8010000            call      004DFAF0h
  004df948  8b442420              mov       eax,[esp+20h]
  004df94c  83c404                add       esp,4
  004df94f  50                    push      eax
  004df950  56                    push      esi
  004df951  e81a3e0000            call      004E3770h
  004df956  8b44241c              mov       eax,[esp+1Ch]
  004df95a  83c408                add       esp,8
  004df95d  3bc5                  cmp       eax,ebp
  004df95f  0f8469010000          je        near 004DFACEh
  004df965  f70617080000          test      dword [esi],817h
  004df96b  0f855d010000          jne       near 004DFACEh
  004df971  8a4643                mov       al,[esi+43h]
  004df974  84c0                  test      al,al
  004df976  0f8552010000          jne       near 004DFACEh
  004df97c  8b466c                mov       eax,[esi+6Ch]
  004df97f  8b5e1c                mov       ebx,[esi+1Ch]
  004df982  8b4e24                mov       ecx,[esi+24h]
  004df985  8b781c                mov       edi,[eax+1Ch]
  004df988  8b6824                mov       ebp,[eax+24h]
  004df98b  2bfb                  sub       edi,ebx
  004df98d  2be9                  sub       ebp,ecx
  004df98f  c1ff0c                sar       edi,0Ch
  004df992  c1fd0c                sar       ebp,0Ch
  004df995  8bcf                  mov       ecx,edi
  004df997  8bd5                  mov       edx,ebp
  004df999  0fafcf                imul      ecx,edi
  004df99c  0fafd5                imul      edx,ebp
  004df99f  03ca                  add       ecx,edx
  004df9a1  51                    push      ecx
  004df9a2  e869600600            call      00545A10h
  004df9a7  55                    push      ebp
  004df9a8  57                    push      edi
  004df9a9  89442438              mov       [esp+38h],eax
  004df9ad  e870600600            call      00545A22h
  004df9b2  668b4e32              mov       cx,[esi+32h]
  004df9b6  2bc1                  sub       eax,ecx
  004df9b8  25ff0f0000            and       eax,0FFFh
  004df9bd  89442410              mov       [esp+10h],eax
  004df9c1  e87a770600            call      00547140h
  004df9c6  bf64000000            mov       edi,64h
  004df9cb  99                    cdq
  004df9cc  8bcf                  mov       ecx,edi
  004df9ce  f7f9                  idiv      ecx
  004df9d0  817c24302c010000      cmp       dword [esp+30h],12Ch
  004df9d8  7d70                  jge       short 004DFA4Ah
  004df9da  83fa28                cmp       edx,28h
  004df9dd  7d3d                  jge       short 004DFA1Ch
  004df9df  8b542410              mov       edx,[esp+10h]
  004df9e3  81c200010000          add       edx,100h
  004df9e9  81e2ff0f0000          and       edx,0FFFh
  004df9ef  81fa00020000          cmp       edx,200h
  004df9f5  7f58                  jg        short 004DFA4Fh
  004df9f7  8b06                  mov       eax,[esi]
  004df9f9  c7461803000000        mov       dword [esi+18h],3
  004dfa00  25fffffffe            and       eax,0FEFFFFFFh
  004dfa05  c6464200              mov       byte [esi+42h],0
  004dfa09  0c02                  or        al,2
  004dfa0b  c7465c00000000        mov       dword [esi+5Ch],0
  004dfa12  8906                  mov       [esi],eax
  004dfa14  5f                    pop       edi
  004dfa15  5e                    pop       esi
  004dfa16  5d                    pop       ebp
  004dfa17  5b                    pop       ebx
  004dfa18  83c41c                add       esp,1Ch
  004dfa1b  c3                    ret

LAB_004dfa1c:
  004dfa1c  83fa3c                cmp       edx,3Ch
  004dfa1f  7d22                  jge       short 004DFA43h
  004dfa21  c6464202              mov       byte [esi+42h],2
  004dfa25  e816770600            call      00547140h
  004dfa2a  99                    cdq
  004dfa2b  b90a000000            mov       ecx,0Ah
  004dfa30  66897e10              mov       [esi+10h],di
  004dfa34  f7f9                  idiv      ecx
  004dfa36  02d1                  add       dl,cl
  004dfa38  885643                mov       [esi+43h],dl
  004dfa3b  5f                    pop       edi
  004dfa3c  5e                    pop       esi
  004dfa3d  5d                    pop       ebp
  004dfa3e  5b                    pop       ebx
  004dfa3f  83c41c                add       esp,1Ch
  004dfa42  c3                    ret

LAB_004dfa43:
  004dfa43  83fa50                cmp       edx,50h
  004dfa46  7d70                  jge       short 004DFAB8h
  004dfa48  eb41                  jmp       short 004DFA8Bh

LAB_004dfa4a:
  004dfa4a  83fa14                cmp       edx,14h
  004dfa4d  7d10                  jge       short 004DFA5Fh

LAB_004dfa4f:
  004dfa4f  c6464201              mov       byte [esi+42h],1
  004dfa53  c6464301              mov       byte [esi+43h],1
  004dfa57  5f                    pop       edi
  004dfa58  5e                    pop       esi
  004dfa59  5d                    pop       ebp
  004dfa5a  5b                    pop       ebx
  004dfa5b  83c41c                add       esp,1Ch
  004dfa5e  c3                    ret

LAB_004dfa5f:
  004dfa5f  83fa28                cmp       edx,28h
  004dfa62  7d22                  jge       short 004DFA86h
  004dfa64  c6464202              mov       byte [esi+42h],2
  004dfa68  e8d3760600            call      00547140h
  004dfa6d  99                    cdq
  004dfa6e  b90a000000            mov       ecx,0Ah
  004dfa73  66897e10              mov       [esi+10h],di
  004dfa77  f7f9                  idiv      ecx
  004dfa79  02d1                  add       dl,cl
  004dfa7b  885643                mov       [esi+43h],dl
  004dfa7e  5f                    pop       edi
  004dfa7f  5e                    pop       esi
  004dfa80  5d                    pop       ebp
  004dfa81  5b                    pop       ebx
  004dfa82  83c41c                add       esp,1Ch
  004dfa85  c3                    ret

LAB_004dfa86:
  004dfa86  83fa50                cmp       edx,50h
  004dfa89  7d2d                  jge       short 004DFAB8h

LAB_004dfa8b:
  004dfa8b  c6464203              mov       byte [esi+42h],3
  004dfa8f  e8ac760600            call      00547140h
  004dfa94  99                    cdq
  004dfa95  b90a000000            mov       ecx,0Ah
  004dfa9a  f7f9                  idiv      ecx
  004dfa9c  02d1                  add       dl,cl
  004dfa9e  885643                mov       [esi+43h],dl
  004dfaa1  e89a760600            call      00547140h
  004dfaa6  240f                  and       al,0Fh
  004dfaa8  33d2                  xor       edx,edx
  004dfaaa  8af0                  mov       dh,al
  004dfaac  66895632              mov       [esi+32h],dx
  004dfab0  5f                    pop       edi
  004dfab1  5e                    pop       esi
  004dfab2  5d                    pop       ebp
  004dfab3  5b                    pop       ebx
  004dfab4  83c41c                add       esp,1Ch
  004dfab7  c3                    ret

LAB_004dfab8:
  004dfab8  c6464200              mov       byte [esi+42h],0
  004dfabc  e87f760600            call      00547140h
  004dfac1  99                    cdq
  004dfac2  b90a000000            mov       ecx,0Ah
  004dfac7  f7f9                  idiv      ecx
  004dfac9  02d1                  add       dl,cl
  004dfacb  885643                mov       [esi+43h],dl

LAB_004dface:
  004dface  5f                    pop       edi
  004dfacf  5e                    pop       esi
  004dfad0  5d                    pop       ebp
  004dfad1  5b                    pop       ebx
  004dfad2  83c41c                add       esp,1Ch
  004dfad5  c3                    ret
  004dfad6  8bff                  mov       edi,edi
  004dfad8  37                    aaa
  004dfad9  f8                    clc
  004dfada  4d                    dec       ebp
  004dfadb  006ef8                add       [esi-8],ch
  004dfade  4d                    dec       ebp
  004dfadf  0089f84d00f6          add       [ecx-9FFB208h],cl
  004dfae5  f8                    clc
  004dfae6  4d                    dec       ebp
  004dfae7  009090909090          add       [eax-6F6F6F70h],dl
  004dfaed  90                    nop
  004dfaee  90                    nop
  004dfaef  90                    nop

31 cible(s) de saut interne : 0x004df70f, 0x004df749, 0x004df759, 0x004df766, 0x004df772, 0x004df782, 0x004df7ea, 0x004df7f4, 0x004df7fc, 0x004df807, 0x004df80f, 0x004df847, 0x004df85a, 0x004df866, 0x004df89e, 0x004df8ae, 0x004df8ba, 0x004df90b, 0x004df917, 0x004df91e, 0x004df927, 0x004df92b, 0x004dfa1c, 0x004dfa43, 0x004dfa4a, 0x004dfa4f, 0x004dfa5f, 0x004dfa86, 0x004dfa8b, 0x004dfab8, 0x004dface
