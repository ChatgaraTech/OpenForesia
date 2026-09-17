C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041b7c0  section .text  offset fichier 0x1abc0  560 octets

  0041b7c0  53                    push      ebx
  0041b7c1  56                    push      esi
  0041b7c2  8b74240c              mov       esi,[esp+0Ch]
  0041b7c6  66837e1000            cmp       word [esi+10h],0
  0041b7cb  0f8552010000          jne       near 0041B923h
  0041b7d1  66837e1400            cmp       word [esi+14h],0
  0041b7d6  66c746100100          mov       word [esi+10h],1
  0041b7dc  b360                  mov       bl,60h
  0041b7de  0f8597000000          jne       near 0041B87Bh
  0041b7e4  c7463c28fe5400        mov       dword [esi+3Ch],54FE28h
  0041b7eb  c6464000              mov       byte [esi+40h],0
  0041b7ef  c6460a01              mov       byte [esi+0Ah],1
  0041b7f3  c6460b00              mov       byte [esi+0Bh],0
  0041b7f7  e844b91200            call      00547140h
  0041b7fc  99                    cdq
  0041b7fd  33c2                  xor       eax,edx
  0041b7ff  2bc2                  sub       eax,edx
  0041b801  25ff010000            and       eax,1FFh
  0041b806  33c2                  xor       eax,edx
  0041b808  2bc2                  sub       eax,edx
  0041b80a  0500100000            add       eax,1000h
  0041b80f  66894628              mov       [esi+28h],ax
  0041b813  6689462a              mov       [esi+2Ah],ax
  0041b817  e824b91200            call      00547140h
  0041b81c  99                    cdq
  0041b81d  33c2                  xor       eax,edx
  0041b81f  2bc2                  sub       eax,edx
  0041b821  25ff0f0000            and       eax,0FFFh
  0041b826  33c2                  xor       eax,edx
  0041b828  2bc2                  sub       eax,edx
  0041b82a  66894612              mov       [esi+12h],ax
  0041b82e  e80db91200            call      00547140h
  0041b833  99                    cdq
  0041b834  b91e000000            mov       ecx,1Eh
  0041b839  885e38                mov       [esi+38h],bl
  0041b83c  f7f9                  idiv      ecx
  0041b83e  885e39                mov       [esi+39h],bl
  0041b841  885e3a                mov       [esi+3Ah],bl
  0041b844  03d1                  add       edx,ecx
  0041b846  66895614              mov       [esi+14h],dx
  0041b84a  e8f1b81200            call      00547140h
  0041b84f  99                    cdq
  0041b850  b914000000            mov       ecx,14h
  0041b855  f7f9                  idiv      ecx
  0041b857  03d1                  add       edx,ecx
  0041b859  89565c                mov       [esi+5Ch],edx
  0041b85c  e8dfb81200            call      00547140h
  0041b861  99                    cdq
  0041b862  b914000000            mov       ecx,14h
  0041b867  f7f9                  idiv      ecx
  0041b869  8b4e20                mov       ecx,[esi+20h]
  0041b86c  b800c0feff            mov       eax,0FFFEC000h
  0041b871  c1e20c                shl       edx,0Ch
  0041b874  2bc2                  sub       eax,edx
  0041b876  03c8                  add       ecx,eax
  0041b878  894e20                mov       [esi+20h],ecx

LAB_0041b87b:
  0041b87b  66837e1401            cmp       word [esi+14h],1
  0041b880  0f8585000000          jne       near 0041B90Bh
  0041b886  c7463c00fe5400        mov       dword [esi+3Ch],54FE00h
  0041b88d  c6464000              mov       byte [esi+40h],0
  0041b891  c6460a01              mov       byte [esi+0Ah],1
  0041b895  c6460b00              mov       byte [esi+0Bh],0
  0041b899  e8a2b81200            call      00547140h
  0041b89e  99                    cdq
  0041b89f  33c2                  xor       eax,edx
  0041b8a1  2bc2                  sub       eax,edx
  0041b8a3  25ff010000            and       eax,1FFh
  0041b8a8  33c2                  xor       eax,edx
  0041b8aa  2bc2                  sub       eax,edx
  0041b8ac  05000c0000            add       eax,0C00h
  0041b8b1  66894628              mov       [esi+28h],ax
  0041b8b5  6689462a              mov       [esi+2Ah],ax
  0041b8b9  e882b81200            call      00547140h
  0041b8be  99                    cdq
  0041b8bf  33c2                  xor       eax,edx
  0041b8c1  2bc2                  sub       eax,edx
  0041b8c3  25ff0f0000            and       eax,0FFFh
  0041b8c8  33c2                  xor       eax,edx
  0041b8ca  2bc2                  sub       eax,edx
  0041b8cc  66894612              mov       [esi+12h],ax
  0041b8d0  e86bb81200            call      00547140h
  0041b8d5  99                    cdq
  0041b8d6  b91e000000            mov       ecx,1Eh
  0041b8db  885e38                mov       [esi+38h],bl
  0041b8de  f7f9                  idiv      ecx
  0041b8e0  885e39                mov       [esi+39h],bl
  0041b8e3  885e3a                mov       [esi+3Ah],bl
  0041b8e6  c7465c0e000000        mov       dword [esi+5Ch],0Eh
  0041b8ed  03d1                  add       edx,ecx
  0041b8ef  66895614              mov       [esi+14h],dx
  0041b8f3  e848b81200            call      00547140h
  0041b8f8  99                    cdq
  0041b8f9  b978000000            mov       ecx,78h
  0041b8fe  f7f9                  idiv      ecx
  0041b900  8b4620                mov       eax,[esi+20h]
  0041b903  c1e20c                shl       edx,0Ch
  0041b906  03c2                  add       eax,edx
  0041b908  894620                mov       [esi+20h],eax

