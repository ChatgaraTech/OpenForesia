Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042e940  section .text  offset fichier 0x2dd40  752 octets

  0042e940  53                    push      ebx
  0042e941  56                    push      esi
  0042e942  8b74240c              mov       esi,[esp+0Ch]
  0042e946  33db                  xor       ebx,ebx
  0042e948  57                    push      edi
  0042e949  bf01000000            mov       edi,1
  0042e94e  66395e10              cmp       [esi+10h],bx
  0042e952  0f85cb000000          jne       near 0042EA23h
  0042e958  c7463c80095500        mov       dword [esi+3Ch],550980h
  0042e95f  885e40                mov       [esi+40h],bl
  0042e962  c6460a0b              mov       byte [esi+0Ah],0Bh
  0042e966  885e0b                mov       [esi+0Bh],bl
  0042e969  66897e10              mov       [esi+10h],di
  0042e96d  e8ce871100            call      00547140h
  0042e972  99                    cdq
  0042e973  33c2                  xor       eax,edx
  0042e975  2bc2                  sub       eax,edx
  0042e977  25ff0f0000            and       eax,0FFFh
  0042e97c  33c2                  xor       eax,edx
  0042e97e  2bc2                  sub       eax,edx
  0042e980  66894612              mov       [esi+12h],ax
  0042e984  e8b7871100            call      00547140h
  0042e989  99                    cdq
  0042e98a  33c2                  xor       eax,edx
  0042e98c  2bc2                  sub       eax,edx
  0042e98e  25ff030000            and       eax,3FFh
  0042e993  33c2                  xor       eax,edx
  0042e995  2bc2                  sub       eax,edx
  0042e997  66894614              mov       [esi+14h],ax
  0042e99b  e8a0871100            call      00547140h
  0042e9a0  99                    cdq
  0042e9a1  b964000000            mov       ecx,64h
  0042e9a6  f7f9                  idiv      ecx
  0042e9a8  81c290010000          add       edx,190h
  0042e9ae  6689969a000000        mov       [esi+9Ah],dx
  0042e9b5  e886871100            call      00547140h
  0042e9ba  99                    cdq
  0042e9bb  b90f000000            mov       ecx,0Fh
  0042e9c0  f7f9                  idiv      ecx
  0042e9c2  8b4620                mov       eax,[esi+20h]
  0042e9c5  8b4e24                mov       ecx,[esi+24h]
  0042e9c8  894654                mov       [esi+54h],eax
  0042e9cb  894e58                mov       [esi+58h],ecx
  0042e9ce  83c20d                add       edx,0Dh
  0042e9d1  89565c                mov       [esi+5Ch],edx
  0042e9d4  8b561c                mov       edx,[esi+1Ch]
  0042e9d7  895650                mov       [esi+50h],edx
  0042e9da  e861871100            call      00547140h
  0042e9df  99                    cdq
  0042e9e0  33c2                  xor       eax,edx
  0042e9e2  885e38                mov       [esi+38h],bl
  0042e9e5  2bc2                  sub       eax,edx
  0042e9e7  885e39                mov       [esi+39h],bl
  0042e9ea  25ff010000            and       eax,1FFh
  0042e9ef  885e3a                mov       [esi+3Ah],bl
  0042e9f2  33c2                  xor       eax,edx
  0042e9f4  2bc2                  sub       eax,edx
  0042e9f6  0500040000            add       eax,400h
  0042e9fb  66894628              mov       [esi+28h],ax
  0042e9ff  6689462a              mov       [esi+2Ah],ax
  0042ea03  e838871100            call      00547140h
  0042ea08  99                    cdq
  0042ea09  b964000000            mov       ecx,64h
  0042ea0e  f7f9                  idiv      ecx
  0042ea10  895648                mov       [esi+48h],edx
  0042ea13  e828871100            call      00547140h
  0042ea18  99                    cdq
  0042ea19  b964000000            mov       ecx,64h
  0042ea1e  f7f9                  idiv      ecx
  0042ea20  89564c                mov       [esi+4Ch],edx

