Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045c870  section .text  offset fichier 0x5bc70  1648 octets

  0045c870  53                    push      ebx
  0045c871  55                    push      ebp
  0045c872  56                    push      esi
  0045c873  8b742410              mov       esi,[esp+10h]
  0045c877  57                    push      edi
  0045c878  33ff                  xor       edi,edi
  0045c87a  66397e10              cmp       [esi+10h],di
  0045c87e  0f85c7000000          jne       near 0045C94Bh
  0045c884  8b5e1c                mov       ebx,[esi+1Ch]
  0045c887  8b6e20                mov       ebp,[esi+20h]
  0045c88a  8b4624                mov       eax,[esi+24h]
  0045c88d  8bcb                  mov       ecx,ebx
  0045c88f  8bd5                  mov       edx,ebp
  0045c891  89442414              mov       [esp+14h],eax
  0045c895  c1f90c                sar       ecx,0Ch
  0045c898  c1fa0c                sar       edx,0Ch
  0045c89b  c1f80c                sar       eax,0Ch
  0045c89e  6689be98000000        mov       [esi+98h],di
  0045c8a5  66897e12              mov       [esi+12h],di
  0045c8a9  897e5c                mov       [esi+5Ch],edi
  0045c8ac  894e44                mov       [esi+44h],ecx
  0045c8af  895648                mov       [esi+48h],edx
  0045c8b2  89464c                mov       [esi+4Ch],eax

LAB_0045c8b5:
  0045c8b5  0fbf4e16              movsx     ecx,word [esi+16h]
  0045c8b9  8b465c                mov       eax,[esi+5Ch]
  0045c8bc  3bc1                  cmp       eax,ecx
  0045c8be  7d0e                  jge       short 0045C8CEh
  0045c8c0  668b5612              mov       dx,[esi+12h]
  0045c8c4  663b5614              cmp       dx,[esi+14h]
  0045c8c8  7d04                  jge       short 0045C8CEh
  0045c8ca  40                    inc       eax
  0045c8cb  89465c                mov       [esi+5Ch],eax

LAB_0045c8ce:
  0045c8ce  0fbf4632              movsx     eax,word [esi+32h]
  0045c8d2  50                    push      eax
  0045c8d3  e844910e00            call      00545A1Ch
  0045c8d8  0faf465c              imul      eax,[esi+5Ch]
  0045c8dc  8b7e1c                mov       edi,[esi+1Ch]
  0045c8df  0fbf4e32              movsx     ecx,word [esi+32h]
  0045c8e3  03f8                  add       edi,eax
  0045c8e5  51                    push      ecx
  0045c8e6  897e1c                mov       [esi+1Ch],edi
  0045c8e9  8b7e5c                mov       edi,[esi+5Ch]
  0045c8ec  e825910e00            call      00545A16h
  0045c8f1  0fafc7                imul      eax,edi
  0045c8f4  8b4e24                mov       ecx,[esi+24h]
  0045c8f7  668b565c              mov       dx,[esi+5Ch]
  0045c8fb  66015612              add       [esi+12h],dx
  0045c8ff  03c8                  add       ecx,eax
  0045c901  668b4612              mov       ax,[esi+12h]
  0045c905  894e24                mov       [esi+24h],ecx
  0045c908  663b4614              cmp       ax,[esi+14h]
  0045c90c  7e11                  jle       short 0045C91Fh
  0045c90e  8d47ff                lea       eax,[edi-1]
  0045c911  83f8ff                cmp       eax,0FFFFFFFFh
  0045c914  89465c                mov       [esi+5Ch],eax
  0045c917  7f06                  jg        short 0045C91Fh
  0045c919  66c746100800          mov       word [esi+10h],8

