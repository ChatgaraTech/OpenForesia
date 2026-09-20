C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043c4a0  section .text  offset fichier 0x3b8a0  1300 octets

  0043c4a0  83ec1c                sub       esp,1Ch
  0043c4a3  53                    push      ebx
  0043c4a4  55                    push      ebp
  0043c4a5  56                    push      esi
  0043c4a6  8b74242c              mov       esi,[esp+2Ch]
  0043c4aa  33ed                  xor       ebp,ebp
  0043c4ac  57                    push      edi
  0043c4ad  8b5e74                mov       ebx,[esi+74h]
  0043c4b0  66396e10              cmp       [esi+10h],bp
  0043c4b4  895c2414              mov       [esp+14h],ebx
  0043c4b8  753f                  jne       short 0043C4F9h
  0043c4ba  806636fe              and       byte [esi+36h],0FEh
  0043c4be  b8fa000000            mov       eax,0FAh
  0043c4c3  66894628              mov       [esi+28h],ax
  0043c4c7  6689462c              mov       [esi+2Ch],ax
  0043c4cb  b096                  mov       al,96h
  0043c4cd  c6460400              mov       byte [esi+4],0
  0043c4d1  c6460a03              mov       byte [esi+0Ah],3
  0043c4d5  c6460b01              mov       byte [esi+0Bh],1
  0043c4d9  66c746100100          mov       word [esi+10h],1
  0043c4df  66896e2a              mov       [esi+2Ah],bp
  0043c4e3  66c746120a00          mov       word [esi+12h],0Ah
  0043c4e9  884638                mov       [esi+38h],al
  0043c4ec  884639                mov       [esi+39h],al
  0043c4ef  88463a                mov       [esi+3Ah],al
  0043c4f2  66896e16              mov       [esi+16h],bp
  0043c4f6  896e44                mov       [esi+44h],ebp

LAB_0043c4f9:
  0043c4f9  66837e1001            cmp       word [esi+10h],1
  0043c4fe  0f854e020000          jne       near 0043C752h
  0043c504  668b4332              mov       ax,[ebx+32h]
  0043c508  66c746340004          mov       word [esi+34h],400h
  0043c50e  6605000c              add       ax,0C00h
  0043c512  8d7e1c                lea       edi,[esi+1Ch]
  0043c515  66894632              mov       [esi+32h],ax
  0043c519  8b4b20                mov       ecx,[ebx+20h]
  0043c51c  0fbfd0                movsx     edx,ax
  0043c51f  0fbf869a000000        movsx     eax,word [esi+9Ah]
  0043c526  81c100300700          add       ecx,73000h
  0043c52c  894e20                mov       [esi+20h],ecx
  0043c52f  8d8c0200f4ffff        lea       ecx,[edx+eax-0C00h]
  0043c536  51                    push      ecx
  0043c537  e8e0941000            call      00545A1Ch
  0043c53c  8bd0                  mov       edx,eax
  0043c53e  c1e203                shl       edx,3
  0043c541  2bd0                  sub       edx,eax
  0043c543  8b431c                mov       eax,[ebx+1Ch]
  0043c546  8d0c50                lea       ecx,[eax+edx*2]
  0043c549  0fbf5632              movsx     edx,word [esi+32h]
  0043c54d  0fbf869a000000        movsx     eax,word [esi+9Ah]
  0043c554  890f                  mov       [edi],ecx
  0043c556  8d8c0200f4ffff        lea       ecx,[edx+eax-0C00h]
  0043c55d  51                    push      ecx
  0043c55e  e8b3941000            call      00545A16h
  0043c563  8bd0                  mov       edx,eax
  0043c565  c1e203                shl       edx,3
  0043c568  2bd0                  sub       edx,eax
  0043c56a  8b4324                mov       eax,[ebx+24h]
  0043c56d  66837e120a            cmp       word [esi+12h],0Ah
  0043c572  8d0c50                lea       ecx,[eax+edx*2]
  0043c575  894e24                mov       [esi+24h],ecx
  0043c578  7513                  jne       short 0043C58Dh
  0043c57a  6aff                  push      0FFFFFFFFh
  0043c57c  55                    push      ebp
  0043c57d  68df000000            push      0DFh
  0043c582  55                    push      ebp
  0043c583  57                    push      edi
  0043c584  56                    push      esi
  0043c585  e8b6e60300            call      0047AC40h
  0043c58a  83c418                add       esp,18h

