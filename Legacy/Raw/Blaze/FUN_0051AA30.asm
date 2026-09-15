Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0051aa30  section .text  offset fichier 0x119e30  3616 octets

  0051aa30  83ec08                sub       esp,8
  0051aa33  b818000000            mov       eax,18h
  0051aa38  53                    push      ebx
  0051aa39  55                    push      ebp
  0051aa3a  56                    push      esi
  0051aa3b  8b742420              mov       esi,[esp+20h]
  0051aa3f  8b6c2418              mov       ebp,[esp+18h]
  0051aa43  33db                  xor       ebx,ebx
  0051aa45  66894608              mov       [esi+8],ax
  0051aa49  6689460a              mov       [esi+0Ah],ax
  0051aa4d  c706286c5600          mov       dword [esi],566C28h
  0051aa53  c7460400000001        mov       dword [esi+4],1000000h
  0051aa5a  a170335600            mov       eax,[563370h]
  0051aa5f  57                    push      edi
  0051aa60  803800                cmp       byte [eax],0
  0051aa63  0f84a40d0000          je        near 0051B80Dh
  0051aa69  8b542420              mov       edx,[esp+20h]
  0051aa6d  bf70335600            mov       edi,563370h

LAB_0051aa72:
  0051aa72  6aff                  push      0FFFFFFFFh
  0051aa74  6a00                  push      0
  0051aa76  6a00                  push      0
  0051aa78  50                    push      eax
  0051aa79  56                    push      esi
  0051aa7a  52                    push      edx
  0051aa7b  55                    push      ebp
  0051aa7c  e89f160000            call      0051C120h
  0051aa81  6683460a0b            add       word [esi+0Ah],0Bh
  0051aa86  83c704                add       edi,4
  0051aa89  8bd0                  mov       edx,eax
  0051aa8b  83c41c                add       esp,1Ch
  0051aa8e  8b07                  mov       eax,[edi]
  0051aa90  43                    inc       ebx
  0051aa91  803800                cmp       byte [eax],0
  0051aa94  75dc                  jne       short 0051AA72h
  0051aa96  89542420              mov       [esp+20h],edx

LAB_0051aa9a:
  0051aa9a  8b4c2428              mov       ecx,[esp+28h]
  0051aa9e  bf00800000            mov       edi,8000h
  0051aaa3  668539                test      [ecx],di
  0051aaa6  7536                  jne       short 0051AADEh
  0051aaa8  f6410610              test      byte [ecx+6],10h
  0051aaac  a11ce05d00            mov       eax,[5DE01Ch]
  0051aab1  7406                  je        short 0051AAB9h
  0051aab3  48                    dec       eax
  0051aab4  a31ce05d00            mov       [5DE01Ch],eax

LAB_0051aab9:
  0051aab9  f6410640              test      byte [ecx+6],40h
  0051aabd  7406                  je        short 0051AAC5h
  0051aabf  40                    inc       eax
  0051aac0  a31ce05d00            mov       [5DE01Ch],eax

LAB_0051aac5:
  0051aac5  85c0                  test      eax,eax
  0051aac7  7d08                  jge       short 0051AAD1h
  0051aac9  8d43ff                lea       eax,[ebx-1]
  0051aacc  a31ce05d00            mov       [5DE01Ch],eax

LAB_0051aad1:
  0051aad1  3bc3                  cmp       eax,ebx
  0051aad3  7c0e                  jl        short 0051AAE3h
  0051aad5  33c0                  xor       eax,eax
  0051aad7  a31ce05d00            mov       [5DE01Ch],eax
  0051aadc  eb05                  jmp       short 0051AAE3h

LAB_0051aade:
  0051aade  a11ce05d00            mov       eax,[5DE01Ch]

LAB_0051aae3:
  0051aae3  8b0d18e05d00          mov       ecx,[5DE018h]
  0051aae9  85c9                  test      ecx,ecx
  0051aaeb  0f85120d0000          jne       near 0051B803h
  0051aaf1  83f80c                cmp       eax,0Ch
  0051aaf4  b901000000            mov       ecx,1
  0051aaf9  0f87a9090000          ja        near 0051B4A8h
  0051aaff  ff248518b85100        jmp       dword [eax*4+51B818h]
  0051ab06  8b5c2428              mov       ebx,[esp+28h]
  0051ab0a  f6430680              test      byte [ebx+6],80h
  0051ab0e  a014bc5d00            mov       al,[5DBC14h]
  0051ab13  7407                  je        short 0051AB1Ch
  0051ab15  fec8                  dec       al
  0051ab17  a214bc5d00            mov       [5DBC14h],al

LAB_0051ab1c:
  0051ab1c  f6430620              test      byte [ebx+6],20h
  0051ab20  7407                  je        short 0051AB29h
  0051ab22  fec0                  inc       al
  0051ab24  a214bc5d00            mov       [5DBC14h],al

LAB_0051ab29:
  0051ab29  8a0d60975e00          mov       cl,[5E9760h]
  0051ab2f  3cff                  cmp       al,0FFh
  0051ab31  7509                  jne       short 0051AB3Ch
  0051ab33  8ac1                  mov       al,cl
  0051ab35  fec8                  dec       al
  0051ab37  a214bc5d00            mov       [5DBC14h],al

LAB_0051ab3c:
  0051ab3c  3ac1                  cmp       al,cl
  0051ab3e  7207                  jb        short 0051AB47h
  0051ab40  32c0                  xor       al,al
  0051ab42  a214bc5d00            mov       [5DBC14h],al

LAB_0051ab47:
  0051ab47  3a0528e55d00          cmp       al,[5DE528h]
  0051ab4d  7407                  je        short 0051AB56h
  0051ab4f  c605d0df5d0000        mov       byte [5DDFD0h],0

LAB_0051ab56:
  0051ab56  66857b02              test      [ebx+2],di
  0051ab5a  0f8448090000          je        near 0051B4A8h
  0051ab60  a228e55d00            mov       [5DE528h],al
  0051ab65  c605846c5600ff        mov       byte [566C84h],0FFh
  0051ab6c  c6052ce55d0000        mov       byte [5DE52Ch],0
  0051ab73  eb62                  jmp       short 0051ABD7h
  0051ab75  8b7c2428              mov       edi,[esp+28h]
  0051ab79  f6470680              test      byte [edi+6],80h
  0051ab7d  a0d0df5d00            mov       al,[5DDFD0h]
  0051ab82  7407                  je        short 0051AB8Bh
  0051ab84  fec8                  dec       al
  0051ab86  a2d0df5d00            mov       [5DDFD0h],al

LAB_0051ab8b:
  0051ab8b  f6470620              test      byte [edi+6],20h
  0051ab8f  7407                  je        short 0051AB98h
  0051ab91  fec0                  inc       al
  0051ab93  a2d0df5d00            mov       [5DDFD0h],al

LAB_0051ab98:
  0051ab98  f64706a0              test      byte [edi+6],0A0h
  0051ab9c  740c                  je        short 0051ABAAh
  0051ab9e  8a0d28e55d00          mov       cl,[5DE528h]
  0051aba4  880d14bc5d00          mov       [5DBC14h],cl

LAB_0051abaa:
  0051abaa  8a0d447b5f00          mov       cl,[5F7B44h]
  0051abb0  3cff                  cmp       al,0FFh
  0051abb2  7509                  jne       short 0051ABBDh
  0051abb4  8ac1                  mov       al,cl
  0051abb6  fec8                  dec       al
  0051abb8  a2d0df5d00            mov       [5DDFD0h],al

LAB_0051abbd:
  0051abbd  3ac1                  cmp       al,cl
  0051abbf  7207                  jb        short 0051ABC8h
  0051abc1  32c0                  xor       al,al
  0051abc3  a2d0df5d00            mov       [5DDFD0h],al

LAB_0051abc8:
  0051abc8  f6470380              test      byte [edi+3],80h
  0051abcc  0f84d6080000          je        near 0051B4A8h
  0051abd2  a22ce55d00            mov       [5DE52Ch],al

