Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0048ad80  section .text  offset fichier 0x8a180  1968 octets

  0048ad80  83ec2c                sub       esp,2Ch
  0048ad83  b832000000            mov       eax,32h
  0048ad88  53                    push      ebx
  0048ad89  55                    push      ebp
  0048ad8a  668944242a            mov       [esp+2Ah],ax
  0048ad8f  668944242e            mov       [esp+2Eh],ax
  0048ad94  56                    push      esi
  0048ad95  8b74243c              mov       esi,[esp+3Ch]
  0048ad99  8d442420              lea       eax,[esp+20h]
  0048ad9d  57                    push      edi
  0048ad9e  8d4c2428              lea       ecx,[esp+28h]
  0048ada2  50                    push      eax
  0048ada3  8d542430              lea       edx,[esp+30h]
  0048ada7  51                    push      ecx
  0048ada8  33ff                  xor       edi,edi
  0048adaa  52                    push      edx
  0048adab  56                    push      esi
  0048adac  66897c2440            mov       [esp+40h],di
  0048adb1  66c74424442300        mov       word [esp+44h],23h
  0048adb8  66897c2448            mov       [esp+48h],di
  0048adbd  66c744244a5a00        mov       word [esp+4Ah],5Ah
  0048adc4  e8b7db0a00            call      00538980h
  0048adc9  83c410                add       esp,10h
  0048adcc  8d442420              lea       eax,[esp+20h]
  0048add0  8d4c241c              lea       ecx,[esp+1Ch]
  0048add4  8d542414              lea       edx,[esp+14h]
  0048add8  50                    push      eax
  0048add9  51                    push      ecx
  0048adda  8d442420              lea       eax,[esp+20h]
  0048adde  52                    push      edx
  0048addf  8d4c244c              lea       ecx,[esp+4Ch]
  0048ade3  50                    push      eax
  0048ade4  51                    push      ecx
  0048ade5  56                    push      esi
  0048ade6  e835b10a00            call      00535F20h
  0048adeb  8b4c2458              mov       ecx,[esp+58h]
  0048adef  b840000000            mov       eax,40h
  0048adf4  2bc1                  sub       eax,ecx
  0048adf6  83c418                add       esp,18h
  0048adf9  89442410              mov       [esp+10h],eax
  0048adfd  8b44241c              mov       eax,[esp+1Ch]
  0048ae01  3bc7                  cmp       eax,edi
  0048ae03  0f841f030000          je        near 0048B128h
  0048ae09  56                    push      esi
  0048ae0a  e891070500            call      004DB5A0h
  0048ae0f  8b06                  mov       eax,[esi]
  0048ae11  83c404                add       esp,4
  0048ae14  a802                  test      al,2
  0048ae16  0f8449010000          je        near 0048AF65h
  0048ae1c  8a4c2420              mov       cl,[esp+20h]
  0048ae20  bb10000000            mov       ebx,10h
  0048ae25  84cb                  test      bl,cl
  0048ae27  7415                  je        short 0048AE3Eh
  0048ae29  24fd                  and       al,0FDh
  0048ae2b  c6464300              mov       byte [esi+43h],0
  0048ae2f  8906                  mov       [esi],eax
  0048ae31  8a4640                mov       al,[esi+40h]
  0048ae34  24fe                  and       al,0FEh
  0048ae36  884640                mov       [esi+40h],al
  0048ae39  e9ea020000            jmp       0048B128h

LAB_0048ae3e:
  0048ae3e  8a4640                mov       al,[esi+40h]
  0048ae41  33ff                  xor       edi,edi
  0048ae43  a801                  test      al,1
  0048ae45  7416                  je        short 0048AE5Dh
  0048ae47  66ff4e14              dec       word [esi+14h]
  0048ae4b  66397e14              cmp       [esi+14h],di
  0048ae4f  0f85aa000000          jne       near 0048AEFFh
  0048ae55  8b7e6c                mov       edi,[esi+6Ch]
  0048ae58  e9a2000000            jmp       0048AEFFh

