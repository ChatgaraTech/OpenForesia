C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00460d60  section .text  offset fichier 0x60160  460 octets

  00460d60  53                    push      ebx
  00460d61  56                    push      esi
  00460d62  8b74240c              mov       esi,[esp+0Ch]
  00460d66  57                    push      edi
  00460d67  33db                  xor       ebx,ebx
  00460d69  0fbf4614              movsx     eax,word [esi+14h]
  00460d6d  99                    cdq
  00460d6e  83e207                and       edx,7
  00460d71  03c2                  add       eax,edx
  00460d73  8bf8                  mov       edi,eax
  00460d75  668b4610              mov       ax,[esi+10h]
  00460d79  c1ff03                sar       edi,3
  00460d7c  663bc3                cmp       ax,bx
  00460d7f  7512                  jne       short 00460D93h
  00460d81  0fbf4634              movsx     eax,word [esi+34h]
  00460d85  89465c                mov       [esi+5Ch],eax
  00460d88  66899e98000000        mov       [esi+98h],bx
  00460d8f  5f                    pop       edi
  00460d90  5e                    pop       esi
  00460d91  5b                    pop       ebx
  00460d92  c3                    ret

LAB_00460d93:
  00460d93  663d0100              cmp       ax,1
  00460d97  7533                  jne       short 00460DCCh
  00460d99  8b0dcc975e00          mov       ecx,[5E97CCh]
  00460d9f  53                    push      ebx
  00460da0  53                    push      ebx
  00460da1  6800100000            push      1000h
  00460da6  8b91a8000000          mov       edx,[ecx+0A8h]
  00460dac  6800080000            push      800h
  00460db1  8b02                  mov       eax,[edx]
  00460db3  660fb64810            movzx     cx,byte [eax+10h]
  00460db8  51                    push      ecx
  00460db9  56                    push      esi
  00460dba  e8c1720000            call      00468080h
  00460dbf  83c418                add       esp,18h
  00460dc2  66c746100200          mov       word [esi+10h],2
  00460dc8  66895e12              mov       [esi+12h],bx

LAB_00460dcc:
  00460dcc  66837e1002            cmp       word [esi+10h],2
  00460dd1  7544                  jne       short 00460E17h
  00460dd3  0fbf4e14              movsx     ecx,word [esi+14h]
  00460dd7  8d14fd00000000        lea       edx,[edi*8]
  00460dde  b001                  mov       al,1
  00460de0  2bca                  sub       ecx,edx
  00460de2  8a97e0965e00          mov       dl,[edi+5E96E0h]
  00460de8  d2e0                  shl       al,cl
  00460dea  0ad0                  or        dl,al
  00460dec  8897e0965e00          mov       [edi+5E96E0h],dl
  00460df2  66ff4612              inc       word [esi+12h]
  00460df6  66837e120c            cmp       word [esi+12h],0Ch
  00460dfb  7c15                  jl        short 00460E12h
  00460dfd  33c9                  xor       ecx,ecx
  00460dff  66837e1601            cmp       word [esi+16h],1
  00460e04  66895e12              mov       [esi+12h],bx
  00460e08  0f95c1                setne     cl
  00460e0b  83c103                add       ecx,3
  00460e0e  66894e10              mov       [esi+10h],cx

LAB_00460e12:
  00460e12  6683463478            add       word [esi+34h],78h

