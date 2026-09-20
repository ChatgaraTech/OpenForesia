C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00469480  section .text  offset fichier 0x68880  1856 octets

  00469480  83ec08                sub       esp,8
  00469483  53                    push      ebx
  00469484  55                    push      ebp
  00469485  56                    push      esi
  00469486  8b742418              mov       esi,[esp+18h]
  0046948a  33db                  xor       ebx,ebx
  0046948c  57                    push      edi
  0046948d  53                    push      ebx
  0046948e  53                    push      ebx
  0046948f  68e7030000            push      3E7h
  00469494  53                    push      ebx
  00469495  68e7030000            push      3E7h
  0046949a  56                    push      esi
  0046949b  e8c029fbff            call      0041BE60h
  004694a0  83c418                add       esp,18h
  004694a3  66395e10              cmp       [esi+10h],bx
  004694a7  7528                  jne       short 004694D1h
  004694a9  bd01000000            mov       ebp,1
  004694ae  891d2c185700          mov       [57182Ch],ebx
  004694b4  66896e10              mov       [esi+10h],bp
  004694b8  66895e12              mov       [esi+12h],bx
  004694bc  66895e28              mov       [esi+28h],bx
  004694c0  66895e2a              mov       [esi+2Ah],bx
  004694c4  66895e2c              mov       [esi+2Ch],bx
  004694c8  c7465c0a000000        mov       dword [esi+5Ch],0Ah
  004694cf  eb05                  jmp       short 004694D6h

LAB_004694d1:
  004694d1  bd01000000            mov       ebp,1

LAB_004694d6:
  004694d6  66396e10              cmp       [esi+10h],bp
  004694da  7519                  jne       short 004694F5h
  004694dc  66ff4612              inc       word [esi+12h]
  004694e0  66396e12              cmp       [esi+12h],bp
  004694e4  750f                  jne       short 004694F5h
  004694e6  bf02000000            mov       edi,2
  004694eb  66895e12              mov       [esi+12h],bx
  004694ef  66897e10              mov       [esi+10h],di
  004694f3  eb05                  jmp       short 004694FAh

LAB_004694f5:
  004694f5  bf02000000            mov       edi,2

LAB_004694fa:
  004694fa  66397e10              cmp       [esi+10h],di
  004694fe  754e                  jne       short 0046954Eh
  00469500  66834634e2            add       word [esi+34h],0FFE2h
  00469505  668b4634              mov       ax,[esi+34h]
  00469509  663bc3                cmp       ax,bx
  0046950c  7d09                  jge       short 00469517h
  0046950e  0500100000            add       eax,1000h
  00469513  66894634              mov       [esi+34h],ax

LAB_00469517:
  00469517  b9c8000000            mov       ecx,0C8h
  0046951c  66014e28              add       [esi+28h],cx
  00469520  668b4628              mov       ax,[esi+28h]
  00469524  66014e2a              add       [esi+2Ah],cx
  00469528  66014e2c              add       [esi+2Ch],cx
  0046952c  663d0010              cmp       ax,1000h
  00469530  7c1c                  jl        short 0046954Eh
  00469532  66c746280010          mov       word [esi+28h],1000h
  00469538  66c746100300          mov       word [esi+10h],3
  0046953e  66895e12              mov       [esi+12h],bx
  00469542  66c74614ffff          mov       word [esi+14h],0FFFFh
  00469548  66c74616e0fc          mov       word [esi+16h],0FCE0h

LAB_0046954e:
  0046954e  66837e1003            cmp       word [esi+10h],3
  00469553  0f8589030000          jne       near 004698E2h
  00469559  668b4614              mov       ax,[esi+14h]
  0046955d  663d0300              cmp       ax,3
  00469561  740a                  je        short 0046956Dh
  00469563  663dfdff              cmp       ax,0FFFDh
  00469567  0f85d5000000          jne       near 00469642h

