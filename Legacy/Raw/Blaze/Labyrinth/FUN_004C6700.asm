Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004c6700  section .text  offset fichier 0xc5b00  2880 octets

  004c6700  81ec84000000          sub       esp,84h
  004c6706  53                    push      ebx
  004c6707  55                    push      ebp
  004c6708  56                    push      esi
  004c6709  8bb42494000000        mov       esi,[esp+94h]
  004c6710  33db                  xor       ebx,ebx
  004c6712  57                    push      edi
  004c6713  8a4641                mov       al,[esi+41h]
  004c6716  66895c2430            mov       [esp+30h],bx
  004c671b  3ac3                  cmp       al,bl
  004c671d  66c74424321e00        mov       word [esp+32h],1Eh
  004c6724  66c74424343200        mov       word [esp+34h],32h
  004c672b  66c74424366400        mov       word [esp+36h],64h
  004c6732  66895c2438            mov       [esp+38h],bx
  004c6737  66c744243a5000        mov       word [esp+3Ah],50h
  004c673e  7510                  jne       short 004C6750h
  004c6740  53                    push      ebx
  004c6741  6a04                  push      4
  004c6743  56                    push      esi
  004c6744  c6464101              mov       byte [esi+41h],1
  004c6748  e803cdffff            call      004C3450h
  004c674d  83c40c                add       esp,0Ch

LAB_004c6750:
  004c6750  8b461c                mov       eax,[esi+1Ch]
  004c6753  8b4e20                mov       ecx,[esi+20h]
  004c6756  8b5624                mov       edx,[esi+24h]
  004c6759  8d6e1c                lea       ebp,[esi+1Ch]
  004c675c  89442424              mov       [esp+24h],eax
  004c6760  8b4664                mov       eax,[esi+64h]
  004c6763  894c2428              mov       [esp+28h],ecx
  004c6767  8b4e60                mov       ecx,[esi+60h]
  004c676a  8954242c              mov       [esp+2Ch],edx
  004c676e  668b5632              mov       dx,[esi+32h]
  004c6772  89442440              mov       [esp+40h],eax
  004c6776  8b467c                mov       eax,[esi+7Ch]
  004c6779  894c2448              mov       [esp+48h],ecx
  004c677d  668b8e90000000        mov       cx,[esi+90h]
  004c6784  6689942482000000      mov       [esp+82h],dx
  004c678c  8b9680000000          mov       edx,[esi+80h]
  004c6792  89442454              mov       [esp+54h],eax
  004c6796  8b8684000000          mov       eax,[esi+84h]
  004c679c  66894c2458            mov       [esp+58h],cx
  004c67a1  8b8e88000000          mov       ecx,[esi+88h]
  004c67a7  89542444              mov       [esp+44h],edx
  004c67ab  8b968c000000          mov       edx,[esi+8Ch]
  004c67b1  89442450              mov       [esp+50h],eax
  004c67b5  894c243c              mov       [esp+3Ch],ecx
  004c67b9  8d44245c              lea       eax,[esp+5Ch]
  004c67bd  8954244c              mov       [esp+4Ch],edx
  004c67c1  8d4c2460              lea       ecx,[esp+60h]
  004c67c5  50                    push      eax
  004c67c6  8d542468              lea       edx,[esp+68h]
  004c67ca  51                    push      ecx
  004c67cb  52                    push      edx
  004c67cc  56                    push      esi
  004c67cd  e8ae210700            call      00538980h
  004c67d2  83c410                add       esp,10h
  004c67d5  8d442468              lea       eax,[esp+68h]
  004c67d9  8d4c2420              lea       ecx,[esp+20h]
  004c67dd  8d542410              lea       edx,[esp+10h]
  004c67e1  50                    push      eax
  004c67e2  51                    push      ecx
  004c67e3  8d442424              lea       eax,[esp+24h]
  004c67e7  52                    push      edx
  004c67e8  8d4c2420              lea       ecx,[esp+20h]
  004c67ec  50                    push      eax
  004c67ed  51                    push      ecx
  004c67ee  56                    push      esi
  004c67ef  e82cf70600            call      00535F20h
  004c67f4  8b7c242c              mov       edi,[esp+2Ch]
  004c67f8  b840000000            mov       eax,40h
  004c67fd  2bc7                  sub       eax,edi
  004c67ff  83c418                add       esp,18h
  004c6802  89442418              mov       [esp+18h],eax
  004c6806  8b442420              mov       eax,[esp+20h]
  004c680a  3bc3                  cmp       eax,ebx
  004c680c  0f84ad040000          je        near 004C6CBFh
  004c6812  8b0e                  mov       ecx,[esi]
  004c6814  33d2                  xor       edx,edx
  004c6816  8a5604                mov       dl,[esi+4]
  004c6819  81e1fffffffd          and       ecx,0FDFFFFFFh
  004c681f  0fbf5e18              movsx     ebx,word [esi+18h]
  004c6823  890e                  mov       [esi],ecx
  004c6825  8b149540965e00        mov       edx,[edx*4+5E9640h]
  004c682c  f7c100000001          test      ecx,1000000h
  004c6832  8b149a                mov       edx,[edx+ebx*4]
  004c6835  741d                  je        short 004C6854h
  004c6837  66ff461a              inc       word [esi+1Ah]
  004c683b  668b4e1a              mov       cx,[esi+1Ah]
  004c683f  33db                  xor       ebx,ebx
  004c6841  668b5a16              mov       bx,[edx+16h]
  004c6845  0fbfc9                movsx     ecx,cx
  004c6848  3bcb                  cmp       ecx,ebx
  004c684a  7c2b                  jl        short 004C6877h
  004c684c  66c7461a0000          mov       word [esi+1Ah],0
  004c6852  eb23                  jmp       short 004C6877h

LAB_004c6854:
  004c6854  66ff461a              inc       word [esi+1Ah]
  004c6858  33db                  xor       ebx,ebx
  004c685a  668b5a16              mov       bx,[edx+16h]
  004c685e  0fbf561a              movsx     edx,word [esi+1Ah]
  004c6862  3bd3                  cmp       edx,ebx
  004c6864  7c11                  jl        short 004C6877h
  004c6866  668b561a              mov       dx,[esi+1Ah]
  004c686a  4a                    dec       edx
  004c686b  81c900000002          or        ecx,2000000h
  004c6871  6689561a              mov       [esi+1Ah],dx
  004c6875  890e                  mov       [esi],ecx

LAB_004c6877:
  004c6877  8a0e                  mov       cl,[esi]
  004c6879  33db                  xor       ebx,ebx
  004c687b  f6c102                test      cl,2
  004c687e  747b                  je        short 004C68FBh
  004c6880  66837e1a0a            cmp       word [esi+1Ah],0Ah
  004c6885  7406                  je        short 004C688Dh
  004c6887  807e4207              cmp       byte [esi+42h],7
  004c688b  7555                  jne       short 004C68E2h

