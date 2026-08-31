Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0044b510  section .text  offset fichier 0x4a910  416 octets

  0044b510  56                    push      esi
  0044b511  8b742408              mov       esi,[esp+8]
  0044b515  57                    push      edi
  0044b516  33ff                  xor       edi,edi
  0044b518  57                    push      edi
  0044b519  57                    push      edi
  0044b51a  56                    push      esi
  0044b51b  e820cbffff            call      00448040h
  0044b520  83c40c                add       esp,0Ch
  0044b523  66397e10              cmp       [esi+10h],di
  0044b527  750b                  jne       short 0044B534h
  0044b529  57                    push      edi
  0044b52a  57                    push      edi
  0044b52b  56                    push      esi
  0044b52c  e87fc7ffff            call      00447CB0h
  0044b531  83c40c                add       esp,0Ch

LAB_0044b534:
  0044b534  66397e10              cmp       [esi+10h],di
  0044b538  b801000000            mov       eax,1
  0044b53d  750c                  jne       short 0044B54Bh
  0044b53f  66894610              mov       [esi+10h],ax
  0044b543  66897e14              mov       [esi+14h],di
  0044b547  66897e16              mov       [esi+16h],di

LAB_0044b54b:
  0044b54b  66394610              cmp       [esi+10h],ax
  0044b54f  7520                  jne       short 0044B571h
  0044b551  66ff4614              inc       word [esi+14h]
  0044b555  66837e1402            cmp       word [esi+14h],2
  0044b55a  7e15                  jle       short 0044B571h
  0044b55c  6aff                  push      0FFFFFFFFh
  0044b55e  57                    push      edi
  0044b55f  6890000000            push      90h
  0044b564  56                    push      esi
  0044b565  66897e14              mov       [esi+14h],di
  0044b569  e872f50200            call      0047AAE0h
  0044b56e  83c410                add       esp,10h

LAB_0044b571:
  0044b571  66397e16              cmp       [esi+16h],di
  0044b575  7519                  jne       short 0044B590h
  0044b577  6800100000            push      1000h
  0044b57c  6800100000            push      1000h
  0044b581  6800100000            push      1000h
  0044b586  6a28                  push      28h
  0044b588  e813190f00            call      0053CEA0h
  0044b58d  83c410                add       esp,10h

LAB_0044b590:
  0044b590  66837e1602            cmp       word [esi+16h],2
  0044b595  7519                  jne       short 0044B5B0h
  0044b597  6800100000            push      1000h
  0044b59c  6800100000            push      1000h
  0044b5a1  6800040000            push      400h
  0044b5a6  6a1c                  push      1Ch
  0044b5a8  e8f3180f00            call      0053CEA0h
  0044b5ad  83c410                add       esp,10h

LAB_0044b5b0:
  0044b5b0  66837e1603            cmp       word [esi+16h],3
  0044b5b5  7519                  jne       short 0044B5D0h
  0044b5b7  6800100000            push      1000h
  0044b5bc  6800100000            push      1000h
  0044b5c1  6853060000            push      653h
  0044b5c6  6a28                  push      28h
  0044b5c8  e8d3180f00            call      0053CEA0h
  0044b5cd  83c410                add       esp,10h

LAB_0044b5d0:
  0044b5d0  66837e1604            cmp       word [esi+16h],4
  0044b5d5  7519                  jne       short 0044B5F0h
  0044b5d7  6800100000            push      1000h
  0044b5dc  6800100000            push      1000h
  0044b5e1  6800040000            push      400h
  0044b5e6  6a23                  push      23h
  0044b5e8  e8b3180f00            call      0053CEA0h
  0044b5ed  83c410                add       esp,10h

LAB_0044b5f0:
  0044b5f0  b896000000            mov       eax,96h
  0044b5f5  66a360915e00          mov       [5E9160h],ax
  0044b5fb  66a362915e00          mov       [5E9162h],ax
  0044b601  668b4616              mov       ax,[esi+16h]
  0044b605  66c70566915e000200    mov       word [5E9166h],2
  0044b60e  66054b00              add       ax,4Bh
  0044b612  d1e0                  shl       eax,1
  0044b614  66a364915e00          mov       [5E9164h],ax
  0044b61a  66397e16              cmp       [esi+16h],di
  0044b61e  7552                  jne       short 0044B672h
  0044b620  6aff                  push      0FFFFFFFFh
  0044b622  57                    push      edi
  0044b623  b87c150000            mov       eax,157Ch
  0044b628  6895000000            push      95h
  0044b62d  56                    push      esi
  0044b62e  66897e30              mov       [esi+30h],di
  0044b632  66897e34              mov       [esi+34h],di
  0044b636  66c746320002          mov       word [esi+32h],200h
  0044b63c  66894628              mov       [esi+28h],ax
  0044b640  6689462c              mov       [esi+2Ch],ax
  0044b644  6689462a              mov       [esi+2Ah],ax
  0044b648  e893f40200            call      0047AAE0h
  0044b64d  83c410                add       esp,10h
  0044b650  b850140000            mov       eax,1450h
  0044b655  66894628              mov       [esi+28h],ax
  0044b659  6689462c              mov       [esi+2Ch],ax
  0044b65d  6aff                  push      0FFFFFFFFh
  0044b65f  57                    push      edi
  0044b660  6895000000            push      95h
  0044b665  56                    push      esi
  0044b666  6689462a              mov       [esi+2Ah],ax
  0044b66a  e871f40200            call      0047AAE0h
  0044b66f  83c410                add       esp,10h

LAB_0044b672:
  0044b672  66ff4616              inc       word [esi+16h]
  0044b676  66837e161e            cmp       word [esi+16h],1Eh
  0044b67b  752e                  jne       short 0044B6ABh
  0044b67d  8b4e74                mov       ecx,[esi+74h]
  0044b680  8a5606                mov       dl,[esi+6]
  0044b683  57                    push      edi
  0044b684  885106                mov       [ecx+6],dl
  0044b687  8b4674                mov       eax,[esi+74h]
  0044b68a  8a4e07                mov       cl,[esi+7]
  0044b68d  884807                mov       [eax+7],cl
  0044b690  8b5678                mov       edx,[esi+78h]
  0044b693  8b4674                mov       eax,[esi+74h]
  0044b696  52                    push      edx
  0044b697  50                    push      eax
  0044b698  e813000000            call      0044B6B0h
  0044b69d  83c40c                add       esp,0Ch
  0044b6a0  57                    push      edi
  0044b6a1  57                    push      edi
  0044b6a2  56                    push      esi
  0044b6a3  e878c9ffff            call      00448020h
  0044b6a8  83c40c                add       esp,0Ch

LAB_0044b6ab:
  0044b6ab  5f                    pop       edi
  0044b6ac  5e                    pop       esi
  0044b6ad  c3                    ret
  0044b6ae  90                    nop
  0044b6af  90                    nop

9 cible(s) de saut interne : 0x0044b534, 0x0044b54b, 0x0044b571, 0x0044b590, 0x0044b5b0, 0x0044b5d0, 0x0044b5f0, 0x0044b672, 0x0044b6ab