LAB_0046956d:
  0046956d  f7d8                  neg       eax
  0046956f  0fbfc8                movsx     ecx,ax
  00469572  66894614              mov       [esi+14h],ax
  00469576  b856555555            mov       eax,55555556h
  0046957b  f7e9                  imul      ecx
  0046957d  8bc2                  mov       eax,edx
  0046957f  53                    push      ebx
  00469580  c1e81f                shr       eax,1Fh
  00469583  03d0                  add       edx,eax
  00469585  53                    push      ebx
  00469586  d1e2                  shl       edx,1
  00469588  68e7030000            push      3E7h
  0046958d  52                    push      edx
  0046958e  68e7030000            push      3E7h
  00469593  56                    push      esi
  00469594  e8c728fbff            call      0041BE60h
  00469599  83c418                add       esp,18h
  0046959c  897c2414              mov       [esp+14h],edi

LAB_004695a0:
  004695a0  e89bdb0d00            call      00547140h
  004695a5  99                    cdq
  004695a6  b996000000            mov       ecx,96h
  004695ab  f7f9                  idiv      ecx
  004695ad  8bfa                  mov       edi,edx
  004695af  83ef4b                sub       edi,4Bh
  004695b2  e889db0d00            call      00547140h
  004695b7  99                    cdq
  004695b8  b928000000            mov       ecx,28h
  004695bd  f7f9                  idiv      ecx
  004695bf  8bda                  mov       ebx,edx
  004695c1  83eb14                sub       ebx,14h
  004695c4  e877db0d00            call      00547140h
  004695c9  99                    cdq
  004695ca  b9c8000000            mov       ecx,0C8h
  004695cf  8b6e1c                mov       ebp,[esi+1Ch]
  004695d2  f7f9                  idiv      ecx
  004695d4  8b4620                mov       eax,[esi+20h]
  004695d7  8b4e24                mov       ecx,[esi+24h]
  004695da  83c39c                add       ebx,0FFFFFF9Ch
  004695dd  6a00                  push      0
  004695df  6a00                  push      0
  004695e1  6aff                  push      0FFFFFFFFh
  004695e3  c1e70c                shl       edi,0Ch
  004695e6  c1e30c                shl       ebx,0Ch
  004695e9  6a00                  push      0
  004695eb  03fd                  add       edi,ebp
  004695ed  03d8                  add       ebx,eax
  004695ef  6a36                  push      36h
  004695f1  56                    push      esi
  004695f2  89442434              mov       [esp+34h],eax
  004695f6  894c2428              mov       [esp+28h],ecx
  004695fa  897e1c                mov       [esi+1Ch],edi
  004695fd  895e20                mov       [esi+20h],ebx
  00469600  83ea64                sub       edx,64h
  00469603  83c2b0                add       edx,0FFFFFFB0h
  00469606  c1e20c                shl       edx,0Ch
  00469609  03d1                  add       edx,ecx
  0046960b  895624                mov       [esi+24h],edx
  0046960e  e8fdeaffff            call      00468110h
  00469613  8b442428              mov       eax,[esp+28h]
  00469617  8b542434              mov       edx,[esp+34h]
  0046961b  894624                mov       [esi+24h],eax
  0046961e  8b44242c              mov       eax,[esp+2Ch]
  00469622  83c418                add       esp,18h
  00469625  48                    dec       eax
  00469626  896e1c                mov       [esi+1Ch],ebp
  00469629  895620                mov       [esi+20h],edx
  0046962c  89442414              mov       [esp+14h],eax
  00469630  0f856affffff          jne       near 004695A0h
  00469636  33db                  xor       ebx,ebx
  00469638  bd01000000            mov       ebp,1
  0046963d  bf02000000            mov       edi,2

LAB_00469642:
  00469642  668b4e5c              mov       cx,[esi+5Ch]
  00469646  666bc9f6              imul      cx,0FFF6h
  0046964a  66014e34              add       [esi+34h],cx
  0046964e  668b4634              mov       ax,[esi+34h]
  00469652  663bc3                cmp       ax,bx
  00469655  7d09                  jge       short 00469660h
  00469657  0500100000            add       eax,1000h
  0046965c  66894634              mov       [esi+34h],ax

