C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045dd00  section .text  offset fichier 0x5d100  608 octets

  0045dd00  83ec18                sub       esp,18h
  0045dd03  53                    push      ebx
  0045dd04  55                    push      ebp
  0045dd05  56                    push      esi
  0045dd06  8b742428              mov       esi,[esp+28h]
  0045dd0a  57                    push      edi
  0045dd0b  33ff                  xor       edi,edi
  0045dd0d  66397e10              cmp       [esi+10h],di
  0045dd11  0f859e000000          jne       near 0045DDB5h
  0045dd17  0fbf4632              movsx     eax,word [esi+32h]
  0045dd1b  8b5e1c                mov       ebx,[esi+1Ch]
  0045dd1e  8b6e24                mov       ebp,[esi+24h]
  0045dd21  0500080000            add       eax,800h
  0045dd26  66c746100100          mov       word [esi+10h],1
  0045dd2c  50                    push      eax
  0045dd2d  66897e12              mov       [esi+12h],di
  0045dd31  66897e2c              mov       [esi+2Ch],di
  0045dd35  66897e30              mov       [esi+30h],di
  0045dd39  e8de7c0e00            call      00545A1Ch
  0045dd3e  8b561c                mov       edx,[esi+1Ch]
  0045dd41  8d0480                lea       eax,[eax+eax*4]
  0045dd44  8d0cc0                lea       ecx,[eax+eax*8]
  0045dd47  c1e102                shl       ecx,2
  0045dd4a  03d1                  add       edx,ecx
  0045dd4c  89561c                mov       [esi+1Ch],edx
  0045dd4f  0fbf5632              movsx     edx,word [esi+32h]
  0045dd53  81c200080000          add       edx,800h
  0045dd59  52                    push      edx
  0045dd5a  e8b77c0e00            call      00545A16h
  0045dd5f  8b5624                mov       edx,[esi+24h]
  0045dd62  8d0480                lea       eax,[eax+eax*4]
  0045dd65  6800800000            push      8000h
  0045dd6a  56                    push      esi
  0045dd6b  8d04c0                lea       eax,[eax+eax*8]
  0045dd6e  c1e002                shl       eax,2
  0045dd71  03d0                  add       edx,eax
  0045dd73  b800a00000            mov       eax,0A000h
  0045dd78  66898682000000        mov       [esi+82h],ax
  0045dd7f  66898686000000        mov       [esi+86h],ax
  0045dd86  6689868a000000        mov       [esi+8Ah],ax
  0045dd8d  6689868e000000        mov       [esi+8Eh],ax
  0045dd94  8b4620                mov       eax,[esi+20h]
  0045dd97  895624                mov       [esi+24h],edx
  0045dd9a  894664                mov       [esi+64h],eax
  0045dd9d  894660                mov       [esi+60h],eax
  0045dda0  8b0dc07b5f00          mov       ecx,[5F7BC0h]
  0045dda6  51                    push      ecx
  0045dda7  e824530d00            call      005330D0h
  0045ddac  83c40c                add       esp,0Ch
  0045ddaf  895e1c                mov       [esi+1Ch],ebx
  0045ddb2  896e24                mov       [esi+24h],ebp

LAB_0045ddb5:
  0045ddb5  bb01000000            mov       ebx,1
  0045ddba  66395e10              cmp       [esi+10h],bx
  0045ddbe  0f8594010000          jne       near 0045DF58h
  0045ddc4  a16ce05d00            mov       eax,[5DE06Ch]
  0045ddc9  8b5620                mov       edx,[esi+20h]
  0045ddcc  c1f80c                sar       eax,0Ch
  0045ddcf  c1fa0c                sar       edx,0Ch
  0045ddd2  2bc2                  sub       eax,edx
  0045ddd4  bd02000000            mov       ebp,2
  0045ddd9  3d70feffff            cmp       eax,0FFFFFE70h
  0045ddde  7c39                  jl        short 0045DE19h
  0045dde0  83f864                cmp       eax,64h
  0045dde3  7f34                  jg        short 0045DE19h
  0045dde5  66397e12              cmp       [esi+12h],di
  0045dde9  753a                  jne       short 0045DE25h
  0045ddeb  66895e12              mov       [esi+12h],bx
  0045ddef  a1cc975e00            mov       eax,[5E97CCh]
  0045ddf4  57                    push      edi
  0045ddf5  57                    push      edi
  0045ddf6  8b88a8000000          mov       ecx,[eax+0A8h]
  0045ddfc  6800180000            push      1800h
  0045de01  6800040000            push      400h
  0045de06  8b11                  mov       edx,[ecx]
  0045de08  660fb64228            movzx     ax,byte [edx+28h]
  0045de0d  50                    push      eax
  0045de0e  56                    push      esi
  0045de0f  e86ca20000            call      00468080h
  0045de14  83c418                add       esp,18h
  0045de17  eb0c                  jmp       short 0045DE25h

LAB_0045de19:
  0045de19  66396e12              cmp       [esi+12h],bp
  0045de1d  7506                  jne       short 0045DE25h
  0045de1f  66c746120300          mov       word [esi+12h],3

LAB_0045de25:
  0045de25  66395e12              cmp       [esi+12h],bx
  0045de29  751a                  jne       short 0045DE45h
  0045de2b  6681462c0001          add       word [esi+2Ch],100h
  0045de31  668b462c              mov       ax,[esi+2Ch]
  0045de35  663d0010              cmp       ax,1000h
  0045de39  7c0a                  jl        short 0045DE45h
  0045de3b  66c7462c0010          mov       word [esi+2Ch],1000h
  0045de41  66896e12              mov       [esi+12h],bp

