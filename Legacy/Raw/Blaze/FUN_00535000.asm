Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00535000  section .text  offset fichier 0x134400  65 octets

  00535000  8b0d20905e00          mov       ecx,[5E9020h]
  00535006  33c0                  xor       eax,eax
  00535008  85c9                  test      ecx,ecx
  0053500a  bac0935e00            mov       edx,5E93C0h
  0053500f  742f                  je        short 00535040h
  00535011  57                    push      edi
  00535012  8b7c240c              mov       edi,[esp+0Ch]
  00535016  56                    push      esi
  00535017  55                    push      ebp
  00535018  8b6c2410              mov       ebp,[esp+10h]
  0053501c  53                    push      ebx
  0053501d  8b5c241c              mov       ebx,[esp+1Ch]
  00535021  8bf1                  mov       esi,ecx

LAB_00535023:
  00535023  396a78                cmp       [edx+78h],ebp
  00535026  750b                  jne       short 00535033h
  00535028  8b0a                  mov       ecx,[edx]
  0053502a  85f9                  test      ecx,edi
  0053502c  7405                  je        short 00535033h
  0053502e  85d9                  test      ecx,ebx
  00535030  7501                  jne       short 00535033h
  00535032  40                    inc       eax

LAB_00535033:
  00535033  81c29c000000          add       edx,9Ch
  00535039  4e                    dec       esi
  0053503a  75e7                  jne       short 00535023h
  0053503c  5b                    pop       ebx
  0053503d  5d                    pop       ebp
  0053503e  5e                    pop       esi
  0053503f  5f                    pop       edi

LAB_00535040:
  00535040  c3                    ret

3 cible(s) de saut interne : 0x00535023, 0x00535033, 0x00535040
