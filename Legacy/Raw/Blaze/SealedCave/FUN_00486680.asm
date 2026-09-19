Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00486680  section .text  offset fichier 0x85a80  160 octets

  00486680  56                    push      esi
  00486681  8b742408              mov       esi,[esp+8]
  00486685  66837e1000            cmp       word [esi+10h],0
  0048668a  0f8584000000          jne       near 00486714h
  00486690  8b466c                mov       eax,[esi+6Ch]
  00486693  57                    push      edi
  00486694  0fbf7e32              movsx     edi,word [esi+32h]
  00486698  53                    push      ebx
  00486699  bb04000000            mov       ebx,4
  0048669e  894674                mov       [esi+74h],eax
  004866a1  c6460438              mov       byte [esi+4],38h
  004866a5  c7463800000000        mov       dword [esi+38h],0
  004866ac  66895e36              mov       [esi+36h],bx
  004866b0  66c7462ef0ff          mov       word [esi+2Eh],0FFF0h
  004866b6  c7462800100002        mov       dword [esi+28h],2001000h
  004866bd  66c7462c0000          mov       word [esi+2Ch],0
  004866c3  66c746120800          mov       word [esi+12h],8
  004866c9  81ef80010000          sub       edi,180h

LAB_004866cf:
  004866cf  e86c0a0c00            call      00547140h
  004866d4  83e03f                and       eax,3Fh
  004866d7  6a04                  push      4
  004866d9  6a00                  push      0
  004866db  8d8c38e0030000        lea       ecx,[eax+edi+3E0h]
  004866e2  66894e32              mov       [esi+32h],cx
  004866e6  8b15cc975e00          mov       edx,[5E97CCh]
  004866ec  8b82d0000000          mov       eax,[edx+0D0h]
  004866f2  33d2                  xor       edx,edx
  004866f4  8b08                  mov       ecx,[eax]
  004866f6  8a5107                mov       dl,[ecx+7]
  004866f9  81c2c0000000          add       edx,0C0h
  004866ff  52                    push      edx
  00486700  56                    push      esi
  00486701  e8da43ffff            call      0047AAE0h
  00486706  83c410                add       esp,10h
  00486709  81c700010000          add       edi,100h
  0048670f  4b                    dec       ebx
  00486710  75bd                  jne       short 004866CFh
  00486712  5b                    pop       ebx
  00486713  5f                    pop       edi

LAB_00486714:
  00486714  c70600000000          mov       dword [esi],0
  0048671a  5e                    pop       esi
  0048671b  c3                    ret
  0048671c  90                    nop
  0048671d  90                    nop
  0048671e  90                    nop
  0048671f  90                    nop

2 cible(s) de saut interne : 0x004866cf, 0x00486714
