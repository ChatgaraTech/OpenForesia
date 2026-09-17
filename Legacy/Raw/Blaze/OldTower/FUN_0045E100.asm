C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0045e100  section .text  offset fichier 0x5d500  256 octets

  0045e100  8b442404              mov       eax,[esp+4]
  0045e104  6683781000            cmp       word [eax+10h],0
  0045e109  7514                  jne       short 0045E11Fh
  0045e10b  668b4816              mov       cx,[eax+16h]
  0045e10f  66c740280000          mov       word [eax+28h],0
  0045e115  66c740100100          mov       word [eax+10h],1
  0045e11b  66894812              mov       [eax+12h],cx

LAB_0045e11f:
  0045e11f  668b4814              mov       cx,[eax+14h]
  0045e123  8a15e0165700          mov       dl,[5716E0h]
  0045e129  6683f901              cmp       cx,1
  0045e12d  7509                  jne       short 0045E138h
  0045e12f  f6c201                test      dl,1
  0045e132  0f84a4000000          je        near 0045E1DCh

LAB_0045e138:
  0045e138  6683f902              cmp       cx,2
  0045e13c  7508                  jne       short 0045E146h
  0045e13e  84d1                  test      cl,dl
  0045e140  0f8496000000          je        near 0045E1DCh

LAB_0045e146:
  0045e146  6683f903              cmp       cx,3
  0045e14a  7509                  jne       short 0045E155h
  0045e14c  f6c204                test      dl,4
  0045e14f  0f8487000000          je        near 0045E1DCh

LAB_0045e155:
  0045e155  6683f904              cmp       cx,4
  0045e159  7505                  jne       short 0045E160h
  0045e15b  f6c208                test      dl,8
  0045e15e  747c                  je        short 0045E1DCh

LAB_0045e160:
  0045e160  6683f905              cmp       cx,5
  0045e164  7505                  jne       short 0045E16Bh
  0045e166  f6c210                test      dl,10h
  0045e169  7471                  je        short 0045E1DCh

LAB_0045e16b:
  0045e16b  6683781001            cmp       word [eax+10h],1
  0045e170  756a                  jne       short 0045E1DCh
  0045e172  668b5012              mov       dx,[eax+12h]
  0045e176  66015028              add       [eax+28h],dx
  0045e17a  668b5028              mov       dx,[eax+28h]
  0045e17e  6681fa0010            cmp       dx,1000h
  0045e183  7c57                  jl        short 0045E1DCh
  0045e185  8b10                  mov       edx,[eax]
  0045e187  66c740280010          mov       word [eax+28h],1000h
  0045e18d  81ca00000040          or        edx,40000000h
  0045e193  66c740100200          mov       word [eax+10h],2
  0045e199  8910                  mov       [eax],edx
  0045e19b  0fbfc1                movsx     eax,cx
  0045e19e  48                    dec       eax
  0045e19f  83f804                cmp       eax,4
  0045e1a2  7738                  ja        short 0045E1DCh
  0045e1a4  ff2485e0e14500        jmp       dword [eax*4+45E1E0h]
  0045e1ab  668125e0165700feff    and       word [5716E0h],0FFFEh
  0045e1b4  c3                    ret
  0045e1b5  668125e0165700fdff    and       word [5716E0h],0FFFDh
  0045e1be  c3                    ret
  0045e1bf  668125e0165700fbff    and       word [5716E0h],0FFFBh
  0045e1c8  c3                    ret
  0045e1c9  668125e0165700f7ff    and       word [5716E0h],0FFF7h
  0045e1d2  c3                    ret
  0045e1d3  668125e0165700efff    and       word [5716E0h],0FFEFh

LAB_0045e1dc:
  0045e1dc  c3                    ret
  0045e1dd  90                    nop
  0045e1de  8bff                  mov       edi,edi
  0045e1e0  ab                    stosd
  0045e1e1  e145                  loope     0045E228h
  0045e1e3  00b5e14500bf          add       [ebp-40FFBA1Fh],dh
  0045e1e9  e145                  loope     0045E230h
  0045e1eb  00c9                  add       cl,cl
  0045e1ed  e145                  loope     0045E234h
  0045e1ef  00d3                  add       bl,dl
  0045e1f1  e145                  loope     0045E238h
  0045e1f3  009090909090          add       [eax-6F6F6F70h],dl
  0045e1f9  90                    nop
  0045e1fa  90                    nop
  0045e1fb  90                    nop
  0045e1fc  90                    nop
  0045e1fd  90                    nop
  0045e1fe  90                    nop
  0045e1ff  90                    nop

7 cible(s) de saut interne : 0x0045e11f, 0x0045e138, 0x0045e146, 0x0045e155, 0x0045e160, 0x0045e16b, 0x0045e1dc
