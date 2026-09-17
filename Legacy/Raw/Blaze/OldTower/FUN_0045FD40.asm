C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045fd40  section .text  offset fichier 0x5f140  608 octets

  0045fd40  83ec18                sub       esp,18h
  0045fd43  53                    push      ebx
  0045fd44  55                    push      ebp
  0045fd45  56                    push      esi
  0045fd46  8b742428              mov       esi,[esp+28h]
  0045fd4a  57                    push      edi
  0045fd4b  33ff                  xor       edi,edi
  0045fd4d  66397e10              cmp       [esi+10h],di
  0045fd51  0f859e000000          jne       near 0045FDF5h
  0045fd57  0fbf4632              movsx     eax,word [esi+32h]
  0045fd5b  8b5e1c                mov       ebx,[esi+1Ch]
  0045fd5e  8b6e24                mov       ebp,[esi+24h]
  0045fd61  0500080000            add       eax,800h
  0045fd66  66c746100100          mov       word [esi+10h],1
  0045fd6c  50                    push      eax
  0045fd6d  66897e12              mov       [esi+12h],di
  0045fd71  66897e2c              mov       [esi+2Ch],di
  0045fd75  66897e30              mov       [esi+30h],di
  0045fd79  e89e5c0e00            call      00545A1Ch
  0045fd7e  8b561c                mov       edx,[esi+1Ch]
  0045fd81  8d0480                lea       eax,[eax+eax*4]
  0045fd84  8d0cc0                lea       ecx,[eax+eax*8]
  0045fd87  c1e102                shl       ecx,2
  0045fd8a  03d1                  add       edx,ecx
  0045fd8c  89561c                mov       [esi+1Ch],edx
  0045fd8f  0fbf5632              movsx     edx,word [esi+32h]
  0045fd93  81c200080000          add       edx,800h
  0045fd99  52                    push      edx
  0045fd9a  e8775c0e00            call      00545A16h
  0045fd9f  8b5624                mov       edx,[esi+24h]
  0045fda2  8d0480                lea       eax,[eax+eax*4]
  0045fda5  6800800000            push      8000h
  0045fdaa  56                    push      esi
  0045fdab  8d04c0                lea       eax,[eax+eax*8]
  0045fdae  c1e002                shl       eax,2
  0045fdb1  03d0                  add       edx,eax
  0045fdb3  b800a00000            mov       eax,0A000h
  0045fdb8  66898682000000        mov       [esi+82h],ax
  0045fdbf  66898686000000        mov       [esi+86h],ax
  0045fdc6  6689868a000000        mov       [esi+8Ah],ax
  0045fdcd  6689868e000000        mov       [esi+8Eh],ax
  0045fdd4  8b4620                mov       eax,[esi+20h]
  0045fdd7  895624                mov       [esi+24h],edx
  0045fdda  894664                mov       [esi+64h],eax
  0045fddd  894660                mov       [esi+60h],eax
  0045fde0  8b0dc07b5f00          mov       ecx,[5F7BC0h]
  0045fde6  51                    push      ecx
  0045fde7  e8e4320d00            call      005330D0h
  0045fdec  83c40c                add       esp,0Ch
  0045fdef  895e1c                mov       [esi+1Ch],ebx
  0045fdf2  896e24                mov       [esi+24h],ebp

LAB_0045fdf5:
  0045fdf5  bb01000000            mov       ebx,1
  0045fdfa  66395e10              cmp       [esi+10h],bx
  0045fdfe  0f8594010000          jne       near 0045FF98h
  0045fe04  a16ce05d00            mov       eax,[5DE06Ch]
  0045fe09  8b5620                mov       edx,[esi+20h]
  0045fe0c  c1f80c                sar       eax,0Ch
  0045fe0f  c1fa0c                sar       edx,0Ch
  0045fe12  2bc2                  sub       eax,edx
  0045fe14  bd02000000            mov       ebp,2
  0045fe19  3d70feffff            cmp       eax,0FFFFFE70h
  0045fe1e  7c39                  jl        short 0045FE59h
  0045fe20  83f864                cmp       eax,64h
  0045fe23  7f34                  jg        short 0045FE59h
  0045fe25  66397e12              cmp       [esi+12h],di
  0045fe29  753a                  jne       short 0045FE65h
  0045fe2b  66895e12              mov       [esi+12h],bx
  0045fe2f  a1cc975e00            mov       eax,[5E97CCh]
  0045fe34  57                    push      edi
  0045fe35  57                    push      edi
  0045fe36  8b88a8000000          mov       ecx,[eax+0A8h]
  0045fe3c  6800180000            push      1800h
  0045fe41  6800040000            push      400h
  0045fe46  8b11                  mov       edx,[ecx]
  0045fe48  660fb64228            movzx     ax,byte [edx+28h]
  0045fe4d  50                    push      eax
  0045fe4e  56                    push      esi
  0045fe4f  e82c820000            call      00468080h
  0045fe54  83c418                add       esp,18h
  0045fe57  eb0c                  jmp       short 0045FE65h

LAB_0045fe59:
  0045fe59  66396e12              cmp       [esi+12h],bp
  0045fe5d  7506                  jne       short 0045FE65h
  0045fe5f  66c746120300          mov       word [esi+12h],3

LAB_0045fe65:
  0045fe65  66395e12              cmp       [esi+12h],bx
  0045fe69  751a                  jne       short 0045FE85h
  0045fe6b  6681462c0001          add       word [esi+2Ch],100h
  0045fe71  668b462c              mov       ax,[esi+2Ch]
  0045fe75  663d0010              cmp       ax,1000h
  0045fe79  7c0a                  jl        short 0045FE85h
  0045fe7b  66c7462c0010          mov       word [esi+2Ch],1000h
  0045fe81  66896e12              mov       [esi+12h],bp

