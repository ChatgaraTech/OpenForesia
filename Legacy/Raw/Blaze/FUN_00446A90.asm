Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00446a90  section .text  offset fichier 0x45e90  507 octets

  00446a90  83ec08                sub       esp,8
  00446a93  53                    push      ebx
  00446a94  56                    push      esi
  00446a95  8b742414              mov       esi,[esp+14h]
  00446a99  57                    push      edi
  00446a9a  85f6                  test      esi,esi
  00446a9c  0f84e2010000          je        near 00446C84h
  00446aa2  8b466c                mov       eax,[esi+6Ch]
  00446aa5  85c0                  test      eax,eax
  00446aa7  7505                  jne       short 00446AAEh
  00446aa9  b858e55d00            mov       eax,5DE558h

LAB_00446aae:
  00446aae  66837e1000            cmp       word [esi+10h],0
  00446ab3  bf01000000            mov       edi,1
  00446ab8  750a                  jne       short 00446AC4h
  00446aba  66897e10              mov       [esi+10h],di
  00446abe  66c746120000          mov       word [esi+12h],0

LAB_00446ac4:
  00446ac4  66397e10              cmp       [esi+10h],di
  00446ac8  0f853b010000          jne       near 00446C09h
  00446ace  8b481c                mov       ecx,[eax+1Ch]
  00446ad1  8d5e1c                lea       ebx,[esi+1Ch]
  00446ad4  890b                  mov       [ebx],ecx
  00446ad6  8b5020                mov       edx,[eax+20h]
  00446ad9  895620                mov       [esi+20h],edx
  00446adc  8b4824                mov       ecx,[eax+24h]
  00446adf  894e24                mov       [esi+24h],ecx
  00446ae2  8b08                  mov       ecx,[eax]
  00446ae4  f7c100008000          test      ecx,800000h
  00446aea  741a                  je        short 00446B06h
  00446aec  66c746141e00          mov       word [esi+14h],1Eh
  00446af2  66c746167017          mov       word [esi+16h],1770h
  00446af8  66c7869a000000a000    mov       word [esi+9Ah],0A0h
  00446b01  e9c6000000            jmp       00446BCCh

LAB_00446b06:
  00446b06  8d542418              lea       edx,[esp+18h]
  00446b0a  8d4c240c              lea       ecx,[esp+0Ch]
  00446b0e  52                    push      edx
  00446b0f  8d542414              lea       edx,[esp+14h]
  00446b13  51                    push      ecx
  00446b14  52                    push      edx
  00446b15  50                    push      eax
  00446b16  e8651e0f00            call      00538980h
  00446b1b  8b442428              mov       eax,[esp+28h]
  00446b1f  83c410                add       esp,10h
  00446b22  0fbf481c              movsx     ecx,word [eax+1Ch]
  00446b26  0fbf5020              movsx     edx,word [eax+20h]
  00446b2a  03ca                  add       ecx,edx
  00446b2c  d1f9                  sar       ecx,1
  00446b2e  66d1f9                sar       cx,1
  00446b31  66894e14              mov       [esi+14h],cx
  00446b35  e806061000            call      00547140h
  00446b3a  0fbf4e14              movsx     ecx,word [esi+14h]
  00446b3e  99                    cdq
  00446b3f  f7f9                  idiv      ecx
  00446b41  8b442418              mov       eax,[esp+18h]
  00446b45  66895614              mov       [esi+14h],dx
  00446b49  0fbf481c              movsx     ecx,word [eax+1Ch]
  00446b4d  0fbf501e              movsx     edx,word [eax+1Eh]
  00446b51  0fbf4020              movsx     eax,word [eax+20h]
  00446b55  03ca                  add       ecx,edx
  00446b57  03c8                  add       ecx,eax
  00446b59  b856555555            mov       eax,55555556h
  00446b5e  f7e9                  imul      ecx
  00446b60  8bca                  mov       ecx,edx
  00446b62  c1e91f                shr       ecx,1Fh
  00446b65  03d1                  add       edx,ecx
  00446b67  6681fa5802            cmp       dx,258h
  00446b6c  66895616              mov       [esi+16h],dx
  00446b70  7e06                  jle       short 00446B78h
  00446b72  66c746165802          mov       word [esi+16h],258h

LAB_00446b78:
  00446b78  668b4616              mov       ax,[esi+16h]
  00446b7c  666bc032              imul      ax,32h
  00446b80  6685c0                test      ax,ax
  00446b83  66894616              mov       [esi+16h],ax
  00446b87  7f04                  jg        short 00446B8Dh
  00446b89  66897e16              mov       [esi+16h],di

