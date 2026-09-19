Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00441450  section .text  offset fichier 0x40850  144 octets

  00441450  8b442404              mov       eax,[esp+4]
  00441454  33c9                  xor       ecx,ecx
  00441456  66394810              cmp       [eax+10h],cx
  0044145a  7529                  jne       short 00441485h
  0044145c  8a15c4de5e00          mov       dl,[5EDEC4h]
  00441462  c6400a04              mov       byte [eax+0Ah],4
  00441466  80c204                add       dl,4
  00441469  88480b                mov       [eax+0Bh],cl
  0044146c  885004                mov       [eax+4],dl
  0044146f  66894828              mov       [eax+28h],cx
  00441473  6689482a              mov       [eax+2Ah],cx
  00441477  6689482c              mov       [eax+2Ch],cx
  0044147b  66c740100100          mov       word [eax+10h],1
  00441481  66894812              mov       [eax+12h],cx

LAB_00441485:
  00441485  6683781001            cmp       word [eax+10h],1
  0044148a  baf4010000            mov       edx,1F4h
  0044148f  7522                  jne       short 004414B3h
  00441491  66814032c800          add       word [eax+32h],0C8h
  00441497  66015028              add       [eax+28h],dx
  0044149b  6601502c              add       [eax+2Ch],dx
  0044149f  6601502a              add       [eax+2Ah],dx
  004414a3  66ff4814              dec       word [eax+14h]
  004414a7  66394814              cmp       [eax+14h],cx
  004414ab  7506                  jne       short 004414B3h
  004414ad  66c740100200          mov       word [eax+10h],2

LAB_004414b3:
  004414b3  6683781002            cmp       word [eax+10h],2
  004414b8  7506                  jne       short 004414C0h
  004414ba  66814032c800          add       word [eax+32h],0C8h

LAB_004414c0:
  004414c0  66ff4016              inc       word [eax+16h]
  004414c4  66395016              cmp       [eax+16h],dx
  004414c8  7c06                  jl        short 004414D0h
  004414ca  8120ffffff7f          and       dword [eax],7FFFFFFFh

LAB_004414d0:
  004414d0  c3                    ret
  004414d1  90                    nop
  004414d2  90                    nop
  004414d3  90                    nop
  004414d4  90                    nop
  004414d5  90                    nop
  004414d6  90                    nop
  004414d7  90                    nop
  004414d8  90                    nop
  004414d9  90                    nop
  004414da  90                    nop
  004414db  90                    nop
  004414dc  90                    nop
  004414dd  90                    nop
  004414de  90                    nop
  004414df  90                    nop

4 cible(s) de saut interne : 0x00441485, 0x004414b3, 0x004414c0, 0x004414d0