LAB_0045c91f:
  0045c91f  66837e1000            cmp       word [esi+10h],0
  0045c924  748f                  je        short 0045C8B5h
  0045c926  8b461c                mov       eax,[esi+1Ch]
  0045c929  8b4e24                mov       ecx,[esi+24h]
  0045c92c  8b542414              mov       edx,[esp+14h]
  0045c930  895e1c                mov       [esi+1Ch],ebx
  0045c933  c1f80c                sar       eax,0Ch
  0045c936  c1f90c                sar       ecx,0Ch
  0045c939  894648                mov       [esi+48h],eax
  0045c93c  66898e9a000000        mov       [esi+9Ah],cx
  0045c943  896e20                mov       [esi+20h],ebp
  0045c946  895624                mov       [esi+24h],edx
  0045c949  33ff                  xor       edi,edi

LAB_0045c94b:
  0045c94b  6800000040            push      40000000h
  0045c950  6a10                  push      10h
  0045c952  56                    push      esi
  0045c953  e8a8860d00            call      00535000h
  0045c958  8be8                  mov       ebp,eax
  0045c95a  83c40c                add       esp,0Ch
  0045c95d  896c2414              mov       [esp+14h],ebp
  0045c961  e80aa40000            call      00466D70h
  0045c966  0fbfc0                movsx     eax,ax
  0045c969  3bc5                  cmp       eax,ebp
  0045c96b  bb08000000            mov       ebx,8
  0045c970  0f84e9000000          je        near 0045CA5Fh
  0045c976  66395e10              cmp       [esi+10h],bx
  0045c97a  0f85df000000          jne       near 0045CA5Fh
  0045c980  668b4628              mov       ax,[esi+28h]
  0045c984  ba00100000            mov       edx,1000h
  0045c989  663bc2                cmp       ax,dx
  0045c98c  7d27                  jge       short 0045C9B5h
  0045c98e  b9c8000000            mov       ecx,0C8h
  0045c993  05c8000000            add       eax,0C8h
  0045c998  66014e2a              add       [esi+2Ah],cx
  0045c99c  66014e2c              add       [esi+2Ch],cx
  0045c9a0  663bc2                cmp       ax,dx
  0045c9a3  66894628              mov       [esi+28h],ax
  0045c9a7  7e0c                  jle       short 0045C9B5h
  0045c9a9  66895628              mov       [esi+28h],dx
  0045c9ad  6689562a              mov       [esi+2Ah],dx
  0045c9b1  6689562c              mov       [esi+2Ch],dx

LAB_0045c9b5:
  0045c9b5  a168e05d00            mov       eax,[5DE068h]
  0045c9ba  8b7e44                mov       edi,[esi+44h]
  0045c9bd  8b0d70e05d00          mov       ecx,[5DE070h]
  0045c9c3  8b6e4c                mov       ebp,[esi+4Ch]
  0045c9c6  c1f80c                sar       eax,0Ch
  0045c9c9  2bc7                  sub       eax,edi
  0045c9cb  c1f90c                sar       ecx,0Ch
  0045c9ce  8bd0                  mov       edx,eax
  0045c9d0  2bcd                  sub       ecx,ebp
  0045c9d2  0fafd0                imul      edx,eax
  0045c9d5  8bc1                  mov       eax,ecx
  0045c9d7  0fafc1                imul      eax,ecx
  0045c9da  03d0                  add       edx,eax
  0045c9dc  52                    push      edx
  0045c9dd  e82e900e00            call      00545A10h
  0045c9e2  8b4e48                mov       ecx,[esi+48h]
  0045c9e5  8bf8                  mov       edi,eax
  0045c9e7  a168e05d00            mov       eax,[5DE068h]
  0045c9ec  0fbf969a000000        movsx     edx,word [esi+9Ah]
  0045c9f3  c1f80c                sar       eax,0Ch
  0045c9f6  2bc1                  sub       eax,ecx
  0045c9f8  8b0d70e05d00          mov       ecx,[5DE070h]
  0045c9fe  c1f90c                sar       ecx,0Ch
  0045ca01  2bca                  sub       ecx,edx
  0045ca03  8bd0                  mov       edx,eax
  0045ca05  0fafd0                imul      edx,eax
  0045ca08  8bc1                  mov       eax,ecx
  0045ca0a  0fafc1                imul      eax,ecx
  0045ca0d  03d0                  add       edx,eax
  0045ca0f  52                    push      edx
  0045ca10  e8fb8f0e00            call      00545A10h
  0045ca15  3bf8                  cmp       edi,eax
  0045ca17  7e2f                  jle       short 0045CA48h
  0045ca19  0fbf969a000000        movsx     edx,word [esi+9Ah]
  0045ca20  8b4e48                mov       ecx,[esi+48h]
  0045ca23  66c746100200          mov       word [esi+10h],2
  0045ca29  c1e10c                shl       ecx,0Ch
  0045ca2c  c1e20c                shl       edx,0Ch
  0045ca2f  33c0                  xor       eax,eax
  0045ca31  894e1c                mov       [esi+1Ch],ecx
  0045ca34  895624                mov       [esi+24h],edx
  0045ca37  66894628              mov       [esi+28h],ax
  0045ca3b  6689462a              mov       [esi+2Ah],ax
  0045ca3f  6689462c              mov       [esi+2Ch],ax
  0045ca43  5f                    pop       edi
  0045ca44  5e                    pop       esi
  0045ca45  5d                    pop       ebp
  0045ca46  5b                    pop       ebx
  0045ca47  c3                    ret

