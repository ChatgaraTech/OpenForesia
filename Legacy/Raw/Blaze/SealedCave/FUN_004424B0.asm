C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004424b0  section .text  offset fichier 0x418b0  1200 octets

  004424b0  81eca0000000          sub       esp,0A0h
  004424b6  53                    push      ebx
  004424b7  55                    push      ebp
  004424b8  56                    push      esi
  004424b9  57                    push      edi
  004424ba  b926000000            mov       ecx,26h
  004424bf  33c0                  xor       eax,eax
  004424c1  8d7c2418              lea       edi,[esp+18h]
  004424c5  c744241400000000      mov       dword [esp+14h],0
  004424cd  f3ab                  rep stosd
  004424cf  e87c8afdff            call      0041AF50h
  004424d4  e867c1fcff            call      0040E640h
  004424d9  8bb424b4000000        mov       esi,[esp+0B4h]
  004424e0  8d7c2414              lea       edi,[esp+14h]
  004424e4  b8dbca5e00            mov       eax,5ECADBh
  004424e9  bd20000000            mov       ebp,20h

LAB_004424ee:
  004424ee  8a48d1                mov       cl,[eax-2Fh]
  004424f1  84c9                  test      cl,cl
  004424f3  7549                  jne       short 0044253Eh
  004424f5  8b50c5                mov       edx,[eax-3Bh]
  004424f8  8d48c5                lea       ecx,[eax-3Bh]
  004424fb  f7c200000080          test      edx,80000000h
  00442501  743b                  je        short 0044253Eh
  00442503  8b50e1                mov       edx,[eax-1Fh]
  00442506  8bf9                  mov       edi,ecx
  00442508  89561c                mov       [esi+1Ch],edx
  0044250b  8b50e5                mov       edx,[eax-1Bh]
  0044250e  895620                mov       [esi+20h],edx
  00442511  8b50e9                mov       edx,[eax-17h]
  00442514  895624                mov       [esi+24h],edx
  00442517  33d2                  xor       edx,edx
  00442519  8a50c9                mov       dl,[eax-37h]
  0044251c  8a18                  mov       bl,[eax]
  0044251e  66c740dd0200          mov       word [eax-23h],2
  00442524  80cb02                or        bl,2
  00442527  8b149540965e00        mov       edx,[edx*4+5E9640h]
  0044252e  8b5208                mov       edx,[edx+8]
  00442531  8818                  mov       [eax],bl
  00442533  89542410              mov       [esp+10h],edx
  00442537  8b11                  mov       edx,[ecx]
  00442539  80ce10                or        dh,10h
  0044253c  8911                  mov       [ecx],edx

LAB_0044253e:
  0044253e  059c000000            add       eax,9Ch
  00442543  4d                    dec       ebp
  00442544  75a8                  jne       short 004424EEh
  00442546  33ed                  xor       ebp,ebp
  00442548  bb01000000            mov       ebx,1
  0044254d  66396e10              cmp       [esi+10h],bp
  00442551  7552                  jne       short 004425A5h
  00442553  66895e10              mov       [esi+10h],bx
  00442557  66896e12              mov       [esi+12h],bp
  0044255b  66896e16              mov       [esi+16h],bp
  0044255f  0fbf05b8925e00        movsx     eax,word [5E92B8h]
  00442566  894650                mov       [esi+50h],eax
  00442569  0fbf0dba925e00        movsx     ecx,word [5E92BAh]
  00442570  894e54                mov       [esi+54h],ecx
  00442573  0fbf15bc925e00        movsx     edx,word [5E92BCh]
  0044257a  895658                mov       [esi+58h],edx
  0044257d  66896f1a              mov       [edi+1Ah],bp
  00442581  c6470a00              mov       byte [edi+0Ah],0
  00442585  c74738ffffff00        mov       dword [edi+38h],0FFFFFFh
  0044258c  66895f36              mov       [edi+36h],bx
  00442590  6689ae9a000000        mov       [esi+9Ah],bp
  00442597  c7465c02000000        mov       dword [esi+5Ch],2
  0044259e  6689ae98000000        mov       [esi+98h],bp

