Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004484f0  section .text  offset fichier 0x478f0  704 octets

  004484f0  53                    push      ebx
  004484f1  55                    push      ebp
  004484f2  56                    push      esi
  004484f3  8b742410              mov       esi,[esp+10h]
  004484f7  33db                  xor       ebx,ebx
  004484f9  57                    push      edi
  004484fa  53                    push      ebx
  004484fb  53                    push      ebx
  004484fc  56                    push      esi
  004484fd  e83efbffff            call      00448040h
  00448502  83c40c                add       esp,0Ch
  00448505  66395e10              cmp       [esi+10h],bx
  00448509  750b                  jne       short 00448516h
  0044850b  53                    push      ebx
  0044850c  53                    push      ebx
  0044850d  56                    push      esi
  0044850e  e89df7ffff            call      00447CB0h
  00448513  83c40c                add       esp,0Ch

LAB_00448516:
  00448516  8b7e78                mov       edi,[esi+78h]
  00448519  3bfb                  cmp       edi,ebx
  0044851b  7505                  jne       short 00448522h
  0044851d  bf58e55d00            mov       edi,5DE558h

LAB_00448522:
  00448522  8b471c                mov       eax,[edi+1Ch]
  00448525  66395e10              cmp       [esi+10h],bx
  00448529  89461c                mov       [esi+1Ch],eax
  0044852c  8b4f20                mov       ecx,[edi+20h]
  0044852f  894e20                mov       [esi+20h],ecx
  00448532  8b5724                mov       edx,[edi+24h]
  00448535  895624                mov       [esi+24h],edx
  00448538  0f85d4010000          jne       near 00448712h
  0044853e  66c746100100          mov       word [esi+10h],1
  00448544  66895e16              mov       [esi+16h],bx
  00448548  668b4732              mov       ax,[edi+32h]
  0044854c  c7465c0e000000        mov       dword [esi+5Ch],0Eh
  00448553  66894614              mov       [esi+14h],ax
  00448557  e8e4eb0f00            call      00547140h
  0044855c  99                    cdq
  0044855d  33c2                  xor       eax,edx
  0044855f  6aff                  push      0FFFFFFFFh
  00448561  2bc2                  sub       eax,edx
  00448563  53                    push      ebx
  00448564  25ff0f0000            and       eax,0FFFh
  00448569  bd18000000            mov       ebp,18h
  0044856e  33c2                  xor       eax,edx
  00448570  68a2000000            push      0A2h
  00448575  2bc2                  sub       eax,edx
  00448577  56                    push      esi
  00448578  66894616              mov       [esi+16h],ax
  0044857c  895e48                mov       [esi+48h],ebx
  0044857f  6689ae9a000000        mov       [esi+9Ah],bp
  00448586  e855250300            call      0047AAE0h
  0044858b  83c410                add       esp,10h
  0044858e  bb19000000            mov       ebx,19h
  00448593  66899e9a000000        mov       [esi+9Ah],bx
  0044859a  6aff                  push      0FFFFFFFFh
  0044859c  6a00                  push      0
  0044859e  68a2000000            push      0A2h
  004485a3  56                    push      esi
  004485a4  e837250300            call      0047AAE0h
  004485a9  668b4f32              mov       cx,[edi+32h]
  004485ad  83c410                add       esp,10h
  004485b0  6681c10004            add       cx,400h
  004485b5  66894e14              mov       [esi+14h],cx
  004485b9  e882eb0f00            call      00547140h
  004485be  99                    cdq
  004485bf  33c2                  xor       eax,edx
  004485c1  6aff                  push      0FFFFFFFFh
  004485c3  2bc2                  sub       eax,edx
  004485c5  6a00                  push      0
  004485c7  25ff0f0000            and       eax,0FFFh
  004485cc  68a2000000            push      0A2h
  004485d1  33c2                  xor       eax,edx
  004485d3  56                    push      esi
  004485d4  2bc2                  sub       eax,edx
  004485d6  c7464800040000        mov       dword [esi+48h],400h
  004485dd  66894616              mov       [esi+16h],ax
  004485e1  6689ae9a000000        mov       [esi+9Ah],bp
  004485e8  e8f3240300            call      0047AAE0h
  004485ed  83c410                add       esp,10h
  004485f0  66899e9a000000        mov       [esi+9Ah],bx
  004485f7  6aff                  push      0FFFFFFFFh
  004485f9  6a00                  push      0
  004485fb  68a2000000            push      0A2h
  00448600  56                    push      esi
  00448601  e8da240300            call      0047AAE0h
  00448606  668b5732              mov       dx,[edi+32h]
  0044860a  83c410                add       esp,10h
  0044860d  6681c20008            add       dx,800h
  00448612  66895614              mov       [esi+14h],dx
  00448616  e825eb0f00            call      00547140h
  0044861b  99                    cdq
  0044861c  33c2                  xor       eax,edx
  0044861e  6aff                  push      0FFFFFFFFh
  00448620  2bc2                  sub       eax,edx
  00448622  6a00                  push      0
  00448624  25ff0f0000            and       eax,0FFFh
  00448629  68a2000000            push      0A2h
  0044862e  33c2                  xor       eax,edx
  00448630  56                    push      esi
  00448631  2bc2                  sub       eax,edx
  00448633  c7464800080000        mov       dword [esi+48h],800h
  0044863a  66894616              mov       [esi+16h],ax
  0044863e  6689ae9a000000        mov       [esi+9Ah],bp
  00448645  e896240300            call      0047AAE0h
  0044864a  83c410                add       esp,10h
  0044864d  66899e9a000000        mov       [esi+9Ah],bx
  00448654  6aff                  push      0FFFFFFFFh
  00448656  6a00                  push      0
  00448658  68a2000000            push      0A2h
  0044865d  56                    push      esi
  0044865e  e87d240300            call      0047AAE0h
  00448663  668b4732              mov       ax,[edi+32h]
  00448667  83c410                add       esp,10h
  0044866a  6605000c              add       ax,0C00h
  0044866e  66894614              mov       [esi+14h],ax
  00448672  e8c9ea0f00            call      00547140h
  00448677  99                    cdq
  00448678  33c2                  xor       eax,edx
  0044867a  6aff                  push      0FFFFFFFFh
  0044867c  2bc2                  sub       eax,edx
  0044867e  6a00                  push      0
  00448680  25ff0f0000            and       eax,0FFFh
  00448685  68a2000000            push      0A2h
  0044868a  33c2                  xor       eax,edx
  0044868c  56                    push      esi
  0044868d  2bc2                  sub       eax,edx
  0044868f  c74648000c0000        mov       dword [esi+48h],0C00h
  00448696  66894616              mov       [esi+16h],ax
  0044869a  6689ae9a000000        mov       [esi+9Ah],bp
  004486a1  e83a240300            call      0047AAE0h
  004486a6  83c410                add       esp,10h
  004486a9  66899e9a000000        mov       [esi+9Ah],bx
  004486b0  6aff                  push      0FFFFFFFFh
  004486b2  6a00                  push      0
  004486b4  68a2000000            push      0A2h
  004486b9  56                    push      esi
  004486ba  e821240300            call      0047AAE0h
  004486bf  83c410                add       esp,10h
  004486c2  66c746160000          mov       word [esi+16h],0
  004486c8  c7465c09000000        mov       dword [esi+5Ch],9
  004486cf  6aff                  push      0FFFFFFFFh
  004486d1  6a00                  push      0
  004486d3  68a3000000            push      0A3h
  004486d8  56                    push      esi
  004486d9  e802240300            call      0047AAE0h
  004486de  8b4674                mov       eax,[esi+74h]
  004486e1  83c410                add       esp,10h
  004486e4  bfa0000000            mov       edi,0A0h
  004486e9  8b08                  mov       ecx,[eax]
  004486eb  80c980                or        cl,80h
  004486ee  8908                  mov       [eax],ecx
  004486f0  66893d60915e00        mov       [5E9160h],di
  004486f7  66893d62915e00        mov       [5E9162h],di
  004486fe  66c70564915e00dc00    mov       word [5E9164h],0DCh
  00448707  66c70566915e000200    mov       word [5E9166h],2
  00448710  eb05                  jmp       short 00448717h