LAB_0043c58d:
  0043c58d  8b17                  mov       edx,[edi]
  0043c58f  8b4620                mov       eax,[esi+20h]
  0043c592  8b4e24                mov       ecx,[esi+24h]
  0043c595  89542420              mov       [esp+20h],edx
  0043c599  0fbf5632              movsx     edx,word [esi+32h]
  0043c59d  81ea000c0000          sub       edx,0C00h
  0043c5a3  89442424              mov       [esp+24h],eax
  0043c5a7  52                    push      edx
  0043c5a8  894c242c              mov       [esp+2Ch],ecx
  0043c5ac  e86b941000            call      00545A1Ch
  0043c5b1  0fbf4e16              movsx     ecx,word [esi+16h]
  0043c5b5  0fbf5632              movsx     edx,word [esi+32h]
  0043c5b9  0fafc1                imul      eax,ecx
  0043c5bc  8b0f                  mov       ecx,[edi]
  0043c5be  81ea000c0000          sub       edx,0C00h
  0043c5c4  03c8                  add       ecx,eax
  0043c5c6  52                    push      edx
  0043c5c7  890f                  mov       [edi],ecx
  0043c5c9  e848941000            call      00545A16h
  0043c5ce  0fbf4e16              movsx     ecx,word [esi+16h]
  0043c5d2  0fafc1                imul      eax,ecx
  0043c5d5  8b4e24                mov       ecx,[esi+24h]
  0043c5d8  896c2410              mov       [esp+10h],ebp
  0043c5dc  03c8                  add       ecx,eax
  0043c5de  b8c0935e00            mov       eax,5E93C0h
  0043c5e3  894e24                mov       [esi+24h],ecx
  0043c5e6  89442430              mov       [esp+30h],eax
  0043c5ea  c744241804000000      mov       dword [esp+18h],4

LAB_0043c5f2:
  0043c5f2  8b08                  mov       ecx,[eax]
  0043c5f4  f7c100000080          test      ecx,80000000h
  0043c5fa  0f84b0000000          je        near 0043C6B0h
  0043c600  f7c100000050          test      ecx,50000000h
  0043c606  0f85a4000000          jne       near 0043C6B0h
  0043c60c  8b781c                mov       edi,[eax+1Ch]
  0043c60f  8b6e1c                mov       ebp,[esi+1Ch]
  0043c612  8b5820                mov       ebx,[eax+20h]
  0043c615  8b5620                mov       edx,[esi+20h]
  0043c618  2bfd                  sub       edi,ebp
  0043c61a  8b6824                mov       ebp,[eax+24h]
  0043c61d  8b4624                mov       eax,[esi+24h]
  0043c620  2bda                  sub       ebx,edx
  0043c622  2be8                  sub       ebp,eax
  0043c624  c1fd0c                sar       ebp,0Ch
  0043c627  c1ff0c                sar       edi,0Ch
  0043c62a  55                    push      ebp
  0043c62b  57                    push      edi
  0043c62c  c1fb0c                sar       ebx,0Ch
  0043c62f  e8ee931000            call      00545A22h
  0043c634  8b542414              mov       edx,[esp+14h]
  0043c638  0fbf4a32              movsx     ecx,word [edx+32h]
  0043c63c  2bc1                  sub       eax,ecx
  0043c63e  8bd7                  mov       edx,edi
  0043c640  25ff0f0000            and       eax,0FFFh
  0043c645  0fafd7                imul      edx,edi
  0043c648  8944241c              mov       [esp+1Ch],eax
  0043c64c  8bc3                  mov       eax,ebx
  0043c64e  0fafc3                imul      eax,ebx
  0043c651  8bcd                  mov       ecx,ebp
  0043c653  03d0                  add       edx,eax
  0043c655  0fafcd                imul      ecx,ebp
  0043c658  03d1                  add       edx,ecx
  0043c65a  52                    push      edx
  0043c65b  e8b0931000            call      00545A10h
  0043c660  3dc8000000            cmp       eax,0C8h
  0043c665  7d45                  jge       short 0043C6ACh
  0043c667  8b44241c              mov       eax,[esp+1Ch]
  0043c66b  3d20030000            cmp       eax,320h
  0043c670  7d04                  jge       short 0043C676h
  0043c672  85c0                  test      eax,eax
  0043c674  7f0e                  jg        short 0043C684h

