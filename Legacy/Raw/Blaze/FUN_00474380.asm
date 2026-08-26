Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00474380  section .text  offset fichier 0x73780  832 octets

  00474380  53                    push      ebx
  00474381  56                    push      esi
  00474382  8b74240c              mov       esi,[esp+0Ch]
  00474386  57                    push      edi
  00474387  bb01000000            mov       ebx,1
  0047438c  668b4610              mov       ax,[esi+10h]
  00474390  6685c0                test      ax,ax
  00474393  7511                  jne       short 004743A6h
  00474395  66394612              cmp       [esi+12h],ax
  00474399  750b                  jne       short 004743A6h
  0047439b  8b0e                  mov       ecx,[esi]
  0047439d  66895e12              mov       [esi+12h],bx
  004743a1  80cd40                or        ch,40h
  004743a4  890e                  mov       [esi],ecx

LAB_004743a6:
  004743a6  663bc3                cmp       ax,bx
  004743a9  bf02000000            mov       edi,2
  004743ae  0f85fe000000          jne       near 004744B2h
  004743b4  8b461c                mov       eax,[esi+1Ch]
  004743b7  a398195700            mov       [571998h],eax
  004743bc  8b4e20                mov       ecx,[esi+20h]
  004743bf  890d9c195700          mov       [57199Ch],ecx
  004743c5  8b5624                mov       edx,[esi+24h]
  004743c8  8915a0195700          mov       [5719A0h],edx
  004743ce  e86d2d0d00            call      00547140h
  004743d3  99                    cdq
  004743d4  b990010000            mov       ecx,190h
  004743d9  f7f9                  idiv      ecx
  004743db  66035630              add       dx,[esi+30h]
  004743df  81c2c8000000          add       edx,0C8h
  004743e5  66891580195700        mov       [571980h],dx
  004743ec  e84f2d0d00            call      00547140h
  004743f1  99                    cdq
  004743f2  b9c8000000            mov       ecx,0C8h
  004743f7  f7f9                  idiv      ecx
  004743f9  a194ca5e00            mov       eax,[5ECA94h]
  004743fe  66035632              add       dx,[esi+32h]
  00474402  81c200020000          add       edx,200h
  00474408  66891582195700        mov       [571982h],dx
  0047440f  668b5634              mov       dx,[esi+34h]
  00474413  66891584195700        mov       [571984h],dx
  0047441a  8b08                  mov       ecx,[eax]
  0047441c  890d88195700          mov       [571988h],ecx
  00474422  8b5004                mov       edx,[eax+4]
  00474425  89158c195700          mov       [57198Ch],edx
  0047442b  8b4008                mov       eax,[eax+8]
  0047442e  a390195700            mov       [571990h],eax
  00474433  e8082d0d00            call      00547140h
  00474438  99                    cdq
  00474439  b9c8000000            mov       ecx,0C8h
  0047443e  c70594ca5e0088195700  mov       dword [5ECA94h],571988h
  00474448  f7f9                  idiv      ecx
  0047444a  33c0                  xor       eax,eax
  0047444c  c705e09a5e0080195700  mov       dword [5E9AE0h],571980h
  00474456  50                    push      eax
  00474457  c705bcde5e0098195700  mov       dword [5EDEBCh],571998h
  00474461  50                    push      eax
  00474462  6800100000            push      1000h
  00474467  6800040000            push      400h
  0047446c  6a0d                  push      0Dh
  0047446e  56                    push      esi
  0047446f  81c258020000          add       edx,258h
  00474475  891590195700          mov       [571990h],edx
  0047447b  8b154ce55d00          mov       edx,[5DE54Ch]
  00474481  89157c4b6000          mov       [604B7Ch],edx
  00474487  c7054ce55d0003000000  mov       dword [5DE54Ch],3
  00474491  8b16                  mov       edx,[esi]
  00474493  66c7462a007d          mov       word [esi+2Ah],7D00h
  00474499  80e6bf                and       dh,0BFh
  0047449c  66894628              mov       [esi+28h],ax
  004744a0  8916                  mov       [esi],edx
  004744a2  6689462c              mov       [esi+2Ch],ax
  004744a6  66897e10              mov       [esi+10h],di
  004744aa  e8d13bffff            call      00468080h
  004744af  83c418                add       esp,18h

