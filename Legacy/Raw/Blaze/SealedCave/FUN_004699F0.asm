Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004699f0  section .text  offset fichier 0x68df0  256 octets

  004699f0  51                    push      ecx
  004699f1  53                    push      ebx
  004699f2  55                    push      ebp
  004699f3  56                    push      esi
  004699f4  57                    push      edi
  004699f5  c744241000000000      mov       dword [esp+10h],0
  004699fd  33ed                  xor       ebp,ebp
  004699ff  b894955e00            mov       eax,5E9594h
  00469a04  ba04000000            mov       edx,4

LAB_00469a09:
  00469a09  8b08                  mov       ecx,[eax]
  00469a0b  f7c100000080          test      ecx,80000000h
  00469a11  740d                  je        short 00469A20h
  00469a13  f7c100000050          test      ecx,50000000h
  00469a19  7505                  jne       short 00469A20h
  00469a1b  45                    inc       ebp
  00469a1c  89442410              mov       [esp+10h],eax

LAB_00469a20:
  00469a20  2d9c000000            sub       eax,9Ch
  00469a25  4a                    dec       edx
  00469a26  75e1                  jne       short 00469A09h
  00469a28  b9c0935e00            mov       ecx,5E93C0h
  00469a2d  ba04000000            mov       edx,4

LAB_00469a32:
  00469a32  8b01                  mov       eax,[ecx]
  00469a34  a900000080            test      eax,80000000h
  00469a39  7405                  je        short 00469A40h
  00469a3b  2500000050            and       eax,50000000h

LAB_00469a40:
  00469a40  81c19c000000          add       ecx,9Ch
  00469a46  4a                    dec       edx
  00469a47  75e9                  jne       short 00469A32h
  00469a49  33db                  xor       ebx,ebx
  00469a4b  b894955e00            mov       eax,5E9594h
  00469a50  b904000000            mov       ecx,4

LAB_00469a55:
  00469a55  f70000000080          test      dword [eax],80000000h
  00469a5b  7401                  je        short 00469A5Eh
  00469a5d  43                    inc       ebx

LAB_00469a5e:
  00469a5e  2d9c000000            sub       eax,9Ch
  00469a63  49                    dec       ecx
  00469a64  75ef                  jne       short 00469A55h
  00469a66  8b742418              mov       esi,[esp+18h]
  00469a6a  6a00                  push      0
  00469a6c  6a10                  push      10h
  00469a6e  56                    push      esi
  00469a6f  e88cb50c00            call      00535000h
  00469a74  83c40c                add       esp,0Ch
  00469a77  8bf8                  mov       edi,eax
  00469a79  6800000040            push      40000000h
  00469a7e  6a10                  push      10h
  00469a80  56                    push      esi
  00469a81  e87ab50c00            call      00535000h
  00469a86  83c40c                add       esp,0Ch
  00469a89  663bfb                cmp       di,bx
  00469a8c  750f                  jne       short 00469A9Dh
  00469a8e  a128185700            mov       eax,[571828h]
  00469a93  80cc04                or        ah,4
  00469a96  a328185700            mov       [571828h],eax
  00469a9b  eb38                  jmp       short 00469AD5h

LAB_00469a9d:
  00469a9d  663bfd                cmp       di,bp
  00469aa0  7533                  jne       short 00469AD5h
  00469aa2  6685ed                test      bp,bp
  00469aa5  742e                  je        short 00469AD5h
  00469aa7  66837e1000            cmp       word [esi+10h],0
  00469aac  7527                  jne       short 00469AD5h
  00469aae  8b0dcc975e00          mov       ecx,[5E97CCh]
  00469ab4  8b442410              mov       eax,[esp+10h]
  00469ab8  6a00                  push      0
  00469aba  68b0335600            push      5633B0h
  00469abf  8b5170                mov       edx,[ecx+70h]
  00469ac2  50                    push      eax
  00469ac3  8b4208                mov       eax,[edx+8]
  00469ac6  50                    push      eax
  00469ac7  e884a80d00            call      00544350h
  00469acc  83c410                add       esp,10h
  00469acf  66c746100100          mov       word [esi+10h],1

LAB_00469ad5:
  00469ad5  a128185700            mov       eax,[571828h]
  00469ada  f6c408                test      ah,8
  00469add  7407                  je        short 00469AE6h
  00469adf  8b06                  mov       eax,[esi]
  00469ae1  80cc60                or        ah,60h
  00469ae4  8906                  mov       [esi],eax

LAB_00469ae6:
  00469ae6  5f                    pop       edi
  00469ae7  5e                    pop       esi
  00469ae8  5d                    pop       ebp
  00469ae9  5b                    pop       ebx
  00469aea  59                    pop       ecx
  00469aeb  c3                    ret
  00469aec  90                    nop
  00469aed  90                    nop
  00469aee  90                    nop
  00469aef  90                    nop

9 cible(s) de saut interne : 0x00469a09, 0x00469a20, 0x00469a32, 0x00469a40, 0x00469a55, 0x00469a5e, 0x00469a9d, 0x00469ad5, 0x00469ae6
