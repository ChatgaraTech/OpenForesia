Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00430c40  section .text  offset fichier 0x30040  480 octets

  00430c40  56                    push      esi
  00430c41  8b742408              mov       esi,[esp+8]
  00430c45  66837e1000            cmp       word [esi+10h],0
  00430c4a  0f854c010000          jne       near 00430D9Ch
  00430c50  c7463c680b5500        mov       dword [esi+3Ch],550B68h
  00430c57  e8e4641100            call      00547140h
  00430c5c  99                    cdq
  00430c5d  33c2                  xor       eax,edx
  00430c5f  2bc2                  sub       eax,edx
  00430c61  25ff030000            and       eax,3FFh
  00430c66  33c2                  xor       eax,edx
  00430c68  2bc2                  sub       eax,edx
  00430c6a  05000c0000            add       eax,0C00h
  00430c6f  66894628              mov       [esi+28h],ax
  00430c73  e8c8641100            call      00547140h
  00430c78  99                    cdq
  00430c79  33c2                  xor       eax,edx
  00430c7b  c6460a09              mov       byte [esi+0Ah],9
  00430c7f  2bc2                  sub       eax,edx
  00430c81  c6460b00              mov       byte [esi+0Bh],0
  00430c85  25ff030000            and       eax,3FFh
  00430c8a  33c2                  xor       eax,edx
  00430c8c  2bc2                  sub       eax,edx
  00430c8e  05000c0000            add       eax,0C00h
  00430c93  6689462a              mov       [esi+2Ah],ax
  00430c97  e8a4641100            call      00547140h
  00430c9c  99                    cdq
  00430c9d  b9c8000000            mov       ecx,0C8h
  00430ca2  f7f9                  idiv      ecx
  00430ca4  8b4e1c                mov       ecx,[esi+1Ch]
  00430ca7  83ea64                sub       edx,64h
  00430caa  c1e20c                shl       edx,0Ch
  00430cad  03ca                  add       ecx,edx
  00430caf  894e1c                mov       [esi+1Ch],ecx
  00430cb2  e889641100            call      00547140h
  00430cb7  99                    cdq
  00430cb8  b932000000            mov       ecx,32h
  00430cbd  f7f9                  idiv      ecx
  00430cbf  8b4620                mov       eax,[esi+20h]
  00430cc2  83ea19                sub       edx,19h
  00430cc5  c1e20c                shl       edx,0Ch
  00430cc8  03c2                  add       eax,edx
  00430cca  894620                mov       [esi+20h],eax
  00430ccd  e86e641100            call      00547140h
  00430cd2  99                    cdq
  00430cd3  b9c8000000            mov       ecx,0C8h
  00430cd8  f7f9                  idiv      ecx
  00430cda  8b4e24                mov       ecx,[esi+24h]
  00430cdd  83ea64                sub       edx,64h
  00430ce0  c1e20c                shl       edx,0Ch
  00430ce3  03ca                  add       ecx,edx
  00430ce5  0fbf5632              movsx     edx,word [esi+32h]
  00430ce9  52                    push      edx
  00430cea  894e24                mov       [esi+24h],ecx
  00430ced  e82a4d1100            call      00545A1Ch
  00430cf2  8b4e24                mov       ecx,[esi+24h]
  00430cf5  8d0480                lea       eax,[eax+eax*4]
  00430cf8  8d0480                lea       eax,[eax+eax*4]
  00430cfb  d1e0                  shl       eax,1
  00430cfd  03c8                  add       ecx,eax
  00430cff  894e24                mov       [esi+24h],ecx
  00430d02  0fbf4e32              movsx     ecx,word [esi+32h]
  00430d06  51                    push      ecx
  00430d07  e80a4d1100            call      00545A16h
  00430d0c  8d0480                lea       eax,[eax+eax*4]
  00430d0f  8d1480                lea       edx,[eax+eax*4]
  00430d12  8b461c                mov       eax,[esi+1Ch]
  00430d15  d1e2                  shl       edx,1
  00430d17  03c2                  add       eax,edx
  00430d19  89461c                mov       [esi+1Ch],eax
  00430d1c  e81f641100            call      00547140h
  00430d21  99                    cdq
  00430d22  b906000000            mov       ecx,6
  00430d27  f7f9                  idiv      ecx
  00430d29  83ea03                sub       edx,3
  00430d2c  c1e20c                shl       edx,0Ch
  00430d2f  895650                mov       [esi+50h],edx
  00430d32  e809641100            call      00547140h
  00430d37  99                    cdq
  00430d38  b90f000000            mov       ecx,0Fh
  00430d3d  f7f9                  idiv      ecx
  00430d3f  83c210                add       edx,10h
  00430d42  c1e20c                shl       edx,0Ch
  00430d45  895654                mov       [esi+54h],edx
  00430d48  e8f3631100            call      00547140h
  00430d4d  99                    cdq
  00430d4e  b906000000            mov       ecx,6
  00430d53  f7f9                  idiv      ecx
  00430d55  83ea03                sub       edx,3
  00430d58  c1e20c                shl       edx,0Ch
  00430d5b  895658                mov       [esi+58h],edx
  00430d5e  e8dd631100            call      00547140h
  00430d63  99                    cdq
  00430d64  b9000c0000            mov       ecx,0C00h
  00430d69  f7f9                  idiv      ecx
  00430d6b  81c200100000          add       edx,1000h
  00430d71  66895628              mov       [esi+28h],dx
  00430d75  e8c6631100            call      00547140h
  00430d7a  99                    cdq
  00430d7b  b9000c0000            mov       ecx,0C00h
  00430d80  66c746100100          mov       word [esi+10h],1
  00430d86  f7f9                  idiv      ecx
  00430d88  66c746120000          mov       word [esi+12h],0
  00430d8e  c6464000              mov       byte [esi+40h],0
  00430d92  81c200100000          add       edx,1000h
  00430d98  6689562a              mov       [esi+2Ah],dx

