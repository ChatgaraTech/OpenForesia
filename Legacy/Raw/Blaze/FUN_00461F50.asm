C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00461f50  section .text  offset fichier 0x61350  383 octets

  00461f50  83ec08                sub       esp,8
  00461f53  53                    push      ebx
  00461f54  56                    push      esi
  00461f55  8b742414              mov       esi,[esp+14h]
  00461f59  56                    push      esi
  00461f5a  668b4610              mov       ax,[esi+10h]
  00461f5e  8a5e10                mov       bl,[esi+10h]
  00461f61  c1f808                sar       eax,8
  00461f64  88442410              mov       [esp+10h],al
  00461f68  8a4616                mov       al,[esi+16h]
  00461f6b  8844240f              mov       [esp+0Fh],al
  00461f6f  668b4616              mov       ax,[esi+16h]
  00461f73  c1f808                sar       eax,8
  00461f76  88442418              mov       [esp+18h],al
  00461f7a  e841f0ffff            call      00460FC0h
  00461f7f  8b06                  mov       eax,[esi]
  00461f81  83c404                add       esp,4
  00461f84  a900000040            test      eax,40000000h
  00461f89  0f853a010000          jne       near 004620C9h
  00461f8f  84db                  test      bl,bl
  00461f91  57                    push      edi
  00461f92  7534                  jne       short 00461FC8h
  00461f94  668b4e34              mov       cx,[esi+34h]
  00461f98  b301                  mov       bl,1
  00461f9a  c644241000            mov       byte [esp+10h],0
  00461f9f  c644240f00            mov       byte [esp+0Fh],0
  00461fa4  c644241800            mov       byte [esp+18h],0
  00461fa9  66894e14              mov       [esi+14h],cx
  00461fad  bf15000000            mov       edi,15h

LAB_00461fb2:
  00461fb2  6a00                  push      0
  00461fb4  6a00                  push      0
  00461fb6  6aff                  push      0FFFFFFFFh
  00461fb8  6a00                  push      0
  00461fba  6a33                  push      33h
  00461fbc  56                    push      esi
  00461fbd  e84e610000            call      00468110h
  00461fc2  83c418                add       esp,18h
  00461fc5  4f                    dec       edi
  00461fc6  75ea                  jne       short 00461FB2h

LAB_00461fc8:
  00461fc8  80fb01                cmp       bl,1
  00461fcb  0f8590000000          jne       near 00462061h
  00461fd1  8b542410              mov       edx,[esp+10h]
  00461fd5  8b4620                mov       eax,[esi+20h]
  00461fd8  81e2ff000000          and       edx,0FFh
  00461fde  c1e20c                shl       edx,0Ch
  00461fe1  2bc2                  sub       eax,edx
  00461fe3  8a542418              mov       dl,[esp+18h]
  00461fe7  894620                mov       [esi+20h],eax
  00461fea  8a44240f              mov       al,[esp+0Fh]
  00461fee  fec0                  inc       al
  00461ff0  fec2                  inc       dl
  00461ff2  3ac3                  cmp       al,bl
  00461ff4  8844240f              mov       [esp+0Fh],al
  00461ff8  88542418              mov       [esp+18h],dl
  00461ffc  750f                  jne       short 0046200Dh
  00461ffe  8a442410              mov       al,[esp+10h]
  00462002  c644240f00            mov       byte [esp+0Fh],0
  00462007  fec0                  inc       al
  00462009  88442410              mov       [esp+10h],al

LAB_0046200d:
  0046200d  8a442418              mov       al,[esp+18h]
  00462011  6683463412            add       word [esi+34h],12h
  00462016  3c19                  cmp       al,19h
  00462018  7547                  jne       short 00462061h
  0046201a  a1cc975e00            mov       eax,[5E97CCh]
  0046201f  6a00                  push      0
  00462021  6a00                  push      0
  00462023  6800100000            push      1000h
  00462028  8b88a8000000          mov       ecx,[eax+0A8h]
  0046202e  6800080000            push      800h
  00462033  b302                  mov       bl,2
  00462035  8b11                  mov       edx,[ecx]
  00462037  660fb64225            movzx     ax,byte [edx+25h]
  0046203c  50                    push      eax
  0046203d  56                    push      esi
  0046203e  e83d600000            call      00468080h
  00462043  83c418                add       esp,18h
  00462046  bf15000000            mov       edi,15h

LAB_0046204b:
  0046204b  6a00                  push      0
  0046204d  6a00                  push      0
  0046204f  6aff                  push      0FFFFFFFFh
  00462051  6a00                  push      0
  00462053  6a34                  push      34h
  00462055  56                    push      esi
  00462056  e8b5600000            call      00468110h
  0046205b  83c418                add       esp,18h
  0046205e  4f                    dec       edi
  0046205f  75ea                  jne       short 0046204Bh

LAB_00462061:
  00462061  80fb02                cmp       bl,2
  00462064  7548                  jne       short 004620AEh
  00462066  668b4634              mov       ax,[esi+34h]
  0046206a  668b5614              mov       dx,[esi+14h]
  0046206e  33c9                  xor       ecx,ecx
  00462070  663bc2                cmp       ax,dx
  00462073  7e0c                  jle       short 00462081h
  00462075  83c0e4                add       eax,0FFFFFFE4h
  00462078  b901000000            mov       ecx,1
  0046207d  66894634              mov       [esi+34h],ax

LAB_00462081:
  00462081  8b4620                mov       eax,[esi+20h]
  00462084  3d0000e7ff            cmp       eax,0FFE70000h
  00462089  7e09                  jle       short 00462094h
  0046208b  0500d0ffff            add       eax,0FFFFD000h
  00462090  41                    inc       ecx
  00462091  894620                mov       [esi+20h],eax

LAB_00462094:
  00462094  85c9                  test      ecx,ecx
  00462096  7516                  jne       short 004620AEh
  00462098  8b06                  mov       eax,[esi]
  0046209a  66895634              mov       [esi+34h],dx
  0046209e  0d00000050            or        eax,50000000h
  004620a3  c746200000e7ff        mov       dword [esi+20h],0FFE70000h
  004620aa  b303                  mov       bl,3
  004620ac  8906                  mov       [esi],eax

LAB_004620ae:
  004620ae  33c9                  xor       ecx,ecx
  004620b0  33d2                  xor       edx,edx
  004620b2  8a6c2410              mov       ch,[esp+10h]
  004620b6  8a742418              mov       dh,[esp+18h]
  004620ba  8a54240f              mov       dl,[esp+0Fh]
  004620be  8acb                  mov       cl,bl
  004620c0  66894e10              mov       [esi+10h],cx
  004620c4  66895616              mov       [esi+16h],dx
  004620c8  5f                    pop       edi

LAB_004620c9:
  004620c9  5e                    pop       esi
  004620ca  5b                    pop       ebx
  004620cb  83c408                add       esp,8
  004620ce  c3                    ret

9 cible(s) de saut interne : 0x00461fb2, 0x00461fc8, 0x0046200d, 0x0046204b, 0x00462061, 0x00462081, 0x00462094, 0x004620ae, 0x004620c9