LAB_0048ae5d:
  0048ae5d  66837e1a09            cmp       word [esi+1Ah],9
  0048ae62  0f8c97000000          jl        near 0048AEFFh
  0048ae68  8d542430              lea       edx,[esp+30h]
  0048ae6c  8d6e1c                lea       ebp,[esi+1Ch]
  0048ae6f  6a00                  push      0
  0048ae71  52                    push      edx
  0048ae72  55                    push      ebp
  0048ae73  56                    push      esi
  0048ae74  e8b7ed0800            call      00519C30h
  0048ae79  8bf8                  mov       edi,eax
  0048ae7b  83c410                add       esp,10h
  0048ae7e  85ff                  test      edi,edi
  0048ae80  0f84bf000000          je        near 0048AF45h
  0048ae86  8b4500                mov       eax,[ebp]
  0048ae89  8b4f1c                mov       ecx,[edi+1Ch]
  0048ae8c  2bc1                  sub       eax,ecx
  0048ae8e  8b4e20                mov       ecx,[esi+20h]
  0048ae91  8b5624                mov       edx,[esi+24h]
  0048ae94  894644                mov       [esi+44h],eax
  0048ae97  2b4f20                sub       ecx,[edi+20h]
  0048ae9a  894e48                mov       [esi+48h],ecx
  0048ae9d  8b6f24                mov       ebp,[edi+24h]
  0048aea0  2bd5                  sub       edx,ebp
  0048aea2  89564c                mov       [esi+4Ch],edx
  0048aea5  e896c20b00            call      00547140h
  0048aeaa  8b4c2440              mov       ecx,[esp+40h]
  0048aeae  83e007                and       eax,7
  0048aeb1  c1f902                sar       ecx,2
  0048aeb4  8d540808              lea       edx,[eax+ecx+8]
  0048aeb8  66895610              mov       [esi+10h],dx
  0048aebc  8a5640                mov       dl,[esi+40h]
  0048aebf  80ca01                or        dl,1
  0048aec2  885640                mov       [esi+40h],dl
  0048aec5  8b0f                  mov       ecx,[edi]
  0048aec7  83c940                or        ecx,40h
  0048aeca  66899f96000000        mov       [edi+96h],bx
  0048aed1  890f                  mov       [edi],ecx
  0048aed3  66837e1600            cmp       word [esi+16h],0
  0048aed8  897e6c                mov       [esi+6Ch],edi
  0048aedb  7422                  je        short 0048AEFFh
  0048aedd  e85ec20b00            call      00547140h
  0048aee2  99                    cdq
  0048aee3  b964000000            mov       ecx,64h
  0048aee8  f7f9                  idiv      ecx
  0048aeea  83fa1e                cmp       edx,1Eh
  0048aeed  7d10                  jge       short 0048AEFFh
  0048aeef  6a00                  push      0
  0048aef1  57                    push      edi
  0048aef2  56                    push      esi
  0048aef3  c6460707              mov       byte [esi+7],7
  0048aef7  e824150b00            call      0053C420h
  0048aefc  83c40c                add       esp,0Ch

LAB_0048aeff:
  0048aeff  85ff                  test      edi,edi
  0048af01  7442                  je        short 0048AF45h
  0048af03  57                    push      edi
  0048af04  56                    push      esi
  0048af05  e876ec0a00            call      00539B80h
  0048af0a  83c408                add       esp,8
  0048af0d  8d571c                lea       edx,[edi+1Ch]
  0048af10  52                    push      edx
  0048af11  6800180000            push      1800h
  0048af16  6800030000            push      300h
  0048af1b  6a03                  push      3
  0048af1d  e8fe2b0b00            call      0053DB20h
  0048af22  8b07                  mov       eax,[edi]
  0048af24  83c410                add       esp,10h
  0048af27  a900000040            test      eax,40000000h
  0048af2c  7411                  je        short 0048AF3Fh
  0048af2e  8a4e40                mov       cl,[esi+40h]
  0048af31  8b06                  mov       eax,[esi]
  0048af33  80e1fe                and       cl,0FEh
  0048af36  24fd                  and       al,0FDh
  0048af38  884e40                mov       [esi+40h],cl
  0048af3b  8906                  mov       [esi],eax
  0048af3d  eb06                  jmp       short 0048AF45h

LAB_0048af3f:
  0048af3f  66c746141e00          mov       word [esi+14h],1Eh

LAB_0048af45:
  0048af45  8b06                  mov       eax,[esi]
  0048af47  a900000002            test      eax,2000000h
  0048af4c  0f84d6010000          je        near 0048B128h
  0048af52  f6464001              test      byte [esi+40h],1
  0048af56  0f85cc010000          jne       near 0048B128h
  0048af5c  24fd                  and       al,0FDh
  0048af5e  8906                  mov       [esi],eax
  0048af60  e9c3010000            jmp       0048B128h

LAB_0048af65:
  0048af65  a811                  test      al,11h
  0048af67  0f85bb010000          jne       near 0048B128h
  0048af6d  a904080000            test      eax,804h
  0048af72  0f85b0010000          jne       near 0048B128h
  0048af78  33c9                  xor       ecx,ecx
  0048af7a  8a4e42                mov       cl,[esi+42h]
  0048af7d  83f903                cmp       ecx,3
  0048af80  0f87a2010000          ja        near 0048B128h
  0048af86  ff248d14b54800        jmp       dword [ecx*4+48B514h]
  0048af8d  33c9                  xor       ecx,ecx
  0048af8f  66394e18              cmp       [esi+18h],cx
  0048af93  740a                  je        short 0048AF9Fh
  0048af95  0d00000001            or        eax,1000000h
  0048af9a  894e18                mov       [esi+18h],ecx
  0048af9d  8906                  mov       [esi],eax

