
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047b7a0  section .text  offset fichier 0x7aba0  176 octets

  0047b7a0  56                    push      esi
  0047b7a1  8b742408              mov       esi,[esp+8]
  0047b7a5  57                    push      edi
  0047b7a6  668b4e10              mov       cx,[esi+10h]
  0047b7aa  6683f910              cmp       cx,10h
  0047b7ae  7d7f                  jge       short 0047B82Fh
  0047b7b0  8b4674                mov       eax,[esi+74h]
  0047b7b3  6685c9                test      cx,cx
  0047b7b6  752d                  jne       short 0047B7E5h
  0047b7b8  8b08                  mov       ecx,[eax]
  0047b7ba  80c980                or        cl,80h
  0047b7bd  8908                  mov       [eax],ecx
  0047b7bf  668b4e36              mov       cx,[esi+36h]
  0047b7c3  83e1fb                and       ecx,0FFFFFFFBh
  0047b7c6  c7463800000001        mov       dword [esi+38h],1000000h
  0047b7cd  83c902                or        ecx,2
  0047b7d0  c6464000              mov       byte [esi+40h],0
  0047b7d4  66894e36              mov       [esi+36h],cx
  0047b7d8  b904000000            mov       ecx,4
  0047b7dd  66894e14              mov       [esi+14h],cx
  0047b7e1  66894e16              mov       [esi+16h],cx

LAB_0047b7e5:
  0047b7e5  8b501c                mov       edx,[eax+1Ch]
  0047b7e8  8d7e1c                lea       edi,[esi+1Ch]
  0047b7eb  8917                  mov       [edi],edx
  0047b7ed  8b4824                mov       ecx,[eax+24h]
  0047b7f0  894e24                mov       [esi+24h],ecx
  0047b7f3  8b5060                mov       edx,[eax+60h]
  0047b7f6  8a4610                mov       al,[esi+10h]
  0047b7f9  895620                mov       [esi+20h],edx
  0047b7fc  a803                  test      al,3
  0047b7fe  7528                  jne       short 0047B828h
  0047b800  33c0                  xor       eax,eax
  0047b802  6aff                  push      0FFFFFFFFh
  0047b804  8a460b                mov       al,[esi+0Bh]
  0047b807  50                    push      eax
  0047b808  6a69                  push      69h
  0047b80a  56                    push      esi
  0047b80b  e8d0f2ffff            call      0047AAE0h
  0047b810  83c410                add       esp,10h
  0047b813  57                    push      edi
  0047b814  6800120000            push      1200h
  0047b819  6829030000            push      329h
  0047b81e  6a16                  push      16h
  0047b820  e8fb220c00            call      0053DB20h
  0047b825  83c410                add       esp,10h

LAB_0047b828:
  0047b828  66ff4610              inc       word [esi+10h]
  0047b82c  5f                    pop       edi
  0047b82d  5e                    pop       esi
  0047b82e  c3                    ret

LAB_0047b82f:
  0047b82f  66837e1400            cmp       word [esi+14h],0
  0047b834  750a                  jne       short 0047B840h
  0047b836  8b4674                mov       eax,[esi+74h]
  0047b839  8b08                  mov       ecx,[eax]
  0047b83b  80e17f                and       cl,7Fh
  0047b83e  8908                  mov       [eax],ecx

LAB_0047b840:
  0047b840  66837e1600            cmp       word [esi+16h],0
  0047b845  7506                  jne       short 0047B84Dh
  0047b847  c70600000000          mov       dword [esi],0

LAB_0047b84d:
  0047b84d  5f                    pop       edi
  0047b84e  5e                    pop       esi
  0047b84f  c3                    ret

5 cible(s) de saut interne : 0x0047b7e5, 0x0047b828, 0x0047b82f, 0x0047b840, 0x0047b84d
