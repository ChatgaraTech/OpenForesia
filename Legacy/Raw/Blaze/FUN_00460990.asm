C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00460990  section .text  offset fichier 0x5fd90  255 octets

  00460990  53                    push      ebx
  00460991  56                    push      esi
  00460992  8b74240c              mov       esi,[esp+0Ch]
  00460996  33db                  xor       ebx,ebx
  00460998  57                    push      edi
  00460999  66395e10              cmp       [esi+10h],bx
  0046099d  7515                  jne       short 004609B4h
  0046099f  668b4634              mov       ax,[esi+34h]
  004609a3  66895e12              mov       [esi+12h],bx
  004609a7  66c746106300          mov       word [esi+10h],63h
  004609ad  6689869a000000        mov       [esi+9Ah],ax

LAB_004609b4:
  004609b4  66837e1001            cmp       word [esi+10h],1
  004609b9  bf02000000            mov       edi,2
  004609be  754d                  jne       short 00460A0Dh
  004609c0  66395e12              cmp       [esi+12h],bx
  004609c4  7529                  jne       short 004609EFh
  004609c6  8b0dcc975e00          mov       ecx,[5E97CCh]
  004609cc  53                    push      ebx
  004609cd  53                    push      ebx
  004609ce  6800100000            push      1000h
  004609d3  8b91a8000000          mov       edx,[ecx+0A8h]
  004609d9  6800080000            push      800h
  004609de  8b02                  mov       eax,[edx]
  004609e0  660fb64810            movzx     cx,byte [eax+10h]
  004609e5  51                    push      ecx
  004609e6  56                    push      esi
  004609e7  e894760000            call      00468080h
  004609ec  83c418                add       esp,18h

LAB_004609ef:
  004609ef  6683463464            add       word [esi+34h],64h
  004609f4  66ff4612              inc       word [esi+12h]
  004609f8  ba0a000000            mov       edx,0Ah
  004609fd  66395612              cmp       [esi+12h],dx
  00460a01  750f                  jne       short 00460A12h
  00460a03  66897e10              mov       [esi+10h],di
  00460a07  66895e12              mov       [esi+12h],bx
  00460a0b  eb05                  jmp       short 00460A12h

LAB_00460a0d:
  00460a0d  ba0a000000            mov       edx,0Ah

LAB_00460a12:
  00460a12  66397e10              cmp       [esi+10h],di
  00460a16  b903000000            mov       ecx,3
  00460a1b  7539                  jne       short 00460A56h
  00460a1d  0fbf4614              movsx     eax,word [esi+14h]
  00460a21  8d3c80                lea       edi,[eax+eax*4]
  00460a24  c1e703                shl       edi,3
  00460a27  2bf8                  sub       edi,eax
  00460a29  33c0                  xor       eax,eax
  00460a2b  8a04bdc8935e00        mov       al,[edi*4+5E93C8h]
  00460a32  8bf8                  mov       edi,eax
  00460a34  33c0                  xor       eax,eax
  00460a36  6bff4c                imul      edi,4Ch
  00460a39  8a8788915e00          mov       al,[edi+5E9188h]
  00460a3f  c1e004                shl       eax,4
  00460a42  f780204a600000a00000  test      dword [eax+604A20h],0A000h
  00460a4c  7508                  jne       short 00460A56h
  00460a4e  66894e10              mov       [esi+10h],cx
  00460a52  66895e12              mov       [esi+12h],bx

LAB_00460a56:
  00460a56  66394e10              cmp       [esi+10h],cx
  00460a5a  7519                  jne       short 00460A75h
  00460a5c  668346349c            add       word [esi+34h],0FF9Ch
  00460a61  66ff4612              inc       word [esi+12h]
  00460a65  66395612              cmp       [esi+12h],dx
  00460a69  750a                  jne       short 00460A75h
  00460a6b  66c746106300          mov       word [esi+10h],63h
  00460a71  66895e12              mov       [esi+12h],bx

LAB_00460a75:
  00460a75  66395610              cmp       [esi+10h],dx
  00460a79  7510                  jne       short 00460A8Bh
  00460a7b  668b8e9a000000        mov       cx,[esi+9Ah]
  00460a82  6681c14c04            add       cx,44Ch
  00460a87  66894e34              mov       [esi+34h],cx

LAB_00460a8b:
  00460a8b  5f                    pop       edi
  00460a8c  5e                    pop       esi
  00460a8d  5b                    pop       ebx
  00460a8e  c3                    ret

7 cible(s) de saut interne : 0x004609b4, 0x004609ef, 0x00460a0d, 0x00460a12, 0x00460a56, 0x00460a75, 0x00460a8b