LAB_004c688d:
  004c688d  8d442430              lea       eax,[esp+30h]
  004c6891  53                    push      ebx
  004c6892  50                    push      eax
  004c6893  55                    push      ebp
  004c6894  56                    push      esi
  004c6895  e896330500            call      00519C30h
  004c689a  8bf8                  mov       edi,eax
  004c689c  a1b8e05d00            mov       eax,[5DE0B8h]
  004c68a1  83c410                add       esp,10h
  004c68a4  3bc3                  cmp       eax,ebx
  004c68a6  7432                  je        short 004C68DAh
  004c68a8  8b0f                  mov       ecx,[edi]
  004c68aa  57                    push      edi
  004c68ab  83c940                or        ecx,40h
  004c68ae  56                    push      esi
  004c68af  66c787960000001000    mov       word [edi+96h],10h
  004c68b8  890f                  mov       [edi],ecx
  004c68ba  e8c1320700            call      00539B80h
  004c68bf  83c408                add       esp,8
  004c68c2  83c71c                add       edi,1Ch
  004c68c5  57                    push      edi
  004c68c6  6800180000            push      1800h
  004c68cb  6800030000            push      300h
  004c68d0  6a03                  push      3
  004c68d2  e849720700            call      0053DB20h
  004c68d7  83c410                add       esp,10h

LAB_004c68da:
  004c68da  8b442420              mov       eax,[esp+20h]
  004c68de  8b7c2414              mov       edi,[esp+14h]

LAB_004c68e2:
  004c68e2  8b0e                  mov       ecx,[esi]
  004c68e4  f7c100000002          test      ecx,2000000h
  004c68ea  740f                  je        short 004C68FBh
  004c68ec  807e4207              cmp       byte [esi+42h],7
  004c68f0  7409                  je        short 004C68FBh
  004c68f2  83e1fd                and       ecx,0FFFFFFFDh
  004c68f5  c6464300              mov       byte [esi+43h],0
  004c68f9  890e                  mov       [esi],ecx

LAB_004c68fb:
  004c68fb  8b0e                  mov       ecx,[esi]
  004c68fd  f6c102                test      cl,2
  004c6900  740a                  je        short 004C690Ch
  004c6902  807e4207              cmp       byte [esi+42h],7
  004c6906  0f85b3030000          jne       near 004C6CBFh

LAB_004c690c:
  004c690c  33d2                  xor       edx,edx
  004c690e  8a5642                mov       dl,[esi+42h]
  004c6911  83fa08                cmp       edx,8
  004c6914  0f87a5030000          ja        near 004C6CBFh
  004c691a  ff249510724c00        jmp       dword [edx*4+4C7210h]
  004c6921  817c241c2c010000      cmp       dword [esp+1Ch],12Ch
  004c6929  7d09                  jge       short 004C6934h
  004c692b  c6464300              mov       byte [esi+43h],0
  004c692f  e98b030000            jmp       004C6CBFh

LAB_004c6934:
  004c6934  8a4643                mov       al,[esi+43h]
  004c6937  84c0                  test      al,al
  004c6939  7466                  je        short 004C69A1h
  004c693b  fec8                  dec       al
  004c693d  895e5c                mov       [esi+5Ch],ebx
  004c6940  884643                mov       [esi+43h],al
  004c6943  e977030000            jmp       004C6CBFh
  004c6948  8b442410              mov       eax,[esp+10h]
  004c694c  8d90c8000000          lea       edx,[eax+0C8h]
  004c6952  81e2ff0f0000          and       edx,0FFFh
  004c6958  81fa90010000          cmp       edx,190h
  004c695e  7f0c                  jg        short 004C696Ch
  004c6960  c6464300              mov       byte [esi+43h],0
  004c6964  895e5c                mov       [esi+5Ch],ebx
  004c6967  e953030000            jmp       004C6CBFh

LAB_004c696c:
  004c696c  25ff0f0000            and       eax,0FFFh
  004c6971  3d00080000            cmp       eax,800h
  004c6976  7d08                  jge       short 004C6980h
  004c6978  8d8780000000          lea       eax,[edi+80h]
  004c697e  eb07                  jmp       short 004C6987h

LAB_004c6980:
  004c6980  b880ffffff            mov       eax,0FFFFFF80h
  004c6985  2bc7                  sub       eax,edi

LAB_004c6987:
  004c6987  66014632              add       [esi+32h],ax
  004c698b  b801000000            mov       eax,1
  004c6990  66394618              cmp       [esi+18h],ax
  004c6994  740b                  je        short 004C69A1h
  004c6996  81c900000001          or        ecx,1000000h
  004c699c  894618                mov       [esi+18h],eax
  004c699f  890e                  mov       [esi],ecx

LAB_004c69a1:
  004c69a1  895e5c                mov       [esi+5Ch],ebx
  004c69a4  e916030000            jmp       004C6CBFh
  004c69a9  0fbf4610              movsx     eax,word [esi+10h]
  004c69ad  3944241c              cmp       [esp+1Ch],eax
  004c69b1  7d09                  jge       short 004C69BCh
  004c69b3  c6464300              mov       byte [esi+43h],0
  004c69b7  e903030000            jmp       004C6CBFh

LAB_004c69bc:
  004c69bc  8a4643                mov       al,[esi+43h]
  004c69bf  84c0                  test      al,al
  004c69c1  7405                  je        short 004C69C8h
  004c69c3  fec8                  dec       al
  004c69c5  884643                mov       [esi+43h],al

LAB_004c69c8:
  004c69c8  b801000000            mov       eax,1
  004c69cd  66394618              cmp       [esi+18h],ax
  004c69d1  740b                  je        short 004C69DEh
  004c69d3  81c900000001          or        ecx,1000000h
  004c69d9  894618                mov       [esi+18h],eax
  004c69dc  890e                  mov       [esi],ecx

LAB_004c69de:
  004c69de  8b442410              mov       eax,[esp+10h]
  004c69e2  8d8800010000          lea       ecx,[eax+100h]
  004c69e8  81e1ff0f0000          and       ecx,0FFFh
  004c69ee  81f900020000          cmp       ecx,200h
  004c69f4  7e23                  jle       short 004C6A19h
  004c69f6  25ff0f0000            and       eax,0FFFh
  004c69fb  3d00080000            cmp       eax,800h
  004c6a00  7d09                  jge       short 004C6A0Bh
  004c6a02  8d843f80000000        lea       eax,[edi+edi+80h]
  004c6a09  eb0a                  jmp       short 004C6A15h