LAB_004425a5:
  004425a5  66395e10              cmp       [esi+10h],bx
  004425a9  0f8589010000          jne       near 00442738h
  004425af  66396e16              cmp       [esi+16h],bp
  004425b3  7519                  jne       short 004425CEh
  004425b5  6800100000            push      1000h
  004425ba  6800100000            push      1000h
  004425bf  6800020000            push      200h
  004425c4  6a20                  push      20h
  004425c6  e8d5a80f00            call      0053CEA0h
  004425cb  83c410                add       esp,10h

LAB_004425ce:
  004425ce  66395e16              cmp       [esi+16h],bx
  004425d2  7519                  jne       short 004425EDh
  004425d4  6800100000            push      1000h
  004425d9  6800100000            push      1000h
  004425de  6820030000            push      320h
  004425e3  6a20                  push      20h
  004425e5  e8b6a80f00            call      0053CEA0h
  004425ea  83c410                add       esp,10h

LAB_004425ed:
  004425ed  66837e1602            cmp       word [esi+16h],2
  004425f2  7519                  jne       short 0044260Dh
  004425f4  6800100000            push      1000h
  004425f9  6800100000            push      1000h
  004425fe  6800020000            push      200h
  00442603  6a23                  push      23h
  00442605  e896a80f00            call      0053CEA0h
  0044260a  83c410                add       esp,10h

LAB_0044260d:
  0044260d  e82e4b1000            call      00547140h
  00442612  99                    cdq
  00442613  b90a000000            mov       ecx,0Ah
  00442618  f7f9                  idiv      ecx
  0044261a  66ff869a000000        inc       word [esi+9Ah]
  00442621  668b869a000000        mov       ax,[esi+9Ah]
  00442628  663d2d00              cmp       ax,2Dh
  0044262c  7d13                  jge       short 00442641h
  0044262e  663d2300              cmp       ax,23h
  00442632  0f8c00010000          jl        near 00442738h
  00442638  663bd3                cmp       dx,bx
  0044263b  0f85f7000000          jne       near 00442738h

