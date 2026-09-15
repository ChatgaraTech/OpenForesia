Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00513ba0  section .text  offset fichier 0x112fa0  560 octets

  00513ba0  e8fb10ffff            call      00504CA0h
  00513ba5  e906000000            jmp       00513BB0h
  00513baa  90                    nop
  00513bab  90                    nop
  00513bac  90                    nop
  00513bad  90                    nop
  00513bae  90                    nop
  00513baf  90                    nop

LAB_00513bb0:
  00513bb0  83ec20                sub       esp,20h
  00513bb3  8d442400              lea       eax,[esp]
  00513bb7  8d4c2404              lea       ecx,[esp+4]
  00513bbb  8d542408              lea       edx,[esp+8]
  00513bbf  53                    push      ebx
  00513bc0  55                    push      ebp
  00513bc1  56                    push      esi
  00513bc2  57                    push      edi
  00513bc3  50                    push      eax
  00513bc4  51                    push      ecx
  00513bc5  52                    push      edx
  00513bc6  33ff                  xor       edi,edi
  00513bc8  33f6                  xor       esi,esi
  00513bca  33ed                  xor       ebp,ebp
  00513bcc  e80f660100            call      0052A1E0h
  00513bd1  33c0                  xor       eax,eax
  00513bd3  33d2                  xor       edx,edx
  00513bd5  a038e55d00            mov       al,[5DE538h]
  00513bda  33db                  xor       ebx,ebx
  00513bdc  83c40c                add       esp,0Ch
  00513bdf  c60598985e0001        mov       byte [5E9898h],1
  00513be6  8d0c80                lea       ecx,[eax+eax*4]
  00513be9  c60508995e0001        mov       byte [5E9908h],1
  00513bf0  c1e103                shl       ecx,3
  00513bf3  2bc8                  sub       ecx,eax
  00513bf5  6a07                  push      7
  00513bf7  8a148dc8935e00        mov       dl,[ecx*4+5E93C8h]
  00513bfe  6bd24c                imul      edx,4Ch
  00513c01  8a9a88915e00          mov       bl,[edx+5E9188h]
  00513c07  c1e304                shl       ebx,4
  00513c0a  81c3204a6000          add       ebx,604A20h
  00513c10  66897b02              mov       [ebx+2],di
  00513c14  e8b75e0100            call      00529AD0h
  00513c19  83c404                add       esp,4
  00513c1c  893d60995e00          mov       [5E9960h],edi

LAB_00513c22:
  00513c22  a160995e00            mov       eax,[5E9960h]
  00513c27  83e03f                and       eax,3Fh
  00513c2a  c1e002                shl       eax,2
  00513c2d  3d80000000            cmp       eax,80h
  00513c32  7e17                  jle       short 00513C4Bh
  00513c34  66f743020880          test      word [ebx+2],8008h
  00513c3a  b880000000            mov       eax,80h
  00513c3f  0f8555010000          jne       near 00513D9Ah
  00513c45  ff0d60995e00          dec       dword [5E9960h]

LAB_00513c4b:
  00513c4b  8b0dc4975e00          mov       ecx,[5E97C4h]
  00513c51  57                    push      edi
  00513c52  57                    push      edi
  00513c53  8b9194000000          mov       edx,[ecx+94h]
  00513c59  8b0a                  mov       ecx,[edx]
  00513c5b  8bd6                  mov       edx,esi
  00513c5d  894c2424              mov       [esp+24h],ecx
  00513c61  8bc8                  mov       ecx,eax
  00513c63  c1e108                shl       ecx,8
  00513c66  c1fa02                sar       edx,2
  00513c69  03c8                  add       ecx,eax
  00513c6b  66897c242e            mov       [esp+2Eh],di
  00513c70  f7da                  neg       edx
  00513c72  c1e108                shl       ecx,8
  00513c75  668954242c            mov       [esp+2Ch],dx
  00513c7a  03c8                  add       ecx,eax
  00513c7c  8b442418              mov       eax,[esp+18h]
  00513c80  8d542424              lea       edx,[esp+24h]
  00513c84  894c2428              mov       [esp+28h],ecx
  00513c88  8b4c241c              mov       ecx,[esp+1Ch]
  00513c8c  52                    push      edx
  00513c8d  50                    push      eax
  00513c8e  51                    push      ecx
  00513c8f  66897c2440            mov       [esp+40h],di
  00513c94  e857e10000            call      00521DF0h
  00513c99  83c414                add       esp,14h
  00513c9c  8d54241c              lea       edx,[esp+1Ch]
  00513ca0  89442410              mov       [esp+10h],eax
  00513ca4  52                    push      edx
  00513ca5  50                    push      eax
  00513ca6  8b44241c              mov       eax,[esp+1Ch]
  00513caa  50                    push      eax
  00513cab  e810bd0000            call      0051F9C0h
  00513cb0  8b542420              mov       edx,[esp+20h]
  00513cb4  83c40c                add       esp,0Ch
  00513cb7  66814424240001        add       word [esp+24h],100h
  00513cbe  8d4c241c              lea       ecx,[esp+1Ch]
  00513cc2  57                    push      edi
  00513cc3  57                    push      edi
  00513cc4  51                    push      ecx
  00513cc5  50                    push      eax
  00513cc6  52                    push      edx
  00513cc7  89442424              mov       [esp+24h],eax
  00513ccb  66c74424400100        mov       word [esp+40h],1
  00513cd2  e819e10000            call      00521DF0h
  00513cd7  8b542428              mov       edx,[esp+28h]
  00513cdb  83c414                add       esp,14h
  00513cde  8d4c241c              lea       ecx,[esp+1Ch]
  00513ce2  89442410              mov       [esp+10h],eax
  00513ce6  51                    push      ecx
  00513ce7  50                    push      eax
  00513ce8  52                    push      edx
  00513ce9  e8d2bc0000            call      0051F9C0h
  00513cee  83c40c                add       esp,0Ch
  00513cf1  89442410              mov       [esp+10h],eax
  00513cf5  8d442410              lea       eax,[esp+10h]
  00513cf9  8d4c2414              lea       ecx,[esp+14h]
  00513cfd  6a01                  push      1
  00513cff  6a03                  push      3
  00513d01  50                    push      eax
  00513d02  8d542424              lea       edx,[esp+24h]
  00513d06  51                    push      ecx
  00513d07  52                    push      edx
  00513d08  e873650100            call      0052A280h
  00513d0d  83c414                add       esp,14h
  00513d10  81fe00040000          cmp       esi,400h
  00513d16  7d06                  jge       short 00513D1Eh
  00513d18  46                    inc       esi
  00513d19  e904ffffff            jmp       00513C22h

