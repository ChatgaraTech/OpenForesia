C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004396e0  section .text  offset fichier 0x38ae0  1300 octets

  004396e0  83ec1c                sub       esp,1Ch
  004396e3  53                    push      ebx
  004396e4  55                    push      ebp
  004396e5  56                    push      esi
  004396e6  8b74242c              mov       esi,[esp+2Ch]
  004396ea  33ed                  xor       ebp,ebp
  004396ec  57                    push      edi
  004396ed  8b5e74                mov       ebx,[esi+74h]
  004396f0  66396e10              cmp       [esi+10h],bp
  004396f4  895c2410              mov       [esp+10h],ebx
  004396f8  7540                  jne       short 0043973Ah
  004396fa  806636fe              and       byte [esi+36h],0FEh
  004396fe  b8fa000000            mov       eax,0FAh
  00439703  c6460400              mov       byte [esi+4],0
  00439707  c6460a03              mov       byte [esi+0Ah],3
  0043970b  c6460b01              mov       byte [esi+0Bh],1
  0043970f  66c746100100          mov       word [esi+10h],1
  00439715  66894628              mov       [esi+28h],ax
  00439719  6689462c              mov       [esi+2Ch],ax
  0043971d  66896e2a              mov       [esi+2Ah],bp
  00439721  66c746120a00          mov       word [esi+12h],0Ah
  00439727  c6463896              mov       byte [esi+38h],96h
  0043972b  c6463900              mov       byte [esi+39h],0
  0043972f  c6463ac8              mov       byte [esi+3Ah],0C8h
  00439733  66896e16              mov       [esi+16h],bp
  00439737  896e44                mov       [esi+44h],ebp

LAB_0043973a:
  0043973a  66837e1001            cmp       word [esi+10h],1
  0043973f  0f8549020000          jne       near 0043998Eh
  00439745  668b4332              mov       ax,[ebx+32h]
  00439749  66c746340004          mov       word [esi+34h],400h
  0043974f  6605000c              add       ax,0C00h
  00439753  8d7e1c                lea       edi,[esi+1Ch]
  00439756  66894632              mov       [esi+32h],ax
  0043975a  8b4b20                mov       ecx,[ebx+20h]
  0043975d  0fbfd0                movsx     edx,ax
  00439760  8b465c                mov       eax,[esi+5Ch]
  00439763  81c100300700          add       ecx,73000h
  00439769  894e20                mov       [esi+20h],ecx
  0043976c  8d8c1000f4ffff        lea       ecx,[eax+edx-0C00h]
  00439773  51                    push      ecx
  00439774  e8a3c21000            call      00545A1Ch
  00439779  8bd0                  mov       edx,eax
  0043977b  c1e203                shl       edx,3
  0043977e  2bd0                  sub       edx,eax
  00439780  8b431c                mov       eax,[ebx+1Ch]
  00439783  8d0c50                lea       ecx,[eax+edx*2]
  00439786  8b465c                mov       eax,[esi+5Ch]
  00439789  0fbf5632              movsx     edx,word [esi+32h]
  0043978d  890f                  mov       [edi],ecx
  0043978f  8d8c1000f4ffff        lea       ecx,[eax+edx-0C00h]
  00439796  51                    push      ecx
  00439797  e87ac21000            call      00545A16h
  0043979c  8bd0                  mov       edx,eax
  0043979e  c1e203                shl       edx,3
  004397a1  2bd0                  sub       edx,eax
  004397a3  8b4324                mov       eax,[ebx+24h]
  004397a6  66837e120a            cmp       word [esi+12h],0Ah
  004397ab  8d0c50                lea       ecx,[eax+edx*2]
  004397ae  894e24                mov       [esi+24h],ecx
  004397b1  7513                  jne       short 004397C6h
  004397b3  6aff                  push      0FFFFFFFFh
  004397b5  55                    push      ebp
  004397b6  68ec000000            push      0ECh
  004397bb  55                    push      ebp
  004397bc  57                    push      edi
  004397bd  56                    push      esi
  004397be  e87d140400            call      0047AC40h
  004397c3  83c418                add       esp,18h