LAB_0048af9f:
  0048af9f  817c24182c010000      cmp       dword [esp+18h],12Ch
  0048afa7  7d09                  jge       short 0048AFB2h
  0048afa9  c6464300              mov       byte [esi+43h],0
  0048afad  e976010000            jmp       0048B128h

LAB_0048afb2:
  0048afb2  8a4643                mov       al,[esi+43h]
  0048afb5  84c0                  test      al,al
  0048afb7  7405                  je        short 0048AFBEh
  0048afb9  fec8                  dec       al
  0048afbb  884643                mov       [esi+43h],al

LAB_0048afbe:
  0048afbe  894e5c                mov       [esi+5Ch],ecx
  0048afc1  e962010000            jmp       0048B128h
  0048afc6  b901000000            mov       ecx,1
  0048afcb  66394e18              cmp       [esi+18h],cx
  0048afcf  740a                  je        short 0048AFDBh
  0048afd1  0d00000001            or        eax,1000000h
  0048afd6  894e18                mov       [esi+18h],ecx
  0048afd9  8906                  mov       [esi],eax

LAB_0048afdb:
  0048afdb  8b442440              mov       eax,[esp+40h]
  0048afdf  8b4c2414              mov       ecx,[esp+14h]
  0048afe3  83c020                add       eax,20h
  0048afe6  8d1408                lea       edx,[eax+ecx]
  0048afe9  8d3c00                lea       edi,[eax+eax]
  0048afec  81e2ff0f0000          and       edx,0FFFh
  0048aff2  3bd7                  cmp       edx,edi
  0048aff4  7f14                  jg        short 0048B00Ah
  0048aff6  66014e32              add       [esi+32h],cx
  0048affa  c6464300              mov       byte [esi+43h],0
  0048affe  c7465c00000000        mov       dword [esi+5Ch],0
  0048b005  e91e010000            jmp       0048B128h

LAB_0048b00a:
  0048b00a  81e1ff0f0000          and       ecx,0FFFh
  0048b010  81f900080000          cmp       ecx,800h
  0048b016  7c02                  jl        short 0048B01Ah
  0048b018  f7d8                  neg       eax

LAB_0048b01a:
  0048b01a  66014632              add       [esi+32h],ax
  0048b01e  c7465c00000000        mov       dword [esi+5Ch],0
  0048b025  e9fe000000            jmp       0048B128h
  0048b02a  b901000000            mov       ecx,1
  0048b02f  66394e18              cmp       [esi+18h],cx
  0048b033  740a                  je        short 0048B03Fh
  0048b035  0d00000001            or        eax,1000000h
  0048b03a  894e18                mov       [esi+18h],ecx
  0048b03d  8906                  mov       [esi],eax

LAB_0048b03f:
  0048b03f  0fbf4610              movsx     eax,word [esi+10h]
  0048b043  39442418              cmp       [esp+18h],eax
  0048b047  7d09                  jge       short 0048B052h
  0048b049  c6464300              mov       byte [esi+43h],0
  0048b04d  e9d6000000            jmp       0048B128h

LAB_0048b052:
  0048b052  8a4643                mov       al,[esi+43h]
  0048b055  84c0                  test      al,al
  0048b057  7405                  je        short 0048B05Eh
  0048b059  fec8                  dec       al
  0048b05b  884643                mov       [esi+43h],al

LAB_0048b05e:
  0048b05e  8b542440              mov       edx,[esp+40h]
  0048b062  8b4c2414              mov       ecx,[esp+14h]
  0048b066  8d4240                lea       eax,[edx+40h]
  0048b069  8d3c08                lea       edi,[eax+ecx]
  0048b06c  8d1c00                lea       ebx,[eax+eax]
  0048b06f  81e7ff0f0000          and       edi,0FFFh
  0048b075  3bfb                  cmp       edi,ebx
  0048b077  7f15                  jg        short 0048B08Eh
  0048b079  66014e32              add       [esi+32h],cx
  0048b07d  81c2e0000000          add       edx,0E0h
  0048b083  c1e207                shl       edx,7
  0048b086  89565c                mov       [esi+5Ch],edx
  0048b089  e99a000000            jmp       0048B128h

LAB_0048b08e:
  0048b08e  81e1ff0f0000          and       ecx,0FFFh
  0048b094  81f900080000          cmp       ecx,800h
  0048b09a  7c02                  jl        short 0048B09Eh
  0048b09c  f7d8                  neg       eax

LAB_0048b09e:
  0048b09e  66014632              add       [esi+32h],ax
  0048b0a2  81c2e0000000          add       edx,0E0h
  0048b0a8  c1e207                shl       edx,7
  0048b0ab  89565c                mov       [esi+5Ch],edx
  0048b0ae  eb78                  jmp       short 0048B128h
  0048b0b0  8a4e43                mov       cl,[esi+43h]
  0048b0b3  84c9                  test      cl,cl
  0048b0b5  7405                  je        short 0048B0BCh
  0048b0b7  fec9                  dec       cl
  0048b0b9  884e43                mov       [esi+43h],cl

