Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00539a20  section .text  offset fichier 0x138e20  310 octets

  00539a20  53                    push      ebx
  00539a21  55                    push      ebp
  00539a22  56                    push      esi
  00539a23  8b74241c              mov       esi,[esp+1Ch]
  00539a27  668b442420            mov       ax,[esp+20h]
  00539a2c  57                    push      edi
  00539a2d  8b4e40                mov       ecx,[esi+40h]
  00539a30  8b7c2418              mov       edi,[esp+18h]
  00539a34  83e1bf                and       ecx,0FFFFFFBFh
  00539a37  66c786960000000000    mov       word [esi+96h],0
  00539a40  894e40                mov       [esi+40h],ecx
  00539a43  8a4c2428              mov       cl,[esp+28h]
  00539a47  66898600010000        mov       [esi+100h],ax
  00539a4e  888e02010000          mov       [esi+102h],cl
  00539a54  8b07                  mov       eax,[edi]
  00539a56  c605c0995e0000        mov       byte [5E99C0h],0
  00539a5d  a900008000            test      eax,800000h
  00539a62  7405                  je        short 00539A69h
  00539a64  6a0a                  push      0Ah
  00539a66  57                    push      edi
  00539a67  eb07                  jmp       short 00539A70h

LAB_00539a69:
  00539a69  8b542414              mov       edx,[esp+14h]
  00539a6d  6a14                  push      14h
  00539a6f  52                    push      edx

LAB_00539a70:
  00539a70  e86b9efeff            call      005238E0h
  00539a75  33db                  xor       ebx,ebx
  00539a77  83c408                add       esp,8
  00539a7a  8a9e02010000          mov       bl,[esi+102h]
  00539a80  33ed                  xor       ebp,ebp
  00539a82  668bae00010000        mov       bp,[esi+100h]
  00539a89  53                    push      ebx
  00539a8a  8d471c                lea       eax,[edi+1Ch]
  00539a8d  55                    push      ebp
  00539a8e  50                    push      eax
  00539a8f  e8dc55fdff            call      0050F070h
  00539a94  8b4f18                mov       ecx,[edi+18h]
  00539a97  66896f18              mov       [edi+18h],bp
  00539a9b  894c242c              mov       [esp+2Ch],ecx
  00539a9f  33c9                  xor       ecx,ecx
  00539aa1  668b4f0a              mov       cx,[edi+0Ah]
  00539aa5  66895f1a              mov       [edi+1Ah],bx
  00539aa9  66296e4c              sub       [esi+4Ch],bp
  00539aad  83c40c                add       esp,0Ch
  00539ab0  66837e4c00            cmp       word [esi+4Ch],0
  00539ab5  894c2424              mov       [esp+24h],ecx
  00539ab9  0f8f97000000          jg        near 00539B56h
  00539abf  66c7464c0000          mov       word [esi+4Ch],0
  00539ac5  8b07                  mov       eax,[edi]
  00539ac7  a900000040            test      eax,40000000h
  00539acc  7573                  jne       short 00539B41h
  00539ace  0d00000040            or        eax,40000000h
  00539ad3  8907                  mov       [edi],eax
  00539ad5  8b5640                mov       edx,[esi+40h]
  00539ad8  81ca00000040          or        edx,40000000h
  00539ade  895640                mov       [esi+40h],edx
  00539ae1  8b07                  mov       eax,[edi]
  00539ae3  a900008000            test      eax,800000h
  00539ae8  7557                  jne       short 00539B41h
  00539aea  8b7614                mov       esi,[esi+14h]
  00539aed  f7c600000080          test      esi,80000000h
  00539af3  754c                  jne       short 00539B41h
  00539af5  33c0                  xor       eax,eax
  00539af7  f7c600900100          test      esi,19000h
  00539afd  7405                  je        short 00539B04h
  00539aff  b851000000            mov       eax,51h

LAB_00539b04:
  00539b04  f7c60000600b          test      esi,0B600000h
  00539b0a  7405                  je        short 00539B11h
  00539b0c  b852000000            mov       eax,52h

LAB_00539b11:
  00539b11  f7c600000800          test      esi,80000h
  00539b17  7405                  je        short 00539B1Eh
  00539b19  b853000000            mov       eax,53h

LAB_00539b1e:
  00539b1e  f7c600689604          test      esi,4966800h
  00539b24  7405                  je        short 00539B2Bh
  00539b26  b854000000            mov       eax,54h

LAB_00539b2b:
  00539b2b  85c0                  test      eax,eax
  00539b2d  7412                  je        short 00539B41h
  00539b2f  6a00                  push      0
  00539b31  6a00                  push      0
  00539b33  50                    push      eax
  00539b34  57                    push      edi
  00539b35  e8960ef4ff            call      0047A9D0h
  00539b3a  8b4c2434              mov       ecx,[esp+34h]
  00539b3e  83c410                add       esp,10h

LAB_00539b41:
  00539b41  8b542420              mov       edx,[esp+20h]
  00539b45  66894f0a              mov       [edi+0Ah],cx
  00539b49  895718                mov       [edi+18h],edx
  00539b4c  b801000000            mov       eax,1
  00539b51  5f                    pop       edi
  00539b52  5e                    pop       esi
  00539b53  5d                    pop       ebp
  00539b54  5b                    pop       ebx
  00539b55  c3                    ret

7 cible(s) de saut interne : 0x00539a69, 0x00539a70, 0x00539b04, 0x00539b11, 0x00539b1e, 0x00539b2b, 0x00539b41