LAB_004397c6:
  004397c6  8b17                  mov       edx,[edi]
  004397c8  8b4620                mov       eax,[esi+20h]
  004397cb  8b4e24                mov       ecx,[esi+24h]
  004397ce  89542420              mov       [esp+20h],edx
  004397d2  0fbf5632              movsx     edx,word [esi+32h]
  004397d6  81ea000c0000          sub       edx,0C00h
  004397dc  89442424              mov       [esp+24h],eax
  004397e0  52                    push      edx
  004397e1  894c242c              mov       [esp+2Ch],ecx
  004397e5  e832c21000            call      00545A1Ch
  004397ea  0fbf4e16              movsx     ecx,word [esi+16h]
  004397ee  0fbf5632              movsx     edx,word [esi+32h]
  004397f2  0fafc1                imul      eax,ecx
  004397f5  8b0f                  mov       ecx,[edi]
  004397f7  81ea000c0000          sub       edx,0C00h
  004397fd  03c8                  add       ecx,eax
  004397ff  52                    push      edx
  00439800  890f                  mov       [edi],ecx
  00439802  e80fc21000            call      00545A16h
  00439807  0fbf4e16              movsx     ecx,word [esi+16h]
  0043980b  0fafc1                imul      eax,ecx
  0043980e  8b4e24                mov       ecx,[esi+24h]
  00439811  896c2414              mov       [esp+14h],ebp
  00439815  03c8                  add       ecx,eax
  00439817  b8c0935e00            mov       eax,5E93C0h
  0043981c  894e24                mov       [esi+24h],ecx
  0043981f  89442430              mov       [esp+30h],eax
  00439823  c744241804000000      mov       dword [esp+18h],4

LAB_0043982b:
  0043982b  8b08                  mov       ecx,[eax]
  0043982d  f7c100000080          test      ecx,80000000h
  00439833  0f84b3000000          je        near 004398ECh
  00439839  f7c100000050          test      ecx,50000000h
  0043983f  0f85a7000000          jne       near 004398ECh
  00439845  8b781c                mov       edi,[eax+1Ch]
  00439848  8b6e1c                mov       ebp,[esi+1Ch]
  0043984b  8b5820                mov       ebx,[eax+20h]
  0043984e  8b5620                mov       edx,[esi+20h]
  00439851  2bfd                  sub       edi,ebp
  00439853  8b6824                mov       ebp,[eax+24h]
  00439856  8b4624                mov       eax,[esi+24h]
  00439859  2bda                  sub       ebx,edx
  0043985b  2be8                  sub       ebp,eax
  0043985d  c1fd0c                sar       ebp,0Ch
  00439860  c1ff0c                sar       edi,0Ch
  00439863  55                    push      ebp
  00439864  57                    push      edi
  00439865  c1fb0c                sar       ebx,0Ch
  00439868  e8b5c11000            call      00545A22h
  0043986d  8b542410              mov       edx,[esp+10h]
  00439871  0fbf4a32              movsx     ecx,word [edx+32h]
  00439875  2bc1                  sub       eax,ecx
  00439877  8bd7                  mov       edx,edi
  00439879  25ff0f0000            and       eax,0FFFh
  0043987e  0fafd7                imul      edx,edi
  00439881  8944241c              mov       [esp+1Ch],eax
  00439885  8bc3                  mov       eax,ebx
  00439887  0fafc3                imul      eax,ebx
  0043988a  8bcd                  mov       ecx,ebp
  0043988c  03d0                  add       edx,eax
  0043988e  0fafcd                imul      ecx,ebp
  00439891  03d1                  add       edx,ecx
  00439893  52                    push      edx
  00439894  e877c11000            call      00545A10h
  00439899  3dc8000000            cmp       eax,0C8h
  0043989e  7d48                  jge       short 004398E8h
  004398a0  8b44241c              mov       eax,[esp+1Ch]
  004398a4  3d20030000            cmp       eax,320h
  004398a9  7d04                  jge       short 004398AFh
  004398ab  85c0                  test      eax,eax
  004398ad  7f0e                  jg        short 004398BDh