LAB_00469660:
  00469660  0fbf5632              movsx     edx,word [esi+32h]
  00469664  52                    push      edx
  00469665  e8b2c30d00            call      00545A1Ch
  0046966a  0faf465c              imul      eax,[esi+5Ch]
  0046966e  8b4e24                mov       ecx,[esi+24h]
  00469671  03c8                  add       ecx,eax
  00469673  0fbf4632              movsx     eax,word [esi+32h]
  00469677  50                    push      eax
  00469678  894e24                mov       [esi+24h],ecx
  0046967b  e896c30d00            call      00545A16h
  00469680  0faf465c              imul      eax,[esi+5Ch]
  00469684  8b4e1c                mov       ecx,[esi+1Ch]
  00469687  8b5624                mov       edx,[esi+24h]
  0046968a  2bc8                  sub       ecx,eax
  0046968c  0fbf4616              movsx     eax,word [esi+16h]
  00469690  894e1c                mov       [esi+1Ch],ecx
  00469693  8bc8                  mov       ecx,eax
  00469695  c1e10c                shl       ecx,0Ch
  00469698  3bd1                  cmp       edx,ecx
  0046969a  7e41                  jle       short 004696DDh
  0046969c  8d8820030000          lea       ecx,[eax+320h]
  004696a2  b81f85eb51            mov       eax,51EB851Fh
  004696a7  f7e9                  imul      ecx
  004696a9  c1fa06                sar       edx,6
  004696ac  8bc2                  mov       eax,edx
  004696ae  8bca                  mov       ecx,edx
  004696b0  c1e81f                shr       eax,1Fh
  004696b3  03c8                  add       ecx,eax
  004696b5  a12c185700            mov       eax,[57182Ch]
  004696ba  8bd5                  mov       edx,ebp
  004696bc  d3e2                  shl       edx,cl
  004696be  0bc2                  or        eax,edx
  004696c0  a32c185700            mov       [57182Ch],eax
  004696c5  b8c8000000            mov       eax,0C8h
  004696ca  66014616              add       [esi+16h],ax
  004696ce  66394616              cmp       [esi+16h],ax
  004696d2  7506                  jne       short 004696DAh
  004696d4  66c746320004          mov       word [esi+32h],400h

LAB_004696da:
  004696da  ff465c                inc       dword [esi+5Ch]

LAB_004696dd:
  004696dd  668b4616              mov       ax,[esi+16h]
  004696e1  663d38ff              cmp       ax,0FF38h
  004696e5  7505                  jne       short 004696ECh
  004696e7  668346320f            add       word [esi+32h],0Fh

LAB_004696ec:
  004696ec  663bc3                cmp       ax,bx
  004696ef  7505                  jne       short 004696F6h
  004696f1  66834632ec            add       word [esi+32h],0FFECh

LAB_004696f6:
  004696f6  663dc800              cmp       ax,0C8h
  004696fa  bae7ffffff            mov       edx,0FFFFFFE7h
  004696ff  7504                  jne       short 00469705h
  00469701  66015632              add       [esi+32h],dx

LAB_00469705:
  00469705  663d9001              cmp       ax,190h
  00469709  b919000000            mov       ecx,19h
  0046970e  7504                  jne       short 00469714h
  00469710  66014e32              add       [esi+32h],cx

LAB_00469714:
  00469714  663d5802              cmp       ax,258h
  00469718  7505                  jne       short 0046971Fh
  0046971a  66834632dd            add       word [esi+32h],0FFDDh

LAB_0046971f:
  0046971f  663d2003              cmp       ax,320h
  00469723  7505                  jne       short 0046972Ah
  00469725  6683463223            add       word [esi+32h],23h

LAB_0046972a:
  0046972a  663de803              cmp       ax,3E8h
  0046972e  7504                  jne       short 00469734h
  00469730  66015632              add       [esi+32h],dx

LAB_00469734:
  00469734  663db004              cmp       ax,4B0h
  00469738  7504                  jne       short 0046973Eh
  0046973a  66014e32              add       [esi+32h],cx

LAB_0046973e:
  0046973e  663d0807              cmp       ax,708h
  00469742  7506                  jne       short 0046974Ah
  00469744  66c746100400          mov       word [esi+10h],4