LAB_0045ca48:
  0045ca48  8b4644                mov       eax,[esi+44h]
  0045ca4b  8b4e4c                mov       ecx,[esi+4Ch]
  0045ca4e  c1e00c                shl       eax,0Ch
  0045ca51  c1e10c                shl       ecx,0Ch
  0045ca54  89461c                mov       [esi+1Ch],eax
  0045ca57  894e24                mov       [esi+24h],ecx
  0045ca5a  5f                    pop       edi
  0045ca5b  5e                    pop       esi
  0045ca5c  5d                    pop       ebp
  0045ca5d  5b                    pop       ebx
  0045ca5e  c3                    ret

LAB_0045ca5f:
  0045ca5f  e80ca30000            call      00466D70h
  0045ca64  0fbfd0                movsx     edx,ax
  0045ca67  3bd5                  cmp       edx,ebp
  0045ca69  0f84e8000000          je        near 0045CB57h
  0045ca6f  66837e1002            cmp       word [esi+10h],2
  0045ca74  0f85dd000000          jne       near 0045CB57h
  0045ca7a  668b4628              mov       ax,[esi+28h]
  0045ca7e  ba00100000            mov       edx,1000h
  0045ca83  663bc2                cmp       ax,dx
  0045ca86  7d27                  jge       short 0045CAAFh
  0045ca88  b9c8000000            mov       ecx,0C8h
  0045ca8d  05c8000000            add       eax,0C8h
  0045ca92  66014e2a              add       [esi+2Ah],cx
  0045ca96  66014e2c              add       [esi+2Ch],cx
  0045ca9a  663bc2                cmp       ax,dx
  0045ca9d  66894628              mov       [esi+28h],ax
  0045caa1  7e0c                  jle       short 0045CAAFh
  0045caa3  66895628              mov       [esi+28h],dx
  0045caa7  6689562a              mov       [esi+2Ah],dx
  0045caab  6689562c              mov       [esi+2Ch],dx