LAB_00442641:
  00442641  663d6400              cmp       ax,64h
  00442645  0f8fed000000          jg        near 00442738h
  0044264b  6aff                  push      0FFFFFFFFh
  0044264d  55                    push      ebp
  0044264e  68c5000000            push      0C5h
  00442653  56                    push      esi
  00442654  66899e98000000        mov       [esi+98h],bx
  0044265b  66c7869a0000006400    mov       word [esi+9Ah],64h
  00442664  66c746140400          mov       word [esi+14h],4
  0044266a  e871840300            call      0047AAE0h
  0044266f  83c410                add       esp,10h
  00442672  6aff                  push      0FFFFFFFFh
  00442674  55                    push      ebp
  00442675  68c5000000            push      0C5h
  0044267a  56                    push      esi
  0044267b  e860840300            call      0047AAE0h
  00442680  83c410                add       esp,10h
  00442683  6aff                  push      0FFFFFFFFh
  00442685  55                    push      ebp
  00442686  68c5000000            push      0C5h
  0044268b  56                    push      esi
  0044268c  e84f840300            call      0047AAE0h
  00442691  83c410                add       esp,10h
  00442694  6aff                  push      0FFFFFFFFh
  00442696  55                    push      ebp
  00442697  68c5000000            push      0C5h
  0044269c  56                    push      esi
  0044269d  e83e840300            call      0047AAE0h
  004426a2  83c410                add       esp,10h
  004426a5  6aff                  push      0FFFFFFFFh
  004426a7  55                    push      ebp
  004426a8  68c5000000            push      0C5h
  004426ad  56                    push      esi
  004426ae  e82d840300            call      0047AAE0h
  004426b3  83c410                add       esp,10h
  004426b6  6aff                  push      0FFFFFFFFh
  004426b8  55                    push      ebp
  004426b9  68c5000000            push      0C5h
  004426be  56                    push      esi
  004426bf  e81c840300            call      0047AAE0h
  004426c4  8b465c                mov       eax,[esi+5Ch]
  004426c7  83c410                add       esp,10h
  004426ca  83f802                cmp       eax,2
  004426cd  7566                  jne       short 00442735h
  004426cf  6aff                  push      0FFFFFFFFh
  004426d1  55                    push      ebp
  004426d2  68c5000000            push      0C5h
  004426d7  56                    push      esi
  004426d8  e803840300            call      0047AAE0h
  004426dd  83c410                add       esp,10h
  004426e0  6aff                  push      0FFFFFFFFh
  004426e2  55                    push      ebp
  004426e3  68c5000000            push      0C5h
  004426e8  56                    push      esi
  004426e9  e8f2830300            call      0047AAE0h
  004426ee  83c410                add       esp,10h
  004426f1  6aff                  push      0FFFFFFFFh
  004426f3  55                    push      ebp
  004426f4  68c5000000            push      0C5h
  004426f9  56                    push      esi
  004426fa  e8e1830300            call      0047AAE0h
  004426ff  83c410                add       esp,10h
  00442702  6aff                  push      0FFFFFFFFh
  00442704  55                    push      ebp
  00442705  68c5000000            push      0C5h
  0044270a  56                    push      esi
  0044270b  e8d0830300            call      0047AAE0h
  00442710  83c410                add       esp,10h
  00442713  6aff                  push      0FFFFFFFFh
  00442715  55                    push      ebp
  00442716  68c5000000            push      0C5h
  0044271b  56                    push      esi
  0044271c  e8bf830300            call      0047AAE0h
  00442721  83c410                add       esp,10h
  00442724  6aff                  push      0FFFFFFFFh
  00442726  55                    push      ebp
  00442727  68c5000000            push      0C5h
  0044272c  56                    push      esi
  0044272d  e8ae830300            call      0047AAE0h
  00442732  83c410                add       esp,10h

LAB_00442735:
  00442735  ff465c                inc       dword [esi+5Ch]

LAB_00442738:
  00442738  668b8698000000        mov       ax,[esi+98h]
  0044273f  663d0200              cmp       ax,2
  00442743  7c45                  jl        short 0044278Ah
  00442745  663d3100              cmp       ax,31h
  00442749  7d3f                  jge       short 0044278Ah
  0044274b  84c3                  test      bl,al
  0044274d  7416                  je        short 00442765h
  0044274f  0fbfd0                movsx     edx,ax
  00442752  c1e206                shl       edx,6
  00442755  b800130000            mov       eax,1300h
  0044275a  2bc2                  sub       eax,edx
  0044275c  50                    push      eax
  0044275d  50                    push      eax
  0044275e  68a60c0000            push      0CA6h
  00442763  eb14                  jmp       short 00442779h

LAB_00442765:
  00442765  0fbfc8                movsx     ecx,ax
  00442768  c1e106                shl       ecx,6
  0044276b  b800130000            mov       eax,1300h
  00442770  2bc1                  sub       eax,ecx
  00442772  50                    push      eax
  00442773  50                    push      eax
  00442774  68fa0d0000            push      0DFAh

LAB_00442779:
  00442779  6a26                  push      26h
  0044277b  e820a70f00            call      0053CEA0h
  00442780  83c410                add       esp,10h
  00442783  66ff8698000000        inc       word [esi+98h]

LAB_0044278a:
  0044278a  66399e98000000        cmp       [esi+98h],bx
  00442791  7509                  jne       short 0044279Ch
  00442793  66c786980000000200    mov       word [esi+98h],2

