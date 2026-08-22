Legacy/Binaries/Blaze.exe  base 0x00400000
0x0053df90  section .text  offset fichier 0x13d390  272 octets

  0053df90  a1384a5600            mov       eax,[564A38h]
  0053df95  8b0d60e15d00          mov       ecx,[5DE160h]
  0053df9b  83ec14                sub       esp,14h
  0053df9e  c7055ce15d0001000000  mov       dword [5DE15Ch],1
  0053dfa8  53                    push      ebx
  0053dfa9  33db                  xor       ebx,ebx
  0053dfab  3bc3                  cmp       eax,ebx
  0053dfad  56                    push      esi
  0053dfae  7513                  jne       short 0053DFC3h
  0053dfb0  83f908                cmp       ecx,8
  0053dfb3  7c0e                  jl        short 0053DFC3h
  0053dfb5  891d58e15d00          mov       [5DE158h],ebx
  0053dfbb  891d54e15d00          mov       [5DE154h],ebx
  0053dfc1  eb1c                  jmp       short 0053DFDFh

LAB_0053dfc3:
  0053dfc3  b80b000000            mov       eax,0Bh
  0053dfc8  c70554e15d00a0000000  mov       dword [5DE154h],0A0h
  0053dfd2  2bc1                  sub       eax,ecx
  0053dfd4  8d0440                lea       eax,[eax+eax*2]
  0053dfd7  c1e002                shl       eax,2
  0053dfda  a358e15d00            mov       [5DE158h],eax

LAB_0053dfdf:
  0053dfdf  8b4c242c              mov       ecx,[esp+2Ch]
  0053dfe3  8b542428              mov       edx,[esp+28h]
  0053dfe7  8b442424              mov       eax,[esp+24h]
  0053dfeb  8b742420              mov       esi,[esp+20h]
  0053dfef  51                    push      ecx
  0053dff0  52                    push      edx
  0053dff1  50                    push      eax
  0053dff2  56                    push      esi
  0053dff3  e8a8000000            call      0053E0A0h
  0053dff8  83c410                add       esp,10h
  0053dffb  b980000000            mov       ecx,80h
  0053e000  66890dfc8f5e00        mov       [5E8FFCh],cx
  0053e007  66894c2410            mov       [esp+10h],cx
  0053e00c  53                    push      ebx
  0053e00d  8d4c240c              lea       ecx,[esp+0Ch]
  0053e011  53                    push      ebx
  0053e012  51                    push      ecx
  0053e013  50                    push      eax
  0053e014  56                    push      esi
  0053e015  891d5ce15d00          mov       [5DE15Ch],ebx
  0053e01b  66891dfe8f5e00        mov       [5E8FFEh],bx
  0053e022  881d88ca5e00          mov       [5ECA88h],bl
  0053e028  881d89ca5e00          mov       [5ECA89h],bl
  0053e02e  881d8aca5e00          mov       [5ECA8Ah],bl
  0053e034  c6058bca5e00df        mov       byte [5ECA8Bh],0DFh
  0053e03b  881d8cca5e00          mov       [5ECA8Ch],bl
  0053e041  c6058dca5e0001        mov       byte [5ECA8Dh],1
  0053e048  c6058eca5e003f        mov       byte [5ECA8Eh],3Fh
  0053e04f  881d8fca5e00          mov       [5ECA8Fh],bl
  0053e055  66c74424267000        mov       word [esp+26h],70h
  0053e05c  66c74424280014        mov       word [esp+28h],1400h
  0053e063  66c744242a4912        mov       word [esp+2Ah],1249h
  0053e06a  66895c242c            mov       [esp+2Ch],bx
  0053e06f  c744241c88ca5e00      mov       dword [esp+1Ch],5ECA88h
  0053e077  e8a43efeff            call      00521F20h
  0053e07c  83c414                add       esp,14h
  0053e07f  8d542408              lea       edx,[esp+8]
  0053e083  52                    push      edx
  0053e084  50                    push      eax
  0053e085  56                    push      esi
  0053e086  e83519feff            call      0051F9C0h
  0053e08b  83c40c                add       esp,0Ch
  0053e08e  5e                    pop       esi
  0053e08f  5b                    pop       ebx
  0053e090  83c414                add       esp,14h
  0053e093  c3                    ret
  0053e094  90                    nop
  0053e095  90                    nop
  0053e096  90                    nop
  0053e097  90                    nop
  0053e098  90                    nop
  0053e099  90                    nop
  0053e09a  90                    nop
  0053e09b  90                    nop
  0053e09c  90                    nop
  0053e09d  90                    nop
  0053e09e  90                    nop
  0053e09f  90                    nop

2 cible(s) de saut interne : 0x0053dfc3, 0x0053dfdf
