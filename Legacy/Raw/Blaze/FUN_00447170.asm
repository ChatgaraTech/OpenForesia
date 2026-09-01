Legacy/Binaries/Blaze.exe  base 0x00400000
0x00447170  section .text  offset fichier 0x46570  272 octets

  00447170  83ec08                sub       esp,8
  00447173  53                    push      ebx
  00447174  56                    push      esi
  00447175  8b742414              mov       esi,[esp+14h]
  00447179  57                    push      edi
  0044717a  85f6                  test      esi,esi
  0044717c  0f84f7000000          je        near 00447279h
  00447182  66837e1001            cmp       word [esi+10h],1
  00447187  752c                  jne       short 004471B5h
  00447189  66ff4612              inc       word [esi+12h]
  0044718d  66837e1204            cmp       word [esi+12h],4
  00447192  7c21                  jl        short 004471B5h
  00447194  8b06                  mov       eax,[esi]
  00447196  25ffffff7f            and       eax,7FFFFFFFh
  0044719b  8906                  mov       [esi],eax
  0044719d  8d461c                lea       eax,[esi+1Ch]
  004471a0  50                    push      eax
  004471a1  6800120000            push      1200h
  004471a6  68b20c0000            push      0CB2h
  004471ab  6a0d                  push      0Dh
  004471ad  e86e690f00            call      0053DB20h
  004471b2  83c410                add       esp,10h

LAB_004471b5:
  004471b5  66837e1000            cmp       word [esi+10h],0
  004471ba  0f85b9000000          jne       near 00447279h
  004471c0  8b5e70                mov       ebx,[esi+70h]
  004471c3  85db                  test      ebx,ebx
  004471c5  7505                  jne       short 004471CCh
  004471c7  bb58e55d00            mov       ebx,5DE558h

LAB_004471cc:
  004471cc  8d4c2418              lea       ecx,[esp+18h]
  004471d0  8d54240c              lea       edx,[esp+0Ch]
  004471d4  51                    push      ecx
  004471d5  8d442414              lea       eax,[esp+14h]
  004471d9  52                    push      edx
  004471da  50                    push      eax
  004471db  53                    push      ebx
  004471dc  e89f170f00            call      00538980h
  004471e1  8b4b1c                mov       ecx,[ebx+1Ch]
  004471e4  8b542428              mov       edx,[esp+28h]
  004471e8  8d7e1c                lea       edi,[esi+1Ch]
  004471eb  83c410                add       esp,10h
  004471ee  890f                  mov       [edi],ecx
  004471f0  8b4b20                mov       ecx,[ebx+20h]
  004471f3  0fbf421e              movsx     eax,word [edx+1Eh]
  004471f7  03c8                  add       ecx,eax
  004471f9  894e20                mov       [esi+20h],ecx
  004471fc  8b5324                mov       edx,[ebx+24h]
  004471ff  895624                mov       [esi+24h],edx
  00447202  66c74614b400          mov       word [esi+14h],0B4h
  00447208  bb05000000            mov       ebx,5

LAB_0044720d:
  0044720d  e82eff0f00            call      00547140h
  00447212  99                    cdq
  00447213  33c2                  xor       eax,edx
  00447215  2bc2                  sub       eax,edx
  00447217  25ff0f0000            and       eax,0FFFh
  0044721c  33c2                  xor       eax,edx
  0044721e  2bc2                  sub       eax,edx
  00447220  66894632              mov       [esi+32h],ax
  00447224  e817ff0f00            call      00547140h
  00447229  99                    cdq
  0044722a  33c2                  xor       eax,edx
  0044722c  6aff                  push      0FFFFFFFFh
  0044722e  2bc2                  sub       eax,edx
  00447230  6a00                  push      0
  00447232  25ff030000            and       eax,3FFh
  00447237  6898000000            push      98h
  0044723c  33c2                  xor       eax,edx
  0044723e  6a00                  push      0
  00447240  2bc2                  sub       eax,edx
  00447242  57                    push      edi
  00447243  0500020000            add       eax,200h
  00447248  56                    push      esi
  00447249  66894616              mov       [esi+16h],ax
  0044724d  e8ee390300            call      0047AC40h
  00447252  83c418                add       esp,18h
  00447255  4b                    dec       ebx
  00447256  75b5                  jne       short 0044720Dh
  00447258  57                    push      edi
  00447259  6800120000            push      1200h
  0044725e  6864190000            push      1964h
  00447263  6a0d                  push      0Dh
  00447265  66c746100100          mov       word [esi+10h],1
  0044726b  66c746120000          mov       word [esi+12h],0
  00447271  e8aa680f00            call      0053DB20h
  00447276  83c410                add       esp,10h

LAB_00447279:
  00447279  5f                    pop       edi
  0044727a  5e                    pop       esi
  0044727b  5b                    pop       ebx
  0044727c  83c408                add       esp,8
  0044727f  c3                    ret

4 cible(s) de saut interne : 0x004471b5, 0x004471cc, 0x0044720d, 0x00447279