LAB_0044279c:
  0044279c  837e5c02              cmp       dword [esi+5Ch],2
  004427a0  7430                  je        short 004427D2h
  004427a2  660fb64738            movzx     ax,byte [edi+38h]
  004427a7  660fb64f39            movzx     cx,byte [edi+39h]
  004427ac  660fb6573a            movzx     dx,byte [edi+3Ah]
  004427b1  83e80a                sub       eax,0Ah
  004427b4  83e90a                sub       ecx,0Ah
  004427b7  83ea0a                sub       edx,0Ah
  004427ba  663bc5                cmp       ax,bp
  004427bd  7d0a                  jge       short 004427C9h
  004427bf  33c0                  xor       eax,eax
  004427c1  33c9                  xor       ecx,ecx
  004427c3  33d2                  xor       edx,edx
  004427c5  804f0140              or        byte [edi+1],40h

LAB_004427c9:
  004427c9  884738                mov       [edi+38h],al
  004427cc  884f39                mov       [edi+39h],cl
  004427cf  88573a                mov       [edi+3Ah],dl

LAB_004427d2:
  004427d2  845e16                test      [esi+16h],bl
  004427d5  7524                  jne       short 004427FBh
  004427d7  8b542410              mov       edx,[esp+10h]
  004427db  66ff471a              inc       word [edi+1Ah]
  004427df  668b4f1a              mov       cx,[edi+1Ah]
  004427e3  668b4216              mov       ax,[edx+16h]
  004427e7  0fbfc9                movsx     ecx,cx
  004427ea  8bd0                  mov       edx,eax
  004427ec  81e2ffff0000          and       edx,0FFFFh
  004427f2  3bca                  cmp       ecx,edx
  004427f4  7c05                  jl        short 004427FBh
  004427f6  48                    dec       eax
  004427f7  6689471a              mov       [edi+1Ah],ax

LAB_004427fb:
  004427fb  66817e168200          cmp       word [esi+16h],82h
  00442801  7e53                  jle       short 00442856h
  00442803  837e5c03              cmp       dword [esi+5Ch],3
  00442807  754d                  jne       short 00442856h
  00442809  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0044280f  e89c0f0c00            call      005037B0h
  00442814  a1cc975e00            mov       eax,[5E97CCh]
  00442819  55                    push      ebp
  0044281a  55                    push      ebp
  0044281b  50                    push      eax
  0044281c  e8af150d00            call      00513DD0h
  00442821  8b07                  mov       eax,[edi]
  00442823  83c40c                add       esp,0Ch
  00442826  0d00400002            or        eax,2004000h
  0044282b  8907                  mov       [edi],eax
  0044282d  668b4e50              mov       cx,[esi+50h]
  00442831  66890d60915e00        mov       [5E9160h],cx
  00442838  668b5654              mov       dx,[esi+54h]
  0044283c  66891562915e00        mov       [5E9162h],dx
  00442843  668b4658              mov       ax,[esi+58h]
  00442847  66a364915e00          mov       [5E9164h],ax
  0044284d  66c70566915e000200    mov       word [5E9166h],2