LAB_0051abd7:
  0051abd7  a038e55d00            mov       al,[5DE538h]
  0051abdc  c605886c5600ff        mov       byte [566C88h],0FFh
  0051abe3  a23ce55d00            mov       [5DE53Ch],al
  0051abe8  a12ce05d00            mov       eax,[5DE02Ch]
  0051abed  0c02                  or        al,2
  0051abef  c60530e55d0000        mov       byte [5DE530h],0
  0051abf6  c605f845560002        mov       byte [5645F8h],2
  0051abfd  a32ce05d00            mov       [5DE02Ch],eax
  0051ac02  e9a1080000            jmp       0051B4A8h
  0051ac07  8b442428              mov       eax,[esp+28h]
  0051ac0b  f6400680              test      byte [eax+6],80h
  0051ac0f  7406                  je        short 0051AC17h
  0051ac11  fe0d18bc5d00          dec       byte [5DBC18h]

LAB_0051ac17:
  0051ac17  f6400620              test      byte [eax+6],20h
  0051ac1b  7406                  je        short 0051AC23h
  0051ac1d  fe0518bc5d00          inc       byte [5DBC18h]

LAB_0051ac23:
  0051ac23  a018bc5d00            mov       al,[5DBC18h]
  0051ac28  84c0                  test      al,al
  0051ac2a  7507                  jne       short 0051AC33h
  0051ac2c  b004                  mov       al,4
  0051ac2e  a218bc5d00            mov       [5DBC18h],al

LAB_0051ac33:
  0051ac33  3c04                  cmp       al,4
  0051ac35  7607                  jbe       short 0051AC3Eh
  0051ac37  c60518bc5d0001        mov       byte [5DBC18h],1

LAB_0051ac3e:
  0051ac3e  8b3d18bc5d00          mov       edi,[5DBC18h]
  0051ac44  a120905e00            mov       eax,[5E9020h]
  0051ac49  81e7ff000000          and       edi,0FFh
  0051ac4f  3bc7                  cmp       eax,edi
  0051ac51  0f8451080000          je        near 0051B4A8h
  0051ac57  33c0                  xor       eax,eax
  0051ac59  893d20905e00          mov       [5E9020h],edi
  0051ac5f  a038e55d00            mov       al,[5DE538h]
  0051ac64  bb01000000            mov       ebx,1
  0051ac69  c7442428d4915e00      mov       dword [esp+28h],5E91D4h
  0051ac71  bf60945e00            mov       edi,5E9460h
  0051ac76  8d0cc0                lea       ecx,[eax+eax*8]
  0051ac79  8d0448                lea       eax,[eax+ecx*2]
  0051ac7c  33c9                  xor       ecx,ecx
  0051ac7e  c1e002                shl       eax,2
  0051ac81  8a8889915e00          mov       cl,[eax+5E9189h]
  0051ac87  894c2410              mov       [esp+10h],ecx
  0051ac8b  33c9                  xor       ecx,ecx
  0051ac8d  8a8888915e00          mov       cl,[eax+5E9188h]
  0051ac93  894c2414              mov       [esp+14h],ecx

LAB_0051ac97:
  0051ac97  33c0                  xor       eax,eax
  0051ac99  8d6ffc                lea       ebp,[edi-4]
  0051ac9c  8a07                  mov       al,[edi]
  0051ac9e  6a27                  push      27h
  0051aca0  55                    push      ebp
  0051aca1  68c0935e00            push      5E93C0h
  0051aca6  89442430              mov       [esp+30h],eax
  0051acaa  e831a60100            call      005352E0h
  0051acaf  8b542434              mov       edx,[esp+34h]
  0051acb3  83c40c                add       esp,0Ch
  0051acb6  83c2f8                add       edx,0FFFFFFF8h
  0051acb9  6a13                  push      13h
  0051acbb  52                    push      edx
  0051acbc  6880915e00            push      5E9180h
  0051acc1  e81aa60100            call      005352E0h
  0051acc6  8a442430              mov       al,[esp+30h]
  0051acca  c6470500              mov       byte [edi+5],0
  0051acce  8b0d20905e00          mov       ecx,[5E9020h]
  0051acd4  8807                  mov       [edi],al
  0051acd6  8b442434              mov       eax,[esp+34h]
  0051acda  885f07                mov       [edi+7],bl
  0051acdd  885f04                mov       [edi+4],bl
  0051ace0  83c40c                add       esp,0Ch
  0051ace3  c60008                mov       byte [eax],8
  0051ace6  885801                mov       [eax+1],bl
  0051ace9  8b4500                mov       eax,[ebp]
  0051acec  0d00000100            or        eax,10000h
  0051acf1  3bd9                  cmp       ebx,ecx
  0051acf3  894500                mov       [ebp],eax
  0051acf6  7c08                  jl        short 0051AD00h
  0051acf8  25ffffff7f            and       eax,7FFFFFFFh
  0051acfd  894500                mov       [ebp],eax

LAB_0051ad00:
  0051ad00  8b442428              mov       eax,[esp+28h]
  0051ad04  43                    inc       ebx
  0051ad05  83c04c                add       eax,4Ch
  0051ad08  81c79c000000          add       edi,9Ch
  0051ad0e  3db8925e00            cmp       eax,5E92B8h
  0051ad13  89442428              mov       [esp+28h],eax
  0051ad17  0f8c7affffff          jl        near 0051AC97h
  0051ad1d  8a542414              mov       dl,[esp+14h]
  0051ad21  8a4c2410              mov       cl,[esp+10h]
  0051ad25  8b6c241c              mov       ebp,[esp+1Ch]
  0051ad29  881588915e00          mov       [5E9188h],dl
  0051ad2f  8b542420              mov       edx,[esp+20h]
  0051ad33  880d89915e00          mov       [5E9189h],cl
  0051ad39  c60538e55d0000        mov       byte [5DE538h],0
  0051ad40  c60540e55d0000        mov       byte [5DE540h],0
  0051ad47  e95c070000            jmp       0051B4A8h
  0051ad4c  8b4c2428              mov       ecx,[esp+28h]
  0051ad50  668539                test      [ecx],di
  0051ad53  8a4106                mov       al,[ecx+6]
  0051ad56  0f84bf000000          je        near 0051AE1Bh
  0051ad5c  a880                  test      al,80h
  0051ad5e  7406                  je        short 0051AD66h
  0051ad60  fe0524bc5d00          inc       byte [5DBC24h]

LAB_0051ad66:
  0051ad66  f6410620              test      byte [ecx+6],20h
  0051ad6a  7406                  je        short 0051AD72h
  0051ad6c  fe0d24bc5d00          dec       byte [5DBC24h]

LAB_0051ad72:
  0051ad72  f6410210              test      byte [ecx+2],10h
  0051ad76  a188df5d00            mov       eax,[5DDF88h]
  0051ad7b  7428                  je        short 0051ADA5h
  0051ad7d  8bc8                  mov       ecx,eax
  0051ad7f  b301                  mov       bl,1
  0051ad81  81e1ff000000          and       ecx,0FFh
  0051ad87  8db9e0965e00          lea       edi,[ecx+5E96E0h]
  0051ad8d  8b0d24bc5d00          mov       ecx,[5DBC24h]
  0051ad93  81e1ff000000          and       ecx,0FFh
  0051ad99  d2e3                  shl       bl,cl
  0051ad9b  8a0f                  mov       cl,[edi]
  0051ad9d  0acb                  or        cl,bl
  0051ad9f  880f                  mov       [edi],cl
  0051ada1  8b4c2428              mov       ecx,[esp+28h]

LAB_0051ada5:
  0051ada5  f6410240              test      byte [ecx+2],40h
  0051ada9  7423                  je        short 0051ADCEh
  0051adab  8b0d24bc5d00          mov       ecx,[5DBC24h]
  0051adb1  25ff000000            and       eax,0FFh
  0051adb6  81e1ff000000          and       ecx,0FFh
  0051adbc  8db8e0965e00          lea       edi,[eax+5E96E0h]
  0051adc2  b001                  mov       al,1
  0051adc4  d2e0                  shl       al,cl
  0051adc6  8a0f                  mov       cl,[edi]
  0051adc8  f6d0                  not       al
  0051adca  22c8                  and       cl,al
  0051adcc  880f                  mov       [edi],cl