LAB_0045caaf:
  0045caaf  a168e05d00            mov       eax,[5DE068h]
  0045cab4  8b0d70e05d00          mov       ecx,[5DE070h]
  0045caba  8b6e44                mov       ebp,[esi+44h]
  0045cabd  8b564c                mov       edx,[esi+4Ch]
  0045cac0  c1f80c                sar       eax,0Ch
  0045cac3  c1f90c                sar       ecx,0Ch
  0045cac6  2bc5                  sub       eax,ebp
  0045cac8  2bca                  sub       ecx,edx
  0045caca  8bd0                  mov       edx,eax
  0045cacc  0fafd0                imul      edx,eax
  0045cacf  8bc1                  mov       eax,ecx
  0045cad1  0fafc1                imul      eax,ecx
  0045cad4  03d0                  add       edx,eax
  0045cad6  52                    push      edx
  0045cad7  e8348f0e00            call      00545A10h
  0045cadc  8b0d70e05d00          mov       ecx,[5DE070h]
  0045cae2  8b6e48                mov       ebp,[esi+48h]
  0045cae5  0fbf969a000000        movsx     edx,word [esi+9Ah]
  0045caec  8bf8                  mov       edi,eax
  0045caee  a168e05d00            mov       eax,[5DE068h]
  0045caf3  c1f80c                sar       eax,0Ch
  0045caf6  c1f90c                sar       ecx,0Ch
  0045caf9  2bc5                  sub       eax,ebp
  0045cafb  2bca                  sub       ecx,edx
  0045cafd  8bd0                  mov       edx,eax
  0045caff  0fafd0                imul      edx,eax
  0045cb02  8bc1                  mov       eax,ecx
  0045cb04  0fafc1                imul      eax,ecx
  0045cb07  03d0                  add       edx,eax
  0045cb09  52                    push      edx
  0045cb0a  e8018f0e00            call      00545A10h
  0045cb0f  3bf8                  cmp       edi,eax
  0045cb11  7d29                  jge       short 0045CB3Ch
  0045cb13  8b4e44                mov       ecx,[esi+44h]
  0045cb16  8b564c                mov       edx,[esi+4Ch]
  0045cb19  c1e10c                shl       ecx,0Ch
  0045cb1c  c1e20c                shl       edx,0Ch
  0045cb1f  33c0                  xor       eax,eax
  0045cb21  894e1c                mov       [esi+1Ch],ecx
  0045cb24  895624                mov       [esi+24h],edx
  0045cb27  66895e10              mov       [esi+10h],bx
  0045cb2b  66894628              mov       [esi+28h],ax
  0045cb2f  6689462a              mov       [esi+2Ah],ax
  0045cb33  6689462c              mov       [esi+2Ch],ax
  0045cb37  5f                    pop       edi
  0045cb38  5e                    pop       esi
  0045cb39  5d                    pop       ebp
  0045cb3a  5b                    pop       ebx
  0045cb3b  c3                    ret

LAB_0045cb3c:
  0045cb3c  0fbf8e9a000000        movsx     ecx,word [esi+9Ah]
  0045cb43  8b4648                mov       eax,[esi+48h]
  0045cb46  c1e00c                shl       eax,0Ch
  0045cb49  c1e10c                shl       ecx,0Ch
  0045cb4c  89461c                mov       [esi+1Ch],eax
  0045cb4f  894e24                mov       [esi+24h],ecx
  0045cb52  5f                    pop       edi
  0045cb53  5e                    pop       esi
  0045cb54  5d                    pop       ebp
  0045cb55  5b                    pop       ebx
  0045cb56  c3                    ret

LAB_0045cb57:
  0045cb57  e814a20000            call      00466D70h
  0045cb5c  0fbfd0                movsx     edx,ax
  0045cb5f  3bd5                  cmp       edx,ebp
  0045cb61  7540                  jne       short 0045CBA3h
  0045cb63  66395e10              cmp       [esi+10h],bx
  0045cb67  753a                  jne       short 0045CBA3h
  0045cb69  bb01000000            mov       ebx,1
  0045cb6e  897e5c                mov       [esi+5Ch],edi
  0045cb71  66895e10              mov       [esi+10h],bx
  0045cb75  66897e12              mov       [esi+12h],di
  0045cb79  a1cc975e00            mov       eax,[5E97CCh]
  0045cb7e  57                    push      edi
  0045cb7f  57                    push      edi
  0045cb80  6800100000            push      1000h
  0045cb85  8b88a8000000          mov       ecx,[eax+0A8h]
  0045cb8b  6880010000            push      180h
  0045cb90  8b11                  mov       edx,[ecx]
  0045cb92  660fb6422b            movzx     ax,byte [edx+2Bh]
  0045cb97  50                    push      eax
  0045cb98  56                    push      esi
  0045cb99  e8e2b40000            call      00468080h
  0045cb9e  83c418                add       esp,18h
  0045cba1  eb05                  jmp       short 0045CBA8h

LAB_0045cba3:
  0045cba3  bb01000000            mov       ebx,1