LAB_00442856:
  00442856  66ff4616              inc       word [esi+16h]
  0044285a  5f                    pop       edi
  0044285b  5e                    pop       esi
  0044285c  5d                    pop       ebp
  0044285d  5b                    pop       ebx
  0044285e  81c4a0000000          add       esp,0A0h
  00442864  c3                    ret
  00442865  90                    nop
  00442866  90                    nop
  00442867  90                    nop
  00442868  90                    nop
  00442869  90                    nop
  0044286a  90                    nop
  0044286b  90                    nop
  0044286c  90                    nop
  0044286d  90                    nop
  0044286e  90                    nop
  0044286f  90                    nop
  00442870  51                    push      ecx
  00442871  53                    push      ebx
  00442872  55                    push      ebp
  00442873  56                    push      esi
  00442874  8b742414              mov       esi,[esp+14h]
  00442878  57                    push      edi
  00442879  66837e1000            cmp       word [esi+10h],0
  0044287e  0f851b010000          jne       near 0044299Fh
  00442884  66c746100100          mov       word [esi+10h],1
  0044288a  66c746120000          mov       word [esi+12h],0
  00442890  c6460408              mov       byte [esi+4],8
  00442894  c6460a09              mov       byte [esi+0Ah],9
  00442898  c6460b00              mov       byte [esi+0Bh],0
  0044289c  e89f481000            call      00547140h
  004428a1  99                    cdq
  004428a2  b984030000            mov       ecx,384h
  004428a7  f7f9                  idiv      ecx
  004428a9  81c200040000          add       edx,400h
  004428af  66895634              mov       [esi+34h],dx
  004428b3  e888481000            call      00547140h
  004428b8  99                    cdq
  004428b9  33c2                  xor       eax,edx
  004428bb  66c74628b80b          mov       word [esi+28h],0BB8h
  004428c1  2bc2                  sub       eax,edx
  004428c3  25ff0f0000            and       eax,0FFFh
  004428c8  33c2                  xor       eax,edx
  004428ca  8bf8                  mov       edi,eax
  004428cc  2bfa                  sub       edi,edx
  004428ce  66ff4e14              dec       word [esi+14h]
  004428d2  668b4614              mov       ax,[esi+14h]
  004428d6  66897e32              mov       [esi+32h],di
  004428da  6685c0                test      ax,ax
  004428dd  6689869a000000        mov       [esi+9Ah],ax
  004428e4  0f8cae000000          jl        near 00442998h
  004428ea  0fbf4634              movsx     eax,word [esi+34h]
  004428ee  8b5624                mov       edx,[esi+24h]
  004428f1  8b6e1c                mov       ebp,[esi+1Ch]
  004428f4  8b5e20                mov       ebx,[esi+20h]
  004428f7  50                    push      eax
  004428f8  89542414              mov       [esp+14h],edx
  004428fc  e815311000            call      00545A16h
  00442901  8d0440                lea       eax,[eax+eax*2]
  00442904  8d0480                lea       eax,[eax+eax*4]
  00442907  8d0c80                lea       ecx,[eax+eax*4]
  0044290a  d1e1                  shl       ecx,1
  0044290c  0fbfc7                movsx     eax,di
  0044290f  8bd1                  mov       edx,ecx
  00442911  50                    push      eax
  00442912  c1fa0c                sar       edx,0Ch
  00442915  8954241c              mov       [esp+1Ch],edx
  00442919  e8f8301000            call      00545A16h
  0044291e  8b4c2418              mov       ecx,[esp+18h]
  00442922  0fafc8                imul      ecx,eax
  00442925  0fbf5634              movsx     edx,word [esi+34h]
  00442929  8b461c                mov       eax,[esi+1Ch]
  0044292c  52                    push      edx
  0044292d  2bc1                  sub       eax,ecx
  0044292f  89461c                mov       [esi+1Ch],eax
  00442932  e8df301000            call      00545A16h
  00442937  0fbf4e32              movsx     ecx,word [esi+32h]
  0044293b  8d0440                lea       eax,[eax+eax*2]
  0044293e  51                    push      ecx
  0044293f  8d0480                lea       eax,[eax+eax*4]
  00442942  8d0480                lea       eax,[eax+eax*4]
  00442945  d1e0                  shl       eax,1
  00442947  8bf8                  mov       edi,eax
  00442949  c1ff0c                sar       edi,0Ch
  0044294c  e8cb301000            call      00545A1Ch
  00442951  0faff8                imul      edi,eax
  00442954  0fbf5634              movsx     edx,word [esi+34h]
  00442958  8b4624                mov       eax,[esi+24h]
  0044295b  52                    push      edx
  0044295c  03c7                  add       eax,edi
  0044295e  8946                  (bad)
  (octet invalide — la plage demandee deborde probablement du code)