LAB_0051adce:
  0051adce  a024bc5d00            mov       al,[5DBC24h]
  0051add3  6633c9                xor       cx,cx
  0051add6  2407                  and       al,7
  0051add8  6aff                  push      0FFFFFFFFh
  0051adda  a224bc5d00            mov       [5DBC24h],al
  0051addf  8ac8                  mov       cl,al
  0051ade1  b815000000            mov       eax,15h
  0051ade6  6a00                  push      0
  0051ade8  2bc1                  sub       eax,ecx
  0051adea  6a00                  push      0
  0051adec  c1e003                shl       eax,3
  0051adef  66894608              mov       [esi+8],ax
  0051adf3  a11ce05d00            mov       eax,[5DE01Ch]
  0051adf8  68304a5600            push      564A30h
  0051adfd  56                    push      esi
  0051adfe  8d0c80                lea       ecx,[eax+eax*4]
  0051ae01  52                    push      edx
  0051ae02  55                    push      ebp
  0051ae03  8d444810              lea       eax,[eax+ecx*2+10h]
  0051ae07  6689460a              mov       [esi+0Ah],ax
  0051ae0b  e810130000            call      0051C120h
  0051ae10  8b4c2444              mov       ecx,[esp+44h]
  0051ae14  83c41c                add       esp,1Ch
  0051ae17  8bd0                  mov       edx,eax
  0051ae19  eb31                  jmp       short 0051AE4Ch

LAB_0051ae1b:
  0051ae1b  a880                  test      al,80h
  0051ae1d  7406                  je        short 0051AE25h
  0051ae1f  fe0d88df5d00          dec       byte [5DDF88h]

LAB_0051ae25:
  0051ae25  f6410620              test      byte [ecx+6],20h
  0051ae29  7406                  je        short 0051AE31h
  0051ae2b  fe0588df5d00          inc       byte [5DDF88h]

LAB_0051ae31:
  0051ae31  a088df5d00            mov       al,[5DDF88h]
  0051ae36  3cff                  cmp       al,0FFh
  0051ae38  7507                  jne       short 0051AE41h
  0051ae3a  b03f                  mov       al,3Fh
  0051ae3c  a288df5d00            mov       [5DDF88h],al

LAB_0051ae41:
  0051ae41  3c40                  cmp       al,40h
  0051ae43  7207                  jb        short 0051AE4Ch
  0051ae45  c60588df5d0000        mov       byte [5DDF88h],0

LAB_0051ae4c:
  0051ae4c  f6410340              test      byte [ecx+3],40h
  0051ae50  0f8452060000          je        near 0051B4A8h
  0051ae56  b910000000            mov       ecx,10h
  0051ae5b  33c0                  xor       eax,eax
  0051ae5d  bfe0965e00            mov       edi,5E96E0h
  0051ae62  f3ab                  rep stosd
  0051ae64  e93f060000            jmp       0051B4A8h
  0051ae69  8b4c2428              mov       ecx,[esp+28h]
  0051ae6d  668539                test      [ecx],di
  0051ae70  8a4106                mov       al,[ecx+6]
  0051ae73  0f84bf000000          je        near 0051AF38h
  0051ae79  a880                  test      al,80h
  0051ae7b  7406                  je        short 0051AE83h
  0051ae7d  fe0524bc5d00          inc       byte [5DBC24h]

LAB_0051ae83:
  0051ae83  f6410620              test      byte [ecx+6],20h
  0051ae87  7406                  je        short 0051AE8Fh
  0051ae89  fe0d24bc5d00          dec       byte [5DBC24h]

LAB_0051ae8f:
  0051ae8f  f6410210              test      byte [ecx+2],10h
  0051ae93  a120bc5d00            mov       eax,[5DBC20h]
  0051ae98  7428                  je        short 0051AEC2h
  0051ae9a  8bc8                  mov       ecx,eax
  0051ae9c  b301                  mov       bl,1
  0051ae9e  81e1ff000000          and       ecx,0FFh
  0051aea4  8db980975e00          lea       edi,[ecx+5E9780h]
  0051aeaa  8b0d24bc5d00          mov       ecx,[5DBC24h]
  0051aeb0  81e1ff000000          and       ecx,0FFh
  0051aeb6  d2e3                  shl       bl,cl
  0051aeb8  8a0f                  mov       cl,[edi]
  0051aeba  0acb                  or        cl,bl
  0051aebc  880f                  mov       [edi],cl
  0051aebe  8b4c2428              mov       ecx,[esp+28h]

LAB_0051aec2:
  0051aec2  f6410240              test      byte [ecx+2],40h
  0051aec6  7423                  je        short 0051AEEBh
  0051aec8  8b0d24bc5d00          mov       ecx,[5DBC24h]
  0051aece  25ff000000            and       eax,0FFh
  0051aed3  81e1ff000000          and       ecx,0FFh
  0051aed9  8db880975e00          lea       edi,[eax+5E9780h]
  0051aedf  b001                  mov       al,1
  0051aee1  d2e0                  shl       al,cl
  0051aee3  8a0f                  mov       cl,[edi]
  0051aee5  f6d0                  not       al
  0051aee7  22c8                  and       cl,al
  0051aee9  880f                  mov       [edi],cl

LAB_0051aeeb:
  0051aeeb  a024bc5d00            mov       al,[5DBC24h]
  0051aef0  6633c9                xor       cx,cx
  0051aef3  2407                  and       al,7
  0051aef5  6aff                  push      0FFFFFFFFh
  0051aef7  a224bc5d00            mov       [5DBC24h],al
  0051aefc  8ac8                  mov       cl,al
  0051aefe  b815000000            mov       eax,15h
  0051af03  6a00                  push      0
  0051af05  2bc1                  sub       eax,ecx
  0051af07  6a00                  push      0
  0051af09  c1e003                shl       eax,3
  0051af0c  66894608              mov       [esi+8],ax
  0051af10  a11ce05d00            mov       eax,[5DE01Ch]
  0051af15  68304a5600            push      564A30h
  0051af1a  56                    push      esi
  0051af1b  8d0c80                lea       ecx,[eax+eax*4]
  0051af1e  52                    push      edx
  0051af1f  55                    push      ebp
  0051af20  8d444810              lea       eax,[eax+ecx*2+10h]
  0051af24  6689460a              mov       [esi+0Ah],ax
  0051af28  e8f3110000            call      0051C120h
  0051af2d  8b4c2444              mov       ecx,[esp+44h]
  0051af31  83c41c                add       esp,1Ch
  0051af34  8bd0                  mov       edx,eax
  0051af36  eb31                  jmp       short 0051AF69h

LAB_0051af38:
  0051af38  a880                  test      al,80h
  0051af3a  7406                  je        short 0051AF42h
  0051af3c  fe0d20bc5d00          dec       byte [5DBC20h]

LAB_0051af42:
  0051af42  f6410620              test      byte [ecx+6],20h
  0051af46  7406                  je        short 0051AF4Eh
  0051af48  fe0520bc5d00          inc       byte [5DBC20h]

LAB_0051af4e:
  0051af4e  a020bc5d00            mov       al,[5DBC20h]
  0051af53  3cff                  cmp       al,0FFh
  0051af55  7507                  jne       short 0051AF5Eh
  0051af57  b03f                  mov       al,3Fh
  0051af59  a220bc5d00            mov       [5DBC20h],al

LAB_0051af5e:
  0051af5e  3c40                  cmp       al,40h
  0051af60  7207                  jb        short 0051AF69h
  0051af62  c60520bc5d0000        mov       byte [5DBC20h],0

LAB_0051af69:
  0051af69  f6410340              test      byte [ecx+3],40h
  0051af6d  0f8435050000          je        near 0051B4A8h
  0051af73  b910000000            mov       ecx,10h
  0051af78  33c0                  xor       eax,eax
  0051af7a  bf80975e00            mov       edi,5E9780h
  0051af7f  f3ab                  rep stosd
  0051af81  e922050000            jmp       0051B4A8h
  0051af86  8b5c2428              mov       ebx,[esp+28h]
  0051af8a  66853b                test      [ebx],di
  0051af8d  8a4306                mov       al,[ebx+6]
  0051af90  743a                  je        short 0051AFCCh
  0051af92  a810                  test      al,10h
  0051af94  a1d4df5d00            mov       eax,[5DDFD4h]
  0051af99  7414                  je        short 0051AFAFh
  0051af9b  8bc8                  mov       ecx,eax
  0051af9d  81e1ff000000          and       ecx,0FFh
  0051afa3  fe8160905e00          inc       byte [ecx+5E9060h]
  0051afa9  8d8960905e00          lea       ecx,[ecx+5E9060h]

