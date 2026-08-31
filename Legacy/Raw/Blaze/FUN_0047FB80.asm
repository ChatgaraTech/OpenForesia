
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047fb80  section .text  offset fichier 0x7ef80  696 octets

  0047fb80  83ec14                sub       esp,14h
  0047fb83  53                    push      ebx
  0047fb84  55                    push      ebp
  0047fb85  56                    push      esi
  0047fb86  8b742424              mov       esi,[esp+24h]
  0047fb8a  33ed                  xor       ebp,ebp
  0047fb8c  57                    push      edi
  0047fb8d  66396e10              cmp       [esi+10h],bp
  0047fb91  8b4674                mov       eax,[esi+74h]
  0047fb94  0f85fd000000          jne       near 0047FC97h
  0047fb9a  8b18                  mov       ebx,[eax]
  0047fb9c  50                    push      eax
  0047fb9d  80cb80                or        bl,80h
  0047fba0  8918                  mov       [eax],ebx
  0047fba2  0fbf4e32              movsx     ecx,word [esi+32h]
  0047fba6  894c242c              mov       [esp+2Ch],ecx
  0047fbaa  e811900b00            call      00538BC0h
  0047fbaf  8b54242c              mov       edx,[esp+2Ch]
  0047fbb3  83c404                add       esp,4
  0047fbb6  8bf8                  mov       edi,eax
  0047fbb8  8d5e1c                lea       ebx,[esi+1Ch]
  0047fbbb  52                    push      edx
  0047fbbc  d1ff                  sar       edi,1
  0047fbbe  e8595e0c00            call      00545A1Ch
  0047fbc3  0fafc7                imul      eax,edi
  0047fbc6  8b0b                  mov       ecx,[ebx]
  0047fbc8  03c8                  add       ecx,eax
  0047fbca  8b442428              mov       eax,[esp+28h]
  0047fbce  50                    push      eax
  0047fbcf  890b                  mov       [ebx],ecx
  0047fbd1  e8405e0c00            call      00545A16h
  0047fbd6  0fafc7                imul      eax,edi
  0047fbd9  8b4e24                mov       ecx,[esi+24h]
  0047fbdc  8b5620                mov       edx,[esi+20h]
  0047fbdf  c1e70c                shl       edi,0Ch
  0047fbe2  03c8                  add       ecx,eax
  0047fbe4  03d7                  add       edx,edi
  0047fbe6  bf00200000            mov       edi,2000h
  0047fbeb  b800040000            mov       eax,400h
  0047fbf0  66014632              add       [esi+32h],ax
  0047fbf4  66014634              add       [esi+34h],ax
  0047fbf8  894e24                mov       [esi+24h],ecx
  0047fbfb  895620                mov       [esi+20h],edx
  0047fbfe  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  0047fc05  c7462800400020        mov       dword [esi+28h],20004000h
  0047fc0c  66897e2c              mov       [esi+2Ch],di
  0047fc10  e82b750c00            call      00547140h
  0047fc15  33c9                  xor       ecx,ecx
  0047fc17  2403                  and       al,3
  0047fc19  8a4e0b                mov       cl,[esi+0Bh]
  0047fc1c  6aff                  push      0FFFFFFFFh
  0047fc1e  51                    push      ecx
  0047fc1f  0408                  add       al,8
  0047fc21  6883000000            push      83h
  0047fc26  56                    push      esi
  0047fc27  884604                mov       [esi+4],al
  0047fc2a  e8b1aeffff            call      0047AAE0h
  0047fc2f  83c410                add       esp,10h
  0047fc32  33d2                  xor       edx,edx
  0047fc34  8a560b                mov       dl,[esi+0Bh]
  0047fc37  6681463400fc          add       word [esi+34h],0FC00h
  0047fc3d  6aff                  push      0FFFFFFFFh
  0047fc3f  52                    push      edx
  0047fc40  68be000000            push      0BEh
  0047fc45  68b01b5500            push      551BB0h
  0047fc4a  53                    push      ebx
  0047fc4b  56                    push      esi
  0047fc4c  896e70                mov       [esi+70h],ebp
  0047fc4f  896e50                mov       [esi+50h],ebp
  0047fc52  896e54                mov       [esi+54h],ebp
  0047fc55  896e58                mov       [esi+58h],ebp
  0047fc58  66c746121f00          mov       word [esi+12h],1Fh
  0047fc5e  896e44                mov       [esi+44h],ebp
  0047fc61  896e48                mov       [esi+48h],ebp
  0047fc64  896e4c                mov       [esi+4Ch],ebp
  0047fc67  896e28                mov       [esi+28h],ebp
  0047fc6a  66897e14              mov       [esi+14h],di
  0047fc6e  66c746160200          mov       word [esi+16h],2
  0047fc74  c74638f0f0f001        mov       dword [esi+38h],1F0F0F0h
  0047fc7b  66c7461a0100          mov       word [esi+1Ah],1
  0047fc81  e8baafffff            call      0047AC40h
  0047fc86  83c418                add       esp,18h
  0047fc89  53                    push      ebx
  0047fc8a  6868255500            push      552568h
  0047fc8f  e84cdf0b00            call      0053DBE0h
  0047fc94  83c408                add       esp,8

