
Legacy/Binaries/Blaze.exe  base 0x00400000
0x00424ae0  section .text  offset fichier 0x23ee0  400 octets

  00424ae0  53                    push      ebx
  00424ae1  56                    push      esi
  00424ae2  8b74240c              mov       esi,[esp+0Ch]
  00424ae6  33db                  xor       ebx,ebx
  00424ae8  57                    push      edi
  00424ae9  bf01000000            mov       edi,1
  00424aee  66395e10              cmp       [esi+10h],bx
  00424af2  0f8596000000          jne       near 00424B8Eh
  00424af8  b800200000            mov       eax,2000h
  00424afd  66897e10              mov       [esi+10h],di
  00424b01  c6460a02              mov       byte [esi+0Ah],2
  00424b05  885e0b                mov       [esi+0Bh],bl
  00424b08  c7463c10045500        mov       dword [esi+3Ch],550410h
  00424b0f  885e40                mov       [esi+40h],bl
  00424b12  66894628              mov       [esi+28h],ax
  00424b16  6689462a              mov       [esi+2Ah],ax
  00424b1a  e821261200            call      00547140h
  00424b1f  99                    cdq
  00424b20  33c2                  xor       eax,edx
  00424b22  2bc2                  sub       eax,edx
  00424b24  25ff0f0000            and       eax,0FFFh
  00424b29  33c2                  xor       eax,edx
  00424b2b  2bc2                  sub       eax,edx
  00424b2d  66894612              mov       [esi+12h],ax
  00424b31  e80a261200            call      00547140h
  00424b36  99                    cdq
  00424b37  b90a000000            mov       ecx,0Ah
  00424b3c  f7f9                  idiv      ecx
  00424b3e  03d1                  add       edx,ecx
  00424b40  66895614              mov       [esi+14h],dx
  00424b44  e8f7251200            call      00547140h
  00424b49  99                    cdq
  00424b4a  b905000000            mov       ecx,5
  00424b4f  895e5c                mov       [esi+5Ch],ebx
  00424b52  f7f9                  idiv      ecx
  00424b54  8b4620                mov       eax,[esi+20h]
  00424b57  66899e9a000000        mov       [esi+9Ah],bx
  00424b5e  66899e98000000        mov       [esi+98h],bx
  00424b65  83c203                add       edx,3
  00424b68  0500200300            add       eax,32000h
  00424b6d  894620                mov       [esi+20h],eax
  00424b70  894654                mov       [esi+54h],eax
  00424b73  8b4624                mov       eax,[esi+24h]
  00424b76  66895616              mov       [esi+16h],dx
  00424b7a  8b561c                mov       edx,[esi+1Ch]
  00424b7d  894658                mov       [esi+58h],eax
  00424b80  b080                  mov       al,80h
  00424b82  895650                mov       [esi+50h],edx
  00424b85  884638                mov       [esi+38h],al
  00424b88  884639                mov       [esi+39h],al
  00424b8b  88463a                mov       [esi+3Ah],al

LAB_00424b8e:
  00424b8e  66397e10              cmp       [esi+10h],di
  00424b92  0f85cc000000          jne       near 00424C64h
  00424b98  66399e98000000        cmp       [esi+98h],bx
  00424b9f  7519                  jne       short 00424BBAh
  00424ba1  6800100000            push      1000h
  00424ba6  6800100000            push      1000h
  00424bab  6800040000            push      400h
  00424bb0  6a22                  push      22h
  00424bb2  e8e9821100            call      0053CEA0h
  00424bb7  83c410                add       esp,10h

LAB_00424bba:
  00424bba  6639be98000000        cmp       [esi+98h],di
  00424bc1  7519                  jne       short 00424BDCh
  00424bc3  68000e0000            push      0E00h
  00424bc8  68000e0000            push      0E00h
  00424bcd  6800020000            push      200h
  00424bd2  6a22                  push      22h
  00424bd4  e8c7821100            call      0053CEA0h
  00424bd9  83c410                add       esp,10h

LAB_00424bdc:
  00424bdc  0fbf4e12              movsx     ecx,word [esi+12h]
  00424be0  66ff8698000000        inc       word [esi+98h]
  00424be7  51                    push      ecx
  00424be8  e82f0e1200            call      00545A1Ch
  00424bed  0faf465c              imul      eax,[esi+5Ch]
  00424bf1  0fbf5612              movsx     edx,word [esi+12h]
  00424bf5  8b4e50                mov       ecx,[esi+50h]
  00424bf8  8b7e5c                mov       edi,[esi+5Ch]
  00424bfb  03c1                  add       eax,ecx
  00424bfd  52                    push      edx
  00424bfe  89461c                mov       [esi+1Ch],eax
  00424c01  e8100e1200            call      00545A16h
  00424c06  0fafc7                imul      eax,edi
  00424c09  8b4e58                mov       ecx,[esi+58h]
  00424c0c  6afe                  push      0FFFFFFFEh
  00424c0e  03c1                  add       eax,ecx
  00424c10  8b4e20                mov       ecx,[esi+20h]
  00424c13  894624                mov       [esi+24h],eax
  00424c16  6afe                  push      0FFFFFFFEh
  00424c18  0fbf4616              movsx     eax,word [esi+16h]
  00424c1c  c1e00c                shl       eax,0Ch
  00424c1f  03c8                  add       ecx,eax
  00424c21  6afe                  push      0FFFFFFFEh
  00424c23  894e20                mov       [esi+20h],ecx
  00424c26  56                    push      esi
  00424c27  0fbf4e14              movsx     ecx,word [esi+14h]
  00424c2b  03cf                  add       ecx,edi
  00424c2d  66ff869a000000        inc       word [esi+9Ah]
  00424c34  894e5c                mov       [esi+5Ch],ecx
  00424c37  e8a401feff            call      00404DE0h
  00424c3c  83c410                add       esp,10h
  00424c3f  6683be9a00000003      cmp       word [esi+9Ah],3
  00424c47  7e1b                  jle       short 00424C64h
  00424c49  8a4e40                mov       cl,[esi+40h]
  00424c4c  66899e9a000000        mov       [esi+9Ah],bx
  00424c53  fec1                  inc       cl
  00424c55  8ac1                  mov       al,cl
  00424c57  884e40                mov       [esi+40h],cl
  00424c5a  3c04                  cmp       al,4
  00424c5c  7506                  jne       short 00424C64h
  00424c5e  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00424c64:
  00424c64  5f                    pop       edi
  00424c65  5e                    pop       esi
  00424c66  5b                    pop       ebx
  00424c67  c3                    ret

4 cible(s) de saut interne : 0x00424b8e, 0x00424bba, 0x00424bdc, 0x00424c64