LAB_004c6a0b:
  004c6a0b  8d143f                lea       edx,[edi+edi]
  004c6a0e  b880ffffff            mov       eax,0FFFFFF80h
  004c6a13  2bc2                  sub       eax,edx

LAB_004c6a15:
  004c6a15  66014632              add       [esi+32h],ax

LAB_004c6a19:
  004c6a19  8d8780010000          lea       eax,[edi+180h]
  004c6a1f  8d0480                lea       eax,[eax+eax*4]
  004c6a22  c1e005                shl       eax,5
  004c6a25  89465c                mov       [esi+5Ch],eax
  004c6a28  e992020000            jmp       004C6CBFh
  004c6a2d  8a5643                mov       dl,[esi+43h]
  004c6a30  84d2                  test      dl,dl
  004c6a32  7405                  je        short 004C6A39h
  004c6a34  feca                  dec       dl
  004c6a36  885643                mov       [esi+43h],dl

LAB_004c6a39:
  004c6a39  ba01000000            mov       edx,1
  004c6a3e  66395618              cmp       [esi+18h],dx
  004c6a42  740b                  je        short 004C6A4Fh
  004c6a44  81c900000001          or        ecx,1000000h
  004c6a4a  895618                mov       [esi+18h],edx
  004c6a4d  890e                  mov       [esi],ecx

LAB_004c6a4f:
  004c6a4f  8d8fa0000000          lea       ecx,[edi+0A0h]
  004c6a55  c1e108                shl       ecx,8
  004c6a58  0fafc1                imul      eax,ecx
  004c6a5b  894e5c                mov       [esi+5Ch],ecx
  004c6a5e  0fbf4e10              movsx     ecx,word [esi+10h]
  004c6a62  c1f80c                sar       eax,0Ch
  004c6a65  51                    push      ecx
  004c6a66  8bf8                  mov       edi,eax
  004c6a68  e8afef0700            call      00545A1Ch
  004c6a6d  0fafc7                imul      eax,edi
  004c6a70  0fbf5610              movsx     edx,word [esi+10h]
  004c6a74  8b4d00                mov       ecx,[ebp]
  004c6a77  52                    push      edx
  004c6a78  c1f80c                sar       eax,0Ch
  004c6a7b  03c8                  add       ecx,eax
  004c6a7d  894d00                mov       [ebp],ecx
  004c6a80  e891ef0700            call      00545A16h
  004c6a85  0fafc7                imul      eax,edi
  004c6a88  8b6e24                mov       ebp,[esi+24h]
  004c6a8b  895e5c                mov       [esi+5Ch],ebx
  004c6a8e  c1f80c                sar       eax,0Ch
  004c6a91  03e8                  add       ebp,eax
  004c6a93  8b442410              mov       eax,[esp+10h]
  004c6a97  896e24                mov       [esi+24h],ebp
  004c6a9a  8d8800010000          lea       ecx,[eax+100h]
  004c6aa0  81e1ff0f0000          and       ecx,0FFFh
  004c6aa6  81f900020000          cmp       ecx,200h
  004c6aac  0f8e0d020000          jle       near 004C6CBFh
  004c6ab2  25ff0f0000            and       eax,0FFFh
  004c6ab7  3d00080000            cmp       eax,800h
  004c6abc  7d14                  jge       short 004C6AD2h
  004c6abe  8b542414              mov       edx,[esp+14h]
  004c6ac2  8d04d532000000        lea       eax,[edx*8+32h]
  004c6ac9  66014632              add       [esi+32h],ax
  004c6acd  e9ed010000            jmp       004C6CBFh

LAB_004c6ad2:
  004c6ad2  8b442414              mov       eax,[esp+14h]
  004c6ad6  8d0cc500000000        lea       ecx,[eax*8]
  004c6add  b8ceffffff            mov       eax,0FFFFFFCEh
  004c6ae2  2bc1                  sub       eax,ecx
  004c6ae4  66014632              add       [esi+32h],ax
  004c6ae8  e9d2010000            jmp       004C6CBFh
  004c6aed  f6c101                test      cl,1
  004c6af0  0f85c9010000          jne       near 004C6CBFh
  004c6af6  81c900000001          or        ecx,1000000h
  004c6afc  c7461801000000        mov       dword [esi+18h],1
  004c6b03  890e                  mov       [esi],ecx
  004c6b05  8d8ff0000000          lea       ecx,[edi+0F0h]
  004c6b0b  c1e108                shl       ecx,8
  004c6b0e  0fafc1                imul      eax,ecx
  004c6b11  0fbf5610              movsx     edx,word [esi+10h]
  004c6b15  c1f80c                sar       eax,0Ch
  004c6b18  52                    push      edx
  004c6b19  894e5c                mov       [esi+5Ch],ecx
  004c6b1c  8bf8                  mov       edi,eax
  004c6b1e  e8f9ee0700            call      00545A1Ch
  004c6b23  0fafc7                imul      eax,edi
  004c6b26  8b6e50                mov       ebp,[esi+50h]
  004c6b29  c1f80c                sar       eax,0Ch
  004c6b2c  03e8                  add       ebp,eax
  004c6b2e  0fbf4610              movsx     eax,word [esi+10h]
  004c6b32  50                    push      eax
  004c6b33  896e50                mov       [esi+50h],ebp
  004c6b36  e8dbee0700            call      00545A16h
  004c6b3b  0fafc7                imul      eax,edi
  004c6b3e  8b5658                mov       edx,[esi+58h]
  004c6b41  8b4e54                mov       ecx,[esi+54h]
  004c6b44  c1f80c                sar       eax,0Ch
  004c6b47  03d0                  add       edx,eax
  004c6b49  8b06                  mov       eax,[esi]
  004c6b4b  81c100e00100          add       ecx,1E000h
  004c6b51  0c01                  or        al,1
  004c6b53  895658                mov       [esi+58h],edx
  004c6b56  894e54                mov       [esi+54h],ecx
  004c6b59  8906                  mov       [esi],eax
  004c6b5b  e95f010000            jmp       004C6CBFh
  004c6b60  668b4612              mov       ax,[esi+12h]
  004c6b64  663bc3                cmp       ax,bx
  004c6b67  7503                  jne       short 004C6B6Ch
  004c6b69  895e5c                mov       [esi+5Ch],ebx

LAB_004c6b6c:
  004c6b6c  663d0a00              cmp       ax,0Ah
  004c6b70  bb05000000            mov       ebx,5
  004c6b75  0f8d9e000000          jge       near 004C6C19h
  004c6b7b  66837e1a06            cmp       word [esi+1Ah],6
  004c6b80  66895e18              mov       [esi+18h],bx
  004c6b84  7c04                  jl        short 004C6B8Ah
  004c6b86  66895e1a              mov       [esi+1Ah],bx

