Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00437bb0  section .text  offset fichier 0x36fb0  208 octets

  00437bb0  8b442404              mov       eax,[esp+4]
  00437bb4  33c9                  xor       ecx,ecx
  00437bb6  53                    push      ebx
  00437bb7  56                    push      esi
  00437bb8  66394810              cmp       [eax+10h],cx
  00437bbc  ba01000000            mov       edx,1
  00437bc1  755a                  jne       short 00437C1Dh
  00437bc3  8a5814                mov       bl,[eax+14h]
  00437bc6  66395014              cmp       [eax+14h],dx
  00437bca  66895010              mov       [eax+10h],dx
  00437bce  885804                mov       [eax+4],bl
  00437bd1  741c                  je        short 00437BEFh
  00437bd3  803d2ce55d0010        cmp       byte [5DE52Ch],10h
  00437bda  7506                  jne       short 00437BE2h
  00437bdc  c6400a0d              mov       byte [eax+0Ah],0Dh
  00437be0  eb04                  jmp       short 00437BE6h

LAB_00437be2:
  00437be2  c6400a0f              mov       byte [eax+0Ah],0Fh

LAB_00437be6:
  00437be6  803d2ce55d0004        cmp       byte [5DE52Ch],4
  00437bed  7504                  jne       short 00437BF3h

LAB_00437bef:
  00437bef  c6400a02              mov       byte [eax+0Ah],2

LAB_00437bf3:
  00437bf3  88480b                mov       [eax+0Bh],cl
  00437bf6  66894828              mov       [eax+28h],cx
  00437bfa  6689482a              mov       [eax+2Ah],cx
  00437bfe  6689482c              mov       [eax+2Ch],cx
  00437c02  66894834              mov       [eax+34h],cx
  00437c06  8b4820                mov       ecx,[eax+20h]
  00437c09  81c100400100          add       ecx,14000h
  00437c0f  894820                mov       [eax+20h],ecx
  00437c12  b1ff                  mov       cl,0FFh
  00437c14  884838                mov       [eax+38h],cl
  00437c17  884839                mov       [eax+39h],cl
  00437c1a  88483a                mov       [eax+3Ah],cl

LAB_00437c1d:
  00437c1d  66395010              cmp       [eax+10h],dx
  00437c21  7553                  jne       short 00437C76h
  00437c23  8a4804                mov       cl,[eax+4]
  00437c26  3aca                  cmp       cl,dl
  00437c28  b990010000            mov       ecx,190h
  00437c2d  7405                  je        short 00437C34h
  00437c2f  b920030000            mov       ecx,320h

LAB_00437c34:
  00437c34  66014828              add       [eax+28h],cx
  00437c38  6601482a              add       [eax+2Ah],cx
  00437c3c  6601482c              add       [eax+2Ch],cx
  00437c40  660fb64838            movzx     cx,byte [eax+38h]
  00437c45  660fb65039            movzx     dx,byte [eax+39h]
  00437c4a  660fb6583a            movzx     bx,byte [eax+3Ah]
  00437c4f  83e906                sub       ecx,6
  00437c52  83ea06                sub       edx,6
  00437c55  83eb06                sub       ebx,6
  00437c58  6685c9                test      cx,cx
  00437c5b  7d10                  jge       short 00437C6Dh
  00437c5d  8b30                  mov       esi,[eax]
  00437c5f  33c9                  xor       ecx,ecx
  00437c61  33d2                  xor       edx,edx
  00437c63  33db                  xor       ebx,ebx
  00437c65  81e6ffffff7f          and       esi,7FFFFFFFh
  00437c6b  8930                  mov       [eax],esi

LAB_00437c6d:
  00437c6d  884838                mov       [eax+38h],cl
  00437c70  885039                mov       [eax+39h],dl
  00437c73  88583a                mov       [eax+3Ah],bl

LAB_00437c76:
  00437c76  5e                    pop       esi
  00437c77  5b                    pop       ebx
  00437c78  c3                    ret
  00437c79  90                    nop
  00437c7a  90                    nop
  00437c7b  90                    nop
  00437c7c  90                    nop
  00437c7d  90                    nop
  00437c7e  90                    nop
  00437c7f  90                    nop

8 cible(s) de saut interne : 0x00437be2, 0x00437be6, 0x00437bef, 0x00437bf3, 0x00437c1d, 0x00437c34, 0x00437c6d, 0x00437c76