LAB_004398af:
  004398af  3d00100000            cmp       eax,1000h
  004398b4  7d32                  jge       short 004398E8h
  004398b6  3de00c0000            cmp       eax,0CE0h
  004398bb  7e2b                  jle       short 004398E8h

LAB_004398bd:
  004398bd  8b4c2414              mov       ecx,[esp+14h]
  004398c1  b801000000            mov       eax,1
  004398c6  d3e0                  shl       eax,cl
  004398c8  8b4e44                mov       ecx,[esi+44h]
  004398cb  85c1                  test      ecx,eax
  004398cd  7519                  jne       short 004398E8h
  004398cf  8b542430              mov       edx,[esp+30h]
  004398d3  0bc1                  or        eax,ecx
  004398d5  894644                mov       [esi+44h],eax
  004398d8  8b442410              mov       eax,[esp+10h]
  004398dc  6a00                  push      0
  004398de  52                    push      edx
  004398df  50                    push      eax
  004398e0  e8db010000            call      00439AC0h
  004398e5  83c40c                add       esp,0Ch

LAB_004398e8:
  004398e8  8b442430              mov       eax,[esp+30h]

LAB_004398ec:
  004398ec  8b7c2414              mov       edi,[esp+14h]
  004398f0  8b4c2418              mov       ecx,[esp+18h]
  004398f4  47                    inc       edi
  004398f5  059c000000            add       eax,9Ch
  004398fa  49                    dec       ecx
  004398fb  897c2414              mov       [esp+14h],edi
  004398ff  89442430              mov       [esp+30h],eax
  00439903  894c2418              mov       [esp+18h],ecx
  00439907  0f851effffff          jne       near 0043982Bh
  0043990d  56                    push      esi
  0043990e  e82da00f00            call      00533940h
  00439913  8b44242c              mov       eax,[esp+2Ch]
  00439917  8b4c2424              mov       ecx,[esp+24h]
  0043991b  8b542428              mov       edx,[esp+28h]
  0043991f  894624                mov       [esi+24h],eax
  00439922  668b462a              mov       ax,[esi+2Ah]
  00439926  894e1c                mov       [esi+1Ch],ecx
  00439929  0fbfc8                movsx     ecx,ax
  0043992c  895620                mov       [esi+20h],edx
  0043992f  ba00400000            mov       edx,4000h
  00439934  2bd1                  sub       edx,ecx
  00439936  83c404                add       esp,4
  00439939  c1fa02                sar       edx,2
  0043993c  03d0                  add       edx,eax
  0043993e  668b4616              mov       ax,[esi+16h]
  00439942  0fbfc8                movsx     ecx,ax
  00439945  6689562a              mov       [esi+2Ah],dx
  00439949  ba58020000            mov       edx,258h
  0043994e  2bd1                  sub       edx,ecx
  00439950  c1fa02                sar       edx,2
  00439953  03d0                  add       edx,eax
  00439955  8b8690000000          mov       eax,[esi+90h]
  0043995b  a902880000            test      eax,8802h
  00439960  66895616              mov       [esi+16h],dx
  00439964  740c                  je        short 00439972h
  00439966  66c746100200          mov       word [esi+10h],2
  0043996c  66c746120a00          mov       word [esi+12h],0Ah

LAB_00439972:
  00439972  66ff4e12              dec       word [esi+12h]
  00439976  33ed                  xor       ebp,ebp
  00439978  66396e12              cmp       [esi+12h],bp
  0043997c  7f0c                  jg        short 0043998Ah
  0043997e  66c746100200          mov       word [esi+10h],2
  00439984  66c746120a00          mov       word [esi+12h],0Ah

LAB_0043998a:
  0043998a  8b5c2410              mov       ebx,[esp+10h]

