
Legacy/Binaries/Blaze.exe  base 0x00400000
0x0047ea80  section .text  offset fichier 0x7de80  83 octets

  0047ea80  8b442404              mov       eax,[esp+4]
  0047ea84  56                    push      esi
  0047ea85  668b5010              mov       dx,[eax+10h]
  0047ea89  6685d2                test      dx,dx
  0047ea8c  7516                  jne       short 0047EAA4h
  0047ea8e  c7402800100060        mov       dword [eax+28h],60001000h
  0047ea95  66c7402c0010          mov       word [eax+2Ch],1000h
  0047ea9b  c74038f0f0f000        mov       dword [eax+38h],0F0F0F0h
  0047eaa2  eb15                  jmp       short 0047EAB9h

LAB_0047eaa4:
  0047eaa4  668b4828              mov       cx,[eax+28h]
  0047eaa8  668bf1                mov       si,cx
  0047eaab  66c1fe02              sar       si,2
  0047eaaf  2bce                  sub       ecx,esi
  0047eab1  66894828              mov       [eax+28h],cx
  0047eab5  6689482c              mov       [eax+2Ch],cx

LAB_0047eab9:
  0047eab9  6683403240            add       word [eax+32h],40h
  0047eabe  668178280001          cmp       word [eax+28h],100h
  0047eac4  7d06                  jge       short 0047EACCh
  0047eac6  c70000000000          mov       dword [eax],0

LAB_0047eacc:
  0047eacc  42                    inc       edx
  0047eacd  5e                    pop       esi
  0047eace  66895010              mov       [eax+10h],dx
  0047ead2  c3                    ret

3 cible(s) de saut interne : 0x0047eaa4, 0x0047eab9, 0x0047eacc
