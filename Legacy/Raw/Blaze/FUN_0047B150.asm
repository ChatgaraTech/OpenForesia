
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047b150  section .text  offset fichier 0x7a550  492 octets

  0047b150  83ec18                sub       esp,18h
  0047b153  53                    push      ebx
  0047b154  55                    push      ebp
  0047b155  56                    push      esi
  0047b156  8b742428              mov       esi,[esp+28h]
  0047b15a  33ed                  xor       ebp,ebp
  0047b15c  b864000000            mov       eax,64h
  0047b161  66396e10              cmp       [esi+10h],bp
  0047b165  57                    push      edi
  0047b166  66896c2410            mov       [esp+10h],bp
  0047b16b  66c7442412ceff        mov       word [esp+12h],0FFCEh
  0047b172  66c74424143200        mov       word [esp+14h],32h
  0047b179  6689442416            mov       [esp+16h],ax
  0047b17e  66896c2418            mov       [esp+18h],bp
  0047b183  668944241a            mov       [esp+1Ah],ax
  0047b188  7537                  jne       short 0047B1C1h
  0047b18a  8b3e                  mov       edi,[esi]
  0047b18c  33c0                  xor       eax,eax
  0047b18e  8a460b                mov       al,[esi+0Bh]
  0047b191  6aff                  push      0FFFFFFFFh
  0047b193  50                    push      eax
  0047b194  6a49                  push      49h
  0047b196  8d4e1c                lea       ecx,[esi+1Ch]
  0047b199  68601a5500            push      551A60h
  0047b19e  81cf80000000          or        edi,80h
  0047b1a4  51                    push      ecx
  0047b1a5  56                    push      esi
  0047b1a6  893e                  mov       [esi],edi
  0047b1a8  897678                mov       [esi+78h],esi
  0047b1ab  c7462800100010        mov       dword [esi+28h],10001000h
  0047b1b2  c74638ffffff00        mov       dword [esi+38h],0FFFFFFh
  0047b1b9  e882faffff            call      0047AC40h
  0047b1be  83c418                add       esp,18h

