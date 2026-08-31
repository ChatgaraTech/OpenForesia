Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004824f0  section .text  offset fichier 0x818f0  954 octets

  004824f0  83ec18                sub       esp,18h
  004824f3  53                    push      ebx
  004824f4  55                    push      ebp
  004824f5  56                    push      esi
  004824f6  8b742428              mov       esi,[esp+28h]
  004824fa  33ed                  xor       ebp,ebp
  004824fc  57                    push      edi
  004824fd  66396e10              cmp       [esi+10h],bp
  00482501  66896c2410            mov       [esp+10h],bp
  00482506  66c744241200ff        mov       word [esp+12h],0FF00h
  0048250d  66c74424140001        mov       word [esp+14h],100h
  00482514  66c74424162c01        mov       word [esp+16h],12Ch
  0048251b  66896c2418            mov       [esp+18h],bp
  00482520  66c744241a0002        mov       word [esp+1Ah],200h
  00482527  7555                  jne       short 0048257Eh
  00482529  8b0e                  mov       ecx,[esi]
  0048252b  804e3606              or        byte [esi+36h],6
  0048252f  6a08                  push      8
  00482531  55                    push      ebp
  00482532  80c980                or        cl,80h
  00482535  6a78                  push      78h
  00482537  56                    push      esi
  00482538  66c746100100          mov       word [esi+10h],1
  0048253e  890e                  mov       [esi],ecx
  00482540  896e70                mov       [esi+70h],ebp
  00482543  c74638ff808000        mov       dword [esi+38h],8080FFh
  0048254a  c7462800180018        mov       dword [esi+28h],18001800h
  00482551  66c7462c0018          mov       word [esi+2Ch],1800h
  00482557  66c7869a000000ff7f    mov       word [esi+9Ah],7FFFh
  00482560  c646041c              mov       byte [esi+4],1Ch
  00482564  e87785ffff            call      0047AAE0h
  00482569  83c410                add       esp,10h
  0048256c  c6460439              mov       byte [esi+4],39h
  00482570  6a02                  push      2
  00482572  55                    push      ebp
  00482573  6a78                  push      78h
  00482575  56                    push      esi
  00482576  e86585ffff            call      0047AAE0h
  0048257b  83c410                add       esp,10h

LAB_0048257e:
  0048257e  8b0e                  mov       ecx,[esi]
  00482580  f7c100000002          test      ecx,2000000h
  00482586  0f845c010000          je        near 004826E8h
  0048258c  668b4610              mov       ax,[esi+10h]
  00482590  663d0a00              cmp       ax,0Ah
  00482594  0f8d44010000          jge       near 004826DEh
  0048259a  f7c100000040          test      ecx,40000000h
  004825a0  0f8538010000          jne       near 004826DEh
  004825a6  40                    inc       eax
  004825a7  66c7461a0700          mov       word [esi+1Ah],7
  004825ad  66894610              mov       [esi+10h],ax
  004825b1  c7463860606001        mov       dword [esi+38h],1606060h
  004825b8  e8834b0c00            call      00547140h
  004825bd  0fbf4e32              movsx     ecx,word [esi+32h]
  004825c1  25ff010000            and       eax,1FFh
  004825c6  8d9c0800fbffff        lea       ebx,[eax+ecx-500h]
  004825cd  53                    push      ebx
  004825ce  e849340c00            call      00545A1Ch
  004825d3  c1e006                shl       eax,6
  004825d6  53                    push      ebx
  004825d7  894650                mov       [esi+50h],eax
  004825da  c7465400000400        mov       dword [esi+54h],40000h
  004825e1  e830340c00            call      00545A16h
  004825e6  c1e006                shl       eax,6
  004825e9  894658                mov       [esi+58h],eax
  004825ec  66c746120300          mov       word [esi+12h],3
  004825f2  c7462800200020        mov       dword [esi+28h],20002000h
  004825f9  66c746140050          mov       word [esi+14h],5000h
  004825ff  66c746160200          mov       word [esi+16h],2
  00482605  e8364b0c00            call      00547140h
  0048260a  8b4e1c                mov       ecx,[esi+1Ch]
  0048260d  8b5620                mov       edx,[esi+20h]
  00482610  25ff010000            and       eax,1FFh
  00482615  89542420              mov       [esp+20h],edx
  00482619  2d00010000            sub       eax,100h
  0048261e  c1e00c                shl       eax,0Ch
  00482621  03c1                  add       eax,ecx
  00482623  8944241c              mov       [esp+1Ch],eax
  00482627  e8144b0c00            call      00547140h
  0048262c  8b7e24                mov       edi,[esi+24h]
  0048262f  25ff010000            and       eax,1FFh
  00482634  2d00010000            sub       eax,100h
  00482639  6a02                  push      2
  0048263b  c1e00c                shl       eax,0Ch
  0048263e  03c7                  add       eax,edi
  00482640  55                    push      ebp
  00482641  8944242c              mov       [esp+2Ch],eax
  00482645  68be000000            push      0BEh
  0048264a  8d442428              lea       eax,[esp+28h]
  0048264e  68081a5500            push      551A08h
  00482653  50                    push      eax
  00482654  56                    push      esi
  00482655  e8e685ffff            call      0047AC40h
  0048265a  83c418                add       esp,18h
  0048265d  8d4c2410              lea       ecx,[esp+10h]
  00482661  51                    push      ecx
  00482662  56                    push      esi
  00482663  e83826ffff            call      00474CA0h
  00482668  8bf8                  mov       edi,eax
  0048266a  83c408                add       esp,8
  0048266d  3bfd                  cmp       edi,ebp
  0048266f  744b                  je        short 004826BCh
  00482671  8b4e74                mov       ecx,[esi+74h]
  00482674  33d2                  xor       edx,edx
  00482676  8a5606                mov       dl,[esi+6]
  00482679  33c0                  xor       eax,eax
  0048267b  8a4607                mov       al,[esi+7]
  0048267e  55                    push      ebp
  0048267f  52                    push      edx
  00482680  50                    push      eax
  00482681  57                    push      edi
  00482682  51                    push      ecx
  00482683  e8287c0b00            call      0053A2B0h
  00482688  83c414                add       esp,14h
  0048268b  53                    push      ebx
  0048268c  e88b330c00            call      00545A1Ch
  00482691  8b4f50                mov       ecx,[edi+50h]
  00482694  53                    push      ebx
  00482695  c1e005                shl       eax,5
  00482698  03c8                  add       ecx,eax
  0048269a  894f50                mov       [edi+50h],ecx
  0048269d  e874330c00            call      00545A16h
  004826a2  8b4f58                mov       ecx,[edi+58h]
  004826a5  66c787960000001000    mov       word [edi+96h],10h
  004826ae  c1e005                shl       eax,5
  004826b1  03c8                  add       ecx,eax
  004826b3  8b07                  mov       eax,[edi]
  004826b5  0c40                  or        al,40h
  004826b7  894f58                mov       [edi+58h],ecx
  004826ba  8907                  mov       [edi],eax