LAB_0051afaf:
  0051afaf  f6430640              test      byte [ebx+6],40h
  0051afb3  7448                  je        short 0051AFFDh
  0051afb5  25ff000000            and       eax,0FFh
  0051afba  8a8860905e00          mov       cl,[eax+5E9060h]
  0051afc0  8d8060905e00          lea       eax,[eax+5E9060h]
  0051afc6  fec9                  dec       cl
  0051afc8  8808                  mov       [eax],cl
  0051afca  eb31                  jmp       short 0051AFFDh

LAB_0051afcc:
  0051afcc  a880                  test      al,80h
  0051afce  7406                  je        short 0051AFD6h
  0051afd0  fe0dd4df5d00          dec       byte [5DDFD4h]

LAB_0051afd6:
  0051afd6  f6430620              test      byte [ebx+6],20h
  0051afda  7406                  je        short 0051AFE2h
  0051afdc  fe05d4df5d00          inc       byte [5DDFD4h]

LAB_0051afe2:
  0051afe2  a0d4df5d00            mov       al,[5DDFD4h]
  0051afe7  3cff                  cmp       al,0FFh
  0051afe9  7507                  jne       short 0051AFF2h
  0051afeb  b03f                  mov       al,3Fh
  0051afed  a2d4df5d00            mov       [5DDFD4h],al

LAB_0051aff2:
  0051aff2  3c40                  cmp       al,40h
  0051aff4  7207                  jb        short 0051AFFDh
  0051aff6  c605d4df5d0000        mov       byte [5DDFD4h],0

LAB_0051affd:
  0051affd  f6430340              test      byte [ebx+3],40h
  0051b001  0f84a1040000          je        near 0051B4A8h
  0051b007  b910000000            mov       ecx,10h
  0051b00c  33c0                  xor       eax,eax
  0051b00e  bf60905e00            mov       edi,5E9060h
  0051b013  f3ab                  rep stosd
  0051b015  e98e040000            jmp       0051B4A8h
  0051b01a  8b4c2428              mov       ecx,[esp+28h]
  0051b01e  668539                test      [ecx],di
  0051b021  8a4106                mov       al,[ecx+6]
  0051b024  0f84bf000000          je        near 0051B0E9h
  0051b02a  a880                  test      al,80h
  0051b02c  7406                  je        short 0051B034h
  0051b02e  fe0524bc5d00          inc       byte [5DBC24h]

LAB_0051b034:
  0051b034  f6410620              test      byte [ecx+6],20h
  0051b038  7406                  je        short 0051B040h
  0051b03a  fe0d24bc5d00          dec       byte [5DBC24h]

LAB_0051b040:
  0051b040  f6410210              test      byte [ecx+2],10h
  0051b044  a11cbc5d00            mov       eax,[5DBC1Ch]
  0051b049  7428                  je        short 0051B073h
  0051b04b  8bc8                  mov       ecx,eax
  0051b04d  b301                  mov       bl,1
  0051b04f  81e1ff000000          and       ecx,0FFh
  0051b055  8db940985e00          lea       edi,[ecx+5E9840h]
  0051b05b  8b0d24bc5d00          mov       ecx,[5DBC24h]
  0051b061  81e1ff000000          and       ecx,0FFh
  0051b067  d2e3                  shl       bl,cl
  0051b069  8a0f                  mov       cl,[edi]
  0051b06b  0acb                  or        cl,bl
  0051b06d  880f                  mov       [edi],cl
  0051b06f  8b4c2428              mov       ecx,[esp+28h]

LAB_0051b073:
  0051b073  f6410240              test      byte [ecx+2],40h
  0051b077  7423                  je        short 0051B09Ch
  0051b079  8b0d24bc5d00          mov       ecx,[5DBC24h]
  0051b07f  25ff000000            and       eax,0FFh
  0051b084  81e1ff000000          and       ecx,0FFh
  0051b08a  8db840985e00          lea       edi,[eax+5E9840h]
  0051b090  b001                  mov       al,1
  0051b092  d2e0                  shl       al,cl
  0051b094  8a0f                  mov       cl,[edi]
  0051b096  f6d0                  not       al
  0051b098  22c8                  and       cl,al
  0051b09a  880f                  mov       [edi],cl

LAB_0051b09c:
  0051b09c  a024bc5d00            mov       al,[5DBC24h]
  0051b0a1  6633c9                xor       cx,cx
  0051b0a4  2407                  and       al,7
  0051b0a6  6aff                  push      0FFFFFFFFh
  0051b0a8  a224bc5d00            mov       [5DBC24h],al
  0051b0ad  8ac8                  mov       cl,al
  0051b0af  b815000000            mov       eax,15h
  0051b0b4  6a00                  push      0
  0051b0b6  2bc1                  sub       eax,ecx
  0051b0b8  6a00                  push      0
  0051b0ba  c1e003                shl       eax,3
  0051b0bd  66894608              mov       [esi+8],ax
  0051b0c1  a11ce05d00            mov       eax,[5DE01Ch]
  0051b0c6  68304a5600            push      564A30h
  0051b0cb  56                    push      esi
  0051b0cc  8d0c80                lea       ecx,[eax+eax*4]
  0051b0cf  52                    push      edx
  0051b0d0  55                    push      ebp
  0051b0d1  8d444810              lea       eax,[eax+ecx*2+10h]
  0051b0d5  6689460a              mov       [esi+0Ah],ax
  0051b0d9  e842100000            call      0051C120h
  0051b0de  8b4c2444              mov       ecx,[esp+44h]
  0051b0e2  83c41c                add       esp,1Ch
  0051b0e5  8bd0                  mov       edx,eax
  0051b0e7  eb31                  jmp       short 0051B11Ah

LAB_0051b0e9:
  0051b0e9  a880                  test      al,80h
  0051b0eb  7406                  je        short 0051B0F3h
  0051b0ed  fe0d1cbc5d00          dec       byte [5DBC1Ch]

LAB_0051b0f3:
  0051b0f3  f6410620              test      byte [ecx+6],20h
  0051b0f7  7406                  je        short 0051B0FFh
  0051b0f9  fe051cbc5d00          inc       byte [5DBC1Ch]

LAB_0051b0ff:
  0051b0ff  a01cbc5d00            mov       al,[5DBC1Ch]
  0051b104  3cff                  cmp       al,0FFh
  0051b106  7507                  jne       short 0051B10Fh
  0051b108  b03f                  mov       al,3Fh
  0051b10a  a21cbc5d00            mov       [5DBC1Ch],al

LAB_0051b10f:
  0051b10f  3c40                  cmp       al,40h
  0051b111  7207                  jb        short 0051B11Ah
  0051b113  c6051cbc5d0000        mov       byte [5DBC1Ch],0

LAB_0051b11a:
  0051b11a  f6410340              test      byte [ecx+3],40h
  0051b11e  0f8484030000          je        near 0051B4A8h
  0051b124  b910000000            mov       ecx,10h
  0051b129  33c0                  xor       eax,eax
  0051b12b  bf40985e00            mov       edi,5E9840h
  0051b130  f3ab                  rep stosd
  0051b132  e971030000            jmp       0051B4A8h
  0051b137  8b442428              mov       eax,[esp+28h]
  0051b13b  668538                test      [eax],di
  0051b13e  8a4806                mov       cl,[eax+6]
  0051b141  0f847b010000          je        near 0051B2C2h
  0051b147  f6c180                test      cl,80h
  0051b14a  7406                  je        short 0051B152h
  0051b14c  fe0524bc5d00          inc       byte [5DBC24h]

LAB_0051b152:
  0051b152  f6400620              test      byte [eax+6],20h
  0051b156  7406                  je        short 0051B15Eh
  0051b158  fe0d24bc5d00          dec       byte [5DBC24h]

