Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00456750  section .text  offset fichier 0x55b50  734 octets

  00456750  55                    push      ebp
  00456751  56                    push      esi
  00456752  8b74240c              mov       esi,[esp+0Ch]
  00456756  33ed                  xor       ebp,ebp
  00456758  57                    push      edi
  00456759  66396e10              cmp       [esi+10h],bp
  0045675d  0f85d6000000          jne       near 00456839h
  00456763  896e5c                mov       [esi+5Ch],ebp
  00456766  6689ae9a000000        mov       [esi+9Ah],bp
  0045676d  66896e12              mov       [esi+12h],bp
  00456771  33ff                  xor       edi,edi
  00456773  b9c0935e00            mov       ecx,5E93C0h
  00456778  ba04000000            mov       edx,4

LAB_0045677d:
  0045677d  8b01                  mov       eax,[ecx]
  0045677f  a900000080            test      eax,80000000h
  00456784  7408                  je        short 0045678Eh
  00456786  a900000050            test      eax,50000000h
  0045678b  7501                  jne       short 0045678Eh
  0045678d  47                    inc       edi

LAB_0045678e:
  0045678e  81c19c000000          add       ecx,9Ch
  00456794  4a                    dec       edx
  00456795  75e6                  jne       short 0045677Dh
  00456797  6800000040            push      40000000h
  0045679c  6a10                  push      10h
  0045679e  56                    push      esi
  0045679f  e85ce80d00            call      00535000h
  004567a4  83c40c                add       esp,0Ch
  004567a7  663bc5                cmp       ax,bp
  004567aa  0f8489000000          je        near 00456839h
  004567b0  663bf8                cmp       di,ax
  004567b3  0f8580000000          jne       near 00456839h
  004567b9  8a0dff965e00          mov       cl,[5E96FFh]
  004567bf  f6c104                test      cl,4
  004567c2  7475                  je        short 00456839h
  004567c4  f6c108                test      cl,8
  004567c7  7470                  je        short 00456839h
  004567c9  b9c0935e00            mov       ecx,5E93C0h
  004567ce  bf04000000            mov       edi,4

LAB_004567d3:
  004567d3  8b11                  mov       edx,[ecx]
  004567d5  f7c200000080          test      edx,80000000h
  004567db  741a                  je        short 004567F7h
  004567dd  f7c200000050          test      edx,50000000h
  004567e3  7512                  jne       short 004567F7h
  004567e5  8b5124                mov       edx,[ecx+24h]
  004567e8  81e200f0ffff          and       edx,0FFFFF000h
  004567ee  81fa00400100          cmp       edx,14000h
  004567f4  7d01                  jge       short 004567F7h
  004567f6  45                    inc       ebp

LAB_004567f7:
  004567f7  81c19c000000          add       ecx,9Ch
  004567fd  4f                    dec       edi
  004567fe  75d3                  jne       short 004567D3h
  00456800  663be8                cmp       bp,ax
  00456803  7534                  jne       short 00456839h
  00456805  33c0                  xor       eax,eax
  00456807  66c746100200          mov       word [esi+10h],2
  0045680d  a038e55d00            mov       al,[5DE538h]
  00456812  6a00                  push      0
  00456814  c60560905e0063        mov       byte [5E9060h],63h
  0045681b  8d0c80                lea       ecx,[eax+eax*4]
  0045681e  c1e103                shl       ecx,3
  00456821  2bc8                  sub       ecx,eax
  00456823  a1cc975e00            mov       eax,[5E97CCh]
  00456828  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  0045682f  52                    push      edx
  00456830  50                    push      eax
  00456831  e89ad50b00            call      00513DD0h
  00456836  83c40c                add       esp,0Ch

LAB_00456839:
  00456839  66837e1002            cmp       word [esi+10h],2
  0045683e  7559                  jne       short 00456899h
  00456840  a060905e00            mov       al,[5E9060h]
  00456845  84c0                  test      al,al
  00456847  7506                  jne       short 0045684Fh
  00456849  66c746100100          mov       word [esi+10h],1

