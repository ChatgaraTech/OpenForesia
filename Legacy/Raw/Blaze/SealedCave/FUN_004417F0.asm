  004417f0  83ec3c                sub       esp,3Ch
  004417f3  53                    push      ebx
  004417f4  55                    push      ebp
  004417f5  56                    push      esi
  004417f6  57                    push      edi
  004417f7  66c74424100001        mov       word [esp+10h],100h
  004417fe  66c74424121501        mov       word [esp+12h],115h
  00441805  66c74424142a01        mov       word [esp+14h],12Ah
  0044180c  66c74424163f01        mov       word [esp+16h],13Fh
  00441813  66c74424185501        mov       word [esp+18h],155h
  0044181a  66c744241a6a01        mov       word [esp+1Ah],16Ah
  00441821  66c744241c7f01        mov       word [esp+1Ch],17Fh
  00441828  66c744241e9501        mov       word [esp+1Eh],195h
  0044182f  66c7442420aa01        mov       word [esp+20h],1AAh
  00441836  66c7442422bf01        mov       word [esp+22h],1BFh
  0044183d  66c7442424d401        mov       word [esp+24h],1D4h
  00441844  66c7442426e901        mov       word [esp+26h],1E9h
  0044184b  66c74424280002        mov       word [esp+28h],200h
  00441852  66c744242a2a02        mov       word [esp+2Ah],22Ah
  00441859  66c744242c5502        mov       word [esp+2Ch],255h
  00441860  66c744242e7f02        mov       word [esp+2Eh],27Fh
  00441867  66c7442430aa02        mov       word [esp+30h],2AAh
  0044186e  66c7442432d402        mov       word [esp+32h],2D4h
  00441875  66c7442434ff02        mov       word [esp+34h],2FFh
  0044187c  66c74424362903        mov       word [esp+36h],329h
  00441883  66c74424385403        mov       word [esp+38h],354h
  0044188a  66c744243a7e03        mov       word [esp+3Ah],37Eh
  00441891  66c744243ca903        mov       word [esp+3Ch],3A9h
  00441898  66c744243ed303        mov       word [esp+3Eh],3D3h
  0044189f  66c74424400004        mov       word [esp+40h],400h
  004418a6  66c74424425504        mov       word [esp+42h],455h
  004418ad  66c7442444aa04        mov       word [esp+44h],4AAh
  004418b4  66c7442446ff04        mov       word [esp+46h],4FFh
  004418bb  66c74424485405        mov       word [esp+48h],554h
  004418c2  66c744244aa905        mov       word [esp+4Ah],5A9h
  004418c9  33ff                  xor       edi,edi
  004418cb  e88096fdff            call      0041AF50h
  004418d0  e86bcdfcff            call      0040E640h
  004418d5  8b742450              mov       esi,[esp+50h]
  004418d9  b8dbca5e00            mov       eax,5ECADBh
  004418de  bd20000000            mov       ebp,20h
  004418e3  ba02000000            mov       edx,2
  004418e8  8a48d1                mov       cl,[eax-2Fh]
  004418eb  84c9                  test      cl,cl
  004418ed  7546                  jne       short 00441935h
  004418ef  8b58c5                mov       ebx,[eax-3Bh]
  004418f2  8d48c5                lea       ecx,[eax-3Bh]
  004418f5  f7c300000080          test      ebx,80000000h
  004418fb  7438                  je        short 00441935h
  004418fd  8b58e1                mov       ebx,[eax-1Fh]
  00441900  8bf9                  mov       edi,ecx
  00441902  895e1c                mov       [esi+1Ch],ebx
  00441905  8b58e5                mov       ebx,[eax-1Bh]
  00441908  895e20                mov       [esi+20h],ebx
  0044190b  8b58e9                mov       ebx,[eax-17h]
  0044190e  895e24                mov       [esi+24h],ebx
  00441911  33db                  xor       ebx,ebx
  00441913  8a58c9                mov       bl,[eax-37h]
  00441916  668950dd              mov       [eax-23h],dx
  0044191a  8b1c9d40965e00        mov       ebx,[ebx*4+5E9640h]
  00441921  8b5b08                mov       ebx,[ebx+8]
  00441924  895c2450              mov       [esp+50h],ebx
  00441928  8a18                  mov       bl,[eax]
  0044192a  0ada                  or        bl,dl
  0044192c  8818                  mov       [eax],bl
  0044192e  8b19                  mov       ebx,[ecx]
  00441930  80cf10                or        bh,10h
  00441933  8919                  mov       [ecx],ebx
  00441935  059c000000            add       eax,9Ch
  0044193a  4d                    dec       ebp
  0044193b  75ab                  jne       short 004418E8h
  0044193d  85ff                  test      edi,edi
  0044193f  7505                  jne       short 00441946h
  00441941  bf58e55d00            mov       edi,5DE558h
  00441946  33ed                  xor       ebp,ebp
  00441948  bb01000000            mov       ebx,1
  0044194d  66396e10              cmp       [esi+10h],bp
  00441951  7533                  jne       short 00441986h
  00441953  6aff                  push      0FFFFFFFFh
  00441955  55                    push      ebp
  00441956  68c6000000            push      0C6h
  0044195b  56                    push      esi
  0044195c  e87f910300            call      0047AAE0h
  00441961  66895e10              mov       [esi+10h],bx
  00441965  66896e16              mov       [esi+16h],bp
  00441969  66896f1a              mov       [edi+1Ah],bp
  0044196d  c6470a00              mov       byte [edi+0Ah],0
  00441971  c74738ffffff00        mov       dword [edi+38h],0FFFFFFh
  00441978  66895f36              mov       [edi+36h],bx
  0044197c  83c410                add       esp,10h
  0044197f  6689ae98000000        mov       [esi+98h],bp
  00441986  66396e16              cmp       [esi+16h],bp
  0044198a  7516                  jne       short 004419A2h
  0044198c  6800100000            push      1000h
  00441991  6800100000            push      1000h
  00441996  6a7d                  push      7Dh
  00441998  6a2d                  push      2Dh
  0044199a  e801b50f00            call      0053CEA0h
  0044199f  83c410                add       esp,10h
  004419a2  66395e16              cmp       [esi+16h],bx
  004419a6  7519                  jne       short 004419C1h
  004419a8  6800100000            push      1000h
  004419ad  6800100000            push      1000h
  004419b2  68c8000000            push      0C8h
  004419b7  6a2d                  push      2Dh
  004419b9  e8e2b40f00            call      0053CEA0h
  004419be  83c410                add       esp,10h
  004419c1  8a4616                mov       al,[esi+16h]
  004419c4  bd64000000            mov       ebp,64h
  004419c9  84c3                  test      bl,al
  004419cb  7432                  je        short 004419FFh
  004419cd  668b8698000000        mov       ax,[esi+98h]
  004419d4  663bc5                cmp       ax,bp
  004419d7  7c26                  jl        short 004419FFh
  004419d9  663d8b00              cmp       ax,8Bh
  004419dd  7f20                  jg        short 004419FFh
  004419df  6800100000            push      1000h
  004419e4  6800100000            push      1000h
  004419e9  68a9050000            push      5A9h
  004419ee  6a31                  push      31h
  004419f0  e8abb40f00            call      0053CEA0h
  004419f5  83c410                add       esp,10h
  004419f8  66ff8698000000        inc       word [esi+98h]
  004419ff  668b4616              mov       ax,[esi+16h]
  00441a03  84c3                  test      bl,al
  00441a05  7448                  je        short 00441A4Fh
  00441a07  663d0300              cmp       ax,3
  00441a0b  7e42                  jle       short 00441A4Fh
  00441a0d  668b8698000000        mov       ax,[esi+98h]
  00441a14  663d3200              cmp       ax,32h
  00441a18  7d35                  jge       short 00441A4Fh
  00441a1a  0fbfc0                movsx     eax,ax
  00441a1d  6800100000            push      1000h
  00441a22  6800100000            push      1000h
  00441a27  0fbf4c4418            movsx     ecx,word [esp+eax*2+18h]
  00441a2c  51                    push      ecx
  00441a2d  6a31                  push      31h
  00441a2f  e86cb40f00            call      0053CEA0h
  00441a34  83c410                add       esp,10h
  00441a37  66ff8698000000        inc       word [esi+98h]
  00441a3e  6683be980000001e      cmp       word [esi+98h],1Eh
  00441a46  7507                  jne       short 00441A4Fh
  00441a48  6689ae98000000        mov       [esi+98h],bp
  00441a4f  66ff4616              inc       word [esi+16h]
  00441a53  33d2                  xor       edx,edx
  00441a55  66395616              cmp       [esi+16h],dx
  00441a59  7c2e                  jl        short 00441A89h
  00441a5b  b9e2ffffff            mov       ecx,0FFFFFFE2h
  00441a60  66014f28              add       [edi+28h],cx
  00441a64  66014f2a              add       [edi+2Ah],cx
  00441a68  66014f2c              add       [edi+2Ch],cx
  00441a6c  8b4f20                mov       ecx,[edi+20h]
  00441a6f  668b4728              mov       ax,[edi+28h]
  00441a73  03cd                  add       ecx,ebp
  00441a75  663bc2                cmp       ax,dx
  00441a78  894f20                mov       [edi+20h],ecx
  00441a7b  7d0c                  jge       short 00441A89h
  00441a7d  66895728              mov       [edi+28h],dx
  00441a81  6689572a              mov       [edi+2Ah],dx
  00441a85  6689572c              mov       [edi+2Ch],dx
  00441a89  bd96000000            mov       ebp,96h
  00441a8e  66396e16              cmp       [esi+16h],bp
  00441a92  7f13                  jg        short 00441AA7h
  00441a94  6aff                  push      0FFFFFFFFh
  00441a96  52                    push      edx
  00441a97  68c7000000            push      0C7h
  00441a9c  56                    push      esi
  00441a9d  e83e900300            call      0047AAE0h
  00441aa2  83c410                add       esp,10h
  00441aa5  33d2                  xor       edx,edx
  00441aa7  668b4616              mov       ax,[esi+16h]
  00441aab  663bc5                cmp       ax,bp
  00441aae  7c1b                  jl        short 00441ACBh
  00441ab0  663daa00              cmp       ax,0AAh
  00441ab4  7f15                  jg        short 00441ACBh
  00441ab6  84c3                  test      bl,al
  00441ab8  7511                  jne       short 00441ACBh
  00441aba  6aff                  push      0FFFFFFFFh
  00441abc  52                    push      edx
  00441abd  68c8000000            push      0C8h
  00441ac2  56                    push      esi
  00441ac3  e818900300            call      0047AAE0h
  00441ac8  83c410                add       esp,10h
  00441acb  66396e16              cmp       [esi+16h],bp
  00441acf  7519                  jne       short 00441AEAh
  00441ad1  6800100000            push      1000h
  00441ad6  6800100000            push      1000h
  00441adb  6820030000            push      320h
  00441ae0  6a25                  push      25h
  00441ae2  e8b9b30f00            call      0053CEA0h
  00441ae7  83c410                add       esp,10h
  00441aea  66817e169700          cmp       word [esi+16h],97h
  00441af0  7519                  jne       short 00441B0Bh
  00441af2  6800100000            push      1000h
  00441af7  6800100000            push      1000h
  00441afc  6800040000            push      400h
  00441b01  6a25                  push      25h
  00441b03  e898b30f00            call      0053CEA0h
  00441b08  83c410                add       esp,10h
  00441b0b  845e16                test      [esi+16h],bl
  00441b0e  7524                  jne       short 00441B34h
  00441b10  8b542450              mov       edx,[esp+50h]
  00441b14  66ff471a              inc       word [edi+1Ah]
  00441b18  668b4f1a              mov       cx,[edi+1Ah]
  00441b1c  668b4216              mov       ax,[edx+16h]
  00441b20  8bd0                  mov       edx,eax
  00441b22  0fbfc9                movsx     ecx,cx
  00441b25  81e2ffff0000          and       edx,0FFFFh
  00441b2b  3bca                  cmp       ecx,edx
  00441b2d  7c05                  jl        short 00441B34h
  00441b2f  48                    dec       eax
  00441b30  6689471a              mov       [edi+1Ah],ax
  00441b34  66817e16f000          cmp       word [esi+16h],0F0h
  00441b3a  7c3a                  jl        short 00441B76h
  00441b3c  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00441b42  e8691c0c00            call      005037B0h
  00441b47  33c0                  xor       eax,eax
  00441b49  8b0dcc975e00          mov       ecx,[5E97CCh]
  00441b4f  8a4709                mov       al,[edi+9]
  00441b52  6a00                  push      0
  00441b54  8d1480                lea       edx,[eax+eax*4]
  00441b57  c1e203                shl       edx,3
  00441b5a  2bd0                  sub       edx,eax
  00441b5c  8d0495c0935e00        lea       eax,[edx*4+5E93C0h]
  00441b63  50                    push      eax
  00441b64  51                    push      ecx
  00441b65  e866220d00            call      00513DD0h
  00441b6a  8b07                  mov       eax,[edi]
  00441b6c  83c40c                add       esp,0Ch
  00441b6f  0d00400002            or        eax,2004000h
  00441b74  8907                  mov       [edi],eax
  00441b76  5f                    pop       edi
  00441b77  5e                    pop       esi
  00441b78  5d                    pop       ebp
  00441b79  5b                    pop       ebx
  00441b7a  83c43c                add       esp,3Ch
  00441b7d  c3                    ret
