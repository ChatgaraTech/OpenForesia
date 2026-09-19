Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004314c0  section .text  offset fichier 0x308c0  320 octets

  004314c0  53                    push      ebx
  004314c1  55                    push      ebp
  004314c2  56                    push      esi
  004314c3  8b742410              mov       esi,[esp+10h]
  004314c7  57                    push      edi
  004314c8  8b7e6c                mov       edi,[esi+6Ch]
  004314cb  85ff                  test      edi,edi
  004314cd  897e74                mov       [esi+74h],edi
  004314d0  7505                  jne       short 004314D7h
  004314d2  bf58e55d00            mov       edi,5DE558h

LAB_004314d7:
  004314d7  8b471c                mov       eax,[edi+1Ch]
  004314da  8d6e1c                lea       ebp,[esi+1Ch]
  004314dd  bb01000000            mov       ebx,1
  004314e2  894500                mov       [ebp],eax
  004314e5  8b4f20                mov       ecx,[edi+20h]
  004314e8  81c100600900          add       ecx,96000h
  004314ee  66837e1000            cmp       word [esi+10h],0
  004314f3  894e20                mov       [esi+20h],ecx
  004314f6  8b5724                mov       edx,[edi+24h]
  004314f9  895624                mov       [esi+24h],edx
  004314fc  668b4732              mov       ax,[edi+32h]
  00431500  66894632              mov       [esi+32h],ax
  00431504  7526                  jne       short 0043152Ch
  00431506  6a00                  push      0
  00431508  6a00                  push      0
  0043150a  6800100000            push      1000h
  0043150f  68fd060000            push      6FDh
  00431514  6a1f                  push      1Fh
  00431516  56                    push      esi
  00431517  66895e10              mov       [esi+10h],bx
  0043151b  e8606b0300            call      00468080h
  00431520  83c418                add       esp,18h
  00431523  66c786980000000000    mov       word [esi+98h],0

LAB_0043152c:
  0043152c  66399e98000000        cmp       [esi+98h],bx
  00431533  7526                  jne       short 0043155Bh
  00431535  66399e9a000000        cmp       [esi+9Ah],bx
  0043153c  6a00                  push      0
  0043153e  6a00                  push      0
  00431540  6800080000            push      800h
  00431545  6800100000            push      1000h
  0043154a  7504                  jne       short 00431550h
  0043154c  6a23                  push      23h
  0043154e  eb02                  jmp       short 00431552h

LAB_00431550:
  00431550  6a20                  push      20h

LAB_00431552:
  00431552  56                    push      esi
  00431553  e8286b0300            call      00468080h
  00431558  83c418                add       esp,18h

LAB_0043155b:
  0043155b  6683be9800000005      cmp       word [esi+98h],5
  00431563  7509                  jne       short 0043156Eh
  00431565  66c786980000000000    mov       word [esi+98h],0

LAB_0043156e:
  0043156e  66ff8698000000        inc       word [esi+98h]
  00431575  8a4743                mov       al,[edi+43h]
  00431578  3ac3                  cmp       al,bl
  0043157a  7506                  jne       short 00431582h
  0043157c  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00431582:
  00431582  8b3f                  mov       edi,[edi]
  00431584  f7c700000040          test      edi,40000000h
  0043158a  7508                  jne       short 00431594h
  0043158c  f7c700000080          test      edi,80000000h
  00431592  7506                  jne       short 0043159Ah

LAB_00431594:
  00431594  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0043159a:
  0043159a  0fbf4e32              movsx     ecx,word [esi+32h]
  0043159e  51                    push      ecx
  0043159f  e878441100            call      00545A1Ch
  004315a4  8b7d00                mov       edi,[ebp]
  004315a7  8d0480                lea       eax,[eax+eax*4]
  004315aa  8d1480                lea       edx,[eax+eax*4]
  004315ad  0fbf4632              movsx     eax,word [esi+32h]
  004315b1  d1e2                  shl       edx,1
  004315b3  03fa                  add       edi,edx
  004315b5  50                    push      eax
  004315b6  897d00                mov       [ebp],edi
  004315b9  e858441100            call      00545A16h
  004315be  8b7e24                mov       edi,[esi+24h]
  004315c1  8b5620                mov       edx,[esi+20h]
  004315c4  8d0480                lea       eax,[eax+eax*4]
  004315c7  6aff                  push      0FFFFFFFFh
  004315c9  6a00                  push      0
  004315cb  68de000000            push      0DEh
  004315d0  8d0c80                lea       ecx,[eax+eax*4]
  004315d3  6a00                  push      0
  004315d5  d1e1                  shl       ecx,1
  004315d7  03f9                  add       edi,ecx
  004315d9  81c20060ffff          add       edx,0FFFF6000h
  004315df  55                    push      ebp
  004315e0  56                    push      esi
  004315e1  897e24                mov       [esi+24h],edi
  004315e4  895620                mov       [esi+20h],edx
  004315e7  e854960400            call      0047AC40h
  004315ec  83c418                add       esp,18h
  004315ef  5f                    pop       edi
  004315f0  5e                    pop       esi
  004315f1  5d                    pop       ebp
  004315f2  5b                    pop       ebx
  004315f3  c3                    ret
  004315f4  90                    nop
  004315f5  90                    nop
  004315f6  90                    nop
  004315f7  90                    nop
  004315f8  90                    nop
  004315f9  90                    nop
  004315fa  90                    nop
  004315fb  90                    nop
  004315fc  90                    nop
  004315fd  90                    nop
  004315fe  90                    nop
  004315ff  90                    nop

9 cible(s) de saut interne : 0x004314d7, 0x0043152c, 0x00431550, 0x00431552, 0x0043155b, 0x0043156e, 0x00431582, 0x00431594, 0x0043159a
