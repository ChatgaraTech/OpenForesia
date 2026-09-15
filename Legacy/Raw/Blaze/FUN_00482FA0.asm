Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00482fa0  section .text  offset fichier 0x823a0  79 octets

  00482fa0  8b442404              mov       eax,[esp+4]
  00482fa4  56                    push      esi
  00482fa5  668b5010              mov       dx,[eax+10h]
  00482fa9  6685d2                test      dx,dx
  00482fac  7517                  jne       short 00482FC5h
  00482fae  c7403ca0195500        mov       dword [eax+3Ch],5519A0h
  00482fb5  c7402800100010        mov       dword [eax+28h],10001000h
  00482fbc  c74038f0f0f000        mov       dword [eax+38h],0F0F0F0h
  00482fc3  eb15                  jmp       short 00482FDAh

LAB_00482fc5:
  00482fc5  668b4828              mov       cx,[eax+28h]
  00482fc9  668bf1                mov       si,cx
  00482fcc  66c1fe02              sar       si,2
  00482fd0  2bce                  sub       ecx,esi
  00482fd2  66894828              mov       [eax+28h],cx
  00482fd6  6689482a              mov       [eax+2Ah],cx

LAB_00482fda:
  00482fda  668178280001          cmp       word [eax+28h],100h
  00482fe0  7d06                  jge       short 00482FE8h
  00482fe2  c70000000000          mov       dword [eax],0

LAB_00482fe8:
  00482fe8  42                    inc       edx
  00482fe9  5e                    pop       esi
  00482fea  66895010              mov       [eax+10h],dx
  00482fee  c3                    ret

3 cible(s) de saut interne : 0x00482fc5, 0x00482fda, 0x00482fe8
