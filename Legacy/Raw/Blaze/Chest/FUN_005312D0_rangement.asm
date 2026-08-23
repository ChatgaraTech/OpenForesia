Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy\Binaries\Blaze.exe  base 0x00400000
0x005312d0  section .text  offset fichier 0x1306d0  640 octets

  005312d0  83ec0c                sub       esp,0Ch
  005312d3  53                    push      ebx
  005312d4  55                    push      ebp
  005312d5  56                    push      esi
  005312d6  57                    push      edi
  005312d7  e8f4fbffff            call      00530ED0h
  005312dc  a09f905e00            mov       al,[5E909Fh]
  005312e1  33db                  xor       ebx,ebx
  005312e3  8a1d507b5f00          mov       bl,[5F7B50h]
  005312e9  84c0                  test      al,al
  005312eb  740b                  je        short 005312F8h
  005312ed  8d4301                lea       eax,[ebx+1]
  005312f0  5f                    pop       edi
  005312f1  5e                    pop       esi
  005312f2  5d                    pop       ebp
  005312f3  5b                    pop       ebx
  005312f4  83c40c                add       esp,0Ch
  005312f7  c3                    ret

LAB_005312f8:
  005312f8  8b6c2420              mov       ebp,[esp+20h]
  005312fc  f7450000008000        test      dword [ebp],800000h
  00531303  750b                  jne       short 00531310h
  00531305  8d4301                lea       eax,[ebx+1]
  00531308  5f                    pop       edi
  00531309  5e                    pop       esi
  0053130a  5d                    pop       ebp
  0053130b  5b                    pop       ebx
  0053130c  83c40c                add       esp,0Ch
  0053130f  c3                    ret

LAB_00531310:
  00531310  8b1524086000          mov       edx,[600824h]
  00531316  8b0db4876000          mov       ecx,[6087B4h]
  0053131c  33c0                  xor       eax,eax
  0053131e  8d3c9b                lea       edi,[ebx+ebx*4]
  00531321  8a4508                mov       al,[ebp+8]
  00531324  8bf0                  mov       esi,eax
  00531326  c1e706                shl       edi,6
  00531329  83c678                add       esi,78h
  0053132c  03fa                  add       edi,edx
  0053132e  8b11                  mov       edx,[ecx]
  00531330  897c2420              mov       [esp+20h],edi
  00531334  c1e60d                shl       esi,0Dh
  00531337  03f2                  add       esi,edx
  00531339  8d551c                lea       edx,[ebp+1Ch]
  0053133c  52                    push      edx
  0053133d  6800130000            push      1300h
  00531342  6800040000            push      400h
  00531347  6a29                  push      29h
  00531349  e8d2c70000            call      0053DB20h
  0053134e  8b456c                mov       eax,[ebp+6Ch]
  00531351  83c410                add       esp,10h
  00531354  85c0                  test      eax,eax
  00531356  741f                  je        short 00531377h
  00531358  8b08                  mov       ecx,[eax]
  0053135a  81e1ffffff7f          and       ecx,7FFFFFFFh
  00531360  8908                  mov       [eax],ecx
  00531362  33c9                  xor       ecx,ecx
  00531364  8a4808                mov       cl,[eax+8]
  00531367  51                    push      ecx
  00531368  6a40                  push      40h
  0053136a  68607b5f00            push      5F7B60h
  0053136f  e89c130000            call      00532710h
  00531374  83c40c                add       esp,0Ch

LAB_00531377:
  00531377  8a9650010000          mov       dl,[esi+150h]
  0053137d  80e207                and       dl,7
  00531380  80fa04                cmp       dl,4
  00531383  7537                  jne       short 005313BCh
  00531385  668bbf86000000        mov       di,[edi+86h]
  0053138c  6681ffc932            cmp       di,32C9h
  00531391  7229                  jb        short 005313BCh
  00531393  6681ffcf32            cmp       di,32CFh
  00531398  7722                  ja        short 005313BCh
  0053139a  81e7ffff0000          and       edi,0FFFFh
  005313a0  8a8c3eafceffff        mov       cl,[esi+edi-3151h]
  005313a7  8d843eafceffff        lea       eax,[esi+edi-3151h]
  005313ae  fec1                  inc       cl
  005313b0  8808                  mov       [eax],cl
  005313b2  33c0                  xor       eax,eax
  005313b4  5f                    pop       edi
  005313b5  5e                    pop       esi
  005313b6  5d                    pop       ebp
  005313b7  5b                    pop       ebx
  005313b8  83c40c                add       esp,0Ch
  005313bb  c3                    ret

LAB_005313bc:
  005313bc  33c0                  xor       eax,eax
  005313be  33ff                  xor       edi,edi
  005313c0  a090ca5e00            mov       al,[5ECA90h]
  005313c5  8d0480                lea       eax,[eax+eax*4]
  005313c8  c1e007                shl       eax,7
  005313cb  668bbc3012030000      mov       di,[eax+esi+312h]
  005313d3  8d0c30                lea       ecx,[eax+esi]
  005313d6  33c0                  xor       eax,eax
  005313d8  668b8110030000        mov       ax,[ecx+310h]
  005313df  81c100030000          add       ecx,300h
  005313e5  89442410              mov       [esp+10h],eax
  005313e9  8b442420              mov       eax,[esp+20h]
  005313ed  c744242050000000      mov       dword [esp+20h],50h