LAB_0045de45:
  0045de45  66396e12              cmp       [esi+12h],bp
  0045de49  0f85ef000000          jne       near 0045DF3Eh
  0045de4f  e8ec920e00            call      00547140h
  0045de54  99                    cdq
  0045de55  b950000000            mov       ecx,50h
  0045de5a  f7f9                  idiv      ecx
  0045de5c  663bd7                cmp       dx,di
  0045de5f  0f85d9000000          jne       near 0045DF3Eh
  0045de65  8b4654                mov       eax,[esi+54h]
  0045de68  8b5650                mov       edx,[esi+50h]
  0045de6b  8b4e58                mov       ecx,[esi+58h]
  0045de6e  89442420              mov       [esp+20h],eax
  0045de72  668b4632              mov       ax,[esi+32h]
  0045de76  8954241c              mov       [esp+1Ch],edx
  0045de7a  8b561c                mov       edx,[esi+1Ch]
  0045de7d  89442418              mov       [esp+18h],eax
  0045de81  0500080000            add       eax,800h
  0045de86  894c2424              mov       [esp+24h],ecx
  0045de8a  8b4e20                mov       ecx,[esi+20h]
  0045de8d  66894632              mov       [esi+32h],ax
  0045de91  0fbfc0                movsx     eax,ax
  0045de94  8954242c              mov       [esp+2Ch],edx
  0045de98  8b5624                mov       edx,[esi+24h]
  0045de9b  50                    push      eax
  0045de9c  894c2414              mov       [esp+14h],ecx
  0045dea0  89542418              mov       [esp+18h],edx
  0045dea4  e8737b0e00            call      00545A1Ch
  0045dea9  0fbf5632              movsx     edx,word [esi+32h]
  0045dead  8b6e1c                mov       ebp,[esi+1Ch]
  0045deb0  8d0480                lea       eax,[eax+eax*4]
  0045deb3  52                    push      edx
  0045deb4  8d0cc0                lea       ecx,[eax+eax*8]
  0045deb7  c1e102                shl       ecx,2
  0045deba  03e9                  add       ebp,ecx
  0045debc  896e1c                mov       [esi+1Ch],ebp
  0045debf  e8527b0e00            call      00545A16h
  0045dec4  8b4e24                mov       ecx,[esi+24h]
  0045dec7  8d0480                lea       eax,[eax+eax*4]
  0045deca  8d04c0                lea       eax,[eax+eax*8]
  0045decd  c1e002                shl       eax,2
  0045ded0  03c8                  add       ecx,eax
  0045ded2  894e24                mov       [esi+24h],ecx

LAB_0045ded5:
  0045ded5  668b5e14              mov       bx,[esi+14h]
  0045ded9  668b6e16              mov       bp,[esi+16h]
  0045dedd  6a00                  push      0
  0045dedf  6a00                  push      0
  0045dee1  6aff                  push      0FFFFFFFFh
  0045dee3  6a00                  push      0
  0045dee5  6a39                  push      39h
  0045dee7  56                    push      esi
  0045dee8  66897e14              mov       [esi+14h],di
  0045deec  66c746160a00          mov       word [esi+16h],0Ah
  0045def2  e819a20000            call      00468110h
  0045def7  83c418                add       esp,18h
  0045defa  47                    inc       edi
  0045defb  6683ff03              cmp       di,3
  0045deff  66895e14              mov       [esi+14h],bx
  0045df03  66896e16              mov       [esi+16h],bp
  0045df07  7ecc                  jle       short 0045DED5h
  0045df09  8b4c242c              mov       ecx,[esp+2Ch]
  0045df0d  8b542410              mov       edx,[esp+10h]
  0045df11  8b442414              mov       eax,[esp+14h]
  0045df15  894e1c                mov       [esi+1Ch],ecx
  0045df18  668b4c2418            mov       cx,[esp+18h]
  0045df1d  895620                mov       [esi+20h],edx
  0045df20  8b54241c              mov       edx,[esp+1Ch]
  0045df24  894624                mov       [esi+24h],eax
  0045df27  8b442420              mov       eax,[esp+20h]
  0045df2b  66894e32              mov       [esi+32h],cx
  0045df2f  8b4c2424              mov       ecx,[esp+24h]
  0045df33  895650                mov       [esi+50h],edx
  0045df36  894654                mov       [esi+54h],eax
  0045df39  894e58                mov       [esi+58h],ecx
  0045df3c  33ff                  xor       edi,edi

LAB_0045df3e:
  0045df3e  66837e1203            cmp       word [esi+12h],3
  0045df43  7513                  jne       short 0045DF58h
  0045df45  6683462c80            add       word [esi+2Ch],0FF80h
  0045df4a  66397e2c              cmp       [esi+2Ch],di
  0045df4e  7f08                  jg        short 0045DF58h
  0045df50  66897e2c              mov       [esi+2Ch],di
  0045df54  66897e12              mov       [esi+12h],di

LAB_0045df58:
  0045df58  5f                    pop       edi
  0045df59  5e                    pop       esi
  0045df5a  5d                    pop       ebp
  0045df5b  5b                    pop       ebx
  0045df5c  83c418                add       esp,18h
  0045df5f  c3                    ret

7 cible(s) de saut interne : 0x0045ddb5, 0x0045de19, 0x0045de25, 0x0045de45, 0x0045ded5, 0x0045df3e, 0x0045df58