LAB_0048b0bc:
  0048b0bc  b901000000            mov       ecx,1
  0048b0c1  66394e18              cmp       [esi+18h],cx
  0048b0c5  740a                  je        short 0048B0D1h
  0048b0c7  0d00000001            or        eax,1000000h
  0048b0cc  894e18                mov       [esi+18h],ecx
  0048b0cf  8906                  mov       [esi],eax

LAB_0048b0d1:
  0048b0d1  8b4c2440              mov       ecx,[esp+40h]
  0048b0d5  668b4610              mov       ax,[esi+10h]
  0048b0d9  8d91e0000000          lea       edx,[ecx+0E0h]
  0048b0df  c1e207                shl       edx,7
  0048b0e2  89565c                mov       [esi+5Ch],edx
  0048b0e5  668b5632              mov       dx,[esi+32h]
  0048b0e9  2bc2                  sub       eax,edx
  0048b0eb  25ff0f0000            and       eax,0FFFh
  0048b0f0  8db800010000          lea       edi,[eax+100h]
  0048b0f6  81e7ff0f0000          and       edi,0FFFh
  0048b0fc  81ff00020000          cmp       edi,200h
  0048b102  7e24                  jle       short 0048B128h
  0048b104  25ff0f0000            and       eax,0FFFh
  0048b109  3d00080000            cmp       eax,800h
  0048b10e  7d09                  jge       short 0048B119h
  0048b110  d1f9                  sar       ecx,1
  0048b112  83c120                add       ecx,20h
  0048b115  8bc1                  mov       eax,ecx
  0048b117  eb09                  jmp       short 0048B122h

LAB_0048b119:
  0048b119  d1f9                  sar       ecx,1
  0048b11b  b8e0ffffff            mov       eax,0FFFFFFE0h
  0048b120  2bc1                  sub       eax,ecx

LAB_0048b122:
  0048b122  03d0                  add       edx,eax
  0048b124  66895632              mov       [esi+32h],dx

LAB_0048b128:
  0048b128  8a4e40                mov       cl,[esi+40h]
  0048b12b  f6c101                test      cl,1
  0048b12e  0f84b0000000          je        near 0048B1E4h
  0048b134  8b466c                mov       eax,[esi+6Ch]
  0048b137  8b6e44                mov       ebp,[esi+44h]
  0048b13a  8b5e48                mov       ebx,[esi+48h]
  0048b13d  8b7e4c                mov       edi,[esi+4Ch]
  0048b140  8b501c                mov       edx,[eax+1Ch]
  0048b143  03d5                  add       edx,ebp
  0048b145  89561c                mov       [esi+1Ch],edx
  0048b148  8b5020                mov       edx,[eax+20h]
  0048b14b  03d3                  add       edx,ebx
  0048b14d  895620                mov       [esi+20h],edx
  0048b150  8b5024                mov       edx,[eax+24h]
  0048b153  03d7                  add       edx,edi
  0048b155  895624                mov       [esi+24h],edx
  0048b158  8b10                  mov       edx,[eax]
  0048b15a  f7c200000100          test      edx,10000h
  0048b160  7409                  je        short 0048B16Bh
  0048b162  33c0                  xor       eax,eax
  0048b164  a038e55d00            mov       al,[5DE538h]
  0048b169  eb07                  jmp       short 0048B172h

LAB_0048b16b:
  0048b16b  33d2                  xor       edx,edx
  0048b16d  8a5008                mov       dl,[eax+8]
  0048b170  8bc2                  mov       eax,edx

