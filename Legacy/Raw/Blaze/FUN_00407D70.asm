Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00407d70  section .text  offset fichier 0x7170  720 octets

  00407d70  83ec10                sub       esp,10h
  00407d73  53                    push      ebx
  00407d74  55                    push      ebp
  00407d75  56                    push      esi
  00407d76  8b742420              mov       esi,[esp+20h]
  00407d7a  57                    push      edi
  00407d7b  bb01000000            mov       ebx,1
  00407d80  8b4674                mov       eax,[esi+74h]
  00407d83  66837e1000            cmp       word [esi+10h],0
  00407d88  89442424              mov       [esp+24h],eax
  00407d8c  bf05000000            mov       edi,5
  00407d91  755e                  jne       short 00407DF1h
  00407d93  c7463c30f45400        mov       dword [esi+3Ch],54F430h
  00407d9a  e8a1f31300            call      00547140h
  00407d9f  99                    cdq
  00407da0  b903000000            mov       ecx,3
  00407da5  c6460b00              mov       byte [esi+0Bh],0
  00407da9  f7f9                  idiv      ecx
  00407dab  8b4e20                mov       ecx,[esi+20h]
  00407dae  b0b4                  mov       al,0B4h
  00407db0  884638                mov       [esi+38h],al
  00407db3  884639                mov       [esi+39h],al
  00407db6  88463a                mov       [esi+3Ah],al
  00407db9  b8dc050000            mov       eax,5DCh
  00407dbe  66894628              mov       [esi+28h],ax
  00407dc2  6689462a              mov       [esi+2Ah],ax
  00407dc6  8b461c                mov       eax,[esi+1Ch]
  00407dc9  894e54                mov       [esi+54h],ecx
  00407dcc  894650                mov       [esi+50h],eax
  00407dcf  66895e10              mov       [esi+10h],bx
  00407dd3  66c746120000          mov       word [esi+12h],0
  00407dd9  66897e14              mov       [esi+14h],di
  00407ddd  fec2                  inc       dl
  00407ddf  885640                mov       [esi+40h],dl
  00407de2  8a969a000000          mov       dl,[esi+9Ah]
  00407de8  88560a                mov       [esi+0Ah],dl
  00407deb  8b5624                mov       edx,[esi+24h]
  00407dee  895658                mov       [esi+58h],edx

LAB_00407df1:
  00407df1  66395e10              cmp       [esi+10h],bx
  00407df5  0f85c9010000          jne       near 00407FC4h
  00407dfb  0fbf4632              movsx     eax,word [esi+32h]
  00407dff  50                    push      eax
  00407e00  e817dc1300            call      00545A1Ch
  00407e05  0fbf4e12              movsx     ecx,word [esi+12h]
  00407e09  0fafc1                imul      eax,ecx
  00407e0c  0fbf5632              movsx     edx,word [esi+32h]
  00407e10  8b4e50                mov       ecx,[esi+50h]
  00407e13  52                    push      edx
  00407e14  03c1                  add       eax,ecx
  00407e16  89461c                mov       [esi+1Ch],eax
  00407e19  e8f8db1300            call      00545A16h
  00407e1e  0fbf4e12              movsx     ecx,word [esi+12h]
  00407e22  0fafc1                imul      eax,ecx
  00407e25  8b4e58                mov       ecx,[esi+58h]
  00407e28  6af9                  push      0FFFFFFF9h
  00407e2a  03c1                  add       eax,ecx
  00407e2c  6af9                  push      0FFFFFFF9h
  00407e2e  894624                mov       [esi+24h],eax
  00407e31  8b4620                mov       eax,[esi+20h]
  00407e34  050040ffff            add       eax,0FFFF4000h
  00407e39  6af9                  push      0FFFFFFF9h
  00407e3b  894620                mov       [esi+20h],eax
  00407e3e  b864000000            mov       eax,64h
  00407e43  66014628              add       [esi+28h],ax
  00407e47  6601462a              add       [esi+2Ah],ax
  00407e4b  56                    push      esi
  00407e4c  e88fcfffff            call      00404DE0h
  00407e51  8a4638                mov       al,[esi+38h]
  00407e54  83c410                add       esp,10h
  00407e57  84c0                  test      al,al
  00407e59  7506                  jne       short 00407E61h
  00407e5b  66c746100200          mov       word [esi+10h],2

