Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004343d0  section .text  offset fichier 0x337d0  320 octets

  004343d0  53                    push      ebx
  004343d1  55                    push      ebp
  004343d2  56                    push      esi
  004343d3  8b742410              mov       esi,[esp+10h]
  004343d7  57                    push      edi
  004343d8  8b466c                mov       eax,[esi+6Ch]
  004343db  8bf8                  mov       edi,eax
  004343dd  85ff                  test      edi,edi
  004343df  7505                  jne       short 004343E6h
  004343e1  bf58e55d00            mov       edi,5DE558h

LAB_004343e6:
  004343e6  894674                mov       [esi+74h],eax
  004343e9  8b471c                mov       eax,[edi+1Ch]
  004343ec  8d6e1c                lea       ebp,[esi+1Ch]
  004343ef  bb01000000            mov       ebx,1
  004343f4  894500                mov       [ebp],eax
  004343f7  8b4f20                mov       ecx,[edi+20h]
  004343fa  81c100600900          add       ecx,96000h
  00434400  66837e1000            cmp       word [esi+10h],0
  00434405  894e20                mov       [esi+20h],ecx
  00434408  8b5724                mov       edx,[edi+24h]
  0043440b  895624                mov       [esi+24h],edx
  0043440e  668b4732              mov       ax,[edi+32h]
  00434412  66894632              mov       [esi+32h],ax
  00434416  7526                  jne       short 0043443Eh
  00434418  6a00                  push      0
  0043441a  6a00                  push      0
  0043441c  6800100000            push      1000h
  00434421  68fd060000            push      6FDh
  00434426  6a1f                  push      1Fh
  00434428  56                    push      esi
  00434429  66895e10              mov       [esi+10h],bx
  0043442d  e84e3c0300            call      00468080h
  00434432  83c418                add       esp,18h
  00434435  66c786980000000000    mov       word [esi+98h],0

LAB_0043443e:
  0043443e  66399e98000000        cmp       [esi+98h],bx
  00434445  7526                  jne       short 0043446Dh
  00434447  66399e9a000000        cmp       [esi+9Ah],bx
  0043444e  6a00                  push      0
  00434450  6a00                  push      0
  00434452  6800080000            push      800h
  00434457  6800100000            push      1000h
  0043445c  7504                  jne       short 00434462h
  0043445e  6a23                  push      23h
  00434460  eb02                  jmp       short 00434464h

LAB_00434462:
  00434462  6a20                  push      20h

LAB_00434464:
  00434464  56                    push      esi
  00434465  e8163c0300            call      00468080h
  0043446a  83c418                add       esp,18h

LAB_0043446d:
  0043446d  6683be9800000005      cmp       word [esi+98h],5
  00434475  7509                  jne       short 00434480h
  00434477  66c786980000000000    mov       word [esi+98h],0

LAB_00434480:
  00434480  66ff8698000000        inc       word [esi+98h]
  00434487  8a4743                mov       al,[edi+43h]
  0043448a  3ac3                  cmp       al,bl
  0043448c  7506                  jne       short 00434494h
  0043448e  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00434494:
  00434494  8b3f                  mov       edi,[edi]
  00434496  f7c700000040          test      edi,40000000h
  0043449c  7508                  jne       short 004344A6h
  0043449e  f7c700000080          test      edi,80000000h
  004344a4  7506                  jne       short 004344ACh

LAB_004344a6:
  004344a6  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004344ac:
  004344ac  0fbf4e32              movsx     ecx,word [esi+32h]
  004344b0  51                    push      ecx
  004344b1  e866151100            call      00545A1Ch
  004344b6  8b7d00                mov       edi,[ebp]
  004344b9  8d0480                lea       eax,[eax+eax*4]
  004344bc  8d1480                lea       edx,[eax+eax*4]
  004344bf  0fbf4632              movsx     eax,word [esi+32h]
  004344c3  d1e2                  shl       edx,1
  004344c5  03fa                  add       edi,edx
  004344c7  50                    push      eax
  004344c8  897d00                mov       [ebp],edi
  004344cb  e846151100            call      00545A16h
  004344d0  8b7e24                mov       edi,[esi+24h]
  004344d3  8b5620                mov       edx,[esi+20h]
  004344d6  8d0480                lea       eax,[eax+eax*4]
  004344d9  6aff                  push      0FFFFFFFFh
  004344db  6a00                  push      0
  004344dd  68df000000            push      0DFh
  004344e2  8d0c80                lea       ecx,[eax+eax*4]
  004344e5  6a00                  push      0
  004344e7  d1e1                  shl       ecx,1
  004344e9  03f9                  add       edi,ecx
  004344eb  81c20060ffff          add       edx,0FFFF6000h
  004344f1  55                    push      ebp
  004344f2  56                    push      esi
  004344f3  897e24                mov       [esi+24h],edi
  004344f6  895620                mov       [esi+20h],edx
  004344f9  e842670400            call      0047AC40h
  004344fe  83c418                add       esp,18h
  00434501  5f                    pop       edi
  00434502  5e                    pop       esi
  00434503  5d                    pop       ebp
  00434504  5b                    pop       ebx
  00434505  c3                    ret
  00434506  90                    nop
  00434507  90                    nop
  00434508  90                    nop
  00434509  90                    nop
  0043450a  90                    nop
  0043450b  90                    nop
  0043450c  90                    nop
  0043450d  90                    nop
  0043450e  90                    nop
  0043450f  90                    nop

9 cible(s) de saut interne : 0x004343e6, 0x0043443e, 0x00434462, 0x00434464, 0x0043446d, 0x00434480, 0x00434494, 0x004344a6, 0x004344ac
