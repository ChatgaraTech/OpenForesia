Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004498b0  section .text  offset fichier 0x48cb0  384 octets

  004498b0  56                    push      esi
  004498b1  8b742408              mov       esi,[esp+8]
  004498b5  6a00                  push      0
  004498b7  6a00                  push      0
  004498b9  56                    push      esi
  004498ba  e881e7ffff            call      00448040h
  004498bf  83c40c                add       esp,0Ch
  004498c2  66837e1000            cmp       word [esi+10h],0
  004498c7  750d                  jne       short 004498D6h
  004498c9  6a00                  push      0
  004498cb  6a00                  push      0
  004498cd  56                    push      esi
  004498ce  e8dde3ffff            call      00447CB0h
  004498d3  83c40c                add       esp,0Ch

LAB_004498d6:
  004498d6  8b4678                mov       eax,[esi+78h]
  004498d9  85c0                  test      eax,eax
  004498db  7505                  jne       short 004498E2h
  004498dd  b858e55d00            mov       eax,5DE558h

LAB_004498e2:
  004498e2  8b481c                mov       ecx,[eax+1Ch]
  004498e5  66837e1000            cmp       word [esi+10h],0
  004498ea  894e1c                mov       [esi+1Ch],ecx
  004498ed  8b5020                mov       edx,[eax+20h]
  004498f0  895620                mov       [esi+20h],edx
  004498f3  8b4824                mov       ecx,[eax+24h]
  004498f6  894e24                mov       [esi+24h],ecx
  004498f9  668b5032              mov       dx,[eax+32h]
  004498fd  66895632              mov       [esi+32h],dx
  00449901  7542                  jne       short 00449945h
  00449903  6aff                  push      0FFFFFFFFh
  00449905  6a00                  push      0
  00449907  68a9000000            push      0A9h
  0044990c  56                    push      esi
  0044990d  66c746100100          mov       word [esi+10h],1
  00449913  66c746160000          mov       word [esi+16h],0
  00449919  66c7869a0000000600    mov       word [esi+9Ah],6
  00449922  66c746140010          mov       word [esi+14h],1000h
  00449928  c7464400100000        mov       dword [esi+44h],1000h
  0044992f  c7464846000000        mov       dword [esi+48h],46h
  00449936  c7465c2c010000        mov       dword [esi+5Ch],12Ch
  0044993d  e89e110300            call      0047AAE0h
  00449942  83c410                add       esp,10h

LAB_00449945:
  00449945  66837e1600            cmp       word [esi+16h],0
  0044994a  7519                  jne       short 00449965h
  0044994c  6800120000            push      1200h
  00449951  6800120000            push      1200h
  00449956  686a010000            push      16Ah
  0044995b  6a30                  push      30h
  0044995d  e83e350f00            call      0053CEA0h
  00449962  83c410                add       esp,10h

LAB_00449965:
  00449965  66837e1601            cmp       word [esi+16h],1
  0044996a  7519                  jne       short 00449985h
  0044996c  6800120000            push      1200h
  00449971  6800120000            push      1200h
  00449976  6800020000            push      200h
  0044997b  6a10                  push      10h
  0044997d  e81e350f00            call      0053CEA0h
  00449982  83c410                add       esp,10h

LAB_00449985:
  00449985  668b4616              mov       ax,[esi+16h]
  00449989  663d0200              cmp       ax,2
  0044998d  7430                  je        short 004499BFh
  0044998f  663d0c00              cmp       ax,0Ch
  00449993  742a                  je        short 004499BFh
  00449995  663d1600              cmp       ax,16h
  00449999  7424                  je        short 004499BFh
  0044999b  663d2000              cmp       ax,20h
  0044999f  741e                  je        short 004499BFh
  004499a1  663d2a00              cmp       ax,2Ah
  004499a5  7418                  je        short 004499BFh
  004499a7  663d3400              cmp       ax,34h
  004499ab  7412                  je        short 004499BFh
  004499ad  663d3e00              cmp       ax,3Eh
  004499b1  740c                  je        short 004499BFh
  004499b3  663d4800              cmp       ax,48h
  004499b7  7406                  je        short 004499BFh
  004499b9  663d5200              cmp       ax,52h
  004499bd  7519                  jne       short 004499D8h

LAB_004499bf:
  004499bf  6800120000            push      1200h
  004499c4  6800120000            push      1200h
  004499c9  6800100000            push      1000h
  004499ce  6a27                  push      27h
  004499d0  e8cb340f00            call      0053CEA0h
  004499d5  83c410                add       esp,10h

LAB_004499d8:
  004499d8  66837e163c            cmp       word [esi+16h],3Ch
  004499dd  7d12                  jge       short 004499F1h
  004499df  6aff                  push      0FFFFFFFFh
  004499e1  6a00                  push      0
  004499e3  688c000000            push      8Ch
  004499e8  56                    push      esi
  004499e9  e8f2100300            call      0047AAE0h
  004499ee  83c410                add       esp,10h

LAB_004499f1:
  004499f1  66ff4616              inc       word [esi+16h]
  004499f5  66837e1664            cmp       word [esi+16h],64h
  004499fa  7c31                  jl        short 00449A2Dh
  004499fc  8b4674                mov       eax,[esi+74h]
  004499ff  8a4e06                mov       cl,[esi+6]
  00449a02  6a00                  push      0
  00449a04  884806                mov       [eax+6],cl
  00449a07  8b5674                mov       edx,[esi+74h]
  00449a0a  8a4607                mov       al,[esi+7]
  00449a0d  884207                mov       [edx+7],al
  00449a10  8b4e78                mov       ecx,[esi+78h]
  00449a13  8b5674                mov       edx,[esi+74h]
  00449a16  51                    push      ecx
  00449a17  52                    push      edx
  00449a18  e813000000            call      00449A30h
  00449a1d  83c40c                add       esp,0Ch
  00449a20  6a00                  push      0
  00449a22  6a00                  push      0
  00449a24  56                    push      esi
  00449a25  e8f6e5ffff            call      00448020h
  00449a2a  83c40c                add       esp,0Ch

LAB_00449a2d:
  00449a2d  5e                    pop       esi
  00449a2e  c3                    ret
  00449a2f  90                    nop

9 cible(s) de saut interne : 0x004498d6, 0x004498e2, 0x00449945, 0x00449965, 0x00449985, 0x004499bf, 0x004499d8, 0x004499f1, 0x00449a2d
