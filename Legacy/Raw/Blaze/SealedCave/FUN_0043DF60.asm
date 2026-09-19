Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x0043df60  section .text  offset fichier 0x3d360  208 octets

  0043df60  8b442404              mov       eax,[esp+4]
  0043df64  33c9                  xor       ecx,ecx
  0043df66  53                    push      ebx
  0043df67  56                    push      esi
  0043df68  66394810              cmp       [eax+10h],cx
  0043df6c  ba01000000            mov       edx,1
  0043df71  7553                  jne       short 0043DFC6h
  0043df73  8a5814                mov       bl,[eax+14h]
  0043df76  66395014              cmp       [eax+14h],dx
  0043df7a  66895010              mov       [eax+10h],dx
  0043df7e  885804                mov       [eax+4],bl
  0043df81  7506                  jne       short 0043DF89h
  0043df83  c6400a02              mov       byte [eax+0Ah],2
  0043df87  eb13                  jmp       short 0043DF9Ch

LAB_0043df89:
  0043df89  803d2ce55d0010        cmp       byte [5DE52Ch],10h
  0043df90  7506                  jne       short 0043DF98h
  0043df92  c6400a0d              mov       byte [eax+0Ah],0Dh
  0043df96  eb04                  jmp       short 0043DF9Ch

LAB_0043df98:
  0043df98  c6400a0f              mov       byte [eax+0Ah],0Fh

LAB_0043df9c:
  0043df9c  88480b                mov       [eax+0Bh],cl
  0043df9f  66894828              mov       [eax+28h],cx
  0043dfa3  6689482a              mov       [eax+2Ah],cx
  0043dfa7  6689482c              mov       [eax+2Ch],cx
  0043dfab  66894834              mov       [eax+34h],cx
  0043dfaf  8b4820                mov       ecx,[eax+20h]
  0043dfb2  81c100400100          add       ecx,14000h
  0043dfb8  894820                mov       [eax+20h],ecx
  0043dfbb  b1ff                  mov       cl,0FFh
  0043dfbd  884838                mov       [eax+38h],cl
  0043dfc0  884839                mov       [eax+39h],cl
  0043dfc3  88483a                mov       [eax+3Ah],cl

LAB_0043dfc6:
  0043dfc6  66395010              cmp       [eax+10h],dx
  0043dfca  7553                  jne       short 0043E01Fh
  0043dfcc  8a4804                mov       cl,[eax+4]
  0043dfcf  3aca                  cmp       cl,dl
  0043dfd1  b990010000            mov       ecx,190h
  0043dfd6  7405                  je        short 0043DFDDh
  0043dfd8  b920030000            mov       ecx,320h

LAB_0043dfdd:
  0043dfdd  66014828              add       [eax+28h],cx
  0043dfe1  6601482a              add       [eax+2Ah],cx
  0043dfe5  6601482c              add       [eax+2Ch],cx
  0043dfe9  660fb64838            movzx     cx,byte [eax+38h]
  0043dfee  660fb65039            movzx     dx,byte [eax+39h]
  0043dff3  660fb6583a            movzx     bx,byte [eax+3Ah]
  0043dff8  83e906                sub       ecx,6
  0043dffb  83ea06                sub       edx,6
  0043dffe  83eb06                sub       ebx,6
  0043e001  6685c9                test      cx,cx
  0043e004  7d10                  jge       short 0043E016h
  0043e006  8b30                  mov       esi,[eax]
  0043e008  33c9                  xor       ecx,ecx
  0043e00a  33d2                  xor       edx,edx
  0043e00c  33db                  xor       ebx,ebx
  0043e00e  81e6ffffff7f          and       esi,7FFFFFFFh
  0043e014  8930                  mov       [eax],esi

LAB_0043e016:
  0043e016  884838                mov       [eax+38h],cl
  0043e019  885039                mov       [eax+39h],dl
  0043e01c  88583a                mov       [eax+3Ah],bl

LAB_0043e01f:
  0043e01f  5e                    pop       esi
  0043e020  5b                    pop       ebx
  0043e021  c3                    ret
  0043e022  90                    nop
  0043e023  90                    nop
  0043e024  90                    nop
  0043e025  90                    nop
  0043e026  90                    nop
  0043e027  90                    nop
  0043e028  90                    nop
  0043e029  90                    nop
  0043e02a  90                    nop
  0043e02b  90                    nop
  0043e02c  90                    nop
  0043e02d  90                    nop
  0043e02e  90                    nop
  0043e02f  90                    nop

7 cible(s) de saut interne : 0x0043df89, 0x0043df98, 0x0043df9c, 0x0043dfc6, 0x0043dfdd, 0x0043e016, 0x0043e01f