LAB_00448712:
  00448712  bfa0000000            mov       edi,0A0h

LAB_00448717:
  00448717  668b4616              mov       ax,[esi+16h]
  0044871b  663d5000              cmp       ax,50h
  0044871f  7c31                  jl        short 00448752h
  00448721  663d8c00              cmp       ax,8Ch
  00448725  66c746145000          mov       word [esi+14h],50h
  0044872b  66c7869a0000000900    mov       word [esi+9Ah],9
  00448734  7d1c                  jge       short 00448752h
  00448736  a801                  test      al,1
  00448738  7418                  je        short 00448752h
  0044873a  6aff                  push      0FFFFFFFFh
  0044873c  6a00                  push      0
  0044873e  68a9000000            push      0A9h
  00448743  8d461c                lea       eax,[esi+1Ch]
  00448746  6a00                  push      0
  00448748  50                    push      eax
  00448749  56                    push      esi
  0044874a  e8f1240300            call      0047AC40h
  0044874f  83c418                add       esp,18h

LAB_00448752:
  00448752  66ff4616              inc       word [esi+16h]
  00448756  668b4616              mov       ax,[esi+16h]
  0044875a  663d7800              cmp       ax,78h
  0044875e  7c0a                  jl        short 0044876Ah
  00448760  8b4674                mov       eax,[esi+74h]
  00448763  8b08                  mov       ecx,[eax]
  00448765  80e17f                and       cl,7Fh
  00448768  8908                  mov       [eax],ecx

LAB_0044876a:
  0044876a  66397e16              cmp       [esi+16h],di
  0044876e  7c3a                  jl        short 004487AAh
  00448770  8b4674                mov       eax,[esi+74h]
  00448773  6a00                  push      0
  00448775  6a00                  push      0
  00448777  56                    push      esi
  00448778  81207fffffff          and       dword [eax],0FFFFFF7Fh
  0044877e  e89df8ffff            call      00448020h
  00448783  8b4e74                mov       ecx,[esi+74h]
  00448786  8a5606                mov       dl,[esi+6]
  00448789  83c40c                add       esp,0Ch
  0044878c  885106                mov       [ecx+6],dl
  0044878f  8b4674                mov       eax,[esi+74h]
  00448792  8a4e07                mov       cl,[esi+7]
  00448795  6a00                  push      0
  00448797  884807                mov       [eax+7],cl
  0044879a  8b5678                mov       edx,[esi+78h]
  0044879d  8b4674                mov       eax,[esi+74h]
  004487a0  52                    push      edx
  004487a1  50                    push      eax
  004487a2  e809000000            call      004487B0h
  004487a7  83c40c                add       esp,0Ch

LAB_004487aa:
  004487aa  5f                    pop       edi
  004487ab  5e                    pop       esi
  004487ac  5d                    pop       ebp
  004487ad  5b                    pop       ebx
  004487ae  c3                    ret
  004487af  90                    nop

7 cible(s) de saut interne : 0x00448516, 0x00448522, 0x00448712, 0x00448717, 0x00448752, 0x0044876a, 0x004487aa
