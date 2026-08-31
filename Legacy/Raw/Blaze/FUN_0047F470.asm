
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047f470  section .text  offset fichier 0x7e870  246 octets

  0047f470  83ec0c                sub       esp,0Ch
  0047f473  56                    push      esi
  0047f474  8b742414              mov       esi,[esp+14h]
  0047f478  57                    push      edi
  0047f479  66c74424080004        mov       word [esp+8],400h
  0047f480  668b4628              mov       ax,[esi+28h]
  0047f484  8b5650                mov       edx,[esi+50h]
  0047f487  0fbfc8                movsx     ecx,ax
  0047f48a  2bd1                  sub       edx,ecx
  0047f48c  8b7e58                mov       edi,[esi+58h]
  0047f48f  c1fa02                sar       edx,2
  0047f492  03d0                  add       edx,eax
  0047f494  668b462a              mov       ax,[esi+2Ah]
  0047f498  0fbfc8                movsx     ecx,ax
  0047f49b  66895628              mov       [esi+28h],dx
  0047f49f  8b5654                mov       edx,[esi+54h]
  0047f4a2  2bd1                  sub       edx,ecx
  0047f4a4  668b4e2c              mov       cx,[esi+2Ch]
  0047f4a8  c1fa02                sar       edx,2
  0047f4ab  03c2                  add       eax,edx
  0047f4ad  66c744240ad8ff        mov       word [esp+0Ah],0FFD8h
  0047f4b4  6689462a              mov       [esi+2Ah],ax
  0047f4b8  66c744240c3c00        mov       word [esp+0Ch],3Ch
  0047f4bf  0fbfc0                movsx     eax,ax
  0047f4c2  0fbfd1                movsx     edx,cx
  0047f4c5  8d0440                lea       eax,[eax+eax*2]
  0047f4c8  2bfa                  sub       edi,edx
  0047f4ca  c1ff02                sar       edi,2
  0047f4cd  8d0480                lea       eax,[eax+eax*4]
  0047f4d0  03f9                  add       edi,ecx
  0047f4d2  8d4c2408              lea       ecx,[esp+8]
  0047f4d6  66c744240e6400        mov       word [esp+0Eh],64h
  0047f4dd  8d0480                lea       eax,[eax+eax*4]
  0047f4e0  51                    push      ecx
  0047f4e1  d1e0                  shl       eax,1
  0047f4e3  c1f80c                sar       eax,0Ch
  0047f4e6  83c028                add       eax,28h
  0047f4e9  56                    push      esi
  0047f4ea  66c74424180000        mov       word [esp+18h],0
  0047f4f1  66897e2c              mov       [esi+2Ch],di
  0047f4f5  668944241a            mov       [esp+1Ah],ax
  0047f4fa  e8a157ffff            call      00474CA0h
  0047f4ff  8bf8                  mov       edi,eax
  0047f501  83c408                add       esp,8
  0047f504  85ff                  test      edi,edi
  0047f506  743b                  je        short 0047F543h
  0047f508  8b5674                mov       edx,[esi+74h]
  0047f50b  668b4606              mov       ax,[esi+6]
  0047f50f  6a00                  push      0
  0047f511  57                    push      edi
  0047f512  66894206              mov       [edx+6],ax
  0047f516  8b4e74                mov       ecx,[esi+74h]
  0047f519  51                    push      ecx
  0047f51a  e8d1c30b00            call      0053B8F0h
  0047f51f  83c40c                add       esp,0Ch
  0047f522  85c0                  test      eax,eax
  0047f524  740e                  je        short 0047F534h
  0047f526  c7475000000000        mov       dword [edi+50h],0
  0047f52d  c7475800000000        mov       dword [edi+58h],0

LAB_0047f534:
  0047f534  8b07                  mov       eax,[edi]
  0047f536  66c787960000000800    mov       word [edi+96h],8
  0047f53f  0c40                  or        al,40h
  0047f541  8907                  mov       [edi],eax

LAB_0047f543:
  0047f543  8b4638                mov       eax,[esi+38h]
  0047f546  05f0efefff            add       eax,0FFEFEFF0h
  0047f54b  894638                mov       [esi+38h],eax
  0047f54e  7510                  jne       short 0047F560h
  0047f550  8b4674                mov       eax,[esi+74h]
  0047f553  8b08                  mov       ecx,[eax]
  0047f555  80e17f                and       cl,7Fh
  0047f558  8908                  mov       [eax],ecx
  0047f55a  c70600000000          mov       dword [esi],0

LAB_0047f560:
  0047f560  5f                    pop       edi
  0047f561  5e                    pop       esi
  0047f562  83c40c                add       esp,0Ch
  0047f565  c3                    ret

3 cible(s) de saut interne : 0x0047f534, 0x0047f543, 0x0047f560