LAB_004826bc:
  004826bc  a1ace05d00            mov       eax,[5DE0ACh]
  004826c1  3bc5                  cmp       eax,ebp
  004826c3  0f84d9010000          je        near 004828A2h
  004826c9  8bd0                  mov       edx,eax
  004826cb  c1fa02                sar       edx,2
  004826ce  2bd0                  sub       edx,eax
  004826d0  8915ace05d00          mov       [5DE0ACh],edx
  004826d6  5f                    pop       edi
  004826d7  5e                    pop       esi
  004826d8  5d                    pop       ebp
  004826d9  5b                    pop       ebx
  004826da  83c418                add       esp,18h
  004826dd  c3                    ret

LAB_004826de:
  004826de  892e                  mov       [esi],ebp
  004826e0  5f                    pop       edi
  004826e1  5e                    pop       esi
  004826e2  5d                    pop       ebp
  004826e3  5b                    pop       ebx
  004826e4  83c418                add       esp,18h
  004826e7  c3                    ret

LAB_004826e8:
  004826e8  0fbf7e32              movsx     edi,word [esi+32h]
  004826ec  81ef00040000          sub       edi,400h
  004826f2  57                    push      edi
  004826f3  e824330c00            call      00545A1Ch
  004826f8  8b4e50                mov       ecx,[esi+50h]
  004826fb  57                    push      edi
  004826fc  c1e003                shl       eax,3
  004826ff  03c8                  add       ecx,eax
  00482701  894e50                mov       [esi+50h],ecx
  00482704  e80d330c00            call      00545A16h
  00482709  8b4e58                mov       ecx,[esi+58h]
  0048270c  56                    push      esi
  0048270d  c1e003                shl       eax,3
  00482710  03c8                  add       ecx,eax
  00482712  894e58                mov       [esi+58h],ecx
  00482715  e866310b00            call      00535880h
  0048271a  83c404                add       esp,4
  0048271d  56                    push      esi
  0048271e  e81d120b00            call      00533940h
  00482723  8b4660                mov       eax,[esi+60h]
  00482726  8b4e20                mov       ecx,[esi+20h]
  00482729  83c404                add       esp,4
  0048272c  3bc8                  cmp       ecx,eax
  0048272e  7f12                  jg        short 00482742h
  00482730  894620                mov       [esi+20h],eax
  00482733  8b8690000000          mov       eax,[esi+90h]
  00482739  80cc08                or        ah,8
  0048273c  898690000000          mov       [esi+90h],eax

LAB_00482742:
  00482742  f7869000000002880000  test      dword [esi+90h],8802h
  0048274c  750c                  jne       short 0048275Ah
  0048274e  f70600000020          test      dword [esi],20000000h
  00482754  0f8438010000          je        near 00482892h