LAB_0047fc97:
  0047fc97  66837e100e            cmp       word [esi+10h],0Eh
  0047fc9c  0f8c8a010000          jl        near 0047FE2Ch
  0047fca2  33c0                  xor       eax,eax
  0047fca4  6aff                  push      0FFFFFFFFh
  0047fca6  8a460b                mov       al,[esi+0Bh]
  0047fca9  8d7e1c                lea       edi,[esi+1Ch]
  0047fcac  50                    push      eax
  0047fcad  68be000000            push      0BEh
  0047fcb2  68b01b5500            push      551BB0h
  0047fcb7  bb03000000            mov       ebx,3
  0047fcbc  57                    push      edi
  0047fcbd  56                    push      esi
  0047fcbe  c74628fc1f0000        mov       dword [esi+28h],1FFCh
  0047fcc5  66896e14              mov       [esi+14h],bp
  0047fcc9  66895e16              mov       [esi+16h],bx
  0047fccd  c74638f0f0f000        mov       dword [esi+38h],0F0F0F0h
  0047fcd4  e867afffff            call      0047AC40h
  0047fcd9  83c418                add       esp,18h
  0047fcdc  57                    push      edi
  0047fcdd  6800120000            push      1200h
  0047fce2  6853060000            push      653h
  0047fce7  6a26                  push      26h
  0047fce9  e832de0b00            call      0053DB20h
  0047fcee  0fbf4632              movsx     eax,word [esi+32h]
  0047fcf2  83c410                add       esp,10h
  0047fcf5  2d00020000            sub       eax,200h
  0047fcfa  89442428              mov       [esp+28h],eax
  0047fcfe  895c2414              mov       [esp+14h],ebx
  0047fd02  eb02                  jmp       short 0047FD06h

LAB_0047fd04:
  0047fd04  33ed                  xor       ebp,ebp

LAB_0047fd06:
  0047fd06  8b4e1c                mov       ecx,[esi+1Ch]
  0047fd09  8b5620                mov       edx,[esi+20h]
  0047fd0c  8b4624                mov       eax,[esi+24h]
  0047fd0f  894c2418              mov       [esp+18h],ecx
  0047fd13  8954241c              mov       [esp+1Ch],edx
  0047fd17  89442420              mov       [esp+20h],eax
  0047fd1b  c744241003000000      mov       dword [esp+10h],3