LAB_0043998e:
  0043998e  66837e1002            cmp       word [esi+10h],2
  00439993  0f8512010000          jne       near 00439AABh
  00439999  668b4332              mov       ax,[ebx+32h]
  0043999d  66c746340004          mov       word [esi+34h],400h
  004399a3  0fbf969a000000        movsx     edx,word [esi+9Ah]
  004399aa  6605000c              add       ax,0C00h
  004399ae  66894632              mov       [esi+32h],ax
  004399b2  8b4b20                mov       ecx,[ebx+20h]
  004399b5  0fbfc0                movsx     eax,ax
  004399b8  81c100300700          add       ecx,73000h
  004399be  894e20                mov       [esi+20h],ecx
  004399c1  8d8c0200f4ffff        lea       ecx,[edx+eax-0C00h]
  004399c8  51                    push      ecx
  004399c9  e84ec01000            call      00545A1Ch
  004399ce  8bd0                  mov       edx,eax
  004399d0  c1e203                shl       edx,3
  004399d3  2bd0                  sub       edx,eax
  004399d5  8b431c                mov       eax,[ebx+1Ch]
  004399d8  8d0c50                lea       ecx,[eax+edx*2]
  004399db  0fbf969a000000        movsx     edx,word [esi+9Ah]
  004399e2  0fbf4632              movsx     eax,word [esi+32h]
  004399e6  894e1c                mov       [esi+1Ch],ecx
  004399e9  8d8c0200f4ffff        lea       ecx,[edx+eax-0C00h]
  004399f0  51                    push      ecx
  004399f1  e820c01000            call      00545A16h
  004399f6  8bd0                  mov       edx,eax
  004399f8  c1e203                shl       edx,3
  004399fb  2bd0                  sub       edx,eax
  004399fd  8b4324                mov       eax,[ebx+24h]
  00439a00  8d0c50                lea       ecx,[eax+edx*2]
  00439a03  0fbf5632              movsx     edx,word [esi+32h]
  00439a07  81ea000c0000          sub       edx,0C00h
  00439a0d  894e24                mov       [esi+24h],ecx
  00439a10  52                    push      edx
  00439a11  e806c01000            call      00545A1Ch
  00439a16  0fbf4e16              movsx     ecx,word [esi+16h]
  00439a1a  0fbf5632              movsx     edx,word [esi+32h]
  00439a1e  0fafc1                imul      eax,ecx
  00439a21  8b4e1c                mov       ecx,[esi+1Ch]
  00439a24  81ea000c0000          sub       edx,0C00h
  00439a2a  03c8                  add       ecx,eax
  00439a2c  52                    push      edx
  00439a2d  894e1c                mov       [esi+1Ch],ecx
  00439a30  e8e1bf1000            call      00545A16h
  00439a35  0fbf4e16              movsx     ecx,word [esi+16h]
  00439a39  0fafc1                imul      eax,ecx
  00439a3c  8b4e24                mov       ecx,[esi+24h]
  00439a3f  66c74634000c          mov       word [esi+34h],0C00h
  00439a45  03c8                  add       ecx,eax
  00439a47  668b462a              mov       ax,[esi+2Ah]
  00439a4b  0fbfd0                movsx     edx,ax
  00439a4e  f7da                  neg       edx
  00439a50  c1fa02                sar       edx,2
  00439a53  03d0                  add       edx,eax
  00439a55  8a4638                mov       al,[esi+38h]
  00439a58  894e24                mov       [esi+24h],ecx
  00439a5b  8bc8                  mov       ecx,eax
  00439a5d  81e1ff000000          and       ecx,0FFh
  00439a63  6689562a              mov       [esi+2Ah],dx
  00439a67  f7d9                  neg       ecx
  00439a69  c1f903                sar       ecx,3
  00439a6c  02c8                  add       cl,al
  00439a6e  8a4639                mov       al,[esi+39h]
  00439a71  8bd0                  mov       edx,eax
  00439a73  884e38                mov       [esi+38h],cl
  00439a76  81e2ff000000          and       edx,0FFh
  00439a7c  f7da                  neg       edx
  00439a7e  c1fa03                sar       edx,3
  00439a81  02d0                  add       dl,al
  00439a83  8a463a                mov       al,[esi+3Ah]
  00439a86  8bc8                  mov       ecx,eax
  00439a88  885639                mov       [esi+39h],dl
  00439a8b  81e1ff000000          and       ecx,0FFh
  00439a91  f7d9                  neg       ecx
  00439a93  c1f903                sar       ecx,3
  00439a96  02c8                  add       cl,al
  00439a98  66ff4e12              dec       word [esi+12h]
  00439a9c  66396e12              cmp       [esi+12h],bp
  00439aa0  884e3a                mov       [esi+3Ah],cl
  00439aa3  7f06                  jg        short 00439AABh
  00439aa5  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00439aab:
  00439aab  5f                    pop       edi
  00439aac  5e                    pop       esi
  00439aad  5d                    pop       ebp
  00439aae  5b                    pop       ebx
  00439aaf  83c41c                add       esp,1Ch
  00439ab2  c3                    ret
  00439ab3  90                    nop
  00439ab4  90                    nop
  00439ab5  90                    nop
  00439ab6  90                    nop
  00439ab7  90                    nop
  00439ab8  90                    nop
  00439ab9  90                    nop
  00439aba  90                    nop
  00439abb  90                    nop
  00439abc  90                    nop
  00439abd  90                    nop
  00439abe  90                    nop
  00439abf  90                    nop
  00439ac0  56                    push      esi
  00439ac1  8b74240c              mov       esi,[esp+0Ch]
  00439ac5  57                    push      edi
  00439ac6  8b7c240c              mov       edi,[esp+0Ch]
  00439aca  56                    push      esi
  00439acb  57                    push      edi
  00439acc  e8dfc9ffff            call      004364B0h
  00439ad1  83c408                add       esp,8
  00439ad4  6685c0                test      ax,ax
  00439ad7  751d                  jne       short 00439AF6h
  00439ad9  8b442414              mov       eax,[esp+14h]
  00439add  50                    push      eax
  00439ade  56                    push      esi
  00439adf  57                    push      edi
  00439ae0  e8cb191000            call      0053B4B0h
  00439ae5  83c40c                add       esp,0Ch
  00439ae8  33c9                  xor       ecx,ecx
  00439aea  6685c0                test      ax,ax
  00439aed  0f95c1                setne     cl
  00439af0  668bc1                mov       ax,cx
  00439af3  5f                    pop       edi
  00439af4  5e                    pop       esi
  00439af5  c3                    ret

