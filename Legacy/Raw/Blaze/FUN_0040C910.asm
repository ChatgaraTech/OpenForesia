Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0040c910  section .text  offset fichier 0xbd10  368 octets

  0040c910  56                    push      esi
  0040c911  8b742408              mov       esi,[esp+8]
  0040c915  57                    push      edi
  0040c916  66837e1000            cmp       word [esi+10h],0
  0040c91b  0f85bf000000          jne       near 0040C9E0h
  0040c921  66c746100100          mov       word [esi+10h],1
  0040c927  c7463c68f55400        mov       dword [esi+3Ch],54F568h
  0040c92e  e80da81300            call      00547140h
  0040c933  99                    cdq
  0040c934  33c2                  xor       eax,edx
  0040c936  2bc2                  sub       eax,edx
  0040c938  25ff030000            and       eax,3FFh
  0040c93d  33c2                  xor       eax,edx
  0040c93f  2bc2                  sub       eax,edx
  0040c941  05000c0000            add       eax,0C00h
  0040c946  66894628              mov       [esi+28h],ax
  0040c94a  e8f1a71300            call      00547140h
  0040c94f  99                    cdq
  0040c950  33c2                  xor       eax,edx
  0040c952  c6460a02              mov       byte [esi+0Ah],2
  0040c956  2bc2                  sub       eax,edx
  0040c958  c6460b00              mov       byte [esi+0Bh],0
  0040c95c  25ff030000            and       eax,3FFh
  0040c961  33c2                  xor       eax,edx
  0040c963  2bc2                  sub       eax,edx
  0040c965  05000c0000            add       eax,0C00h
  0040c96a  6689462a              mov       [esi+2Ah],ax
  0040c96e  e8cda71300            call      00547140h
  0040c973  99                    cdq
  0040c974  33c2                  xor       eax,edx
  0040c976  66c746120000          mov       word [esi+12h],0
  0040c97c  2bc2                  sub       eax,edx
  0040c97e  25ff0f0000            and       eax,0FFFh
  0040c983  33c2                  xor       eax,edx
  0040c985  2bc2                  sub       eax,edx
  0040c987  66014632              add       [esi+32h],ax
  0040c98b  e8b0a71300            call      00547140h
  0040c990  99                    cdq
  0040c991  b90f000000            mov       ecx,0Fh
  0040c996  c6464000              mov       byte [esi+40h],0
  0040c99a  f7f9                  idiv      ecx
  0040c99c  8b06                  mov       eax,[esi]
  0040c99e  bf0b000000            mov       edi,0Bh
  0040c9a3  80cc40                or        ah,40h
  0040c9a6  8906                  mov       [esi],eax
  0040c9a8  66895614              mov       [esi+14h],dx

LAB_0040c9ac:
  0040c9ac  0fbf5632              movsx     edx,word [esi+32h]
  0040c9b0  52                    push      edx
  0040c9b1  e866901300            call      00545A1Ch
  0040c9b6  8b561c                mov       edx,[esi+1Ch]
  0040c9b9  c1e003                shl       eax,3
  0040c9bc  03d0                  add       edx,eax
  0040c9be  0fbf4632              movsx     eax,word [esi+32h]
  0040c9c2  50                    push      eax
  0040c9c3  89561c                mov       [esi+1Ch],edx
  0040c9c6  e84b901300            call      00545A16h
  0040c9cb  8b4e24                mov       ecx,[esi+24h]
  0040c9ce  c1e003                shl       eax,3
  0040c9d1  03c8                  add       ecx,eax
  0040c9d3  4f                    dec       edi
  0040c9d4  894e24                mov       [esi+24h],ecx
  0040c9d7  75d3                  jne       short 0040C9ACh
  0040c9d9  81462000c0f9ff        add       dword [esi+20h],0FFF9C000h

LAB_0040c9e0:
  0040c9e0  66837e1001            cmp       word [esi+10h],1
  0040c9e5  752e                  jne       short 0040CA15h
  0040c9e7  66ff4612              inc       word [esi+12h]
  0040c9eb  e850a71300            call      00547140h
  0040c9f0  99                    cdq
  0040c9f1  b905000000            mov       ecx,5
  0040c9f6  f7f9                  idiv      ecx
  0040c9f8  6683fa01              cmp       dx,1
  0040c9fc  7407                  je        short 0040CA05h
  0040c9fe  66837e1203            cmp       word [esi+12h],3
  0040ca03  7510                  jne       short 0040CA15h

LAB_0040ca05:
  0040ca05  8b06                  mov       eax,[esi]
  0040ca07  66c746100200          mov       word [esi+10h],2
  0040ca0d  80e4bf                and       ah,0BFh
  0040ca10  8906                  mov       [esi],eax
  0040ca12  5f                    pop       edi
  0040ca13  5e                    pop       esi
  0040ca14  c3                    ret

LAB_0040ca15:
  0040ca15  66837e1002            cmp       word [esi+10h],2
  0040ca1a  755c                  jne       short 0040CA78h
  0040ca1c  0fbf5632              movsx     edx,word [esi+32h]
  0040ca20  52                    push      edx
  0040ca21  e8f68f1300            call      00545A1Ch
  0040ca26  0fbf4e14              movsx     ecx,word [esi+14h]
  0040ca2a  ba11000000            mov       edx,11h
  0040ca2f  2bd1                  sub       edx,ecx
  0040ca31  0fafc2                imul      eax,edx
  0040ca34  8b561c                mov       edx,[esi+1Ch]
  0040ca37  0fbf7e14              movsx     edi,word [esi+14h]
  0040ca3b  03d0                  add       edx,eax
  0040ca3d  0fbf4632              movsx     eax,word [esi+32h]
  0040ca41  50                    push      eax
  0040ca42  89561c                mov       [esi+1Ch],edx
  0040ca45  e8cc8f1300            call      00545A16h
  0040ca4a  b911000000            mov       ecx,11h
  0040ca4f  8b5620                mov       edx,[esi+20h]
  0040ca52  2bcf                  sub       ecx,edi
  0040ca54  0fafc1                imul      eax,ecx
  0040ca57  8b4e24                mov       ecx,[esi+24h]
  0040ca5a  c1e70c                shl       edi,0Ch
  0040ca5d  03c8                  add       ecx,eax
  0040ca5f  03d7                  add       edx,edi
  0040ca61  66ff4612              inc       word [esi+12h]
  0040ca65  66837e120f            cmp       word [esi+12h],0Fh
  0040ca6a  894e24                mov       [esi+24h],ecx
  0040ca6d  895620                mov       [esi+20h],edx
  0040ca70  7c06                  jl        short 0040CA78h
  0040ca72  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_0040ca78:
  0040ca78  5f                    pop       edi
  0040ca79  5e                    pop       esi
  0040ca7a  c3                    ret
  0040ca7b  90                    nop
  0040ca7c  90                    nop
  0040ca7d  90                    nop
  0040ca7e  90                    nop
  0040ca7f  90                    nop

5 cible(s) de saut interne : 0x0040c9ac, 0x0040c9e0, 0x0040ca05, 0x0040ca15, 0x0040ca78