LAB_0048b172:
  0048b172  8d14c0                lea       edx,[eax+eax*8]
  0048b175  8d0450                lea       eax,[eax+edx*2]
  0048b178  33d2                  xor       edx,edx
  0048b17a  8a148588915e00        mov       dl,[eax*4+5E9188h]
  0048b181  8bc2                  mov       eax,edx
  0048b183  c1e004                shl       eax,4
  0048b186  05204a6000            add       eax,604A20h
  0048b18b  f64002f0              test      byte [eax+2],0F0h
  0048b18f  7461                  je        short 0048B1F2h
  0048b191  66ff4e10              dec       word [esi+10h]
  0048b195  33db                  xor       ebx,ebx
  0048b197  66395e10              cmp       [esi+10h],bx
  0048b19b  7557                  jne       short 0048B1F4h
  0048b19d  0fbf4632              movsx     eax,word [esi+32h]
  0048b1a1  8b16                  mov       edx,[esi]
  0048b1a3  0500080000            add       eax,800h
  0048b1a8  83e2fd                and       edx,0FFFFFFFDh
  0048b1ab  80e1fe                and       cl,0FEh
  0048b1ae  50                    push      eax
  0048b1af  8916                  mov       [esi],edx
  0048b1b1  885e43                mov       [esi+43h],bl
  0048b1b4  884e40                mov       [esi+40h],cl
  0048b1b7  e860a80b00            call      00545A1Ch
  0048b1bc  8b4e50                mov       ecx,[esi+50h]
  0048b1bf  c1e005                shl       eax,5
  0048b1c2  03c8                  add       ecx,eax
  0048b1c4  894e50                mov       [esi+50h],ecx
  0048b1c7  0fbf4e32              movsx     ecx,word [esi+32h]
  0048b1cb  81c100080000          add       ecx,800h
  0048b1d1  51                    push      ecx
  0048b1d2  e83fa80b00            call      00545A16h
  0048b1d7  8b4e58                mov       ecx,[esi+58h]
  0048b1da  c1e005                shl       eax,5
  0048b1dd  03c8                  add       ecx,eax
  0048b1df  894e58                mov       [esi+58h],ecx
  0048b1e2  eb10                  jmp       short 0048B1F4h

LAB_0048b1e4:
  0048b1e4  8b54241c              mov       edx,[esp+1Ch]
  0048b1e8  52                    push      edx
  0048b1e9  56                    push      esi
  0048b1ea  e871cfffff            call      00488160h
  0048b1ef  83c408                add       esp,8

LAB_0048b1f2:
  0048b1f2  33db                  xor       ebx,ebx

LAB_0048b1f4:
  0048b1f4  56                    push      esi
  0048b1f5  e846870a00            call      00533940h
  0048b1fa  8b06                  mov       eax,[esi]
  0048b1fc  83c404                add       esp,4
  0048b1ff  8bc8                  mov       ecx,eax
  0048b201  83e101                and       ecx,1
  0048b204  740a                  je        short 0048B210h
  0048b206  8b5660                mov       edx,[esi+60h]
  0048b209  8b7e20                mov       edi,[esi+20h]
  0048b20c  3bfa                  cmp       edi,edx
  0048b20e  7e14                  jle       short 0048B224h

LAB_0048b210:
  0048b210  3bcb                  cmp       ecx,ebx
  0048b212  753f                  jne       short 0048B253h
  0048b214  8b7e20                mov       edi,[esi+20h]
  0048b217  8b5660                mov       edx,[esi+60h]
  0048b21a  81ef00c00000          sub       edi,0C000h
  0048b220  3bfa                  cmp       edi,edx
  0048b222  7f27                  jg        short 0048B24Bh

LAB_0048b224:
  0048b224  f7869000000000a00000  test      dword [esi+90h],0A000h
  0048b22e  751b                  jne       short 0048B24Bh
  0048b230  3bcb                  cmp       ecx,ebx
  0048b232  895620                mov       [esi+20h],edx
  0048b235  740b                  je        short 0048B242h
  0048b237  8a4e43                mov       cl,[esi+43h]
  0048b23a  84c9                  test      cl,cl
  0048b23c  7404                  je        short 0048B242h
  0048b23e  c6464300              mov       byte [esi+43h],0

LAB_0048b242:
  0048b242  24fe                  and       al,0FEh
  0048b244  895e54                mov       [esi+54h],ebx
  0048b247  8906                  mov       [esi],eax
  0048b249  eb08                  jmp       short 0048B253h

LAB_0048b24b:
  0048b24b  3bcb                  cmp       ecx,ebx
  0048b24d  7504                  jne       short 0048B253h
  0048b24f  0c01                  or        al,1
  0048b251  8906                  mov       [esi],eax

LAB_0048b253:
  0048b253  8a4640                mov       al,[esi+40h]
  0048b256  a801                  test      al,1
  0048b258  7411                  je        short 0048B26Bh
  0048b25a  8b4e6c                mov       ecx,[esi+6Ch]
  0048b25d  f70100200050          test      dword [ecx],50002000h
  0048b263  7512                  jne       short 0048B277h
  0048b265  66395912              cmp       [ecx+12h],bx
  0048b269  750c                  jne       short 0048B277h

LAB_0048b26b:
  0048b26b  f7869000000082ad0020  test      dword [esi+90h],2000AD82h
  0048b275  7417                  je        short 0048B28Eh

LAB_0048b277:
  0048b277  8b16                  mov       edx,[esi]
  0048b279  c6464201              mov       byte [esi+42h],1
  0048b27d  83e2fd                and       edx,0FFFFFFFDh
  0048b280  24fe                  and       al,0FEh
  0048b282  8916                  mov       [esi],edx
  0048b284  c6464301              mov       byte [esi+43h],1
  0048b288  884640                mov       [esi+40h],al
  0048b28b  895e5c                mov       [esi+5Ch],ebx