LAB_00439af6:
  00439af6  5f                    pop       edi
  00439af7  66b80100              mov       ax,1
  00439afb  5e                    pop       esi
  00439afc  c3                    ret
  00439afd  90                    nop
  00439afe  90                    nop
  00439aff  90                    nop
  00439b00  56                    push      esi
  00439b01  8b742408              mov       esi,[esp+8]
  00439b05  57                    push      edi
  00439b06  8b7e74                mov       edi,[esi+74h]
  00439b09  8b565c                mov       edx,[esi+5Ch]
  00439b0c  0fbf4e32              movsx     ecx,word [esi+32h]
  00439b10  8b4720                mov       eax,[edi+20h]
  00439b13  0500300700            add       eax,73000h
  00439b18  894620                mov       [esi+20h],eax
  00439b1b  8d840a00f4ffff        lea       eax,[edx+ecx-0C00h]
  00439b22  50                    push      eax
  00439b23  e8f4be1000            call      00545A1Ch
  00439b28  8b571c                mov       edx,[edi+1Ch]
  00439b2b  8bc8                  mov       ecx,eax
  00439b2d  c1e103                shl       ecx,3
  00439b30  2bc8                  sub       ecx,eax
  00439b32  8d044a                lea       eax,[edx+ecx*2]
  00439b35  8b565c                mov       edx,[esi+5Ch]
  00439b38  0fbf4e32              movsx     ecx,word [esi+32h]
  00439b3c  89461c                mov       [esi+1Ch],eax
  00439b3f  8d840a00f4ffff        lea       eax,[edx+ecx-0C00h]
  00439b46  50                    push      eax
  00439b47  e8cabe1000            call      00545A16h
  00439b4c  8b5724                mov       edx,[edi+24h]
  00439b4f  8bc8                  mov       ecx,eax
  00439b51  c1e103                shl       ecx,3
  00439b54  2bc8                  sub       ecx,eax
  00439b56  66837e1000            cmp       word [esi+10h],0
  00439b5b  bf01000000            mov       edi,1
  00439b60  8d044a                lea       eax,[edx+ecx*2]
  00439b63  894624                mov       [esi+24h],eax
  00439b66  7536                  jne       short 00439B9Eh
  00439b68  c7463c18115500        mov       dword [esi+3Ch],551118h
  00439b6f  e8ccd51000            call      00547140h
  00439b74  99                    cdq
  00439b75  b903000000            mov       ecx,3
  00439b7a  c6460a02              mov       byte [esi+0Ah],2
  00439b7e  f7f9                  idiv      ecx
  00439b80  b8a00f0000            mov       eax,0FA0h
  00439b85  c6460b00              mov       byte [esi+0Bh],0
  00439b89  66894628              mov       [esi+28h],ax
  00439b8d  6689462a              mov       [esi+2Ah],ax
  00439b91  66897e10              mov       [esi+10h],di
  00439b95  66c746120b00          mov       word [esi+12h],0Bh
  00439b9b  885640                mov       [esi+40h],dl

