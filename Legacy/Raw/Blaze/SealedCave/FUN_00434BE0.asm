Fichier     : D:\Open Foresia Project\GitHub Project\OpenForesia\Legacy\Binaries\Blaze.exe
Image base  : 0x00400000

Legacy/Binaries/Blaze.exe  base 0x00400000
0x00434be0  section .text  offset fichier 0x33fe0  160 octets

  00434be0  56                    push      esi
  00434be1  8b742408              mov       esi,[esp+8]
  00434be5  66837e1000            cmp       word [esi+10h],0
  00434bea  7553                  jne       short 00434C3Fh
  00434bec  e84f251100            call      00547140h
  00434bf1  99                    cdq
  00434bf2  33c2                  xor       eax,edx
  00434bf4  c6460a06              mov       byte [esi+0Ah],6
  00434bf8  2bc2                  sub       eax,edx
  00434bfa  c6460b00              mov       byte [esi+0Bh],0
  00434bfe  83e003                and       eax,3
  00434c01  66c74628bc34          mov       word [esi+28h],34BCh
  00434c07  33c2                  xor       eax,edx
  00434c09  66c74636feff          mov       word [esi+36h],0FFFEh
  00434c0f  2bc2                  sub       eax,edx
  00434c11  66c746100100          mov       word [esi+10h],1
  00434c17  0408                  add       al,8
  00434c19  884604                mov       [esi+4],al
  00434c1c  b870170000            mov       eax,1770h
  00434c21  6689462a              mov       [esi+2Ah],ax
  00434c25  6689462c              mov       [esi+2Ch],ax
  00434c29  8b4620                mov       eax,[esi+20h]
  00434c2c  0500800700            add       eax,78000h
  00434c31  894620                mov       [esi+20h],eax
  00434c34  b0c8                  mov       al,0C8h
  00434c36  884638                mov       [esi+38h],al
  00434c39  884639                mov       [esi+39h],al
  00434c3c  88463a                mov       [esi+3Ah],al

LAB_00434c3f:
  00434c3f  66837e1001            cmp       word [esi+10h],1
  00434c44  752e                  jne       short 00434C74h
  00434c46  e8f5241100            call      00547140h
  00434c4b  99                    cdq
  00434c4c  33c2                  xor       eax,edx
  00434c4e  2bc2                  sub       eax,edx
  00434c50  83e003                and       eax,3
  00434c53  33c2                  xor       eax,edx
  00434c55  2bc2                  sub       eax,edx
  00434c57  0408                  add       al,8
  00434c59  884604                mov       [esi+4],al
  00434c5c  a002975e00            mov       al,[5E9702h]
  00434c61  a802                  test      al,2
  00434c63  740f                  je        short 00434C74h
  00434c65  6af8                  push      0FFFFFFF8h
  00434c67  6afa                  push      0FFFFFFFAh
  00434c69  6af8                  push      0FFFFFFF8h
  00434c6b  56                    push      esi
  00434c6c  e86f01fdff            call      00404DE0h
  00434c71  83c410                add       esp,10h

LAB_00434c74:
  00434c74  5e                    pop       esi
  00434c75  c3                    ret
  00434c76  90                    nop
  00434c77  90                    nop
  00434c78  90                    nop
  00434c79  90                    nop
  00434c7a  90                    nop
  00434c7b  90                    nop
  00434c7c  90                    nop
  00434c7d  90                    nop
  00434c7e  90                    nop
  00434c7f  90                    nop

2 cible(s) de saut interne : 0x00434c3f, 0x00434c74