LAB_0046974a:
  0046974a  66837e14ff            cmp       word [esi+14h],0FFFFh
  0046974f  7513                  jne       short 00469764h
  00469751  66ff4612              inc       word [esi+12h]
  00469755  66837e120a            cmp       word [esi+12h],0Ah
  0046975a  7508                  jne       short 00469764h
  0046975c  66895e14              mov       [esi+14h],bx
  00469760  66897e12              mov       [esi+12h],di

LAB_00469764:
  00469764  66395e14              cmp       [esi+14h],bx
  00469768  0f850c010000          jne       near 0046987Ah
  0046976e  66ff4612              inc       word [esi+12h]
  00469772  8b7e20                mov       edi,[esi+20h]
  00469775  0fbf4612              movsx     eax,word [esi+12h]
  00469779  8bc8                  mov       ecx,eax
  0046977b  c1e10c                shl       ecx,0Ch
  0046977e  2bf9                  sub       edi,ecx
  00469780  b90080dfff            mov       ecx,0FFDF8000h
  00469785  8bd7                  mov       edx,edi
  00469787  897e20                mov       [esi+20h],edi
  0046978a  3bd1                  cmp       edx,ecx
  0046978c  0f8fe8000000          jg        near 0046987Ah
  00469792  53                    push      ebx
  00469793  53                    push      ebx
  00469794  68e7030000            push      3E7h
  00469799  50                    push      eax
  0046979a  68e7030000            push      3E7h
  0046979f  56                    push      esi
  004697a0  894e20                mov       [esi+20h],ecx
  004697a3  66896e14              mov       [esi+14h],bp
  004697a7  e8b426fbff            call      0041BE60h
  004697ac  8b15cc975e00          mov       edx,[5E97CCh]
  004697b2  83c418                add       esp,18h
  004697b5  8b82a8000000          mov       eax,[edx+0A8h]
  004697bb  53                    push      ebx
  004697bc  53                    push      ebx
  004697bd  6800120000            push      1200h
  004697c2  8b08                  mov       ecx,[eax]
  004697c4  6800040000            push      400h
  004697c9  660fb6511a            movzx     dx,byte [ecx+1Ah]
  004697ce  52                    push      edx
  004697cf  56                    push      esi
  004697d0  e8abe8ffff            call      00468080h
  004697d5  83c418                add       esp,18h
  004697d8  c744241406000000      mov       dword [esp+14h],6

LAB_004697e0:
  004697e0  e85bd90d00            call      00547140h
  004697e5  99                    cdq
  004697e6  b996000000            mov       ecx,96h
  004697eb  f7f9                  idiv      ecx
  004697ed  8bfa                  mov       edi,edx
  004697ef  83ef4b                sub       edi,4Bh
  004697f2  e849d90d00            call      00547140h
  004697f7  99                    cdq
  004697f8  b928000000            mov       ecx,28h
  004697fd  f7f9                  idiv      ecx
  004697ff  8bda                  mov       ebx,edx
  00469801  83eb14                sub       ebx,14h
  00469804  e837d90d00            call      00547140h
  00469809  99                    cdq
  0046980a  b9c8000000            mov       ecx,0C8h
  0046980f  8b6e1c                mov       ebp,[esi+1Ch]
  00469812  f7f9                  idiv      ecx
  00469814  8b4620                mov       eax,[esi+20h]
  00469817  8b4e24                mov       ecx,[esi+24h]
  0046981a  83c39c                add       ebx,0FFFFFF9Ch
  0046981d  6a00                  push      0
  0046981f  6a00                  push      0
  00469821  6aff                  push      0FFFFFFFFh
  00469823  c1e70c                shl       edi,0Ch
  00469826  c1e30c                shl       ebx,0Ch
  00469829  6a00                  push      0
  0046982b  03fd                  add       edi,ebp
  0046982d  03d8                  add       ebx,eax
  0046982f  6a36                  push      36h
  00469831  56                    push      esi
  00469832  89442434              mov       [esp+34h],eax
  00469836  894c2428              mov       [esp+28h],ecx
  0046983a  897e1c                mov       [esi+1Ch],edi
  0046983d  895e20                mov       [esi+20h],ebx
  00469840  83ea64                sub       edx,64h
  00469843  c1e20c                shl       edx,0Ch
  00469846  03d1                  add       edx,ecx
  00469848  895624                mov       [esi+24h],edx
  0046984b  e8c0e8ffff            call      00468110h
  00469850  8b442428              mov       eax,[esp+28h]
  00469854  8b542434              mov       edx,[esp+34h]
  00469858  894624                mov       [esi+24h],eax
  0046985b  8b44242c              mov       eax,[esp+2Ch]
  0046985f  83c418                add       esp,18h
  00469862  48                    dec       eax
  00469863  896e1c                mov       [esi+1Ch],ebp
  00469866  895620                mov       [esi+20h],edx
  00469869  89442414              mov       [esp+14h],eax
  0046986d  0f856dffffff          jne       near 004697E0h
  00469873  33db                  xor       ebx,ebx
  00469875  bd01000000            mov       ebp,1

