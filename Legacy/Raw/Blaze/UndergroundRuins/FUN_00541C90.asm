Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00541c90  section .text  offset fichier 0x141090  1184 octets

  00541c90  a02ce55d00            mov       al,[5DE52Ch]
  00541c95  56                    push      esi
  00541c96  3c02                  cmp       al,2
  00541c98  57                    push      edi
  00541c99  0f8514010000          jne       near 00541DB3h
  00541c9f  a1b4876000            mov       eax,[6087B4h]
  00541ca4  33ff                  xor       edi,edi
  00541ca6  8b30                  mov       esi,[eax]
  00541ca8  a120905e00            mov       eax,[5E9020h]
  00541cad  81c600000f00          add       esi,0F0000h
  00541cb3  85c0                  test      eax,eax
  00541cb5  0f8ef8000000          jle       near 00541DB3h
  00541cbb  55                    push      ebp
  00541cbc  53                    push      ebx
  00541cbd  33db                  xor       ebx,ebx

LAB_00541cbf:
  00541cbf  56                    push      esi
  00541cc0  e8eb6dffff            call      00538AB0h
  00541cc5  83c404                add       esp,4
  00541cc8  56                    push      esi
  00541cc9  e8826fffff            call      00538C50h
  00541cce  83c404                add       esp,4
  00541cd1  56                    push      esi
  00541cd2  e84971ffff            call      00538E20h
  00541cd7  8b15cc975e00          mov       edx,[5E97CCh]
  00541cdd  33c0                  xor       eax,eax
  00541cdf  8a8650010000          mov       al,[esi+150h]
  00541ce5  83c404                add       esp,4
  00541ce8  8b8a84000000          mov       ecx,[edx+84h]
  00541cee  8b5210                mov       edx,[edx+10h]
  00541cf1  81c600200000          add       esi,2000h
  00541cf7  8b2cc1                mov       ebp,[ecx+eax*8]
  00541cfa  896cfa20              mov       [edx+edi*8+20h],ebp
  00541cfe  8b15cc975e00          mov       edx,[5E97CCh]
  00541d04  8bacc180000000        mov       ebp,[ecx+eax*8+80h]
  00541d0b  8b522c                mov       edx,[edx+2Ch]
  00541d0e  892cfa                mov       [edx+edi*8],ebp
  00541d11  8b15cc975e00          mov       edx,[5E97CCh]
  00541d17  8b84c100010000        mov       eax,[ecx+eax*8+100h]
  00541d1e  8b527c                mov       edx,[edx+7Ch]
  00541d21  8904ba                mov       [edx+edi*4],eax
  00541d24  8b0dcc975e00          mov       ecx,[5E97CCh]
  00541d2a  668b9644e1ffff        mov       dx,[esi-1EBCh]
  00541d31  8b8198000000          mov       eax,[ecx+98h]
  00541d37  6689541810            mov       [eax+ebx+10h],dx
  00541d3c  668b8e48e1ffff        mov       cx,[esi-1EB8h]
  00541d43  03c3                  add       eax,ebx
  00541d45  47                    inc       edi
  00541d46  83c360                add       ebx,60h
  00541d49  66894814              mov       [eax+14h],cx
  00541d4d  668b964ae1ffff        mov       dx,[esi-1EB6h]
  00541d54  66895016              mov       [eax+16h],dx
  00541d58  668b8e2ee1ffff        mov       cx,[esi-1ED2h]
  00541d5f  6689481a              mov       [eax+1Ah],cx
  00541d63  668b962ce1ffff        mov       dx,[esi-1ED4h]
  00541d6a  66895018              mov       [eax+18h],dx
  00541d6e  8b8e30e1ffff          mov       ecx,[esi-1ED0h]
  00541d74  894830                mov       [eax+30h],ecx
  00541d77  8b9634e1ffff          mov       edx,[esi-1ECCh]
  00541d7d  895034                mov       [eax+34h],edx
  00541d80  8b8e38e1ffff          mov       ecx,[esi-1EC8h]
  00541d86  894838                mov       [eax+38h],ecx
  00541d89  8b963ce1ffff          mov       edx,[esi-1EC4h]
  00541d8f  89503c                mov       [eax+3Ch],edx
  00541d92  8b8e18e1ffff          mov       ecx,[esi-1EE8h]
  00541d98  894828                mov       [eax+28h],ecx
  00541d9b  8b961ce1ffff          mov       edx,[esi-1EE4h]
  00541da1  89502c                mov       [eax+2Ch],edx
  00541da4  a120905e00            mov       eax,[5E9020h]
  00541da9  3bf8                  cmp       edi,eax
  00541dab  0f8c0effffff          jl        near 00541CBFh
  00541db1  5b                    pop       ebx
  00541db2  5d                    pop       ebp