LAB_004c6b8a:
  004c6b8a  40                    inc       eax
  004c6b8b  663d0700              cmp       ax,7
  004c6b8f  66894612              mov       [esi+12h],ax
  004c6b93  7505                  jne       short 004C6B9Ah
  004c6b95  83c902                or        ecx,2
  004c6b98  890e                  mov       [esi],ecx

LAB_004c6b9a:
  004c6b9a  663d0a00              cmp       ax,0Ah
  004c6b9e  7541                  jne       short 004C6BE1h
  004c6ba0  81c700010000          add       edi,100h
  004c6ba6  8b5654                mov       edx,[esi+54h]
  004c6ba9  55                    push      ebp
  004c6baa  6800120000            push      1200h
  004c6baf  8d0cbf                lea       ecx,[edi+edi*4]
  004c6bb2  81c200400100          add       edx,14000h
  004c6bb8  c1e108                shl       ecx,8
  004c6bbb  894e5c                mov       [esi+5Ch],ecx
  004c6bbe  8b0e                  mov       ecx,[esi]
  004c6bc0  83c903                or        ecx,3
  004c6bc3  6800060000            push      600h
  004c6bc8  6a24                  push      24h
  004c6bca  66c746126400          mov       word [esi+12h],64h
  004c6bd0  895654                mov       [esi+54h],edx
  004c6bd3  890e                  mov       [esi],ecx
  004c6bd5  e8466f0700            call      0053DB20h
  004c6bda  8b7c2424              mov       edi,[esp+24h]
  004c6bde  83c410                add       esp,10h

LAB_004c6be1:
  004c6be1  8b442410              mov       eax,[esp+10h]
  004c6be5  8d9000010000          lea       edx,[eax+100h]
  004c6beb  81e2ff0f0000          and       edx,0FFFh
  004c6bf1  81fa00020000          cmp       edx,200h
  004c6bf7  7e20                  jle       short 004C6C19h
  004c6bf9  25ff0f0000            and       eax,0FFFh
  004c6bfe  3d00080000            cmp       eax,800h
  004c6c03  7d06                  jge       short 004C6C0Bh
  004c6c05  8d7c3f32              lea       edi,[edi+edi+32h]
  004c6c09  eb0a                  jmp       short 004C6C15h

LAB_004c6c0b:
  004c6c0b  8d043f                lea       eax,[edi+edi]
  004c6c0e  bfceffffff            mov       edi,0FFFFFFCEh
  004c6c13  2bf8                  sub       edi,eax

LAB_004c6c15:
  004c6c15  66017e32              add       [esi+32h],di

LAB_004c6c19:
  004c6c19  66837e1264            cmp       word [esi+12h],64h
  004c6c1e  b965000000            mov       ecx,65h
  004c6c23  752e                  jne       short 004C6C53h
  004c6c25  8b465c                mov       eax,[esi+5Ch]
  004c6c28  66895e18              mov       [esi+18h],bx
  004c6c2c  8bd0                  mov       edx,eax
  004c6c2e  c1fa02                sar       edx,2
  004c6c31  2bc2                  sub       eax,edx
  004c6c33  66837e1a0b            cmp       word [esi+1Ah],0Bh
  004c6c38  89465c                mov       [esi+5Ch],eax
  004c6c3b  7c06                  jl        short 004C6C43h
  004c6c3d  66c7461a0a00          mov       word [esi+1Ah],0Ah

LAB_004c6c43:
  004c6c43  2500f0ffff            and       eax,0FFFFF000h
  004c6c48  3d00a00000            cmp       eax,0A000h
  004c6c4d  7d04                  jge       short 004C6C53h
  004c6c4f  66894e12              mov       [esi+12h],cx

LAB_004c6c53:
  004c6c53  66394e12              cmp       [esi+12h],cx
  004c6c57  7566                  jne       short 004C6CBFh
  004c6c59  8b0e                  mov       ecx,[esi]
  004c6c5b  66895e18              mov       [esi+18h],bx
  004c6c5f  83e1fd                and       ecx,0FFFFFFFDh
  004c6c62  66837e1a0c            cmp       word [esi+1Ah],0Ch
  004c6c67  890e                  mov       [esi],ecx
  004c6c69  7554                  jne       short 004C6CBFh
  004c6c6b  c6464300              mov       byte [esi+43h],0
  004c6c6f  eb4e                  jmp       short 004C6CBFh
  004c6c71  668b5612              mov       dx,[esi+12h]
  004c6c75  bd01000000            mov       ebp,1
  004c6c7a  663bd5                cmp       dx,bp
  004c6c7d  751c                  jne       short 004C6C9Bh
  004c6c7f  0fbf4610              movsx     eax,word [esi+10h]
  004c6c83  3944241c              cmp       [esp+1Ch],eax
  004c6c87  7d06                  jge       short 004C6C8Fh
  004c6c89  c6464300              mov       byte [esi+43h],0
  004c6c8d  eb0c                  jmp       short 004C6C9Bh

LAB_004c6c8f:
  004c6c8f  8a4643                mov       al,[esi+43h]
  004c6c92  84c0                  test      al,al
  004c6c94  7405                  je        short 004C6C9Bh
  004c6c96  fec8                  dec       al
  004c6c98  884643                mov       [esi+43h],al

LAB_004c6c9b:
  004c6c9b  663bd3                cmp       dx,bx
  004c6c9e  751f                  jne       short 004C6CBFh
  004c6ca0  d1ff                  sar       edi,1
  004c6ca2  81c900000001          or        ecx,1000000h
  004c6ca8  81c700400000          add       edi,4000h
  004c6cae  66896e18              mov       [esi+18h],bp
  004c6cb2  66895e1a              mov       [esi+1Ah],bx
  004c6cb6  890e                  mov       [esi],ecx
  004c6cb8  66896e12              mov       [esi+12h],bp
  004c6cbc  897e5c                mov       [esi+5Ch],edi

