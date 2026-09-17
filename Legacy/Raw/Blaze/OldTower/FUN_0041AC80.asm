C:\Program Files (x86)\dotnet\sdk\9.0.317\Sdks\Microsoft.NET.Sdk\targets\Microsoft.NET.RuntimeIdentifierInference.targets(278,5): warning NETSDK1189: Prefer32Bit n’est pas pris en charge et n’a aucun effet pour la cible netcoreapp. [D:\Open Foresia Project\GitHub Project\OpenForesia\OpenForesia.Tools\OpenForesia.Tools.csproj]
Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0041ac80  section .text  offset fichier 0x1a080  720 octets

  0041ac80  81eca0000000          sub       esp,0A0h
  0041ac86  53                    push      ebx
  0041ac87  55                    push      ebp
  0041ac88  56                    push      esi
  0041ac89  57                    push      edi
  0041ac8a  b926000000            mov       ecx,26h
  0041ac8f  33c0                  xor       eax,eax
  0041ac91  8d7c2418              lea       edi,[esp+18h]
  0041ac95  c744241400000000      mov       dword [esp+14h],0
  0041ac9d  f3ab                  rep stosd
  0041ac9f  8d7c2414              lea       edi,[esp+14h]
  0041aca3  e8a8020000            call      0041AF50h
  0041aca8  e89339ffff            call      0040E640h
  0041acad  8bb424b4000000        mov       esi,[esp+0B4h]
  0041acb4  8b5c2410              mov       ebx,[esp+10h]
  0041acb8  b8a0ca5e00            mov       eax,5ECAA0h
  0041acbd  bd20000000            mov       ebp,20h
  0041acc2  b909000000            mov       ecx,9

LAB_0041acc7:
  0041acc7  80780c01              cmp       byte [eax+0Ch],1
  0041accb  7536                  jne       short 0041AD03h
  0041accd  f70000000080          test      dword [eax],80000000h
  0041acd3  742e                  je        short 0041AD03h
  0041acd5  8b501c                mov       edx,[eax+1Ch]
  0041acd8  8bf8                  mov       edi,eax
  0041acda  89561c                mov       [esi+1Ch],edx
  0041acdd  8b5020                mov       edx,[eax+20h]
  0041ace0  895620                mov       [esi+20h],edx
  0041ace3  8b5024                mov       edx,[eax+24h]
  0041ace6  895624                mov       [esi+24h],edx
  0041ace9  33d2                  xor       edx,edx
  0041aceb  8a5004                mov       dl,[eax+4]
  0041acee  66894818              mov       [eax+18h],cx
  0041acf2  8b149540965e00        mov       edx,[edx*4+5E9640h]
  0041acf9  8b5a24                mov       ebx,[edx+24h]
  0041acfc  8b10                  mov       edx,[eax]
  0041acfe  80ce10                or        dh,10h
  0041ad01  8910                  mov       [eax],edx

LAB_0041ad03:
  0041ad03  059c000000            add       eax,9Ch
  0041ad08  4d                    dec       ebp
  0041ad09  75bc                  jne       short 0041ACC7h
  0041ad0b  33ed                  xor       ebp,ebp
  0041ad0d  b801000000            mov       eax,1
  0041ad12  66396e10              cmp       [esi+10h],bp
  0041ad16  7557                  jne       short 0041AD6Fh
  0041ad18  66894610              mov       [esi+10h],ax
  0041ad1c  66896e12              mov       [esi+12h],bp
  0041ad20  66896e14              mov       [esi+14h],bp
  0041ad24  66c74616ceff          mov       word [esi+16h],0FFCEh
  0041ad2a  0fbf0db8925e00        movsx     ecx,word [5E92B8h]
  0041ad31  894e50                mov       [esi+50h],ecx
  0041ad34  0fbf15ba925e00        movsx     edx,word [5E92BAh]
  0041ad3b  895654                mov       [esi+54h],edx
  0041ad3e  0fbf0dbc925e00        movsx     ecx,word [5E92BCh]
  0041ad45  894e58                mov       [esi+58h],ecx
  0041ad48  66896f1a              mov       [edi+1Ah],bp
  0041ad4c  c6470a00              mov       byte [edi+0Ah],0
  0041ad50  c6460b00              mov       byte [esi+0Bh],0
  0041ad54  8a5709                mov       dl,[edi+9]
  0041ad57  c74738ffffff00        mov       dword [edi+38h],0FFFFFFh
  0041ad5e  66894736              mov       [edi+36h],ax
  0041ad62  8815a8145700          mov       [5714A8h],dl
  0041ad68  6689ae98000000        mov       [esi+98h],bp