LAB_0046987a:
  0046987a  66396e14              cmp       [esi+14h],bp
  0046987e  7562                  jne       short 004698E2h
  00469880  66834612fe            add       word [esi+12h],0FFFEh
  00469885  668b4e12              mov       cx,[esi+12h]
  00469889  8b6e20                mov       ebp,[esi+20h]
  0046988c  0fbfc1                movsx     eax,cx
  0046988f  8bd0                  mov       edx,eax
  00469891  c1e20c                shl       edx,0Ch
  00469894  03ea                  add       ebp,edx
  00469896  663bcb                cmp       cx,bx
  00469899  896e20                mov       [esi+20h],ebp
  0046989c  8bd5                  mov       edx,ebp
  0046989e  7d42                  jge       short 004698E2h
  004698a0  81fa0080dfff          cmp       edx,0FFDF8000h
  004698a6  7f3a                  jg        short 004698E2h
  004698a8  53                    push      ebx
  004698a9  53                    push      ebx
  004698aa  68e7030000            push      3E7h
  004698af  50                    push      eax
  004698b0  68e7030000            push      3E7h
  004698b5  56                    push      esi
  004698b6  c746200080dfff        mov       dword [esi+20h],0FFDF8000h
  004698bd  66c746140300          mov       word [esi+14h],3
  004698c3  e89825fbff            call      0041BE60h
  004698c8  83c418                add       esp,18h
  004698cb  bf04000000            mov       edi,4

LAB_004698d0:
  004698d0  e86bd80d00            call      00547140h
  004698d5  e866d80d00            call      00547140h
  004698da  e861d80d00            call      00547140h
  004698df  4f                    dec       edi
  004698e0  75ee                  jne       short 004698D0h

LAB_004698e2:
  004698e2  66837e1004            cmp       word [esi+10h],4
  004698e7  752c                  jne       short 00469915h
  004698e9  b938ffffff            mov       ecx,0FFFFFF38h
  004698ee  66014e28              add       [esi+28h],cx
  004698f2  668b4628              mov       ax,[esi+28h]
  004698f6  66014e2a              add       [esi+2Ah],cx
  004698fa  66014e2c              add       [esi+2Ch],cx
  004698fe  663bc3                cmp       ax,bx
  00469901  7f12                  jg        short 00469915h
  00469903  66895e28              mov       [esi+28h],bx
  00469907  66895e2a              mov       [esi+2Ah],bx
  0046990b  66895e2c              mov       [esi+2Ch],bx
  0046990f  66c746100500          mov       word [esi+10h],5

