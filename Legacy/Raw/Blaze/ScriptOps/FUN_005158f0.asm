
Legacy/Binaries/Blaze.exe  base 0x00400000
0x005158f0  section .text  offset fichier 0x114cf0  96 octets

  005158f0  8b542408              mov       edx,[esp+8]
  005158f4  56                    push      esi
  005158f5  85d2                  test      edx,edx
  005158f7  744c                  je        short 00515945h
  005158f9  8b02                  mov       eax,[edx]
  005158fb  8bc8                  mov       ecx,eax
  005158fd  81e10000fc00          and       ecx,0FC0000h
  00515903  81f900008000          cmp       ecx,800000h
  00515909  753a                  jne       short 00515945h
  0051590b  a900000050            test      eax,50000000h
  00515910  7507                  jne       short 00515919h
  00515912  66837a1200            cmp       word [edx+12h],0
  00515917  742c                  je        short 00515945h

LAB_00515919:
  00515919  8b0d20905e00          mov       ecx,[5E9020h]
  0051591f  b8c0935e00            mov       eax,5E93C0h
  00515924  85c9                  test      ecx,ecx
  00515926  741d                  je        short 00515945h

LAB_00515928:
  00515928  f70000000150          test      dword [eax],50010000h
  0051592e  7507                  jne       short 00515937h
  00515930  6683781200            cmp       word [eax+12h],0
  00515935  740c                  je        short 00515943h

LAB_00515937:
  00515937  49                    dec       ecx
  00515938  059c000000            add       eax,9Ch
  0051593d  85c9                  test      ecx,ecx
  0051593f  75e7                  jne       short 00515928h
  00515941  eb02                  jmp       short 00515945h

LAB_00515943:
  00515943  8bd0                  mov       edx,eax

LAB_00515945:
  00515945  8b742410              mov       esi,[esp+10h]
  00515949  a1cc975e00            mov       eax,[5E97CCh]
  0051594e  6a00                  push      0

5 cible(s) de saut interne : 0x00515919, 0x00515928, 0x00515937, 0x00515943, 0x00515945