LAB_00513d1e:
  00513d1e  45                    inc       ebp
  00513d1f  81fdbc020000          cmp       ebp,2BCh
  00513d25  7d73                  jge       short 00513D9Ah
  00513d27  81fd58020000          cmp       ebp,258h
  00513d2d  0f8ceffeffff          jl        near 00513C22h
  00513d33  a1c4975e00            mov       eax,[5E97C4h]
  00513d38  57                    push      edi
  00513d39  57                    push      edi
  00513d3a  8b8894000000          mov       ecx,[eax+94h]
  00513d40  b867666666            mov       eax,66666667h
  00513d45  8b5104                mov       edx,[ecx+4]
  00513d48  8d8da8fdffff          lea       ecx,[ebp-258h]
  00513d4e  89542424              mov       [esp+24h],edx
  00513d52  f7e9                  imul      ecx
  00513d54  c1fa03                sar       edx,3
  00513d57  8bc2                  mov       eax,edx
  00513d59  8d4c2424              lea       ecx,[esp+24h]
  00513d5d  c1e81f                shr       eax,1Fh
  00513d60  03d0                  add       edx,eax
  00513d62  8b44241c              mov       eax,[esp+1Ch]
  00513d66  6689542434            mov       [esp+34h],dx
  00513d6b  8b542418              mov       edx,[esp+18h]
  00513d6f  51                    push      ecx
  00513d70  52                    push      edx
  00513d71  50                    push      eax
  00513d72  e879e00000            call      00521DF0h
  00513d77  8b542428              mov       edx,[esp+28h]
  00513d7b  83c414                add       esp,14h
  00513d7e  8d4c241c              lea       ecx,[esp+1Ch]
  00513d82  89442410              mov       [esp+10h],eax
  00513d86  51                    push      ecx
  00513d87  50                    push      eax
  00513d88  52                    push      edx
  00513d89  e832bc0000            call      0051F9C0h
  00513d8e  83c40c                add       esp,0Ch
  00513d91  89442410              mov       [esp+10h],eax
  00513d95  e988feffff            jmp       00513C22h

LAB_00513d9a:
  00513d9a  6a01                  push      1
  00513d9c  8d442414              lea       eax,[esp+14h]
  00513da0  6a03                  push      3
  00513da2  8d4c241c              lea       ecx,[esp+1Ch]
  00513da6  50                    push      eax
  00513da7  8d542424              lea       edx,[esp+24h]
  00513dab  51                    push      ecx
  00513dac  52                    push      edx
  00513dad  893d10e55d00          mov       [5DE510h],edi
  00513db3  e8c8640100            call      0052A280h
  00513db8  83c414                add       esp,14h
  00513dbb  5f                    pop       edi
  00513dbc  5e                    pop       esi
  00513dbd  5d                    pop       ebp
  00513dbe  5b                    pop       ebx
  00513dbf  83c420                add       esp,20h
  00513dc2  c3                    ret
  00513dc3  90                    nop
  00513dc4  90                    nop
  00513dc5  90                    nop
  00513dc6  90                    nop
  00513dc7  90                    nop
  00513dc8  90                    nop
  00513dc9  90                    nop
  00513dca  90                    nop
  00513dcb  90                    nop
  00513dcc  90                    nop
  00513dcd  90                    nop
  00513dce  90                    nop
  00513dcf  90                    nop

5 cible(s) de saut interne : 0x00513bb0, 0x00513c22, 0x00513c4b, 0x00513d1e, 0x00513d9a
