Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x004423d0  section .text  offset fichier 0x417d0  224 octets

  004423d0  8b442404              mov       eax,[esp+4]
  004423d4  56                    push      esi
  004423d5  33f6                  xor       esi,esi
  004423d7  b901000000            mov       ecx,1
  004423dc  66397010              cmp       [eax+10h],si
  004423e0  752a                  jne       short 0044240Ch
  004423e2  8b5020                mov       edx,[eax+20h]
  004423e5  66894810              mov       [eax+10h],cx
  004423e9  81c200400100          add       edx,14000h
  004423ef  884804                mov       [eax+4],cl
  004423f2  c6400a03              mov       byte [eax+0Ah],3
  004423f6  88480b                mov       [eax+0Bh],cl
  004423f9  66897012              mov       [eax+12h],si
  004423fd  895020                mov       [eax+20h],edx
  00442400  6689702a              mov       [eax+2Ah],si
  00442404  66897028              mov       [eax+28h],si
  00442408  6689702c              mov       [eax+2Ch],si

LAB_0044240c:
  0044240c  66394810              cmp       [eax+10h],cx
  00442410  7530                  jne       short 00442442h
  00442412  668340120a            add       word [eax+12h],0Ah
  00442417  668b4812              mov       cx,[eax+12h]
  0044241b  66014828              add       [eax+28h],cx
  0044241f  668b5028              mov       dx,[eax+28h]
  00442423  66814032c800          add       word [eax+32h],0C8h
  00442429  6601482a              add       [eax+2Ah],cx
  0044242d  6601482c              add       [eax+2Ch],cx
  00442431  6681faa861            cmp       dx,61A8h
  00442436  7e0a                  jle       short 00442442h
  00442438  66c740100200          mov       word [eax+10h],2
  0044243e  66897012              mov       [eax+12h],si

LAB_00442442:
  00442442  6683781002            cmp       word [eax+10h],2
  00442447  7520                  jne       short 00442469h
  00442449  66ff4012              inc       word [eax+12h]
  0044244d  668b4812              mov       cx,[eax+12h]
  00442451  66814032c800          add       word [eax+32h],0C8h
  00442457  6683f902              cmp       cx,2
  0044245b  7c0c                  jl        short 00442469h
  0044245d  66c740100300          mov       word [eax+10h],3
  00442463  66c740121e00          mov       word [eax+12h],1Eh

LAB_00442469:
  00442469  6683781003            cmp       word [eax+10h],3
  0044246e  753a                  jne       short 004424AAh
  00442470  6683401228            add       word [eax+12h],28h
  00442475  668b4812              mov       cx,[eax+12h]
  00442479  66294828              sub       [eax+28h],cx
  0044247d  668b5028              mov       dx,[eax+28h]
  00442481  66814032c800          add       word [eax+32h],0C8h
  00442487  6629482a              sub       [eax+2Ah],cx
  0044248b  6629482c              sub       [eax+2Ch],cx
  0044248f  663bd6                cmp       dx,si
  00442492  7d16                  jge       short 004424AAh
  00442494  8b08                  mov       ecx,[eax]
  00442496  66897028              mov       [eax+28h],si
  0044249a  81e1ffffff7f          and       ecx,7FFFFFFFh
  004424a0  6689702a              mov       [eax+2Ah],si
  004424a4  6689702c              mov       [eax+2Ch],si
  004424a8  8908                  mov       [eax],ecx

LAB_004424aa:
  004424aa  66ff4016              inc       word [eax+16h]
  004424ae  5e                    pop       esi
  004424af  c3                    ret

4 cible(s) de saut interne : 0x0044240c, 0x00442442, 0x00442469, 0x004424aa
