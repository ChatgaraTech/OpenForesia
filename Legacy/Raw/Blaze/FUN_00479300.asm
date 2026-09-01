Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00479300  section .text  offset fichier 0x78700  400 octets

  00479300  51                    push      ecx
  00479301  53                    push      ebx
  00479302  55                    push      ebp
  00479303  56                    push      esi
  00479304  8b742414              mov       esi,[esp+14h]
  00479308  57                    push      edi
  00479309  8b06                  mov       eax,[esi]
  0047930b  a900000002            test      eax,2000000h
  00479310  740c                  je        short 0047931Eh
  00479312  c70600000000          mov       dword [esi],0
  00479318  5f                    pop       edi
  00479319  5e                    pop       esi
  0047931a  5d                    pop       ebp
  0047931b  5b                    pop       ebx
  0047931c  59                    pop       ecx
  0047931d  c3                    ret

LAB_0047931e:
  0047931e  668b4e10              mov       cx,[esi+10h]
  00479322  33ed                  xor       ebp,ebp
  00479324  663bcd                cmp       cx,bp
  00479327  0f85af000000          jne       near 004793DCh
  0047932d  8b5674                mov       edx,[esi+74h]
  00479330  8b461c                mov       eax,[esi+1Ch]
  00479333  8b4e24                mov       ecx,[esi+24h]
  00479336  52                    push      edx
  00479337  0fbf7e32              movsx     edi,word [esi+32h]
  0047933b  66c746100100          mov       word [esi+10h],1
  00479341  8944241c              mov       [esp+1Ch],eax
  00479345  894c2414              mov       [esp+14h],ecx
  00479349  e872f80b00            call      00538BC0h
  0047934e  8b5660                mov       edx,[esi+60h]
  00479351  8bd8                  mov       ebx,eax
  00479353  83c404                add       esp,4
  00479356  66896e12              mov       [esi+12h],bp
  0047935a  8d431e                lea       eax,[ebx+1Eh]
  0047935d  c6460439              mov       byte [esi+4],39h
  00479361  c1e00c                shl       eax,0Ch
  00479364  03c2                  add       eax,edx
  00479366  bd03000000            mov       ebp,3
  0047936b  d1fb                  sar       ebx,1
  0047936d  668146320004          add       word [esi+32h],400h
  00479373  894620                mov       [esi+20h],eax

LAB_00479376:
  00479376  57                    push      edi
  00479377  e8a0c60c00            call      00545A1Ch
  0047937c  0fafc3                imul      eax,ebx
  0047937f  8b4c2418              mov       ecx,[esp+18h]
  00479383  57                    push      edi
  00479384  03c1                  add       eax,ecx
  00479386  89461c                mov       [esi+1Ch],eax
  00479389  e888c60c00            call      00545A16h
  0047938e  0fafc3                imul      eax,ebx
  00479391  8b542410              mov       edx,[esp+10h]
  00479395  33c9                  xor       ecx,ecx
  00479397  8a4e0b                mov       cl,[esi+0Bh]
  0047939a  6aff                  push      0FFFFFFFFh
  0047939c  51                    push      ecx
  0047939d  03c2                  add       eax,edx
  0047939f  6a7f                  push      7Fh
  004793a1  56                    push      esi
  004793a2  894624                mov       [esi+24h],eax
  004793a5  e836170000            call      0047AAE0h
  004793aa  668146125505          add       word [esi+12h],555h
  004793b0  83c410                add       esp,10h
  004793b3  81c755050000          add       edi,555h
  004793b9  4d                    dec       ebp
  004793ba  75ba                  jne       short 00479376h
  004793bc  8d561c                lea       edx,[esi+1Ch]
  004793bf  52                    push      edx
  004793c0  68f8185500            push      5518F8h
  004793c5  e816480c00            call      0053DBE0h
  004793ca  8b7674                mov       esi,[esi+74h]
  004793cd  83c408                add       esp,8
  004793d0  8b06                  mov       eax,[esi]
  004793d2  0c80                  or        al,80h
  004793d4  8906                  mov       [esi],eax
  004793d6  5f                    pop       edi
  004793d7  5e                    pop       esi
  004793d8  5d                    pop       ebp
  004793d9  5b                    pop       ebx
  004793da  59                    pop       ecx
  004793db  c3                    ret