LAB_00460e17:
  00460e17  66837e1003            cmp       word [esi+10h],3
  00460e1c  bf04000000            mov       edi,4
  00460e21  0f85e9000000          jne       near 00460F10h
  00460e27  8b06                  mov       eax,[esi]
  00460e29  80cc20                or        ah,20h
  00460e2c  66ff8698000000        inc       word [esi+98h]
  00460e33  8906                  mov       [esi],eax
  00460e35  8a0d2ce55d00          mov       cl,[5DE52Ch]
  00460e3b  668b8698000000        mov       ax,[esi+98h]
  00460e42  3acb                  cmp       cl,bl
  00460e44  7538                  jne       short 00460E7Eh
  00460e46  663d0300              cmp       ax,3
  00460e4a  7532                  jne       short 00460E7Eh
  00460e4c  66899e98000000        mov       [esi+98h],bx
  00460e53  8b15cc975e00          mov       edx,[5E97CCh]
  00460e59  6800080000            push      800h
  00460e5e  6800080000            push      800h
  00460e63  8b82a8000000          mov       eax,[edx+0A8h]
  00460e69  33d2                  xor       edx,edx
  00460e6b  6800040000            push      400h
  00460e70  8b08                  mov       ecx,[eax]
  00460e72  8a5114                mov       dl,[ecx+14h]
  00460e75  52                    push      edx
  00460e76  e825c00d00            call      0053CEA0h
  00460e7b  83c410                add       esp,10h

LAB_00460e7e:
  00460e7e  803d2ce55d0002        cmp       byte [5DE52Ch],2
  00460e85  753b                  jne       short 00460EC2h
  00460e87  6683be980000000a      cmp       word [esi+98h],0Ah
  00460e8f  7531                  jne       short 00460EC2h
  00460e91  66899e98000000        mov       [esi+98h],bx
  00460e98  a1cc975e00            mov       eax,[5E97CCh]
  00460e9d  6800080000            push      800h
  00460ea2  6800080000            push      800h
  00460ea7  8b88a8000000          mov       ecx,[eax+0A8h]
  00460ead  33c0                  xor       eax,eax
  00460eaf  6800040000            push      400h
  00460eb4  8b11                  mov       edx,[ecx]
  00460eb6  8a4217                mov       al,[edx+17h]
  00460eb9  50                    push      eax
  00460eba  e8e1bf0d00            call      0053CEA0h
  00460ebf  83c410                add       esp,10h

LAB_00460ec2:
  00460ec2  66ff4612              inc       word [esi+12h]
  00460ec6  668b4612              mov       ax,[esi+12h]
  00460eca  663d3200              cmp       ax,32h
  00460ece  7c1a                  jl        short 00460EEAh
  00460ed0  53                    push      ebx
  00460ed1  53                    push      ebx
  00460ed2  68e7030000            push      3E7h
  00460ed7  53                    push      ebx
  00460ed8  68e7030000            push      3E7h
  00460edd  56                    push      esi
  00460ede  66897e10              mov       [esi+10h],di
  00460ee2  e879affbff            call      0041BE60h
  00460ee7  83c418                add       esp,18h

LAB_00460eea:
  00460eea  53                    push      ebx
  00460eeb  53                    push      ebx
  00460eec  68e7030000            push      3E7h
  00460ef1  e84a620e00            call      00547140h
  00460ef6  99                    cdq
  00460ef7  b907000000            mov       ecx,7
  00460efc  f7f9                  idiv      ecx
  00460efe  83ea03                sub       edx,3
  00460f01  52                    push      edx
  00460f02  68e7030000            push      3E7h
  00460f07  56                    push      esi
  00460f08  e853affbff            call      0041BE60h
  00460f0d  83c418                add       esp,18h

LAB_00460f10:
  00460f10  66837e1063            cmp       word [esi+10h],63h
  00460f15  7511                  jne       short 00460F28h
  00460f17  8b06                  mov       eax,[esi]
  00460f19  668146341806          add       word [esi+34h],618h
  00460f1f  80cc20                or        ah,20h
  00460f22  66897e10              mov       [esi+10h],di
  00460f26  8906                  mov       [esi],eax

LAB_00460f28:
  00460f28  5f                    pop       edi
  00460f29  5e                    pop       esi
  00460f2a  5b                    pop       ebx
  00460f2b  c3                    ret

9 cible(s) de saut interne : 0x00460d93, 0x00460dcc, 0x00460e12, 0x00460e17, 0x00460e7e, 0x00460ec2, 0x00460eea, 0x00460f10, 0x00460f28
