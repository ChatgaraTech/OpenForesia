Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00470860  section .text  offset fichier 0x6fc60  160 octets

  00470860  8b442404              mov       eax,[esp+4]
  00470864  8a1578195700          mov       dl,[571978h]
  0047086a  56                    push      esi
  0047086b  668b4814              mov       cx,[eax+14h]
  0047086f  6683f901              cmp       cx,1
  00470873  7505                  jne       short 0047087Ah
  00470875  f6c201                test      dl,1
  00470878  747b                  je        short 004708F5h

LAB_0047087a:
  0047087a  6683f902              cmp       cx,2
  0047087e  7505                  jne       short 00470885h
  00470880  f6c208                test      dl,8
  00470883  7470                  je        short 004708F5h

LAB_00470885:
  00470885  6683f903              cmp       cx,3
  00470889  7505                  jne       short 00470890h
  0047088b  f6c240                test      dl,40h
  0047088e  7465                  je        short 004708F5h

LAB_00470890:
  00470890  668b4810              mov       cx,[eax+10h]
  00470894  6683f902              cmp       cx,2
  00470898  745b                  je        short 004708F5h
  0047089a  6685c9                test      cx,cx
  0047089d  7519                  jne       short 004708B8h
  0047089f  66894812              mov       [eax+12h],cx
  004708a3  668b4832              mov       cx,[eax+32h]
  004708a7  66c740100100          mov       word [eax+10h],1
  004708ad  c7405c28000000        mov       dword [eax+5Ch],28h
  004708b4  66894816              mov       [eax+16h],cx

LAB_004708b8:
  004708b8  6683781001            cmp       word [eax+10h],1
  004708bd  7536                  jne       short 004708F5h
  004708bf  8b485c                mov       ecx,[eax+5Ch]
  004708c2  668b505c              mov       dx,[eax+5Ch]
  004708c6  66015032              add       [eax+32h],dx
  004708ca  83c10a                add       ecx,0Ah
  004708cd  0fbf7032              movsx     esi,word [eax+32h]
  004708d1  89485c                mov       [eax+5Ch],ecx
  004708d4  668b4816              mov       cx,[eax+16h]
  004708d8  0fbfd1                movsx     edx,cx
  004708db  81c200040000          add       edx,400h
  004708e1  3bf2                  cmp       esi,edx
  004708e3  7c10                  jl        short 004708F5h
  004708e5  81c100040000          add       ecx,400h
  004708eb  66c740100200          mov       word [eax+10h],2
  004708f1  66894832              mov       [eax+32h],cx

LAB_004708f5:
  004708f5  5e                    pop       esi
  004708f6  c3                    ret
  004708f7  90                    nop
  004708f8  90                    nop
  004708f9  90                    nop
  004708fa  90                    nop
  004708fb  90                    nop
  004708fc  90                    nop
  004708fd  90                    nop
  004708fe  90                    nop
  004708ff  90                    nop

5 cible(s) de saut interne : 0x0047087a, 0x00470885, 0x00470890, 0x004708b8, 0x004708f5