LAB_004c6cbf:
  004c6cbf  0fbf4e32              movsx     ecx,word [esi+32h]
  004c6cc3  51                    push      ecx
  004c6cc4  e853ed0700            call      00545A1Ch
  004c6cc9  0fbf5632              movsx     edx,word [esi+32h]
  004c6ccd  52                    push      edx
  004c6cce  89442470              mov       [esp+70h],eax
  004c6cd2  e83fed0700            call      00545A16h
  004c6cd7  8b5650                mov       edx,[esi+50h]
  004c6cda  8b4c2420              mov       ecx,[esp+20h]
  004c6cde  0fbf9e9a000000        movsx     ebx,word [esi+9Ah]
  004c6ce5  0faf4e5c              imul      ecx,[esi+5Ch]
  004c6ce9  89442474              mov       [esp+74h],eax
  004c6ced  8bc2                  mov       eax,edx
  004c6cef  c1f808                sar       eax,8
  004c6cf2  8bfa                  mov       edi,edx
  004c6cf4  56                    push      esi
  004c6cf5  2bf8                  sub       edi,eax
  004c6cf7  8b4654                mov       eax,[esi+54h]
  004c6cfa  c1fa03                sar       edx,3
  004c6cfd  2bfa                  sub       edi,edx
  004c6cff  8be8                  mov       ebp,eax
  004c6d01  8bd3                  mov       edx,ebx
  004c6d03  897e50                mov       [esi+50h],edi
  004c6d06  c1fd08                sar       ebp,8
  004c6d09  c1fa08                sar       edx,8
  004c6d0c  2bd5                  sub       edx,ebp
  004c6d0e  8b6e58                mov       ebp,[esi+58h]
  004c6d11  03d0                  add       edx,eax
  004c6d13  8bc5                  mov       eax,ebp
  004c6d15  2bd3                  sub       edx,ebx
  004c6d17  8bdd                  mov       ebx,ebp
  004c6d19  c1fb03                sar       ebx,3
  004c6d1c  c1f90c                sar       ecx,0Ch
  004c6d1f  2bc3                  sub       eax,ebx
  004c6d21  8bd9                  mov       ebx,ecx
  004c6d23  0faf5c2470            imul      ebx,[esp+70h]
  004c6d28  0faf4c2478            imul      ecx,[esp+78h]
  004c6d2d  c1fd08                sar       ebp,8
  004c6d30  2bc5                  sub       eax,ebp
  004c6d32  8b6e20                mov       ebp,[esi+20h]
  004c6d35  c1fb0c                sar       ebx,0Ch
  004c6d38  c1f90c                sar       ecx,0Ch
  004c6d3b  895654                mov       [esi+54h],edx
  004c6d3e  03df                  add       ebx,edi
  004c6d40  8b7e1c                mov       edi,[esi+1Ch]
  004c6d43  03ea                  add       ebp,edx
  004c6d45  8b5624                mov       edx,[esi+24h]
  004c6d48  03c8                  add       ecx,eax
  004c6d4a  03fb                  add       edi,ebx
  004c6d4c  03d1                  add       edx,ecx
  004c6d4e  894658                mov       [esi+58h],eax
  004c6d51  897e1c                mov       [esi+1Ch],edi
  004c6d54  896e20                mov       [esi+20h],ebp
  004c6d57  895624                mov       [esi+24h],edx
  004c6d5a  e8e1cb0600            call      00533940h
  004c6d5f  8b06                  mov       eax,[esi]
  004c6d61  8b9e90000000          mov       ebx,[esi+90h]
  004c6d67  8bc8                  mov       ecx,eax
  004c6d69  83c404                add       esp,4
  004c6d6c  80e7fc                and       bh,0FCh
  004c6d6f  83e101                and       ecx,1
  004c6d72  899e90000000          mov       [esi+90h],ebx
  004c6d78  8bd3                  mov       edx,ebx
  004c6d7a  740a                  je        short 004C6D86h
  004c6d7c  8b7e60                mov       edi,[esi+60h]
  004c6d7f  8b5e20                mov       ebx,[esi+20h]
  004c6d82  3bdf                  cmp       ebx,edi
  004c6d84  7e18                  jle       short 004C6D9Eh

LAB_004c6d86:
  004c6d86  33db                  xor       ebx,ebx
  004c6d88  3bcb                  cmp       ecx,ebx
  004c6d8a  7533                  jne       short 004C6DBFh
  004c6d8c  8b6e20                mov       ebp,[esi+20h]
  004c6d8f  8b7e60                mov       edi,[esi+60h]
  004c6d92  81ed00c00000          sub       ebp,0C000h
  004c6d98  3bef                  cmp       ebp,edi
  004c6d9a  7f23                  jg        short 004C6DBFh
  004c6d9c  eb02                  jmp       short 004C6DA0h

LAB_004c6d9e:
  004c6d9e  33db                  xor       ebx,ebx

LAB_004c6da0:
  004c6da0  f6c6a0                test      dh,0A0h
  004c6da3  751a                  jne       short 004C6DBFh
  004c6da5  a802                  test      al,2
  004c6da7  897e20                mov       [esi+20h],edi
  004c6daa  750c                  jne       short 004C6DB8h
  004c6dac  3bcb                  cmp       ecx,ebx
  004c6dae  7408                  je        short 004C6DB8h
  004c6db0  385e43                cmp       [esi+43h],bl
  004c6db3  7403                  je        short 004C6DB8h
  004c6db5  885e43                mov       [esi+43h],bl

LAB_004c6db8:
  004c6db8  24fe                  and       al,0FEh
  004c6dba  895e54                mov       [esi+54h],ebx
  004c6dbd  8906                  mov       [esi],eax

LAB_004c6dbf:
  004c6dbf  f7c282ad0020          test      edx,2000AD82h
  004c6dc5  0f840a010000          je        near 004C6ED5h
  004c6dcb  f60601                test      byte [esi],1
  004c6dce  0f85d9000000          jne       near 004C6EADh
  004c6dd4  8a4e42                mov       cl,[esi+42h]
  004c6dd7  b008                  mov       al,8
  004c6dd9  3ac8                  cmp       cl,al
  004c6ddb  7420                  je        short 004C6DFDh
  004c6ddd  884642                mov       [esi+42h],al
  004c6de0  e85b030800            call      00547140h
  004c6de5  99                    cdq
  004c6de6  b928000000            mov       ecx,28h
  004c6deb  66895e12              mov       [esi+12h],bx
  004c6def  f7f9                  idiv      ecx
  004c6df1  66c746105000          mov       word [esi+10h],50h
  004c6df7  80c214                add       dl,14h
  004c6dfa  885643                mov       [esi+43h],dl

