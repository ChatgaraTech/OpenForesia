Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00468210  section .text  offset fichier 0x67610  864 octets

  00468210  53                    push      ebx
  00468211  56                    push      esi
  00468212  8b74240c              mov       esi,[esp+0Ch]
  00468216  33d2                  xor       edx,edx
  00468218  57                    push      edi
  00468219  bb01000000            mov       ebx,1
  0046821e  66395610              cmp       [esi+10h],dx
  00468222  7512                  jne       short 00468236h
  00468224  66895e10              mov       [esi+10h],bx
  00468228  66895612              mov       [esi+12h],dx
  0046822c  895e5c                mov       [esi+5Ch],ebx
  0046822f  6689969a000000        mov       [esi+9Ah],dx

LAB_00468236:
  00468236  668b4614              mov       ax,[esi+14h]
  0046823a  8b0dc8175700          mov       ecx,[5717C8h]
  00468240  663bc3                cmp       ax,bx
  00468243  7508                  jne       short 0046824Dh
  00468245  84cb                  test      bl,cl
  00468247  0f8404030000          je        near 00468551h

LAB_0046824d:
  0046824d  bf02000000            mov       edi,2
  00468252  663bc7                cmp       ax,di
  00468255  7509                  jne       short 00468260h
  00468257  f6c102                test      cl,2
  0046825a  0f84f1020000          je        near 00468551h

LAB_00468260:
  00468260  663d0300              cmp       ax,3
  00468264  7509                  jne       short 0046826Fh
  00468266  f6c104                test      cl,4
  00468269  0f84e2020000          je        near 00468551h

LAB_0046826f:
  0046826f  663d0400              cmp       ax,4
  00468273  7509                  jne       short 0046827Eh
  00468275  f6c108                test      cl,8
  00468278  0f84d3020000          je        near 00468551h

LAB_0046827e:
  0046827e  663d0500              cmp       ax,5
  00468282  7509                  jne       short 0046828Dh
  00468284  f6c110                test      cl,10h
  00468287  0f84c4020000          je        near 00468551h

LAB_0046828d:
  0046828d  663bc3                cmp       ax,bx
  00468290  7524                  jne       short 004682B6h
  00468292  f6c120                test      cl,20h
  00468295  741f                  je        short 004682B6h
  00468297  c7461c0040b2ff        mov       dword [esi+1Ch],0FFB24000h
  0046829e  c7462000900f00        mov       dword [esi+20h],0F9000h
  004682a5  c7462400c063ff        mov       dword [esi+24h],0FF63C000h
  004682ac  66c746346404          mov       word [esi+34h],464h
  004682b2  5f                    pop       edi
  004682b3  5e                    pop       esi
  004682b4  5b                    pop       ebx
  004682b5  c3                    ret

LAB_004682b6:
  004682b6  663bc7                cmp       ax,di
  004682b9  7524                  jne       short 004682DFh
  004682bb  f6c140                test      cl,40h
  004682be  741f                  je        short 004682DFh
  004682c0  c7461c00c0beff        mov       dword [esi+1Ch],0FFBEC000h
  004682c7  c7462000900f00        mov       dword [esi+20h],0F9000h
  004682ce  c74624004057ff        mov       dword [esi+24h],0FF574000h
  004682d5  66c746346404          mov       word [esi+34h],464h
  004682db  5f                    pop       edi
  004682dc  5e                    pop       esi
  004682dd  5b                    pop       ebx
  004682de  c3                    ret

LAB_004682df:
  004682df  663d0300              cmp       ax,3
  004682e3  7524                  jne       short 00468309h
  004682e5  f6c180                test      cl,80h
  004682e8  741f                  je        short 00468309h
  004682ea  c7461c0040b2ff        mov       dword [esi+1Ch],0FFB24000h
  004682f1  c7462000900f00        mov       dword [esi+20h],0F9000h
  004682f8  c7462400401f00        mov       dword [esi+24h],1F4000h
  004682ff  66c746346404          mov       word [esi+34h],464h
  00468305  5f                    pop       edi
  00468306  5e                    pop       esi
  00468307  5b                    pop       ebx
  00468308  c3                    ret

LAB_00468309:
  00468309  663d0400              cmp       ax,4
  0046830d  7524                  jne       short 00468333h
  0046830f  f6c501                test      ch,1
  00468312  741f                  je        short 00468333h
  00468314  c7461c00c0f0ff        mov       dword [esi+1Ch],0FFF0C000h
  0046831b  c7462000101c00        mov       dword [esi+20h],1C1000h
  00468322  c7462400c05d00        mov       dword [esi+24h],5DC000h
  00468329  66c746346404          mov       word [esi+34h],464h
  0046832f  5f                    pop       edi
  00468330  5e                    pop       esi
  00468331  5b                    pop       ebx
  00468332  c3                    ret

