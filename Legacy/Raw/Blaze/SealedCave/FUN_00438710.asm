Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00438710  section .text  offset fichier 0x37b10  320 octets

  00438710  53                    push      ebx
  00438711  56                    push      esi
  00438712  8b74240c              mov       esi,[esp+0Ch]
  00438716  33db                  xor       ebx,ebx
  00438718  66395e10              cmp       [esi+10h],bx
  0043871c  7564                  jne       short 00438782h
  0043871e  66817e140901          cmp       word [esi+14h],109h
  00438724  c6460408              mov       byte [esi+4],8
  00438728  c6460a09              mov       byte [esi+0Ah],9
  0043872c  885e0b                mov       [esi+0Bh],bl
  0043872f  66c746100100          mov       word [esi+10h],1
  00438735  66895e34              mov       [esi+34h],bx
  00438739  7508                  jne       short 00438743h
  0043873b  66c746320006          mov       word [esi+32h],600h
  00438741  eb06                  jmp       short 00438749h

LAB_00438743:
  00438743  66c746320002          mov       word [esi+32h],200h

LAB_00438749:
  00438749  b8a00f0000            mov       eax,0FA0h
  0043874e  66c74628141e          mov       word [esi+28h],1E14h
  00438754  6689462c              mov       [esi+2Ch],ax
  00438758  6689462a              mov       [esi+2Ah],ax
  0043875c  b0fa                  mov       al,0FAh
  0043875e  884638                mov       [esi+38h],al
  00438761  884639                mov       [esi+39h],al
  00438764  88463a                mov       [esi+3Ah],al
  00438767  e8d4e91000            call      00547140h
  0043876c  99                    cdq
  0043876d  b914000000            mov       ecx,14h
  00438772  66899e9a000000        mov       [esi+9Ah],bx
  00438779  f7f9                  idiv      ecx
  0043877b  66899698000000        mov       [esi+98h],dx

LAB_00438782:
  00438782  66ff869a000000        inc       word [esi+9Ah]
  00438789  66837e1001            cmp       word [esi+10h],1
  0043878e  0f85b2000000          jne       near 00438846h
  00438794  e8a7e91000            call      00547140h
  00438799  99                    cdq
  0043879a  33c2                  xor       eax,edx
  0043879c  0fbf4e14              movsx     ecx,word [esi+14h]
  004387a0  2bc2                  sub       eax,edx
  004387a2  83e003                and       eax,3
  004387a5  33c2                  xor       eax,edx
  004387a7  2bc2                  sub       eax,edx
  004387a9  0408                  add       al,8
  004387ab  884604                mov       [esi+4],al
  004387ae  8bc1                  mov       eax,ecx
  004387b0  99                    cdq
  004387b1  83e207                and       edx,7
  004387b4  03c2                  add       eax,edx
  004387b6  c1f803                sar       eax,3
  004387b9  0fbfc0                movsx     eax,ax
  004387bc  8d14c500000000        lea       edx,[eax*8]
  004387c3  8a80e0965e00          mov       al,[eax+5E96E0h]
  004387c9  2bca                  sub       ecx,edx
  004387cb  ba01000000            mov       edx,1
  004387d0  d3e2                  shl       edx,cl
  004387d2  84d0                  test      al,dl
  004387d4  742f                  je        short 00438805h
  004387d6  6af9                  push      0FFFFFFF9h
  004387d8  6af6                  push      0FFFFFFF6h
  004387da  6af6                  push      0FFFFFFF6h
  004387dc  56                    push      esi
  004387dd  e8fec5fcff            call      00404DE0h
  004387e2  8a463a                mov       al,[esi+3Ah]
  004387e5  83c410                add       esp,10h
  004387e8  3ac3                  cmp       al,bl
  004387ea  7506                  jne       short 004387F2h
  004387ec  8126ffffff7f          and       dword [esi],7FFFFFFFh

LAB_004387f2:
  004387f2  6683be9a0000000a      cmp       word [esi+9Ah],0Ah
  004387fa  7d4a                  jge       short 00438846h
  004387fc  8126ffffff7f          and       dword [esi],7FFFFFFFh
  00438802  5e                    pop       esi
  00438803  5b                    pop       ebx
  00438804  c3                    ret

LAB_00438805:
  00438805  66ff8698000000        inc       word [esi+98h]
  0043880c  6683be9800000027      cmp       word [esi+98h],27h
  00438814  7530                  jne       short 00438846h
  00438816  66899e98000000        mov       [esi+98h],bx
  0043881d  8b0dcc975e00          mov       ecx,[5E97CCh]
  00438823  53                    push      ebx
  00438824  53                    push      ebx
  00438825  8b91a8000000          mov       edx,[ecx+0A8h]
  0043882b  6800100000            push      1000h
  00438830  6800040000            push      400h
  00438835  8b02                  mov       eax,[edx]
  00438837  660fb64833            movzx     cx,byte [eax+33h]
  0043883c  51                    push      ecx
  0043883d  56                    push      esi
  0043883e  e83df80200            call      00468080h
  00438843  83c418                add       esp,18h

LAB_00438846:
  00438846  5e                    pop       esi
  00438847  5b                    pop       ebx
  00438848  c3                    ret
  00438849  90                    nop
  0043884a  90                    nop
  0043884b  90                    nop
  0043884c  90                    nop
  0043884d  90                    nop
  0043884e  90                    nop
  0043884f  90                    nop

6 cible(s) de saut interne : 0x00438743, 0x00438749, 0x00438782, 0x004387f2, 0x00438805, 0x00438846