LAB_0045cba8:
  0045cba8  66395e10              cmp       [esi+10h],bx
  0045cbac  0f85d9000000          jne       near 0045CC8Bh
  0045cbb2  0fbf4e16              movsx     ecx,word [esi+16h]
  0045cbb6  8b465c                mov       eax,[esi+5Ch]
  0045cbb9  3bc1                  cmp       eax,ecx
  0045cbbb  7d0e                  jge       short 0045CBCBh
  0045cbbd  668b5612              mov       dx,[esi+12h]
  0045cbc1  663b5614              cmp       dx,[esi+14h]
  0045cbc5  7d04                  jge       short 0045CBCBh
  0045cbc7  40                    inc       eax
  0045cbc8  89465c                mov       [esi+5Ch],eax

LAB_0045cbcb:
  0045cbcb  0fbf4632              movsx     eax,word [esi+32h]
  0045cbcf  50                    push      eax
  0045cbd0  e8478e0e00            call      00545A1Ch
  0045cbd5  0faf465c              imul      eax,[esi+5Ch]
  0045cbd9  8b7e1c                mov       edi,[esi+1Ch]
  0045cbdc  0fbf4e32              movsx     ecx,word [esi+32h]
  0045cbe0  03f8                  add       edi,eax
  0045cbe2  51                    push      ecx
  0045cbe3  897e1c                mov       [esi+1Ch],edi
  0045cbe6  8b7e5c                mov       edi,[esi+5Ch]
  0045cbe9  e8288e0e00            call      00545A16h
  0045cbee  0fafc7                imul      eax,edi
  0045cbf1  8b4e24                mov       ecx,[esi+24h]
  0045cbf4  668b565c              mov       dx,[esi+5Ch]
  0045cbf8  66015612              add       [esi+12h],dx
  0045cbfc  03c8                  add       ecx,eax
  0045cbfe  668b4612              mov       ax,[esi+12h]
  0045cc02  894e24                mov       [esi+24h],ecx
  0045cc05  663b4614              cmp       ax,[esi+14h]
  0045cc09  7e55                  jle       short 0045CC60h
  0045cc0b  6683be9800000000      cmp       word [esi+98h],0
  0045cc13  7507                  jne       short 0045CC1Ch
  0045cc15  66899e98000000        mov       [esi+98h],bx

LAB_0045cc1c:
  0045cc1c  8d47ff                lea       eax,[edi-1]
  0045cc1f  83f8ff                cmp       eax,0FFFFFFFFh
  0045cc22  89465c                mov       [esi+5Ch],eax
  0045cc25  7539                  jne       short 0045CC60h
  0045cc27  66c746100400          mov       word [esi+10h],4
  0045cc2d  66c786980000000000    mov       word [esi+98h],0
  0045cc36  a1cc975e00            mov       eax,[5E97CCh]
  0045cc3b  6a00                  push      0
  0045cc3d  6a00                  push      0
  0045cc3f  6800100000            push      1000h
  0045cc44  8b88a8000000          mov       ecx,[eax+0A8h]
  0045cc4a  6800030000            push      300h
  0045cc4f  8b11                  mov       edx,[ecx]
  0045cc51  660fb6422c            movzx     ax,byte [edx+2Ch]
  0045cc56  50                    push      eax
  0045cc57  56                    push      esi
  0045cc58  e823b40000            call      00468080h
  0045cc5d  83c418                add       esp,18h

LAB_0045cc60:
  0045cc60  6800000040            push      40000000h
  0045cc65  6a20                  push      20h
  0045cc67  56                    push      esi
  0045cc68  e893830d00            call      00535000h
  0045cc6d  83c40c                add       esp,0Ch
  0045cc70  8bf8                  mov       edi,eax
  0045cc72  e8f9a00000            call      00466D70h
  0045cc77  0fbfc8                movsx     ecx,ax
  0045cc7a  3bcf                  cmp       ecx,edi
  0045cc7c  740d                  je        short 0045CC8Bh
  0045cc7e  66c746100600          mov       word [esi+10h],6
  0045cc84  c7465c00000000        mov       dword [esi+5Ch],0