LAB_004c6dfd:
  004c6dfd  8b16                  mov       edx,[esi]
  004c6dff  8b442428              mov       eax,[esp+28h]
  004c6e03  8b4c242c              mov       ecx,[esp+2Ch]
  004c6e07  83e2fd                and       edx,0FFFFFFFDh
  004c6e0a  8916                  mov       [esi],edx
  004c6e0c  8b542424              mov       edx,[esp+24h]
  004c6e10  894620                mov       [esi+20h],eax
  004c6e13  8b442458              mov       eax,[esp+58h]
  004c6e17  89561c                mov       [esi+1Ch],edx
  004c6e1a  8b542454              mov       edx,[esp+54h]
  004c6e1e  25ffff0000            and       eax,0FFFFh
  004c6e23  894e24                mov       [esi+24h],ecx
  004c6e26  8b4c2450              mov       ecx,[esp+50h]
  004c6e2a  89567c                mov       [esi+7Ch],edx
  004c6e2d  8b54243c              mov       edx,[esp+3Ch]
  004c6e31  898690000000          mov       [esi+90h],eax
  004c6e37  8b442444              mov       eax,[esp+44h]
  004c6e3b  898e84000000          mov       [esi+84h],ecx
  004c6e41  8b4c2440              mov       ecx,[esp+40h]
  004c6e45  898680000000          mov       [esi+80h],eax
  004c6e4b  8b44244c              mov       eax,[esp+4Ch]
  004c6e4f  899688000000          mov       [esi+88h],edx
  004c6e55  8b542448              mov       edx,[esp+48h]
  004c6e59  6882ad0020            push      2000AD82h
  004c6e5e  56                    push      esi
  004c6e5f  89868c000000          mov       [esi+8Ch],eax
  004c6e65  894e64                mov       [esi+64h],ecx
  004c6e68  895660                mov       [esi+60h],edx
  004c6e6b  e8f0ef0000            call      004D5E60h
  004c6e70  83c408                add       esp,8
  004c6e73  663bc3                cmp       ax,bx
  004c6e76  7525                  jne       short 004C6E9Dh
  004c6e78  e8c3020800            call      00547140h
  004c6e7d  99                    cdq
  004c6e7e  33c2                  xor       eax,edx
  004c6e80  8b4c2414              mov       ecx,[esp+14h]
  004c6e84  2bc2                  sub       eax,edx
  004c6e86  83c140                add       ecx,40h
  004c6e89  83e001                and       eax,1
  004c6e8c  33c2                  xor       eax,edx
  004c6e8e  2bc2                  sub       eax,edx
  004c6e90  8d4400ff              lea       eax,[eax+eax-1]
  004c6e94  0fafc8                imul      ecx,eax
  004c6e97  66014e32              add       [esi+32h],cx
  004c6e9b  eb38                  jmp       short 004C6ED5h

LAB_004c6e9d:
  004c6e9d  8b542414              mov       edx,[esp+14h]
  004c6ea1  83c240                add       edx,40h
  004c6ea4  0fafd0                imul      edx,eax
  004c6ea7  66015632              add       [esi+32h],dx
  004c6eab  eb28                  jmp       short 004C6ED5h

LAB_004c6ead:
  004c6ead  8b442424              mov       eax,[esp+24h]
  004c6eb1  8b4c242c              mov       ecx,[esp+2Ch]
  004c6eb5  895e5c                mov       [esi+5Ch],ebx
  004c6eb8  895e50                mov       [esi+50h],ebx
  004c6ebb  895e58                mov       [esi+58h],ebx
  004c6ebe  89461c                mov       [esi+1Ch],eax
  004c6ec1  894e24                mov       [esi+24h],ecx
  004c6ec4  885e42                mov       [esi+42h],bl
  004c6ec7  c6464328              mov       byte [esi+43h],28h
  004c6ecb  66895e12              mov       [esi+12h],bx
  004c6ecf  66c7462c0010          mov       word [esi+2Ch],1000h

LAB_004c6ed5:
  004c6ed5  6aff                  push      0FFFFFFFFh
  004c6ed7  6aff                  push      0FFFFFFFFh
  004c6ed9  6aff                  push      0FFFFFFFFh
  004c6edb  56                    push      esi
  004c6edc  e82fc9ffff            call      004C3810h
  004c6ee1  8b442430              mov       eax,[esp+30h]
  004c6ee5  83c410                add       esp,10h
  004c6ee8  3bc3                  cmp       eax,ebx
  004c6eea  7517                  jne       short 004C6F03h
  004c6eec  668b942482000000      mov       dx,[esp+82h]
  004c6ef4  66895632              mov       [esi+32h],dx
  004c6ef8  5f                    pop       edi
  004c6ef9  5e                    pop       esi
  004c6efa  5d                    pop       ebp
  004c6efb  5b                    pop       ebx
  004c6efc  81c484000000          add       esp,84h
  004c6f02  c3                    ret

LAB_004c6f03:
  004c6f03  385e43                cmp       [esi+43h],bl
  004c6f06  0f85f7020000          jne       near 004C7203h
  004c6f0c  8b466c                mov       eax,[esi+6Ch]
  004c6f0f  8b6e1c                mov       ebp,[esi+1Ch]
  004c6f12  8b4e24                mov       ecx,[esi+24h]
  004c6f15  8b781c                mov       edi,[eax+1Ch]
  004c6f18  2bfd                  sub       edi,ebp
  004c6f1a  8b6824                mov       ebp,[eax+24h]
  004c6f1d  2be9                  sub       ebp,ecx
  004c6f1f  c1ff0c                sar       edi,0Ch
  004c6f22  c1fd0c                sar       ebp,0Ch
  004c6f25  8bc7                  mov       eax,edi
  004c6f27  8bcd                  mov       ecx,ebp
  004c6f29  0fafc7                imul      eax,edi
  004c6f2c  0fafcd                imul      ecx,ebp
  004c6f2f  03c1                  add       eax,ecx
  004c6f31  50                    push      eax
  004c6f32  e8d9ea0700            call      00545A10h
  004c6f37  55                    push      ebp
  004c6f38  57                    push      edi
  004c6f39  89442424              mov       [esp+24h],eax
  004c6f3d  e8e0ea0700            call      00545A22h
  004c6f42  668b5632              mov       dx,[esi+32h]
  004c6f46  2bc2                  sub       eax,edx
  004c6f48  25ff0f0000            and       eax,0FFFh
  004c6f4d  89442410              mov       [esp+10h],eax
  004c6f51  8b44241c              mov       eax,[esp+1Ch]
  004c6f55  3dfa000000            cmp       eax,0FAh
  004c6f5a  0f8d22010000          jge       near 004C7082h
  004c6f60  e8db010800            call      00547140h
  004c6f65  99                    cdq
  004c6f66  b964000000            mov       ecx,64h
  004c6f6b  f7f9                  idiv      ecx
  004c6f6d  83fa32                cmp       edx,32h
  004c6f70  0f8da3000000          jge       near 004C7019h
  004c6f76  8b7c2418              mov       edi,[esp+18h]
  004c6f7a  8b542410              mov       edx,[esp+10h]
  004c6f7e  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  004c6f85  8d0cfd00020000        lea       ecx,[edi*8+200h]
  004c6f8c  25ff0f0000            and       eax,0FFFh
  004c6f91  3bc1                  cmp       eax,ecx
  004c6f93  7f3d                  jg        short 004C6FD2h
  004c6f95  e8a6010800            call      00547140h
  004c6f9a  99                    cdq
  004c6f9b  8d4c3f01              lea       ecx,[edi+edi+1]
  004c6f9f  f7f9                  idiv      ecx
  004c6fa1  8b44241c              mov       eax,[esp+1Ch]
  004c6fa5  83c264                add       edx,64h
  004c6fa8  3bc2                  cmp       eax,edx
  004c6faa  7d26                  jge       short 004C6FD2h
  004c6fac  8b16                  mov       edx,[esi]
  004c6fae  c7461803000000        mov       dword [esi+18h],3
  004c6fb5  81e2fffffffe          and       edx,0FEFFFFFFh
  004c6fbb  c6464301              mov       byte [esi+43h],1
  004c6fbf  83ca02                or        edx,2
  004c6fc2  895e5c                mov       [esi+5Ch],ebx
  004c6fc5  8916                  mov       [esi],edx
  004c6fc7  5f                    pop       edi
  004c6fc8  5e                    pop       esi
  004c6fc9  5d                    pop       ebp
  004c6fca  5b                    pop       ebx
  004c6fcb  81c484000000          add       esp,84h
  004c6fd1  c3                    ret