LAB_0045fe85:
  0045fe85  66396e12              cmp       [esi+12h],bp
  0045fe89  0f85ef000000          jne       near 0045FF7Eh
  0045fe8f  e8ac720e00            call      00547140h
  0045fe94  99                    cdq
  0045fe95  b950000000            mov       ecx,50h
  0045fe9a  f7f9                  idiv      ecx
  0045fe9c  663bd7                cmp       dx,di
  0045fe9f  0f85d9000000          jne       near 0045FF7Eh
  0045fea5  8b4654                mov       eax,[esi+54h]
  0045fea8  8b5650                mov       edx,[esi+50h]
  0045feab  8b4e58                mov       ecx,[esi+58h]
  0045feae  89442420              mov       [esp+20h],eax
  0045feb2  668b4632              mov       ax,[esi+32h]
  0045feb6  8954241c              mov       [esp+1Ch],edx
  0045feba  8b561c                mov       edx,[esi+1Ch]
  0045febd  89442418              mov       [esp+18h],eax
  0045fec1  0500080000            add       eax,800h
  0045fec6  894c2424              mov       [esp+24h],ecx
  0045feca  8b4e20                mov       ecx,[esi+20h]
  0045fecd  66894632              mov       [esi+32h],ax
  0045fed1  0fbfc0                movsx     eax,ax
  0045fed4  8954242c              mov       [esp+2Ch],edx
  0045fed8  8b5624                mov       edx,[esi+24h]
  0045fedb  50                    push      eax
  0045fedc  894c2414              mov       [esp+14h],ecx
  0045fee0  89542418              mov       [esp+18h],edx
  0045fee4  e8335b0e00            call      00545A1Ch
  0045fee9  0fbf5632              movsx     edx,word [esi+32h]
  0045feed  8b6e1c                mov       ebp,[esi+1Ch]
  0045fef0  8d0480                lea       eax,[eax+eax*4]
  0045fef3  52                    push      edx
  0045fef4  8d0cc0                lea       ecx,[eax+eax*8]
  0045fef7  c1e102                shl       ecx,2
  0045fefa  03e9                  add       ebp,ecx
  0045fefc  896e1c                mov       [esi+1Ch],ebp
  0045feff  e8125b0e00            call      00545A16h
  0045ff04  8b4e24                mov       ecx,[esi+24h]
  0045ff07  8d0480                lea       eax,[eax+eax*4]
  0045ff0a  8d04c0                lea       eax,[eax+eax*8]
  0045ff0d  c1e002                shl       eax,2
  0045ff10  03c8                  add       ecx,eax
  0045ff12  894e24                mov       [esi+24h],ecx

LAB_0045ff15:
  0045ff15  668b5e14              mov       bx,[esi+14h]
  0045ff19  668b6e16              mov       bp,[esi+16h]
  0045ff1d  6a00                  push      0
  0045ff1f  6a00                  push      0
  0045ff21  6aff                  push      0FFFFFFFFh
  0045ff23  6a00                  push      0
  0045ff25  6a38                  push      38h
  0045ff27  56                    push      esi
  0045ff28  66897e14              mov       [esi+14h],di
  0045ff2c  66c746160a00          mov       word [esi+16h],0Ah
  0045ff32  e8d9810000            call      00468110h
  0045ff37  83c418                add       esp,18h
  0045ff3a  47                    inc       edi
  0045ff3b  6683ff03              cmp       di,3
  0045ff3f  66895e14              mov       [esi+14h],bx
  0045ff43  66896e16              mov       [esi+16h],bp
  0045ff47  7ecc                  jle       short 0045FF15h
  0045ff49  8b4c242c              mov       ecx,[esp+2Ch]
  0045ff4d  8b542410              mov       edx,[esp+10h]
  0045ff51  8b442414              mov       eax,[esp+14h]
  0045ff55  894e1c                mov       [esi+1Ch],ecx
  0045ff58  668b4c2418            mov       cx,[esp+18h]
  0045ff5d  895620                mov       [esi+20h],edx
  0045ff60  8b54241c              mov       edx,[esp+1Ch]
  0045ff64  894624                mov       [esi+24h],eax
  0045ff67  8b442420              mov       eax,[esp+20h]
  0045ff6b  66894e32              mov       [esi+32h],cx
  0045ff6f  8b4c2424              mov       ecx,[esp+24h]
  0045ff73  895650                mov       [esi+50h],edx
  0045ff76  894654                mov       [esi+54h],eax
  0045ff79  894e58                mov       [esi+58h],ecx
  0045ff7c  33ff                  xor       edi,edi

LAB_0045ff7e:
  0045ff7e  66837e1203            cmp       word [esi+12h],3
  0045ff83  7513                  jne       short 0045FF98h
  0045ff85  6683462c80            add       word [esi+2Ch],0FF80h
  0045ff8a  66397e2c              cmp       [esi+2Ch],di
  0045ff8e  7f08                  jg        short 0045FF98h
  0045ff90  66897e2c              mov       [esi+2Ch],di
  0045ff94  66897e12              mov       [esi+12h],di

LAB_0045ff98:
  0045ff98  5f                    pop       edi
  0045ff99  5e                    pop       esi
  0045ff9a  5d                    pop       ebp
  0045ff9b  5b                    pop       ebx
  0045ff9c  83c418                add       esp,18h
  0045ff9f  c3                    ret

7 cible(s) de saut interne : 0x0045fdf5, 0x0045fe59, 0x0045fe65, 0x0045fe85, 0x0045ff15, 0x0045ff7e, 0x0045ff98