LAB_004744b2:
  004744b2  66397e10              cmp       [esi+10h],di
  004744b6  7544                  jne       short 004744FCh
  004744b8  b978000000            mov       ecx,78h
  004744bd  bf00100000            mov       edi,1000h
  004744c2  66014e28              add       [esi+28h],cx
  004744c6  668b4628              mov       ax,[esi+28h]
  004744ca  66014e2c              add       [esi+2Ch],cx
  004744ce  663bc7                cmp       ax,di
  004744d1  7c2e                  jl        short 00474501h
  004744d3  6a00                  push      0
  004744d5  6a00                  push      0
  004744d7  57                    push      edi
  004744d8  68bc020000            push      2BCh
  004744dd  6a0a                  push      0Ah
  004744df  56                    push      esi
  004744e0  66897e28              mov       [esi+28h],di
  004744e4  66897e2c              mov       [esi+2Ch],di
  004744e8  66c746100300          mov       word [esi+10h],3
  004744ee  66894e12              mov       [esi+12h],cx
  004744f2  e8893bffff            call      00468080h
  004744f7  83c418                add       esp,18h
  004744fa  eb05                  jmp       short 00474501h

LAB_004744fc:
  004744fc  bf00100000            mov       edi,1000h

LAB_00474501:
  00474501  66837e1003            cmp       word [esi+10h],3
  00474506  753a                  jne       short 00474542h
  00474508  668b4612              mov       ax,[esi+12h]
  0047450c  6629462a              sub       [esi+2Ah],ax
  00474510  83c00f                add       eax,0Fh
  00474513  66397e2a              cmp       [esi+2Ah],di
  00474517  66894612              mov       [esi+12h],ax
  0047451b  7f25                  jg        short 00474542h
  0047451d  6a00                  push      0
  0047451f  6a00                  push      0
  00474521  57                    push      edi
  00474522  6800040000            push      400h
  00474527  6a0f                  push      0Fh
  00474529  56                    push      esi
  0047452a  66897e2a              mov       [esi+2Ah],di
  0047452e  66c746100400          mov       word [esi+10h],4
  00474534  66c746120000          mov       word [esi+12h],0
  0047453a  e8413bffff            call      00468080h
  0047453f  83c418                add       esp,18h

LAB_00474542:
  00474542  66837e1004            cmp       word [esi+10h],4
  00474547  0f8556010000          jne       near 004746A3h
  0047454d  66837e1208            cmp       word [esi+12h],8
  00474552  881d2be05d00          mov       [5DE02Bh],bl
  00474558  7d2c                  jge       short 00474586h
  0047455a  8a1528e05d00          mov       dl,[5DE028h]
  00474560  8a0d29e05d00          mov       cl,[5DE029h]
  00474566  a02ae05d00            mov       al,[5DE02Ah]
  0047456b  80c20a                add       dl,0Ah
  0047456e  80c10a                add       cl,0Ah
  00474571  040a                  add       al,0Ah
  00474573  881528e05d00          mov       [5DE028h],dl
  00474579  880d29e05d00          mov       [5DE029h],cl
  0047457f  a22ae05d00            mov       [5DE02Ah],al
  00474584  eb2c                  jmp       short 004745B2h

LAB_00474586:
  00474586  8a0d28e05d00          mov       cl,[5DE028h]
  0047458c  8a1529e05d00          mov       dl,[5DE029h]
  00474592  b0fb                  mov       al,0FBh
  00474594  02c8                  add       cl,al
  00474596  02d0                  add       dl,al
  00474598  880d28e05d00          mov       [5DE028h],cl
  0047459e  8a0d2ae05d00          mov       cl,[5DE02Ah]
  004745a4  02c8                  add       cl,al
  004745a6  881529e05d00          mov       [5DE029h],dl
  004745ac  880d2ae05d00          mov       [5DE02Ah],cl