LAB_0047b1c1:
  0047b1c1  0fbf7e32              movsx     edi,word [esi+32h]
  0047b1c5  57                    push      edi
  0047b1c6  8d5e1c                lea       ebx,[esi+1Ch]
  0047b1c9  e84ea80c00            call      00545A1Ch
  0047b1ce  8b0b                  mov       ecx,[ebx]
  0047b1d0  8d1480                lea       edx,[eax+eax*4]
  0047b1d3  c1e203                shl       edx,3
  0047b1d6  03ca                  add       ecx,edx
  0047b1d8  57                    push      edi
  0047b1d9  890b                  mov       [ebx],ecx
  0047b1db  e836a80c00            call      00545A16h
  0047b1e0  8b5624                mov       edx,[esi+24h]
  0047b1e3  8d0480                lea       eax,[eax+eax*4]
  0047b1e6  c1e003                shl       eax,3
  0047b1e9  03d0                  add       edx,eax
  0047b1eb  895624                mov       [esi+24h],edx
  0047b1ee  e84dbf0c00            call      00547140h
  0047b1f3  8b0b                  mov       ecx,[ebx]
  0047b1f5  83e03f                and       eax,3Fh
  0047b1f8  83e820                sub       eax,20h
  0047b1fb  c1e00c                shl       eax,0Ch
  0047b1fe  03c1                  add       eax,ecx
  0047b200  8944241c              mov       [esp+1Ch],eax
  0047b204  e837bf0c00            call      00547140h
  0047b209  8b7e20                mov       edi,[esi+20h]
  0047b20c  83e03f                and       eax,3Fh
  0047b20f  83e820                sub       eax,20h
  0047b212  c1e00c                shl       eax,0Ch
  0047b215  03c7                  add       eax,edi
  0047b217  89442420              mov       [esp+20h],eax
  0047b21b  e820bf0c00            call      00547140h
  0047b220  8b5624                mov       edx,[esi+24h]
  0047b223  83e03f                and       eax,3Fh
  0047b226  83e820                sub       eax,20h
  0047b229  33c9                  xor       ecx,ecx
  0047b22b  8a4e0b                mov       cl,[esi+0Bh]
  0047b22e  6aff                  push      0FFFFFFFFh
  0047b230  c1e00c                shl       eax,0Ch
  0047b233  03c2                  add       eax,edx
  0047b235  51                    push      ecx
  0047b236  6a48                  push      48h
  0047b238  8d542428              lea       edx,[esp+28h]
  0047b23c  68a0195500            push      5519A0h
  0047b241  52                    push      edx
  0047b242  56                    push      esi
  0047b243  8944243c              mov       [esp+3Ch],eax
  0047b247  e8f4f9ffff            call      0047AC40h
  0047b24c  8b7e20                mov       edi,[esi+20h]
  0047b24f  83c418                add       esp,18h
  0047b252  56                    push      esi
  0047b253  e8e8860b00            call      00533940h
  0047b258  8b06                  mov       eax,[esi]
  0047b25a  83c404                add       esp,4
  0047b25d  a900000040            test      eax,40000000h
  0047b262  897e20                mov       [esi+20h],edi
  0047b265  0f85c7000000          jne       near 0047B332h
  0047b26b  f7869000000002880000  test      dword [esi+90h],8802h
  0047b275  0f85b7000000          jne       near 0047B332h
  0047b27b  8d442410              lea       eax,[esp+10h]
  0047b27f  50                    push      eax
  0047b280  56                    push      esi
  0047b281  e81a9affff            call      00474CA0h
  0047b286  8bf8                  mov       edi,eax
  0047b288  83c408                add       esp,8
  0047b28b  3bfd                  cmp       edi,ebp
  0047b28d  0f8486000000          je        near 0047B319h
  0047b293  8b4674                mov       eax,[esi+74h]
  0047b296  33c9                  xor       ecx,ecx
  0047b298  8a4e06                mov       cl,[esi+6]
  0047b29b  33d2                  xor       edx,edx
  0047b29d  8a5607                mov       dl,[esi+7]
  0047b2a0  55                    push      ebp
  0047b2a1  51                    push      ecx
  0047b2a2  52                    push      edx
  0047b2a3  57                    push      edi
  0047b2a4  50                    push      eax
  0047b2a5  e806f00b00            call      0053A2B0h
  0047b2aa  83c414                add       esp,14h
  0047b2ad  33c9                  xor       ecx,ecx
  0047b2af  8a4e0b                mov       cl,[esi+0Bh]
  0047b2b2  897e78                mov       [esi+78h],edi
  0047b2b5  6aff                  push      0FFFFFFFFh
  0047b2b7  51                    push      ecx
  0047b2b8  6a49                  push      49h
  0047b2ba  68601a5500            push      551A60h
  0047b2bf  53                    push      ebx
  0047b2c0  56                    push      esi
  0047b2c1  e87af9ffff            call      0047AC40h
  0047b2c6  0fbf6e32              movsx     ebp,word [esi+32h]
  0047b2ca  83c418                add       esp,18h
  0047b2cd  81ed00040000          sub       ebp,400h
  0047b2d3  55                    push      ebp
  0047b2d4  e843a70c00            call      00545A1Ch
  0047b2d9  8b4f50                mov       ecx,[edi+50h]
  0047b2dc  55                    push      ebp
  0047b2dd  c1e004                shl       eax,4
  0047b2e0  03c8                  add       ecx,eax
  0047b2e2  894f50                mov       [edi+50h],ecx
  0047b2e5  e82ca70c00            call      00545A16h
  0047b2ea  8b4f58                mov       ecx,[edi+58h]
  0047b2ed  53                    push      ebx
  0047b2ee  c1e004                shl       eax,4
  0047b2f1  03c8                  add       ecx,eax
  0047b2f3  8b07                  mov       eax,[edi]
  0047b2f5  6800120000            push      1200h
  0047b2fa  0c40                  or        al,40h
  0047b2fc  6800080000            push      800h
  0047b301  6a14                  push      14h
  0047b303  894f58                mov       [edi+58h],ecx
  0047b306  66c787960000001000    mov       word [edi+96h],10h
  0047b30f  8907                  mov       [edi],eax
  0047b311  e80a280c00            call      0053DB20h
  0047b316  83c410                add       esp,10h

LAB_0047b319:
  0047b319  66ff4610              inc       word [esi+10h]
  0047b31d  66837e100c            cmp       word [esi+10h],0Ch
  0047b322  7c10                  jl        short 0047B334h
  0047b324  c70600000000          mov       dword [esi],0
  0047b32a  5f                    pop       edi
  0047b32b  5e                    pop       esi
  0047b32c  5d                    pop       ebp
  0047b32d  5b                    pop       ebx
  0047b32e  83c418                add       esp,18h
  0047b331  c3                    ret

LAB_0047b332:
  0047b332  892e                  mov       [esi],ebp

LAB_0047b334:
  0047b334  5f                    pop       edi
  0047b335  5e                    pop       esi
  0047b336  5d                    pop       ebp
  0047b337  5b                    pop       ebx
  0047b338  83c418                add       esp,18h
  0047b33b  c3                    ret

4 cible(s) de saut interne : 0x0047b1c1, 0x0047b319, 0x0047b332, 0x0047b334