LAB_005313f5:
  005313f5  8b10                  mov       edx,[eax]
  005313f7  83c004                add       eax,4
  005313fa  8911                  mov       [ecx],edx
  005313fc  8b542420              mov       edx,[esp+20h]
  00531400  83c104                add       ecx,4
  00531403  4a                    dec       edx
  00531404  89542420              mov       [esp+20h],edx
  00531408  75eb                  jne       short 005313F5h
  0053140a  66a1a68f5e00          mov       ax,[5E8FA6h]
  00531410  8acb                  mov       cl,bl
  00531412  8ad0                  mov       dl,al
  00531414  83e10f                and       ecx,0Fh
  00531417  83e23f                and       edx,3Fh
  0053141a  6a00                  push      0
  0053141c  03ca                  add       ecx,edx
  0053141e  66c744241c1000        mov       word [esp+1Ch],10h
  00531425  c1e104                shl       ecx,4
  00531428  66894c2418            mov       [esp+18h],cx
  0053142d  8bcb                  mov       ecx,ebx
  0053142f  c1f904                sar       ecx,4
  00531432  66c1e806              shr       ax,6
  00531436  03c8                  add       ecx,eax
  00531438  66c744241e0100        mov       word [esp+1Eh],1
  0053143f  66894c241a            mov       [esp+1Ah],cx
  00531444  e87b460100            call      00545AC4h
  00531449  33c0                  xor       eax,eax
  0053144b  8d4c2414              lea       ecx,[esp+14h]
  0053144f  a090ca5e00            mov       al,[5ECA90h]
  00531454  8d1480                lea       edx,[eax+eax*4]
  00531457  c1e207                shl       edx,7
  0053145a  8d843240040000        lea       eax,[edx+esi+440h]
  00531461  50                    push      eax
  00531462  51                    push      ecx
  00531463  e856460100            call      00545ABEh
  00531468  6a00                  push      0
  0053146a  e855460100            call      00545AC4h
  0053146f  33d2                  xor       edx,edx
  00531471  33c0                  xor       eax,eax
  00531473  8a5508                mov       dl,[ebp+8]
  00531476  a090ca5e00            mov       al,[5ECA90h]
  0053147b  81c2f1010000          add       edx,1F1h
  00531481  8d4c2414              lea       ecx,[esp+14h]
  00531485  c1e004                shl       eax,4
  00531488  52                    push      edx
  00531489  50                    push      eax
  0053148a  51                    push      ecx
  0053148b  e8ae470100            call      00545C3Eh
  00531490  8bc3                  mov       eax,ebx
  00531492  b90a000000            mov       ecx,0Ah
  00531497  99                    cdq
  00531498  f7f9                  idiv      ecx
  0053149a  8a0da48f5e00          mov       cl,[5E8FA4h]
  005314a0  6a00                  push      0
  005314a2  8ac1                  mov       al,cl
  005314a4  83e110                and       ecx,10h
  005314a7  c1e104                shl       ecx,4
  005314aa  66c744241c0600        mov       word [esp+1Ch],6
  005314b1  66c744241e1800        mov       word [esp+1Eh],18h
  005314b8  83e00f                and       eax,0Fh
  005314bb  8d1452                lea       edx,[edx+edx*2]
  005314be  c1e006                shl       eax,6
  005314c1  8d1450                lea       edx,[eax+edx*2]
  005314c4  b867666666            mov       eax,66666667h
  005314c9  6689542418            mov       [esp+18h],dx
  005314ce  f7eb                  imul      ebx
  005314d0  c1fa02                sar       edx,2
  005314d3  8bc2                  mov       eax,edx
  005314d5  c1e81f                shr       eax,1Fh
  005314d8  8d540204              lea       edx,[edx+eax+4]
  005314dc  8d1452                lea       edx,[edx+edx*2]
  005314df  8d04d1                lea       eax,[ecx+edx*8]
  005314e2  668944241a            mov       [esp+1Ah],ax
  005314e7  e8d8450100            call      00545AC4h
  005314ec  33c0                  xor       eax,eax
  005314ee  a090ca5e00            mov       al,[5ECA90h]
  005314f3  8d0c80                lea       ecx,[eax+eax*4]
  005314f6  8d442414              lea       eax,[esp+14h]
  005314fa  c1e107                shl       ecx,7
  005314fd  8d943160040000        lea       edx,[ecx+esi+460h]
  00531504  52                    push      edx
  00531505  50                    push      eax
  00531506  e8b3450100            call      00545ABEh
  0053150b  6a00                  push      0
  0053150d  e8b2450100            call      00545AC4h
  00531512  33c0                  xor       eax,eax
  00531514  8a4508                mov       al,[ebp+8]
  00531517  8d0c40                lea       ecx,[eax+eax*2]
  0053151a  8d14cd00010000        lea       edx,[ecx*8+100h]
  00531521  52                    push      edx
  00531522  33c0                  xor       eax,eax
  00531524  8d4c2418              lea       ecx,[esp+18h]
  00531528  a090ca5e00            mov       al,[5ECA90h]
  0053152d  8d0440                lea       eax,[eax+eax*2]
  00531530  c1e003                shl       eax,3
  00531533  c1f802                sar       eax,2
  00531536  0580010000            add       eax,180h
  0053153b  50                    push      eax
  0053153c  51                    push      ecx
  0053153d  e8fc460100            call      00545C3Eh
  00531542  33c0                  xor       eax,eax
  00531544  81e700800000          and       edi,8000h
  0053154a  a090ca5e00            mov       al,[5ECA90h]
  0053154f  8d                    (bad)
  (octet invalide — la plage demandee deborde probablement du code)
