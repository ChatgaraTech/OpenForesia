Legacy/Binaries/Blaze.exe  base 0x00400000
0x0051a6b0  section .text  offset fichier 0x119ab0  138 octets

  0051a6b0  8b442404              mov       eax,[esp+4]
  0051a6b4  56                    push      esi
  0051a6b5  8b08                  mov       ecx,[eax]
  0051a6b7  f6c102                test      cl,2
  0051a6ba  743f                  je        short 0051A6FBh
  0051a6bc  8b5018                mov       edx,[eax+18h]
  0051a6bf  8b742410              mov       esi,[esp+10h]
  0051a6c3  81fa03001000          cmp       edx,100003h
  0051a6c9  7509                  jne       short 0051A6D4h
  0051a6cb  f6865001000008        test      byte [esi+150h],8
  0051a6d2  7411                  je        short 0051A6E5h

LAB_0051a6d4:
  0051a6d4  81fa03000c00          cmp       edx,0C0003h
  0051a6da  751f                  jne       short 0051A6FBh
  0051a6dc  f6865001000008        test      byte [esi+150h],8
  0051a6e3  7416                  je        short 0051A6FBh

LAB_0051a6e5:
  0051a6e5  6aff                  push      0FFFFFFFFh
  0051a6e7  6aff                  push      0FFFFFFFFh
  0051a6e9  6a00                  push      0
  0051a6eb  80cd08                or        ch,8
  0051a6ee  6a05                  push      5
  0051a6f0  50                    push      eax
  0051a6f1  8908                  mov       [eax],ecx
  0051a6f3  e888020200            call      0053A980h
  0051a6f8  83c414                add       esp,14h

LAB_0051a6fb:
  0051a6fb  5e                    pop       esi
  0051a6fc  c3                    ret
  0051a6fd  90                    nop
  0051a6fe  90                    nop
  0051a6ff  90                    nop
  0051a700  8b4c2404              mov       ecx,[esp+4]
  0051a704  8b01                  mov       eax,[ecx]
  0051a706  a801                  test      al,1
  0051a708  7404                  je        short 0051A70Eh
  0051a70a  24fb                  and       al,0FBh
  0051a70c  8901                  mov       [ecx],eax

LAB_0051a70e:
  0051a70e  c3                    ret
  0051a70f  90                    nop
  0051a710  8b4c2404              mov       ecx,[esp+4]
  0051a714  8b01                  mov       eax,[ecx]
  0051a716  a802                  test      al,2
  0051a718  741f                  je        short 0051A739h
  0051a71a  81791803000300        cmp       dword [ecx+18h],30003h
  0051a721  7516                  jne       short 0051A739h
  0051a723  6aff                  push      0FFFFFFFFh
  0051a725  6aff                  push      0FFFFFFFFh
  0051a727  6a00                  push      0
  0051a729  80cc08                or        ah,8
  0051a72c  6a06                  push      6
  0051a72e  51                    push      ecx
  0051a72f  8901                  mov       [ecx],eax
  0051a731  e84a020200            call      0053A980h
  0051a736  83c414                add       esp,14h

LAB_0051a739:
  0051a739  c3                    ret

5 cible(s) de saut interne : 0x0051a6d4, 0x0051a6e5, 0x0051a6fb, 0x0051a70e, 0x0051a739
