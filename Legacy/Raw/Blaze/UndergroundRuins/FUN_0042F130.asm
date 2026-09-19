Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0042f130  section .text  offset fichier 0x2e530  300 octets

  0042f130  56                    push      esi
  0042f131  8b742408              mov       esi,[esp+8]
  0042f135  85f6                  test      esi,esi
  0042f137  7505                  jne       short 0042F13Eh
  0042f139  be58e55d00            mov       esi,5DE558h

LAB_0042f13e:
  0042f13e  8b4648                mov       eax,[esi+48h]
  0042f141  83f80a                cmp       eax,0Ah
  0042f144  7c10                  jl        short 0042F156h
  0042f146  48                    dec       eax
  0042f147  83f809                cmp       eax,9
  0042f14a  894648                mov       [esi+48h],eax
  0042f14d  7f07                  jg        short 0042F156h
  0042f14f  c7464800000000        mov       dword [esi+48h],0

LAB_0042f156:
  0042f156  8b4648                mov       eax,[esi+48h]
  0042f159  85c0                  test      eax,eax
  0042f15b  7516                  jne       short 0042F173h
  0042f15d  668b44240c            mov       ax,[esp+0Ch]
  0042f162  c7464801000000        mov       dword [esi+48h],1
  0042f169  66c7461affff          mov       word [esi+1Ah],0FFFFh
  0042f16f  66894618              mov       [esi+18h],ax

LAB_0042f173:
  0042f173  8b4e48                mov       ecx,[esi+48h]
  0042f176  b802000000            mov       eax,2
  0042f17b  83f901                cmp       ecx,1
  0042f17e  7517                  jne       short 0042F197h
  0042f180  66ff461a              inc       word [esi+1Ah]
  0042f184  66837e1a0a            cmp       word [esi+1Ah],0Ah
  0042f189  7c0c                  jl        short 0042F197h
  0042f18b  66c7869a0000000a00    mov       word [esi+9Ah],0Ah
  0042f194  894648                mov       [esi+48h],eax

LAB_0042f197:
  0042f197  8b5648                mov       edx,[esi+48h]
  0042f19a  b903000000            mov       ecx,3
  0042f19f  3bd0                  cmp       edx,eax
  0042f1a1  7514                  jne       short 0042F1B7h
  0042f1a3  66ff8e9a000000        dec       word [esi+9Ah]
  0042f1aa  6683be9a00000000      cmp       word [esi+9Ah],0
  0042f1b2  7d03                  jge       short 0042F1B7h
  0042f1b4  894e48                mov       [esi+48h],ecx

LAB_0042f1b7:
  0042f1b7  394e48                cmp       [esi+48h],ecx
  0042f1ba  7551                  jne       short 0042F20Dh
  0042f1bc  66ff4e1a              dec       word [esi+1Ah]
  0042f1c0  66837e1a00            cmp       word [esi+1Ah],0
  0042f1c5  7f46                  jg        short 0042F20Dh
  0042f1c7  8b4e5c                mov       ecx,[esi+5Ch]
  0042f1ca  8b4644                mov       eax,[esi+44h]
  0042f1cd  81c900000080          or        ecx,80000000h
  0042f1d3  66c7461a0000          mov       word [esi+1Ah],0
  0042f1d9  85c0                  test      eax,eax
  0042f1db  894e5c                mov       [esi+5Ch],ecx
  0042f1de  742d                  je        short 0042F20Dh
  0042f1e0  8b5670                mov       edx,[esi+70h]
  0042f1e3  8bc8                  mov       ecx,eax
  0042f1e5  a1cc975e00            mov       eax,[5E97CCh]
  0042f1ea  51                    push      ecx
  0042f1eb  52                    push      edx
  0042f1ec  50                    push      eax
  0042f1ed  e8de4b0e00            call      00513DD0h
  0042f1f2  8b465c                mov       eax,[esi+5Ch]
  0042f1f5  83c40c                add       esp,0Ch
  0042f1f8  0d00000080            or        eax,80000000h
  0042f1fd  c7464400000000        mov       dword [esi+44h],0
  0042f204  89465c                mov       [esi+5Ch],eax
  0042f207  66b80100              mov       ax,1
  0042f20b  5e                    pop       esi
  0042f20c  c3                    ret

LAB_0042f20d:
  0042f20d  6633c0                xor       ax,ax
  0042f210  5e                    pop       esi
  0042f211  c3                    ret
  0042f212  90                    nop
  0042f213  90                    nop
  0042f214  90                    nop
  0042f215  90                    nop
  0042f216  90                    nop
  0042f217  90                    nop
  0042f218  90                    nop
  0042f219  90                    nop
  0042f21a  90                    nop
  0042f21b  90                    nop
  0042f21c  90                    nop
  0042f21d  90                    nop
  0042f21e  90                    nop
  0042f21f  90                    nop
  0042f220  8b442404              mov       eax,[esp+4]
  0042f224  53                    push      ebx
  0042f225  85c0                  test      eax,eax
  0042f227  7505                  jne       short 0042F22Eh
  0042f229  b858e55d00            mov       eax,5DE558h

LAB_0042f22e:
  0042f22e  8b485c                mov       ecx,[eax+5Ch]
  0042f231  8a5810                mov       bl,[eax+10h]
  0042f234  8bd1                  mov       edx,ecx
  0042f236  81e3ff000000          and       ebx,0FFh
  0042f23c  81e2ffffff3f          and       edx,3FFFFFFFh
  0042f242  33d3                  xor       edx,ebx
  0042f244  741b                  je        short 0042F261h
  0042f246  f7c100000080          test      ecx,80000000h
  0042f24c  7413                  je        short 0042F261h
  0042f24e  8a4810                mov       cl,[eax+10h]
  0042f251  0fbf5014              movsx     edx,word [eax+14h]
  0042f255  81e1ff000000          and       ecx,0FFh
  0042f25b  89                    (bad)
  (octet invalide - la plage demandee deborde probablement du code)