LAB_0045684f:
  0045684f  803d60905e0001        cmp       byte [5E9060h],1
  00456856  7541                  jne       short 00456899h
  00456858  33ff                  xor       edi,edi
  0045685a  b9c0935e00            mov       ecx,5E93C0h
  0045685f  ba04000000            mov       edx,4

LAB_00456864:
  00456864  8b01                  mov       eax,[ecx]
  00456866  a900000080            test      eax,80000000h
  0045686b  7408                  je        short 00456875h
  0045686d  a900000050            test      eax,50000000h
  00456872  7501                  jne       short 00456875h
  00456874  47                    inc       edi

LAB_00456875:
  00456875  81c19c000000          add       ecx,9Ch
  0045687b  4a                    dec       edx
  0045687c  75e6                  jne       short 00456864h
  0045687e  6800000040            push      40000000h
  00456883  6a10                  push      10h
  00456885  56                    push      esi
  00456886  e875e70d00            call      00535000h
  0045688b  83c40c                add       esp,0Ch
  0045688e  663bf8                cmp       di,ax
  00456891  7406                  je        short 00456899h
  00456893  66c746100000          mov       word [esi+10h],0

LAB_00456899:
  00456899  66837e1001            cmp       word [esi+10h],1
  0045689e  0f8586010000          jne       near 00456A2Ah
  004568a4  56                    push      esi
  004568a5  e896d00d00            call      00533940h
  004568aa  8b8e80000000          mov       ecx,[esi+80h]
  004568b0  83c404                add       esp,4
  004568b3  890d707d5f00          mov       [5F7D70h],ecx
  004568b9  8b9684000000          mov       edx,[esi+84h]
  004568bf  8915747d5f00          mov       [5F7D74h],edx
  004568c5  8b8688000000          mov       eax,[esi+88h]
  004568cb  a3787d5f00            mov       [5F7D78h],eax
  004568d0  8b8e8c000000          mov       ecx,[esi+8Ch]
  004568d6  890d7c7d5f00          mov       [5F7D7Ch],ecx
  004568dc  8b561c                mov       edx,[esi+1Ch]
  004568df  8915309a5e00          mov       [5E9A30h],edx
  004568e5  8b4620                mov       eax,[esi+20h]
  004568e8  a3349a5e00            mov       [5E9A34h],eax
  004568ed  8b4e24                mov       ecx,[esi+24h]
  004568f0  890d389a5e00          mov       [5E9A38h],ecx
  004568f6  668b9690000000        mov       dx,[esi+90h]
  004568fd  668915389b5e00        mov       [5E9B38h],dx
  00456904  8b467c                mov       eax,[esi+7Ch]
  00456907  a3487b5f00            mov       [5F7B48h],eax
  0045690c  8b7e5c                mov       edi,[esi+5Ch]
  0045690f  47                    inc       edi
  00456910  66ff869a000000        inc       word [esi+9Ah]
  00456917  8bc7                  mov       eax,edi
  00456919  897e5c                mov       [esi+5Ch],edi
  0045691c  83f80a                cmp       eax,0Ah
  0045691f  7c07                  jl        short 00456928h
  00456921  c7465c0a000000        mov       dword [esi+5Ch],0Ah

LAB_00456928:
  00456928  0fbf4e32              movsx     ecx,word [esi+32h]
  0045692c  51                    push      ecx
  0045692d  e8eaf00e00            call      00545A1Ch
  00456932  0faf465c              imul      eax,[esi+5Ch]
  00456936  0fbf5632              movsx     edx,word [esi+32h]
  0045693a  8b4e24                mov       ecx,[esi+24h]
  0045693d  52                    push      edx
  0045693e  2bc8                  sub       ecx,eax
  00456940  894e24                mov       [esi+24h],ecx
  00456943  e8cef00e00            call      00545A16h
  00456948  0faf465c              imul      eax,[esi+5Ch]
  0045694c  8b6e1c                mov       ebp,[esi+1Ch]
  0045694f  668b4e12              mov       cx,[esi+12h]
  00456953  66014e32              add       [esi+32h],cx
  00456957  03e8                  add       ebp,eax
  00456959  668b869a000000        mov       ax,[esi+9Ah]
  00456960  896e1c                mov       [esi+1Ch],ebp
  00456963  663d1900              cmp       ax,19h
  00456967  7c0d                  jl        short 00456976h
  00456969  663d2100              cmp       ax,21h
  0045696d  7f07                  jg        short 00456976h
  0045696f  83c1fc                add       ecx,0FFFFFFFCh
  00456972  66894e12              mov       [esi+12h],cx

