Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004491f0  section .text  offset fichier 0x485f0  822 octets

  004491f0  83ec08                sub       esp,8
  004491f3  53                    push      ebx
  004491f4  55                    push      ebp
  004491f5  56                    push      esi
  004491f6  57                    push      edi
  004491f7  e874030000            call      00449570h
  004491fc  8b74241c              mov       esi,[esp+1Ch]
  00449200  33db                  xor       ebx,ebx
  00449202  53                    push      ebx
  00449203  53                    push      ebx
  00449204  56                    push      esi
  00449205  89442420              mov       [esp+20h],eax
  00449209  e832eeffff            call      00448040h
  0044920e  83c40c                add       esp,0Ch
  00449211  66395e10              cmp       [esi+10h],bx
  00449215  750b                  jne       short 00449222h
  00449217  53                    push      ebx
  00449218  53                    push      ebx
  00449219  56                    push      esi
  0044921a  e891eaffff            call      00447CB0h
  0044921f  83c40c                add       esp,0Ch

LAB_00449222:
  00449222  8b4678                mov       eax,[esi+78h]
  00449225  3bc3                  cmp       eax,ebx
  00449227  7505                  jne       short 0044922Eh
  00449229  b858e55d00            mov       eax,5DE558h

LAB_0044922e:
  0044922e  8b481c                mov       ecx,[eax+1Ch]
  00449231  8d6e1c                lea       ebp,[esi+1Ch]
  00449234  66395e10              cmp       [esi+10h],bx
  00449238  894d00                mov       [ebp],ecx
  0044923b  8b5020                mov       edx,[eax+20h]
  0044923e  895620                mov       [esi+20h],edx
  00449241  8b4024                mov       eax,[eax+24h]
  00449244  894624                mov       [esi+24h],eax
  00449247  0f85fe000000          jne       near 0044934Bh
  0044924d  b801000000            mov       eax,1
  00449252  66895e12              mov       [esi+12h],bx
  00449256  66894610              mov       [esi+10h],ax
  0044925a  66c746145000          mov       word [esi+14h],50h
  00449260  66895e16              mov       [esi+16h],bx
  00449264  6689869a000000        mov       [esi+9Ah],ax
  0044926b  66899e98000000        mov       [esi+98h],bx
  00449272  bfc0935e00            mov       edi,5E93C0h
  00449277  895c241c              mov       [esp+1Ch],ebx
  0044927b  c744241004000000      mov       dword [esp+10h],4

LAB_00449283:
  00449283  8b07                  mov       eax,[edi]
  00449285  a900000080            test      eax,80000000h
  0044928a  747e                  je        short 0044930Ah
  0044928c  a900000050            test      eax,50000000h
  00449291  7577                  jne       short 0044930Ah
  00449293  8b4e74                mov       ecx,[esi+74h]
  00449296  8a5606                mov       dl,[esi+6]
  00449299  897e78                mov       [esi+78h],edi
  0044929c  53                    push      ebx
  0044929d  885106                mov       [ecx+6],dl
  004492a0  8b4674                mov       eax,[esi+74h]
  004492a3  8a4e07                mov       cl,[esi+7]
  004492a6  884807                mov       [eax+7],cl
  004492a9  8b5678                mov       edx,[esi+78h]
  004492ac  8b4674                mov       eax,[esi+74h]
  004492af  52                    push      edx
  004492b0  50                    push      eax
  004492b1  e87a020000            call      00449530h
  004492b6  83c40c                add       esp,0Ch
  004492b9  663bc3                cmp       ax,bx
  004492bc  753a                  jne       short 004492F8h
  004492be  8b4f1c                mov       ecx,[edi+1Ch]
  004492c1  6aff                  push      0FFFFFFFFh
  004492c3  894d00                mov       [ebp],ecx
  004492c6  8b5720                mov       edx,[edi+20h]
  004492c9  53                    push      ebx
  004492ca  68a9000000            push      0A9h
  004492cf  895620                mov       [esi+20h],edx
  004492d2  8b4724                mov       eax,[edi+24h]
  004492d5  53                    push      ebx
  004492d6  55                    push      ebp
  004492d7  56                    push      esi
  004492d8  894624                mov       [esi+24h],eax
  004492db  e860190300            call      0047AC40h
  004492e0  83c418                add       esp,18h
  004492e3  6aff                  push      0FFFFFFFFh
  004492e5  53                    push      ebx
  004492e6  68a9000000            push      0A9h
  004492eb  53                    push      ebx
  004492ec  55                    push      ebp
  004492ed  56                    push      esi
  004492ee  e84d190300            call      0047AC40h
  004492f3  83c418                add       esp,18h
  004492f6  eb12                  jmp       short 0044930Ah