LAB_00407e61:
  00407e61  56                    push      esi
  00407e62  e8d9ba1200            call      00533940h
  00407e67  8b8690000000          mov       eax,[esi+90h]
  00407e6d  83c404                add       esp,4
  00407e70  a902880000            test      eax,8802h
  00407e75  7406                  je        short 00407E7Dh
  00407e77  66c746100200          mov       word [esi+10h],2

LAB_00407e7d:
  00407e7d  8b5660                mov       edx,[esi+60h]
  00407e80  8b4620                mov       eax,[esi+20h]
  00407e83  3bd0                  cmp       edx,eax
  00407e85  7c06                  jl        short 00407E8Dh
  00407e87  66c746100200          mov       word [esi+10h],2

LAB_00407e8d:
  00407e8d  66ff4e14              dec       word [esi+14h]
  00407e91  668b4614              mov       ax,[esi+14h]
  00407e95  6683461214            add       word [esi+12h],14h
  00407e9a  6685c0                test      ax,ax
  00407e9d  7f15                  jg        short 00407EB4h
  00407e9f  8a5640                mov       dl,[esi+40h]
  00407ea2  66897e14              mov       [esi+14h],di
  00407ea6  fec2                  inc       dl
  00407ea8  8ac2                  mov       al,dl
  00407eaa  885640                mov       [esi+40h],dl
  00407ead  3c04                  cmp       al,4
  00407eaf  7503                  jne       short 00407EB4h
  00407eb1  885e40                mov       [esi+40h],bl

LAB_00407eb4:
  00407eb4  bfc0935e00            mov       edi,5E93C0h
  00407eb9  c744241404000000      mov       dword [esp+14h],4
  00407ec1  bb10000000            mov       ebx,10h

LAB_00407ec6:
  00407ec6  8b07                  mov       eax,[edi]
  00407ec8  a900000080            test      eax,80000000h
  00407ecd  0f84dc000000          je        near 00407FAFh
  00407ed3  a900000050            test      eax,50000000h
  00407ed8  0f85d1000000          jne       near 00407FAFh
  00407ede  66837f1200            cmp       word [edi+12h],0
  00407ee3  0f85c6000000          jne       near 00407FAFh
  00407ee9  8b4f1c                mov       ecx,[edi+1Ch]
  00407eec  8b461c                mov       eax,[esi+1Ch]
  00407eef  8b5620                mov       edx,[esi+20h]
  00407ef2  8b6e24                mov       ebp,[esi+24h]
  00407ef5  2bc8                  sub       ecx,eax
  00407ef7  8b4720                mov       eax,[edi+20h]
  00407efa  2bc2                  sub       eax,edx
  00407efc  8b5724                mov       edx,[edi+24h]
  00407eff  c1f90c                sar       ecx,0Ch
  00407f02  0500c00300            add       eax,3C000h
  00407f07  2bd5                  sub       edx,ebp
  00407f09  8be9                  mov       ebp,ecx
  00407f0b  c1f80c                sar       eax,0Ch
  00407f0e  0fafe9                imul      ebp,ecx
  00407f11  8bc8                  mov       ecx,eax
  00407f13  c1fa0c                sar       edx,0Ch
  00407f16  0fafc8                imul      ecx,eax
  00407f19  8bc2                  mov       eax,edx
  00407f1b  03e9                  add       ebp,ecx
  00407f1d  0fafc2                imul      eax,edx
  00407f20  03e8                  add       ebp,eax
  00407f22  55                    push      ebp
  00407f23  e8e8da1300            call      00545A10h
  00407f28  6683bf9600000000      cmp       word [edi+96h],0
  00407f30  757d                  jne       short 00407FAFh
  00407f32  83f850                cmp       eax,50h
  00407f35  7d78                  jge       short 00407FAFh
  00407f37  8b4e24                mov       ecx,[esi+24h]
  00407f3a  8b5620                mov       edx,[esi+20h]
  00407f3d  8b461c                mov       eax,[esi+1Ch]
  00407f40  51                    push      ecx
  00407f41  52                    push      edx
  00407f42  50                    push      eax
  00407f43  57                    push      edi
  00407f44  e857f60200            call      004375A0h
  00407f49  83c410                add       esp,10h
  00407f4c  6685c0                test      ax,ax
  00407f4f  7558                  jne       short 00407FA9h
  00407f51  8d4c2410              lea       ecx,[esp+10h]
  00407f55  8d542418              lea       edx,[esp+18h]
  00407f59  51                    push      ecx
  00407f5a  8b4c2428              mov       ecx,[esp+28h]
  00407f5e  8d442420              lea       eax,[esp+20h]
  00407f62  52                    push      edx
  00407f63  50                    push      eax
  00407f64  51                    push      ecx
  00407f65  e8160a1300            call      00538980h
  00407f6a  8b442420              mov       eax,[esp+20h]
  00407f6e  83c410                add       esp,10h
  00407f71  668b6830              mov       bp,[eax+30h]
  00407f75  57                    push      edi
  00407f76  668bd5                mov       dx,bp
  00407f79  8bcd                  mov       ecx,ebp
  00407f7b  66c1fa02              sar       dx,2
  00407f7f  2bca                  sub       ecx,edx
  00407f81  8b542428              mov       edx,[esp+28h]
  00407f85  52                    push      edx
  00407f86  66894830              mov       [eax+30h],cx
  00407f8a  e8f11b1300            call      00539B80h
  00407f8f  8b442418              mov       eax,[esp+18h]
  00407f93  83c408                add       esp,8
  00407f96  66896830              mov       [eax+30h],bp
  00407f9a  66899f96000000        mov       [edi+96h],bx
  00407fa1  8b07                  mov       eax,[edi]
  00407fa3  0c40                  or        al,40h
  00407fa5  8907                  mov       [edi],eax
  00407fa7  eb06                  jmp       short 00407FAFh