LAB_004c6fd2:
  004c6fd2  8b442410              mov       eax,[esp+10h]
  004c6fd6  8d14fd00040000        lea       edx,[edi*8+400h]
  004c6fdd  8d8cb800020000        lea       ecx,[eax+edi*4+200h]
  004c6fe4  81e1ff0f0000          and       ecx,0FFFh
  004c6fea  3bca                  cmp       ecx,edx
  004c6fec  0f8fb5010000          jg        near 004C71A7h
  004c6ff2  8b06                  mov       eax,[esi]
  004c6ff4  c6464207              mov       byte [esi+42h],7
  004c6ff8  25fffffffe            and       eax,0FEFFFFFFh
  004c6ffd  c6464301              mov       byte [esi+43h],1
  004c7001  8906                  mov       [esi],eax
  004c7003  c7461803000000        mov       dword [esi+18h],3
  004c700a  66895e12              mov       [esi+12h],bx
  004c700e  5f                    pop       edi
  004c700f  5e                    pop       esi
  004c7010  5d                    pop       ebp
  004c7011  5b                    pop       ebx
  004c7012  81c484000000          add       esp,84h
  004c7018  c3                    ret

LAB_004c7019:
  004c7019  83fa46                cmp       edx,46h
  004c701c  0f8c0b010000          jl        near 004C712Dh
  004c7022  83fa50                cmp       edx,50h
  004c7025  0f8dc7010000          jge       near 004C71F2h
  004c702b  c6464203              mov       byte [esi+42h],3
  004c702f  e80c010800            call      00547140h
  004c7034  99                    cdq
  004c7035  b90a000000            mov       ecx,0Ah
  004c703a  f7f9                  idiv      ecx
  004c703c  8b442418              mov       eax,[esp+18h]
  004c7040  8b4c2414              mov       ecx,[esp+14h]
  004c7044  d1f8                  sar       eax,1
  004c7046  8db980000000          lea       edi,[ecx+80h]
  004c704c  c1e704                shl       edi,4
  004c704f  02d0                  add       dl,al
  004c7051  80c214                add       dl,14h
  004c7054  885643                mov       [esi+43h],dl
  004c7057  e8e4000800            call      00547140h
  004c705c  99                    cdq
  004c705d  f7ff                  idiv      edi
  004c705f  d1ff                  sar       edi,1
  004c7061  2bd7                  sub       edx,edi
  004c7063  8b7c2410              mov       edi,[esp+10h]
  004c7067  03d7                  add       edx,edi
  004c7069  66035632              add       dx,[esi+32h]
  004c706d  81c200080000          add       edx,800h
  004c7073  66895610              mov       [esi+10h],dx
  004c7077  5f                    pop       edi
  004c7078  5e                    pop       esi
  004c7079  5d                    pop       ebp
  004c707a  5b                    pop       ebx
  004c707b  81c484000000          add       esp,84h
  004c7081  c3                    ret

LAB_004c7082:
  004c7082  3d58020000            cmp       eax,258h
  004c7087  0f8dd0000000          jge       near 004C715Dh
  004c708d  e8ae000800            call      00547140h
  004c7092  99                    cdq
  004c7093  b964000000            mov       ecx,64h
  004c7098  f7f9                  idiv      ecx
  004c709a  83fa1e                cmp       edx,1Eh
  004c709d  7d2f                  jge       short 004C70CEh
  004c709f  8b542410              mov       edx,[esp+10h]
  004c70a3  81c200040000          add       edx,400h
  004c70a9  81e2ff0f0000          and       edx,0FFFh
  004c70af  81fa00080000          cmp       edx,800h
  004c70b5  0f8cb4000000          jl        near 004C716Fh
  004c70bb  c6464201              mov       byte [esi+42h],1
  004c70bf  c6464301              mov       byte [esi+43h],1
  004c70c3  5f                    pop       edi
  004c70c4  5e                    pop       esi
  004c70c5  5d                    pop       ebp
  004c70c6  5b                    pop       ebx
  004c70c7  81c484000000          add       esp,84h
  004c70cd  c3                    ret

LAB_004c70ce:
  004c70ce  83fa32                cmp       edx,32h
  004c70d1  7d51                  jge       short 004C7124h
  004c70d3  c6464203              mov       byte [esi+42h],3
  004c70d7  e864000800            call      00547140h
  004c70dc  99                    cdq
  004c70dd  b90a000000            mov       ecx,0Ah
  004c70e2  f7f9                  idiv      ecx
  004c70e4  8b442418              mov       eax,[esp+18h]
  004c70e8  8b4c2414              mov       ecx,[esp+14h]
  004c70ec  d1f8                  sar       eax,1
  004c70ee  8db980000000          lea       edi,[ecx+80h]
  004c70f4  c1e704                shl       edi,4
  004c70f7  02d0                  add       dl,al
  004c70f9  80c214                add       dl,14h
  004c70fc  885643                mov       [esi+43h],dl
  004c70ff  e83c000800            call      00547140h
  004c7104  99                    cdq
  004c7105  f7ff                  idiv      edi
  004c7107  8b4c2410              mov       ecx,[esp+10h]
  004c710b  d1ff                  sar       edi,1
  004c710d  2bd7                  sub       edx,edi
  004c710f  03d1                  add       edx,ecx
  004c7111  66035632              add       dx,[esi+32h]
  004c7115  66895610              mov       [esi+10h],dx
  004c7119  5f                    pop       edi
  004c711a  5e                    pop       esi
  004c711b  5d                    pop       ebp
  004c711c  5b                    pop       ebx
  004c711d  81c484000000          add       esp,84h
  004c7123  c3                    ret

LAB_004c7124:
  004c7124  83fa5a                cmp       edx,5Ah
  004c7127  0f8dc5000000          jge       near 004C71F2h