LAB_00541db3:
  00541db3  5f                    pop       edi
  00541db4  5e                    pop       esi
  00541db5  c3                    ret
  00541db6  90                    nop
  00541db7  90                    nop
  00541db8  90                    nop
  00541db9  90                    nop
  00541dba  90                    nop
  00541dbb  90                    nop
  00541dbc  90                    nop
  00541dbd  90                    nop
  00541dbe  90                    nop
  00541dbf  90                    nop
  00541dc0  e90b000000            jmp       00541DD0h
  00541dc5  90                    nop
  00541dc6  90                    nop
  00541dc7  90                    nop
  00541dc8  90                    nop
  00541dc9  90                    nop
  00541dca  90                    nop
  00541dcb  90                    nop
  00541dcc  90                    nop
  00541dcd  90                    nop
  00541dce  90                    nop
  00541dcf  90                    nop

LAB_00541dd0:
  00541dd0  83ec2c                sub       esp,2Ch
  00541dd3  8d442410              lea       eax,[esp+10h]
  00541dd7  8d4c2428              lea       ecx,[esp+28h]
  00541ddb  8d54241c              lea       edx,[esp+1Ch]
  00541ddf  53                    push      ebx
  00541de0  55                    push      ebp
  00541de1  56                    push      esi
  00541de2  57                    push      edi
  00541de3  50                    push      eax
  00541de4  51                    push      ecx
  00541de5  52                    push      edx
  00541de6  e8f583feff            call      0052A1E0h
  00541deb  83c40c                add       esp,0Ch
  00541dee  33f6                  xor       esi,esi
  00541df0  c60598985e0001        mov       byte [5E9898h],1
  00541df7  c60508995e0001        mov       byte [5E9908h],1
  00541dfe  33c9                  xor       ecx,ecx
  00541e00  b8dc935e00            mov       eax,5E93DCh

LAB_00541e05:
  00541e05  ba10000000            mov       edx,10h
  00541e0a  8848ec                mov       [eax-14h],cl
  00541e0d  c70080000000          mov       dword [eax],80h
  00541e13  c7400880000000        mov       dword [eax+8],80h
  00541e1a  66897016              mov       [eax+16h],si
  00541e1e  668950f4              mov       [eax-0Ch],dx
  00541e22  668950f6              mov       [eax-0Ah],dx
  00541e26  668950f8              mov       [eax-8],dx
  00541e2a  668950fa              mov       [eax-6],dx
  00541e2e  41                    inc       ecx
  00541e2f  059c000000            add       eax,9Ch
  00541e34  83f902                cmp       ecx,2
  00541e37  7ccc                  jl        short 00541E05h

LAB_00541e39:
  00541e39  b8d4935e00            mov       eax,5E93D4h
  00541e3e  c744242800000000      mov       dword [esp+28h],0
  00541e46  8944241c              mov       [esp+1Ch],eax
  00541e4a  c744241888ff5d00      mov       dword [esp+18h],5DFF88h
  00541e52  c7442414c8935e00      mov       dword [esp+14h],5E93C8h
  00541e5a  eb04                  jmp       short 00541E60h

LAB_00541e5c:
  00541e5c  8b44241c              mov       eax,[esp+1Ch]

LAB_00541e60:
  00541e60  8b542414              mov       edx,[esp+14h]
  00541e64  33c9                  xor       ecx,ecx
  00541e66  8a0a                  mov       cl,[edx]
  00541e68  8d14c9                lea       edx,[ecx+ecx*8]
  00541e6b  8d0c51                lea       ecx,[ecx+edx*2]
  00541e6e  33d2                  xor       edx,edx
  00541e70  8a148d88915e00        mov       dl,[ecx*4+5E9188h]
  00541e77  8bca                  mov       ecx,edx
  00541e79  c1e104                shl       ecx,4
  00541e7c  81c1204a6000          add       ecx,604A20h
  00541e82  f60110                test      byte [ecx],10h
  00541e85  7403                  je        short 00541E8Ah
  00541e87  ff4810                dec       dword [eax+10h]