LAB_0043c676:
  0043c676  3d00100000            cmp       eax,1000h
  0043c67b  7d2f                  jge       short 0043C6ACh
  0043c67d  3de00c0000            cmp       eax,0CE0h
  0043c682  7e28                  jle       short 0043C6ACh

LAB_0043c684:
  0043c684  8b4c2410              mov       ecx,[esp+10h]
  0043c688  b801000000            mov       eax,1
  0043c68d  d3e0                  shl       eax,cl
  0043c68f  8b4e44                mov       ecx,[esi+44h]
  0043c692  85c1                  test      ecx,eax
  0043c694  7516                  jne       short 0043C6ACh
  0043c696  8b542430              mov       edx,[esp+30h]
  0043c69a  6a00                  push      0
  0043c69c  0bc1                  or        eax,ecx
  0043c69e  6a05                  push      5
  0043c6a0  52                    push      edx
  0043c6a1  894644                mov       [esi+44h],eax
  0043c6a4  e807d10f00            call      005397B0h
  0043c6a9  83c40c                add       esp,0Ch

LAB_0043c6ac:
  0043c6ac  8b442430              mov       eax,[esp+30h]

LAB_0043c6b0:
  0043c6b0  8b7c2410              mov       edi,[esp+10h]
  0043c6b4  8b4c2418              mov       ecx,[esp+18h]
  0043c6b8  47                    inc       edi
  0043c6b9  059c000000            add       eax,9Ch
  0043c6be  49                    dec       ecx
  0043c6bf  897c2410              mov       [esp+10h],edi
  0043c6c3  89442430              mov       [esp+30h],eax
  0043c6c7  894c2418              mov       [esp+18h],ecx
  0043c6cb  0f8521ffffff          jne       near 0043C5F2h
  0043c6d1  56                    push      esi
  0043c6d2  e869720f00            call      00533940h
  0043c6d7  8b442424              mov       eax,[esp+24h]
  0043c6db  8b4c2428              mov       ecx,[esp+28h]
  0043c6df  8b54242c              mov       edx,[esp+2Ch]
  0043c6e3  89461c                mov       [esi+1Ch],eax
  0043c6e6  668b462a              mov       ax,[esi+2Ah]
  0043c6ea  894e20                mov       [esi+20h],ecx
  0043c6ed  0fbfc8                movsx     ecx,ax
  0043c6f0  895624                mov       [esi+24h],edx
  0043c6f3  ba00400000            mov       edx,4000h
  0043c6f8  2bd1                  sub       edx,ecx
  0043c6fa  83c404                add       esp,4
  0043c6fd  c1fa02                sar       edx,2
  0043c700  03d0                  add       edx,eax
  0043c702  668b4616              mov       ax,[esi+16h]
  0043c706  0fbfc8                movsx     ecx,ax
  0043c709  6689562a              mov       [esi+2Ah],dx
  0043c70d  ba58020000            mov       edx,258h
  0043c712  2bd1                  sub       edx,ecx
  0043c714  c1fa02                sar       edx,2
  0043c717  03d0                  add       edx,eax
  0043c719  8b8690000000          mov       eax,[esi+90h]
  0043c71f  a902880000            test      eax,8802h
  0043c724  66895616              mov       [esi+16h],dx
  0043c728  740c                  je        short 0043C736h
  0043c72a  66c746100200          mov       word [esi+10h],2
  0043c730  66c746120a00          mov       word [esi+12h],0Ah