LAB_0051b15e:
  0051b15e  8a4802                mov       cl,[eax+2]
  0051b161  a148bb5d00            mov       eax,[5DBB48h]
  0051b166  f6c110                test      cl,10h
  0051b169  7478                  je        short 0051B1E3h
  0051b16b  8bc8                  mov       ecx,eax
  0051b16d  b201                  mov       dl,1
  0051b16f  81e1ff000000          and       ecx,0FFh
  0051b175  8db900905e00          lea       edi,[ecx+5E9000h]
  0051b17b  8b0d24bc5d00          mov       ecx,[5DBC24h]
  0051b181  81e1ff000000          and       ecx,0FFh
  0051b187  8a1f                  mov       bl,[edi]
  0051b189  d2e2                  shl       dl,cl
  0051b18b  8b0d20905e00          mov       ecx,[5E9020h]
  0051b191  0ada                  or        bl,dl
  0051b193  881f                  mov       [edi],bl
  0051b195  33ff                  xor       edi,edi
  0051b197  85c9                  test      ecx,ecx
  0051b199  7e48                  jle       short 0051B1E3h
  0051b19b  33ed                  xor       ebp,ebp

LAB_0051b19d:
  0051b19d  8b0db4876000          mov       ecx,[6087B4h]
  0051b1a3  25ff000000            and       eax,0FFh
  0051b1a8  03c5                  add       eax,ebp
  0051b1aa  81c500200000          add       ebp,2000h
  0051b1b0  8b11                  mov       edx,[ecx]
  0051b1b2  8b0d24bc5d00          mov       ecx,[5DBC24h]
  0051b1b8  81e1ff000000          and       ecx,0FFh
  0051b1be  8a9c10e0020f00        mov       bl,[eax+edx+0F02E0h]
  0051b1c5  8d8410e0020f00        lea       eax,[eax+edx+0F02E0h]
  0051b1cc  b201                  mov       dl,1
  0051b1ce  d2e2                  shl       dl,cl
  0051b1d0  0ada                  or        bl,dl
  0051b1d2  47                    inc       edi
  0051b1d3  8818                  mov       [eax],bl
  0051b1d5  a120905e00            mov       eax,[5E9020h]
  0051b1da  3bf8                  cmp       edi,eax
  0051b1dc  a148bb5d00            mov       eax,[5DBB48h]
  0051b1e1  7cba                  jl        short 0051B19Dh

LAB_0051b1e3:
  0051b1e3  8b4c2428              mov       ecx,[esp+28h]
  0051b1e7  f6410240              test      byte [ecx+2],40h
  0051b1eb  7479                  je        short 0051B266h
  0051b1ed  8b0d24bc5d00          mov       ecx,[5DBC24h]
  0051b1f3  8bd0                  mov       edx,eax
  0051b1f5  81e2ff000000          and       edx,0FFh
  0051b1fb  81e1ff000000          and       ecx,0FFh
  0051b201  8dba00905e00          lea       edi,[edx+5E9000h]
  0051b207  b201                  mov       dl,1
  0051b209  d2e2                  shl       dl,cl
  0051b20b  8a1f                  mov       bl,[edi]
  0051b20d  8b0d20905e00          mov       ecx,[5E9020h]
  0051b213  f6d2                  not       dl
  0051b215  22da                  and       bl,dl
  0051b217  881f                  mov       [edi],bl
  0051b219  33ff                  xor       edi,edi
  0051b21b  85c9                  test      ecx,ecx
  0051b21d  7e47                  jle       short 0051B266h
  0051b21f  33ed                  xor       ebp,ebp
  0051b221  eb05                  jmp       short 0051B228h

LAB_0051b223:
  0051b223  a148bb5d00            mov       eax,[5DBB48h]

LAB_0051b228:
  0051b228  8b0db4876000          mov       ecx,[6087B4h]
  0051b22e  25ff000000            and       eax,0FFh
  0051b233  03c5                  add       eax,ebp
  0051b235  81c500200000          add       ebp,2000h
  0051b23b  8b11                  mov       edx,[ecx]
  0051b23d  8b0d24bc5d00          mov       ecx,[5DBC24h]
  0051b243  81e1ff000000          and       ecx,0FFh
  0051b249  8d8410e0020f00        lea       eax,[eax+edx+0F02E0h]
  0051b250  b201                  mov       dl,1
  0051b252  d2e2                  shl       dl,cl
  0051b254  8a18                  mov       bl,[eax]
  0051b256  f6d2                  not       dl
  0051b258  22da                  and       bl,dl
  0051b25a  47                    inc       edi
  0051b25b  8818                  mov       [eax],bl
  0051b25d  a120905e00            mov       eax,[5E9020h]
  0051b262  3bf8                  cmp       edi,eax
  0051b264  7cbd                  jl        short 0051B223h

LAB_0051b266:
  0051b266  a024bc5d00            mov       al,[5DBC24h]
  0051b26b  b915000000            mov       ecx,15h
  0051b270  2407                  and       al,7
  0051b272  6aff                  push      0FFFFFFFFh
  0051b274  a224bc5d00            mov       [5DBC24h],al
  0051b279  6a00                  push      0
  0051b27b  660fb6c0              movzx     ax,al
  0051b27f  2bc8                  sub       ecx,eax
  0051b281  6a00                  push      0
  0051b283  c1e103                shl       ecx,3
  0051b286  66894e08              mov       [esi+8],cx
  0051b28a  a11ce05d00            mov       eax,[5DE01Ch]
  0051b28f  8b4c242c              mov       ecx,[esp+2Ch]
  0051b293  68304a5600            push      564A30h
  0051b298  8d1480                lea       edx,[eax+eax*4]
  0051b29b  56                    push      esi
  0051b29c  51                    push      ecx
  0051b29d  8d445010              lea       eax,[eax+edx*2+10h]
  0051b2a1  8b542434              mov       edx,[esp+34h]
  0051b2a5  52                    push      edx
  0051b2a6  6689460a              mov       [esi+0Ah],ax
  0051b2aa  e8710e0000            call      0051C120h
  0051b2af  8b6c2438              mov       ebp,[esp+38h]
  0051b2b3  8944243c              mov       [esp+3Ch],eax
  0051b2b7  8bd0                  mov       edx,eax
  0051b2b9  8b442444              mov       eax,[esp+44h]
  0051b2bd  83c41c                add       esp,1Ch
  0051b2c0  eb36                  jmp       short 0051B2F8h

LAB_0051b2c2:
  0051b2c2  f6c180                test      cl,80h
  0051b2c5  7406                  je        short 0051B2CDh
  0051b2c7  fe0d48bb5d00          dec       byte [5DBB48h]

LAB_0051b2cd:
  0051b2cd  f6400620              test      byte [eax+6],20h
  0051b2d1  7406                  je        short 0051B2D9h
  0051b2d3  fe0548bb5d00          inc       byte [5DBB48h]

LAB_0051b2d9:
  0051b2d9  8a0d48bb5d00          mov       cl,[5DBB48h]
  0051b2df  80f9ff                cmp       cl,0FFh
  0051b2e2  7508                  jne       short 0051B2ECh
  0051b2e4  b11f                  mov       cl,1Fh
  0051b2e6  880d48bb5d00          mov       [5DBB48h],cl

LAB_0051b2ec:
  0051b2ec  80f920                cmp       cl,20h
  0051b2ef  7207                  jb        short 0051B2F8h
  0051b2f1  c60548bb5d0000        mov       byte [5DBB48h],0

LAB_0051b2f8:
  0051b2f8  f6400340              test      byte [eax+3],40h
  0051b2fc  0f84a6010000          je        near 0051B4A8h
  0051b302  b908000000            mov       ecx,8
  0051b307  33c0                  xor       eax,eax
  0051b309  bf00905e00            mov       edi,5E9000h
  0051b30e  f3ab                  rep stosd
  0051b310  a120905e00            mov       eax,[5E9020h]
  0051b315  33ff                  xor       edi,edi
  0051b317  85c0                  test      eax,eax
  0051b319  0f8e89010000          jle       near 0051B4A8h

LAB_0051b31f:
  0051b31f  8bcf                  mov       ecx,edi
  0051b321  33c0                  xor       eax,eax
  0051b323  c1e10d                shl       ecx,0Dh

