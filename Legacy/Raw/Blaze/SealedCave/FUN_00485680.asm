Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00485680  section .text  offset fichier 0x84a80  224 octets

  00485680  83ec0c                sub       esp,0Ch
  00485683  53                    push      ebx
  00485684  56                    push      esi
  00485685  8b742418              mov       esi,[esp+18h]
  00485689  57                    push      edi
  0048568a  33db                  xor       ebx,ebx
  0048568c  56                    push      esi
  0048568d  66895c2410            mov       [esp+10h],bx
  00485692  66895c2412            mov       [esp+12h],bx
  00485697  66895c2414            mov       [esp+14h],bx
  0048569c  66c74424162c01        mov       word [esp+16h],12Ch
  004856a3  66895c2418            mov       [esp+18h],bx
  004856a8  66895c241a            mov       [esp+1Ah],bx
  004856ad  e8ce010b00            call      00535880h
  004856b2  0fbf4628              movsx     eax,word [esi+28h]
  004856b6  b900400000            mov       ecx,4000h
  004856bb  83c404                add       esp,4
  004856be  2bc8                  sub       ecx,eax
  004856c0  c1f902                sar       ecx,2
  004856c3  03c1                  add       eax,ecx
  004856c5  66894628              mov       [esi+28h],ax
  004856c9  6689462c              mov       [esi+2Ch],ax
  004856cd  8d1480                lea       edx,[eax+eax*4]
  004856d0  8d0480                lea       eax,[eax+eax*4]
  004856d3  c1e204                shl       edx,4
  004856d6  8d04c0                lea       eax,[eax+eax*8]
  004856d9  d1e0                  shl       eax,1
  004856db  c1f80c                sar       eax,0Ch
  004856de  6689442416            mov       [esp+16h],ax
  004856e3  8b4638                mov       eax,[esi+38h]
  004856e6  c1fa0c                sar       edx,0Ch
  004856e9  3d40404000            cmp       eax,404040h
  004856ee  6689542410            mov       [esp+10h],dx
  004856f3  724e                  jb        short 00485743h
  004856f5  8d4c240c              lea       ecx,[esp+0Ch]
  004856f9  51                    push      ecx
  004856fa  56                    push      esi
  004856fb  e870f5feff            call      00474C70h
  00485700  8bf8                  mov       edi,eax
  00485702  a1b8e05d00            mov       eax,[5DE0B8h]
  00485707  83c408                add       esp,8
  0048570a  3bc3                  cmp       eax,ebx
  0048570c  7435                  je        short 00485743h
  0048570e  8b5674                mov       edx,[esi+74h]
  00485711  57                    push      edi
  00485712  52                    push      edx
  00485713  e868440b00            call      00539B80h
  00485718  8b17                  mov       edx,[edi]
  0048571a  83c408                add       esp,8
  0048571d  8d461c                lea       eax,[esi+1Ch]
  00485720  83ca40                or        edx,40h
  00485723  50                    push      eax
  00485724  6800180000            push      1800h
  00485729  6800040000            push      400h
  0048572e  6a03                  push      3
  00485730  66c787960000001000    mov       word [edi+96h],10h
  00485739  8917                  mov       [edi],edx
  0048573b  e8e0830b00            call      0053DB20h
  00485740  83c410                add       esp,10h

LAB_00485743:
  00485743  8b4638                mov       eax,[esi+38h]
  00485746  05f0efefff            add       eax,0FFEFEFF0h
  0048574b  894638                mov       [esi+38h],eax
  0048574e  7502                  jne       short 00485752h
  00485750  891e                  mov       [esi],ebx

LAB_00485752:
  00485752  5f                    pop       edi
  00485753  5e                    pop       esi
  00485754  5b                    pop       ebx
  00485755  83c40c                add       esp,0Ch
  00485758  c3                    ret
  00485759  90                    nop
  0048575a  90                    nop
  0048575b  90                    nop
  0048575c  90                    nop
  0048575d  90                    nop
  0048575e  90                    nop
  0048575f  90                    nop

2 cible(s) de saut interne : 0x00485743, 0x00485752