LAB_0043c736:
  0043c736  66ff4e12              dec       word [esi+12h]
  0043c73a  33ed                  xor       ebp,ebp
  0043c73c  66396e12              cmp       [esi+12h],bp
  0043c740  7f0c                  jg        short 0043C74Eh
  0043c742  66c746100200          mov       word [esi+10h],2
  0043c748  66c746120a00          mov       word [esi+12h],0Ah

LAB_0043c74e:
  0043c74e  8b5c2414              mov       ebx,[esp+14h]

LAB_0043c752:
  0043c752  66837e1002            cmp       word [esi+10h],2
  0043c757  0f8512010000          jne       near 0043C86Fh
  0043c75d  668b4332              mov       ax,[ebx+32h]
  0043c761  66c746340004          mov       word [esi+34h],400h
  0043c767  6605000c              add       ax,0C00h
  0043c76b  66894632              mov       [esi+32h],ax
  0043c76f  8b4b20                mov       ecx,[ebx+20h]
  0043c772  0fbfd0                movsx     edx,ax
  0043c775  0fbf869a000000        movsx     eax,word [esi+9Ah]
  0043c77c  81c100300700          add       ecx,73000h
  0043c782  894e20                mov       [esi+20h],ecx
  0043c785  8d8c0200f4ffff        lea       ecx,[edx+eax-0C00h]
  0043c78c  51                    push      ecx
  0043c78d  e88a921000            call      00545A1Ch
  0043c792  8bd0                  mov       edx,eax
  0043c794  c1e203                shl       edx,3
  0043c797  2bd0                  sub       edx,eax
  0043c799  8b431c                mov       eax,[ebx+1Ch]
  0043c79c  8d0c50                lea       ecx,[eax+edx*2]
  0043c79f  0fbf5632              movsx     edx,word [esi+32h]
  0043c7a3  0fbf869a000000        movsx     eax,word [esi+9Ah]
  0043c7aa  894e1c                mov       [esi+1Ch],ecx
  0043c7ad  8d8c0200f4ffff        lea       ecx,[edx+eax-0C00h]
  0043c7b4  51                    push      ecx
  0043c7b5  e85c921000            call      00545A16h
  0043c7ba  8bd0                  mov       edx,eax
  0043c7bc  c1e203                shl       edx,3
  0043c7bf  2bd0                  sub       edx,eax
  0043c7c1  8b4324                mov       eax,[ebx+24h]
  0043c7c4  8d0c50                lea       ecx,[eax+edx*2]
  0043c7c7  0fbf5632              movsx     edx,word [esi+32h]
  0043c7cb  81ea000c0000          sub       edx,0C00h
  0043c7d1  894e24                mov       [esi+24h],ecx
  0043c7d4  52                    push      edx
  0043c7d5  e842921000            call      00545A1Ch
  0043c7da  0fbf4e16              movsx     ecx,word [esi+16h]
  0043c7de  0fbf5632              movsx     edx,word [esi+32h]
  0043c7e2  0fafc1                imul      eax,ecx
  0043c7e5  8b4e1c                mov       ecx,[esi+1Ch]
  0043c7e8  81ea000c0000          sub       edx,0C00h
  0043c7ee  03c8                  add       ecx,eax
  0043c7f0  52                    push      edx
  0043c7f1  894e1c                mov       [esi+1Ch],ecx
  0043c7f4  e81d921000            call      00545A16h
  0043c7f9  0fbf4e16              movsx     ecx,word [esi+16h]
  0043c7fd  0fafc1                imul      eax,ecx
  0043c800  8b4e24                mov       ecx,[esi+24h]
  0043c803  66c74634000c          mov       word [esi+34h],0C00h
  0043c809  03c8                  add       ecx,eax
  0043c80b  668b462a              mov       ax,[esi+2Ah]
  0043c80f  0fbfd0                movsx     edx,ax
  0043c812  f7da                  neg       edx
  0043c814  c1fa02                sar       edx,2
  0043c817  03d0                  add       edx,eax
  0043c819  8a4638                mov       al,[esi+38h]
  0043c81c  894e24                mov       [esi+24h],ecx
  0043c81f  8bc8                  mov       ecx,eax
  0043c821  81e1ff000000          and       ecx,0FFh
  0043c827  6689562a              mov       [esi+2Ah],dx
  0043c82b  f7d9                  neg       ecx
  0043c82d  c1f903                sar       ecx,3
  0043c830  02c8                  add       cl,al
  0043c832  8a4639                mov       al,[esi+39h]
  0043c835  8bd0                  mov       edx,eax
  0043c837  884e38                mov       [esi+38h],cl
  0043c83a  81e2ff000000          and       edx,0FFh
  0043c840  f7da                  neg       edx
  0043c842  c1fa03                sar       edx,3
  0043c845  02d0                  add       dl,al
  0043c847  8a463a                mov       al,[esi+3Ah]
  0043c84a  8bc8                  mov       ecx,eax
  0043c84c  885639                mov       [esi+39h],dl
  0043c84f  81e1ff000000          and       ecx,0FFh
  0043c855  f7d9                  neg       ecx
  0043c857  c1f903                sar       ecx,3
  0043c85a  02c8                  add       cl,al
  0043c85c  66ff4e12              dec       word [esi+12h]
  0043c860  66396e12              cmp       [esi+12h],bp
  0043c864  884e3a                mov       [esi+3Ah],cl
  0043c867  7f06                  jg        short 0043C86Fh
  0043c869  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043c86f:
  0043c86f  5f                    pop       edi
  0043c870  5e                    pop       esi
  0043c871  5d                    pop       ebp
  0043c872  5b                    pop       ebx
  0043c873  83c41c                add       esp,1Ch
  0043c876  c3                    ret
  0043c877  90                    nop
  0043c878  90                    nop
  0043c879  90                    nop
  0043c87a  90                    nop
  0043c87b  90                    nop
  0043c87c  90                    nop
  0043c87d  90                    nop
  0043c87e  90                    nop
  0043c87f  90                    nop
  0043c880  56                    push      esi
  0043c881  8b742408              mov       esi,[esp+8]
  0043c885  66837e1000            cmp       word [esi+10h],0
  0043c88a  7538                  jne       short 0043C8C4h
  0043c88c  c7463c30135500        mov       dword [esi+3Ch],551330h
  0043c893  e8a8a81000            call      00547140h
  0043c898  99                    cdq
  0043c899  b903000000            mov       ecx,3
  0043c89e  c6460a02              mov       byte [esi+0Ah],2
  0043c8a2  f7f9                  idiv      ecx
  0043c8a4  b8a00f0000            mov       eax,0FA0h
  0043c8a9  c6460b00              mov       byte [esi+0Bh],0
  0043c8ad  66894628              mov       [esi+28h],ax
  0043c8b1  6689462a              mov       [esi+2Ah],ax
  0043c8b5  66c746100100          mov       word [esi+10h],1
  0043c8bb  66c746120b00          mov       word [esi+12h],0Bh
  0043c8c1  885640                mov       [esi+40h],dl

