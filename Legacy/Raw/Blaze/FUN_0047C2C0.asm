
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047c2c0  section .text  offset fichier 0x7b6c0  219 octets

  0047c2c0  53                    push      ebx
  0047c2c1  55                    push      ebp
  0047c2c2  56                    push      esi
  0047c2c3  8b742410              mov       esi,[esp+10h]
  0047c2c7  57                    push      edi
  0047c2c8  8b06                  mov       eax,[esi]
  0047c2ca  a900000003            test      eax,3000000h
  0047c2cf  7434                  je        short 0047C305h
  0047c2d1  0fbf4628              movsx     eax,word [esi+28h]
  0047c2d5  8bc8                  mov       ecx,eax
  0047c2d7  c1f902                sar       ecx,2
  0047c2da  f7d9                  neg       ecx
  0047c2dc  8d1c48                lea       ebx,[eax+ecx*2]
  0047c2df  33c0                  xor       eax,eax
  0047c2e1  8a4638                mov       al,[esi+38h]
  0047c2e4  89442414              mov       [esp+14h],eax
  0047c2e8  8a4c2414              mov       cl,[esp+14h]
  0047c2ec  c1f802                sar       eax,2
  0047c2ef  2ac8                  sub       cl,al
  0047c2f1  81fb00010000          cmp       ebx,100h
  0047c2f7  884c2414              mov       [esp+14h],cl
  0047c2fb  7d78                  jge       short 0047C375h
  0047c2fd  c70600000000          mov       dword [esi],0
  0047c303  eb70                  jmp       short 0047C375h

LAB_0047c305:
  0047c305  8b7e6c                mov       edi,[esi+6Ch]
  0047c308  668b4e10              mov       cx,[esi+10h]
  0047c30c  8b17                  mov       edx,[edi]
  0047c30e  81e200000003          and       edx,3000000h
  0047c314  0bd0                  or        edx,eax
  0047c316  6685c9                test      cx,cx
  0047c319  8916                  mov       [esi],edx
  0047c31b  7507                  jne       short 0047C324h
  0047c31d  c7462800000000        mov       dword [esi+28h],0

LAB_0047c324:
  0047c324  0fbf4628              movsx     eax,word [esi+28h]
  0047c328  ba00180000            mov       edx,1800h
  0047c32d  2bd0                  sub       edx,eax
  0047c32f  c1fa02                sar       edx,2
  0047c332  0fbfc9                movsx     ecx,cx
  0047c335  8d1c50                lea       ebx,[eax+edx*2]
  0047c338  33c0                  xor       eax,eax
  0047c33a  8a4638                mov       al,[esi+38h]
  0047c33d  c1e109                shl       ecx,9
  0047c340  8be8                  mov       ebp,eax
  0047c342  51                    push      ecx
  0047c343  896c2418              mov       [esp+18h],ebp
  0047c347  e8d0960c00            call      00545A1Ch
  0047c34c  8a4c2414              mov       cl,[esp+14h]
  0047c350  8b571c                mov       edx,[edi+1Ch]
  0047c353  c1f807                sar       eax,7
  0047c356  2bc5                  sub       eax,ebp
  0047c358  89561c                mov       [esi+1Ch],edx
  0047c35b  0580000000            add       eax,80h
  0047c360  c1f802                sar       eax,2
  0047c363  02c8                  add       cl,al
  0047c365  8b4720                mov       eax,[edi+20h]
  0047c368  884c2414              mov       [esp+14h],cl
  0047c36c  894620                mov       [esi+20h],eax
  0047c36f  8b4f24                mov       ecx,[edi+24h]
  0047c372  894e24                mov       [esi+24h],ecx

LAB_0047c375:
  0047c375  8b442414              mov       eax,[esp+14h]
  0047c379  8bd3                  mov       edx,ebx
  0047c37b  8bc8                  mov       ecx,eax
  0047c37d  5f                    pop       edi
  0047c37e  c1e108                shl       ecx,8
  0047c381  0bc8                  or        ecx,eax
  0047c383  c1e210                shl       edx,10h
  0047c386  c1e108                shl       ecx,8
  0047c389  0bd3                  or        edx,ebx
  0047c38b  0bc8                  or        ecx,eax
  0047c38d  66ff4610              inc       word [esi+10h]
  0047c391  895628                mov       [esi+28h],edx
  0047c394  894e38                mov       [esi+38h],ecx
  0047c397  5e                    pop       esi
  0047c398  5d                    pop       ebp
  0047c399  5b                    pop       ebx
  0047c39a  c3                    ret

3 cible(s) de saut interne : 0x0047c305, 0x0047c324, 0x0047c375