LAB_00541e8a:
  00541e8a  f60140                test      byte [ecx],40h
  00541e8d  7403                  je        short 00541E92h
  00541e8f  ff4010                inc       dword [eax+10h]

LAB_00541e92:
  00541e92  f60180                test      byte [ecx],80h
  00541e95  7403                  je        short 00541E9Ah
  00541e97  ff4808                dec       dword [eax+8]

LAB_00541e9a:
  00541e9a  f60120                test      byte [ecx],20h
  00541e9d  7403                  je        short 00541EA2h
  00541e9f  ff4008                inc       dword [eax+8]

LAB_00541ea2:
  00541ea2  668b11                mov       dx,[ecx]
  00541ea5  f6c201                test      dl,1
  00541ea8  744f                  je        short 00541EF9h
  00541eaa  668b70fc              mov       si,[eax-4]
  00541eae  6685f6                test      si,si
  00541eb1  740a                  je        short 00541EBDh
  00541eb3  f6c640                test      dh,40h
  00541eb6  7405                  je        short 00541EBDh
  00541eb8  4e                    dec       esi
  00541eb9  668970fc              mov       [eax-4],si

LAB_00541ebd:
  00541ebd  668b50fe              mov       dx,[eax-2]
  00541ec1  6685d2                test      dx,dx
  00541ec4  740b                  je        short 00541ED1h
  00541ec6  f6410180              test      byte [ecx+1],80h
  00541eca  7405                  je        short 00541ED1h
  00541ecc  4a                    dec       edx
  00541ecd  668950fe              mov       [eax-2],dx

LAB_00541ed1:
  00541ed1  668b10                mov       dx,[eax]
  00541ed4  6685d2                test      dx,dx
  00541ed7  740a                  je        short 00541EE3h
  00541ed9  f6410110              test      byte [ecx+1],10h
  00541edd  7404                  je        short 00541EE3h
  00541edf  4a                    dec       edx
  00541ee0  668910                mov       [eax],dx

LAB_00541ee3:
  00541ee3  668b5002              mov       dx,[eax+2]
  00541ee7  6685d2                test      dx,dx
  00541eea  7433                  je        short 00541F1Fh
  00541eec  f6410120              test      byte [ecx+1],20h
  00541ef0  742d                  je        short 00541F1Fh
  00541ef2  4a                    dec       edx
  00541ef3  66895002              mov       [eax+2],dx
  00541ef7  eb26                  jmp       short 00541F1Fh

LAB_00541ef9:
  00541ef9  f6c640                test      dh,40h
  00541efc  7404                  je        short 00541F02h
  00541efe  66ff40fc              inc       word [eax-4]

LAB_00541f02:
  00541f02  f6410180              test      byte [ecx+1],80h
  00541f06  7404                  je        short 00541F0Ch
  00541f08  66ff40fe              inc       word [eax-2]

LAB_00541f0c:
  00541f0c  f6410110              test      byte [ecx+1],10h
  00541f10  7403                  je        short 00541F15h
  00541f12  66ff00                inc       word [eax]

LAB_00541f15:
  00541f15  f6410120              test      byte [ecx+1],20h
  00541f19  7404                  je        short 00541F1Fh
  00541f1b  66ff4002              inc       word [eax+2]

LAB_00541f1f:
  00541f1f  f6410104              test      byte [ecx+1],4
  00541f23  7405                  je        short 00541F2Ah
  00541f25  6683401e80            add       word [eax+1Eh],0FF80h

LAB_00541f2a:
  00541f2a  f6410108              test      byte [ecx+1],8
  00541f2e  7406                  je        short 00541F36h
  00541f30  6681401e8000          add       word [eax+1Eh],80h

LAB_00541f36:
  00541f36  8b4808                mov       ecx,[eax+8]
  00541f39  8b5010                mov       edx,[eax+10h]
  00541f3c  0fbf781e              movsx     edi,word [eax+1Eh]
  00541f40  8b5c2418              mov       ebx,[esp+18h]
  00541f44  83c0fc                add       eax,0FFFFFFFCh
  00541f47  894c2430              mov       [esp+30h],ecx
  00541f4b  89542434              mov       [esp+34h],edx
  00541f4f  89442410              mov       [esp+10h],eax
  00541f53  c744242404000000      mov       dword [esp+24h],4

