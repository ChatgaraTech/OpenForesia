Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047a950  section .text  offset fichier 0x79d50  121 octets

  0047a950  a160995e00            mov       eax,[5E9960h]
  0047a955  56                    push      esi
  0047a956  8b742408              mov       esi,[esp+8]
  0047a95a  57                    push      edi
  0047a95b  c1e009                shl       eax,9
  0047a95e  8b7e6c                mov       edi,[esi+6Ch]
  0047a961  50                    push      eax
  0047a962  e8b5b00c00            call      00545A1Ch
  0047a967  d1f8                  sar       eax,1
  0047a969  0500100000            add       eax,1000h
  0047a96e  c74638ffffff00        mov       dword [esi+38h],0FFFFFFh
  0047a975  8bc8                  mov       ecx,eax
  0047a977  25ffff0000            and       eax,0FFFFh
  0047a97c  c1e110                shl       ecx,10h
  0047a97f  0bc8                  or        ecx,eax
  0047a981  c7464000000000        mov       dword [esi+40h],0
  0047a988  894e28                mov       [esi+28h],ecx
  0047a98b  66c7462ef6ff          mov       word [esi+2Eh],0FFF6h
  0047a991  8b571c                mov       edx,[edi+1Ch]
  0047a994  57                    push      edi
  0047a995  89561c                mov       [esi+1Ch],edx
  0047a998  e823e20b00            call      00538BC0h
  0047a99d  8b5720                mov       edx,[edi+20h]
  0047a9a0  83c404                add       esp,4
  0047a9a3  c1e00c                shl       eax,0Ch
  0047a9a6  03c2                  add       eax,edx
  0047a9a8  894620                mov       [esi+20h],eax
  0047a9ab  8b4724                mov       eax,[edi+24h]
  0047a9ae  894624                mov       [esi+24h],eax
  0047a9b1  668b4610              mov       ax,[esi+10h]
  0047a9b5  663d0100              cmp       ax,1
  0047a9b9  7c06                  jl        short 0047A9C1h
  0047a9bb  c70600000000          mov       dword [esi],0

LAB_0047a9c1:
  0047a9c1  40                    inc       eax
  0047a9c2  5f                    pop       edi
  0047a9c3  66894610              mov       [esi+10h],ax
  0047a9c7  5e                    pop       esi
  0047a9c8  c3                    ret

1 cible(s) de saut interne : 0x0047a9c1