LAB_00407fa9:
  00407fa9  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00407faf:
  00407faf  8b442414              mov       eax,[esp+14h]
  00407fb3  81c79c000000          add       edi,9Ch
  00407fb9  48                    dec       eax
  00407fba  89442414              mov       [esp+14h],eax
  00407fbe  0f8502ffffff          jne       near 00407EC6h

LAB_00407fc4:
  00407fc4  66837e1002            cmp       word [esi+10h],2
  00407fc9  7506                  jne       short 00407FD1h
  00407fcb  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00407fd1:
  00407fd1  5f                    pop       edi
  00407fd2  5e                    pop       esi
  00407fd3  5d                    pop       ebp
  00407fd4  5b                    pop       ebx
  00407fd5  83c410                add       esp,10h
  00407fd8  c3                    ret
  00407fd9  90                    nop
  00407fda  90                    nop
  00407fdb  90                    nop
  00407fdc  90                    nop
  00407fdd  90                    nop
  00407fde  90                    nop
  00407fdf  90                    nop
  00407fe0  8b442404              mov       eax,[esp+4]
  00407fe4  50                    push      eax
  00407fe5  c7405c39000000        mov       dword [eax+5Ch],39h
  00407fec  8b0da4a95f00          mov       ecx,[5FA9A4h]
  00407ff2  89481c                mov       [eax+1Ch],ecx
  00407ff5  8b15a8a95f00          mov       edx,[5FA9A8h]
  00407ffb  895020                mov       [eax+20h],edx
  00407ffe  8b0daca95f00          mov       ecx,[5FA9ACh]
  00408004  894824                mov       [eax+24h],ecx
  00408007  668b15b8a95f00        mov       dx,[5FA9B8h]
  0040800e  66895030              mov       [eax+30h],dx
  00408012  668b0dbaa95f00        mov       cx,[5FA9BAh]
  00408019  66894832              mov       [eax+32h],cx
  0040801d  668b15bca95f00        mov       dx,[5FA9BCh]
  00408024  66895034              mov       [eax+34h],dx
  00408028  e8a3030100            call      004183D0h
  0040802d  83c404                add       esp,4
  00408030  c3                    ret
  00408031  90                    nop
  00408032  90                    nop
  00408033  90                    nop
  00408034  90                    nop
  00408035  90                    nop
  00408036  90                    nop
  00408037  90                    nop
  00408038  90                    nop
  00408039  90                    nop
  0040803a  90                    nop
  0040803b  90                    nop
  0040803c  90                    nop
  0040803d  90                    nop
  0040803e  90                    nop
  0040803f  90                    nop

10 cible(s) de saut interne : 0x00407df1, 0x00407e61, 0x00407e7d, 0x00407e8d, 0x00407eb4, 0x00407ec6, 0x00407fa9, 0x00407faf, 0x00407fc4, 0x00407fd1
