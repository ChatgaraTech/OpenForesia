
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047eae0  section .text  offset fichier 0x7dee0  323 octets

  0047eae0  53                    push      ebx
  0047eae1  55                    push      ebp
  0047eae2  56                    push      esi
  0047eae3  8b742410              mov       esi,[esp+10h]
  0047eae7  8b06                  mov       eax,[esi]
  0047eae9  a900000002            test      eax,2000000h
  0047eaee  7420                  je        short 0047EB10h
  0047eaf0  a802                  test      al,2
  0047eaf2  0f8427010000          je        near 0047EC1Fh
  0047eaf8  668b4612              mov       ax,[esi+12h]
  0047eafc  6685c0                test      ax,ax
  0047eaff  7406                  je        short 0047EB07h
  0047eb01  c70600000000          mov       dword [esi],0

LAB_0047eb07:
  0047eb07  40                    inc       eax
  0047eb08  66894612              mov       [esi+12h],ax
  0047eb0c  5e                    pop       esi
  0047eb0d  5d                    pop       ebp
  0047eb0e  5b                    pop       ebx
  0047eb0f  c3                    ret

LAB_0047eb10:
  0047eb10  66837e1000            cmp       word [esi+10h],0
  0047eb15  7557                  jne       short 0047EB6Eh
  0047eb17  33c0                  xor       eax,eax
  0047eb19  6aff                  push      0FFFFFFFFh
  0047eb1b  8a460b                mov       al,[esi+0Bh]
  0047eb1e  c7463800000000        mov       dword [esi+38h],0
  0047eb25  50                    push      eax
  0047eb26  6889000000            push      89h
  0047eb2b  56                    push      esi
  0047eb2c  c7462800140030        mov       dword [esi+28h],30001400h
  0047eb33  66c7462c0014          mov       word [esi+2Ch],1400h
  0047eb39  c646041f              mov       byte [esi+4],1Fh
  0047eb3d  e89ebfffff            call      0047AAE0h
  0047eb42  8b4674                mov       eax,[esi+74h]
  0047eb45  c7467800000000        mov       dword [esi+78h],0
  0047eb4c  83c410                add       esp,10h
  0047eb4f  8b08                  mov       ecx,[eax]
  0047eb51  80c980                or        cl,80h
  0047eb54  8908                  mov       [eax],ecx
  0047eb56  8d4e1c                lea       ecx,[esi+1Ch]
  0047eb59  51                    push      ecx
  0047eb5a  6800120000            push      1200h
  0047eb5f  68fd060000            push      6FDh
  0047eb64  6a1a                  push      1Ah
  0047eb66  e8b5ef0b00            call      0053DB20h
  0047eb6b  83c410                add       esp,10h

LAB_0047eb6e:
  0047eb6e  8b06                  mov       eax,[esi]
  0047eb70  a802                  test      al,2
  0047eb72  0f8490000000          je        near 0047EC08h
  0047eb78  a900008000            test      eax,800000h
  0047eb7d  747b                  je        short 0047EBFAh
  0047eb7f  8b5674                mov       edx,[esi+74h]
  0047eb82  8b1db4876000          mov       ebx,[6087B4h]
  0047eb88  33c9                  xor       ecx,ecx
  0047eb8a  8a4a08                mov       cl,[edx+8]
  0047eb8d  8b2b                  mov       ebp,[ebx]
  0047eb8f  8bd1                  mov       edx,ecx
  0047eb91  8d1cd2                lea       ebx,[edx+edx*8]
  0047eb94  8d4a78                lea       ecx,[edx+78h]
  0047eb97  c1e10d                shl       ecx,0Dh
  0047eb9a  8d145a                lea       edx,[edx+ebx*2]
  0047eb9d  33db                  xor       ebx,ebx
  0047eb9f  03cd                  add       ecx,ebp
  0047eba1  8a1c9588915e00        mov       bl,[edx*4+5E9188h]
  0047eba8  c1e304                shl       ebx,4
  0047ebab  668b93204a6000        mov       dx,[ebx+604A20h]
  0047ebb2  668591c41f0000        test      [ecx+1FC4h],dx
  0047ebb9  7507                  jne       short 0047EBC2h
  0047ebbb  0d00000002            or        eax,2000000h
  0047ebc0  8906                  mov       [esi],eax

LAB_0047ebc2:
  0047ebc2  a1f02c5f00            mov       eax,[5F2CF0h]
  0047ebc7  33d2                  xor       edx,edx
  0047ebc9  bb14000000            mov       ebx,14h
  0047ebce  f7f3                  div       ebx
  0047ebd0  85d2                  test      edx,edx
  0047ebd2  7534                  jne       short 0047EC08h
  0047ebd4  668b814e010000        mov       ax,[ecx+14Eh]
  0047ebdb  663d0300              cmp       ax,3
  0047ebdf  7c0c                  jl        short 0047EBEDh
  0047ebe1  83c0fd                add       eax,0FFFFFFFDh
  0047ebe4  6689814e010000        mov       [ecx+14Eh],ax
  0047ebeb  eb1b                  jmp       short 0047EC08h

LAB_0047ebed:
  0047ebed  66c7814e0100000000    mov       word [ecx+14Eh],0
  0047ebf6  8b06                  mov       eax,[esi]
  0047ebf8  eb07                  jmp       short 0047EC01h

LAB_0047ebfa:
  0047ebfa  66837e1078            cmp       word [esi+10h],78h
  0047ebff  7c07                  jl        short 0047EC08h

LAB_0047ec01:
  0047ec01  0d00000002            or        eax,2000000h
  0047ec06  8906                  mov       [esi],eax

LAB_0047ec08:
  0047ec08  56                    push      esi
  0047ec09  e80265ffff            call      00475110h
  0047ec0e  83c404                add       esp,4
  0047ec11  85c0                  test      eax,eax
  0047ec13  7406                  je        short 0047EC1Bh
  0047ec15  810e00000002          or        dword [esi],2000000h

LAB_0047ec1b:
  0047ec1b  66ff4610              inc       word [esi+10h]

LAB_0047ec1f:
  0047ec1f  5e                    pop       esi
  0047ec20  5d                    pop       ebp
  0047ec21  5b                    pop       ebx
  0047ec22  c3                    ret

10 cible(s) de saut interne : 0x0047eb07, 0x0047eb10, 0x0047eb6e, 0x0047ebc2, 0x0047ebed, 0x0047ebfa, 0x0047ec01, 0x0047ec08, 0x0047ec1b, 0x0047ec1f