LAB_0045cc8b:
  0045cc8b  e8e0a00000            call      00466D70h
  0045cc90  0fbfd0                movsx     edx,ax
  0045cc93  3bd5                  cmp       edx,ebp
  0045cc95  bf03000000            mov       edi,3
  0045cc9a  753e                  jne       short 0045CCDAh
  0045cc9c  66837e1002            cmp       word [esi+10h],2
  0045cca1  7537                  jne       short 0045CCDAh
  0045cca3  33ed                  xor       ebp,ebp
  0045cca5  66897e10              mov       [esi+10h],di
  0045cca9  896e5c                mov       [esi+5Ch],ebp
  0045ccac  66896e12              mov       [esi+12h],bp
  0045ccb0  a1cc975e00            mov       eax,[5E97CCh]
  0045ccb5  55                    push      ebp
  0045ccb6  55                    push      ebp
  0045ccb7  6800100000            push      1000h
  0045ccbc  8b88a8000000          mov       ecx,[eax+0A8h]
  0045ccc2  6880010000            push      180h
  0045ccc7  8b11                  mov       edx,[ecx]
  0045ccc9  660fb6422b            movzx     ax,byte [edx+2Bh]
  0045ccce  50                    push      eax
  0045cccf  56                    push      esi
  0045ccd0  e8abb30000            call      00468080h
  0045ccd5  83c418                add       esp,18h
  0045ccd8  eb02                  jmp       short 0045CCDCh

LAB_0045ccda:
  0045ccda  33ed                  xor       ebp,ebp

LAB_0045ccdc:
  0045ccdc  66397e10              cmp       [esi+10h],di
  0045cce0  bb05000000            mov       ebx,5
  0045cce5  0f85d0000000          jne       near 0045CDBBh
  0045cceb  0fbf4e16              movsx     ecx,word [esi+16h]
  0045ccef  8b465c                mov       eax,[esi+5Ch]
  0045ccf2  3bc1                  cmp       eax,ecx
  0045ccf4  7d0e                  jge       short 0045CD04h
  0045ccf6  668b5612              mov       dx,[esi+12h]
  0045ccfa  663b5614              cmp       dx,[esi+14h]
  0045ccfe  7d04                  jge       short 0045CD04h
  0045cd00  40                    inc       eax
  0045cd01  89465c                mov       [esi+5Ch],eax

LAB_0045cd04:
  0045cd04  0fbf4632              movsx     eax,word [esi+32h]
  0045cd08  50                    push      eax
  0045cd09  e80e8d0e00            call      00545A1Ch
  0045cd0e  0faf465c              imul      eax,[esi+5Ch]
  0045cd12  8b7e1c                mov       edi,[esi+1Ch]
  0045cd15  0fbf4e32              movsx     ecx,word [esi+32h]
  0045cd19  2bf8                  sub       edi,eax
  0045cd1b  51                    push      ecx
  0045cd1c  897e1c                mov       [esi+1Ch],edi
  0045cd1f  8b7e5c                mov       edi,[esi+5Ch]
  0045cd22  e8ef8c0e00            call      00545A16h
  0045cd27  0fafc7                imul      eax,edi
  0045cd2a  8b4e24                mov       ecx,[esi+24h]
  0045cd2d  668b565c              mov       dx,[esi+5Ch]
  0045cd31  66015612              add       [esi+12h],dx
  0045cd35  2bc8                  sub       ecx,eax
  0045cd37  668b4612              mov       ax,[esi+12h]
  0045cd3b  894e24                mov       [esi+24h],ecx
  0045cd3e  663b4614              cmp       ax,[esi+14h]
  0045cd42  7e50                  jle       short 0045CD94h
  0045cd44  6639ae98000000        cmp       [esi+98h],bp
  0045cd4b  7509                  jne       short 0045CD56h
  0045cd4d  66c786980000000100    mov       word [esi+98h],1

