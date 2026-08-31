
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047add0  section .text  offset fichier 0x7a1d0  161 octets

  0047add0  53                    push      ebx
  0047add1  56                    push      esi
  0047add2  8b74240c              mov       esi,[esp+0Ch]
  0047add6  57                    push      edi
  0047add7  0fbf5e32              movsx     ebx,word [esi+32h]
  0047addb  804e3606              or        byte [esi+36h],6
  0047addf  53                    push      ebx
  0047ade0  8d7e1c                lea       edi,[esi+1Ch]
  0047ade3  e834ac0c00            call      00545A1Ch
  0047ade8  8b0f                  mov       ecx,[edi]
  0047adea  8d0480                lea       eax,[eax+eax*4]
  0047aded  c1e004                shl       eax,4
  0047adf0  03c8                  add       ecx,eax
  0047adf2  53                    push      ebx
  0047adf3  890f                  mov       [edi],ecx
  0047adf5  e81cac0c00            call      00545A16h
  0047adfa  8b5674                mov       edx,[esi+74h]
  0047adfd  8d0c80                lea       ecx,[eax+eax*4]
  0047ae00  8b4624                mov       eax,[esi+24h]
  0047ae03  52                    push      edx
  0047ae04  c1e104                shl       ecx,4
  0047ae07  03c1                  add       eax,ecx
  0047ae09  894624                mov       [esi+24h],eax
  0047ae0c  e8afdd0b00            call      00538BC0h
  0047ae11  8b5620                mov       edx,[esi+20h]
  0047ae14  24fe                  and       al,0FEh
  0047ae16  c1e00b                shl       eax,0Bh
  0047ae19  03d0                  add       edx,eax
  0047ae1b  668146320004          add       word [esi+32h],400h
  0047ae21  83c404                add       esp,4
  0047ae24  33c0                  xor       eax,eax
  0047ae26  8a460b                mov       al,[esi+0Bh]
  0047ae29  895620                mov       [esi+20h],edx
  0047ae2c  6aff                  push      0FFFFFFFFh
  0047ae2e  50                    push      eax
  0047ae2f  6a64                  push      64h
  0047ae31  56                    push      esi
  0047ae32  c7462840064006        mov       dword [esi+28h],6400640h
  0047ae39  66c7462c4006          mov       word [esi+2Ch],640h
  0047ae3f  c74638ffffff00        mov       dword [esi+38h],0FFFFFFh
  0047ae46  c6460439              mov       byte [esi+4],39h
  0047ae4a  e891fcffff            call      0047AAE0h
  0047ae4f  83c410                add       esp,10h
  0047ae52  57                    push      edi
  0047ae53  6800120000            push      1200h
  0047ae58  68fd060000            push      6FDh
  0047ae5d  6a1f                  push      1Fh
  0047ae5f  e8bc2c0c00            call      0053DB20h
  0047ae64  83c410                add       esp,10h
  0047ae67  c70600000000          mov       dword [esi],0
  0047ae6d  5f                    pop       edi
  0047ae6e  5e                    pop       esi
  0047ae6f  5b                    pop       ebx
  0047ae70  c3                    ret