LAB_004c712d:
  004c712d  c6464202              mov       byte [esi+42h],2
  004c7131  e80a000800            call      00547140h
  004c7136  99                    cdq
  004c7137  b90a000000            mov       ecx,0Ah
  004c713c  66c746106400          mov       word [esi+10h],64h
  004c7142  f7f9                  idiv      ecx
  004c7144  8b442418              mov       eax,[esp+18h]
  004c7148  d1f8                  sar       eax,1
  004c714a  02d0                  add       dl,al
  004c714c  80c214                add       dl,14h
  004c714f  885643                mov       [esi+43h],dl
  004c7152  5f                    pop       edi
  004c7153  5e                    pop       esi
  004c7154  5d                    pop       ebp
  004c7155  5b                    pop       ebx
  004c7156  81c484000000          add       esp,84h
  004c715c  c3                    ret

LAB_004c715d:
  004c715d  e8deff0700            call      00547140h
  004c7162  99                    cdq
  004c7163  b964000000            mov       ecx,64h
  004c7168  f7f9                  idiv      ecx
  004c716a  83fa0a                cmp       edx,0Ah
  004c716d  7d33                  jge       short 004C71A2h

LAB_004c716f:
  004c716f  885e42                mov       [esi+42h],bl
  004c7172  e8c9ff0700            call      00547140h
  004c7177  99                    cdq
  004c7178  b90a000000            mov       ecx,0Ah
  004c717d  895e18                mov       [esi+18h],ebx
  004c7180  f7f9                  idiv      ecx
  004c7182  8b442418              mov       eax,[esp+18h]
  004c7186  c1f802                sar       eax,2
  004c7189  02d0                  add       dl,al
  004c718b  8b06                  mov       eax,[esi]
  004c718d  0d00000001            or        eax,1000000h
  004c7192  885643                mov       [esi+43h],dl
  004c7195  8906                  mov       [esi],eax
  004c7197  5f                    pop       edi
  004c7198  5e                    pop       esi
  004c7199  5d                    pop       ebp
  004c719a  5b                    pop       ebx
  004c719b  81c484000000          add       esp,84h
  004c71a1  c3                    ret

LAB_004c71a2:
  004c71a2  83fa32                cmp       edx,32h
  004c71a5  7d13                  jge       short 004C71BAh

LAB_004c71a7:
  004c71a7  c6464201              mov       byte [esi+42h],1
  004c71ab  c6464301              mov       byte [esi+43h],1
  004c71af  5f                    pop       edi
  004c71b0  5e                    pop       esi
  004c71b1  5d                    pop       ebp
  004c71b2  5b                    pop       ebx
  004c71b3  81c484000000          add       esp,84h
  004c71b9  c3                    ret

LAB_004c71ba:
  004c71ba  83fa5a                cmp       edx,5Ah
  004c71bd  7d33                  jge       short 004C71F2h
  004c71bf  c6464203              mov       byte [esi+42h],3
  004c71c3  e878ff0700            call      00547140h
  004c71c8  99                    cdq
  004c71c9  b90a000000            mov       ecx,0Ah
  004c71ce  f7f9                  idiv      ecx
  004c71d0  8b442418              mov       eax,[esp+18h]
  004c71d4  d1f8                  sar       eax,1
  004c71d6  02d0                  add       dl,al
  004c71d8  80c214                add       dl,14h
  004c71db  885643                mov       [esi+43h],dl
  004c71de  e85dff0700            call      00547140h
  004c71e3  66894610              mov       [esi+10h],ax
  004c71e7  5f                    pop       edi
  004c71e8  5e                    pop       esi
  004c71e9  5d                    pop       ebp
  004c71ea  5b                    pop       ebx
  004c71eb  81c484000000          add       esp,84h
  004c71f1  c3                    ret

LAB_004c71f2:
  004c71f2  c6464206              mov       byte [esi+42h],6
  004c71f6  c6464301              mov       byte [esi+43h],1
  004c71fa  e841ff0700            call      00547140h
  004c71ff  66894610              mov       [esi+10h],ax

LAB_004c7203:
  004c7203  5f                    pop       edi
  004c7204  5e                    pop       esi
  004c7205  5d                    pop       ebp
  004c7206  5b                    pop       ebx
  004c7207  81c484000000          add       esp,84h
  004c720d  c3                    ret
  004c720e  8bff                  mov       edi,edi
  004c7210  21694c                and       [ecx+4Ch],ebp
  004c7213  004869                add       [eax+69h],cl
  004c7216  4c                    dec       esp
  004c7217  00a9694c002d          add       [ecx+2D004C69h],ch
  004c721d  6a4c                  push      4Ch
  004c721f  00bf6c4c00bf          add       [edi-40FFB394h],bh
  004c7225  6c                    insb
  004c7226  4c                    dec       esp
  004c7227  00ed                  add       ch,ch
  004c7229  6a4c                  push      4Ch
  004c722b  00606b                add       [eax+6Bh],ah
  004c722e  4c                    dec       esp
  004c722f  00716c                add       [ecx+6Ch],dh
  004c7232  4c                    dec       esp
  004c7233  009090909090          add       [eax-6F6F6F70h],dl
  004c7239  90                    nop
  004c723a  90                    nop
  004c723b  90                    nop
  004c723c  90                    nop
  004c723d  90                    nop
  004c723e  90                    nop
  004c723f  90                    nop

57 cible(s) de saut interne : 0x004c6750, 0x004c6854, 0x004c6877, 0x004c688d, 0x004c68da, 0x004c68e2, 0x004c68fb, 0x004c690c, 0x004c6934, 0x004c696c, 0x004c6980, 0x004c6987, 0x004c69a1, 0x004c69bc, 0x004c69c8, 0x004c69de, 0x004c6a0b, 0x004c6a15, 0x004c6a19, 0x004c6a39, 0x004c6a4f, 0x004c6ad2, 0x004c6b6c, 0x004c6b8a, 0x004c6b9a, 0x004c6be1, 0x004c6c0b, 0x004c6c15, 0x004c6c19, 0x004c6c43, 0x004c6c53, 0x004c6c8f, 0x004c6c9b, 0x004c6cbf, 0x004c6d86, 0x004c6d9e, 0x004c6da0, 0x004c6db8, 0x004c6dbf, 0x004c6dfd, 0x004c6e9d, 0x004c6ead, 0x004c6ed5, 0x004c6f03, 0x004c6fd2, 0x004c7019, 0x004c7082, 0x004c70ce, 0x004c7124, 0x004c712d, 0x004c715d, 0x004c716f, 0x004c71a2, 0x004c71a7, 0x004c71ba, 0x004c71f2, 0x004c7203