LAB_00430d9c:
  00430d9c  66837e1001            cmp       word [esi+10h],1
  00430da1  7570                  jne       short 00430E13h
  00430da3  8b5650                mov       edx,[esi+50h]
  00430da6  8b461c                mov       eax,[esi+1Ch]
  00430da9  8b4e20                mov       ecx,[esi+20h]
  00430dac  03c2                  add       eax,edx
  00430dae  8b5624                mov       edx,[esi+24h]
  00430db1  89461c                mov       [esi+1Ch],eax
  00430db4  8b4654                mov       eax,[esi+54h]
  00430db7  03c8                  add       ecx,eax
  00430db9  0500e0ffff            add       eax,0FFFFE000h
  00430dbe  894e20                mov       [esi+20h],ecx
  00430dc1  8b4e58                mov       ecx,[esi+58h]
  00430dc4  03d1                  add       edx,ecx
  00430dc6  b1fe                  mov       cl,0FEh
  00430dc8  895624                mov       [esi+24h],edx
  00430dcb  8a5638                mov       dl,[esi+38h]
  00430dce  02d1                  add       dl,cl
  00430dd0  66ff4612              inc       word [esi+12h]
  00430dd4  885638                mov       [esi+38h],dl
  00430dd7  8a5639                mov       dl,[esi+39h]
  00430dda  02d1                  add       dl,cl
  00430ddc  894654                mov       [esi+54h],eax
  00430ddf  885639                mov       [esi+39h],dl
  00430de2  8a563a                mov       dl,[esi+3Ah]
  00430de5  02d1                  add       dl,cl
  00430de7  88563a                mov       [esi+3Ah],dl
  00430dea  e851631100            call      00547140h
  00430def  99                    cdq
  00430df0  b906000000            mov       ecx,6
  00430df5  f7f9                  idiv      ecx
  00430df7  668b4612              mov       ax,[esi+12h]
  00430dfb  663d0a00              cmp       ax,0Ah
  00430dff  7e06                  jle       short 00430E07h
  00430e01  6683fa01              cmp       dx,1
  00430e05  7406                  je        short 00430E0Dh

LAB_00430e07:
  00430e07  663d1000              cmp       ax,10h
  00430e0b  7506                  jne       short 00430E13h

LAB_00430e0d:
  00430e0d  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00430e13:
  00430e13  5e                    pop       esi
  00430e14  c3                    ret
  00430e15  90                    nop
  00430e16  90                    nop
  00430e17  90                    nop
  00430e18  90                    nop
  00430e19  90                    nop
  00430e1a  90                    nop
  00430e1b  90                    nop
  00430e1c  90                    nop
  00430e1d  90                    nop
  00430e1e  90                    nop
  00430e1f  90                    nop

4 cible(s) de saut interne : 0x00430d9c, 0x00430e07, 0x00430e0d, 0x00430e13