LAB_0043c8c4:
  0043c8c4  66837e1001            cmp       word [esi+10h],1
  0043c8c9  7526                  jne       short 0043C8F1h
  0043c8cb  8a5640                mov       dl,[esi+40h]
  0043c8ce  66ff4e12              dec       word [esi+12h]
  0043c8d2  668b4612              mov       ax,[esi+12h]
  0043c8d6  fec2                  inc       dl
  0043c8d8  8aca                  mov       cl,dl
  0043c8da  885640                mov       [esi+40h],dl
  0043c8dd  80f903                cmp       cl,3
  0043c8e0  7204                  jb        short 0043C8E6h
  0043c8e2  c6464000              mov       byte [esi+40h],0

LAB_0043c8e6:
  0043c8e6  6685c0                test      ax,ax
  0043c8e9  7f06                  jg        short 0043C8F1h
  0043c8eb  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043c8f1:
  0043c8f1  5e                    pop       esi
  0043c8f2  c3                    ret
  0043c8f3  90                    nop
  0043c8f4  90                    nop
  0043c8f5  90                    nop
  0043c8f6  90                    nop
  0043c8f7  90                    nop
  0043c8f8  90                    nop
  0043c8f9  90                    nop
  0043c8fa  90                    nop
  0043c8fb  90                    nop
  0043c8fc  90                    nop
  0043c8fd  90                    nop
  0043c8fe  90                    nop
  0043c8ff  90                    nop
  0043c900  53                    push      ebx
  0043c901  55                    push      ebp
  0043c902  56                    push      esi
  0043c903  57                    push      edi
  0043c904  33ff                  xor       edi,edi
  0043c906  e845e6fdff            call      0041AF50h
  0043c90b  e8301dfdff            call      0040E640h
  0043c910  8b742414              mov       esi,[esp+14h]
  0043c914  8b6c2414              mov       ebp,[esp+14h]
  0043c918  b8dbca5e00            mov       eax,5ECADBh
  0043c91d  ba20000000            mov       edx,20h