LAB_00446b8d:
  00446b8d  668b7e16              mov       di,[esi+16h]
  00446b91  e8aa051000            call      00547140h
  00446b96  0fbfcf                movsx     ecx,di
  00446b99  99                    cdq
  00446b9a  d1f9                  sar       ecx,1
  00446b9c  f7f9                  idiv      ecx
  00446b9e  66d1ff                sar       di,1
  00446ba1  03d7                  add       edx,edi
  00446ba3  66895616              mov       [esi+16h],dx
  00446ba7  e894051000            call      00547140h
  00446bac  8b542418              mov       edx,[esp+18h]
  00446bb0  0fbf4a1e              movsx     ecx,word [edx+1Eh]
  00446bb4  99                    cdq
  00446bb5  f7f9                  idiv      ecx
  00446bb7  6685d2                test      dx,dx
  00446bba  6689969a000000        mov       [esi+9Ah],dx
  00446bc1  7f09                  jg        short 00446BCCh
  00446bc3  66c7869a0000000b00    mov       word [esi+9Ah],0Bh

LAB_00446bcc:
  00446bcc  33d2                  xor       edx,edx
  00446bce  33c0                  xor       eax,eax
  00446bd0  8a560a                mov       dl,[esi+0Ah]
  00446bd3  8a460b                mov       al,[esi+0Bh]
  00446bd6  52                    push      edx
  00446bd7  50                    push      eax
  00446bd8  689b000000            push      9Bh
  00446bdd  6a00                  push      0
  00446bdf  53                    push      ebx
  00446be0  56                    push      esi
  00446be1  e85a400300            call      0047AC40h
  00446be6  83c418                add       esp,18h
  00446be9  66ff4612              inc       word [esi+12h]
  00446bed  66837e1205            cmp       word [esi+12h],5
  00446bf2  7c15                  jl        short 00446C09h
  00446bf4  66c746100200          mov       word [esi+10h],2
  00446bfa  66c746121e00          mov       word [esi+12h],1Eh
  00446c00  66c786980000000000    mov       word [esi+98h],0

LAB_00446c09:
  00446c09  bf02000000            mov       edi,2
  00446c0e  66397e10              cmp       [esi+10h],di
  00446c12  7570                  jne       short 00446C84h
  00446c14  66ff4e12              dec       word [esi+12h]
  00446c18  66ff8698000000        inc       word [esi+98h]
  00446c1f  668b8698000000        mov       ax,[esi+98h]
  00446c26  6685c0                test      ax,ax
  00446c29  7518                  jne       short 00446C43h
  00446c2b  8d4e1c                lea       ecx,[esi+1Ch]
  00446c2e  51                    push      ecx
  00446c2f  6800100000            push      1000h
  00446c34  684e0f0000            push      0F4Eh
  00446c39  6a14                  push      14h
  00446c3b  e8e06e0f00            call      0053DB20h
  00446c40  83c410                add       esp,10h

LAB_00446c43:
  00446c43  6639be98000000        cmp       [esi+98h],di
  00446c4a  7518                  jne       short 00446C64h
  00446c4c  8d561c                lea       edx,[esi+1Ch]
  00446c4f  52                    push      edx
  00446c50  6800100000            push      1000h
  00446c55  6800100000            push      1000h
  00446c5a  6a14                  push      14h
  00446c5c  e8bf6e0f00            call      0053DB20h
  00446c61  83c410                add       esp,10h

LAB_00446c64:
  00446c64  6683be9800000004      cmp       word [esi+98h],4
  00446c6c  7509                  jne       short 00446C77h
  00446c6e  66c786980000000000    mov       word [esi+98h],0

LAB_00446c77:
  00446c77  66837e1200            cmp       word [esi+12h],0
  00446c7c  7d06                  jge       short 00446C84h
  00446c7e  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_00446c84:
  00446c84  5f                    pop       edi
  00446c85  5e                    pop       esi
  00446c86  5b                    pop       ebx
  00446c87  83c408                add       esp,8
  00446c8a  c3                    ret

11 cible(s) de saut interne : 0x00446aae, 0x00446ac4, 0x00446b06, 0x00446b78, 0x00446b8d, 0x00446bcc, 0x00446c09, 0x00446c43, 0x00446c64, 0x00446c77, 0x00446c84