LAB_0048b28e:
  0048b28e  66399e96000000        cmp       [esi+96h],bx
  0048b295  741b                  je        short 0048B2B2h
  0048b297  8b06                  mov       eax,[esi]
  0048b299  a840                  test      al,40h
  0048b29b  740a                  je        short 0048B2A7h
  0048b29d  a843                  test      al,43h
  0048b29f  7511                  jne       short 0048B2B2h
  0048b2a1  c6464300              mov       byte [esi+43h],0
  0048b2a5  eb0b                  jmp       short 0048B2B2h

LAB_0048b2a7:
  0048b2a7  c6464200              mov       byte [esi+42h],0
  0048b2ab  c6464301              mov       byte [esi+43h],1
  0048b2af  895e5c                mov       [esi+5Ch],ebx

LAB_0048b2b2:
  0048b2b2  395c241c              cmp       [esp+1Ch],ebx
  0048b2b6  0f844e020000          je        near 0048B50Ah
  0048b2bc  f70617080000          test      dword [esi],817h
  0048b2c2  0f8542020000          jne       near 0048B50Ah
  0048b2c8  8a4643                mov       al,[esi+43h]
  0048b2cb  84c0                  test      al,al
  0048b2cd  0f8537020000          jne       near 0048B50Ah
  0048b2d3  f6464001              test      byte [esi+40h],1
  0048b2d7  0f852d020000          jne       near 0048B50Ah
  0048b2dd  8b466c                mov       eax,[esi+6Ch]
  0048b2e0  8b6e1c                mov       ebp,[esi+1Ch]
  0048b2e3  8b4e24                mov       ecx,[esi+24h]
  0048b2e6  8b781c                mov       edi,[eax+1Ch]
  0048b2e9  2bfd                  sub       edi,ebp
  0048b2eb  8b6824                mov       ebp,[eax+24h]
  0048b2ee  2be9                  sub       ebp,ecx
  0048b2f0  c1ff0c                sar       edi,0Ch
  0048b2f3  c1fd0c                sar       ebp,0Ch
  0048b2f6  8bc7                  mov       eax,edi
  0048b2f8  8bcd                  mov       ecx,ebp
  0048b2fa  0fafc7                imul      eax,edi
  0048b2fd  0fafcd                imul      ecx,ebp
  0048b300  03c1                  add       eax,ecx
  0048b302  50                    push      eax
  0048b303  e808a70b00            call      00545A10h
  0048b308  55                    push      ebp
  0048b309  57                    push      edi
  0048b30a  89442420              mov       [esp+20h],eax
  0048b30e  e80fa70b00            call      00545A22h
  0048b313  668b5632              mov       dx,[esi+32h]
  0048b317  2bc2                  sub       eax,edx
  0048b319  25ff0f0000            and       eax,0FFFh
  0048b31e  89442414              mov       [esp+14h],eax
  0048b322  8b442418              mov       eax,[esp+18h]
  0048b326  3dfa000000            cmp       eax,0FAh
  0048b32b  0f8dc6000000          jge       near 0048B3F7h
  0048b331  e80abe0b00            call      00547140h
  0048b336  99                    cdq
  0048b337  b964000000            mov       ecx,64h
  0048b33c  f7f9                  idiv      ecx
  0048b33e  83fa3c                cmp       edx,3Ch
  0048b341  0f8da9000000          jge       near 0048B3F0h
  0048b347  8b542414              mov       edx,[esp+14h]
  0048b34b  8b7c2410              mov       edi,[esp+10h]
  0048b34f  8d84ba00010000        lea       eax,[edx+edi*4+100h]
  0048b356  8d0cfd00020000        lea       ecx,[edi*8+200h]
  0048b35d  25ff0f0000            and       eax,0FFFh
  0048b362  3bc1                  cmp       eax,ecx
  0048b364  7f3e                  jg        short 0048B3A4h
  0048b366  e8d5bd0b00            call      00547140h
  0048b36b  99                    cdq
  0048b36c  8d4c3f01              lea       ecx,[edi+edi+1]
  0048b370  f7f9                  idiv      ecx
  0048b372  8b442418              mov       eax,[esp+18h]
  0048b376  83c264                add       edx,64h
  0048b379  3bc2                  cmp       eax,edx
  0048b37b  7d27                  jge       short 0048B3A4h
  0048b37d  8b16                  mov       edx,[esi]
  0048b37f  8a4640                mov       al,[esi+40h]
  0048b382  81e2fffffffe          and       edx,0FEFFFFFFh
  0048b388  c7461803000000        mov       dword [esi+18h],3
  0048b38f  83ca02                or        edx,2
  0048b392  24fe                  and       al,0FEh
  0048b394  8916                  mov       [esi],edx
  0048b396  884640                mov       [esi+40h],al
  0048b399  895e5c                mov       [esi+5Ch],ebx
  0048b39c  5f                    pop       edi
  0048b39d  5e                    pop       esi
  0048b39e  5d                    pop       ebp
  0048b39f  5b                    pop       ebx
  0048b3a0  83c42c                add       esp,2Ch
  0048b3a3  c3                    ret