LAB_004745b2:
  004745b2  66ff4612              inc       word [esi+12h]
  004745b6  66837e1214            cmp       word [esi+12h],14h
  004745bb  0f8ee2000000          jle       near 004746A3h
  004745c1  881d2be05d00          mov       [5DE02Bh],bl
  004745c7  c60528e05d0000        mov       byte [5DE028h],0
  004745ce  c60529e05d0000        mov       byte [5DE029h],0
  004745d5  c6052ae05d0000        mov       byte [5DE02Ah],0
  004745dc  66c746106300          mov       word [esi+10h],63h
  004745e2  c70594ca5e0078e05d00  mov       dword [5ECA94h],5DE078h
  004745ec  c705e09a5e0050465600  mov       dword [5E9AE0h],564650h
  004745f6  c705bcde5e0068e05d00  mov       dword [5EDEBCh],5DE068h
  00474600  e8abf10800            call      005037B0h
  00474605  e8f60d0c00            call      00535400h
  0047460a  a028e55d00            mov       al,[5DE528h]
  0047460f  8a0d2ce55d00          mov       cl,[5DE52Ch]
  00474615  3c03                  cmp       al,3
  00474617  7509                  jne       short 00474622h
  00474619  80f908                cmp       cl,8
  0047461c  0f8481000000          je        near 004746A3h

LAB_00474622:
  00474622  3c04                  cmp       al,4
  00474624  7505                  jne       short 0047462Bh
  00474626  80f906                cmp       cl,6
  00474629  7478                  je        short 004746A3h

LAB_0047462b:
  0047462b  8a4614                mov       al,[esi+14h]
  0047462e  8a0d8c6c5600          mov       cl,[566C8Ch]
  00474634  3ac8                  cmp       cl,al
  00474636  a234e55d00            mov       [5DE534h],al
  0047463b  7466                  je        short 004746A3h
  0047463d  33c9                  xor       ecx,ecx
  0047463f  a28c6c5600            mov       [566C8Ch],al
  00474644  8ac8                  mov       cl,al
  00474646  51                    push      ecx
  00474647  e884540b00            call      00529AD0h
  0047464c  a028e55d00            mov       al,[5DE528h]
  00474651  8a0d2ce55d00          mov       cl,[5DE52Ch]
  00474657  83c404                add       esp,4
  0047465a  3c03                  cmp       al,3
  0047465c  7505                  jne       short 00474663h
  0047465e  80f908                cmp       cl,8
  00474661  7440                  je        short 004746A3h

LAB_00474663:
  00474663  3c04                  cmp       al,4
  00474665  7505                  jne       short 0047466Ch
  00474667  80f906                cmp       cl,6
  0047466a  7437                  je        short 004746A3h

LAB_0047466c:
  0047466c  bfc0935e00            mov       edi,5E93C0h

LAB_00474671:
  00474671  8b07                  mov       eax,[edi]
  00474673  a900000080            test      eax,80000000h
  00474678  7416                  je        short 00474690h
  0047467a  a900000050            test      eax,50000000h
  0047467f  750f                  jne       short 00474690h
  00474681  6aff                  push      0FFFFFFFFh
  00474683  6a00                  push      0
  00474685  6a59                  push      59h
  00474687  57                    push      edi
  00474688  e843630000            call      0047A9D0h
  0047468d  83c410                add       esp,10h

LAB_00474690:
  00474690  81c79c000000          add       edi,9Ch
  00474696  81ff94955e00          cmp       edi,5E9594h
  0047469c  7ed3                  jle       short 00474671h
  0047469e  bf00100000            mov       edi,1000h

LAB_004746a3:
  004746a3  66837e1063            cmp       word [esi+10h],63h
  004746a8  750c                  jne       short 004746B6h
  004746aa  66897e28              mov       [esi+28h],di
  004746ae  66897e2a              mov       [esi+2Ah],di
  004746b2  66897e2c              mov       [esi+2Ch],di

LAB_004746b6:
  004746b6  5f                    pop       edi
  004746b7  5e                    pop       esi
  004746b8  5b                    pop       ebx
  004746b9  c3                    ret
  004746ba  90                    nop
  004746bb  90                    nop
  004746bc  90                    nop
  004746bd  90                    nop
  004746be  90                    nop
  004746bf  90                    nop

15 cible(s) de saut interne : 0x004743a6, 0x004744b2, 0x004744fc, 0x00474501, 0x00474542, 0x00474586, 0x004745b2, 0x00474622, 0x0047462b, 0x00474663, 0x0047466c, 0x00474671, 0x00474690, 0x004746a3, 0x004746b6