LAB_00469915:
  00469915  53                    push      ebx
  00469916  53                    push      ebx
  00469917  53                    push      ebx
  00469918  6a0a                  push      0Ah
  0046991a  56                    push      esi
  0046991b  e82041ffff            call      0045DA40h
  00469920  83c414                add       esp,14h
  00469923  5f                    pop       edi
  00469924  5e                    pop       esi
  00469925  5d                    pop       ebp
  00469926  5b                    pop       ebx
  00469927  83c408                add       esp,8
  0046992a  c3                    ret
  0046992b  90                    nop
  0046992c  90                    nop
  0046992d  90                    nop
  0046992e  90                    nop
  0046992f  90                    nop
  00469930  a12c185700            mov       eax,[57182Ch]
  00469935  56                    push      esi
  00469936  8b742408              mov       esi,[esp+8]
  0046993a  8a4e14                mov       cl,[esi+14h]
  0046993d  d3e8                  shr       eax,cl
  0046993f  a801                  test      al,1
  00469941  0f849c000000          je        near 004699E3h
  00469947  66837e1000            cmp       word [esi+10h],0
  0046994c  755c                  jne       short 004699AAh
  0046994e  66c746100100          mov       word [esi+10h],1
  00469954  66c746120000          mov       word [esi+12h],0
  0046995a  e8e1d70d00            call      00547140h
  0046995f  99                    cdq
  00469960  b90a000000            mov       ecx,0Ah
  00469965  f7f9                  idiv      ecx
  00469967  83c208                add       edx,8
  0046996a  89565c                mov       [esi+5Ch],edx
  0046996d  e8ced70d00            call      00547140h
  00469972  99                    cdq
  00469973  b903000000            mov       ecx,3
  00469978  6a00                  push      0
  0046997a  f7f9                  idiv      ecx
  0046997c  6a00                  push      0
  0046997e  6800100000            push      1000h
  00469983  6800020000            push      200h
  00469988  4a                    dec       edx
  00469989  66895616              mov       [esi+16h],dx
  0046998d  8b15cc975e00          mov       edx,[5E97CCh]
  00469993  8b82a8000000          mov       eax,[edx+0A8h]
  00469999  8b08                  mov       ecx,[eax]
  0046999b  660fb6512c            movzx     dx,byte [ecx+2Ch]
  004699a0  52                    push      edx
  004699a1  56                    push      esi
  004699a2  e8d9e6ffff            call      00468080h
  004699a7  83c418                add       esp,18h

LAB_004699aa:
  004699aa  66837e1001            cmp       word [esi+10h],1
  004699af  7532                  jne       short 004699E3h
  004699b1  66ff4612              inc       word [esi+12h]
  004699b5  8b4e20                mov       ecx,[esi+20h]
  004699b8  0fbf4612              movsx     eax,word [esi+12h]
  004699bc  668b5616              mov       dx,[esi+16h]
  004699c0  66015632              add       [esi+32h],dx
  004699c4  c1e00c                shl       eax,0Ch
  004699c7  2bc8                  sub       ecx,eax
  004699c9  894e20                mov       [esi+20h],ecx
  004699cc  8bc1                  mov       eax,ecx
  004699ce  668b4e5c              mov       cx,[esi+5Ch]
  004699d2  66014e34              add       [esi+34h],cx
  004699d6  3d004025ff            cmp       eax,0FF254000h
  004699db  7f06                  jg        short 004699E3h
  004699dd  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004699e3:
  004699e3  5e                    pop       esi
  004699e4  c3                    ret
  004699e5  90                    nop
  004699e6  90                    nop
  004699e7  90                    nop
  004699e8  90                    nop
  004699e9  90                    nop
  004699ea  90                    nop
  004699eb  90                    nop
  004699ec  90                    nop
  004699ed  90                    nop
  004699ee  90                    nop
  004699ef  90                    nop
  004699f0  51                    push      ecx
  004699f1  53                    push      ebx
  004699f2  55                    push      ebp
  004699f3  56                    push      esi
  004699f4  57                    push      edi
  004699f5  c744241000000000      mov       dword [esp+10h],0
  004699fd  33ed                  xor       ebp,ebp
  004699ff  b894955e00            mov       eax,5E9594h
  00469a04  ba04000000            mov       edx,4

LAB_00469a09:
  00469a09  8b08                  mov       ecx,[eax]
  00469a0b  f7c100000080          test      ecx,80000000h
  00469a11  740d                  je        short 00469A20h
  00469a13  f7c100000050          test      ecx,50000000h
  00469a19  7505                  jne       short 00469A20h
  00469a1b  45                    inc       ebp
  00469a1c  89442410              mov       [esp+10h],eax