LAB_00468333:
  00468333  663d0500              cmp       ax,5
  00468337  7524                  jne       short 0046835Dh
  00468339  f6c502                test      ch,2
  0046833c  741f                  je        short 0046835Dh
  0046833e  c7461c00400f00        mov       dword [esi+1Ch],0F4000h
  00468345  c7462000101c00        mov       dword [esi+20h],1C1000h
  0046834c  c7462400c0c100        mov       dword [esi+24h],0C1C000h
  00468353  66c746346404          mov       word [esi+34h],464h
  00468359  5f                    pop       edi
  0046835a  5e                    pop       esi
  0046835b  5b                    pop       ebx
  0046835c  c3                    ret

LAB_0046835d:
  0046835d  52                    push      edx
  0046835e  52                    push      edx
  0046835f  68e7030000            push      3E7h
  00468364  52                    push      edx
  00468365  68e7030000            push      3E7h
  0046836a  56                    push      esi
  0046836b  e8f03afbff            call      0041BE60h
  00468370  83c418                add       esp,18h
  00468373  66ff4612              inc       word [esi+12h]
  00468377  66837e120d            cmp       word [esi+12h],0Dh
  0046837c  7c0e                  jl        short 0046838Ch
  0046837e  66395e10              cmp       [esi+10h],bx
  00468382  0f858e000000          jne       near 00468416h
  00468388  66897e10              mov       [esi+10h],di

LAB_0046838c:
  0046838c  66395e10              cmp       [esi+10h],bx
  00468390  0f8580000000          jne       near 00468416h
  00468396  6683be9a00000000      cmp       word [esi+9Ah],0
  0046839e  7531                  jne       short 004683D1h
  004683a0  a1cc975e00            mov       eax,[5E97CCh]
  004683a5  6a00                  push      0
  004683a7  6a00                  push      0
  004683a9  6800180000            push      1800h
  004683ae  8b88a8000000          mov       ecx,[eax+0A8h]
  004683b4  6800040000            push      400h
  004683b9  8b11                  mov       edx,[ecx]
  004683bb  660fb64214            movzx     ax,byte [edx+14h]
  004683c0  50                    push      eax
  004683c1  56                    push      esi
  004683c2  e8b9fcffff            call      00468080h
  004683c7  83c418                add       esp,18h
  004683ca  66899e9a000000        mov       [esi+9Ah],bx

LAB_004683d1:
  004683d1  0fbf4e32              movsx     ecx,word [esi+32h]
  004683d5  8b5e20                mov       ebx,[esi+20h]
  004683d8  51                    push      ecx
  004683d9  81c300f0ffff          add       ebx,0FFFFF000h
  004683df  895e20                mov       [esi+20h],ebx
  004683e2  e835d60d00            call      00545A1Ch
  004683e7  8d1440                lea       edx,[eax+eax*2]
  004683ea  8b4624                mov       eax,[esi+24h]
  004683ed  03c2                  add       eax,edx
  004683ef  894624                mov       [esi+24h],eax
  004683f2  0fbf4632              movsx     eax,word [esi+32h]
  004683f6  50                    push      eax
  004683f7  e81ad60d00            call      00545A16h
  004683fc  8b561c                mov       edx,[esi+1Ch]
  004683ff  8d0c40                lea       ecx,[eax+eax*2]
  00468402  03d1                  add       edx,ecx
  00468404  8b4e5c                mov       ecx,[esi+5Ch]
  00468407  41                    inc       ecx
  00468408  89561c                mov       [esi+1Ch],edx
  0046840b  894e5c                mov       [esi+5Ch],ecx
  0046840e  668b565c              mov       dx,[esi+5Ch]
  00468412  66015634              add       [esi+34h],dx

LAB_00468416:
  00468416  66397e10              cmp       [esi+10h],di
  0046841a  0f8594000000          jne       near 004684B4h
  00468420  8b5e5c                mov       ebx,[esi+5Ch]
  00468423  03df                  add       ebx,edi
  00468425  895e5c                mov       [esi+5Ch],ebx
  00468428  668b465c              mov       ax,[esi+5Ch]
  0046842c  66014634              add       [esi+34h],ax
  00468430  668b4634              mov       ax,[esi+34h]
  00468434  663d6404              cmp       ax,464h
  00468438  7c69                  jl        short 004684A3h
  0046843a  66c746346404          mov       word [esi+34h],464h
  00468440  66c746100300          mov       word [esi+10h],3
  00468446  66c746120800          mov       word [esi+12h],8
  0046844c  8b0dcc975e00          mov       ecx,[5E97CCh]
  00468452  6a00                  push      0
  00468454  6a00                  push      0
  00468456  8b91a8000000          mov       edx,[ecx+0A8h]
  0046845c  6800100000            push      1000h
  00468461  6800040000            push      400h
  00468466  8b02                  mov       eax,[edx]
  00468468  660fb64812            movzx     cx,byte [eax+12h]
  0046846d  51                    push      ecx
  0046846e  56                    push      esi
  0046846f  e80cfcffff            call      00468080h
  00468474  83c418                add       esp,18h
  00468477  6a00                  push      0
  00468479  6a00                  push      0
  0046847b  6a00                  push      0
  0046847d  6a0a                  push      0Ah
  0046847f  56                    push      esi
  00468480  e8bb55ffff            call      0045DA40h
  00468485  83c414                add       esp,14h
  00468488  bf1f000000            mov       edi,1Fh