LAB_0041ad6f:
  0041ad6f  66394610              cmp       [esi+10h],ax
  0041ad73  0f85c5010000          jne       near 0041AF3Eh
  0041ad79  b8e6000000            mov       eax,0E6h
  0041ad7e  66c70566915e000200    mov       word [5E9166h],2
  0041ad87  66a360915e00          mov       [5E9160h],ax
  0041ad8d  66a362915e00          mov       [5E9162h],ax
  0041ad93  66a364915e00          mov       [5E9164h],ax
  0041ad99  66ff4612              inc       word [esi+12h]
  0041ad9d  668b4612              mov       ax,[esi+12h]
  0041ada1  66ff4616              inc       word [esi+16h]
  0041ada5  668b4e16              mov       cx,[esi+16h]
  0041ada9  663d0200              cmp       ax,2
  0041adad  7c24                  jl        short 0041ADD3h
  0041adaf  6683f928              cmp       cx,28h
  0041adb3  7d1e                  jge       short 0041ADD3h
  0041adb5  663bcd                cmp       cx,bp
  0041adb8  7e19                  jle       short 0041ADD3h
  0041adba  66ff4614              inc       word [esi+14h]
  0041adbe  6aff                  push      0FFFFFFFFh
  0041adc0  55                    push      ebp
  0041adc1  68c1000000            push      0C1h
  0041adc6  56                    push      esi
  0041adc7  66896e12              mov       [esi+12h],bp
  0041adcb  e810fd0500            call      0047AAE0h
  0041add0  83c410                add       esp,10h

LAB_0041add3:
  0041add3  66837e1628            cmp       word [esi+16h],28h
  0041add8  7511                  jne       short 0041ADEBh
  0041adda  6aff                  push      0FFFFFFFFh
  0041addc  55                    push      ebp
  0041addd  68c1000000            push      0C1h
  0041ade2  56                    push      esi
  0041ade3  e8f8fc0500            call      0047AAE0h
  0041ade8  83c410                add       esp,10h

LAB_0041adeb:
  0041adeb  668b4616              mov       ax,[esi+16h]
  0041adef  663d2800              cmp       ax,28h
  0041adf3  7d3f                  jge       short 0041AE34h
  0041adf5  663bc5                cmp       ax,bp
  0041adf8  7e3a                  jle       short 0041AE34h
  0041adfa  6639ae98000000        cmp       [esi+98h],bp
  0041ae01  7519                  jne       short 0041AE1Ch
  0041ae03  6800080000            push      800h
  0041ae08  6800080000            push      800h
  0041ae0d  68f41b0000            push      1BF4h
  0041ae12  6a20                  push      20h
  0041ae14  e887201200            call      0053CEA0h
  0041ae19  83c410                add       esp,10h

LAB_0041ae1c:
  0041ae1c  66ff8698000000        inc       word [esi+98h]
  0041ae23  6683be9800000003      cmp       word [esi+98h],3
  0041ae2b  7507                  jne       short 0041AE34h
  0041ae2d  6689ae98000000        mov       [esi+98h],bp

LAB_0041ae34:
  0041ae34  66837e1614            cmp       word [esi+16h],14h
  0041ae39  754b                  jne       short 0041AE86h
  0041ae3b  6800060000            push      600h
  0041ae40  6800060000            push      600h
  0041ae45  6800010000            push      100h
  0041ae4a  6a30                  push      30h
  0041ae4c  e84f201200            call      0053CEA0h
  0041ae51  83c410                add       esp,10h
  0041ae54  6800060000            push      600h
  0041ae59  6800060000            push      600h
  0041ae5e  6800010000            push      100h
  0041ae63  6a2d                  push      2Dh
  0041ae65  e836201200            call      0053CEA0h
  0041ae6a  83c410                add       esp,10h
  0041ae6d  6800060000            push      600h
  0041ae72  6800060000            push      600h
  0041ae77  6800020000            push      200h
  0041ae7c  6a10                  push      10h
  0041ae7e  e81d201200            call      0053CEA0h
  0041ae83  83c410                add       esp,10h