LAB_004492f8:
  004492f8  8b4c241c              mov       ecx,[esp+1Ch]
  004492fc  ba01000000            mov       edx,1
  00449301  d3e2                  shl       edx,cl
  00449303  66099698000000        or        [esi+98h],dx

LAB_0044930a:
  0044930a  8b4c241c              mov       ecx,[esp+1Ch]
  0044930e  8b442410              mov       eax,[esp+10h]
  00449312  81c79c000000          add       edi,9Ch
  00449318  41                    inc       ecx
  00449319  48                    dec       eax
  0044931a  894c241c              mov       [esp+1Ch],ecx
  0044931e  89442410              mov       [esp+10h],eax
  00449322  0f855bffffff          jne       near 00449283h
  00449328  b882000000            mov       eax,82h
  0044932d  66c70564915e00c800    mov       word [5E9164h],0C8h
  00449336  66a360915e00          mov       [5E9160h],ax
  0044933c  66a362915e00          mov       [5E9162h],ax
  00449342  66c70566915e000200    mov       word [5E9166h],2

LAB_0044934b:
  0044934b  66395e16              cmp       [esi+16h],bx
  0044934f  7519                  jne       short 0044936Ah
  00449351  6800100000            push      1000h
  00449356  6800100000            push      1000h
  0044935b  6800020000            push      200h
  00449360  6a30                  push      30h
  00449362  e8393b0f00            call      0053CEA0h
  00449367  83c410                add       esp,10h

LAB_0044936a:
  0044936a  66837e1602            cmp       word [esi+16h],2
  0044936f  7519                  jne       short 0044938Ah
  00449371  6800150000            push      1500h
  00449376  6800150000            push      1500h
  0044937b  6853060000            push      653h
  00449380  6a2f                  push      2Fh
  00449382  e8193b0f00            call      0053CEA0h
  00449387  83c410                add       esp,10h

LAB_0044938a:
  0044938a  66837e160c            cmp       word [esi+16h],0Ch
  0044938f  7519                  jne       short 004493AAh
  00449391  6800150000            push      1500h
  00449396  6800150000            push      1500h
  0044939b  6800080000            push      800h
  004493a0  6a2f                  push      2Fh
  004493a2  e8f93a0f00            call      0053CEA0h
  004493a7  83c410                add       esp,10h

LAB_004493aa:
  004493aa  66837e1616            cmp       word [esi+16h],16h
  004493af  7519                  jne       short 004493CAh
  004493b1  6800150000            push      1500h
  004493b6  6800150000            push      1500h
  004493bb  68fc0b0000            push      0BFCh
  004493c0  6a2f                  push      2Fh
  004493c2  e8d93a0f00            call      0053CEA0h
  004493c7  83c410                add       esp,10h

LAB_004493ca:
  004493ca  f6461601              test      byte [esi+16h],1
  004493ce  0f8434010000          je        near 00449508h
  004493d4  33c9                  xor       ecx,ecx
  004493d6  bfc0935e00            mov       edi,5E93C0h
  004493db  894c241c              mov       [esp+1Ch],ecx
  004493df  c744241004000000      mov       dword [esp+10h],4

LAB_004493e7:
  004493e7  8b07                  mov       eax,[edi]
  004493e9  a900000080            test      eax,80000000h
  004493ee  0f84f6000000          je        near 004494EAh
  004493f4  a900000050            test      eax,50000000h
  004493f9  0f85eb000000          jne       near 004494EAh
  004493ff  0fbf8698000000        movsx     eax,word [esi+98h]
  00449406  ba01000000            mov       edx,1
  0044940b  d3e2                  shl       edx,cl
  0044940d  85c2                  test      edx,eax
  0044940f  0f85d5000000          jne       near 004494EAh
  00449415  66c746145000          mov       word [esi+14h],50h
  0044941b  897e78                mov       [esi+78h],edi
  0044941e  8b471c                mov       eax,[edi+1Ch]
  00449421  66837c24143c          cmp       word [esp+14h],3Ch
  00449427  894500                mov       [ebp],eax
  0044942a  8b4f20                mov       ecx,[edi+20h]
  0044942d  894e20                mov       [esi+20h],ecx
  00449430  8b5724                mov       edx,[edi+24h]
  00449433  895624                mov       [esi+24h],edx
  00449436  7d13                  jge       short 0044944Bh
  00449438  6aff                  push      0FFFFFFFFh
  0044943a  53                    push      ebx
  0044943b  68a9000000            push      0A9h
  00449440  53                    push      ebx
  00449441  55                    push      ebp
  00449442  56                    push      esi
  00449443  e8f8170300            call      0047AC40h
  00449448  83c418                add       esp,18h