LAB_00469a20:
  00469a20  2d9c000000            sub       eax,9Ch
  00469a25  4a                    dec       edx
  00469a26  75e1                  jne       short 00469A09h
  00469a28  b9c0935e00            mov       ecx,5E93C0h
  00469a2d  ba04000000            mov       edx,4

LAB_00469a32:
  00469a32  8b01                  mov       eax,[ecx]
  00469a34  a900000080            test      eax,80000000h
  00469a39  7405                  je        short 00469A40h
  00469a3b  2500000050            and       eax,50000000h

LAB_00469a40:
  00469a40  81c19c000000          add       ecx,9Ch
  00469a46  4a                    dec       edx
  00469a47  75e9                  jne       short 00469A32h
  00469a49  33db                  xor       ebx,ebx
  00469a4b  b894955e00            mov       eax,5E9594h
  00469a50  b904000000            mov       ecx,4

LAB_00469a55:
  00469a55  f70000000080          test      dword [eax],80000000h
  00469a5b  7401                  je        short 00469A5Eh
  00469a5d  43                    inc       ebx

LAB_00469a5e:
  00469a5e  2d9c000000            sub       eax,9Ch
  00469a63  49                    dec       ecx
  00469a64  75ef                  jne       short 00469A55h
  00469a66  8b742418              mov       esi,[esp+18h]
  00469a6a  6a00                  push      0
  00469a6c  6a10                  push      10h
  00469a6e  56                    push      esi
  00469a6f  e88cb50c00            call      00535000h
  00469a74  83c40c                add       esp,0Ch
  00469a77  8bf8                  mov       edi,eax
  00469a79  6800000040            push      40000000h
  00469a7e  6a10                  push      10h
  00469a80  56                    push      esi
  00469a81  e87ab50c00            call      00535000h
  00469a86  83c40c                add       esp,0Ch
  00469a89  663bfb                cmp       di,bx
  00469a8c  750f                  jne       short 00469A9Dh
  00469a8e  a128185700            mov       eax,[571828h]
  00469a93  80cc04                or        ah,4
  00469a96  a328185700            mov       [571828h],eax
  00469a9b  eb38                  jmp       short 00469AD5h

LAB_00469a9d:
  00469a9d  663bfd                cmp       di,bp
  00469aa0  7533                  jne       short 00469AD5h
  00469aa2  6685ed                test      bp,bp
  00469aa5  742e                  je        short 00469AD5h
  00469aa7  66837e1000            cmp       word [esi+10h],0
  00469aac  7527                  jne       short 00469AD5h
  00469aae  8b0dcc975e00          mov       ecx,[5E97CCh]
  00469ab4  8b442410              mov       eax,[esp+10h]
  00469ab8  6a00                  push      0
  00469aba  68b0335600            push      5633B0h
  00469abf  8b5170                mov       edx,[ecx+70h]
  00469ac2  50                    push      eax
  00469ac3  8b4208                mov       eax,[edx+8]
  00469ac6  50                    push      eax
  00469ac7  e884a80d00            call      00544350h
  00469acc  83c410                add       esp,10h
  00469acf  66c746100100          mov       word [esi+10h],1

LAB_00469ad5:
  00469ad5  a128185700            mov       eax,[571828h]
  00469ada  f6c408                test      ah,8
  00469add  7407                  je        short 00469AE6h
  00469adf  8b06                  mov       eax,[esi]
  00469ae1  80cc60                or        ah,60h
  00469ae4  8906                  mov       [esi],eax