LAB_0047fd23:
  0047fd23  e818740c00            call      00547140h
  0047fd28  8b4c2428              mov       ecx,[esp+28h]
  0047fd2c  25c0010000            and       eax,1C0h
  0047fd31  2bc5                  sub       eax,ebp
  0047fd33  2d00010000            sub       eax,100h
  0047fd38  8be8                  mov       ebp,eax
  0047fd3a  03cd                  add       ecx,ebp
  0047fd3c  66894e32              mov       [esi+32h],cx
  0047fd40  e8fb730c00            call      00547140h
  0047fd45  8bf8                  mov       edi,eax
  0047fd47  b8d34d6210            mov       eax,10624DD3h
  0047fd4c  81e7ff000000          and       edi,0FFh
  0047fd52  81c700010000          add       edi,100h
  0047fd58  8bcf                  mov       ecx,edi
  0047fd5a  c1e10c                shl       ecx,0Ch
  0047fd5d  f7e9                  imul      ecx
  0047fd5f  c1fa04                sar       edx,4
  0047fd62  8bc2                  mov       eax,edx
  0047fd64  c1e81f                shr       eax,1Fh
  0047fd67  03d0                  add       edx,eax
  0047fd69  66895628              mov       [esi+28h],dx
  0047fd6d  e8ce730c00            call      00547140h
  0047fd72  25ff0f0000            and       eax,0FFFh
  0047fd77  0500200000            add       eax,2000h
  0047fd7c  6689462a              mov       [esi+2Ah],ax
  0047fd80  e8bb730c00            call      00547140h
  0047fd85  25ff0f0000            and       eax,0FFFh
  0047fd8a  0500280000            add       eax,2800h
  0047fd8f  6689462c              mov       [esi+2Ch],ax
  0047fd93  e8a8730c00            call      00547140h
  0047fd98  99                    cdq
  0047fd99  b903000000            mov       ecx,3
  0047fd9e  6aff                  push      0FFFFFFFFh
  0047fda0  f7f9                  idiv      ecx
  0047fda2  80c208                add       dl,8
  0047fda5  885604                mov       [esi+4],dl
  0047fda8  33d2                  xor       edx,edx
  0047fdaa  8a560b                mov       dl,[esi+0Bh]
  0047fdad  52                    push      edx
  0047fdae  6a75                  push      75h
  0047fdb0  56                    push      esi
  0047fdb1  e82aadffff            call      0047AAE0h
  0047fdb6  0fbf5e32              movsx     ebx,word [esi+32h]
  0047fdba  83c410                add       esp,10h
  0047fdbd  81eb00040000          sub       ebx,400h
  0047fdc3  53                    push      ebx
  0047fdc4  e8535c0c00            call      00545A1Ch
  0047fdc9  0fafc7                imul      eax,edi
  0047fdcc  8b4e1c                mov       ecx,[esi+1Ch]
  0047fdcf  53                    push      ebx
  0047fdd0  03c8                  add       ecx,eax
  0047fdd2  894e1c                mov       [esi+1Ch],ecx
  0047fdd5  e83c5c0c00            call      00545A16h
  0047fdda  0fafc7                imul      eax,edi
  0047fddd  8b4e24                mov       ecx,[esi+24h]
  0047fde0  03c8                  add       ecx,eax
  0047fde2  8b442410              mov       eax,[esp+10h]
  0047fde6  48                    dec       eax
  0047fde7  894e24                mov       [esi+24h],ecx
  0047fdea  89442410              mov       [esp+10h],eax
  0047fdee  0f852fffffff          jne       near 0047FD23h
  0047fdf4  8b4c241c              mov       ecx,[esp+1Ch]
  0047fdf8  8b442418              mov       eax,[esp+18h]
  0047fdfc  8b542420              mov       edx,[esp+20h]
  0047fe00  894e20                mov       [esi+20h],ecx
  0047fe03  8b4c2428              mov       ecx,[esp+28h]
  0047fe07  89461c                mov       [esi+1Ch],eax
  0047fe0a  8b442414              mov       eax,[esp+14h]
  0047fe0e  81c100020000          add       ecx,200h
  0047fe14  48                    dec       eax
  0047fe15  895624                mov       [esi+24h],edx
  0047fe18  894c2428              mov       [esp+28h],ecx
  0047fe1c  89442414              mov       [esp+14h],eax
  0047fe20  0f85defeffff          jne       near 0047FD04h
  0047fe26  c70600000000          mov       dword [esi],0

LAB_0047fe2c:
  0047fe2c  66ff4610              inc       word [esi+10h]
  0047fe30  5f                    pop       edi
  0047fe31  5e                    pop       esi
  0047fe32  5d                    pop       ebp
  0047fe33  5b                    pop       ebx
  0047fe34  83c414                add       esp,14h
  0047fe37  c3                    ret

5 cible(s) de saut interne : 0x0047fc97, 0x0047fd04, 0x0047fd06, 0x0047fd23, 0x0047fe2c