LAB_0051b326:
  0051b326  8b1db4876000          mov       ebx,[6087B4h]
  0051b32c  c68000905e0000        mov       byte [eax+5E9000h],0
  0051b333  40                    inc       eax
  0051b334  41                    inc       ecx
  0051b335  8b1b                  mov       ebx,[ebx]
  0051b337  83f820                cmp       eax,20h
  0051b33a  c68419df020f0000      mov       byte [ecx+ebx+0F02DFh],0
  0051b342  7ce2                  jl        short 0051B326h
  0051b344  a120905e00            mov       eax,[5E9020h]
  0051b349  47                    inc       edi
  0051b34a  3bf8                  cmp       edi,eax
  0051b34c  7cd1                  jl        short 0051B31Fh
  0051b34e  e955010000            jmp       0051B4A8h
  0051b353  8b4c2428              mov       ecx,[esp+28h]
  0051b357  668b01                mov       ax,[ecx]
  0051b35a  85c7                  test      edi,eax
  0051b35c  743a                  je        short 0051B398h
  0051b35e  a880                  test      al,80h
  0051b360  7407                  je        short 0051B369h
  0051b362  800588ca5e0004        add       byte [5ECA88h],4

LAB_0051b369:
  0051b369  8a19                  mov       bl,[ecx]
  0051b36b  b0fc                  mov       al,0FCh
  0051b36d  f6c320                test      bl,20h
  0051b370  7406                  je        short 0051B378h
  0051b372  000588ca5e00          add       [5ECA88h],al

LAB_0051b378:
  0051b378  f60110                test      byte [ecx],10h
  0051b37b  7407                  je        short 0051B384h
  0051b37d  800589ca5e0004        add       byte [5ECA89h],4

LAB_0051b384:
  0051b384  f60140                test      byte [ecx],40h
  0051b387  0f841b010000          je        near 0051B4A8h
  0051b38d  000589ca5e00          add       [5ECA89h],al
  0051b393  e910010000            jmp       0051B4A8h

LAB_0051b398:
  0051b398  f6410680              test      byte [ecx+6],80h
  0051b39c  a168335600            mov       eax,[563368h]
  0051b3a1  7414                  je        short 0051B3B7h
  0051b3a3  48                    dec       eax
  0051b3a4  c60588ca5e0000        mov       byte [5ECA88h],0
  0051b3ab  a368335600            mov       [563368h],eax
  0051b3b0  c60589ca5e0000        mov       byte [5ECA89h],0

LAB_0051b3b7:
  0051b3b7  f6410620              test      byte [ecx+6],20h
  0051b3bb  740f                  je        short 0051B3CCh
  0051b3bd  40                    inc       eax
  0051b3be  c60588ca5e0000        mov       byte [5ECA88h],0
  0051b3c5  c60589ca5e0000        mov       byte [5ECA89h],0

LAB_0051b3cc:
  0051b3cc  83e01f                and       eax,1Fh
  0051b3cf  a368335600            mov       [563368h],eax
  0051b3d4  750a                  jne       short 0051B3E0h
  0051b3d6  b804000000            mov       eax,4
  0051b3db  a368335600            mov       [563368h],eax

LAB_0051b3e0:
  0051b3e0  83f803                cmp       eax,3
  0051b3e3  750a                  jne       short 0051B3EFh
  0051b3e5  b81f000000            mov       eax,1Fh
  0051b3ea  a368335600            mov       [563368h],eax

LAB_0051b3ef:
  0051b3ef  83f810                cmp       eax,10h
  0051b3f2  750a                  jne       short 0051B3FEh
  0051b3f4  b814000000            mov       eax,14h
  0051b3f9  a368335600            mov       [563368h],eax

LAB_0051b3fe:
  0051b3fe  83f813                cmp       eax,13h
  0051b401  0f85a1000000          jne       near 0051B4A8h
  0051b407  c705683356000f000000  mov       dword [563368h],0Fh
  0051b411  e992000000            jmp       0051B4A8h
  0051b416  8b442428              mov       eax,[esp+28h]
  0051b41a  66857802              test      [eax+2],di
  0051b41e  0f8484000000          je        near 0051B4A8h
  0051b424  a12ce05d00            mov       eax,[5DE02Ch]
  0051b429  c605f8455600fe        mov       byte [5645F8h],0FEh
  0051b430  0c02                  or        al,2
  0051b432  c70510e55d000a000000  mov       dword [5DE510h],0Ah
  0051b43c  a32ce05d00            mov       [5DE02Ch],eax
  0051b441  eb65                  jmp       short 0051B4A8h
  0051b443  8b442428              mov       eax,[esp+28h]
  0051b447  66857802              test      [eax+2],di
  0051b44b  745b                  je        short 0051B4A8h
  0051b44d  a12ce05d00            mov       eax,[5DE02Ch]
  0051b452  c605f8455600fe        mov       byte [5645F8h],0FEh
  0051b459  0c02                  or        al,2
  0051b45b  890d10e55d00          mov       [5DE510h],ecx
  0051b461  a32ce05d00            mov       [5DE02Ch],eax
  0051b466  eb40                  jmp       short 0051B4A8h
  0051b468  8b442428              mov       eax,[esp+28h]
  0051b46c  f6400280              test      byte [eax+2],80h
  0051b470  7407                  je        short 0051B479h
  0051b472  c60520e05d0000        mov       byte [5DE020h],0

LAB_0051b479:
  0051b479  f6400220              test      byte [eax+2],20h
  0051b47d  7429                  je        short 0051B4A8h
  0051b47f  c60520e05d0001        mov       byte [5DE020h],1
  0051b486  eb20                  jmp       short 0051B4A8h
  0051b488  8b442428              mov       eax,[esp+28h]
  0051b48c  f6400280              test      byte [eax+2],80h
  0051b490  740a                  je        short 0051B49Ch
  0051b492  c705e06b560000000000  mov       dword [566BE0h],0

LAB_0051b49c:
  0051b49c  f6400220              test      byte [eax+2],20h
  0051b4a0  7406                  je        short 0051B4A8h
  0051b4a2  890de06b5600          mov       [566BE0h],ecx

LAB_0051b4a8:
  0051b4a8  66c746080c00          mov       word [esi+8],0Ch
  0051b4ae  a11ce05d00            mov       eax,[5DE01Ch]
  0051b4b3  6aff                  push      0FFFFFFFFh
  0051b4b5  6a00                  push      0
  0051b4b7  8d0c80                lea       ecx,[eax+eax*4]
  0051b4ba  6a00                  push      0
  0051b4bc  682c4a5600            push      564A2Ch
  0051b4c1  56                    push      esi
  0051b4c2  8d444818              lea       eax,[eax+ecx*2+18h]
  0051b4c6  52                    push      edx
  0051b4c7  55                    push      ebp
  0051b4c8  6689460a              mov       [esi+0Ah],ax
  0051b4cc  e84f0c0000            call      0051C120h
  0051b4d1  83c41c                add       esp,1Ch
  0051b4d4  66c746085000          mov       word [esi+8],50h
  0051b4da  66c7460a1800          mov       word [esi+0Ah],18h
  0051b4e0  c706306c5600          mov       dword [esi],566C30h
  0051b4e6  8b0d14bc5d00          mov       ecx,[5DBC14h]
  0051b4ec  6a00                  push      0
  0051b4ee  81e1ff000000          and       ecx,0FFh
  0051b4f4  6a02                  push      2
  0051b4f6  51                    push      ecx
  0051b4f7  56                    push      esi
  0051b4f8  50                    push      eax
  0051b4f9  55                    push      ebp
  0051b4fa  e891090000            call      0051BE90h
  0051b4ff  bf0b000000            mov       edi,0Bh
  0051b504  83c418                add       esp,18h
  0051b507  66017e0a              add       [esi+0Ah],di
  0051b50b  8b15d0df5d00          mov       edx,[5DDFD0h]
  0051b511  6a00                  push      0
  0051b513  81e2ff000000          and       edx,0FFh
  0051b519  6a02                  push      2
  0051b51b  52                    push      edx
  0051b51c  56                    push      esi
  0051b51d  50                    push      eax
  0051b51e  55                    push      ebp
  0051b51f  e86c090000            call      0051BE90h
  0051b524  66017e0a              add       [esi+0Ah],di
  0051b528  8b0d18bc5d00          mov       ecx,[5DBC18h]
  0051b52e  83c418                add       esp,18h
  0051b531  81e1ff000000          and       ecx,0FFh
  0051b537  6a00                  push      0
  0051b539  6a02                  push      2
  0051b53b  51                    push      ecx
  0051b53c  56                    push      esi
  0051b53d  50                    push      eax
  0051b53e  55                    push      ebp
  0051b53f  e84c090000            call      0051BE90h
  0051b544  66017e0a              add       [esi+0Ah],di
  0051b548  8b1588df5d00          mov       edx,[5DDF88h]
  0051b54e  83c418                add       esp,18h
  0051b551  81e2ff000000          and       edx,0FFh
  0051b557  6a00                  push      0
  0051b559  6a02                  push      2
  0051b55b  52                    push      edx
  0051b55c  56                    push      esi
  0051b55d  50                    push      eax
  0051b55e  55                    push      ebp
  0051b55f  e82c090000            call      0051BE90h
  0051b564  b908000000            mov       ecx,8
  0051b569  83c418                add       esp,18h
  0051b56c  6683460820            add       word [esi+8],20h
  0051b571  8bf9                  mov       edi,ecx