LAB_00541f5b:
  00541f5b  8b442410              mov       eax,[esp+10h]
  00541f5f  57                    push      edi
  00541f60  0fbf28                movsx     ebp,word [eax]
  00541f63  e8b43a0000            call      00545A1Ch
  00541f68  8bf0                  mov       esi,eax
  00541f6a  57                    push      edi
  00541f6b  0faff5                imul      esi,ebp
  00541f6e  c1fe0c                sar       esi,0Ch
  00541f71  e8a03a0000            call      00545A16h
  00541f76  0fafc5                imul      eax,ebp
  00541f79  8b6c2430              mov       ebp,[esp+30h]
  00541f7d  8b4c2434              mov       ecx,[esp+34h]
  00541f81  c1f80c                sar       eax,0Ch
  00541f84  03f5                  add       esi,ebp
  00541f86  03c8                  add       ecx,eax
  00541f88  8b442424              mov       eax,[esp+24h]
  00541f8c  81e6ffff0000          and       esi,0FFFFh
  00541f92  c1e110                shl       ecx,10h
  00541f95  0bce                  or        ecx,esi
  00541f97  81c700040000          add       edi,400h
  00541f9d  890b                  mov       [ebx],ecx
  00541f9f  8b4c2410              mov       ecx,[esp+10h]
  00541fa3  83c304                add       ebx,4
  00541fa6  83c102                add       ecx,2
  00541fa9  48                    dec       eax
  00541faa  894c2410              mov       [esp+10h],ecx
  00541fae  89442424              mov       [esp+24h],eax
  00541fb2  75a7                  jne       short 00541F5Bh
  00541fb4  8b742428              mov       esi,[esp+28h]
  00541fb8  8b442418              mov       eax,[esp+18h]
  00541fbc  8bd6                  mov       edx,esi
  00541fbe  8b4c2420              mov       ecx,[esp+20h]
  00541fc2  f7da                  neg       edx
  00541fc4  1bd2                  sbb       edx,edx
  00541fc6  81e241ffbe00          and       edx,0BEFF41h
  00541fcc  81c2ff804000          add       edx,4080FFh
  00541fd2  52                    push      edx
  00541fd3  8b542430              mov       edx,[esp+30h]
  00541fd7  50                    push      eax
  00541fd8  51                    push      ecx
  00541fd9  52                    push      edx
  00541fda  e8d1000000            call      005420B0h
  00541fdf  8b54242c              mov       edx,[esp+2Ch]
  00541fe3  89442430              mov       [esp+30h],eax
  00541fe7  8b442424              mov       eax,[esp+24h]
  00541feb  83c410                add       esp,10h
  00541fee  059c000000            add       eax,9Ch
  00541ff3  46                    inc       esi
  00541ff4  81c29c000000          add       edx,9Ch
  00541ffa  3d00955e00            cmp       eax,5E9500h
  00541fff  89742428              mov       [esp+28h],esi
  00542003  89442414              mov       [esp+14h],eax
  00542007  895c2418              mov       [esp+18h],ebx
  0054200b  8954241c              mov       [esp+1Ch],edx
  0054200f  0f8c47feffff          jl        near 00541E5Ch
  00542015  6898ff5d00            push      5DFF98h
  0054201a  6888ff5d00            push      5DFF88h
  0054201f  e8cc38ffff            call      005358F0h
  00542024  83c408                add       esp,8
  00542027  8bf0                  mov       esi,eax
  00542029  6888ff5d00            push      5DFF88h
  0054202e  6898ff5d00            push      5DFF98h
  00542033  e8b838ffff            call      005358F0h
  00542038  83c408                add       esp,8
  0054203b  8bf8                  mov       edi,eax
  0054203d  56                    push      esi
  0054203e  e8fd39ffff            call      00535A40h
  00542043  83c404                add       esp,4
  00542046  8bd8                  mov       ebx,eax
  00542048  57                    push      edi
  00542049  e8f239ffff            call      00535A40h
  0054204e  83c404                add       esp,4
  00542051  8be8                  mov       ebp,eax
  00542053  53                    push      ebx
  00542054  56                    push      esi
  00542055  6844dd5600            push      56DD44h
  0054205a  e8fd3b0000            call      00545C5Ch
  0054205f  83c40c                add       esp,0Ch
  00542062  55                    push      ebp
  00542063  57                    push      edi
  00542064  6844dd5600            push      56DD44h
  00542069  e8ee3b0000            call      00545C5Ch
  0054206e  03eb                  add       ebp,ebx
  00542070  83c40c                add       esp,0Ch
  00542073  85ed                  test      ebp,ebp
  00542075  7e0f                  jle       short 00542086h
  00542077  6a00                  push      0
  00542079  683cdd5600            push      56DD3Ch
  0054207e  e8d93b0000            call      00545C5Ch
  00542083  83c408                add       esp,8

