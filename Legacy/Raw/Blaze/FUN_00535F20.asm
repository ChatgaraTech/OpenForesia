Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00535f20  section .text  offset fichier 0x135320  366 octets

  00535f20  8b15b4876000          mov       edx,[6087B4h]
  00535f26  8b4c2404              mov       ecx,[esp+4]
  00535f2a  53                    push      ebx
  00535f2b  55                    push      ebp
  00535f2c  8b1a                  mov       ebx,[edx]
  00535f2e  33d2                  xor       edx,edx
  00535f30  8a5108                mov       dl,[ecx+8]
  00535f33  56                    push      esi
  00535f34  8bea                  mov       ebp,edx
  00535f36  8b542424              mov       edx,[esp+24h]
  00535f3a  57                    push      edi
  00535f3b  8b796c                mov       edi,[ecx+6Ch]
  00535f3e  8d6ced00              lea       ebp,[ebp+ebp*8]
  00535f42  33c0                  xor       eax,eax
  00535f44  8a4708                mov       al,[edi+8]
  00535f47  c1e505                shl       ebp,5
  00535f4a  81c560a55e00          add       ebp,5EA560h
  00535f50  83c078                add       eax,78h
  00535f53  c1e00d                shl       eax,0Dh
  00535f56  8b7540                mov       esi,[ebp+40h]
  00535f59  03c3                  add       eax,ebx
  00535f5b  8932                  mov       [edx],esi
  00535f5d  33f6                  xor       esi,esi
  00535f5f  668b7544              mov       si,[ebp+44h]
  00535f63  33db                  xor       ebx,ebx
  00535f65  668b9846010000        mov       bx,[eax+146h]
  00535f6c  2bde                  sub       ebx,esi
  00535f6e  8b742418              mov       esi,[esp+18h]
  00535f72  891e                  mov       [esi],ebx
  00535f74  8b02                  mov       eax,[edx]
  00535f76  f6c402                test      ah,2
  00535f79  7406                  je        short 00535F81h
  00535f7b  c70640000000          mov       dword [esi],40h

LAB_00535f81:
  00535f81  0fbf454c              movsx     eax,word [ebp+4Ch]
  00535f85  0fbf5d48              movsx     ebx,word [ebp+48h]
  00535f89  c1e005                shl       eax,5
  00535f8c  99                    cdq
  00535f8d  f7fb                  idiv      ebx
  00535f8f  ba20000000            mov       edx,20h
  00535f94  2bd0                  sub       edx,eax
  00535f96  8b06                  mov       eax,[esi]
  00535f98  03c2                  add       eax,edx
  00535f9a  8906                  mov       [esi],eax
  00535f9c  7908                  jns       short 00535FA6h
  00535f9e  c70600000000          mov       dword [esi],0
  00535fa4  eb0b                  jmp       short 00535FB1h

LAB_00535fa6:
  00535fa6  83f840                cmp       eax,40h
  00535fa9  7e06                  jle       short 00535FB1h
  00535fab  c70640000000          mov       dword [esi],40h

LAB_00535fb1:
  00535fb1  8b5f20                mov       ebx,[edi+20h]
  00535fb4  8b5120                mov       edx,[ecx+20h]
  00535fb7  8b771c                mov       esi,[edi+1Ch]
  00535fba  8b7f24                mov       edi,[edi+24h]
  00535fbd  8b411c                mov       eax,[ecx+1Ch]
  00535fc0  2bda                  sub       ebx,edx
  00535fc2  8b5124                mov       edx,[ecx+24h]
  00535fc5  2bf0                  sub       esi,eax
  00535fc7  2bfa                  sub       edi,edx
  00535fc9  c1ff0c                sar       edi,0Ch
  00535fcc  c1fe0c                sar       esi,0Ch
  00535fcf  57                    push      edi
  00535fd0  56                    push      esi
  00535fd1  c1fb0c                sar       ebx,0Ch
  00535fd4  e849fa0000            call      00545A22h
  00535fd9  8b4c2414              mov       ecx,[esp+14h]
  00535fdd  668b5132              mov       dx,[ecx+32h]
  00535fe1  8b4c2420              mov       ecx,[esp+20h]
  00535fe5  2bc2                  sub       eax,edx
  00535fe7  8bd6                  mov       edx,esi
  00535fe9  25ff0f0000            and       eax,0FFFh
  00535fee  0fafd6                imul      edx,esi
  00535ff1  8901                  mov       [ecx],eax
  00535ff3  8bc3                  mov       eax,ebx
  00535ff5  0fafc3                imul      eax,ebx
  00535ff8  8bcf                  mov       ecx,edi
  00535ffa  03d0                  add       edx,eax
  00535ffc  0fafcf                imul      ecx,edi
  00535fff  03d1                  add       edx,ecx
  00536001  52                    push      edx
  00536002  e809fa0000            call      00545A10h
  00536007  8b54241c              mov       edx,[esp+1Ch]
  0053600b  8b742428              mov       esi,[esp+28h]
  0053600f  8902                  mov       [edx],eax
  00536011  8a06                  mov       al,[esi]
  00536013  a808                  test      al,8
  00536015  7410                  je        short 00536027h
  00536017  e824110100            call      00547140h
  0053601c  8b4c2420              mov       ecx,[esp+20h]
  00536020  25ff0f0000            and       eax,0FFFh
  00536025  8901                  mov       [ecx],eax

LAB_00536027:
  00536027  8b442424              mov       eax,[esp+24h]
  0053602b  c70000100000          mov       dword [eax],1000h
  00536031  8b0e                  mov       ecx,[esi]
  00536033  f6c540                test      ch,40h
  00536036  7406                  je        short 0053603Eh
  00536038  c70033130000          mov       dword [eax],1333h

LAB_0053603e:
  0053603e  8b0e                  mov       ecx,[esi]
  00536040  f6c580                test      ch,80h
  00536043  7406                  je        short 0053604Bh
  00536045  c700cc0c0000          mov       dword [eax],0CCCh

LAB_0053604b:
  0053604b  f60604                test      byte [esi],4
  0053604e  7406                  je        short 00536056h
  00536050  c70000080000          mov       dword [eax],800h

LAB_00536056:
  00536056  f706c0010000          test      dword [esi],1C0h
  0053605c  7406                  je        short 00536064h
  0053605e  c70000000000          mov       dword [eax],0

LAB_00536064:
  00536064  8b4c2414              mov       ecx,[esp+14h]
  00536068  f60180                test      byte [ecx],80h
  0053606b  7406                  je        short 00536073h
  0053606d  c70000000000          mov       dword [eax],0

LAB_00536073:
  00536073  f7451400000080        test      dword [ebp+14h],80000000h
  0053607a  750d                  jne       short 00536089h
  0053607c  8b5174                mov       edx,[ecx+74h]
  0053607f  85d2                  test      edx,edx
  00536081  7406                  je        short 00536089h
  00536083  c70000000000          mov       dword [eax],0

LAB_00536089:
  00536089  5f                    pop       edi
  0053608a  5e                    pop       esi
  0053608b  5d                    pop       ebp
  0053608c  5b                    pop       ebx
  0053608d  c3                    ret

10 cible(s) de saut interne : 0x00535f81, 0x00535fa6, 0x00535fb1, 0x00536027, 0x0053603e, 0x0053604b, 0x00536056, 0x00536064, 0x00536073, 0x00536089