LAB_00439b9e:
  00439b9e  66397e10              cmp       [esi+10h],di
  00439ba2  7526                  jne       short 00439BCAh
  00439ba4  8a5640                mov       dl,[esi+40h]
  00439ba7  66ff4e12              dec       word [esi+12h]
  00439bab  668b4612              mov       ax,[esi+12h]
  00439baf  fec2                  inc       dl
  00439bb1  8aca                  mov       cl,dl
  00439bb3  885640                mov       [esi+40h],dl
  00439bb6  80f903                cmp       cl,3
  00439bb9  7204                  jb        short 00439BBFh
  00439bbb  c6464000              mov       byte [esi+40h],0

LAB_00439bbf:
  00439bbf  6685c0                test      ax,ax
  00439bc2  7f06                  jg        short 00439BCAh
  00439bc4  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00439bca:
  00439bca  5f                    pop       edi
  00439bcb  5e                    pop       esi
  00439bcc  c3                    ret
  00439bcd  90                    nop
  00439bce  90                    nop
  00439bcf  90                    nop
  00439bd0  53                    push      ebx
  00439bd1  55                    push      ebp
  00439bd2  56                    push      esi
  00439bd3  8b742410              mov       esi,[esp+10h]
  00439bd7  57                    push      edi
  00439bd8  8b466c                mov       eax,[esi+6Ch]
  00439bdb  8bf8                  mov       edi,eax
  00439bdd  85ff                  test      edi,edi
  00439bdf  7505                  jne       short 00439BE6h
  00439be1  bf58e55d00            mov       edi,5DE558h

LAB_00439be6:
  00439be6  894674                mov       [esi+74h],eax
  00439be9  8b471c                mov       eax,[edi+1Ch]
  00439bec  8d6e1c                lea       ebp,[esi+1Ch]
  00439bef  bb01000000            mov       ebx,1

16 cible(s) de saut interne : 0x0043973a, 0x004397c6, 0x0043982b, 0x004398af, 0x004398bd, 0x004398e8, 0x004398ec, 0x00439972, 0x0043998a, 0x0043998e, 0x00439aab, 0x00439af6, 0x00439b9e, 0x00439bbf, 0x00439bca, 0x00439be6
