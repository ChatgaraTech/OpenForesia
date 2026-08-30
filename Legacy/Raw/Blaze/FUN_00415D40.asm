; 0x00415d40 — le panache qui enfle et s'eteint (57 instructions, absente de Blaze.c).
; Jumeau : 0x004157a0, identique a un 'mov [eax+0Bh],cl' redondant pres.

Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00415d40  section .text  offset fichier 0x15140  180 octets

  00415d40  8b442404              mov       eax,[esp+4]
  00415d44  33c9                  xor       ecx,ecx
  00415d46  53                    push      ebx
  00415d47  56                    push      esi
  00415d48  66394810              cmp       [eax+10h],cx
  00415d4c  ba01000000            mov       edx,1
  00415d51  7544                  jne       short 00415D97h
  00415d53  8a5814                mov       bl,[eax+14h]
  00415d56  66395014              cmp       [eax+14h],dx
  00415d5a  66895010              mov       [eax+10h],dx
  00415d5e  885804                mov       [eax+4],bl
  00415d61  7506                  jne       short 00415D69h
  00415d63  c6400a02              mov       byte [eax+0Ah],2
  00415d67  eb04                  jmp       short 00415D6Dh

LAB_00415d69:
  00415d69  c6400a09              mov       byte [eax+0Ah],9

LAB_00415d6d:
  00415d6d  88480b                mov       [eax+0Bh],cl
  00415d70  66894828              mov       [eax+28h],cx
  00415d74  6689482a              mov       [eax+2Ah],cx
  00415d78  6689482c              mov       [eax+2Ch],cx
  00415d7c  66894834              mov       [eax+34h],cx
  00415d80  8b4820                mov       ecx,[eax+20h]
  00415d83  81c100400100          add       ecx,14000h
  00415d89  894820                mov       [eax+20h],ecx
  00415d8c  b1ff                  mov       cl,0FFh
  00415d8e  884838                mov       [eax+38h],cl
  00415d91  884839                mov       [eax+39h],cl
  00415d94  88483a                mov       [eax+3Ah],cl

LAB_00415d97:
  00415d97  66395010              cmp       [eax+10h],dx
  00415d9b  7553                  jne       short 00415DF0h
  00415d9d  8a4804                mov       cl,[eax+4]
  00415da0  3aca                  cmp       cl,dl
  00415da2  b990010000            mov       ecx,190h
  00415da7  7405                  je        short 00415DAEh
  00415da9  b920030000            mov       ecx,320h

LAB_00415dae:
  00415dae  66014828              add       [eax+28h],cx
  00415db2  6601482a              add       [eax+2Ah],cx
  00415db6  6601482c              add       [eax+2Ch],cx
  00415dba  660fb64838            movzx     cx,byte [eax+38h]
  00415dbf  660fb65039            movzx     dx,byte [eax+39h]
  00415dc4  660fb6583a            movzx     bx,byte [eax+3Ah]
  00415dc9  83e906                sub       ecx,6
  00415dcc  83ea06                sub       edx,6
  00415dcf  83eb06                sub       ebx,6
  00415dd2  6685c9                test      cx,cx
  00415dd5  7d10                  jge       short 00415DE7h
  00415dd7  8b30                  mov       esi,[eax]
  00415dd9  33c9                  xor       ecx,ecx
  00415ddb  33d2                  xor       edx,edx
  00415ddd  33db                  xor       ebx,ebx
  00415ddf  81e6ffffff7f          and       esi,7FFFFFFFh
  00415de5  8930                  mov       [eax],esi

LAB_00415de7:
  00415de7  884838                mov       [eax+38h],cl
  00415dea  885039                mov       [eax+39h],dl
  00415ded  88583a                mov       [eax+3Ah],bl

LAB_00415df0:
  00415df0  5e                    pop       esi
  00415df1  5b                    pop       ebx
  00415df2  c3                    ret
  00415df3  90                    nop

6 cible(s) de saut interne : 0x00415d69, 0x00415d6d, 0x00415d97, 0x00415dae, 0x00415de7, 0x00415df0