LAB_0051b573:
  0051b573  8d59ff                lea       ebx,[ecx-1]
  0051b576  8b0d88df5d00          mov       ecx,[5DDF88h]
  0051b57c  81e1ff000000          and       ecx,0FFh
  0051b582  33d2                  xor       edx,edx
  0051b584  6a00                  push      0
  0051b586  6a01                  push      1
  0051b588  8a91e0965e00          mov       dl,[ecx+5E96E0h]
  0051b58e  8acb                  mov       cl,bl
  0051b590  d3ea                  shr       edx,cl
  0051b592  83e201                and       edx,1
  0051b595  52                    push      edx
  0051b596  56                    push      esi
  0051b597  50                    push      eax
  0051b598  55                    push      ebp
  0051b599  e8f2080000            call      0051BE90h
  0051b59e  66017e08              add       [esi+8],di
  0051b5a2  668b5608              mov       dx,[esi+8]
  0051b5a6  8bcb                  mov       ecx,ebx
  0051b5a8  83c418                add       esp,18h
  0051b5ab  85c9                  test      ecx,ecx
  0051b5ad  75c4                  jne       short 0051B573h
  0051b5af  6683460a0b            add       word [esi+0Ah],0Bh
  0051b5b4  83c2a0                add       edx,0FFFFFFA0h
  0051b5b7  66895608              mov       [esi+8],dx
  0051b5bb  8b0d20bc5d00          mov       ecx,[5DBC20h]
  0051b5c1  53                    push      ebx
  0051b5c2  81e1ff000000          and       ecx,0FFh
  0051b5c8  6a02                  push      2
  0051b5ca  51                    push      ecx
  0051b5cb  56                    push      esi
  0051b5cc  50                    push      eax
  0051b5cd  55                    push      ebp
  0051b5ce  e8bd080000            call      0051BE90h
  0051b5d3  83c418                add       esp,18h
  0051b5d6  6683460820            add       word [esi+8],20h
  0051b5db  8bcf                  mov       ecx,edi
  0051b5dd  bf08000000            mov       edi,8

LAB_0051b5e2:
  0051b5e2  8b1520bc5d00          mov       edx,[5DBC20h]
  0051b5e8  8d59ff                lea       ebx,[ecx-1]
  0051b5eb  81e2ff000000          and       edx,0FFh
  0051b5f1  33c9                  xor       ecx,ecx
  0051b5f3  6a00                  push      0
  0051b5f5  6a01                  push      1
  0051b5f7  8a8a80975e00          mov       cl,[edx+5E9780h]
  0051b5fd  8bd1                  mov       edx,ecx
  0051b5ff  8acb                  mov       cl,bl
  0051b601  d3ea                  shr       edx,cl
  0051b603  83e201                and       edx,1
  0051b606  52                    push      edx
  0051b607  56                    push      esi
  0051b608  50                    push      eax
  0051b609  55                    push      ebp
  0051b60a  e881080000            call      0051BE90h
  0051b60f  66017e08              add       [esi+8],di
  0051b613  668b5608              mov       dx,[esi+8]
  0051b617  8bcb                  mov       ecx,ebx
  0051b619  83c418                add       esp,18h
  0051b61c  85c9                  test      ecx,ecx
  0051b61e  75c2                  jne       short 0051B5E2h
  0051b620  83c2a0                add       edx,0FFFFFFA0h
  0051b623  bb0b000000            mov       ebx,0Bh
  0051b628  66015e0a              add       [esi+0Ah],bx
  0051b62c  66895608              mov       [esi+8],dx
  0051b630  51                    push      ecx
  0051b631  8b0dd4df5d00          mov       ecx,[5DDFD4h]
  0051b637  81e1ff000000          and       ecx,0FFh
  0051b63d  6a02                  push      2
  0051b63f  51                    push      ecx
  0051b640  56                    push      esi
  0051b641  50                    push      eax
  0051b642  55                    push      ebp
  0051b643  e848080000            call      0051BE90h
  0051b648  6683460820            add       word [esi+8],20h
  0051b64d  8b15d4df5d00          mov       edx,[5DDFD4h]
  0051b653  83c418                add       esp,18h
  0051b656  81e2ff000000          and       edx,0FFh
  0051b65c  33c9                  xor       ecx,ecx
  0051b65e  8a8a60905e00          mov       cl,[edx+5E9060h]
  0051b664  6a00                  push      0
  0051b666  6a03                  push      3
  0051b668  51                    push      ecx
  0051b669  56                    push      esi
  0051b66a  50                    push      eax
  0051b66b  55                    push      ebp
  0051b66c  e81f080000            call      0051BE90h
  0051b671  66834608e0            add       word [esi+8],0FFE0h
  0051b676  66015e0a              add       [esi+0Ah],bx
  0051b67a  8b151cbc5d00          mov       edx,[5DBC1Ch]
  0051b680  83c418                add       esp,18h
  0051b683  81e2ff000000          and       edx,0FFh
  0051b689  6a00                  push      0
  0051b68b  6a02                  push      2
  0051b68d  52                    push      edx
  0051b68e  56                    push      esi
  0051b68f  50                    push      eax
  0051b690  55                    push      ebp
  0051b691  e8fa070000            call      0051BE90h
  0051b696  83c418                add       esp,18h
  0051b699  6683460820            add       word [esi+8],20h
  0051b69e  8bcf                  mov       ecx,edi
  0051b6a0  bf08000000            mov       edi,8

LAB_0051b6a5:
  0051b6a5  8d59ff                lea       ebx,[ecx-1]
  0051b6a8  8b0d1cbc5d00          mov       ecx,[5DBC1Ch]
  0051b6ae  81e1ff000000          and       ecx,0FFh
  0051b6b4  33d2                  xor       edx,edx
  0051b6b6  6a00                  push      0
  0051b6b8  6a01                  push      1
  0051b6ba  8a9140985e00          mov       dl,[ecx+5E9840h]
  0051b6c0  8acb                  mov       cl,bl
  0051b6c2  d3ea                  shr       edx,cl
  0051b6c4  83e201                and       edx,1
  0051b6c7  52                    push      edx
  0051b6c8  56                    push      esi
  0051b6c9  50                    push      eax
  0051b6ca  55                    push      ebp
  0051b6cb  e8c0070000            call      0051BE90h
  0051b6d0  66017e08              add       [esi+8],di
  0051b6d4  668b5608              mov       dx,[esi+8]
  0051b6d8  8bcb                  mov       ecx,ebx
  0051b6da  83c418                add       esp,18h
  0051b6dd  85c9                  test      ecx,ecx
  0051b6df  75c4                  jne       short 0051B6A5h
  0051b6e1  6683460a0b            add       word [esi+0Ah],0Bh
  0051b6e6  83c2a0                add       edx,0FFFFFFA0h
  0051b6e9  66895608              mov       [esi+8],dx
  0051b6ed  8b0d48bb5d00          mov       ecx,[5DBB48h]
  0051b6f3  53                    push      ebx
  0051b6f4  81e1ff000000          and       ecx,0FFh
  0051b6fa  6a02                  push      2
  0051b6fc  51                    push      ecx
  0051b6fd  56                    push      esi
  0051b6fe  50                    push      eax
  0051b6ff  55                    push      ebp
  0051b700  e88b070000            call      0051BE90h
  0051b705  83c418                add       esp,18h
  0051b708  6683460820            add       word [esi+8],20h
  0051b70d  8bcf                  mov       ecx,edi
  0051b70f  bf08000000            mov       edi,8