LAB_0046848d:
  0046848d  6a00                  push      0
  0046848f  6a00                  push      0
  00468491  6aff                  push      0FFFFFFFFh
  00468493  6a00                  push      0
  00468495  6a30                  push      30h
  00468497  56                    push      esi
  00468498  e873fcffff            call      00468110h
  0046849d  83c418                add       esp,18h
  004684a0  4f                    dec       edi
  004684a1  75ea                  jne       short 0046848Dh

LAB_004684a3:
  004684a3  0fbf4616              movsx     eax,word [esi+16h]
  004684a7  8b4e20                mov       ecx,[esi+20h]
  004684aa  c1e00c                shl       eax,0Ch
  004684ad  3bc8                  cmp       ecx,eax
  004684af  7f03                  jg        short 004684B4h
  004684b1  894620                mov       [esi+20h],eax

LAB_004684b4:
  004684b4  66837e1003            cmp       word [esi+10h],3
  004684b9  0f8592000000          jne       near 00468551h
  004684bf  668b4612              mov       ax,[esi+12h]
  004684c3  6685c0                test      ax,ax
  004684c6  7e05                  jle       short 004684CDh
  004684c8  48                    dec       eax
  004684c9  66894612              mov       [esi+12h],ax

LAB_004684cd:
  004684cd  668b4612              mov       ax,[esi+12h]
  004684d1  6685c0                test      ax,ax
  004684d4  7d05                  jge       short 004684DBh
  004684d6  40                    inc       eax
  004684d7  66894612              mov       [esi+12h],ax

LAB_004684db:
  004684db  66837e1200            cmp       word [esi+12h],0
  004684e0  7549                  jne       short 0046852Bh
  004684e2  0fbf4614              movsx     eax,word [esi+14h]
  004684e6  48                    dec       eax
  004684e7  66c746100400          mov       word [esi+10h],4
  004684ed  83f804                cmp       eax,4
  004684f0  7739                  ja        short 0046852Bh
  004684f2  ff248558854600        jmp       dword [eax*4+468558h]
  004684f9  a1c8175700            mov       eax,[5717C8h]
  004684fe  0c20                  or        al,20h
  00468500  eb24                  jmp       short 00468526h
  00468502  a1c8175700            mov       eax,[5717C8h]
  00468507  0c40                  or        al,40h
  00468509  eb1b                  jmp       short 00468526h
  0046850b  a1c8175700            mov       eax,[5717C8h]
  00468510  0c80                  or        al,80h
  00468512  eb12                  jmp       short 00468526h
  00468514  a1c8175700            mov       eax,[5717C8h]
  00468519  80cc01                or        ah,1
  0046851c  eb08                  jmp       short 00468526h
  0046851e  a1c8175700            mov       eax,[5717C8h]
  00468523  80cc02                or        ah,2

LAB_00468526:
  00468526  a3c8175700            mov       [5717C8h],eax

LAB_0046852b:
  0046852b  668b4612              mov       ax,[esi+12h]
  0046852f  6a00                  push      0
  00468531  66f7d8                neg       ax
  00468534  0fbfd0                movsx     edx,ax
  00468537  6a00                  push      0
  00468539  68e7030000            push      3E7h
  0046853e  52                    push      edx
  0046853f  68e7030000            push      3E7h
  00468544  56                    push      esi
  00468545  66894612              mov       [esi+12h],ax
  00468549  e81239fbff            call      0041BE60h
  0046854e  83c418                add       esp,18h

LAB_00468551:
  00468551  5f                    pop       edi
  00468552  5e                    pop       esi
  00468553  5b                    pop       ebx
  00468554  c3                    ret
  00468555  90                    nop
  00468556  8bff                  mov       edi,edi
  00468558  f9                    stc
  00468559  844600                test      [esi],al
  0046855c  028546000b85          add       al,[ebp-7AF4FFBAh]
  00468562  46                    inc       esi
  00468563  00148546001e85        add       [eax*4-7AE1FFBAh],dl
  0046856a  46                    inc       esi
  0046856b  0090909090            (bad)
  (octet invalide - la plage demandee deborde probablement du code)