LAB_0045cd56:
  0045cd56  8d47ff                lea       eax,[edi-1]
  0045cd59  83f8ff                cmp       eax,0FFFFFFFFh
  0045cd5c  89465c                mov       [esi+5Ch],eax
  0045cd5f  7533                  jne       short 0045CD94h
  0045cd61  66895e10              mov       [esi+10h],bx
  0045cd65  6689ae98000000        mov       [esi+98h],bp
  0045cd6c  a1cc975e00            mov       eax,[5E97CCh]
  0045cd71  55                    push      ebp
  0045cd72  55                    push      ebp
  0045cd73  6800100000            push      1000h
  0045cd78  8b88a8000000          mov       ecx,[eax+0A8h]
  0045cd7e  6800030000            push      300h
  0045cd83  8b11                  mov       edx,[ecx]
  0045cd85  660fb6422c            movzx     ax,byte [edx+2Ch]
  0045cd8a  50                    push      eax
  0045cd8b  56                    push      esi
  0045cd8c  e8efb20000            call      00468080h
  0045cd91  83c418                add       esp,18h

LAB_0045cd94:
  0045cd94  6800000040            push      40000000h
  0045cd99  6a20                  push      20h
  0045cd9b  56                    push      esi
  0045cd9c  e85f820d00            call      00535000h
  0045cda1  83c40c                add       esp,0Ch
  0045cda4  8bf8                  mov       edi,eax
  0045cda6  e8c59f0000            call      00466D70h
  0045cdab  0fbfc8                movsx     ecx,ax
  0045cdae  3bcf                  cmp       ecx,edi
  0045cdb0  7409                  je        short 0045CDBBh
  0045cdb2  66c746100700          mov       word [esi+10h],7
  0045cdb8  896e5c                mov       [esi+5Ch],ebp

LAB_0045cdbb:
  0045cdbb  66837e1004            cmp       word [esi+10h],4
  0045cdc0  750c                  jne       short 0045CDCEh
  0045cdc2  396c2414              cmp       [esp+14h],ebp
  0045cdc6  7506                  jne       short 0045CDCEh
  0045cdc8  66c746100200          mov       word [esi+10h],2

LAB_0045cdce:
  0045cdce  66395e10              cmp       [esi+10h],bx
  0045cdd2  750c                  jne       short 0045CDE0h
  0045cdd4  396c2414              cmp       [esp+14h],ebp
  0045cdd8  7506                  jne       short 0045CDE0h
  0045cdda  66c746100800          mov       word [esi+10h],8

LAB_0045cde0:
  0045cde0  66837e1006            cmp       word [esi+10h],6
  0045cde5  b938ffffff            mov       ecx,0FFFFFF38h
  0045cdea  752c                  jne       short 0045CE18h
  0045cdec  396e5c                cmp       [esi+5Ch],ebp
  0045cdef  7527                  jne       short 0045CE18h
  0045cdf1  66014e28              add       [esi+28h],cx
  0045cdf5  668b4628              mov       ax,[esi+28h]
  0045cdf9  66014e2a              add       [esi+2Ah],cx
  0045cdfd  66014e2c              add       [esi+2Ch],cx
  0045ce01  663bc5                cmp       ax,bp
  0045ce04  7f12                  jg        short 0045CE18h
  0045ce06  66896e28              mov       [esi+28h],bp
  0045ce0a  66896e2a              mov       [esi+2Ah],bp
  0045ce0e  66896e2c              mov       [esi+2Ch],bp
  0045ce12  66c746100800          mov       word [esi+10h],8

LAB_0045ce18:
  0045ce18  66837e1007            cmp       word [esi+10h],7
  0045ce1d  752c                  jne       short 0045CE4Bh
  0045ce1f  396e5c                cmp       [esi+5Ch],ebp
  0045ce22  7527                  jne       short 0045CE4Bh
  0045ce24  66014e28              add       [esi+28h],cx
  0045ce28  668b4628              mov       ax,[esi+28h]
  0045ce2c  66014e2a              add       [esi+2Ah],cx
  0045ce30  66014e2c              add       [esi+2Ch],cx
  0045ce34  663bc5                cmp       ax,bp
  0045ce37  7f12                  jg        short 0045CE4Bh
  0045ce39  66896e28              mov       [esi+28h],bp
  0045ce3d  66896e2a              mov       [esi+2Ah],bp
  0045ce41  66896e2c              mov       [esi+2Ch],bp
  0045ce45  66c746100200          mov       word [esi+10h],2