LAB_00469ae6:
  00469ae6  5f                    pop       edi
  00469ae7  5e                    pop       esi
  00469ae8  5d                    pop       ebp
  00469ae9  5b                    pop       ebx
  00469aea  59                    pop       ecx
  00469aeb  c3                    ret
  00469aec  90                    nop
  00469aed  90                    nop
  00469aee  90                    nop
  00469aef  90                    nop
  00469af0  a128185700            mov       eax,[571828h]
  00469af5  56                    push      esi
  00469af6  f6c404                test      ah,4
  00469af9  0f84b2000000          je        near 00469BB1h
  00469aff  f6c408                test      ah,8
  00469b02  7422                  je        short 00469B26h
  00469b04  8b442408              mov       eax,[esp+8]
  00469b08  66ff4010              inc       word [eax+10h]
  00469b0c  6683781002            cmp       word [eax+10h],2
  00469b11  0f859a000000          jne       near 00469BB1h
  00469b17  8b08                  mov       ecx,[eax]
  00469b19  66c740100000          mov       word [eax+10h],0
  00469b1f  80cd60                or        ch,60h
  00469b22  8908                  mov       [eax],ecx
  00469b24  5e                    pop       esi
  00469b25  c3                    ret

LAB_00469b26:
  00469b26  8b742408              mov       esi,[esp+8]
  00469b2a  66837e1000            cmp       word [esi+10h],0
  00469b2f  7530                  jne       short 00469B61h
  00469b31  a1cc975e00            mov       eax,[5E97CCh]
  00469b36  6a00                  push      0
  00469b38  6a00                  push      0
  00469b3a  6800180000            push      1800h
  00469b3f  8b88a8000000          mov       ecx,[eax+0A8h]
  00469b45  6800040000            push      400h
  00469b4a  8b11                  mov       edx,[ecx]
  00469b4c  660fb64239            movzx     ax,byte [edx+39h]
  00469b51  50                    push      eax
  00469b52  56                    push      esi
  00469b53  e828e5ffff            call      00468080h
  00469b58  83c418                add       esp,18h
  00469b5b  66c746100100          mov       word [esi+10h],1

LAB_00469b61:
  00469b61  66834634ba            add       word [esi+34h],0FFBAh
  00469b66  66837e3400            cmp       word [esi+34h],0
  00469b6b  7d44                  jge       short 00469BB1h
  00469b6d  8b0dcc975e00          mov       ecx,[5E97CCh]
  00469b73  6a00                  push      0
  00469b75  6a00                  push      0
  00469b77  6800180000            push      1800h
  00469b7c  8b91a8000000          mov       edx,[ecx+0A8h]
  00469b82  6800040000            push      400h
  00469b87  8b02                  mov       eax,[edx]
  00469b89  660fb6483a            movzx     cx,byte [eax+3Ah]
  00469b8e  51                    push      ecx
  00469b8f  56                    push      esi
  00469b90  e8ebe4ffff            call      00468080h
  00469b95  66c746340000          mov       word [esi+34h],0
  00469b9b  a128185700            mov       eax,[571828h]
  00469ba0  83c418                add       esp,18h
  00469ba3  80cc08                or        ah,8
  00469ba6  a328185700            mov       [571828h],eax
  00469bab  66c746100000          mov       word [esi+10h],0

LAB_00469bb1:
  00469bb1  5e                    pop       esi
  00469bb2  c3                    ret
  00469bb3  90                    nop
  00469bb4  90                    nop
  00469bb5  90                    nop
  00469bb6  90                    nop
  00469bb7  90                    nop
  00469bb8  90                    nop
  00469bb9  90                    nop
  00469bba  90                    nop
  00469bbb  90                    nop
  00469bbc  90                    nop
  00469bbd  90                    nop
  00469bbe  90                    nop
  00469bbf  90                    nop

41 cible(s) de saut interne : 0x004694d1, 0x004694d6, 0x004694f5, 0x004694fa, 0x00469517, 0x0046954e, 0x0046956d, 0x004695a0, 0x00469642, 0x00469660, 0x004696da, 0x004696dd, 0x004696ec, 0x004696f6, 0x00469705, 0x00469714, 0x0046971f, 0x0046972a, 0x00469734, 0x0046973e, 0x0046974a, 0x00469764, 0x004697e0, 0x0046987a, 0x004698d0, 0x004698e2, 0x00469915, 0x004699aa, 0x004699e3, 0x00469a09, 0x00469a20, 0x00469a32, 0x00469a40, 0x00469a55, 0x00469a5e, 0x00469a9d, 0x00469ad5, 0x00469ae6, 0x00469b26, 0x00469b61, 0x00469bb1