LAB_0048b3a4:
  0048b3a4  c6464203              mov       byte [esi+42h],3
  0048b3a8  e893bd0b00            call      00547140h
  0048b3ad  99                    cdq
  0048b3ae  b90a000000            mov       ecx,0Ah
  0048b3b3  f7f9                  idiv      ecx
  0048b3b5  8b4c2440              mov       ecx,[esp+40h]
  0048b3b9  8bc7                  mov       eax,edi
  0048b3bb  d1f8                  sar       eax,1
  0048b3bd  8db980000000          lea       edi,[ecx+80h]
  0048b3c3  c1e704                shl       edi,4
  0048b3c6  02d0                  add       dl,al
  0048b3c8  80c214                add       dl,14h
  0048b3cb  885643                mov       [esi+43h],dl
  0048b3ce  e86dbd0b00            call      00547140h
  0048b3d3  99                    cdq
  0048b3d4  f7ff                  idiv      edi
  0048b3d6  8b4c2414              mov       ecx,[esp+14h]
  0048b3da  d1ff                  sar       edi,1
  0048b3dc  2bd7                  sub       edx,edi
  0048b3de  03d1                  add       edx,ecx
  0048b3e0  66035632              add       dx,[esi+32h]
  0048b3e4  66895610              mov       [esi+10h],dx
  0048b3e8  5f                    pop       edi
  0048b3e9  5e                    pop       esi
  0048b3ea  5d                    pop       ebp
  0048b3eb  5b                    pop       ebx
  0048b3ec  83c42c                add       esp,2Ch
  0048b3ef  c3                    ret

LAB_0048b3f0:
  0048b3f0  83fa50                cmp       edx,50h
  0048b3f3  7d51                  jge       short 0048B446h
  0048b3f5  eb1d                  jmp       short 0048B414h

LAB_0048b3f7:
  0048b3f7  3df4010000            cmp       eax,1F4h
  0048b3fc  0f8d92000000          jge       near 0048B494h
  0048b402  e839bd0b00            call      00547140h
  0048b407  99                    cdq
  0048b408  b964000000            mov       ecx,64h
  0048b40d  f7f9                  idiv      ecx
  0048b40f  83fa32                cmp       edx,32h
  0048b412  7d2d                  jge       short 0048B441h

LAB_0048b414:
  0048b414  c6464202              mov       byte [esi+42h],2
  0048b418  e823bd0b00            call      00547140h
  0048b41d  99                    cdq
  0048b41e  b90a000000            mov       ecx,0Ah
  0048b423  66c746105a00          mov       word [esi+10h],5Ah
  0048b429  f7f9                  idiv      ecx
  0048b42b  8b442410              mov       eax,[esp+10h]
  0048b42f  d1f8                  sar       eax,1
  0048b431  02d0                  add       dl,al
  0048b433  80c214                add       dl,14h
  0048b436  885643                mov       [esi+43h],dl
  0048b439  5f                    pop       edi
  0048b43a  5e                    pop       esi
  0048b43b  5d                    pop       ebp
  0048b43c  5b                    pop       ebx
  0048b43d  83c42c                add       esp,2Ch
  0048b440  c3                    ret

LAB_0048b441:
  0048b441  83fa50                cmp       edx,50h
  0048b444  7d60                  jge       short 0048B4A6h

LAB_0048b446:
  0048b446  c6464203              mov       byte [esi+42h],3
  0048b44a  e8f1bc0b00            call      00547140h
  0048b44f  99                    cdq
  0048b450  b90a000000            mov       ecx,0Ah
  0048b455  f7f9                  idiv      ecx
  0048b457  8b442410              mov       eax,[esp+10h]
  0048b45b  8b4c2440              mov       ecx,[esp+40h]
  0048b45f  d1f8                  sar       eax,1
  0048b461  8db980000000          lea       edi,[ecx+80h]
  0048b467  c1e704                shl       edi,4
  0048b46a  02d0                  add       dl,al
  0048b46c  80c214                add       dl,14h
  0048b46f  885643                mov       [esi+43h],dl
  0048b472  e8c9bc0b00            call      00547140h
  0048b477  99                    cdq
  0048b478  f7ff                  idiv      edi
  0048b47a  8b4c2414              mov       ecx,[esp+14h]
  0048b47e  d1ff                  sar       edi,1
  0048b480  2bd7                  sub       edx,edi
  0048b482  03d1                  add       edx,ecx
  0048b484  66035632              add       dx,[esi+32h]
  0048b488  66895610              mov       [esi+10h],dx
  0048b48c  5f                    pop       edi
  0048b48d  5e                    pop       esi
  0048b48e  5d                    pop       ebp
  0048b48f  5b                    pop       ebx
  0048b490  83c42c                add       esp,2Ch
  0048b493  c3                    ret

