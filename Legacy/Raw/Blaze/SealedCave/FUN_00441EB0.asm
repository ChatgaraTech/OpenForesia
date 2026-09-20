  00441eb0  81eca0000000          sub       esp,0A0h
  00441eb6  53                    push      ebx
  00441eb7  55                    push      ebp
  00441eb8  56                    push      esi
  00441eb9  57                    push      edi
  00441eba  33db                  xor       ebx,ebx
  00441ebc  b926000000            mov       ecx,26h
  00441ec1  33c0                  xor       eax,eax
  00441ec3  8d7c2418              lea       edi,[esp+18h]
  00441ec7  895c2414              mov       [esp+14h],ebx
  00441ecb  f3ab                  rep stosd
  00441ecd  e87e90fdff            call      0041AF50h
  00441ed2  e869c7fcff            call      0040E640h
  00441ed7  8bb424b4000000        mov       esi,[esp+0B4h]
  00441ede  8d7c2414              lea       edi,[esp+14h]
  00441ee2  b8dbca5e00            mov       eax,5ECADBh
  00441ee7  bd20000000            mov       ebp,20h
  00441eec  8a48d1                mov       cl,[eax-2Fh]
  00441eef  84c9                  test      cl,cl
  00441ef1  7549                  jne       short 00441F3Ch
  00441ef3  8b50c5                mov       edx,[eax-3Bh]
  00441ef6  8d48c5                lea       ecx,[eax-3Bh]
  00441ef9  f7c200000080          test      edx,80000000h
  00441eff  743b                  je        short 00441F3Ch
  00441f01  8b50e1                mov       edx,[eax-1Fh]
  00441f04  8bf9                  mov       edi,ecx
  00441f06  89561c                mov       [esi+1Ch],edx
  00441f09  8b50e5                mov       edx,[eax-1Bh]
  00441f0c  895620                mov       [esi+20h],edx
  00441f0f  8b50e9                mov       edx,[eax-17h]
  00441f12  895624                mov       [esi+24h],edx
  00441f15  33d2                  xor       edx,edx
  00441f17  8a50c9                mov       dl,[eax-37h]
  00441f1a  66c740dd0200          mov       word [eax-23h],2
  00441f20  8b149540965e00        mov       edx,[edx*4+5E9640h]
  00441f27  8b5208                mov       edx,[edx+8]
  00441f2a  89542410              mov       [esp+10h],edx
  00441f2e  8a10                  mov       dl,[eax]
  00441f30  80ca02                or        dl,2
  00441f33  8810                  mov       [eax],dl
  00441f35  8b11                  mov       edx,[ecx]
  00441f37  80ce10                or        dh,10h
  00441f3a  8911                  mov       [ecx],edx
  00441f3c  059c000000            add       eax,9Ch
  00441f41  4d                    dec       ebp
  00441f42  75a8                  jne       short 00441EECh
  00441f44  66395e10              cmp       [esi+10h],bx
  00441f48  b801000000            mov       eax,1
  00441f4d  7548                  jne       short 00441F97h
  00441f4f  66894610              mov       [esi+10h],ax
  00441f53  66895e12              mov       [esi+12h],bx
  00441f57  66895e14              mov       [esi+14h],bx
  00441f5b  66895e16              mov       [esi+16h],bx
  00441f5f  0fbf0db8925e00        movsx     ecx,word [5E92B8h]
  00441f66  894e50                mov       [esi+50h],ecx
  00441f69  0fbf15ba925e00        movsx     edx,word [5E92BAh]
  00441f70  895654                mov       [esi+54h],edx
  00441f73  0fbf0dbc925e00        movsx     ecx,word [5E92BCh]
  00441f7a  894e58                mov       [esi+58h],ecx
  00441f7d  66895f1a              mov       [edi+1Ah],bx
  00441f81  c6470a00              mov       byte [edi+0Ah],0
  00441f85  c74738ffffff00        mov       dword [edi+38h],0FFFFFFh
  00441f8c  66894736              mov       [edi+36h],ax
  00441f90  66899e98000000        mov       [esi+98h],bx
  00441f97  66394610              cmp       [esi+10h],ax
  00441f9b  0f85f3010000          jne       near 00442194h
  00441fa1  660fb64738            movzx     ax,byte [edi+38h]
  00441fa6  660fb64f39            movzx     cx,byte [edi+39h]
  00441fab  660fb6573a            movzx     dx,byte [edi+3Ah]
  00441fb0  83e802                sub       eax,2
  00441fb3  83e902                sub       ecx,2
  00441fb6  83ea02                sub       edx,2
  00441fb9  663bc3                cmp       ax,bx
  00441fbc  7d06                  jge       short 00441FC4h
  00441fbe  33c0                  xor       eax,eax
  00441fc0  33c9                  xor       ecx,ecx
  00441fc2  33d2                  xor       edx,edx
  00441fc4  bdfa000000            mov       ebp,0FAh
  00441fc9  884738                mov       [edi+38h],al
  00441fcc  884f39                mov       [edi+39h],cl
  00441fcf  88573a                mov       [edi+3Ah],dl
  00441fd2  66892d60915e00        mov       [5E9160h],bp
  00441fd9  66892d62915e00        mov       [5E9162h],bp
  00441fe0  66892d64915e00        mov       [5E9164h],bp
  00441fe7  66c70566915e000200    mov       word [5E9166h],2
  00441ff0  66ff4612              inc       word [esi+12h]
  00441ff4  66ff4614              inc       word [esi+14h]
  00441ff8  66837e163c            cmp       word [esi+16h],3Ch
  00441ffd  7d16                  jge       short 00442015h
  00441fff  6aff                  push      0FFFFFFFFh
  00442001  53                    push      ebx
  00442002  68f9000000            push      0F9h
  00442007  8d561c                lea       edx,[esi+1Ch]
  0044200a  53                    push      ebx
  0044200b  52                    push      edx
  0044200c  56                    push      esi
  0044200d  e82e8c0300            call      0047AC40h
  00442012  83c418                add       esp,18h
  00442015  66837e1407            cmp       word [esi+14h],7
  0044201a  751c                  jne       short 00442038h
  0044201c  66837e1646            cmp       word [esi+16h],46h
  00442021  7d15                  jge       short 00442038h
  00442023  6aff                  push      0FFFFFFFFh
  00442025  53                    push      ebx
  00442026  68c3000000            push      0C3h
  0044202b  56                    push      esi
  0044202c  66895e14              mov       [esi+14h],bx
  00442030  e8ab8a0300            call      0047AAE0h
  00442035  83c410                add       esp,10h
  00442038  bb14000000            mov       ebx,14h
  0044203d  66395e12              cmp       [esi+12h],bx
  00442041  752d                  jne       short 00442070h
  00442043  6aff                  push      0FFFFFFFFh
  00442045  6a00                  push      0
  00442047  68c4000000            push      0C4h
  0044204c  56                    push      esi
  0044204d  e88e8a0300            call      0047AAE0h
  00442052  83c410                add       esp,10h
  00442055  66395e12              cmp       [esi+12h],bx
  00442059  7515                  jne       short 00442070h
  0044205b  6800100000            push      1000h
  00442060  6800100000            push      1000h
  00442065  55                    push      ebp
  00442066  6a11                  push      11h
  00442068  e833ae0f00            call      0053CEA0h
  0044206d  83c410                add       esp,10h
  00442070  66837e1215            cmp       word [esi+12h],15h
  00442075  7519                  jne       short 00442090h
  00442077  6800100000            push      1000h
  0044207c  6800100000            push      1000h
  00442081  6820030000            push      320h
  00442086  6a11                  push      11h
  00442088  e813ae0f00            call      0053CEA0h
  0044208d  83c410                add       esp,10h
  00442090  66837e1216            cmp       word [esi+12h],16h
  00442095  7516                  jne       short 004420ADh
  00442097  6800100000            push      1000h
  0044209c  6800100000            push      1000h
  004420a1  6a7d                  push      7Dh
  004420a3  6a16                  push      16h
  004420a5  e8f6ad0f00            call      0053CEA0h
  004420aa  83c410                add       esp,10h
  004420ad  66837e1217            cmp       word [esi+12h],17h
  004420b2  7519                  jne       short 004420CDh
  004420b4  6800100000            push      1000h
  004420b9  6800100000            push      1000h
  004420be  6890010000            push      190h
  004420c3  6a16                  push      16h
  004420c5  e8d6ad0f00            call      0053CEA0h
  004420ca  83c410                add       esp,10h
  004420cd  668b4612              mov       ax,[esi+12h]
  004420d1  663d1900              cmp       ax,19h
  004420d5  7e39                  jle       short 00442110h
  004420d7  663d6400              cmp       ax,64h
  004420db  7d33                  jge       short 00442110h
  004420dd  66ff8698000000        inc       word [esi+98h]
  004420e4  6683be9800000005      cmp       word [esi+98h],5
  004420ec  7522                  jne       short 00442110h
  004420ee  6800100000            push      1000h
  004420f3  6800100000            push      1000h
  004420f8  6800020000            push      200h
  004420fd  6a2d                  push      2Dh
  004420ff  e89cad0f00            call      0053CEA0h
  00442104  83c410                add       esp,10h
  00442107  66c786980000000000    mov       word [esi+98h],0
  00442110  f6461201              test      byte [esi+12h],1
  00442114  7524                  jne       short 0044213Ah
  00442116  8b442410              mov       eax,[esp+10h]
  0044211a  66ff471a              inc       word [edi+1Ah]
  0044211e  668b4f1a              mov       cx,[edi+1Ah]
  00442122  668b4016              mov       ax,[eax+16h]
  00442126  0fbfc9                movsx     ecx,cx
  00442129  8bd0                  mov       edx,eax
  0044212b  81e2ffff0000          and       edx,0FFFFh
  00442131  3bca                  cmp       ecx,edx
  00442133  7c05                  jl        short 0044213Ah
  00442135  48                    dec       eax
  00442136  6689471a              mov       [edi+1Ah],ax
  0044213a  66817e128c00          cmp       word [esi+12h],8Ch
  00442140  7c52                  jl        short 00442194h
  00442142  e869160c00            call      005037B0h
  00442147  8b06                  mov       eax,[esi]
  00442149  6a00                  push      0
  0044214b  25ffffff7f            and       eax,7FFFFFFFh
  00442150  6a00                  push      0
  00442152  8906                  mov       [esi],eax
  00442154  a1cc975e00            mov       eax,[5E97CCh]
  00442159  50                    push      eax
  0044215a  e8711c0d00            call      00513DD0h
  0044215f  8b07                  mov       eax,[edi]
  00442161  83c40c                add       esp,0Ch
  00442164  0d00400002            or        eax,2004000h
  00442169  8907                  mov       [edi],eax
  0044216b  668b4e50              mov       cx,[esi+50h]
  0044216f  66890d60915e00        mov       [5E9160h],cx
  00442176  668b5654              mov       dx,[esi+54h]
  0044217a  66891562915e00        mov       [5E9162h],dx
  00442181  668b4658              mov       ax,[esi+58h]
  00442185  66a364915e00          mov       [5E9164h],ax
  0044218b  66c70566915e000200    mov       word [5E9166h],2
  00442194  66ff4616              inc       word [esi+16h]
  00442198  5f                    pop       edi
  00442199  5e                    pop       esi
  0044219a  5d                    pop       ebp
  0044219b  5b                    pop       ebx
  0044219c  81c4a0000000          add       esp,0A0h
  004421a2  c3                    ret
