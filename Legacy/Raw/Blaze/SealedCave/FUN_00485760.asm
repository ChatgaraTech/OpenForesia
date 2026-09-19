Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00485760  section .text  offset fichier 0x84b60  240 octets

  00485760  56                    push      esi
  00485761  8b742408              mov       esi,[esp+8]
  00485765  57                    push      edi
  00485766  0fbf7e32              movsx     edi,word [esi+32h]
  0048576a  8b466c                mov       eax,[esi+6Ch]
  0048576d  57                    push      edi
  0048576e  894674                mov       [esi+74h],eax
  00485771  c646041e              mov       byte [esi+4],1Eh
  00485775  66c7462ee0ff          mov       word [esi+2Eh],0FFE0h
  0048577b  66c746360400          mov       word [esi+36h],4
  00485781  c7463880808000        mov       dword [esi+38h],808080h
  00485788  66c7462c0060          mov       word [esi+2Ch],6000h
  0048578e  66c7869a0000000000    mov       word [esi+9Ah],0
  00485797  e880020c00            call      00545A1Ch
  0048579c  c1e003                shl       eax,3
  0048579f  57                    push      edi
  004857a0  894650                mov       [esi+50h],eax
  004857a3  c7465400000000        mov       dword [esi+54h],0
  004857aa  e867020c00            call      00545A16h
  004857af  c1e003                shl       eax,3
  004857b2  894658                mov       [esi+58h],eax
  004857b5  c7462800300000        mov       dword [esi+28h],3000h
  004857bc  66c746120060          mov       word [esi+12h],6000h
  004857c2  8b0dcc975e00          mov       ecx,[5E97CCh]
  004857c8  6a02                  push      2
  004857ca  6a01                  push      1
  004857cc  8b91d0000000          mov       edx,[ecx+0D0h]
  004857d2  33c9                  xor       ecx,ecx
  004857d4  8b02                  mov       eax,[edx]
  004857d6  8a4802                mov       cl,[eax+2]
  004857d9  81c1c0000000          add       ecx,0C0h
  004857df  51                    push      ecx
  004857e0  56                    push      esi
  004857e1  e8fa52ffff            call      0047AAE0h
  004857e6  83c410                add       esp,10h
  004857e9  57                    push      edi
  004857ea  e82d020c00            call      00545A1Ch
  004857ef  c1e005                shl       eax,5
  004857f2  57                    push      edi
  004857f3  894650                mov       [esi+50h],eax
  004857f6  c7465400000000        mov       dword [esi+54h],0
  004857fd  e814020c00            call      00545A16h
  00485802  c1e005                shl       eax,5
  00485805  894658                mov       [esi+58h],eax
  00485808  b800400000            mov       eax,4000h
  0048580d  894628                mov       [esi+28h],eax
  00485810  66894612              mov       [esi+12h],ax
  00485814  8b15cc975e00          mov       edx,[5E97CCh]
  0048581a  6a02                  push      2
  0048581c  6a01                  push      1
  0048581e  8b82d0000000          mov       eax,[edx+0D0h]
  00485824  33d2                  xor       edx,edx
  00485826  8b08                  mov       ecx,[eax]
  00485828  8a5102                mov       dl,[ecx+2]
  0048582b  81c2c0000000          add       edx,0C0h
  00485831  52                    push      edx
  00485832  56                    push      esi
  00485833  e8a852ffff            call      0047AAE0h
  00485838  83c410                add       esp,10h
  0048583b  c70600000000          mov       dword [esi],0
  00485841  5f                    pop       edi
  00485842  5e                    pop       esi
  00485843  c3                    ret
  00485844  90                    nop
  00485845  90                    nop
  00485846  90                    nop
  00485847  90                    nop
  00485848  90                    nop
  00485849  90                    nop
  0048584a  90                    nop
  0048584b  90                    nop
  0048584c  90                    nop
  0048584d  90                    nop
  0048584e  90                    nop
  0048584f  90                    nop