LAB_00542086:
  00542086  6a01                  push      1
  00542088  8d442424              lea       eax,[esp+24h]
  0054208c  6a00                  push      0
  0054208e  8d4c2440              lea       ecx,[esp+40h]
  00542092  50                    push      eax
  00542093  8d542438              lea       edx,[esp+38h]
  00542097  51                    push      ecx
  00542098  52                    push      edx
  00542099  e8e281feff            call      0052A280h
  0054209e  83c414                add       esp,14h
  005420a1  e993fdffff            jmp       00541E39h
  005420a6  90                    nop
  005420a7  90                    nop
  005420a8  90                    nop
  005420a9  90                    nop
  005420aa  90                    nop
  005420ab  90                    nop
  005420ac  90                    nop
  005420ad  90                    nop
  005420ae  90                    nop
  005420af  90                    nop
  005420b0  8b442408              mov       eax,[esp+8]
  005420b4  8b4c240c              mov       ecx,[esp+0Ch]
  005420b8  c74004ff000041        mov       dword [eax+4],410000FFh
  005420bf  8b11                  mov       edx,[ecx]
  005420c1  895008                mov       [eax+8],edx
  005420c4  8b5104                mov       edx,[ecx+4]
  005420c7  89500c                mov       [eax+0Ch],edx
  005420ca  c7401000ff0041        mov       dword [eax+10h],4100FF00h
  005420d1  8b5104                mov       edx,[ecx+4]
  005420d4  895014                mov       [eax+14h],edx
  005420d7  8b5108                mov       edx,[ecx+8]
  005420da  895018                mov       [eax+18h],edx
  005420dd  c7401c0000ff41        mov       dword [eax+1Ch],41FF0000h
  005420e4  8b5108                mov       edx,[ecx+8]
  005420e7  895020                mov       [eax+20h],edx
  005420ea  8b510c                mov       edx,[ecx+0Ch]
  005420ed  895024                mov       [eax+24h],edx
  005420f0  c74028ffffff41        mov       dword [eax+28h],41FFFFFFh
  005420f7  8b510c                mov       edx,[ecx+0Ch]
  005420fa  89502c                mov       [eax+2Ch],edx
  005420fd  8b09                  mov       ecx,[ecx]
  005420ff  894830                mov       [eax+30h],ecx
  00542102  8b4c2404              mov       ecx,[esp+4]
  00542106  8b11                  mov       edx,[ecx]
  00542108  81ca0000000c          or        edx,0C000000h
  0054210e  8910                  mov       [eax],edx
  00542110  8901                  mov       [ecx],eax
  00542112  83c034                add       eax,34h
  00542115  c3                    ret
  00542116  90                    nop
  00542117  90                    nop
  00542118  90                    nop
  00542119  90                    nop
  0054211a  90                    nop
  0054211b  90                    nop
  0054211c  90                    nop
  0054211d  90                    nop
  0054211e  90                    nop
  0054211f  90                    nop
  00542120  e83b000000            call      00542160h
  00542125  e85679feff            call      00529A80h
  0054212a  e921010000            jmp       00542250h
  0054212f  90                    nop

23 cible(s) de saut interne : 0x00541cbf, 0x00541db3, 0x00541dd0, 0x00541e05, 0x00541e39, 0x00541e5c, 0x00541e60, 0x00541e8a, 0x00541e92, 0x00541e9a, 0x00541ea2, 0x00541ebd, 0x00541ed1, 0x00541ee3, 0x00541ef9, 0x00541f02, 0x00541f0c, 0x00541f15, 0x00541f1f, 0x00541f2a, 0x00541f36, 0x00541f5b, 0x00542086