LAB_0042ea23:
  0042ea23  66397e10              cmp       [esi+10h],di
  0042ea27  0f8513010000          jne       near 0042EB40h
  0042ea2d  0fbf5614              movsx     edx,word [esi+14h]
  0042ea31  52                    push      edx
  0042ea32  e8df6f1100            call      00545A16h
  0042ea37  8bf8                  mov       edi,eax
  0042ea39  0fbf869a000000        movsx     eax,word [esi+9Ah]
  0042ea40  0fbf4e12              movsx     ecx,word [esi+12h]
  0042ea44  0faff8                imul      edi,eax
  0042ea47  51                    push      ecx
  0042ea48  c1ff0c                sar       edi,0Ch
  0042ea4b  e8cc6f1100            call      00545A1Ch
  0042ea50  0faff8                imul      edi,eax
  0042ea53  037e50                add       edi,[esi+50h]
  0042ea56  0fbf5614              movsx     edx,word [esi+14h]
  0042ea5a  52                    push      edx
  0042ea5b  897e1c                mov       [esi+1Ch],edi
  0042ea5e  e8b36f1100            call      00545A16h
  0042ea63  8bf8                  mov       edi,eax
  0042ea65  0fbf869a000000        movsx     eax,word [esi+9Ah]
  0042ea6c  0fbf4e12              movsx     ecx,word [esi+12h]
  0042ea70  0faff8                imul      edi,eax
  0042ea73  51                    push      ecx
  0042ea74  c1ff0c                sar       edi,0Ch
  0042ea77  e89a6f1100            call      00545A16h
  0042ea7c  0faff8                imul      edi,eax
  0042ea7f  8b4e58                mov       ecx,[esi+58h]
  0042ea82  0fbf5614              movsx     edx,word [esi+14h]
  0042ea86  03f9                  add       edi,ecx
  0042ea88  52                    push      edx
  0042ea89  897e24                mov       [esi+24h],edi
  0042ea8c  668bbe9a000000        mov       di,[esi+9Ah]
  0042ea93  e8846f1100            call      00545A1Ch
  0042ea98  0fbfcf                movsx     ecx,di
  0042ea9b  0fafc1                imul      eax,ecx
  0042ea9e  8b4e54                mov       ecx,[esi+54h]
  0042eaa1  668b5648              mov       dx,[esi+48h]
  0042eaa5  66015612              add       [esi+12h],dx
  0042eaa9  668b564c              mov       dx,[esi+4Ch]
  0042eaad  66015614              add       [esi+14h],dx
  0042eab1  03c1                  add       eax,ecx
  0042eab3  668b4e12              mov       cx,[esi+12h]
  0042eab7  668b5614              mov       dx,[esi+14h]
  0042eabb  894620                mov       [esi+20h],eax
  0042eabe  668bc7                mov       ax,di
  0042eac1  662b465c              sub       ax,[esi+5Ch]
  0042eac5  663d1801              cmp       ax,118h
  0042eac9  6689869a000000        mov       [esi+9Ah],ax
  0042ead0  7d16                  jge       short 0042EAE8h
  0042ead2  8b4648                mov       eax,[esi+48h]
  0042ead5  d1f8                  sar       eax,1
  0042ead7  2bc8                  sub       ecx,eax
  0042ead9  66894e12              mov       [esi+12h],cx
  0042eadd  8b4e4c                mov       ecx,[esi+4Ch]
  0042eae0  d1f9                  sar       ecx,1
  0042eae2  2bd1                  sub       edx,ecx
  0042eae4  66895614              mov       [esi+14h],dx

LAB_0042eae8:
  0042eae8  8b7e5c                mov       edi,[esi+5Ch]
  0042eaeb  6a0a                  push      0Ah
  0042eaed  6a0a                  push      0Ah
  0042eaef  4f                    dec       edi
  0042eaf0  6a0a                  push      0Ah
  0042eaf2  56                    push      esi
  0042eaf3  897e5c                mov       [esi+5Ch],edi
  0042eaf6  e8e562fdff            call      00404DE0h
  0042eafb  8b4e5c                mov       ecx,[esi+5Ch]
  0042eafe  b802000000            mov       eax,2
  0042eb03  83c410                add       esp,10h
  0042eb06  3bc8                  cmp       ecx,eax
  0042eb08  7f03                  jg        short 0042EB0Dh
  0042eb0a  89465c                mov       [esi+5Ch],eax

LAB_0042eb0d:
  0042eb0d  66837e163c            cmp       word [esi+16h],3Ch
  0042eb12  7e2c                  jle       short 0042EB40h
  0042eb14  66c746100300          mov       word [esi+10h],3
  0042eb1a  e821861100            call      00547140h
  0042eb1f  99                    cdq
  0042eb20  b914000000            mov       ecx,14h
  0042eb25  f7f9                  idiv      ecx
  0042eb27  83c228                add       edx,28h
  0042eb2a  89565c                mov       [esi+5Ch],edx
  0042eb2d  e80e861100            call      00547140h
  0042eb32  99                    cdq
  0042eb33  b996000000            mov       ecx,96h
  0042eb38  f7f9                  idiv      ecx
  0042eb3a  83c246                add       edx,46h
  0042eb3d  895644                mov       [esi+44h],edx