LAB_0041b90b:
  0041b90b  8b561c                mov       edx,[esi+1Ch]
  0041b90e  8b4620                mov       eax,[esi+20h]
  0041b911  8b4e24                mov       ecx,[esi+24h]
  0041b914  895650                mov       [esi+50h],edx
  0041b917  894654                mov       [esi+54h],eax
  0041b91a  894e58                mov       [esi+58h],ecx
  0041b91d  66c746160000          mov       word [esi+16h],0

LAB_0041b923:
  0041b923  66837e1001            cmp       word [esi+10h],1
  0041b928  0f85b8000000          jne       near 0041B9E6h
  0041b92e  660fb64638            movzx     ax,byte [esi+38h]
  0041b933  660fb64e39            movzx     cx,byte [esi+39h]
  0041b938  660fb6563a            movzx     dx,byte [esi+3Ah]
  0041b93d  83e803                sub       eax,3
  0041b940  83e903                sub       ecx,3
  0041b943  83ea03                sub       edx,3
  0041b946  6685c0                test      ax,ax
  0041b949  7d06                  jge       short 0041B951h
  0041b94b  33c0                  xor       eax,eax
  0041b94d  33c9                  xor       ecx,ecx
  0041b94f  33d2                  xor       edx,edx

LAB_0041b951:
  0041b951  6683461240            add       word [esi+12h],40h
  0041b956  884638                mov       [esi+38h],al
  0041b959  668b4612              mov       ax,[esi+12h]
  0041b95d  884e39                mov       [esi+39h],cl
  0041b960  663d0010              cmp       ax,1000h
  0041b964  88563a                mov       [esi+3Ah],dl
  0041b967  7c09                  jl        short 0041B972h
  0041b969  0500f0ffff            add       eax,0FFFFF000h
  0041b96e  66894612              mov       [esi+12h],ax

LAB_0041b972:
  0041b972  0fbf5612              movsx     edx,word [esi+12h]
  0041b976  52                    push      edx
  0041b977  e8a0a01200            call      00545A1Ch
  0041b97c  0fbf4e14              movsx     ecx,word [esi+14h]
  0041b980  0fafc1                imul      eax,ecx
  0041b983  0fbf5612              movsx     edx,word [esi+12h]
  0041b987  8b4e50                mov       ecx,[esi+50h]
  0041b98a  52                    push      edx
  0041b98b  03c1                  add       eax,ecx
  0041b98d  89461c                mov       [esi+1Ch],eax
  0041b990  e881a01200            call      00545A16h
  0041b995  0fbf4e14              movsx     ecx,word [esi+14h]
  0041b999  0fafc1                imul      eax,ecx
  0041b99c  8b5e58                mov       ebx,[esi+58h]
  0041b99f  8b5620                mov       edx,[esi+20h]
  0041b9a2  03c3                  add       eax,ebx
  0041b9a4  81c200a00000          add       edx,0A000h
  0041b9aa  66ff4616              inc       word [esi+16h]
  0041b9ae  66837e1602            cmp       word [esi+16h],2
  0041b9b3  894624                mov       [esi+24h],eax
  0041b9b6  895620                mov       [esi+20h],edx
  0041b9b9  7518                  jne       short 0041B9D3h
  0041b9bb  8a4e40                mov       cl,[esi+40h]
  0041b9be  66c746160000          mov       word [esi+16h],0
  0041b9c4  fec1                  inc       cl
  0041b9c6  8ac1                  mov       al,cl
  0041b9c8  884e40                mov       [esi+40h],cl
  0041b9cb  3c07                  cmp       al,7
  0041b9cd  7504                  jne       short 0041B9D3h
  0041b9cf  c6464000              mov       byte [esi+40h],0

LAB_0041b9d3:
  0041b9d3  8b4e5c                mov       ecx,[esi+5Ch]
  0041b9d6  49                    dec       ecx
  0041b9d7  8bc1                  mov       eax,ecx
  0041b9d9  894e5c                mov       [esi+5Ch],ecx
  0041b9dc  85c0                  test      eax,eax
  0041b9de  7f06                  jg        short 0041B9E6h
  0041b9e0  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0041b9e6:
  0041b9e6  5e                    pop       esi
  0041b9e7  5b                    pop       ebx
  0041b9e8  c3                    ret
  0041b9e9  90                    nop
  0041b9ea  90                    nop
  0041b9eb  90                    nop
  0041b9ec  90                    nop
  0041b9ed  90                    nop
  0041b9ee  90                    nop
  0041b9ef  90                    nop

7 cible(s) de saut interne : 0x0041b87b, 0x0041b90b, 0x0041b923, 0x0041b951, 0x0041b972, 0x0041b9d3, 0x0041b9e6