LAB_004793dc:
  004793dc  a802                  test      al,2
  004793de  0f849e000000          je        near 00479482h
  004793e4  a900008000            test      eax,800000h
  004793e9  7477                  je        short 00479462h
  004793eb  8b4e74                mov       ecx,[esi+74h]
  004793ee  8b3db4876000          mov       edi,[6087B4h]
  004793f4  33d2                  xor       edx,edx
  004793f6  8a5108                mov       dl,[ecx+8]
  004793f9  8b1f                  mov       ebx,[edi]
  004793fb  8d4a78                lea       ecx,[edx+78h]
  004793fe  8d3cd2                lea       edi,[edx+edx*8]
  00479401  c1e10d                shl       ecx,0Dh
  00479404  03cb                  add       ecx,ebx
  00479406  8d147a                lea       edx,[edx+edi*2]
  00479409  33db                  xor       ebx,ebx
  0047940b  8a1c9588915e00        mov       bl,[edx*4+5E9188h]
  00479412  c1e304                shl       ebx,4
  00479415  668b93204a6000        mov       dx,[ebx+604A20h]
  0047941c  668591c41f0000        test      [ecx+1FC4h],dx
  00479423  7507                  jne       short 0047942Ch
  00479425  0d00000002            or        eax,2000000h
  0047942a  8906                  mov       [esi],eax

LAB_0047942c:
  0047942c  a1f02c5f00            mov       eax,[5F2CF0h]
  00479431  33d2                  xor       edx,edx
  00479433  bf05000000            mov       edi,5
  00479438  f7f7                  div       edi
  0047943a  85d2                  test      edx,edx
  0047943c  7531                  jne       short 0047946Fh
  0047943e  668b814e010000        mov       ax,[ecx+14Eh]
  00479445  663d0200              cmp       ax,2
  00479449  7c0c                  jl        short 00479457h
  0047944b  83c0fe                add       eax,0FFFFFFFEh
  0047944e  6689814e010000        mov       [ecx+14Eh],ax
  00479455  eb18                  jmp       short 0047946Fh

LAB_00479457:
  00479457  6689a94e010000        mov       [ecx+14Eh],bp
  0047945e  8b06                  mov       eax,[esi]
  00479460  eb06                  jmp       short 00479468h

LAB_00479462:
  00479462  6683f978              cmp       cx,78h
  00479466  7c07                  jl        short 0047946Fh

LAB_00479468:
  00479468  0d00000002            or        eax,2000000h
  0047946d  8906                  mov       [esi],eax

LAB_0047946f:
  0047946f  56                    push      esi
  00479470  e89bbcffff            call      00475110h
  00479475  83c404                add       esp,4
  00479478  85c0                  test      eax,eax
  0047947a  7406                  je        short 00479482h
  0047947c  810e00000002          or        dword [esi],2000000h

LAB_00479482:
  00479482  5f                    pop       edi
  00479483  5e                    pop       esi
  00479484  5d                    pop       ebp
  00479485  5b                    pop       ebx
  00479486  59                    pop       ecx
  00479487  c3                    ret
  00479488  90                    nop
  00479489  90                    nop
  0047948a  90                    nop
  0047948b  90                    nop
  0047948c  90                    nop
  0047948d  90                    nop
  0047948e  90                    nop
  0047948f  90                    nop

9 cible(s) de saut interne : 0x0047931e, 0x00479376, 0x004793dc, 0x0047942c, 0x00479457, 0x00479462, 0x00479468, 0x0047946f, 0x00479482