LAB_0041ae86:
  0041ae86  66837e1664            cmp       word [esi+16h],64h
  0041ae8b  7e29                  jle       short 0041AEB6h
  0041ae8d  668b4650              mov       ax,[esi+50h]
  0041ae91  66a360915e00          mov       [5E9160h],ax
  0041ae97  668b4e54              mov       cx,[esi+54h]
  0041ae9b  66890d62915e00        mov       [5E9162h],cx
  0041aea2  668b5658              mov       dx,[esi+58h]
  0041aea6  66891564915e00        mov       [5E9164h],dx
  0041aead  66c70566915e000200    mov       word [5E9166h],2

LAB_0041aeb6:
  0041aeb6  66817e168c00          cmp       word [esi+16h],8Ch
  0041aebc  7c3d                  jl        short 0041AEFBh
  0041aebe  8126ffffff7f          and       dword [esi],7FFFFFFFh
  0041aec4  e8e7880e00            call      005037B0h
  0041aec9  a1a8145700            mov       eax,[5714A8h]
  0041aece  55                    push      ebp
  0041aecf  25ff000000            and       eax,0FFh
  0041aed4  8d0c80                lea       ecx,[eax+eax*4]
  0041aed7  c1e103                shl       ecx,3
  0041aeda  2bc8                  sub       ecx,eax
  0041aedc  a1cc975e00            mov       eax,[5E97CCh]
  0041aee1  8d148dc0935e00        lea       edx,[ecx*4+5E93C0h]
  0041aee8  52                    push      edx
  0041aee9  50                    push      eax
  0041aeea  e8e18e0f00            call      00513DD0h
  0041aeef  8b07                  mov       eax,[edi]
  0041aef1  83c40c                add       esp,0Ch
  0041aef4  0d00400002            or        eax,2004000h
  0041aef9  8907                  mov       [edi],eax

LAB_0041aefb:
  0041aefb  f6461601              test      byte [esi+16h],1
  0041aeff  7528                  jne       short 0041AF29h
  0041af01  66ff471a              inc       word [edi+1Ah]
  0041af05  668b4f1a              mov       cx,[edi+1Ah]
  0041af09  668b4316              mov       ax,[ebx+16h]
  0041af0d  8bd0                  mov       edx,eax
  0041af0f  0fbfc9                movsx     ecx,cx
  0041af12  81e2ffff0000          and       edx,0FFFFh
  0041af18  3bca                  cmp       ecx,edx
  0041af1a  7c0d                  jl        short 0041AF29h
  0041af1c  48                    dec       eax
  0041af1d  6689471a              mov       [edi+1Ah],ax
  0041af21  8a473b                mov       al,[edi+3Bh]
  0041af24  0c02                  or        al,2
  0041af26  88473b                mov       [edi+3Bh],al

LAB_0041af29:
  0041af29  66396e16              cmp       [esi+16h],bp
  0041af2d  7e0f                  jle       short 0041AF3Eh
  0041af2f  6afc                  push      0FFFFFFFCh
  0041af31  6afc                  push      0FFFFFFFCh
  0041af33  6afc                  push      0FFFFFFFCh
  0041af35  57                    push      edi
  0041af36  e8a59efeff            call      00404DE0h
  0041af3b  83c410                add       esp,10h

LAB_0041af3e:
  0041af3e  5f                    pop       edi
  0041af3f  5e                    pop       esi
  0041af40  5d                    pop       ebp
  0041af41  5b                    pop       ebx
  0041af42  81c4a0000000          add       esp,0A0h
  0041af48  c3                    ret
  0041af49  90                    nop
  0041af4a  90                    nop
  0041af4b  90                    nop
  0041af4c  90                    nop
  0041af4d  90                    nop
  0041af4e  90                    nop
  0041af4f  90                    nop

12 cible(s) de saut interne : 0x0041acc7, 0x0041ad03, 0x0041ad6f, 0x0041add3, 0x0041adeb, 0x0041ae1c, 0x0041ae34, 0x0041ae86, 0x0041aeb6, 0x0041aefb, 0x0041af29, 0x0041af3e