LAB_0048b494:
  0048b494  e8a7bc0b00            call      00547140h
  0048b499  99                    cdq
  0048b49a  b964000000            mov       ecx,64h
  0048b49f  f7f9                  idiv      ecx
  0048b4a1  83fa1e                cmp       edx,1Eh
  0048b4a4  7d27                  jge       short 0048B4CDh

LAB_0048b4a6:
  0048b4a6  c6464200              mov       byte [esi+42h],0
  0048b4aa  e891bc0b00            call      00547140h
  0048b4af  99                    cdq
  0048b4b0  b90a000000            mov       ecx,0Ah
  0048b4b5  f7f9                  idiv      ecx
  0048b4b7  8b442410              mov       eax,[esp+10h]
  0048b4bb  d1f8                  sar       eax,1
  0048b4bd  02d0                  add       dl,al
  0048b4bf  80c214                add       dl,14h
  0048b4c2  885643                mov       [esi+43h],dl
  0048b4c5  5f                    pop       edi
  0048b4c6  5e                    pop       esi
  0048b4c7  5d                    pop       ebp
  0048b4c8  5b                    pop       ebx
  0048b4c9  83c42c                add       esp,2Ch
  0048b4cc  c3                    ret

LAB_0048b4cd:
  0048b4cd  83fa3c                cmp       edx,3Ch
  0048b4d0  7d10                  jge       short 0048B4E2h
  0048b4d2  c6464201              mov       byte [esi+42h],1
  0048b4d6  c6464301              mov       byte [esi+43h],1
  0048b4da  5f                    pop       edi
  0048b4db  5e                    pop       esi
  0048b4dc  5d                    pop       ebp
  0048b4dd  5b                    pop       ebx
  0048b4de  83c42c                add       esp,2Ch
  0048b4e1  c3                    ret

LAB_0048b4e2:
  0048b4e2  c6464203              mov       byte [esi+42h],3
  0048b4e6  e855bc0b00            call      00547140h
  0048b4eb  99                    cdq
  0048b4ec  b90a000000            mov       ecx,0Ah
  0048b4f1  f7f9                  idiv      ecx
  0048b4f3  8b442410              mov       eax,[esp+10h]
  0048b4f7  d1f8                  sar       eax,1
  0048b4f9  02d0                  add       dl,al
  0048b4fb  80c214                add       dl,14h
  0048b4fe  885643                mov       [esi+43h],dl
  0048b501  e83abc0b00            call      00547140h
  0048b506  66894610              mov       [esi+10h],ax

LAB_0048b50a:
  0048b50a  5f                    pop       edi
  0048b50b  5e                    pop       esi
  0048b50c  5d                    pop       ebp
  0048b50d  5b                    pop       ebx
  0048b50e  83c42c                add       esp,2Ch
  0048b511  c3                    ret
  0048b512  8bff                  mov       edi,edi
  0048b514  8daf4800c6af          lea       ebp,[edi-5039FFB8h]
  0048b51a  48                    dec       eax
  0048b51b  002a                  add       [edx],ch
  0048b51d  b048                  mov       al,48h
  0048b51f  00b0b0480090          add       [eax-6FFFB750h],dh
  0048b525  90                    nop
  0048b526  90                    nop
  0048b527  90                    nop
  0048b528  90                    nop
  0048b529  90                    nop
  0048b52a  90                    nop
  0048b52b  90                    nop
  0048b52c  90                    nop
  0048b52d  90                    nop
  0048b52e  90                    nop
  0048b52f  90                    nop

48 cible(s) de saut interne : 0x0048ae3e, 0x0048ae5d, 0x0048aeff, 0x0048af3f, 0x0048af45, 0x0048af65, 0x0048af9f, 0x0048afb2, 0x0048afbe, 0x0048afdb, 0x0048b00a, 0x0048b01a, 0x0048b03f, 0x0048b052, 0x0048b05e, 0x0048b08e, 0x0048b09e, 0x0048b0bc, 0x0048b0d1, 0x0048b119, 0x0048b122, 0x0048b128, 0x0048b16b, 0x0048b172, 0x0048b1e4, 0x0048b1f2, 0x0048b1f4, 0x0048b210, 0x0048b224, 0x0048b242, 0x0048b24b, 0x0048b253, 0x0048b26b, 0x0048b277, 0x0048b28e, 0x0048b2a7, 0x0048b2b2, 0x0048b3a4, 0x0048b3f0, 0x0048b3f7, 0x0048b414, 0x0048b441, 0x0048b446, 0x0048b494, 0x0048b4a6, 0x0048b4cd, 0x0048b4e2, 0x0048b50a