LAB_00456976:
  00456976  663d2300              cmp       ax,23h
  0045697a  7c0b                  jl        short 00456987h
  0045697c  663d2b00              cmp       ax,2Bh
  00456980  7f05                  jg        short 00456987h
  00456982  6683461204            add       word [esi+12h],4

LAB_00456987:
  00456987  663d6c00              cmp       ax,6Ch
  0045698b  7c0b                  jl        short 00456998h
  0045698d  663d7500              cmp       ax,75h
  00456991  7f05                  jg        short 00456998h
  00456993  6683461203            add       word [esi+12h],3

LAB_00456998:
  00456998  663d7800              cmp       ax,78h
  0045699c  7c0b                  jl        short 004569A9h
  0045699e  663d8100              cmp       ax,81h
  004569a2  7f05                  jg        short 004569A9h
  004569a4  66834612fd            add       word [esi+12h],0FFFDh

LAB_004569a9:
  004569a9  663dc800              cmp       ax,0C8h
  004569ad  753a                  jne       short 004569E9h
  004569af  66c746106300          mov       word [esi+10h],63h
  004569b5  bfc0935e00            mov       edi,5E93C0h
  004569ba  bd04000000            mov       ebp,4

LAB_004569bf:
  004569bf  8b07                  mov       eax,[edi]
  004569c1  a900000080            test      eax,80000000h
  004569c6  7418                  je        short 004569E0h
  004569c8  a900000050            test      eax,50000000h
  004569cd  7511                  jne       short 004569E0h
  004569cf  a1cc975e00            mov       eax,[5E97CCh]
  004569d4  6a01                  push      1
  004569d6  57                    push      edi
  004569d7  50                    push      eax
  004569d8  e8f3d30b00            call      00513DD0h
  004569dd  83c40c                add       esp,0Ch

LAB_004569e0:
  004569e0  81c79c000000          add       edi,9Ch
  004569e6  4d                    dec       ebp
  004569e7  75d6                  jne       short 004569BFh

LAB_004569e9:
  004569e9  bf02000000            mov       edi,2

LAB_004569ee:
  004569ee  6a00                  push      0
  004569f0  6a00                  push      0
  004569f2  6aff                  push      0FFFFFFFFh
  004569f4  6a00                  push      0
  004569f6  6a30                  push      30h
  004569f8  56                    push      esi
  004569f9  e812170100            call      00468110h
  004569fe  83c418                add       esp,18h
  00456a01  e83a070f00            call      00547140h
  00456a06  99                    cdq
  00456a07  b906000000            mov       ecx,6
  00456a0c  f7f9                  idiv      ecx
  00456a0e  6683fa01              cmp       dx,1
  00456a12  7513                  jne       short 00456A27h
  00456a14  6a00                  push      0
  00456a16  6a00                  push      0
  00456a18  6aff                  push      0FFFFFFFFh
  00456a1a  6a00                  push      0
  00456a1c  6a30                  push      30h
  00456a1e  56                    push      esi
  00456a1f  e8ec160100            call      00468110h
  00456a24  83c418                add       esp,18h

LAB_00456a27:
  00456a27  4f                    dec       edi
  00456a28  75c4                  jne       short 004569EEh

LAB_00456a2a:
  00456a2a  5f                    pop       edi
  00456a2b  5e                    pop       esi
  00456a2c  5d                    pop       ebp
  00456a2d  c3                    ret

20 cible(s) de saut interne : 0x0045677d, 0x0045678e, 0x004567d3, 0x004567f7, 0x00456839, 0x0045684f, 0x00456864, 0x00456875, 0x00456899, 0x00456928, 0x00456976, 0x00456987, 0x00456998, 0x004569a9, 0x004569bf, 0x004569e0, 0x004569e9, 0x004569ee, 0x00456a27, 0x00456a2a