LAB_0044944b:
  0044944b  e8f0dc0f00            call      00547140h
  00449450  99                    cdq
  00449451  b907000000            mov       ecx,7
  00449456  f7f9                  idiv      ecx
  00449458  6683fa01              cmp       dx,1
  0044945c  7513                  jne       short 00449471h
  0044945e  6aff                  push      0FFFFFFFFh
  00449460  53                    push      ebx
  00449461  68a9000000            push      0A9h
  00449466  53                    push      ebx
  00449467  55                    push      ebp
  00449468  56                    push      esi
  00449469  e8d2170300            call      0047AC40h
  0044946e  83c418                add       esp,18h

LAB_00449471:
  00449471  8b5678                mov       edx,[esi+78h]
  00449474  c646043f              mov       byte [esi+4],3Fh
  00449478  c6460a09              mov       byte [esi+0Ah],9
  0044947c  885e0b                mov       [esi+0Bh],bl
  0044947f  895670                mov       [esi+70h],edx
  00449482  895e44                mov       [esi+44h],ebx
  00449485  c7464800000500        mov       dword [esi+48h],50000h
  0044948c  895e4c                mov       [esi+4Ch],ebx
  0044948f  e8acdc0f00            call      00547140h
  00449494  99                    cdq
  00449495  33c2                  xor       eax,edx
  00449497  2bc2                  sub       eax,edx
  00449499  25ff030000            and       eax,3FFh
  0044949e  33c2                  xor       eax,edx
  004494a0  2bc2                  sub       eax,edx
  004494a2  0500040000            add       eax,400h
  004494a7  66894632              mov       [esi+32h],ax
  004494ab  e890dc0f00            call      00547140h
  004494b0  99                    cdq
  004494b1  33c2                  xor       eax,edx
  004494b3  6aff                  push      0FFFFFFFFh
  004494b5  2bc2                  sub       eax,edx
  004494b7  53                    push      ebx
  004494b8  25ff0f0000            and       eax,0FFFh
  004494bd  6a70                  push      70h
  004494bf  33c2                  xor       eax,edx
  004494c1  56                    push      esi
  004494c2  2bc2                  sub       eax,edx
  004494c4  66895e14              mov       [esi+14h],bx
  004494c8  66894634              mov       [esi+34h],ax
  004494cc  66c74612c800          mov       word [esi+12h],0C8h
  004494d2  66c74628ff1f          mov       word [esi+28h],1FFFh
  004494d8  66895e2a              mov       [esi+2Ah],bx
  004494dc  66c7462c0001          mov       word [esi+2Ch],100h
  004494e2  e8f9150300            call      0047AAE0h
  004494e7  83c410                add       esp,10h

LAB_004494ea:
  004494ea  8b4c241c              mov       ecx,[esp+1Ch]
  004494ee  8b442410              mov       eax,[esp+10h]
  004494f2  81c79c000000          add       edi,9Ch
  004494f8  41                    inc       ecx
  004494f9  48                    dec       eax
  004494fa  894c241c              mov       [esp+1Ch],ecx
  004494fe  89442410              mov       [esp+10h],eax
  00449502  0f85dffeffff          jne       near 004493E7h

LAB_00449508:
  00449508  66ff4616              inc       word [esi+16h]
  0044950c  66837e1628            cmp       word [esi+16h],28h
  00449511  7e0b                  jle       short 0044951Eh
  00449513  53                    push      ebx
  00449514  53                    push      ebx
  00449515  56                    push      esi
  00449516  e805ebffff            call      00448020h
  0044951b  83c40c                add       esp,0Ch

LAB_0044951e:
  0044951e  5f                    pop       edi
  0044951f  5e                    pop       esi
  00449520  5d                    pop       ebp
  00449521  5b                    pop       ebx
  00449522  83c408                add       esp,8
  00449525  c3                    ret

16 cible(s) de saut interne : 0x00449222, 0x0044922e, 0x00449283, 0x004492f8, 0x0044930a, 0x0044934b, 0x0044936a, 0x0044938a, 0x004493aa, 0x004493ca, 0x004493e7, 0x0044944b, 0x00449471, 0x004494ea, 0x00449508, 0x0044951e