LAB_0042eb40:
  0042eb40  66837e1003            cmp       word [esi+10h],3
  0042eb45  0f85da000000          jne       near 0042EC25h
  0042eb4b  0fbf5614              movsx     edx,word [esi+14h]
  0042eb4f  52                    push      edx
  0042eb50  e8c16e1100            call      00545A16h
  0042eb55  8bf8                  mov       edi,eax
  0042eb57  0fbf869a000000        movsx     eax,word [esi+9Ah]
  0042eb5e  0fbf4e12              movsx     ecx,word [esi+12h]
  0042eb62  0faff8                imul      edi,eax
  0042eb65  51                    push      ecx
  0042eb66  c1ff0c                sar       edi,0Ch
  0042eb69  e8ae6e1100            call      00545A1Ch
  0042eb6e  0faff8                imul      edi,eax
  0042eb71  037e50                add       edi,[esi+50h]
  0042eb74  0fbf5614              movsx     edx,word [esi+14h]
  0042eb78  52                    push      edx
  0042eb79  897e1c                mov       [esi+1Ch],edi
  0042eb7c  e8956e1100            call      00545A16h
  0042eb81  8bf8                  mov       edi,eax
  0042eb83  0fbf869a000000        movsx     eax,word [esi+9Ah]
  0042eb8a  0fbf4e12              movsx     ecx,word [esi+12h]
  0042eb8e  0faff8                imul      edi,eax
  0042eb91  51                    push      ecx
  0042eb92  c1ff0c                sar       edi,0Ch
  0042eb95  e87c6e1100            call      00545A16h
  0042eb9a  0faff8                imul      edi,eax
  0042eb9d  8b4e58                mov       ecx,[esi+58h]
  0042eba0  0fbf5614              movsx     edx,word [esi+14h]
  0042eba4  03f9                  add       edi,ecx
  0042eba6  52                    push      edx
  0042eba7  897e24                mov       [esi+24h],edi
  0042ebaa  668bbe9a000000        mov       di,[esi+9Ah]
  0042ebb1  e8666e1100            call      00545A1Ch
  0042ebb6  0fbfcf                movsx     ecx,di
  0042ebb9  0fafc1                imul      eax,ecx
  0042ebbc  8b4e54                mov       ecx,[esi+54h]
  0042ebbf  668b565c              mov       dx,[esi+5Ch]
  0042ebc3  6afb                  push      0FFFFFFFBh
  0042ebc5  6af9                  push      0FFFFFFF9h
  0042ebc7  03c1                  add       eax,ecx
  0042ebc9  6603d7                add       dx,di
  0042ebcc  6af9                  push      0FFFFFFF9h
  0042ebce  56                    push      esi
  0042ebcf  894620                mov       [esi+20h],eax
  0042ebd2  6689969a000000        mov       [esi+9Ah],dx
  0042ebd9  e80262fdff            call      00404DE0h
  0042ebde  668b4644              mov       ax,[esi+44h]
  0042ebe2  ba00020000            mov       edx,200h
  0042ebe7  66d1e0                shl       ax,1
  0042ebea  66294628              sub       [esi+28h],ax
  0042ebee  668b4e28              mov       cx,[esi+28h]
  0042ebf2  6629462a              sub       [esi+2Ah],ax
  0042ebf6  668b462a              mov       ax,[esi+2Ah]
  0042ebfa  83c410                add       esp,10h
  0042ebfd  663bca                cmp       cx,dx
  0042ec00  7d04                  jge       short 0042EC06h
  0042ec02  66895628              mov       [esi+28h],dx

LAB_0042ec06:
  0042ec06  663bc2                cmp       ax,dx
  0042ec09  7d04                  jge       short 0042EC0Fh
  0042ec0b  6689562a              mov       [esi+2Ah],dx

LAB_0042ec0f:
  0042ec0f  8b4e44                mov       ecx,[esi+44h]
  0042ec12  8a463a                mov       al,[esi+3Ah]
  0042ec15  83c1fb                add       ecx,0FFFFFFFBh
  0042ec18  3ac3                  cmp       al,bl
  0042ec1a  894e44                mov       [esi+44h],ecx
  0042ec1d  7506                  jne       short 0042EC25h
  0042ec1f  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0042ec25:
  0042ec25  66ff4616              inc       word [esi+16h]
  0042ec29  5f                    pop       edi
  0042ec2a  5e                    pop       esi
  0042ec2b  5b                    pop       ebx
  0042ec2c  c3                    ret
  0042ec2d  90                    nop
  0042ec2e  90                    nop
  0042ec2f  90                    nop

7 cible(s) de saut interne : 0x0042ea23, 0x0042eae8, 0x0042eb0d, 0x0042eb40, 0x0042ec06, 0x0042ec0f, 0x0042ec25