LAB_0045ce4b:
  0045ce4b  5f                    pop       edi
  0045ce4c  5e                    pop       esi
  0045ce4d  5d                    pop       ebp
  0045ce4e  5b                    pop       ebx
  0045ce4f  c3                    ret
  0045ce50  53                    push      ebx
  0045ce51  56                    push      esi
  0045ce52  8b74240c              mov       esi,[esp+0Ch]
  0045ce56  56                    push      esi
  0045ce57  e864410000            call      00460FC0h
  0045ce5c  83c404                add       esp,4
  0045ce5f  66837e1000            cmp       word [esi+10h],0
  0045ce64  b801000000            mov       eax,1
  0045ce69  750a                  jne       short 0045CE75h
  0045ce6b  66894610              mov       [esi+10h],ax
  0045ce6f  66c746120000          mov       word [esi+12h],0

LAB_0045ce75:
  0045ce75  66394610              cmp       [esi+10h],ax
  0045ce79  7554                  jne       short 0045CECFh
  0045ce7b  668146120001          add       word [esi+12h],100h
  0045ce81  0fbf4612              movsx     eax,word [esi+12h]
  0045ce85  50                    push      eax
  0045ce86  e8918b0e00            call      00545A1Ch
  0045ce8b  8bd8                  mov       ebx,eax
  0045ce8d  c1fb08                sar       ebx,8
  0045ce90  e8aba20e00            call      00547140h
  0045ce95  99                    cdq
  0045ce96  33c2                  xor       eax,edx
  0045ce98  2bc2                  sub       eax,edx
  0045ce9a  83e00f                and       eax,0Fh
  0045ce9d  33c2                  xor       eax,edx
  0045ce9f  2bc2                  sub       eax,edx
  0045cea1  2ac3                  sub       al,bl
  0045cea3  0478                  add       al,78h
  0045cea5  8844240c              mov       [esp+0Ch],al
  0045cea9  8b44240c              mov       eax,[esp+0Ch]
  0045cead  25ff000000            and       eax,0FFh
  0045ceb2  8bc8                  mov       ecx,eax
  0045ceb4  c1e108                shl       ecx,8
  0045ceb7  0bc8                  or        ecx,eax
  0045ceb9  c1e108                shl       ecx,8
  0045cebc  0bc8                  or        ecx,eax
  0045cebe  66817e120010          cmp       word [esi+12h],1000h
  0045cec4  894e38                mov       [esi+38h],ecx
  0045cec7  7506                  jne       short 0045CECFh
  0045cec9  66c746120000          mov       word [esi+12h],0

LAB_0045cecf:
  0045cecf  5e                    pop       esi
  0045ced0  5b                    pop       ebx
  0045ced1  c3                    ret
  0045ced2  90                    nop
  0045ced3  90                    nop
  0045ced4  90                    nop
  0045ced5  90                    nop
  0045ced6  90                    nop
  0045ced7  90                    nop
  0045ced8  90                    nop
  0045ced9  90                    nop
  0045ceda  90                    nop
  0045cedb  90                    nop
  0045cedc  90                    nop
  0045cedd  90                    nop
  0045cede  90                    nop
  0045cedf  90                    nop

28 cible(s) de saut interne : 0x0045c8b5, 0x0045c8ce, 0x0045c91f, 0x0045c94b, 0x0045c9b5, 0x0045ca48, 0x0045ca5f, 0x0045caaf, 0x0045cb3c, 0x0045cb57, 0x0045cba3, 0x0045cba8, 0x0045cbcb, 0x0045cc1c, 0x0045cc60, 0x0045cc8b, 0x0045ccda, 0x0045ccdc, 0x0045cd04, 0x0045cd56, 0x0045cd94, 0x0045cdbb, 0x0045cdce, 0x0045cde0, 0x0045ce18, 0x0045ce4b, 0x0045ce75, 0x0045cecf