LAB_0051b714:
  0051b714  8b1548bb5d00          mov       edx,[5DBB48h]
  0051b71a  8d59ff                lea       ebx,[ecx-1]
  0051b71d  81e2ff000000          and       edx,0FFh
  0051b723  33c9                  xor       ecx,ecx
  0051b725  6a00                  push      0
  0051b727  6a01                  push      1
  0051b729  8a8a00905e00          mov       cl,[edx+5E9000h]
  0051b72f  8bd1                  mov       edx,ecx
  0051b731  8acb                  mov       cl,bl
  0051b733  d3ea                  shr       edx,cl
  0051b735  83e201                and       edx,1
  0051b738  52                    push      edx
  0051b739  56                    push      esi
  0051b73a  50                    push      eax
  0051b73b  55                    push      ebp
  0051b73c  e84f070000            call      0051BE90h
  0051b741  66017e08              add       [esi+8],di
  0051b745  668b5608              mov       dx,[esi+8]
  0051b749  8bcb                  mov       ecx,ebx
  0051b74b  83c418                add       esp,18h
  0051b74e  85c9                  test      ecx,ecx
  0051b750  75c2                  jne       short 0051B714h
  0051b752  6683460a0b            add       word [esi+0Ah],0Bh
  0051b757  83c2a0                add       edx,0FFFFFFA0h
  0051b75a  66895608              mov       [esi+8],dx
  0051b75e  8b0d68335600          mov       ecx,[563368h]
  0051b764  53                    push      ebx
  0051b765  6a02                  push      2
  0051b767  51                    push      ecx
  0051b768  56                    push      esi
  0051b769  50                    push      eax
  0051b76a  55                    push      ebp
  0051b76b  e820070000            call      0051BE90h
  0051b770  8bd0                  mov       edx,eax
  0051b772  a11ce05d00            mov       eax,[5DE01Ch]
  0051b777  83c418                add       esp,18h
  0051b77a  3bc7                  cmp       eax,edi
  0051b77c  0f8581000000          jne       near 0051B803h
  0051b782  66895e08              mov       [esi+8],bx
  0051b786  66895e0a              mov       [esi+0Ah],bx
  0051b78a  66895e10              mov       [esi+10h],bx
  0051b78e  c70688ca5e00          mov       dword [esi],5ECA88h
  0051b794  a168335600            mov       eax,[563368h]
  0051b799  53                    push      ebx
  0051b79a  8bc8                  mov       ecx,eax
  0051b79c  53                    push      ebx
  0051b79d  c1e106                shl       ecx,6
  0051b7a0  c1e004                shl       eax,4
  0051b7a3  c1f906                sar       ecx,6
  0051b7a6  c1f804                sar       eax,4
  0051b7a9  83e10f                and       ecx,0Fh
  0051b7ac  83e010                and       eax,10h
  0051b7af  0bc8                  or        ecx,eax
  0051b7b1  56                    push      esi
  0051b7b2  80cd02                or        ch,2
  0051b7b5  52                    push      edx
  0051b7b6  55                    push      ebp
  0051b7b7  881d8aca5e00          mov       [5ECA8Ah],bl
  0051b7bd  c6058bca5e00e0        mov       byte [5ECA8Bh],0E0h
  0051b7c4  881d8cca5e00          mov       [5ECA8Ch],bl
  0051b7ca  c6058dca5e0003        mov       byte [5ECA8Dh],3
  0051b7d1  c6058eca5e0032        mov       byte [5ECA8Eh],32h
  0051b7d8  881d8fca5e00          mov       [5ECA8Fh],bl
  0051b7de  66890dc88f5e00        mov       [5E8FC8h],cx
  0051b7e5  66c705ca8f5e000070    mov       word [5E8FCAh],7000h
  0051b7ee  e8fd650000            call      00521DF0h
  0051b7f3  83c414                add       esp,14h
  0051b7f6  56                    push      esi
  0051b7f7  50                    push      eax
  0051b7f8  55                    push      ebp
  0051b7f9  e8c2410000            call      0051F9C0h
  0051b7fe  83c40c                add       esp,0Ch
  0051b801  8bd0                  mov       edx,eax

LAB_0051b803:
  0051b803  5f                    pop       edi
  0051b804  5e                    pop       esi
  0051b805  5d                    pop       ebp
  0051b806  8bc2                  mov       eax,edx
  0051b808  5b                    pop       ebx
  0051b809  83c408                add       esp,8
  0051b80c  c3                    ret

LAB_0051b80d:
  0051b80d  8b542420              mov       edx,[esp+20h]
  0051b811  e984f2ffff            jmp       0051AA9Ah
  0051b816  8bff                  mov       edi,edi
  0051b818  06                    push      es
  0051b819  ab                    stosd
  0051b81a  51                    push      ecx
  0051b81b  0075ab                add       [ebp-55h],dh
  0051b81e  51                    push      ecx
  0051b81f  0007                  add       [edi],al
  0051b821  ac                    lodsb
  0051b822  51                    push      ecx
  0051b823  004cad51              add       [ebp+ebp*4+51h],cl
  0051b827  0069ae                add       [ecx-52h],ch
  0051b82a  51                    push      ecx
  0051b82b  0086af51001a          add       [esi+1A0051AFh],al
  0051b831  b051                  mov       al,51h
  0051b833  0037                  add       [edi],dh
  0051b835  b151                  mov       cl,51h
  0051b837  0053b3                add       [ebx-4Dh],dl
  0051b83a  51                    push      ecx
  0051b83b  0016                  add       [esi],dl
  0051b83d  b451                  mov       ah,51h
  0051b83f  0043b4                add       [ebx-4Ch],al
  0051b842  51                    push      ecx
  0051b843  0068b4                add       [eax-4Ch],ch
  0051b846  51                    push      ecx
  0051b847  0088b4510090          add       [eax-6FFFAE4Ch],cl
  0051b84d  90                    nop
  0051b84e  90                    nop
  0051b84f  90                    nop

89 cible(s) de saut interne : 0x0051aa72, 0x0051aa9a, 0x0051aab9, 0x0051aac5, 0x0051aad1, 0x0051aade, 0x0051aae3, 0x0051ab1c, 0x0051ab29, 0x0051ab3c, 0x0051ab47, 0x0051ab56, 0x0051ab8b, 0x0051ab98, 0x0051abaa, 0x0051abbd, 0x0051abc8, 0x0051abd7, 0x0051ac17, 0x0051ac23, 0x0051ac33, 0x0051ac3e, 0x0051ac97, 0x0051ad00, 0x0051ad66, 0x0051ad72, 0x0051ada5, 0x0051adce, 0x0051ae1b, 0x0051ae25, 0x0051ae31, 0x0051ae41, 0x0051ae4c, 0x0051ae83, 0x0051ae8f, 0x0051aec2, 0x0051aeeb, 0x0051af38, 0x0051af42, 0x0051af4e, 0x0051af5e, 0x0051af69, 0x0051afaf, 0x0051afcc, 0x0051afd6, 0x0051afe2, 0x0051aff2, 0x0051affd, 0x0051b034, 0x0051b040, 0x0051b073, 0x0051b09c, 0x0051b0e9, 0x0051b0f3, 0x0051b0ff, 0x0051b10f, 0x0051b11a, 0x0051b152, 0x0051b15e, 0x0051b19d, 0x0051b1e3, 0x0051b223, 0x0051b228, 0x0051b266, 0x0051b2c2, 0x0051b2cd, 0x0051b2d9, 0x0051b2ec, 0x0051b2f8, 0x0051b31f, 0x0051b326, 0x0051b369, 0x0051b378, 0x0051b384, 0x0051b398, 0x0051b3b7, 0x0051b3cc, 0x0051b3e0, 0x0051b3ef, 0x0051b3fe, 0x0051b479, 0x0051b49c, 0x0051b4a8, 0x0051b573, 0x0051b5e2, 0x0051b6a5, 0x0051b714, 0x0051b803, 0x0051b80d