LAB_0048275a:
  0048275a  6a03                  push      3
  0048275c  55                    push      ebp
  0048275d  b800400000            mov       eax,4000h
  00482762  bf00200000            mov       edi,2000h
  00482767  bb80808000            mov       ebx,808080h
  0048276c  6a6c                  push      6Ch
  0048276e  56                    push      esi
  0048276f  66896e34              mov       [esi+34h],bp
  00482773  66c7462ec0ff          mov       word [esi+2Eh],0FFC0h
  00482779  896e44                mov       [esi+44h],ebp
  0048277c  896e48                mov       [esi+48h],ebp
  0048277f  896e4c                mov       [esi+4Ch],ebp
  00482782  894650                mov       [esi+50h],eax
  00482785  897e54                mov       [esi+54h],edi
  00482788  894658                mov       [esi+58h],eax
  0048278b  895e38                mov       [esi+38h],ebx
  0048278e  896e28                mov       [esi+28h],ebp
  00482791  66896e2c              mov       [esi+2Ch],bp
  00482795  c6460407              mov       byte [esi+4],7
  00482799  e84283ffff            call      0047AAE0h
  0048279e  83c410                add       esp,10h
  004827a1  897e50                mov       [esi+50h],edi
  004827a4  897e54                mov       [esi+54h],edi
  004827a7  897e58                mov       [esi+58h],edi
  004827aa  6a0e                  push      0Eh
  004827ac  6a01                  push      1
  004827ae  6a6c                  push      6Ch
  004827b0  56                    push      esi
  004827b1  c6460401              mov       byte [esi+4],1
  004827b5  e82683ffff            call      0047AAE0h
  004827ba  83c410                add       esp,10h
  004827bd  897e50                mov       [esi+50h],edi
  004827c0  c7465400100000        mov       dword [esi+54h],1000h
  004827c7  897e58                mov       [esi+58h],edi
  004827ca  6a06                  push      6
  004827cc  6a01                  push      1
  004827ce  6a6c                  push      6Ch
  004827d0  56                    push      esi
  004827d1  895e38                mov       [esi+38h],ebx
  004827d4  c7462800700050        mov       dword [esi+28h],50007000h
  004827db  66c7462c0070          mov       word [esi+2Ch],7000h
  004827e1  c6460426              mov       byte [esi+4],26h
  004827e5  e8f682ffff            call      0047AAE0h
  004827ea  83c410                add       esp,10h
  004827ed  8d6e1c                lea       ebp,[esi+1Ch]
  004827f0  bb04000000            mov       ebx,4

LAB_004827f5:
  004827f5  e846490c00            call      00547140h
  004827fa  8bf8                  mov       edi,eax
  004827fc  57                    push      edi
  004827fd  e81a320c00            call      00545A1Ch
  00482802  c1e005                shl       eax,5
  00482805  894650                mov       [esi+50h],eax
  00482808  e833490c00            call      00547140h
  0048280d  83e01f                and       eax,1Fh
  00482810  57                    push      edi
  00482811  83c032                add       eax,32h
  00482814  c1e00c                shl       eax,0Ch
  00482817  894654                mov       [esi+54h],eax
  0048281a  e8f7310c00            call      00545A16h
  0048281f  6a02                  push      2
  00482821  6a00                  push      0
  00482823  c1e005                shl       eax,5
  00482826  68bf000000            push      0BFh
  0048282b  894658                mov       [esi+58h],eax
  0048282e  c74638f0f0f001        mov       dword [esi+38h],1F0F0F0h
  00482835  c7462800288002        mov       dword [esi+28h],2802800h
  0048283c  66c7462c1f00          mov       word [esi+2Ch],1Fh
  00482842  66c7461a0100          mov       word [esi+1Ah],1
  00482848  e8f3480c00            call      00547140h
  0048284d  99                    cdq
  0048284e  b903000000            mov       ecx,3
  00482853  f7f9                  idiv      ecx
  00482855  8d14d5f81a5500        lea       edx,[edx*8+551AF8h]
  0048285c  52                    push      edx
  0048285d  55                    push      ebp
  0048285e  56                    push      esi
  0048285f  e8dc83ffff            call      0047AC40h
  00482864  83c418                add       esp,18h
  00482867  4b                    dec       ebx
  00482868  758b                  jne       short 004827F5h
  0048286a  8b06                  mov       eax,[esi]
  0048286c  55                    push      ebp
  0048286d  6800120000            push      1200h
  00482872  0d00000002            or        eax,2000000h
  00482877  6800040000            push      400h
  0048287c  6a2e                  push      2Eh
  0048287e  8906                  mov       [esi],eax
  00482880  e89bb20b00            call      0053DB20h
  00482885  83c410                add       esp,10h
  00482888  c705ace05d0008000000  mov       dword [5DE0ACh],8

LAB_00482892:
  00482892  8b06                  mov       eax,[esi]
  00482894  a900000040            test      eax,40000000h
  00482899  7407                  je        short 004828A2h
  0048289b  0d00000002            or        eax,2000000h
  004828a0  8906                  mov       [esi],eax

LAB_004828a2:
  004828a2  5f                    pop       edi
  004828a3  5e                    pop       esi
  004828a4  5d                    pop       ebp
  004828a5  5b                    pop       ebx
  004828a6  83c418                add       esp,18h
  004828a9  c3                    ret

9 cible(s) de saut interne : 0x0048257e, 0x004826bc, 0x004826de, 0x004826e8, 0x00482742, 0x0048275a, 0x004827f5, 0x00482892, 0x004828a2