LAB_0043c922:
  0043c922  8a48d1                mov       cl,[eax-2Fh]
  0043c925  84c9                  test      cl,cl
  0043c927  7545                  jne       short 0043C96Eh
  0043c929  8b58c5                mov       ebx,[eax-3Bh]
  0043c92c  8d48c5                lea       ecx,[eax-3Bh]
  0043c92f  f7c300000080          test      ebx,80000000h
  0043c935  7437                  je        short 0043C96Eh
  0043c937  8b58e1                mov       ebx,[eax-1Fh]
  0043c93a  8bf9                  mov       edi,ecx
  0043c93c  895e1c                mov       [esi+1Ch],ebx
  0043c93f  8b58e5                mov       ebx,[eax-1Bh]
  0043c942  895e20                mov       [esi+20h],ebx
  0043c945  8b58e9                mov       ebx,[eax-17h]
  0043c948  895e24                mov       [esi+24h],ebx
  0043c94b  33db                  xor       ebx,ebx
  0043c94d  8a58c9                mov       bl,[eax-37h]
  0043c950  66c740dd0200          mov       word [eax-23h],2
  0043c956  8b1c9d40965e00        mov       ebx,[ebx*4+5E9640h]
  0043c95d  8b6b08                mov       ebp,[ebx+8]
  0043c960  8a18                  mov       bl,[eax]
  0043c962  80cb02                or        bl,2
  0043c965  8818                  mov       [eax],bl
  0043c967  8b19                  mov       ebx,[ecx]
  0043c969  80cf10                or        bh,10h
  0043c96c  8919                  mov       [ecx],ebx

LAB_0043c96e:
  0043c96e  059c000000            add       eax,9Ch
  0043c973  4a                    dec       edx
  0043c974  75ac                  jne       short 0043C922h
  0043c976  33c9                  xor       ecx,ecx
  0043c978  3bf9                  cmp       edi,ecx
  0043c97a  7505                  jne       short 0043C981h
  0043c97c  bf58e55d00            mov       edi,5DE558h

LAB_0043c981:
  0043c981  66394e10              cmp       [esi+10h],cx
  0043c985  b801000000            mov       eax,1
  0043c98a  754b                  jne       short 0043C9D7h
  0043c98c  66894610              mov       [esi+10h],ax
  0043c990  66894e12              mov       [esi+12h],cx
  0043c994  66894e14              mov       [esi+14h],cx
  0043c998  66894e16              mov       [esi+16h],cx
  0043c99c  0fbf15b8925e00        movsx     edx,word [5E92B8h]
  0043c9a3  895650                mov       [esi+50h],edx
  0043c9a6  0fbf15ba925e00        movsx     edx,word [5E92BAh]
  0043c9ad  895654                mov       [esi+54h],edx
  0043c9b0  0fbf15bc              (bad)
  (octet invalide — la plage demandee deborde probablement du code)
